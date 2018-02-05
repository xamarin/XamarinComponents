using System;
using System.IO.Compression;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using System.Xml.Linq;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using Mono.Cecil;
using System.Reflection;

namespace Xamarin.Build.Download
{
	public class XamarinBuildAndroidAarProguardConfigs : XamarinBuildAndroidResourceRestore
	{
        const string TASK_KEY = "Xamarin.Build.Download.AarProugardConfigs-";

		public bool FixAndroidManifests { get; set; } = true;

		[Output]
		public ITaskItem [] AarProguardConfiguration { get; set; }

        string proguardIntermediateOutputPath = null;

		// We intentionally won't call the base implementation in this override
		// since other tasks should handle the restores
		// This task is just responsible for extracting proguard config files
		// from the .aar input files so we are reusing the base task to help
		// track down the .aar files themselves.
		public override bool Execute ()
		{
            // Get the dir to store proguard config files in
            proguardIntermediateOutputPath = Path.Combine (MergeOutputDir, "proguard");
			if (!Directory.Exists (proguardIntermediateOutputPath))
				Directory.CreateDirectory (proguardIntermediateOutputPath);

			var additionalFileWrites = new List<string> ();

			// Make sure our XbdMerge directory exists
			var outputDir = MergeOutputDir;
			Directory.CreateDirectory (outputDir);

			// Get our assembly restore map
			var restoreMap = BuildRestoreMap (RestoreAssemblyResources);
			if (restoreMap == null)
				return false;

			// Look through all the assemblies we would restore for
			foreach (var asm in restoreMap) {

                var taskKey = TASK_KEY + DownloadUtils.HashMd5(asm.Key);

                if (BuildEngine4.GetRegisteredTaskObject(taskKey, RegisteredTaskObjectLifetime.AppDomain) != null) {
                    Log.LogMessage("Xamarin.Build.Download.AarProguardConfigs already processed for: " + asm.Key);
                    continue;
                }

                BuildEngine4.RegisterTaskObject(taskKey, asm.Key, RegisteredTaskObjectLifetime.AppDomain, false);


                // We only want to find proguard files in .aar files referenced
                // for assemblies we actually have referenced and have them mapped to
                var asmName = new AssemblyName (asm.Key);

				ITaskItem item = FindMatchingAssembly (InputReferencePaths, asmName);
				if (item == null) {
					if (ThrowOnMissingAssembly)
						return false;
					else
						continue;
				}

				// Use a hash for the assembly name to keep paths shorter
				var saveNameHash = DownloadUtils.HashMd5 (asmName.Name)?.Substring (0, 8);

				// We keep a stamp file around to avoid reprocessing, so skip if it exists
				var stampPath = Path.Combine (outputDir, saveNameHash + ".proguard.stamp");

                // If we have a stamp file, it should contain any proguard config files that
                // were previously processed.  These need to be emitted still as FileWrites
                if (File.Exists(stampPath)) {
                    // Each line should be a filename
                    var stampLines = File.ReadAllLines(stampPath);
                    if (stampLines != null && stampLines.Any()) {
                        foreach (var line in stampLines) {
                            if (File.Exists(line)) // make sure we only add files that exist
                                additionalFileWrites.Add(line);
                        }
                    }
                    // the stamp file itself is a filewrites file
                    additionalFileWrites.Add(stampPath);
                    continue;
                }

				// Get all the mapped .aar files
				var resourceItems = asm.Value;

				// We want to increment on the hash name in case there are multiple .aar files and/or proguard config
				// files for a given assembly, so we use them all and not overwrite the same name
				var entryCount = 0;

				// In theory we could have multiple .aar files? Probably never happen...
				foreach (var resourceItem in resourceItems) {
					
					// Full path to .aar file
					var resourceFullPath = resourceItem.GetMetadata ("FullPath");

					using (var fileStream = File.OpenRead (resourceFullPath))
					using (var zipArchive = new ZipArchive (fileStream, ZipArchiveMode.Read)) {

						// Look for proguard config files in the archive
						foreach (var entry in zipArchive.Entries) {
							
							// Skip entries which are not proguard configs
							if (!entry.Name.Equals ("proguard.txt", StringComparison.OrdinalIgnoreCase)
								&& !entry.Name.Equals ("proguard.cfg", StringComparison.OrdinalIgnoreCase))
								continue;

							// Figure out our destination filename
							var proguardSaveFile = new FileInfo(Path.Combine (proguardIntermediateOutputPath, saveNameHash + entryCount + ".txt"));

							// Add this to our file writes
							additionalFileWrites.Add (proguardSaveFile.FullName);

							// Save out the proguard file
							using (var entryStream = entry.Open ())
							using (var fs = File.Create (proguardSaveFile.FullName)) {
								entryStream.CopyTo (fs);
								fs.Flush ();
								fs.Close ();
							}

							entryCount++;
						}
					}
				}

				// *.proguard.stamp files are additional file writes
				File.WriteAllText (stampPath, string.Join(Environment.NewLine, additionalFileWrites));
				additionalFileWrites.Add (stampPath);
			}

            AdditionalFileWrites = additionalFileWrites.Select(a => new TaskItem(a)).ToArray();

			return true;
		}
	}
}
