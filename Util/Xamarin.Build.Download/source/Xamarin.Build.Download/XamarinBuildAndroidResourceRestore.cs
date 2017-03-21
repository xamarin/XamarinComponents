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
	public class XamarinBuildAndroidResourceRestore : BaseXamarinBuildResourceRestore
	{
		[Required]
		public string TargetFrameworkVerison { get; set; }

		public override bool OverwriteSourceAssembly { get; set; } = true;

		protected override IAssemblyResolver CreateAssemblyResolver ()
		{
			var resolver = new DefaultAssemblyResolver ();
			if (Platform.IsMac) {
				resolver.AddSearchDirectory (
					"/Library/Frameworks/Xamarin.Android.framework/Versions/Current/lib/xbuild-frameworks/MonoAndroid/" + this.TargetFrameworkVerison
				);
			} else {

				if (!string.IsNullOrEmpty (VsInstallRoot) && Directory.Exists (VsInstallRoot)) {
					resolver.AddSearchDirectory (Path.Combine (
						VsInstallRoot,
						@"Common7\IDE\ReferenceAssemblies\Microsoft\Framework\MonoAndroid\" + this.TargetFrameworkVerison
						));
				} else {
					resolver.AddSearchDirectory (Path.Combine (
						Environment.GetFolderPath (Environment.SpecialFolder.ProgramFilesX86),
						@"Reference Assemblies\Microsoft\Framework\MonoAndroid\" + this.TargetFrameworkVerison
					));
				}
			}
			return resolver;
		}
	}
}
