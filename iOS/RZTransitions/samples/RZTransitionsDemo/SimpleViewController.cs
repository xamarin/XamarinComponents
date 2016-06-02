using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using RZTransitions;

namespace RZTransitionsDemo
{
	partial class SimpleViewController : UIViewController, ITransitionInteractionControllerDelegate
	{
		ITransitionInteractionController pushPopInteractionController;
		ITransitionInteractionController presentInteractionController;
		ITransitionInteractionController pinchInteractionController;

		public SimpleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Create the push and pop interaction controller that allows a custom gesture
			// to control pushing and popping from the navigation controller
			pushPopInteractionController = new HorizontalInteractionController ();
			pushPopInteractionController.NextViewControllerDelegate = this;
			pushPopInteractionController.AttachViewController (this, TransitionAction.PushPop);
			TransitionsManager.Shared.SetInteractionController<SimpleViewController> (
				pushPopInteractionController,
				TransitionAction.PushPop);

			// Create the presentation interaction controller that allows a custom gesture
			// to control presenting a new VC via a presentViewController
			presentInteractionController = new VerticalSwipeInteractionController ();
			presentInteractionController.NextViewControllerDelegate = this;
			presentInteractionController.AttachViewController (this, TransitionAction.Present);

			pinchInteractionController = new PinchInteractionController ();
			pinchInteractionController.NextViewControllerDelegate = this;
			pinchInteractionController.AttachViewController (this, TransitionAction.Present);

			// Setup the push & pop animations as well as a special animation for pushing a
			// RZSimpleCollectionViewController
			TransitionsManager.Shared.SetAnimationController<SimpleViewController> (
				new CardSlideAnimationController (),
				TransitionAction.PushPop);
//			TransitionsManager.Shared.SetAnimationController (new ZoomPushAnimationController (),
//				this.Class,
//				toViewController:[RZSimpleCollectionViewController class]
//				TransitionAction.PushPop);

			// Setup the animations for presenting and dismissing a new VC
			TransitionsManager.Shared.SetAnimationController<SimpleViewController> (
				new CirclePushAnimationController (),
				TransitionAction.PresentDismiss);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			TransitionsManager.Shared.SetInteractionController<SimpleViewController> (
				presentInteractionController,
				TransitionAction.Present);
			TransitionsManager.Shared.SetInteractionController<SimpleViewController> (
				pinchInteractionController,
				TransitionAction.Present);
		}

		partial void PushNewViewController (UIButton sender)
		{
			NavigationController.PushViewController (NextSimpleViewController (), true);
		}

		partial void PopViewController (UIButton sender)
		{
			NavigationController.PopViewController (true);
		}

		partial void ShowModal (UIButton sender)
		{
			PresentViewController (NextSimpleColorViewController (), true, null);
		}

		UIViewController NextSimpleViewController ()
		{
			var newVC = UIStoryboard.FromName ("Main", null).InstantiateViewController ("SimpleViewController") as SimpleViewController;
			newVC.TransitioningDelegate = TransitionsManager.Shared;
			return newVC;
		}

		UIViewController NextSimpleColorViewController ()
		{
			var newColorVC = UIStoryboard.FromName ("Main", null).InstantiateViewController ("SimpleColorViewController") as SimpleColorViewController;
			newColorVC.TransitioningDelegate = TransitionsManager.Shared;

			// Create a dismiss interaction controller that will be attached to the presented
			// view controller to allow for a custom dismissal
			var dismissInteractionController = new VerticalSwipeInteractionController ();
			dismissInteractionController.AttachViewController (newColorVC, TransitionAction.Dismiss);
			TransitionsManager.Shared.SetInteractionController<SimpleViewController> (
				dismissInteractionController,
				TransitionAction.Dismiss);
			
			return newColorVC;
		}

		[Export ("nextViewControllerForInteractor:")]
		public virtual UIViewController NextViewController (ITransitionInteractionController interactor)
		{
			return NextSimpleViewController ();
		}
	}
}
