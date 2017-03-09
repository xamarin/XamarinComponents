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
	public abstract class BaseXamarinBuildResourceRestore : Task
	{
		[Required]
		public string IntermediateOutputPath { get; set; }

		[Required]
		public ITaskItem [] InputReferencePaths { get; set; }

		[Required]
		public ITaskItem [] RestoreAssemblyResources { get; set; }

		public string VsInstallRoot { get; set; }

		public bool ThrowOnMissingAssembly { get; set; } = true;

		[Output]
		public ITaskItem [] ChangedReferencePaths { get; set; }

		[Output]
		public ITaskItem [] RemovedReferencePaths { get; set; }

		protected virtual string MergeOutputDir {
			get {
				return Path.Combine (IntermediateOutputPath, "XbdMerge");
			}
		}

		protected abstract IAssemblyResolver CreateAssemblyResolver ();

		protected virtual Stream LoadResource (string resourceFullPath, string assemblyName)
		{
			return File.OpenRead (resourceFullPath);
		}

		public override bool Execute ()
		{
			var outputDir = MergeOutputDir;
			Directory.CreateDirectory (outputDir);

			var restoreMap = BuildRestoreMap (RestoreAssemblyResources);
			if (restoreMap == null) {
				return false;
			}

			var changedReferencePaths = new List<ITaskItem> ();
			var removedReferencePaths = new List<ITaskItem> ();

			IAssemblyResolver resolver = null;

			foreach (var asm in restoreMap) {
				var asmName = new AssemblyName (asm.Key);
				ITaskItem item = FindMatchingAssembly (InputReferencePaths, asmName);
				if (item == null) {
					if (ThrowOnMissingAssembly)
						return false;
					else
						continue;
				}

				var originalAsmPath = item.GetMetadata ("FullPath");

				//TODO: collision avoidance. AssemblyName MD5? NuGet package ID?
				var mergedAsmPath = Path.Combine (outputDir, asmName.Name + ".dll");

				var mergedFileInfo = new FileInfo (mergedAsmPath);
				if (!mergedFileInfo.Exists || mergedFileInfo.LastWriteTime < File.GetLastWriteTime (originalAsmPath)) {
					if (resolver == null)
						resolver = CreateAssemblyResolver ();
					if (!MergeResources (resolver, originalAsmPath, mergedAsmPath, asm.Key, asm.Value))
						return false;
				}

				removedReferencePaths.Add (item);
				var newItem = new TaskItem (mergedAsmPath);
				item.CopyMetadataTo (newItem);
				changedReferencePaths.Add (newItem);
			}

			ChangedReferencePaths = changedReferencePaths.ToArray ();
			RemovedReferencePaths = removedReferencePaths.ToArray ();

			return true;
		}

		bool MergeResources (IAssemblyResolver resolver, string originalAsmPath, string mergedAsmPath, string assemblyName, List<ITaskItem> resourceItems)
		{
			var disposeList = new List<IDisposable> ();

			try {
				var asmDefinition = AssemblyDefinition.ReadAssembly (originalAsmPath, new ReaderParameters {AssemblyResolver = resolver});

				foreach (var resourceItem in resourceItems) {
					var logicalName = resourceItem.GetMetadata ("LogicalName");

					if (asmDefinition.MainModule.Resources.Where (tr => tr.Name == logicalName).Any ())
						continue;

					var resourceFullPath = resourceItem.GetMetadata ("FullPath");

					// This gives subclasses a chance to deal with the resource however it needs to
					var stream = LoadResource (resourceFullPath, assemblyName);

					// Reset the position if the stream was manipulated
					if (stream.CanSeek && stream.Position != 0)
						stream.Position = 0;

					disposeList.Add (stream);

					var erTemp = new EmbeddedResource (logicalName, ManifestResourceAttributes.Public, stream);
					asmDefinition.MainModule.Resources.Add (erTemp);
				}

				asmDefinition.Write (mergedAsmPath);
				return true;
			} catch (Exception ex) {
				try {
					File.Delete (mergedAsmPath);
				} catch { }
				Log.LogErrorFromException (ex, true);
				return false;
			} finally {
				foreach (var dispose in disposeList) {
					try {
						dispose.Dispose ();
					} catch { }
				}
			}
		}

		ITaskItem FindMatchingAssembly (ITaskItem [] assemblies, AssemblyName name)
		{
			foreach (var asm in assemblies) {
				var asmPath = asm.GetMetadata ("FullPath");
				var filename = Path.GetFileNameWithoutExtension (asmPath);
				if (filename == name.Name) {
					AssemblyName aname;
					try {
						aname = AssemblyName.GetAssemblyName (asmPath);
					} catch {
						Log.LogError ("Unable to read reference '{0}'", asmPath);
						return null;
					}
					//TODO: should the check be more thorough?
					if (aname.Name == name.Name) {
						return asm;
					}
				}
			}
			if (ThrowOnMissingAssembly)
				Log.LogError ("Did not find reference matching RestoreAssemblyResources AssemblyName metadata '{0}'", name);
			return null;
		}

		Dictionary<string, List<ITaskItem>> BuildRestoreMap (ITaskItem [] restoreAssemblyResources)
		{
			var restoreMap = new Dictionary<string, List<ITaskItem>> ();

			foreach (var res in restoreAssemblyResources) {
				var nameStr = res.GetMetadata ("AssemblyName");
				if (string.IsNullOrEmpty (nameStr)) {
					Log.LogError ("Missing AssemblyName metadata on RestoreAssemblyResources item '{0}'", res.ItemSpec);
					return null;
				}
				string canonicalName;
				try {
					canonicalName = new AssemblyName (nameStr).FullName;
				} catch {
					Log.LogError ("Malformed AssemblyName metadata '{0}' on RestoreAssemblyResources item '{1}'", nameStr, res.ItemSpec);
					return null;
				}
				List<ITaskItem> restoreList;
				if (!restoreMap.TryGetValue (canonicalName, out restoreList)) {
					restoreMap [canonicalName] = restoreList = new List<ITaskItem> ();
				}
				restoreList.Add (res);
			}

			return restoreMap;
		}
	}
}
