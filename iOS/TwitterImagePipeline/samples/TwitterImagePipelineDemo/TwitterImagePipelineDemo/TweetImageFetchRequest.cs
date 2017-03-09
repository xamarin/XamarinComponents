using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	public class TweetImageFetchRequest : TIPImageFetchRequest
	{
		private static readonly Dictionary<string, double> variantSizeMap = new Dictionary<string, double> {
			{ "small",   680 },
			{ "medium", 1200 },
			{ "large",  2048 },
		};

		private TweetImageInfo tweetImage;
		private UIViewContentMode targetContentMode;
		private CGSize targetDimensions;
		private NSUrl imageUrl;
		private bool forcePlaceholder;

		public TweetImageFetchRequest(TweetImageInfo image, UIView view, bool placeholder)
		{
			var scale = UIScreen.MainScreen.Scale;

			tweetImage = image;
			targetContentMode = view.ContentMode;
			targetDimensions = TIPImageUtils.TIPDimensionsFromView(view);
			forcePlaceholder = placeholder;

			if (forcePlaceholder)
			{
				imageUrl = new NSUrl("placeholder://placeholder.com/placeholder.jpg");
			}
			else
			{
				if (tweetImage.MediaUrl.StartsWith("https://pbs.twimg.com/media/", StringComparison.OrdinalIgnoreCase))
				{
					var variantName = DetermineVariant(tweetImage.OriginalDimensions, targetDimensions, targetContentMode);
					imageUrl = new NSUrl($"{tweetImage.MediaUrlWithoutExtension}?format={tweetImage.Format}&name={variantName}");
				}
				else
				{
					imageUrl = new NSUrl(tweetImage.MediaUrl);
				}
			}
		}

		public override CGSize TargetDimensions => targetDimensions;

		public override UIViewContentMode TargetContentMode => targetContentMode;

		public override string ImageIdentifier => tweetImage.MediaUrl;

		public override NSUrl ImageUrl => imageUrl;

		public override TIPImageFetchOptions Options => forcePlaceholder ? TIPImageFetchOptions.TreatAsPlaceholder : TIPImageFetchOptions.NoOptions;

		private string DetermineVariant(CGSize aspectRatio, CGSize dimensions, UIViewContentMode contentMode)
		{
			if (aspectRatio.Height <= 0 || aspectRatio.Width <= 0)
			{
				aspectRatio = new CGSize(1, 1);
			}

			var scaleToFit = (UIViewContentMode.ScaleAspectFit == contentMode);
			var scaledToTargetDimensions = TIPImageUtils.TIPDimensionsScaledToTargetSizing(aspectRatio, dimensions, (scaleToFit ? UIViewContentMode.ScaleAspectFit : UIViewContentMode.ScaleAspectFill));

			string selectedVariantName = null;
			foreach (var variant in variantSizeMap)
			{
				var variantSize = new CGSize(variant.Value, variant.Value);
				var scaledToVariantDimensions = TIPImageUtils.TIPDimensionsScaledToTargetSizing(aspectRatio, variantSize, UIViewContentMode.ScaleAspectFit);
				if (scaledToVariantDimensions.Width >= scaledToTargetDimensions.Width && scaledToVariantDimensions.Height >= scaledToTargetDimensions.Height)
				{
					selectedVariantName = variant.Key;
					break;
				}
			}

			if (string.IsNullOrEmpty(selectedVariantName))
			{
				selectedVariantName = variantSizeMap.Keys.LastOrDefault();
			}

			return selectedVariantName;
		}
	}
}
