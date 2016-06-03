
**AMScrollingNavbar** provides a custom UINavigationController that enables the scrolling of the 
navigation bar alongside the scrolling of an observed content view.

## Usage

Make sure to use a subclass of `ScrollingNavigationController` for your `UINavigationController`. 
Either set the class of your `UINavigationController` in your storyboard, or create 
programmatically a `ScrollingNavigationController` instance in your code.

Use the `FollowScrollView()` method to start following the scrolling of a scrollable 
view (e.g.: a `UIScrollView` or `UITableView`):

    public override void ViewDidAppear (bool animated)
    {
        base.ViewDidAppear (animated);

        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            // Enable the navbar scrolling
            scrolling.FollowScrollView (scrollView, 100.0);
        }
    }

Use the `StopFollowingScrollview()` method to stop the behaviour. Remember to call this function 
on disappear:

    public override void ViewDidDisappear (bool animated)
    {
        base.ViewDidDisappear (animated);

        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            scrolling.StopFollowingScrollView (true);
        }
    }

As the navigation bar may be hidden, we should make sure to show it whenever the view is about to 
disappear:

    public override void ViewWillDisappear (bool animated)
    {
        base.ViewWillDisappear (animated);

        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            scrolling.ShowNavbar (true);
        }
    }

### StateChanged Event

You can attach an event handler to receive a call when the state of the navigation bar changes:

    public override void ViewDidAppear (bool animated)
    {
        base.ViewDidAppear (animated);

        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            scrolling.StateChanged += OnStateChanged;
        }
    }

Similar to attaching the behaviour, you should detach the event on disappear:

    public override void ViewWillDisappear (bool animated)
    {
        base.ViewWillDisappear (animated);

        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            scrolling.StateChanged -= OnStateChanged;
        }
    }

### Scrolling To Top

When the user taps the status bar, by default a scrollable view scrolls to the top of its 
content. If you want to also show the navigation bar, make sure implement the `IUIScrollViewDelegate` 
interface and include this in your controller:

    [Export ("scrollViewShouldScrollToTop:")]
    public bool ShouldScrollToTop (UIScrollView scrollView)
    {
        var scrolling = NavigationController as ScrollingNavigationController;
        if (scrolling != null) {
            scrolling.ShowNavbar (true);
        }
        return true;
    }

You then need to attach this method to the scroll view:

    // for UIScrollView
    scrollView.ShouldScrollToTop = ShouldScrollToTop;
    
    // for UIWebView
    webView.ScrollView.ShouldScrollToTop = ShouldScrollToTop;

Nothing has to be done for a `UITableViewController` as it will automatically pick up
the method. 

### ScrollingNavigationViewController & ScrollingNavigationTableViewController

To DRY things up you can let your view controller subclass `ScrollingNavigationViewController` or 
`ScrollingNavigationViewController`, which provide the base setup implementation. 

You will just need to call the `FollowScrollView()` method. This class provides a 
`ScrollingNavigationController` property to help keep the code as clean as possible:

    public override void ViewDidAppear (bool animated)
    {
        base.ViewDidAppear (animated);

        if (ScrollingNavigationController != null) {
            // Enable the navbar scrolling
            ScrollingNavigationController.FollowScrollView (scrollView, 100.0);
        }
    }

Instead of having to manage the `StateChanged` event, you can override the
`ScrollingNavigationStateChanged` method:

    public override void ScrollingNavigationStateChanged (NavigationBarState state)
    {
        // handle the new state
    }

To handle the scroll-to-top mechanism, you still need to attach the `ShouldScrollToTop` method to 
the scroll view. To perform custom logic when this happens, you can just override the `ShouldScrollToTop`
method. 
