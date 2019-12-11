// Copyright (c) 2015-2016 Xamarin Inc.

using System.Linq;
using Microsoft.Build.Construction;

namespace Xamarin.ContentPipeline.Tests
{
	//workaround for missing MSBuild API in Mono. Project.AddItem explodes,
	//so we use ProjectRootElement, which doesn't have SetProperty/GetPropertyValue
	public static class MSBuildExtensions
	{
		public static void SetProperty (this ProjectRootElement prel, string name, string value)
		{
			var existing = prel.PropertyGroups.SelectMany (g => g.Properties).FirstOrDefault (p => p.Name == name);
			if (existing != null)
				existing.Value = value;
			else
				prel.AddProperty (name, value);
		}

		public static string GetPropertyValue (this ProjectRootElement prel, string name)
		{
			var existing = prel.Properties.FirstOrDefault (p => p.Name == name);
			if (existing != null)
				return existing.Value;
			return null;
		}

		public static void RemoveItems (this ProjectRootElement prel, ProjectItemElement pel)
		{
			pel.Parent.RemoveChild (pel);
		}

		public static void SetMetadataValue (this ProjectItemElement pel, string name, string value)
		{
			var existing = pel.Metadata.FirstOrDefault (p => p.Name == name);
			if (existing != null)
				existing.Value = value;
			else
				pel.AddMetadata (name, value);
		}

		public static void RemoveMetadata (this ProjectItemElement pel, string name)
		{
			var existing = pel.Metadata.FirstOrDefault (p => p.Name == name);
			if (existing != null)
				pel.RemoveChild (existing);
		}
	}
}