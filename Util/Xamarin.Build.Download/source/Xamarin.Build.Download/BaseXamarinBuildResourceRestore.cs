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

        const string RES_RESTORE_TASK_KEY = "Xamarin.Build.Download.BaseXamarinBuildResourceRestore-";

		public override bool Execute ()
		{
            cancelTaskSource = new CancellationTokenSource ();

			var additionalFileWrites = new List<string> ();

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

                // Our key is also keyed to the type name since this class is inherited, and could in theory
                // be implemented and used by multiple subclasses
                var taskKey = RES_RESTORE_TASK_KEY + this.GetType().Name + "-" + DownloadUtils.HashMd5(asm.Key);

                // Check to see if another build task instance has already processed this task key and skip
                if (BuildEngine4.GetRegisteredTaskObject(taskKey, RegisteredTaskObjectLifetime.AppDomain) != null) {
                    Log.LogMessage("Xamarin.Build.Download.BaseXamarinBuildResourceRestore already processed: " + asm.Key);
                    continue;
                }

                // Register this task key with the build engine to allow other instances to skip this item
                BuildEngine4.RegisterTaskObject(taskKey, asm.Key, RegisteredTaskObjectLifetime.AppDomain, false);

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
					additionalFileWrites.Add (stampAsmPath);
					Log.LogMessage ("Reference has already had resources merged, skipping due to: {0}", stampAsmPath);

				} else if (OverwriteSourceAssembly || !File.Exists(intermediateAsmPath) || File.GetLastWriteTime (intermediateAsmPath) < File.GetLastWriteTime (originalAsmPath)) {
					if (resolver == null)
						resolver = CreateAssemblyResolver ();
					if (!MergeResources (resolver, originalAsmPath, outputAsmPath, asm.Key, asm.Value))
						return false;

					File.WriteAllText (stampAsmPath, string.Empty);
					additionalFileWrites.Add (stampAsmPath);
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

			AdditionalFileWrites = additionalFileWrites.Select(a => new TaskItem(a)).ToArray ();

			return true;
		}

		bool MergeResources (IAssemblyResolver resolver, string originalAsmPath, string mergedAsmPath, string assemblyName, List<ITaskItem> resourceItems)
		{
			var disposeList = new List<IDisposable> ();
			var sameAssembly = originalAsmPath == mergedAsmPath;

            Stream xlock = null;

			try {
                // obtain an exclusive Lock on the assembly.dll.lock to ensure other instances of this task or the proguard task
                // aren't accessing this dll at the same time
                xlock = DownloadUtils.ObtainExclusiveFileLock(originalAsmPath + ".lock", cancelTaskSource.Token, TimeSpan.FromSeconds(30));
                if (xlock == null)
                    return false;

                // Copy the original file to a temp location so we can work on it
                var asmTmpPath = originalAsmPath + ".tmp";
                File.Copy(originalAsmPath, asmTmpPath, true);

				var asmDefinition = AssemblyDefinition.ReadAssembly (asmTmpPath, new ReaderParameters { AssemblyResolver = resolver });
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

                if (needToWrite) {
                    asmDefinition.Write(asmTmpPath);
                    // Copy our temp/working file back to overwrite the original file
                    File.Copy(asmTmpPath, originalAsmPath, true);
                }

				return true;
			} catch (Exception ex) {
				try {
					if (!sameAssembly && File.Exists(mergedAsmPath))
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

                // Release our exclusive file lock regardless of what happens
                if (xlock != null) {
                    try { xlock.Dispose(); }
                    catch { }
                    xlock = null;
                }
			}
		}

        const string ASM_NAME_TASK_KEY = "Xamarin.Build.Download.AssemblyName-";

        protected ITaskItem FindMatchingAssembly (ITaskItem [] assemblies, AssemblyName name)
		{
			foreach (var asm in assemblies) {
                if (cancelTaskSource?.IsCancellationRequested ?? false)
                    return null;

                var asmPath = asm.GetMetadata("FullPath");
                var filename = Path.GetFileNameWithoutExtension (asmPath);

				if (filename == name.Name) {
                    var taskKey = ASM_NAME_TASK_KEY + DownloadUtils.HashMd5(asmPath);

                    AssemblyName aname;
					try {
                        // Check for an existing task with the same key having already executed
                        // if it exists, we will have the AssemblyName already cached and have no need
                        // to read it again
                        aname = BuildEngine4.GetRegisteredTaskObject(taskKey, RegisteredTaskObjectLifetime.Build) as AssemblyName;
                        if (aname == null) {
                            // XBD uses an exclusive file lock on the filename.dll.lock
                            // to help ensure we aren't accessing the assembly at the same time from multiple tasks/threads
                            using (var xlock = DownloadUtils.ObtainExclusiveFileLock(asmPath + ".lock", cancelTaskSource.Token, TimeSpan.FromSeconds(30)))
                                 aname = AssemblyName.GetAssemblyName(asmPath);
                            // Register the taskKey with the build engine so other tasks know can use the cached AssemblyName
                            BuildEngine4.RegisterTaskObject(taskKey, aname, RegisteredTaskObjectLifetime.Build, false);
                            Log.LogMessage("AssemblyName cached for '{0}'", asmPath);
                        } else {
                            Log.LogMessage("AssemblyName cache hit for '{0}'", asmPath);
                        }
					} catch (Exception ex) {
						Log.LogError ("GetAssemblyName -> Unable to read reference '{0}'", asmPath);
						return null;
					}
					//TODO: should the check be more thorough?
					if (aname.Name == name.Name)
                        return asm;
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
