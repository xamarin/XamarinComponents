using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;
using WebKit;

namespace AMScrollingNavbar
{
	/// <summary>
	/// A custom `UINavigationController` that enables the scrolling of the navigation bar alongside the
	/// scrolling of an observed content view
	/// </summary>
	[DesignTimeVisible (true), Category ("Controllers & Objects")]
	[Register ("ScrollingNavigationController")]
	public class ScrollingNavigationController : UINavigationController
	{
		private nfloat fullNavbarHeight {
			get { return navbarHeight + statusBarHeight; }
		}

		private nfloat navbarHeight {
			get { return NavigationBar.Frame.Height; }
		}

		private nfloat statusBarHeight {
			get { return UIApplication.SharedApplication.StatusBarFrame.Height - extendedStatusBarDifference; }
		}

		private nfloat extendedStatusBarDifference {
			get {
				var window = UIApplication.SharedApplication.Delegate?.GetWindow ();
				var height = UIScreen.MainScreen.Bounds.Height;
				if (window != null) {
					height = window.Frame.Height;
				}
				return (nfloat)Math.Abs(View.Bounds.Height - height);
			}
		}

		private nfloat tabBarOffset {
			get {
				// Only account for the tab bar if a tab bar controller is present and the bar is not translucent
				if (TabBarController != null) {
					return TabBarController.TabBar.Translucent ? 0 : TabBarController.TabBar.Frame.Height;
				}
				return 0;
			}
		}

		private UIScrollView scrollView {
			get {
				switch (scrollableView) {
					case UIWebView webView:
						return webView.ScrollView;
					case WKWebView wkWebView:
						return wkWebView.ScrollView;
					default:
						return scrollableView as UIScrollView;
				}
			}
		}

		private CGPoint contentOffset {
			get { return scrollView?.ContentOffset ?? CGPoint.Empty; }
		}

		private CGSize contentSize {
			get {
				var sv = scrollView;
				if (sv == null) {
					return CGSize.Empty;
				}
				var verticalInset = sv.ContentInset.Top + sv.ContentInset.Bottom;
				return new CGSize (sv.ContentSize.Width, sv.ContentSize.Height + verticalInset);
			}
		}

		private nfloat deltaLimit {
			get { return navbarHeight - statusBarHeight; }
		}

		private nfloat delayDistance;
		private nfloat maxDelay;
		private UIPanGestureRecognizer gestureRecognizer;
		private UITabBar sourceTabBar;
		private UIView scrollableView;
		private nfloat lastContentOffset;
		private nfloat scrollSpeedFactor;

		private NSObject willResignActiveObserver;
		private NSObject didBecomeActiveObserver;
		private NSObject deviceOrientationDidChange;

		private NavigationBarState state;
		private NavigationBarState previousState;

		public ScrollingNavigationController (UIViewController rootViewController)
			: base (rootViewController)
		{
			Setup ();
		}

		public ScrollingNavigationController (NSCoder coder)
			: base (coder)
		{
			Setup ();
		}

		public ScrollingNavigationController ()
			: base ()
		{
			Setup ();
		}

		public ScrollingNavigationController (string nibName, NSBundle bundle)
			: base (nibName, bundle)
		{
			Setup ();
		}

		protected ScrollingNavigationController (IntPtr handle)
			: base (handle)
		{
			Setup ();
		}

		protected ScrollingNavigationController (NSObjectFlag t)
			: base (t)
		{
			Setup ();
		}

		public ScrollingNavigationController (Type navigationBarType, Type toolbarType)
			: base (navigationBarType, toolbarType)
		{
			Setup ();
		}

		private void Setup ()
		{
			delayDistance = 0.0f;
			maxDelay = 0.0f;
			lastContentOffset = 0.0f;
			scrollSpeedFactor = 1.0f;
			state = NavigationBarState.Expanded;
			previousState = NavigationBarState.Expanded;
			ShouldScrollWhenContentFits = false;
			ExpandOnActive = true;
			ScrollingEnabled = true;
		}

