using System;

#if __UNIFIED__
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using OpenGLES;
using UIKit;
#else
using MonoTouch.AVFoundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMedia;
using MonoTouch.CoreVideo;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nint = System.Int32;
using nuint = System.UInt32;
using nfloat = System.Single;
#endif

using GPUImage;
using GPUImage.Sources;
using GPUImage.Pipeline;
using GPUImage.Filters;
using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.ImageProcessing;
using GPUImage.Filters.Blends;
using GPUImage.Filters.Effects;
using GPUImage.Outputs;

namespace GPUImage
{
	// @interface GLProgram : NSObject
	[BaseType (typeof(NSObject))]
	interface GLProgram
	{
		// @property (readwrite, nonatomic) BOOL initialized;
		[Export ("initialized")]
		bool Initialized { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * vertexShaderLog;
		[Export ("vertexShaderLog")]
		string VertexShaderLog { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * fragmentShaderLog;
		[Export ("fragmentShaderLog")]
		string FragmentShaderLog { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * programLog;
		[Export ("programLog")]
		string ProgramLog { get; set; }

		// -(id)initWithVertexShaderString:(NSString *)vShaderString fragmentShaderString:(NSString *)fShaderString;
		[Export ("initWithVertexShaderString:fragmentShaderString:")]
		IntPtr Constructor (string vShaderString, string fShaderString);

		// -(id)initWithVertexShaderString:(NSString *)vShaderString fragmentShaderFilename:(NSString *)fShaderFilename;
		[Internal]
		[Export ("initWithVertexShaderString:fragmentShaderFilename:")]
		IntPtr InitWithVertexShaderStringAndFragmentShaderFilename (string vShaderString, string fShaderFilename);

		// -(id)initWithVertexShaderFilename:(NSString *)vShaderFilename fragmentShaderFilename:(NSString *)fShaderFilename;
		[Internal]
		[Export ("initWithVertexShaderFilename:fragmentShaderFilename:")]
		IntPtr InitWithVertexShaderFilenameAndFragmentShaderFilename (string vShaderFilename, string fShaderFilename);

		// -(void)addAttribute:(NSString *)attributeName;
		[Export ("addAttribute:")]
		void AddAttribute (string attributeName);

		// -(GLuint)attributeIndex:(NSString *)attributeName;
		[Export ("attributeIndex:")]
		uint AttributeIndex (string attributeName);

		// -(GLuint)uniformIndex:(NSString *)uniformName;
		[Export ("uniformIndex:")]
		uint UniformIndex (string uniformName);

		// -(BOOL)link;
		[Export ("link")]
		bool Link ();

		// -(void)use;
		[Export ("use")]
		void Use ();

		// -(void)validate;
		[Export ("validate")]
		void Validate ();
	}

	// @interface GPUImageFramebuffer : NSObject
	[BaseType (typeof(NSObject))]
	interface GPUImageFramebuffer
	{
		// @property (readonly) CGSize size;
		[Export ("size")]
		CGSize Size { get; }

		// @property (readonly) GPUTextureOptions textureOptions;
		[Export ("textureOptions")]
		GPUTextureOptions TextureOptions { get; }

		// @property (readonly) GLuint texture;
		[Export ("texture")]
		uint Texture { get; }

		// @property (readonly) BOOL missingFramebuffer;
		[Export ("missingFramebuffer")]
		bool MissingFramebuffer { get; }

		// -(id)initWithSize:(CGSize)framebufferSize;
		[Export ("initWithSize:")]
		IntPtr Constructor (CGSize framebufferSize);

		// -(id)initWithSize:(CGSize)framebufferSize textureOptions:(GPUTextureOptions)fboTextureOptions onlyTexture:(BOOL)onlyGenerateTexture;
		[Export ("initWithSize:textureOptions:onlyTexture:")]
		IntPtr Constructor (CGSize framebufferSize, GPUTextureOptions fboTextureOptions, bool onlyGenerateTexture);

		// -(id)initWithSize:(CGSize)framebufferSize overriddenTexture:(GLuint)inputTexture;
		[Export ("initWithSize:overriddenTexture:")]
		IntPtr Constructor (CGSize framebufferSize, uint inputTexture);

		// -(void)activateFramebuffer;
		[Export ("activateFramebuffer")]
		void ActivateFramebuffer ();

		// -(void)lock;
		[Export ("lock")]
		void Lock ();

		// -(void)unlock;
		[Export ("unlock")]
		void Unlock ();

		// -(void)clearAllLocks;
		[Export ("clearAllLocks")]
		void ClearAllLocks ();

		// -(void)disableReferenceCounting;
		[Export ("disableReferenceCounting")]
		void DisableReferenceCounting ();

		// -(void)enableReferenceCounting;
		[Export ("enableReferenceCounting")]
		void EnableReferenceCounting ();

		// -(CGImageRef)newCGImageFromFramebufferContents;
		[Export ("newCGImageFromFramebufferContents")]
		CGImage ToCGImage ();

		// -(void)restoreRenderTarget;
		[Export ("restoreRenderTarget")]
		void RestoreRenderTarget ();

		// -(void)lockForReading;
		[Export ("lockForReading")]
		void LockForReading ();

		// -(void)unlockAfterReading;
		[Export ("unlockAfterReading")]
		void UnlockAfterReading ();

		// -(NSUInteger)bytesPerRow;
		[Export ("bytesPerRow")]
		nuint BytesPerRow { get; }

		// -(GLubyte *)byteBuffer;
		[Export ("byteBuffer")]
		IntPtr ByteBuffer { get; }
	}

	// @interface GPUImageFramebufferCache : NSObject
	[BaseType (typeof(NSObject))]
	interface GPUImageFramebufferCache
	{
		// -(GPUImageFramebuffer *)fetchFramebufferForSize:(CGSize)framebufferSize textureOptions:(GPUTextureOptions)textureOptions onlyTexture:(BOOL)onlyTexture;
		[Export ("fetchFramebufferForSize:textureOptions:onlyTexture:")]
		GPUImageFramebuffer Fetch (CGSize framebufferSize, GPUTextureOptions textureOptions, bool onlyTexture);

		// -(GPUImageFramebuffer *)fetchFramebufferForSize:(CGSize)framebufferSize onlyTexture:(BOOL)onlyTexture;
		[Export ("fetchFramebufferForSize:onlyTexture:")]
		GPUImageFramebuffer Fetch (CGSize framebufferSize, bool onlyTexture);

		// -(void)returnFramebufferToCache:(GPUImageFramebuffer *)framebuffer;
		[Export ("returnFramebufferToCache:")]
		void Return (GPUImageFramebuffer framebuffer);

		// -(void)purgeAllUnassignedFramebuffers;
		[Export ("purgeAllUnassignedFramebuffers")]
		void PurgeAllUnassigned ();

		// -(void)addFramebufferToActiveImageCaptureList:(GPUImageFramebuffer *)framebuffer;
		[Export ("addFramebufferToActiveImageCaptureList:")]
		void AddToActiveImageCaptureList (GPUImageFramebuffer framebuffer);

		// -(void)removeFramebufferFromActiveImageCaptureList:(GPUImageFramebuffer *)framebuffer;
		[Export ("removeFramebufferFromActiveImageCaptureList:")]
		void RemoveFromActiveImageCaptureList (GPUImageFramebuffer framebuffer);
	}

	// @interface GPUImageContext : NSObject
	[BaseType (typeof(NSObject))]
	interface GPUImageContext
	{
		// @property (readonly, nonatomic) dispatch_queue_t contextQueue;
		[Export ("contextQueue")]
		DispatchQueue ContextQueue { get; }

		// @property (readwrite, retain, nonatomic) GLProgram * currentShaderProgram;
		[NullAllowed]
		[Export ("currentShaderProgram", ArgumentSemantic.Retain)]
		GLProgram CurrentShaderProgram { get; set; }

		// @property (readonly, retain, nonatomic) EAGLContext * context;
		[Export ("context", ArgumentSemantic.Retain)]
		EAGLContext Context { get; }

		// TODO
		// @property (readonly) CVOpenGLESTextureCacheRef coreVideoTextureCache;
		[Export ("coreVideoTextureCache")]
		IntPtr CoreVideoTextureCache { get; }

		// @property (readonly) GPUImageFramebufferCache * framebufferCache;
		[Export ("framebufferCache")]
		GPUImageFramebufferCache FramebufferCache { get; }

		// +(void *)contextKey;
		[Static]
		[Export ("contextKey")]
		IntPtr ContextKey { get; }

		// +(GPUImageContext *)sharedImageProcessingContext;
		[Static]
		[Export ("sharedImageProcessingContext")]
		GPUImageContext SharedImageProcessingContext { get; }

		// +(dispatch_queue_t)sharedContextQueue;
		[Static]
		[Export ("sharedContextQueue")]
		DispatchQueue SharedContextQueue { get; }

		// +(GPUImageFramebufferCache *)sharedFramebufferCache;
		[Static]
		[Export ("sharedFramebufferCache")]
		GPUImageFramebufferCache SharedFramebufferCache { get; }

		// +(void)useImageProcessingContext;
		[Static]
		[Export ("useImageProcessingContext")]
		void UseImageProcessingContext ();

		// -(void)useAsCurrentContext;
		[Export ("useAsCurrentContext")]
		void UseAsCurrentContext ();

		// +(void)setActiveShaderProgram:(GLProgram *)shaderProgram;
		[Static]
		[Export ("setActiveShaderProgram:")]
		void SetActiveShaderProgram (GLProgram shaderProgram);

		// -(void)setContextShaderProgram:(GLProgram *)shaderProgram;
		[Export ("setContextShaderProgram:")]
		void SetContextShaderProgram (GLProgram shaderProgram);

		// +(GLint)maximumTextureSizeForThisDevice;
		[Static]
		[Export ("maximumTextureSizeForThisDevice")]
		int MaximumTextureSizeForThisDevice { get; }

		// +(GLint)maximumTextureUnitsForThisDevice;
		[Static]
		[Export ("maximumTextureUnitsForThisDevice")]
		int MaximumTextureUnitsForThisDevice { get; }

		// +(GLint)maximumVaryingVectorsForThisDevice;
		[Static]
		[Export ("maximumVaryingVectorsForThisDevice")]
		int MaximumVaryingVectorsForThisDevice { get; }

		// +(BOOL)deviceSupportsOpenGLESExtension:(NSString *)extension;
		[Static]
		[Export ("deviceSupportsOpenGLESExtension:")]
		bool DeviceSupportsOpenGLESExtension (string extension);

		// +(BOOL)deviceSupportsRedTextures;
		[Static]
		[Export ("deviceSupportsRedTextures")]
		bool DeviceSupportsRedTextures { get; }

		// +(BOOL)deviceSupportsFramebufferReads;
		[Static]
		[Export ("deviceSupportsFramebufferReads")]
		bool DeviceSupportsFramebufferReads { get; }

		// +(CGSize)sizeThatFitsWithinATextureForSize:(CGSize)inputSize;
		[Static]
		[Export ("sizeThatFitsWithinATextureForSize:")]
		CGSize SizeThatFitsWithinATexture (CGSize inputSize);

		// -(void)presentBufferForDisplay;
		[Export ("presentBufferForDisplay")]
		void PresentBufferForDisplay ();

		// -(GLProgram *)programForVertexShaderString:(NSString *)vertexShaderString fragmentShaderString:(NSString *)fragmentShaderString;
		[Export ("programForVertexShaderString:fragmentShaderString:")]
		GLProgram GetProgram (string vertexShaderString, string fragmentShaderString);

		// -(void)useSharegroup:(EAGLSharegroup *)sharegroup;
		[Export ("useSharegroup:")]
		void UseSharegroup (EAGLSharegroup sharegroup);

		// +(BOOL)supportsFastTextureUpload;
		[Static]
		[Export ("supportsFastTextureUpload")]
		bool SupportsFastTextureUpload { get; }
	}

	interface IGPUImageInput
	{
	}

	// @protocol GPUImageInput <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GPUImageInput
	{
		// @required -(void)newFrameReadyAtTime:(CMTime)frameTime atIndex:(NSInteger)textureIndex;
		[Abstract]
		[Export ("newFrameReadyAtTime:atIndex:")]
		void NewFrameReadyAtTime (CMTime frameTime, nint textureIndex);

		// @required -(void)setInputFramebuffer:(GPUImageFramebuffer *)newInputFramebuffer atIndex:(NSInteger)textureIndex;
		[Abstract]
		[Export ("setInputFramebuffer:atIndex:")]
		void SetInputFramebuffer (GPUImageFramebuffer newInputFramebuffer, nint textureIndex);

		// @required -(NSInteger)nextAvailableTextureIndex;
		[Abstract]
		[Export ("nextAvailableTextureIndex")]
		nint NextAvailableTextureIndex { get; }

		// @required -(void)setInputSize:(CGSize)newSize atIndex:(NSInteger)textureIndex;
		[Abstract]
		[Export ("setInputSize:atIndex:")]
		void SetInputSize (CGSize newSize, nint textureIndex);

		// @required -(void)setInputRotation:(GPUImageRotationMode)newInputRotation atIndex:(NSInteger)textureIndex;
		[Abstract]
		[Export ("setInputRotation:atIndex:")]
		void SetInputRotation (GPUImageRotationMode newInputRotation, nint textureIndex);

		// @required -(CGSize)maximumOutputSize;
		[Abstract]
		[Export ("maximumOutputSize")]
		CGSize MaximumOutputSize { get; }

		// @required -(void)endProcessing;
		[Abstract]
		[Export ("endProcessing")]
		void EndProcessing ();

		// @required -(BOOL)shouldIgnoreUpdatesToThisTarget;
		[Abstract]
		[Export ("shouldIgnoreUpdatesToThisTarget")]
		bool ShouldIgnoreUpdatesToThisTarget { get; }

		// @required -(BOOL)enabled;
		[Abstract]
		[Export ("enabled")]
		bool Enabled { get; }

		// @required -(BOOL)wantsMonochromeInput;
		[Abstract]
		[Export ("wantsMonochromeInput")]
		bool WantsMonochromeInput { get; }

		// @required -(void)setCurrentlyReceivingMonochromeInput:(BOOL)newValue;
		[Abstract]
		[Export ("setCurrentlyReceivingMonochromeInput:")]
		void SetCurrentlyReceivingMonochromeInput (bool newValue);
	}
}

namespace GPUImage.Sources
{
	// @interface GPUImageOutput : NSObject
	[BaseType (typeof(NSObject))]
	interface GPUImageOutput
	{
		// @property (readwrite, nonatomic) BOOL shouldSmoothlyScaleOutput;
		[Export ("shouldSmoothlyScaleOutput")]
		bool ShouldSmoothlyScaleOutput { get; set; }

		// @property (readwrite, nonatomic) BOOL shouldIgnoreUpdatesToThisTarget;
		[Export ("shouldIgnoreUpdatesToThisTarget")]
		bool ShouldIgnoreUpdatesToThisTarget { get; set; }

		// @property (readwrite, retain, nonatomic) GPUImageMovieWriter * audioEncodingTarget;
		[NullAllowed]
		[Export ("audioEncodingTarget", ArgumentSemantic.Retain)]
		GPUImageMovieWriter AudioEncodingTarget { get; set; }

		// @property (readwrite, nonatomic, unsafe_unretained) id<GPUImageInput> targetToIgnoreForUpdates;
		[NullAllowed]
		[Export ("targetToIgnoreForUpdates", ArgumentSemantic.Assign)]
		IGPUImageInput TargetToIgnoreForUpdates { get; set; }

		// @property (copy, nonatomic) void (^frameProcessingCompletionBlock)(GPUImageOutput *, CMTime);
		[NullAllowed]
		[Export ("frameProcessingCompletionBlock", ArgumentSemantic.Copy)]
		Action<GPUImageOutput, CMTime> FrameProcessingCompletionHandler { get; set; }

		// @property (nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// @property (readwrite, nonatomic) GPUTextureOptions outputTextureOptions;
		[Export ("outputTextureOptions", ArgumentSemantic.Assign)]
		GPUTextureOptions OutputTextureOptions { get; set; }

		// -(void)setInputFramebufferForTarget:(id<GPUImageInput>)target atIndex:(NSInteger)inputTextureIndex;
		[Export ("setInputFramebufferForTarget:atIndex:")]
		void SetInputFramebuffer (IGPUImageInput target, nint inputTextureIndex);

		// -(GPUImageFramebuffer *)framebufferForOutput;
		[Export ("framebufferForOutput")]
		GPUImageFramebuffer FrameufferForOutput { get; }

		// -(void)removeOutputFramebuffer;
		[Export ("removeOutputFramebuffer")]
		void RemoveOutputFramebuffer ();

		// -(void)notifyTargetsAboutNewOutputTexture;
		[Export ("notifyTargetsAboutNewOutputTexture")]
		void NotifyTargetsAboutNewOutputTexture ();

		// -(NSArray *)targets;
		[Export ("targets")]
		GPUImageInput[] Targets { get; }

		// -(void)addTarget:(id<GPUImageInput>)newTarget;
		[Export ("addTarget:")]
		void AddTarget (IGPUImageInput newTarget);

		// -(void)addTarget:(id<GPUImageInput>)newTarget atTextureLocation:(NSInteger)textureLocation;
		[Export ("addTarget:atTextureLocation:")]
		void AddTarget (IGPUImageInput newTarget, nint textureLocation);

		// -(void)removeTarget:(id<GPUImageInput>)targetToRemove;
		[Export ("removeTarget:")]
		void RemoveTarget (IGPUImageInput targetToRemove);

		// -(void)removeAllTargets;
		[Export ("removeAllTargets")]
		void RemoveAllTargets ();

		// -(void)forceProcessingAtSize:(CGSize)frameSize;
		[Export ("forceProcessingAtSize:")]
		void ForceProcessingAtSize (CGSize frameSize);

		// -(void)forceProcessingAtSizeRespectingAspectRatio:(CGSize)frameSize;
		[Export ("forceProcessingAtSizeRespectingAspectRatio:")]
		void ForceProcessingAtSizeRespectingAspectRatio (CGSize frameSize);

		// -(void)useNextFrameForImageCapture;
		[Export ("useNextFrameForImageCapture")]
		void UseNextFrameForImageCapture ();

		// -(CGImageRef)newCGImageFromCurrentlyProcessedOutput;
		[Export ("newCGImageFromCurrentlyProcessedOutput")]
		CGImage ToCGImage ();

		// -(CGImageRef)newCGImageByFilteringCGImage:(CGImageRef)imageToFilter;
		[Export ("newCGImageByFilteringCGImage:")]
		CGImage CreateFilteredCGImage (CGImage imageToFilter);

		// -(CGImageRef)newCGImageByFilteringImage:(UIImage *)imageToFilter;
		[Export ("newCGImageByFilteringImage:")]
		CGImage CreateFilteredCGImage (UIImage imageToFilter);

		// -(UIImage *)imageFromCurrentFramebuffer;
		[Export ("imageFromCurrentFramebuffer")]
		UIImage ToImage ();

		// -(UIImage *)imageFromCurrentFramebufferWithOrientation:(UIImageOrientation)imageOrientation;
		[Export ("imageFromCurrentFramebufferWithOrientation:")]
		UIImage ToImage (UIImageOrientation imageOrientation);

		// -(UIImage *)imageByFilteringImage:(UIImage *)imageToFilter;
		[Export ("imageByFilteringImage:")]
		UIImage CreateFilteredImage (UIImage imageToFilter);

		// -(BOOL)providesMonochromeOutput;
		[Export ("providesMonochromeOutput")]
		bool ProvidesMonochromeOutput { get; }
	}

	interface IGPUImageVideoCameraDelegate
	{
	}

	// @protocol GPUImageVideoCameraDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GPUImageVideoCameraDelegate
	{
		// @optional -(void)willOutputSampleBuffer:(CMSampleBufferRef)sampleBuffer;
		[Export ("willOutputSampleBuffer:")]
		void WillOutputSampleBuffer (CMSampleBuffer sampleBuffer);
	}

	// @interface GPUImageVideoCamera : GPUImageOutput <AVCaptureVideoDataOutputSampleBufferDelegate, AVCaptureAudioDataOutputSampleBufferDelegate>
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageVideoCamera : IAVCaptureVideoDataOutputSampleBufferDelegate, IAVCaptureAudioDataOutputSampleBufferDelegate
	{
		// extern const GLfloat [] kColorConversion601;
		[Static]
		[Internal]
		[Field ("kColorConversion601", "__Internal")]
		IntPtr ColorConversion601Internal { get; }

		// extern const GLfloat [] kColorConversion601FullRange;
		[Static]
		[Internal]
		[Field ("kColorConversion601FullRange", "__Internal")]
		IntPtr ColorConversion601FullRangeInternal { get; }

		// extern const GLfloat [] kColorConversion709;
		[Static]
		[Internal]
		[Field ("kColorConversion709", "__Internal")]
		IntPtr ColorConversion709Internal { get; }

		// extern NSString *const kGPUImageYUVVideoRangeConversionForRGFragmentShaderString;
		[Static]
		[Field ("kGPUImageYUVVideoRangeConversionForRGFragmentShaderString", "__Internal")]
		NSString YUVVideoRangeConversionForRGFragmentShaderString { get; }

		// extern NSString *const kGPUImageYUVFullRangeConversionForLAFragmentShaderString;
		[Static]
		[Field ("kGPUImageYUVFullRangeConversionForLAFragmentShaderString", "__Internal")]
		NSString YUVFullRangeConversionForLAFragmentShaderString { get; }

		// extern NSString *const kGPUImageYUVVideoRangeConversionForLAFragmentShaderString;
		[Static]
		[Field ("kGPUImageYUVVideoRangeConversionForLAFragmentShaderString", "__Internal")]
		NSString YUVVideoRangeConversionForLAFragmentShaderString { get; }


		// @property (readonly, retain, nonatomic) AVCaptureSession * captureSession;
		[Export ("captureSession", ArgumentSemantic.Retain)]
		AVCaptureSession CaptureSession { get; }

		// @property (readwrite, copy, nonatomic) NSString * captureSessionPreset;
		[Export ("captureSessionPreset")]
		string CaptureSessionPreset { get; set; }

		// @property (readwrite) int32_t frameRate;
		[Export ("frameRate")]
		int FrameRate { get; set; }

		// Duplicate members
		//		// @property (readonly, getter = isFrontFacingCameraPresent) BOOL frontFacingCameraPresent;
		//		[Export ("frontFacingCameraPresent")]
		//		bool IsFrontFacingCameraPresent { [Bind ("isFrontFacingCameraPresent")] get; }
		//
		//		// @property (readonly, getter = isBackFacingCameraPresent) BOOL backFacingCameraPresent;
		//		[Export ("backFacingCameraPresent")]
		//		bool IsBackFacingCameraPresent { [Bind ("isBackFacingCameraPresent")] get; }

		// @property (readwrite, nonatomic) BOOL runBenchmark;
		[Export ("runBenchmark")]
		bool RunBenchmark { get; set; }

		// @property (readonly) AVCaptureDevice * inputCamera;
		[Export ("inputCamera")]
		AVCaptureDevice InputCamera { get; }

		// @property (readwrite, nonatomic) UIInterfaceOrientation outputImageOrientation;
		[Export ("outputImageOrientation", ArgumentSemantic.Assign)]
		UIInterfaceOrientation OutputImageOrientation { get; set; }

		// @property (readwrite, nonatomic) BOOL horizontallyMirrorFrontFacingCamera;
		[Export ("horizontallyMirrorFrontFacingCamera")]
		bool HorizontallyMirrorFrontFacingCamera { get; set; }

		// @property (readwrite, nonatomic) BOOL horizontallyMirrorRearFacingCamera;
		[Export ("horizontallyMirrorRearFacingCamera")]
		bool HorizontallyMirrorRearFacingCamera { get; set; }

		// @property (assign, nonatomic) id<GPUImageVideoCameraDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IGPUImageVideoCameraDelegate Delegate { get; set; }

		// -(id)initWithSessionPreset:(NSString *)sessionPreset cameraPosition:(AVCaptureDevicePosition)cameraPosition;
		[Export ("initWithSessionPreset:cameraPosition:")]
		IntPtr Constructor (string sessionPreset, AVCaptureDevicePosition cameraPosition);

		// -(BOOL)addAudioInputsAndOutputs;
		[Export ("addAudioInputsAndOutputs")]
		bool AddAudioInputsAndOutputs ();

		// -(BOOL)removeAudioInputsAndOutputs;
		[Export ("removeAudioInputsAndOutputs")]
		bool RemoveAudioInputsAndOutputs ();

		// -(void)removeInputsAndOutputs;
		[Export ("removeInputsAndOutputs")]
		void RemoveInputsAndOutputs ();

		// -(void)startCameraCapture;
		[Export ("startCameraCapture")]
		void StartCameraCapture ();

		// -(void)stopCameraCapture;
		[Export ("stopCameraCapture")]
		void StopCameraCapture ();

		// -(void)pauseCameraCapture;
		[Export ("pauseCameraCapture")]
		void PauseCameraCapture ();

		// -(void)resumeCameraCapture;
		[Export ("resumeCameraCapture")]
		void ResumeCameraCapture ();

		// -(void)processVideoSampleBuffer:(CMSampleBufferRef)sampleBuffer;
		[Export ("processVideoSampleBuffer:")]
		void ProcessVideoSampleBuffer (CMSampleBuffer sampleBuffer);

		// -(void)processAudioSampleBuffer:(CMSampleBufferRef)sampleBuffer;
		[Export ("processAudioSampleBuffer:")]
		void ProcessAudioSampleBuffer (CMSampleBuffer sampleBuffer);

		// -(AVCaptureDevicePosition)cameraPosition;
		[Export ("cameraPosition")]
		AVCaptureDevicePosition CameraPosition { get; }

		// -(AVCaptureConnection *)videoCaptureConnection;
		[Export ("videoCaptureConnection")]
		AVCaptureConnection VideoCaptureConnection { get; }

		// -(void)rotateCamera;
		[Export ("rotateCamera")]
		void RotateCamera ();

		// -(CGFloat)averageFrameDurationDuringCapture;
		[Export ("averageFrameDurationDuringCapture")]
		nfloat AverageFrameDurationDuringCapture { get; }

		// -(void)resetBenchmarkAverage;
		[Export ("resetBenchmarkAverage")]
		void ResetBenchmarkAverage ();

		// +(BOOL)isBackFacingCameraPresent;
		[Static]
		[Export ("isBackFacingCameraPresent")]
		bool IsBackFacingCameraPresent { get; }

		// +(BOOL)isFrontFacingCameraPresent;
		[Static]
		[Export ("isFrontFacingCameraPresent")]
		bool IsFrontFacingCameraPresent { get; }
	}

	// @interface GPUImageStillCamera : GPUImageVideoCamera
	[BaseType (typeof(GPUImageVideoCamera))]
	interface GPUImageStillCamera
	{
		// @property CGFloat jpegCompressionQuality;
		[Export ("jpegCompressionQuality")]
		nfloat JpegCompressionQuality { get; set; }

		// @property (readonly) NSDictionary * currentCaptureMetadata;
		[Export ("currentCaptureMetadata")]
		NSDictionary CurrentCaptureMetadata { get; }

		// -(id)initWithSessionPreset:(NSString *)sessionPreset cameraPosition:(AVCaptureDevicePosition)cameraPosition;
		[Export ("initWithSessionPreset:cameraPosition:")]
		IntPtr Constructor (string sessionPreset, AVCaptureDevicePosition cameraPosition);

		// TODO
		//		// -(void)capturePhotoAsSampleBufferWithCompletionHandler:(void (^)(CMSampleBufferRef, NSError *))block;
		//		[Export ("capturePhotoAsSampleBufferWithCompletionHandler:")]
		//		void CapturePhotoAsSampleBufferWithCompletionHandler (Action<ref CMSampleBuffer, NSError> block);

		// -(void)capturePhotoAsImageProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withCompletionHandler:(void (^)(UIImage *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsImageProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsImage (IGPUImageInput finalFilterInChain, Action<UIImage, NSError> block);

		// -(void)capturePhotoAsImageProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withOrientation:(UIImageOrientation)orientation withCompletionHandler:(void (^)(UIImage *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsImageProcessedUpToFilter:withOrientation:withCompletionHandler:")]
		void CapturePhotoAsImage (IGPUImageInput finalFilterInChain, UIImageOrientation orientation, Action<UIImage, NSError> block);

		// -(void)capturePhotoAsJPEGProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withCompletionHandler:(void (^)(NSData *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsJPEGProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsJPEG (IGPUImageInput finalFilterInChain, Action<NSData, NSError> block);

		// -(void)capturePhotoAsJPEGProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withOrientation:(UIImageOrientation)orientation withCompletionHandler:(void (^)(NSData *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsJPEGProcessedUpToFilter:withOrientation:withCompletionHandler:")]
		void CapturePhotoAsJPEG (IGPUImageInput finalFilterInChain, UIImageOrientation orientation, Action<NSData, NSError> block);

		// -(void)capturePhotoAsPNGProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withCompletionHandler:(void (^)(NSData *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsPNGProcessedUpToFilter:withCompletionHandler:")]
		void CapturePhotoAsPNG (IGPUImageInput finalFilterInChain, Action<NSData, NSError> block);

		// -(void)capturePhotoAsPNGProcessedUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withOrientation:(UIImageOrientation)orientation withCompletionHandler:(void (^)(NSData *, NSError *))block;
		[Async]
		[Export ("capturePhotoAsPNGProcessedUpToFilter:withOrientation:withCompletionHandler:")]
		void CapturePhotoAsPNG (IGPUImageInput finalFilterInChain, UIImageOrientation orientation, Action<NSData, NSError> block);
	}

	// @interface GPUImagePicture : GPUImageOutput
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImagePicture
	{
		// -(id)initWithURL:(NSURL *)url;
		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		// -(id)initWithImage:(UIImage *)newImageSource;
		[Export ("initWithImage:")]
		IntPtr Constructor (UIImage newImageSource);

		// -(id)initWithCGImage:(CGImageRef)newImageSource;
		[Export ("initWithCGImage:")]
		IntPtr Constructor (CGImage newImageSource);

		// -(id)initWithImage:(UIImage *)newImageSource smoothlyScaleOutput:(BOOL)smoothlyScaleOutput;
		[Export ("initWithImage:smoothlyScaleOutput:")]
		IntPtr Constructor (UIImage newImageSource, bool smoothlyScaleOutput);

		// -(id)initWithCGImage:(CGImageRef)newImageSource smoothlyScaleOutput:(BOOL)smoothlyScaleOutput;
		[Export ("initWithCGImage:smoothlyScaleOutput:")]
		IntPtr Constructor (CGImage newImageSource, bool smoothlyScaleOutput);

		// -(void)processImage;
		[Export ("processImage")]
		void ProcessImage ();

		// -(CGSize)outputImageSize;
		[Export ("outputImageSize")]
		CGSize OutputImageSize { get; }

		// -(BOOL)processImageWithCompletionHandler:(void (^)(void))completion;
		[Async]
		[Export ("processImageWithCompletionHandler:")]
		bool ProcessImage (Action completion);

		// -(void)processImageUpToFilter:(GPUImageOutput<GPUImageInput> *)finalFilterInChain withCompletionHandler:(void (^)(UIImage *))block;
		[Async]
		[Export ("processImageUpToFilter:withCompletionHandler:")]
		void ProcessImage (IGPUImageInput finalFilterInChain, Action<UIImage> block);
	}

	// @interface TextureSubimage (GPUImagePicture)
	[Category]
	[BaseType (typeof(GPUImagePicture))]
	interface GPUImagePicture_TextureSubimage
	{
		// -(void)replaceTextureWithSubimage:(UIImage *)subimage;
		[Export ("replaceTextureWithSubimage:")]
		void ReplaceTextureWithSubimage (UIImage subimage);

		// -(void)replaceTextureWithSubCGImage:(CGImageRef)subimageSource;
		[Export ("replaceTextureWithSubCGImage:")]
		void ReplaceTextureWithSubCGImage (CGImage subimageSource);

		// -(void)replaceTextureWithSubimage:(UIImage *)subimage inRect:(CGRect)subRect;
		[Export ("replaceTextureWithSubimage:inRect:")]
		void ReplaceTextureWithSubimage (UIImage subimage, CGRect subRect);

		// -(void)replaceTextureWithSubCGImage:(CGImageRef)subimageSource inRect:(CGRect)subRect;
		[Export ("replaceTextureWithSubCGImage:inRect:")]
		void ReplaceTextureWithSubCGImage (CGImage subimageSource, CGRect subRect);
	}

	interface IGPUImageMovieDelegate
	{
	}

	// @protocol GPUImageMovieDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GPUImageMovieDelegate
	{
		// @required -(void)didCompletePlayingMovie;
		[Abstract]
		[Export ("didCompletePlayingMovie")]
		void DidCompletePlayingMovie ();
	}

	// @interface GPUImageMovie : GPUImageOutput
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageMovie
	{
		// @property (readwrite, retain) AVAsset * asset;
		[NullAllowed]
		[Export ("asset", ArgumentSemantic.Retain)]
		AVAsset Asset { get; set; }

		// @property (readwrite, retain) AVPlayerItem * playerItem;
		[NullAllowed]
		[Export ("playerItem", ArgumentSemantic.Retain)]
		AVPlayerItem PlayerItem { get; set; }

		// @property (readwrite, retain) NSURL * url;
		[NullAllowed]
		[Export ("url", ArgumentSemantic.Retain)]
		NSUrl Url { get; set; }

		// @property (readwrite, nonatomic) BOOL runBenchmark;
		[Export ("runBenchmark")]
		bool RunBenchmark { get; set; }

		// @property (readwrite, nonatomic) BOOL playAtActualSpeed;
		[Export ("playAtActualSpeed")]
		bool PlayAtActualSpeed { get; set; }

		// @property (readwrite, nonatomic) BOOL shouldRepeat;
		[Export ("shouldRepeat")]
		bool ShouldRepeat { get; set; }

		// @property (readonly, nonatomic) float progress;
		[Export ("progress")]
		float Progress { get; }

		// @property (assign, readwrite, nonatomic) id<GPUImageMovieDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IGPUImageMovieDelegate Delegate { get; set; }

		// @property (readonly, nonatomic) AVAssetReader * assetReader;
		[NullAllowed]
		[Export ("assetReader")]
		AVAssetReader AssetReader { get; }

		// @property (readonly, nonatomic) BOOL audioEncodingIsFinished;
		[Export ("audioEncodingIsFinished")]
		bool AudioEncodingIsFinished { get; }

		// @property (readonly, nonatomic) BOOL videoEncodingIsFinished;
		[Export ("videoEncodingIsFinished")]
		bool VideoEncodingIsFinished { get; }

		// -(id)initWithAsset:(AVAsset *)asset;
		[Export ("initWithAsset:")]
		IntPtr Constructor (AVAsset asset);

		// -(id)initWithPlayerItem:(AVPlayerItem *)playerItem;
		[Export ("initWithPlayerItem:")]
		IntPtr Constructor (AVPlayerItem playerItem);

		// -(id)initWithURL:(NSURL *)url;
		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		// -(void)yuvConversionSetup;
		[Export ("yuvConversionSetup")]
		void YuvConversionSetup ();

		// -(void)enableSynchronizedEncodingUsingMovieWriter:(GPUImageMovieWriter *)movieWriter;
		[Export ("enableSynchronizedEncodingUsingMovieWriter:")]
		void EnableSynchronizedEncoding (GPUImageMovieWriter movieWriter);

		// -(BOOL)readNextVideoFrameFromOutput:(AVAssetReaderOutput *)readerVideoTrackOutput;
		[Export ("readNextVideoFrameFromOutput:")]
		bool ReadNextVideoFrame (AVAssetReaderOutput readerVideoTrackOutput);

		// -(BOOL)readNextAudioSampleFromOutput:(AVAssetReaderOutput *)readerAudioTrackOutput;
		[Export ("readNextAudioSampleFromOutput:")]
		bool ReadNextAudioSample (AVAssetReaderOutput readerAudioTrackOutput);

		// -(void)startProcessing;
		[Export ("startProcessing")]
		void StartProcessing ();

		// -(void)endProcessing;
		[Export ("endProcessing")]
		void EndProcessing ();

		// -(void)cancelProcessing;
		[Export ("cancelProcessing")]
		void CancelProcessing ();

		// -(void)processMovieFrame:(CMSampleBufferRef)movieSampleBuffer;
		[Export ("processMovieFrame:")]
		void ProcessMovieFrame (CMSampleBuffer movieSampleBuffer);
	}

	// @interface GPUImageMovieComposition : GPUImageMovie
	[BaseType (typeof(GPUImageMovie))]
	interface GPUImageMovieComposition
	{
		// @property (readwrite, retain) AVComposition * compositon;
		[NullAllowed]
		[Export ("compositon", ArgumentSemantic.Retain)]
		AVComposition Compositon { get; set; }

		// @property (readwrite, retain) AVVideoComposition * videoComposition;
		[NullAllowed]
		[Export ("videoComposition", ArgumentSemantic.Retain)]
		AVVideoComposition VideoComposition { get; set; }

		// @property (readwrite, retain) AVAudioMix * audioMix;
		[NullAllowed]
		[Export ("audioMix", ArgumentSemantic.Retain)]
		AVAudioMix AudioMix { get; set; }

		// -(id)initWithComposition:(AVComposition *)compositon andVideoComposition:(AVVideoComposition *)videoComposition andAudioMix:(AVAudioMix *)audioMix;
		[Export ("initWithComposition:andVideoComposition:andAudioMix:")]
		IntPtr Constructor (AVComposition compositon, AVVideoComposition videoComposition, AVAudioMix audioMix);
	}

	// @interface GPUImageTextureInput : GPUImageOutput
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageTextureInput
	{
		// -(id)initWithTexture:(GLuint)newInputTexture size:(CGSize)newTextureSize;
		[Export ("initWithTexture:size:")]
		IntPtr Constructor (uint newInputTexture, CGSize newTextureSize);

		// -(void)processTextureWithFrameTime:(CMTime)frameTime;
		[Export ("processTextureWithFrameTime:")]
		void ProcessTexture (CMTime frameTime);
	}

	// @interface GPUImageRawDataInput : GPUImageOutput
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageRawDataInput
	{
		// -(id)initWithBytes:(GLubyte *)bytesToUpload size:(CGSize)imageSize;
		[Export ("initWithBytes:size:")]
		IntPtr Constructor (IntPtr bytesToUpload, CGSize imageSize);

		// -(id)initWithBytes:(GLubyte *)bytesToUpload size:(CGSize)imageSize pixelFormat:(GPUPixelFormat)pixelFormat;
		[Export ("initWithBytes:size:pixelFormat:")]
		IntPtr Constructor (IntPtr bytesToUpload, CGSize imageSize, GPUPixelFormat pixelFormat);

		// -(id)initWithBytes:(GLubyte *)bytesToUpload size:(CGSize)imageSize pixelFormat:(GPUPixelFormat)pixelFormat type:(GPUPixelType)pixelType;
		[Export ("initWithBytes:size:pixelFormat:type:")]
		IntPtr Constructor (IntPtr bytesToUpload, CGSize imageSize, GPUPixelFormat pixelFormat, GPUPixelType pixelType);

		// @property (readwrite, nonatomic) GPUPixelFormat pixelFormat;
		[Export ("pixelFormat", ArgumentSemantic.Assign)]
		GPUPixelFormat PixelFormat { get; set; }

		// @property (readwrite, nonatomic) GPUPixelType pixelType;
		[Export ("pixelType", ArgumentSemantic.Assign)]
		GPUPixelType PixelType { get; set; }

		// -(void)updateDataFromBytes:(GLubyte *)bytesToUpload size:(CGSize)imageSize;
		[Export ("updateDataFromBytes:size:")]
		void UpdateData (IntPtr bytesToUpload, CGSize imageSize);

		// -(void)processData;
		[Export ("processData")]
		void ProcessData ();

		// -(void)processDataForTimestamp:(CMTime)frameTime;
		[Export ("processDataForTimestamp:")]
		void ProcessData (CMTime frameTime);

		// -(CGSize)outputImageSize;
		[Export ("outputImageSize")]
		CGSize OutputImageSize { get; }
	}

	// @interface GPUImageUIElement : GPUImageOutput
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageUIElement
	{
		// -(id)initWithView:(UIView *)inputView;
		[Export ("initWithView:")]
		IntPtr Constructor (UIView inputView);

		// -(id)initWithLayer:(CALayer *)inputLayer;
		[Export ("initWithLayer:")]
		IntPtr Constructor (CALayer inputLayer);

		// -(CGSize)layerSizeInPixels;
		[Export ("layerSizeInPixels")]
		CGSize LayerSizeInPixels { get; }

		// -(void)update;
		[Export ("update")]
		void Update ();

		// -(void)updateUsingCurrentTime;
		[Export ("updateUsingCurrentTime")]
		void UpdateUsingCurrentTime ();

		// -(void)updateWithTimestamp:(CMTime)frameTime;
		[Export ("updateWithTimestamp:")]
		void Update (CMTime frameTime);
	}
}

namespace GPUImage.Pipeline
{
	// @interface GPUImageFilterPipeline : NSObject
	[BaseType (typeof(NSObject))]
	interface GPUImageFilterPipeline
	{
		// @property (strong) NSMutableArray * filters;
		[NullAllowed]
		[Export ("filters", ArgumentSemantic.Strong)]
		NSMutableArray Filters { get; set; }

		// @property (strong) GPUImageOutput * input;
		[NullAllowed]
		[Export ("input", ArgumentSemantic.Strong)]
		GPUImageOutput Input { get; set; }

		// @property (strong) id<GPUImageInput> output;
		[NullAllowed]
		[Export ("output", ArgumentSemantic.Strong)]
		IGPUImageInput Output { get; set; }

		// -(id)initWithOrderedFilters:(NSArray *)filters input:(GPUImageOutput *)input output:(id<GPUImageInput>)output;
		[Export ("initWithOrderedFilters:input:output:")]
		IntPtr Constructor (GPUImageInput[] filters, GPUImageOutput input, GPUImageInput output);

		// -(id)initWithConfiguration:(NSDictionary *)configuration input:(GPUImageOutput *)input output:(id<GPUImageInput>)output;
		[Export ("initWithConfiguration:input:output:")]
		IntPtr Constructor (NSDictionary configuration, GPUImageOutput input, GPUImageInput output);

		// -(id)initWithConfigurationFile:(NSURL *)configuration input:(GPUImageOutput *)input output:(id<GPUImageInput>)output;
		[Export ("initWithConfigurationFile:input:output:")]
		IntPtr Constructor (NSUrl configuration, GPUImageOutput input, GPUImageInput output);

		// -(void)addFilter:(GPUImageOutput<GPUImageInput> *)filter;
		[Export ("addFilter:")]
		void AddFilter (IGPUImageInput filter);

		// -(void)addFilter:(GPUImageOutput<GPUImageInput> *)filter atIndex:(NSUInteger)insertIndex;
		[Export ("addFilter:atIndex:")]
		void AddFilter (IGPUImageInput filter, nuint insertIndex);

		// -(void)replaceFilterAtIndex:(NSUInteger)index withFilter:(GPUImageOutput<GPUImageInput> *)filter;
		[Export ("replaceFilterAtIndex:withFilter:")]
		void ReplaceFilter (nuint index, GPUImageInput filter);

		// -(void)replaceAllFilters:(NSArray *)newFilters;
		[Export ("replaceAllFilters:")]
		void ReplaceAllFilters (GPUImageInput[] newFilters);

		// -(void)removeFilter:(GPUImageOutput<GPUImageInput> *)filter;
		[Export ("removeFilter:")]
		void RemoveFilter (IGPUImageInput filter);

		// -(void)removeFilterAtIndex:(NSUInteger)index;
		[Export ("removeFilterAtIndex:")]
		void RemoveFilter (nuint index);

		// -(void)removeAllFilters;
		[Export ("removeAllFilters")]
		void RemoveAllFilters ();

		// -(UIImage *)currentFilteredFrame;
		[Export ("currentFilteredFrame")]
		UIImage ToImage ();

		// -(UIImage *)currentFilteredFrameWithOrientation:(UIImageOrientation)imageOrientation;
		[Export ("currentFilteredFrameWithOrientation:")]
		UIImage ToImage (UIImageOrientation imageOrientation);

		// -(CGImageRef)newCGImageFromCurrentFilteredFrame;
		[Export ("newCGImageFromCurrentFilteredFrame")]
		CGImage ToCGImage ();
	}
}

namespace GPUImage.Filters
{
	// @interface GPUImageFilter : GPUImageOutput <GPUImageInput>
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageFilter : GPUImageInput
	{
		// extern NSString *const kGPUImageVertexShaderString;
		[Static]
		[Field ("kGPUImageVertexShaderString", "__Internal")]
		NSString VertexShaderString { get; }

		// extern NSString *const kGPUImagePassthroughFragmentShaderString;
		[Static]
		[Field ("kGPUImagePassthroughFragmentShaderString", "__Internal")]
		NSString PassthroughFragmentShaderString { get; }


		// TODO
		//		// @property (readonly) CVPixelBufferRef renderTarget;
		//		[Export ("renderTarget")]
		//		CVPixelBuffer RenderTarget { get; }

		// @property (readwrite, nonatomic) BOOL preventRendering;
		[Export ("preventRendering")]
		bool PreventRendering { get; set; }

		// @property (readwrite, nonatomic) BOOL currentlyReceivingMonochromeInput;
		[Export ("currentlyReceivingMonochromeInput")]
		bool IsCurrentlyReceivingMonochromeInput ();

		// @property (readwrite, nonatomic) BOOL currentlyReceivingMonochromeInput;
		[Export ("setCurrentlyReceivingMonochromeInput:")]
		void SetCurrentlyReceivingMonochromeInput (bool newValue);

		// -(id)initWithVertexShaderFromString:(NSString *)vertexShaderString fragmentShaderFromString:(NSString *)fragmentShaderString;
		[Export ("initWithVertexShaderFromString:fragmentShaderFromString:")]
		IntPtr Constructor (string vertexShaderString, string fragmentShaderString);

		// -(id)initWithFragmentShaderFromString:(NSString *)fragmentShaderString;
		[Internal]
		[Export ("initWithFragmentShaderFromString:")]
		IntPtr InitWithFragmentShaderFromString (string fragmentShaderString);

		// -(id)initWithFragmentShaderFromFile:(NSString *)fragmentShaderFilename;
		[Internal]
		[Export ("initWithFragmentShaderFromFile:")]
		IntPtr InitWithFragmentShaderFromFile (string fragmentShaderFilename);

		// -(void)initializeAttributes;
		[Export ("initializeAttributes")]
		void InitializeAttributes ();

		// -(void)setupFilterForSize:(CGSize)filterFrameSize;
		[Export ("setupFilterForSize:")]
		void SetupFilter (CGSize filterFrameSize);

		// -(CGSize)rotatedSize:(CGSize)sizeToRotate forIndex:(NSInteger)textureIndex;
		[Export ("rotatedSize:forIndex:")]
		CGSize RotatedSize (CGSize sizeToRotate, nint textureIndex);

		// -(CGPoint)rotatedPoint:(CGPoint)pointToRotate forRotation:(GPUImageRotationMode)rotation;
		[Export ("rotatedPoint:forRotation:")]
		CGPoint RotatedPoint (CGPoint pointToRotate, GPUImageRotationMode rotation);

		// -(CGSize)sizeOfFBO;
		[Export ("sizeOfFBO")]
		CGSize SizeOfFBO { get; }

		// +(const GLfloat *)textureCoordinatesForRotation:(GPUImageRotationMode)rotationMode;
		[Internal]
		[Static]
		[Export ("textureCoordinatesForRotation:")]
		IntPtr TextureCoordinatesInternal (GPUImageRotationMode rotationMode);

		// -(void)renderToTextureWithVertices:(const GLfloat *)vertices textureCoordinates:(const GLfloat *)textureCoordinates;
		[Internal]
		[Export ("renderToTextureWithVertices:textureCoordinates:")]
		void RenderToTexture (IntPtr vertices, IntPtr textureCoordinates);

		// -(void)informTargetsAboutNewFrameAtTime:(CMTime)frameTime;
		[Export ("informTargetsAboutNewFrameAtTime:")]
		void InformTargetsAboutNewFrame (CMTime frameTime);

		// -(CGSize)outputFrameSize;
		[Export ("outputFrameSize")]
		CGSize OutputFrameSize { get; }

		// -(void)setBackgroundColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent alpha:(GLfloat)alphaComponent;
		[Export ("setBackgroundColorRed:green:blue:alpha:")]
		void SetBackgroundColor (float redComponent, float greenComponent, float blueComponent, float alphaComponent);

		// -(void)setInteger:(GLint)newInteger forUniformName:(NSString *)uniformName;
		[Export ("setInteger:forUniformName:")]
		void SetInteger (int newInteger, string uniformName);

		// -(void)setFloat:(GLfloat)newFloat forUniformName:(NSString *)uniformName;
		[Export ("setFloat:forUniformName:")]
		void SetFloat (float newFloat, string uniformName);

		// -(void)setSize:(CGSize)newSize forUniformName:(NSString *)uniformName;
		[Export ("setSize:forUniformName:")]
		void SetSize (CGSize newSize, string uniformName);

		// -(void)setPoint:(CGPoint)newPoint forUniformName:(NSString *)uniformName;
		[Export ("setPoint:forUniformName:")]
		void SetPoint (CGPoint newPoint, string uniformName);

		// -(void)setFloatVec3:(GPUVector3)newVec3 forUniformName:(NSString *)uniformName;
		[Export ("setFloatVec3:forUniformName:")]
		void SetFloatVec3 (GPUVector3 newVec3, string uniformName);

		// -(void)setFloatVec4:(GPUVector4)newVec4 forUniform:(NSString *)uniformName;
		[Export ("setFloatVec4:forUniform:")]
		void SetFloatVec4 (GPUVector4 newVec4, string uniformName);

		// -(void)setFloatArray:(GLfloat *)array length:(GLsizei)count forUniform:(NSString *)uniformName;
		[Internal]
		[Export ("setFloatArray:length:forUniform:")]
		void SetFloatArray (IntPtr array, int count, string uniformName);

		// -(void)setMatrix3f:(GPUMatrix3x3)matrix forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setMatrix3f:forUniform:program:")]
		void SetMatrix3f (GPUMatrix3x3 matrix, int uniform, GLProgram shaderProgram);

		// -(void)setMatrix4f:(GPUMatrix4x4)matrix forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setMatrix4f:forUniform:program:")]
		void SetMatrix4f (GPUMatrix4x4 matrix, int uniform, GLProgram shaderProgram);

		// -(void)setFloat:(GLfloat)floatValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setFloat:forUniform:program:")]
		void SetFloat (float floatValue, int uniform, GLProgram shaderProgram);

		// -(void)setPoint:(CGPoint)pointValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setPoint:forUniform:program:")]
		void SetPoint (CGPoint pointValue, int uniform, GLProgram shaderProgram);

		// -(void)setSize:(CGSize)sizeValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setSize:forUniform:program:")]
		void SetSize (CGSize sizeValue, int uniform, GLProgram shaderProgram);

		// -(void)setVec3:(GPUVector3)vectorValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setVec3:forUniform:program:")]
		void SetVec3 (GPUVector3 vectorValue, int uniform, GLProgram shaderProgram);

		// -(void)setVec4:(GPUVector4)vectorValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setVec4:forUniform:program:")]
		void SetVec4 (GPUVector4 vectorValue, int uniform, GLProgram shaderProgram);

		// -(void)setFloatArray:(GLfloat *)arrayValue length:(GLsizei)arrayLength forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Internal]
		[Export ("setFloatArray:length:forUniform:program:")]
		void SetFloatArray (IntPtr arrayValue, int arrayLength, int uniform, GLProgram shaderProgram);

		// -(void)setInteger:(GLint)intValue forUniform:(GLint)uniform program:(GLProgram *)shaderProgram;
		[Export ("setInteger:forUniform:program:")]
		void SetInteger (int intValue, int uniform, GLProgram shaderProgram);

		// -(void)setAndExecuteUniformStateCallbackAtIndex:(GLint)uniform forProgram:(GLProgram *)shaderProgram toBlock:(dispatch_block_t)uniformStateBlock;
		[Export ("setAndExecuteUniformStateCallbackAtIndex:forProgram:toBlock:")]
		void SetAndExecuteUniformStateCallback (int uniform, GLProgram shaderProgram, Action uniformStateBlock);

		// -(void)setUniformsForProgramAtIndex:(NSUInteger)programIndex;
		[Export ("setUniformsForProgramAtIndex:")]
		void SetUniforms (nuint programIndex);
	}

	// @interface GPUImageTwoInputFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageTwoInputFilter
	{
		// extern NSString *const kGPUImageTwoInputTextureVertexShaderString;
		[Static]
		[Field ("kGPUImageTwoInputTextureVertexShaderString", "__Internal")]
		NSString TwoInputTextureVertexShaderString { get; }


		// -(void)disableFirstFrameCheck;
		[Export ("disableFirstFrameCheck")]
		void DisableFirstFrameCheck ();

		// -(void)disableSecondFrameCheck;
		[Export ("disableSecondFrameCheck")]
		void DisableSecondFrameCheck ();
	}

	// @interface GPUImageThreeInputFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageThreeInputFilter
	{
		// extern NSString *const kGPUImageThreeInputTextureVertexShaderString;
		[Field ("kGPUImageThreeInputTextureVertexShaderString", "__Internal")]
		NSString ThreeInputTextureVertexShaderString { get; }


		// -(void)disableThirdFrameCheck;
		[Export ("disableThirdFrameCheck")]
		void DisableThirdFrameCheck ();
	}

	// @interface GPUImageTwoPassFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageTwoPassFilter
	{
		// -(id)initWithFirstStageVertexShaderFromString:(NSString *)firstStageVertexShaderString firstStageFragmentShaderFromString:(NSString *)firstStageFragmentShaderString secondStageVertexShaderFromString:(NSString *)secondStageVertexShaderString secondStageFragmentShaderFromString:(NSString *)secondStageFragmentShaderString;
		[Export ("initWithFirstStageVertexShaderFromString:firstStageFragmentShaderFromString:secondStageVertexShaderFromString:secondStageFragmentShaderFromString:")]
		IntPtr Constructor (string firstStageVertexShaderString, string firstStageFragmentShaderString, string secondStageVertexShaderString, string secondStageFragmentShaderString);

		// -(id)initWithFirstStageFragmentShaderFromString:(NSString *)firstStageFragmentShaderString secondStageFragmentShaderFromString:(NSString *)secondStageFragmentShaderString;
		[Export ("initWithFirstStageFragmentShaderFromString:secondStageFragmentShaderFromString:")]
		IntPtr Constructor (string firstStageFragmentShaderString, string secondStageFragmentShaderString);

		// -(void)initializeSecondaryAttributes;
		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();
	}

	// @interface GPUImageTwoPassTextureSamplingFilter : GPUImageTwoPassFilter
	[BaseType (typeof(GPUImageTwoPassFilter))]
	interface GPUImageTwoPassTextureSamplingFilter
	{
		// @property (readwrite, nonatomic) CGFloat verticalTexelSpacing;
		[Export ("verticalTexelSpacing")]
		nfloat VerticalTexelSpacing { get; set; }

		// @property (readwrite, nonatomic) CGFloat horizontalTexelSpacing;
		[Export ("horizontalTexelSpacing")]
		nfloat HorizontalTexelSpacing { get; set; }
	}

	// @interface GPUImageFilterGroup : GPUImageOutput <GPUImageInput>
	[BaseType (typeof(GPUImageOutput))]
	interface GPUImageFilterGroup : GPUImageInput
	{
		// @property (readwrite, nonatomic, strong) GPUImageOutput<GPUImageInput> * terminalFilter;
		[NullAllowed]
		[Export ("terminalFilter", ArgumentSemantic.Strong)]
		IGPUImageInput TerminalFilter { get; set; }

		// @property (readwrite, nonatomic, strong) NSArray * initialFilters;
		[NullAllowed]
		[Export ("initialFilters", ArgumentSemantic.Strong)]
		IGPUImageInput[] InitialFilters { get; set; }

		// @property (readwrite, nonatomic, strong) GPUImageOutput<GPUImageInput> * inputFilterToIgnoreForUpdates;
		[NullAllowed]
		[Export ("inputFilterToIgnoreForUpdates", ArgumentSemantic.Strong)]
		IGPUImageInput InputFilterToIgnoreForUpdates { get; set; }

		// -(void)addFilter:(GPUImageOutput<GPUImageInput> *)newFilter;
		[Export ("addFilter:")]
		void AddFilter (IGPUImageInput newFilter);

		// -(GPUImageOutput<GPUImageInput> *)filterAtIndex:(NSUInteger)filterIndex;
		[Export ("filterAtIndex:")]
		IGPUImageInput GetFilter (nuint filterIndex);

		// -(NSUInteger)filterCount;
		[Export ("filterCount")]
		nuint FilterCount { get; }
	}

	// @interface GPUImage3x3TextureSamplingFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImage3x3TextureSamplingFilter
	{
		// extern NSString *const kGPUImageNearbyTexelSamplingVertexShaderString;
		[Static]
		[Field ("kGPUImageNearbyTexelSamplingVertexShaderString", "__Internal")]
		NSString NearbyTexelSamplingVertexShaderString { get; }


		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }
	}

	// @interface GPUImageTwoInputCrossTextureSamplingFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageTwoInputCrossTextureSamplingFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }
	}

