using System;
using CoreFoundation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace TwitterImagePipeline
{
	// @interface TIPSafety (NSOperationQueue)
	[Category]
	[BaseType(typeof(NSOperationQueue))]
	interface NSOperationQueueTIPSafety
	{
		// -(void)tip_safeAddOperation:(NSOperation * _Nonnull)op;
		[Export("tip_safeAddOperation:")]
		void SafeAddOperation(NSOperation op);
	}

	[Static]
	interface TIPImageTypes
	{
		// extern NSString *const _Nonnull TIPImageTypeJPEG;
		[Field("TIPImageTypeJPEG", "__Internal")]
		NSString JPEG { get; }

		// extern NSString *const _Nonnull TIPImageTypeJPEG2000;
		[Field("TIPImageTypeJPEG2000", "__Internal")]
		NSString JPEG2000 { get; }

		// extern NSString *const _Nonnull TIPImageTypePNG;
		[Field("TIPImageTypePNG", "__Internal")]
		NSString PNG { get; }

		// extern NSString *const _Nonnull TIPImageTypeGIF;
		[Field("TIPImageTypeGIF", "__Internal")]
		NSString GIF { get; }

		// extern NSString *const _Nonnull TIPImageTypeTIFF;
		[Field("TIPImageTypeTIFF", "__Internal")]
		NSString TIFF { get; }

		// extern NSString *const _Nonnull TIPImageTypeBMP;
		[Field("TIPImageTypeBMP", "__Internal")]
		NSString BMP { get; }

		// extern NSString *const _Nonnull TIPImageTypeTARGA;
		[Field("TIPImageTypeTARGA", "__Internal")]
		NSString TARGA { get; }

		// extern NSString *const _Nonnull TIPImageTypeICO;
		[Field("TIPImageTypeICO", "__Internal")]
		NSString ICO { get; }

		// extern NSString *const _Nonnull TIPImageTypeRAW __attribute__((availability(ios, introduced=8_0)));
		[iOS(8, 0)]
		[Field("TIPImageTypeRAW", "__Internal")]
		NSString RAW { get; }

		// extern NSString *const _Nonnull TIPImageTypePICT;
		[Field("TIPImageTypePICT", "__Internal")]
		NSString PICT { get; }

		// extern NSString *const _Nonnull TIPImageTypeQTIF;
		[Field("TIPImageTypeQTIF", "__Internal")]
		NSString QTIF { get; }

		// extern NSString *const _Nonnull TIPImageTypeICNS;
		[Field("TIPImageTypeICNS", "__Internal")]
		NSString ICNS { get; }
	}

	interface ITIPImageCodec : INativeObject { }

	// @protocol TIPImageCodec <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageCodec")]
	interface TIPImageCodec : INativeObject
	{
		// @required -(id<TIPImageDecoder> _Nonnull)tip_decoder;
		[Abstract]
		[Export("tip_decoder")]
		ITIPImageDecoder Decoder { get; }

		// @required -(id<TIPImageEncoder> _Nullable)tip_encoder;
		[Abstract]
		[NullAllowed, Export("tip_encoder")]
		ITIPImageEncoder Encoder { get; }

		// @optional -(BOOL)tip_isAnimated;
		[Export("tip_isAnimated")]
		bool IsAnimated { get; }
	}

	interface ITIPImageEncoder { }

	// @protocol TIPImageEncoder <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageEncoder")]
	interface TIPImageEncoder
	{
		// @required -(NSData * _Nullable)tip_writeDataWithImage:(TIPImageContainer * _Nonnull)image encodingOptions:(TIPImageEncodingOptions)encodingOptions suggestedQuality:(float)quality error:(NSError * _Nullable * _Nullable)error;
		[Abstract]
		[Export("tip_writeDataWithImage:encodingOptions:suggestedQuality:error:")]
		[return: NullAllowed]
		NSData WriteData(TIPImageContainer image, TIPImageEncodingOptions encodingOptions, float quality, [NullAllowed] out NSError error);

		// @optional -(BOOL)tip_writeToFile:(NSString * _Nonnull)filePath withImage:(TIPImageContainer * _Nonnull)image encodingOptions:(TIPImageEncodingOptions)encodingOptions suggestedQuality:(float)quality atomically:(BOOL)atomic error:(NSError * _Nullable * _Nullable)error;
		[Export("tip_writeToFile:withImage:encodingOptions:suggestedQuality:atomically:error:")]
		bool WriteToFile(string filePath, TIPImageContainer image, TIPImageEncodingOptions encodingOptions, float quality, bool atomic, [NullAllowed] out NSError error);
	}

	interface ITIPImageDecoderContext { }

	// @protocol TIPImageDecoderContext <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageDecoderContext")]
	interface TIPImageDecoderContext
	{
		// @required @property (readonly, nonatomic) NSData * _Nonnull tip_data;
		[Abstract]
		[Export("tip_data")]
		NSData Data { get; }

		// @required @property (readonly, nonatomic) CGSize tip_dimensions;
		[Abstract]
		[Export("tip_dimensions")]
		CGSize Dimensions { get; }

		// @required @property (readonly, nonatomic) NSUInteger tip_frameCount;
		[Abstract]
		[Export("tip_frameCount")]
		nuint FrameCount { get; }

		// @optional @property (readonly, nonatomic) BOOL tip_isProgressive;
		[Export("tip_isProgressive")]
		bool IsProgressive { get; }

		// @optional @property (readonly, nonatomic) BOOL tip_isAnimated;
		[Export("tip_isAnimated")]
		bool IsAnimated { get; }

		// @optional @property (readonly, nonatomic) BOOL tip_hasAlpha;
		[Export("tip_hasAlpha")]
		bool HasAlpha { get; }

		// @optional @property (readonly, nonatomic) BOOL tip_hasGPSInfo;
		[Export("tip_hasGPSInfo")]
		bool HasGpsInfo { get; }
	}

	interface ITIPImageDecoder { }

	// @protocol TIPImageDecoder <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageDecoder")]
	interface TIPImageDecoder
	{
		// @required -(TIPImageDecoderDetectionResult)tip_detectDecodableData:(NSData * _Nonnull)data earlyGuessImageType:(NSString * _Nullable)imageType;
		[Abstract]
		[Export("tip_detectDecodableData:earlyGuessImageType:")]
		TIPImageDecoderDetectionResult DetectDecodableData(NSData data, [NullAllowed] string imageType);

		// @required -(id<TIPImageDecoderContext> _Nonnull)tip_initiateDecodingWithExpectedDataLength:(NSUInteger)expectedDataLength buffer:(NSMutableData * _Nullable)buffer;
		[Abstract]
		[Export("tip_initiateDecodingWithExpectedDataLength:buffer:")]
		ITIPImageDecoderContext InitiateDecoding(nuint expectedDataLength, [NullAllowed] NSMutableData buffer);

		// @required -(TIPImageDecoderAppendResult)tip_append:(id<TIPImageDecoderContext> _Nonnull)context data:(NSData * _Nonnull)data;
		[Abstract]
		[Export("tip_append:data:")]
		TIPImageDecoderAppendResult Append(ITIPImageDecoderContext context, NSData data);

		// @required -(TIPImageContainer * _Nullable)tip_renderImage:(id<TIPImageDecoderContext> _Nonnull)context mode:(TIPImageDecoderRenderMode)mode;
		[Abstract]
		[Export("tip_renderImage:mode:")]
		[return: NullAllowed]
		TIPImageContainer RenderImage(ITIPImageDecoderContext context, TIPImageDecoderRenderMode mode);

		// @required -(TIPImageDecoderAppendResult)tip_finalizeDecoding:(id<TIPImageDecoderContext> _Nonnull)context;
		[Abstract]
		[Export("tip_finalizeDecoding:")]
		TIPImageDecoderAppendResult FinalizeDecoding(ITIPImageDecoderContext context);

		// @optional -(BOOL)tip_supportsProgressiveDecoding;
		[Export("tip_supportsProgressiveDecoding")]
		bool SupportsProgressiveDecoding { get; }

		// @optional -(TIPImageContainer * _Nullable)tip_decodeImageWithData:(NSData * _Nonnull)imageData;
		[Export("tip_decodeImageWithData:")]
		[return: NullAllowed]
		TIPImageContainer DecodeImage(NSData imageData);
	}

	interface ITIPDependencyOperation { }

	// @protocol TIPDependencyOperation <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPDependencyOperation")]
	interface TIPDependencyOperation
	{
		// @required -(void)makeDependencyOfTargetOperation:(NSOperation * _Nonnull)op;
		[Abstract]
		[Export("makeDependencyOfTargetOperation:")]
		void MakeDependencyOfTargetOperation(NSOperation op);

		// @required -(void)waitUntilFinished;
		[Abstract]
		[Export("waitUntilFinished")]
		void WaitUntilFinished();

		// @required -(BOOL)isFinished;
		[Abstract]
		[Export("isFinished")]
		bool IsFinished { get; }

		// @required -(BOOL)isExecuting;
		[Abstract]
		[Export("isExecuting")]
		bool IsExecuting { get; }
	}

	[Static]
	interface TIPErrorDomains
	{
		// extern NSString *const _Nonnull TIPImageFetchErrorDomain;
		[Field("TIPImageFetchErrorDomain", "__Internal")]
		NSString ImageFetchErrorDomain { get; }

		// extern NSString *const _Nonnull TIPImageStoreErrorDomain;
		[Field("TIPImageStoreErrorDomain", "__Internal")]
		NSString ImageStoreErrorDomain { get; }

		// extern NSString *const _Nonnull TIPErrorDomain;
		[Field("TIPErrorDomain", "__Internal")]
		NSString ErrorDomain { get; }

		// extern NSString *const _Nonnull TIPErrorUserInfoHTTPStatusCodeKey;
		[Field("TIPErrorUserInfoHTTPStatusCodeKey", "__Internal")]
		NSString ErrorUserInfoHttpStatusCodeKey { get; }
	}

	interface ITIPProblemObserver { }

	// @protocol TIPProblemObserver <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPProblemObserver")]
	interface TIPProblemObserver
	{
		// @optional -(void)tip_problemWasEncountered:(NSString * _Nonnull)problemName userInfo:(NSDictionary<NSString *,id> * _Nonnull)userInfo;
		[Export("tip_problemWasEncountered:userInfo:")]
		void ProblemWasEncountered(string problemName, NSDictionary<NSString, NSObject> userInfo);

		// @optional -(void)tip_CGContextAccessed:(NSTimeInterval)duration serially:(BOOL)serially;
		[Export("tip_CGContextAccessed:serially:")]
		void CGContextAccessed(double duration, bool serially);
	}

	[Static]
	partial interface TIPProblemNames
	{
		// extern NSString *const _Nonnull TIPProblemDiskCacheUpdateImageEntryCouldNotGenerateFileName;
		[Field("TIPProblemDiskCacheUpdateImageEntryCouldNotGenerateFileName", "__Internal")]
		NSString DiskCacheUpdateImageEntryCouldNotGenerateFileName { get; }

		// extern NSString *const _Nonnull TIPProblemImageFailedToScale;
		[Field("TIPProblemImageFailedToScale", "__Internal")]
		NSString ImageFailedToScale { get; }

		// extern NSString *const _Nonnull TIPProblemImageContainerHasNilImage;
		[Field("TIPProblemImageContainerHasNilImage", "__Internal")]
		NSString ImageContainerHasNullImage { get; }

		// extern NSString *const _Nonnull TIPProblemImageFetchHasInvalidTargetDimensions;
		[Field("TIPProblemImageFetchHasInvalidTargetDimensions", "__Internal")]
		NSString ImageFetchHasInvalidTargetDimensions { get; }

		// extern NSString *const _Nonnull TIPProblemImageDownloadedHasGPSInfo;
		[Field("TIPProblemImageDownloadedHasGPSInfo", "__Internal")]
		NSString ImageDownloadedHasGpsInfo { get; }
	}

	[Static]
	partial interface TIPProblemInfoKeys
	{
		// extern NSString *const _Nonnull TIPProblemInfoKeyImageIdentifier;
		[Field("TIPProblemInfoKeyImageIdentifier", "__Internal")]
		NSString ImageIdentifier { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeySafeImageIdentifier;
		[Field("TIPProblemInfoKeySafeImageIdentifier", "__Internal")]
		NSString SafeImageIdentifier { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyImageURL;
		[Field("TIPProblemInfoKeyImageURL", "__Internal")]
		NSString ImageUrl { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyTargetDimensions;
		[Field("TIPProblemInfoKeyTargetDimensions", "__Internal")]
		NSString TargetDimensions { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyTargetContentMode;
		[Field("TIPProblemInfoKeyTargetContentMode", "__Internal")]
		NSString TargetContentMode { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyScaledDimensions;
		[Field("TIPProblemInfoKeyScaledDimensions", "__Internal")]
		NSString ScaledDimensions { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyImageDimensions;
		[Field("TIPProblemInfoKeyImageDimensions", "__Internal")]
		NSString ImageDimensions { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyImageIsAnimated;
		[Field("TIPProblemInfoKeyImageIsAnimated", "__Internal")]
		NSString ImageIsAnimated { get; }

		// extern NSString *const _Nonnull TIPProblemInfoKeyFetchRequest;
		[Field("TIPProblemInfoKeyFetchRequest", "__Internal")]
		NSString FetchRequest { get; }
	}

	[Static]
	partial interface TIPGlobalConfigurationDefaults
	{
		// extern const SInt64 TIPMaxBytesForAllRenderedCachesDefault;
		[Field("TIPMaxBytesForAllRenderedCachesDefault", "__Internal")]
		long MaxBytesForAllRenderedCaches { get; }

		// extern const SInt64 TIPMaxBytesForAllMemoryCachesDefault;
		[Field("TIPMaxBytesForAllMemoryCachesDefault", "__Internal")]
		long MaxBytesForAllMemoryCaches { get; }

		// extern const SInt64 TIPMaxBytesForAllDiskCachesDefault;
		[Field("TIPMaxBytesForAllDiskCachesDefault", "__Internal")]
		long MaxBytesForAllDiskCaches { get; }

		// extern const NSInteger TIPMaxConcurrentImagePipelineDownloadCountDefault;
		[Field("TIPMaxConcurrentImagePipelineDownloadCountDefault", "__Internal")]
		nint MaxConcurrentImagePipelineDownloadCount { get; }

		// extern const NSTimeInterval TIPMaxEstimatedTimeRemainingForDetachedHTTPDownloadsDefault;
		[Field("TIPMaxEstimatedTimeRemainingForDetachedHTTPDownloadsDefault", "__Internal")]
		double MaxEstimatedTimeRemainingForDetachedHttpDownloads { get; }

		// extern const NSUInteger TIPMaxRatioSizeOfCacheEntryDefault;
		[Field("TIPMaxRatioSizeOfCacheEntryDefault", "__Internal")]
		nuint MaxRatioSizeOfCacheEntry { get; }

		//// TODO: this is really a short, but https://bugzilla.xamarin.com/show_bug.cgi?id=58845
		//// extern const SInt16 TIPMaxCountForAllMemoryCachesDefault;
		//[Field("TIPMaxCountForAllMemoryCachesDefault", "__Internal")]
		//long MaxCountForAllMemoryCaches { get; }

		//// TODO: this is really a short, but https://bugzilla.xamarin.com/show_bug.cgi?id=58845
		//// extern const SInt16 TIPMaxCountForAllRenderedCachesDefault;
		//[Field("TIPMaxCountForAllRenderedCachesDefault", "__Internal")]
		//long MaxCountForAllRenderedCaches { get; }

		//// TODO: this is really a short, but https://bugzilla.xamarin.com/show_bug.cgi?id=58845
		//// extern const SInt16 TIPMaxCountForAllDiskCachesDefault;
		//[Field("TIPMaxCountForAllDiskCachesDefault", "__Internal")]
		//long MaxCountForAllDiskCaches { get; }
	}

	// typedef int64_t (^TIPEstimatedBitrateProviderBlock)(NSString * _Nonnull);
	delegate long TIPEstimatedBitrateProviderDelegate(string domain);

	// @interface TIPGlobalConfiguration : NSObject
	[BaseType(typeof(NSObject), Name = "TIPGlobalConfiguration")]
	[DisableDefaultCtor]
	interface TIPGlobalConfiguration
	{
		// @property (atomic) SInt64 maxBytesForAllRenderedCaches;
		[Export("maxBytesForAllRenderedCaches")]
		long MaxBytesForAllRenderedCaches { get; set; }

		// @property (atomic) SInt64 maxBytesForAllMemoryCaches;
		[Export("maxBytesForAllMemoryCaches")]
		long MaxBytesForAllMemoryCaches { get; set; }

		// @property (atomic) SInt64 maxBytesForAllDiskCaches;
		[Export("maxBytesForAllDiskCaches")]
		long MaxBytesForAllDiskCaches { get; set; }

		// @property (atomic) SInt16 maxCountForAllRenderedCaches;
		[Export("maxCountForAllRenderedCaches")]
		short MaxCountForAllRenderedCaches { get; set; }

		// @property (atomic) SInt16 maxCountForAllMemoryCaches;
		[Export("maxCountForAllMemoryCaches")]
		short MaxCountForAllMemoryCaches { get; set; }

		// @property (atomic) SInt16 maxCountForAllDiskCaches;
		[Export("maxCountForAllDiskCaches")]
		short MaxCountForAllDiskCaches { get; set; }

		// @property (atomic) NSInteger maxRatioSizeOfCacheEntry;
		[Export("maxRatioSizeOfCacheEntry")]
		nint MaxRatioSizeOfCacheEntry { get; set; }

		// @property (readonly, atomic) SInt64 totalBytesForAllRenderedCaches;
		[Export("totalBytesForAllRenderedCaches")]
		long TotalBytesForAllRenderedCaches { get; }

		// @property (readonly, atomic) SInt64 totalBytesForAllMemoryCaches;
		[Export("totalBytesForAllMemoryCaches")]
		long TotalBytesForAllMemoryCaches { get; }

		// @property (readonly, atomic) SInt64 totalBytesForAllDiskCaches;
		[Export("totalBytesForAllDiskCaches")]
		long TotalBytesForAllDiskCaches { get; }

		// -(void)clearAllDiskCaches;
		[Export("clearAllDiskCaches")]
		void ClearAllDiskCaches();

		// -(void)clearAllMemoryCaches;
		[Export("clearAllMemoryCaches")]
		void ClearAllMemoryCaches();

		// @property (atomic) NSTimeInterval maxEstimatedTimeRemainingForDetachedHTTPDownloads;
		[Export("maxEstimatedTimeRemainingForDetachedHTTPDownloads")]
		double MaxEstimatedTimeRemainingForDetachedHttpDownloads { get; set; }

		// @property (copy, atomic) TIPEstimatedBitrateProviderBlock _Nullable estimatedBitrateProviderBlock;
		[NullAllowed, Export("estimatedBitrateProviderBlock", ArgumentSemantic.Copy)]
		TIPEstimatedBitrateProviderDelegate EstimatedBitrateProviderBlock { get; set; }

		// @property (nonatomic) id<TIPImageFetchDownloadProvider> _Null_unspecified imageFetchDownloadProvider;
		[Export("imageFetchDownloadProvider", ArgumentSemantic.Assign)]
		ITIPImageFetchDownloadProvider ImageFetchDownloadProvider { get; set; }

		// @property (atomic) NSInteger maxConcurrentImagePipelineDownloadCount;
		[Export("maxConcurrentImagePipelineDownloadCount")]
		nint MaxConcurrentImagePipelineDownloadCount { get; set; }

		// -(void)addImagePipelineObserver:(id<TIPImagePipelineObserver> _Nonnull)observer;
		[Export("addImagePipelineObserver:")]
		void AddImagePipelineObserver(ITIPImagePipelineObserver observer);

		// -(void)removeImagePipelineObserver:(id<TIPImagePipelineObserver> _Nonnull)observer;
		[Export("removeImagePipelineObserver:")]
		void RemoveImagePipelineObserver(ITIPImagePipelineObserver observer);

		// @property (readwrite, atomic) id<TIPLogger> _Nullable logger;
		[NullAllowed, Export("logger", ArgumentSemantic.Assign)]
		ITIPLogger Logger { get; set; }

		// @property (readwrite, atomic) id<TIPProblemObserver> _Nullable problemObserver;
		[NullAllowed, Export("problemObserver", ArgumentSemantic.Assign)]
		ITIPProblemObserver ProblemObserver { get; set; }

		// @property (getter = areAssertsEnabled, readwrite, nonatomic) BOOL assertsEnabled;
		[Export("assertsEnabled")]
		bool AssertsEnabled { [Bind("areAssertsEnabled")] get; set; }

		// @property (readwrite, nonatomic) BOOL serializeCGContextAccess;
		[Export("serializeCGContextAccess")]
		bool SerializeCGContextAccess { get; set; }

		// @property (getter = isClearMemoryCachesOnApplicationBackgroundEnabled, readwrite, nonatomic) BOOL clearMemoryCachesOnApplicationBackgroundEnabled;
		[Export("clearMemoryCachesOnApplicationBackgroundEnabled")]
		bool ClearMemoryCachesOnApplicationBackgroundEnabled { [Bind("isClearMemoryCachesOnApplicationBackgroundEnabled")] get; set; }

		// +(instancetype _Nonnull)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		TIPGlobalConfiguration SharedInstance { get; }
	}

	// typedef void (^TIPGlobalConfigurationInspectionCallback)(NSDictionary<NSString *,TIPImagePipelineInspectionResult *> * _Nonnull);
	delegate void TIPGlobalConfigurationInspectionCallback(NSDictionary<NSString, TIPImagePipelineInspectionResult> results);

	// @interface Inspect (TIPGlobalConfiguration)
	[Category]
	[BaseType(typeof(TIPGlobalConfiguration))]
	interface TIPGlobalConfigurationInspect
	{
		// -(void)inspect:(TIPGlobalConfigurationInspectionCallback _Nonnull)callback;
		[Export("inspect:")]
		void Inspect(TIPGlobalConfigurationInspectionCallback callback);
	}

	interface ITIPImageCache { }

	// @interface TIPImageContainer : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImageContainer")]
	[DisableDefaultCtor]
	interface TIPImageContainer
	{
		// @property (readonly, nonatomic) UIImage * _Nonnull image;
		[Export("image")]
		UIImage Image { get; }

		// @property (readonly, getter = isAnimated, nonatomic) BOOL animated;
		[Export("animated")]
		bool Animated { [Bind("isAnimated")] get; }

		// -(instancetype _Nonnull)initWithImage:(UIImage * _Nonnull)image;
		[Export("initWithImage:")]
		IntPtr Constructor(UIImage image);

		// +(instancetype _Nullable)imageContainerWithImageSource:(CGImageSourceRef _Nonnull)imageSource;
		[Static]
		[Export("imageContainerWithImageSource:")]
		[return: NullAllowed]
		[Internal]
		TIPImageContainer Create(IntPtr imageSource);

		// +(instancetype _Nullable)imageContainerWithData:(NSData * _Nonnull)data codecCatalogue:(TIPImageCodecCatalogue * _Nullable)catalogue;
		[Static]
		[Export("imageContainerWithData:codecCatalogue:")]
		[return: NullAllowed]
		TIPImageContainer Create(NSData data, [NullAllowed] TIPImageCodecCatalogue catalogue);

		// +(instancetype _Nullable)imageContainerWithFilePath:(NSString * _Nonnull)filePath codecCatalogue:(TIPImageCodecCatalogue * _Nullable)catalogue;
		[Static]
		[Export("imageContainerWithFilePath:codecCatalogue:")]
		[return: NullAllowed]
		TIPImageContainer Create(string filePath, [NullAllowed] TIPImageCodecCatalogue catalogue);

		// +(instancetype _Nullable)imageContainerWithFileURL:(NSURL * _Nonnull)fileURL codecCatalogue:(TIPImageCodecCatalogue * _Nullable)catalogue;
		[Static]
		[Export("imageContainerWithFileURL:codecCatalogue:")]
		[return: NullAllowed]
		TIPImageContainer Create(NSUrl fileUrl, [NullAllowed] TIPImageCodecCatalogue catalogue);

		// -(instancetype _Nonnull)initWithAnimatedImage:(UIImage * _Nonnull)image loopCount:(NSUInteger)loopCount frameDurations:(NSArray<NSNumber *> * _Nullable)durations;
		[Static]
		[Export("initWithAnimatedImage:loopCount:frameDurations:")]
		TIPImageContainer CreateWithAnimatedImage(UIImage image, nuint loopCount, [NullAllowed] NSNumber[] durations);
	}

	// @interface Animated (TIPImageContainer)
	[Category]
	[BaseType(typeof(TIPImageContainer))]
	interface TIPImageContainerAnimated
	{
		// @property (readonly, nonatomic) NSUInteger loopCount;
		[Export("loopCount")]
		nuint LoopCount();

		// @property (readonly, nonatomic) NSUInteger frameCount;
		[Export("frameCount")]
		nuint FrameCount();

		// @property (readonly, nonatomic) NSArray<UIImage *> * _Nullable frames;
		[NullAllowed, Export("frames")]
		UIImage[] Frames();

		// @property (readonly, nonatomic) NSArray<NSNumber *> * _Nullable frameDurations;
		[NullAllowed, Export("frameDurations")]
		NSNumber[] FrameDurations();

		// -(UIImage * _Nullable)frameAtIndex:(NSUInteger)index;
		[Export("frameAtIndex:")]
		[return: NullAllowed]
		UIImage FrameAtIndex(nuint index);

		// -(NSTimeInterval)frameDurationAtIndex:(NSUInteger)index;
		[Export("frameDurationAtIndex:")]
		double FrameDurationAtIndex(nuint index);
	}

	// @interface Convenience (TIPImageContainer)
	[Category]
	[BaseType(typeof(TIPImageContainer))]
	interface TIPImageContainerConvenience
	{
		// @property (readonly, nonatomic) NSUInteger sizeInMemory;
		[Export("sizeInMemory")]
		nuint SizeInMemory();

		// @property (readonly, nonatomic) CGSize dimensions;
		[Export("dimensions")]
		CGSize Dimensions();

		// -(void)decode;
		[Export("decode")]
		void Decode();

		// -(TIPImageContainer * _Nullable)scaleToTargetDimensions:(CGSize)dimensions contentMode:(UIViewContentMode)contentMode;
		[Export("scaleToTargetDimensions:contentMode:")]
		[return: NullAllowed]
		TIPImageContainer ScaleTo(CGSize dimensions, UIViewContentMode contentMode);

		// -(BOOL)saveToFilePath:(NSString * _Nonnull)path type:(NSString * _Nullable)type codecCatalogue:(TIPImageCodecCatalogue * _Nullable)catalogue options:(TIPImageEncodingOptions)options quality:(float)quality atomic:(BOOL)atomic error:(NSError * _Nullable * _Nullable)error;
		[Export("saveToFilePath:type:codecCatalogue:options:quality:atomic:error:")]
		bool SaveTo(string path, [NullAllowed] string type, [NullAllowed] TIPImageCodecCatalogue catalogue, TIPImageEncodingOptions options, float quality, bool atomic, [NullAllowed] out NSError error);
	}

	// @interface TIPImageCodecCatalogue : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImageCodecCatalogue")]
	interface TIPImageCodecCatalogue
	{
		// +(NSDictionary<NSString *,id<TIPImageCodec>> * _Nonnull)defaultCodecs;
		[Static]
		[Export("defaultCodecs")]
		NSDictionary<NSString, ITIPImageCodec> DefaultCodecs { get; }

		// +(instancetype _Nonnull)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		TIPImageCodecCatalogue SharedInstance { get; }

		// -(instancetype _Nonnull)initWithCodecs:(NSDictionary<NSString *,id<TIPImageCodec>> * _Nullable)codecs __attribute__((objc_designated_initializer));
		[Export("initWithCodecs:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] NSDictionary<NSString, ITIPImageCodec> codecs);

		// @property (readonly, atomic) NSDictionary<NSString *,id<TIPImageCodec>> * _Nonnull allCodecs;
		[Export("allCodecs")]
		NSDictionary<NSString, ITIPImageCodec> AllCodecs { get; }

		// -(id<TIPImageCodec> _Nullable)codecForImageType:(NSString * _Nonnull)imageType;
		[Export("codecForImageType:")]
		[return: NullAllowed]
		ITIPImageCodec GetCodec(string imageType);

		// -(void)setCodec:(id<TIPImageCodec> _Nonnull)codec forImageType:(NSString * _Nonnull)imageType;
		[Export("setCodec:forImageType:")]
		void SetCodec(ITIPImageCodec codec, string imageType);

		// -(void)removeCodecForImageType:(NSString * _Nonnull)imageType;
		[Export("removeCodecForImageType:")]
		void RemoveCodec(string imageType);

		// -(void)removeCodecForImageType:(NSString * _Nonnull)imageType removedCodec:(id<TIPImageCodec>  _Nullable * _Nullable)codec;
		[Export("removeCodecForImageType:removedCodec:")]
		[Internal]
		void RemoveCodec(string imageType, [NullAllowed] out IntPtr codec);
	}

	// @interface KeyedSubscripting (TIPImageCodecCatalogue)
	[Category]
	[BaseType(typeof(TIPImageCodecCatalogue))]
	interface TIPImageCodecCatalogueKeyedSubscripting
	{
		// -(void)setObject:(id<TIPImageCodec> _Nullable)codec forKeyedSubscript:(NSString * _Nonnull)imageType;
		[Export("setObject:forKeyedSubscript:")]
		void SetObject([NullAllowed] ITIPImageCodec codec, string imageType);

		// -(id<TIPImageCodec> _Nullable)objectForKeyedSubscript:(NSString * _Nonnull)imageType;
		[Export("objectForKeyedSubscript:")]
		[return: NullAllowed]
		ITIPImageCodec GetObject(string imageType);
	}

	// @interface Convenience (TIPImageCodecCatalogue)
	[Category]
	[BaseType(typeof(TIPImageCodecCatalogue))]
	interface TIPImageCodecCatalogueConvenience
	{
		// -(BOOL)codecWithImageTypeSupportsProgressiveLoading:(NSString * _Nullable)type;
		[Export("codecWithImageTypeSupportsProgressiveLoading:")]
		bool CodecSupportsProgressiveLoading([NullAllowed] string type);

		// -(BOOL)codecWithImageTypeSupportsAnimation:(NSString * _Nullable)type;
		[Export("codecWithImageTypeSupportsAnimation:")]
		bool CodecSupportsAnimation([NullAllowed] string type);

		// -(BOOL)codecWithImageTypeSupportsDecoding:(NSString * _Nullable)type;
		[Export("codecWithImageTypeSupportsDecoding:")]
		bool CodecSupportsDecoding([NullAllowed] string type);

		// -(BOOL)codecWithImageTypeSupportsEncoding:(NSString * _Nullable)type;
		[Export("codecWithImageTypeSupportsEncoding:")]
		bool CodecSupportsEncoding([NullAllowed] string type);

		// -(TIPImageCodecProperties)propertiesForCodecWithImageType:(NSString * _Nullable)type;
		[Export("propertiesForCodecWithImageType:")]
		TIPImageCodecProperties PropertiesForCodec([NullAllowed] string type);

		// -(TIPImageContainer * _Nullable)decodeImageWithData:(NSData * _Nonnull)data imageType:(NSString * _Nullable * _Nullable)imageType;
		[Export("decodeImageWithData:imageType:")]
		[return: NullAllowed]
		TIPImageContainer DecodeImage(NSData data, [NullAllowed] out string imageType);

		// -(BOOL)encodeImage:(TIPImageContainer * _Nonnull)image toFilePath:(NSString * _Nonnull)filePath withImageType:(NSString * _Nonnull)imageType quality:(float)quality options:(TIPImageEncodingOptions)options atomic:(BOOL)atomic error:(NSError * _Nullable * _Nullable)error;
		[Export("encodeImage:toFilePath:withImageType:quality:options:atomic:error:")]
		bool EncodeImage(TIPImageContainer image, string filePath, string imageType, float quality, TIPImageEncodingOptions options, bool atomic, [NullAllowed] out NSError error);

		// -(NSData * _Nullable)encodeImage:(TIPImageContainer * _Nonnull)image withImageType:(NSString * _Nonnull)imageType quality:(float)quality options:(TIPImageEncodingOptions)options error:(NSError * _Nullable * _Nullable)error;
		[Export("encodeImage:withImageType:quality:options:error:")]
		[return: NullAllowed]
		NSData EncodeImage(TIPImageContainer image, string imageType, float quality, TIPImageEncodingOptions options, [NullAllowed] out NSError error);
	}

	interface ITIPImageFetchProgressiveLoadingPolicy : INativeObject { }

	// @protocol TIPImageFetchProgressiveLoadingPolicy <NSObject>
	[Protocol(Name = "TIPImageFetchProgressiveLoadingPolicy"), Model]
	[BaseType(typeof(NSObject))]
	interface TIPImageFetchProgressiveLoadingPolicy : INativeObject
	{
		// @required -(TIPImageFetchProgressUpdateBehavior)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op behaviorForProgress:(TIPImageFetchProgress)frameProgress frameCount:(NSUInteger)frameCount progress:(float)progress type:(NSString * _Nonnull)type dimensions:(CGSize)dimensions renderCount:(NSUInteger)renderCount;
		[Abstract]
		[Export("tip_imageFetchOperation:behaviorForProgress:frameCount:progress:type:dimensions:renderCount:")]
		TIPImageFetchProgressUpdateBehavior BehaviorForProgress(TIPImageFetchOperation op, TIPImageFetchProgress frameProgress, nuint frameCount, float progress, string type, CGSize dimensions, nuint renderCount);
	}

	// TODO ???
	//// @interface TIPImageFetchProgressiveLoadingPolicy
	//// @interface ClassMethods (TIPImageFetchProgressiveLoadingPolicy)
	//[BaseType(typeof(NSObject), Name = "TIPImageFetchProgressiveLoadingPolicy")]
	//[DisableDefaultCtor]
	//interface TIPImageFetchProgressiveLoadingPolicyClass
	//{
	//	// +(NSDictionary<NSString *,id<TIPImageFetchProgressiveLoadingPolicy>> * _Nonnull)defaultProgressiveLoadingPolicies;
	//	[Static]
	//	[Export("defaultProgressiveLoadingPolicies")]
	//	NSDictionary<NSString, TIPImageFetchProgressiveLoadingPolicy> DefaultProgressiveLoadingPolicies { get; }
	//}
	//
	//[Category]
	//[BaseType(typeof(TIPImageFetchProgressiveLoadingPolicy))]
	//interface TIPImageFetchProgressiveLoadingPolicyClassMethods
	//{
	//}

	// typedef void (^TIPImageFetchHydrationCompletionBlock)(NSURLRequest * _Nullable, NSError * _Nullable);
	delegate void TIPImageFetchHydrationCompletionDelegate([NullAllowed] NSUrlRequest hydratedRequest, [NullAllowed] NSError error);

	// typedef void (^TIPImageFetchHydrationBlock)(NSURLRequest * _Nonnull, id<TIPImageFetchOperationUnderlyingContext> _Nonnull, TIPImageFetchHydrationCompletionBlock _Nonnull);
	delegate void TIPImageFetchHydrationDelegate(NSUrlRequest requestToHydrate, ITIPImageFetchOperationUnderlyingContext context, TIPImageFetchHydrationCompletionDelegate complete);

	interface ITIPImageFetchRequest { }

	// @protocol TIPImageFetchRequest <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchRequest")]
	interface TIPImageFetchRequest
	{
		// @required @property (readonly, nonatomic) NSURL * _Nonnull imageURL;
		[Abstract]
		[Export("imageURL")]
		NSUrl ImageUrl { get; }

		// @optional @property (readonly, copy, nonatomic) NSString * _Nullable imageIdentifier;
		[NullAllowed, Export("imageIdentifier")]
		string ImageIdentifier { get; }

		// @optional @property (readonly, nonatomic) CGSize targetDimensions;
		[Export("targetDimensions")]
		CGSize TargetDimensions { get; }

		// @optional @property (readonly, nonatomic) UIViewContentMode targetContentMode;
		[Export("targetContentMode")]
		UIViewContentMode TargetContentMode { get; }

		// @optional @property (readonly, nonatomic) NSTimeInterval timeToLive;
		[Export("timeToLive")]
		double TimeToLive { get; }

		// @optional @property (readonly, nonatomic) TIPImageFetchOptions options;
		[Export("options")]
		TIPImageFetchOptions Options { get; }

		// @optional @property (readonly, copy, nonatomic) NSDictionary<NSString *,id<TIPImageFetchProgressiveLoadingPolicy>> * _Nullable progressiveLoadingPolicies;
		[NullAllowed, Export("progressiveLoadingPolicies", ArgumentSemantic.Copy)]
		NSDictionary<NSString, ITIPImageFetchProgressiveLoadingPolicy> ProgressiveLoadingPolicies { get; }

		// @optional @property (readonly, nonatomic) TIPImageFetchLoadingSources loadingSources;
		[Export("loadingSources")]
		TIPImageFetchLoadingSources LoadingSources { get; }

		// @optional @property (readonly, copy, nonatomic) TIPImageFetchHydrationBlock _Nullable imageRequestHydrationBlock;
		[NullAllowed, Export("imageRequestHydrationBlock", ArgumentSemantic.Copy)]
		TIPImageFetchHydrationDelegate ImageRequestHydrationBlock { get; }
	}

	interface ITIPMutableTargetSizingImageFetchRequest { }

	// @protocol TIPMutableTargetSizingImageFetchRequest <TIPImageFetchRequest>
	[Protocol(Name = "TIPMutableTargetSizingImageFetchRequest"), Model]
	interface TIPMutableTargetSizingImageFetchRequest : TIPImageFetchRequest
	{
		// @required @property (nonatomic) CGSize targetDimensions;
		[Abstract]
		[Export("targetDimensions", ArgumentSemantic.Assign)]
		CGSize TargetDimensions { get; set; }

		// @required @property (nonatomic) UIViewContentMode targetContentMode;
		[Abstract]
		[Export("targetContentMode", ArgumentSemantic.Assign)]
		UIViewContentMode TargetContentMode { get; set; }
	}

	[Static]
	partial interface TIPImageFetchDownloadConstructorConstants
	{
		// extern NSString *const _Nonnull TIPImageFetchDownloadConstructorExceptionName;
		[Field("TIPImageFetchDownloadConstructorExceptionName", "__Internal")]
		NSString ExceptionName { get; }
	}

	interface ITIPImageFetchDownload { }

	// @protocol TIPImageFetchDownload <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchDownload")]
	interface TIPImageFetchDownload
	{
		// @required @property (readonly, nonatomic) id<TIPImageFetchDownloadContext> _Nullable context;
		[Abstract]
		[NullAllowed, Export("context")]
		ITIPImageFetchDownloadContext Context { get; }

		// @required -(void)start;
		[Abstract]
		[Export("start")]
		void Start();

		// @required -(void)cancelWithDescription:(NSString * _Nonnull)cancelDescription;
		[Abstract]
		[Export("cancelWithDescription:")]
		void Cancel(string cancelDescription);

		// @required -(void)discardContext;
		[Abstract]
		[Export("discardContext")]
		void DiscardContext();

		// @required @property (readonly, nonatomic) NSURLRequest * _Nonnull finalURLRequest;
		[Abstract]
		[Export("finalURLRequest")]
		NSUrlRequest FinalUrlRequest { get; }

		// @optional @property (readonly, nonatomic) id _Nonnull downloadMetrics;
		[Export("downloadMetrics")]
		NSObject DownloadMetrics { get; }

		// @optional @property (nonatomic) NSOperationQueuePriority priority;
		[Export("priority", ArgumentSemantic.Assign)]
		NSOperationQueuePriority Priority { get; set; }
	}

	// typedef void (^TIPImageFetchDownloadRequestHydrationCompleteBlock)(NSError * _Nullable);
	delegate void TIPImageFetchDownloadRequestHydrationCompleteDelegate([NullAllowed] NSError error);

	interface ITIPImageFetchDownloadClient { }

	// @protocol TIPImageFetchDownloadClient <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchDownloadClient")]
	interface TIPImageFetchDownloadClient
	{
		// @required -(void)imageFetchDownloadDidStart:(id<TIPImageFetchDownload> _Nonnull)download;
		[Abstract]
		[Export("imageFetchDownloadDidStart:")]
		void DidStart(ITIPImageFetchDownload download);

		// @required -(void)imageFetchDownload:(id<TIPImageFetchDownload> _Nonnull)download hydrateRequest:(NSURLRequest * _Nonnull)request completion:(TIPImageFetchDownloadRequestHydrationCompleteBlock _Nonnull)complete;
		[Abstract]
		[Export("imageFetchDownload:hydrateRequest:completion:")]
		void HydrateRequest(ITIPImageFetchDownload download, NSUrlRequest request, TIPImageFetchDownloadRequestHydrationCompleteDelegate complete);

		// @required -(void)imageFetchDownload:(id<TIPImageFetchDownload> _Nonnull)download didReceiveURLResponse:(NSHTTPURLResponse * _Nonnull)response;
		[Abstract]
		[Export("imageFetchDownload:didReceiveURLResponse:")]
		void DidReceiveUrlResponse(ITIPImageFetchDownload download, NSHttpUrlResponse response);

		// @required -(void)imageFetchDownload:(id<TIPImageFetchDownload> _Nonnull)download didReceiveData:(NSData * _Nonnull)data;
		[Abstract]
		[Export("imageFetchDownload:didReceiveData:")]
		void DidReceiveData(ITIPImageFetchDownload download, NSData data);

		// @required -(void)imageFetchDownload:(id<TIPImageFetchDownload> _Nonnull)download didCompleteWithError:(NSError * _Nullable)error;
		[Abstract]
		[Export("imageFetchDownload:didCompleteWithError:")]
		void DidComplete(ITIPImageFetchDownload download, [NullAllowed] NSError error);
	}

	interface ITIPImageFetchDownloadContext { }

	// @protocol TIPImageFetchDownloadContext <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchDownloadContext")]
	interface TIPImageFetchDownloadContext
	{
		// @required @property (readonly, copy, nonatomic) NSURLRequest * _Nonnull originalRequest;
		[Abstract]
		[Export("originalRequest", ArgumentSemantic.Copy)]
		NSUrlRequest OriginalRequest { get; }

		// @required @property (readonly, copy, nonatomic) NSURLRequest * _Nonnull hydratedRequest;
		[Abstract]
		[Export("hydratedRequest", ArgumentSemantic.Copy)]
		NSUrlRequest HydratedRequest { get; }

		// @required @property (readonly, nonatomic) id<TIPImageFetchDownloadClient> _Nonnull client;
		[Abstract]
		[Export("client")]
		ITIPImageFetchDownloadClient Client { get; }

		// @required @property (readonly, nonatomic) dispatch_queue_t _Nonnull downloadQueue;
		[Abstract]
		[Export("downloadQueue")]
		DispatchQueue DownloadQueue { get; }
	}

	interface ITIPImageFetchDownloadProvider { }

	// @protocol TIPImageFetchDownloadProvider <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchDownloadProvider")]
	interface TIPImageFetchDownloadProvider
	{
		// @required -(id<TIPImageFetchDownload> _Nonnull)imageFetchDownloadWithContext:(id<TIPImageFetchDownloadContext> _Nonnull)context;
		[Abstract]
		[Export("imageFetchDownloadWithContext:")]
		ITIPImageFetchDownload CreateImageFetchDownload(ITIPImageFetchDownloadContext context);
	}

	interface ITIPImageFetchDownloadProviderWithStubbingSupport { }

	// @protocol TIPImageFetchDownloadProviderWithStubbingSupport <TIPImageFetchDownloadProvider>
	[Protocol(Name = "TIPImageFetchDownloadProviderWithStubbingSupport"), Model]
	interface TIPImageFetchDownloadProviderWithStubbingSupport : TIPImageFetchDownloadProvider
	{
		// @required @property (readwrite, nonatomic) BOOL downloadStubbingEnabled;
		[Abstract]
		[Export("downloadStubbingEnabled")]
		bool DownloadStubbingEnabled { get; set; }

		// @required -(void)addDownloadStubForRequestURL:(NSURL * _Nonnull)requestURL responseData:(NSData * _Nullable)responseData responseMIMEType:(NSString * _Nullable)MIMEType shouldSupportResuming:(BOOL)shouldSupportResume suggestedBitrate:(uint64_t)suggestedBitrate;
		[Abstract]
		[Export("addDownloadStubForRequestURL:responseData:responseMIMEType:shouldSupportResuming:suggestedBitrate:")]
		void AddDownloadStub(NSUrl requestUrl, [NullAllowed] NSData responseData, [NullAllowed] string MIMEType, bool shouldSupportResume, ulong suggestedBitrate);

		// @required -(void)removeDownloadStubForRequestURL:(NSURL * _Nonnull)requestURL;
		[Abstract]
		[Export("removeDownloadStubForRequestURL:")]
		void RemoveDownloadStub(NSUrl requestUrl);

		// @required -(void)removeAllDownloadStubs;
		[Abstract]
		[Export("removeAllDownloadStubs")]
		void RemoveAllDownloadStubs();
	}

	// @interface TIPStubbingSupport (NSHTTPURLResponse)
	[Category]
	[BaseType(typeof(NSHttpUrlResponse))]
	interface TIPNSHttpUrlResponse
	{
		// +(instancetype _Nonnull)tip_responseWithRequestURL:(NSURL * _Nonnull)requestURL dataLength:(NSUInteger)dataLength responseMIMEType:(NSString * _Nullable)MIMEType;
		[Static]
		[Internal]
		[Export("tip_responseWithRequestURL:dataLength:responseMIMEType:")]
		NSHttpUrlResponse ResponseWithRequest(NSUrl requestUrl, nuint dataLength, [NullAllowed] string MIMEType);
	}

	// @interface TIPImageFetchOperation : NSOperation
	[BaseType(typeof(NSOperation), Name = "TIPImageFetchOperation")]
	[DisableDefaultCtor]
	interface TIPImageFetchOperation
	{
		// @property (readonly, nonatomic) TIPImageFetchOperationState state;
		[Export("state")]
		TIPImageFetchOperationState State { get; }

		// @property (readonly, nonatomic) id<TIPImageFetchRequest> _Nonnull request;
		[Export("request")]
		ITIPImageFetchRequest Request { get; }

		// @property (readonly, atomic, weak) id<TIPImageFetchDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		ITIPImageFetchDelegate Delegate { get; }

		// @property (readonly, nonatomic) TIPImagePipeline * _Nonnull imagePipeline;
		[Export("imagePipeline")]
		TIPImagePipeline ImagePipeline { get; }

		// @property (readonly, nonatomic) id<TIPImageFetchResult> _Nullable previewResult;
		[NullAllowed, Export("previewResult")]
		ITIPImageFetchResult PreviewResult { get; }

		// @property (readonly, nonatomic) id<TIPImageFetchResult> _Nullable progressiveResult;
		[NullAllowed, Export("progressiveResult")]
		ITIPImageFetchResult ProgressiveResult { get; }

		// @property (readonly, nonatomic) id<TIPImageFetchResult> _Nullable finalResult;
		[NullAllowed, Export("finalResult")]
		ITIPImageFetchResult FinalResult { get; }

		// @property (readonly, copy, nonatomic) NSString * _Nullable networkLoadImageType;
		[NullAllowed, Export("networkLoadImageType")]
		string NetworkLoadImageType { get; }

		// @property (readonly, nonatomic) CGSize networkImageOriginalDimensions;
		[Export("networkImageOriginalDimensions")]
		CGSize NetworkImageOriginalDimensions { get; }

		// @property (readonly, nonatomic) NSUInteger progressiveFrameCount;
		[Export("progressiveFrameCount")]
		nuint ProgressiveFrameCount { get; }

		// @property (readonly, nonatomic) float progress;
		[Export("progress")]
		float Progress { get; }

		// @property (readonly, nonatomic) NSError * _Nullable error;
		[NullAllowed, Export("error")]
		NSError Error { get; }

		// @property (readonly, nonatomic) TIPImageFetchMetrics * _Nullable metrics;
		[NullAllowed, Export("metrics")]
		TIPImageFetchMetrics Metrics { get; }

		// @property (nonatomic) NSOperationQueuePriority priority;
		[Export("priority", ArgumentSemantic.Assign)]
		NSOperationQueuePriority Priority { get; set; }

		// @property (nonatomic) id _Nullable context;
		[NullAllowed, Export("context", ArgumentSemantic.Assign)]
		NSObject Context { get; set; }

		// -(void)waitUntilFinished;
		[Export("waitUntilFinished")]
		void WaitUntilFinished();

		// -(void)waitUntilFinishedWithoutBlockingRunLoop;
		[Export("waitUntilFinishedWithoutBlockingRunLoop")]
		void WaitUntilFinishedWithoutBlockingRunLoop();

		// -(void)discardDelegate;
		[Export("discardDelegate")]
		void DiscardDelegate();

		// -(void)cancel;
		[Export("cancel")]
		void Cancel();

		// -(void)cancelAndDiscardDelegate;
		[Export("cancelAndDiscardDelegate")]
		void CancelAndDiscardDelegate();
	}

	interface ITIPImageFetchOperationUnderlyingContext { }

	// @protocol TIPImageFetchOperationUnderlyingContext <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchOperationUnderlyingContext")]
	interface TIPImageFetchOperationUnderlyingContext
	{
		// @required -(TIPImageFetchOperation * _Nullable)associatedImageFetchOperation;
		[Abstract]
		[NullAllowed, Export("associatedImageFetchOperation")]
		TIPImageFetchOperation AssociatedImageFetchOperation { get; }
	}

	interface ITIPImageFetchResult { }

	// @protocol TIPImageFetchResult <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchResult")]
	interface TIPImageFetchResult
	{
		// @required @property (readonly, nonatomic) TIPImageContainer * _Nonnull imageContainer;
		[Abstract]
		[Export("imageContainer")]
		TIPImageContainer ImageContainer { get; }

		// @required @property (readonly, nonatomic) TIPImageLoadSource imageSource;
		[Abstract]
		[Export("imageSource")]
		TIPImageLoadSource ImageSource { get; }

		// @required @property (readonly, nonatomic) NSURL * _Nonnull imageURL;
		[Abstract]
		[Export("imageURL")]
		NSUrl ImageUrl { get; }

		// @required @property (readonly, nonatomic) CGSize imageOriginalDimensions;
		[Abstract]
		[Export("imageOriginalDimensions")]
		CGSize ImageOriginalDimensions { get; }

		// @required @property (readonly, nonatomic) BOOL imageIsTreatedAsPlaceholder;
		[Abstract]
		[Export("imageIsTreatedAsPlaceholder")]
		bool ImageIsTreatedAsPlaceholder { get; }

		// @required @property (readonly, copy, nonatomic) NSString * _Nonnull imageIdentifier;
		[Abstract]
		[Export("imageIdentifier")]
		string ImageIdentifier { get; }
	}

	// typedef void (^TIPImageFetchDidLoadPreviewCallback)(TIPImageFetchPreviewLoadedBehavior);
	delegate void TIPImageFetchDidLoadPreviewCallback(TIPImageFetchPreviewLoadedBehavior behavior);

	interface ITIPImageFetchDelegate { }

	// @protocol TIPImageFetchDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageFetchDelegate")]
	interface TIPImageFetchDelegate
	{
		// @optional -(void)tip_imageFetchOperationDidStart:(TIPImageFetchOperation * _Nonnull)op;
		[Export("tip_imageFetchOperationDidStart:")]
		void DidStart(TIPImageFetchOperation op);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op willAttemptToLoadFromSource:(TIPImageLoadSource)source;
		[Export("tip_imageFetchOperation:willAttemptToLoadFromSource:")]
		void WillAttemptToLoad(TIPImageFetchOperation op, TIPImageLoadSource source);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didLoadPreviewImage:(id<TIPImageFetchResult> _Nonnull)previewResult completion:(TIPImageFetchDidLoadPreviewCallback _Nonnull)completion;
		[Export("tip_imageFetchOperation:didLoadPreviewImage:completion:")]
		void DidLoadPreviewImage(TIPImageFetchOperation op, ITIPImageFetchResult previewResult, TIPImageFetchDidLoadPreviewCallback completion);

		// @optional -(BOOL)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op shouldLoadProgressivelyWithIdentifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)URL imageType:(NSString * _Nonnull)imageType originalDimensions:(CGSize)originalDimensions;
		[Export("tip_imageFetchOperation:shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
		bool ShouldLoadProgressively(TIPImageFetchOperation op, string identifier, NSUrl url, string imageType, CGSize originalDimensions);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didUpdateProgressiveImage:(id<TIPImageFetchResult> _Nonnull)progressiveResult progress:(float)progress;
		[Export("tip_imageFetchOperation:didUpdateProgressiveImage:progress:")]
		void DidUpdateProgressiveImage(TIPImageFetchOperation op, ITIPImageFetchResult progressiveResult, float progress);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didLoadFirstAnimatedImageFrame:(id<TIPImageFetchResult> _Nonnull)progressiveResult progress:(float)progress;
		[Export("tip_imageFetchOperation:didLoadFirstAnimatedImageFrame:progress:")]
		void DidLoadFirstAnimatedImageFrame(TIPImageFetchOperation op, ITIPImageFetchResult progressiveResult, float progress);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didUpdateProgress:(float)progress;
		[Export("tip_imageFetchOperation:didUpdateProgress:")]
		void DidUpdateProgress(TIPImageFetchOperation op, float progress);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didLoadFinalImage:(id<TIPImageFetchResult> _Nonnull)finalResult;
		[Export("tip_imageFetchOperation:didLoadFinalImage:")]
		void DidLoadFinalImage(TIPImageFetchOperation op, ITIPImageFetchResult finalResult);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didFailToLoadFinalImage:(NSError * _Nonnull)error;
		[Export("tip_imageFetchOperation:didFailToLoadFinalImage:")]
		void DidFailToLoadFinalImage(TIPImageFetchOperation op, NSError error);
	}

	// @interface TIPImageFetchMetrics : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImageFetchMetrics")]
	[DisableDefaultCtor]
	interface TIPImageFetchMetrics
	{
		// @property (readonly, nonatomic) NSTimeInterval totalDuration;
		[Export("totalDuration")]
		double TotalDuration { get; }

		// @property (readonly, nonatomic) NSTimeInterval firstImageLoadDuration;
		[Export("firstImageLoadDuration")]
		double FirstImageLoadDuration { get; }

		// @property (readonly, nonatomic) BOOL wasCancelled;
		[Export("wasCancelled")]
		bool WasCancelled { get; }

		// -(TIPImageFetchMetricInfo * _Nullable)metricInfoForSource:(TIPImageLoadSource)source;
		[Export("metricInfoForSource:")]
		[return: NullAllowed]
		TIPImageFetchMetricInfo MetricInfo(TIPImageLoadSource source);
	}

	// @interface TIPImageFetchMetricInfo : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImageFetchMetricInfo")]
	[DisableDefaultCtor]
	interface TIPImageFetchMetricInfo
	{
		// @property (readonly, nonatomic) TIPImageLoadSource source;
		[Export("source")]
		TIPImageLoadSource Source { get; }

		// @property (readonly, nonatomic) TIPImageFetchLoadResult result;
		[Export("result")]
		TIPImageFetchLoadResult Result { get; }

		// @property (readonly, nonatomic) BOOL wasCancelled;
		[Export("wasCancelled")]
		bool WasCancelled { get; }

		// @property (readonly, nonatomic) NSTimeInterval loadDuration;
		[Export("loadDuration")]
		double LoadDuration { get; }
	}

	// @interface NetworkSourceInfo (TIPImageFetchMetricInfo)
	[Category]
	[BaseType(typeof(TIPImageFetchMetricInfo))]
	interface TIPImageFetchMetricInfoNetworkSourceInfo
	{
		// @property (readonly, nonatomic) id _Nullable networkMetrics;
		[NullAllowed, Export("networkMetrics")]
		NSObject NetworkMetrics();

		// @property (readonly, nonatomic) NSURLRequest * _Nullable networkRequest;
		[NullAllowed, Export("networkRequest")]
		NSUrlRequest NetworkRequest();

		// @property (readonly, nonatomic) NSTimeInterval totalNetworkLoadDuration;
		[Export("totalNetworkLoadDuration")]
		double TotalNetworkLoadDuration();

		// @property (readonly, nonatomic) NSTimeInterval firstProgressiveFrameNetworkLoadDuration;
		[Export("firstProgressiveFrameNetworkLoadDuration")]
		double FirstProgressiveFrameNetworkLoadDuration();

		// @property (readonly, nonatomic) NSUInteger networkImageSizeInBytes;
		[Export("networkImageSizeInBytes")]
		nuint NetworkImageSizeInBytes();

		// @property (readonly, copy, nonatomic) NSString * _Nullable networkImageType;
		[NullAllowed, Export("networkImageType")]
		string NetworkImageType();

		// @property (readonly, nonatomic) CGSize networkImageDimensions;
		[Export("networkImageDimensions")]
		CGSize NetworkImageDimensions();

		// @property (readonly, nonatomic) float networkImagePixelsPerByte;
		[Export("networkImagePixelsPerByte")]
		float NetworkImagePixelsPerByte();
	}

	// @interface TIPFirstAndLastFrameProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPFirstAndLastFrameProgressiveLoadingPolicy")]
	interface TIPFirstAndLastFrameProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
		// @property (nonatomic) BOOL shouldRenderLowQualityFrame;
		[Export("shouldRenderLowQualityFrame")]
		bool ShouldRenderLowQualityFrame { get; set; }
	}

	// @interface TIPFullFrameProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPFullFrameProgressiveLoadingPolicy")]
	interface TIPFullFrameProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
		// @property (nonatomic) BOOL shouldRenderLowQualityFrame;
		[Export("shouldRenderLowQualityFrame")]
		bool ShouldRenderLowQualityFrame { get; set; }
	}

	// @interface TIPGreedyProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPGreedyProgressiveLoadingPolicy")]
	interface TIPGreedyProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
		// @property (nonatomic) float minimumProgress;
		[Export("minimumProgress")]
		float MinimumProgress { get; set; }
	}

	// @interface TIPFirstAndLastOpportunityProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPFirstAndLastOpportunityProgressiveLoadingPolicy")]
	interface TIPFirstAndLastOpportunityProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
		// @property (nonatomic) float minimumProgress;
		[Export("minimumProgress")]
		float MinimumProgress { get; set; }
	}

	// @interface TIPDisabledProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPDisabledProgressiveLoadingPolicy")]
	interface TIPDisabledProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
	}

	// @interface TIPWrapperProgressiveLoadingPolicy : NSObject <TIPImageFetchProgressiveLoadingPolicy>
	[BaseType(typeof(NSObject), Name = "TIPWrapperProgressiveLoadingPolicy")]
	interface TIPWrapperProgressiveLoadingPolicy : TIPImageFetchProgressiveLoadingPolicy
	{
		// @property (readonly, nonatomic, weak) id<TIPImageFetchProgressiveLoadingPolicy> _Nullable wrappedPolicy;
		[NullAllowed, Export("wrappedPolicy", ArgumentSemantic.Weak)]
		ITIPImageFetchProgressiveLoadingPolicy WrappedPolicy { get; }

		// -(instancetype _Nonnull)initWithProgressiveLoadingPolicy:(id<TIPImageFetchProgressiveLoadingPolicy> _Nullable)policy __attribute__((objc_designated_initializer));
		[Export("initWithProgressiveLoadingPolicy:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] ITIPImageFetchProgressiveLoadingPolicy policy);
	}

	// typedef void (^TIPImagePipelineFetchCompletionBlock)(id<TIPImageFetchResult> _Nullable, NSError * _Nullable);
	delegate void TIPImagePipelineFetchCompletionDelegate([NullAllowed] ITIPImageFetchResult finalResult, [NullAllowed] NSError error);

	// typedef void (^TIPImagePipelineStoreCompletionBlock)(BOOL, NSError * _Nullable);
	delegate void TIPImagePipelineStoreCompletionDelegate(bool succeeded, [NullAllowed] NSError error);

	// typedef void (^TIPImagePipelineCopyFileCompletionBlock)(NSString * _Nullable, NSError * _Nullable);
	delegate void TIPImagePipelineCopyFileCompletionDelegate([NullAllowed] string temporaryFilePath, [NullAllowed] NSError error);

	[Static]
	partial interface TIPImagePipelineNotifications
	{
		// extern NSString *const _Nonnull TIPImagePipelineDidStoreCachedImageNotification;
		[Field("TIPImagePipelineDidStoreCachedImageNotification", "__Internal")]
		NSString TIPImagePipelineDidStoreCachedImageNotification { get; }

		// extern NSString *const _Nonnull TIPImagePipelineDidStandUpImagePipelineNotification;
		[Field("TIPImagePipelineDidStandUpImagePipelineNotification", "__Internal")]
		NSString TIPImagePipelineDidStandUpImagePipelineNotification { get; }

		// extern NSString *const _Nonnull TIPImagePipelineDidTearDownImagePipelineNotification;
		[Field("TIPImagePipelineDidTearDownImagePipelineNotification", "__Internal")]
		NSString TIPImagePipelineDidTearDownImagePipelineNotification { get; }
	}

	[Static]
	partial interface TIPImagePipelineNotificationKeys
	{
		// extern NSString *const _Nonnull TIPImagePipelineImageIdentifierNotificationKey;
		[Field("TIPImagePipelineImageIdentifierNotificationKey", "__Internal")]
		NSString ImageIdentifier { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImageURLNotificationKey;
		[Field("TIPImagePipelineImageURLNotificationKey", "__Internal")]
		NSString ImageUrl { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImageDimensionsNotificationKey;
		[Field("TIPImagePipelineImageDimensionsNotificationKey", "__Internal")]
		NSString ImageDimensions { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImageContainerNotificationKey;
		[Field("TIPImagePipelineImageContainerNotificationKey", "__Internal")]
		NSString ImageContainer { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImageWasManuallyStoredNotificationKey;
		[Field("TIPImagePipelineImageWasManuallyStoredNotificationKey", "__Internal")]
		NSString ImageWasManuallyStored { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImagePipelineIdentifierNotificationKey;
		[Field("TIPImagePipelineImagePipelineIdentifierNotificationKey", "__Internal")]
		NSString ImagePipelineIdentifier { get; }

		// extern NSString *const _Nonnull TIPImagePipelineImageTreatAsPlaceholderNofiticationKey;
		[Field("TIPImagePipelineImageTreatAsPlaceholderNofiticationKey", "__Internal")]
		NSString ImageTreatAsPlaceholder { get; }
	}

	// @interface TIPImagePipeline : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImagePipeline")]
	[DisableDefaultCtor]
	interface TIPImagePipeline : INativeObject
	{
		// @property (copy, atomic) NSArray<id<TIPImageAdditionalCache>> * _Nullable additionalCaches;
		[NullAllowed, Export("additionalCaches", ArgumentSemantic.Copy)]
		ITIPImageAdditionalCache[] AdditionalCaches { get; set; }

		// @property (atomic) id<TIPImagePipelineObserver> _Nullable observer;
		[NullAllowed, Export("observer", ArgumentSemantic.Assign)]
		ITIPImagePipelineObserver Observer { get; set; }

		// @property (readonly, copy, nonatomic) NSString * _Nonnull identifier;
		[Export("identifier")]
		string Identifier { get; }

		// -(instancetype _Nullable)initWithIdentifier:(NSString * _Nonnull)identifier __attribute__((objc_designated_initializer));
		[Export("initWithIdentifier:")]
		[DesignatedInitializer]
		IntPtr Constructor(string identifier);

		// -(TIPImageFetchOperation * _Nonnull)operationWithRequest:(id<TIPImageFetchRequest> _Nonnull)request context:(id _Nullable)context delegate:(id<TIPImageFetchDelegate> _Nullable)delegate __attribute__((objc_requires_super));
		[Export("operationWithRequest:context:delegate:")]
		[Advice("You must call the base method when overriding.")]
		TIPImageFetchOperation CreateFetchOperation(ITIPImageFetchRequest request, [NullAllowed] NSObject context, [NullAllowed] ITIPImageFetchDelegate @delegate);

		// -(TIPImageFetchOperation * _Nonnull)operationWithRequest:(id<TIPImageFetchRequest> _Nonnull)request context:(id _Nullable)context completion:(TIPImagePipelineFetchCompletionBlock _Nullable)completion;
		[Export("operationWithRequest:context:completion:")]
		TIPImageFetchOperation CreateFetchOperation(ITIPImageFetchRequest request, [NullAllowed] NSObject context, [NullAllowed] TIPImagePipelineFetchCompletionDelegate completion);

		// -(void)fetchImageWithOperation:(TIPImageFetchOperation * _Nonnull)op;
		[Export("fetchImageWithOperation:")]
		void FetchImage(TIPImageFetchOperation op);

		// -(NSObject<TIPDependencyOperation> * _Nonnull)storeImageWithRequest:(id<TIPImageStoreRequest> _Nonnull)request completion:(TIPImagePipelineStoreCompletionBlock _Nullable)completion;
		[Export("storeImageWithRequest:completion:")]
		ITIPDependencyOperation StoreImage(ITIPImageStoreRequest request, [NullAllowed] TIPImagePipelineStoreCompletionDelegate completion);

		// -(void)clearImageWithIdentifier:(NSString * _Nonnull)imageIdentifier;
		[Export("clearImageWithIdentifier:")]
		void ClearImage(string imageIdentifier);

		// -(void)clearMemoryCaches;
		[Export("clearMemoryCaches")]
		void ClearMemoryCaches();

		// -(void)clearDiskCache;
		[Export("clearDiskCache")]
		void ClearDiskCache();

		// -(void)copyDiskCacheFileWithIdentifier:(NSString * _Nonnull)imageIdentifier completion:(TIPImagePipelineCopyFileCompletionBlock _Nullable)completion;
		[Export("copyDiskCacheFileWithIdentifier:completion:")]
		void CopyDiskCacheFile(string imageIdentifier, [NullAllowed] TIPImagePipelineCopyFileCompletionDelegate completion);

		// +(void)getKnownImagePipelineIdentifiers:(void (^ _Nonnull)(NSSet<NSString *> * _Nonnull))callback;
		[Static]
		[Export("getKnownImagePipelineIdentifiers:")]
		void GetKnownImagePipelineIdentifiers(Action<NSSet<NSString>> callback);
	}

	// typedef void (^TIPImagePipelineInspectionCallback)(TIPImagePipelineInspectionResult * _Nullable);
	delegate void TIPImagePipelineInspectionCallback([NullAllowed] TIPImagePipelineInspectionResult result);

	// @interface Inspect (TIPImagePipeline)
	[Category]
	[BaseType(typeof(TIPImagePipeline))]
	interface TIPImagePipelineInspect
	{
		// -(void)inspect:(TIPImagePipelineInspectionCallback _Nonnull)callback;
		[Export("inspect:")]
		void Inspect(TIPImagePipelineInspectionCallback callback);
	}

	interface ITIPImagePipelineObserver { }

	// @protocol TIPImagePipelineObserver <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImagePipelineObserver")]
	interface TIPImagePipelineObserver
	{
		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didStartDownloadingImageAtURL:(NSURL * _Nonnull)URL;
		[Export("tip_imageFetchOperation:didStartDownloadingImageAtURL:")]
		void DidStartDownloadingImage(TIPImageFetchOperation op, NSUrl url);

		// @optional -(void)tip_imageFetchOperation:(TIPImageFetchOperation * _Nonnull)op didFinishDownloadingImageAtURL:(NSURL * _Nonnull)URL imageType:(NSString * _Nonnull)type sizeInBytes:(NSUInteger)byteSize dimensions:(CGSize)dimensions wasResumed:(BOOL)wasResumed;
		[Export("tip_imageFetchOperation:didFinishDownloadingImageAtURL:imageType:sizeInBytes:dimensions:wasResumed:")]
		void DidFinishDownloadingImage(TIPImageFetchOperation op, NSUrl url, string type, nuint byteSize, CGSize dimensions, bool wasResumed);
	}

	// typedef void (^TIPImageAdditionalCacheFetchCompletion)(UIImage * _Nullable);
	delegate void TIPImageAdditionalCacheFetchCompletion([NullAllowed] UIImage image);

	interface ITIPImageAdditionalCache { }

	// @protocol TIPImageAdditionalCache <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageAdditionalCache")]
	interface TIPImageAdditionalCache
	{
		// @optional -(void)tip_retrieveImageForURL:(NSURL * _Nonnull)URL completion:(TIPImageAdditionalCacheFetchCompletion _Nonnull)completion;
		[Export("tip_retrieveImageForURL:completion:")]
		void RetrieveImageForUrl(NSUrl url, TIPImageAdditionalCacheFetchCompletion completion);
	}

	// @interface TIPImagePipelineInspectionResult : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImagePipelineInspectionResult")]
	[DisableDefaultCtor]
	interface TIPImagePipelineInspectionResult : INativeObject
	{
		// @property (readonly, nonatomic) TIPImagePipeline * _Nonnull imagePipeline;
		[Export("imagePipeline")]
		TIPImagePipeline ImagePipeline { get; }

		// @property (readonly, nonatomic) NSArray<id<TIPImagePipelineInspectionResultEntry>> * _Nonnull completeRenderedEntries;
		[Export("completeRenderedEntries")]
		ITIPImagePipelineInspectionResultEntry[] CompleteRenderedEntries { get; }

		// @property (readonly, nonatomic) NSArray<id<TIPImagePipelineInspectionResultEntry>> * _Nonnull completeMemoryEntries;
		[Export("completeMemoryEntries")]
		ITIPImagePipelineInspectionResultEntry[] CompleteMemoryEntries { get; }

		// @property (readonly, nonatomic) NSArray<id<TIPImagePipelineInspectionResultEntry>> * _Nonnull completeDiskEntries;
		[Export("completeDiskEntries")]
		ITIPImagePipelineInspectionResultEntry[] CompleteDiskEntries { get; }

		// @property (readonly, nonatomic) NSArray<id<TIPImagePipelineInspectionResultEntry>> * _Nonnull partialMemoryEntries;
		[Export("partialMemoryEntries")]
		ITIPImagePipelineInspectionResultEntry[] PartialMemoryEntries { get; }

		// @property (readonly, nonatomic) NSArray<id<TIPImagePipelineInspectionResultEntry>> * _Nonnull partialDiskEntries;
		[Export("partialDiskEntries")]
		ITIPImagePipelineInspectionResultEntry[] PartialDiskEntries { get; }

		// @property (readonly, nonatomic) unsigned long long inMemoryBytesUsed;
		[Export("inMemoryBytesUsed")]
		ulong InMemoryBytesUsed { get; }

		// @property (readonly, nonatomic) unsigned long long onDiskBytesUsed;
		[Export("onDiskBytesUsed")]
		ulong OnDiskBytesUsed { get; }
	}

	interface ITIPImagePipelineInspectionResultEntry { }

	// @protocol TIPImagePipelineInspectionResultEntry <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImagePipelineInspectionResultEntry")]
	interface TIPImagePipelineInspectionResultEntry
	{
		// @required @property (readonly, copy, nonatomic) NSString * _Nullable identifier;
		[Abstract]
		[NullAllowed, Export("identifier")]
		string Identifier { get; }

		// @required @property (readonly, nonatomic) NSURL * _Nullable URL;
		[Abstract]
		[NullAllowed, Export("URL")]
		NSUrl Url { get; }

		// @required @property (readonly, nonatomic) CGSize dimensions;
		[Abstract]
		[Export("dimensions")]
		CGSize Dimensions { get; }

		// @required @property (readonly, nonatomic) unsigned long long bytesUsed;
		[Abstract]
		[Export("bytesUsed")]
		ulong BytesUsed { get; }

		// @required @property (readonly, nonatomic) float progress;
		[Abstract]
		[Export("progress")]
		float Progress { get; }

		// @required @property (readonly, nonatomic) UIImage * _Nullable image;
		[Abstract]
		[NullAllowed, Export("image")]
		UIImage Image { get; }
	}

	// @interface TIPImagePipelineInspectionResultEntry : NSObject <TIPImagePipelineInspectionResultEntry>
	[BaseType(typeof(NSObject), Name = "TIPImagePipelineInspectionResultEntry")]
	interface TIPImagePipelineInspectionResultEntryBase : TIPImagePipelineInspectionResultEntry
	{
		// @property (copy, nonatomic) NSString * _Nullable identifier;
		[NullAllowed, Export("identifier")]
		string Identifier { get; set; }

		// @property (nonatomic) NSURL * _Nullable URL;
		[NullAllowed, Export("URL", ArgumentSemantic.Assign)]
		NSUrl Url { get; set; }

		// @property (nonatomic) CGSize dimensions;
		[Export("dimensions", ArgumentSemantic.Assign)]
		CGSize Dimensions { get; set; }

		// @property (nonatomic) unsigned long long bytesUsed;
		[Export("bytesUsed")]
		ulong BytesUsed { get; set; }

		// @property (nonatomic) float progress;
		[Export("progress")]
		float Progress { get; set; }

		// @property (nonatomic) UIImage * _Nullable image;
		[NullAllowed, Export("image", ArgumentSemantic.Assign)]
		UIImage Image { get; set; }
	}

	interface ITIPImageStoreRequest { }

	// @protocol TIPImageStoreRequest <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageStoreRequest")]
	interface TIPImageStoreRequest
	{
		// @required @property (readonly, nonatomic) NSURL * _Nonnull imageURL;
		[Abstract]
		[Export("imageURL")]
		NSUrl ImageUrl { get; }

		// @optional @property (readonly, copy, nonatomic) NSString * _Nullable imageIdentifier;
		[NullAllowed, Export("imageIdentifier")]
		string ImageIdentifier { get; }

		// @optional @property (readonly, nonatomic) NSTimeInterval timeToLive;
		[Export("timeToLive")]
		double TimeToLive { get; }

		// @optional @property (readonly, nonatomic) TIPImageStoreOptions options;
		[Export("options")]
		TIPImageStoreOptions Options { get; }

		// @optional @property (readonly, nonatomic) UIImage * _Nullable image;
		[NullAllowed, Export("image")]
		UIImage Image { get; }

		// @optional @property (readonly, nonatomic) NSData * _Nullable imageData;
		[NullAllowed, Export("imageData")]
		NSData ImageData { get; }

		// @optional @property (readonly, copy, nonatomic) NSString * _Nullable imageFilePath;
		[NullAllowed, Export("imageFilePath")]
		string ImageFilePath { get; }

		// @optional @property (readonly, copy, nonatomic) NSString * _Nullable imageType;
		[NullAllowed, Export("imageType")]
		string ImageType { get; }

		// @optional @property (readonly, nonatomic) CGSize imageDimensions;
		[Export("imageDimensions")]
		CGSize ImageDimensions { get; }

		// @optional @property (readonly, nonatomic) NSUInteger animationLoopCount;
		[Export("animationLoopCount")]
		nuint AnimationLoopCount { get; }

		// @optional @property (readonly, copy, nonatomic) NSArray<NSNumber *> * _Nullable animationFrameDurations;
		[NullAllowed, Export("animationFrameDurations", ArgumentSemantic.Copy)]
		NSNumber[] AnimationFrameDurations { get; }

		// @optional @property (readonly, nonatomic) id<TIPImageStoreRequestHydrater> _Nullable hydrater;
		[NullAllowed, Export("hydrater")]
		ITIPImageStoreRequestHydrater Hydrater { get; }
	}

	interface ITIPImageObjectStoreRequest { }

	// @protocol TIPImageObjectStoreRequest <TIPImageStoreRequest>
	[Protocol(Name = "TIPImageObjectStoreRequest"), Model]
	interface TIPImageObjectStoreRequest : TIPImageStoreRequest
	{
		// @required @property (readonly, nonatomic) UIImage * _Nullable image;
		[Abstract]
		[NullAllowed, Export("image")]
		UIImage Image { get; }

		// @required @property (readonly, copy, nonatomic) NSString * _Nullable imageType;
		[Abstract]
		[NullAllowed, Export("imageType")]
		string ImageType { get; }

		// @optional @property (readonly, nonatomic) NSUInteger animationLoopCount;
		[Export("animationLoopCount")]
		nuint AnimationLoopCount { get; }

		// @optional @property (readonly, copy, nonatomic) NSArray<NSNumber *> * _Nullable animationFrameDurations;
		[NullAllowed, Export("animationFrameDurations", ArgumentSemantic.Copy)]
		NSNumber[] AnimationFrameDurations { get; }
	}

	interface ITIPImageDataStoreRequest { }

	// @protocol TIPImageDataStoreRequest <TIPImageStoreRequest>
	[Protocol(Name = "TIPImageDataStoreRequest"), Model]
	interface TIPImageDataStoreRequest : TIPImageStoreRequest
	{
		// @required @property (readonly, nonatomic) NSData * _Nullable imageData;
		[Abstract]
		[NullAllowed, Export("imageData")]
		NSData ImageData { get; }

		// @required @property (readonly, nonatomic) CGSize imageDimensions;
		[Abstract]
		[Export("imageDimensions")]
		CGSize ImageDimensions { get; }
	}

	interface ITIPImageFileStoreRequest { }

	// @protocol TIPImageFileStoreRequest <TIPImageStoreRequest>
	[Protocol(Name = "TIPImageFileStoreRequest"), Model]
	interface TIPImageFileStoreRequest : TIPImageStoreRequest
	{
		// @required @property (readonly, copy, nonatomic) NSString * _Nullable imageFilePath;
		[Abstract]
		[NullAllowed, Export("imageFilePath")]
		string ImageFilePath { get; }

		// @required @property (readonly, nonatomic) CGSize imageDimensions;
		[Abstract]
		[Export("imageDimensions")]
		CGSize ImageDimensions { get; }
	}

	// typedef void (^TIPImageStoreHydraterCompletionBlock)(id<TIPImageStoreRequest> _Nullable, NSError * _Nullable);
	delegate void TIPImageStoreHydraterCompletionDelegate([NullAllowed] ITIPImageStoreRequest request, [NullAllowed] NSError error);

	interface ITIPImageStoreRequestHydrater { }

	// @protocol TIPImageStoreRequestHydrater <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageStoreRequestHydrater")]
	interface TIPImageStoreRequestHydrater
	{
		// @required -(void)tip_hydrateImageStoreRequest:(id<TIPImageStoreRequest> _Nonnull)request imagePipeline:(TIPImagePipeline * _Nonnull)pipeline completion:(TIPImageStoreHydraterCompletionBlock _Nonnull)completion;
		[Abstract]
		[Export("tip_hydrateImageStoreRequest:imagePipeline:completion:")]
		void HydrateImageStoreRequest(ITIPImageStoreRequest request, TIPImagePipeline pipeline, TIPImageStoreHydraterCompletionDelegate completion);
	}

	// @interface TIPImageView : UIImageView
	[BaseType(typeof(UIImageView), Name = "TIPImageView")]
	interface TIPImageView
	{
		// @property (nonatomic) TIPImageViewFetchHelper * _Nullable fetchHelper;
		[NullAllowed, Export("fetchHelper", ArgumentSemantic.Assign)]
		TIPImageViewFetchHelper FetchHelper { get; set; }

		// -(instancetype _Nonnull)initWithFetchHelper:(TIPImageViewFetchHelper * _Nullable)fetchHelper;
		[Export("initWithFetchHelper:")]
		IntPtr Constructor([NullAllowed] TIPImageViewFetchHelper fetchHelper);
	}

	// @interface TIPImageViewFetchHelper : NSObject
	[BaseType(typeof(NSObject), Name = "TIPImageViewFetchHelper")]
	interface TIPImageViewFetchHelper
	{
		// @property (nonatomic) TIPImageViewDisappearanceBehavior fetchDisappearanceBehavior;
		[Export("fetchDisappearanceBehavior", ArgumentSemantic.Assign)]
		TIPImageViewDisappearanceBehavior FetchDisappearanceBehavior { get; set; }

		// @property (nonatomic, weak) UIImageView * _Nullable fetchImageView;
		[NullAllowed, Export("fetchImageView", ArgumentSemantic.Weak)]
		UIImageView FetchImageView { get; set; }

		// @property (readonly, nonatomic) id<TIPImageFetchRequest> _Nullable fetchRequest;
		[NullAllowed, Export("fetchRequest")]
		ITIPImageFetchRequest FetchRequest { get; }

		// @property (readonly, getter = isLoading, nonatomic) BOOL loading;
		[Export("loading")]
		bool Loading { [Bind("isLoading")] get; }

		// @property (readonly, nonatomic) float fetchProgress;
		[Export("fetchProgress")]
		float FetchProgress { get; }

		// @property (readonly, nonatomic) NSError * _Nullable fetchError;
		[NullAllowed, Export("fetchError")]
		NSError FetchError { get; }

		// @property (readonly, nonatomic) TIPImageFetchMetrics * _Nullable fetchMetrics;
		[NullAllowed, Export("fetchMetrics")]
		TIPImageFetchMetrics FetchMetrics { get; }

		// @property (readonly, nonatomic) TIPImageLoadSource fetchSource;
		[Export("fetchSource")]
		TIPImageLoadSource FetchSource { get; }

		// @property (readonly, nonatomic) BOOL fetchedImageTreatedAsPlaceholder;
		[Export("fetchedImageTreatedAsPlaceholder")]
		bool FetchedImageTreatedAsPlaceholder { get; }

		// @property (readonly, nonatomic) BOOL fetchedImageIsPreview;
		[Export("fetchedImageIsPreview")]
		bool FetchedImageIsPreview { get; }

		// @property (readonly, nonatomic) BOOL fetchedImageIsScaledPreviewAsFinal;
		[Export("fetchedImageIsScaledPreviewAsFinal")]
		bool FetchedImageIsScaledPreviewAsFinal { get; }

		// @property (readonly, nonatomic) BOOL fetchedImageIsProgressiveFrame;
		[Export("fetchedImageIsProgressiveFrame")]
		bool FetchedImageIsProgressiveFrame { get; }

		// @property (readonly, nonatomic) BOOL fetchedImageIsFullLoad;
		[Export("fetchedImageIsFullLoad")]
		bool FetchedImageIsFullLoad { get; }

		// @property (readonly, nonatomic) BOOL didLoadAny;
		[Export("didLoadAny")]
		bool DidLoadAny { get; }

		// @property (readonly, nonatomic) NSURL * _Nullable fetchedImageURL;
		[NullAllowed, Export("fetchedImageURL")]
		NSUrl FetchedImageUrl { get; }

		// @property (nonatomic, weak) id<TIPImageViewFetchHelperDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		ITIPImageViewFetchHelperDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<TIPImageViewFetchHelperDataSource> _Nullable dataSource;
		[NullAllowed, Export("dataSource", ArgumentSemantic.Weak)]
		ITIPImageViewFetchHelperDataSource DataSource { get; set; }

		// -(instancetype _Nonnull)initWithDelegate:(id<TIPImageViewFetchHelperDelegate> _Nullable)delegate dataSource:(id<TIPImageViewFetchHelperDataSource> _Nullable)dataSource __attribute__((objc_designated_initializer));
		[Export("initWithDelegate:dataSource:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] ITIPImageViewFetchHelperDelegate @delegate, [NullAllowed] ITIPImageViewFetchHelperDataSource dataSource);

		// -(void)reload;
		[Export("reload")]
		void Reload();

		// -(void)cancelFetchRequest;
		[Export("cancelFetchRequest")]
		void CancelFetchRequest();

		// -(void)clearImage;
		[Export("clearImage")]
		void ClearImage();

		// -(void)setImageAsIfLoaded:(UIImage * _Nonnull)image;
		[Export("setImageAsIfLoaded:")]
		void SetImageAsIfLoaded(UIImage image);

		// -(void)markAsIfLoaded;
		[Export("markAsIfLoaded")]
		void MarkAsIfLoaded();

		// -(void)triggerViewWillDisappear __attribute__((objc_requires_super));
		[Export("triggerViewWillDisappear")]
		[Advice("You must call the base method when overriding.")]
		void TriggerViewWillDisappear();

		// -(void)triggerViewDidDisappear __attribute__((objc_requires_super));
		[Export("triggerViewDidDisappear")]
		[Advice("You must call the base method when overriding.")]
		void TriggerViewDidDisappear();

		// -(void)triggerViewWillAppear __attribute__((objc_requires_super));
		[Export("triggerViewWillAppear")]
		[Advice("You must call the base method when overriding.")]
		void TriggerViewWillAppear();

		// -(void)triggerViewDidAppear __attribute__((objc_requires_super));
		[Export("triggerViewDidAppear")]
		[Advice("You must call the base method when overriding.")]
		void TriggerViewDidAppear();

		// -(void)triggerViewLayingOutSubviews __attribute__((objc_requires_super));
		[Export("triggerViewLayingOutSubviews")]
		[Advice("You must call the base method when overriding.")]
		void TriggerViewLayingOutSubviews();

		// -(void)setViewHidden:(BOOL)hidden;
		[Export("setViewHidden:")]
		void SetViewHidden(bool hidden);

		// -(void)viewWillMoveToWindow:(UIWindow * _Nullable)newWindow;
		[Export("viewWillMoveToWindow:")]
		void ViewWillMoveToWindow([NullAllowed] UIWindow newWindow);

		// -(void)viewDidMoveToWindow;
		[Export("viewDidMoveToWindow")]
		void ViewDidMoveToWindow();

		// +(void)transitionView:(UIImageView * _Nonnull)imageView fromFetchHelper:(TIPImageViewFetchHelper * _Nullable)fromHelper toFetchHelper:(TIPImageViewFetchHelper * _Nullable)toHelper;
		[Static]
		[Export("transitionView:fromFetchHelper:toFetchHelper:")]
		void TransitionView(UIImageView imageView, [NullAllowed] TIPImageViewFetchHelper fromHelper, [NullAllowed] TIPImageViewFetchHelper toHelper);

		// -(BOOL)shouldUpdateImageWithPreviewImageResult:(id<TIPImageFetchResult> _Nonnull)previewImageResult;
		[Export("shouldUpdateImageWithPreviewImageResult:")]
		bool ShouldUpdateImage(ITIPImageFetchResult previewImageResult);

		// -(BOOL)shouldContinueLoadingAfterFetchingPreviewImageResult:(id<TIPImageFetchResult> _Nonnull)previewImageResult;
		[Export("shouldContinueLoadingAfterFetchingPreviewImageResult:")]
		bool ShouldContinueLoadingAfterFetching(ITIPImageFetchResult previewImageResult);

		// -(BOOL)shouldLoadProgressivelyWithIdentifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)URL imageType:(NSString * _Nonnull)imageType originalDimensions:(CGSize)originalDimensions;
		[Export("shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
		bool ShouldLoadProgressively(string identifier, NSUrl url, string imageType, CGSize originalDimensions);

		// -(BOOL)shouldReloadAfterDifferentFetchCompletedWithImage:(UIImage * _Nonnull)image dimensions:(CGSize)dimensions identifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)URL treatedAsPlaceholder:(BOOL)placeholder manuallyStored:(BOOL)manuallyStored;
		[Export("shouldReloadAfterDifferentFetchCompletedWithImage:dimensions:identifier:URL:treatedAsPlaceholder:manuallyStored:")]
		bool ShouldReloadAfterDifferentFetchCompleted(UIImage image, CGSize dimensions, string identifier, NSUrl url, bool placeholder, bool manuallyStored);

		// -(void)didStartLoading;
		[Export("didStartLoading")]
		void DidStartLoading();

		// -(void)didUpdateProgress:(float)progress;
		[Export("didUpdateProgress:")]
		void DidUpdateProgress(float progress);

		// -(void)didUpdateDisplayedImage:(UIImage * _Nonnull)image fromSourceDimensions:(CGSize)size isFinal:(BOOL)isFinal;
		[Export("didUpdateDisplayedImage:fromSourceDimensions:isFinal:")]
		void DidUpdateDisplayedImage(UIImage image, CGSize size, bool isFinal);

		// -(void)didLoadFinalImageFromSource:(TIPImageLoadSource)source;
		[Export("didLoadFinalImageFromSource:")]
		void DidLoadFinalImage(TIPImageLoadSource source);

		// -(void)didFailToLoadFinalImage:(NSError * _Nonnull)error;
		[Export("didFailToLoadFinalImage:")]
		void DidFailToLoadFinalImage(NSError error);

		// -(void)didReset;
		[Export("didReset")]
		void DidReset();

		//// +(void)setDebugInfoVisible:(BOOL)debugInfoVisible;
		//[Static]
		//[Export("setDebugInfoVisible:")]
		//void SetDebugInfoVisible(bool debugInfoVisible);
		//
		//// +(BOOL)isDebugInfoVisible;
		//[Static]
		//[Export("isDebugInfoVisible")]
		//bool IsDebugInfoVisible();
		//
		[Static]
		[Export("isDebugInfoVisible")]
		bool IsDebugInfoVisible { get; [Bind("setDebugInfoVisible:")] set; }
	}

	[Static]
	partial interface TIPImageViewNotifications
	{
		// extern NSString *const _Nonnull TIPImageViewDidUpdateDebugInfoVisibilityNotification;
		[Field("TIPImageViewDidUpdateDebugInfoVisibilityNotification", "__Internal")]
		NSString TIPImageViewDidUpdateDebugInfoVisibilityNotification { get; }

		// extern NSString *const _Nonnull TIPImageViewDidUpdateDebugInfoVisibilityNotificationKeyVisible;
		[Field("TIPImageViewDidUpdateDebugInfoVisibilityNotificationKeyVisible", "__Internal")]
		NSString TIPImageViewDidUpdateDebugInfoVisibilityNotificationKeyVisible { get; }
	}

	// @interface Debugging (TIPImageViewFetchHelper)
	[Category]
	[BaseType(typeof(TIPImageViewFetchHelper))]
	interface TIPImageViewFetchHelperDebugging
	{
		// @property (nonatomic) UIColor * _Nullable debugImageHighlightColor;
		[Export("debugImageHighlightColor", ArgumentSemantic.Assign)]
		[return: NullAllowed]
		UIColor GetDebugImageHighlightColor();

		// @property (nonatomic) UIColor * _Nullable debugImageHighlightColor;
		[Export("setDebugImageHighlightColor:", ArgumentSemantic.Assign)]
		void SetDebugImageHighlightColor([NullAllowed] UIColor value);

		// @property (nonatomic) UIColor * _Nullable debugInfoTextColor;
		[Export("debugInfoTextColor", ArgumentSemantic.Assign)]
		[return: NullAllowed]
		UIColor GetDebugInfoTextColor();

		// @property (nonatomic) UIColor * _Nullable debugInfoTextColor;
		[Export("setDebugInfoTextColor:", ArgumentSemantic.Assign)]
		void SetDebugInfoTextColor([NullAllowed] UIColor value);

		// -(NSMutableArray<NSString *> * _Nonnull)debugInfoStrings __attribute__((objc_requires_super));
		[Export("debugInfoStrings")]
		[Advice("You must call the base method when overriding.")]
		NSMutableArray<NSString> GetDebugInfoStrings();

		// -(void)setDebugInfoNeedsUpdate;
		[Export("setDebugInfoNeedsUpdate")]
		void SetDebugInfoNeedsUpdate();
	}

	interface ITIPImageViewFetchHelperDataSource { }

	// @protocol TIPImageViewFetchHelperDataSource <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageViewFetchHelperDataSource")]
	interface TIPImageViewFetchHelperDataSource
	{
		// @optional -(UIImage * _Nullable)tip_imageForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_imageForFetchHelper:")]
		[return: NullAllowed]
		UIImage GetImage(TIPImageViewFetchHelper helper);

		// @optional -(NSURL * _Nullable)tip_imageURLForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_imageURLForFetchHelper:")]
		[return: NullAllowed]
		NSUrl GetImageUrl(TIPImageViewFetchHelper helper);

		// @optional -(id<TIPImageFetchRequest> _Nullable)tip_imageFetchRequestForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_imageFetchRequestForFetchHelper:")]
		[return: NullAllowed]
		ITIPImageFetchRequest GetImageFetchRequest(TIPImageViewFetchHelper helper);

		// @optional -(TIPImagePipeline * _Nullable)tip_imagePipelineForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_imagePipelineForFetchHelper:")]
		[return: NullAllowed]
		TIPImagePipeline GetImagePipeline(TIPImageViewFetchHelper helper);

		// @optional -(BOOL)tip_shouldRefetchOnTargetSizingChangeForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_shouldRefetchOnTargetSizingChangeForFetchHelper:")]
		bool ShouldRefetchOnTargetSizingChange(TIPImageViewFetchHelper helper);

		// @optional -(NSOperationQueuePriority)tip_fetchOperationPriorityForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_fetchOperationPriorityForFetchHelper:")]
		NSOperationQueuePriority GetFetchOperationPriority(TIPImageViewFetchHelper helper);

		// @optional -(NSArray<NSString *> * _Nullable)tip_additionalDebugInfoStringsForFetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_additionalDebugInfoStringsForFetchHelper:")]
		[return: NullAllowed]
		string[] GetAdditionalDebugInfoStrings(TIPImageViewFetchHelper helper);
	}

	interface ITIPImageViewFetchHelperDelegate { }

	// @protocol TIPImageViewFetchHelperDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPImageViewFetchHelperDelegate")]
	interface TIPImageViewFetchHelperDelegate
	{
		// @optional -(BOOL)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper shouldUpdateImageWithPreviewImageResult:(id<TIPImageFetchResult> _Nonnull)previewImageResult;
		[Export("tip_fetchHelper:shouldUpdateImageWithPreviewImageResult:")]
		bool ShouldUpdateImage(TIPImageViewFetchHelper helper, ITIPImageFetchResult previewImageResult);

		// @optional -(BOOL)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper shouldContinueLoadingAfterFetchingPreviewImageResult:(id<TIPImageFetchResult> _Nonnull)previewImageResult;
		[Export("tip_fetchHelper:shouldContinueLoadingAfterFetchingPreviewImageResult:")]
		bool ShouldContinueLoadingAfterFetching(TIPImageViewFetchHelper helper, ITIPImageFetchResult previewImageResult);

		// @optional -(BOOL)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper shouldLoadProgressivelyWithIdentifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)URL imageType:(NSString * _Nonnull)imageType originalDimensions:(CGSize)originalDimensions;
		[Export("tip_fetchHelper:shouldLoadProgressivelyWithIdentifier:URL:imageType:originalDimensions:")]
		bool ShouldLoadProgressively(TIPImageViewFetchHelper helper, string identifier, NSUrl url, string imageType, CGSize originalDimensions);

		// @optional -(BOOL)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper shouldReloadAfterDifferentFetchCompletedWithImage:(UIImage * _Nonnull)image dimensions:(CGSize)dimensions identifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)URL treatedAsPlaceholder:(BOOL)placeholder manuallyStored:(BOOL)manuallyStored;
		[Export("tip_fetchHelper:shouldReloadAfterDifferentFetchCompletedWithImage:dimensions:identifier:URL:treatedAsPlaceholder:manuallyStored:")]
		bool ShouldReloadAfterDifferentFetchCompleted(TIPImageViewFetchHelper helper, UIImage image, CGSize dimensions, string identifier, NSUrl url, bool placeholder, bool manuallyStored);

		// @optional -(void)tip_fetchHelperDidStartLoading:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_fetchHelperDidStartLoading:")]
		void DidStartLoading(TIPImageViewFetchHelper helper);

		// @optional -(void)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper didUpdateProgress:(float)progress;
		[Export("tip_fetchHelper:didUpdateProgress:")]
		void DidUpdateProgress(TIPImageViewFetchHelper helper, float progress);

		// @optional -(void)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper didUpdateDisplayedImage:(UIImage * _Nonnull)image fromSourceDimensions:(CGSize)size isFinal:(BOOL)isFinal;
		[Export("tip_fetchHelper:didUpdateDisplayedImage:fromSourceDimensions:isFinal:")]
		void DidUpdateDisplayedImage(TIPImageViewFetchHelper helper, UIImage image, CGSize size, bool isFinal);

		// @optional -(void)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper didLoadFinalImageFromSource:(TIPImageLoadSource)source;
		[Export("tip_fetchHelper:didLoadFinalImageFromSource:")]
		void DidLoadFinalImage(TIPImageViewFetchHelper helper, TIPImageLoadSource source);

		// @optional -(void)tip_fetchHelper:(TIPImageViewFetchHelper * _Nonnull)helper didFailToLoadFinalImage:(NSError * _Nonnull)error;
		[Export("tip_fetchHelper:didFailToLoadFinalImage:")]
		void DidFailToLoadFinalImage(TIPImageViewFetchHelper helper, NSError error);

		// @optional -(void)tip_fetchHelperDidReset:(TIPImageViewFetchHelper * _Nonnull)helper;
		[Export("tip_fetchHelperDidReset:")]
		void DidReset(TIPImageViewFetchHelper helper);
	}

	interface ITIPLogger { }

	// @protocol TIPLogger <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject), Name = "TIPLogger")]
	interface TIPLogger
	{
		// @required -(void)tip_logWithLevel:(TIPLogLevel)level file:(NSString * _Nonnull)file function:(NSString * _Nonnull)function line:(int)line format:(NSString * _Nonnull)format, ... __attribute__((format(NSString, 5, 6)));
		// TODO: [Internal]
		[Abstract]
		[Export("tip_logWithLevel:file:function:line:format:", IsVariadic = true)]
		void Log(TIPLogLevel level, string file, string function, int line, string format, IntPtr varArgs);

		// @optional -(BOOL)tip_canLogWithLevel:(TIPLogLevel)level;
		[Export("tip_canLogWithLevel:")]
		bool CanLog(TIPLogLevel level);
	}

	[BaseType(typeof(NSObject), Name = "TIPSimpleLogger")]
	interface TIPSimpleLogger : TIPLogger
	{
		[Sealed]
		[Export("tip_logWithLevel:file:function:line:format:", IsVariadic = true)]
		void Log(TIPLogLevel level, string file, string function, int line, string format, IntPtr varArgs);

		[Abstract]
		[Export("tip_logWithLevel:file:function:line:message:")]
		void Log(TIPLogLevel level, string file, string function, int line, string message);
	}

	// @interface TIPAdditions (UIImage)
	[Category]
	[BaseType(typeof(UIImage))]
	interface UIImageTIPAdditions
	{
		// -(CGSize)tip_dimensions;
		[Export("tip_dimensions")]
		CGSize Dimensions();

		// -(BOOL)tip_hasAlpha:(BOOL)inspectPixels;
		[Export("tip_hasAlpha:")]
		bool HasAlpha(bool inspectPixels);

		// -(NSUInteger)tip_imageCountBasedOnImageType:(NSString * _Nullable)type;
		[Export("tip_imageCountBasedOnImageType:")]
		nuint ImageCount([NullAllowed] string type);

		// -(NSUInteger)tip_estimatedSizeInBytes;
		[Export("tip_estimatedSizeInBytes")]
		nuint EstimatedSizeInBytes();

		// -(NSString * _Nonnull)tip_recommendedImageType:(TIPRecommendedImageTypeOptions)options;
		[Export("tip_recommendedImageType:")]
		string RecommendedImageType(TIPRecommendedImageTypeOptions options);

		// -(BOOL)tip_matchesTargetDimensions:(CGSize)targetDimensions contentMode:(UIViewContentMode)targetContentMode;
		[Export("tip_matchesTargetDimensions:contentMode:")]
		bool MatchesTargetDimensions(CGSize targetDimensions, UIViewContentMode targetContentMode);

		// -(UIImage * _Nonnull)tip_scaledImageWithTargetDimensions:(CGSize)targetDimensions contentMode:(UIViewContentMode)targetContentMode;
		[Export("tip_scaledImageWithTargetDimensions:contentMode:")]
		UIImage ScaledImage(CGSize targetDimensions, UIViewContentMode targetContentMode);

		// -(UIImage * _Nonnull)tip_orientationAdjustedImage;
		[Export("tip_orientationAdjustedImage")]
		UIImage OrientationAdjustedImage();

		// -(UIImage * _Nullable)tip_CGImageBackedImageAndReturnError:(NSError * _Nullable * _Nullable)error;
		[Export("tip_CGImageBackedImageAndReturnError:")]
		[return: NullAllowed]
		UIImage CGImageBackedImage([NullAllowed] out NSError error);

		// -(UIImage * _Nullable)tip_grayscaleImage;
		[NullAllowed, Export("tip_grayscaleImage")]
		UIImage GrayscaleImage();

		// +(UIImage * _Nullable)tip_imageWithAnimatedImageData:(NSData * _Nonnull)data durations:(NSArray<NSNumber *> * _Nullable * _Nullable)durationsOut loopCount:(NSUInteger * _Nullable)loopCountOut;
		[Static]
		[Export("tip_imageWithAnimatedImageData:durations:loopCount:")]
		[return: NullAllowed]
		unsafe UIImage ImageWithAnimatedImageData(NSData data, [NullAllowed] out NSArray<NSNumber> durationsOut, [NullAllowed] out nuint loopCountOut);

		// +(UIImage * _Nullable)tip_imageWithAnimatedImageFile:(NSString * _Nonnull)filePath durations:(NSArray<NSNumber *> * _Nullable * _Nullable)durationsOut loopCount:(NSUInteger * _Nullable)loopCountOut;
		[Static]
		[Export("tip_imageWithAnimatedImageFile:durations:loopCount:")]
		[return: NullAllowed]
		unsafe UIImage ImageWithAnimatedImageFile(string filePath, [NullAllowed] out NSArray<NSNumber> durationsOut, [NullAllowed] out nuint loopCountOut);

		// -(NSData * _Nullable)tip_writeToDataWithType:(NSString * _Nullable)type encodingOptions:(TIPImageEncodingOptions)encodingOptions quality:(float)quality animationLoopCount:(NSUInteger)animationLoopCount animationFrameDurations:(NSArray<NSNumber *> * _Nullable)animationFrameDurations error:(NSError * _Nullable * _Nullable)error;
		[Export("tip_writeToDataWithType:encodingOptions:quality:animationLoopCount:animationFrameDurations:error:")]
		[return: NullAllowed]
		NSData WriteToData([NullAllowed] string type, TIPImageEncodingOptions encodingOptions, float quality, nuint animationLoopCount, [NullAllowed] NSNumber[] animationFrameDurations, [NullAllowed] out NSError error);

		// -(BOOL)tip_writeToFile:(NSString * _Nonnull)filePath type:(NSString * _Nullable)type encodingOptions:(TIPImageEncodingOptions)encodingOptions quality:(float)quality animationLoopCount:(NSUInteger)animationLoopCount animationFrameDurations:(NSArray<NSNumber *> * _Nullable)animationFrameDurations atomically:(BOOL)atomic error:(NSError * _Nullable * _Nullable)error;
		[Export("tip_writeToFile:type:encodingOptions:quality:animationLoopCount:animationFrameDurations:atomically:error:")]
		bool WriteToFile(string filePath, [NullAllowed] string type, TIPImageEncodingOptions encodingOptions, float quality, nuint animationLoopCount, [NullAllowed] NSNumber[] animationFrameDurations, bool atomic, [NullAllowed] out NSError error);

		// -(void)tip_decode;
		[Export("tip_decode")]
		void Decode();
	}

	// TODO
	//// @interface TIPAdditions_CGImage (UIImage)
	//[Category]
	//[BaseType(typeof(UIImage))]
	//interface UIImageTIPAdditionsCGImage
	//{
	//	// +(UIImage * _Nullable)tip_imageWithAnimatedImageSource:(CGImageSourceRef _Nonnull)imageSource durations:(NSArray<NSNumber *> * _Nullable * _Nullable)durationsOut loopCount:(NSUInteger * _Nullable)loopCountOut;
	//	[Static]
	//	[Export("tip_imageWithAnimatedImageSource:durations:loopCount:")]
	//	[return: NullAllowed]
	//	unsafe UIImage ImageWithAnimatedImageSource(CGImageSourceRef* imageSource, [NullAllowed] out NSNumber[] durationsOut, [NullAllowed] nuint* loopCountOut);

	//	// -(BOOL)tip_writeToCGImageDestination:(CGImageDestinationRef _Nonnull)destinationRef type:(NSString * _Nonnull)type encodingOptions:(TIPImageEncodingOptions)options quality:(float)quality animationLoopCount:(NSUInteger)animationLoopCount animationFrameDurations:(NSArray<NSNumber *> * _Nullable)animationFrameDurations error:(NSError * _Nullable * _Nullable)error;
	//	[Export("tip_writeToCGImageDestination:type:encodingOptions:quality:animationLoopCount:animationFrameDurations:error:")]
	//	unsafe bool WriteToCGImageDestination(CGImageDestinationRef* destinationRef, string type, TIPImageEncodingOptions options, float quality, nuint animationLoopCount, [NullAllowed] NSNumber[] animationFrameDurations, [NullAllowed] out NSError error);
	//}
}
