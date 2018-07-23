// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace SDWebImageSampleMac
{
	[Register ("ImageListItem")]
	partial class ImageListItem
	{
		[Outlet]
		public AppKit.NSImageView ImageView { get; private set; }

		[Outlet]
		SDWebImageSampleMac.ImageListItemController ParentController { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (ParentController != null) {
				ParentController.Dispose ();
				ParentController = null;
			}
		}
	}
}