	// @interface GPUImageBuffer : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageBuffer
	{
		// @property (readwrite, nonatomic) NSUInteger bufferSize;
		[Export ("bufferSize")]
		nuint BufferSize { get; set; }
	}
}

namespace GPUImage.Filters.ColorProcessing
{
	// @interface GPUImageBrightnessFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageBrightnessFilter
	{
		// @property (readwrite, nonatomic) CGFloat brightness;
		[Export ("brightness")]
		nfloat Brightness { get; set; }
	}

	// @interface GPUImageLevelsFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageLevelsFilter
	{
		// -(void)setRedMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max minOut:(CGFloat)minOut maxOut:(CGFloat)maxOut;
		[Export ("setRedMin:gamma:max:minOut:maxOut:")]
		void SetRed (nfloat min, nfloat mid, nfloat max, nfloat minOut, nfloat maxOut);

		// -(void)setRedMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max;
		[Export ("setRedMin:gamma:max:")]
		void SetRed (nfloat min, nfloat mid, nfloat max);

		// -(void)setGreenMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max minOut:(CGFloat)minOut maxOut:(CGFloat)maxOut;
		[Export ("setGreenMin:gamma:max:minOut:maxOut:")]
		void SetGreen (nfloat min, nfloat mid, nfloat max, nfloat minOut, nfloat maxOut);

		// -(void)setGreenMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max;
		[Export ("setGreenMin:gamma:max:")]
		void SetGreen (nfloat min, nfloat mid, nfloat max);

		// -(void)setBlueMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max minOut:(CGFloat)minOut maxOut:(CGFloat)maxOut;
		[Export ("setBlueMin:gamma:max:minOut:maxOut:")]
		void SetBlue (nfloat min, nfloat mid, nfloat max, nfloat minOut, nfloat maxOut);

		// -(void)setBlueMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max;
		[Export ("setBlueMin:gamma:max:")]
		void SetBlue (nfloat min, nfloat mid, nfloat max);

		// -(void)setMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max minOut:(CGFloat)minOut maxOut:(CGFloat)maxOut;
		[Export ("setMin:gamma:max:minOut:maxOut:")]
		void Set (nfloat min, nfloat mid, nfloat max, nfloat minOut, nfloat maxOut);

		// -(void)setMin:(CGFloat)min gamma:(CGFloat)mid max:(CGFloat)max;
		[Export ("setMin:gamma:max:")]
		void Set (nfloat min, nfloat mid, nfloat max);
	}

