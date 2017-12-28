// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace YouTubePlayerSample
{
	[Register ("VideoCell")]
	partial class VideoCell
	{
		[Outlet]
		UIKit.UIImageView ImgThumbnail { get; set; }

		[Outlet]
		UIKit.UILabel LblTitle { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImgThumbnail != null) {
				ImgThumbnail.Dispose ();
				ImgThumbnail = null;
			}

			if (LblTitle != null) {
				LblTitle.Dispose ();
				LblTitle = null;
			}
		}
	}
}
