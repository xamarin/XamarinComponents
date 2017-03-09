using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using CoreImage;
using Foundation;
using ImageIO;
using ObjCRuntime;
using UIKit;

namespace TwitterImagePipeline
{
	[Native]
	public enum TIPImageEncodingOptions : long
	{
		NoOptions = 0,
		Progressive = 1 << 0,
		NoAlpha = 1 << 1,
		Grayscale = 1 << 2
	}

	[Native]
	public enum TIPRecommendedImageTypeOptions : long
	{
		NoOptions = 0,
		AssumeAlpha = 1 << 0,
		AssumeNoAlpha = 1 << 1,
		PermitLossy = 1 << 2,
		PreferProgressive = 1 << 3
	}

	// TODO
	static class CFunctions1
	{
	//	// extern BOOL TIPImageTypeCanReadWithImageIO (NSString * _Nullable type);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern bool TIPImageTypeCanReadWithImageIO ([NullAllowed] NSString type);

	//	// extern BOOL TIPImageTypeCanWriteWithImageIO (NSString * _Nullable type);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern bool TIPImageTypeCanWriteWithImageIO ([NullAllowed] NSString type);

	//	// extern NSString * _Nullable TIPImageTypeFromUTType (NSString * _Nullable utType);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSString TIPImageTypeFromUTType ([NullAllowed] NSString utType);

	//	// extern NSString * _Nullable TIPImageTypeToUTType (NSString * _Nullable type);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSString TIPImageTypeToUTType ([NullAllowed] NSString type);

	//	// extern NSString * _Nullable TIPDetectImageType (NSData * _Nonnull data, TIPImageEncodingOptions * _Nullable optionsOut, NSUInteger * _Nullable animationFrameCountOut, BOOL hasCompleteImageData);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern unsafe NSString TIPDetectImageType (NSData data, [NullAllowed] TIPImageEncodingOptions* optionsOut, [NullAllowed] nuint* animationFrameCountOut, bool hasCompleteImageData);

	//	// extern NSString * _Nullable TIPDetectImageTypeViaMagicNumbers (NSData * _Nonnull data);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSString TIPDetectImageTypeViaMagicNumbers (NSData data);

	//	// extern NSUInteger TIPImageDetectProgressiveScanCount (NSData * _Nonnull data);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nuint TIPImageDetectProgressiveScanCount (NSData data);

	//	// extern TIPRecommendedImageTypeOptions TIPRecommendedImageTypeOptionsFromEncodingOptions (TIPImageEncodingOptions encodingOptions, float quality) __attribute__((const));
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern TIPRecommendedImageTypeOptions TIPRecommendedImageTypeOptionsFromEncodingOptions (TIPImageEncodingOptions encodingOptions, float quality);

	//	// extern TIPImageContainer * _Nullable TIPDecodeImageFromData (id<TIPImageCodec> _Nonnull codec, NSData * _Nonnull imageData) __attribute__((overloadable));
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern TIPImageContainer TIPDecodeImageFromData (TIPImageCodec codec, NSData imageData);

	//	// extern TIPImageContainer * _Nullable TIPDecodeImageFromData (id<TIPImageCodec> _Nonnull codec, NSData * _Nonnull imageData, NSString * _Nullable earlyGuessImageType) __attribute__((overloadable));
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern TIPImageContainer TIPDecodeImageFromData (TIPImageCodec codec, NSData imageData, [NullAllowed] NSString earlyGuessImageType);

	//	// extern BOOL TIPEncodeImageToFile (id<TIPImageCodec> _Nonnull codec, TIPImageContainer * _Nonnull imageContainer, NSString * _Nonnull filePath, TIPImageEncodingOptions options, float quality, BOOL atomic, NSError * _Nullable * _Nullable error);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern bool TIPEncodeImageToFile (TIPImageCodec codec, TIPImageContainer imageContainer, NSString filePath, TIPImageEncodingOptions options, float quality, bool atomic, [NullAllowed] out NSError error);

	//	// extern NSArray<NSString *> * _Nullable TIPContentsAtPath (NSString * _Nonnull dirPath, NSError * _Nullable * _Nullable outError);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSString[] TIPContentsAtPath (NSString dirPath, [NullAllowed] out NSError outError);

	//	// extern NSUInteger TIPFileSizeAtPath (NSString * _Nonnull filePath, NSError * _Nullable * _Nullable outError);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nuint TIPFileSizeAtPath (NSString filePath, [NullAllowed] out NSError outError);

