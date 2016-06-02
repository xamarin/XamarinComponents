using System;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace SCCatWaitingHUD
{
	// @interface SCCatWaitingHUD : UIView
	[BaseType (typeof(UIView), Name = "SCCatWaitingHUD")]
	[DisableDefaultCtor]
	interface CatWaitingHUD
	{
		// @property (nonatomic, strong) UIWindow * backgroundWindow;
		[Export ("backgroundWindow", ArgumentSemantic.Strong)]
		UIWindow BackgroundWindow { get; set; }

		// @property (nonatomic, strong) UIVisualEffectView * blurView;
		[Export ("blurView", ArgumentSemantic.Strong)]
		UIVisualEffectView BlurView { get; set; }

		// @property (nonatomic, strong) UIView * indicatorView;
		[Export ("indicatorView", ArgumentSemantic.Strong)]
		UIView IndicatorView { get; set; }

		// @property (nonatomic, strong) UIImageView * faceView;
		[Export ("faceView", ArgumentSemantic.Strong)]
		UIImageView FaceView { get; set; }

		// @property (nonatomic, strong) UIImageView * mouseView;
		[Export ("mouseView", ArgumentSemantic.Strong)]
		UIImageView MouseView { get; set; }

		// @property (nonatomic, strong) UILabel * contentLabel;
		[Export ("contentLabel", ArgumentSemantic.Strong)]
		UILabel ContentLabel { get; set; }

		// @property (nonatomic) BOOL isAnimating;
		[Export ("isAnimating")]
		bool IsAnimating { get; set; }

		// @property (nonatomic, strong) NSString * title;
		[Export ("title", ArgumentSemantic.Strong)]
		string Title { get; set; }

		// +(SCCatWaitingHUD *)sharedInstance;
		[Static]
		[Export ("sharedInstance")]
		CatWaitingHUD SharedInstance { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// -(void)animate;
		[Export ("animate")]
		void Start ();

		// -(void)animateWithInteractionEnabled:(BOOL)enabled;
		[Export ("animateWithInteractionEnabled:")]
		void Start (bool interactionEnabled);

		// -(void)animateWithInteractionEnabled:(BOOL)enabled title:(NSString *)title;
		[Export ("animateWithInteractionEnabled:title:")]
		void Start (bool interactionEnabled, string title);

		// -(void)animateWithInteractionEnabled:(BOOL)enabled title:(NSString *)title duration:(CGFloat)duration;
		[Export ("animateWithInteractionEnabled:title:duration:")]
		void Start (bool interactionEnabled, string title, nfloat duration);

		// -(void)stop;
		[Export ("stop")]
		void Stop ();
	}
}