	// @interface GPUImageExposureFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageExposureFilter
	{
		// @property (readwrite, nonatomic) CGFloat exposure;
		[Export ("exposure")]
		nfloat Exposure { get; set; }
	}

	// @interface GPUImageContrastFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageContrastFilter
	{
		// @property (readwrite, nonatomic) CGFloat contrast;
		[Export ("contrast")]
		nfloat Contrast { get; set; }
	}

	// @interface GPUImageSaturationFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageSaturationFilter
	{
		// @property (readwrite, nonatomic) CGFloat saturation;
		[Export ("saturation")]
		nfloat Saturation { get; set; }
	}

	// @interface GPUImageGammaFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageGammaFilter
	{
		// @property (readwrite, nonatomic) CGFloat gamma;
		[Export ("gamma")]
		nfloat Gamma { get; set; }
	}

	// @interface GPUImageColorMatrixFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageColorMatrixFilter
	{
		// @property (readwrite, nonatomic) GPUMatrix4x4 colorMatrix;
		[Export ("colorMatrix", ArgumentSemantic.Assign)]
		GPUMatrix4x4 ColorMatrix { get; set; }

		// @property (readwrite, nonatomic) CGFloat intensity;
		[Export ("intensity")]
		nfloat Intensity { get; set; }
	}