	//	// extern NSDate * _Nullable TIPLastModifiedDateAtPath (NSString * _Nonnull path);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSDate TIPLastModifiedDateAtPath (NSString path);

	//	// extern NSDate * _Nullable TIPLastModifiedDateAtPathURL (NSURL * _Nonnull pathURL);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSDate TIPLastModifiedDateAtPathURL (NSUrl pathURL);

	//	// extern void TIPSetLastModifiedDateAtPath (NSString * _Nonnull path, NSDate * _Nonnull date);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern void TIPSetLastModifiedDateAtPath (NSString path, NSDate date);

	//	// extern void TIPSetLastModifiedDateAtPathURL (NSURL * _Nonnull pathURL, NSDate * _Nonnull date);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern void TIPSetLastModifiedDateAtPathURL (NSUrl pathURL, NSDate date);

	//	// extern NSDictionary<NSString *,id> * _Nullable TIPGetXAttributesForFile (NSString * _Nonnull filePath, NSDictionary<NSString *,Class> * _Nonnull keyKindMap);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSDictionary<NSString, NSObject> TIPGetXAttributesForFile (NSString filePath, NSDictionary<NSString, Class> keyKindMap);

	//	// extern NSUInteger TIPSetXAttributesForFile (NSDictionary<NSString *,id> * _Nonnull xattrs, NSString * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nuint TIPSetXAttributesForFile (NSDictionary<NSString, NSObject> xattrs, NSString filePath);

	//	// extern NSArray<NSString *> * _Nullable TIPListXAttributesForFile (NSString * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern NSString[] TIPListXAttributesForFile (NSString filePath);

	//	// extern int TIPSetXAttributeNumberForFile (const char * _Nonnull name, NSNumber * _Nonnull number, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern unsafe int TIPSetXAttributeNumberForFile (sbyte* name, NSNumber number, sbyte* filePath);

	//	// extern int TIPSetXAttributeStringForFile (const char * _Nonnull name, NSString * _Nonnull string, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern unsafe int TIPSetXAttributeStringForFile (sbyte* name, NSString @string, sbyte* filePath);

	//	// extern int TIPSetXAttributeDateForFile (const char * _Nonnull name, NSDate * _Nonnull date, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern unsafe int TIPSetXAttributeDateForFile (sbyte* name, NSDate date, sbyte* filePath);

	//	// extern int TIPSetXAttributeURLForFile (const char * _Nonnull name, NSURL * _Nonnull url, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern unsafe int TIPSetXAttributeURLForFile (sbyte* name, NSUrl url, sbyte* filePath);

	//	// extern NSString * _Nullable TIPGetXAttributeStringFromFile (const char * _Nonnull name, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern unsafe NSString TIPGetXAttributeStringFromFile (sbyte* name, sbyte* filePath);

	//	// extern NSNumber * _Nullable TIPGetXAttributeNumberFromFile (const char * _Nonnull name, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern unsafe NSNumber TIPGetXAttributeNumberFromFile (sbyte* name, sbyte* filePath);

	//	// extern NSDate * _Nullable TIPGetXAttributeDateFromFile (const char * _Nonnull name, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern unsafe NSDate TIPGetXAttributeDateFromFile (sbyte* name, sbyte* filePath);

	//	// extern NSURL * _Nullable TIPGetXAttributeURLFromFile (const char * _Nonnull name, const char * _Nonnull filePath);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	[return: NullAllowed]
	//	static extern unsafe NSUrl TIPGetXAttributeURLFromFile (sbyte* name, sbyte* filePath);

	}

	// TODO
	public static class TIPImageUtils
	{
		public static CGSize TIPDimensionsFromSizeScaled (CGSize size, nfloat scale)
		{
			size.Width *= scale;
			size.Height *= scale;
			return size;
		}

		public static CGSize TIPDimensionsFromView (UIView view)
		{
			if (view == null)
			{
				return CGSize.Empty;
			}
			return TIPDimensionsFromSizeScaled(view.Bounds.Size, UIScreen.MainScreen.Scale);
		}

		// extern NSUInteger TIPEstimateMemorySizeOfImageWithSettings (CGSize size, CGFloat scale, NSUInteger componentsPerPixel, NSUInteger frameCount);
		[DllImport ("__Internal")]
		public static extern nuint TIPEstimateMemorySizeOfImageWithSettings (CGSize size, nfloat scale, nuint componentsPerPixel, nuint frameCount);

