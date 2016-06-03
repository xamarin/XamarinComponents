
A custom view for presenting a dynamically blurred version of another view's 
content.

## Usage

An instance of `BlurringView` can be added to a layout resource (or through 
code): 

    <Blurring.BlurringView
        android:id="@+id/blurringView"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />

Then, we can specify the view to be blurred:

    blurringView.BlurredView = someView;

## Customizing

To control the amount of blurring to take place, we can adjust the blur radius:

    blurringView.BlurRadius = 10;

To reduce the size of the bitmap that will be blurred, we can adjust the 
downsample factor:

    blurringView.DownsampleFactor = 5;

The higher the downsample factor, the smaller the bitmap. This is useful when
the device does not have large amounts of memory, or if the view being blurred
does not require a high resolution.

To control the color tint of the blurred view, we set the overlay color:

    var translucentRed = Color.Argb(100, 255, 0, 0);
    blurringView.OverlayColor = translucentRed;

In addition to setting the properties in code, we can assign these values 
using XML attributes in the layout resource:

    <Blurring.BlurringView
        xmlns:app="http://schemas.android.com/apk/res-auto" 
        app:blurRadius="10"
        app:overlayColor="#66FF0000"
        app:downsampleFactor="5"
        android:id="@+id/blurringView"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />

## Compatibility

The `BlurringView` is supported down to Android version 2.2 (API level 8) 
through the Android Support Library.

Currently RenderScript is not supported on the `x86_64` and the old `armeabi` 
CPU architecture. In these cases, the app will fall back to a StackBlur 
implementation.

To ensure that RenderScript is used on the new `x86_64` CPU architecture, 
uncheck this architecture from the list of supported architectures. 
This will result in the app using the `x86` architecture instead.
