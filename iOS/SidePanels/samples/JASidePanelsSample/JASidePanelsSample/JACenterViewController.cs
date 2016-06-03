using System;
using UIKit;

using JASidePanels;

namespace JASidePanelsSample
{
	public class JACenterViewController : JADebugViewController
	{
		public JACenterViewController ()
		{
			Title = "Center Panel";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Random rnd = new Random ();

			View.BackgroundColor = UIColor.FromRGB (
				(float)rnd.NextDouble (),
				(float)rnd.NextDouble (),
				(float)rnd.NextDouble ());
		}
	}
}
