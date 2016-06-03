
<iframe src="https://appetize.io/embed/epgjudw76k84fnw19nb1bxqzrc?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

**FXBlurView** is a UIView subclass that replicates the iOS 7 realtime background blur effect, 
but works on iOS 5 and above. It is designed to be as fast and as simple to use as possible. 

FXBlurView offers two modes of operation: static, where the view is rendered only once when 
it is added to a superview (though it can be updated by calling `SetNeedsDisplay()` or 
`UpdateAsynchronously()`) or dynamic, where it will automatically redraw itself on a background 
thread as often as possible.

## FXBlurView Members

 * `static` **SetBlurEnabled()**  
   Used to globally enable/disable the blur effect on all FXBlurView instances.
 * `static` **SetUpdatesEnabled()** & **SetUpdatesDisabled()**  
   Used to enable and disable updates for all dynamic FXBlurView instances with a single command.
 * **UpdateAsynchronously()**  
   Used to trigger an update of the blur effect.
 * **SetNeedsDisplay()**  
   Used to trigger a (synchronous) update of the view. 
 * **BlurEnabled**  
   Toggles blurring on and off for an individual FXBlurView instance.
 * **Dynamic**  
   Controls whether the FXBlurView updates dynamically, or only once when the view 
   is added to its superview. 
 * **Iterations**  
   Represents the number of blur iterations.
 * **UpdateInterval**  
   Controls the interval (in seconds) between successive updates when the FXBlurView is 
   operating in dynamic mode.
 * **BlurRadius**  	
   Controls the radius of the blur effect (in points).
 * **TintColor**  
   An optional RGB tint color to be applied to the FXBlurView.
 * **UnderlyingView**  
   Specifies the view that the FXBlurView will sample to create the blur effect.

## UIImage Extensions

FXBlurView extends UIImage with the following method:

    UIImage CreateBlurredImage (
        nfloat radius, 
        nuint iterations, 
        UIColor tintColor);

This method applies a blur effect and returns the resultant blurred image without modifying 
the original: 

 * The radius property controls the extent of the blur effect. 
 * The iterations property controls the number of iterations.   
   More iterations means higher quality. 
 * The tintColor is an optional color that will be blended with the resultant image.  
   *Note that the alpha component of the tintColor is ignored.*
