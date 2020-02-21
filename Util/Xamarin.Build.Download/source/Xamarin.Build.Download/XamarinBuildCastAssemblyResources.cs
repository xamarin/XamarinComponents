using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Xamarin.MacDev;

namespace Xamarin.Build.Download
{
	public class XamarinBuildCastAssemblyResources : Task
	{
		const string identityKey = "Identity";
		const string logicalNameKey = "LogicalName";
		const string optimizeKey = "Optimize";

		[Required]
		public ITaskItem [] RestoreAssemblyResources { get; set; }

		[Output]
		public ITaskItem [] BundleResources { get; set; }

		public override bool Execute ()
		{
			var bundleResources = new List<ITaskItem> ();

			foreach (var resource in RestoreAssemblyResources) {
				var identity = resource.GetMetadata (identityKey);
				
				var logicalName = resource.GetMetadata (logicalNameKey);
				logicalName = logicalName.Replace ("__monotouch_content_", "");
				logicalName = logicalName.Replace ("_f", @"\");
				logicalName = logicalName.Replace ("__", "_");

				var bundleResource = new TaskItem (identity);
				bundleResource.SetMetadata (logicalNameKey, logicalName);

				if (identity.ToLower().EndsWith (".png"))
					bundleResource.SetMetadata (optimizeKey, "False");

				bundleResources.Add (bundleResource);
			}

			BundleResources = bundleResources.ToArray ();

			return true;
		}
	}
}
