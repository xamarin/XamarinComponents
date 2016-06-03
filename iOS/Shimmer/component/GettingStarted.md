
`ShimmeringView` is a container `UIView` that will add a shmmering effect to any view that it
contains. It's useful as an unobtrusive loading indicator.

##  Using ShimmeringView

The easiest way to make use of `ShimmeringView` is by simply adding it to your view 
hierachy and adding a sub view to shimmer:

    // create the shimmering view
    shimmeringView = new ShimmeringView(View.Bounds);
    View.AddSubview(shimmeringView);
    
    // create the view to shimmer
    logoLabel = new UILabel(View.Bounds);
    logoLabel.Text = "Shimmer";
    logoLabel.TextColor = UIColor.White;
    logoLabel.Font = UIFont.FromName("HelveticaNeue-UltraLight", 60.0f);
    
    // add the view to shimmering view
    shimmeringView.AddSubview(logoLabel);
    
    // start the shimmering
    shimmeringView.Shimmering = true;

## Using Storyboards

`ShimmeringView` is fully supported in storyboards and the storyboard designer. 
There are several properties that are available in the designer:

  * `Shimmering`  
    Whether or not the view is shimmering
    
  * `ShimmeringPauseDuration`  
    The time interval between shimmerings in seconds
    
  * `ShimmeringAnimationOpacity`  
    The opacity of the content while it is shimmering
    
  * `ShimmeringOpacity`  
    The opacity of the content before it is shimmering
    
  * `ShimmeringSpeed`  
    The speed of shimmering
    
  * `ShimmeringHighlightLength`  
    The length of the shimmering highlight
    
  * `ShimmeringDirection`  
    The direction of the shimmering animation
    
  * `ShimmeringBeginFadeDuration`  
    The duration of the fade used when shimmer begins
    
  * `ShimmeringEndFadeDuration`  
    The duration of the fade used when shimmer ends
    
  * `ShimmeringFadeTime`  
    The absolute CoreAnimation media time when the shimmer will fade in
    
  * `ShimmeringBeginTime`  
    The absolute CoreAnimation media time when the shimmer will begin
