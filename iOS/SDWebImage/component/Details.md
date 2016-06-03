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

