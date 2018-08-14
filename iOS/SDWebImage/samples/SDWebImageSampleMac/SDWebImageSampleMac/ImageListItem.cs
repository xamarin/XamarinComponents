using System;
using AppKit;
using Foundation;

namespace SDWebImageSampleMac
{
	public partial class ImageListItem : NSView
	{
		public ImageListItem(IntPtr handle)
			: base(handle)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public ImageListItem(NSCoder coder)
			: base(coder)
		{
			Initialize();
		}

		private void Initialize()
		{
		}
	}
}
