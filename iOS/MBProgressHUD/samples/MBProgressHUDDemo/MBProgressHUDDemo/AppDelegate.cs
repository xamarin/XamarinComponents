using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using nuint = global::System.UInt32;
#endif

using MBProgressHUD;
using MonoTouch.Dialog;

namespace MBProgressHUDDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navController;
		DialogViewController dvcDialog;
		MTMBProgressHUD hud;

		// vars for managing NSUrlConnection Demo
		public long expectedLength;
		public nuint currentLength;

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

			var root = new RootElement("MBProgressHUD")
			{
				new Section ("Samples")
				{
					new StringElement ("Simple indeterminate progress", ShowSimple),
					new StringElement ("With label", ShowWithLabel),
					new StringElement ("With details label", ShowWithDetailsLabel),
					new StringElement ("Determinate mode", ShowWithLabelDeterminate),
					new StringElement ("Annular determinate mode", ShowWithLabelAnnularDeterminate),
					new StringElement ("Horizontal determinate mode", ShowWithLabelDeterminateHorizontalBar),
					new StringElement ("Custom view", ShowWithCustomView),
					new StringElement ("Mode switching", ShowWithLabelMixed),
					new StringElement ("Using handlers", ShowUsingHandlers),
					new StringElement ("On Window", ShowOnWindow),
					new StringElement ("NSURLConnection", ShowUrl),
					new StringElement ("Dim background", ShowWithGradient),
					new StringElement ("Text only", ShowTextOnly),
					new StringElement ("Colored", ShowWithColor),
				}
			};


			dvcDialog = new DialogViewController(UITableViewStyle.Grouped, root, false);
			navController = new UINavigationController(dvcDialog);

			window.RootViewController = navController;
			window.MakeKeyAndVisible ();

			return true;
		}

		#region Button Actions
		void ShowSimple ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;
			
			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector("MyTask"), this, null, true);
		}

		void ShowWithLabel ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);
			
			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Add information to your HUD
			hud.LabelText = "Loading";

			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyTask"), this, null, true);
		}

		void ShowWithDetailsLabel ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);
			
			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;
			
			// Add information to your HUD
			hud.LabelText = "Loading";
			hud.DetailsLabelText = "updating data";
			hud.Square = true;
			
			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyTask"), this, null, true);
		}

		void ShowWithLabelDeterminate ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Set determinate mode
			hud.Mode = MBProgressHUDMode.Determinate;
			
			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;
			
			// Add information to your HUD
			hud.LabelText = "Loading";
			
			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyProgressTask"), this, null, true);
		}

		void ShowWithLabelAnnularDeterminate ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview(hud);
			
			// Set determinate mode
			hud.Mode = MBProgressHUDMode.AnnularDeterminate;
			
			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;
			
			// Add information to your HUD
			hud.LabelText = "Loading";
			
			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyProgressTask"), this, null, true);
		}

		void ShowWithLabelDeterminateHorizontalBar ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview(hud);

			// Set determinate mode
			hud.Mode = MBProgressHUDMode.DeterminateHorizontalBar;

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Add information to your HUD
			hud.LabelText = "Loading";

			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyProgressTask"), this, null, true);
		}

		void ShowWithCustomView ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Set custom view mode
			hud.Mode = MBProgressHUDMode.CustomView;

			// The sample image is based on the work by http://www.pixelpressicons.com, http://creativecommons.org/licenses/by/2.5/ca/
			// Make the customViews 37 by 37 pixels for best results (those are the bounds of the build-in progress indicators)
			hud.CustomView = new UIImageView (UIImage.FromBundle ("37x-Checkmark.png"));

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;
			
			// Add information to your HUD
			hud.LabelText = "Completed";
			
			// Show the HUD
			hud.Show(true);

			// Hide the HUD after 3 seconds
			hud.Hide (true, 3);
		}

		void ShowWithLabelMixed () 
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Add information to your HUD
			hud.LabelText = "Connecting";
			hud.MinSize = new System.Drawing.SizeF (135f, 135f);

			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyMixedTask"), this, null, true);
		}

		void ShowUsingHandlers ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Add information to your HUD
			hud.LabelText = "With a handler";

			// We show the hud while executing MyTask, and then we clean up
			hud.Show (true, () => { 
				MyTask(); 
			}, () => {
				hud.RemoveFromSuperview();
				hud = null;
			});
		}

		void ShowOnWindow ()
		{
			// The hud will dispable all input on the window
			hud = new MTMBProgressHUD (window);
			this.window.AddSubview (hud);

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Add information to your HUD
			hud.LabelText = "Loading";

			// Show the HUD while the provided method executes in a new thread
			hud.Show (new Selector ("MyTask"), this, null, true);
		}

		void ShowUrl ()
		{
			// Show the hud on top most view
			hud = MTMBProgressHUD.ShowHUD (this.navController.View, true);
			
			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			NSUrl url = new NSUrl ("https://github.com/matej/MBProgressHUD/zipball/master");
			NSUrlRequest request = new NSUrlRequest (url);

			NSUrlConnection connection = new NSUrlConnection (request, new MyNSUrlConnectionDelegete (this, hud));
			connection.Start();
		}

		void ShowWithGradient ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Set HUD to dim Background
			hud.DimBackground = true;

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Show the HUD while the provided method executes in a new thread
			hud.Show(new Selector ("MyTask"), this, null, true);
		}

		void ShowTextOnly ()
		{
			// Show the hud on top most view
			hud = MTMBProgressHUD.ShowHUD (this.navController.View, true);

			// Configure for text only and offset down
			hud.Mode = MBProgressHUDMode.Text;
			hud.LabelText = "Some message...";
			hud.Margin = 10f;
			hud.YOffset = 150f;
			hud.RemoveFromSuperViewOnHide = true;

			hud.Hide (true, 3);
		}

		void ShowWithColor ()
		{
			// The hud will dispable all input on the view (use the higest view possible in the view hierarchy)
			hud = new MTMBProgressHUD (navController.View);
			navController.View.AddSubview (hud);

			// Set the hud to display with a color
			hud.Color = new UIColor (0.23f, 0.5f, 0.82f, 0.90f);

			// Regiser for DidHide Event so we can remove it from the window at the right time
			hud.DidHide += HandleDidHide;

			// Show the HUD while the provided method executes in a new thread
			hud.Show(new Selector ("MyTask"), this, null, true);
		}

		#endregion

		#region fake tasks and Hide Handler

		[Export ("MyTask")]
		void MyTask ()
		{
			System.Threading.Thread.Sleep(3000);
		}

		[Export ("MyProgressTask")]
		void MyProgressTask ()
		{
			float progress = 0.0f;

			while (progress < 1.0f) {
				progress += 0.01f;
				hud.Progress = progress;
				System.Threading.Thread.Sleep(50);
			}
		}

		[Export ("MyMixedTask")]
		void MyMixedTask ()
		{
			// Indeterminate mode
			System.Threading.Thread.Sleep(2000);

			// Switch to determinate mode
			hud.Mode = MBProgressHUDMode.Determinate;
			hud.LabelText = "Progress";
			float progress = 0.0f;
			while (progress < 1.0f)
			{
				progress += 0.01f;
				hud.Progress = progress;
				System.Threading.Thread.Sleep(50);
			}

			// Back to indeterminate mode
			hud.Mode = MBProgressHUDMode.Indeterminate;
			hud.LabelText = "Cleaning up";
			System.Threading.Thread.Sleep(2000);

			// The sample image is based on the work by www.pixelpressicons.com, http://creativecommons.org/licenses/by/2.5/ca/
			// Make the customViews 37 by 37 pixels for best results (those are the bounds of the build-in progress indicators)
			// Since HUD is already on screen, we must set It's custom vien on Main Thread
			BeginInvokeOnMainThread ( ()=> {
				hud.CustomView = new UIImageView (UIImage.FromBundle ("37x-Checkmark.png"));
			});

			hud.Mode = MBProgressHUDMode.CustomView;
			hud.LabelText = "Completed";
			System.Threading.Thread.Sleep (2000);
		}

		void HandleDidHide (object sender, EventArgs e)
		{
			hud.RemoveFromSuperview();
			hud = null;
		}

		#endregion
	}
}

