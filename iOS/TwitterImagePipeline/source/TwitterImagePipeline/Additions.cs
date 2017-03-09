using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

namespace TwitterImagePipeline
{
	partial class TIPGlobalConfigurationInspect
	{
		public static Task<NSDictionary<NSString, TIPImagePipelineInspectionResult>> InspectAsync(this TIPGlobalConfiguration This)
		{
			var tcs = new TaskCompletionSource<NSDictionary<NSString, TIPImagePipelineInspectionResult>>();
			This.Inspect(results => tcs.SetResult(results));
			return tcs.Task;
		}
	}

	public class TIPSimpleImageFetchRequest : NSObject, ITIPImageFetchRequest
	{
		public TIPSimpleImageFetchRequest(NSUrl imageUrl)
		{
			ImageUrl = imageUrl;
		}

		public TIPSimpleImageFetchRequest(NSUrl imageUrl, UIView targetView)
		{
			if (targetView == null)
				throw new ArgumentNullException(nameof(targetView));

			ImageUrl = imageUrl;

			TargetDimensions = TIPImageUtils.TIPDimensionsFromView(targetView);
			TargetContentMode = targetView.ContentMode;
		}

		public string ImageIdentifier { get; set; }

		public TIPImageFetchHydrationDelegate ImageRequestHydrationBlock { get; set; }

		public NSUrl ImageUrl { get; set; }

		public TIPImageFetchLoadingSources LoadingSources { get; set; }

		public TIPImageFetchOptions Options { get; set; }

		public NSDictionary<NSString, TIPImageFetchProgressiveLoadingPolicy> ProgressiveLoadingPolicies { get; set; }

		public UIViewContentMode TargetContentMode { get; set; }

		public CGSize TargetDimensions { get; set; }

		public double TimeToLive { get; set; }
	}
}
