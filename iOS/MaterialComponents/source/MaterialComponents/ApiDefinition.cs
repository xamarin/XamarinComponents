using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace MaterialComponents
{
	// @interface MDCAnimationTiming (CAMediaTimingFunction)
	[Category]
	[BaseType (typeof (CAMediaTimingFunction))]
	interface CAMediaTimingFunction_MDCAnimationTiming
	{
		// +(CAMediaTimingFunction * _Nullable)mdc_functionWithType:(MDCAnimationTimingFunction)type;
		[Static]
		[Export ("mdc_functionWithType:")]
		[return: NullAllowed]
		CAMediaTimingFunction Mdc_functionWithType (MDCAnimationTimingFunction type);
	}

	// @interface MDCActivityIndicator : UIView
	[BaseType (typeof (UIView))]
	interface MDCActivityIndicator
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCActivityIndicatorDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCActivityIndicatorDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (getter = isAnimating, assign, nonatomic) BOOL animating;
		[Export ("animating")]
		bool Animating { [Bind ("isAnimating")] get; set; }

		// @property (assign, nonatomic) CGFloat radius __attribute__((annotate("ui_appearance_selector")));
		[Export ("radius")]
		nfloat Radius { get; set; }

		// @property (assign, nonatomic) CGFloat strokeWidth __attribute__((annotate("ui_appearance_selector")));
		[Export ("strokeWidth")]
		nfloat StrokeWidth { get; set; }

		// @property (assign, nonatomic) BOOL trackEnabled;
		[Export ("trackEnabled")]
		bool TrackEnabled { get; set; }

		// @property (assign, nonatomic) MDCActivityIndicatorMode indicatorMode;
		[Export ("indicatorMode",ArgumentSemantic.Assign)]
		MDCActivityIndicatorMode IndicatorMode { get; set; }

		// -(void)setIndicatorMode:(MDCActivityIndicatorMode)mode animated:(BOOL)animated;
		[Export ("setIndicatorMode:animated:")]
		void SetIndicatorMode (MDCActivityIndicatorMode mode,bool animated);

		// @property (assign, nonatomic) float progress;
		[Export ("progress")]
		float Progress { get; set; }

		// @property (copy, nonatomic) NSArray<UIColor *> * _Nonnull cycleColors __attribute__((annotate("ui_appearance_selector")));
		[Export ("cycleColors",ArgumentSemantic.Copy)]
		UIColor[] CycleColors { get; set; }

		// -(void)startAnimating;
		[Export ("startAnimating")]
		void StartAnimating ();

		// -(void)stopAnimating;
		[Export ("stopAnimating")]
		void StopAnimating ();
	}

	// @protocol MDCActivityIndicatorDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCActivityIndicatorDelegate
	{
		// @optional -(void)activityIndicatorAnimationDidFinish:(MDCActivityIndicator * _Nonnull)activityIndicator;
		[Export ("activityIndicatorAnimationDidFinish:")]
		void ActivityIndicatorAnimationDidFinish (MDCActivityIndicator activityIndicator);
	}

	// @protocol MDCColorScheme
	[BaseType (typeof (NSObject))]
	[Protocol, Model]
	interface MDCColorScheme
	{
		// @required @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryColor;
		[Abstract]
		[Export ("primaryColor",ArgumentSemantic.Strong)]
		UIColor PrimaryColor { get; }

		// @optional @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryLightColor;
		[Export ("primaryLightColor",ArgumentSemantic.Strong)]
		UIColor PrimaryLightColor { get; }

		// @optional @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryDarkColor;
		[Export ("primaryDarkColor",ArgumentSemantic.Strong)]
		UIColor PrimaryDarkColor { get; }

		// @optional @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryColor;
		[Export ("secondaryColor",ArgumentSemantic.Strong)]
		UIColor SecondaryColor { get; }

		// @optional @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryLightColor;
		[Export ("secondaryLightColor",ArgumentSemantic.Strong)]
		UIColor SecondaryLightColor { get; }

		// @optional @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryDarkColor;
		[Export ("secondaryDarkColor",ArgumentSemantic.Strong)]
		UIColor SecondaryDarkColor { get; }
	}

	// @interface MDCBasicColorScheme : NSObject <MDCColorScheme, NSCopying>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCBasicColorScheme : MDCColorScheme, INSCopying
	{
		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryColor;
		[Export ("primaryColor",ArgumentSemantic.Strong)]
		UIColor PrimaryColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryLightColor;
		[Export ("primaryLightColor",ArgumentSemantic.Strong)]
		UIColor PrimaryLightColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryDarkColor;
		[Export ("primaryDarkColor",ArgumentSemantic.Strong)]
		UIColor PrimaryDarkColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryColor;
		[Export ("secondaryColor",ArgumentSemantic.Strong)]
		UIColor SecondaryColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryLightColor;
		[Export ("secondaryLightColor",ArgumentSemantic.Strong)]
		UIColor SecondaryLightColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryDarkColor;
		[Export ("secondaryDarkColor",ArgumentSemantic.Strong)]
		UIColor SecondaryDarkColor { get; }

		// -(instancetype _Nonnull)initWithPrimaryColor:(UIColor * _Nonnull)primaryColor primaryLightColor:(UIColor * _Nonnull)primaryLightColor primaryDarkColor:(UIColor * _Nonnull)primaryDarkColor secondaryColor:(UIColor * _Nonnull)secondaryColor secondaryLightColor:(UIColor * _Nonnull)secondaryLightColor secondaryDarkColor:(UIColor * _Nonnull)secondaryDarkColor __attribute__((objc_designated_initializer));
		[Export ("initWithPrimaryColor:primaryLightColor:primaryDarkColor:secondaryColor:secondaryLightColor:secondaryDarkColor:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIColor primaryColor,UIColor primaryLightColor,UIColor primaryDarkColor,UIColor secondaryColor,UIColor secondaryLightColor,UIColor secondaryDarkColor);

		// -(instancetype _Nonnull)initWithPrimaryColor:(UIColor * _Nonnull)primaryColor;
		[Export ("initWithPrimaryColor:")]
		IntPtr Constructor (UIColor primaryColor);

		// -(instancetype _Nonnull)initWithPrimaryColor:(UIColor * _Nonnull)primaryColor primaryLightColor:(UIColor * _Nonnull)primaryLightColor primaryDarkColor:(UIColor * _Nonnull)primaryDarkColor;
		[Export ("initWithPrimaryColor:primaryLightColor:primaryDarkColor:")]
		IntPtr Constructor (UIColor primaryColor,UIColor primaryLightColor,UIColor primaryDarkColor);

		// -(instancetype _Nonnull)initWithPrimaryColor:(UIColor * _Nonnull)primaryColor secondaryColor:(UIColor * _Nonnull)secondaryColor;
		[Export ("initWithPrimaryColor:secondaryColor:")]
		IntPtr Constructor (UIColor primaryColor,UIColor secondaryColor);
	}

	// @interface MDCTonalPalette : NSObject <NSCoding, NSCopying>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCTonalPalette : INSCoding, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSArray<UIColor *> * _Nonnull colors;
		[Export ("colors",ArgumentSemantic.Copy)]
		UIColor[] Colors { get; }

		// @property (readonly, nonatomic) NSUInteger mainColorIndex;
		[Export ("mainColorIndex")]
		nuint MainColorIndex { get; }

		// @property (readonly, nonatomic) NSUInteger lightColorIndex;
		[Export ("lightColorIndex")]
		nuint LightColorIndex { get; }

		// @property (readonly, nonatomic) NSUInteger darkColorIndex;
		[Export ("darkColorIndex")]
		nuint DarkColorIndex { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull mainColor;
		[Export ("mainColor",ArgumentSemantic.Strong)]
		UIColor MainColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull lightColor;
		[Export ("lightColor",ArgumentSemantic.Strong)]
		UIColor LightColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull darkColor;
		[Export ("darkColor",ArgumentSemantic.Strong)]
		UIColor DarkColor { get; }

		// -(instancetype _Nonnull)initWithColors:(NSArray<UIColor *> * _Nonnull)colors mainColorIndex:(NSUInteger)mainColorIndex lightColorIndex:(NSUInteger)lightColorIndex darkColorIndex:(NSUInteger)darkColorIndex __attribute__((objc_designated_initializer));
		[Export ("initWithColors:mainColorIndex:lightColorIndex:darkColorIndex:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIColor[] colors,nuint mainColorIndex,nuint lightColorIndex,nuint darkColorIndex);

		//// -(instancetype _Nonnull)initWithCoder:(NSCoder * _Nonnull)coder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder coder); TODO not needed
	}

	// @interface MDCTonalColorScheme : NSObject <MDCColorScheme, NSCopying>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCTonalColorScheme : MDCColorScheme, INSCopying
	{
		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryColor;
		[Export ("primaryColor",ArgumentSemantic.Strong)]
		UIColor PrimaryColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryLightColor;
		[Export ("primaryLightColor",ArgumentSemantic.Strong)]
		UIColor PrimaryLightColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull primaryDarkColor;
		[Export ("primaryDarkColor",ArgumentSemantic.Strong)]
		UIColor PrimaryDarkColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryColor;
		[Export ("secondaryColor",ArgumentSemantic.Strong)]
		UIColor SecondaryColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryLightColor;
		[Export ("secondaryLightColor",ArgumentSemantic.Strong)]
		UIColor SecondaryLightColor { get; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull secondaryDarkColor;
		[Export ("secondaryDarkColor",ArgumentSemantic.Strong)]
		UIColor SecondaryDarkColor { get; }

		// @property (readonly, nonatomic, strong) MDCTonalPalette * _Nonnull primaryTonalPalette;
		[Export ("primaryTonalPalette",ArgumentSemantic.Strong)]
		MDCTonalPalette PrimaryTonalPalette { get; }

		// @property (readonly, nonatomic, strong) MDCTonalPalette * _Nonnull secondaryTonalPalette;
		[Export ("secondaryTonalPalette",ArgumentSemantic.Strong)]
		MDCTonalPalette SecondaryTonalPalette { get; }

		// -(instancetype _Nonnull)initWithPrimaryTonalPalette:(MDCTonalPalette * _Nonnull)primaryTonalPalette secondaryTonalPalette:(MDCTonalPalette * _Nonnull)secondaryTonalPalette __attribute__((objc_designated_initializer));
		[Export ("initWithPrimaryTonalPalette:secondaryTonalPalette:")]
		[DesignatedInitializer]
		IntPtr Constructor (MDCTonalPalette primaryTonalPalette,MDCTonalPalette secondaryTonalPalette);
	}

	// @interface MDCActivityIndicatorColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCActivityIndicatorColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toActivityIndicator:(MDCActivityIndicator *)activityIndicator;
		[Static]
		[Export ("applyColorScheme:toActivityIndicator:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCActivityIndicator activityIndicator);
	}

	// @interface MDCAlertController : UIViewController
	[BaseType (typeof (UIViewController))]
	interface MDCAlertController
	{
		// +(instancetype _Nonnull)alertControllerWithTitle:(NSString * _Nullable)title message:(NSString * _Nullable)message;
		[Static]
		[Export ("alertControllerWithTitle:message:")]
		MDCAlertController AlertControllerWithTitle ([NullAllowed] string title,[NullAllowed] string message);

		// -(void)addAction:(MDCAlertAction * _Nonnull)action;
		[Export ("addAction:")]
		void AddAction (MDCAlertAction action);

		// @property (readonly, nonatomic) NSArray<MDCAlertAction *> * _Nonnull actions;
		[Export ("actions")]
		MDCAlertAction[] Actions { get; }

		// @property (copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable message;
		[NullAllowed, Export ("message")]
		string Message { get; set; }

		// @property (readwrite, nonatomic, setter = mdc_setAdjustsFontForContentSizeCategory:) BOOL mdc_adjustsFontForContentSizeCategory __attribute__((annotate("ui_appearance_selector")));
		[Export ("mdc_adjustsFontForContentSizeCategory")]
		bool Mdc_adjustsFontForContentSizeCategory { get; [Bind ("mdc_setAdjustsFontForContentSizeCategory:")] set; }
	}

	// typedef void (^MDCActionHandler)(MDCAlertAction * _Nonnull);
	delegate void MDCActionHandler (MDCAlertAction arg0);

	// @interface MDCAlertAction : NSObject <NSCopying>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCAlertAction : INSCopying
	{
		// +(instancetype _Nonnull)actionWithTitle:(NSString * _Nonnull)title handler:(MDCActionHandler _Nullable)handler;
		[Static]
		[Export ("actionWithTitle:handler:")]
		MDCAlertAction ActionWithTitle (string title,[NullAllowed] MDCActionHandler handler);

		// @property (readonly, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; }
	}

	// @interface MDCDialogPresentationController : UIPresentationController
	[BaseType (typeof (UIPresentationController))]
	interface MDCDialogPresentationController
	{
		// @property (assign, nonatomic) BOOL dismissOnBackgroundTap;
		[Export ("dismissOnBackgroundTap")]
		bool DismissOnBackgroundTap { get; set; }

		// -(CGSize)sizeForChildContentContainer:(id<UIContentContainer> _Nonnull)container withParentContainerSize:(CGSize)parentSize;
		[Export ("sizeForChildContentContainer:withParentContainerSize:")]
		CGSize SizeForChildContentContainer (UIContentContainer container,CGSize parentSize);

		// -(CGRect)frameOfPresentedViewInContainerView;
		[Export ("frameOfPresentedViewInContainerView")]
		CGRect FrameOfPresentedViewInContainerView { get; }
	}

	// @interface MDCDialogTransitionController : NSObject <UIViewControllerAnimatedTransitioning, UIViewControllerTransitioningDelegate>
	[BaseType (typeof (NSObject))]
	interface MDCDialogTransitionController : IUIViewControllerAnimatedTransitioning, IUIViewControllerTransitioningDelegate
	{
	}

	// @interface MaterialDialogs (UIViewController)
	[Category]
	[BaseType (typeof (UIViewController))]
	interface UIViewController_MaterialDialogs
	{
		// @property (readonly, nonatomic) MDCDialogPresentationController * _Nullable mdc_dialogPresentationController;
		[NullAllowed, Export ("mdc_dialogPresentationController")]
		MDCDialogPresentationController Mdc_dialogPresentationController ();
	}

	// @interface MDCAlertColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCAlertColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme;
		[Static]
		[Export ("applyColorScheme:")]
		void ApplyColorScheme (MDCColorScheme colorScheme);
	}

	// @interface MDCFlexibleHeaderContainerViewController : UIViewController
	[BaseType (typeof (UIViewController))]
	interface MDCFlexibleHeaderContainerViewController
	{
		// -(instancetype _Nonnull)initWithContentViewController:(UIViewController * _Nullable)contentViewController __attribute__((objc_designated_initializer));
		[Export ("initWithContentViewController:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] UIViewController contentViewController);

		// @property (readonly, nonatomic, strong) MDCFlexibleHeaderViewController * _Nonnull headerViewController;
		[Export ("headerViewController",ArgumentSemantic.Strong)]
		MDCFlexibleHeaderViewController HeaderViewController { get; }

		// @property (nonatomic, strong) UIViewController * _Nullable contentViewController;
		[NullAllowed, Export ("contentViewController",ArgumentSemantic.Strong)]
		UIViewController ContentViewController { get; set; }
	}

	// typedef void (^MDCFlexibleHeaderChangeContentInsetsBlock)();
	delegate void MDCFlexibleHeaderChangeContentInsetsBlock ();

	// typedef void (^MDCFlexibleHeaderShadowIntensityChangeBlock)(CALayer * _Nonnull, CGFloat);
	delegate void MDCFlexibleHeaderShadowIntensityChangeBlock (CALayer arg0,nfloat arg1);

	// @interface MDCFlexibleHeaderView : UIView
	[BaseType (typeof (UIView))]
	interface MDCFlexibleHeaderView
	{
		// @property (nonatomic, strong) CALayer * _Nullable shadowLayer;
		[NullAllowed, Export ("shadowLayer",ArgumentSemantic.Strong)]
		CALayer ShadowLayer { get; set; }

		// -(void)setShadowLayer:(CALayer * _Nonnull)shadowLayer intensityDidChangeBlock:(MDCFlexibleHeaderShadowIntensityChangeBlock _Nonnull)block;
		[Export ("setShadowLayer:intensityDidChangeBlock:")]
		void SetShadowLayer (CALayer shadowLayer,MDCFlexibleHeaderShadowIntensityChangeBlock block);

		// -(void)trackingScrollViewDidScroll;
		[Export ("trackingScrollViewDidScroll")]
		void TrackingScrollViewDidScroll ();

		// -(void)trackingScrollViewDidEndDraggingWillDecelerate:(BOOL)willDecelerate;
		[Export ("trackingScrollViewDidEndDraggingWillDecelerate:")]
		void TrackingScrollViewDidEndDraggingWillDecelerate (bool willDecelerate);

		// -(void)trackingScrollViewDidEndDecelerating;
		[Export ("trackingScrollViewDidEndDecelerating")]
		void TrackingScrollViewDidEndDecelerating ();

		// -(BOOL)trackingScrollViewWillEndDraggingWithVelocity:(CGPoint)velocity targetContentOffset:(CGPoint * _Nonnull)targetContentOffset;
		[Export ("trackingScrollViewWillEndDraggingWithVelocity:targetContentOffset:")]
		//unsafe bool TrackingScrollViewWillEndDraggingWithVelocity (CGPoint velocity,CGPoint* targetContentOffset); TODO check
		unsafe bool TrackingScrollViewWillEndDraggingWithVelocity (CGPoint velocity,ref CGPoint targetContentOffset);

		// -(void)trackingScrollWillChangeToScrollView:(UIScrollView * _Nullable)scrollView;
		[Export ("trackingScrollWillChangeToScrollView:")]
		void TrackingScrollWillChangeToScrollView ([NullAllowed] UIScrollView scrollView);

		// -(void)shiftHeaderOnScreenAnimated:(BOOL)animated;
		[Export ("shiftHeaderOnScreenAnimated:")]
		void ShiftHeaderOnScreenAnimated (bool animated);

		// -(void)shiftHeaderOffScreenAnimated:(BOOL)animated;
		[Export ("shiftHeaderOffScreenAnimated:")]
		void ShiftHeaderOffScreenAnimated (bool animated);

		// @property (readonly, nonatomic) BOOL prefersStatusBarHidden;
		[Export ("prefersStatusBarHidden")]
		bool PrefersStatusBarHidden { get; }

		// -(void)interfaceOrientationWillChange;
		[Export ("interfaceOrientationWillChange")]
		void InterfaceOrientationWillChange ();

		// -(void)interfaceOrientationIsChanging;
		[Export ("interfaceOrientationIsChanging")]
		void InterfaceOrientationIsChanging ();

		// -(void)interfaceOrientationDidChange;
		[Export ("interfaceOrientationDidChange")]
		void InterfaceOrientationDidChange ();

		// -(void)viewWillTransitionToSize:(CGSize)size withTransitionCoordinator:(id<UIViewControllerTransitionCoordinator> _Nonnull)coordinator;
		[Export ("viewWillTransitionToSize:withTransitionCoordinator:")]
		void ViewWillTransitionToSize (CGSize size,IUIViewControllerTransitionCoordinator coordinator);

		// -(void)changeContentInsets:(MDCFlexibleHeaderChangeContentInsetsBlock _Nonnull)block;
		[Export ("changeContentInsets:")]
		void ChangeContentInsets (MDCFlexibleHeaderChangeContentInsetsBlock block);

		// -(void)forwardTouchEventsForView:(UIView * _Nonnull)view;
		[Export ("forwardTouchEventsForView:")]
		void ForwardTouchEventsForView (UIView view);

		// -(void)stopForwardingTouchEventsForView:(UIView * _Nonnull)view;
		[Export ("stopForwardingTouchEventsForView:")]
		void StopForwardingTouchEventsForView (UIView view);

		// @property (readonly, nonatomic) MDCFlexibleHeaderScrollPhase scrollPhase;
		[Export ("scrollPhase")]
		MDCFlexibleHeaderScrollPhase ScrollPhase { get; }

		// @property (readonly, nonatomic) CGFloat scrollPhaseValue;
		[Export ("scrollPhaseValue")]
		nfloat ScrollPhaseValue { get; }

		// @property (readonly, nonatomic) CGFloat scrollPhasePercentage;
		[Export ("scrollPhasePercentage")]
		nfloat ScrollPhasePercentage { get; }

		// @property (nonatomic) CGFloat minimumHeight;
		[Export ("minimumHeight")]
		nfloat MinimumHeight { get; set; }

		// @property (nonatomic) CGFloat maximumHeight;
		[Export ("maximumHeight")]
		nfloat MaximumHeight { get; set; }

		// @property (nonatomic) BOOL minMaxHeightIncludesSafeArea;
		[Export ("minMaxHeightIncludesSafeArea")]
		bool MinMaxHeightIncludesSafeArea { get; set; }

		// @property (nonatomic) MDCFlexibleHeaderShiftBehavior shiftBehavior;
		[Export ("shiftBehavior",ArgumentSemantic.Assign)]
		MDCFlexibleHeaderShiftBehavior ShiftBehavior { get; set; }

		// @property (nonatomic) MDCFlexibleHeaderContentImportance headerContentImportance;
		[Export ("headerContentImportance",ArgumentSemantic.Assign)]
		MDCFlexibleHeaderContentImportance HeaderContentImportance { get; set; }

		// @property (nonatomic) BOOL canOverExtend;
		[Export ("canOverExtend")]
		bool CanOverExtend { get; set; }

		// @property (nonatomic) BOOL statusBarHintCanOverlapHeader;
		[Export ("statusBarHintCanOverlapHeader")]
		bool StatusBarHintCanOverlapHeader { get; set; }

		// @property (nonatomic) float visibleShadowOpacity;
		[Export ("visibleShadowOpacity")]
		float VisibleShadowOpacity { get; set; }

		// @property (nonatomic, weak) UIScrollView * _Nullable trackingScrollView;
		[NullAllowed, Export ("trackingScrollView",ArgumentSemantic.Weak)]
		UIScrollView TrackingScrollView { get; set; }

		// @property (nonatomic) BOOL trackingScrollViewIsBeingScrubbed;
		[Export ("trackingScrollViewIsBeingScrubbed")]
		bool TrackingScrollViewIsBeingScrubbed { get; set; }

		// @property (getter = isInFrontOfInfiniteContent, nonatomic) BOOL inFrontOfInfiniteContent;
		[Export ("inFrontOfInfiniteContent")]
		bool InFrontOfInfiniteContent { [Bind ("isInFrontOfInfiniteContent")] get; set; }

		// @property (nonatomic) BOOL sharedWithManyScrollViews;
		[Export ("sharedWithManyScrollViews")]
		bool SharedWithManyScrollViews { get; set; }

		// @property (nonatomic) BOOL contentIsTranslucent;
		[Export ("contentIsTranslucent")]
		bool ContentIsTranslucent { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCFlexibleHeaderViewDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCFlexibleHeaderViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }
	}

	// @protocol MDCFlexibleHeaderViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCFlexibleHeaderViewDelegate
	{
		// @required -(void)flexibleHeaderViewNeedsStatusBarAppearanceUpdate:(MDCFlexibleHeaderView * _Nonnull)headerView;
		[Abstract]
		[Export ("flexibleHeaderViewNeedsStatusBarAppearanceUpdate:")]
		void FlexibleHeaderViewNeedsStatusBarAppearanceUpdate (MDCFlexibleHeaderView headerView);

		// @required -(void)flexibleHeaderViewFrameDidChange:(MDCFlexibleHeaderView * _Nonnull)headerView;
		[Abstract]
		[Export ("flexibleHeaderViewFrameDidChange:")]
		void FlexibleHeaderViewFrameDidChange (MDCFlexibleHeaderView headerView);
	}

	// @interface  (MDCFlexibleHeaderView)
	[Category]
	[BaseType (typeof (MDCFlexibleHeaderView))]
	interface MDCFlexibleHeaderView_
	{
		// @property (nonatomic) MDCFlexibleHeaderShiftBehavior behavior __attribute__((deprecated("Use shiftBehavior instead.")));
		[Export ("behavior",ArgumentSemantic.Assign)]
		MDCFlexibleHeaderShiftBehavior Behavior ();

		// @property (nonatomic, strong) UIView * _Nonnull contentView __attribute__((deprecated("Please register views directly to the flexible header.")));
		[Export ("contentView",ArgumentSemantic.Strong)]
		UIView ContentView ();
	}

	// @interface MDCFlexibleHeaderViewController : UIViewController <UIScrollViewDelegate, UITableViewDelegate>
	[BaseType (typeof (UIViewController))]
	interface MDCFlexibleHeaderViewController : IUIScrollViewDelegate, IUITableViewDelegate
	{
		// @property (readonly, nonatomic, strong) MDCFlexibleHeaderView * _Nonnull headerView;
		[Export ("headerView",ArgumentSemantic.Strong)]
		MDCFlexibleHeaderView HeaderView { get; }

		[Wrap ("WeakLayoutDelegate")]
		[NullAllowed]
		MDCFlexibleHeaderViewLayoutDelegate LayoutDelegate { get; set; }

		// @property (nonatomic, weak) id<MDCFlexibleHeaderViewLayoutDelegate> _Nullable layoutDelegate;
		[NullAllowed, Export ("layoutDelegate",ArgumentSemantic.Weak)]
		NSObject WeakLayoutDelegate { get; set; }

		// -(BOOL)prefersStatusBarHidden;
		[Export ("prefersStatusBarHidden")]
		bool PrefersStatusBarHidden { get; }

		// -(UIStatusBarStyle)preferredStatusBarStyle;
		[Export ("preferredStatusBarStyle")]
		UIStatusBarStyle PreferredStatusBarStyle { get; }

		// -(void)updateTopLayoutGuide;
		[Export ("updateTopLayoutGuide")]
		void UpdateTopLayoutGuide ();
	}

	// @protocol MDCFlexibleHeaderViewLayoutDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCFlexibleHeaderViewLayoutDelegate
	{
		// @required -(void)flexibleHeaderViewController:(MDCFlexibleHeaderViewController * _Nonnull)flexibleHeaderViewController flexibleHeaderViewFrameDidChange:(MDCFlexibleHeaderView * _Nonnull)flexibleHeaderView;
		[Abstract]
		[Export ("flexibleHeaderViewController:flexibleHeaderViewFrameDidChange:")]
		void FlexibleHeaderViewFrameDidChange (MDCFlexibleHeaderViewController flexibleHeaderViewController,MDCFlexibleHeaderView flexibleHeaderView);
	}

	// @interface MDCHeaderStackView : UIView
	[BaseType (typeof (UIView))]
	interface MDCHeaderStackView
	{
		// @property (nonatomic, strong) UIView * _Nullable topBar;
		[NullAllowed, Export ("topBar",ArgumentSemantic.Strong)]
		UIView TopBar { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable bottomBar;
		[NullAllowed, Export ("bottomBar",ArgumentSemantic.Strong)]
		UIView BottomBar { get; set; }
	}

	// @protocol MDCUINavigationItemObservables <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCUINavigationItemObservables
	{
		// @required @property (copy, nonatomic) NSString * _Nullable title;
		[Abstract]
		[NullAllowed, Export ("title")]
		string Title { get; set; }

		// @required @property (nonatomic, strong) UIView * _Nullable titleView;
		[Abstract]
		[NullAllowed, Export ("titleView",ArgumentSemantic.Strong)]
		UIView TitleView { get; set; }

		// @required @property (nonatomic) BOOL hidesBackButton;
		[Abstract]
		[Export ("hidesBackButton")]
		bool HidesBackButton { get; set; }

		// @required @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable leftBarButtonItems;
		[Abstract]
		[NullAllowed, Export ("leftBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] LeftBarButtonItems { get; set; }

		// @required @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable rightBarButtonItems;
		[Abstract]
		[NullAllowed, Export ("rightBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] RightBarButtonItems { get; set; }

		// @required @property (nonatomic) BOOL leftItemsSupplementBackButton;
		[Abstract]
		[Export ("leftItemsSupplementBackButton")]
		bool LeftItemsSupplementBackButton { get; set; }

		// @required @property (nonatomic, strong) UIBarButtonItem * _Nullable leftBarButtonItem;
		[Abstract]
		[NullAllowed, Export ("leftBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem LeftBarButtonItem { get; set; }

		// @required @property (nonatomic, strong) UIBarButtonItem * _Nullable rightBarButtonItem;
		[Abstract]
		[NullAllowed, Export ("rightBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem RightBarButtonItem { get; set; }
	}

	// @interface MDCNavigationBarTextColorAccessibilityMutator : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCNavigationBarTextColorAccessibilityMutator
	{
		// -(void)mutate:(MDCNavigationBar * _Nonnull)navBar;
		[Export ("mutate:")]
		void Mutate (MDCNavigationBar navBar);
	}

	// @interface MDCNavigationBar : UIView
	[BaseType (typeof (UIView))]
	interface MDCNavigationBar
	{
		// @property (copy, nonatomic) NSString * _Nullable title;
		[NullAllowed, Export ("title")]
		string Title { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable titleView;
		[NullAllowed, Export ("titleView",ArgumentSemantic.Strong)]
		UIView TitleView { get; set; }

		// @property (copy, nonatomic) NSDictionary<NSAttributedStringKey,id> * _Nullable titleTextAttributes __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("titleTextAttributes",ArgumentSemantic.Copy)]
		NSDictionary<NSString,NSObject> TitleTextAttributes { get; set; }

		// @property (nonatomic, strong) UIBarButtonItem * _Nullable backItem;
		[NullAllowed, Export ("backItem",ArgumentSemantic.Strong)]
		UIBarButtonItem BackItem { get; set; }

		// @property (nonatomic) BOOL hidesBackButton;
		[Export ("hidesBackButton")]
		bool HidesBackButton { get; set; }

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable leadingBarButtonItems;
		[NullAllowed, Export ("leadingBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] LeadingBarButtonItems { get; set; }

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable trailingBarButtonItems;
		[NullAllowed, Export ("trailingBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] TrailingBarButtonItems { get; set; }

		// @property (nonatomic) BOOL leadingItemsSupplementBackButton;
		[Export ("leadingItemsSupplementBackButton")]
		bool LeadingItemsSupplementBackButton { get; set; }

		// @property (nonatomic, strong) UIBarButtonItem * _Nullable leadingBarButtonItem;
		[NullAllowed, Export ("leadingBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem LeadingBarButtonItem { get; set; }

		// @property (nonatomic, strong) UIBarButtonItem * _Nullable trailingBarButtonItem;
		[NullAllowed, Export ("trailingBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem TrailingBarButtonItem { get; set; }

		// @property (nonatomic) MDCNavigationBarTitleAlignment titleAlignment;
		[Export ("titleAlignment",ArgumentSemantic.Assign)]
		MDCNavigationBarTitleAlignment TitleAlignment { get; set; }

		// -(void)observeNavigationItem:(UINavigationItem * _Nonnull)navigationItem;
		[Export ("observeNavigationItem:")]
		void ObserveNavigationItem (UINavigationItem navigationItem);

		// -(void)unobserveNavigationItem;
		[Export ("unobserveNavigationItem")]
		void UnobserveNavigationItem ();

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable leftBarButtonItems;
		[NullAllowed, Export ("leftBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] LeftBarButtonItems { get; set; }

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable rightBarButtonItems;
		[NullAllowed, Export ("rightBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] RightBarButtonItems { get; set; }

		// @property (nonatomic, strong) UIBarButtonItem * _Nullable leftBarButtonItem;
		[NullAllowed, Export ("leftBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem LeftBarButtonItem { get; set; }

		// @property (nonatomic, strong) UIBarButtonItem * _Nullable rightBarButtonItem;
		[NullAllowed, Export ("rightBarButtonItem",ArgumentSemantic.Strong)]
		UIBarButtonItem RightBarButtonItem { get; set; }

		// @property (nonatomic) BOOL leftItemsSupplementBackButton;
		[Export ("leftItemsSupplementBackButton")]
		bool LeftItemsSupplementBackButton { get; set; }

		// @property (nonatomic) NSTextAlignment textAlignment __attribute__((deprecated("Use titleAlignment instead.")));
		[Export ("textAlignment",ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }
	}

	// @interface MDCAppBarTextColorAccessibilityMutator : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCAppBarTextColorAccessibilityMutator
	{
		// -(void)mutate:(MDCAppBar * _Nonnull)appBar;
		[Export ("mutate:")]
		void Mutate (MDCAppBar appBar);
	}

	// @interface MDCAppBar : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCAppBar
	{
		// -(void)addSubviewsToParent;
		[Export ("addSubviewsToParent")]
		void AddSubviewsToParent ();

		// @property (readonly, nonatomic, strong) MDCFlexibleHeaderViewController * _Nonnull headerViewController;
		[Export ("headerViewController",ArgumentSemantic.Strong)]
		MDCFlexibleHeaderViewController HeaderViewController { get; }

		// @property (readonly, nonatomic, strong) MDCNavigationBar * _Nonnull navigationBar;
		[Export ("navigationBar",ArgumentSemantic.Strong)]
		MDCNavigationBar NavigationBar { get; }

		// @property (readonly, nonatomic, strong) MDCHeaderStackView * _Nonnull headerStackView;
		[Export ("headerStackView",ArgumentSemantic.Strong)]
		MDCHeaderStackView HeaderStackView { get; }
	}

	// @interface MDCAppBarContainerViewController : UIViewController
	[BaseType (typeof (UIViewController))]
	[DisableDefaultCtor]
	interface MDCAppBarContainerViewController
	{
		// -(instancetype _Nonnull)initWithContentViewController:(UIViewController * _Nonnull)contentViewController __attribute__((objc_designated_initializer));
		[Export ("initWithContentViewController:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIViewController contentViewController);

		// @property (readonly, nonatomic, strong) MDCAppBar * _Nonnull appBar;
		[Export ("appBar",ArgumentSemantic.Strong)]
		MDCAppBar AppBar { get; }

		// @property (readonly, nonatomic, strong) UIViewController * _Nonnull contentViewController;
		[Export ("contentViewController",ArgumentSemantic.Strong)]
		UIViewController ContentViewController { get; }
	}

	// @interface MDCAppBarColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCAppBarColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toAppBar:(MDCAppBar *)appBar;
		[Static]
		[Export ("applyColorScheme:toAppBar:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCAppBar appBar);
	}

	// @interface MDCInkGestureRecognizer : UIGestureRecognizer
	[BaseType (typeof (UIGestureRecognizer))]
	interface MDCInkGestureRecognizer
	{
		// @property (assign, nonatomic) CGFloat dragCancelDistance;
		[Export ("dragCancelDistance")]
		nfloat DragCancelDistance { get; set; }

		// @property (assign, nonatomic) BOOL cancelOnDragOut;
		[Export ("cancelOnDragOut")]
		bool CancelOnDragOut { get; set; }

		// @property (nonatomic) CGRect targetBounds;
		[Export ("targetBounds",ArgumentSemantic.Assign)]
		CGRect TargetBounds { get; set; }

		// -(CGPoint)touchStartLocationInView:(UIView *)view;
		[Export ("touchStartLocationInView:")]
		CGPoint TouchStartLocationInView (UIView view);

		// -(BOOL)isTouchWithinTargetBounds;
		[Export ("isTouchWithinTargetBounds")]
		bool IsTouchWithinTargetBounds { get; }
	}

	// @interface MDCInkTouchController : NSObject <UIGestureRecognizerDelegate>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCInkTouchController : IUIGestureRecognizerDelegate
	{
		// @property (readonly, nonatomic, weak) UIView * _Nullable view;
		[NullAllowed, Export ("view",ArgumentSemantic.Weak)]
		UIView View { get; }

		// @property (readonly, nonatomic, strong) MDCInkView * _Nonnull defaultInkView;
		[Export ("defaultInkView",ArgumentSemantic.Strong)]
		MDCInkView DefaultInkView { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCInkTouchControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCInkTouchControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) BOOL delaysInkSpread;
		[Export ("delaysInkSpread")]
		bool DelaysInkSpread { get; set; }

		// @property (assign, nonatomic) CGFloat dragCancelDistance;
		[Export ("dragCancelDistance")]
		nfloat DragCancelDistance { get; set; }

		// @property (assign, nonatomic) BOOL cancelsOnDragOut;
		[Export ("cancelsOnDragOut")]
		bool CancelsOnDragOut { get; set; }

		// @property (nonatomic) CGRect targetBounds;
		[Export ("targetBounds",ArgumentSemantic.Assign)]
		CGRect TargetBounds { get; set; }

		// @property (readonly, nonatomic, strong) MDCInkGestureRecognizer * _Nonnull gestureRecognizer;
		[Export ("gestureRecognizer",ArgumentSemantic.Strong)]
		MDCInkGestureRecognizer GestureRecognizer { get; }

		// -(instancetype _Nonnull)initWithView:(UIView * _Nonnull)view __attribute__((objc_designated_initializer));
		[Export ("initWithView:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIView view);

		// -(void)addInkView;
		[Export ("addInkView")]
		void AddInkView ();

		// -(void)cancelInkTouchProcessing;
		[Export ("cancelInkTouchProcessing")]
		void CancelInkTouchProcessing ();

		// -(MDCInkView * _Nullable)inkViewAtTouchLocation:(CGPoint)location;
		[Export ("inkViewAtTouchLocation:")]
		[return: NullAllowed]
		MDCInkView InkViewAtTouchLocation (CGPoint location);
	}

	// @protocol MDCInkTouchControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCInkTouchControllerDelegate
	{
		// @optional -(void)inkTouchController:(MDCInkTouchController * _Nonnull)inkTouchController insertInkView:(UIView * _Nonnull)inkView intoView:(UIView * _Nonnull)view;
		[Export ("inkTouchController:insertInkView:intoView:")]
		void InkTouchController (MDCInkTouchController inkTouchController,UIView inkView,UIView view);

		// @optional -(MDCInkView * _Nullable)inkTouchController:(MDCInkTouchController * _Nonnull)inkTouchController inkViewAtTouchLocation:(CGPoint)location;
		[Export ("inkTouchController:inkViewAtTouchLocation:")]
		[return: NullAllowed]
		MDCInkView InkTouchController (MDCInkTouchController inkTouchController,CGPoint location);

		// @optional -(BOOL)inkTouchController:(MDCInkTouchController * _Nonnull)inkTouchController shouldProcessInkTouchesAtTouchLocation:(CGPoint)location;
		[Export ("inkTouchController:shouldProcessInkTouchesAtTouchLocation:")]
		bool InkTouchController2 (MDCInkTouchController inkTouchController,CGPoint location);

		// @optional -(void)inkTouchController:(MDCInkTouchController * _Nonnull)inkTouchController didProcessInkView:(MDCInkView * _Nonnull)inkView atTouchLocation:(CGPoint)location;
		[Export ("inkTouchController:didProcessInkView:atTouchLocation:")]
		void InkTouchController (MDCInkTouchController inkTouchController,MDCInkView inkView,CGPoint location);
	}

	// typedef void (^MDCInkCompletionBlock)();
	delegate void MDCInkCompletionBlock ();

	// @interface MDCInkView : UIView
	[BaseType (typeof (UIView))]
	interface MDCInkView
	{
		// @property (assign, nonatomic) MDCInkStyle inkStyle;
		[Export ("inkStyle",ArgumentSemantic.Assign)]
		MDCInkStyle InkStyle { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified inkColor;
		[Export ("inkColor",ArgumentSemantic.Strong)]
		UIColor InkColor { get; set; }

		// @property (readonly, nonatomic, strong) UIColor * _Nonnull defaultInkColor;
		[Export ("defaultInkColor",ArgumentSemantic.Strong)]
		UIColor DefaultInkColor { get; }

		// @property (assign, nonatomic) CGFloat maxRippleRadius;
		[Export ("maxRippleRadius")]
		nfloat MaxRippleRadius { get; set; }

		// @property (assign, nonatomic) BOOL usesCustomInkCenter;
		[Export ("usesCustomInkCenter")]
		bool UsesCustomInkCenter { get; set; }

		// @property (assign, nonatomic) CGPoint customInkCenter;
		[Export ("customInkCenter",ArgumentSemantic.Assign)]
		CGPoint CustomInkCenter { get; set; }

		// -(void)startTouchBeganAnimationAtPoint:(CGPoint)point completion:(MDCInkCompletionBlock _Nullable)completionBlock;
		[Export ("startTouchBeganAnimationAtPoint:completion:")]
		void StartTouchBeganAnimationAtPoint (CGPoint point,[NullAllowed] MDCInkCompletionBlock completionBlock);

		// -(void)startTouchEndedAnimationAtPoint:(CGPoint)point completion:(MDCInkCompletionBlock _Nullable)completionBlock;
		[Export ("startTouchEndedAnimationAtPoint:completion:")]
		void StartTouchEndedAnimationAtPoint (CGPoint point,[NullAllowed] MDCInkCompletionBlock completionBlock);

		// -(void)cancelAllAnimationsAnimated:(BOOL)animated;
		[Export ("cancelAllAnimationsAnimated:")]
		void CancelAllAnimationsAnimated (bool animated);

		// +(MDCInkView * _Nonnull)injectedInkViewForView:(UIView * _Nonnull)view;
		[Static]
		[Export ("injectedInkViewForView:")]
		MDCInkView InjectedInkViewForView (UIView view);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants3
	{
		// extern const MDCShadowElevation MDCShadowElevationAppBar;
		[Field ("MDCShadowElevationAppBar","__Internal")]
		double MDCShadowElevationAppBar { get; }

		// extern const MDCShadowElevation MDCShadowElevationCardPickedUp;
		[Field ("MDCShadowElevationCardPickedUp","__Internal")]
		double MDCShadowElevationCardPickedUp { get; }

		// extern const MDCShadowElevation MDCShadowElevationCardResting;
		[Field ("MDCShadowElevationCardResting","__Internal")]
		double MDCShadowElevationCardResting { get; }

		// extern const MDCShadowElevation MDCShadowElevationDialog;
		[Field ("MDCShadowElevationDialog","__Internal")]
		double MDCShadowElevationDialog { get; }

		// extern const MDCShadowElevation MDCShadowElevationFABPressed;
		[Field ("MDCShadowElevationFABPressed","__Internal")]
		double MDCShadowElevationFABPressed { get; }

		// extern const MDCShadowElevation MDCShadowElevationFABResting;
		[Field ("MDCShadowElevationFABResting","__Internal")]
		double MDCShadowElevationFABResting { get; }

		// extern const MDCShadowElevation MDCShadowElevationMenu;
		[Field ("MDCShadowElevationMenu","__Internal")]
		double MDCShadowElevationMenu { get; }

		// extern const MDCShadowElevation MDCShadowElevationModalBottomSheet;
		[Field ("MDCShadowElevationModalBottomSheet","__Internal")]
		double MDCShadowElevationModalBottomSheet { get; }

		// extern const MDCShadowElevation MDCShadowElevationNavDrawer;
		[Field ("MDCShadowElevationNavDrawer","__Internal")]
		double MDCShadowElevationNavDrawer { get; }

		// extern const MDCShadowElevation MDCShadowElevationNone;
		[Field ("MDCShadowElevationNone","__Internal")]
		double MDCShadowElevationNone { get; }

		// extern const MDCShadowElevation MDCShadowElevationPicker;
		[Field ("MDCShadowElevationPicker","__Internal")]
		double MDCShadowElevationPicker { get; }

		// extern const MDCShadowElevation MDCShadowElevationQuickEntry;
		[Field ("MDCShadowElevationQuickEntry","__Internal")]
		double MDCShadowElevationQuickEntry { get; }

		// extern const MDCShadowElevation MDCShadowElevationQuickEntryResting;
		[Field ("MDCShadowElevationQuickEntryResting","__Internal")]
		double MDCShadowElevationQuickEntryResting { get; }

		// extern const MDCShadowElevation MDCShadowElevationRaisedButtonPressed;
		[Field ("MDCShadowElevationRaisedButtonPressed","__Internal")]
		double MDCShadowElevationRaisedButtonPressed { get; }

		// extern const MDCShadowElevation MDCShadowElevationRaisedButtonResting;
		[Field ("MDCShadowElevationRaisedButtonResting","__Internal")]
		double MDCShadowElevationRaisedButtonResting { get; }

		// extern const MDCShadowElevation MDCShadowElevationRefresh;
		[Field ("MDCShadowElevationRefresh","__Internal")]
		double MDCShadowElevationRefresh { get; }

		// extern const MDCShadowElevation MDCShadowElevationRightDrawer;
		[Field ("MDCShadowElevationRightDrawer","__Internal")]
		double MDCShadowElevationRightDrawer { get; }

		// extern const MDCShadowElevation MDCShadowElevationSearchBarResting;
		[Field ("MDCShadowElevationSearchBarResting","__Internal")]
		double MDCShadowElevationSearchBarResting { get; }

		// extern const MDCShadowElevation MDCShadowElevationSearchBarScrolled;
		[Field ("MDCShadowElevationSearchBarScrolled","__Internal")]
		double MDCShadowElevationSearchBarScrolled { get; }

		// extern const MDCShadowElevation MDCShadowElevationSnackbar;
		[Field ("MDCShadowElevationSnackbar","__Internal")]
		double MDCShadowElevationSnackbar { get; }

		// extern const MDCShadowElevation MDCShadowElevationSubMenu;
		[Field ("MDCShadowElevationSubMenu","__Internal")]
		double MDCShadowElevationSubMenu { get; }

		// extern const MDCShadowElevation MDCShadowElevationSwitch;
		[Field ("MDCShadowElevationSwitch","__Internal")]
		double MDCShadowElevationSwitch { get; }
	}

	// @interface MDCButton : UIButton
	[BaseType (typeof (UIButton))]
	interface MDCButton
	{
		// @property (assign, nonatomic) MDCInkStyle inkStyle __attribute__((annotate("ui_appearance_selector")));
		[Export ("inkStyle",ArgumentSemantic.Assign)]
		MDCInkStyle InkStyle { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified inkColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("inkColor",ArgumentSemantic.Strong)]
		UIColor InkColor { get; set; }

		// @property (assign, nonatomic) CGFloat inkMaxRippleRadius __attribute__((annotate("ui_appearance_selector")));
		[Export ("inkMaxRippleRadius")]
		nfloat InkMaxRippleRadius { get; set; }

		// @property (nonatomic) CGFloat disabledAlpha;
		[Export ("disabledAlpha")]
		nfloat DisabledAlpha { get; set; }

		// @property (getter = isUppercaseTitle, nonatomic) BOOL uppercaseTitle __attribute__((annotate("ui_appearance_selector")));
		[Export ("uppercaseTitle")]
		bool UppercaseTitle { [Bind ("isUppercaseTitle")] get; set; }

		// @property (nonatomic) UIEdgeInsets hitAreaInsets;
		[Export ("hitAreaInsets",ArgumentSemantic.Assign)]
		UIEdgeInsets HitAreaInsets { get; set; }

		// @property (assign, nonatomic) CGSize minimumSize __attribute__((annotate("ui_appearance_selector")));
		[Export ("minimumSize",ArgumentSemantic.Assign)]
		CGSize MinimumSize { get; set; }

		// @property (assign, nonatomic) CGSize maximumSize __attribute__((annotate("ui_appearance_selector")));
		[Export ("maximumSize",ArgumentSemantic.Assign)]
		CGSize MaximumSize { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable underlyingColorHint;
		[NullAllowed, Export ("underlyingColorHint",ArgumentSemantic.Strong)]
		UIColor UnderlyingColorHint { get; set; }

		// @property (readwrite, nonatomic, setter = mdc_setAdjustsFontForContentSizeCategory:) BOOL mdc_adjustsFontForContentSizeCategory __attribute__((annotate("ui_appearance_selector")));
		[Export ("mdc_adjustsFontForContentSizeCategory")]
		bool Mdc_adjustsFontForContentSizeCategory { get; [Bind ("mdc_setAdjustsFontForContentSizeCategory:")] set; }

		// -(UIColor * _Nullable)backgroundColorForState:(UIControlState)state;
		[Export ("backgroundColorForState:")]
		[return: NullAllowed]
		UIColor BackgroundColorForState (UIControlState state);

		// -(void)setBackgroundColor:(UIColor * _Nullable)backgroundColor forState:(UIControlState)state __attribute__((annotate("ui_appearance_selector")));
		[Export ("setBackgroundColor:forState:")]
		void SetBackgroundColor ([NullAllowed] UIColor backgroundColor,UIControlState state);

		// -(void)setBackgroundColor:(UIColor * _Nullable)backgroundColor;
		[Export ("setBackgroundColor:")]
		void SetBackgroundColor ([NullAllowed] UIColor backgroundColor);

		// -(void)setEnabled:(BOOL)enabled animated:(BOOL)animated;
		[Export ("setEnabled:animated:")]
		void SetEnabled (bool enabled,bool animated);

		// -(MDCShadowElevation)elevationForState:(UIControlState)state;
		[Export ("elevationForState:")]
		double ElevationForState (UIControlState state);

		// -(void)setElevation:(MDCShadowElevation)elevation forState:(UIControlState)state;
		[Export ("setElevation:forState:")]
		void SetElevation (double elevation,UIControlState state);

		// -(UIColor * _Nullable)borderColorForState:(UIControlState)state;
		[Export ("borderColorForState:")]
		[return: NullAllowed]
		UIColor BorderColorForState (UIControlState state);

		// -(void)setBorderColor:(UIColor * _Nullable)borderColor forState:(UIControlState)state __attribute__((annotate("ui_appearance_selector")));
		[Export ("setBorderColor:forState:")]
		void SetBorderColor ([NullAllowed] UIColor borderColor,UIControlState state);

		// -(CGFloat)borderWidthForState:(UIControlState)state;
		[Export ("borderWidthForState:")]
		nfloat BorderWidthForState (UIControlState state);

		// -(void)setBorderWidth:(CGFloat)borderWidth forState:(UIControlState)state __attribute__((annotate("ui_appearance_selector")));
		[Export ("setBorderWidth:forState:")]
		void SetBorderWidth (nfloat borderWidth,UIControlState state);

		// -(void)setShadowColor:(UIColor * _Nullable)shadowColor forState:(UIControlState)state __attribute__((annotate("ui_appearance_selector")));
		[Export ("setShadowColor:forState:")]
		void SetShadowColor ([NullAllowed] UIColor shadowColor,UIControlState state);

		// -(UIColor * _Nullable)shadowColorForState:(UIControlState)state;
		[Export ("shadowColorForState:")]
		[return: NullAllowed]
		UIColor ShadowColorForState (UIControlState state);

		// @property (nonatomic, strong) UIColor * _Nullable customTitleColor __attribute__((deprecated("Use setTitleColor:forState: instead"))) __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("customTitleColor",ArgumentSemantic.Strong)]
		UIColor CustomTitleColor { get; set; }

		// @property (nonatomic) BOOL shouldRaiseOnTouch __attribute__((deprecated("Use MDCFlatButton instead of shouldRaiseOnTouch = NO")));
		[Export ("shouldRaiseOnTouch")]
		bool ShouldRaiseOnTouch { get; set; }

		// @property (nonatomic) BOOL shouldCapitalizeTitle __attribute__((deprecated("Use uppercaseTitle instead.")));
		[Export ("shouldCapitalizeTitle")]
		bool ShouldCapitalizeTitle { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable underlyingColor __attribute__((deprecated("Use underlyingColorHint instead.")));
		[NullAllowed, Export ("underlyingColor",ArgumentSemantic.Strong)]
		UIColor UnderlyingColor { get; set; }
	}

	// @interface MDCFlatButton : MDCButton
	[BaseType (typeof (MDCButton))]
	interface MDCFlatButton
	{
		// @property (nonatomic) BOOL hasOpaqueBackground;
		[Export ("hasOpaqueBackground")]
		bool HasOpaqueBackground { get; set; }
	}

	// @interface MDCFloatingButton : MDCButton
	[BaseType (typeof (MDCButton))]
	interface MDCFloatingButton
	{
		// +(instancetype _Nonnull)floatingButtonWithShape:(MDCFloatingButtonShape)shape;
		[Static]
		[Export ("floatingButtonWithShape:")]
		MDCFloatingButton FloatingButtonWithShape (MDCFloatingButtonShape shape);

		// +(CGFloat)defaultDimension;
		[Static]
		[Export ("defaultDimension")]
		nfloat DefaultDimension { get; }

		// +(CGFloat)miniDimension;
		[Static]
		[Export ("miniDimension")]
		nfloat MiniDimension { get; }

		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame shape:(MDCFloatingButtonShape)shape __attribute__((objc_designated_initializer));
		[Export ("initWithFrame:shape:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGRect frame,MDCFloatingButtonShape shape);

		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame;
		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed

		// +(instancetype _Nonnull)buttonWithShape:(MDCFloatingButtonShape)shape __attribute__((deprecated("Use floatingButtonWithShape: instead.")));
		[Static]
		[Export ("buttonWithShape:")]
		MDCFloatingButton ButtonWithShape (MDCFloatingButtonShape shape);
	}

	// @interface Animation (MDCFloatingButton)
	[Category]
	[BaseType (typeof (MDCFloatingButton))]
	interface MDCFloatingButton_Animation
	{
		// -(void)expand:(BOOL)animated completion:(void (^ _Nullable)(void))completion;
		[Export ("expand:completion:")]
		void Expand (bool animated,[NullAllowed] Action completion);

		// -(void)collapse:(BOOL)animated completion:(void (^ _Nullable)(void))completion;
		[Export ("collapse:completion:")]
		void Collapse (bool animated,[NullAllowed] Action completion);
	}

	// @interface MDCRaisedButton : MDCButton
	[BaseType (typeof (MDCButton))]
	interface MDCRaisedButton
	{
	}

	// @interface MDCBottomAppBarView : UIView
	[BaseType (typeof (UIView))]
	interface MDCBottomAppBarView
	{
		// @property (getter = isFloatingButtonHidden, assign, nonatomic) BOOL floatingButtonHidden;
		[Export ("floatingButtonHidden")]
		bool FloatingButtonHidden { [Bind ("isFloatingButtonHidden")] get; set; }

		// @property (assign, nonatomic) MDCBottomAppBarFloatingButtonElevation floatingButtonElevation;
		[Export ("floatingButtonElevation",ArgumentSemantic.Assign)]
		MDCBottomAppBarFloatingButtonElevation FloatingButtonElevation { get; set; }

		// @property (assign, nonatomic) MDCBottomAppBarFloatingButtonPosition floatingButtonPosition;
		[Export ("floatingButtonPosition",ArgumentSemantic.Assign)]
		MDCBottomAppBarFloatingButtonPosition FloatingButtonPosition { get; set; }

		// @property (readonly, nonatomic, strong) MDCFloatingButton * _Nonnull floatingButton;
		[Export ("floatingButton",ArgumentSemantic.Strong)]
		MDCFloatingButton FloatingButton { get; }

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable leadingBarButtonItems;
		[NullAllowed, Export ("leadingBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] LeadingBarButtonItems { get; set; }

		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable trailingBarButtonItems;
		[NullAllowed, Export ("trailingBarButtonItems",ArgumentSemantic.Copy)]
		UIBarButtonItem[] TrailingBarButtonItems { get; set; }

		// -(void)setFloatingButtonHidden:(BOOL)floatingButtonHidden animated:(BOOL)animated;
		[Export ("setFloatingButtonHidden:animated:")]
		void SetFloatingButtonHidden (bool floatingButtonHidden,bool animated);

		// -(void)setFloatingButtonElevation:(MDCBottomAppBarFloatingButtonElevation)floatingButtonElevation animated:(BOOL)animated;
		[Export ("setFloatingButtonElevation:animated:")]
		void SetFloatingButtonElevation (MDCBottomAppBarFloatingButtonElevation floatingButtonElevation,bool animated);

		// -(void)setFloatingButtonPosition:(MDCBottomAppBarFloatingButtonPosition)floatingButtonPosition animated:(BOOL)animated;
		[Export ("setFloatingButtonPosition:animated:")]
		void SetFloatingButtonPosition (MDCBottomAppBarFloatingButtonPosition floatingButtonPosition,bool animated);
	}

	// @interface MDCBottomNavigationBar : UIView
	[BaseType (typeof (UIView))]
	interface MDCBottomNavigationBar
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCBottomNavigationBarDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCBottomNavigationBarDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) MDCBottomNavigationBarTitleVisibility titleVisibility __attribute__((annotate("ui_appearance_selector")));
		[Export ("titleVisibility",ArgumentSemantic.Assign)]
		MDCBottomNavigationBarTitleVisibility TitleVisibility { get; set; }

		// @property (assign, nonatomic) MDCBottomNavigationBarAlignment alignment __attribute__((annotate("ui_appearance_selector")));
		[Export ("alignment",ArgumentSemantic.Assign)]
		MDCBottomNavigationBarAlignment Alignment { get; set; }

		// @property (copy, nonatomic) NSArray<UITabBarItem *> * _Nonnull items;
		[Export ("items",ArgumentSemantic.Copy)]
		UITabBarItem[] Items { get; set; }

		// @property (nonatomic, weak) UITabBarItem * _Nullable selectedItem;
		[NullAllowed, Export ("selectedItem",ArgumentSemantic.Weak)]
		UITabBarItem SelectedItem { get; set; }

		// @property (nonatomic, strong) UIFont * _Nonnull itemTitleFont __attribute__((annotate("ui_appearance_selector")));
		[Export ("itemTitleFont",ArgumentSemantic.Strong)]
		UIFont ItemTitleFont { get; set; }

		// @property (readwrite, nonatomic, strong) UIColor * _Nonnull selectedItemTintColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("selectedItemTintColor",ArgumentSemantic.Strong)]
		UIColor SelectedItemTintColor { get; set; }

		// @property (readwrite, nonatomic, strong) UIColor * _Nonnull unselectedItemTintColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("unselectedItemTintColor",ArgumentSemantic.Strong)]
		UIColor UnselectedItemTintColor { get; set; }
	}

	// @protocol MDCBottomNavigationBarDelegate <UINavigationBarDelegate>
	[BaseType (typeof (NSObject))]
	[Protocol, Model]
	interface MDCBottomNavigationBarDelegate : IUINavigationBarDelegate
	{
		// @optional -(BOOL)bottomNavigationBar:(MDCBottomNavigationBar * _Nonnull)bottomNavigationBar shouldSelectItem:(UITabBarItem * _Nonnull)item;
		[Export ("bottomNavigationBar:shouldSelectItem:")]
		bool BottomNavigationBar (MDCBottomNavigationBar bottomNavigationBar,UITabBarItem item);

		// @optional -(void)bottomNavigationBar:(MDCBottomNavigationBar * _Nonnull)bottomNavigationBar didSelectItem:(UITabBarItem * _Nonnull)item;
		[Export ("bottomNavigationBar:didSelectItem:")]
		void BottomNavigationBar2 (MDCBottomNavigationBar bottomNavigationBar,UITabBarItem item);
	}

	// @interface MDCBottomNavigationBarColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCBottomNavigationBarColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toBottomNavigationBar:(MDCBottomNavigationBar *)bottomNavigationBar;
		[Static]
		[Export ("applyColorScheme:toBottomNavigationBar:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCBottomNavigationBar bottomNavigationBar);
	}

	// @interface MDCBottomSheetController : UIViewController
	[BaseType (typeof (UIViewController))]
	interface MDCBottomSheetController
	{
		// @property (readonly, nonatomic, strong) UIViewController * _Nonnull contentViewController;
		[Export ("contentViewController",ArgumentSemantic.Strong)]
		UIViewController ContentViewController { get; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCBottomSheetControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCBottomSheetControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype _Nonnull)initWithContentViewController:(UIViewController * _Nonnull)contentViewController;
		[Export ("initWithContentViewController:")]
		IntPtr Constructor (UIViewController contentViewController);
	}

	// @protocol MDCBottomSheetControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCBottomSheetControllerDelegate
	{
		// @required -(void)bottomSheetControllerDidDismissBottomSheet:(MDCBottomSheetController * _Nonnull)controller;
		[Abstract]
		[Export ("bottomSheetControllerDidDismissBottomSheet:")]
		void BottomSheetControllerDidDismissBottomSheet (MDCBottomSheetController controller);
	}

	// @protocol MDCBottomSheetPresentationControllerDelegate <UIAdaptivePresentationControllerDelegate>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCBottomSheetPresentationControllerDelegate : IUIAdaptivePresentationControllerDelegate
	{
		// @optional -(void)prepareForBottomSheetPresentation:(MDCBottomSheetPresentationController * _Nonnull)bottomSheet;
		[Export ("prepareForBottomSheetPresentation:")]
		void PrepareForBottomSheetPresentation (MDCBottomSheetPresentationController bottomSheet);

		// @optional -(void)bottomSheetPresentationControllerDidDismissBottomSheet:(MDCBottomSheetPresentationController * _Nonnull)bottomSheet;
		[Export ("bottomSheetPresentationControllerDidDismissBottomSheet:")]
		void BottomSheetPresentationControllerDidDismissBottomSheet (MDCBottomSheetPresentationController bottomSheet);
	}

	// @interface MDCBottomSheetPresentationController : UIPresentationController
	[BaseType (typeof (UIPresentationController))]
	interface MDCBottomSheetPresentationController
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCBottomSheetPresentationControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCBottomSheetPresentationControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }
	}

	// @interface MDCBottomSheetTransitionController : NSObject <UIViewControllerAnimatedTransitioning, UIViewControllerTransitioningDelegate>
	[BaseType (typeof (NSObject))]
	interface MDCBottomSheetTransitionController : IUIViewControllerAnimatedTransitioning, IUIViewControllerTransitioningDelegate
	{
	}

	// @interface MDCButtonBar : UIView
	[BaseType (typeof (UIView))]
	interface MDCButtonBar
	{
		// @property (copy, nonatomic) NSArray<UIBarButtonItem *> * _Nullable items;
		[NullAllowed, Export ("items",ArgumentSemantic.Copy)]
		UIBarButtonItem[] Items { get; set; }

		// @property (nonatomic) CGFloat buttonTitleBaseline;
		[Export ("buttonTitleBaseline")]
		nfloat ButtonTitleBaseline { get; set; }

		// @property (nonatomic) MDCButtonBarLayoutPosition layoutPosition;
		[Export ("layoutPosition",ArgumentSemantic.Assign)]
		MDCButtonBarLayoutPosition LayoutPosition { get; set; }

		// -(CGSize)sizeThatFits:(CGSize)size;
		[Export ("sizeThatFits:")]
		CGSize SizeThatFits (CGSize size);
	}

	// @protocol MDCButtonBarDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCButtonBarDelegate
	{
		// @required -(UIView * _Nonnull)buttonBar:(MDCButtonBar * _Nonnull)buttonBar viewForItem:(UIBarButtonItem * _Nonnull)barButtonItem layoutHints:(MDCBarButtonItemLayoutHints)layoutHints;
		[Abstract]
		[Export ("buttonBar:viewForItem:layoutHints:")]
		UIView ViewForItem (MDCButtonBar buttonBar,UIBarButtonItem barButtonItem,MDCBarButtonItemLayoutHints layoutHints);
	}

	// @interface MDCButtonBarColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCButtonBarColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toButtonBar:(MDCButtonBar *)buttonBar;
		[Static]
		[Export ("applyColorScheme:toButtonBar:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCButtonBar buttonBar);
	}

	// @interface MDCButtonColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCButtonColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toButton:(MDCButton *)button;
		[Static]
		[Export ("applyColorScheme:toButton:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCButton button);
	}

	// @interface MDCButtonTitleColorAccessibilityMutator : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCButtonTitleColorAccessibilityMutator
	{
		// +(void)changeTitleColorOfButton:(MDCButton *)button;
		[Static]
		[Export ("changeTitleColorOfButton:")]
		void ChangeTitleColorOfButton (MDCButton button);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants2
	{
		// extern NSString *const _Nonnull kSelectedCellAccessibilityHintKey;
		[Field ("kSelectedCellAccessibilityHintKey","__Internal")]
		NSString kSelectedCellAccessibilityHintKey { get; }

		// extern NSString *const _Nonnull kDeselectedCellAccessibilityHintKey;
		[Field ("kDeselectedCellAccessibilityHintKey","__Internal")]
		NSString kDeselectedCellAccessibilityHintKey { get; }
	}

	// @interface MDCCollectionViewCell : UICollectionViewCell
	[BaseType (typeof (UICollectionViewCell))]
	interface MDCCollectionViewCell
	{
		// @property (nonatomic) MDCCollectionViewCellAccessoryType accessoryType;
		[Export ("accessoryType",ArgumentSemantic.Assign)]
		MDCCollectionViewCellAccessoryType AccessoryType { get; set; }

		// @property (nonatomic, strong) UIView * _Nullable accessoryView;
		[NullAllowed, Export ("accessoryView",ArgumentSemantic.Strong)]
		UIView AccessoryView { get; set; }

		// @property (nonatomic) UIEdgeInsets accessoryInset;
		[Export ("accessoryInset",ArgumentSemantic.Assign)]
		UIEdgeInsets AccessoryInset { get; set; }

		// @property (nonatomic) BOOL shouldHideSeparator;
		[Export ("shouldHideSeparator")]
		bool ShouldHideSeparator { get; set; }

		// @property (nonatomic) UIEdgeInsets separatorInset;
		[Export ("separatorInset",ArgumentSemantic.Assign)]
		UIEdgeInsets SeparatorInset { get; set; }

		// @property (nonatomic) BOOL allowsCellInteractionsWhileEditing;
		[Export ("allowsCellInteractionsWhileEditing")]
		bool AllowsCellInteractionsWhileEditing { get; set; }

		// @property (getter = isEditing, nonatomic) BOOL editing;
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified editingSelectorColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("editingSelectorColor",ArgumentSemantic.Strong)]
		UIColor EditingSelectorColor { get; set; }

		// -(void)setEditing:(BOOL)editing animated:(BOOL)animated;
		[Export ("setEditing:animated:")]
		void SetEditing (bool editing,bool animated);

		// @property (nonatomic, strong) MDCInkView * _Nullable inkView;
		[NullAllowed, Export ("inkView",ArgumentSemantic.Strong)]
		MDCInkView InkView { get; set; }
	}

	// @protocol MDCCollectionViewEditing <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCCollectionViewEditing
	{
		// @required @property (readonly, nonatomic, weak) UICollectionView * _Nullable collectionView;
		[Abstract]
		[NullAllowed, Export ("collectionView",ArgumentSemantic.Weak)]
		UICollectionView CollectionView { get; }

		// @required @property (nonatomic, weak) id<MDCCollectionViewEditingDelegate> _Nullable delegate;
		[Abstract]
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		IMDCCollectionViewEditingDelegate Delegate { get; set; }

		// @required @property (readonly, nonatomic, strong) NSIndexPath * _Nullable reorderingCellIndexPath;
		[Abstract]
		[NullAllowed, Export ("reorderingCellIndexPath",ArgumentSemantic.Strong)]
		NSIndexPath ReorderingCellIndexPath { get; }

		// @required @property (readonly, nonatomic, strong) NSIndexPath * _Nullable dismissingCellIndexPath;
		[Abstract]
		[NullAllowed, Export ("dismissingCellIndexPath",ArgumentSemantic.Strong)]
		NSIndexPath DismissingCellIndexPath { get; }

		// @required @property (readonly, assign, nonatomic) NSInteger dismissingSection;
		[Abstract]
		[Export ("dismissingSection")]
		nint DismissingSection { get; }

		// @required @property (getter = isEditing, nonatomic) BOOL editing;
		[Abstract]
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; set; }

		// @required -(void)setEditing:(BOOL)editing animated:(BOOL)animated;
		[Abstract]
		[Export ("setEditing:animated:")]
		void Animated (bool editing,bool animated);
	}

	interface IMDCCollectionViewEditingDelegate { }

	// @protocol MDCCollectionViewEditingDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCCollectionViewEditingDelegate
	{
		// @optional -(BOOL)collectionViewAllowsEditing:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewAllowsEditing:")]
		bool CollectionViewAllowsEditing (UICollectionView collectionView);

		// @optional -(void)collectionViewWillBeginEditing:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewWillBeginEditing:")]
		void CollectionViewWillBeginEditing (UICollectionView collectionView);

		// @optional -(void)collectionViewDidBeginEditing:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewDidBeginEditing:")]
		void CollectionViewDidBeginEditing (UICollectionView collectionView);

		// @optional -(void)collectionViewWillEndEditing:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewWillEndEditing:")]
		void CollectionViewWillEndEditing (UICollectionView collectionView);

		// @optional -(void)collectionViewDidEndEditing:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewDidEndEditing:")]
		void CollectionViewDidEndEditing (UICollectionView collectionView);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canEditItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:canEditItemAtIndexPath:")]
		bool CollectionView100 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canSelectItemDuringEditingAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:canSelectItemDuringEditingAtIndexPath:")]
		bool CollectionView268476 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionViewAllowsReordering:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewAllowsReordering:")]
		bool CollectionViewAllowsReordering (UICollectionView collectionView);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canMoveItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:canMoveItemAtIndexPath:")]
		bool CollectionView35634567 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canMoveItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath toIndexPath:(NSIndexPath * _Nonnull)newIndexPath;
		[Export ("collectionView:canMoveItemAtIndexPath:toIndexPath:")]
		bool CollectionView101 (UICollectionView collectionView,NSIndexPath indexPath,NSIndexPath newIndexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willMoveItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath toIndexPath:(NSIndexPath * _Nonnull)newIndexPath;
		[Export ("collectionView:willMoveItemAtIndexPath:toIndexPath:")]
		void CollectionView4 (UICollectionView collectionView,NSIndexPath indexPath,NSIndexPath newIndexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didMoveItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath toIndexPath:(NSIndexPath * _Nonnull)newIndexPath;
		[Export ("collectionView:didMoveItemAtIndexPath:toIndexPath:")]
		void CollectionView5 (UICollectionView collectionView,NSIndexPath indexPath,NSIndexPath newIndexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willBeginDraggingItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:willBeginDraggingItemAtIndexPath:")]
		void CollectionView6564576 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didEndDraggingItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:didEndDraggingItemAtIndexPath:")]
		void CollectionView75636475478 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willDeleteItemsAtIndexPaths:(NSArray<NSIndexPath *> * _Nonnull)indexPaths;
		[Export ("collectionView:willDeleteItemsAtIndexPaths:")]
		void CollectionView (UICollectionView collectionView,NSIndexPath[] indexPaths);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didDeleteItemsAtIndexPaths:(NSArray<NSIndexPath *> * _Nonnull)indexPaths;
		[Export ("collectionView:didDeleteItemsAtIndexPaths:")]
		void CollectionView8 (UICollectionView collectionView,NSIndexPath[] indexPaths);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willDeleteSections:(NSIndexSet * _Nonnull)sections;
		[Export ("collectionView:willDeleteSections:")]
		void CollectionView102 (UICollectionView collectionView,NSIndexSet sections);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didDeleteSections:(NSIndexSet * _Nonnull)sections;
		[Export ("collectionView:didDeleteSections:")]
		void CollectionView9 (UICollectionView collectionView,NSIndexSet sections);

		// @optional -(BOOL)collectionViewAllowsSwipeToDismissItem:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewAllowsSwipeToDismissItem:")]
		bool CollectionViewAllowsSwipeToDismissItem (UICollectionView collectionView);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canSwipeToDismissItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:canSwipeToDismissItemAtIndexPath:")]
		bool CollectionView10 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willBeginSwipeToDismissItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:willBeginSwipeToDismissItemAtIndexPath:")]
		void CollectionView11 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didEndSwipeToDismissItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:didEndSwipeToDismissItemAtIndexPath:")]
		void CollectionView12 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didCancelSwipeToDismissItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:didCancelSwipeToDismissItemAtIndexPath:")]
		void CollectionView13 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionViewAllowsSwipeToDismissSection:(UICollectionView * _Nonnull)collectionView;
		[Export ("collectionViewAllowsSwipeToDismissSection:")]
		bool CollectionViewAllowsSwipeToDismissSection (UICollectionView collectionView);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView canSwipeToDismissSection:(NSInteger)section;
		[Export ("collectionView:canSwipeToDismissSection:")]
		bool CollectionView1000 (UICollectionView collectionView,nint section);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView willBeginSwipeToDismissSection:(NSInteger)section;
		[Export ("collectionView:willBeginSwipeToDismissSection:")]
		void CollectionView14 (UICollectionView collectionView,nint section);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didEndSwipeToDismissSection:(NSInteger)section;
		[Export ("collectionView:didEndSwipeToDismissSection:")]
		void CollectionView15 (UICollectionView collectionView,nint section);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didCancelSwipeToDismissSection:(NSInteger)section;
		[Export ("collectionView:didCancelSwipeToDismissSection:")]
		void CollectionView16 (UICollectionView collectionView,nint section);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants4
	{
		// extern const CGFloat MDCCollectionViewCellStyleCardSectionInset;
		[Field ("MDCCollectionViewCellStyleCardSectionInset","__Internal")]
		nfloat MDCCollectionViewCellStyleCardSectionInset { get; }
	}

	// @protocol MDCCollectionViewStyling <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCCollectionViewStyling
	{
		// @required @property (readonly, nonatomic, weak) UICollectionView * _Nullable collectionView;
		[Abstract]
		[NullAllowed, Export ("collectionView",ArgumentSemantic.Weak)]
		UICollectionView CollectionView { get; }

		// @required @property (nonatomic, weak) id<MDCCollectionViewStylingDelegate> _Nullable delegate;
		[Abstract]
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		IMDCCollectionViewStylingDelegate Delegate { get; set; }

		// @required @property (assign, nonatomic) BOOL shouldInvalidateLayout;
		[Abstract]
		[Export ("shouldInvalidateLayout")]
		bool ShouldInvalidateLayout { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Nonnull cellBackgroundColor;
		[Abstract]
		[Export ("cellBackgroundColor",ArgumentSemantic.Strong)]
		UIColor CellBackgroundColor { get; set; }

		// @required @property (assign, nonatomic) MDCCollectionViewCellLayoutType cellLayoutType;
		[Abstract]
		[Export ("cellLayoutType",ArgumentSemantic.Assign)]
		MDCCollectionViewCellLayoutType CellLayoutType { get; set; }

		// @required @property (assign, nonatomic) NSInteger gridColumnCount;
		[Abstract]
		[Export ("gridColumnCount")]
		nint GridColumnCount { get; set; }

		// @required @property (assign, nonatomic) CGFloat gridPadding;
		[Abstract]
		[Export ("gridPadding")]
		nfloat GridPadding { get; set; }

		// @required @property (assign, nonatomic) MDCCollectionViewCellStyle cellStyle;
		[Abstract]
		[Export ("cellStyle",ArgumentSemantic.Assign)]
		MDCCollectionViewCellStyle CellStyle { get; set; }

		// @required -(void)setCellStyle:(MDCCollectionViewCellStyle)cellStyle animated:(BOOL)animated;
		[Abstract]
		[Export ("setCellStyle:animated:")]
		void SetCellStyle (MDCCollectionViewCellStyle cellStyle,bool animated);

		// @required -(MDCCollectionViewCellStyle)cellStyleAtSectionIndex:(NSInteger)section;
		[Abstract]
		[Export ("cellStyleAtSectionIndex:")]
		MDCCollectionViewCellStyle CellStyleAtSectionIndex (nint section);

		// @required -(UIEdgeInsets)backgroundImageViewOutsetsForCellWithAttribute:(MDCCollectionViewLayoutAttributes * _Nonnull)attr;
		[Abstract]
		[Export ("backgroundImageViewOutsetsForCellWithAttribute:")]
		UIEdgeInsets BackgroundImageViewOutsetsForCellWithAttribute (MDCCollectionViewLayoutAttributes attr);

		// @required -(UIImage * _Nullable)backgroundImageForCellLayoutAttributes:(MDCCollectionViewLayoutAttributes * _Nonnull)attr;
		[Abstract]
		[Export ("backgroundImageForCellLayoutAttributes:")]
		[return: NullAllowed]
		UIImage BackgroundImageForCellLayoutAttributes (MDCCollectionViewLayoutAttributes attr);

		// @required @property (nonatomic, strong) UIColor * _Nullable separatorColor;
		[Abstract]
		[NullAllowed, Export ("separatorColor",ArgumentSemantic.Strong)]
		UIColor SeparatorColor { get; set; }

		// @required @property (nonatomic) UIEdgeInsets separatorInset;
		[Abstract]
		[Export ("separatorInset",ArgumentSemantic.Assign)]
		UIEdgeInsets SeparatorInset { get; set; }

		// @required @property (nonatomic) CGFloat separatorLineHeight;
		[Abstract]
		[Export ("separatorLineHeight")]
		nfloat SeparatorLineHeight { get; set; }

		// @required @property (nonatomic) BOOL shouldHideSeparators;
		[Abstract]
		[Export ("shouldHideSeparators")]
		bool ShouldHideSeparators { get; set; }

		// @required -(BOOL)shouldHideSeparatorForCellLayoutAttributes:(MDCCollectionViewLayoutAttributes * _Nonnull)attr;
		[Abstract]
		[Export ("shouldHideSeparatorForCellLayoutAttributes:")]
		bool ShouldHideSeparatorForCellLayoutAttributes (MDCCollectionViewLayoutAttributes attr);

		// @required @property (assign, nonatomic) BOOL allowsItemInlay;
		[Abstract]
		[Export ("allowsItemInlay")]
		bool AllowsItemInlay { get; set; }

		// @required @property (assign, nonatomic) BOOL allowsMultipleItemInlays;
		[Abstract]
		[Export ("allowsMultipleItemInlays")]
		bool AllowsMultipleItemInlays { get; set; }

		// @required -(NSArray<NSIndexPath *> * _Nullable)indexPathsForInlaidItems;
		[Abstract]
		[NullAllowed, Export ("indexPathsForInlaidItems")]
		NSIndexPath[] IndexPathsForInlaidItems { get; }

		// @required -(BOOL)isItemInlaidAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Abstract]
		[Export ("isItemInlaidAtIndexPath:")]
		bool IsItemInlaidAtIndexPath (NSIndexPath indexPath);

		// @required -(void)applyInlayToItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath animated:(BOOL)animated;
		[Abstract]
		[Export ("applyInlayToItemAtIndexPath:animated:")]
		void ApplyInlayToItemAtIndexPath (NSIndexPath indexPath,bool animated);

		// @required -(void)removeInlayFromItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath animated:(BOOL)animated;
		[Abstract]
		[Export ("removeInlayFromItemAtIndexPath:animated:")]
		void RemoveInlayFromItemAtIndexPath (NSIndexPath indexPath,bool animated);

		// @required -(void)applyInlayToAllItemsAnimated:(BOOL)animated;
		[Abstract]
		[Export ("applyInlayToAllItemsAnimated:")]
		void ApplyInlayToAllItemsAnimated (bool animated);

		// @required -(void)removeInlayFromAllItemsAnimated:(BOOL)animated;
		[Abstract]
		[Export ("removeInlayFromAllItemsAnimated:")]
		void RemoveInlayFromAllItemsAnimated (bool animated);

		// @required -(void)resetIndexPathsForInlaidItems;
		[Abstract]
		[Export ("resetIndexPathsForInlaidItems")]
		void ResetIndexPathsForInlaidItems ();

		// @required @property (assign, nonatomic) BOOL shouldAnimateCellsOnAppearance;
		[Abstract]
		[Export ("shouldAnimateCellsOnAppearance")]
		bool ShouldAnimateCellsOnAppearance { get; set; }

		// @required @property (readonly, assign, nonatomic) BOOL willAnimateCellsOnAppearance;
		[Abstract]
		[Export ("willAnimateCellsOnAppearance")]
		bool WillAnimateCellsOnAppearance { get; }

		// @required @property (readonly, assign, nonatomic) CGFloat animateCellsOnAppearancePadding;
		[Abstract]
		[Export ("animateCellsOnAppearancePadding")]
		nfloat AnimateCellsOnAppearancePadding { get; }

		// @required @property (readonly, assign, nonatomic) NSTimeInterval animateCellsOnAppearanceDuration;
		[Abstract]
		[Export ("animateCellsOnAppearanceDuration")]
		double AnimateCellsOnAppearanceDuration { get; }

		// @required -(void)beginCellAppearanceAnimation;
		[Abstract]
		[Export ("beginCellAppearanceAnimation")]
		void BeginCellAppearanceAnimation ();
	}

	interface IMDCCollectionViewStylingDelegate {}

	// @protocol MDCCollectionViewStylingDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCCollectionViewStylingDelegate
	{
		// @optional -(CGFloat)collectionView:(UICollectionView * _Nonnull)collectionView cellHeightAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:cellHeightAtIndexPath:")]
		nfloat CollectionView201 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(MDCCollectionViewCellStyle)collectionView:(UICollectionView * _Nonnull)collectionView cellStyleForSection:(NSInteger)section;
		[Export ("collectionView:cellStyleForSection:")]
		MDCCollectionViewCellStyle CollectionView (UICollectionView collectionView,nint section);

		// @optional -(UIColor * _Nullable)collectionView:(UICollectionView * _Nonnull)collectionView cellBackgroundColorAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:cellBackgroundColorAtIndexPath:")]
		[return: NullAllowed]
		UIColor CollectionView2453457 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideItemBackgroundAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:shouldHideItemBackgroundAtIndexPath:")]
		bool CollectionView3574568 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideHeaderBackgroundForSection:(NSInteger)section;
		[Export ("collectionView:shouldHideHeaderBackgroundForSection:")]
		bool CollectionView4 (UICollectionView collectionView,nint section);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideFooterBackgroundForSection:(NSInteger)section;
		[Export ("collectionView:shouldHideFooterBackgroundForSection:")]
		bool CollectionView5 (UICollectionView collectionView,nint section);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideItemSeparatorAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:shouldHideItemSeparatorAtIndexPath:")]
		bool CollectionView6345235 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideHeaderSeparatorForSection:(NSInteger)section;
		[Export ("collectionView:shouldHideHeaderSeparatorForSection:")]
		bool CollectionView7 (UICollectionView collectionView,nint section);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHideFooterSeparatorForSection:(NSInteger)section;
		[Export ("collectionView:shouldHideFooterSeparatorForSection:")]
		bool CollectionView8 (UICollectionView collectionView,nint section);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didApplyInlayToItemAtIndexPaths:(NSArray<NSIndexPath *> * _Nonnull)indexPaths;
		[Export ("collectionView:didApplyInlayToItemAtIndexPaths:")]
		void CollectionView202 (UICollectionView collectionView,NSIndexPath[] indexPaths);

		// @optional -(void)collectionView:(UICollectionView * _Nonnull)collectionView didRemoveInlayFromItemAtIndexPaths:(NSArray<NSIndexPath *> * _Nonnull)indexPaths;
		[Export ("collectionView:didRemoveInlayFromItemAtIndexPaths:")]
		void CollectionView9 (UICollectionView collectionView,NSIndexPath[] indexPaths);

		// @optional -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView hidesInkViewAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:hidesInkViewAtIndexPath:")]
		bool CollectionView10000 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(UIColor * _Nullable)collectionView:(UICollectionView * _Nonnull)collectionView inkColorAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:inkColorAtIndexPath:")]
		[return: NullAllowed]
		UIColor CollectionView1123546 (UICollectionView collectionView,NSIndexPath indexPath);

		// @optional -(MDCInkView * _Nonnull)collectionView:(UICollectionView * _Nonnull)collectionView inkTouchController:(MDCInkTouchController * _Nonnull)inkTouchController inkViewAtIndexPath:(NSIndexPath * _Nonnull)indexPath;
		[Export ("collectionView:inkTouchController:inkViewAtIndexPath:")]
		MDCInkView CollectionView203 (UICollectionView collectionView,MDCInkTouchController inkTouchController,NSIndexPath indexPath);
	}

	// @interface MDCCollectionViewController : UICollectionViewController <MDCCollectionViewEditingDelegate, MDCCollectionViewStylingDelegate, UICollectionViewDelegateFlowLayout>
	[BaseType (typeof (UICollectionViewController))]
	interface MDCCollectionViewController : MDCCollectionViewEditingDelegate, MDCCollectionViewStylingDelegate, IUICollectionViewDelegateFlowLayout
	{
		// @property (readonly, nonatomic, strong) id<MDCCollectionViewStyling> _Nonnull styler;
		[Export ("styler",ArgumentSemantic.Strong)]
		MDCCollectionViewStyling Styler { get; }

		// @property (readonly, nonatomic, strong) id<MDCCollectionViewEditing> _Nonnull editor;
		[Export ("editor",ArgumentSemantic.Strong)]
		MDCCollectionViewEditing Editor { get; }

		// -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldHighlightItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:shouldHighlightItemAtIndexPath:")]
		[RequiresSuper]
		bool CollectionView (UICollectionView collectionView,NSIndexPath indexPath);

		// -(void)collectionView:(UICollectionView * _Nonnull)collectionView didHighlightItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:didHighlightItemAtIndexPath:")]
		[RequiresSuper]
		void CollectionView2 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(void)collectionView:(UICollectionView * _Nonnull)collectionView didUnhighlightItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:didUnhighlightItemAtIndexPath:")]
		[RequiresSuper]
		void CollectionView3 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldSelectItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:shouldSelectItemAtIndexPath:")]
		[RequiresSuper]
		bool CollectionView4 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(BOOL)collectionView:(UICollectionView * _Nonnull)collectionView shouldDeselectItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:shouldDeselectItemAtIndexPath:")]
		[RequiresSuper]
		bool CollectionView5 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(void)collectionView:(UICollectionView * _Nonnull)collectionView didSelectItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:didSelectItemAtIndexPath:")]
		[RequiresSuper]
		void CollectionView6 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(void)collectionView:(UICollectionView * _Nonnull)collectionView didDeselectItemAtIndexPath:(NSIndexPath * _Nonnull)indexPath __attribute__((objc_requires_super));
		[Export ("collectionView:didDeselectItemAtIndexPath:")]
		[RequiresSuper]
		void CollectionView7 (UICollectionView collectionView,NSIndexPath indexPath);

		// -(void)collectionViewWillBeginEditing:(UICollectionView * _Nonnull)collectionView __attribute__((objc_requires_super));
		[Export ("collectionViewWillBeginEditing:")]
		[RequiresSuper]
		void CollectionViewWillBeginEditing (UICollectionView collectionView);

		// -(void)collectionViewWillEndEditing:(UICollectionView * _Nonnull)collectionView __attribute__((objc_requires_super));
		[Export ("collectionViewWillEndEditing:")]
		[RequiresSuper]
		void CollectionViewWillEndEditing (UICollectionView collectionView);

		// -(CGFloat)cellWidthAtSectionIndex:(NSInteger)section;
		[Export ("cellWidthAtSectionIndex:")]
		nfloat CellWidthAtSectionIndex (nint section);
	}

	// @interface MDCCollectionViewFlowLayout : UICollectionViewFlowLayout
	[BaseType (typeof (UICollectionViewFlowLayout))]
	interface MDCCollectionViewFlowLayout
	{
	}

	// @interface MDCCollectionViewLayoutAttributes : UICollectionViewLayoutAttributes <NSCopying>
	[BaseType (typeof (UICollectionViewLayoutAttributes))]
	interface MDCCollectionViewLayoutAttributes : INSCopying
	{
		// @property (getter = isEditing, nonatomic) BOOL editing;
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; set; }

		// @property (assign, nonatomic) BOOL shouldShowReorderStateMask;
		[Export ("shouldShowReorderStateMask")]
		bool ShouldShowReorderStateMask { get; set; }

		// @property (assign, nonatomic) BOOL shouldShowSelectorStateMask;
		[Export ("shouldShowSelectorStateMask")]
		bool ShouldShowSelectorStateMask { get; set; }

		// @property (assign, nonatomic) BOOL shouldShowGridBackground;
		[Export ("shouldShowGridBackground")]
		bool ShouldShowGridBackground { get; set; }

		// @property (nonatomic, strong) UIImage * _Nullable backgroundImage;
		[NullAllowed, Export ("backgroundImage",ArgumentSemantic.Strong)]
		UIImage BackgroundImage { get; set; }

		// @property (nonatomic) UIEdgeInsets backgroundImageViewInsets;
		[Export ("backgroundImageViewInsets",ArgumentSemantic.Assign)]
		UIEdgeInsets BackgroundImageViewInsets { get; set; }

		// @property (assign, nonatomic) BOOL isGridLayout;
		[Export ("isGridLayout")]
		bool IsGridLayout { get; set; }

		// @property (assign, nonatomic) MDCCollectionViewOrdinalPosition sectionOrdinalPosition;
		[Export ("sectionOrdinalPosition",ArgumentSemantic.Assign)]
		MDCCollectionViewOrdinalPosition SectionOrdinalPosition { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable separatorColor;
		[NullAllowed, Export ("separatorColor",ArgumentSemantic.Strong)]
		UIColor SeparatorColor { get; set; }

		// @property (nonatomic) UIEdgeInsets separatorInset;
		[Export ("separatorInset",ArgumentSemantic.Assign)]
		UIEdgeInsets SeparatorInset { get; set; }

		// @property (nonatomic) CGFloat separatorLineHeight;
		[Export ("separatorLineHeight")]
		nfloat SeparatorLineHeight { get; set; }

		// @property (nonatomic) BOOL shouldHideSeparators;
		[Export ("shouldHideSeparators")]
		bool ShouldHideSeparators { get; set; }

		// @property (assign, nonatomic) BOOL willAnimateCellsOnAppearance;
		[Export ("willAnimateCellsOnAppearance")]
		bool WillAnimateCellsOnAppearance { get; set; }

		// @property (assign, nonatomic) NSTimeInterval animateCellsOnAppearanceDuration;
		[Export ("animateCellsOnAppearanceDuration")]
		double AnimateCellsOnAppearanceDuration { get; set; }

		// @property (assign, nonatomic) NSTimeInterval animateCellsOnAppearanceDelay;
		[Export ("animateCellsOnAppearanceDelay")]
		double AnimateCellsOnAppearanceDelay { get; set; }
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants5
	{
		// extern const CGFloat MDCCellDefaultOneLineHeight;
		[Field ("MDCCellDefaultOneLineHeight","__Internal")]
		nfloat MDCCellDefaultOneLineHeight { get; }

		// extern const CGFloat MDCCellDefaultOneLineWithAvatarHeight;
		[Field ("MDCCellDefaultOneLineWithAvatarHeight","__Internal")]
		nfloat MDCCellDefaultOneLineWithAvatarHeight { get; }

		// extern const CGFloat MDCCellDefaultTwoLineHeight;
		[Field ("MDCCellDefaultTwoLineHeight","__Internal")]
		nfloat MDCCellDefaultTwoLineHeight { get; }

		// extern const CGFloat MDCCellDefaultThreeLineHeight;
		[Field ("MDCCellDefaultThreeLineHeight","__Internal")]
		nfloat MDCCellDefaultThreeLineHeight { get; }
	}

	// @interface MDCCollectionViewTextCell : MDCCollectionViewCell
	[BaseType (typeof (MDCCollectionViewCell))]
	interface MDCCollectionViewTextCell
	{
		// @property (readonly, nonatomic, strong) UILabel * _Nullable textLabel;
		[NullAllowed, Export ("textLabel",ArgumentSemantic.Strong)]
		UILabel TextLabel { get; }

		// @property (readonly, nonatomic, strong) UILabel * _Nullable detailTextLabel;
		[NullAllowed, Export ("detailTextLabel",ArgumentSemantic.Strong)]
		UILabel DetailTextLabel { get; }

		// @property (readonly, nonatomic, strong) UIImageView * _Nullable imageView;
		[NullAllowed, Export ("imageView",ArgumentSemantic.Strong)]
		UIImageView ImageView { get; }
	}

	// @interface MDCCornerTreatment : NSObject <NSCopying, NSSecureCoding>
	[BaseType (typeof (NSObject))]
	interface MDCCornerTreatment : INSCopying, INSSecureCoding
	{
		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed?

		// -(MDCPathGenerator * _Nonnull)pathGeneratorForCornerWithAngle:(CGFloat)angle;
		[Export ("pathGeneratorForCornerWithAngle:")]
		MDCPathGenerator PathGeneratorForCornerWithAngle (nfloat angle);
	}

	// @interface MDCCurvedCornerTreatment : MDCCornerTreatment
	[BaseType (typeof (MDCCornerTreatment))]
	interface MDCCurvedCornerTreatment
	{
		// @property (assign, nonatomic) CGSize size;
		[Export ("size",ArgumentSemantic.Assign)]
		CGSize Size { get; set; }
	}

	// @protocol MDCShapeGenerating <NSCopying, NSSecureCoding>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCShapeGenerating : INSCopying, INSSecureCoding
	{
		// @required -(CGPathRef _Nullable)pathForSize:(CGSize)size;
		[Abstract]
		[Export ("pathForSize:")]
		[return: NullAllowed]
		//unsafe CGPathRef* PathForSize (CGSize size); TODO Check this
		unsafe CGPath PathForSize (CGSize size);
	}

	// @interface MDCCurvedRectShapeGenerator : NSObject <MDCShapeGenerating>
	[BaseType (typeof (NSObject))]
	interface MDCCurvedRectShapeGenerator : MDCShapeGenerating
	{
		// @property (assign, nonatomic) CGSize cornerSize;
		[Export ("cornerSize",ArgumentSemantic.Assign)]
		CGSize CornerSize { get; set; }

		// -(instancetype)initWithCornerSize:(CGSize)cornerSize __attribute__((objc_designated_initializer));
		[Export ("initWithCornerSize:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGSize cornerSize);

		//// -(instancetype)initWithCoder:(NSCoder *)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed
	}

	// @interface MDCEdgeTreatment : NSObject <NSCopying, NSSecureCoding>
	[BaseType (typeof (NSObject))]
	interface MDCEdgeTreatment : INSCopying, INSSecureCoding
	{
		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed

		// -(MDCPathGenerator * _Nonnull)pathGeneratorForEdgeWithLength:(CGFloat)length;
		[Export ("pathGeneratorForEdgeWithLength:")]
		MDCPathGenerator PathGeneratorForEdgeWithLength (nfloat length);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants6
	{
		// extern const CGFloat kMDCFeatureHighlightOuterHighlightAlpha;
		[Field ("kMDCFeatureHighlightOuterHighlightAlpha","__Internal")]
		nfloat kMDCFeatureHighlightOuterHighlightAlpha { get; }
	}

	// typedef void (^MDCFeatureHighlightCompletion)(BOOL);
	delegate void MDCFeatureHighlightCompletion (bool arg0);

	// @interface MDCFeatureHighlightViewController : UIViewController
	[BaseType (typeof (UIViewController))]
	[DisableDefaultCtor]
	interface MDCFeatureHighlightViewController
	{
		// -(instancetype _Nonnull)initWithHighlightedView:(UIView * _Nonnull)highlightedView andShowView:(UIView * _Nonnull)displayedView completion:(MDCFeatureHighlightCompletion _Nullable)completion __attribute__((objc_designated_initializer));
		[Export ("initWithHighlightedView:andShowView:completion:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIView highlightedView,UIView displayedView,[NullAllowed] MDCFeatureHighlightCompletion completion);

		// -(instancetype _Nonnull)initWithHighlightedView:(UIView * _Nonnull)highlightedView completion:(MDCFeatureHighlightCompletion _Nullable)completion;
		[Export ("initWithHighlightedView:completion:")]
		IntPtr Constructor (UIView highlightedView,[NullAllowed] MDCFeatureHighlightCompletion completion);

		// @property (copy, nonatomic) NSString * _Nullable titleText;
		[NullAllowed, Export ("titleText")]
		string TitleText { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable bodyText;
		[NullAllowed, Export ("bodyText")]
		string BodyText { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified outerHighlightColor;
		[Export ("outerHighlightColor",ArgumentSemantic.Strong)]
		UIColor OuterHighlightColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified innerHighlightColor;
		[Export ("innerHighlightColor",ArgumentSemantic.Strong)]
		UIColor InnerHighlightColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable titleColor;
		[NullAllowed, Export ("titleColor",ArgumentSemantic.Strong)]
		UIColor TitleColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable bodyColor;
		[NullAllowed, Export ("bodyColor",ArgumentSemantic.Strong)]
		UIColor BodyColor { get; set; }

		// @property (readwrite, nonatomic, setter = mdc_setAdjustsFontForContentSizeCategory:) BOOL mdc_adjustsFontForContentSizeCategory __attribute__((annotate("ui_appearance_selector")));
		[Export ("mdc_adjustsFontForContentSizeCategory")]
		bool Mdc_adjustsFontForContentSizeCategory { get; [Bind ("mdc_setAdjustsFontForContentSizeCategory:")] set; }

		// -(void)acceptFeature;
		[Export ("acceptFeature")]
		void AcceptFeature ();

		// -(void)rejectFeature;
		[Export ("rejectFeature")]
		void RejectFeature ();
	}

	// @interface MDCFeatureHighlightView : UIView
	[BaseType (typeof (UIView))]
	interface MDCFeatureHighlightView
	{
		// @property (nonatomic, strong) UIColor * _Nullable innerHighlightColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("innerHighlightColor",ArgumentSemantic.Strong)]
		UIColor InnerHighlightColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable outerHighlightColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("outerHighlightColor",ArgumentSemantic.Strong)]
		UIColor OuterHighlightColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable titleColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("titleColor",ArgumentSemantic.Strong)]
		UIColor TitleColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable bodyColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("bodyColor",ArgumentSemantic.Strong)]
		UIColor BodyColor { get; set; }
	}

	// @interface MDCFeatureHighlightColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCFeatureHighlightColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toFeatureHighlightView:(MDCFeatureHighlightView *)featureHighlightView;
		[Static]
		[Export ("applyColorScheme:toFeatureHighlightView:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCFeatureHighlightView featureHighlightView);
	}

	// @interface MDCFlexibleHeaderColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCFlexibleHeaderColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toFlexibleHeaderView:(MDCFlexibleHeaderView *)flexibleHeaderView;
		[Static]
		[Export ("applyColorScheme:toFlexibleHeaderView:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCFlexibleHeaderView flexibleHeaderView);

		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toMDCFlexibleHeaderController:(MDCFlexibleHeaderViewController *)flexibleHeaderController;
		[Static]
		[Export ("applyColorScheme:toMDCFlexibleHeaderController:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCFlexibleHeaderViewController flexibleHeaderController);
	}

	// @interface MDCHeaderStackViewColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCHeaderStackViewColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toHeaderStackView:(MDCHeaderStackView *)headerStackView;
		[Static]
		[Export ("applyColorScheme:toHeaderStackView:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCHeaderStackView headerStackView);
	}

	// @interface MDCIcons : NSObject
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCIcons
	{
	}

	// @interface BundleLoader (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_BundleLoader
	{
		// +(NSString * _Nonnull)pathForIconName:(NSString * _Nonnull)iconName withBundleName:(NSString * _Nonnull)bundleName;
		[Static]
		[Export ("pathForIconName:withBundleName:")]
		string PathForIconName (string iconName,string bundleName);

		// +(NSBundle * _Nullable)bundleNamed:(NSString * _Nonnull)bundleName;
		[Static]
		[Export ("bundleNamed:")]
		[return: NullAllowed]
		NSBundle BundleNamed (string bundleName);
	}

	// @interface MDCInkColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCInkColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toInkView:(MDCInkView *)inkView;
		[Static]
		[Export ("applyColorScheme:toInkView:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCInkView inkView);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants7
	{
		// extern NSString *const MDCKeyboardWatcherKeyboardWillShowNotification;
		[Field ("MDCKeyboardWatcherKeyboardWillShowNotification","__Internal")]
		NSString MDCKeyboardWatcherKeyboardWillShowNotification { get; }

		// extern NSString *const MDCKeyboardWatcherKeyboardWillHideNotification;
		[Field ("MDCKeyboardWatcherKeyboardWillHideNotification","__Internal")]
		NSString MDCKeyboardWatcherKeyboardWillHideNotification { get; }

		// extern NSString *const MDCKeyboardWatcherKeyboardWillChangeFrameNotification;
		[Field ("MDCKeyboardWatcherKeyboardWillChangeFrameNotification","__Internal")]
		NSString MDCKeyboardWatcherKeyboardWillChangeFrameNotification { get; }
	}

	// @interface MDCKeyboardWatcher : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCKeyboardWatcher
	{
		// +(instancetype)sharedKeyboardWatcher;
		[Static]
		[Export ("sharedKeyboardWatcher")]
		MDCKeyboardWatcher SharedKeyboardWatcher ();

		// +(NSTimeInterval)animationDurationFromKeyboardNotification:(NSNotification *)notification;
		[Static]
		[Export ("animationDurationFromKeyboardNotification:")]
		double AnimationDurationFromKeyboardNotification (NSNotification notification);

		// +(UIViewAnimationOptions)animationCurveOptionFromKeyboardNotification:(NSNotification *)notification;
		[Static]
		[Export ("animationCurveOptionFromKeyboardNotification:")]
		UIViewAnimationOptions AnimationCurveOptionFromKeyboardNotification (NSNotification notification);

		// @property (readonly, nonatomic) CGFloat visibleKeyboardHeight;
		[Export ("visibleKeyboardHeight")]
		nfloat VisibleKeyboardHeight { get; }

		// @property (readonly, nonatomic) CGFloat keyboardOffset __attribute__((deprecated("Use visibleKeyboardHeight instead of keyboardOffset")));
		[Export ("keyboardOffset")]
		nfloat KeyboardOffset { get; }
	}

	// @interface MDCMaskedTransition : NSObject <MDMTransition>
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface MDCMaskedTransition // : MDMTransition This is in a private header
	{
		// -(instancetype _Nonnull)initWithSourceView:(UIView * _Nonnull)sourceView __attribute__((objc_designated_initializer));
		[Export ("initWithSourceView:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIView sourceView);

		// @property (copy, nonatomic) CGRect (^ _Nullable)(UIPresentationController * _Nonnull) calculateFrameOfPresentedView;
		[NullAllowed, Export ("calculateFrameOfPresentedView",ArgumentSemantic.Copy)]
		Func<UIPresentationController,CGRect> CalculateFrameOfPresentedView { get; set; }
	}

	// @protocol MDCTextInput <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTextInput
	{
		// @required @property (copy, nonatomic) NSAttributedString * _Nullable attributedPlaceholder;
		[Abstract]
		[NullAllowed, Export ("attributedPlaceholder",ArgumentSemantic.Copy)]
		NSAttributedString AttributedPlaceholder { get; set; }

		// @required @property (copy, nonatomic) NSAttributedString * _Nullable attributedText;
		[Abstract]
		[NullAllowed, Export ("attributedText",ArgumentSemantic.Copy)]
		NSAttributedString AttributedText { get; set; }

		// @required @property (copy, nonatomic) UIBezierPath * _Nullable borderPath __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[NullAllowed, Export ("borderPath",ArgumentSemantic.Copy)]
		UIBezierPath BorderPath { get; set; }

		// @required @property (nonatomic, strong) MDCTextInputBorderView * _Nullable borderView;
		[Abstract]
		[NullAllowed, Export ("borderView",ArgumentSemantic.Strong)]
		MDCTextInputBorderView BorderView { get; set; }

		// @required @property (readonly, nonatomic, strong) UIButton * _Nonnull clearButton;
		[Abstract]
		[Export ("clearButton",ArgumentSemantic.Strong)]
		UIButton ClearButton { get; }

		// @required @property (assign, nonatomic) UITextFieldViewMode clearButtonMode __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[Export ("clearButtonMode",ArgumentSemantic.Assign)]
		UITextFieldViewMode ClearButtonMode { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Nullable cursorColor __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[NullAllowed, Export ("cursorColor",ArgumentSemantic.Strong)]
		UIColor CursorColor { get; set; }

		// @required @property (readonly, getter = isEditing, assign, nonatomic) BOOL editing;
		[Abstract]
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; }

		// @required @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
		[Abstract]
		[Export ("enabled")]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		// @required @property (nonatomic, strong) UIFont * _Nullable font;
		[Abstract]
		[NullAllowed, Export ("font",ArgumentSemantic.Strong)]
		UIFont Font { get; set; }

		// @required @property (assign, nonatomic) BOOL hidesPlaceholderOnInput;
		[Abstract]
		[Export ("hidesPlaceholderOnInput")]
		bool HidesPlaceholderOnInput { get; set; }

		// @required @property (readonly, nonatomic, strong) UILabel * _Nonnull leadingUnderlineLabel;
		[Abstract]
		[Export ("leadingUnderlineLabel",ArgumentSemantic.Strong)]
		UILabel LeadingUnderlineLabel { get; }

		// @required @property (nonatomic, setter = mdc_setAdjustsFontForContentSizeCategory:) BOOL mdc_adjustsFontForContentSizeCategory __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[Export ("mdc_adjustsFontForContentSizeCategory")]
		bool Mdc_adjustsFontForContentSizeCategory { get; [Bind ("mdc_setAdjustsFontForContentSizeCategory:")] set; }

		// @required @property (copy, nonatomic) NSString * _Nullable placeholder;
		[Abstract]
		[NullAllowed, Export ("placeholder")]
		string Placeholder { get; set; }

		// @required @property (readonly, nonatomic, strong) UILabel * _Nonnull placeholderLabel;
		[Abstract]
		[Export ("placeholderLabel",ArgumentSemantic.Strong)]
		UILabel PlaceholderLabel { get; }

		// @required @property (nonatomic, weak) id<MDCTextInputPositioningDelegate> _Nullable positioningDelegate;
		[Abstract]
		[NullAllowed, Export ("positioningDelegate",ArgumentSemantic.Weak)]
		IMDCTextInputPositioningDelegate PositioningDelegate { get; set; }

		// @required @property (copy, nonatomic) NSString * _Nullable text;
		[Abstract]
		[NullAllowed, Export ("text")]
		string Text { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Nullable textColor;
		[Abstract]
		[NullAllowed, Export ("textColor",ArgumentSemantic.Strong)]
		UIColor TextColor { get; set; }

		// @required @property (readonly, assign, nonatomic) UIEdgeInsets textInsets;
		[Abstract]
		[Export ("textInsets",ArgumentSemantic.Assign)]
		UIEdgeInsets TextInsets { get; }

		// @required @property (assign, nonatomic) MDCTextInputTextInsetsMode textInsetsMode __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[Export ("textInsetsMode",ArgumentSemantic.Assign)]
		MDCTextInputTextInsetsMode TextInsetsMode { get; set; }

		// @required @property (readonly, nonatomic, strong) UILabel * _Nonnull trailingUnderlineLabel;
		[Abstract]
		[Export ("trailingUnderlineLabel",ArgumentSemantic.Strong)]
		UILabel TrailingUnderlineLabel { get; }

		// @required @property (nonatomic, strong) UIView * _Nullable trailingView;
		[Abstract]
		[NullAllowed, Export ("trailingView",ArgumentSemantic.Strong)]
		UIView TrailingView { get; set; }

		// @required @property (assign, nonatomic) UITextFieldViewMode trailingViewMode;
		[Abstract]
		[Export ("trailingViewMode",ArgumentSemantic.Assign)]
		UITextFieldViewMode TrailingViewMode { get; set; }

		// @required @property (readonly, nonatomic, strong) MDCTextInputUnderlineView * _Nullable underline;
		[Abstract]
		[NullAllowed, Export ("underline",ArgumentSemantic.Strong)]
		MDCTextInputUnderlineView Underline { get; }
	}

	// @protocol MDCMultilineTextInput <MDCTextInput>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCMultilineTextInput : MDCTextInput
	{
		// @required @property (assign, nonatomic) BOOL expandsOnOverflow;
		[Abstract]
		[Export ("expandsOnOverflow")]
		bool ExpandsOnOverflow { get; set; }

		// @required @property (assign, nonatomic) NSUInteger minimumLines __attribute__((annotate("ui_appearance_selector")));
		[Abstract]
		[Export ("minimumLines")]
		nuint MinimumLines { get; set; }
	}

	// @interface MDCMultilineTextField : UIView <MDCTextInput, MDCMultilineTextInput>
	[BaseType (typeof (UIView))]
	interface MDCMultilineTextField : MDCTextInput, MDCMultilineTextInput
	{
		// @property (assign, nonatomic) BOOL adjustsFontForContentSizeCategory;
		[Export ("adjustsFontForContentSizeCategory")]
		bool AdjustsFontForContentSizeCategory { get; set; }

		// @property (assign, nonatomic) BOOL expandsOnOverflow;
		[Export ("expandsOnOverflow")]
		bool ExpandsOnOverflow { get; set; }

		[Wrap ("WeakLayoutDelegate")]
		[NullAllowed]
		MDCMultilineTextInputLayoutDelegate LayoutDelegate { get; set; }

		// @property (nonatomic, weak) id<MDCMultilineTextInputLayoutDelegate> _Nullable layoutDelegate __attribute__((iboutlet));
		[NullAllowed, Export ("layoutDelegate",ArgumentSemantic.Weak)]
		NSObject WeakLayoutDelegate { get; set; }

		[Wrap ("WeakMultilineDelegate")]
		[NullAllowed]
		MDCMultilineTextInputDelegate MultilineDelegate { get; set; }

		// @property (nonatomic, weak) id<MDCMultilineTextInputDelegate> _Nullable multilineDelegate __attribute__((iboutlet));
		[NullAllowed, Export ("multilineDelegate",ArgumentSemantic.Weak)]
		NSObject WeakMultilineDelegate { get; set; }

		// @property (copy, nonatomic) NSString * _Nullable placeholder;
		[NullAllowed, Export ("placeholder")]
		string Placeholder { get; set; }

		// @property (readonly, assign, nonatomic) UIEdgeInsets textInsets;
		[Export ("textInsets",ArgumentSemantic.Assign)]
		UIEdgeInsets TextInsets { get; }

		// @property (nonatomic, weak) UITextView * _Nullable textView __attribute__((iboutlet));
		[NullAllowed, Export ("textView",ArgumentSemantic.Weak)]
		UITextView TextView { get; set; }
	}

	// @protocol MDCMultilineTextInputLayoutDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCMultilineTextInputLayoutDelegate
	{
		// @optional -(void)multilineTextField:(id<MDCMultilineTextInput> _Nonnull)multilineTextField didChangeContentSize:(CGSize)size;
		[Export ("multilineTextField:didChangeContentSize:")]
		void DidChangeContentSize (MDCMultilineTextInput multilineTextField,CGSize size);
	}

	// @protocol MDCMultilineTextInputDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCMultilineTextInputDelegate
	{
		// @optional -(BOOL)multilineTextFieldShouldClear:(UIView<MDCTextInput> *)textField;
		[Export ("multilineTextFieldShouldClear:")]
		bool MultilineTextFieldShouldClear (MDCTextInput textField);
	}

	// @interface MDCNavigationBarColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCNavigationBarColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toNavigationBar:(MDCNavigationBar *)navigationBar;
		[Static]
		[Export ("applyColorScheme:toNavigationBar:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCNavigationBar navigationBar);
	}

	// @interface MDCNumericValueLabel : UIView
	[BaseType (typeof (UIView))]
	interface MDCNumericValueLabel
	{
		// @property (retain, nonatomic) UIColor * backgroundColor;
		[Export ("backgroundColor",ArgumentSemantic.Retain)]
		UIColor BackgroundColor { get; set; }

		// @property (retain, nonatomic) UIColor * textColor;
		[Export ("textColor",ArgumentSemantic.Retain)]
		UIColor TextColor { get; set; }

		// @property (nonatomic) CGFloat fontSize;
		[Export ("fontSize")]
		nfloat FontSize { get; set; }

		// @property (copy, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; set; }
	}

	// @interface MDCOverlayObserver : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCOverlayObserver
	{
		// +(instancetype)observerForScreen:(UIScreen *)screen;
		[Static]
		[Export ("observerForScreen:")]
		MDCOverlayObserver ObserverForScreen (UIScreen screen);

		// -(void)addTarget:(id)target action:(SEL)action;
		[Export ("addTarget:action:")]
		void AddTarget (NSObject target,Selector action);

		// -(void)removeTarget:(id)target action:(SEL)action;
		[Export ("removeTarget:action:")]
		void RemoveTarget (NSObject target,Selector action);

		// -(void)removeTarget:(id)target;
		[Export ("removeTarget:")]
		void RemoveTarget (NSObject target);
	}

	// @protocol MDCOverlay <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCOverlay
	{
		// @required @property (readonly, copy, nonatomic) NSString * identifier;
		[Abstract]
		[Export ("identifier")]
		string Identifier { get; }

		// @required @property (readonly, nonatomic) CGRect frame;
		[Abstract]
		[Export ("frame")]
		CGRect Frame { get; }
	}

	// @protocol MDCOverlayTransitioning <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCOverlayTransitioning
	{
		// @required @property (readonly, nonatomic) NSTimeInterval duration;
		[Abstract]
		[Export ("duration")]
		double Duration { get; }

		// @required @property (readonly, nonatomic) CAMediaTimingFunction * customTimingFunction;
		[Abstract]
		[Export ("customTimingFunction")]
		CAMediaTimingFunction CustomTimingFunction { get; }

		// @required @property (readonly, nonatomic) UIViewAnimationCurve animationCurve;
		[Abstract]
		[Export ("animationCurve")]
		UIViewAnimationCurve AnimationCurve { get; }

		// @required @property (readonly, nonatomic) CGRect compositeFrame;
		[Abstract]
		[Export ("compositeFrame")]
		CGRect CompositeFrame { get; }

		// @required -(CGRect)compositeFrameInView:(UIView *)targetView;
		[Abstract]
		[Export ("compositeFrameInView:")]
		CGRect CompositeFrameInView (UIView targetView);

		// @required -(void)enumerateOverlays:(void (^)(id<MDCOverlay>, NSUInteger, BOOL *))handler;
		[Abstract]
		[Export ("enumerateOverlays:")]
		//unsafe void EnumerateOverlays (Action<MDCOverlay,nuint,bool*> handler); TODO
		unsafe void EnumerateOverlays (EnumerateOverlaysHandler handler);

		// @required -(void)animateAlongsideTransition:(void (^)(void))animations;
		[Abstract]
		[Export ("animateAlongsideTransition:")]
		void AnimateAlongsideTransition (Action animations);

		// @required -(void)animateAlongsideTransitionWithOptions:(UIViewAnimationOptions)options animations:(void (^)(void))animations completion:(void (^)(BOOL))completion;
		[Abstract]
		[Export ("animateAlongsideTransitionWithOptions:animations:completion:")]
		void AnimateAlongsideTransitionWithOptions (UIViewAnimationOptions options,Action animations,Action<bool> completion);
	}

	delegate void EnumerateOverlaysHandler (MDCOverlay overlay,nuint idx,ref bool stop);

	// @interface MDCOverlayWindow : UIWindow
	[BaseType (typeof (UIWindow))]
	interface MDCOverlayWindow
	{
		// -(void)activateOverlay:(UIView *)overlay withLevel:(UIWindowLevel)level;
		[Export ("activateOverlay:withLevel:")]
		void ActivateOverlay (UIView overlay,double level);

		// -(void)deactivateOverlay:(UIView *)overlay;
		[Export ("deactivateOverlay:")]
		void DeactivateOverlay (UIView overlay);
	}

	// @interface MDCPageControl : UIControl <UIScrollViewDelegate>
	[BaseType (typeof (UIControl))]
	interface MDCPageControl : IUIScrollViewDelegate
	{
		// @property (nonatomic) NSInteger numberOfPages;
		[Export ("numberOfPages")]
		nint NumberOfPages { get; set; }

		// @property (nonatomic) NSInteger currentPage;
		[Export ("currentPage")]
		nint CurrentPage { get; set; }

		// -(void)setCurrentPage:(NSInteger)currentPage animated:(BOOL)animated;
		[Export ("setCurrentPage:animated:")]
		void SetCurrentPage (nint currentPage,bool animated);

		// @property (nonatomic) BOOL hidesForSinglePage;
		[Export ("hidesForSinglePage")]
		bool HidesForSinglePage { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable pageIndicatorTintColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("pageIndicatorTintColor",ArgumentSemantic.Strong)]
		UIColor PageIndicatorTintColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable currentPageIndicatorTintColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("currentPageIndicatorTintColor",ArgumentSemantic.Strong)]
		UIColor CurrentPageIndicatorTintColor { get; set; }

		// @property (nonatomic) BOOL defersCurrentPageDisplay;
		[Export ("defersCurrentPageDisplay")]
		bool DefersCurrentPageDisplay { get; set; }

		// -(void)updateCurrentPageDisplay;
		[Export ("updateCurrentPageDisplay")]
		void UpdateCurrentPageDisplay ();

		// +(CGSize)sizeForNumberOfPages:(NSInteger)pageCount;
		[Static]
		[Export ("sizeForNumberOfPages:")]
		CGSize SizeForNumberOfPages (nint pageCount);

		// -(void)scrollViewDidScroll:(UIScrollView * _Nonnull)scrollView;
		[Export ("scrollViewDidScroll:")]
		void ScrollViewDidScroll (UIScrollView scrollView);

		// -(void)scrollViewDidEndDecelerating:(UIScrollView * _Nonnull)scrollView;
		[Export ("scrollViewDidEndDecelerating:")]
		void ScrollViewDidEndDecelerating (UIScrollView scrollView);

		// -(void)scrollViewDidEndScrollingAnimation:(UIScrollView * _Nonnull)scrollView;
		[Export ("scrollViewDidEndScrollingAnimation:")]
		void ScrollViewDidEndScrollingAnimation (UIScrollView scrollView);
	}

	// @interface MDCPageControlColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCPageControlColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toPageControl:(MDCPageControl *)pageControl;
		[Static]
		[Export ("applyColorScheme:toPageControl:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCPageControl pageControl);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants8
	{
		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint50Name;
		[Field ("MDCPaletteTint50Name","__Internal")]
		NSString MDCPaletteTint50Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint100Name;
		[Field ("MDCPaletteTint100Name","__Internal")]
		NSString MDCPaletteTint100Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint200Name;
		[Field ("MDCPaletteTint200Name","__Internal")]
		NSString MDCPaletteTint200Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint300Name;
		[Field ("MDCPaletteTint300Name","__Internal")]
		NSString MDCPaletteTint300Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint400Name;
		[Field ("MDCPaletteTint400Name","__Internal")]
		NSString MDCPaletteTint400Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint500Name;
		[Field ("MDCPaletteTint500Name","__Internal")]
		NSString MDCPaletteTint500Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint600Name;
		[Field ("MDCPaletteTint600Name","__Internal")]
		NSString MDCPaletteTint600Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint700Name;
		[Field ("MDCPaletteTint700Name","__Internal")]
		NSString MDCPaletteTint700Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint800Name;
		[Field ("MDCPaletteTint800Name","__Internal")]
		NSString MDCPaletteTint800Name { get; }

		// extern MDCPaletteTint  _Nonnull const MDCPaletteTint900Name;
		[Field ("MDCPaletteTint900Name","__Internal")]
		NSString MDCPaletteTint900Name { get; }

		// extern MDCPaletteAccent  _Nonnull const MDCPaletteAccent100Name;
		[Field ("MDCPaletteAccent100Name","__Internal")]
		NSString MDCPaletteAccent100Name { get; }

		// extern MDCPaletteAccent  _Nonnull const MDCPaletteAccent200Name;
		[Field ("MDCPaletteAccent200Name","__Internal")]
		NSString MDCPaletteAccent200Name { get; }

		// extern MDCPaletteAccent  _Nonnull const MDCPaletteAccent400Name;
		[Field ("MDCPaletteAccent400Name","__Internal")]
		NSString MDCPaletteAccent400Name { get; }

		// extern MDCPaletteAccent  _Nonnull const MDCPaletteAccent700Name;
		[Field ("MDCPaletteAccent700Name","__Internal")]
		NSString MDCPaletteAccent700Name { get; }
	}

	// @interface MDCPalette : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCPalette
	{
		// @property (readonly, strong, class) MDCPalette * _Nonnull redPalette;
		[Static]
		[Export ("redPalette",ArgumentSemantic.Strong)]
		MDCPalette RedPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull pinkPalette;
		[Static]
		[Export ("pinkPalette",ArgumentSemantic.Strong)]
		MDCPalette PinkPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull purplePalette;
		[Static]
		[Export ("purplePalette",ArgumentSemantic.Strong)]
		MDCPalette PurplePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull deepPurplePalette;
		[Static]
		[Export ("deepPurplePalette",ArgumentSemantic.Strong)]
		MDCPalette DeepPurplePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull indigoPalette;
		[Static]
		[Export ("indigoPalette",ArgumentSemantic.Strong)]
		MDCPalette IndigoPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull bluePalette;
		[Static]
		[Export ("bluePalette",ArgumentSemantic.Strong)]
		MDCPalette BluePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull lightBluePalette;
		[Static]
		[Export ("lightBluePalette",ArgumentSemantic.Strong)]
		MDCPalette LightBluePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull cyanPalette;
		[Static]
		[Export ("cyanPalette",ArgumentSemantic.Strong)]
		MDCPalette CyanPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull tealPalette;
		[Static]
		[Export ("tealPalette",ArgumentSemantic.Strong)]
		MDCPalette TealPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull greenPalette;
		[Static]
		[Export ("greenPalette",ArgumentSemantic.Strong)]
		MDCPalette GreenPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull lightGreenPalette;
		[Static]
		[Export ("lightGreenPalette",ArgumentSemantic.Strong)]
		MDCPalette LightGreenPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull limePalette;
		[Static]
		[Export ("limePalette",ArgumentSemantic.Strong)]
		MDCPalette LimePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull yellowPalette;
		[Static]
		[Export ("yellowPalette",ArgumentSemantic.Strong)]
		MDCPalette YellowPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull amberPalette;
		[Static]
		[Export ("amberPalette",ArgumentSemantic.Strong)]
		MDCPalette AmberPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull orangePalette;
		[Static]
		[Export ("orangePalette",ArgumentSemantic.Strong)]
		MDCPalette OrangePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull deepOrangePalette;
		[Static]
		[Export ("deepOrangePalette",ArgumentSemantic.Strong)]
		MDCPalette DeepOrangePalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull brownPalette;
		[Static]
		[Export ("brownPalette",ArgumentSemantic.Strong)]
		MDCPalette BrownPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull greyPalette;
		[Static]
		[Export ("greyPalette",ArgumentSemantic.Strong)]
		MDCPalette GreyPalette { get; }

		// @property (readonly, strong, class) MDCPalette * _Nonnull blueGreyPalette;
		[Static]
		[Export ("blueGreyPalette",ArgumentSemantic.Strong)]
		MDCPalette BlueGreyPalette { get; }

		// +(instancetype _Nonnull)paletteGeneratedFromColor:(UIColor * _Nonnull)target500Color;
		[Static]
		[Export ("paletteGeneratedFromColor:")]
		MDCPalette PaletteGeneratedFromColor (UIColor target500Color);

		// +(instancetype _Nonnull)paletteWithTints:(NSDictionary<MDCPaletteTint,UIColor *> * _Nonnull)tints accents:(NSDictionary<MDCPaletteAccent,UIColor *> * _Nullable)accents;
		[Static]
		[Export ("paletteWithTints:accents:")]
		MDCPalette PaletteWithTints (NSDictionary<NSString,UIColor> tints,[NullAllowed] NSDictionary<NSString,UIColor> accents);

		// -(instancetype _Nonnull)initWithTints:(NSDictionary<MDCPaletteTint,UIColor *> * _Nonnull)tints accents:(NSDictionary<MDCPaletteAccent,UIColor *> * _Nullable)accents;
		[Export ("initWithTints:accents:")]
		IntPtr Constructor (NSDictionary<NSString,UIColor> tints,[NullAllowed] NSDictionary<NSString,UIColor> accents);

		// @property (readonly, nonatomic) UIColor * _Nonnull tint50;
		[Export ("tint50")]
		UIColor Tint50 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint100;
		[Export ("tint100")]
		UIColor Tint100 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint200;
		[Export ("tint200")]
		UIColor Tint200 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint300;
		[Export ("tint300")]
		UIColor Tint300 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint400;
		[Export ("tint400")]
		UIColor Tint400 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint500;
		[Export ("tint500")]
		UIColor Tint500 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint600;
		[Export ("tint600")]
		UIColor Tint600 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint700;
		[Export ("tint700")]
		UIColor Tint700 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint800;
		[Export ("tint800")]
		UIColor Tint800 { get; }

		// @property (readonly, nonatomic) UIColor * _Nonnull tint900;
		[Export ("tint900")]
		UIColor Tint900 { get; }

		// @property (readonly, nonatomic) UIColor * _Nullable accent100;
		[NullAllowed, Export ("accent100")]
		UIColor Accent100 { get; }

		// @property (readonly, nonatomic) UIColor * _Nullable accent200;
		[NullAllowed, Export ("accent200")]
		UIColor Accent200 { get; }

		// @property (readonly, nonatomic) UIColor * _Nullable accent400;
		[NullAllowed, Export ("accent400")]
		UIColor Accent400 { get; }

		// @property (readonly, nonatomic) UIColor * _Nullable accent700;
		[NullAllowed, Export ("accent700")]
		UIColor Accent700 { get; }
	}

	// @interface MDCPathGenerator : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCPathGenerator
	{
		// @property (readonly, nonatomic) CGPoint startPoint;
		[Export ("startPoint")]
		CGPoint StartPoint { get; }

		// @property (readonly, nonatomic) CGPoint endPoint;
		[Export ("endPoint")]
		CGPoint EndPoint { get; }

		// +(instancetype _Nonnull)pathGenerator;
		[Static]
		[Export ("pathGenerator")]
		MDCPathGenerator PathGenerator ();

		// +(instancetype _Nonnull)pathGeneratorWithStartPoint:(CGPoint)startPoint;
		[Static]
		[Export ("pathGeneratorWithStartPoint:")]
		MDCPathGenerator PathGeneratorWithStartPoint (CGPoint startPoint);

		// -(void)addLineToPoint:(CGPoint)point;
		[Export ("addLineToPoint:")]
		void AddLineToPoint (CGPoint point);

		// -(void)addArcWithCenter:(CGPoint)center radius:(CGFloat)radius startAngle:(CGFloat)startAngle endAngle:(CGFloat)endAngle clockwise:(BOOL)clockwise;
		[Export ("addArcWithCenter:radius:startAngle:endAngle:clockwise:")]
		void AddArcWithCenter (CGPoint center,nfloat radius,nfloat startAngle,nfloat endAngle,bool clockwise);

		// -(void)addArcWithTangentPoint:(CGPoint)tangentPoint toPoint:(CGPoint)toPoint radius:(CGFloat)radius;
		[Export ("addArcWithTangentPoint:toPoint:radius:")]
		void AddArcWithTangentPoint (CGPoint tangentPoint,CGPoint toPoint,nfloat radius);

		// -(void)addCurveWithControlPoint1:(CGPoint)controlPoint1 controlPoint2:(CGPoint)controlPoint2 toPoint:(CGPoint)toPoint;
		[Export ("addCurveWithControlPoint1:controlPoint2:toPoint:")]
		void AddCurveWithControlPoint1 (CGPoint controlPoint1,CGPoint controlPoint2,CGPoint toPoint);

		// -(void)addQuadCurveWithControlPoint:(CGPoint)controlPoint toPoint:(CGPoint)toPoint;
		[Export ("addQuadCurveWithControlPoint:toPoint:")]
		void AddQuadCurveWithControlPoint (CGPoint controlPoint,CGPoint toPoint);

		// -(void)appendToCGPath:(CGMutablePathRef _Nonnull)cgPath transform:(CGAffineTransform * _Nullable)transform;
		[Export ("appendToCGPath:transform:")]
		//unsafe void AppendToCGPath (CGMutablePathRef* cgPath,[NullAllowed] CGAffineTransform* transform); TODO check
		unsafe void AppendToCGPath (CGPath cgPath,[NullAllowed] CGAffineTransform transform);
	}

	// @interface MDCPillShapeGenerator : NSObject <MDCShapeGenerating>
	[BaseType (typeof (NSObject))]
	interface MDCPillShapeGenerator : MDCShapeGenerating
	{
	}

	// @interface MDCProgressView : UIView
	[BaseType (typeof (UIView))]
	interface MDCProgressView
	{
		// @property (nonatomic, strong) UIColor * _Null_unspecified progressTintColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("progressTintColor",ArgumentSemantic.Strong)]
		UIColor ProgressTintColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified trackTintColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("trackTintColor",ArgumentSemantic.Strong)]
		UIColor TrackTintColor { get; set; }

		// @property (assign, nonatomic) float progress;
		[Export ("progress")]
		float Progress { get; set; }

		// @property (assign, nonatomic) MDCProgressViewBackwardAnimationMode backwardProgressAnimationMode;
		[Export ("backwardProgressAnimationMode",ArgumentSemantic.Assign)]
		MDCProgressViewBackwardAnimationMode BackwardProgressAnimationMode { get; set; }

		// -(void)setProgress:(float)progress animated:(BOOL)animated completion:(void (^ _Nullable)(BOOL))completion;
		[Export ("setProgress:animated:completion:")]
		void SetProgress (float progress,bool animated,[NullAllowed] Action<bool> completion);

		// -(void)setHidden:(BOOL)hidden animated:(BOOL)animated completion:(void (^ _Nullable)(BOOL))completion;
		[Export ("setHidden:animated:completion:")]
		void SetHidden (bool hidden,bool animated,[NullAllowed] Action<bool> completion);
	}

	// @interface MDCProgressViewColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCProgressViewColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toProgressView:(MDCProgressView *)progressView;
		[Static]
		[Export ("applyColorScheme:toProgressView:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCProgressView progressView);
	}

	// @interface MDCRectangleShapeGenerator : NSObject <MDCShapeGenerating>
	[BaseType (typeof (NSObject))]
	interface MDCRectangleShapeGenerator : MDCShapeGenerating
	{
		// @property (nonatomic, strong) MDCCornerTreatment * topLeftCorner;
		[Export ("topLeftCorner",ArgumentSemantic.Strong)]
		MDCCornerTreatment TopLeftCorner { get; set; }

		// @property (nonatomic, strong) MDCCornerTreatment * topRightCorner;
		[Export ("topRightCorner",ArgumentSemantic.Strong)]
		MDCCornerTreatment TopRightCorner { get; set; }

		// @property (nonatomic, strong) MDCCornerTreatment * bottomLeftCorner;
		[Export ("bottomLeftCorner",ArgumentSemantic.Strong)]
		MDCCornerTreatment BottomLeftCorner { get; set; }

		// @property (nonatomic, strong) MDCCornerTreatment * bottomRightCorner;
		[Export ("bottomRightCorner",ArgumentSemantic.Strong)]
		MDCCornerTreatment BottomRightCorner { get; set; }

		// @property (assign, nonatomic) CGPoint topLeftCornerOffset;
		[Export ("topLeftCornerOffset",ArgumentSemantic.Assign)]
		CGPoint TopLeftCornerOffset { get; set; }

		// @property (assign, nonatomic) CGPoint topRightCornerOffset;
		[Export ("topRightCornerOffset",ArgumentSemantic.Assign)]
		CGPoint TopRightCornerOffset { get; set; }

		// @property (assign, nonatomic) CGPoint bottomLeftCornerOffset;
		[Export ("bottomLeftCornerOffset",ArgumentSemantic.Assign)]
		CGPoint BottomLeftCornerOffset { get; set; }

		// @property (assign, nonatomic) CGPoint bottomRightCornerOffset;
		[Export ("bottomRightCornerOffset",ArgumentSemantic.Assign)]
		CGPoint BottomRightCornerOffset { get; set; }

		// @property (nonatomic, strong) MDCEdgeTreatment * topEdge;
		[Export ("topEdge",ArgumentSemantic.Strong)]
		MDCEdgeTreatment TopEdge { get; set; }

		// @property (nonatomic, strong) MDCEdgeTreatment * rightEdge;
		[Export ("rightEdge",ArgumentSemantic.Strong)]
		MDCEdgeTreatment RightEdge { get; set; }

		// @property (nonatomic, strong) MDCEdgeTreatment * bottomEdge;
		[Export ("bottomEdge",ArgumentSemantic.Strong)]
		MDCEdgeTreatment BottomEdge { get; set; }

		// @property (nonatomic, strong) MDCEdgeTreatment * leftEdge;
		[Export ("leftEdge",ArgumentSemantic.Strong)]
		MDCEdgeTreatment LeftEdge { get; set; }

		// -(void)setCorners:(MDCCornerTreatment *)cornerShape;
		[Export ("setCorners:")]
		void SetCorners (MDCCornerTreatment cornerShape);

		// -(void)setEdges:(MDCEdgeTreatment *)edgeShape;
		[Export ("setEdges:")]
		void SetEdges (MDCEdgeTreatment edgeShape);
	}

	// @interface MDCRoundedCornerTreatment : MDCCornerTreatment
	[BaseType (typeof (MDCCornerTreatment))]
	interface MDCRoundedCornerTreatment
	{
		// @property (assign, nonatomic) CGFloat radius;
		[Export ("radius")]
		nfloat Radius { get; set; }

		// -(instancetype _Nonnull)initWithRadius:(CGFloat)radius __attribute__((objc_designated_initializer));
		[Export ("initWithRadius:")]
		[DesignatedInitializer]
		IntPtr Constructor (nfloat radius);

		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed
	}

	// @interface MDCShadowMetrics : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCShadowMetrics
	{
		// @property (readonly, nonatomic) CGFloat topShadowRadius;
		[Export ("topShadowRadius")]
		nfloat TopShadowRadius { get; }

		// @property (readonly, nonatomic) CGSize topShadowOffset;
		[Export ("topShadowOffset")]
		CGSize TopShadowOffset { get; }

		// @property (readonly, nonatomic) float topShadowOpacity;
		[Export ("topShadowOpacity")]
		float TopShadowOpacity { get; }

		// @property (readonly, nonatomic) CGFloat bottomShadowRadius;
		[Export ("bottomShadowRadius")]
		nfloat BottomShadowRadius { get; }

		// @property (readonly, nonatomic) CGSize bottomShadowOffset;
		[Export ("bottomShadowOffset")]
		CGSize BottomShadowOffset { get; }

		// @property (readonly, nonatomic) float bottomShadowOpacity;
		[Export ("bottomShadowOpacity")]
		float BottomShadowOpacity { get; }

		// +(MDCShadowMetrics * _Nonnull)metricsWithElevation:(CGFloat)elevation;
		[Static]
		[Export ("metricsWithElevation:")]
		MDCShadowMetrics MetricsWithElevation (nfloat elevation);
	}

	// @interface MDCShadowLayer : CALayer
	[BaseType (typeof (CALayer))]
	interface MDCShadowLayer
	{
		// @property (assign, nonatomic) MDCShadowElevation elevation;
		[Export ("elevation")]
		double Elevation { get; set; }

		// @property (getter = isShadowMaskEnabled, assign, nonatomic) BOOL shadowMaskEnabled;
		[Export ("shadowMaskEnabled")]
		bool ShadowMaskEnabled { [Bind ("isShadowMaskEnabled")] get; set; }
	}

	// @interface MDCShapeLayer : CAShapeLayer
	[BaseType (typeof (CAShapeLayer))]
	interface MDCShapeLayer
	{
		// @property (nonatomic, strong) id<MDCShapeGenerating> _Nullable shapeGenerator;
		[NullAllowed, Export ("shapeGenerator",ArgumentSemantic.Strong)]
		MDCShapeGenerating ShapeGenerator { get; set; }
	}

	// @interface MDCShapedShadowLayer : MDCShadowLayer
	[BaseType (typeof (MDCShadowLayer))]
	interface MDCShapedShadowLayer
	{
		// @property CGColorRef _Nullable fillColor;
		[NullAllowed, Export ("fillColor",ArgumentSemantic.Assign)]
		//unsafe CGColorRef* FillColor { get; set; } TODO
		unsafe CGColor FillColor { get; set; }

		// @property (nonatomic, strong) id<MDCShapeGenerating> _Nullable shapeGenerator;
		[NullAllowed, Export ("shapeGenerator",ArgumentSemantic.Strong)]
		MDCShapeGenerating ShapeGenerator { get; set; }
	}

	// @interface MDCShapedView : UIView
	[BaseType (typeof (UIView))]
	interface MDCShapedView
	{
		// @property (assign, nonatomic) MDCShadowElevation elevation;
		[Export ("elevation")]
		double Elevation { get; set; }

		// @property (nonatomic, strong) id<MDCShapeGenerating> _Nullable shapeGenerator __attribute__((iboutlet));
		[NullAllowed, Export ("shapeGenerator",ArgumentSemantic.Strong)]
		MDCShapeGenerating ShapeGenerator { get; set; }

		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame shapeGenerator:(id<MDCShapeGenerating> _Nullable)shapeGenerator __attribute__((objc_designated_initializer));
		[Export ("initWithFrame:shapeGenerator:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGRect frame,[NullAllowed] MDCShapeGenerating shapeGenerator);

		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame;
		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nullable)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor ([NullAllowed] NSCoder aDecoder); TODO not needed
	}

	// @interface MDCSlantedRectShapeGenerator : NSObject <MDCShapeGenerating>
	[BaseType (typeof (NSObject))]
	interface MDCSlantedRectShapeGenerator : MDCShapeGenerating
	{
		// @property (assign, nonatomic) CGFloat slant;
		[Export ("slant")]
		nfloat Slant { get; set; }
	}

	// @interface MDCSlider : UIControl <NSSecureCoding>
	[BaseType (typeof (UIControl))]
	interface MDCSlider : INSSecureCoding
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCSliderDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCSliderDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified color;
		[Export ("color",ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified disabledColor;
		[Export ("disabledColor",ArgumentSemantic.Strong)]
		UIColor DisabledColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified trackBackgroundColor;
		[Export ("trackBackgroundColor",ArgumentSemantic.Strong)]
		UIColor TrackBackgroundColor { get; set; }

		// @property (assign, nonatomic) NSUInteger numberOfDiscreteValues;
		[Export ("numberOfDiscreteValues")]
		nuint NumberOfDiscreteValues { get; set; }

		// @property (assign, nonatomic) CGFloat value;
		[Export ("value")]
		nfloat Value { get; set; }

		// -(void)setValue:(CGFloat)value animated:(BOOL)animated;
		[Export ("setValue:animated:")]
		void SetValue (nfloat value,bool animated);

		// @property (assign, nonatomic) CGFloat minimumValue;
		[Export ("minimumValue")]
		nfloat MinimumValue { get; set; }

		// @property (assign, nonatomic) CGFloat maximumValue;
		[Export ("maximumValue")]
		nfloat MaximumValue { get; set; }

		// @property (getter = isContinuous, assign, nonatomic) BOOL continuous;
		[Export ("continuous")]
		bool Continuous { [Bind ("isContinuous")] get; set; }

		// @property (assign, nonatomic) CGFloat filledTrackAnchorValue;
		[Export ("filledTrackAnchorValue")]
		nfloat FilledTrackAnchorValue { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisplayDiscreteValueLabel;
		[Export ("shouldDisplayDiscreteValueLabel")]
		bool ShouldDisplayDiscreteValueLabel { get; set; }

		// @property (getter = isThumbHollowAtStart, assign, nonatomic) BOOL thumbHollowAtStart;
		[Export ("thumbHollowAtStart")]
		bool ThumbHollowAtStart { [Bind ("isThumbHollowAtStart")] get; set; }
	}

	// @protocol MDCSliderDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCSliderDelegate
	{
		// @optional -(BOOL)slider:(MDCSlider * _Nonnull)slider shouldJumpToValue:(CGFloat)value;
		[Export ("slider:shouldJumpToValue:")]
		bool Slider (MDCSlider slider,nfloat value);

		// @optional -(NSString * _Nonnull)slider:(MDCSlider * _Nonnull)slider displayedStringForValue:(CGFloat)value;
		[Export ("slider:displayedStringForValue:")]
		string Slider2 (MDCSlider slider,nfloat value);

		// @optional -(NSString * _Nonnull)slider:(MDCSlider * _Nonnull)slider accessibilityLabelForValue:(CGFloat)value;
		[Export ("slider:accessibilityLabelForValue:")]
		string Slide3 (MDCSlider slider,nfloat value);
	}

	// @interface MDCSliderColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCSliderColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> * _Nonnull)colorScheme toSlider:(MDCSlider * _Nonnull)slider;
		[Static]
		[Export ("applyColorScheme:toSlider:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCSlider slider);

		// +(MDCBasicColorScheme * _Nonnull)defaultSliderLightColorScheme;
		[Static]
		[Export ("defaultSliderLightColorScheme")]
		MDCBasicColorScheme DefaultSliderLightColorScheme { get; }

		// +(MDCBasicColorScheme * _Nonnull)defaultSliderDarkColorScheme;
		[Static]
		[Export ("defaultSliderDarkColorScheme")]
		MDCBasicColorScheme DefaultSliderDarkColorScheme { get; }
	}

	// @interface MDCSnackbarManager : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCSnackbarManager
	{
		// +(void)showMessage:(MDCSnackbarMessage *)message;
		[Static]
		[Export ("showMessage:")]
		void ShowMessage (MDCSnackbarMessage message);

		// +(void)setPresentationHostView:(UIView *)hostView;
		[Static]
		[Export ("setPresentationHostView:")]
		void SetPresentationHostView (UIView hostView);

		// +(void)dismissAndCallCompletionBlocksWithCategory:(NSString *)category;
		[Static]
		[Export ("dismissAndCallCompletionBlocksWithCategory:")]
		void DismissAndCallCompletionBlocksWithCategory (string category);

		// +(void)setBottomOffset:(CGFloat)offset;
		[Static]
		[Export ("setBottomOffset:")]
		void SetBottomOffset (nfloat offset);

		// +(id<MDCSnackbarSuspensionToken>)suspendAllMessages;
		[Static]
		[Export ("suspendAllMessages")]
		MDCSnackbarSuspensionToken SuspendAllMessages ();

		// +(id<MDCSnackbarSuspensionToken>)suspendMessagesWithCategory:(NSString *)category;
		[Static]
		[Export ("suspendMessagesWithCategory:")]
		MDCSnackbarSuspensionToken SuspendMessagesWithCategory (string category);

		// +(void)resumeMessagesWithToken:(id<MDCSnackbarSuspensionToken>)token;
		[Static]
		[Export ("resumeMessagesWithToken:")]
		void ResumeMessagesWithToken (MDCSnackbarSuspensionToken token);
	}

	// @protocol MDCSnackbarSuspensionToken <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCSnackbarSuspensionToken
	{
	}

	// typedef void (^MDCSnackbarMessageCompletionHandler)(BOOL);
	delegate void MDCSnackbarMessageCompletionHandler (bool arg0);

	// typedef void (^MDCSnackbarMessageActionHandler)();
	delegate void MDCSnackbarMessageActionHandler ();

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants9
	{
		// extern const NSTimeInterval MDCSnackbarMessageDurationMax;
		[Field ("MDCSnackbarMessageDurationMax","__Internal")]
		double MDCSnackbarMessageDurationMax { get; }

		// extern NSString *const MDCSnackbarMessageBoldAttributeName;
		[Field ("MDCSnackbarMessageBoldAttributeName","__Internal")]
		NSString MDCSnackbarMessageBoldAttributeName { get; }
	}

	// @interface MDCSnackbarMessage : NSObject <NSCopying, UIAccessibilityIdentification>
	[BaseType (typeof (NSObject))]
	interface MDCSnackbarMessage : INSCopying, IUIAccessibilityIdentification
	{
		// +(instancetype)messageWithText:(NSString *)text;
		[Static]
		[Export ("messageWithText:")]
		MDCSnackbarMessage MessageWithText (string text);

		// +(instancetype)messageWithAttributedText:(NSAttributedString *)attributedText;
		[Static]
		[Export ("messageWithAttributedText:")]
		MDCSnackbarMessage MessageWithAttributedText (NSAttributedString attributedText);

		// @property (copy, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; set; }

		// @property (copy, nonatomic) NSAttributedString * attributedText;
		[Export ("attributedText",ArgumentSemantic.Copy)]
		NSAttributedString AttributedText { get; set; }

		// @property (nonatomic, strong) MDCSnackbarMessageAction * action;
		[Export ("action",ArgumentSemantic.Strong)]
		MDCSnackbarMessageAction Action { get; set; }

		// @property (nonatomic, strong) UIColor * buttonTextColor;
		[Export ("buttonTextColor",ArgumentSemantic.Strong)]
		UIColor ButtonTextColor { get; set; }

		// @property (nonatomic, strong) UIColor * highlightedButtonTextColor;
		[Export ("highlightedButtonTextColor",ArgumentSemantic.Strong)]
		UIColor HighlightedButtonTextColor { get; set; }

		// @property (assign, nonatomic) NSTimeInterval duration;
		[Export ("duration")]
		double Duration { get; set; }

		// @property (copy, nonatomic) MDCSnackbarMessageCompletionHandler completionHandler;
		[Export ("completionHandler",ArgumentSemantic.Copy)]
		MDCSnackbarMessageCompletionHandler CompletionHandler { get; set; }

		// @property (copy, nonatomic) NSString * category;
		[Export ("category")]
		string Category { get; set; }

		// @property (copy, nonatomic) NSString * accessibilityLabel;
		[Export ("accessibilityLabel")]
		string AccessibilityLabel { get; set; }

		// @property (readonly, nonatomic) NSString * voiceNotificationText;
		[Export ("voiceNotificationText")]
		string VoiceNotificationText { get; }
	}

	// @interface MDCSnackbarMessageAction : NSObject <UIAccessibilityIdentification, NSCopying>
	[BaseType (typeof (NSObject))]
	interface MDCSnackbarMessageAction : IUIAccessibilityIdentification, INSCopying
	{
		// @property (copy, nonatomic) NSString * title;
		[Export ("title")]
		string Title { get; set; }

		// @property (copy, nonatomic) MDCSnackbarMessageActionHandler handler;
		[Export ("handler",ArgumentSemantic.Copy)]
		MDCSnackbarMessageActionHandler Handler { get; set; }
	}

	// @interface MDCSnackbarMessageView : UIView
	[BaseType (typeof (UIView))]
	interface MDCSnackbarMessageView
	{
		// @property (nonatomic, strong) UIColor * _Nullable snackbarMessageViewBackgroundColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("snackbarMessageViewBackgroundColor",ArgumentSemantic.Strong)]
		UIColor SnackbarMessageViewBackgroundColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable snackbarMessageViewShadowColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("snackbarMessageViewShadowColor",ArgumentSemantic.Strong)]
		UIColor SnackbarMessageViewShadowColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable snackbarMessageViewTextColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("snackbarMessageViewTextColor",ArgumentSemantic.Strong)]
		UIColor SnackbarMessageViewTextColor { get; set; }
	}

	// @interface MDCTabBar : UIView <UIBarPositioning>
	[BaseType (typeof (UIView))]
	interface MDCTabBar : IUIBarPositioning
	{
		// +(CGFloat)defaultHeightForItemAppearance:(MDCTabBarItemAppearance)appearance;
		[Static]
		[Export ("defaultHeightForItemAppearance:")]
		nfloat DefaultHeightForItemAppearance (MDCTabBarItemAppearance appearance);

		// @property (copy, nonatomic) NSArray<UITabBarItem *> * _Nonnull items;
		[Export ("items",ArgumentSemantic.Copy)]
		UITabBarItem[] Items { get; set; }

		// @property (nonatomic, strong) UITabBarItem * _Nullable selectedItem;
		[NullAllowed, Export ("selectedItem",ArgumentSemantic.Strong)]
		UITabBarItem SelectedItem { get; set; }

		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCTabBarDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCTabBarDelegate> _Nullable delegate __attribute__((iboutlet));
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified tintColor;
		[Export ("tintColor",ArgumentSemantic.Strong)]
		UIColor TintColor { get; set; }

		// @property (nonatomic) UIColor * _Nullable selectedItemTintColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("selectedItemTintColor",ArgumentSemantic.Assign)]
		UIColor SelectedItemTintColor { get; set; }

		// @property (nonatomic) UIColor * _Nonnull unselectedItemTintColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("unselectedItemTintColor",ArgumentSemantic.Assign)]
		UIColor UnselectedItemTintColor { get; set; }

		// @property (nonatomic) UIColor * _Nonnull inkColor __attribute__((annotate("ui_appearance_selector")));
		[Export ("inkColor",ArgumentSemantic.Assign)]
		UIColor InkColor { get; set; }

		// @property (nonatomic) UIColor * _Nullable barTintColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("barTintColor",ArgumentSemantic.Assign)]
		UIColor BarTintColor { get; set; }

		// @property (nonatomic) MDCTabBarAlignment alignment;
		[Export ("alignment",ArgumentSemantic.Assign)]
		MDCTabBarAlignment Alignment { get; set; }

		// @property (nonatomic) MDCTabBarItemAppearance itemAppearance;
		[Export ("itemAppearance",ArgumentSemantic.Assign)]
		MDCTabBarItemAppearance ItemAppearance { get; set; }

		// @property (nonatomic) BOOL displaysUppercaseTitles;
		[Export ("displaysUppercaseTitles")]
		bool DisplaysUppercaseTitles { get; set; }

		// -(void)setSelectedItem:(UITabBarItem * _Nullable)selectedItem animated:(BOOL)animated;
		[Export ("setSelectedItem:animated:")]
		void SetSelectedItem ([NullAllowed] UITabBarItem selectedItem,bool animated);

		// -(void)setAlignment:(MDCTabBarAlignment)alignment animated:(BOOL)animated;
		[Export ("setAlignment:animated:")]
		void SetAlignment (MDCTabBarAlignment alignment,bool animated);
	}

	// @interface MDCAccessibility (MDCTabBar)
	[Category]
	[BaseType (typeof (MDCTabBar))]
	interface MDCTabBar_MDCAccessibility
	{
		// -(id _Nullable)accessibilityElementForItem:(UITabBarItem * _Nonnull)item;
		[Export ("accessibilityElementForItem:")]
		[return: NullAllowed]
		NSObject AccessibilityElementForItem (UITabBarItem item);
	}

	// @protocol MDCTabBarDelegate <UIBarPositioningDelegate>
	[BaseType (typeof (NSObject))]
	[Protocol, Model]
	interface MDCTabBarDelegate : IUIBarPositioningDelegate
	{
		// @optional -(BOOL)tabBar:(MDCTabBar * _Nonnull)tabBar shouldSelectItem:(UITabBarItem * _Nonnull)item;
		[Export ("tabBar:shouldSelectItem:")]
		bool TabBar1 (MDCTabBar tabBar,UITabBarItem item);

		// @optional -(void)tabBar:(MDCTabBar * _Nonnull)tabBar willSelectItem:(UITabBarItem * _Nonnull)item;
		[Export ("tabBar:willSelectItem:")]
		void TabBar2 (MDCTabBar tabBar,UITabBarItem item);

		// @optional -(void)tabBar:(MDCTabBar * _Nonnull)tabBar didSelectItem:(UITabBarItem * _Nonnull)item;
		[Export ("tabBar:didSelectItem:")]
		void TabBar3 (MDCTabBar tabBar,UITabBarItem item);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants10
	{
		// extern const CGFloat MDCTabBarViewControllerAnimationDuration;
		[Field ("MDCTabBarViewControllerAnimationDuration","__Internal")]
		nfloat MDCTabBarViewControllerAnimationDuration { get; }
	}

	// @interface MDCTabBarViewController : UIViewController <MDCTabBarDelegate, UIBarPositioningDelegate>
	[BaseType (typeof (UIViewController))]
	interface MDCTabBarViewController : MDCTabBarDelegate, IUIBarPositioningDelegate
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCTabBarControllerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCTabBarControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (copy, nonatomic) NSArray<UIViewController *> * _Nonnull viewControllers;
		[Export ("viewControllers",ArgumentSemantic.Copy)]
		UIViewController[] ViewControllers { get; set; }

		// @property (nonatomic, weak) UIViewController * _Nullable selectedViewController;
		[NullAllowed, Export ("selectedViewController",ArgumentSemantic.Weak)]
		UIViewController SelectedViewController { get; set; }

		// @property (readonly, nonatomic) MDCTabBar * _Nullable tabBar;
		[NullAllowed, Export ("tabBar")]
		MDCTabBar TabBar { get; }

		// @property (nonatomic) BOOL tabBarHidden;
		[Export ("tabBarHidden")]
		bool TabBarHidden { get; set; }

		// -(void)setTabBarHidden:(BOOL)hidden animated:(BOOL)animated;
		[Export ("setTabBarHidden:animated:")]
		void SetTabBarHidden (bool hidden,bool animated);
	}

	// @protocol MDCTabBarControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTabBarControllerDelegate
	{
		// @optional -(BOOL)tabBarController:(MDCTabBarViewController * _Nonnull)tabBarController shouldSelectViewController:(UIViewController * _Nonnull)viewController;
		[Export ("tabBarController:shouldSelectViewController:")]
		bool TabBarController (MDCTabBarViewController tabBarController,UIViewController viewController);

		// @optional -(void)tabBarController:(MDCTabBarViewController * _Nonnull)tabBarController didSelectViewController:(UIViewController * _Nonnull)viewController;
		[Export ("tabBarController:didSelectViewController:")]
		void TabBarController2 (MDCTabBarViewController tabBarController,UIViewController viewController);
	}

	// @interface MDCTabBarColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCTabBarColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toTabBar:(MDCTabBar *)tabBar;
		[Static]
		[Export ("applyColorScheme:toTabBar:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCTabBar tabBar);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants11
	{
		// extern NSString *const _Nonnull MDCTextFieldTextDidSetTextNotification;
		[Field ("MDCTextFieldTextDidSetTextNotification","__Internal")]
		NSString MDCTextFieldTextDidSetTextNotification { get; }
	}

	// @interface MDCTextField : UITextField <MDCTextInput>
	[BaseType (typeof (UITextField))]
	interface MDCTextField : MDCTextInput
	{
		// @property (nonatomic, strong) UIView * _Nullable leadingView;
		[NullAllowed, Export ("leadingView",ArgumentSemantic.Strong)]
		UIView LeadingView { get; set; }

		// @property (assign, nonatomic) UITextFieldViewMode leadingViewMode;
		[Export ("leadingViewMode",ArgumentSemantic.Assign)]
		UITextFieldViewMode LeadingViewMode { get; set; }
	}

	interface IMDCTextInputPositioningDelegate {}

	// @protocol MDCTextInputPositioningDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTextInputPositioningDelegate
	{
		// @optional -(UIEdgeInsets)textInsets:(UIEdgeInsets)defaultInsets;
		[Export ("textInsets:")]
		UIEdgeInsets TextInsets (UIEdgeInsets defaultInsets);

		// @optional -(CGRect)editingRectForBounds:(CGRect)bounds defaultRect:(CGRect)defaultRect;
		[Export ("editingRectForBounds:defaultRect:")]
		CGRect EditingRectForBounds (CGRect bounds,CGRect defaultRect);

		// @optional -(void)textInputDidLayoutSubviews;
		[Export ("textInputDidLayoutSubviews")]
		void TextInputDidLayoutSubviews ();

		// @optional -(void)textInputDidUpdateConstraints;
		[Export ("textInputDidUpdateConstraints")]
		void TextInputDidUpdateConstraints ();
	}

	// @protocol MDCTextInputCharacterCounter <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTextInputCharacterCounter
	{
		// @required -(NSUInteger)characterCountForTextInput:(UIView<MDCTextInput> * _Nullable)textInput;
		[Abstract]
		[Export ("characterCountForTextInput:")]
		nuint CharacterCountForTextInput ([NullAllowed] MDCTextInput textInput);
	}

	// @interface MDCTextInputAllCharactersCounter : NSObject <MDCTextInputCharacterCounter>
	[BaseType (typeof (NSObject))]
	interface MDCTextInputAllCharactersCounter : MDCTextInputCharacterCounter
	{
	}

	// @protocol MDCTextInputController <NSObject, NSCoding, NSCopying, MDCTextInputPositioningDelegate>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTextInputController : INSCoding, INSCopying, MDCTextInputPositioningDelegate
	{
		// @required @property (nonatomic, strong) UIColor * _Null_unspecified activeColor;
		[Abstract]
		[Export ("activeColor",ArgumentSemantic.Strong)]
		UIColor ActiveColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified activeColorDefault;
		[Static, Abstract]
		[Export ("activeColorDefault",ArgumentSemantic.Strong)]
		UIColor ActiveColorDefault { get; set; }

		// @required @property (nonatomic, weak) id<MDCTextInputCharacterCounter> _Null_unspecified characterCounter;
		[Abstract]
		[Export ("characterCounter",ArgumentSemantic.Weak)]
		MDCTextInputCharacterCounter CharacterCounter { get; set; }

		// @required @property (assign, nonatomic) NSUInteger characterCountMax;
		[Abstract]
		[Export ("characterCountMax")]
		nuint CharacterCountMax { get; set; }

		// @required @property (assign, nonatomic) UITextFieldViewMode characterCountViewMode;
		[Abstract]
		[Export ("characterCountViewMode",ArgumentSemantic.Assign)]
		UITextFieldViewMode CharacterCountViewMode { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Null_unspecified disabledColor;
		[Abstract]
		[Export ("disabledColor",ArgumentSemantic.Strong)]
		UIColor DisabledColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified disabledColorDefault;
		[Static, Abstract]
		[Export ("disabledColorDefault",ArgumentSemantic.Strong)]
		UIColor DisabledColorDefault { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Null_unspecified errorColor;
		[Abstract]
		[Export ("errorColor",ArgumentSemantic.Strong)]
		UIColor ErrorColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified errorColorDefault;
		[Static, Abstract]
		[Export ("errorColorDefault",ArgumentSemantic.Strong)]
		UIColor ErrorColorDefault { get; set; }

		// @required @property (readonly, copy, nonatomic) NSString * _Nullable errorText;
		[Abstract]
		[NullAllowed, Export ("errorText")]
		string ErrorText { get; }

		// @required @property (copy, nonatomic) NSString * _Nullable helperText;
		[Abstract]
		[NullAllowed, Export ("helperText")]
		string HelperText { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Null_unspecified inlinePlaceholderColor;
		[Abstract]
		[Export ("inlinePlaceholderColor",ArgumentSemantic.Strong)]
		UIColor InlinePlaceholderColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified inlinePlaceholderColorDefault;
		[Static, Abstract]
		[Export ("inlinePlaceholderColorDefault",ArgumentSemantic.Strong)]
		UIColor InlinePlaceholderColorDefault { get; set; }

		// @required @property (nonatomic, strong) UIFont * _Null_unspecified inlinePlaceholderFont;
		[Abstract]
		[Export ("inlinePlaceholderFont",ArgumentSemantic.Strong)]
		UIFont InlinePlaceholderFont { get; set; }

		// @required @property (nonatomic, strong, class) UIFont * _Null_unspecified inlinePlaceholderFontDefault;
		[Static, Abstract]
		[Export ("inlinePlaceholderFontDefault",ArgumentSemantic.Strong)]
		UIFont InlinePlaceholderFontDefault { get; set; }

		// @required @property (nonatomic, strong) UIFont * _Null_unspecified leadingUnderlineLabelFont;
		[Abstract]
		[Export ("leadingUnderlineLabelFont",ArgumentSemantic.Strong)]
		UIFont LeadingUnderlineLabelFont { get; set; }

		// @required @property (nonatomic, strong, class) UIFont * _Null_unspecified leadingUnderlineLabelFontDefault;
		[Static, Abstract]
		[Export ("leadingUnderlineLabelFontDefault",ArgumentSemantic.Strong)]
		UIFont LeadingUnderlineLabelFontDefault { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Null_unspecified leadingUnderlineLabelTextColor;
		[Abstract]
		[Export ("leadingUnderlineLabelTextColor",ArgumentSemantic.Strong)]
		UIColor LeadingUnderlineLabelTextColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified leadingUnderlineLabelTextColorDefault;
		[Static, Abstract]
		[Export ("leadingUnderlineLabelTextColorDefault",ArgumentSemantic.Strong)]
		UIColor LeadingUnderlineLabelTextColorDefault { get; set; }

		// @required @property (assign, readwrite, nonatomic, setter = mdc_setAdjustsFontForContentSizeCategory:) BOOL mdc_adjustsFontForContentSizeCategory;
		[Abstract]
		[Export ("mdc_adjustsFontForContentSizeCategory")]
		bool Mdc_adjustsFontForContentSizeCategory { get; [Bind ("mdc_setAdjustsFontForContentSizeCategory:")] set; }

		// @required @property (assign, nonatomic, class) BOOL mdc_adjustsFontForContentSizeCategoryDefault;
		[Static, Abstract]
		[Export ("mdc_adjustsFontForContentSizeCategoryDefault")]
		bool Mdc_adjustsFontForContentSizeCategoryDefault { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Null_unspecified normalColor;
		[Abstract]
		[Export ("normalColor",ArgumentSemantic.Strong)]
		UIColor NormalColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Null_unspecified normalColorDefault;
		[Static, Abstract]
		[Export ("normalColorDefault",ArgumentSemantic.Strong)]
		UIColor NormalColorDefault { get; set; }

		// @required @property (copy, nonatomic) NSString * _Nullable placeholderText;
		[Abstract]
		[NullAllowed, Export ("placeholderText")]
		string PlaceholderText { get; set; }

		// @required @property (assign, nonatomic) UIRectCorner roundedCorners;
		[Abstract]
		[Export ("roundedCorners",ArgumentSemantic.Assign)]
		UIRectCorner RoundedCorners { get; set; }

		// @required @property (assign, nonatomic, class) UIRectCorner roundedCornersDefault;
		[Static, Abstract]
		[Export ("roundedCornersDefault",ArgumentSemantic.Assign)]
		UIRectCorner RoundedCornersDefault { get; set; }

		// @required @property (nonatomic, strong) UIView<MDCTextInput> * _Nullable textInput;
		[Abstract]
		[NullAllowed, Export ("textInput",ArgumentSemantic.Strong)]
		MDCTextInput TextInput { get; set; }

		// @required @property (nonatomic, strong) UIFont * _Null_unspecified trailingUnderlineLabelFont;
		[Abstract]
		[Export ("trailingUnderlineLabelFont",ArgumentSemantic.Strong)]
		UIFont TrailingUnderlineLabelFont { get; set; }

		// @required @property (nonatomic, strong, class) UIFont * _Null_unspecified trailingUnderlineLabelFontDefault;
		[Static, Abstract]
		[Export ("trailingUnderlineLabelFontDefault",ArgumentSemantic.Strong)]
		UIFont TrailingUnderlineLabelFontDefault { get; set; }

		// @required @property (nonatomic, strong) UIColor * _Nullable trailingUnderlineLabelTextColor;
		[Abstract]
		[NullAllowed, Export ("trailingUnderlineLabelTextColor",ArgumentSemantic.Strong)]
		UIColor TrailingUnderlineLabelTextColor { get; set; }

		// @required @property (nonatomic, strong, class) UIColor * _Nullable trailingUnderlineLabelTextColorDefault;
		[Static, Abstract]
		[NullAllowed, Export ("trailingUnderlineLabelTextColorDefault",ArgumentSemantic.Strong)]
		UIColor TrailingUnderlineLabelTextColorDefault { get; set; }

		// @required @property (assign, nonatomic) UITextFieldViewMode underlineViewMode;
		[Abstract]
		[Export ("underlineViewMode",ArgumentSemantic.Assign)]
		UITextFieldViewMode UnderlineViewMode { get; set; }

		// @required @property (assign, nonatomic, class) UITextFieldViewMode underlineViewModeDefault;
		[Static, Abstract]
		[Export ("underlineViewModeDefault",ArgumentSemantic.Assign)]
		UITextFieldViewMode UnderlineViewModeDefault { get; set; }

		//// @required -(instancetype _Nonnull)initWithTextInput:(UIView<MDCTextInput> * _Nullable)input;
		////[Abstract] TODO invalid
		//[Export ("initWithTextInput:")]
		//IntPtr Constructor ([NullAllowed] MDCTextInput input);

		// @required -(void)setErrorText:(NSString * _Nullable)errorText errorAccessibilityValue:(NSString * _Nullable)errorAccessibilityValue;
		[Abstract]
		[Export ("setErrorText:errorAccessibilityValue:")]
		void SetErrorText ([NullAllowed] string errorText,[NullAllowed] string errorAccessibilityValue);
	}

	[Static]
	//[Verify (ConstantsInterfaceAssociation)]
	partial interface Constants12
	{
		// extern const CGFloat MDCTextInputDefaultBorderRadius;
		[Field ("MDCTextInputDefaultBorderRadius","__Internal")]
		nfloat MDCTextInputDefaultBorderRadius { get; }

		// extern const CGFloat MDCTextInputDefaultUnderlineActiveHeight;
		[Field ("MDCTextInputDefaultUnderlineActiveHeight","__Internal")]
		nfloat MDCTextInputDefaultUnderlineActiveHeight { get; }
	}

	// @interface MDCTextInputControllerDefault : NSObject <MDCTextInputController>
	[BaseType (typeof (NSObject))]
	interface MDCTextInputControllerDefault : MDCTextInputController
	{
		// @property (nonatomic, strong) UIColor * _Nullable borderFillColor;
		[NullAllowed, Export ("borderFillColor",ArgumentSemantic.Strong)]
		UIColor BorderFillColor { get; set; }

		// @property (nonatomic, strong, class) UIColor * _Null_unspecified borderFillColorDefault;
		[Static]
		[Export ("borderFillColorDefault",ArgumentSemantic.Strong)]
		UIColor BorderFillColorDefault { get; set; }

		// @property (assign, nonatomic) BOOL expandsOnOverflow;
		[Export ("expandsOnOverflow")]
		bool ExpandsOnOverflow { get; set; }

		// @property (nonatomic, strong) UIColor * _Null_unspecified floatingPlaceholderNormalColor;
		[Export ("floatingPlaceholderNormalColor",ArgumentSemantic.Strong)]
		UIColor FloatingPlaceholderNormalColor { get; set; }

		// @property (nonatomic, strong, class) UIColor * _Null_unspecified floatingPlaceholderNormalColorDefault;
		[Static]
		[Export ("floatingPlaceholderNormalColorDefault",ArgumentSemantic.Strong)]
		UIColor FloatingPlaceholderNormalColorDefault { get; set; }

		// @property (readonly, nonatomic) UIOffset floatingPlaceholderOffset;
		[Export ("floatingPlaceholderOffset")]
		UIOffset FloatingPlaceholderOffset { get; }

		// @property (nonatomic, strong) NSNumber * _Null_unspecified floatingPlaceholderScale;
		[Export ("floatingPlaceholderScale",ArgumentSemantic.Strong)]
		NSNumber FloatingPlaceholderScale { get; set; }

		// @property (assign, nonatomic, class) CGFloat floatingPlaceholderScaleDefault;
		[Static]
		[Export ("floatingPlaceholderScaleDefault")]
		nfloat FloatingPlaceholderScaleDefault { get; set; }

		// @property (getter = isFloatingEnabled, assign, nonatomic) BOOL floatingEnabled;
		[Export ("floatingEnabled")]
		bool FloatingEnabled { [Bind ("isFloatingEnabled")] get; set; }

		// @property (getter = isFloatingEnabledDefault, assign, nonatomic, class) BOOL floatingEnabledDefault;
		[Static]
		[Export ("floatingEnabledDefault")]
		bool FloatingEnabledDefault { [Bind ("isFloatingEnabledDefault")] get; set; }

		// @property (assign, nonatomic) NSUInteger minimumLines;
		[Export ("minimumLines")]
		nuint MinimumLines { get; set; }
	}

	// @interface MDCTextInputControllerFullWidth : NSObject <MDCTextInputController>
	[BaseType (typeof (NSObject))]
	interface MDCTextInputControllerFullWidth : MDCTextInputController
	{
	}

	// @interface MDCTextInputControllerLegacyDefault : NSObject <MDCTextInputController>
	[BaseType (typeof (NSObject))]
	interface MDCTextInputControllerLegacyDefault : MDCTextInputController
	{
		// @property (nonatomic, strong) UIColor * _Null_unspecified floatingPlaceholderNormalColor;
		[Export ("floatingPlaceholderNormalColor",ArgumentSemantic.Strong)]
		UIColor FloatingPlaceholderNormalColor { get; set; }

		// @property (nonatomic, strong, class) UIColor * _Null_unspecified floatingPlaceholderNormalColorDefault;
		[Static]
		[Export ("floatingPlaceholderNormalColorDefault",ArgumentSemantic.Strong)]
		UIColor FloatingPlaceholderNormalColorDefault { get; set; }

		// @property (nonatomic, strong) NSNumber * _Null_unspecified floatingPlaceholderScale;
		[Export ("floatingPlaceholderScale",ArgumentSemantic.Strong)]
		NSNumber FloatingPlaceholderScale { get; set; }

		// @property (assign, nonatomic, class) CGFloat floatingPlaceholderScaleDefault;
		[Static]
		[Export ("floatingPlaceholderScaleDefault")]
		nfloat FloatingPlaceholderScaleDefault { get; set; }

		// @property (getter = isFloatingEnabled, assign, nonatomic) BOOL floatingEnabled;
		[Export ("floatingEnabled")]
		bool FloatingEnabled { [Bind ("isFloatingEnabled")] get; set; }

		// @property (getter = isFloatingEnabledDefault, assign, nonatomic, class) BOOL floatingEnabledDefault;
		[Static]
		[Export ("floatingEnabledDefault")]
		bool FloatingEnabledDefault { [Bind ("isFloatingEnabledDefault")] get; set; }
	}

	// @interface MDCTextInputControllerLegacyFullWidth : NSObject <MDCTextInputController>
	[BaseType (typeof (NSObject))]
	interface MDCTextInputControllerLegacyFullWidth : MDCTextInputController
	{
	}

	// @interface MDCTextInputControllerOutlined : MDCTextInputControllerDefault
	[BaseType (typeof (MDCTextInputControllerDefault))]
	interface MDCTextInputControllerOutlined
	{
	}

	// @interface MDCTextInputControllerOutlinedTextArea : MDCTextInputControllerDefault
	[BaseType (typeof (MDCTextInputControllerDefault))]
	interface MDCTextInputControllerOutlinedTextArea
	{
	}

	// @interface MDCTextInputControllerFilled : MDCTextInputControllerDefault
	[BaseType (typeof (MDCTextInputControllerDefault))]
	interface MDCTextInputControllerFilled
	{
	}

	// @interface MDCTextInputUnderlineView : UIView <NSCopying, NSCoding>
	[BaseType (typeof (UIView))]
	interface MDCTextInputUnderlineView : INSCopying, INSCoding
	{
		// @property (nonatomic, strong) UIColor * color;
		[Export ("color",ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		// @property (nonatomic, strong) UIColor * disabledColor;
		[Export ("disabledColor",ArgumentSemantic.Strong)]
		UIColor DisabledColor { get; set; }

		// @property (assign, nonatomic) BOOL enabled;
		[Export ("enabled")]
		bool Enabled { get; set; }

		// @property (assign, nonatomic) CGFloat lineHeight;
		[Export ("lineHeight")]
		nfloat LineHeight { get; set; }
	}

	// @interface MDCTextFieldColorThemer : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCTextFieldColorThemer
	{
		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toTextInputController:(NSObject<MDCTextInputController> *)textInputController;
		[Static]
		[Export ("applyColorScheme:toTextInputController:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCTextInputController textInputController);

		// +(void)applyColorScheme:(NSObject<MDCColorScheme> *)colorScheme toTextInputControllerDefault:(MDCTextInputControllerLegacyDefault *)textInputControllerDefault;
		[Static]
		[Export ("applyColorScheme:toTextInputControllerDefault:")]
		void ApplyColorScheme (MDCColorScheme colorScheme,MDCTextInputControllerLegacyDefault textInputControllerDefault);

		// +(void)applyColorSchemeToAllTextInputControllerDefault:(NSObject<MDCColorScheme> *)colorScheme;
		[Static]
		[Export ("applyColorSchemeToAllTextInputControllerDefault:")]
		void ApplyColorSchemeToAllTextInputControllerDefault (MDCColorScheme colorScheme);
	}

	// @interface MDCTextInputBorderView : UIView <NSCopying>
	[BaseType (typeof (UIView))]
	interface MDCTextInputBorderView : INSCopying
	{
		// @property (nonatomic, strong) UIColor * _Nullable borderFillColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("borderFillColor",ArgumentSemantic.Strong)]
		UIColor BorderFillColor { get; set; }

		// @property (nonatomic, strong) UIBezierPath * _Nullable borderPath __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("borderPath",ArgumentSemantic.Strong)]
		UIBezierPath BorderPath { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable borderStrokeColor __attribute__((annotate("ui_appearance_selector")));
		[NullAllowed, Export ("borderStrokeColor",ArgumentSemantic.Strong)]
		UIColor BorderStrokeColor { get; set; }
	}

	// @interface MDCThumbTrack : UIControl
	[BaseType (typeof (UIControl))]
	interface MDCThumbTrack
	{
		[Wrap ("WeakDelegate")]
		[NullAllowed]
		MDCThumbTrackDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<MDCThumbTrackDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate",ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable primaryColor;
		[NullAllowed, Export ("primaryColor",ArgumentSemantic.Strong)]
		UIColor PrimaryColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable thumbOffColor;
		[NullAllowed, Export ("thumbOffColor",ArgumentSemantic.Strong)]
		UIColor ThumbOffColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable trackOffColor;
		[NullAllowed, Export ("trackOffColor",ArgumentSemantic.Strong)]
		UIColor TrackOffColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable thumbDisabledColor;
		[NullAllowed, Export ("thumbDisabledColor",ArgumentSemantic.Strong)]
		UIColor ThumbDisabledColor { get; set; }

		// @property (nonatomic, strong) UIColor * _Nullable trackDisabledColor;
		[NullAllowed, Export ("trackDisabledColor",ArgumentSemantic.Strong)]
		UIColor TrackDisabledColor { get; set; }

		// @property (assign, nonatomic) NSUInteger numDiscreteValues;
		[Export ("numDiscreteValues")]
		nuint NumDiscreteValues { get; set; }

		// @property (assign, nonatomic) CGFloat value;
		[Export ("value")]
		nfloat Value { get; set; }

		// @property (assign, nonatomic) CGFloat minimumValue;
		[Export ("minimumValue")]
		nfloat MinimumValue { get; set; }

		// @property (assign, nonatomic) CGFloat maximumValue;
		[Export ("maximumValue")]
		nfloat MaximumValue { get; set; }

		// @property (readonly, assign, nonatomic) CGPoint thumbPosition;
		[Export ("thumbPosition",ArgumentSemantic.Assign)]
		CGPoint ThumbPosition { get; }

		// @property (assign, nonatomic) CGFloat trackHeight;
		[Export ("trackHeight")]
		nfloat TrackHeight { get; set; }

		// @property (assign, nonatomic) CGFloat thumbRadius;
		[Export ("thumbRadius")]
		nfloat ThumbRadius { get; set; }

		// @property (assign, nonatomic) BOOL thumbIsSmallerWhenDisabled;
		[Export ("thumbIsSmallerWhenDisabled")]
		bool ThumbIsSmallerWhenDisabled { get; set; }

		// @property (assign, nonatomic) BOOL thumbIsHollowAtStart;
		[Export ("thumbIsHollowAtStart")]
		bool ThumbIsHollowAtStart { get; set; }

		// @property (assign, nonatomic) BOOL thumbGrowsWhenDragging;
		[Export ("thumbGrowsWhenDragging")]
		bool ThumbGrowsWhenDragging { get; set; }

		// @property (assign, nonatomic) CGFloat thumbMaxRippleRadius;
		[Export ("thumbMaxRippleRadius")]
		nfloat ThumbMaxRippleRadius { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisplayInk;
		[Export ("shouldDisplayInk")]
		bool ShouldDisplayInk { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisplayDiscreteDots;
		[Export ("shouldDisplayDiscreteDots")]
		bool ShouldDisplayDiscreteDots { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisplayDiscreteValueLabel;
		[Export ("shouldDisplayDiscreteValueLabel")]
		bool ShouldDisplayDiscreteValueLabel { get; set; }

		// @property (assign, nonatomic) BOOL shouldDisplayFilledTrack;
		[Export ("shouldDisplayFilledTrack")]
		bool ShouldDisplayFilledTrack { get; set; }

		// @property (assign, nonatomic) BOOL disabledTrackHasThumbGaps;
		[Export ("disabledTrackHasThumbGaps")]
		bool DisabledTrackHasThumbGaps { get; set; }

		// @property (assign, nonatomic) BOOL trackEndsAreRounded;
		[Export ("trackEndsAreRounded")]
		bool TrackEndsAreRounded { get; set; }

		// @property (assign, nonatomic) BOOL trackEndsAreInset;
		[Export ("trackEndsAreInset")]
		bool TrackEndsAreInset { get; set; }

		// @property (assign, nonatomic) CGFloat filledTrackAnchorValue;
		[Export ("filledTrackAnchorValue")]
		nfloat FilledTrackAnchorValue { get; set; }

		// @property (nonatomic, strong) MDCThumbView * _Nullable thumbView;
		[NullAllowed, Export ("thumbView",ArgumentSemantic.Strong)]
		MDCThumbView ThumbView { get; set; }

		// @property (assign, nonatomic) BOOL continuousUpdateEvents;
		[Export ("continuousUpdateEvents")]
		bool ContinuousUpdateEvents { get; set; }

		// @property (assign, nonatomic) BOOL panningAllowedOnEntireControl;
		[Export ("panningAllowedOnEntireControl")]
		bool PanningAllowedOnEntireControl { get; set; }

		// @property (assign, nonatomic) BOOL tapsAllowedOnThumb;
		[Export ("tapsAllowedOnThumb")]
		bool TapsAllowedOnThumb { get; set; }

		// -(instancetype _Nonnull)initWithFrame:(CGRect)frame onTintColor:(UIColor * _Nullable)onTintColor;
		[Export ("initWithFrame:onTintColor:")]
		IntPtr Constructor (CGRect frame,[NullAllowed] UIColor onTintColor);

		// -(void)setValue:(CGFloat)value animated:(BOOL)animated;
		[Export ("setValue:animated:")]
		void SetValue (nfloat value,bool animated);

		// -(void)setValue:(CGFloat)value animated:(BOOL)animated animateThumbAfterMove:(BOOL)animateThumbAfterMove userGenerated:(BOOL)userGenerated completion:(void (^ _Nullable)(void))completion;
		[Export ("setValue:animated:animateThumbAfterMove:userGenerated:completion:")]
		void SetValue (nfloat value,bool animated,bool animateThumbAfterMove,bool userGenerated,[NullAllowed] Action completion);

		// -(void)setIcon:(UIImage * _Nullable)icon;
		[Export ("setIcon:")]
		void SetIcon ([NullAllowed] UIImage icon);
	}

	// @protocol MDCThumbTrackDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCThumbTrackDelegate
	{
		// @optional -(NSString * _Nonnull)thumbTrack:(MDCThumbTrack * _Nonnull)thumbTrack stringForValue:(CGFloat)value;
		[Export ("thumbTrack:stringForValue:")]
		string ThumbTrack (MDCThumbTrack thumbTrack,nfloat value);

		// @optional -(BOOL)thumbTrack:(MDCThumbTrack * _Nonnull)thumbTrack shouldJumpToValue:(CGFloat)value;
		[Export ("thumbTrack:shouldJumpToValue:")]
		bool ThumbTrack2 (MDCThumbTrack thumbTrack,nfloat value);

		// @optional -(void)thumbTrack:(MDCThumbTrack * _Nonnull)thumbTrack willJumpToValue:(CGFloat)value;
		[Export ("thumbTrack:willJumpToValue:")]
		void ThumbTrack3 (MDCThumbTrack thumbTrack,nfloat value);

		// @optional -(void)thumbTrack:(MDCThumbTrack * _Nonnull)thumbTrack willAnimateToValue:(CGFloat)value;
		[Export ("thumbTrack:willAnimateToValue:")]
		void ThumbTrack4 (MDCThumbTrack thumbTrack,nfloat value);

		// @optional -(void)thumbTrack:(MDCThumbTrack * _Nonnull)thumbTrack didAnimateToValue:(CGFloat)value;
		[Export ("thumbTrack:didAnimateToValue:")]
		void ThumbTrack5 (MDCThumbTrack thumbTrack,nfloat value);
	}

	// @interface MDCThumbView : UIView
	[BaseType (typeof (UIView))]
	interface MDCThumbView
	{
		// @property (assign, nonatomic) BOOL hasShadow;
		[Export ("hasShadow")]
		bool HasShadow { get; set; }

		// @property (assign, nonatomic) CGFloat borderWidth;
		[Export ("borderWidth")]
		nfloat BorderWidth { get; set; }

		// @property (assign, nonatomic) CGFloat cornerRadius;
		[Export ("cornerRadius")]
		nfloat CornerRadius { get; set; }

		// -(void)setIcon:(UIImage * _Nullable)icon;
		[Export ("setIcon:")]
		void SetIcon ([NullAllowed] UIImage icon);
	}

	// @interface MDCTriangleEdgeTreatment : MDCEdgeTreatment
	[BaseType (typeof (MDCEdgeTreatment))]
	[DisableDefaultCtor]
	interface MDCTriangleEdgeTreatment
	{
		// @property (assign, nonatomic) CGFloat size;
		[Export ("size")]
		nfloat Size { get; set; }

		// @property (assign, nonatomic) MDCTriangleEdgeStyle style;
		[Export ("style",ArgumentSemantic.Assign)]
		MDCTriangleEdgeStyle Style { get; set; }

		// -(instancetype _Nonnull)initWithSize:(CGFloat)size style:(MDCTriangleEdgeStyle)style __attribute__((objc_designated_initializer));
		[Export ("initWithSize:style:")]
		[DesignatedInitializer]
		IntPtr Constructor (nfloat size,MDCTriangleEdgeStyle style);

		//// -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
		//[Export ("initWithCoder:")]
		//[DesignatedInitializer]
		//IntPtr Constructor (NSCoder aDecoder); TODO not needed
	}

	// @protocol MDCTypographyFontLoading <NSObject>
	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface MDCTypographyFontLoading
	{
		// @required -(UIFont * _Nullable)lightFontOfSize:(CGFloat)fontSize;
		[Abstract]
		[Export ("lightFontOfSize:")]
		[return: NullAllowed]
		UIFont LightFontOfSize (nfloat fontSize);

		// @required -(UIFont * _Nonnull)regularFontOfSize:(CGFloat)fontSize;
		[Abstract]
		[Export ("regularFontOfSize:")]
		UIFont RegularFontOfSize (nfloat fontSize);

		// @required -(UIFont * _Nullable)mediumFontOfSize:(CGFloat)fontSize;
		[Abstract]
		[Export ("mediumFontOfSize:")]
		[return: NullAllowed]
		UIFont MediumFontOfSize (nfloat fontSize);

		// @optional -(UIFont * _Nonnull)boldFontOfSize:(CGFloat)fontSize;
		[Export ("boldFontOfSize:")]
		UIFont BoldFontOfSize (nfloat fontSize);

		// @optional -(UIFont * _Nonnull)italicFontOfSize:(CGFloat)fontSize;
		[Export ("italicFontOfSize:")]
		UIFont ItalicFontOfSize (nfloat fontSize);

		// @optional -(UIFont * _Nullable)boldItalicFontOfSize:(CGFloat)fontSize;
		[Export ("boldItalicFontOfSize:")]
		[return: NullAllowed]
		UIFont BoldItalicFontOfSize (nfloat fontSize);

		// @optional -(UIFont * _Nonnull)boldFontFromFont:(UIFont * _Nonnull)font;
		[Export ("boldFontFromFont:")]
		UIFont BoldFontFromFont (UIFont font);

		// @optional -(UIFont * _Nonnull)italicFontFromFont:(UIFont * _Nonnull)font;
		[Export ("italicFontFromFont:")]
		UIFont ItalicFontFromFont (UIFont font);

		// @optional -(BOOL)isLargeForContrastRatios:(UIFont * _Nonnull)font;
		[Export ("isLargeForContrastRatios:")]
		bool IsLargeForContrastRatios (UIFont font);
	}

	// @interface MDCTypography : NSObject
	[BaseType (typeof (NSObject))]
	interface MDCTypography
	{
		// +(id<MDCTypographyFontLoading> _Nonnull)fontLoader;
		// +(void)setFontLoader:(id<MDCTypographyFontLoading> _Nonnull)fontLoader;
		[Static]
		[Export ("fontLoader")]
		MDCTypographyFontLoading FontLoader { get; set; }

		// +(UIFont * _Nonnull)display4Font;
		[Static]
		[Export ("display4Font")]
		UIFont Display4Font { get; }

		// +(CGFloat)display4FontOpacity;
		[Static]
		[Export ("display4FontOpacity")]
		nfloat Display4FontOpacity { get; }

		// +(UIFont * _Nonnull)display3Font;
		[Static]
		[Export ("display3Font")]
		UIFont Display3Font { get; }

		// +(CGFloat)display3FontOpacity;
		[Static]
		[Export ("display3FontOpacity")]
		nfloat Display3FontOpacity { get; }

		// +(UIFont * _Nonnull)display2Font;
		[Static]
		[Export ("display2Font")]
		UIFont Display2Font { get; }

		// +(CGFloat)display2FontOpacity;
		[Static]
		[Export ("display2FontOpacity")]
		nfloat Display2FontOpacity { get; }

		// +(UIFont * _Nonnull)display1Font;
		[Static]
		[Export ("display1Font")]
		UIFont Display1Font { get; }

		// +(CGFloat)display1FontOpacity;
		[Static]
		[Export ("display1FontOpacity")]
		nfloat Display1FontOpacity { get; }

		// +(UIFont * _Nonnull)headlineFont;
		[Static]
		[Export ("headlineFont")]
		UIFont HeadlineFont { get; }

		// +(CGFloat)headlineFontOpacity;
		[Static]
		[Export ("headlineFontOpacity")]
		nfloat HeadlineFontOpacity { get; }

		// +(UIFont * _Nonnull)titleFont;
		[Static]
		[Export ("titleFont")]
		UIFont TitleFont { get; }

		// +(CGFloat)titleFontOpacity;
		[Static]
		[Export ("titleFontOpacity")]
		nfloat TitleFontOpacity { get; }

		// +(UIFont * _Nonnull)subheadFont;
		[Static]
		[Export ("subheadFont")]
		UIFont SubheadFont { get; }

		// +(CGFloat)subheadFontOpacity;
		[Static]
		[Export ("subheadFontOpacity")]
		nfloat SubheadFontOpacity { get; }

		// +(UIFont * _Nonnull)body2Font;
		[Static]
		[Export ("body2Font")]
		UIFont Body2Font { get; }

		// +(CGFloat)body2FontOpacity;
		[Static]
		[Export ("body2FontOpacity")]
		nfloat Body2FontOpacity { get; }

		// +(UIFont * _Nonnull)body1Font;
		[Static]
		[Export ("body1Font")]
		UIFont Body1Font { get; }

		// +(CGFloat)body1FontOpacity;
		[Static]
		[Export ("body1FontOpacity")]
		nfloat Body1FontOpacity { get; }

		// +(UIFont * _Nonnull)captionFont;
		[Static]
		[Export ("captionFont")]
		UIFont CaptionFont { get; }

		// +(CGFloat)captionFontOpacity;
		[Static]
		[Export ("captionFontOpacity")]
		nfloat CaptionFontOpacity { get; }

		// +(UIFont * _Nonnull)buttonFont;
		[Static]
		[Export ("buttonFont")]
		UIFont ButtonFont { get; }

		// +(CGFloat)buttonFontOpacity;
		[Static]
		[Export ("buttonFontOpacity")]
		nfloat ButtonFontOpacity { get; }

		// +(UIFont * _Nonnull)boldFontFromFont:(UIFont * _Nonnull)font;
		[Static]
		[Export ("boldFontFromFont:")]
		UIFont BoldFontFromFont (UIFont font);

		// +(UIFont * _Nonnull)italicFontFromFont:(UIFont * _Nonnull)font;
		[Static]
		[Export ("italicFontFromFont:")]
		UIFont ItalicFontFromFont (UIFont font);

		// +(BOOL)isLargeForContrastRatios:(UIFont * _Nonnull)font;
		[Static]
		[Export ("isLargeForContrastRatios:")]
		bool IsLargeForContrastRatios (UIFont font);
	}

	// @interface MDCSystemFontLoader : NSObject <MDCTypographyFontLoading>
	[BaseType (typeof (NSObject))]
	interface MDCSystemFontLoader : MDCTypographyFontLoading
	{
	}

	// @interface MDCTimingFunction (UIView)
	[Category]
	[BaseType (typeof (UIView))]
	interface UIView_MDCTimingFunction
	{
		// +(void)mdc_animateWithTimingFunction:(CAMediaTimingFunction * _Nullable)timingFunction duration:(NSTimeInterval)duration delay:(NSTimeInterval)delay options:(UIViewAnimationOptions)options animations:(void (^ _Nonnull)(void))animations completion:(void (^ _Nullable)(BOOL))completion;
		[Static]
		[Export ("mdc_animateWithTimingFunction:duration:delay:options:animations:completion:")]
		void Mdc_animateWithTimingFunction ([NullAllowed] CAMediaTimingFunction timingFunction,double duration,double delay,UIViewAnimationOptions options,Action animations,[NullAllowed] Action<bool> completion);
	}

	// @interface AppExtensions (UIApplication)
	[Category]
	[BaseType (typeof (UIApplication))]
	interface UIApplication_AppExtensions
	{
		// +(UIApplication *)mdc_safeSharedApplication;
		[Static]
		[Export ("mdc_safeSharedApplication")]
		UIApplication Mdc_safeSharedApplication ();

		// +(BOOL)mdc_isAppExtension;
		[Static]
		[Export ("mdc_isAppExtension")]
		bool Mdc_isAppExtension ();
	}

	// @interface MaterialBottomSheet (UIViewController)
	[Category]
	[BaseType (typeof (UIViewController))]
	interface UIViewController_MaterialBottomSheet
	{
		// @property (readonly, nonatomic) MDCBottomSheetPresentationController * _Nullable mdc_bottomSheetPresentationController;
		[NullAllowed, Export ("mdc_bottomSheetPresentationController")]
		MDCBottomSheetPresentationController Mdc_bottomSheetPresentationController ();
	}

	// @interface ic_arrow_back (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_arrow_back
	{
		// +(NSString * _Nonnull)pathFor_ic_arrow_back;
		[Static]
		[Export ("pathFor_ic_arrow_back")]
		string PathFor_ic_arrow_back { get; }

		// +(void)ic_arrow_backUseNewStyle:(BOOL)useNewStyle;
		[Static]
		[Export ("ic_arrow_backUseNewStyle:")]
		void Ic_arrow_backUseNewStyle (bool useNewStyle);

		// +(UIImage * _Nullable)imageFor_ic_arrow_back;
		[Static]
		[NullAllowed, Export ("imageFor_ic_arrow_back")]
		UIImage ImageFor_ic_arrow_back { get; }
	}

	// @interface ic_check (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_check
	{
		// +(NSString * _Nonnull)pathFor_ic_check;
		[Static]
		[Export ("pathFor_ic_check")]
		string PathFor_ic_check { get; }

		// +(UIImage * _Nullable)imageFor_ic_check;
		[Static]
		[NullAllowed, Export ("imageFor_ic_check")]
		UIImage ImageFor_ic_check { get; }
	}

	// @interface ic_check_circle (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_check_circle
	{
		// +(NSString * _Nonnull)pathFor_ic_check_circle;
		[Static]
		[Export ("pathFor_ic_check_circle")]
		string PathFor_ic_check_circle { get; }

		// +(UIImage * _Nullable)imageFor_ic_check_circle;
		[Static]
		[NullAllowed, Export ("imageFor_ic_check_circle")]
		UIImage ImageFor_ic_check_circle { get; }
	}

	// @interface ic_chevron_right (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_chevron_right
	{
		// +(NSString * _Nonnull)pathFor_ic_chevron_right;
		[Static]
		[Export ("pathFor_ic_chevron_right")]
		string PathFor_ic_chevron_right { get; }

		// +(UIImage * _Nullable)imageFor_ic_chevron_right;
		[Static]
		[NullAllowed, Export ("imageFor_ic_chevron_right")]
		UIImage ImageFor_ic_chevron_right { get; }
	}

	// @interface ic_info (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_info
	{
		// +(NSString * _Nonnull)pathFor_ic_info;
		[Static]
		[Export ("pathFor_ic_info")]
		string PathFor_ic_info { get; }

		// +(UIImage * _Nullable)imageFor_ic_info;
		[Static]
		[NullAllowed, Export ("imageFor_ic_info")]
		UIImage ImageFor_ic_info { get; }
	}

	// @interface ic_radio_button_unchecked (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_radio_button_unchecked
	{
		// +(NSString * _Nonnull)pathFor_ic_radio_button_unchecked;
		[Static]
		[Export ("pathFor_ic_radio_button_unchecked")]
		string PathFor_ic_radio_button_unchecked { get; }

		// +(UIImage * _Nullable)imageFor_ic_radio_button_unchecked;
		[Static]
		[NullAllowed, Export ("imageFor_ic_radio_button_unchecked")]
		UIImage ImageFor_ic_radio_button_unchecked { get; }
	}

	// @interface ic_reorder (MDCIcons)
	[Category]
	[BaseType (typeof (MDCIcons))]
	interface MDCIcons_ic_reorder
	{
		// +(NSString * _Nonnull)pathFor_ic_reorder;
		[Static]
		[Export ("pathFor_ic_reorder")]
		string PathFor_ic_reorder { get; }

		// +(UIImage * _Nullable)imageFor_ic_reorder;
		[Static]
		[NullAllowed, Export ("imageFor_ic_reorder")]
		UIImage ImageFor_ic_reorder { get; }
	}

	// @interface MaterialTypography (UIFont)
	[Category]
	[BaseType (typeof (UIFont))]
	interface UIFont_MaterialTypography
	{
		// +(UIFont * _Nonnull)mdc_preferredFontForMaterialTextStyle:(MDCFontTextStyle)style;
		[Static]
		[Export ("mdc_preferredFontForMaterialTextStyle:")]
		UIFont Mdc_preferredFontForMaterialTextStyle (MDCFontTextStyle style);
	}

	// @interface MaterialTypography (UIFontDescriptor)
	[Category]
	[BaseType (typeof (UIFontDescriptor))]
	interface UIFontDescriptor_MaterialTypography
	{
		// +(UIFontDescriptor * _Nonnull)mdc_preferredFontDescriptorForMaterialTextStyle:(MDCFontTextStyle)style;
		[Static]
		[Export ("mdc_preferredFontDescriptorForMaterialTextStyle:")]
		UIFontDescriptor Mdc_preferredFontDescriptorForMaterialTextStyle (MDCFontTextStyle style);
	}


}
