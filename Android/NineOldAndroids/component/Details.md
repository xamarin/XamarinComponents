
<iframe src="https://appetize.io/embed/fp45tfdndcw7qu5v9gcy5dfux4?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;"></iframe>

Android library for using the Honeycomb (Android 3.0) animation API on all versions
of the platform back to 1.0!

Animation prior to Honeycomb was very limited in what it could accomplish so in 
Android 3.x a new API was written. With only a change in imports, we are able to 
use a large subset of the new-style animation with exactly the same API.

This library also includes support for animating rotation, translation, alpha, 
and scale on platforms prior to Honeycomb!

## Usage

The API is exactly the same as the Honeycomb API, just change your using to be:

    using NineOldAndroids.Animation;

For example, to animate a `View` vertically off of the screen you can use 
an `ObjectAnimator`:

    ObjectAnimator.OfFloat(myObject, "translationY", -myObject.Height).Start();

If you're familiar with the animation API already you should notice that this is 
precisely what would be done with the native API. The only difference, however, 
is that we've used the `NineOldAndroids.Animation` namespace at the top of the file
instead of `Android.Animation`.

Here's another example of a `View` animating its own background to go from red to 
blue and then back again forever:

    var red = new Color(255, 127, 127);
    var blue = new Color(127, 127, 255);
    
    var colorAnim = ObjectAnimator.OfInt(this, "backgroundColor", red, blue);
    colorAnim.SetDuration(3000);
    colorAnim.SetEvaluator(new ArgbEvaluator());
    colorAnim.RepeatCount = ValueAnimator.Infinite;
    colorAnim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
    colorAnim.Start();

This library includes compatibility implementations of almost all of the new 
properties which were introduced with Android 3.0 allowing you to perform very 
complex animations that work on every API level:

    var set = new AnimatorSet();
    set.PlayTogether(
        ObjectAnimator.OfFloat(myView, "rotationX", 0, 360),
        ObjectAnimator.OfFloat(myView, "rotationY", 0, 180),
        ObjectAnimator.OfFloat(myView, "rotation", 0, -90),
        ObjectAnimator.OfFloat(myView, "translationX", 0, 90),
        ObjectAnimator.OfFloat(myView, "translationY", 0, 90),
        ObjectAnimator.OfFloat(myView, "scaleX", 1, 1.5f),
        ObjectAnimator.OfFloat(myView, "scaleY", 1, 0.5f),
        ObjectAnimator.OfFloat(myView, "alpha", 1, 0.25f, 1)
    );
    set.SetDuration(5000);
    set.Start();

You can also use a compatibility version of `ViewPropertyAnimator` which allows 
animating views in a much more declarative manner:

    var myButton = FindViewById<Button>(Resource.Id.myButton);
    ViewPropertyAnimator.Animate(myButton)
                        .SetDuration(2000)
                        .RotationYBy(720)
                        .X(100)
                        .Y(100);

There are some caveats to using this API on versions of Android prior to Honeycomb:
 * The native Property class and its animation was introduced in Android 3.2 and is 
   thus not included in this library.  
   There is, however, a `NineOldAndroids.Util.Property` type which allows you to 
   create custom properties on your own views for easier animation.
 * `LayoutTransition` is not possible to implement prior to Android 3.0 and is not 
   included.
