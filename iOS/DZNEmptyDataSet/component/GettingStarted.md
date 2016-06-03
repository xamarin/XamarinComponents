
**DZNEmptyDataSet** is a drop-in `UITableView`/`UICollectionView` superclass category for 
showing empty datasets whenever the view has no content to display.

It will work automatically, by just conforming to `EmptyDataSetSource`, and returning the 
data you want to show. The `ReloadData ()` call will be observed so the empty dataset will 
be configured whenever needed. 

It is (extremely) important to set the data source and delegate to `null`, whenever the view 
is going to be released. This class uses KVO under the hood, so it needs to remove the observer 
before dealocating the view.

## The Empty Data Set Pattern

Also known as *[Empty State](http://emptystat.es/)* or 
*[Blank Slate](http://patternry.com/p=blank-slate/)*.

Most applications show lists of content (data sets), which many turn out to be empty at 
one point, specially for new users with blank accounts. Empty screens create confusion 
by not being clear about what's going on, if there is an error/bug or if the user is 
supposed to do something within your app to be able to consume the content.

Please read this very interesting article about [*Designing For The Empty States*](http://tympanus.net/codrops/2013/01/09/designing-for-the-empty-states/).

**[Empty Data Sets](http://pttrns.com/?did=1&scid=30)** are helpful for:
* Avoiding white-screens and communicating to your users why the screen is empty.
* Calling to action (particularly as an onboarding process).
* Avoiding other interruptive mechanisms like showing error alerts.
* Beeing consistent and improving the user experience.
* Delivering a brand presence.


## Features
* Compatible with `UITableView` and `UICollectionView`. Also compatible with 
  `UISearchDisplayController` and `UIScrollView`.
* Gives multiple possibilities of layout and appearance, by showing an image 
  and/or title label and/or description label and/or button.
* Uses `NSAttributedString` for easier appearance customisation.
* Uses auto-layout to automagically center the content to the tableview, with 
  auto-rotation support. Also accepts custom vertical and horizontal alignment.
* Background color customisation.
* Allows tap gesture on the whole tableview rectangle (useful for resigning first 
  responder or similar actions).
* For more advanced customisation, it allows a custom view.
* Compatible with Storyboard.
* Compatible with iOS 6 or later.
* Compatible with iPhone and iPad.
* **App Store ready**

This library has been designed in a way where you won't need to extend `UITableView`
or `UICollectionView` class. It will still work when using `UITableViewController` 
or `UICollectionViewController`.

By just conforming to `EmptyDataSetSource` & `EmptyDataSetDelegate`, you will be able to 
fully customize the content and appearance of the empty states for your application.

## Using

Whether using the interfaces, or inheriting from the concrete types, we assign the 
`EmptyDataSetSource` and `EmptyDataSetDelegate` using the `SetEmptyDataSetSource` and 
`SetEmptyDataSetDelegate` method respectively:

    public override void ViewDidLoad()
    {
        emptyDataSetSource = new MyEmptyDataSetSource();
        emptyDataSetDelegate = new MyEmptyDataSetDelegate();
        
        TableView.SetEmptyDataSetSource(emptyDataSetSource);
        TableView.SetEmptyDataSetDelegate(emptyDataSetDelegate);
        
        // A little trick for removing the cell separators
        TableView.TableFooterView = new UIView();
    }


### Data Source Implementation

Return the content you want to show on the empty state, and take advantage of 
`NSAttributedString` features to customise the text appearance. This can be done 
for the title:

    public override NSAttributedString GetTitle (UIScrollView scrollView)
    {
        var text = "No Colors Loaded";
        
        var attributes = new UIStringAttributes {
            Font = UIFont.BoldSystemFontOfSize (17.0f),
            ForegroundColor = UIColor.FromRGB (170, 171, 179)
        };
        
        return new NSAttributedString (text, attributes);
    }

And similarly for the description:

    public override NSAttributedString GetDescription (UIScrollView scrollView)
    {
        var text = "To show a list of random colors, tap on the refresh icon in the right top corner.\n\nTo clean the list, tap on the trash icon.";
    
        var attributes = new UIStringAttributes {
            Font = UIFont.BoldSystemFontOfSize (15.0f),
            ForegroundColor = UIColor.FromRGB (170, 171, 179),
            ParagraphStyle = new NSMutableParagraphStyle {
                LineBreakMode = UILineBreakMode.WordWrap,
                Alignment = UITextAlignment.Center
            }
        };
    
        return new NSAttributedString (text, attributes);
    }

The image for the empty state:

    public override UIImage GetImage (UIScrollView scrollView)
    {
        return UIImage.FromBundle ("empty_placeholder.png");
    }

The image view animation:

    public override CAAnimation GetImageAnimation (UIScrollView scrollView)
    {
        var animation = new CABasicAnimation ("transform");

        animation.From = NSValue.FromCATransform3D (CATransform3D.Identity);
        animation.To = NSValue.FromCATransform3D (CATransform3D.MakeRotation ((nfloat)Math.PI / 2f, 0.0f, 0.0f, 1.0f));

        animation.Duration = 0.25;
        animation.Cumulative = true;
        animation.RepeatCount = float.MaxValue;

        return animation;
    }

The attributed string to be used for the specified button state:

    public override NSAttributedString GetButtonTitle (UIScrollView scrollView, UIControlState state)
    {
        var text = "Add Another Color";
        var font = UIFont.SystemFontOfSize (16);
        var textColor = state == UIControlState.Normal 
            ? UIColor.FromRGB (0, 122, 255) 
            : UIColor.FromRGB (198, 222, 249);
        
        return new NSAttributedString (text, font, textColor);
    }

or the image to be used for the specified button state:

    public override UIImage GetButtonImage (UIScrollView scrollView, UIControlState state)
    {
        return UIImage.FromBundle ("button_image.png");
    }

The background color for the empty state:

    public override UIColor GetBackgroundColor (UIScrollView scrollView)
    {
        return UIColor.White;
    }

If you need a more complex layout, you can return a custom view instead:

    public override UIView GetCustomView (UIScrollView scrollView)
    {
        var activityView = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
        activityView.StartAnimating ();
        return activityView;
    }

Additionally, you can also adjust the vertical alignment of the content view 
(ie: useful when there is TableHeaderView visible):

    public override nfloat GetVerticalOffset (UIScrollView scrollView)
    {
        return -TableView.TableHeaderView.Frame.Height / 2.0f;
    }

Finally, you can separate components from each other (default separation is 11 pts):

    public override nfloat GetSpaceHeight (UIScrollView scrollView)
    {
        return 20.0f;
    }


### Delegate Implementation

Return the behaviours you would expect from the empty states, and receive the user events.

Asks to know if the empty state should be rendered and displayed (Default is `true`):

    public override bool EmptyDataSetShouldDisplay (UIScrollView scrollView)
    {
        return true;
    }

Asks for interaction permission (Default is `true`):

    public override bool EmptyDataSetShouldAllowTouch (UIScrollView scrollView)
    {
        return true;
    }

Asks for scrolling permission (Default is `false`) :

    public override bool EmptyDataSetShouldAllowScroll (UIScrollView scrollView)
    {
        return false;
    }

Asks for image view animation permission (Default is `false`) :

    public override bool EmptyDataSetShouldAllowImageViewAnimate (UIScrollView scrollView)
    {
        return false;
    }

Notifies when the dataset view was tapped:

    public override void EmptyDataSetDidTapView (UIScrollView scrollView, UIView view)
    {
        // Do something
    }

Notifies when the data set call to action button was tapped:

    public override void EmptyDataSetDidTapButton (UIScrollView scrollView, UIButton button)
    {
        // Do something
    }


### Refresh Layout

If you need to refresh the empty state layout, simply call:

    TableView.ReloadData ();
    
    // or (depending on which you are using)
    CollectionView.ReloadData ();


### Force Layout Update

You can also call `ReloadEmptyDataSet ()` to invalidate the current empty state 
layout and trigger a layout update, bypassing `ReloadData ()`. This might be useful if you 
have a lot of logic on your data source that you want to avoid calling, when not needed:

    TableView.ReloadEmptyDataSet ();

Using `ReloadEmptyDataSet ()` is the only way to refresh content when using with 
`UIScrollView`:

    scrollView.ReloadEmptyDataSet ()
