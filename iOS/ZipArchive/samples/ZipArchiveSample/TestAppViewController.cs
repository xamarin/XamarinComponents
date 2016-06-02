using System;
using System.IO;
using UIKit;

using MiniZip.ZipArchive;

namespace ZipArchiveSample
{
	public partial class TestAppViewController : UIViewController
	{
		public TestAppViewController ()
			: base ("TestAppViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();

			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			string zipFolderPath = Path.Combine (documentsPath, "ZipFolder");
			string outFolderPath = Path.Combine (documentsPath, "OutputFolder");
			string zipFileName = Path.Combine (documentsPath, "file.zip");

			// build the zip file contents
			if (!Directory.Exists (zipFolderPath)) {
				Directory.CreateDirectory (zipFolderPath);
			}
			File.WriteAllText (Path.Combine (zipFolderPath, "textFile.txt"), DateTime.Now.ToString ());

			// create the archiver
			ZipArchive zip = new ZipArchive ();
			zip.OnError += (sender, e) => {
				Console.WriteLine ("Something went wrong: {0}", e);
			};

			// create archive
			this.zipButton.TouchUpInside += (sender, e) => {
				zip.CreateZipFile (zipFileName, "passw0rd");
				zip.AddFolder (zipFolderPath, "prefix");
				zip.CloseZipFile ();
			};

			// open archive
			this.unzipButton.TouchUpInside += (sender, e) => {
				zip.UnzipOpenFile (zipFileName, "passw0rd");
				zip.UnzipFileTo (outFolderPath, true);
				zip.UnzipCloseFile ();
			};
		}
	}
}
