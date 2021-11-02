using System;
using System.Collections.Generic;
using System.Text;
using MavenNet.Models;

namespace AndroidBinderator
{
	public static class Extensions
	{
		public static string OrEmpty (this string value) => value ?? string.Empty;

		public static string GroupAndArtifactId (this Dependency dependency) => $"{dependency.GroupId}.{dependency.ArtifactId}";

		public static bool IsCompileDependency (this Dependency dependency) => string.IsNullOrWhiteSpace (dependency.Scope) || dependency.Scope.ToLowerInvariant ().Equals ("compile");

		public static bool IsRuntimeDependency (this Dependency dependency) => dependency?.Scope != null && dependency.Scope.ToLowerInvariant ().Equals ("runtime");
	}
}
