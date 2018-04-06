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

		CancellationTokenSource cancelTaskSource = new CancellationTokenSource();

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
            var xbdLockFile = Path.Combine(DownloadUtils.GetCacheDir(), "xbd.lock");

            using (var xlck = DownloadUtils.ObtainExclusiveFileLock(xbdLockFile, CancelToken, TimeSpan.FromSeconds(30), Log))
            {
                if (xlck == null)
                    return false;

                return execute();
            }
        }

        bool execute ()
        {
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
				var originalAsmInfo = new FileInfo(item.GetMetadata ("FullPath"));
				var intermediateAsmInfo = new FileInfo (Path.Combine (outputDir, asm.Value.CanonicalName.Name + ".dll"));
				var outputAsmInfo = OverwriteSourceAssembly ? originalAsmInfo : intermediateAsmInfo;
				var stampAsmInfo = new FileInfo(intermediateAsmInfo.FullName + ".stamp");
                var procAsmInfo = new FileInfo(originalAsmInfo.FullName + ".proc");

                // Check if stamp file already exists and is newer than the original input assembly to fix up
                if (stampAsmInfo.Exists && stampAsmInfo.LastWriteTimeUtc >= originalAsmInfo.LastWriteTimeUtc) {
                    Log.LogMessage("Reference has already had resources merged, skipping due to: {0}", stampAsmInfo.FullName);
                } else if (OverwriteSourceAssembly && procAsmInfo.Exists && procAsmInfo.LastWriteTimeUtc >= originalAsmInfo.LastWriteTimeUtc) {
                    Log.LogMessage("Reference has already had resources merged, skipping due to: {0}", procAsmInfo.FullName);
                } else {
                    // Lock the output assembly .lock file path so nothing else in our own tasks will read/write it
                    using (var xlock = DownloadUtils.ObtainExclusiveFileLock(originalAsmInfo.FullName + ".lock", cancelTaskSource.Token, TimeSpan.FromSeconds(10), Log)) {
                        if (xlock == null) {
                            Log.LogMessage("Couldn't obtain exclusive file lock for: {0}, skipping...", outputAsmInfo.FullName + ".lock");
                            return false;
                        }

                        stampAsmInfo.Refresh();
                        procAsmInfo.Refresh();

                        // See if the stamp file already exists and is newer than the file we're processing
                        if (stampAsmInfo.Exists && stampAsmInfo.LastWriteTimeUtc >= originalAsmInfo.LastWriteTimeUtc) {
                            Log.LogMessage("Reference has already had resources merged, skipping due to: {0}", stampAsmInfo.FullName);
                        } else if (OverwriteSourceAssembly && procAsmInfo.Exists && procAsmInfo.LastWriteTimeUtc >= originalAsmInfo.LastWriteTimeUtc) {
                            Log.LogMessage("Reference has already had resources merged, skipping due to: {0}", procAsmInfo.FullName);
                        } else {
                            DownloadUtils.TouchFile(stampAsmInfo.FullName);
                            if (OverwriteSourceAssembly)
                                DownloadUtils.TouchFile(procAsmInfo.FullName);

                            if (OverwriteSourceAssembly || !intermediateAsmInfo.Exists || intermediateAsmInfo.LastWriteTimeUtc < originalAsmInfo.LastWriteTime) {
                                if (resolver == null)
                                    resolver = CreateAssemblyResolver();
                                if (!MergeResources(resolver, originalAsmInfo.FullName, outputAsmInfo.FullName, asm.Key, asm.Value.RestoreItems))
                                    return false;

                                // Update the stamp file since we may have just processed the original assembly again
                                // so we need the stamp file last write to be >= the original assembly last write for cache validation of subsequent builds 
                                DownloadUtils.TouchFile(stampAsmInfo.FullName);
                                if (OverwriteSourceAssembly)
                                    DownloadUtils.TouchFile(procAsmInfo.FullName);
                            }
                        }                        
                    }
				}

                // This stamp file goes in intermediate output paths, so we need to track it in FileWrites
                additionalFileWrites.Add(new TaskItem(stampAsmInfo.FullName));

                removedReferencePaths.Add (item);
				var newItem = new TaskItem (intermediateAsmInfo.FullName);
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

            var tempAsmPath = Path.GetTempFileName();

            // Copy the original to a temp file to work on
            if (!DownloadUtils.CopyFileWithRetry(originalAsmPath, tempAsmPath, Log))
                return false;
            
            var needToWrite = !sameAssembly;

            try {
                using (var xlockedAsm = File.Open(tempAsmPath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                using (var asmDefinition = AssemblyDefinition.ReadAssembly(xlockedAsm, new ReaderParameters { AssemblyResolver = resolver })) {
                   
                    foreach (var resourceItem in resourceItems) {
                        var logicalName = resourceItem.GetMetadata("LogicalName");

                        // Check if the resource already exists and skip if so
                        if (asmDefinition.MainModule.Resources.Any(tr => tr.Name == logicalName))
                            continue;

                        var resourceFullPath = resourceItem.GetMetadata("FullPath");

                        // This gives subclasses a chance to deal with the resource however it needs to
                        var stream = LoadResource(resourceFullPath, assemblyName);
                        disposeList.Add(stream);

                        // Reset the position if the stream was manipulated
                        if (stream.CanSeek && stream.Position != 0)
                            stream.Position = 0;

                        var erTemp = new EmbeddedResource(logicalName, ManifestResourceAttributes.Public, stream);
                        asmDefinition.MainModule.Resources.Add(erTemp);
                        needToWrite = true;
                    }

                    if (needToWrite)
                    {
                        var writeSucceeded = false;
                        for (int i = 0; i < 20; i++)
                        {
                            try {
                                asmDefinition.Write(originalAsmPath);
                                writeSucceeded = true;
                                break;
                            } catch {
                                Log.LogMessage("Write Assembly Changes failed: {0}", originalAsmPath);
                                Thread.Sleep(250);
                            }
                        }

                        if (!writeSucceeded) {
                            Log.LogError("Failed to write assembly changed: {0}", originalAsmPath);
                            return false;
                        }
                    }
                }
				
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
            
            return true;
        }

		protected Dictionary<string, AssemblyRestoreMapping> BuildRestoreMap (ITaskItem [] restoreAssemblyResources, ITaskItem[] inputReferences, CancellationToken cancelToken)
		{
			// First load up all the assembly names from the input references
			var inputRefAssemblyNames = new List<Tuple<ITaskItem, AssemblyName>> ();

			foreach (var iasm in inputReferences) {
				var asmPath = iasm.GetMetadata ("FullPath");

				// See if the build has cached this assembly name to input reference mapping already
				var asmCacheKey = "XBD-AssemblyName-" + DownloadUtils.HashMd5 (asmPath);
                //var refInfo = BuildEngine4.GetRegisteredTaskObject (asmCacheKey, RegisteredTaskObjectLifetime.Build) as Tuple<ITaskItem, AssemblyName>;
                Tuple<ITaskItem, AssemblyName> refInfo = null;

                if (refInfo == null) {
					AssemblyName aname = null;

					// Try and get the assembly name a few times in case something is locking it
					for (int i = 0; i < 20; i++) {
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
					//BuildEngine4.RegisterTaskObject (asmCacheKey, refInfo, RegisteredTaskObjectLifetime.Build, false);
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

                if (refInfo == null)
                {
                    Log.LogError("Missing Input Reference Assembly: {0}", asmName.Name);
                    continue;
                }

				AssemblyRestoreMapping mapping;
				if (!restoreMap.TryGetValue (canonicalName, out mapping)) {
                    mapping = new AssemblyRestoreMapping {
                        RestoreItems = new List<ITaskItem>(),
                        InputAssemblyReference = refInfo.Item1,
                        CanonicalName = asmName
                    };
                    restoreMap[canonicalName] = mapping;
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
