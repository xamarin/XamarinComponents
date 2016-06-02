
<iframe src="https://appetize.io/embed/5p4nc7mma7tkt5ycc0cbzv2fvg?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

Pull To Bounce can wrap any `UIScrollView` and add a cool, bouncy pull-to-refresh animation. All 
that needs to be done is to add a `UIScrollView` as a sub view, and then subscribe to an event.

##  Using PullToBounceWrapper

`PullToBounceWrapper` can be used in both storyboards and code-behind. Once instantiated, it can
be used to wrap any `UIScrollView`.

    // create the wrapper
    var wrapper = new PullToBounceWrapper(View.Frame);
    View.AddSubview(wrapper);
    
    // add the UIScrollView
    var tableView = new SampleTableView(View.Frame);
    tableView.BackgroundColor = UIColor.Clear;
    wrapper.AddSubview(tableView);
    
    // listen for a refresh
    wrapper.RefreshStarted += async delegate {
        await Task.Delay(2000);
        
        // let the view know we have loaded our content 
        wrapper.StopLoadingAnimation();
    };

## Using Properties

`PullToBounceWrapper` is fully supported in storyboards and in the storyboard designer. 
There are several properties that are available in the designer and in code:

  * `BallColor`  
    The color of the refresh ball
    
  * `WaveColor`  
    The color of the wave animation
    
  * `WaveBounceDuration`  
    The duration of the wave animation
    
  * `BallMoveUpDuration`  
    The duration of the ball movement animation
    
  * `BallMovementTimingFunction`  
    The animation curve of the ball movement

  * `BallSize`  
    The diameter of the refresh ball

  * `PullDistance`  
    The distance the user has to pull before a refresh is triggered

  * `BendDistance`  
    The distance the wave will curve during a pull

    
## Using Events

When we want to listen for a refresh event, we can subscribe to the `RefreshStarted` event:
    
  * `RefreshStarted`  
    The event that is raised when the user triggers a refresh.
    
After the work has been completed, we need to let the `PullToBounceWrapper` know. We do this 
by invoking the `StopLoadingAnimation` method:

    pullToBounceWrapper.StopLoadingAnimation();
