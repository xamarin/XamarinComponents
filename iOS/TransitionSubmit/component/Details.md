
<iframe src="https://appetize.io/embed/yua2g7y61py7m3d134uzyve3z4?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

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
    
If the work is not `await`-able, we can use the `StartLoadingAnimation` and `StartFinishAnimation` methods with callbacks:
    
    button.StartLoadingAnimation();
    // do some work on a background thread ...
    DoBackgroundWork(() => {
        // work is done, so start the finishing
        button.StartFinishAnimation(() => {
            // work is done and animation is over
            PerformSegue("loginSegue", this);
        }
    }));

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
