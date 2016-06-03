using System;

using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace RZTransitions
{
	// @interface RZTransitionsNavigationController : UINavigationController
	[BaseType (typeof(UINavigationController), Name="RZTransitionsNavigationController")]
	interface TransitionsNavigationController
	{
	}

	// @interface RZUniqueTransition : NSObject <NSCopying>
	[BaseType (typeof(NSObject), Name="RZUniqueTransition")]
	interface UniqueTransition : INSCopying
	{
		// @property (assign, nonatomic) RZTransitionAction transitionAction;
		[Export ("transitionAction", ArgumentSemantic.Assign)]
		TransitionAction TransitionAction { get; set; }

		// @property (assign, nonatomic) Class fromViewControllerClass;
		[Export ("fromViewControllerClass", ArgumentSemantic.Assign)]
		Class FromViewControllerClass { get; set; }

		// @property (assign, nonatomic) Class toViewControllerClass;
		[Export ("toViewControllerClass", ArgumentSemantic.Assign)]
		Class ToViewControllerClass { get; set; }

		// -(instancetype)initWithAction:(RZTransitionAction)action withFromViewControllerClass:(Class)fromViewController withToViewControllerClass:(Class)toViewController;
		[Export ("initWithAction:withFromViewControllerClass:withToViewControllerClass:")]
		IntPtr Constructor (TransitionAction action, Class fromViewController, Class toViewController);
	}

	interface ITransitionInteractionControllerDelegate {}

	// @protocol RZTransitionInteractionControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name="RZTransitionInteractionControllerDelegate")]
	interface TransitionInteractionControllerDelegate
	{
		// @optional -(UIViewController *)nextViewControllerForInteractor:(id<RZTransitionInteractionController>)interactor;
		[Export ("nextViewControllerForInteractor:")]
		UIViewController NextViewController (ITransitionInteractionController interactor);
	}

	interface ITransitionInteractionController {}

	// @protocol RZTransitionInteractionController <UIViewControllerInteractiveTransitioning>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name="RZTransitionInteractionController")]
	interface TransitionInteractionController : IUIViewControllerInteractiveTransitioning
	{
		// @required @property (assign, readwrite, nonatomic) BOOL isInteractive;
		[Abstract]
		[Export ("isInteractive")]
		bool IsInteractive { get; set; }

		// @required @property (assign, readwrite, nonatomic) BOOL shouldCompleteTransition;
		[Abstract]
		[Export ("shouldCompleteTransition")]
		bool ShouldCompleteTransition { get; set; }

		// @required @property (assign, readwrite, nonatomic) RZTransitionAction action;
		[Abstract]
		[Export ("action", ArgumentSemantic.Assign)]
		TransitionAction Action { get; set; }

		// @required @property (nonatomic, weak) id<RZTransitionInteractionControllerDelegate> _Nullable nextViewControllerDelegate;
		[Abstract]
		[NullAllowed, Export ("nextViewControllerDelegate", ArgumentSemantic.Weak)]
		ITransitionInteractionControllerDelegate NextViewControllerDelegate { get; set; }

		// @required -(void)attachViewController:(UIViewController *)viewController withAction:(RZTransitionAction)action;
		[Abstract]
		[Export ("attachViewController:withAction:")]
		void AttachViewController (UIViewController viewController, TransitionAction action);
	}

	// @interface RZBaseSwipeInteractionController : UIPercentDrivenInteractiveTransition <RZTransitionInteractionController, UIGestureRecognizerDelegate>
	[BaseType (typeof(UIPercentDrivenInteractiveTransition), Name="RZBaseSwipeInteractionController")]
	interface BaseSwipeInteractionController : TransitionInteractionController, IUIGestureRecognizerDelegate
	{
		// @property (nonatomic, weak) UIViewController * _Nullable fromViewController;
		[NullAllowed, Export ("fromViewController", ArgumentSemantic.Weak)]
		UIViewController FromViewController { get; set; }

		// @property (nonatomic, strong) UIPanGestureRecognizer * gestureRecognizer;
		[Export ("gestureRecognizer", ArgumentSemantic.Strong)]
		UIPanGestureRecognizer GestureRecognizer { get; set; }

		// @property (assign, nonatomic) BOOL reverseGestureDirection;
		[Export ("reverseGestureDirection")]
		bool ReverseGestureDirection { get; set; }

		// -(BOOL)isGesturePositive:(UIPanGestureRecognizer *)panGestureRecognizer;
		[Export ("isGesturePositive:")]
		bool IsGesturePositive (UIPanGestureRecognizer panGestureRecognizer);

		// -(CGFloat)swipeCompletionPercent;
		[Export ("swipeCompletionPercent")]
		nfloat SwipeCompletionPercent { get; }

		// -(CGFloat)translationPercentageWithPanGestureRecongizer:(UIPanGestureRecognizer *)panGestureRecognizer;
		[Export ("translationPercentageWithPanGestureRecongizer:")]
		nfloat TranslationPercentage (UIPanGestureRecognizer panGestureRecognizer);

		// -(CGFloat)translationWithPanGestureRecongizer:(UIPanGestureRecognizer *)panGestureRecognizer;
		[Export ("translationWithPanGestureRecongizer:")]
		nfloat Translation (UIPanGestureRecognizer panGestureRecognizer);
	}

	// @interface RZHorizontalInteractionController : RZBaseSwipeInteractionController
	[BaseType (typeof(BaseSwipeInteractionController), Name="RZHorizontalInteractionController")]
	interface HorizontalInteractionController
	{
	}

	// @interface RZOverscrollInteractionController : UIPercentDrivenInteractiveTransition <RZTransitionInteractionController, UIScrollViewDelegate>
	[BaseType (typeof(UIPercentDrivenInteractiveTransition), Name="RZOverscrollInteractionController")]
	interface OverscrollInteractionController : TransitionInteractionController, IUIScrollViewDelegate
	{
		// @property (nonatomic, strong) UIViewController * fromViewController;
		[Export ("fromViewController", ArgumentSemantic.Strong)]
		UIViewController FromViewController { get; set; }

		// -(void)watchScrollView:(UIScrollView *)scrollView;
		[Export ("watchScrollView:")]
		void WatchScrollView (UIScrollView scrollView);

		// -(CGFloat)completionPercent;
		[Export ("completionPercent")]
		nfloat CompletionPercent { get; }
	}

	// @interface RZPinchInteractionController : UIPercentDrivenInteractiveTransition <RZTransitionInteractionController, UIGestureRecognizerDelegate>
	[BaseType (typeof(UIPercentDrivenInteractiveTransition), Name="RZPinchInteractionController")]
	interface PinchInteractionController : TransitionInteractionController, IUIGestureRecognizerDelegate
	{
		// @property (nonatomic, weak) UIViewController * _Nullable fromViewController;
		[NullAllowed, Export ("fromViewController", ArgumentSemantic.Weak)]
		UIViewController FromViewController { get; set; }

		// @property (nonatomic, strong) UIPinchGestureRecognizer * gestureRecognizer;
		[Export ("gestureRecognizer", ArgumentSemantic.Strong)]
		UIPinchGestureRecognizer GestureRecognizer { get; set; }

		// -(CGFloat)translationPercentageWithPinchGestureRecognizer:(UIPinchGestureRecognizer *)pinchGestureRecognizer;
		[Export ("translationPercentageWithPinchGestureRecognizer:")]
		nfloat TranslationPercentage (UIPinchGestureRecognizer pinchGestureRecognizer);
	}

	// @interface RZVerticalSwipeInteractionController : RZBaseSwipeInteractionController
	[BaseType (typeof(BaseSwipeInteractionController), Name="RZVerticalSwipeInteractionController")]
	interface VerticalSwipeInteractionController
	{
	}

	// @interface RZTransitionsManager : NSObject <UINavigationControllerDelegate, UIViewControllerTransitioningDelegate, UITabBarControllerDelegate>
	[BaseType (typeof(NSObject), Name="RZTransitionsManager")]
	interface TransitionsManager : IUINavigationControllerDelegate, IUIViewControllerTransitioningDelegate, IUITabBarControllerDelegate
	{
		// @property (nonatomic, strong) id<RZAnimationControllerProtocol> _Nullable defaultPushPopAnimationController;
		[NullAllowed, Export ("defaultPushPopAnimationController", ArgumentSemantic.Strong)]
		IAnimationController DefaultPushPopAnimationController { get; set; }

		// @property (nonatomic, strong) id<RZAnimationControllerProtocol> _Nullable defaultPresentDismissAnimationController;
		[NullAllowed, Export ("defaultPresentDismissAnimationController", ArgumentSemantic.Strong)]
		IAnimationController DefaultPresentDismissAnimationController { get; set; }

		// @property (nonatomic, strong) id<RZAnimationControllerProtocol> _Nullable defaultTabBarAnimationController;
		[NullAllowed, Export ("defaultTabBarAnimationController", ArgumentSemantic.Strong)]
		IAnimationController DefaultTabBarAnimationController { get; set; }

		// +(RZTransitionsManager * _Nonnull)shared;
		[Static]
		[Export ("shared")]
		TransitionsManager Shared { get; }

		// -(RZUniqueTransition * _Nonnull)setAnimationController:(id<RZAnimationControllerProtocol> _Nullable)animationController fromViewController:(Class _Nonnull)fromViewController forAction:(RZTransitionAction)action;
		[Export ("setAnimationController:fromViewController:forAction:")]
		UniqueTransition SetAnimationController ([NullAllowed] IAnimationController animationController, Class fromViewController, TransitionAction action);

		// -(RZUniqueTransition * _Nonnull)setAnimationController:(id<RZAnimationControllerProtocol> _Nullable)animationController fromViewController:(Class _Nullable)fromViewController toViewController:(Class _Nullable)toViewController forAction:(RZTransitionAction)action;
		[Export ("setAnimationController:fromViewController:toViewController:forAction:")]
		UniqueTransition SetAnimationController ([NullAllowed] IAnimationController animationController, [NullAllowed] Class fromViewController, [NullAllowed] Class toViewController, TransitionAction action);

		// -(RZUniqueTransition * _Nonnull)setInteractionController:(id<RZTransitionInteractionController> _Nullable)interactionController fromViewController:(Class _Nullable)fromViewController toViewController:(Class _Nullable)toViewController forAction:(RZTransitionAction)action;
		[Export ("setInteractionController:fromViewController:toViewController:forAction:")]
		UniqueTransition SetInteractionController ([NullAllowed] ITransitionInteractionController interactionController, [NullAllowed] Class fromViewController, [NullAllowed] Class toViewController, TransitionAction action);

		// -(void)overrideAnimationDirection:(BOOL)override withTransition:(RZUniqueTransition * _Nonnull)transitionKey;
		[Export ("overrideAnimationDirection:withTransition:")]
		void OverrideAnimationDirection (bool aOverride, UniqueTransition transitionKey);
	}

	interface IAnimationController {}

	// @protocol RZAnimationControllerProtocol <UIViewControllerAnimatedTransitioning>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name="RZAnimationControllerProtocol")]
	interface AnimationController : IUIViewControllerAnimatedTransitioning
	{
		// @required @property (assign, nonatomic) BOOL isPositiveAnimation;
		[Abstract]
		[Export ("isPositiveAnimation")]
		bool IsPositiveAnimation { get; set; }
	}

	// @interface RZCardSlideAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZCardSlideAnimationController")]
	interface CardSlideAnimationController : AnimationController
	{
		// @property (assign, nonatomic) NSTimeInterval transitionTime;
		[Export ("transitionTime")]
		double TransitionTime { get; set; }

		// @property (assign, nonatomic) BOOL horizontalOrientation;
		[Export ("horizontalOrientation")]
		bool HorizontalOrientation { get; set; }

		// @property (nonatomic, strong) UIColor * containerBackgroundColor;
		[Export ("containerBackgroundColor", ArgumentSemantic.Strong)]
		UIColor ContainerBackgroundColor { get; set; }
	}

	// @interface RZZoomPushAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZZoomPushAnimationController")]
	interface ZoomPushAnimationController : AnimationController
	{
	}

	// @interface RZCirclePushAnimationController : RZZoomPushAnimationController
	[BaseType (typeof(ZoomPushAnimationController), Name="RZCirclePushAnimationController")]
	interface CirclePushAnimationController
	{
		// @property (nonatomic, weak) id<RZCirclePushAnimationDelegate> _Nullable circleDelegate;
		[NullAllowed, Export ("circleDelegate", ArgumentSemantic.Weak)]
		ICirclePushAnimationDelegate CircleDelegate { get; set; }

		// @property (assign, nonatomic) CGFloat maximumCircleScale;
		[Export ("maximumCircleScale")]
		nfloat MaximumCircleScale { get; set; }

		// @property (assign, nonatomic) CGFloat minimumCircleScale;
		[Export ("minimumCircleScale")]
		nfloat MinimumCircleScale { get; set; }
	}

	interface ICirclePushAnimationDelegate {}

	// @protocol RZCirclePushAnimationDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name="RZCirclePushAnimationDelegate")]
	interface CirclePushAnimationDelegate
	{
		// @optional -(CGPoint)circleCenter;
		[Export ("circleCenter")]
		CGPoint CircleCenter { get; }

		// @optional -(CGFloat)circleStartingRadius;
		[Export ("circleStartingRadius")]
		nfloat CircleStartingRadius { get; }
	}

	// @interface RZRectZoomAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZRectZoomAnimationController")]
	interface RectZoomAnimationController : AnimationController
	{
		// @property (nonatomic, weak) id<RZRectZoomAnimationDelegate> _Nullable rectZoomDelegate;
		[NullAllowed, Export ("rectZoomDelegate", ArgumentSemantic.Weak)]
		IRectZoomAnimationDelegate RectZoomDelegate { get; set; }

		// @property (assign, nonatomic) BOOL shouldFadeBackgroundViewController;
		[Export ("shouldFadeBackgroundViewController")]
		bool ShouldFadeBackgroundViewController { get; set; }

		// @property (assign, nonatomic) CGFloat animationSpringDampening;
		[Export ("animationSpringDampening")]
		nfloat AnimationSpringDampening { get; set; }

		// @property (assign, nonatomic) CGFloat animationSpringVelocity;
		[Export ("animationSpringVelocity")]
		nfloat AnimationSpringVelocity { get; set; }
	}

	interface IRectZoomAnimationDelegate {}

	// @protocol RZRectZoomAnimationDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name="RZRectZoomAnimationDelegate")]
	interface RectZoomAnimationDelegate
	{
		// @required -(CGRect)rectZoomPosition;
		[Abstract]
		[Export ("rectZoomPosition")]
		CGRect RectZoomPosition { get; }
	}

	// @interface RZSegmentControlMoveFadeAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZSegmentControlMoveFadeAnimationController")]
	interface SegmentControlMoveFadeAnimationController : AnimationController
	{
	}

	// @interface RZShrinkZoomAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZShrinkZoomAnimationController")]
	interface ShrinkZoomAnimationController : AnimationController
	{
	}

	// @interface RZZoomAlphaAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZZoomAlphaAnimationController")]
	interface ZoomAlphaAnimationController : AnimationController
	{
	}

	// @interface RZZoomBlurAnimationController : NSObject <RZAnimationControllerProtocol>
	[BaseType (typeof(NSObject), Name="RZZoomBlurAnimationController")]
	interface ZoomBlurAnimationController : AnimationController
	{
		// @property (assign, nonatomic) CGFloat blurRadius;
		[Export ("blurRadius")]
		nfloat BlurRadius { get; set; }

		// @property (assign, nonatomic) CGFloat saturationDelta;
		[Export ("saturationDelta")]
		nfloat SaturationDelta { get; set; }

		// @property (nonatomic, strong) UIColor * blurTintColor;
		[Export ("blurTintColor", ArgumentSemantic.Strong)]
		UIColor BlurTintColor { get; set; }
	}

	// @interface RZTransitionsViewHelpers (NSObject)
	[Category]
	[BaseType (typeof(NSObject))]
	interface NSObject_RZTransitionsViewHelpers
	{
		// -(UIView *)rzt_toView;
		[Export ("rzt_toView")]
		UIView Rzt_toView ();

		// -(UIView *)rzt_fromView;
		[Export ("rzt_fromView")]
		UIView Rzt_fromView ();
	}

	// @interface RZTransitionsFastImageBlur (UIImage)
	[Category]
	[BaseType (typeof(UIImage))]
	interface UIImage_RZTransitionsFastImageBlur
	{
		// +(UIImage *)blurredImageByCapturingView:(UIView *)view withRadius:(CGFloat)blurRadius tintColor:(UIColor *)tintColor saturationDeltaFactor:(CGFloat)saturationDeltaFactor;
		[Static]
		[Export ("blurredImageByCapturingView:withRadius:tintColor:saturationDeltaFactor:")]
		UIImage BlurredImageByCapturingView (UIView view, nfloat blurRadius, UIColor tintColor, nfloat saturationDeltaFactor);

		// +(UIImage *)blurredImageByCapturingView:(UIView *)view withRadius:(CGFloat)blurRadius tintColor:(UIColor *)tintColor saturationDeltaFactor:(CGFloat)saturationDeltaFactor afterScreenUpdates:(BOOL)screenUpdates;
		[Static]
		[Export ("blurredImageByCapturingView:withRadius:tintColor:saturationDeltaFactor:afterScreenUpdates:")]
		UIImage BlurredImageByCapturingView (UIView view, nfloat blurRadius, UIColor tintColor, nfloat saturationDeltaFactor, bool screenUpdates);
	}

	// @interface RZTransitionsSnapshotHelpers (UIImage)
	[Category]
	[BaseType (typeof(UIImage))]
	interface UIImage_RZTransitionsSnapshotHelpers
	{
		// +(UIImage *)imageByCapturingView:(UIView *)view afterScreenUpdate:(BOOL)waitForUpdate;
		[Static]
		[Export ("imageByCapturingView:afterScreenUpdate:")]
		UIImage ImageByCapturingView (UIView view, bool waitForUpdate);
	}

}