	// @interface GPUImageRGBFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageRGBFilter
	{
		// @property (readwrite, nonatomic) CGFloat red;
		[Export ("red")]
		nfloat Red { get; set; }

		// @property (readwrite, nonatomic) CGFloat green;
		[Export ("green")]
		nfloat Green { get; set; }

		// @property (readwrite, nonatomic) CGFloat blue;
		[Export ("blue")]
		nfloat Blue { get; set; }
	}

	// @interface GPUImageHSBFilter : GPUImageColorMatrixFilter
	[BaseType (typeof(GPUImageColorMatrixFilter))]
	interface GPUImageHSBFilter
	{
		// -(void)reset;
		[Export ("reset")]
		void Reset ();

		// -(void)rotateHue:(float)h;
		[Export ("rotateHue:")]
		void RotateHue (float h);

		// -(void)adjustSaturation:(float)s;
		[Export ("adjustSaturation:")]
		void AdjustSaturation (float s);

		// -(void)adjustBrightness:(float)b;
		[Export ("adjustBrightness:")]
		void AdjustBrightness (float b);
	}

	// @interface GPUImageHueFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageHueFilter
	{
		// @property (readwrite, nonatomic) CGFloat hue;
		[Export ("hue")]
		nfloat Hue { get; set; }
	}

