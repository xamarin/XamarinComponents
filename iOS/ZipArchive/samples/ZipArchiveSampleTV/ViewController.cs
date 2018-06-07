using System;
using System.IO;
using Foundation;
using UIKit;

using MiniZip.ZipArchive;

namespace ZipArchiveSampleTV
{
	public partial class ViewController : UIViewController, IZipArchiveDelegate
	{
		private string documentsPath;
		private string zipFolderPath;
		private string outFolderPath;
		private string zipFileName;

		private ZipArchive zip;

		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// set the variables
			documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			zipFolderPath = Path.Combine(documentsPath, "ZipFolder");
			outFolderPath = Path.Combine(documentsPath, "OutputFolder");
			zipFileName = Path.Combine(documentsPath, "file.zip");

			// create the archiver
			zip = new ZipArchive();
			zip.Delegate = this;
		}

		partial void OnZip(UIButton sender)
		{
			// build the zip file contents
			if (!Directory.Exists(zipFolderPath))
			{
				Directory.CreateDirectory(zipFolderPath);
			}
			File.WriteAllText(Path.Combine(zipFolderPath, "textFile.txt"), DateTime.Now.ToString());

			// zip
			zip.CreateZipFile(zipFileName, "passw0rd");
			zip.AddFolder(zipFolderPath, "prefix");
			zip.CloseZipFile();

			// status
			using (var file = File.OpenRead(zipFileName))
			{
				statusLabel.Text = $"Created a zip file:\n{file.Length} bytes";
			}
		}

		partial void OnUnzip(UIButton sender)
		{
			// unzip
			zip.UnzipOpenFile(zipFileName, "passw0rd");
			zip.UnzipFileTo(outFolderPath, true);
			zip.UnzipCloseFile();

			// status
			var extracted = Path.Combine(outFolderPath, "prefix", "ZipFolder", "textFile.txt");
			if (File.Exists(extracted))
			{
				var text = File.ReadAllText(extracted);
				statusLabel.Text = $"Read the zip file:\n'{text}'";
			}
		}

		[Export("ErrorMessage:")]
		private void ErrorMessage(string msg)
		{
			statusLabel.Text = $"Something went wrong:\n{msg}";
		}
	}
}
