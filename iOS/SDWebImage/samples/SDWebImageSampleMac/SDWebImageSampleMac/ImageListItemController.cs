using System;
using AppKit;
using Foundation;

namespace SDWebImageSampleMac
{
	public partial class ImageListItemController : NSCollectionViewItem
	{
		public ImageListItemController(IntPtr handle)
			: base(handle)
		{
			Initialize();
		}

		[Export("initWithCoder:")]
		public ImageListItemController(NSCoder coder)
			: base(coder)
		{
			Initialize();
		}

		public ImageListItemController()
			: base("ImageListItem", NSBundle.MainBundle)
		{
			Initialize();
		}

		private void Initialize()
		{
		}

		public override bool Selected
		{
			get { return base.Selected; }
			set
			{
				base.Selected = value;

				Background.FillColor = value ? NSColor.DarkGray : NSColor.LightGray;
			}
		}

		public new ImageListItem View => (ImageListItem)base.View;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Background.FillColor = NSColor.LightGray;
		}
	}
}
