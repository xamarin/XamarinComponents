using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AndroidBinderator
{
	public class TemplateSetModel
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("mavenRepositoryType")]
		public MavenRepoType? MavenRepositoryType { get; set; }

		[JsonProperty("mavenRepositoryLocation")]
		public string MavenRepositoryLocation { get; set; } = null;

		[JsonProperty("templates")]
		public List<TemplateConfig> Templates { get; set; } = new List<TemplateConfig> ();
	}
}
