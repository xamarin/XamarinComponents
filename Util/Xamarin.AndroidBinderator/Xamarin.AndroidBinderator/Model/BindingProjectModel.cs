using System;
using System.Collections.Generic;

namespace AndroidBinderator
{
	public class BindingProjectModel
	{
		public string Id { get; private set; } = Guid.NewGuid().ToString().ToUpperInvariant();

		public string Name { get; set; }

		public string MavenGroupId { get; set; }

		public List<MavenArtifactModel> MavenArtifacts { get; set; } = new List<MavenArtifactModel>();

		public string NuGetPackageId { get; set; }
		public string NuGetVersionBase { get; set; }
		public string NuGetVersionSuffix { get; set; }

		public string NuGetVersion =>
			string.IsNullOrWhiteSpace(NuGetVersionSuffix)
				? NuGetVersionBase
				: NuGetVersionBase + NuGetVersionSuffix;

		public string AssemblyName { get; set; }

		public List<NuGetDependencyModel> NuGetDependencies { get; set; } = new List<NuGetDependencyModel>();

		public List<string> ProjectReferences { get; set; } = new List<string>();

		public BindingConfig Config { get; set; }
	}
}
