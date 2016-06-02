using System;

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
#endif

namespace JDStatusBarNotification
{
	[Static]
	interface StatusBarStyles
	{
		// extern NSString *const JDStatusBarStyleError;
		[Field ("JDStatusBarStyleError", "__Internal")]
		NSString Error { get; }

		// extern NSString *const JDStatusBarStyleWarning;
		[Field ("JDStatusBarStyleWarning", "__Internal")]
		NSString Warning { get; }

		// extern NSString *const JDStatusBarStyleSuccess;
		[Field ("JDStatusBarStyleSuccess", "__Internal")]
		NSString Success { get; }

		// extern NSString *const JDStatusBarStyleMatrix;
		[Field ("JDStatusBarStyleMatrix", "__Internal")]
		NSString Matrix { get; }

		// extern NSString *const JDStatusBarStyleDefault;
		[Field ("JDStatusBarStyleDefault", "__Internal")]
		NSString Default { get; }

		// extern NSString *const JDStatusBarStyleDark;
		[Field ("JDStatusBarStyleDark", "__Internal")]
		NSString Dark { get; }
	}

	// @interface JDStatusBarStyle : NSObject <NSCopying>
	[BaseType (typeof(NSObject), Name = "JDStatusBarStyle")]
	interface StatusBarStyle : INSCopying
	{
		// @property (nonatomic, strong) UIColor * barColor;
		[Export ("barColor", ArgumentSemantic.Strong)]
		UIColor BarColor { get; set; }

		// @property (nonatomic, strong) UIColor * textColor;
		[Export ("textColor", ArgumentSemantic.Strong)]
		UIColor TextColor { get; set; }

		// @property (nonatomic, strong) NSShadow * textShadow;
		[Export ("textShadow", ArgumentSemantic.Strong)]
		NSShadow TextShadow { get; set; }

		// @property (nonatomic, strong) UIFont * font;
		[Export ("font", ArgumentSemantic.Strong)]
		UIFont Font { get; set; }

		// @property (assign, nonatomic) CGFloat textVerticalPositionAdjustment;
		[Export ("textVerticalPositionAdjustment")]
		nfloat TextVerticalPositionAdjustment { get; set; }

		// @property (assign, nonatomic) JDStatusBarAnimationType animationType;
		[Export ("animationType", ArgumentSemantic.Assign)]
		StatusBarAnimationType AnimationType { get; set; }

		// @property (nonatomic, strong) UIColor * progressBarColor;
		[Export ("progressBarColor", ArgumentSemantic.Strong)]
		UIColor ProgressBarColor { get; set; }

		// @property (assign, nonatomic) CGFloat progressBarHeight;
		[Export ("progressBarHeight")]
		nfloat ProgressBarHeight { get; set; }

		// @property (assign, nonatomic) JDStatusBarProgressBarPosition progressBarPosition;
		[Export ("progressBarPosition", ArgumentSemantic.Assign)]
		StatusBarProgressBarPosition ProgressBarPosition { get; set; }
	}

	// @interface JDStatusBarView : UIView
	[BaseType (typeof(UIView), Name = "JDStatusBarView")]
	interface StatusBarView
	{
		// @property (readonly, nonatomic, strong) UILabel * textLabel;
		[Export ("textLabel", ArgumentSemantic.Strong)]
		UILabel TextLabel { get; }

		// @property (readonly, nonatomic, strong) UIActivityIndicatorView * activityIndicatorView;
		[Export ("activityIndicatorView", ArgumentSemantic.Strong)]
		UIActivityIndicatorView ActivityIndicatorView { get; }

		// @property (assign, nonatomic) CGFloat textVerticalPositionAdjustment;
		[Export ("textVerticalPositionAdjustment")]
		nfloat TextVerticalPositionAdjustment { get; set; }
	}

	// typedef JDStatusBarStyle * (^JDPrepareStyleBlock)(JDStatusBarStyle *);
	delegate StatusBarStyle PrepareStyleDelegate (StatusBarStyle style);

	// @interface JDStatusBarNotification : NSObject
	[BaseType (typeof(NSObject), Name = "JDStatusBarNotification")]
	interface StatusBarNotification
	{
		// +(JDStatusBarView *)showWithStatus:(NSString *)status;
		[Static]
		[Export ("showWithStatus:")]
		StatusBarView Show (string status);

		// +(JDStatusBarView *)showWithStatus:(NSString *)status styleName:(NSString *)styleName;
		[Static]
		[Export ("showWithStatus:styleName:")]
		StatusBarView Show (string status, string styleName);

		// +(JDStatusBarView *)showWithStatus:(NSString *)status dismissAfter:(NSTimeInterval)timeInterval;
		[Static]
		[Export ("showWithStatus:dismissAfter:")]
		StatusBarView Show (string status, double timeInterval);

		// +(JDStatusBarView *)showWithStatus:(NSString *)status dismissAfter:(NSTimeInterval)timeInterval styleName:(NSString *)styleName;
		[Static]
		[Export ("showWithStatus:dismissAfter:styleName:")]
		StatusBarView Show (string status, double timeInterval, string styleName);

		// +(void)dismiss;
		[Static]
		[Export ("dismiss")]
		void Dismiss ();

		// +(void)dismissAnimated:(BOOL)animated;
		[Static]
		[Export ("dismissAnimated:")]
		void Dismiss (bool animated);

		// +(void)dismissAfter:(NSTimeInterval)delay;
		[Static]
		[Export ("dismissAfter:")]
		void Dismiss (double delay);

		// +(void)setDefaultStyle:(JDPrepareStyleBlock)prepareBlock;
		[Static]
		[Export ("setDefaultStyle:")]
		void SetDefaultStyle (PrepareStyleDelegate prepareHandler);

		// +(NSString *)addStyleNamed:(NSString *)identifier prepare:(JDPrepareStyleBlock)prepareBlock;
		[Static]
		[Export ("addStyleNamed:prepare:")]
		string AddStyle (string identifier, PrepareStyleDelegate prepareHandler);

		// +(void)showProgress:(CGFloat)progress;
		[Static]
		[Export ("showProgress:")]
		void ShowProgress (nfloat progress);

		// +(void)showActivityIndicator:(BOOL)show indicatorStyle:(UIActivityIndicatorViewStyle)style;
		[Static]
		[Export ("showActivityIndicator:indicatorStyle:")]
		void ShowActivityIndicator (bool show, UIActivityIndicatorViewStyle style);

		// +(BOOL)isVisible;
		[Static]
		[Export ("isVisible")]
		bool IsVisible { get; }
	}
}
