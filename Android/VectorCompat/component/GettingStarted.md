
A support library for [`VectorDrawable`][1] and [`AnimatedVectorDrawable`][2] 
introduced in Lollipop with fully backwards compatible tint support (api 14+ 
so far).

`vector-compat` provides the necessary tools to make animated icons similar to 
the new drawer hamburger icon that morphs to a back arrow when clicked. Any 
other morph animation between icons can be defined _purely in `xml` (**no 
code required**)_ and the library takes care of the transformation animation. 
Because they are in vector format, these drawables can be of any height and 
width with no resulting pixelation.

The library will transparently fall back to the lollipop implementation of 
`VectorDrawable` and `AnimatedVectorDrawable` on api 21+ devices

## Commonly Animations
The library packs some ready-made morph animations developers can use in 
their code with `MorphButton`. More will be added soon as this is a 
work-in-progress. The library has the following morph animations:

 * Play-Pause morph animation (bi-directional morph)
 * Play-Stop morph animation (bi-directional morph)
 * Arrow-Hamburger menu morph animation (bi-directional morph)

## Usage
`VectorDrawable` and `AnimatedVectorDrawable` xml drawable syntax is exactly 
the same as the lollipop documentation (can be seen [here][1] and [here][2] 
respectively). With 2 caveats: 

 * Some attributes under the `<vector>` nodes must be listed once for the 
   `android:` namespace and once for the local namespace with a `vc_` prefix 
   (e.g. `app:vc_fillColor`). (For a complete list of `vc_` prefixed attributes 
   see [attr.xml][6])  
   See example [here][4]. 
 * Any `pathType` anim xml must have the `android:valueType="pathType"` in 
   addition to `app:vc_valueType="pathType"` to allow for lollipop implementation 
   fallback.  
   See example [here][5].


### Inflation
`VectorDrawable` and `AnimatedVectorDrawable` in this support library can be 
inflated in one of 2 ways:

#### Static `GetDrawable()` Methods

    // This will only inflate a drawable with <vector> as the root element
    VectorDrawable.GetDrawable(context, Resource.Drawable.ic_arrow_vector);
    
    // This will only inflate a drawable with <animated-vector> as the root element
    AnimatedVectorDrawable.GetDrawable(context, Resource.Drawable.ic_arrow_to_menu_animated_vector);
    
    // This will inflate any drawable and will auto-fallback to the lollipop implementation on api 21+ devices
    ResourcesCompat.getDrawable(context, Resource.Drawable.any_drawable);

_If inflating the `Drawable` in code, it is recommended to always use 
`ResourcesCompat.GetDrawable()` as this handles Lollipop fallback when 
applicable. This allows the system to cache `Drawable ConstantState` and hence 
is more efficient._

#### Directly From `MorphButton` XML

    <com.wnafee.vector.MorphButton
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/playPauseBtn"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        app:vc_startDrawable="@drawable/ic_pause_to_play"
        app:vc_endDrawable="@drawable/ic_play_to_pause" /> 

### MorphButton
A `MorphButton` is a `CompoundButton` with 2 states: `MorphState.Start` or 
`MorphState.End`. The attributes `vc_startDrawable` and `vc_endDrawable` define 
which foreground drawables to use for the button depending on the button's state. 

These can be any type of drawable (e.g. `BitmapDrawable`, `ColorDrawable`, 
`VectorDrawable`, `AnimatedVectorDrawable` etc.)

To use MorphButton in your app, make sure to include the `morphButtonStyle` item 
in your base app theme:

    <style name="MyAppTheme" parent="Theme.AppCompat.Light.DarkActionBar">
        <item name="morphButtonStyle">@style/Widget.MorphButton</item>
    </style>


`MorphButtons` allow you to tint your foreground drawables (i.e. 
`vc_startDrawable` and `vc_endDrawable`) and background drawable separately 
in both xml and java. See the following examples for defining `MorphButtons`:

#### XML

    <com.wnafee.vector.MorphButton
        xmlns:app="http://schemas.android.com/apk/res-auto"
        android:id="@+id/drawerBtn"
        android:layout_width="wrap_content"
        android:layout_height="wrap_content"
        android:scaleType="fitCenter"
        app:vc_backgroundTint="#f50057"
        app:vc_foregroundTint="#3F51B5"
        app:vc_startDrawable="@drawable/ic_arrow_to_drawer"
        app:vc_endDrawable="@drawable/ic_drawer_to_arrow" />


#### Code

    MorphButton mb = new MorphButton(this);
    mb.BackgroundTintList = Resources.GetColorStateList(Resource.Color.background_tint_color);
    mb.ForegroundTintList = ColorStateList.ValueOf(Color.Red);
    mb.SetStartDrawable(Resource.Drawable.ic_pause_to_play);
    mb.SetEndDrawable(Resource.Drawable.ic_play_to_pause);
    mb.State = MorphButton.MorphState.End;

The `scaleType` attribute defines how to scale the foreground drawable to fill 
the button's background. This is the same as [`ImageView.ScaleType`][7] which 
you can take a look at [here][7].

Button clicks will toggle between the foreground drawables. If the drawables 
happen to implement the [`IAnimatable`][3] interface (e.g. `AnimatedVectorDrawable` 
or `AnimationDrawable`) then `Start()` will be automatically called to animate 
between the start and end drawables defined in xml.
 
MorphButton states can be set manually, using eitehr the `State` property or
`SetState` method:

    // transition with no animation
    morphButton.State = MorphButton.MorphState.End; 
    
    // or transition with animation if drawable is IAnimatable
    morphButton.SetState (MorphButton.MorphState.Start, true); 

If you need to be informed of button state changes, you can subscribe to the 
`StateChanged` event:

    morphButton.StateChanged += (sender, e) => {
        // changeTo is the new state
        MorphButton.MorphState changedTo = e.ChangedTo;

        // isAnimating = true if the state changed with animation
        bool isAnimating = e.IsAnimating;
    };

[1]: http://developer.android.com/reference/android/graphics/drawable/VectorDrawable.html
[2]: http://developer.android.com/reference/android/graphics/drawable/AnimatedVectorDrawable.html
[3]: http://developer.android.com/reference/android/graphics/drawable/Animatable.html
[4]: https://github.com/wnafee/vector-compat/blob/master/library/src/main/res/drawable/ic_arrow_vector.xml
[5]: https://github.com/wnafee/vector-compat/blob/master/library/src/main/res/anim/arrow_to_drawer_path.xml
[6]: https://github.com/wnafee/vector-compat/blob/master/library/src/main/res/values/attr.xml
[7]: http://developer.android.com/reference/android/widget/ImageView.ScaleType.html
