using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class TweetWithMediaTableViewCell : UITableViewCell, ITIPImageViewFetchHelperDataSource, ITIPImageViewFetchHelperDelegate
	{
		public static string ReuseId = "TweetWithMedia";

		private TIPImageView tweetImageView;
		private TIPImageViewFetchHelper tweetFetchHelper;
		private TweetInfo tweet;

		public TweetWithMediaTableViewCell()
			: base(UITableViewCellStyle.Subtitle, ReuseId)
		{
			tweetFetchHelper = new TIPImageViewFetchHelper(this, this);

			tweetImageView = new TIPImageView(tweetFetchHelper);
			tweetImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			tweetImageView.ClipsToBounds = true;
			tweetImageView.BackgroundColor = UIColor.LightGray;
			tweetImageView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			ContentView.AddSubview(tweetImageView);
		}

		public TweetInfo Tweet
		{
			get { return tweet; }
			set
			{
				tweet = value;
				tweetFetchHelper.ClearImage();
				tweetFetchHelper.Reload();
			}
		}

		public override void PrepareForReuse()
		{
			base.PrepareForReuse();
			tweet = null;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var detailRect = DetailTextLabel.Frame;
			var textRect = TextLabel.Frame;
			var yDelta = detailRect.Y - textRect.Y;
			textRect.Y = 3;
			detailRect.Y = textRect.Y + yDelta;
			DetailTextLabel.Frame = detailRect;
			TextLabel.Frame = textRect;

			var imageRect = tweetImageView.Frame;
			imageRect.Y = detailRect.Y + detailRect.Height + 3;
			imageRect.X = detailRect.X;
			imageRect.Width = ContentView.Bounds.Width - (2 * imageRect.X);
			imageRect.Height = ContentView.Bounds.Height - (imageRect.Y + 3);
			tweetImageView.Frame = imageRect;
		}

		[Export("tip_imagePipelineForFetchHelper:")]
		public TIPImagePipeline GetImagePipeline(TIPImageViewFetchHelper helper)
		{
			return AppDelegate.Current.ImagePipeline;
		}

		[Export("tip_imageFetchRequestForFetchHelper:")]
		public ITIPImageFetchRequest GetImageFetchRequest(TIPImageViewFetchHelper helper)
		{
			var tweetImage = tweet.Images.FirstOrDefault();
			if (tweetImage == null)
			{
				return null;
			}

			var request = new TweetImageFetchRequest(tweetImage, helper.FetchImageView, AppDelegate.Current.UsePlaceholder);
			return request;
		}

		[Export("tip_fetchHelper:shouldUpdateImageWithPreviewImageResult:")]
		public bool FetchHelperShouldUpdateImage(TIPImageViewFetchHelper helper, ITIPImageFetchResult previewImageResult) => true;

		[Export("tip_fetchHelper:shouldContinueLoadingAfterFetchingPreviewImageResult:")]
		public bool FetchHelperShouldContinueLoadingAfterFetching(TIPImageViewFetchHelper helper, ITIPImageFetchResult previewImageResult)
		{
			if (previewImageResult.ImageIsTreatedAsPlaceholder)
			{
				return true;
			}

			var request = helper.FetchRequest;
			if (request.GetOptions().HasFlag(TIPImageFetchOptions.TreatAsPlaceholder))
			{
				// would be a downgrade, stop
				return false;
			}

			var originalDimensions = previewImageResult.ImageOriginalDimensions;
			var viewDimensions = TIPImageUtils.TIPDimensionsFromView(helper.FetchImageView);
			if (originalDimensions.Height >= viewDimensions.Height && originalDimensions.Width >= viewDimensions.Width)
			{
				return false;
			}

			return true;
		}

		[Export("tip_fetchHelper:shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
		public bool FetchHelperShouldLoadProgressively(TIPImageViewFetchHelper helper, string identifier, NSUrl url, string imageType, CGSize originalDimensions) => true;
	}
}
