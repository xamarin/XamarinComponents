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
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSCollectionView collectionView { get; set; }

		[Outlet]
		AppKit.NSImageView imageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (collectionView != null) {
				collectionView.Dispose ();
				collectionView = null;
			}

			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}
		}
	}
}
