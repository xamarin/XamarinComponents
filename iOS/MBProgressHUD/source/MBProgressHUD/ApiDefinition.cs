using System;
using System.Drawing;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreFoundation;
using CoreGraphics;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreFoundation;
using CGSize = global::System.Drawing.SizeF;
using nuint = global::System.UInt32;
#endif

namespace MBProgressHUD
{
	// typedef void (^MBProgressHUDCompletionBlock)();
	delegate void MBProgressHUDCompletionHandler();
	delegate void NSDispatchHandlerT();

	// @interface MBProgressHUD : UIView
	[BaseType (typeof (UIView), Name="MBProgressHUD",
	Delegates=new string [] {"WeakDelegate"},
	Events=new Type [] { typeof (MBProgressHUDDelegate) })]
	interface MTMBProgressHUD {
	
		// + (MB_INSTANCETYPE)showHUDAddedTo:(UIView *)view animated:(BOOL)animated;
		[Static]
		[Export ("showHUDAddedTo:animated:")]
		MTMBProgressHUD ShowHUD (UIView view, bool animated);

		// + (BOOL)hideHUDForView:(UIView *)view animated:(BOOL)animated;
		[Static]
		[Export ("hideHUDForView:animated:")]
		bool HideHUD (UIView view, bool animated);

		// + (NSUInteger)hideAllHUDsForView:(UIView *)view animated:(BOOL)animated;
		[Static]
		[Export ("hideAllHUDsForView:animated:")]
		nuint HideAllHUDs (UIView view, bool animated);

		// + (MB_INSTANCETYPE)HUDForView:(UIView *)view;
		[Static]
		[Export ("HUDForView:")]
		MTMBProgressHUD HUDForView (UIView view);

		// + (NSArray *)allHUDsForView:(UIView *)view;
		[Static]
		[Export ("allHUDsForView:")]
		MTMBProgressHUD [] AllHUDsForView (UIView view);

		// - (id)initWithWindow:(UIWindow *)window;
		[Export ("initWithWindow:")]
		IntPtr Constructor (UIWindow window);

		// - (id)initWithView:(UIView *)view;
		[Export ("initWithView:")]
		IntPtr Constructor (UIView view);

		// - (void)show:(BOOL)animated;
		[Export ("show:")]
		void Show (bool animated);

		// - (void)hide:(BOOL)animated;
		[Export ("hide:")]
		void Hide (bool animated);

		// - (void)hide:(BOOL)animated afterDelay:(NSTimeInterval)delay;
		[Export ("hide:afterDelay:")]
		void Hide (bool animated, double delay);

		// - (void)showWhileExecuting:(SEL)method onTarget:(id)target withObject:(id)object animated:(BOOL)animated;
		[Export ("showWhileExecuting:onTarget:withObject:animated:")]
		void Show (Selector method, NSObject target, [NullAllowed] NSObject aObject, bool animated);

		// - (void)showAnimated:(BOOL)animated whileExecutingBlock:(dispatch_block_t)block;
		[Export ("showAnimated:whileExecutingBlock:")]
		void Show (bool animated, NSDispatchHandlerT whileExecutingHandler);

		// - (void)showAnimated:(BOOL)animated whileExecutingBlock:(dispatch_block_t)block completionBlock:(MBProgressHUDCompletionBlock)completion;
		[Export ("showAnimated:whileExecutingBlock:completionBlock:")]
		void Show (bool animated, NSDispatchHandlerT whileExecutingHandler, MBProgressHUDCompletionHandler completionHandler);

		// - (void)showAnimated:(BOOL)animated whileExecutingBlock:(dispatch_block_t)block onQueue:(dispatch_queue_t)queue;
		[Export ("showAnimated:whileExecutingBlock:onQueue:")]
		void Show (bool animated, NSDispatchHandlerT whileExecutingHandler, DispatchQueue queue);

		// - (void)showAnimated:(BOOL)animated whileExecutingBlock:(dispatch_block_t)block onQueue:(dispatch_queue_t)queue completionBlock:(MBProgressHUDCompletionBlock)completion;
		[Export ("showAnimated:whileExecutingBlock:onQueue:completionBlock:")]
		void Show (bool animated, NSDispatchHandlerT whileExecutingHandler, DispatchQueue queue, MBProgressHUDCompletionHandler completionHandler);

		// @property (copy) MBProgressHUDCompletionBlock completionBlock;
		[Export ("completionBlock", ArgumentSemantic.Copy)]
		MBProgressHUDCompletionHandler CompletionHandler { get; set; }

		// @property (assign) MBProgressHUDMode mode;
		[Export ("mode", ArgumentSemantic.Assign)]
		MBProgressHUDMode Mode { get; set; }

		// @property (assign) MBProgressHUDAnimation animationType;
		[Export ("animationType", ArgumentSemantic.Assign)]
		MBProgressHUDAnimation AnimationType { get; set; }

		// @property (MB_STRONG) UIView *customView;
		[Export ("customView", ArgumentSemantic.Retain)]
		UIView CustomView { get; set; }

		// @property (MB_WEAK) id<MBProgressHUDDelegate> delegate;
		[Wrap ("WeakDelegate")]
		MBProgressHUDDelegate Delegate { get; set; }
		
		[Export ("delegate", ArgumentSemantic.Assign)][NullAllowed]
		NSObject WeakDelegate { get; set; }

