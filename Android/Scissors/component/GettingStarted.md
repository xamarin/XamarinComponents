
**Scissors** is a fixed viewport image cropping library for Android.

## Usage

To make use of `CropView` in your layout:

    <com.lyft.android.scissors.CropView
        android:id="@+id/crop_view"
        android:layout_width="match_parent"
        android:layout_height="match_parent"
        app:cropviewViewportHeightRatio="1" />

_Use the `cropviewViewportHeightRatio` attribute so set the aspect ratio of the 
height with relation to the width._

Then, set an image to be cropped, using one of the `SetImage` methods:

    // by resource ID
    cropView.SetImageResource (Resource.Drawable.AfricanLion);
    
    // by bitmap
    cropView.SetImageBitmap (someBitmap);

To get the cropped image, use the `Crop` method:

    Bitmap croppedBitmap = cropView.Crop ();

### Extensions

Scissors comes with handy extensions which help with common tasks like:

#### Loading a Bitmap
To load a Bitmap automatically with Square's Picasso library:

    cropView.WithExtensions
        .Load (galleryUri);

#### Cropping into a File
To save a cropped Bitmap into a `File` use as follows:

    var croppedFile = new File (CacheDir, "cropped.jpg");
    await cropView.WithExtensions
        .Crop ()
        .Quality (87)
        .Format (Bitmap.CompressFormat.Jpeg)
        .IntoAsync (croppedFile));
