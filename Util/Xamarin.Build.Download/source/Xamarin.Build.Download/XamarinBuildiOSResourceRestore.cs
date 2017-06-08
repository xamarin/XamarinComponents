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
	public class XamarinBuildiOSResourceRestore : BaseXamarinBuildResourceRestore
	{
		protected override IAssemblyResolver CreateAssemblyResolver ()
		{
			var resolver = new DefaultAssemblyResolver ();
			if (Platform.IsMac) {
				resolver.AddSearchDirectory (
					"/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/lib/mono/Xamarin.iOS"
				);
			} else {

				if (!string.IsNullOrEmpty (VsInstallRoot) && Directory.Exists (VsInstallRoot)) {
					resolver.AddSearchDirectory (Path.Combine (
						VsInstallRoot,
						@"Common7\IDE\ReferenceAssemblies\Microsoft\Framework\Xamarin.iOS\v1.0"
					));
				} else {
					resolver.AddSearchDirectory (Path.Combine (
						Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86),
						@"Reference Assemblies\Microsoft\Framework\Xamarin.iOS\v1.0"
					));
				}
			}
			return resolver;
		}

		protected override Stream LoadResource (string resourceFullPath, string assemblyName, string assemblyOutputPath)
		{
			var fileStream = base.LoadResource (resourceFullPath, assemblyName, assemblyOutputPath);

			var resourceFileName = Path.GetFileName (resourceFullPath);

			//HACK: CFBundleExecutable is invalid in bundle plists but some downloaded libraries contain it. remove it.
			if (resourceFileName == "Info.plist") {
				var po = PObject.FromStream (fileStream) as PDictionary;
				if (po != null && po.Remove ("CFBundleExecutable")) {
					var memoryStream = new MemoryStream ();

					using (var ctx = PropertyListFormat.Binary.StartWriting (memoryStream))
						ctx.WriteObject (po);

					fileStream.Dispose ();
					memoryStream.Position = 0;
					return memoryStream;
				}
			}

			return fileStream;
		}
	}
}