		// extern BOOL TIPSizeMatchesTargetSizing (CGSize size, CGSize targetSize, UIViewContentMode targetContentMode, CGFloat scale);
		[DllImport ("__Internal")]
		public static extern bool TIPSizeMatchesTargetSizing (CGSize size, CGSize targetSize, UIViewContentMode targetContentMode, nfloat scale);

		//// TODO
		//// extern BOOL TIPCGImageHasAlpha (CGImageRef _Nonnull imageRef, BOOL inspectPixels);
		//[DllImport ("__Internal")]
		//[Verify (PlatformInvoke)]
		//static extern unsafe bool TIPCGImageHasAlpha (CGImageRef* imageRef, bool inspectPixels);

		// extern BOOL TIPCIImageHasAlpha (CIImage * _Nonnull image, BOOL inspectPixels);
		[DllImport ("__Internal")]
		public static extern bool TIPCIImageHasAlpha (CIImage image, bool inspectPixels);

		// extern CGSize TIPSizeScaledToTargetSizing (CGSize sizeToScale, CGSize targetSizeOrZero, UIViewContentMode targetContentMode, CGFloat scale);
		[DllImport ("__Internal")]
		public static extern CGSize TIPSizeScaledToTargetSizing (CGSize sizeToScale, CGSize targetSizeOrZero, UIViewContentMode targetContentMode, nfloat scale);

		// extern CGSize TIPDimensionsScaledToTargetSizing (CGSize dimensionsToScale, CGSize targetDimensionsOrZero, UIViewContentMode targetContentMode);
		[DllImport ("__Internal")]
		public static extern CGSize TIPDimensionsScaledToTargetSizing (CGSize dimensionsToScale, CGSize targetDimensionsOrZero, UIViewContentMode targetContentMode);

		// extern CGImagePropertyOrientation TIPCGImageOrientationFromUIImageOrientation (UIImageOrientation orientation) __attribute__((const));
		[DllImport ("__Internal")]
		public static extern CGImagePropertyOrientation TIPCGImageOrientationFromUIImageOrientation (UIImageOrientation orientation);

		// extern UIImageOrientation TIPUIImageOrientationFromCGImageOrientation (CGImagePropertyOrientation cgOrientation) __attribute__((const));
		[DllImport ("__Internal")]
		public static extern UIImageOrientation TIPUIImageOrientationFromCGImageOrientation (CGImagePropertyOrientation cgOrientation);

		//// TODO
		//// extern void TIPExecuteCGContextBlock (dispatch_block_t _Nonnull block);
		//[DllImport ("__Internal")]
		//[Verify (PlatformInvoke)]
		//static extern void TIPExecuteCGContextBlock (dispatch_block_t block);

	}

	// TODO
	static class CFunctions3
	{
	//	// extern uint64_t TIPAbsoluteToNanoseconds (uint64_t absolute);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern ulong TIPAbsoluteToNanoseconds (ulong absolute);

	//	// extern uint64_t TIPAbsoluteFromNanoseconds (uint64_t nano);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern ulong TIPAbsoluteFromNanoseconds (ulong nano);

	//	// extern NSTimeInterval TIPAbsoluteToTimeInterval (uint64_t absolute);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern double TIPAbsoluteToTimeInterval (ulong absolute);

	//	// extern uint64_t TIPAbsoluteFromTimeInterval (NSTimeInterval ti);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern ulong TIPAbsoluteFromTimeInterval (double ti);

	//	// extern NSTimeInterval TIPComputeDuration (uint64_t startTime, uint64_t endTime);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern double TIPComputeDuration (ulong startTime, ulong endTime);

	//	// extern NSString * TIPURLEncodeString (NSString *string);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern NSString TIPURLEncodeString (NSString @string);

	//	// extern NSString * TIPURLDecodeString (NSString *string, BOOL replacePlussesWithSpaces);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern NSString TIPURLDecodeString (NSString @string, bool replacePlussesWithSpaces);
	}

	[Native]
	public enum TIPImageCodecProperties : long
	{
		NoProperties = 0,
		SupportsDecoding = 1 << 0,
		SupportsEncoding = 1 << 1,
		SupportsAnimation = 1 << 2,
		SupportsProgressiveLoading = 1 << 3
	}

	[Native]
	public enum TIPImageDecoderDetectionResult : long
	{
		NoMatch = -1,
		NeedMoreData = 0,
		Match = 1
	}

	[Native]
	public enum TIPImageDecoderAppendResult : long
	{
		Progress = 0,
		LoadHeaders = 1,
		LoadFrame = 2,
		CompleteLoading = 3
	}

