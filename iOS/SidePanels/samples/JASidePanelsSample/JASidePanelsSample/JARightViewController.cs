using CoreGraphics;
using UIKit;

using JASidePanels;

namespace JASidePanelsSample
{
	public class JARightViewController : JALeftViewController
	{
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.Red;
			label.Text = "Right Panel";
			label.SizeToFit ();
			hide.Frame = new CGRect (View.Bounds.Size.Width - 220.0f, 110.0f, 200.0f, 40.0f);
			hide.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleBottomMargin;
			show.Frame = hide.Frame;
			show.AutoresizingMask = hide.AutoresizingMask;

			removeRightPanel.Hidden = true;
			addRightPanel.Hidden = true;
			changeCenterPanel.Hidden = true;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			var width = this.GetSidePanelController ().RightVisibleWidth;
			label.Center = new CGPoint ((View.Bounds.Size.Width - width) + (width / 2.0f), 65.0f);
		}
	}
}
