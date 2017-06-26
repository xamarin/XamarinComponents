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
		public bool FixAndroidManifests { get; set; } = true;

		[Output]
		public ITaskItem [] AarProguardConfiguration { get; set; }

		string proguardIntermediateOutputPath = null;

		public override bool Execute ()
		{
			// Get the dir to store proguard config files in
			proguardIntermediateOutputPath = Path.Combine (MergeOutputDir, "proguard");
			if (!Directory.Exists (proguardIntermediateOutputPath))
				Directory.CreateDirectory (proguardIntermediateOutputPath);


			var additionalFileWrites = new List<ITaskItem> ();

			var outputDir = MergeOutputDir;
			Directory.CreateDirectory (outputDir);

			var restoreMap = base.BuildRestoreMap (RestoreAssemblyResources);
			if (restoreMap == null) {
				return false;
			}

			IAssemblyResolver resolver = CreateAssemblyResolver ();

			foreach (var asm in restoreMap) {
				var asmName = new AssemblyName (asm.Key);
				ITaskItem item = FindMatchingAssembly (InputReferencePaths, asmName);
				if (item == null) {
					if (ThrowOnMissingAssembly)
						return false;
					else
						continue;
				}

				var intermediateAsmPath = Path.Combine (outputDir, asmName.Name + ".dll");

				var saveNameHash = DownloadUtils.HashMd5 (asmName.Name)?.Substring (0, 8);

				// Keep a stamp file around to avoid reprocessing
				var stampPath = Path.Combine (outputDir, saveNameHash + ".proguard.stamp");
				if (File.Exists (stampPath))
					continue;

				var resourceItems = asm.Value;

				var entryCount = 0;

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
							var proguardSaveFilename = Path.Combine (proguardIntermediateOutputPath, saveNameHash + entryCount + ".txt");

							// Save out the proguard file
							using (var entryStream = entry.Open ())
							using (var fs = File.Create (proguardSaveFilename)) {
								entryStream.CopyTo (fs);
								fs.Flush ();
								fs.Close ();
							}

							entryCount++;
						}
					}

					entryCount++;
				}

				File.WriteAllText (stampPath, string.Empty);
				additionalFileWrites.Add (new TaskItem (stampPath));

			}

			AdditionalFileWrites = additionalFileWrites.ToArray ();

			return true;
		}

		protected override Stream LoadResource (string resourceFullPath, string assemblyName)
		{
			Log.LogMessage ("LoadResource: {0}", resourceFullPath);
			Log.LogMessage ("  for:        {0}", assemblyName);

			const string AAR_DIR_PREFIX = "library_project_imports";

			var assemblyNameMd5 = DownloadUtils.HashMd5 (assemblyName).Substring (0, 8);

			var memoryStream = new MemoryStream ();
			using (var fileStream = base.LoadResource (resourceFullPath, assemblyName))
				fileStream.CopyTo (memoryStream);

			using (var zipArchive = new ZipArchive (memoryStream, ZipArchiveMode.Update, true)) {

				var entryNames = zipArchive.Entries.Select (zae => zae.FullName).ToList ();

				Log.LogMessage ("Found {0} entries in {1}", entryNames.Count, resourceFullPath);

				foreach (var entryName in entryNames) {

					// Calculate the new name with the aar directory prefix
					var newName = entryName;
					if (!entryName.StartsWith (AAR_DIR_PREFIX, StringComparison.InvariantCulture))
						newName = AAR_DIR_PREFIX + "/" + newName;

					// Open the old entry
					var oldEntry = zipArchive.GetEntry (entryName);

					// We are only re-adding non empty folders, otherwise we end up with a corrupt zip in mono
					if (!string.IsNullOrEmpty (oldEntry.Name)) {

						// SPOILER ALERT: UGLY WORKAROUND
						// In the Android Support libraries, there exist multiple .aar files which have a `libs/internal_impl-25.0.0` file.
						// In Xamarin.Android, there is a Task "CheckDuplicateJavaLibraries" which inspects jar files being pulled in from .aar files
						// in assemblies to see if there exist any files with the same name but different content, and will throw an error if it finds any.
						// However, for us, it is perfectly valid to have this scenario and we should not see an error.
						// We are working around this by detecting files named like this, and renaming them to some unique value
						// in this case, a part of the hash of the assembly name.
						var newFile = Path.GetFileName (newName);
						var newDir = Path.GetDirectoryName (newName);

						if (newFile.StartsWith ("internal_impl", StringComparison.InvariantCulture))
							newName = Path.Combine (newDir, "internal_impl-" + DownloadUtils.HashSha1 (assemblyName).Substring (0, 6) + ".jar");

						Log.LogMessage ("Renaming: {0} to {1}", entryName, newName);

						// Create a new entry based on our new name
						var newEntry = zipArchive.CreateEntry (newName);

						// Since Xamarin.Android's AndoridManifest.xml merging code is not as sophisticated as gradle's yet, we may need
						// to fix some things up in the manifest file to get it to merge properly into our applications
						// Here we will check to see if Fixing manifests was enabled, and if the entry we are on is the AndroidManifest.xml file
						if (FixAndroidManifests && oldEntry.Length > 0 && newName.EndsWith ("AndroidManifest.xml", StringComparison.OrdinalIgnoreCase)) {

							// android: namespace
							XNamespace xns = "http://schemas.android.com/apk/res/android";

							using (var oldStream = oldEntry.Open ())
							using (var xmlReader = System.Xml.XmlReader.Create (oldStream)) {
								var xdoc = XDocument.Load (xmlReader);

								// BEGIN FIXUP #1
								// Some `android:name` attributes will start with a . indicating, that the `package` value of the `manifest` element
								// should be prefixed dynamically/at merge to this attribute value.  Xamarin.Android doesn't handle this case yet
								// so we are going to manually take care of it.

								// Get the package name from the manifest node
								var packageName = xdoc.Document.Descendants ("manifest")?.FirstOrDefault ()?.Attribute ("package")?.Value;

								if (!string.IsNullOrEmpty (packageName)) {
									// Find all elements in the xml document that have a `android:name` attribute which starts with a .
									// Select all of them, and then change the `android:name` attribute value to be the
									// package name we found in the `manifest` element previously + the original attribute value
									xdoc.Document.Descendants ()
										.Where (elem => elem.Attribute (xns + "name")?.Value?.StartsWith (".", StringComparison.Ordinal) ?? false)
										.ToList ()
										.ForEach (elem => elem.SetAttributeValue (xns + "name", packageName + elem.Attribute (xns + "name").Value));
								}
								// END FIXUP #1

								using (var newStream = newEntry.Open ())
								using (var xmlWriter = System.Xml.XmlWriter.Create (newStream)) {
									xdoc.WriteTo (xmlWriter);
								}
							}

							// Xamarin.Android does not consider using proguard config files from within .aar files, so we need to extract it
							// to a known location and output it to 
						} else if (oldEntry.Length > 0 && (newName.EndsWith ("proguard.txt", StringComparison.OrdinalIgnoreCase) || newName.EndsWith ("proguard.cfg", StringComparison.OrdinalIgnoreCase))) {


							// We still want to copy the file over into the .aar
							using (var oldStream = oldEntry.Open ())
							using (var newStream = newEntry.Open ()) {
								oldStream.CopyTo (newStream);
							}

							// Calculate an output path beside the merged/output assembly name's md5 hash
							var proguardCfgOutputPath = Path.Combine (proguardIntermediateOutputPath, assemblyNameMd5 + ".txt");

							Log.LogMessage ("Extracting Proguard Configuration to: {0}", proguardCfgOutputPath);

							// Create a copy of the file
							using (var oldStream = oldEntry.Open ())
							using (var proguardStream = File.OpenWrite (proguardCfgOutputPath)) {
								oldStream.CopyTo (proguardStream);
							}
						} else {
							// Copy file contents over if they exist
							if (oldEntry.Length > 0) {
								using (var oldStream = oldEntry.Open ())
								using (var newStream = newEntry.Open ()) {
									oldStream.CopyTo (newStream);
								}
							}
						}
					}

					// Delete the old entry regardless of if it's a folder or not
					oldEntry.Delete ();
				}
			}

			memoryStream.Position = 0;
			return memoryStream;
		}
	}
}
