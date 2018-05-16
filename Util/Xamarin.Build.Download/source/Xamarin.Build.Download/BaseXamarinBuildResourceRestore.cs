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

		public override bool Execute ()
		{
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

				if (needToWrite) {
					var fileStream = GetWritableFileStream(mergedAsmPath);

					asmDefinition.Write(fileStream, new WriterParameters());

					// Close the file stream, as Mono.Cecil will not do so for streams
					// passed in from outside.
					fileStream.Close();
				}

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

		/// <summary>
		/// Opens a FileStream for writing for a given path, running the garbage collector
		/// and retrying if the file was already open.
		/// </summary>
		/// <param name="filePath">Fully qualified name of the file, or relative file name.</param>
		/// <returns>A FileStream instance with ReadWrite access</returns>
		/// <see cref="System.IO.FileInfo.Open">For exceptions that may be thrown</see>
		private FileStream GetWritableFileStream(string filePath)
		{
			var file = new FileInfo(filePath);

			FileStream stream = null;

			try {
				stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			} catch (IOException) {
				Log.LogWarning("MergeResources: File is locked for writing; this would lead to a sharing violation. Manually triggering GC. File: {0}", filePath);

				GC.Collect();
				GC.WaitForPendingFinalizers();

				// If the file still cannot be opened, a new IOException is thrown
				stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}

			// If either the first or second attempt at opening worked, we have a valid FileStream now.
			return stream;
		}
	}
}
