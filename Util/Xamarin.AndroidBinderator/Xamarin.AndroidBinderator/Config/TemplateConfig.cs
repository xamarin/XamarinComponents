using System.Linq;
using Newtonsoft.Json;

namespace AndroidBinderator
{
	public class TemplateConfig
	{
		[JsonProperty("templateFile")]
		public string TemplateFile { get; set; }

		[JsonProperty("outputFileRule")]
		public string OutputFileRule { get; set; }

		public string GetOutputFile(BindingConfig config, BindingProjectModel model)
		{
			var p = OutputFileRule
					 .Replace("{generated}", config.GeneratedDir)
					 .Replace("{groupid}", model.MavenGroupId)
					 .Replace("{artifactid}", model.MavenArtifacts?.FirstOrDefault()?.MavenArtifactId ?? "")
					 .Replace("{name}", model.Name)
					 .Replace("{nugetid}", model.NuGetPackageId);

			return System.IO.Path.Combine(config.BasePath, p);
		}
	}
}