		// @property (copy) NSString *labelText;
		[Export ("labelText", ArgumentSemantic.Copy)]
		string LabelText { get; set; }

		// @property (copy) NSString *detailsLabelText;
		[Export ("detailsLabelText", ArgumentSemantic.Copy)]
		string DetailsLabelText { get; set; }

		// @property (assign) float opacity;
		[Export ("opacity", ArgumentSemantic.Assign)]
		float Opacity { get; set; }

		// @property (MB_STRONG) UIColor *color;
		[Export ("color", ArgumentSemantic.Retain)]
		UIColor Color { get; set; }

		// @property (assign) float xOffset;
		[Export ("xOffset", ArgumentSemantic.Assign)]
		float XOffset { get; set; }

		// @property (assign) float yOffset;
		[Export ("yOffset", ArgumentSemantic.Assign)]
		float YOffset { get; set; }

		// @property (assign) float margin;
		[Export ("margin", ArgumentSemantic.Assign)]
		float Margin { get; set; }

		// @property (assign) float cornerRadius;
		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		float CornerRadius { get; set; }

		// @property (assign) BOOL dimBackground;
		[Export ("dimBackground", ArgumentSemantic.Assign)]
		bool DimBackground { get; set; }

		// @property (assign) float graceTime;
		[Export ("graceTime", ArgumentSemantic.Assign)]
		float GraceTime { get; set; }

		// @property (assign) float minShowTime;
		[Export ("minShowTime", ArgumentSemantic.Assign)]
		float MinShowTime { get; set; }

		// @property (assign) BOOL taskInProgress;
		[Export ("taskInProgress", ArgumentSemantic.Assign)]
		bool TaskInProgress { get; set; }

		// @property (assign) BOOL removeFromSuperViewOnHide;
		[Export ("removeFromSuperViewOnHide", ArgumentSemantic.Assign)]
		bool RemoveFromSuperViewOnHide { get; set; }

		// @property (MB_STRONG) UIFont* labelFont;
		[Export ("labelFont", ArgumentSemantic.Retain)]
		UIFont LabelFont { get; set; }

		// @property (MB_STRONG) UIColor* labelColor;
		[Export ("labelColor", ArgumentSemantic.Retain)]
		UIColor LabelColor { get; set; }

		// @property (MB_STRONG) UIFont* detailsLabelFont;
		[Export ("detailsLabelFont", ArgumentSemantic.Retain)]
		UIFont DetailsLabelFont { get; set; }

		// @property (MB_STRONG) UIColor* detailsLabelColor;
		[Export ("detailsLabelColor", ArgumentSemantic.Retain)]
		UIColor DetailsLabelColor { get; set; }

		// @property (assign) float progress;
		[Export ("progress", ArgumentSemantic.Assign)]
		float Progress { get; set; }

		// @property (assign) CGSize minSize;
		[Export ("minSize", ArgumentSemantic.Assign)]
		CGSize MinSize { get; set; }

		// @property (assign, getter = isSquare) BOOL square;
		[Export ("square", ArgumentSemantic.Assign)]
		bool Square { [Bind ("isSquare")] get; set; }
	}

	// @protocol MBProgressHUDDelegate <NSObject>
	[BaseType (typeof (NSObject))]
	[Model]
	[Protocol]
	interface MBProgressHUDDelegate {

		//- (void)hudWasHidden:(MBProgressHUD *)hud;
		[Export ("hudWasHidden:"), EventArgs("MBProgressHUDS"), EventName("DidHide")]
		void HudWasHidden (MTMBProgressHUD hud);
	
	}

	// @interface MBRoundProgressView : UIView
	[BaseType (typeof (UIView))]
	interface MBRoundProgressView {

		// @property (nonatomic, assign) float progress;
		[Export ("progress", ArgumentSemantic.Assign)]
		float Progress { get; set; }

		// @property (nonatomic, MB_STRONG) UIColor *progressTintColor;
		[Export ("progressTintColor", ArgumentSemantic.Retain)]
		UIColor ProgressTintColor { get; set; }

		// @property (nonatomic, MB_STRONG) UIColor *backgroundTintColor;
		[Export ("backgroundTintColor", ArgumentSemantic.Retain)]
		UIColor BackgroundTintColor { get; set; }

		// @property (nonatomic, assign, getter = isAnnular) BOOL annular;
		[Export ("annular", ArgumentSemantic.Assign)]
		bool Annular { [Bind ("isAnnular")] get; set; }
	}

	// @interface MBBarProgressView : UIView
	[BaseType (typeof (UIView))]
	interface MBBarProgressView {

		// @property (nonatomic, assign) float progress;
		[Export ("progress", ArgumentSemantic.Assign)]
		float Progress { get; set; }

		// @property (nonatomic, MB_STRONG) UIColor *lineColor;
		[Export ("lineColor", ArgumentSemantic.Retain)]
		UIColor LineColor { get; set; }

		// @property (nonatomic, MB_STRONG) UIColor *progressRemainingColor;
		[Export ("progressRemainingColor", ArgumentSemantic.Retain)]
		UIColor ProgressRemainingColor { get; set; }

		// @property (nonatomic, MB_STRONG) UIColor *progressColor;
		[Export ("progressColor", ArgumentSemantic.Retain)]
		UIColor ProgressColor { get; set; }
	}
}
