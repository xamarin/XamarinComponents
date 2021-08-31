using System.Collections.Generic;

namespace AndroidBinderator
{
	public class MavenArtifactModel
	{
		public string MavenGroupId { get; set; }
		public string MavenArtifactId { get; set; }
		public string MavenArtifactVersion { get; set; }
		public string MavenArtifactPackaging { get; set; }
		public string MavenArtifactMd5 { get; set; }
		public string MavenArtifactSha256 { get; set; }
		public MavenArtifactConfig MavenArtifactConfig { get; set; }

		public string DownloadedArtifact { get; set; }
		public string ProguardFile { get; set; }

		public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
	}
}
