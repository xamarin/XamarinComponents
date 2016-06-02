
`TransitionSubmitButton` is an animated `UIButton` that has a built-in activity spinner. It can then transition 
to a fullscreen color on completion. 

##  Using TransitionSubmitButton

The easiest way to make use of `TransitionSubmitButton` is through the `AnimateAsync` method:

    await button.AnimateAsync(async () => {
        // do some work on a background thread ...
        await Task.Delay(1000);
    });
    
    // work is done and animation is over
    PerformSegue("loginSegue", this);

If the work is not `await`-able, we can use the `StartLoadingAnimation` and `StartFinishAnimation` methods 
with callbacks:

    button.StartLoadingAnimation();
    // do some work on a background thread ...
    DoBackgroundWork(() => {
        // work is done, so start the finishing
        button.StartFinishAnimation(() => {
            // work is done and animation is over
            PerformSegue("loginSegue", this);
        }
    }));

In some cases, a combination can be used:

    button.StartLoadingAnimation();
    // do some work on a background thread ...
    DoBackgroundWork(async () => {
        // work is done, so start the finishing
        await button.StartFinishAnimationAsync();
    
        // work is done and animation is over
        PerformSegue("loginSegue", this);
    });


In most cases, we don't want to permit the user from double-tapping the button.
To prevent this, we can make use of an iOS feature and surround the action with `UserInteractionEnabled`:

    button.UserInteractionEnabled = false;
    await button.AnimateAsync(async () => {
        // the work ...
    });
    button.UserInteractionEnabled = true;

## Using Storyboards

`TransitionSubmitButton` is fully supported in storyboards and the storyboard designer. 
There are several properties that are available in the designer:

  * `NormalBackgroundColor`  
    The background color of the button
    
  * `HighlightedBackgroundColor`  
    The background color of the button when the user is pressing it

## Helper Animation

In addition to the `TransitionSubmitButton`, this library also includes two types that can be used when transitioning 
between `UIViewControllers`. This transition fits in nicely with the appearance and animation of the button.

To use the transition, the simplest way is to set the `TransitioningDelegate` to an instance of 
`FadeInTransitioningDelegate`. This works with storyboards as well as code-behide navigation:

    viewController.TransitioningDelegate = new FadeInTransitioningDelegate(0.5, 0.8f);

In the case where more control might be needed, the `FadeInAnimator` can be used instead. This type is an
instance of `IUIViewControllerAnimatedTransitioning`.
