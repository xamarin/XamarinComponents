using System;
using System.Linq;
using AppKit;
using CoreGraphics;

using Carousels;

namespace iCarouselSampleMac
{
	public partial class ViewController : NSViewController
	{
		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			bool wrap = false;

			// create the carousel
			carousel.Type = iCarouselType.CoverFlow2;
			carousel.DataSource = new CarouselDataSource();

			// customize the appearance of the carousel
			carousel.GetValue = (sender, option, value) =>
			{
				// set a nice spacing between items
				if (option == iCarouselOption.Spacing)
				{
					return value * 1.1F;
				}
				else if (option == iCarouselOption.Wrap)
				{
					return wrap ? 1 : 0;
				}

				// use the defaults for everything else
				return value;
			};
		}

		private class CarouselDataSource : iCarouselDataSource
		{
			int[] items;

			public CarouselDataSource()
			{
				// create our amazing data source
				items = Enumerable.Range(0, 100).ToArray();
			}

			public override nint GetNumberOfItems(iCarousel carousel)
			{
				// return the number of items in the data
				return items.Length;
			}

			public override NSView GetViewForItem(iCarousel carousel, nint index, NSView view)
			{
				NSTextField label = null;
				NSImageView imageView = null;

				if (view == null)
				{
					// create new view if no view is available for recycling
					imageView = new NSImageView(new CGRect(0, 0, 200.0f, 200.0f));
					imageView.Image = NSImage.ImageNamed("page");
					imageView.ImageAlignment = NSImageAlignment.Center;

					label = new NSTextField(imageView.Bounds);
					label.BackgroundColor = NSColor.Clear;
					label.Alignment = NSTextAlignment.Center;
					label.Font = NSFont.LabelFontOfSize(50);
					label.Bordered = false;
					label.Editable = false;
					label.Tag = 1;
					imageView.AddSubview(label);
				}
				else
				{
					// get a reference to the label in the recycled view
					imageView = (NSImageView)view;
					label = (NSTextField)view.ViewWithTag(1);
				}

				// set the values of the view
				label.StringValue = items[index].ToString();

				return imageView;
			}
		}
	}
}
