using System;
using CoreGraphics;
using Foundation;
using GLKit;
using ObjCRuntime;
using OpenGLES;
using UIKit;
using OpenTK;

namespace Google.VR
{
	// @interface GVRWidgetView : UIView
	[BaseType (typeof(UIView))]
	interface GVRWidgetView
	{
		// @property (nonatomic, weak) id<GVRWidgetViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IGVRWidgetViewDelegate Delegate { get; set; }

		// @property (nonatomic) BOOL enableFullscreenButton;
		[Export ("enableFullscreenButton")]
		bool EnableFullscreenButton { get; set; }

		// @property (nonatomic) BOOL enableCardboardButton;
		[Export ("enableCardboardButton")]
		bool EnableCardboardButton { get; set; }

		// @property (nonatomic) BOOL enableTouchTracking;
		[Export("enableTouchTracking")]
		bool EnableTouchTracking { get; set; }

		// @property(nonatomic, readonly) GVRHeadRotation headRotation;
		[Export("headRotation")]
		GVRHeadRotation HeadRotation { get; }

		// @property(nonatomic) GVRWidgetDisplayMode displayMode;
		[Export("displayMode")]
		GVRWidgetDisplayMode DisplayMode { get; set; }
	}

    interface IGVRWidgetViewDelegate { }

	// @protocol GVRWidgetViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GVRWidgetViewDelegate
	{
		// @optional -(void)widgetViewDidTap:(GVRWidgetView *)widgetView;
		[Export ("widgetViewDidTap:")]
		void WidgetViewDidTap (GVRWidgetView widgetView);

		// @optional -(void)widgetView:(GVRWidgetView *)widgetView didChangeDisplayMode:(GVRWidgetDisplayMode)displayMode;
		[Export ("widgetView:didChangeDisplayMode:")]
		void WidgetView (GVRWidgetView widgetView, GVRWidgetDisplayMode displayMode);

		// @optional -(void)widgetView:(GVRWidgetView *)widgetView didLoadContent:(id)content;
		[Export ("widgetView:didLoadContent:")]
		void WidgetView (GVRWidgetView widgetView, NSObject content);

		// @optional -(void)widgetView:(GVRWidgetView *)widgetView didFailToLoadContent:(id)content withErrorMessage:(NSString *)errorMessage;
		[Export ("widgetView:didFailToLoadContent:withErrorMessage:")]
		void WidgetView (GVRWidgetView widgetView, NSObject content, string errorMessage);
	}

	// @interface GVRAudioEngine : NSObject
	[BaseType (typeof(NSObject))]
	interface GVRAudioEngine
	{
		// -(id)initWithRenderingMode:(renderingMode)rendering_mode;
		[Export ("initWithRenderingMode:")]
		IntPtr Constructor (GVRRenderingMode rendering_mode);

		// -(_Bool)start;
		[Export ("start")]
		bool Start { get; }

		// -(void)stop;
		[Export ("stop")]
		void Stop ();

		// -(void)update;
		[Export ("update")]
		void Update ();

		// -(void)enableStereoSpeakerMode:(bool)enable;
		[Export ("enableStereoSpeakerMode:")]
		void EnableStereoSpeakerMode(bool enable);

		// -(_Bool)preloadSoundFile:(const NSString *)filename;
		[Export ("preloadSoundFile:")]
		bool PreloadSoundFile (string filename);

		// -(void)unloadSoundFile:(const NSString*)filename;
		[Export("unloadSoundFile:")]
		void UnloadSoundFile(string filename);

		// -(int)createSoundObject:(const NSString *)filename;
		[Export ("createSoundObject:")]
		int CreateSoundObject (string filename);

		// -(int)createSoundfield:(const NSString *)filename;
		[Export ("createSoundfield:")]
		int CreateSoundfield (string filename);

		// -(int)createStereoSound:(const NSString*)filename;
		[Export("createStereoSound:")]
		int CreateStereoSound(string filename);

		// -(void)playSound:(int)soundId loopingEnabled:(_Bool)loopingEnabled;
		[Export ("playSound:loopingEnabled:")]
		void PlaySound (int soundId, bool loopingEnabled);

		// - (void)pauseSound:(int)sourceId;
		[Export("pauseSound:")]
		void PauseSound(int soundId);

		// -(void)resumeSound:(int)sourceId;
		[Export("resumeSound:")]
		void ResumeSound(int soundId);

		// -(void)stopSound:(int)soundId;
		[Export ("stopSound:")]
		void StopSound (int soundId);

		// -(void)setSoundObjectPosition:(int)soundObjectId x:(float)x y:(float)y z:(float)z;
		[Export ("setSoundObjectPosition:x:y:z:")]
		void SetSoundObjectPosition (int soundObjectId, float x, float y, float z);