		/// <summary>
		/// Returns the `NavigationBarState` of the navigation bar
		/// </summary>
		/// <value>The `NavigationBarState` of the navigation bar.</value>
		[Browsable (true)]
		[Export ("State")]
		public NavigationBarState State {
			get {
				return state;
			}
			private set {
				if (state != value) {
					StateChanging?.Invoke (this, EventArgs.Empty);
					state = value;
					StateChanged?.Invoke (this, EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// Determines whether the navbar should scroll when the content inside the scrollview fits
		/// the view's size. Defaults to `false`
		/// </summary>
		[Browsable (true)]
		[Export ("ShouldScrollWhenContentFits")]
		public bool ShouldScrollWhenContentFits { get; set; }

		/// <summary>
		/// Determines if the navbar should expand once the application becomes active after entering background
		/// Defaults to `true`
		/// </summary>
		[Browsable (true)]
		[Export ("ExpandOnActive")]
		public bool ExpandOnActive { get; set; }

		/// <summary>
		/// Determines if the navbar scrolling is enabled.
		/// Defaults to `true`
		/// </summary>
		[Browsable (true)]
		[Export ("ScrollingEnabled")]
		public bool ScrollingEnabled { get; set; }

		public UIView[] Followers { get; set; }

		public event EventHandler StateChanging;

		public event EventHandler StateChanged;

		/// <summary>
		/// Start scrolling
		/// Enables the scrolling by observing a view
		/// </summary>
		/// <param name="scrollableView">The view with the scrolling content that will be observed.</param>
		public void FollowScrollView (UIView scrollableView)
		{
			FollowScrollView (scrollableView, 0, 1, null);
		}

		/// <summary>
		/// Start scrolling
		/// Enables the scrolling by observing a view
		/// </summary>
		/// <param name="scrollableView">The view with the scrolling content that will be observed.</param>
		/// <param name="delay">The delay expressed in points that determines the scrolling resistance. Defaults to `0`.</param>
		public void FollowScrollView (UIView scrollableView, double delay)
		{
			FollowScrollView (scrollableView, delay, 1, null);
		}

		/// <summary>
		/// Start scrolling
		/// Enables the scrolling by observing a view
		/// </summary>
		/// <param name="scrollableView">The view with the scrolling content that will be observed.</param>
		/// <param name="delay">The delay expressed in points that determines the scrolling resistance. Defaults to `0`.</param>
		/// <param name="scrollSpeedFactor">This factor determines the speed of the scrolling content toward the navigation bar animation.</param>
		public void FollowScrollView (UIView scrollableView, double delay, double scrollSpeedFactor)
		{
			FollowScrollView (scrollableView, delay, scrollSpeedFactor, null);
		}

		/// <summary>
		/// Start scrolling
		/// Enables the scrolling by observing a view
		/// </summary>
		/// <param name="scrollableView">The view with the scrolling content that will be observed.</param>
		/// <param name="delay">The delay expressed in points that determines the scrolling resistance. Defaults to `0`.</param>
		/// <param name="followers">An array of `UIView`s that will follow the navbar.</param>
		public void FollowScrollView (UIView scrollableView, double delay, UIView[] followers)
		{
			FollowScrollView (scrollableView, delay, 1, followers);
		}

		/// <summary>
		/// Start scrolling
		/// Enables the scrolling by observing a view
		/// </summary>
		/// <param name="scrollableView">The view with the scrolling content that will be observed.</param>
		/// <param name="delay">The delay expressed in points that determines the scrolling resistance. Defaults to `0`.</param>
		/// <param name="scrollSpeedFactor">This factor determines the speed of the scrolling content toward the navigation bar animation.</param>
		/// <param name="followers">An array of `UIView`s that will follow the navbar.</param>
		public void FollowScrollView (UIView scrollableView, double delay, double scrollSpeedFactor, UIView[] followers)
		{
			this.scrollableView = scrollableView;

			var recognizer = new UIPanGestureRecognizer (OnPan);
			recognizer.ShouldRecognizeSimultaneously = delegate {
				// Enables the scrolling of both the content and the navigation bar
				return true;
			};
			recognizer.ShouldReceiveTouch = delegate {
				// Only scrolls the navigation bar with the content when `scrollingEnabled` is true
				return ScrollingEnabled;
			};
			recognizer.ShouldBegin = delegate {
				// Begin scrolling only if the direction is vertical (prevents conflicts with horizontal scroll views)
				var velocity = gestureRecognizer.VelocityInView (gestureRecognizer.View);
				return Math.Abs (velocity.Y) > Math.Abs (velocity.X);
			};
			recognizer.MaximumNumberOfTouches = 1;

			gestureRecognizer = recognizer;
			scrollableView.AddGestureRecognizer (gestureRecognizer);
			
			willResignActiveObserver = NSNotificationCenter.DefaultCenter.AddObserver (UIApplication.WillResignActiveNotification, notification => {
				previousState = state;
			});
			didBecomeActiveObserver = NSNotificationCenter.DefaultCenter.AddObserver (UIApplication.DidBecomeActiveNotification, notification => {
				if (ExpandOnActive) {
					ShowNavbar (false);
				} else {
					if (previousState == NavigationBarState.Collapsed) {
						HideNavbar (false);
					}
				}
			});
			deviceOrientationDidChange = NSNotificationCenter.DefaultCenter.AddObserver (UIDevice.OrientationDidChangeNotification, notification => {
				ShowNavbar ();
			});

			maxDelay = (nfloat)delay;
			delayDistance = (nfloat)delay;
			this.scrollSpeedFactor = (nfloat)scrollSpeedFactor;
			ScrollingEnabled = true;

			// Save TabBar state (the state is changed during the transition and restored on compeltion)
			if (followers == null) {
				Followers = new UIView[0];
			} else {
				var tab = followers.OfType<UITabBar> ().FirstOrDefault ();
				if (tab != null) {
					sourceTabBar = new UITabBar (tab.Frame);
					sourceTabBar.Translucent = tab.Translucent;
				}
				Followers = followers;
			}
		}

		private void OnPan (UIPanGestureRecognizer gesture)
		{
			if (gesture.State != UIGestureRecognizerState.Failed) {
				var superview = scrollableView?.Superview;
				if (superview != null) {
					var translation = gesture.TranslationInView (superview);
					var delta = (lastContentOffset - translation.Y) / scrollSpeedFactor;
					lastContentOffset = translation.Y;
					if (ShouldScrollWithDelta (delta)) {
						ScrollWithDelta (delta);
					}
				}
			}
			if (gesture.State == UIGestureRecognizerState.Ended || gesture.State == UIGestureRecognizerState.Cancelled || gesture.State == UIGestureRecognizerState.Failed) {
				CheckForPartialScroll ();
				lastContentOffset = 0.0f;
			}
		}

		/// <summary>
		/// Hide the navigation bar, animated.
		/// </summary>
		public void HideNavbar ()
		{
			HideNavbar (true);
		}

		/// <summary>
		/// Hide the navigation bar
		/// </summary>
		/// <param name="animated">If set to <c>true</c> the scrolling is animated.</param>
		public void HideNavbar (bool animated)
		{
			HideNavbar (animated, 0.1);
		}

		/// <summary>
		/// Hide the navigation bar
		/// </summary>
		/// <param name="animated">If set to <c>true</c> the scrolling is animated.</param>
		/// <param name="duration">Animation duration.</param>
		public void HideNavbar (bool animated, double duration)
		{
			if (scrollableView == null || VisibleViewController == null) {
				return;
			}

			if (State == NavigationBarState.Expanded) {
				State = NavigationBarState.Scrolling;
				UIView.AnimateNotify (animated ? duration : 0, () => {
					ScrollWithDelta (fullNavbarHeight);
					VisibleViewController.View.SetNeedsLayout ();
					var scroll = scrollView;
					if (NavigationBar.Translucent && scroll != null) {
						var currentOffset = contentOffset;
						scroll.ContentOffset = new CGPoint (currentOffset.X, currentOffset.Y + navbarHeight);
					}
				}, completed => {
					State = NavigationBarState.Collapsed;
				});
			} else {
				UpdateNavbarAlpha ();
			}
		}

		/// <summary>
		/// Show the navigation bar, animated.
		/// </summary>
		public void ShowNavbar ()
		{
			ShowNavbar (true);
		}

		/// <summary>
		/// Show the navigation bar
		/// </summary>
		/// <param name="animated">If set to <c>true</c> the scrolling is animated.</param>
		public void ShowNavbar (bool animated)
		{
			ShowNavbar (animated, 0.1);
		}

		/// <summary>
		/// Show the navigation bar
		/// </summary>
		/// <param name="animated">If set to <c>true</c> the scrolling is animated.</param>
		/// <param name="duration">Animation duration.</param>
		public void ShowNavbar (bool animated, double duration)
		{
			if (scrollableView == null || VisibleViewController == null) {
				return;
			}

			if (State == NavigationBarState.Collapsed) {
				if (gestureRecognizer != null) {
					gestureRecognizer.Enabled = false;
				}
				State = NavigationBarState.Scrolling;
				UIView.AnimateNotify (animated ? duration : 0, () => {
					lastContentOffset = 0;
					ScrollWithDelta (-fullNavbarHeight, true);
					VisibleViewController.View.SetNeedsLayout ();
					var scroll = scrollView;
					if (NavigationBar.Translucent) {
						var currentOffset = contentOffset;
						scroll.ContentOffset = new CGPoint (currentOffset.X, currentOffset.Y - navbarHeight);
					}
				}, completed => {
					State = NavigationBarState.Expanded;
					if (gestureRecognizer != null) {
						gestureRecognizer.Enabled = true;
					}
				});
			} else {
				UpdateNavbarAlpha ();
			}
		}

		/// <summary>
		/// Stop observing the view and reset the navigation bar
		/// </summary>
		public void StopFollowingScrollView ()
		{
			StopFollowingScrollView (true);
		}

		/// <summary>
		/// Stop observing the view and reset the navigation bar
		/// </summary>
		/// <param name="showingNavbar">If set to <c>true</c> the navbar is shown, otherwise it remains in its current state.</param>
		public void StopFollowingScrollView (bool showingNavbar)
		{
			if (showingNavbar) {
				ShowNavbar (true);
			}
			var recognizer = gestureRecognizer;
			if (recognizer != null) {
				scrollableView?.RemoveGestureRecognizer (recognizer);
			}
			scrollableView = null;
			gestureRecognizer = null;
			ScrollingEnabled = false;

			willResignActiveObserver?.Dispose ();
			willResignActiveObserver = null;
			didBecomeActiveObserver?.Dispose ();
			didBecomeActiveObserver = null;
			deviceOrientationDidChange?.Dispose ();
			deviceOrientationDidChange = null;
		}

		/// <summary>
		/// Will show the navigation bar upon rotation or changes in the trait sizes.
		/// </summary>
		public override void ViewWillTransitionToSize (CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
		{
			base.ViewWillTransitionToSize (toSize, coordinator);

			ShowNavbar ();
		}

		private bool ShouldScrollWithDelta (nfloat delta)
		{
			// Check for rubberbanding
			if (delta < 0) {
				if (scrollableView != null) {
					if (contentOffset.Y + scrollableView.Frame.Height > contentSize.Height) {
						if (scrollableView?.Frame.Height < contentSize.Height) {
							// Only if the content is big enough
							return false;
						}
					}
				}
			}
			return true;
		}

		private void ScrollWithDelta (nfloat delta, bool ignoreDelay = false)
		{
			var frame = NavigationBar.Frame;

			// View scrolling up, hide the navbar
			if (delta > 0) {
				// Update the delay
				if (!ignoreDelay) {
					delayDistance -= delta;

					// Skip if the delay is not over yet
					if (delayDistance > 0) {
						return;
					}
				}

				// No need to scroll if the content fits
				if (!ShouldScrollWhenContentFits && State != NavigationBarState.Collapsed) {
					if (scrollableView?.Frame.Height >= contentSize.Height) {
						return;
					}
				}

				// Compute the bar position
				if (frame.Y - delta < -deltaLimit) {
					delta = frame.Y + deltaLimit;
				}

				// Detect when the bar is completely collapsed
				if (frame.Y <= -deltaLimit) {
					State = NavigationBarState.Collapsed;
					delayDistance = maxDelay;
				} else {
					State = NavigationBarState.Scrolling;
				}
			} else if (delta < 0) {
				// Update the delay
				if (!ignoreDelay) {
					delayDistance += delta;

					// Skip if the delay is not over yet
					if (delayDistance > 0 && maxDelay < contentOffset.Y) {
						return;
					}
				}

				// Compute the bar position
				if (frame.Y - delta > statusBarHeight) {
					delta = frame.Y - statusBarHeight;
				}

				// Detect when the bar is completely expanded
				if (frame.Y >= statusBarHeight) {
					State = NavigationBarState.Expanded;
					delayDistance = maxDelay;
				} else {
					State = NavigationBarState.Scrolling;
				}
			}

			UpdateSizing (delta);
			UpdateNavbarAlpha ();
			RestoreContentOffset (delta);
			UpdateFollowers (delta);
		}

		private void UpdateFollowers (nfloat delta)
		{
			foreach (var follower in Followers) {
				if (follower is UITabBar tabBar) {
					tabBar.Translucent = true;
					var frame = tabBar.Frame;
					frame.Y += (nfloat)(delta * 1.5);
					tabBar.Frame = frame;

					if (sourceTabBar != null) {
						// Set the bar to its original state if it's in its original position
						if (sourceTabBar.Frame.Y == tabBar.Frame.Y) {
							tabBar.Translucent = sourceTabBar.Translucent;
						}
					}
				} else {
					follower.Transform = CGAffineTransform.Translate (follower.Transform, 0, -delta);
				}
			}
		}

		private void UpdateSizing (nfloat delta)
		{
			if (TopViewController == null) {
				return;
			}

			var frame = NavigationBar.Frame;

			// Move the navigation bar
			frame.Location = new CGPoint (frame.X, frame.Location.Y - delta);
			NavigationBar.Frame = frame;
        
			// Resize the view if the navigation bar is not translucent
			if (!NavigationBar.Translucent) {
				var navBarY = NavigationBar.Frame.Y + NavigationBar.Frame.Height;
				frame = TopViewController.View.Frame;
				frame.Location = new CGPoint (frame.X, navBarY);
				frame.Size = new CGSize (frame.Width, View.Frame.Height - (navBarY) - tabBarOffset);
				TopViewController.View.Frame = frame;
			}
		}

		private void RestoreContentOffset (nfloat delta)
		{
			if (NavigationBar.Translucent || delta == 0) {
				return;
			}

			// Hold the scroll steady until the navbar appears/disappears
			scrollView?.SetContentOffset (new CGPoint (contentOffset.X, contentOffset.Y - delta), false);
		}

		private void CheckForPartialScroll ()
		{
			var frame = NavigationBar.Frame;
			var duration = 0.0;
			nfloat delta = 0.0f;

			// Scroll back down
			if (NavigationBar.Frame.Y >= (statusBarHeight - (frame.Height / 2))) {
				delta = frame.Y - statusBarHeight;
				duration = Math.Abs ((delta / (frame.Height / 2)) * 0.2);
				State = NavigationBarState.Expanded;
			} else {
				// Scroll up
				delta = frame.Y + deltaLimit;
				duration = Math.Abs ((delta / (frame.Height / 2)) * 0.2);
				State = NavigationBarState.Collapsed;
			}

			delayDistance = maxDelay;

			UIView.AnimateNotify (duration, 0, UIViewAnimationOptions.BeginFromCurrentState, () => {
				UpdateSizing (delta);
				UpdateFollowers (delta);
				UpdateNavbarAlpha ();
			}, null);
		}

		private void UpdateNavbarAlpha ()
		{
			if (VisibleViewController?.NavigationItem == null) {
				return;
			}

			var frame = NavigationBar.Frame;

			// Change the alpha channel of every item on the navbr
			var alpha = (frame.Y + deltaLimit) / frame.Height;

			// Hide all the possible titles
			if (NavigationItem.TitleView != null) {
				NavigationItem.TitleView.Alpha = alpha;
			}
			NavigationBar.TintColor = NavigationBar.TintColor.ColorWithAlpha (alpha);
			if (NavigationBar.TitleTextAttributes != null) {
				var titleColor = NavigationBar.TitleTextAttributes.ForegroundColor;
				if (titleColor != null) {
					NavigationBar.TitleTextAttributes.ForegroundColor = titleColor.ColorWithAlpha (alpha);
				} else {
					NavigationBar.TitleTextAttributes.ForegroundColor = UIColor.Black.ColorWithAlpha (alpha);
				}
			}

			// Hide all possible button items and navigation items
			void setAlphaOfSubviews (UIView v, nfloat a) {
				v.Alpha = a;
				foreach (var subV in v.Subviews) {
					setAlphaOfSubviews (subV, a);
				}
			};
			bool shouldHideView (UIView view){
				var className = view.Class.Name.Replace ("_", string.Empty);
				var classNames = new List<string> {
					"UINavigationButton",
					"UINavigationItemView",
					"UIImageView",
					"UISegmentedControl"
				};
				if (NavigationBar.RespondsToSelector (new ObjCRuntime.Selector ("prefersLargeTitles"))) {
					classNames.Add (NavigationBar.PrefersLargeTitles ? "UINavigationBarLargeTitleView" : "UINavigationBarContentView");
				} else {
					classNames.Add ("UINavigationBarContentView");
				}
				return classNames.Contains (className);
			}
			foreach (var view in NavigationBar.Subviews) {
				if (shouldHideView (view)) {
					Console.WriteLine ($"setting alpha: {view.Class.Name} to {alpha}");
					setAlphaOfSubviews (view, alpha);
				}
			}

			// Hide the left items
			if (NavigationItem.LeftBarButtonItems != null) {
				foreach (var item in NavigationItem.LeftBarButtonItems) {
					if (item.CustomView != null) {
						item.CustomView.Alpha = alpha;
					}
				}
			}

			// Hide the right items
			if (NavigationItem.RightBarButtonItems != null) {
				foreach (var item in NavigationItem.RightBarButtonItems) {
					if (item.CustomView != null) {
						item.CustomView.Alpha = alpha;
					}
				}
			}
		}
	}
}
