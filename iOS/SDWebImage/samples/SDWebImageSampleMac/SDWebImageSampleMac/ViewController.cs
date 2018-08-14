using System;
using System.Collections.Generic;
using System.Linq;
using AppKit;
using CoreGraphics;
using Foundation;

using SDWebImage;

namespace SDWebImageSampleMac
{
	public partial class ViewController : NSViewController
	{
		private List<string> images;

		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			collectionView.RegisterClassForItem(typeof(ImageListItemController), nameof(ImageListItem));

			collectionView.WantsLayer = true;
			collectionView.CollectionViewLayout = new NSCollectionViewFlowLayout
			{
				ItemSize = new CGSize(140, 140),
				SectionInset = new NSEdgeInsets(10, 10, 10, 10),
				MinimumInteritemSpacing = 10,
				MinimumLineSpacing = 10
			};

			images = ImageRepository.GetImages();
			collectionView.DataSource = new CollectionDataSource(images);
			collectionView.Delegate = new CollectionDelegate(this);

			collectionView.ReloadData();
		}

		private class CollectionDataSource : NSCollectionViewDataSource
		{
			private List<string> images;

			public CollectionDataSource(List<string> images)
			{
				this.images = images;
			}

			public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
			{
				var item = collectionView.MakeItem(nameof(ImageListItem), indexPath) as ImageListItemController;
				item.View.ImageView.SetImage(new NSUrl(images[(int)indexPath.Item]), NSImage.ImageNamed("placeholder"));
				return item;
			}

			public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
			{
				return images.Count;
			}
		}

		private class CollectionDelegate : NSCollectionViewDelegateFlowLayout
		{
			private ViewController parent;

			public CollectionDelegate(ViewController parent)
			{
				this.parent = parent;
			}

			public override void ItemsSelected(NSCollectionView collectionView, NSSet indexPaths)
			{
				var indexPath = indexPaths.ToArray<NSIndexPath>().FirstOrDefault();
				if (indexPath != null)
				{
					var image = parent.images[(int)indexPath.Item];
					parent.imageView.SetImage(new NSUrl(image), NSImage.ImageNamed("placeholder"));
				}
				else
				{
					parent.imageView.Image = null;
				}
			}
		}
	}
}
