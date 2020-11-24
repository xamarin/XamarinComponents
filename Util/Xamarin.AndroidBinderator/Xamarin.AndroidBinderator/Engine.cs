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
using AndroidBinderator.Common;
using Newtonsoft.Json;

namespace AndroidBinderator
{
    public static class Engine
    {
        public static event LogHandler Logger;

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
            else if (config.MavenRepositoryType == MavenRepoType.MavenCentral)
                maven = MavenRepository.FromMavenCentral();
            else
                maven = MavenRepository.FromGoogle();

            Logger?.Invoke(LogLevel.Information, $"Base Path: {config.BasePath}");
            Logger?.Invoke(LogLevel.Information, $"Repository: {config.MavenRepositoryType.ToString()}");


            //var artifacts = config.MavenArtifacts
            //        .Where(ma => !ma.DependencyOnly)
            //        .Select(ma => ma.GroupId)
            //        .Distinct()
            //        .ToArray();

            //foreach(var a in artifacts)
            //{
            //    Logger?.Invoke(LogLevel.Debug, a);
            //    await maven.Refresh(a);
            //}

            await maven.Refresh(
                config.MavenArtifacts
                    .Where(ma => !ma.DependencyOnly)
                    .Select(ma => ma.GroupId)
                    .Distinct()
                    .ToArray()
                );

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

            Logger?.Invoke(LogLevel.Information, "Building Project list...");
            foreach (var artifact in config.MavenArtifacts)
            {
                if (artifact.DependencyOnly)
                    continue;

                Logger?.Invoke(LogLevel.Debug, $"\tAdding {artifact.GroupId}.{artifact.ArtifactId}");

                var mavenGroup = maven.Groups.FirstOrDefault(g => g.Id == artifact.GroupId);

                Project project = null;

                project = await maven.GetProjectAsync(artifact.GroupId, artifact.ArtifactId, artifact.Version);

                if (project != null)
                    mavenProjects.Add($"{artifact.GroupId}/{artifact.ArtifactId}-{artifact.Version}", project);
            }

            if (config.DownloadExternals)
            {
                Logger?.Invoke(LogLevel.Information, "Downloading Externals...");
                await DownloadArtifacts(maven, config, mavenProjects);
            }

            var slnProjModels = new Dictionary<string, BindingProjectModel>();

            Logger?.Invoke(LogLevel.Information, $"Building Project Models for {config.Templates.Count} templates...");
            foreach (var template in config.Templates)
            {
                Logger?.Invoke(LogLevel.Debug, $"\tTemplate: {template.TemplateFile}:");
                var models = BuildProjectModels(config, template, mavenProjects);

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(models);

                if (config.Debug.DumpModels)
                    File.WriteAllText(Path.Combine(config.BasePath, "models.json"), json);

                var inputTemplateFile = Path.Combine(config.BasePath, template.TemplateFile);
                var templateSrc = File.ReadAllText(inputTemplateFile);

                var engine = new RazorLightEngineBuilder()
                    .UseMemoryCachingProvider()
                    .Build();

                Logger?.Invoke(LogLevel.Debug, "\tRendering template...");
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
                Logger?.Invoke(LogLevel.Debug, "\tMerging solution file");
                var slnPath = Path.Combine(config.BasePath ?? AppDomain.CurrentDomain.BaseDirectory, config.SolutionFile);
                var sln = SolutionFileBuilder.Build(config, slnProjModels);
                File.WriteAllText(slnPath, sln);
            }
        }

