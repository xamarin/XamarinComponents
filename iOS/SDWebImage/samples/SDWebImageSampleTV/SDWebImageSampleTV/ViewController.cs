using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

using SDWebImage;

namespace SDWebImageSampleTV
{
	public partial class ViewController : UIViewController
	{
		private List<string> images;

		public ViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			images = ImageRepository.GetImages();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Clear Cache", UIBarButtonItemStyle.Plain, ClearCache);

			SDWebImageManager.SharedManager.ImageDownloader.SetHttpHeaderValue("SDWebImage Demo", "SDWebImageSampleTV");
			SDWebImageManager.SharedManager.ImageDownloader.ExecutionOrder = SDWebImageDownloaderExecutionOrder.LastInFirstOut;

			tableView.Delegate = new TableViewDelegate(this);
			tableView.DataSource = new TableViewDataSource(images);

			tableView.Delegate.RowSelected(tableView, NSIndexPath.FromRowSection(0, 0));
		}

		private void ClearCache(object sender, EventArgs e)
		{
			SDWebImageManager.SharedManager.ImageCache.ClearMemory();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk();
		}

		private class TableViewDelegate : UITableViewDelegate
		{
			private ViewController viewController;

			public TableViewDelegate(ViewController viewController)
			{
				this.viewController = viewController;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				viewController.activity.StartAnimating();
				viewController.progress.Hidden = false;
				viewController.progress.Progress = 0;

				viewController.imageView.SetImage(
					new NSUrl(viewController.images[indexPath.Row]),
					UIImage.FromBundle("placeholder"),
					SDWebImageOptions.ProgressiveDownload,
					ProgressHandler,
					CompletedHandler);
			}

			private void ProgressHandler(nint receivedSize, nint expectedSize, NSUrl url)
			{
				InvokeOnMainThread(() =>
				{
					viewController.progress.Progress = (float)receivedSize / (float)expectedSize;
				});
			}

			private void CompletedHandler(UIImage image, NSError error, SDImageCacheType cacheType, NSUrl url)
			{
				InvokeOnMainThread(() =>
				{
					viewController.activity.StopAnimating();
					viewController.progress.Hidden = true;
				});
			}
		}

		private class TableViewDataSource : UITableViewDataSource
		{
			private List<string> images;

			public TableViewDataSource(List<string> images)
			{
				this.images = images;
			}
			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return images.Count;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("ImageCell");

				cell.TextLabel.Text = string.Format("Image #{0}", indexPath.Row);
				cell.ImageView.SetImage(new NSUrl(images[indexPath.Row]), UIImage.FromBundle("placeholder"));

				return cell;
			}
		}
	}
}