	// @interface GPUImageMonochromeFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageMonochromeFilter
	{
		// @property (readwrite, nonatomic) CGFloat intensity;
		[Export ("intensity")]
		nfloat Intensity { get; set; }

		// @property (readwrite, nonatomic) GPUVector4 color;
		[Export ("color", ArgumentSemantic.Assign)]
		GPUVector4 Color { get; set; }

		// -(void)setColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setColorRed:green:blue:")]
		void SetColor (float redComponent, float greenComponent, float blueComponent);
	}

	// @interface GPUImageFalseColorFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageFalseColorFilter
	{
		// @property (readwrite, nonatomic) GPUVector4 firstColor;
		[Export ("firstColor", ArgumentSemantic.Assign)]
		GPUVector4 FirstColor { get; set; }

		// @property (readwrite, nonatomic) GPUVector4 secondColor;
		[Export ("secondColor", ArgumentSemantic.Assign)]
		GPUVector4 SecondColor { get; set; }

		// -(void)setFirstColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setFirstColorRed:green:blue:")]
		void SetFirstColor (float redComponent, float greenComponent, float blueComponent);

		// -(void)setSecondColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setSecondColorRed:green:blue:")]
		void SetSecondColor (float redComponent, float greenComponent, float blueComponent);
	}

	// @interface GPUImageHazeFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageHazeFilter
	{
		// @property (readwrite, nonatomic) CGFloat distance;
		[Export ("distance")]
		nfloat Distance { get; set; }

		// @property (readwrite, nonatomic) CGFloat slope;
		[Export ("slope")]
		nfloat Slope { get; set; }
	}

	// @interface GPUImageSepiaFilter : GPUImageColorMatrixFilter
	[BaseType (typeof(GPUImageColorMatrixFilter))]
	interface GPUImageSepiaFilter
	{
	}

	// @interface GPUImageColorInvertFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageColorInvertFilter
	{
	}

	// @interface GPUImageGrayscaleFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageGrayscaleFilter
	{
		// extern NSString *const kGPUImageLuminanceFragmentShaderString;
		[Static]
		[Field ("kGPUImageLuminanceFragmentShaderString", "__Internal")]
		NSString LuminanceFragmentShaderString { get; }
	}

	// @interface GPUImageLuminanceThresholdFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageLuminanceThresholdFilter
	{
		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }
	}

	// @interface GPUImageAdaptiveThresholdFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageAdaptiveThresholdFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }
	}

	// @interface GPUImageAverageLuminanceThresholdFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageAverageLuminanceThresholdFilter
	{
		// @property (readwrite, nonatomic) CGFloat thresholdMultiplier;
		[Export ("thresholdMultiplier")]
		nfloat ThresholdMultiplier { get; set; }
	}

	// @interface GPUImageHistogramFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageHistogramFilter
	{
		// @property (readwrite, nonatomic) NSUInteger downsamplingFactor;
		[Export ("downsamplingFactor")]
		nuint DownsamplingFactor { get; set; }

		// -(id)initWithHistogramType:(GPUImageHistogramType)newHistogramType;
		[Export ("initWithHistogramType:")]
		IntPtr Constructor (GPUImageHistogramType newHistogramType);

		// -(void)initializeSecondaryAttributes;
		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();
	}

	// @interface GPUImageHistogramGenerator : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageHistogramGenerator
	{
	}

	// @interface GPUImageHistogramEqualizationFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageHistogramEqualizationFilter
	{
		// @property (readwrite, nonatomic) NSUInteger downsamplingFactor;
		[Export ("downsamplingFactor")]
		nuint DownsamplingFactor { get; set; }

		// -(id)initWithHistogramType:(GPUImageHistogramType)newHistogramType;
		[Export ("initWithHistogramType:")]
		IntPtr Constructor (GPUImageHistogramType newHistogramType);
	}

	// @interface GPUImageToneCurveFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageToneCurveFilter
	{
		// @property (readwrite, copy, nonatomic) NSArray * redControlPoints;
		// TODO [Verify (StronglyTypedNSArray)]
		[Export ("redControlPoints", ArgumentSemantic.Copy)]
		NSObject[] RedControlPoints { get; set; }

		// TODO [Verify (StronglyTypedNSArray)]
		// @property (readwrite, copy, nonatomic) NSArray * greenControlPoints;
		[Export ("greenControlPoints", ArgumentSemantic.Copy)]
		NSObject[] GreenControlPoints { get; set; }

		// TODO [Verify (StronglyTypedNSArray)]
		// @property (readwrite, copy, nonatomic) NSArray * blueControlPoints;
		[Export ("blueControlPoints", ArgumentSemantic.Copy)]
		NSObject[] BlueControlPoints { get; set; }

		// TODO [Verify (StronglyTypedNSArray)]
		// @property (readwrite, copy, nonatomic) NSArray * rgbCompositeControlPoints;
		[Export ("rgbCompositeControlPoints", ArgumentSemantic.Copy)]
		NSObject[] RgbCompositeControlPoints { get; set; }

		// -(id)initWithACVData:(NSData *)data;
		[Export ("initWithACVData:")]
		IntPtr Constructor (NSData data);

		// -(id)initWithACV:(NSString *)curveFilename;
		[Export ("initWithACV:")]
		IntPtr Constructor (string curveFilename);

		// -(id)initWithACVURL:(NSURL *)curveFileURL;
		[Export ("initWithACVURL:")]
		IntPtr Constructor (NSUrl curveFileURL);

		// -(void)setRGBControlPoints:(NSArray *)points __attribute__((deprecated("")));
		[Export ("setRGBControlPoints:")]
		// TODO [Verify (StronglyTypedNSArray)]
		void SetRGBControlPoints (NSObject[] points);

		// -(void)setPointsWithACV:(NSString *)curveFilename;
		[Export ("setPointsWithACV:")]
		void SetPointsWithACV (string curveFilename);

		// -(void)setPointsWithACVURL:(NSURL *)curveFileURL;
		[Export ("setPointsWithACVURL:")]
		void SetPointsWithACV (NSUrl curveFileURL);

		// TODO [Verify (StronglyTypedNSArray)]
		// -(NSMutableArray *)getPreparedSplineCurve:(NSArray *)points;
		[Export ("getPreparedSplineCurve:")]
		NSMutableArray GetPreparedSplineCurve (NSObject[] points);

		// TODO [Verify (StronglyTypedNSArray)]
		// -(NSMutableArray *)splineCurve:(NSArray *)points;
		[Export ("splineCurve:")]
		NSMutableArray SplineCurve (NSObject[] points);

		// TODO [Verify (StronglyTypedNSArray)]
		// -(NSMutableArray *)secondDerivative:(NSArray *)cgPoints;
		[Export ("secondDerivative:")]
		NSMutableArray SecondDerivative (NSObject[] cgPoints);

		// -(void)updateToneCurveTexture;
		[Export ("updateToneCurveTexture")]
		void UpdateToneCurveTexture ();
	}

	// @interface GPUImageHighlightShadowFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageHighlightShadowFilter
	{
		// @property (readwrite, nonatomic) CGFloat shadows;
		[Export ("shadows")]
		nfloat Shadows { get; set; }

		// @property (readwrite, nonatomic) CGFloat highlights;
		[Export ("highlights")]
		nfloat Highlights { get; set; }
	}

	// @interface GPUImageLookupFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageLookupFilter
	{
		// @property (readwrite, nonatomic) CGFloat intensity;
		[Export ("intensity")]
		nfloat Intensity { get; set; }
	}

	// @interface GPUImageAmatorkaFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageAmatorkaFilter
	{
	}

	// @interface GPUImageMissEtikateFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageMissEtikateFilter
	{
	}

	// @interface GPUImageSoftEleganceFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageSoftEleganceFilter
	{
	}

	// @interface GPUImageOpacityFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageOpacityFilter
	{
		// @property (readwrite, nonatomic) CGFloat opacity;
		[Export ("opacity")]
		nfloat Opacity { get; set; }
	}

	// @interface GPUImageAverageColor : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageAverageColor
	{
		// extern NSString *const kGPUImageColorAveragingVertexShaderString;
		[Static]
		[Field ("kGPUImageColorAveragingVertexShaderString", "__Internal")]
		NSString ColorAveragingVertexShaderString { get; }


		// @property (copy, nonatomic) void (^colorAverageProcessingFinishedBlock)(CGFloat, CGFloat, CGFloat, CGFloat, CMTime);
		[NullAllowed]
		[Export ("colorAverageProcessingFinishedBlock", ArgumentSemantic.Copy)]
		Action<nfloat, nfloat, nfloat, nfloat, CMTime> ColorAverageProcessingFinishedHandler { get; set; }

		// -(void)extractAverageColorAtFrameTime:(CMTime)frameTime;
		[Export ("extractAverageColorAtFrameTime:")]
		void ExtractAverageColor (CMTime frameTime);
	}

	// @interface GPUImageLuminosity : GPUImageAverageColor
	[BaseType (typeof(GPUImageAverageColor))]
	interface GPUImageLuminosity
	{
		// @property (copy, nonatomic) void (^luminosityProcessingFinishedBlock)(CGFloat, CMTime);
		[NullAllowed]
		[Export ("luminosityProcessingFinishedBlock", ArgumentSemantic.Copy)]
		Action<nfloat, CMTime> LuminosityProcessingFinishedHandler { get; set; }

		// -(void)extractLuminosityAtFrameTime:(CMTime)frameTime;
		[Export ("extractLuminosityAtFrameTime:")]
		void ExtractLuminosity (CMTime frameTime);

		// -(void)initializeSecondaryAttributes;
		[Export ("initializeSecondaryAttributes")]
		void InitializeSecondaryAttributes ();
	}

	// @interface GPUImageSolidColorGenerator : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageSolidColorGenerator
	{
		// @property (readwrite, nonatomic) GPUVector4 color;
		[Export ("color", ArgumentSemantic.Assign)]
		GPUVector4 Color { get; set; }

		// @property (assign, readwrite, nonatomic) BOOL useExistingAlpha;
		[Export ("useExistingAlpha")]
		bool UseExistingAlpha { get; set; }

		// -(void)setColorRed:(CGFloat)redComponent green:(CGFloat)greenComponent blue:(CGFloat)blueComponent alpha:(CGFloat)alphaComponent;
		[Export ("setColorRed:green:blue:alpha:")]
		void SetColor (nfloat redComponent, nfloat greenComponent, nfloat blueComponent, nfloat alphaComponent);
	}

	// @interface GPUImageWhiteBalanceFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageWhiteBalanceFilter
	{
		// @property (readwrite, nonatomic) CGFloat temperature;
		[Export ("temperature")]
		nfloat Temperature { get; set; }

		// @property (readwrite, nonatomic) CGFloat tint;
		[Export ("tint")]
		nfloat Tint { get; set; }
	}

	// @interface GPUImageChromaKeyFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageChromaKeyFilter
	{
		// @property (readwrite, nonatomic) CGFloat thresholdSensitivity;
		[Export ("thresholdSensitivity")]
		nfloat ThresholdSensitivity { get; set; }

		// @property (readwrite, nonatomic) CGFloat smoothing;
		[Export ("smoothing")]
		nfloat Smoothing { get; set; }

		// -(void)setColorToReplaceRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setColorToReplaceRed:green:blue:")]
		void SetColorToReplace (float redComponent, float greenComponent, float blueComponent);
	}

	// @interface GPUImageLuminanceRangeFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageLuminanceRangeFilter
	{
		// @property (readwrite, nonatomic) CGFloat rangeReductionFactor;
		[Export ("rangeReductionFactor")]
		nfloat RangeReductionFactor { get; set; }
	}
}

