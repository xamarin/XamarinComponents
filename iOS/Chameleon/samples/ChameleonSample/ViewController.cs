using System;
using UIKit;

using ChameleonFramework;

namespace ChameleonSample
{
	partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			NavigationController.HidesNavigationBarHairline();

			var image = UIImage.FromBundle ("africa-blue.jpg");
			var average = ChameleonColor.GetImageAverageColor (null, image);
			View.BackgroundColor = average;
		}
	}
}
