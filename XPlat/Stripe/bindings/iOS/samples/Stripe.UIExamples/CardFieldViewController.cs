using System;
using CoreGraphics;
using Stripe.iOS;
using UIKit;

namespace Stripe.UIExamples
{
	public class CardFieldViewController : UIViewController
	{
		PaymentCardTextField cardField = new PaymentCardTextField ();

		public Theme Theme { get; set; } = Theme.DefaultTheme;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Card Field";

			View.BackgroundColor = UIColor.White;
			View.AddSubview (cardField);
			EdgesForExtendedLayout = UIRectEdge.None;
			View.BackgroundColor = Theme.PrimaryBackgroundColor;

			cardField.BackgroundColor = Theme.SecondaryBackgroundColor;
			cardField.TextColor = Theme.PrimaryForegroundColor;
			cardField.PlaceholderColor = Theme.SecondaryForegroundColor;
			cardField.BorderColor = Theme.AccentColor;
			cardField.BorderWidth = 1.0f;
			cardField.TextErrorColor = Theme.ErrorColor;
			cardField.PostalCodeEntryEnabled = true;

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Done, Done);
			NavigationController.NavigationBar.SetStripeTheme (Theme);
		}

		void Done (object sender, EventArgs e)
		{
			DismissViewController (true, null);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			cardField.BecomeFirstResponder ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			nfloat padding = 15;

			cardField.Frame = new CGRect (padding, padding, View.Bounds.Width - (padding * 2), 50);
		}
	}
}
