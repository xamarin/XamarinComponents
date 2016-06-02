
**MWPhotoBrowser** can display one or more images or videos by providing either 
`UIImage` objects, `PHAsset` objects, or URLs to library assets, web images/videos 
or local files. The photo browser handles the downloading and caching of photos from 
the web seamlessly. Photos can be zoomed and panned, and optional (customisable) 
captions can be displayed.

The browser can also be used to allow the user to select one or more photos using either 
the grid or main image view.

## Usage

`PhotoBrowser` is designed to be presented within a navigation controller. Simply set the 
delegate (which must conform to `IPhotoBrowserDelegate`) and implement the 2 required 
delegate methods to provide the photo browser with the data in the form of 
`IPhoto` objects. You can create an `IPhoto` object through the `PhotoBrowserPhoto` type
by providing a `UIImage` object, `PHAsset` object, or a URL containing the path to a file, 
an image online or an asset from the asset library.

`PhotoBrowserPhoto` objects handle caching, file management, downloading of web images, 
and various optimisations for you. If however you would like to use your own data model 
to represent photos you can simply ensure your model conforms to the `IPhoto` interface. 
You can then handle the management of caching, downloads, etc, yourself.

    // Create browser delegate
    class BrowserDelegate : PhotoBrowserDelegate {
        private IPhoto[] photos;

        public BrowserDelegate() {
            // Local photo
            var resource = PhotoBrowserPhoto.FromFilePath(NSBundle.MainBundle.PathForResource("photo", "jpg"));
            
            // Remote photo
            var photo = PhotoBrowserPhoto.FromUrl(new NSUrl("http://example.org/photo.jpg"));
            
            // Video with thumbnail photo
            var video = PhotoBrowserPhoto.FromUrl(new NSUrl("http://example.org/preview.jpg"));
            video.VideoUrl = new NSUrl("http://example.org/video.mp4");
            
            // Create array of IPhoto objects
            photos = new IPhoto[] { resource, photo, video };
        }

        public override nuint GetPhotoCount(PhotoBrowser photoBrowser) {
            return (nuint)photos.Length;
        }

        public override IPhoto GetPhoto(PhotoBrowser photoBrowser, nuint index) {
            return photos[(int)index];
        }
    }

    // Create browser (can't be re-used)
    var browser = new PhotoBrowser(new BrowserDelegate());
    browser.DisplayNavArrows = true;
    browser.ZoomPhotosToFill = true;
    browser.EnableGrid = true;
    
    // Present
    NavigationController.PushViewController(browser, true);

    // Manipulate
    browser.ShowNextPhoto(true);
    browser.ShowPreviousPhoto(true);
    browser.CurrentIndex = 2;
