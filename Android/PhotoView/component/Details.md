
`PhotoView` aims to help produce an easily usable implementation of a zooming 
Android `ImageView`.

## Features

- Out of the box zooming, using multi-touch and double-tap.
- Scrolling, with smooth scrolling fling.
- Works perfectly when using used in a scrolling parent (such as `ViewPager`).
- Allows the application to be notified when the displayed Matrix has changed. 
  Useful for when you need to update your UI based on the current zoom/scroll position.
- Allows the application to be notified when the user taps on the Photo.

## Usage

The `PhotoViewAttacher` type provides an easy means to attach the features of 
`PhotoView` to any `ImageView` instance:

	// any implementation of ImageView can be used!
	var image = FindViewById<ImageView>(Resource.Id.image);
	
	// set the Drawable displayed
	var bitmap = BitmapFactory.DecodeResource(
        Resources, Resource.Drawable.wallpaper);
    image.SetImageBitmap(bitmap);

	// attach a PhotoViewAttacher, which takes care 
    // of all of the zooming functionality.
	var attacher = new PhotoViewAttacher(image);

    // . . . 

    // update the image
    image.SetImageBitmap(newBitmap);

    // update the attachment
    attacher.Update();

In addition to using the `PhotoViewAttacher` type, we can use the `PhotoView`
type directly:

    var photo = new PhotoView(this);
    photo.SetImageBitmap(newBitmap);
    
And the `PhotoView` type can also be created in layout resources:

    <uk.co.senab.photoview.PhotoView
        android:id="@+id/imageView"
        android:layout_width="fill_parent"
        android:layout_height="fill_parent" />
