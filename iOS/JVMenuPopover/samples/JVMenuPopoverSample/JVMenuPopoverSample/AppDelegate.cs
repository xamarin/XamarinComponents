using Foundation;
using UIKit;
using JVMenuPopoverSample.ViewControllers;
using JVMenuPopover;
using System.Collections.Generic;

namespace JVMenuPopoverSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		public override UIWindow Window {get; set;}
		public UINavigationController NavigationController {get; set;}

		/// <summary>
		/// Finished launching.
		/// </summary>
		/// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
		/// <param name="application">Application.</param>
		/// <param name="launchOptions">Launch options.</param>
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			//create the initial view controller
			var rootController = new JVMenuRootViewController();

			//build the shared menu
			JVMenuPopoverConfig.SharedInstance.MenuItems = new List<JVMenuItem>()
			{
				new JVMenuViewControllerItem()
				{
					//View exisiting view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"home-48"),
					Title = @"Home",
					ViewController = rootController,
				},
				new JVMenuViewControllerItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"about-48"),
					Title = @"About Us",
					ViewController = new JVMenuSecondController(),
				},
				new JVMenuViewControllerItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"settings-48"),
					Title = @"Our Service",
					ViewController = new JVMenuThirdController(),
				},
				new JVMenuViewControllerItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"business_contact-48"),
					Title = @"Contact Us",
					ViewController = new JVMenuFourthController(),
				},
				new JVMenuViewControllerItem<JVMenuFifthController>()
				{
					//New view controller, will be recreated afresh everytime the item is selected
					Icon = UIImage.FromBundle(@"ask_question-48"),
					Title = @"Help?",
					AlwaysNew = true,
				},
				new JVMenuActionItem()
				{
					//Action is called, on the UI thread, everytime the item is selected
					Icon = UIImage.FromBundle(@"ask_question-48"),
					Title = @"Logout",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Logout","Are you sure you want to log out?",null,"No","Yes");
						uiAlert.Show();
					},
				},
			};

			//create a Nav controller an set the root controller
			NavigationController = new UINavigationController(rootController);

			//setup the window
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			Window.RootViewController = NavigationController;
			Window.ContentMode = UIViewContentMode.ScaleAspectFill;
			Window.BackgroundColor = UIColor.FromPatternImage(JVMenuHelper.ImageWithImage(UIImage.FromBundle("app_bg1.jpg"),this.Window.Frame.Width));
			Window.Add(NavigationController.View);
			Window.MakeKeyAndVisible();
	
			return true;
		}
			
	}
}


