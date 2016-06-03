using System;
using UIKit;

namespace FXBlurViewSample
{
	partial class BasicBlurViewController : UIViewController
	{
		public BasicBlurViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			blurView.TintColor = UIColor.Clear;
		}

		partial void OnToggleDynamic (UISwitch sender)
		{
			blurView.Dynamic = sender.On;
		}

		partial void OnUpdateBlur (UISlider sender)
		{
			blurView.BlurRadius = sender.Value;
		}
	}
}
