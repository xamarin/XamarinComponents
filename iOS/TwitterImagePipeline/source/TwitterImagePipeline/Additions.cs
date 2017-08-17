using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using ImageIO;
using ObjCRuntime;
using UIKit;

namespace TwitterImagePipeline
{
	partial class TIPNSHttpUrlResponse
	{
		public static NSHttpUrlResponse ResponseWithRequest(NSUrl requestUrl, nuint dataLength, string MIMEType)
		{
			return ResponseWithRequest(((NSHttpUrlResponse)null), requestUrl, dataLength, MIMEType);
		}
	}

	partial class TIPGlobalConfigurationInspect
	{
		public static Task<NSDictionary<NSString, TIPImagePipelineInspectionResult>> InspectAsync(this TIPGlobalConfiguration This)
		{
			var tcs = new TaskCompletionSource<NSDictionary<NSString, TIPImagePipelineInspectionResult>>();
			This.Inspect(results => tcs.SetResult(results));
			return tcs.Task;
		}
	}

	partial class TIPLogger
	{
		private static Selector tip_canLogWithLevel = new Selector("tip_canLogWithLevel:");

		private static void LogInternal(TIPLogLevel level, string format, params object[] args)
		{
			var logger = TIPGlobalConfiguration.SharedInstance.Logger;
			var nsLogger = logger as NSObject;

			if (logger != null && (nsLogger == null || !nsLogger.RespondsToSelector(tip_canLogWithLevel) || logger.CanLog(level)))
			{
				var stacktrace = new StackTrace(true);
				var frame = stacktrace.FrameCount >= 2 ? stacktrace.GetFrame(2) : null;

				var file = frame?.GetFileName();
				var method = frame?.GetMethod();
				var function = method == null ? null : method.DeclaringType.FullName + ": " + method.ToString();
				var line = frame?.GetFileLineNumber();

				var message = string.Format(format, args);

				logger.Log(level, file ?? string.Empty, function ?? string.Empty, line ?? 0, message, IntPtr.Zero);
			}
		}

		public static void Log(TIPLogLevel level, string format, params object[] args) => LogInternal(level, format, args);
		public static void LogError(string format, params object[] args) => LogInternal(TIPLogLevel.Error, format, args);
		public static void LogWarning(string format, params object[] args) => LogInternal(TIPLogLevel.Warning, format, args);
		public static void LogInformation(string format, params object[] args) => LogInternal(TIPLogLevel.Information, format, args);
		public static void LogDebug(string format, params object[] args) => LogInternal(TIPLogLevel.Debug, format, args);
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

		public NSDictionary<NSString, ITIPImageFetchProgressiveLoadingPolicy> ProgressiveLoadingPolicies { get; set; }

		public UIViewContentMode TargetContentMode { get; set; }

		public CGSize TargetDimensions { get; set; }

		public double TimeToLive { get; set; }
	}

	partial class TIPImageCodecCatalogue
	{
		public void RemoveCodec(string imageType, out ITIPImageCodec codec)
		{
			IntPtr codecPtr;
			RemoveCodec(imageType, out codecPtr);
			codec = Runtime.GetINativeObject<ITIPImageCodec>(codecPtr, false);
		}
	}

	partial class TIPGlobalConfigurationDefaults
	{
		[DllImport(Constants.libSystemLibrary)]
		private static extern IntPtr dlsym(IntPtr handle, string symbol);

		private static short GetInt16(IntPtr handle, string symbol)
		{
			var indirect = dlsym(handle, symbol);
			if (indirect == IntPtr.Zero)
				return 0;
			return Marshal.ReadInt16(indirect);
		}

		[Field("TIPMaxCountForAllDiskCachesDefault", "__Internal")]
		public static short MaxCountForAllDiskCaches => GetInt16(Libraries.__Internal.Handle, "TIPMaxCountForAllDiskCachesDefault");

		[Field("TIPMaxCountForAllMemoryCachesDefault", "__Internal")]
		public static short MaxCountForAllMemoryCaches => GetInt16(Libraries.__Internal.Handle, "TIPMaxCountForAllMemoryCachesDefault");

		[Field("TIPMaxCountForAllRenderedCachesDefault", "__Internal")]
		public static short MaxCountForAllRenderedCaches => GetInt16(Libraries.__Internal.Handle, "TIPMaxCountForAllRenderedCachesDefault");
	}

	partial class TIPImageContainer
	{
		public static TIPImageContainer Create(CGImageSource imageSource)
		{
			return Create(imageSource.Handle);
		}
	}
}
