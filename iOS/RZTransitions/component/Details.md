
<iframe src="https://appetize.io/embed/rh47p8bj2tnuh3cczpvyrqukdc?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

**RZTransitions** is a library to help make iOS custom View Controller transitions slick and simple.

## Features

 - A comprehensive library of animation controllers
 - A comprehensive library of interaction controllers
 - Mix and match any animation controller with any interaction controller
 - A shared instance manager that helps wrap the iOS custom transition protocol 
   to expose a friendlier API

You can use any of the animation controllers or interaction controllers without 
the `TransitionsManager` and simply use them with the iOS custom View Controller 
transition APIs.

## Usage

### Presenting a View Controller

    var manager = TransitionsManager.Shared;
    this.TransitioningDelegate = manager;
    
    var nextController = new UIViewController ();
    nextController.TransitioningDelegate = manager;
    
    this.PresentViewController (nextController, true, null);

### Creating a Navigation Controller 

_Or, use `TransitionsNavigationController`_

    var navigationController = new UINavigationController ();
    navigationController.Delegate = TransitionsManager.Shared;

## Specifying Transitions for Specific View Controllers

    // Use the ZoomPushAnimationController when pushing from this view controller 
    // to a SimpleCollectionViewController or popping from a 
    // SimpleCollectionViewController to this view controller.
    TransitionsManager.Shared.SetAnimationController (
        new ZoomPushAnimationController (),
        this.GetType (),
        typeof (SimpleCollectionViewController)
        TransitionAction.PushPop);

## Hooking up Interactors

    private ITransitionInteractionController presentInteractionController;

    public override void ViewDidLoad ()
    {
        base.ViewDidLoad ();
        
        // Create the presentation interaction controller that allows a custom 
        // gesture to control presenting a new VC via a presentViewController
        presentInteractionController = new VerticalSwipeInteractionController ();
        presentInteractionController.NextViewControllerDelegate = this;
        presentInteractionController.AttachViewController (this, TransitionAction.Present);
    }

    public override void ViewWillAppear (bool animated)
    {
        base.ViewWillAppear (animated);
        
        // Use the present interaction controller for presenting any view controller 
        // from this view controller
        TransitionsManager.Shared.SetInteractionController<SimpleViewController> (
            presentInteractionController,
            TransitionAction.Present);
    }

## Setting a New Default Transition

    TransitionsManager.Shared.DefaultPresentDismissAnimationController = new ZoomAlphaAnimationController ();
    TransitionsManager.Shared.DefaultPushPopAnimationController = new CardSlideAnimationController ();