namespace GPUImage.Filters.ImageProcessing
{
	// @interface GPUImageTransformFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageTransformFilter
	{
		// @property (readwrite, nonatomic) CGAffineTransform affineTransform;
		[Export ("affineTransform", ArgumentSemantic.Assign)]
		CGAffineTransform AffineTransform { get; set; }

		// @property (readwrite, nonatomic) CATransform3D transform3D;
		[Export ("transform3D", ArgumentSemantic.Assign)]
		CATransform3D Transform3D { get; set; }

		// @property (readwrite, nonatomic) BOOL ignoreAspectRatio;
		[Export ("ignoreAspectRatio")]
		bool IgnoreAspectRatio { get; set; }

		// @property (readwrite, nonatomic) BOOL anchorTopLeft;
		[Export ("anchorTopLeft")]
		bool AnchorTopLeft { get; set; }
	}

	// @interface GPUImageCropFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageCropFilter
	{
		// @property (readwrite, nonatomic) CGRect cropRegion;
		[Export ("cropRegion", ArgumentSemantic.Assign)]
		CGRect CropRegion { get; set; }

		// -(id)initWithCropRegion:(CGRect)newCropRegion;
		[Export ("initWithCropRegion:")]
		IntPtr Constructor (CGRect newCropRegion);
	}

	// @interface GPUImageSharpenFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageSharpenFilter
	{
		// @property (readwrite, nonatomic) CGFloat sharpness;
		[Export ("sharpness")]
		nfloat Sharpness { get; set; }
	}

	// @interface GPUImageUnsharpMaskFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageUnsharpMaskFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat intensity;
		[Export ("intensity")]
		nfloat Intensity { get; set; }
	}

	// @interface GPUImageBoxBlurFilter : GPUImageGaussianBlurFilter
	[BaseType (typeof(GPUImageGaussianBlurFilter))]
	interface GPUImageBoxBlurFilter
	{
	}

	// @interface GPUImageGaussianBlurFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageGaussianBlurFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelSpacingMultiplier;
		[Export ("texelSpacingMultiplier")]
		nfloat TexelSpacingMultiplier { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusAsFractionOfImageWidth;
		[Export ("blurRadiusAsFractionOfImageWidth")]
		nfloat BlurRadiusAsFractionOfImageWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusAsFractionOfImageHeight;
		[Export ("blurRadiusAsFractionOfImageHeight")]
		nfloat BlurRadiusAsFractionOfImageHeight { get; set; }

		// @property (readwrite, nonatomic) NSUInteger blurPasses;
		[Export ("blurPasses")]
		nuint BlurPasses { get; set; }

		// +(NSString *)vertexShaderForStandardBlurOfRadius:(NSUInteger)blurRadius sigma:(CGFloat)sigma;
		[Static]
		[Export ("vertexShaderForStandardBlurOfRadius:sigma:")]
		string VertexShaderForStandardBlur (nuint blurRadius, nfloat sigma);

		// +(NSString *)fragmentShaderForStandardBlurOfRadius:(NSUInteger)blurRadius sigma:(CGFloat)sigma;
		[Static]
		[Export ("fragmentShaderForStandardBlurOfRadius:sigma:")]
		string FragmentShaderForStandardBlur (nuint blurRadius, nfloat sigma);

		// +(NSString *)vertexShaderForOptimizedBlurOfRadius:(NSUInteger)blurRadius sigma:(CGFloat)sigma;
		[Static]
		[Export ("vertexShaderForOptimizedBlurOfRadius:sigma:")]
		string VertexShaderForOptimizedBlur (nuint blurRadius, nfloat sigma);

		// +(NSString *)fragmentShaderForOptimizedBlurOfRadius:(NSUInteger)blurRadius sigma:(CGFloat)sigma;
		[Static]
		[Export ("fragmentShaderForOptimizedBlurOfRadius:sigma:")]
		string FragmentShaderForOptimizedBlur (nuint blurRadius, nfloat sigma);

		// -(void)switchToVertexShader:(NSString *)newVertexShader fragmentShader:(NSString *)newFragmentShader;
		[Export ("switchToVertexShader:fragmentShader:")]
		void SwitchToShaders (string newVertexShader, string newFragmentShader);
	}

	// @interface GPUImageGaussianSelectiveBlurFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageGaussianSelectiveBlurFilter
	{
		// @property (readwrite, nonatomic) CGFloat excludeCircleRadius;
		[Export ("excludeCircleRadius")]
		nfloat ExcludeCircleRadius { get; set; }

		// @property (readwrite, nonatomic) CGPoint excludeCirclePoint;
		[Export ("excludeCirclePoint", ArgumentSemantic.Assign)]
		CGPoint ExcludeCirclePoint { get; set; }

		// @property (readwrite, nonatomic) CGFloat excludeBlurSize;
		[Export ("excludeBlurSize")]
		nfloat ExcludeBlurSize { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat aspectRatio;
		[Export ("aspectRatio")]
		nfloat AspectRatio { get; set; }
	}

	// @interface GPUImageGaussianBlurPositionFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageGaussianBlurPositionFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurSize;
		[Export ("blurSize")]
		nfloat BlurSize { get; set; }

		// @property (readwrite, nonatomic) CGPoint blurCenter;
		[Export ("blurCenter", ArgumentSemantic.Assign)]
		CGPoint BlurCenter { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadius;
		[Export ("blurRadius")]
		nfloat BlurRadius { get; set; }
	}

	// @interface GPUImageSingleComponentGaussianBlurFilter : GPUImageGaussianBlurFilter
	[BaseType (typeof(GPUImageGaussianBlurFilter))]
	interface GPUImageSingleComponentGaussianBlurFilter
	{
	}

	// @interface GPUImageMedianFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageMedianFilter
	{
	}

	// @interface GPUImageBilateralFilter : GPUImageGaussianBlurFilter
	[BaseType (typeof(GPUImageGaussianBlurFilter))]
	interface GPUImageBilateralFilter
	{
		// @property (readwrite, nonatomic) CGFloat distanceNormalizationFactor;
		[Export ("distanceNormalizationFactor")]
		nfloat DistanceNormalizationFactor { get; set; }
	}

	// @interface GPUImageTiltShiftFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageTiltShiftFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat topFocusLevel;
		[Export ("topFocusLevel")]
		nfloat TopFocusLevel { get; set; }

		// @property (readwrite, nonatomic) CGFloat bottomFocusLevel;
		[Export ("bottomFocusLevel")]
		nfloat BottomFocusLevel { get; set; }

		// @property (readwrite, nonatomic) CGFloat focusFallOffRate;
		[Export ("focusFallOffRate")]
		nfloat FocusFallOffRate { get; set; }
	}

	// @interface GPUImage3x3ConvolutionFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImage3x3ConvolutionFilter
	{
		// @property (readwrite, nonatomic) GPUMatrix3x3 convolutionKernel;
		[Export ("convolutionKernel", ArgumentSemantic.Assign)]
		GPUMatrix3x3 ConvolutionKernel { get; set; }
	}

	// @interface GPUImageLaplacianFilter : GPUImage3x3ConvolutionFilter
	[BaseType (typeof(GPUImage3x3ConvolutionFilter))]
	interface GPUImageLaplacianFilter
	{
	}

	// @interface GPUImageSobelEdgeDetectionFilter : GPUImageTwoPassFilter
	[BaseType (typeof(GPUImageTwoPassFilter))]
	interface GPUImageSobelEdgeDetectionFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }

		// @property (readwrite, nonatomic) CGFloat edgeStrength;
		[Export ("edgeStrength")]
		nfloat EdgeStrength { get; set; }
	}

	// @interface GPUImageThresholdEdgeDetectionFilter : GPUImageSobelEdgeDetectionFilter
	[BaseType (typeof(GPUImageSobelEdgeDetectionFilter))]
	interface GPUImageThresholdEdgeDetectionFilter
	{
		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }
	}

	// @interface GPUImageDirectionalSobelEdgeDetectionFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageDirectionalSobelEdgeDetectionFilter
	{
	}

	// @interface GPUImageDirectionalNonMaximumSuppressionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageDirectionalNonMaximumSuppressionFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }

		// @property (readwrite, nonatomic) CGFloat upperThreshold;
		[Export ("upperThreshold")]
		nfloat UpperThreshold { get; set; }

		// @property (readwrite, nonatomic) CGFloat lowerThreshold;
		[Export ("lowerThreshold")]
		nfloat LowerThreshold { get; set; }
	}

	// @interface GPUImageWeakPixelInclusionFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageWeakPixelInclusionFilter
	{
	}

	// @interface GPUImageCannyEdgeDetectionFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageCannyEdgeDetectionFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurTexelSpacingMultiplier;
		[Export ("blurTexelSpacingMultiplier")]
		nfloat BlurTexelSpacingMultiplier { get; set; }

		// @property (readwrite, nonatomic) CGFloat upperThreshold;
		[Export ("upperThreshold")]
		nfloat UpperThreshold { get; set; }

		// @property (readwrite, nonatomic) CGFloat lowerThreshold;
		[Export ("lowerThreshold")]
		nfloat LowerThreshold { get; set; }
	}

	// @interface GPUImagePrewittEdgeDetectionFilter : GPUImageSobelEdgeDetectionFilter
	[BaseType (typeof(GPUImageSobelEdgeDetectionFilter))]
	interface GPUImagePrewittEdgeDetectionFilter
	{
	}

	// @interface GPUImageXYDerivativeFilter : GPUImageSobelEdgeDetectionFilter
	[BaseType (typeof(GPUImageSobelEdgeDetectionFilter))]
	interface GPUImageXYDerivativeFilter
	{
	}

	// @interface GPUImageHarrisCornerDetectionFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageHarrisCornerDetectionFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat sensitivity;
		[Export ("sensitivity")]
		nfloat Sensitivity { get; set; }

		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }

		// TODO 'Action<float[], nuint, CMTime> CornersDetectedBlock'
		// @property (copy, nonatomic) void (^cornersDetectedBlock)(GLfloat *, NSUInteger, CMTime);
		[NullAllowed]
		[Export ("cornersDetectedBlock", ArgumentSemantic.Copy)]
		Action<IntPtr, nuint, CMTime> CornersDetectedHandler { get; set; }

		// @property (readonly, nonatomic, strong) NSMutableArray * intermediateImages;
		[Export ("intermediateImages", ArgumentSemantic.Strong)]
		NSMutableArray IntermediateImages { get; }

		// -(id)initWithCornerDetectionFragmentShader:(NSString *)cornerDetectionFragmentShader;
		[Export ("initWithCornerDetectionFragmentShader:")]
		IntPtr Constructor (string cornerDetectionFragmentShader);
	}

	// @interface GPUImageNobleCornerDetectionFilter : GPUImageHarrisCornerDetectionFilter
	[BaseType (typeof(GPUImageHarrisCornerDetectionFilter))]
	interface GPUImageNobleCornerDetectionFilter
	{
	}

	// @interface GPUImageShiTomasiFeatureDetectionFilter : GPUImageHarrisCornerDetectionFilter
	[BaseType (typeof(GPUImageHarrisCornerDetectionFilter))]
	interface GPUImageShiTomasiFeatureDetectionFilter
	{
	}

	// @interface GPUImageFASTCornerDetectionFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageFASTCornerDetectionFilter
	{
		// -(id)initWithFASTDetectorVariant:(GPUImageFASTDetectorType)detectorType;
		[Export ("initWithFASTDetectorVariant:")]
		IntPtr Constructor (GPUImageFASTDetectorType detectorType);
	}

	// @interface GPUImageNonMaximumSuppressionFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageNonMaximumSuppressionFilter
	{
	}

	// @interface GPUImageThresholdedNonMaximumSuppressionFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageThresholdedNonMaximumSuppressionFilter
	{
		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }

		// -(id)initWithPackedColorspace:(BOOL)inputUsesPackedColorspace;
		[Export ("initWithPackedColorspace:")]
		IntPtr Constructor (bool inputUsesPackedColorspace);
	}

	// @interface GPUImageCrosshairGenerator : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageCrosshairGenerator
	{
		// @property (readwrite, nonatomic) CGFloat crosshairWidth;
		[Export ("crosshairWidth")]
		nfloat CrosshairWidth { get; set; }

		// -(void)setCrosshairColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setCrosshairColorRed:green:blue:")]
		void SetCrosshairColor (float redComponent, float greenComponent, float blueComponent);

		// -(void)renderCrosshairsFromArray:(GLfloat *)crosshairCoordinates count:(NSUInteger)numberOfCrosshairs frameTime:(CMTime)frameTime;
		[Export ("renderCrosshairsFromArray:count:frameTime:")]
		void RenderCrosshairs (IntPtr crosshairCoordinates, nuint numberOfCrosshairs, CMTime frameTime);
	}

	// @interface GPUImageDilationFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageDilationFilter
	{
		// extern NSString *const kGPUImageDilationRadiusOneVertexShaderString;
		[Static]
		[Field ("kGPUImageDilationRadiusOneVertexShaderString", "__Internal")]
		NSString DilationRadiusOneVertexShaderString { get; }

		// extern NSString *const kGPUImageDilationRadiusTwoVertexShaderString;
		[Static]
		[Field ("kGPUImageDilationRadiusTwoVertexShaderString", "__Internal")]
		NSString DilationRadiusTwoVertexShaderString { get; }

		// extern NSString *const kGPUImageDilationRadiusThreeVertexShaderString;
		[Static]
		[Field ("kGPUImageDilationRadiusThreeVertexShaderString", "__Internal")]
		NSString DilationRadiusThreeVertexShaderString { get; }

		// extern NSString *const kGPUImageDilationRadiusFourVertexShaderString;
		[Static]
		[Field ("kGPUImageDilationRadiusFourVertexShaderString", "__Internal")]
		NSString DilationRadiusFourVertexShaderString { get; }


		// -(id)initWithRadius:(NSUInteger)dilationRadius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint dilationRadius);
	}

	// @interface GPUImageRGBDilationFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageRGBDilationFilter
	{
		// -(id)initWithRadius:(NSUInteger)dilationRadius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint dilationRadius);
	}

	// @interface GPUImageErosionFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageErosionFilter
	{
		// -(id)initWithRadius:(NSUInteger)erosionRadius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint erosionRadius);
	}

	// @interface GPUImageRGBErosionFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageRGBErosionFilter
	{
		// -(id)initWithRadius:(NSUInteger)erosionRadius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint erosionRadius);
	}

	// @interface GPUImageOpeningFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageOpeningFilter
	{
		// @property (readwrite, nonatomic) CGFloat verticalTexelSpacing;
		[Export ("verticalTexelSpacing")]
		nfloat VerticalTexelSpacing { get; set; }

		// @property (readwrite, nonatomic) CGFloat horizontalTexelSpacing;
		[Export ("horizontalTexelSpacing")]
		nfloat HorizontalTexelSpacing { get; set; }

		// -(id)initWithRadius:(NSUInteger)radius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint radius);
	}

	// @interface GPUImageRGBOpeningFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageRGBOpeningFilter
	{
		// -(id)initWithRadius:(NSUInteger)radius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint radius);
	}

	// @interface GPUImageClosingFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageClosingFilter
	{
		// @property (readwrite, nonatomic) CGFloat verticalTexelSpacing;
		[Export ("verticalTexelSpacing")]
		nfloat VerticalTexelSpacing { get; set; }

		// @property (readwrite, nonatomic) CGFloat horizontalTexelSpacing;
		[Export ("horizontalTexelSpacing")]
		nfloat HorizontalTexelSpacing { get; set; }

		// -(id)initWithRadius:(NSUInteger)radius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint radius);
	}

	// @interface GPUImageRGBClosingFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageRGBClosingFilter
	{
		// -(id)initWithRadius:(NSUInteger)radius;
		[Export ("initWithRadius:")]
		IntPtr Constructor (nuint radius);
	}

	// @interface GPUImageColorPackingFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageColorPackingFilter
	{
	}

	// @interface GPUImageLocalBinaryPatternFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageLocalBinaryPatternFilter
	{
	}

	// @interface GPUImageLanczosResamplingFilter : GPUImageTwoPassTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoPassTextureSamplingFilter))]
	interface GPUImageLanczosResamplingFilter
	{
		// @property (readwrite, nonatomic) CGSize originalImageSize;
		[Export ("originalImageSize", ArgumentSemantic.Assign)]
		CGSize OriginalImageSize { get; set; }
	}

	// @interface GPUImageLowPassFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageLowPassFilter
	{
		// @property (readwrite, nonatomic) CGFloat filterStrength;
		[Export ("filterStrength")]
		nfloat FilterStrength { get; set; }
	}

	// @interface GPUImageHighPassFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageHighPassFilter
	{
		// @property (readwrite, nonatomic) CGFloat filterStrength;
		[Export ("filterStrength")]
		nfloat FilterStrength { get; set; }
	}

	// @interface GPUImageMotionDetector : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageMotionDetector
	{
		// @property (readwrite, nonatomic) CGFloat lowPassFilterStrength;
		[Export ("lowPassFilterStrength")]
		nfloat LowPassFilterStrength { get; set; }

		// @property (copy, nonatomic) void (^motionDetectionBlock)(CGPoint, CGFloat, CMTime);
		[NullAllowed]
		[Export ("motionDetectionBlock", ArgumentSemantic.Copy)]
		Action<CGPoint, nfloat, CMTime> MotionDetectionHandler { get; set; }
	}

	// @interface GPUImageHoughTransformLineDetector : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageHoughTransformLineDetector
	{
		// @property (readwrite, nonatomic) CGFloat edgeThreshold;
		[Export ("edgeThreshold")]
		nfloat EdgeThreshold { get; set; }

		// @property (readwrite, nonatomic) CGFloat lineDetectionThreshold;
		[Export ("lineDetectionThreshold")]
		nfloat LineDetectionThreshold { get; set; }

		// TODO 'Action<float[], nuint, CMTime> LinesDetectedBlock'
		// @property (copy, nonatomic) void (^linesDetectedBlock)(GLfloat *, NSUInteger, CMTime);
		[NullAllowed]
		[Export ("linesDetectedBlock", ArgumentSemantic.Copy)]
		Action<IntPtr, nuint, CMTime> LinesDetectedHandler { get; set; }

		// @property (readonly, nonatomic, strong) NSMutableArray * intermediateImages;
		[Export ("intermediateImages", ArgumentSemantic.Strong)]
		NSMutableArray IntermediateImages { get; }
	}

	// @interface GPUImageParallelCoordinateLineTransformFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageParallelCoordinateLineTransformFilter
	{
	}

	// @interface GPUImageLineGenerator : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageLineGenerator
	{
		// @property (readwrite, nonatomic) CGFloat lineWidth;
		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }

		// -(void)setLineColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setLineColorRed:green:blue:")]
		void SetLineColor (float redComponent, float greenComponent, float blueComponent);

		// -(void)renderLinesFromArray:(GLfloat *)lineSlopeAndIntercepts count:(NSUInteger)numberOfLines frameTime:(CMTime)frameTime;
		[Internal]
		[Export ("renderLinesFromArray:count:frameTime:")]
		void RenderLines (IntPtr lineSlopeAndIntercepts, nuint numberOfLines, CMTime frameTime);
	}

	// @interface GPUImageMotionBlurFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageMotionBlurFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurSize;
		[Export ("blurSize")]
		nfloat BlurSize { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurAngle;
		[Export ("blurAngle")]
		nfloat BlurAngle { get; set; }
	}

	// @interface GPUImageZoomBlurFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageZoomBlurFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurSize;
		[Export ("blurSize")]
		nfloat BlurSize { get; set; }

		// @property (readwrite, nonatomic) CGPoint blurCenter;
		[Export ("blurCenter", ArgumentSemantic.Assign)]
		CGPoint BlurCenter { get; set; }
	}

	// @interface GPUImageiOSBlurFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageiOSBlurFilter
	{
		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat saturation;
		[Export ("saturation")]
		nfloat Saturation { get; set; }

		// @property (readwrite, nonatomic) CGFloat downsampling;
		[Export ("downsampling")]
		nfloat Downsampling { get; set; }

		// @property (readwrite, nonatomic) CGFloat rangeReductionFactor;
		[Export ("rangeReductionFactor")]
		nfloat RangeReductionFactor { get; set; }
	}
}

