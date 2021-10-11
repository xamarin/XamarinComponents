using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MavenNet;
using MavenNet.Models;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using RazorLight;
using MavenGroup = MavenNet.Models.Group;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace AndroidBinderator
{
	public class Engine
	{
		public static Task BinderateAsync(string configFile, string basePath = null)
		{
			var config = Newtonsoft.Json.JsonConvert.DeserializeObject<BindingConfig>(File.ReadAllText(configFile));

			if (!string.IsNullOrEmpty(basePath))
				config.BasePath = basePath;

			return BinderateAsync(config);
		}

		public static async Task BinderateAsync(BindingConfig config)
		{
			// Prime our Maven repositories
			await MavenFactory.Initialize(config);

			if (config.DownloadExternals)
			{
				var artifactDir = Path.Combine(config.BasePath, config.ExternalsDir);
				if (!Directory.Exists(artifactDir))
					Directory.CreateDirectory(artifactDir);
			}

			await ProcessConfig(config);
		}

		static async Task ProcessConfig(BindingConfig config)
		{
			var mavenProjects = new Dictionary<string, Project>();
			var mavenGroups = new List<MavenGroup>();

			foreach (var artifact in config.MavenArtifacts)
			{
				if (artifact.DependencyOnly)
					continue;

				var maven = MavenFactory.GetMavenRepository (config, artifact);

				var mavenGroup = maven.Groups.FirstOrDefault(g => g.Id == artifact.GroupId);

				Project project = null;

				project = await maven.GetProjectAsync(artifact.GroupId, artifact.ArtifactId, artifact.Version);

				if (project != null)
					mavenProjects.Add($"{artifact.GroupId}/{artifact.ArtifactId}-{artifact.Version}", project);
			}

			if (config.DownloadExternals)
				await DownloadArtifacts(config, mavenProjects);

			var slnProjModels = new Dictionary<string, BindingProjectModel>();
			var models = BuildProjectModels(config, mavenProjects);

			var json = Newtonsoft.Json.JsonConvert.SerializeObject(models);

			if (config.Debug.DumpModels)
				File.WriteAllText(Path.Combine(config.BasePath, "models.json"), json);

			var engine = new RazorLightEngineBuilder()
				.UseMemoryCachingProvider()
				.Build();

			foreach (var model in models) {
				var template_set = config.GetTemplateSet(model.MavenArtifacts.FirstOrDefault()?.MavenArtifactConfig?.TemplateSet);

				foreach (var template in template_set.Templates) {
					var inputTemplateFile = Path.Combine(config.BasePath, template.TemplateFile);
					var templateSrc = File.ReadAllText(inputTemplateFile);

					AssignMetadata(model, template);

					var outputFile = new FileInfo (template.GetOutputFile (config, model));
					if (!outputFile.Directory.Exists)
						outputFile.Directory.Create();

					string result = await engine.CompileRenderAsync(inputTemplateFile, templateSrc, model);

					File.WriteAllText(outputFile.FullName, result);

					// We want to collect all the models for the .csproj's so we can add them to a .sln file after
					if (!slnProjModels.ContainsKey(outputFile.FullName) && template.OutputFileRule.EndsWith(".csproj"))
						slnProjModels.Add(outputFile.FullName, model);
				}
			}

			if (!string.IsNullOrEmpty(config.SolutionFile))
			{
				var slnPath = Path.Combine(config.BasePath ?? AppDomain.CurrentDomain.BaseDirectory, config.SolutionFile);
				var sln = SolutionFileBuilder.Build(config, slnProjModels);
				File.WriteAllText(slnPath, sln);
			}
		}

		static async Task DownloadArtifacts(BindingConfig config, Dictionary<string, Project> mavenProjects)
		{
			var httpClient = new HttpClient();

			foreach (var mavenArtifact in config.MavenArtifacts)
			{
                // Skip downloading dependencies
                if (mavenArtifact.DependencyOnly)
                    continue;

				var maven = MavenFactory.GetMavenRepository(config, mavenArtifact);
				var version = mavenArtifact.Version;

				if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
					continue;

				var artifactDir = Path.Combine(
					config.BasePath,
					config.ExternalsDir,
					mavenArtifact.GroupId);

				var artifactExtractDir = Path.Combine(artifactDir, mavenArtifact.ArtifactId);

				Directory.CreateDirectory(artifactDir);
				Directory.CreateDirectory(artifactExtractDir);

				var mvnArt = maven.Groups.FirstOrDefault(g => g.Id == mavenArtifact.GroupId)?.Artifacts?.FirstOrDefault(a => a.Id == mavenArtifact.ArtifactId);

				// Download artifact
				var artifactFile = await DownloadPayload(mvnArt, mavenArtifact, mavenProject, artifactDir, config);

				// Determine MD5
				var md5File = artifactFile + ".md5";
				var md5_func = new Func<Task<Stream>>(() => mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging + ".md5"));

				if (!(await TryDownloadFile(md5_func, md5File, ErrorLevel.Info))) {
					// Then hash the downloaded artifact
					using (var file = File.OpenRead(artifactFile))
						File.WriteAllText (md5File, Util.HashMd5(file));
				}

				// Determine Sha256
				var sha256File = artifactFile + ".sha256";
				var sha_func = new Func<Task<Stream>>(() => mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging + ".sha256"));

				// First try download, this almost certainly won't work
				// but in case Maven ever starts supporting sha256 it should start
				// they currently support .sha1 so there's no reason to believe the naming
				// convention should be any different, and one day .sha256 may exist
				if (!(await TryDownloadFile(sha_func, sha256File, ErrorLevel.Ignore))) {
					// Create Sha256 hash if we couldn't download
					using (var file = File.OpenRead(artifactFile))
						File.WriteAllText(sha256File, Util.HashSha256(file));
				}

				var base_file_name = Path.Combine (artifactDir, config.DownloadExternalsWithFullName ? $"{mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}" : $"{mavenArtifact.ArtifactId}");

				if (config.DownloadJavaSourceJars) {
					var source_file = base_file_name + "-sources.jar";
					var source_func = new Func<Task<Stream>>(() => maven.OpenArtifactSourcesFile(mavenArtifact.GroupId, mavenArtifact.ArtifactId, version));
					await TryDownloadFile(source_func, source_file, ErrorLevel.Info);
				}

				if (config.DownloadPoms) {
					var pom_file = base_file_name + ".pom";
					var pom_func = new Func<Task<Stream>>(() => maven.OpenArtifactPomFile(mavenArtifact.GroupId, mavenArtifact.ArtifactId, version));
					await TryDownloadFile(pom_func, pom_file, ErrorLevel.Info);
				}

				if (config.DownloadJavaDocJars) {
					var javadoc_file = base_file_name + "-javadoc.jar";
					var javadoc_func = new Func<Task<Stream>> (() => maven.OpenArtifactDocsFile (mavenArtifact.GroupId, mavenArtifact.ArtifactId, version));
					await TryDownloadFile (javadoc_func, javadoc_file, ErrorLevel.Info);
				}

				if (config.DownloadMetadataFiles) {
					var metadata_file = base_file_name + "-metadata.xml";
					var metadata_func = new Func<Task<Stream>>(() => maven.OpenMavenMetadataFile(mavenArtifact.GroupId, mavenArtifact.ArtifactId));
					await TryDownloadFile(metadata_func, metadata_file, ErrorLevel.Info);
				}

				if (Directory.Exists(artifactExtractDir))
					Directory.Delete(artifactExtractDir, true);

				// Unzip artifact into externals
				if (mavenProject.Packaging.ToLowerInvariant() == "aar")
					ZipFile.ExtractToDirectory(artifactFile, artifactExtractDir);
			}
		}

		// Returns artifact output path
		static async Task<string> DownloadPayload(Artifact mvnArt, MavenArtifactConfig mavenArtifact, Project mavenProject, string artifactDir, BindingConfig config)
		{
			var package_prefix = config.DownloadExternalsWithFullName ? $"{mavenArtifact.GroupId}." : string.Empty;
			package_prefix += $"{mavenArtifact.ArtifactId}.";

			var base_path = Path.Combine(artifactDir, package_prefix);

			if (mavenProject.Packaging == "jar" || mavenProject.Packaging == "aar") {
				var func = new Func<Task<Stream>>(() => mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging));
				await TryDownloadFile(func, base_path + mavenProject.Packaging, ErrorLevel.Error);

				return base_path + mavenProject.Packaging;
			}

			// Sometimes the "Packaging" isn't useful, like "bundle" for Guava or "pom" for KotlinX Coroutines.
			// In this case we're going to try "jar" and "aar" to try to find the real payload

			// Try jar
			var jar_func = new Func<Task<Stream>>(() => mvnArt.OpenLibraryFile(mavenArtifact.Version, "jar"));
			var jar_result = await TryDownloadFile(jar_func, base_path + "jar", ErrorLevel.Ignore);

			if (jar_result) {
				mavenProject.Packaging = "jar";
				return base_path + "jar";
			}

			// Try aar
			var aar_func = new Func<Task<Stream>>(() => mvnArt.OpenLibraryFile(mavenArtifact.Version, "aar"));
			var aar_result = await TryDownloadFile(aar_func, base_path + "aar", ErrorLevel.Ignore);

			if (aar_result) {
				mavenProject.Packaging = "aar";
				return base_path + "aar";
			}

			throw new Exception($"Could not find artifact payload {base_path + "jar"}. [Packaging was {mavenProject.Packaging}.]");
		}

		// Return value indicates download success
		static async Task<bool> TryDownloadFile(Func<Task<Stream>> func, string outputFile, ErrorLevel level = ErrorLevel.Info)
		{
			try {
				using (var astrm = await func.Invoke())
				using (var sw = File.Create(outputFile))
					await astrm.CopyToAsync(sw);

				return true;
			} catch (Exception ex) {
				if (level == ErrorLevel.Error)
					throw;

				if (level == ErrorLevel.Info)
					Trace.WriteLine($"Failed to download {Path.GetFileName (outputFile)}: {ex.Message}");

				return false;
			}
		}

		enum ErrorLevel
		{
			Ignore,
			Info,
			Error
		}

		static List<BindingProjectModel> BuildProjectModels(BindingConfig config, Dictionary<string, Project> mavenProjects)
		{
			var projectModels = new List<BindingProjectModel>();
			var exceptions = new List<Exception>();

			foreach (var mavenArtifact in config.MavenArtifacts)
			{
				if (mavenArtifact.DependencyOnly)
					continue;

				if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
					continue;

				var projectModel = new BindingProjectModel
				{
					Name = mavenArtifact.ArtifactId,
					NuGetPackageId = mavenArtifact.NugetPackageId,
					NuGetVersionBase = mavenArtifact.NugetVersion,
					NuGetVersionSuffix = config.NugetVersionSuffix,
					MavenGroupId = mavenArtifact.GroupId,
					AssemblyName = mavenArtifact.AssemblyName,
					Config = config
				};
				projectModels.Add(projectModel);


				var artifactDir = Path.Combine(config.BasePath, config.ExternalsDir, mavenArtifact.GroupId);
				var artifactFile = Path.Combine(artifactDir, $"{mavenArtifact.ArtifactId}.{mavenProject.Packaging}");
				var md5File = artifactFile + ".md5";
				var sha256File = artifactFile + ".sha256";
				var md5 = File.Exists(md5File) ? File.ReadAllText(md5File) : string.Empty;
				var sha256 = File.Exists(sha256File) ? File.ReadAllText(sha256File) : string.Empty;
				var artifactExtractDir = Path.Combine(artifactDir, mavenArtifact.ArtifactId);

				var proguardFile = Path.Combine(artifactExtractDir, "proguard.txt");

				projectModel.MavenArtifacts.Add(new MavenArtifactModel
				{
					MavenGroupId = mavenArtifact.GroupId,
					MavenArtifactId = mavenArtifact.ArtifactId,
					MavenArtifactPackaging = mavenProject.Packaging,
					MavenArtifactVersion = mavenArtifact.Version,
					MavenArtifactMd5 = md5,
					MavenArtifactSha256 = sha256,
					ProguardFile = File.Exists(proguardFile) ? GetRelativePath(proguardFile, config.BasePath).Replace("/", "\\") : null,
					MavenArtifactConfig = mavenArtifact
				});

				// Gather maven dependencies to try and map out nuget dependencies
				foreach (var mavenDep in mavenProject.Dependencies)
				{
					if (!ShouldIncludeDependency(config, mavenArtifact, mavenDep, exceptions))
						continue;

					mavenDep.Version = FixVersion(mavenDep.Version, mavenProject);

					mavenDep.GroupId = mavenDep.GroupId.Replace ("${project.groupId}", mavenProject.GroupId);
					mavenDep.Version = mavenDep.Version.Replace ("${project.version}", mavenProject.Version);

					var depMapping = config.MavenArtifacts.FirstOrDefault(
						ma => !string.IsNullOrEmpty(ma.Version)
						&& ma.GroupId == mavenDep.GroupId
						&& ma.ArtifactId == mavenDep.ArtifactId
						&& mavenDep.Satisfies(ma.Version));

					if (depMapping is null && mavenDep.IsRuntimeDependency()) {
						exceptions.Add(new Exception($"Artifact '{mavenArtifact.GroupAndArtifactId}' has unknown 'Runtime' dependency '{mavenDep.GroupAndArtifactId()}'. Either fulfill or exclude this dependency."));
						continue;
					}

					if (depMapping == null)
					{
						StringBuilder sb = new StringBuilder();
						sb.AppendLine($"");
						sb.AppendLine($"No matching artifact config found for: ");
						sb.AppendLine($"			{mavenDep.GroupId}.{mavenDep.ArtifactId}:{mavenDep.Version}");
						sb.AppendLine($"to satisfy dependency of: ");
						sb.AppendLine($"			{mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}:{mavenArtifact.Version}");
						sb.AppendLine($"");
						sb.AppendLine($"	Please add following json snippet to config.json:");
						sb.AppendLine($"");
						sb.AppendLine
						($@"
      {{
        ""groupId"": ""{mavenDep.GroupId}"",
        ""artifactId"": ""{mavenDep.ArtifactId}"",
        ""version"": ""{mavenDep.Version}"",
        ""nugetVersion"": ""CHECK PREFIX {mavenDep.Version}"",
        ""nugetId"": ""CHECK NUGET VERSION"",
        ""dependencyOnly"": true/false
      }}
						");
						sb.AppendLine($"");
						exceptions.Add(new Exception(sb.ToString()));
						continue;
					}

					projectModel.NuGetDependencies.Add(new NuGetDependencyModel
					{
						IsProjectReference = !depMapping.DependencyOnly,
						NuGetPackageId = depMapping.NugetPackageId,
						NuGetVersionBase = depMapping.NugetVersion,
						NuGetVersionSuffix = config.NugetVersionSuffix,
						MavenArtifactConfig = depMapping,

						MavenArtifact = new MavenArtifactModel
						{
							MavenGroupId = mavenDep.GroupId,
							MavenArtifactId = mavenDep.ArtifactId,
							MavenArtifactVersion = mavenDep.Version,
							MavenArtifactMd5 = md5,
							MavenArtifactSha256 = sha256,
							DownloadedArtifact = artifactFile,
						}
					});
				}

			}

			if (exceptions.Any())
				throw new AggregateException(exceptions.ToArray());


			return projectModels;
		}

		static void AssignMetadata (BindingProjectModel project, TemplateConfig template)
		{
			// Calculate metadata from the config file and template file
			var baseMetadata = new Dictionary<string, string>();

			MergeValues(baseMetadata, project.Config.Metadata);
			MergeValues(baseMetadata, template.Metadata);

			// Add metadata for artifact
			var artifactMetadata = new Dictionary<string, string>();
			MergeValues(artifactMetadata, baseMetadata);

			if (project.MavenArtifacts.FirstOrDefault() is MavenArtifactModel artifact)
				MergeValues(artifactMetadata, artifact.MavenArtifactConfig.Metadata);

			project.Metadata = artifactMetadata;

			foreach (var art in project.MavenArtifacts)
				art.Metadata = artifactMetadata;

			// Add metadata for dependency
			var dependencyMetadata = new Dictionary<string, string>();
			MergeValues(dependencyMetadata, baseMetadata);

			if (project.NuGetDependencies.FirstOrDefault() is NuGetDependencyModel depMapping)
				MergeValues(dependencyMetadata, depMapping.MavenArtifactConfig.Metadata);

			foreach (var dep in project.NuGetDependencies) {
				dep.Metadata = dependencyMetadata;
				dep.MavenArtifact.Metadata = dependencyMetadata;
			}
		}

		static bool ShouldIncludeDependency(BindingConfig config, MavenArtifactConfig artifact, Dependency dependency, List<Exception> exceptions)
		{
			// We always care about 'compile' scoped dependencies
			if (dependency.IsCompileDependency())
				return true;

			// If we're not processing Runtime dependencies then ignore the rest
			if (!config.StrictRuntimeDependencies)
				return false;

			// The only other thing we may care about is 'runtime', bail if this isn't 'runtime'
			if (!dependency.IsRuntimeDependency())
				return false;

			// Check 'artifact' list
			if (artifact.ExcludedRuntimeDependencies.OrEmpty().Split(',').Contains(dependency.GroupAndArtifactId(), StringComparer.OrdinalIgnoreCase))
				return false;

			// Check 'global' list
			if (config.ExcludedRuntimeDependencies.OrEmpty ().Split (',').Contains (dependency.GroupAndArtifactId (), StringComparer.OrdinalIgnoreCase))
				return false;

			return true;
		}

		static string GetRelativePath(string filespec, string folder)
		{
			Uri pathUri = new Uri(filespec);
			// Folders must end in a slash
			if (!folder.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
				folder += Path.DirectorySeparatorChar;
			Uri folderUri = new Uri(folder);
			return Uri.UnescapeDataString(folderUri.MakeRelativeUri(pathUri).ToString().Replace('/', Path.DirectorySeparatorChar));
		}

		static Dictionary<string, string> MergeValues(Dictionary<string, string> dest, Dictionary<string, string> src)
		{
			dest = dest ?? new Dictionary<string, string>();
			if (src != null)
			{
				foreach (var kvp in src)
				{
					dest[kvp.Key] = kvp.Value;
				}
			}
			return dest;
		}

		// VersionRange.Parse cannot handle single number versions that we sometimes see in Maven, like "1".
		// Fix them to be "1.0".
		// https://github.com/NuGet/Home/issues/10342
		static string FixVersion(string version, Project project)
		{
			if (string.IsNullOrWhiteSpace(version))
				return version;

			// Handle versions with Properties, like:
			// <properties>
			//   <java.version>1.8</java.version>
			//   <gson.version>2.8.6</gson.version>
			// </properties>
			// <dependencies>
			//   <dependency>
			//     <groupId>com.google.code.gson</groupId>
			//     <artifactId>gson</artifactId>
			//     <version>${gson.version}</version>
			//   </dependency>
			// </dependencies>
			if (project?.Properties != null) {
				foreach (var prop in project.Properties.Any)
					version = version.Replace($"${{{prop.Name.LocalName}}}", prop.Value);
			}

			if (!version.Contains("."))
				version += ".0";

			return version;
		}
	}
}
