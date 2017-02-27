using System;
using System.IO.Compression;
using System.IO;
using System.Linq;

namespace Xamarin.Build.Download
{
	public class XamarinBuildAndroidAarRestore : XamarinBuildAndroidResourceRestore
	{
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
						newName = AAR_DIR_PREFIX + Path.DirectorySeparatorChar + newName;

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

						// Copy file contents over if they exist
						if (oldEntry.Length > 0) {
							using (var oldStream = oldEntry.Open ())
							using (var newStream = newEntry.Open ()) {
								oldStream.CopyTo (newStream);
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
