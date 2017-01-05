using System;
using System.Drawing;

#if __UNIFIED__
using Foundation;
using UIKit;
using CoreGraphics;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

using CGPoint = global::System.Drawing.PointF;
#endif

using LoginScreen;

namespace LoginScreenSample
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController ()
		{
			Title = "Login Sample";
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			var loginButton = new UIButton (UIButtonType.RoundedRect);
			loginButton.SetTitle ("Log In", UIControlState.Normal);
			loginButton.SizeToFit ();
			loginButton.Center = new CGPoint (View.Bounds.GetMidX (), View.Bounds.GetMidY ());
			loginButton.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			loginButton.TouchUpInside += LoginClick;

			View.AddSubview (loginButton);
		}

		void LoginClick (object sender, EventArgs e)
		{
			LoginScreenControl<TestCredentialsProvider>.Activate (this);
		}

#pragma warning disable 0672
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
#pragma warning restore 0672
		{
			return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad ||
				toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown;
		}
	}
}