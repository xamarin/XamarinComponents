using System;
using System.Drawing;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
using MapKit;
using CoreGraphics;
using CoreFoundation;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;
using MonoTouch.CoreFoundation;

using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace SDWebImage
{
	delegate void SDWebImageNoParamsHandler ();

	delegate void SDWebImageQueryCompletedHandler (UIImage image, SDImageCacheType cacheType);
	delegate void SDWebImageCheckCacheCompletionHandler (bool isInCache);
	delegate void SDImageCacheCalculateSizeHandler (nuint fileCount, nuint totalSize);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface SDImageCache
	{
		// @property (assign, nonatomic) BOOL shouldDecompressImages;
		[Export ("shouldDecompressImages")]
		bool ShouldDecompressImages { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisableiCloud;
		[Export ("shouldDisableiCloud")]
		bool ShouldDisableiCloud { get; set; }

		// @property (assign, nonatomic) BOOL shouldCacheImagesInMemory;
		[Export ("shouldCacheImagesInMemory")]
		bool ShouldCacheImagesInMemory { get; set; }

		// @property (assign, nonatomic) NSUInteger maxMemoryCost;
		[Export ("maxMemoryCost", ArgumentSemantic.Assign)]
		nuint MaxMemoryCost { get; set; }

		// @property (assign, nonatomic) NSUInteger maxMemoryCountLimit;
		[Export ("maxMemoryCountLimit", ArgumentSemantic.Assign)]
		nuint MaxMemoryCountLimit { get; set; }

		// @property (assign, nonatomic) NSInteger maxCacheAge;
		[Export ("maxCacheAge", ArgumentSemantic.Assign)]
		nint MaxCacheAge { get; set; }

		// @property (assign, nonatomic) NSUInteger maxCacheSize;
		[Export ("maxCacheSize", ArgumentSemantic.Assign)]
		nuint MaxCacheSize { get; set; }

		// +(SDImageCache *)sharedImageCache;
		[Static]
		[Export ("sharedImageCache")]
		SDImageCache SharedImageCache { get; }

		// -(id)initWithNamespace:(NSString *)ns;
		[Export ("initWithNamespace:")]
		IntPtr Constructor (string ns);

		// -(id)initWithNamespace:(NSString *)ns diskCacheDirectory:(NSString *)directory;
		[Export ("initWithNamespace:diskCacheDirectory:")]
		IntPtr Constructor (string ns, string directory);

		// -(NSString *)makeDiskCachePath:(NSString *)fullNamespace;
		[Export ("makeDiskCachePath:")]
		string MakeDiskCachePath (string fullNamespace);

		// -(void)addReadOnlyCachePath:(NSString *)path;
		[Export ("addReadOnlyCachePath:")]
		void AddReadOnlyCachePath (string path);

		// -(void)storeImage:(UIImage *)image forKey:(NSString *)key;
		[Export ("storeImage:forKey:")]
		void StoreImage (UIImage image, string key);

		// -(void)storeImage:(UIImage *)image forKey:(NSString *)key toDisk:(BOOL)toDisk;
		[Export ("storeImage:forKey:toDisk:")]
		void StoreImage (UIImage image, string key, bool toDisk);

		// -(void)storeImage:(UIImage *)image recalculateFromImage:(BOOL)recalculate imageData:(NSData *)imageData forKey:(NSString *)key toDisk:(BOOL)toDisk;
		[Export ("storeImage:recalculateFromImage:imageData:forKey:toDisk:")]
		void StoreImage (UIImage image, bool recalculate, NSData imageData, string key, bool toDisk);

		// -(NSOperation *)queryDiskCacheForKey:(NSString *)key done:(SDWebImageQueryCompletedBlock)doneBlock;
		[Export ("queryDiskCacheForKey:done:")]
		NSOperation QueryDiskCache (string key, SDWebImageCheckCacheCompletionHandler doneBlock);

		// -(UIImage *)imageFromMemoryCacheForKey:(NSString *)key;
		[Export ("imageFromMemoryCacheForKey:")]
		UIImage ImageFromMemoryCache (string key);

		// -(UIImage *)imageFromDiskCacheForKey:(NSString *)key;
		[Export ("imageFromDiskCacheForKey:")]
		UIImage ImageFromDiskCache(string key);

		// -(void)removeImageForKey:(NSString *)key;
		[Export ("removeImageForKey:")]
		void RemoveImage (string key);

		// -(void)removeImageForKey:(NSString *)key withCompletion:(SDWebImageNoParamsBlock)completion;
		[Export ("removeImageForKey:withCompletion:")]
		void RemoveImage (string key, SDWebImageNoParamsHandler completion);

		// -(void)removeImageForKey:(NSString *)key fromDisk:(BOOL)fromDisk;
		[Export ("removeImageForKey:fromDisk:")]
		void RemoveImage (string key, bool fromDisk);

		// -(void)removeImageForKey:(NSString *)key fromDisk:(BOOL)fromDisk withCompletion:(SDWebImageNoParamsBlock)completion;
		[Export ("removeImageForKey:fromDisk:withCompletion:")]
		void RemoveImage (string key, bool fromDisk, SDWebImageNoParamsHandler completion);

		// -(void)clearMemory;
		[Export ("clearMemory")]
		void ClearMemory ();

		// -(void)clearDiskOnCompletion:(SDWebImageNoParamsBlock)completion;
		[Export ("clearDiskOnCompletion:")]
		void ClearDisk (SDWebImageNoParamsHandler completion);

		// -(void)clearDisk;
		[Export ("clearDisk")]
		void ClearDisk ();

		// -(void)cleanDiskWithCompletionBlock:(SDWebImageNoParamsBlock)completionBlock;
		[Export ("cleanDiskWithCompletionBlock:")]
		void CleanDisk (SDWebImageNoParamsHandler completionBlock);

		// -(void)cleanDisk;
		[Export ("cleanDisk")]
		void CleanDisk ();

		// -(NSUInteger)getSize;
		[Export ("getSize")]
		nuint Size { get; }

		// -(NSUInteger)getDiskCount;
		[Export ("getDiskCount")]
		nuint DiskCount { get; }

		// -(void)calculateSizeWithCompletionBlock:(SDWebImageCalculateSizeBlock)completionBlock;
		[Export ("calculateSizeWithCompletionBlock:")]
		void CalculateSize (SDImageCacheCalculateSizeHandler completionBlock);

		// -(void)diskImageExistsWithKey:(NSString *)key completion:(SDWebImageCheckCacheCompletionBlock)completionBlock;
		[Export ("diskImageExistsWithKey:completion:")]
		void DiskImageExists (string key, SDWebImageCheckCacheCompletionHandler completionBlock);

		// -(BOOL)diskImageExistsWithKey:(NSString *)key;
		[Export ("diskImageExistsWithKey:")]
		bool DiskImageExists (string key);

		// -(NSString *)cachePathForKey:(NSString *)key inPath:(NSString *)path;
		[Export ("cachePathForKey:inPath:")]
		string CachePath (string key, string path);

		// -(NSString *)defaultCachePathForKey:(NSString *)key;
		[Export ("defaultCachePathForKey:")]
		string DefaultCachePath (string key);
	}

	
	delegate void SDWebImageDownloaderProgressHandler (nint receivedSize, nint expectedSize);
	delegate void SDWebImageDownloaderCompletedHandler (UIImage image, NSData data, NSError error, bool finished);
	delegate NSDictionary SDWebImageDownloaderHeadersFilterHandler (NSUrl url, NSDictionary headers);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface SDWebImageDownloader
	{
		[Notification]
		[Field ("SDWebImageDownloadStartNotification", "__Internal")]
		NSString DownloadStartNotification { get; }
		
		[Notification]
		[Field ("SDWebImageDownloadReceiveResponseNotification", "__Internal")]
		NSString DownloadReceiveResponseNotification { get; }

		[Notification]
		[Field ("SDWebImageDownloadStopNotification", "__Internal")]
		NSString DownloadStopNotification { get; }

		[Notification]
		[Field ("SDWebImageDownloadFinishNotification", "__Internal")]
		NSString DownloadFinishNotification { get; }

		// @property (assign, nonatomic) BOOL shouldDecompressImages;
		[Export ("shouldDecompressImages")]
		bool ShouldDecompressImages { get; set; }

		// @property (assign, nonatomic) NSInteger maxConcurrentDownloads;
		[Export ("maxConcurrentDownloads", ArgumentSemantic.Assign)]
		nint MaxConcurrentDownloads { get; set; }

		// @property (readonly, nonatomic) NSUInteger currentDownloadCount;
		[Export ("currentDownloadCount")]
		nint CurrentDownloadCount { get; }

		// @property (assign, nonatomic) NSTimeInterval downloadTimeout;
		[Export ("downloadTimeout")]
		double DownloadTimeout { get; set; }

		// @property (assign, nonatomic) SDWebImageDownloaderExecutionOrder executionOrder;
		[Export ("executionOrder", ArgumentSemantic.Assign)]
		SDWebImageDownloaderExecutionOrder ExecutionOrder { get; set; }

		// +(SDWebImageDownloader *)sharedDownloader;
		[Static]
		[Export ("sharedDownloader")]
		SDWebImageDownloader SharedDownloader { get; }

		// @property (nonatomic, strong) NSURLCredential * urlCredential;
		[Export ("urlCredential", ArgumentSemantic.Strong)]
		NSUrlCredential UrlCredential { get; set; }

		// @property (nonatomic, strong) NSString * username;
		[Export ("username", ArgumentSemantic.Strong)]
		string Username { get; set; }

		// @property (nonatomic, strong) NSString * password;
		[Export ("password", ArgumentSemantic.Strong)]
		string Password { get; set; }

		// @property (copy, nonatomic) SDWebImageDownloaderHeadersFilterBlock headersFilter;
		[NullAllowed]
		[Export ("headersFilter", ArgumentSemantic.Copy)]
		SDWebImageDownloaderHeadersFilterHandler HeadersFilter { get; set; }

		// -(void)setValue:(NSString *)value forHTTPHeaderField:(NSString *)field;
		[Export ("setValue:forHTTPHeaderField:")]
		void SetHttpHeaderValue (string value, string field);

		// -(NSString *)valueForHTTPHeaderField:(NSString *)field;
		[Export ("valueForHTTPHeaderField:")]
		string GetHttpHeaderValue (string field);

		// -(void)setOperationClass:(Class)operationClass;
		[Export ("setOperationClass:")]
		void SetOperationClass (Class operationClass);

		// -(id<SDWebImageOperation>)downloadImageWithURL:(NSURL *)url options:(SDWebImageDownloaderOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageDownloaderCompletedBlock)completedBlock;
		[Export ("downloadImageWithURL:options:progress:completed:")]
		ISDWebImageOperation DownloadImage (NSUrl url, SDWebImageDownloaderOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageDownloaderCompletedHandler completedBlock);

		// -(void)setSuspended:(BOOL)suspended;
		[Export ("setSuspended:")]
		void SetSuspended (bool suspended);
	}
	
	[BaseType (typeof (NSOperation))]
	interface SDWebImageDownloaderOperation : ISDWebImageOperation
	{
		// @property (readonly, nonatomic, strong) NSURLRequest * request;
		[Export ("request", ArgumentSemantic.Strong)]
		NSUrlRequest Request { get; }

		// @property (assign, nonatomic) BOOL shouldDecompressImages;
		[Export ("shouldDecompressImages")]
		bool ShouldDecompressImages { get; set; }

		// @property (assign, nonatomic) BOOL shouldUseCredentialStorage;
		[Export ("shouldUseCredentialStorage")]
		bool ShouldUseCredentialStorage { get; set; }

		// @property (nonatomic, strong) NSURLCredential * credential;
		[Export ("credential", ArgumentSemantic.Strong)]
		NSUrlCredential Credential { get; set; }

		// @property (readonly, assign, nonatomic) SDWebImageDownloaderOptions options;
		[Export ("options", ArgumentSemantic.Assign)]
		SDWebImageDownloaderOptions Options { get; }

		// @property (assign, nonatomic) NSInteger expectedSize;
		[Export ("expectedSize", ArgumentSemantic.Assign)]
		nint ExpectedSize { get; set; }

		// @property (nonatomic, strong) NSURLResponse * response;
		[Export ("response", ArgumentSemantic.Strong)]
		NSUrlResponse Response { get; set; }

		// -(id)initWithRequest:(NSURLRequest *)request options:(SDWebImageDownloaderOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageDownloaderCompletedBlock)completedBlock cancelled:(SDWebImageNoParamsBlock)cancelBlock;
		[Export ("initWithRequest:options:progress:completed:cancelled:")]
		IntPtr Constructor (NSUrlRequest request, SDWebImageDownloaderOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageDownloaderCompletedHandler completedBlock, SDWebImageNoParamsHandler cancelBlock);
	}
	
	delegate void SDWebImageCompletionHandler (UIImage image, NSError error, SDImageCacheType cacheType, NSUrl imageUrl);
	delegate void SDWebImageCompletionWithFinishedHandler (UIImage image, NSError error, SDImageCacheType cacheType, bool finished, NSUrl imageUrl);
	delegate NSString SDWebImageManagerCacheKeyFilterHandler (NSUrl url);

	interface ISDWebImageManagerDelegate { }

	[BaseType (typeof (NSObject))]
	[Protocol]
	[Model]
	interface SDWebImageManagerDelegate
	{
		// @optional -(BOOL)imageManager:(SDWebImageManager *)imageManager shouldDownloadImageForURL:(NSURL *)imageURL;
		[Export ("imageManager:shouldDownloadImageForURL:"), DelegateName ("SDWebImageManagerDelegateCondition"), DefaultValue (true)]
		bool ShouldDownloadImageForURL (SDWebImageManager imageManager, NSUrl imageURL);

		// @optional -(UIImage *)imageManager:(SDWebImageManager *)imageManager transformDownloadedImage:(UIImage *)image withURL:(NSURL *)imageURL;
		[Export ("imageManager:transformDownloadedImage:withURL:"), DelegateName ("SDWebImageManagerDelegateImage"), DefaultValueFromArgument ("image")]
		UIImage TransformDownloadedImage (SDWebImageManager imageManager, UIImage image, NSUrl imageURL);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject),
		Delegates=new string [] {"Delegate"}, 
		Events=new Type [] { typeof (SDWebImageManagerDelegate) })]
	interface SDWebImageManager
	{
		// @property (nonatomic, weak) id<SDWebImageManagerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ISDWebImageManagerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, strong) SDImageCache * imageCache;
		[Export ("imageCache", ArgumentSemantic.Strong)]
		SDImageCache ImageCache { get; }

		// @property (readonly, nonatomic, strong) SDWebImageDownloader * imageDownloader;
		[Export ("imageDownloader", ArgumentSemantic.Strong)]
		SDWebImageDownloader ImageDownloader { get; }

		// @property (copy, nonatomic) SDWebImageCacheKeyFilterBlock cacheKeyFilter;
		[Export ("cacheKeyFilter", ArgumentSemantic.Copy)]
		SDWebImageManagerCacheKeyFilterHandler CacheKeyFilter { get; set; }

		// +(SDWebImageManager *)sharedManager;
		[Static]
		[Export ("sharedManager")]
		SDWebImageManager SharedManager { get; }

		// -(id<SDWebImageOperation>)downloadImageWithURL:(NSURL *)url options:(SDWebImageOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageCompletionWithFinishedBlock)completedBlock;
		[Export ("downloadImageWithURL:options:progress:completed:")]
		ISDWebImageOperation Download (NSUrl url, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageCompletionWithFinishedHandler completedBlock);

		// -(void)saveImageToCache:(UIImage *)image forURL:(NSURL *)url;
		[Export ("saveImageToCache:forURL:")]
		void SaveImageToCache (UIImage image, NSUrl url);

		// -(void)cancelAll;
		[Export ("cancelAll")]
		void CancelAll ();

		// -(BOOL)isRunning;
		[Export ("isRunning")]
		bool IsRunning { get; }

		// -(BOOL)cachedImageExistsForURL:(NSURL *)url;
		[Export ("cachedImageExistsForURL:")]
		bool CachedImageExists (NSUrl url);

		// -(BOOL)diskImageExistsForURL:(NSURL *)url;
		[Export ("diskImageExistsForURL:")]
		bool DiskImageExists (NSUrl url);

		// -(void)cachedImageExistsForURL:(NSURL *)url completion:(SDWebImageCheckCacheCompletionBlock)completionBlock;
		[Export ("cachedImageExistsForURL:completion:")]
		void CachedImageExists (NSUrl url, SDWebImageCheckCacheCompletionHandler completionBlock);

		// -(void)diskImageExistsForURL:(NSURL *)url completion:(SDWebImageCheckCacheCompletionBlock)completionBlock;
		[Export ("diskImageExistsForURL:completion:")]
		void DiskImageExists (NSUrl url, SDWebImageCheckCacheCompletionHandler completionBlock);

		// -(NSString *)cacheKeyForURL:(NSURL *)url;
		[Export ("cacheKeyForURL:")]
		string CacheKey (NSUrl url);
	}

	interface ISDWebImageOperation { }

	[Protocol]
	[Model]
	[BaseType (typeof(NSObject))]
	interface SDWebImageOperation
	{
		// @required -(void)cancel;
		[Abstract]
		[Export ("cancel")]
		void Cancel ();
	}

	interface ISDWebImagePrefetcherDelegate { }

	[BaseType (typeof (NSObject))]
	[Protocol]
	[Model]
	interface SDWebImagePrefetcherDelegate
	{
		// @optional -(void)imagePrefetcher:(SDWebImagePrefetcher *)imagePrefetcher didPrefetchURL:(NSURL *)imageURL finishedCount:(NSUInteger)finishedCount totalCount:(NSUInteger)totalCount;
		[Export ("imagePrefetcher:didPrefetchURL:finishedCount:totalCount:"), EventArgs ("SDWebImagePrefetcherDelegatePrefech"), EventName ("PrefetchedUrl")]
		void DidPrefetchUrl (SDWebImagePrefetcher imagePrefetcher, NSUrl imageURL, nuint finishedCount, nuint totalCount);

		// @optional -(void)imagePrefetcher:(SDWebImagePrefetcher *)imagePrefetcher didFinishWithTotalCount:(NSUInteger)totalCount skippedCount:(NSUInteger)skippedCount;
		[Export ("imagePrefetcher:didFinishWithTotalCount:skippedCount:"), EventArgs ("SDWebImagePrefetcherDelegateFinish"), EventName ("Finished")]
		void DidFinish (SDWebImagePrefetcher imagePrefetcher, nuint totalCount, nuint skippedCount);
	}

	delegate void SDWebImagePrefetcherProgressHandler (nuint finishedCount, nuint totalCount);
	delegate void SDWebImagePrefetcherCompletionHandler (nuint finishedCount, nuint skippedCount);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject),
		Delegates=new string [] {"Delegate"}, 
		Events=new Type [] { typeof (SDWebImagePrefetcherDelegate) })]
	interface SDWebImagePrefetcher
	{
		// @property (readonly, nonatomic, strong) SDWebImageManager * manager;
		[Export ("manager", ArgumentSemantic.Strong)]
		SDWebImageManager Manager { get; }

		// @property (assign, nonatomic) NSUInteger maxConcurrentDownloads;
		[Export ("maxConcurrentDownloads", ArgumentSemantic.Assign)]
		nuint MaxConcurrentDownloads { get; set; }

		// @property (assign, nonatomic) SDWebImageOptions options;
		[Export ("options", ArgumentSemantic.Assign)]
		SDWebImageOptions Options { get; set; }

		// @property (assign, nonatomic) dispatch_queue_t prefetcherQueue;
		[Export ("prefetcherQueue", ArgumentSemantic.Assign)]
		DispatchQueue PrefetcherQueue { get; set; }

		// @property (nonatomic, weak) id<SDWebImagePrefetcherDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ISDWebImagePrefetcherDelegate Delegate { get; set; }

		// +(SDWebImagePrefetcher *)sharedImagePrefetcher;
		[Static]
		[Export ("sharedImagePrefetcher")]
		SDWebImagePrefetcher SharedImagePrefetcher { get; }

		// - (id)initWithImageManager:(SDWebImageManager *)manager;
		[Export ("initWithImageManager:")]
		IntPtr Constructor (SDWebImageManager manager);

		// -(void)prefetchURLs:(NSArray *)urls;
		[Export ("prefetchURLs:")]
		void PrefetchUrls (NSUrl[] urls);

		// -(void)prefetchURLs:(NSArray *)urls progress:(SDWebImagePrefetcherProgressBlock)progressBlock completed:(SDWebImagePrefetcherCompletionBlock)completionBlock;
		[Export ("prefetchURLs:progress:completed:")]
		void PrefetchUrls (NSUrl[] urls, SDWebImagePrefetcherProgressHandler progressBlock, SDWebImagePrefetcherCompletionHandler completionBlock);

		// -(void)cancelPrefetching;
		[Export ("cancelPrefetching")]
		void CancelPrefetching ();
	}

	// @interface ImageContentType (NSData)
	[Static]
	[BaseType (typeof(NSObject), Name = "NSData")]
	interface ImageContentType
	{
		// +(NSString *)sd_contentTypeForImageData:(NSData *)data;
		[Static]
		[Export ("sd_contentTypeForImageData:")]
		string FromImageData (NSData data);
	}

	[Category]
	[BaseType (typeof (UIButton))]
	interface WebCacheUIButtonExtension {

		// -(NSURL *)sd_currentImageURL;
		[Export ("sd_currentImageURL")]
		NSUrl GetImage ();

		// -(NSURL *)sd_imageURLForState:(UIControlState)state;
		[Export ("sd_imageURLForState:")]
		NSUrl GetImage (UIControlState state);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state;
		[Export ("sd_setImageWithURL:forState:")]
		void SetImage (NSUrl url, UIControlState state);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder;
		[Export ("sd_setImageWithURL:forState:placeholderImage:")]
		void SetImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options;
		[Export ("sd_setImageWithURL:forState:placeholderImage:options:")]
		void SetImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:forState:completed:")]
		void SetImage (NSUrl url, UIControlState state, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:forState:placeholderImage:completed:")]
		void SetImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:forState:placeholderImage:options:completed:")]
		void SetImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state;
		[Export ("sd_setBackgroundImageWithURL:forState:")]
		void SetBackgroundImage (NSUrl url, UIControlState state);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder;
		[Export ("sd_setBackgroundImageWithURL:forState:placeholderImage:")]
		void SetBackgroundImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options;
		[Export ("sd_setBackgroundImageWithURL:forState:placeholderImage:options:")]
		void SetBackgroundImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setBackgroundImageWithURL:forState:completed:")]
		void SetBackgroundImage (NSUrl url, UIControlState state, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setBackgroundImageWithURL:forState:placeholderImage:completed:")]
		void SetBackgroundImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setBackgroundImageWithURL:(NSURL *)url forState:(UIControlState)state placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setBackgroundImageWithURL:forState:placeholderImage:options:completed:")]
		void SetBackgroundImage (NSUrl url, UIControlState state, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_cancelImageLoadForState:(UIControlState)state;
		[Export ("sd_cancelImageLoadForState:")]
		void CancelImageLoad (UIControlState state);

		// -(void)sd_cancelBackgroundImageLoadForState:(UIControlState)state;
		[Export ("sd_cancelBackgroundImageLoadForState:")]
		void CancelBackgroundImageLoad (UIControlState state);
	}

	[Static]
	[BaseType (typeof (NSObject), Name = "UIImage")]
	interface ForceDecodeUIImage {
		// +(UIImage *)decodedImageWithImage:(UIImage *)image;
		[Static]
		[Export ("decodedImageWithImage:")]
		UIImage DecodedImageWithImage (UIImage image);
	}

	[Category]
	[BaseType (typeof (UIImageView))]
	interface HighlightedWebCacheUIImageViewExtension {

		// -(void)sd_setHighlightedImageWithURL:(NSURL *)url;
		[Export ("sd_setHighlightedImageWithURL:")]
		void SetHighlightedImage (NSUrl url);

		// -(void)sd_setHighlightedImageWithURL:(NSURL *)url options:(SDWebImageOptions)options;
		[Export ("sd_setHighlightedImageWithURL:options:")]
		void SetHighlightedImage (NSUrl url, SDWebImageOptions options);

		// -(void)sd_setHighlightedImageWithURL:(NSURL *)url completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setHighlightedImageWithURL:completed:")]
		void SetHighlightedImage (NSUrl url, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setHighlightedImageWithURL:(NSURL *)url options:(SDWebImageOptions)options completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setHighlightedImageWithURL:options:completed:")]
		void SetHighlightedImage (NSUrl url, SDWebImageOptions options, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setHighlightedImageWithURL:(NSURL *)url options:(SDWebImageOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setHighlightedImageWithURL:options:progress:completed:")]
		void SetHighlightedImage (NSUrl url, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_cancelCurrentHighlightedImageLoad;
		[Export ("sd_cancelCurrentHighlightedImageLoad")]
		void CancelCurrentHighlightedImageLoad ();

	}

	[Category]
	[BaseType (typeof (UIImageView))]
	interface WebCacheUIImageViewExtension {

		// -(NSURL *)sd_imageURL;
		[Export ("sd_imageURL")]
		NSUrl GetImage ();

		// -(void)sd_setImageWithURL:(NSURL *)url;
		[Export ("sd_setImageWithURL:")]
		void SetImage (NSUrl url);

		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder;
		[Export ("sd_setImageWithURL:placeholderImage:")]
		void SetImage (NSUrl url, [NullAllowed] UIImage placeholder);

		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options;
		[Export ("sd_setImageWithURL:placeholderImage:options:")]
		void SetImage (NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options);

		// -(void)sd_setImageWithURL:(NSURL *)url completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:completed:")]
		void SetImage (NSUrl url, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:placeholderImage:completed:")]
		void SetImage (NSUrl url, [NullAllowed] UIImage placeholder, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:placeholderImage:options:completed:")]
		void SetImage (NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:placeholderImage:options:progress:completed:")]
		void SetImage (NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setImageWithPreviousCachedImageWithURL:(NSURL *)url andPlaceholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options progress:(SDWebImageDownloaderProgressBlock)progressBlock completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithPreviousCachedImageWithURL:andPlaceholderImage:options:progress:completed:")]
		void SetImageWithPreviousCache (NSUrl url, [NullAllowed] UIImage placeholder, SDWebImageOptions options, [NullAllowed] SDWebImageDownloaderProgressHandler progressBlock, [NullAllowed] SDWebImageCompletionHandler completedBlock);

		// -(void)sd_setAnimationImagesWithURLs:(NSArray *)arrayOfURLs;
		[Export ("sd_setAnimationImagesWithURLs:")]
		void SetAnimationImages (NSUrl[] arrayOfURLs);

        // -(void)sd_cancelCurrentImageLoad;
        [Export ("sd_cancelCurrentImageLoad")]
        void CancelCurrentImageLoad ();

        // -(void)sd_cancelCurrentAnimationImagesLoad;
        [Export ("sd_cancelCurrentAnimationImagesLoad")]
        void CancelCurrentAnimationImagesLoad ();
	}

	// @interface GIF (UIImage)
	[Static]
	[BaseType (typeof(NSObject), Name = "UIImage")]
	interface AnimatedUIImage
	{
		// +(UIImage *)sd_animatedGIFNamed:(NSString *)name;
		[Static]
		[Export ("sd_animatedGIFNamed:")]
		UIImage Create (string name);

		// +(UIImage *)sd_animatedGIFWithData:(NSData *)data;
		[Static]
		[Export ("sd_animatedGIFWithData:")]
		UIImage Create (NSData data);
	}

	// @interface GIF (UIImage)
	[Category]
	[BaseType (typeof(UIImage))]
	interface AnimatedUIImageExtension
	{
		// -(UIImage *)sd_animatedImageByScalingAndCroppingToSize:(CGSize)size;
		[Export ("sd_animatedImageByScalingAndCroppingToSize:")]
		UIImage ScaleAndCropAnimated (CGSize size);
	}

	// @interface MultiFormat (UIImage)
	[Static]
	[BaseType (typeof(NSObject), Name = "UIImage")]
	interface MultiFormatUIImage
	{
		// +(UIImage *)sd_imageWithData:(NSData *)data;
		[Static]
		[Export ("sd_imageWithData:")]
		UIImage Create (NSData data);
	}

	// @interface WebCacheOperation (UIView)
	[Category]
	[BaseType (typeof(UIView))]
	interface WebCacheOperationUIViewExtension
	{
		// -(void)sd_setImageLoadOperation:(id)operation forKey:(NSString *)key;
		[Export ("sd_setImageLoadOperation:forKey:")]
		void SetImageLoadOperation (NSObject operation, string key);

		// -(void)sd_cancelImageLoadOperationWithKey:(NSString *)key;
		[Export ("sd_cancelImageLoadOperationWithKey:")]
		void CancelImageLoadOperation (string key);

		// -(void)sd_removeImageLoadOperationWithKey:(NSString *)key;
		[Export ("sd_removeImageLoadOperationWithKey:")]
		void RemoveImageLoadOperation (string key);
	}
	
	// @interface WebCache (MKAnnotationView)
	[Category]
	[BaseType (typeof(MKAnnotationView))]
	interface MKAnnotationView_WebCache
	{
		// -(NSURL *)sd_imageURL;
		[Export ("sd_imageURL")]
		NSUrl GetImageUrl ();
	
		// -(void)sd_setImageWithURL:(NSURL *)url;
		[Export ("sd_setImageWithURL:")]
		void SetImage (NSUrl url);
	
		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder;
		[Export ("sd_setImageWithURL:placeholderImage:")]
		void SetImage (NSUrl url, UIImage placeholder);
	
		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options;
		[Export ("sd_setImageWithURL:placeholderImage:options:")]
		void SetImage (NSUrl url, UIImage placeholder, SDWebImageOptions options);
	
		// -(void)sd_setImageWithURL:(NSURL *)url completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:completed:")]
		void SetImage (NSUrl url, SDWebImageCompletionHandler completedBlock);
	
		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:placeholderImage:completed:")]
		void SetImage (NSUrl url, UIImage placeholder, SDWebImageCompletionHandler completedBlock);
	
		// -(void)sd_setImageWithURL:(NSURL *)url placeholderImage:(UIImage *)placeholder options:(SDWebImageOptions)options completed:(SDWebImageCompletionBlock)completedBlock;
		[Export ("sd_setImageWithURL:placeholderImage:options:completed:")]
		void SetImage (NSUrl url, UIImage placeholder, SDWebImageOptions options, SDWebImageCompletionHandler completedBlock);
	
		// -(void)sd_cancelCurrentImageLoad;
		[Export ("sd_cancelCurrentImageLoad")]
		void CancelCurrentImageLoad ();
	}
}