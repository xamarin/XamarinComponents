using System;
using UIKit;

namespace RZTransitions
{
	partial class UniqueTransition
	{
		public UniqueTransition (TransitionAction action, Type fromViewController, Type toViewController)
			: this (action, new ObjCRuntime.Class (fromViewController), new ObjCRuntime.Class (toViewController))
		{
		}

		public void SetFromViewControllerClass (Type type)
		{
			FromViewControllerClass = new ObjCRuntime.Class (type);
		}

		public void SetFromViewControllerClass<T> ()
			where T : UIViewController
		{
			FromViewControllerClass = new ObjCRuntime.Class (typeof(T));
		}

		public void SetToViewControllerClass (Type type)
		{
			ToViewControllerClass = new ObjCRuntime.Class (type);
		}

		public void SetToViewControllerClass<T> ()
			where T : UIViewController
		{
			ToViewControllerClass = new ObjCRuntime.Class (typeof(T));
		}
	}

	partial class TransitionsManager
	{
		public UniqueTransition SetAnimationController (IAnimationController animationController, Type fromViewController, TransitionAction action)
		{
			return SetAnimationController (
				animationController, 
				fromViewController == null ? null : new ObjCRuntime.Class (fromViewController),
				action);
		}

		public UniqueTransition SetAnimationController<T> (IAnimationController animationController, TransitionAction action)
			where T : UIViewController
		{
			return SetAnimationController (
				animationController,
				new ObjCRuntime.Class (typeof(T)), 
				action);
		}

		public UniqueTransition SetAnimationController (IAnimationController animationController, Type fromViewController, Type toViewController, TransitionAction action)
		{
			return SetAnimationController (
				animationController, 
				fromViewController == null ? null : new ObjCRuntime.Class (fromViewController),
				toViewController == null ? null : new ObjCRuntime.Class (toViewController),
				action);
		}

		public UniqueTransition SetAnimationController<TFrom, TTo> (IAnimationController animationController, TransitionAction action)
			where TFrom : UIViewController
			where TTo : UIViewController
		{
			return SetAnimationController (
				animationController, 
				new ObjCRuntime.Class (typeof(TFrom)), 
				new ObjCRuntime.Class (typeof(TTo)), 
				action);
		}

		public UniqueTransition SetInteractionController (ITransitionInteractionController interactionController, Type fromViewController, Type toViewController, TransitionAction action)
		{
			return SetInteractionController (
				interactionController, 
				fromViewController == null ? null : new ObjCRuntime.Class (fromViewController),
				toViewController == null ? null : new ObjCRuntime.Class (toViewController),
				action);
		}

		public UniqueTransition SetInteractionController<TFrom, TTo> (ITransitionInteractionController interactionController, TransitionAction action)
			where TFrom : UIViewController
			where TTo : UIViewController
		{
			return SetInteractionController (
				interactionController, 
				new ObjCRuntime.Class (typeof(TFrom)), 
				new ObjCRuntime.Class (typeof(TTo)), 
				action);
		}

		public UniqueTransition SetInteractionController (ITransitionInteractionController interactionController, Type fromViewController, TransitionAction action)
		{
			return SetInteractionController (
				interactionController, 
				fromViewController == null ? null : new ObjCRuntime.Class (fromViewController),
				null,
				action);
		}

		public UniqueTransition SetInteractionController<TFrom> (ITransitionInteractionController interactionController, TransitionAction action)
			where TFrom : UIViewController
		{
			return SetInteractionController (
				interactionController, 
				new ObjCRuntime.Class (typeof(TFrom)), 
				null, 
				action);
		}
	}
}