	[Native]
	public enum TIPImageDecoderRenderMode : long
	{
		AnyProgress = 0,
		FullFrameProgress = 1,
		CompleteImage = 2
	}

	[Native]
	public enum TIPImageLoadSource : long
	{
		Unknown = 0,
		MemoryCache,
		DiskCache,
		AdditionalCache,
		Network,
		NetworkResumed
	}

	[Native]
	public enum TIPImageFetchLoadingSources : long
	{
		MemoryCache = (1 << (int)TIPImageLoadSource.MemoryCache),
		DiskCache = (1 << (int)TIPImageLoadSource.DiskCache),
		AdditionalCache = (1 << (int)TIPImageLoadSource.AdditionalCache),
		Network = (1 << (int)TIPImageLoadSource.Network),
		NetworkResumed = (1 << (int)TIPImageLoadSource.NetworkResumed)
	}

	[Native]
	public enum TIPImageFetchErrorCode : long
	{
		Unknown = 0,
		InvalidRequest,
		HTTPTransactionError,
		CouldNotDecodeImage,
		IllegalModificationByHydrationBlock,
		CouldNotDownloadImage,
		CouldNotLoadImage,
		DownloadEncounteredToStartMoreThanOnce = 1001,
		DownloadAttemptedToHydrateRequestMoreThanOnce,
		DownloadReceivedResponseMoreThanOnce,
		DownloadNeverStarted,
		DownloadNeverAttemptedToHydrateRequest,
		DownloadNeverReceivedResponse,
		Cancelled = -1,
		CancelledAfterLoadingPreview = -2
	}

	[Native]
	public enum TIPImageStoreErrorCode : long
	{
		Unknown = 0,
		ImageNotProvided,
		ImageURLNotProvided,
		NoCacheForStoring,
		StorageFailed
	}

	[Native]
	public enum TIPErrorCode : long
	{
		Unknown = 0,
		CannotUseGPUInBackground,
		MissingCIImage,
		MissingCGImage,
		FailedToInitializeImageDestination,
		FailedToFinalizeImageDestination,
		EncodingUnsupported
	}

	[Native]
	public enum TIPImageCacheType : long
	{
		Rendered,
		Memory,
		Disk
	}

	[Native]
	public enum TIPImageDiskCacheFetchOptions : long
	{
		None = 0,
		CompleteImage = (1 << 0),
		PartialImage = (1 << 1),
		TemporaryFile = (1 << 2),
		PartialImageIfNoCompleteImage = (1 << 3),
		TemporaryFileIfNoCompleteImage = (1 << 4)
	}

	[Native]
	public enum TIPImageFetchProgress : long
	{
		None = 0,
		PartialFrame,
		FullFrame
	}

	[Native]
	public enum TIPImageFetchProgressUpdateBehavior : long
	{
		None = TIPImageFetchProgress.None,
		UpdateWithAnyProgress = TIPImageFetchProgress.PartialFrame,
		UpdateWithFullFrameProgress = TIPImageFetchProgress.FullFrame
	}

	[Native]
	public enum TIPImageFetchOptions : long
	{
		NoOptions = 0,
		DoNotResetExpiryOnAccess = 1 << 0,
		TreatAsPlaceholder = 1 << 1
	}

	[Native]
	public enum TIPPartialImageState : long
	{
		NoData = 0,
		LoadingHeaders,
		LoadingImage,
		Complete
	}

	[Native]
	public enum TIPImageFetchOperationState : long
	{
		Idle = 0,
		Starting,
		LoadingFromMemory,
		LoadingFromDisk,
		LoadingFromAdditionalCache,
		LoadingFromNetwork,
		Cancelled = -1,
		Failed = -2,
		Succeeded = -100
	}

	[Native]
	public enum TIPImageFetchPreviewLoadedBehavior : long
	{
		ContinueLoading,
		StopLoading
	}

	[Native]
	public enum TIPImageFetchLoadResult : long
	{
		NeverCompleted = -1,
		Miss = 0,
		HitPreview,
		HitProgressFrame,
		HitFinal
	}

	[Native]
	public enum TIPImageStoreOptions : long
	{
		NoOptions = 0,
		DoNotResetExpiryOnAccess = 1 << 0,
		TreatAsPlaceholder = 1 << 1
	}

	[Native]
	public enum TIPImageViewDisappearanceBehavior : long
	{
		None = 0,
		CancelImageFetch,
		LowerImageFetchPriority
	}

	[Native]
	public enum TIPLogLevel : long
	{
		Emergency,
		Alert,
		Critical,
		Error,
		Warning,
		Notice,
		Information,
		Debug
	}
}
