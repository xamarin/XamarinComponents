using System.Collections.Generic;
using Newtonsoft.Json;

namespace AndroidBinderator
{
	public class BindingConfig
	{
		[JsonProperty("basePath")]
		public string BasePath { get; set; }

		[JsonProperty("mavenRepositoryType")]
		public MavenRepoType MavenRepositoryType { get; set; } = MavenRepoType.Google;

		[JsonProperty("mavenRepositoryLocation")]
		public string MavenRepositoryLocation { get; set; } = null;

		[JsonProperty("generatedDir")]
		public string GeneratedDir { get; set; } = "generated";

		[JsonProperty("downloadExternals")]
		public bool DownloadExternals { get; set; } = true;

		[JsonProperty("downloadJavaSourceJars")]
		public bool DownloadJavaSourceJars { get; set; } = true;

		[JsonProperty("externalsDir")]
		public string ExternalsDir { get; set; } = "externals";

		[JsonProperty("templates")]
		public List<TemplateConfig> Templates { get; set; } = new List<TemplateConfig>();

		[JsonProperty("slnFile")]
		public string SolutionFile { get; set; }

		[JsonProperty("artifacts")]
		public List<MavenArtifactConfig> MavenArtifacts { get; set; }

		[JsonProperty("debug")]
		public BindingConfigDebug Debug { get; set; } = new BindingConfigDebug();

		[JsonProperty("additionalProjects")]
		public List<string> AdditionalProjects { get; set; }
	}

	public class BindingConfigDebug
	{
		public bool DebugMode { get; set; } = false;
		public bool DumpModels { get; set; } = false;
	}

	public enum MavenRepoType
	{
		Url,
		Directory,
		Google
	}
}
