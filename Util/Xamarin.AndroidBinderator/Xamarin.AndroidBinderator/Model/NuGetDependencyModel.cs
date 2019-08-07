namespace AndroidBinderator
{
	public class NuGetDependencyModel
	{
		public bool IsProjectReference { get; set; }

		public string NuGetPackageId { get; set; }
		public string NuGetVersion { get; set; }

		public MavenArtifactModel MavenArtifact { get; set; }
	}
}
