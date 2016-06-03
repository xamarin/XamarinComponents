UrlImageViewHelper will automatically download and manage all the web images and ImageViews. Duplicate urls will not be loaded into memory twice. Bitmap memory is managed by using a weak reference hash table, so as soon as the image is no longer used by you, it will be garbage collected automatically.

**Usage is simple:**

```csharp
Koush.UrlImageViewHelper.SetUrlDrawable (imageView, "http://example.com/image.png");
```

Want a placeholder image while it is being downloaded?

```csharp
Koush.UrlImageViewHelper.SetUrlDrawable (imageView, "http://example.com/image.png", Resource.Drawable.placeholder);
```

Don't want to use a placeholder resource, but a drawable instead?

```csharp
Koush.UrlImageViewHelper.SetUrlDrawable (imageView, "http://example.com/image.png", drawable);
```

What if you want to preload images for snazzy fast loading?

```csharp
Koush.UrlImageViewHelper.LoadUrlDrawable (context, "http://example.com/image.png");
```

What if you only want to cache the images for a minute?

```csharp
// Note that the 3rd argument "null" is an optional interstitial placeholder image.
Koush.UrlImageViewHelper.SetUrlDrawable (imageView, "http://example.com/image.png", null, 60000);
```

## Learn More
Learn more about [UrlImageViewHelper](https://github.com/koush/UrlImageViewHelper).