		// -(void)setSoundObjectDistanceRolloffModel:(int)soundObjectId rolloffModel:(distanceRolloffModel)rolloffModel minDistance:(float)minDistance maxDistance:(float)maxDistance;
		[Export("setSoundObjectDistanceRolloffModel:rolloffModel:minDistance:maxDistance:")]
		void SetSoundObjectDistanceRolloffModel(int soundObjectId, GVRDistanceRolloffModel rolloffModel, float minDistance, float maxDistance);

		// -(void)setSoundfieldRotation:(int)soundfieldId x:(float)x y:(float)y z:(float)z w:(float)w;
		[Export("setSoundfieldRotation:x:y:z:w:")]
		void SetSoundfieldRotation(int soundObjectId, float x, float y, float z, float w);

		// -(void)setSoundVolume:(int)soundId volume:(float)volume;
		[Export ("setSoundVolume:volume:")]
		void SetSoundVolume (int soundId, float volume);

		// -(_Bool)isSoundPlaying:(int)soundId;
		[Export ("isSoundPlaying:")]
		bool IsSoundPlaying (int soundId);

		// -(void)setHeadPosition:(float)x y:(float)y z:(float)z;
		[Export ("setHeadPosition:y:z:")]
		void SetHeadPosition (float x, float y, float z);

		// -(void)setHeadRotation:(float)x y:(float)y z:(float)z w:(float)w;
		[Export ("setHeadRotation:y:z:w:")]
		void SetHeadRotation (float x, float y, float z, float w);

		// -(void)enableRoom:(_Bool)enable;
		[Export ("enableRoom:")]
		void EnableRoom (bool enable);

		// -(void)setRoomProperties:(float)size_x size_y:(float)size_y size_z:(float)size_z wall_material:(materialName)wall_material ceiling_material:(materialName)ceiling_material floor_material:(materialName)floor_material;
		[Export ("setRoomProperties:size_y:size_z:wall_material:ceiling_material:floor_material:")]
		void SetRoomProperties (float size_x, float size_y, float size_z, GVRMaterialName wall_material, GVRMaterialName ceiling_material, GVRMaterialName floor_material);

		// -(void)setRoomReverbAdjustments:(float)gain timeAdjust:(float)timeAdjust brightnessAdjust:(float)brightnessAdjust;
		[Export("setRoomReverbAdjustments:timeAdjust:brightnessAdjust:")]
		void SetRoomReverbAdjustments(float gain, float timeAdjust, float brightnessAdjust);
	}

	// @interface GVRHeadTransform : NSObject
	[BaseType (typeof(NSObject))]
	interface GVRHeadTransform
	{
		// -(CGRect)viewportForEye:(GVREye)eye;
		[Export ("viewportForEye:")]
		CGRect ViewportForEye (GVREye eye);

		// -(GLKMatrix4)projectionMatrixForEye:(GVREye)eye near:(CGFloat)near far:(CGFloat)far;
		[Export ("projectionMatrixForEye:near:far:")]
		Matrix4 ProjectionMatrixForEye (GVREye eye, nfloat near, nfloat far);

		// -(GLKMatrix4)eyeFromHeadMatrix:(GVREye)eye;
		[Export ("eyeFromHeadMatrix:")]
		Matrix4 EyeFromHeadMatrix (GVREye eye);

		// -(GLKMatrix4)headPoseInStartSpace;
		[Export ("headPoseInStartSpace")]
		Matrix4 HeadPoseInStartSpace { get; }

		// -(GVRFieldOfView)fieldOfViewForEye:(GVREye)eye;
		[Export ("fieldOfViewForEye:")]
		GVRFieldOfView FieldOfViewForEye (GVREye eye);
	}

    interface IGVRCardboardViewDelegate { }

	// @protocol GVRCardboardViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface GVRCardboardViewDelegate
	{
		// @optional -(void)cardboardView:(GVRCardboardView *)cardboardView didFireEvent:(GVRUserEvent)event;
		[Export ("cardboardView:didFireEvent:")]
		void DidFireEvent (GVRCardboardView cardboardView, GVRUserEvent @event);

		// @optional -(void)cardboardView:(GVRCardboardView *)cardboardView willStartDrawing:(GVRHeadTransform *)headTransform;
		[Export ("cardboardView:willStartDrawing:")]
		void WillStartDrawing (GVRCardboardView cardboardView, GVRHeadTransform headTransform);

		// @optional -(void)cardboardView:(GVRCardboardView *)cardboardView prepareDrawFrame:(GVRHeadTransform *)headTransform;
		[Export ("cardboardView:prepareDrawFrame:")]
		void PrepareDrawFrame (GVRCardboardView cardboardView, GVRHeadTransform headTransform);

		// @required -(void)cardboardView:(GVRCardboardView *)cardboardView drawEye:(GVREye)eye withHeadTransform:(GVRHeadTransform *)headTransform;
		[Abstract]
		[Export ("cardboardView:drawEye:withHeadTransform:")]
		void DrawEye (GVRCardboardView cardboardView, GVREye eye, GVRHeadTransform headTransform);

		// @optional -(void)cardboardView:(GVRCardboardView *)cardboardView shouldPauseDrawing:(BOOL)pause;
		[Export ("cardboardView:shouldPauseDrawing:")]
		void ShouldPauseDrawing (GVRCardboardView cardboardView, bool pause);
	}

