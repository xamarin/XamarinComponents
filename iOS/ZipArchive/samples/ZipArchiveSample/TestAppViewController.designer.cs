// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ZipArchiveSample
{
	[Register ("TestAppViewController")]
	partial class TestAppViewController
	{
		[Outlet]
		UIKit.UIButton selectDateButton { get; set; }

		[Outlet]
		UIKit.UIButton unzipButton { get; set; }

		[Outlet]
		UIKit.UIButton zipButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (unzipButton != null) {
				unzipButton.Dispose ();
				unzipButton = null;
			}

			if (zipButton != null) {
				zipButton.Dispose ();
				zipButton = null;
			}

			if (selectDateButton != null) {
				selectDateButton.Dispose ();
				selectDateButton = null;
			}
		}
	}
}
