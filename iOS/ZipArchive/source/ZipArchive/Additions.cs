using System;
using Foundation;

namespace MiniZip.ZipArchive
{
	partial class ZipArchive
	{
		public nint AddFolder (string path, string prefix)
		{
			nint fileCount = 0;

			NSError error = null;
			var	dirArray = NSFileManager.DefaultManager.GetDirectoryContent (path, out error);

			if (string.IsNullOrEmpty (prefix)) {
				prefix = new NSString (path).LastPathComponent;
			} else {
				prefix = new NSString (prefix).AppendPathComponent (new NSString (path).LastPathComponent);
			}

			AddFile (path, prefix + "/");

			for (int i = 0; i < dirArray.Length; i++) {
				var dirItem = dirArray [i];
				var path_dirItem = new NSString (path).AppendPathComponent (new NSString (dirItem));

				var	dict = NSFileManager.DefaultManager.GetAttributes (path_dirItem);

				if (dict.Type == NSFileType.Directory || dict.Type == NSFileType.SymbolicLink) {
					//Recursively do subfolders.
					fileCount += AddFolder (path_dirItem, prefix);
				} else {
					var prefixedName = prefix.Length > 0 ? new NSString (prefix).AppendPathComponent (new NSString (dirItem)) : dirItem;

					//Count if added OK.
					if (AddFile (path_dirItem, prefixedName)) {
						fileCount++;
					}
				}
			}

			return fileCount;
		}
	}
}