        static async Task DownloadArtifacts(MavenRepository maven, BindingConfig config, Dictionary<string, Project> mavenProjects)
        {
            foreach (var mavenArtifact in config.MavenArtifacts)
            {
                // Skip downloading dependencies
                if (mavenArtifact.DependencyOnly)
                    continue;

                Logger?.Invoke(LogLevel.Debug, $"\tDownloading {mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}...");

                var version = mavenArtifact.Version;

                if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
                    continue;

                var artifactDir = Path.Combine(
                    config.BasePath,
                    config.ExternalsDir,
                    mavenArtifact.GroupId);
                var artifactFile = Path.Combine(artifactDir, config.DownloadExternalsWithFullName ? $"{mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}.{mavenProject.Packaging}"
                    : $"{mavenArtifact.ArtifactId}.{mavenProject.Packaging}");
                var md5File = artifactFile + ".md5";
                var sha256File = artifactFile + ".sha256";
                var sourcesFile = Path.Combine(artifactDir, config.DownloadExternalsWithFullName ? $"{mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}-sources.jar"
                    : $"{mavenArtifact.ArtifactId}-sources.jar");
                var artifactExtractDir = Path.Combine(artifactDir, mavenArtifact.ArtifactId);

                Logger?.Invoke(LogLevel.Debug, $"\tTarget path: {artifactDir}");


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
                    Logger?.Invoke(LogLevel.Debug, "\t\tDownloading MD5");

                    // First try download
                    using (var astrm = await mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging + ".md5"))
                    using (var sw = File.Create(md5File))
                        await astrm.CopyToAsync(sw);
                }
                catch (Exception ex)
                {
                    Logger?.Invoke(LogLevel.Error, ex.ToString());
                    Logger?.Invoke(LogLevel.Warning, "\t\tHashing manually.");

                    // Then hash the downloaded artifact
                    using (var file = File.OpenRead(artifactFile))
                        File.WriteAllText(md5File, Util.HashMd5(file));
                }
                finally
                {
                    Logger?.Invoke(LogLevel.Debug, "\t\tDone");
                }

                // Determine Sha256
                try
                {
                    Logger?.Invoke(LogLevel.Debug, "\t\tDownloading SHA256");

                    // First try download, this almost certainly won't work
                    // but in case Maven ever starts supporting sha256 it should start
                    // they currently support .sha1 so there's no reason to believe the naming 
                    // convention should be any different, and one day .sha256 may exist
                    using (var astrm = await mvnArt.OpenLibraryFile(mavenArtifact.Version, mavenProject.Packaging + ".sha256"))
                    using (var sw = File.Create(sha256File))
                        await astrm.CopyToAsync(sw);
                }
                catch (HttpRequestException ex)
                {
                    Logger?.Invoke(LogLevel.Warning, "\t\tCreating SHA256 manually.");

                    // Create Sha256 hash if we couldn't download
                    using (var file = File.OpenRead(artifactFile))
                        File.WriteAllText(sha256File, Util.HashSha256(file));
                }
                catch (Exception ex)
                {
                    Logger?.Invoke(LogLevel.Error, ex.ToString());
                }
                finally
                {
                    Logger?.Invoke(LogLevel.Debug, "\t\tDone");
                }

                if (config.DownloadJavaSourceJars)
                {
                    try
                    {
                        Logger?.Invoke(LogLevel.Debug, "\t\tDownloading Java Source JARs.");


                        using (var astrm = await maven.OpenArtifactSourcesFile(mavenArtifact.GroupId, mavenArtifact.ArtifactId, version))
                        using (var sw = File.Create(sourcesFile))
                            await astrm.CopyToAsync(sw);
                    }
                    catch (Exception ex)
                    {
                        Logger?.Invoke(LogLevel.Error, ex.ToString());
                    }
                    finally
                    {
                        Logger?.Invoke(LogLevel.Debug, "\t\tDone");
                    }
                }

                if (Directory.Exists(artifactExtractDir))
                    Directory.Delete(artifactExtractDir, true);

