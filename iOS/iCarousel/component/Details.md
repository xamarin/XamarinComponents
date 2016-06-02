iCarousel is a class designed to simplify the implementation of various types of carousel (paged, scrolling views) on iPhone, iPad and Mac OS. 

iCarousel implements a number of common effects such as cylindrical, flat and "CoverFlow" style carousels, as well as providing hooks to implement your own bespoke effects. Unlike many other "CoverFlow" libraries, iCarousel can work with any kind of view, not just images, so it is ideal for presenting paged data in a fluid and impressive way in your app. It also makes it extremely easy to swap between different carousel effects with minimal code changes.

Here's an example:

```csharp
using Carousels;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

    // create and add the Carousel to the view
    carousel = new iCarousel ();
    carousel.Type = iCarouselType.CoverFlow2;
    carousel.DataSource = new CarouselDataSource ();
    carousel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
    View.AddSubview (carousel);
    
    // handle item selections / taps
    carousel.ItemSelected += (sender, args) => {
        var indexSelected = args.Index;
        // do something with a selection
    };
}

// a data source that displays 100 items
private class CarouselDataSource : iCarouselDataSource
{
	int[] items;
	
	public CarouselDataSource()
	{
		// create our amazing data source
		items = Enumerable.Range (0, 100).ToArray ();
	}

    // let the carousel know how many items to render
	public override nint GetNumberOfItems (iCarousel carousel)
	{
		// return the number of items in the data
		return items.Length;
	}

    // create the view each item in the carousel
	public override UIView GetViewForItem (iCarousel carousel, nint index, UIView view)
	{
		UILabel label = null;
		UIImageView imageView = null;

		if (view == null)
		{
			// create new view if no view is available for recycling
			imageView = new UIImageView(new CGRect(0, 0, 200.0f, 200.0f));
			imageView.Image = UIImage.FromBundle("page.png");
			imageView.ContentMode = UIViewContentMode.Center;

			label = new UILabel(imageView.Bounds);
			label.BackgroundColor = UIColor.Clear;
			label.TextAlignment = UITextAlignment.Center;
			label.Font = label.Font.WithSize(50);
			label.Tag = 1;
			imageView.AddSubview(label);
		}
		else
		{
			// get a reference to the label in the recycled view
			imageView = (UIImageView)view;
			label = (UILabel)view.ViewWithTag(1);
		}

		// set the values of the view
		label.Text = items [index].ToString ();

		return imageView;
	}
}

```

