
**FXBlurView** is a UIView subclass that replicates the iOS 7 realtime background blur effect, 
but works on iOS 5 and above. It is designed to be as fast and as simple to use as possible. 

FXBlurView offers two modes of operation: static, where the view is rendered only once when 
it is added to a superview (though it can be updated by calling `SetNeedsDisplay()` or 
`UpdateAsynchronously()`) or dynamic, where it will automatically redraw itself on a background 
thread as often as possible.

## FXBlurView Methods

    static void SetBlurEnabled (bool blurEnabled);
      
This method can be used to globally enable/disable the blur effect on all FXBlurView 
instances. This is useful for testing, or if you wish to disable blurring on iPhone 4 
and below (for consistency with iOS7 blur view behavior). *By default blurring is enabled.*

    static void SetUpdatesEnabled ();
    static void SetUpdatesDisabled ();
    
These methods can be used to enable and disable updates for all dynamic FXBlurView instances 
with a single command. Useful for disabling updates immediately before performing an animation 
so that the FXBlurView updates don't cause the animation to stutter. Calls can be nested, but 
ensure that the enabled/disabled calls are balanced, or the updates will be left permanently 
enabled or disabled.

    void UpdateAsynchronously (bool async, Action completion);

This method can be used to trigger an update of the blur effect (useful when 
`Dynamic = false`): 
 * The async argument controls whether the blur will be redrawn on the 
   main thread or in the background. 
 * The completion argument is an optional callback block that will be 
   called when the blur is completed.


    void SetNeedsDisplay ();

Inherited from `UIView`, this method can be used to trigger a (synchronous) update of the view. 
Calling this method is more-or-less equivalent to calling:

    view.UpdateAsynchronously (false, null);


## FXBlurView Properties

    bool BlurEnabled { get; set; }

This property toggles blurring on and off for an individual FXBlurView instance. Blurring is 
enabled by default. 

If you disable blurring using the static `SetBlurEnabled` method 
then that will override this setting.

    bool Dynamic { get; set; }
	
This property controls whether the FXBlurView updates dynamically, or only once when the view 
is added to its superview. Defaults to `true`. 

If `Dynamic` is set to `false`, you can still force the view to update by calling 
`SetNeedsDisplay()` or `UpdateAsynchronously()`.
 
Dynamic blurring is extremely cpu-intensive, so you should always disable dynamic views 
immediately prior to performing an animation to avoid stuttering. However, if you have multiple 
FXBlurViews on screen then it is simpler to disable updates using the `SetUpdatesDisabled()` method 
rather than setting the `Dynamic` property to `false`.

    nuint Iterations { get; set; }

The number of blur iterations. More iterations improves the quality but reduces the performance. 
Defaults to 2 iterations.

    double UpdateInterval { get; set; }
    
This controls the interval (in seconds) between successive updates when the FXBlurView is 
operating in dynamic mode. This defaults to zero, which means that the FXBlurView will update 
as fast as possible. 

This yields the best frame rate, but is also extremely CPU 
intensive and may cause the rest of your app's performance to degrade, especially on older 
devices. To alleviate this, try increasing the `UpdateInterval` value.

    nfloat BlurRadius { get; set; }	

This property controls the radius of the blur effect (in points). Defaults to a 40 point radius, 
which is similar to the iOS 7 blur effect.

    UIColor TintColor { get; set; }
    
This in an optional tint color to be applied to the FXBlurView. The RGB components of the color 
will be blended with the blurred image, resulting in a gentle tint. To vary the intensity of 
the tint effect, use brighter or darker colors. The alpha component of the `TintColor` is ignored. 

If you do not wish to apply a tint, set this value to `null` or `UIColor.Clear`. 

    UIView UnderlyingView { get; set; }

This property specifies the view that the FXBlurView will sample to create the blur effect. 
If set to `null` (the default), this will be the superview of the blur view itself, 
but you can override this if you need to.

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