                // Unzip artifact into externals
                if (mavenProject.Packaging.ToLowerInvariant() == "aar")
                {
                    Logger?.Invoke(LogLevel.Debug, "\t\tUnzipping AARs.");
                    ZipFile.ExtractToDirectory(artifactFile, artifactExtractDir);
                }
            }
        }

        static List<BindingProjectModel> BuildProjectModels(BindingConfig config, TemplateConfig template, Dictionary<string, Project> mavenProjects)
        {
            var projectModels = new List<BindingProjectModel>();

            var baseMetadata = new Dictionary<string, string>();
            MergeValues(baseMetadata, config.Metadata);
            MergeValues(baseMetadata, template.Metadata);

            bool failedDependencies = false;

            foreach (var mavenArtifact in config.MavenArtifacts)
            {
                if (mavenArtifact.DependencyOnly)
                    continue;

                if (!mavenProjects.TryGetValue($"{mavenArtifact.GroupId}/{mavenArtifact.ArtifactId}-{mavenArtifact.Version}", out var mavenProject))
                    continue;

                Logger?.Invoke(LogLevel.Debug, $"\t\tArtifact: {mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}");

                var artifactMetadata = new Dictionary<string, string>();
                MergeValues(artifactMetadata, baseMetadata);
                MergeValues(artifactMetadata, mavenArtifact.Metadata);

                var projectModel = new BindingProjectModel
                {
                    Name = mavenArtifact.ArtifactId,
                    NuGetPackageId = mavenArtifact.NugetPackageId,
                    NuGetVersionBase = mavenArtifact.NugetVersion,
                    NuGetVersionSuffix = config.NugetVersionSuffix,
                    MavenGroupId = mavenArtifact.GroupId,
                    AssemblyName = mavenArtifact.AssemblyName,
                    Metadata = artifactMetadata,
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
                    Metadata = artifactMetadata,
                });


                // Gather maven dependencies to try and map out nuget dependencies
                foreach (var mavenDep in mavenProject.Dependencies)
                {
                    // We only really care about 'compile' scoped dependencies (also null/blank means compile)
                    if (!string.IsNullOrEmpty(mavenDep.Scope) && !mavenDep.Scope.Equals("compile", StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    var depMapping = config.MavenArtifacts.FirstOrDefault(
                        ma => !string.IsNullOrEmpty(ma.Version)
                        && ma.GroupId == mavenDep.GroupId
                        && ma.ArtifactId == mavenDep.ArtifactId
                        && mavenDep.Satisfies(ma.Version));

                    if (depMapping == null)
                    {
                        var dependency = new MavenArtifactConfig
                        {
                            GroupId = mavenDep.GroupId,
                            ArtifactId = mavenDep.ArtifactId,
                            Version = mavenDep.Version
                        };

                        var outputPath = Path.Combine(config.BasePath, config.GeneratedDir, "missingDependencies");
                        if (!Directory.Exists(outputPath))
                            Directory.CreateDirectory(outputPath);

                        var json = JsonConvert.SerializeObject(dependency);
                        File.WriteAllText(Path.Combine(outputPath, $"{dependency.GroupId}.{dependency.ArtifactId}.json"), json);

                        Logger?.Invoke(LogLevel.Error, $"No matching artifact config found for: {mavenDep.GroupId}.{mavenDep.ArtifactId}:{mavenDep.Version} to satisfy dependency of: {mavenArtifact.GroupId}.{mavenArtifact.ArtifactId}:{mavenArtifact.Version}");

                        failedDependencies = true;
                        continue;
                    }

                    var dependencyMetadata = new Dictionary<string, string>();
                    MergeValues(dependencyMetadata, baseMetadata);
                    MergeValues(dependencyMetadata, depMapping.Metadata);

                    projectModel.NuGetDependencies.Add(new NuGetDependencyModel
                    {
                        IsProjectReference = !depMapping.DependencyOnly,
                        NuGetPackageId = depMapping.NugetPackageId,
                        NuGetVersionBase = depMapping.NugetVersion,
                        NuGetVersionSuffix = config.NugetVersionSuffix,
                        Metadata = dependencyMetadata,

                        MavenArtifact = new MavenArtifactModel
                        {
                            MavenGroupId = mavenDep.GroupId,
                            MavenArtifactId = mavenDep.ArtifactId,
                            MavenArtifactVersion = mavenDep.Version,
                            MavenArtifactMd5 = md5,
                            MavenArtifactSha256 = sha256,
                            DownloadedArtifact = artifactFile,
                            Metadata = dependencyMetadata,
                        }
                    });
                }

            }
            
            if (failedDependencies)
                throw new AndroidBinderatorException("Missing dependencies");

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
    }
}