namespace GPUImage.Filters.Blends
{
	// @interface GPUImageSourceOverBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageSourceOverBlendFilter
	{
	}

	// @interface GPUImageColorBurnBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageColorBurnBlendFilter
	{
	}

	// @interface GPUImageColorDodgeBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageColorDodgeBlendFilter
	{
	}

	// @interface GPUImageDarkenBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageDarkenBlendFilter
	{
	}

	// @interface GPUImageDifferenceBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageDifferenceBlendFilter
	{
	}

	// @interface GPUImageDissolveBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageDissolveBlendFilter
	{
		// @property (readwrite, nonatomic) CGFloat mix;
		[Export ("mix")]
		nfloat Mix { get; set; }
	}

	// @interface GPUImageExclusionBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageExclusionBlendFilter
	{
	}

	// @interface GPUImageHardLightBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageHardLightBlendFilter
	{
	}

	// @interface GPUImageSoftLightBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageSoftLightBlendFilter
	{
	}

	// @interface GPUImageLightenBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageLightenBlendFilter
	{
	}

	// @interface GPUImageAddBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageAddBlendFilter
	{
	}

	// @interface GPUImageSubtractBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageSubtractBlendFilter
	{
	}

	// @interface GPUImageMultiplyBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageMultiplyBlendFilter
	{
	}

	// @interface GPUImageDivideBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageDivideBlendFilter
	{
	}

	// @interface GPUImageOverlayBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageOverlayBlendFilter
	{
	}

	// @interface GPUImageScreenBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageScreenBlendFilter
	{
	}

	// @interface GPUImageChromaKeyBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageChromaKeyBlendFilter
	{
		// @property (readwrite, nonatomic) CGFloat thresholdSensitivity;
		[Export ("thresholdSensitivity")]
		nfloat ThresholdSensitivity { get; set; }

		// @property (readwrite, nonatomic) CGFloat smoothing;
		[Export ("smoothing")]
		nfloat Smoothing { get; set; }

		// -(void)setColorToReplaceRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent;
		[Export ("setColorToReplaceRed:green:blue:")]
		void SetColorToReplace (float redComponent, float greenComponent, float blueComponent);
	}

	// @interface GPUImageAlphaBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageAlphaBlendFilter
	{
		// @property (readwrite, nonatomic) CGFloat mix;
		[Export ("mix")]
		nfloat Mix { get; set; }
	}

	// @interface GPUImageNormalBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageNormalBlendFilter
	{
	}

	// @interface GPUImageColorBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageColorBlendFilter
	{
	}

	// @interface GPUImageHueBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageHueBlendFilter
	{
	}

	// @interface GPUImageSaturationBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageSaturationBlendFilter
	{
	}

	// @interface GPUImageLuminosityBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageLuminosityBlendFilter
	{
	}

	// @interface GPUImageLinearBurnBlendFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageLinearBurnBlendFilter
	{
	}

	// @interface GPUImagePoissonBlendFilter : GPUImageTwoInputCrossTextureSamplingFilter
	[BaseType (typeof(GPUImageTwoInputCrossTextureSamplingFilter))]
	interface GPUImagePoissonBlendFilter
	{
		// @property (readwrite, nonatomic) CGFloat mix;
		[Export ("mix")]
		nfloat Mix { get; set; }

		// @property (readwrite, nonatomic) NSUInteger numIterations;
		[Export ("numIterations")]
		nuint NumIterations { get; set; }
	}

	// @interface GPUImageMaskFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageMaskFilter
	{
	}
}

namespace GPUImage.Filters.Effects
{
	// @interface GPUImagePerlinNoiseFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePerlinNoiseFilter
	{
		// @property (readwrite, nonatomic) GPUVector4 colorStart;
		[Export ("colorStart", ArgumentSemantic.Assign)]
		GPUVector4 ColorStart { get; set; }

		// @property (readwrite, nonatomic) GPUVector4 colorFinish;
		[Export ("colorFinish", ArgumentSemantic.Assign)]
		GPUVector4 ColorFinish { get; set; }

		// @property (readwrite, nonatomic) float scale;
		[Export ("scale")]
		float Scale { get; set; }
	}

	// @interface GPUImagePixellateFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePixellateFilter
	{
		// @property (readwrite, nonatomic) CGFloat fractionalWidthOfAPixel;
		[Export ("fractionalWidthOfAPixel")]
		nfloat FractionalWidthOfAPixel { get; set; }
	}

