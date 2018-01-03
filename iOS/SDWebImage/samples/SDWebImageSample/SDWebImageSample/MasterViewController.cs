using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

using SDWebImage;

namespace SDWebImageSample
{
	public partial class MasterViewController : UITableViewController
	{
		private List<string> images;

		public MasterViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			images = ImageRepository.GetImages();

			NavigationItem.RightBarButtonItem = new UIBarButtonItem("Clear Cache", UIBarButtonItemStyle.Plain, ClearCache);

			SDWebImageManager.SharedManager.ImageDownloader.SetHttpHeaderValue("SDWebImage Demo", "SDWebImageSample");
			SDWebImageManager.SharedManager.ImageDownloader.ExecutionOrder = SDWebImageDownloaderExecutionOrder.LastInFirstOut;
		}

		private void ClearCache(object sender, EventArgs e)
		{
			SDWebImageManager.SharedManager.ImageCache.ClearMemory();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk();
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

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "ShowImage")
			{
				var details = (DetailViewController)segue.DestinationViewController;

				var row = TableView.IndexPathForSelectedRow.Row;
				details.ImageUrl = new NSUrl(images[row]);
			}

			base.PrepareForSegue(segue, sender);
		}
	}
}
