using System;
using Foundation;
using UIKit;

using AMViralSwitch;

namespace AMViralSwitchSample
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

			// blue switch logging
			blueSwitch.OnCompleted += delegate {
				Console.WriteLine ("Animation On");
			};
			blueSwitch.OffCompleted += delegate {
				Console.WriteLine ("Animation Off");
			};

			// the blue switch effects
			blueSwitch.SetAnimationElementsOn (
				AnimationElement.TextColor (blueLabel, UIColor.White),
				AnimationElement.TintColor (infoButton, UIColor.White));
			blueSwitch.SetAnimationElementsOff (
				AnimationElement.TextColor (blueLabel, UIColor.Black),
				AnimationElement.TintColor (infoButton, UIColor.Black));

			// the blue switch events
			blueSwitch.OnCompleted += delegate {
				UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
			};
			blueSwitch.OffCompleted += delegate {
				UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
			};

			// the green switch effects
			greenSwitch.SetAnimationElementsOn (
				AnimationElement.Layer (greenView.Layer, "backgroundColor", UIColor.Clear.CGColor, UIColor.White.CGColor)
			);
			greenSwitch.SetAnimationElementsOff (
				AnimationElement.Layer (greenView.Layer, "backgroundColor", UIColor.White.CGColor, UIColor.Clear.CGColor)
			);
		}
	}
}
