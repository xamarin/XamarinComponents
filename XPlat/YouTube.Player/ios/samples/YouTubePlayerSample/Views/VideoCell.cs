using System;
using System.Threading;
using Foundation;
using UIKit;

namespace YouTubePlayerSample
{
	public partial class VideoCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString("VideoCellId");

		protected VideoCell(IntPtr handle)
			: base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public UIImage ImgThumbnailImage
		{
			get { return ImgThumbnail.Image; }
			set { ImgThumbnail.Image = value; }
		}

		public string LblTitleText
		{
			get { return LblTitle.Text; }
			set { LblTitle.Text = value; }
		}

		public CancellationTokenSource CancellationTokenSource { get; set; }
	}
}
