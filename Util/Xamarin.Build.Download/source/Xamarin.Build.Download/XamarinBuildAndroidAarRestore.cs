using System;
using System.IO.Compression;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using System.Xml.Linq;

namespace Xamarin.Build.Download
{
	public class XamarinBuildAndroidAarRestore : XamarinBuildAndroidResourceRestore
	{
		public bool FixAndroidManifests { get; set; } = true;

		protected override Stream LoadResource (string resourceFullPath, string assemblyName)
		{
			const string AAR_DIR_PREFIX = "library_project_imports";

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

					// Some .aars contain an AndroidManifest.xml in the aapt folder which is essentially a duplicate of the main one
					// but a sanitized version with placeholders like ${applicationId} being escaped to _dollar blah
					// we don't care about these for xamarin.android, which picks up both manifests and merges both
					// This will ensure the 'sanitized' version doesn't get packaged
					if (entryName.TrimStart ('/').Equals ("aapt/AndroidManifest.xml", StringComparison.InvariantCultureIgnoreCase)) {
						Log.LogMessage("Found aapt/AndroidManifest.xml, skipping...");
						// Delete the entry entirely and continue
						oldEntry.Delete();
						continue;
					}

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
