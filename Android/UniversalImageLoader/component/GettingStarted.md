
The Universal Image Loader aims to provide a powerful, flexible and 
highly customizable instrument for image loading, caching and displaying.  
It provides a lot of configuration options and good control over the image 
loading and caching process.

## Using

### Configuration

Include following permission if you load images from Internet:

    [assembly: UsesPermission(Android.Manifest.Permission.Internet)]
    
Include following permission if you want to cache images on SD card:

    [assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
    
Create the configuration once in an `Application`, or an `Activity` before using the library:

    [Application]
    public class UILApplication : Application
    {
        protected UILApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            // Use default options
            var config = ImageLoaderConfiguration.CreateDefault(ApplicationContext);
            // Initialize ImageLoader with configuration.
            ImageLoader.Instance.Init(config);
        }
    }    

### Simple

We can load an image into any `ImageView`:

    // Get singleton instance
    ImageLoader imageLoader = ImageLoader.Instance; 
    
    // Load image
    imageLoader.DisplayImage(imageUri, imageView);
    
We can also load an image, without displaying it:
    
    // Load image and return it in callback
    imageLoader.LoadImage(
        imageUri, 
        new ImageLoadingListener(
            loadingComplete: (imageUri, view, loadedImage) => {
                // Do whatever you want with Bitmap
            }));
        
If we are already on another thread, we can load an image on that thread:

    // Load image synchronously
    Bitmap bmp = imageLoader.LoadImageSync(imageUri);

### Complete

We can load an image and then decode it to `Bitmap`. This bitmap can be 
displayed in any `ImageView`, or any other view which implements the
`IImageAware` interface:

    imageLoader.DisplayImage(
        imageUri,
        imageView, 
        options, 
        new ImageLoadingListener(
            loadingStarted: (imageUri, view) => {
            },
            loadingComplete: (imageUri, view, loadedImage) => {
            },
            loadingFailed: (imageUri, view, failReason) => {
            },
            loadingCancelled: (imageUri, view) => {
            }),
        new ImageLoadingProgressListener(
            progressUpdate: (imageUri, view, current, total) => {
            }));

The final size of the loaded image can be controlled:

    ImageSize targetSize = new ImageSize(80, 50); 
    imageLoader.LoadImage(
        imageUri, 
        targetSize,
        options, 
        new ImageLoadingListener(
            loadingComplete: delegate {
                Bitmap loadedImage = e.LoadedImage;
                // Do whatever you want with Bitmap (80x50)
            }));

### Acceptable URIs examples

    "http://site.com/image.png" // from Web
    "file:///mnt/sdcard/image.png" // from SD card
    "file:///mnt/sdcard/video.mp4" // from SD card (video thumbnail)
    "content://media/external/images/media/13" // from content provider
    "content://media/external/video/media/13" // from content provider (video thumbnail)
    "assets://image.png" // from assets
    "drawable://" + R.drawable.img // from drawables (non-9patch images)

NOTE: Use `drawable://` only if you really need it! Always consider the native way to load drawables:

    ImageView.SetImageResource(...)

## Features
 * Multithread image loading *(async or sync)*
 * Wide customization of ImageLoader's configuration 
   *(thread executors, downloader, decoder, memory and disk cache, 
   display image options, etc.)*
 * Many customization options for every display image call 
   *(stub images, caching switch, decoding options, Bitmap processing and 
   displaying, etc.)*
 * Image caching in memory and/or on disk *(device's file system or SD card)*
 * Listening loading process *(including downloading progress)*
 * Android 2.0+ support
 
More information can be found on the wiki: 
https://github.com/nostra13/Android-Universal-Image-Loader/wiki
