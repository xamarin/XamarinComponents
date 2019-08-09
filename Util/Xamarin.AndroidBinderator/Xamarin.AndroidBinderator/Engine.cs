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
using ArtifactInfo = System.Collections.Generic.Dictionary<string, string>;
using System.Security.Cryptography;

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
			MavenRepository maven;

			if (config.MavenRepositoryType == MavenRepoType.Directory)
				maven = MavenRepository.FromDirectory(config.MavenRepositoryLocation);
			else if (config.MavenRepositoryType == MavenRepoType.Url)
				maven = MavenRepository.FromUrl(config.MavenRepositoryLocation);
			else
				maven = MavenRepository.FromGoogle();

			await maven.Refresh(config.MavenArtifacts.Where(ma => !ma.DependencyOnly).Select(ma => ma.GroupId).Distinct().ToArray());

			if (config.DownloadExternals)
			{
				var artifactDir = Path.Combine(config.BasePath, config.ExternalsDir);
				if (!Directory.Exists(artifactDir))
					Directory.CreateDirectory(artifactDir);
			}

			await ProcessConfig(maven, config);
		}

		static async Task ProcessConfig(MavenRepository maven, BindingConfig config)
		{
			var mavenProjects = new Dictionary<string, Project>();
			var mavenGroups = new List<MavenGroup>();

			foreach (var artifact in config.MavenArtifacts)
			{
                if (artifact.DependencyOnly)
                    continue;

				var mavenGroup = maven.Groups.FirstOrDefault(g => g.Id == artifact.GroupId);

				Project project = null;

				project = await maven.GetProjectAsync(artifact.GroupId, artifact.ArtifactId, artifact.Version);

				if (project != null)
					mavenProjects.Add($"{artifact.GroupId}/{artifact.ArtifactId}-{artifact.Version}", project);
			}

			if (config.DownloadExternals)
				await DownloadArtifacts(maven, config, mavenProjects);

			var slnProjModels = new Dictionary<string, BindingProjectModel>();

			foreach (var template in config.Templates)
			{
				var models = BuildProjectModels(config, mavenProjects);

				var json = Newtonsoft.Json.JsonConvert.SerializeObject(models);

				if (config.Debug.DumpModels)
					File.WriteAllText(Path.Combine(config.BasePath, "models.json"), json);

				var inputTemplateFile = Path.Combine(config.BasePath, template.TemplateFile);
				var templateSrc = File.ReadAllText(inputTemplateFile);

				var engine = new RazorLightEngineBuilder()
					.UseMemoryCachingProvider()
					.Build();

				foreach (var model in models)
				{
					var outputFile = new FileInfo(template.GetOutputFile(config, model));
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

		static async Task DownloadArtifacts(MavenRepository maven, BindingConfig config, Dictionary<string, Project> mavenProjects)
		{
			var httpClient = new HttpClient();

			foreach (var mavenArtifact in config.MavenArtifacts)
			{
                // Skip downloading dependencies
                if (mavenArtifact.DependencyOnly)
                    continue;

				var version = mavenArtifact.Version;

				if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
					continue;

				var artifactDir = Path.Combine(
					config.BasePath,
					config.ExternalsDir,
					mavenArtifact.GroupId);
				var artifactFile = Path.Combine(artifactDir, $"{mavenArtifact.ArtifactId}.{mavenProject.Packaging}");
				var md5File = artifactFile + ".md5";
				var sourcesFile = Path.Combine(artifactDir, $"{mavenArtifact.ArtifactId}-sources.jar");
				var artifactExtractDir = Path.Combine(artifactDir, mavenArtifact.ArtifactId);

				if (!Directory.Exists(artifactDir))
					Directory.CreateDirectory(artifactDir);
				if (!Directory.Exists(artifactExtractDir))
					Directory.CreateDirectory(artifactExtractDir);

				var mvnArt = maven.Groups.FirstOrDefault(g => g.Id == mavenArtifact.GroupId)?.Artifacts?.FirstOrDefault(a => a.Id == mavenArtifact.ArtifactId);

				// Download artifact
				using (var astrm = await mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging))
				using (var sw = File.Create(artifactFile))
					await astrm.CopyToAsync(sw);

				// Determine MD5
				try
				{
					// First try download
					using (var astrm = await mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging + ".md5"))
					using (var sw = File.Create(md5File))
						await astrm.CopyToAsync(sw);
				}
				catch
				{
					// Then hash the downloaded artifact
					using (var file = File.OpenRead(artifactFile))
						File.WriteAllText(md5File, HashMd5(file));
				}

				if (config.DownloadJavaSourceJars)
				{
					try
					{
						using (var astrm = await maven.OpenArtifactSourcesFile(mavenArtifact.GroupId, mavenArtifact.ArtifactId, version))
						using (var sw = File.Create(sourcesFile))
							await astrm.CopyToAsync(sw);
					}
					catch { }
				}

				if (Directory.Exists(artifactExtractDir))
					Directory.Delete(artifactExtractDir, true);

				// Unzip artifact into externals
				if (mavenProject.Packaging.ToLowerInvariant() == "aar")
					ZipFile.ExtractToDirectory(artifactFile, artifactExtractDir);
			}
		}

		static List<BindingProjectModel> BuildProjectModels(BindingConfig config, Dictionary<string, Project> mavenProjects)
		{
			var projectModels = new List<BindingProjectModel>();

			foreach (var mavenArtifact in config.MavenArtifacts)
			{

				if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
					continue;

				if (mavenArtifact.DependencyOnly)
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
				var md5 = File.Exists(md5File) ? File.ReadAllText(md5File) : string.Empty;
				var artifactExtractDir = Path.Combine(artifactDir, mavenArtifact.ArtifactId);

				var proguardFile = Path.Combine(artifactExtractDir, "proguard.txt");

				projectModel.MavenArtifacts.Add(new MavenArtifactModel
				{
					MavenGroupId = mavenArtifact.GroupId,
					MavenArtifactId = mavenArtifact.ArtifactId,
					MavenArtifactPackaging = mavenProject.Packaging,
					MavenArtifactVersion = mavenArtifact.Version,
					MavenArtifactMd5 = md5,
					ProguardFile = File.Exists(proguardFile) ? GetRelativePath(proguardFile, config.BasePath).Replace("/", "\\") : null,
				});

				// Gather maven dependencies to try and map out nuget dependencies
				foreach (var mavenDep in mavenProject.Dependencies)
				{
					// We only really care about 'compile' scoped dependencies (also null/blank means compile)
					if (!string.IsNullOrEmpty(mavenDep.Scope) && !mavenDep.Scope.ToLowerInvariant().Equals("compile"))
						continue;

					var depMapping = config.MavenArtifacts.FirstOrDefault(
						ma => ma.GroupId == mavenDep.GroupId
						&& ma.ArtifactId == mavenDep.ArtifactId
						&& mavenDep.Satisfies(ma.Version));

					if (depMapping == null)
						throw new Exception($"No matching artifact config found for: {mavenDep.GroupId}.{mavenDep.ArtifactId}:{mavenDep.Version} to satisfy dependency of: {mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}:{mavenArtifact.Version}");

					projectModel.NuGetDependencies.Add(new NuGetDependencyModel
					{
						IsProjectReference = !depMapping.DependencyOnly,
						NuGetPackageId = depMapping.NugetPackageId,
						NuGetVersion = depMapping.NugetVersion,

						MavenArtifact = new MavenArtifactModel
						{
							MavenGroupId = mavenDep.GroupId,
							MavenArtifactId = mavenDep.ArtifactId,
							MavenArtifactVersion = mavenDep.Version,
							MavenArtifactMd5 = md5,
							DownloadedArtifact = artifactFile,
						}
					});
				}
			}

			return projectModels;
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

		static string HashMd5(Stream value)
		{
			using (var md5 = MD5.Create())
				return BitConverter.ToString(md5.ComputeHash(value)).Replace("-", "").ToLowerInvariant();
		}
	}
}
