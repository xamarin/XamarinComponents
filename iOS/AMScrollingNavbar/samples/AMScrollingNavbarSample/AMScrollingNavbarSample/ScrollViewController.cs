using System;
using CoreGraphics;
using UIKit;

using AMScrollingNavbar;

namespace AMScrollingNavbarSample
{
	partial class ScrollViewController : ScrollingNavigationViewController
	{
		private const string AmazingGraceLyrics =
			"Amazing Grace!\n" +
			"How sweet the sound\n" +
			"That saved a wretch like me\n" +
			"I once was lost but now am found,\n" +
			"Was blind but now I see\n" +
			"\n" +
			"'Twas Grace that taught my heart to fear\n" +
			"And Grace my fears relieved\n" +
			"How precious did that grace appear\n" +
			"The hour I first believed\n" +
			"\n" +
			"Through many dangers, toils and snares\n" +
			"I have already come\n" +
			"'Tis Grace has brought me safe thus far\n" +
			"And Grace will lead me home\n" +
			"\n" +
			"The Lord has promised good to me\n" +
			"His word my hope secures;\n" +
			"He will my sheild and portion be,\n" +
			"As long as life endures\n" +
			"\n" +
			"Yet, when this flesh and heart shall fail,\n" +
			"And mortal life shall cease,\n" +
			"I shall posess within the veil,\n" +
			"A life of joy and peace.\n" +
			"\n" +
			"When we've been there ten thousand years\n" +
			"Bright shining as the sun,\n" +
			"We've no less days to sing God's praise\n" +
			"Than when we've first begun";

		public ScrollViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Scroll View";

			if (NavigationController?.NavigationBar != null) {
				NavigationController.NavigationBar.BarTintColor = new UIColor (0.17f, 0.59f, 0.87f, 1.0f);
				NavigationController.NavigationBar.PrefersLargeTitles = false;
			}
			if (TabBarController?.TabBar != null) {
				TabBarController.TabBar.BarTintColor = new UIColor (0.17f, 0.59f, 0.87f, 1.0f);
				TabBarController.TabBar.TintColor = UIColor.White;
			}

			scrollView.BackgroundColor = new UIColor (0.13f, 0.5f, 0.73f, 1.0f);

			var label = new UILabel (new CGRect (10, 10, 0, 0));
			var font = UIFont.FromName ("STHeitiSC-Light", 20);
			if (font != null) {
				label.Font = font;
			}
			scrollView.AddSubview (label);

			// Fake some content
			label.Text = AmazingGraceLyrics;
			label.Lines = 0;
			label.TextColor = UIColor.White;
			label.SizeToFit ();

			scrollView.ContentSize = new CGSize (View.Frame.Width, label.Frame.Height);

			scrollView.Delegate = this;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (ScrollingNavigationController?.NavigationBar != null) {
				ScrollingNavigationController.NavigationBar.BarTintColor = new UIColor (0.17f, 0.59f, 0.87f, 1.0f);

				// Enable the navbar scrolling
				if (TabBarController != null) {
					ScrollingNavigationController.FollowScrollView (scrollView, 0, 2, new[] { TabBarController.TabBar });
				} else {
					ScrollingNavigationController.FollowScrollView (scrollView, 0.0, 2);
				}

				ScrollingNavigationController.ExpandOnActive = false;
			}
		}

		public override void ScrollingNavigationStateChanged (NavigationBarState state)
		{
			Console.WriteLine ("Navigation bar state changed: " + state);
		}
	}
}
