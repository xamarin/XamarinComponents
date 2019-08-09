using System;
using System.Linq;
using Newtonsoft.Json;

namespace AndroidBinderator
{
	public class TemplateConfig
	{
		public TemplateConfig()
		{
		}

		public TemplateConfig(string templateFile, string outputFileRule)
		{
			TemplateFile = templateFile ?? throw new ArgumentNullException(nameof(templateFile));
			OutputFileRule = outputFileRule ?? throw new ArgumentNullException(nameof(outputFileRule));
		}

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
