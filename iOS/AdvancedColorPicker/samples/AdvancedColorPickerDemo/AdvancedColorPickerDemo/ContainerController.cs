using System;

using CoreGraphics;
using UIKit;

using AdvancedColorPicker;

namespace AdvancedColorPickerDemo
{
	public class ContainerController : UIViewController
	{
		public ContainerController()
		{
			Title = "Pick a color!";
			View.BackgroundColor = UIColor.FromRGB(0.3f, 0.8f, 0.3f);

			var pickAColorBtn = UIButton.FromType(UIButtonType.RoundedRect);
			pickAColorBtn.Frame = new CGRect(UIScreen.MainScreen.Bounds.Width / 2 - 50, UIScreen.MainScreen.Bounds.Height / 2 - 22, 100, 44);
			pickAColorBtn.AutoresizingMask = UIViewAutoresizing.FlexibleMargins;
			pickAColorBtn.SetTitle("Pick a color!", UIControlState.Normal);
			pickAColorBtn.TouchUpInside += async delegate
			{
				var alert = new UIAlertView("Hi", "hi there", null, null);

				// get a color from the user
				var color = await ColorPickerViewController.PresentAsync(NavigationController, "Pick a color!", View.BackgroundColor);

				// changethe background
				View.BackgroundColor = color;

				// set the title to the hex value
				nfloat red, green, blue, alpha;
				color.GetRGBA(out red, out green, out blue, out alpha);
				Title = string.Format("#{0:X2}{1:X2}{2:X2}", (int)(red * 255), (int)(green * 255), (int)(blue * 255));
			};
			View.AddSubview(pickAColorBtn);
		}
	}
}