	// @interface GVRCardboardView : UIView
	[BaseType (typeof(UIView))]
	interface GVRCardboardView
	{
		// -(instancetype)initWithFrame:(CGRect)frame __attribute__((objc_designated_initializer));
		[Export ("initWithFrame:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGRect frame);

		// @property (nonatomic, weak) id<GVRCardboardViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IGVRCardboardViewDelegate Delegate { get; set; }

		// @property (nonatomic) EAGLContext * context;
		[Export ("context", ArgumentSemantic.Assign)]
		EAGLContext Context { get; set; }

		// @property (nonatomic) BOOL vrModeEnabled;
		[Export ("vrModeEnabled")]
		bool VrModeEnabled { get; set; }

		// -(void)render;
		[Export ("render")]
		void Render ();
	}

	// @interface GVRPanoramaView : GVRWidgetView
	[BaseType (typeof(GVRWidgetView))]
	interface GVRPanoramaView
	{
		// -(void)loadImage:(UIImage *)image;
		[Export ("loadImage:")]
		void LoadImage (UIImage image);

		// -(void)loadImage:(UIImage *)image ofType:(GVRPanoramaImageType)imageType;
		[Export ("loadImage:ofType:")]
		void LoadImage (UIImage image, GVRPanoramaImageType imageType);
	}

	// @interface GVRVideoView : GVRWidgetView
	[BaseType (typeof(GVRWidgetView))]
	interface GVRVideoView
	{
		// -(void)loadFromUrl:(NSURL *)videoUrl;
		[Export ("loadFromUrl:")]
		void LoadFromUrl (NSUrl videoUrl);

		// -(void)loadFromUrl:(NSURL*)videoUrl ofType:(GVRVideoType)videoType;
		[Export("loadFromUrl:ofType:")]
		void LoadFromUrl(NSUrl videoUrl, GVRPanoramaVideoType videoType);

		// -(void)pause;
		[Export ("pause")]
		void Pause ();

		// -(void)resume;
		[Export ("resume")]
		void Resume ();

		// -(void)stop;
		[Export ("stop")]
		void Stop ();

		// -(NSTimeInterval)duration;
		[Export ("duration")]
		double Duration { get; }

		// -(void)seekTo:(NSTimeInterval)position;
		[Export ("seekTo:")]
		void SeekTo (double position);

		// @property(nonatomic) float volume;
		[Export("volume", ArgumentSemantic.Assign)]
		float Volume { get; set; }
	}

	interface IGVRVideoViewDelegate { }

	// @protocol GVRVideoViewDelegate <GVRWidgetViewDelegate>
	[Protocol, Model]
	[BaseType (typeof(GVRWidgetViewDelegate))]
	interface GVRVideoViewDelegate
	{
		// @required -(void)videoView:(GVRVideoView *)videoView didUpdatePosition:(NSTimeInterval)position;
		[Abstract]
		[Export ("videoView:didUpdatePosition:")]
		void DidUpdatePosition (GVRVideoView videoView, double position);
	}

	interface IGVROverlayViewDelegate { }

	// @protocol GVROverlayViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface GVROverlayViewDelegate
	{
		// @required -(void)didTapBackButton;
		[Abstract]
		[Export("didTapBackButton")]
		void DidTapBackButton();

		// @optional -(void)didTapCardboardButton;
		[Export("didTapCardboardButton")]
		void DidTapCardboardButton();

		// @optional -(UIViewController *)presentingViewControllerForSettingsDialog;
		[Export("presentingViewControllerForSettingsDialog")]
		UIViewController PresentingViewControllerForSettingsDialog();

		// @optional -(void)didPresentSettingsDialog:(BOOL)presented;
		[Export("didPresentSettingsDialog:")]
		void DidPresentSettingsDialog(bool presented);

		// @optional -(void)didChangeViewerProfile;
		[Export("didChangeViewerProfile")]
		void DidChangeViewerProfile();

		// @optional -(void)shouldDisableIdleTimer:(BOOL)shouldDisable;
		[Export("shouldDisableIdleTimer:")]
		void ShouldDisableIdleTimer(bool shouldDisable);
	}

	// @interface GVROverlayView : UIView
	[BaseType(typeof(UIView))]
	interface GVROverlayView
	{
		// @property (nonatomic, weak) id<GVROverlayViewDelegate> _Nullable delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		IGVROverlayViewDelegate Delegate { get; set; }
	}


}

