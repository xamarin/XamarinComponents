using System;
using System.Diagnostics;
using CoreGraphics;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class ZoomingTweetImageViewController : UIViewController, IUIScrollViewDelegate, ITIPImageFetchDelegate
	{
		private readonly TweetImageInfo tweetImageInfo;

		private UIScrollView scrollView;
		private UIImageView imageView;
		private UIProgressView progressView;
		private UITapGestureRecognizer doubleTapGuestureRecognizer;
		private TIPImageFetchOperation fetchOp;

		public ZoomingTweetImageViewController(TweetImageInfo imageInfo)
		{
			tweetImageInfo = imageInfo;

			NavigationItem.Title = "Tweet Image";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var targetSize = tweetImageInfo.OriginalDimensions;
			var scale = UIScreen.MainScreen.Scale;
			if (scale != 1)
			{
				targetSize.Height /= scale;
				targetSize.Width /= scale;
			}

			progressView = new UIProgressView(new CGRect(0, 0, View.Bounds.Width, 4));
			progressView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleBottomMargin;
			progressView.TintColor = UIColor.Yellow;
			progressView.Progress = 0;

			imageView = new UIImageView(new CGRect(0, 0, targetSize.Width, targetSize.Height));
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;
			imageView.ClipsToBounds = true;
			imageView.BackgroundColor = UIColor.Gray;
			scrollView = new UIScrollView(View.Bounds);
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			scrollView.BackgroundColor = UIColor.Black;

			doubleTapGuestureRecognizer = new UITapGestureRecognizer(DoubleTapTriggered);
			doubleTapGuestureRecognizer.NumberOfTapsRequired = 2;
			imageView.Image = null;

			imageView.AddGestureRecognizer(doubleTapGuestureRecognizer);
			imageView.UserInteractionEnabled = true;
			scrollView.Delegate = this;
			scrollView.MinimumZoomScale = 0.01f; // start VERY small
			scrollView.MaximumZoomScale = 2.0f;
			scrollView.ContentSize = targetSize;

			View.AddSubview(scrollView);
			View.AddSubview(progressView);
			scrollView.AddSubview(imageView);
			scrollView.ZoomToRect(imageView.Frame, false);
			scrollView.MinimumZoomScale = scrollView.ZoomScale; // readjust minimum
			if (scrollView.MinimumZoomScale > scrollView.MaximumZoomScale)
			{
				scrollView.MaximumZoomScale = scrollView.MinimumZoomScale;
			}
			DidZoom(scrollView);
			Load();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			DidZoom(scrollView);
			var frame = progressView.Frame;
			frame.Y = scrollView.ContentInset.Top;
			progressView.Frame = frame;
		}

		private void Load()
		{
			var request = new TweetImageFetchRequest(tweetImageInfo, imageView, false);
			fetchOp = AppDelegate.Current.ImagePipeline.CreateFetchOperation(request, null, this);
			AppDelegate.Current.ImagePipeline.FetchImage(fetchOp);
		}

		private void DoubleTapTriggered(UITapGestureRecognizer tapper)
		{
			if (tapper.State == UIGestureRecognizerState.Recognized)
			{
				if (scrollView.ZoomScale == scrollView.MaximumZoomScale)
				{
					scrollView.SetZoomScale(scrollView.MinimumZoomScale, true);
				}
				else
				{
					scrollView.SetZoomScale(scrollView.MaximumZoomScale, true);
				}
			}
		}

		// IUIScrollViewDelegate

		[Export("viewForZoomingInScrollView:")]
		public UIView ViewForZoomingInScrollView(UIScrollView scrollView) => imageView;

		[Export("scrollViewDidZoom:")]
		public void DidZoom(UIScrollView scrollView)
		{
			var offsetX = Math.Max((scrollView.Bounds.Width - scrollView.ContentInset.Left - scrollView.ContentInset.Right - scrollView.ContentSize.Width) * 0.5, 0.0);
			var offsetY = Math.Max((scrollView.Bounds.Height - scrollView.ContentInset.Top - scrollView.ContentInset.Bottom - scrollView.ContentSize.Height) * 0.5, 0.0);

			imageView.Center = new CGPoint(scrollView.ContentSize.Width * 0.5 + offsetX, scrollView.ContentSize.Height * 0.5 + offsetY);
		}

		// ITIPImageFetchDelegate

		[Export("tip_imageFetchOperationDidStart:")]
		public void ImageFetchOperationDidStart(TIPImageFetchOperation op)
		{
			Debug.WriteLine("starting Zoom fetch...");
		}

		[Export("tip_imageFetchOperation:willAttemptToLoadFromSource:")]
		public void ImageFetchOperationWillAttemptToLoad(TIPImageFetchOperation op, TIPImageLoadSource source)
		{
			Debug.WriteLine($"...attempting load from next source: {source}...");
		}

		[Export("tip_imageFetchOperation:didLoadPreviewImage:completion:")]
		public void ImageFetchOperationDidLoadPreviewImage(TIPImageFetchOperation op, ITIPImageFetchResult previewResult, TIPImageFetchDidLoadPreviewCallback completion)
		{
			Debug.WriteLine("...preview loaded...");

			progressView.TintColor = UIColor.Blue;
			imageView.Image = previewResult.ImageContainer.Image;

			completion(TIPImageFetchPreviewLoadedBehavior.ContinueLoading);
		}

		[Export("tip_imageFetchOperation:shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
		public bool ImageFetchOperationShouldLoadProgressively(TIPImageFetchOperation op, string identifier, NSUrl url, string imageType, CGSize originalDimensions)
		{
			var isNull = false;
			InvokeOnMainThread(() => isNull = (imageView.Image == null));
			return isNull;
		}

		[Export("tip_imageFetchOperation:didUpdateProgressiveImage:progress:")]
		public void ImageFetchOperationDidUpdateProgressiveImage(TIPImageFetchOperation op, ITIPImageFetchResult progressiveResult, float progress)
		{
			Debug.WriteLine($"...progressive update ({progress:0.000})...");

			progressView.TintColor = UIColor.Orange;
			progressView.SetProgress(progress, true);
			imageView.Image = progressiveResult.ImageContainer.Image;
		}

		[Export("tip_imageFetchOperation:didLoadFirstAnimatedImageFrame:progress:")]
		public void ImageFetchOperationDidLoadFirstAnimatedImageFrame(TIPImageFetchOperation op, ITIPImageFetchResult progressiveResult, float progress)
		{
			Debug.WriteLine($"...animated first frame ({progress:0.000})...");

			imageView.Image = progressiveResult.ImageContainer.Image;
			progressView.TintColor = UIColor.Purple;
			progressView.SetProgress(progress, true);
		}

		[Export("tip_imageFetchOperation:didUpdateProgress:")]
		public void ImageFetchOperationDidUpdateProgress(TIPImageFetchOperation op, float progress)
		{
			Debug.WriteLine($"...progress ({progress:0.000})...");

			progressView.SetProgress(progress, true);
		}

		[Export("tip_imageFetchOperation:didLoadFinalImage:")]
		public void ImageFetchOperationDidLoadFinalImage(TIPImageFetchOperation op, ITIPImageFetchResult finalResult)
		{
			Debug.WriteLine("...completed zoom fetch");

			progressView.TintColor = UIColor.Green;
			progressView.SetProgress(1, true);
			imageView.Image = finalResult.ImageContainer.Image;
			fetchOp = null;
		}

		[Export("tip_imageFetchOperation:didFailToLoadFinalImage:")]
		public void ImageFetchOperationDidFailToLoadFinalImage(TIPImageFetchOperation op, NSError error)
		{
			Debug.WriteLine($"...failed zoom fetch: {error}");

			progressView.TintColor = UIColor.Red;
			fetchOp = null;
		}
	}
}
