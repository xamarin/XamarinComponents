using System.Collections.Generic;
using Newtonsoft.Json;

namespace AndroidBinderator
{
	public class MavenArtifactConfig
	{
		public MavenArtifactConfig()
		{
		}

		public MavenArtifactConfig(string groupId, string artifactId, string version, string nugetPackageId = null, string nugetVersion = null)
		{
			GroupId = groupId;
			ArtifactId = artifactId;
			Version = version;
			NugetVersion = nugetVersion;
			NugetPackageId = nugetPackageId;
		}

		[JsonProperty("groupId")]
		public string GroupId { get; set; }

		[JsonProperty("artifactId")]
		public string ArtifactId { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		string nugetVersion = null;

		[JsonProperty("nugetVersion")]
		public string NugetVersion {
			get { return nugetVersion ?? Version; }
			set { nugetVersion = value; }
		}

		[JsonProperty("nugetId")]
		public string NugetPackageId { get; set; }

		[JsonProperty("dependencyOnly")]
		public bool DependencyOnly { get; set; } = false;

		[JsonProperty("assemblyName")]
		public string AssemblyName { get; set; } = null;

		[JsonProperty("metadata")]
		public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

		[JsonProperty("excludedRuntimeDependencies")]
		public string ExcludedRuntimeDependencies { get; set; }

		public string GroupAndArtifactId => $"{GroupId}.{ArtifactId}";
	}
}
