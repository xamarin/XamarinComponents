using System;
using System.ComponentModel;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
using nuint = System.UInt32;
#endif

namespace FXBlur
{
	// @interface FXBlurView (UIImage)
	[Category]
	[BaseType (typeof(UIImage))]
	interface UIImageExtensions
	{
		// -(UIImage *)blurredImageWithRadius:(CGFloat)radius iterations:(NSUInteger)iterations tintColor:(UIColor *)tintColor;
		[Export ("blurredImageWithRadius:iterations:tintColor:")]
		UIImage CreateBlurredImage (nfloat radius, nuint iterations, UIColor tintColor);
	}

	// @interface FXBlurView : UIView
	[BaseType (typeof(UIView))]
	interface FXBlurView
	{
		// +(void)setBlurEnabled:(BOOL)blurEnabled;
		[Static]
		[Export ("setBlurEnabled:")]
		void SetBlurEnabled (bool blurEnabled);

		// +(void)setUpdatesEnabled;
		[Static]
		[Export ("setUpdatesEnabled")]
		void SetUpdatesEnabled ();

		// +(void)setUpdatesDisabled;
		[Static]
		[Export ("setUpdatesDisabled")]
		void SetUpdatesDisabled ();

		// @property (getter = isBlurEnabled, nonatomic) BOOL blurEnabled;
		[Export ("blurEnabled")]
		bool BlurEnabled { [Bind ("isBlurEnabled")] get; set; }

		// @property (getter = isDynamic, nonatomic) BOOL dynamic;
		[Export ("dynamic")]
		bool Dynamic { [Bind ("isDynamic")] get; set; }

		// @property (assign, nonatomic) NSUInteger iterations;
		[Export ("iterations")]
		nuint Iterations { get; set; }

		// @property (assign, nonatomic) NSTimeInterval updateInterval;
		[Export ("updateInterval")]
		double UpdateInterval { get; set; }

		// @property (assign, nonatomic) CGFloat blurRadius;
		[Export ("blurRadius")]
		nfloat BlurRadius { get; set; }

		// @property (nonatomic, strong) UIColor * tintColor;
		[Override]
		[Export ("tintColor", ArgumentSemantic.Strong)]
		UIColor TintColor { get; set; }

		// @property (nonatomic, unsafe_unretained) UIView * underlyingView __attribute__((iboutlet));
		[Export ("underlyingView", ArgumentSemantic.Assign)]
		UIView UnderlyingView { get; set; }

		// -(void)updateAsynchronously:(BOOL)async completion:(void (^)())completion;
		[Export ("updateAsynchronously:completion:")]
		void UpdateAsynchronously (bool async, Action completion);

		// -(void)clearImage;
		[Export ("clearImage")]
		void ClearImage ();

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}
}
