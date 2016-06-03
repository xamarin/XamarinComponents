using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RZTransitionsDemo
{
	partial class SimpleColorViewController : UIViewController
	{
		public SimpleColorViewController (IntPtr handle) : base (handle)
		{
			
		}

		UIColor backgroundColor;
		UIColor BackgroundColor {
			get {
				if (backgroundColor == null)
					backgroundColor = UIColorExtensions.Random ();
				return backgroundColor;
			}
			set {
				backgroundColor = value;
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = BackgroundColor;

			var tgr = new UITapGestureRecognizer (() => {
				DismissViewController (true, null);
			});

			View.AddGestureRecognizer (tgr);
		}
	}

	static class UIColorExtensions
	{
		public static UIColor Random ()
		{
			var rand = new Random ();
			var hue = new nfloat ((rand.Next (0, 25600) % 256) / 256.0);
			var saturation = new nfloat ((rand.Next (0, 25600) % 128) / 256.0 + .5);
			var brightness = new nfloat ((rand.Next (0, 25600) % 128) / 256.0 + .5);
			return UIColor.FromHSB (hue, saturation, brightness);
		}
	}
}
