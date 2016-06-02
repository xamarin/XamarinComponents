Adds methods to `UIImageView` supporting asynchronous web image loading:

```csharp
using SDWebImage;
...

const string CellIdentifier = "Cell";

public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
{
	UITableViewCell cell = tableView.DequeueReusableCell (CellIdentifier) ??
		new UITableViewCell (UITableViewCellStyle.Default, CellIdentifier);
	
	// Use the SetImage extension method to load the web image:
	cell.ImageView.SetImage (
		url: new NSUrl ("http://db.tt/ayAqtbFy"), 
		placeholder: UIImage.FromBundle ("placeholder.png")
	);

	return cell;
}
```

It provides:

* `UIImageView` and `UIButton` extension methods adding web image loading and cache management.
* An asynchronous image downloader
* An asynchronous memory + disk image caching with automatic cache expiration handling
* Animated GIF support
* WebP format support
* A background image decompression
* A guarantee that the same URL won't be downloaded several times
* A guarantee that bogus URLs won't be retried again and again
* A guarantee that main thread will never be blocked
* Performances!
* Use GCD and ARC
* Arm64 support

## iOS 9 Support

Since iOS 9.0, Apple introduced a new security feature called App Transport Security.
App Transport Security (ATS) enforces best practices in the secure connections between an app and 
its back end. ATS prevents accidental disclosure, provides secure default behavior, and is easy 
to adopt; it is also on by default in iOS 9 and OS X v10.11. You should adopt ATS as soon as 
possible, regardless of whether you’re creating a new app or updating an existing one.

If you’re developing a new app, you should use HTTPS exclusively. If you have an existing app, 
you should use HTTPS as much as you can right now, and create a plan for migrating the rest of 
your app as soon as possible. In addition, your communication through higher-level APIs needs 
to be encrypted using TLS version 1.2 with forward secrecy. 

If you try to make a connection that doesn't follow this requirement, an error is thrown. 
If your app needs to make a request to an insecure domain, you have to specify this domain 
in your app's `Info.plist` file. 

For example, if you are loading images from `http://example.org`, you will need to add that domain
in the `Info.plist` file:

    <key>NSAppTransportSecurity</key>
	<dict>
        <key>NSExceptionDomains</key>
		<dict>
		    <key>example.org</key>
			<dict>
			    <key>NSIncludesSubdomains</key> <false/>
			    <key>NSTemporaryExceptionAllowsInsecureHTTPLoads</key> <true/>
			</dict>
		</dict>
	</dict>

However, in the case where you need to access arbitrary domains, you can allow all requests 
to go through. However, this is not advised if using HTTPS is possible:

    <key>NSAppTransportSecurity</key>  
    <dict>
        <key>NSAllowsArbitraryLoads</key> <true/> 
    </dict>


## Callbacks

With callbacks, you can be notified about the image download progress
and whenever the image retrieval has completed:

```csharp
cell.ImageView.SetImage (
	url: new NSUrl ("http://db.tt/ayAqtbFy"), 
	placeholder: UIImage.FromBundle ("placeholder.png"),
	completedHandler: (image, error, cacheType) => {
		// Handle download completed...
	}
);
```

Callbacks are not called if the request is canceled.

## Using SDWebImageManager Independently

The SDWebImageManager is the class behind the UIImageView extension
methods. It owns the asynchronous downloader and the image cache.  You
can reuse this class directly for cached web image downloading in other
contexts.

```csharp
SDWebImageManager.SharedManager.Download (
	url: new NSUrl ("http://db.tt/ayAqtbFy"), 
	options: SDWebImageOptions.CacheMemoryOnly,
	progressHandler: (recievedSize, expectedSize) => {
		// Track progress...
	},
	completedHandler: (image, error, cacheType, finished) => {
		if (image != null) {
			// do something with the image
		}
	}
);
```

## Using SDWebImageDownloader Independently

It's also possible to use the asynchronous image downloader independently:

```csharp
SDWebImageDownloader.SharedDownloader.DownloadImage (
	url: new NSUrl ("http://db.tt/ayAqtbFy"),
	options: SDWebImageDownloaderOptions.LowPriority,
	progressHandler: (receivedSize, expectedSize) => {
		// Track progress...
	},
	completedHandler: (image, data, error, finished) => {
		if (image != null && finished) {
			// do something with the image
		}
	}
);
```

## Using SDImageCache Independently

You may also use the image cache independently. SDImageCache maintains a
memory cache and an optional disk cache. Disk writes are performed
asynchronously as well.

The SDImageCache class provides a singleton instance for convenience but
you can create your own instance if you want to create Independent
caches.

```csharp
var myCache = new SDImageCache ("MyUniqueCacheKey");

myCache.QueryDiskCache ("UniqueImageKey", image => {
	// If image is not null, image was found
	if (image != null) {
		// Do something with the image
	}
 });
```

By default SDImageCache will lookup the disk cache if an image can't be
found in the memory cache. You can prevent this from happening by
calling the alternative method `ImageFromMemoryCache`.

To store an image into the cache, you use the `StoreImage` method:

```csharp
SDImageCache.SharedImageCache.StoreImage (image: myImage, key: "myKey");
```

By default, the image will be stored in memory cache as well as on disk
cache. If you want only the memory cache use:


```csharp
SDImageCache.SharedImageCache.StoreImage (image: myImage, key: "myKey", toDisk: false);
```

## Using cache key filter

Sometimes, you may not want to use image URLs as cache keys because part
of the URL is unstable.  SDWebImageManager provides a way to set a cache
key filter that maps NSUrls to cache key strings.

The following example sets a filter in the application delegate that
removes query parameters from the URL before querying the cache:
key:

```csharp
using SDWebImage;
...

public override bool FinishedLaunching (UIApplication app, NSDictionary options)
{
	SDWebImageManager.SharedManager.SetCacheKeyFilter (url => {
		var stableUrl = new NSUrl (scheme: url.Scheme, host: url.Host, path: url.Path);  
		return stableUrl.AbsoluteString;
	});
	...
}
```

## Handle image refresh

`SDWebImage` does very aggressive caching by default; it ignores any
caching control headers returned by the HTTP server, and caches images
with no time restrictions. This implies that your images change only if
their URLs change.

If you don't control the image server, you may not be able
to change the URL when an image changes--this is the case with
Facebook profile URLs, for example. In this case, you may use the
`SDWebImageOptions.RefreshCached` flag, which causes the cache to
respect HTTP caching control headers:

```csharp
var imageView = new UIImageView ();
imageView.SetImage (
	url: new NSUrl ("http://db.tt/ayAqtbFy"), 
	placeholder: UIImage.FromBundle ("yourPlaceholder.png"),
	options: SDWebImageOptions.RefreshCached
);
```
