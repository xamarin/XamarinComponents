using System;
using System.ComponentModel;
using Foundation;
using UIKit;

namespace AMScrollingNavbar
{
	/// <summary>
	/// A custom `UIViewController` that implements the base configuration.
	/// </summary>
	[DesignTimeVisible (true), Category ("Controllers & Objects")]
	[Register ("ScrollingNavigationTableViewController")]
	public class ScrollingNavigationTableViewController : UITableViewController, IUIScrollViewDelegate
	{
		public ScrollingNavigationTableViewController (NSCoder coder)
			: base (coder)
		{
		}

		public ScrollingNavigationTableViewController ()
			: base ()
		{
		}

		public ScrollingNavigationTableViewController (string nibName, NSBundle bundle)
			: base (nibName, bundle)
		{
		}

		protected ScrollingNavigationTableViewController (IntPtr handle)
			: base (handle)
		{
		}

		protected ScrollingNavigationTableViewController (NSObjectFlag t)
			: base (t)
		{
		}

		public ScrollingNavigationTableViewController (UITableViewStyle withStyle)
			: base (withStyle)
		{
		}

		public ScrollingNavigationController ScrollingNavigationController {
			get { return NavigationController as ScrollingNavigationController; }
		}
		
		/// <summary>
		/// On appear calls `ShowNavbar()` by default
		/// </summary>
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (ScrollingNavigationController != null) {
				ScrollingNavigationController.ShowNavbar (true);
				ScrollingNavigationController.StateChanged += OnStateChanged;
			}
		}

		/// <summary>
		/// On disappear calls `StopFollowingScrollView()` to stop observing the current scroll view, and perform the tear down
		/// </summary>
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			if (ScrollingNavigationController != null) {
				ScrollingNavigationController.StateChanged -= OnStateChanged;
				ScrollingNavigationController.StopFollowingScrollView ();
			}
		}

		/// <summary>
		/// Calls `ShowNavbar()` when a `ScrollToTop` is requested
		/// </summary>
		[Export ("scrollViewShouldScrollToTop:")]
		public virtual bool ShouldScrollToTop (UIScrollView scrollView)
		{
			if (ScrollingNavigationController != null) {
				ScrollingNavigationController.ShowNavbar (true);
			}
			return true;
		}

		public virtual void ScrollingNavigationStateChanged (NavigationBarState state)
		{
		}

		private void OnStateChanged (object sender, EventArgs e)
		{
			ScrollingNavigationStateChanged (ScrollingNavigationController.State);
		}
	}
}
