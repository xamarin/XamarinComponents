using System.Collections.Generic;

namespace AndroidBinderator
{
	public class NuGetDependencyModel
	{
		public bool IsProjectReference { get; set; }

		public string NuGetPackageId { get; set; }
		public string NuGetVersionBase { get; set; }
		public string NuGetVersionSuffix { get; set; }
		public MavenArtifactConfig MavenArtifactConfig { get; set; }

		public string NuGetVersion =>
			!IsProjectReference || string.IsNullOrWhiteSpace(NuGetVersionSuffix)
				? NuGetVersionBase
				: NuGetVersionBase + NuGetVersionSuffix;

		public MavenArtifactModel MavenArtifact { get; set; }

		public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
	}
}
