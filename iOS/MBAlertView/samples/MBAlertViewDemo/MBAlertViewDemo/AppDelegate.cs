using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using AlertView;

namespace MBAlertViewDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UIViewController controller;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			window.BackgroundColor = UIColor.FromPatternImage (UIImage.FromBundle ("background.png"));
			window.MakeKeyAndVisible ();

			controller = new UIViewController ();
			window.RootViewController = controller;

			PlayDemo ();

			return true;
		}

		public void PlayDemo ()
		{
			MBAlertViewButtonHandler goodbye = () => {
				MBHUDView.HudWithBody ("Goodbye then", MBAlertViewHUDType.Default, 2.0f, true);
			};

			MBHUDView.HudWithBody ("Hello", MBAlertViewHUDType.Checkmark, 1.0f, true);
			MBAlertView alert = MBAlertView.AlertWithBody ("Do you want to see more? \n\n(Note: you do have a choice with multibuttons, but you should tap yes)", null, null);
			alert.AddButtonWithText ("Yes", MBAlertViewItemType.Positive, () => {
				MBHUDView.HudWithBody ("Say please", MBAlertViewHUDType.ExclamationMark, 1.5f, true);
				MBAlertView please = MBAlertView.AlertWithBody ("Did you say please?", null, null);
				please.Size = new Size (280, 180);
				please.AddButtonWithText ("Yes", MBAlertViewItemType.Positive, () => {

					MBHUDView.HudWithBody ("Good boy.", MBAlertViewHUDType.Checkmark, 1.0f, true);
					MBHUDView.HudWithBody ("Wait.", MBAlertViewHUDType.ActivityIndicator, 4.0f, true);
					MBHUDView.HudWithBody ("Ready?", MBAlertViewHUDType.Default, 2.0f, true);

					MBFlatAlertView flatView = MBFlatAlertView.AlertWithTitle ("One more thing", "Before we continue, we want to see the new style. ", "I like it!", () => {
						MBFlatAlertView moreButtons = MBFlatAlertView.AlertWithTitle ("More Buttons", "This one can also have more buttons. ", null, null);
						moreButtons.AddButtonWithTitle ("Cool!", MBFlatAlertButtonType.Green, () => {

							BlurryAlertView blurry = BlurryAlertView.AlertWithBody ("We had fun, now we are going to continue...\n(Did you notice the custom backgrond on this one? Done in C#!)", "I love C# types!", () => {

								MBAlertView destruct = MBAlertView.AlertWithBody ("Do you want your device to self-destruct?", null, null);
								destruct.ImageView.Image = UIImage.FromBundle ("image.png");

								destruct.AddButtonWithText ("Yes please", MBAlertViewItemType.Destructive, () => {

									MBHUDView.HudWithBody ("Ok", MBAlertViewHUDType.Checkmark, 1.0f, true);
									MBHUDView.HudWithBody ("5", MBAlertViewHUDType.Default, 1.0f, true);
									MBHUDView.HudWithBody ("4", MBAlertViewHUDType.Default, 1.0f, true);
									MBHUDView.HudWithBody ("3", MBAlertViewHUDType.Default, 1.0f, true);
									MBHUDView.HudWithBody ("2", MBAlertViewHUDType.Default, 1.0f, true);
									MBHUDView.HudWithBody ("1", MBAlertViewHUDType.Default, 2.0f, true);
									this.DoSomething (() => {
										MBHUDView hud = MBHUDView.HudWithBody ("Goodbye", MBAlertViewHUDType.ExclamationMark, 2.0f, true);
										hud.UponDismissalHandler = () => {
											UIView.Animate (0.5f, () => {
												controller.View.BackgroundColor = UIColor.Black;
											});
										};
									}, 2);
								});

								destruct.AddButtonWithText ("No thank you", MBAlertViewItemType.Default, () => {
									MBHUDView.HudWithBody ("Ok, bye.", MBAlertViewHUDType.Default, 2.0f, true);
								});

								destruct.AddToDisplayQueue ();
							});
							blurry.AddToWindow ();
						});
						moreButtons.AddButtonWithTitle ("Boring...", MBFlatAlertButtonType.Red, new MBFlatAlertButtonHandler (goodbye));
						moreButtons.AddToDisplayQueue ();
					});
					flatView.AddToDisplayQueue ();
				});

				please.AddButtonWithText ("Maybe", MBAlertViewItemType.Default, goodbye);
				please.AddButtonWithText ("No", MBAlertViewItemType.Destructive, goodbye);

				please.AddToDisplayQueue ();
			});

			alert.AddButtonWithText ("I don\'t know", MBAlertViewItemType.Default, goodbye);
			alert.AddButtonWithText ("Maybe", MBAlertViewItemType.Default, goodbye);
			alert.AddButtonWithText ("No", MBAlertViewItemType.Destructive, goodbye);

			alert.AddToDisplayQueue ();
		}

		public void DoSomething (MBAlertViewButtonHandler block, int delay)
		{
			ThreadPool.QueueUserWorkItem (state => { 
				Thread.Sleep (delay);
				InvokeOnMainThread (() => {
					block ();
				});
			});
		}
	}
}

