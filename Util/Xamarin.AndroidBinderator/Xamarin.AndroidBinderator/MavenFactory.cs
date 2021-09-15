using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MavenNet;

namespace AndroidBinderator
{
	public static class MavenFactory
	{
		static readonly Dictionary<string, MavenRepository> repositories = new Dictionary<string, MavenRepository>();

		public static async Task Initialize(BindingConfig config)
		{
			var artifact_mavens = new List<(MavenRepository, MavenArtifactConfig)>();

			foreach (var artifact in config.MavenArtifacts.Where(ma => !ma.DependencyOnly)) {
				var (type, location) = GetMavenInfoForArtifact(config, artifact);
				var repo = GetOrCreateRepository(type, location);

				artifact_mavens.Add((repo, artifact));
			}

			foreach (var maven_group in artifact_mavens.GroupBy(a => a.Item1)) {
				var maven = maven_group.Key;
				var artifacts = maven_group.Select(a => a.Item2);

				foreach (var artifact_group in artifacts.GroupBy(a => a.GroupId)) {
					var gid = artifact_group.Key;
					var artifact_ids = artifact_group.Select(a => a.ArtifactId).ToArray();

					await maven.Populate(gid, artifact_ids);
				}
			}
		}

		public static MavenRepository GetMavenRepository(BindingConfig config, MavenArtifactConfig artifact)
		{
			var (type, location) = GetMavenInfoForArtifact(config, artifact);
			var repo = GetOrCreateRepository(type, location);

			return repo;
		}

		static (MavenRepoType type, string location) GetMavenInfoForArtifact(BindingConfig config, MavenArtifactConfig artifact)
		{
			var template = config.GetTemplateSet(artifact.TemplateSet);

			if (template.MavenRepositoryType.HasValue)
				return (template.MavenRepositoryType.Value, template.MavenRepositoryLocation);

			return (config.MavenRepositoryType, config.MavenRepositoryLocation);
		}

		static MavenRepository GetOrCreateRepository(MavenRepoType type, string location)
		{
			var key = $"{type}|{location}";

			if (repositories.TryGetValue(key, out MavenRepository repository))
				return repository;

			MavenRepository maven;

			if (type == MavenRepoType.Directory)
				maven = MavenRepository.FromDirectory(location);
			else if (type == MavenRepoType.Url)
				maven = MavenRepository.FromUrl(location);
			else if (type == MavenRepoType.MavenCentral)
				maven = MavenRepository.FromMavenCentral();
			else
				maven = MavenRepository.FromGoogle();

			repositories.Add(key, maven);

			return maven;
		}
	}
}
