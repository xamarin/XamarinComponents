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

		CancellationTokenSource cancelTaskSource;

		protected CancellationToken CancelToken {
			get { return cancelTaskSource.Token; }
		}

		public virtual void Cancel ()
		{
			if (!cancelTaskSource.IsCancellationRequested)
				cancelTaskSource.Cancel ();
		}

		public override bool Execute ()
		{
			cancelTaskSource = new CancellationTokenSource ();

			var additionalFileWrites = new List<ITaskItem> ();

			var outputDir = MergeOutputDir;
			Directory.CreateDirectory (outputDir);

			var restoreMap = BuildRestoreMap (RestoreAssemblyResources, InputReferencePaths, CancelToken);
			if (restoreMap == null) {
				return false;
			}

			var changedReferencePaths = new List<ITaskItem> ();
			var removedReferencePaths = new List<ITaskItem> ();

			IAssemblyResolver resolver = null;

			foreach (var asm in restoreMap) {
				ITaskItem item = asm.Value.InputAssemblyReference;
				if (item == null) {
					if (ThrowOnMissingAssembly)
						return false;
					else
						continue;
				}

				//TODO: collision avoidance. AssemblyName MD5? NuGet package ID?
				var originalAsmPath = item.GetMetadata ("FullPath");
				var intermediateAsmPath = Path.Combine (outputDir, asm.Value.CanonicalName.Name + ".dll");
				var outputAsmPath = OverwriteSourceAssembly ? originalAsmPath : intermediateAsmPath;
				var stampAsmPath = intermediateAsmPath + ".stamp";

				var asmTaskKey = "XBD-" + this.GetType ().Name + "-" + DownloadUtils.HashMd5 (outputAsmPath);
				var existingTask = BuildEngine4.GetRegisteredTaskObject (asmTaskKey, RegisteredTaskObjectLifetime.Build);

				if (existingTask != null) {
					Log.LogMessage ("Another task is already processing resources for this item, skipping: {0}", outputAsmPath);
				} else if (File.Exists (stampAsmPath)) {
					additionalFileWrites.Add (new TaskItem (stampAsmPath));
					Log.LogMessage ("Reference has already had resources merged, skipping due to: {0}", stampAsmPath);
				} else {
					// Lock the output assembly .lock file path so nothing else in our own tasks will read/write it
					using (var xlock = DownloadUtils.ObtainExclusiveFileLock (outputAsmPath + ".lock", cancelTaskSource.Token, TimeSpan.FromSeconds (30))){
						// If we were waiting on a lock for this assembly there's a good chance we were waiting because another 
						// instance was already doing this work, so let's check once more to see if the work was done 
						existingTask = BuildEngine4.GetRegisteredTaskObject (asmTaskKey, RegisteredTaskObjectLifetime.Build);
						if (existingTask != null) {
							Log.LogMessage ("Another task is already processing resources for this item, skipping: {0}", outputAsmPath);
						} else {
							// Register with the build engine that we're handling processing this item
							BuildEngine4.RegisterTaskObject (asmTaskKey, outputAsmPath, RegisteredTaskObjectLifetime.Build, false);
							if (OverwriteSourceAssembly || !File.Exists (intermediateAsmPath) || File.GetLastWriteTime (intermediateAsmPath) < File.GetLastWriteTime (originalAsmPath)) {
								if (resolver == null)
									resolver = CreateAssemblyResolver ();
								if (!MergeResources (resolver, originalAsmPath, outputAsmPath, asm.Key, asm.Value.RestoreItems))
									return false;

								File.WriteAllText (stampAsmPath, string.Empty);
								additionalFileWrites.Add (new TaskItem (stampAsmPath));
							}
						}
					}
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
			var disposeList = new List<IDisposable> ();
			var sameAssembly = originalAsmPath == mergedAsmPath;

			try {
				var asmDefinition = AssemblyDefinition.ReadAssembly (originalAsmPath, new ReaderParameters {AssemblyResolver = resolver});
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

				return true;
			} catch (Exception ex) {
				try {
					if (!sameAssembly) File.Delete (mergedAsmPath);
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

		protected Dictionary<string, AssemblyRestoreMapping> BuildRestoreMap (ITaskItem [] restoreAssemblyResources, ITaskItem[] inputReferences, CancellationToken cancelToken)
		{
			// First load up all the assembly names from the input references
			var inputRefAssemblyNames = new List<Tuple<ITaskItem, AssemblyName>> ();

			foreach (var iasm in inputReferences) {
				var asmPath = iasm.GetMetadata ("FullPath");

				// See if the build has cached this assembly name to input reference mapping already
				var asmCacheKey = "XBD-AssemblyName-" + DownloadUtils.HashMd5 (asmPath);
				var refInfo = BuildEngine4.GetRegisteredTaskObject (asmCacheKey, RegisteredTaskObjectLifetime.Build) as Tuple<ITaskItem, AssemblyName>;
				if (refInfo == null) {
					AssemblyName aname = null;

					// Try and get the assembly name a few times in case something is locking it
					for (int i = 0; i < 10; i++) {
						try {
							//using (var xlock = DownloadUtils.ObtainExclusiveFileLock (asmPath + ".lock", cancelToken, TimeSpan.FromSeconds (30)))
								aname = AssemblyName.GetAssemblyName (asmPath);
							break;
						} catch {
							Thread.Sleep (250);
						}
					}

					// If still null, we failed several attempts
					if (aname == null) {
						Log.LogError ("Unable to read reference '{0}'", asmPath);
						return null;
					}

					refInfo = Tuple.Create (iasm, aname);
					BuildEngine4.RegisterTaskObject (asmCacheKey, refInfo, RegisteredTaskObjectLifetime.Build, false);
				}
				inputRefAssemblyNames.Add (refInfo);
			}

			var restoreMap = new Dictionary<string, AssemblyRestoreMapping> ();

			foreach (var res in restoreAssemblyResources) {
				// This will just be `Xamarin.Some.AssemblyName` not an actual filename
				var nameStr = res.GetMetadata ("AssemblyName");
				if (string.IsNullOrEmpty (nameStr)) {
					Log.LogError ("Missing AssemblyName metadata on RestoreAssemblyResources item '{0}'", res.ItemSpec);
					return null;
				}

				string canonicalName;
				try {
					// FYI This doesn't incur loading an assemblyname info from an actual file
					canonicalName = new AssemblyName (nameStr).FullName;
				} catch {
					Log.LogError ("Malformed AssemblyName metadata '{0}' on RestoreAssemblyResources item '{1}'", nameStr, res.ItemSpec);
					return null;
				}

				var asmName = new AssemblyName (canonicalName);

				// Find the matching input reference assembly for the given restore item assembly canonical name
				// which comes from our targets (eg: <_XbdAssemblyName_x>Xamarin.GooglePlayServices.Location</_XbdAssemblyName_x> )
				var refInfo = inputRefAssemblyNames.FirstOrDefault (i => i.Item2.Name == asmName.Name);

				AssemblyRestoreMapping mapping;
				if (!restoreMap.TryGetValue (canonicalName, out mapping)) {
					restoreMap [canonicalName] = mapping = new AssemblyRestoreMapping {
						RestoreItems = new List<ITaskItem> (),
						InputAssemblyReference = refInfo.Item1,
						CanonicalName = asmName
					};;
				}
				mapping.RestoreItems.Add (res);
			}

			return restoreMap;
		}

		public struct AssemblyRestoreMapping
		{
			public List<ITaskItem> RestoreItems;
			public ITaskItem InputAssemblyReference;
			public AssemblyName CanonicalName;
		}
	}
}