	// @interface GPUImagePixellatePositionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePixellatePositionFilter
	{
		// @property (readwrite, nonatomic) CGFloat fractionalWidthOfAPixel;
		[Export ("fractionalWidthOfAPixel")]
		nfloat FractionalWidthOfAPixel { get; set; }

		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }
	}

	// @interface GPUImagePolkaDotFilter : GPUImagePixellateFilter
	[BaseType (typeof(GPUImagePixellateFilter))]
	interface GPUImagePolkaDotFilter
	{
		// @property (readwrite, nonatomic) CGFloat dotScaling;
		[Export ("dotScaling")]
		nfloat DotScaling { get; set; }
	}

	// @interface GPUImageHalftoneFilter : GPUImagePixellateFilter
	[BaseType (typeof(GPUImagePixellateFilter))]
	interface GPUImageHalftoneFilter
	{
	}

	// @interface GPUImagePolarPixellateFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePolarPixellateFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGSize pixelSize;
		[Export ("pixelSize", ArgumentSemantic.Assign)]
		CGSize PixelSize { get; set; }
	}

	// @interface GPUImageCrosshatchFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageCrosshatchFilter
	{
		// @property (readwrite, nonatomic) CGFloat crossHatchSpacing;
		[Export ("crossHatchSpacing")]
		nfloat CrossHatchSpacing { get; set; }

		// @property (readwrite, nonatomic) CGFloat lineWidth;
		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }
	}

	// @interface GPUImageSketchFilter : GPUImageSobelEdgeDetectionFilter
	[BaseType (typeof(GPUImageSobelEdgeDetectionFilter))]
	interface GPUImageSketchFilter
	{
	}

	// @interface GPUImageThresholdSketchFilter : GPUImageThresholdEdgeDetectionFilter
	[BaseType (typeof(GPUImageThresholdEdgeDetectionFilter))]
	interface GPUImageThresholdSketchFilter
	{
	}

	// @interface GPUImageEmbossFilter : GPUImage3x3ConvolutionFilter
	[BaseType (typeof(GPUImage3x3ConvolutionFilter))]
	interface GPUImageEmbossFilter
	{
		// @property (readwrite, nonatomic) CGFloat intensity;
		[Export ("intensity")]
		nfloat Intensity { get; set; }
	}

	// @interface GPUImageToonFilter : GPUImage3x3TextureSamplingFilter
	[BaseType (typeof(GPUImage3x3TextureSamplingFilter))]
	interface GPUImageToonFilter
	{
		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }

		// @property (readwrite, nonatomic) CGFloat quantizationLevels;
		[Export ("quantizationLevels")]
		nfloat QuantizationLevels { get; set; }
	}

	// @interface GPUImageSmoothToonFilter : GPUImageFilterGroup
	[BaseType (typeof(GPUImageFilterGroup))]
	interface GPUImageSmoothToonFilter
	{
		// @property (readwrite, nonatomic) CGFloat texelWidth;
		[Export ("texelWidth")]
		nfloat TexelWidth { get; set; }

		// @property (readwrite, nonatomic) CGFloat texelHeight;
		[Export ("texelHeight")]
		nfloat TexelHeight { get; set; }

		// @property (readwrite, nonatomic) CGFloat blurRadiusInPixels;
		[Export ("blurRadiusInPixels")]
		nfloat BlurRadiusInPixels { get; set; }

		// @property (readwrite, nonatomic) CGFloat threshold;
		[Export ("threshold")]
		nfloat Threshold { get; set; }

		// @property (readwrite, nonatomic) CGFloat quantizationLevels;
		[Export ("quantizationLevels")]
		nfloat QuantizationLevels { get; set; }
	}

	// @interface GPUImageCGAColorspaceFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageCGAColorspaceFilter
	{
	}

	// @interface GPUImagePosterizeFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePosterizeFilter
	{
		// @property (readwrite, nonatomic) NSUInteger colorLevels;
		[Export ("colorLevels")]
		nuint ColorLevels { get; set; }
	}

	// @interface GPUImageSwirlFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageSwirlFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }

		// @property (readwrite, nonatomic) CGFloat angle;
		[Export ("angle")]
		nfloat Angle { get; set; }
	}

	// @interface GPUImageBulgeDistortionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageBulgeDistortionFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }

		// @property (readwrite, nonatomic) CGFloat scale;
		[Export ("scale")]
		nfloat Scale { get; set; }
	}

	// @interface GPUImagePinchDistortionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImagePinchDistortionFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }

		// @property (readwrite, nonatomic) CGFloat scale;
		[Export ("scale")]
		nfloat Scale { get; set; }
	}

	// @interface GPUImageStretchDistortionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageStretchDistortionFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }
	}

	// @interface GPUImageSphereRefractionFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageSphereRefractionFilter
	{
		// @property (readwrite, nonatomic) CGPoint center;
		[Export ("center", ArgumentSemantic.Assign)]
		CGPoint Center { get; set; }

		// @property (readwrite, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }

		// @property (readwrite, nonatomic) CGFloat refractiveIndex;
		[Export ("refractiveIndex")]
		nfloat RefractiveIndex { get; set; }
	}

	// @interface GPUImageGlassSphereFilter : GPUImageSphereRefractionFilter
	[BaseType (typeof(GPUImageSphereRefractionFilter))]
	interface GPUImageGlassSphereFilter
	{
	}

	// @interface GPUImageKuwaharaFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageKuwaharaFilter
	{
		// @property (readwrite, nonatomic) NSUInteger radius;
		[Export ("radius")]
		nuint Radius { get; set; }
	}

	// @interface GPUImageKuwaharaRadius3Filter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageKuwaharaRadius3Filter
	{
	}

	// @interface GPUImageVignetteFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageVignetteFilter
	{
		// @property (readwrite, nonatomic) CGPoint vignetteCenter;
		[Export ("vignetteCenter", ArgumentSemantic.Assign)]
		CGPoint VignetteCenter { get; set; }

		// @property (readwrite, nonatomic) GPUVector3 vignetteColor;
		[Export ("vignetteColor", ArgumentSemantic.Assign)]
		GPUVector3 VignetteColor { get; set; }

		// @property (readwrite, nonatomic) CGFloat vignetteStart;
		[Export ("vignetteStart")]
		nfloat VignetteStart { get; set; }

		// @property (readwrite, nonatomic) CGFloat vignetteEnd;
		[Export ("vignetteEnd")]
		nfloat VignetteEnd { get; set; }
	}

	// @interface GPUImageJFAVoronoiFilter : GPUImageFilter
	[BaseType (typeof(GPUImageFilter))]
	interface GPUImageJFAVoronoiFilter
	{
		// @property (readwrite, nonatomic) CGSize sizeInPixels;
		[Export ("sizeInPixels", ArgumentSemantic.Assign)]
		CGSize SizeInPixels { get; set; }
	}

	// @interface GPUImageMosaicFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageMosaicFilter
	{
		// @property (readwrite, nonatomic) CGSize inputTileSize;
		[Export ("inputTileSize", ArgumentSemantic.Assign)]
		CGSize InputTileSize { get; set; }

		// @property (readwrite, nonatomic) float numTiles;
		[Export ("numTiles")]
		float NumTiles { get; set; }

		// @property (readwrite, nonatomic) CGSize displayTileSize;
		[Export ("displayTileSize", ArgumentSemantic.Assign)]
		CGSize DisplayTileSize { get; set; }

		// @property (readwrite, nonatomic) BOOL colorOn;
		[Export ("colorOn")]
		bool ColorOn { get; set; }

		// @property (readwrite, copy, nonatomic) NSString * tileSet;
		[Export ("tileSet")]
		string TileSet { get; set; }
	}

	// @interface GPUImageVoronoiConsumerFilter : GPUImageTwoInputFilter
	[BaseType (typeof(GPUImageTwoInputFilter))]
	interface GPUImageVoronoiConsumerFilter
	{
		// @property (readwrite, nonatomic) CGSize sizeInPixels;
		[Export ("sizeInPixels", ArgumentSemantic.Assign)]
		CGSize SizeInPixels { get; set; }
	}
}

namespace GPUImage.Outputs
{
	// @interface GPUImageView : UIView <GPUImageInput>
	[BaseType (typeof(UIView))]
	interface GPUImageView : GPUImageInput
	{
		// @property (readwrite, nonatomic) GPUImageFillModeType fillMode;
		[Export ("fillMode", ArgumentSemantic.Assign)]
		GPUImageFillModeType FillMode { get; set; }

		// @property (readonly, nonatomic) CGSize sizeInPixels;
		[Export ("sizeInPixels")]
		CGSize SizeInPixels { get; }

		// @property (nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// -(void)setBackgroundColorRed:(GLfloat)redComponent green:(GLfloat)greenComponent blue:(GLfloat)blueComponent alpha:(GLfloat)alphaComponent;
		[Export ("setBackgroundColorRed:green:blue:alpha:")]
		void SetBackgroundColor (float redComponent, float greenComponent, float blueComponent, float alphaComponent);

		// -(void)setCurrentlyReceivingMonochromeInput:(BOOL)newValue;
		[Export ("setCurrentlyReceivingMonochromeInput:")]
		void SetCurrentlyReceivingMonochromeInput (bool newValue);
	}

	interface IGPUImageMovieWriterDelegate
	{
	}

	// @protocol GPUImageMovieWriterDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GPUImageMovieWriterDelegate
	{
		// @optional -(void)movieRecordingCompleted;
		[Export ("movieRecordingCompleted")]
		void MovieRecordingCompleted ();

		// @optional -(void)movieRecordingFailedWithError:(NSError *)error;
		[Export ("movieRecordingFailedWithError:")]
		void MovieRecordingFailed (NSError error);
	}

	// TODO
	//	public delegate void AudioProcessingCallbackDelegate(ref short[] samples, nint numSamplesInBuffer);

	// @interface GPUImageMovieWriter : NSObject <GPUImageInput>
	[BaseType (typeof(NSObject))]
	interface GPUImageMovieWriter : GPUImageInput
	{
		// extern NSString *const kGPUImageColorSwizzlingFragmentShaderString;
		[Static]
		[Field ("kGPUImageColorSwizzlingFragmentShaderString", "__Internal")]
		NSString ColorSwizzlingFragmentShaderString { get; }


		// @property (readwrite, nonatomic) BOOL hasAudioTrack;
		[Export ("hasAudioTrack")]
		bool HasAudioTrack { get; set; }

		// @property (readwrite, nonatomic) BOOL shouldPassthroughAudio;
		[Export ("shouldPassthroughAudio")]
		bool ShouldPassthroughAudio { get; set; }

		// @property (readwrite, nonatomic) BOOL shouldInvalidateAudioSampleWhenDone;
		[Export ("shouldInvalidateAudioSampleWhenDone")]
		bool ShouldInvalidateAudioSampleWhenDone { get; set; }

		// @property (copy, nonatomic) void (^completionBlock)();
		[NullAllowed]
		[Export ("completionBlock", ArgumentSemantic.Copy)]
		Action CompletionHandler { get; set; }

		// @property (copy, nonatomic) void (^failureBlock)(NSError *);
		[NullAllowed]
		[Export ("failureBlock", ArgumentSemantic.Copy)]
		Action<NSError> FailureHandler { get; set; }

		// @property (assign, nonatomic) id<GPUImageMovieWriterDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IGPUImageMovieWriterDelegate Delegate { get; set; }

		// @property (readwrite, nonatomic) BOOL encodingLiveVideo;
		[Export ("encodingLiveVideo")]
		bool EncodingLiveVideo { get; set; }

		// @property (copy, nonatomic) BOOL (^videoInputReadyCallback)();
		[NullAllowed]
		[Export ("videoInputReadyCallback", ArgumentSemantic.Copy)]
		Func<bool> VideoInputReadyCallback { get; set; }

		// @property (copy, nonatomic) BOOL (^audioInputReadyCallback)();
		[NullAllowed]
		[Export ("audioInputReadyCallback", ArgumentSemantic.Copy)]
		Func<bool> AudioInputReadyCallback { get; set; }

		// TODO
		//		// @property (copy, nonatomic) void (^audioProcessingCallback)(SInt16 **, CMItemCount);
		//		[NullAllowed]
		//		[Export ("audioProcessingCallback", ArgumentSemantic.Copy)]
		//		AudioProcessingCallbackDelegate AudioProcessingCallback { get; set; }

		// @property (nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// @property (readonly, nonatomic) AVAssetWriter * assetWriter;
		[Export ("assetWriter")]
		AVAssetWriter AssetWriter { get; }

		// @property (readonly, nonatomic) CMTime duration;
		[Export ("duration")]
		CMTime Duration { get; }

		// @property (assign, nonatomic) CGAffineTransform transform;
		[Export ("transform", ArgumentSemantic.Assign)]
		CGAffineTransform Transform { get; set; }

		// @property (copy, nonatomic) NSArray * metaData;
		[NullAllowed]
		[Export ("metaData", ArgumentSemantic.Copy)]
		AVMetadataItem[] MetaData { get; set; }

		// @property (getter = isPaused, assign, nonatomic) BOOL paused;
		[Export ("paused")]
		bool Paused { [Bind ("isPaused")] get; set; }

		// @property (retain, nonatomic) GPUImageContext * movieWriterContext;
		[NullAllowed]
		[Export ("movieWriterContext", ArgumentSemantic.Retain)]
		GPUImageContext MovieWriterContext { get; set; }

		// -(id)initWithMovieURL:(NSURL *)newMovieURL size:(CGSize)newSize;
		[Export ("initWithMovieURL:size:")]
		IntPtr Constructor (NSUrl newMovieURL, CGSize newSize);

		// -(id)initWithMovieURL:(NSURL *)newMovieURL size:(CGSize)newSize fileType:(NSString *)newFileType outputSettings:(NSDictionary *)outputSettings;
		[Export ("initWithMovieURL:size:fileType:outputSettings:")]
		IntPtr Constructor (NSUrl newMovieURL, CGSize newSize, string newFileType, NSDictionary outputSettings);

		// -(void)setHasAudioTrack:(BOOL)hasAudioTrack audioSettings:(NSDictionary *)audioOutputSettings;
		[Export ("setHasAudioTrack:audioSettings:")]
		void SetHasAudioTrack (bool hasAudioTrack, NSDictionary audioOutputSettings);

		// -(void)startRecording;
		[Export ("startRecording")]
		void StartRecording ();

		// -(void)startRecordingInOrientation:(CGAffineTransform)orientationTransform;
		[Export ("startRecordingInOrientation:")]
		void StartRecording (CGAffineTransform orientationTransform);

		// -(void)finishRecording;
		[Export ("finishRecording")]
		void FinishRecording ();

		// -(void)finishRecordingWithCompletionHandler:(void (^)(void))handler;
		[Async]
		[Export ("finishRecordingWithCompletionHandler:")]
		void FinishRecording (Action handler);

		// -(void)cancelRecording;
		[Export ("cancelRecording")]
		void CancelRecording ();

		// -(void)processAudioBuffer:(CMSampleBufferRef)audioBuffer;
		[Export ("processAudioBuffer:")]
		void ProcessAudioBuffer (CMSampleBuffer audioBuffer);

		// -(void)enableSynchronizationCallbacks;
		[Export ("enableSynchronizationCallbacks")]
		void EnableSynchronizationCallbacks ();
	}

	interface IGPUImageTextureOutputDelegate
	{
	}

	// @protocol GPUImageTextureOutputDelegate
	[Protocol, Model]
	interface GPUImageTextureOutputDelegate
	{
		// @required -(void)newFrameReadyFromTextureOutput:(GPUImageTextureOutput *)callbackTextureOutput;
		[Abstract]
		[Export ("newFrameReadyFromTextureOutput:")]
		void NewFrameReadyFromTextureOutput (GPUImageTextureOutput callbackTextureOutput);
	}

	// @interface GPUImageTextureOutput : NSObject <GPUImageInput>
	[BaseType (typeof(NSObject))]
	interface GPUImageTextureOutput : GPUImageInput
	{
		// @property (readwrite, nonatomic, unsafe_unretained) id<GPUImageTextureOutputDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Assign)]
		IGPUImageTextureOutputDelegate Delegate { get; set; }

		// @property (readonly) GLuint texture;
		[Export ("texture")]
		uint Texture { get; }

		// @property (nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// -(void)doneWithTexture;
		[Export ("doneWithTexture")]
		void DoneWithTexture ();
	}

	// @interface GPUImageRawDataOutput : NSObject <GPUImageInput>
	[BaseType (typeof(NSObject))]
	interface GPUImageRawDataOutput : GPUImageInput
	{
		// @property (readonly) GLubyte * rawBytesForImage;
		[Export ("rawBytesForImage")]
		IntPtr RawBytesForImage { get; }

		// @property (copy, nonatomic) void (^newFrameAvailableBlock)();
		[Export ("newFrameAvailableBlock", ArgumentSemantic.Copy)]
		Action NewFrameAvailableHandler { get; set; }

		// @property (nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// -(id)initWithImageSize:(CGSize)newImageSize resultsInBGRAFormat:(BOOL)resultsInBGRAFormat;
		[Export ("initWithImageSize:resultsInBGRAFormat:")]
		IntPtr Constructor (CGSize newImageSize, bool resultsInBGRAFormat);

		// -(GPUByteColorVector)colorAtLocation:(CGPoint)locationInImage;
		[Export ("colorAtLocation:")]
		GPUByteColorVector ColorAtLocation (CGPoint locationInImage);

		// -(NSUInteger)bytesPerRowInOutput;
		[Export ("bytesPerRowInOutput")]
		nuint BytesPerRowInOutput { get; }

		// -(void)setImageSize:(CGSize)newImageSize;
		[Export ("setImageSize:")]
		void SetImageSize (CGSize newImageSize);

		// -(void)lockFramebufferForReading;
		[Export ("lockFramebufferForReading")]
		void LockFramebufferForReading ();

		// -(void)unlockFramebufferAfterReading;
		[Export ("unlockFramebufferAfterReading")]
		void UnlockFramebufferAfterReading ();
	}
}
