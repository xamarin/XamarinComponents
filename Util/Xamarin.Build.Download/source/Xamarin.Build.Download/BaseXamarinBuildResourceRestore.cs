using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Xamarin.MacDev;

namespace Xamarin.Build.Download
{
	public abstract class BaseXamarinBuildResourceRestore : Task, ICancelableTask
	{
		[Required]
		public string IntermediateOutputPath { get; set; }

		[Required]
		public ITaskItem [] InputReferencePaths { get; set; }

		[Required]
		public ITaskItem [] RestoreAssemblyResources { get; set; }

		public string VsInstallRoot { get; set; }

		public bool ThrowOnMissingAssembly { get; set; } = true;

		public virtual bool  OverwriteSourceAssembly { get; set; } = true;

		[Output]
		public ITaskItem [] ChangedReferencePaths { get; set; }

		[Output]
		public ITaskItem [] RemovedReferencePaths { get; set; }

		[Output]
		public ITaskItem [] AdditionalFileWrites { get; set; }

		CancellationTokenSource cancelTaskSource;

		protected virtual string MergeOutputDir {
			get {
				return Path.Combine (IntermediateOutputPath, "XbdMerge");
			}
		}

		protected IBuildEngine4 BuildEngine4 {
			get {
				return (IBuildEngine4)BuildEngine;
			}
		}

		protected abstract IAssemblyResolver CreateAssemblyResolver ();

		protected virtual Stream LoadResource (string resourceFullPath, string assemblyName)
		{
			return File.OpenRead (resourceFullPath);
		}

		public void Cancel ()
		{
			if (cancelTaskSource != null && !cancelTaskSource.IsCancellationRequested)
				cancelTaskSource.Cancel ();
		}

		public override bool Execute ()
		{
			cancelTaskSource = new CancellationTokenSource ();

			var additionalFileWrites = new List<ITaskItem> ();

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

				if (cancelTaskSource.IsCancellationRequested)
					return false;

				var asmName = new AssemblyName (asm.Key);
				ITaskItem item = FindMatchingAssembly (InputReferencePaths, asmName);
				if (item == null) {
					if (ThrowOnMissingAssembly)
						return false;
					else
						continue;
				}


				//TODO: collision avoidance. AssemblyName MD5? NuGet package ID?
				var originalAsmPath = item.GetMetadata ("FullPath");
				var intermediateAsmPath = Path.Combine (outputDir, asmName.Name + ".dll");
				var outputAsmPath = OverwriteSourceAssembly ? originalAsmPath : intermediateAsmPath;
				var stampAsmPath = intermediateAsmPath + ".stamp";

				if (File.Exists (stampAsmPath)) {
					additionalFileWrites.Add (new TaskItem (stampAsmPath));
					Log.LogMessage ("Reference has already had resources merged, skipping due to: {0}", stampAsmPath);

				} else if (OverwriteSourceAssembly || !File.Exists(intermediateAsmPath) || File.GetLastWriteTime (intermediateAsmPath) < File.GetLastWriteTime (originalAsmPath)) {
					if (resolver == null)
						resolver = CreateAssemblyResolver ();
					if (!MergeResources (resolver, originalAsmPath, outputAsmPath, asm.Key, asm.Value))
						return false;

					File.WriteAllText (stampAsmPath, string.Empty);
					additionalFileWrites.Add (new TaskItem (stampAsmPath));
				}

				removedReferencePaths.Add (item);
				var newItem = new TaskItem (intermediateAsmPath);
				item.CopyMetadataTo (newItem);
				changedReferencePaths.Add (newItem);
			}

			if (OverwriteSourceAssembly) {
				ChangedReferencePaths = new ITaskItem [0];
				RemovedReferencePaths = new ITaskItem [0];
			} else {
				ChangedReferencePaths = changedReferencePaths.ToArray ();
				RemovedReferencePaths = removedReferencePaths.ToArray ();
			}

			AdditionalFileWrites = additionalFileWrites.ToArray ();

			return true;
		}

		bool MergeResources (IAssemblyResolver resolver, string originalAsmPath, string mergedAsmPath, string assemblyName, List<ITaskItem> resourceItems)
		{
			// Check the build engine for an existing registered task object for this assembly
			// If it exists, another task in parallel is already or has already handled merging resources
			// for this assembly, so we can just skip it.
			var registeredTaskObjectKey = "Xamarin.Build.Download.MergeResources." + DownloadUtils.HashMd5 (originalAsmPath);
			var existingTaskObject = BuildEngine4.GetRegisteredTaskObject (registeredTaskObjectKey, RegisteredTaskObjectLifetime.Build);
			if (existingTaskObject != null)
				return true;

			// Register this assembly as a task object so other parallel invocations will know to skip it
			// as we're already doing the work here
			BuildEngine4.RegisterTaskObject (registeredTaskObjectKey, originalAsmPath, RegisteredTaskObjectLifetime.Build, false);

			var disposeList = new List<IDisposable> ();
			var sameAssembly = originalAsmPath == mergedAsmPath;

			Stream exclusiveLock = null;
			var asmLockFilePath = originalAsmPath + ".lock";
			var writeSucceded = false;

			try {
				// Try to obtain an exclusive write lock to the lock file of the assembly
				// we're wroking with
				exclusiveLock = DownloadUtils.ObtainExclusiveFileLock (asmLockFilePath, cancelTaskSource.Token, TimeSpan.FromSeconds (60));
				if (exclusiveLock == null)
					return false;

				var asmDefinition = AssemblyDefinition.ReadAssembly (originalAsmPath, new ReaderParameters { AssemblyResolver = resolver });
				var needToWrite = !sameAssembly;

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
					needToWrite = true;
				}

				if (needToWrite)
					asmDefinition.Write (mergedAsmPath);

				writeSucceded = true;
				return true;
			} catch (Exception ex) {
				try {
					if (!sameAssembly) File.Delete (mergedAsmPath);
				} catch { }
				Log.LogErrorFromException (ex, true);
				return false;
			} finally {
				// If we failed to write the assembly, we should unregister this item
				// so another invocation can still try again
				if (!writeSucceded)
					BuildEngine4.UnregisterTaskObject (registeredTaskObjectKey, RegisteredTaskObjectLifetime.Build);

				foreach (var dispose in disposeList) {
					try {
						dispose.Dispose ();
					} catch { }
				}

				try {
					if (File.Exists (asmLockFilePath))
						File.Delete (asmLockFilePath);
				} catch { }

				try {
					exclusiveLock?.Dispose ();
				} catch { }

				exclusiveLock = null;
			}
		}

		protected ITaskItem FindMatchingAssembly (ITaskItem [] assemblies, AssemblyName name)
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

		protected Dictionary<string, List<ITaskItem>> BuildRestoreMap (ITaskItem [] restoreAssemblyResources)
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
