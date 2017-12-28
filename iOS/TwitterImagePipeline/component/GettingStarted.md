# Twitter Image Pipeline (TIP)

The __Twitter Image Pipeline__ is a streamlined framework for _fetching_ and
_storing_ images in an application.  The high level concept is that all
requests to fetch or store an image go through an _image pipeline_ which
encapsulates the work of checking the _in memory caches_ and an _on disk
cache_ before retrieving the image from over the _network_ as well as
keeping the caches both up to date and pruned.

## Usage

The simplest way to use *TIP* is with the `TIPImageView` and its `TIPImageViewHelper` counterpart.

*For concrete coding samples, look at the sample app.*

Here's a simple example of using *TIP* with a `UIViewController` that has an array of image views to
populate with images.

First, we create a shared pipeline for the entire app:

    public class AppDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ImagePipeline = new TIPImagePipeline("com.my.app.image.pipeline");

            // ...
        }

        public static TIPImagePipeline ImagePipeline { get; private set; }
    }

Then, in our view controller:

    public class ImagesViewController : UIViewController, ITIPImageFetchDelegate
    {
        private List<UIImageView> imageViews;
        private List<NSUrl> imageUrls;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            for (var imageIndex = 0; imageIndex < imageViews.Count; imageIndex++) {
                // clear the image view
                var imageView = imageViews[imageIndex];
                imageView.Image = null;

                // create the image request
                var request = new TIPSimpleImageFetchRequest(imageUrls[imageIndex]);
                var op = AppDelegate.ImagePipeline.CreateFetchOperation(request, (NSNumber)imageIndex, this);

                // queue the request
                AppDelegate.ImagePipeline.FetchImage(op);
            }
        }

        // ITIPImageFetchDelegate members

        [Export("tip_imageFetchOperation:didLoadPreviewImage:completion:")]
        public void ImageFetchOperationDidLoadPreviewImage(TIPImageFetchOperation op, ITIPImageFetchResult previewResult, TIPImageFetchDidLoadPreviewCallback completion)
        {
            var imageContainer = previewResult.ImageContainer;
            var dimensions = imageContainer.Dimensions();
            var originalDimensions = previewResult.ImageOriginalDimensions;

            var idx = ((NSNumber)op.Context).Int32Value;
            var imageView = imageViews[idx];

            imageView.Image = imageContainer.Image;

            if ((dimensions.Width * dimensions.Height) >= (originalDimensions.Width * originalDimensions.Height)) {
                // scaled down, preview is plenty
                completion(TIPImageFetchPreviewLoadedBehavior.StopLoading);
            } else {
                completion(TIPImageFetchPreviewLoadedBehavior.ContinueLoading);
            }
        }

        [Export("tip_imageFetchOperation:shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
        public bool ImageFetchOperationShouldLoadProgressively(TIPImageFetchOperation op, string identifier, NSUrl url, string imageType, CGSize originalDimensions)
        {
            var idx = ((NSNumber)op.Context).Int32Value;
            var imageView = imageViews[idx];

            // only load progressively if we didn't load a "preview"
            return imageView.Image == null;
        }

        [Export("tip_imageFetchOperation:didUpdateProgressiveImage:progress:")]
        public void ImageFetchOperationDidUpdateProgressiveImage(TIPImageFetchOperation op, ITIPImageFetchResult progressiveResult, float progress)
        {
            var idx = ((NSNumber)op.Context).Int32Value;
            var imageView = imageViews[idx];

            imageView.Image = progressiveResult.ImageContainer.Image;
        }

        [Export("tip_imageFetchOperation:didLoadFinalImage:")]
        public void ImageFetchOperationDidLoadFinalImage(TIPImageFetchOperation op, ITIPImageFetchResult finalResult)
        {
            var idx = ((NSNumber)op.Context).Int32Value;
            var imageView = imageViews[idx];

            imageView.Image = finalResult.ImageContainer.Image;
        }

        [Export("tip_imageFetchOperation:didFailToLoadFinalImage:")]
        public void ImageFetchOperationDidFailToLoadFinalImage(TIPImageFetchOperation op, NSError error)
        {
            var idx = ((NSNumber)op.Context).Int32Value;
            var imageView = imageViews[idx];

            if (imageView.Image == null) {
                imageView.Image = AppDelegate.FailedPlaceholderImage;
            }

            Console.WriteLine("Failed to load image: " + error);
        }
    }


