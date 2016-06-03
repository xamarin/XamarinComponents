using System;
using System.ComponentModel;
using AMScrollingNavbar;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AMScrollingNavbar
{
	/// <summary>
	/// A custom `UIViewController` that implements the base configuration.
	/// </summary>
	[DesignTimeVisible (true), Category ("Controllers & Objects")]
	[Register ("ScrollingNavigationViewController")]
	public class ScrollingNavigationViewController : UIViewController, IUIScrollViewDelegate
	{
		public ScrollingNavigationViewController (NSCoder coder)
			: base (coder)
		{
		}

		public ScrollingNavigationViewController ()
			: base ()
		{
		}

		public ScrollingNavigationViewController (string nibName, NSBundle bundle)
			: base (nibName, bundle)
		{
		}

		protected ScrollingNavigationViewController (IntPtr handle)
			: base (handle)
		{
		}

		protected ScrollingNavigationViewController (NSObjectFlag t)
			: base (t)
		{
		}

		public ScrollingNavigationController ScrollingNavigationController {
			get { return NavigationController as ScrollingNavigationController; }
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (ScrollingNavigationController != null) {
				// subscribe to events
				ScrollingNavigationController.StateChanged += OnStateChanged;
			}
		}

		/// <summary>
		/// On appear calls `ShowNavbar()` by default
		/// </summary>
		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			if (ScrollingNavigationController != null) {
				ScrollingNavigationController.ShowNavbar (true);

				// ignore events
				ScrollingNavigationController.StateChanged -= OnStateChanged;
			}
		}

		/// <summary>
		/// On disappear calls `StopFollowingScrollView()` to stop observing the current scroll view, and perform the tear down
		/// </summary>
		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			if (ScrollingNavigationController != null) {
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
