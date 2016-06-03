using System;
using UIKit;

namespace FXBlurViewSample
{
	partial class AnimatedBlurViewController : UIViewController
	{
		public AnimatedBlurViewController (IntPtr handle) 
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			blurView.BlurRadius = 0;
			blurView.TintColor = UIColor.Clear;
			blurView.Dynamic = false;
		}

		partial void OnToggleBlur (UIButton sender)
		{
			if (blurView.BlurRadius < 5) {
				UIView.Animate (0.5, () => {
					blurView.BlurRadius = 40;
				});
			} else {
				UIView.Animate (0.5, () => {
					blurView.BlurRadius = 0;
				});
			}
		}
	}
}
