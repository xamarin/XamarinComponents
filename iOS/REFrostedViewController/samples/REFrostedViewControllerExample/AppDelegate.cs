using Foundation;
using UIKit;
using REFrostedViewControllerExample.Controllers;
using REFrostedViewController;
using System.Collections.Generic;

namespace REFrostedViewControllerExample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate, IREFrostedViewControllerDelegate
	{
		// class-level declarations

		public override UIWindow Window{get;set;}

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

			// ViewControllers
			var aRootVC = new DEMOHomeViewController();
			var secondVC = new DEMOSecondViewController();


			//define the menu structure
			var sections = new List<REMenuItemSection>()
			{
				
				new REMenuItemSection()
				{
					Items = new List<REMenuItem>()
					{
						new REMenuViewControllerItem()
						{
							//View exisiting view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"home-48"),
							Title = @"Home",
							ViewController = aRootVC,
						},
						new REMenuViewControllerItem()
						{
							//New view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"about-48"),
							Title = @"Profile",
							ViewController = secondVC,
						},
						new REMenuViewControllerItem()
						{
							//New view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"about-48"),
							Title = @"Chats",
							ViewController = secondVC,
						},
					},
				},
				new REMenuItemSection()
				{
					Title = "Friends Online",
					Items = new List<REMenuItem>()
					{
						new REMenuViewControllerItem()
						{
							//View exisiting view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"business_contact-48"),
							Title = @"John Appleseed",
							ViewController = secondVC,
						},
						new REMenuViewControllerItem()
						{
							//New view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"business_contact-48"),
							Title = @"John Doe",
							ViewController = secondVC,
						},
						new REMenuViewControllerItem()
						{
							//New view controller, will be reused everytime the item is selected
							Icon = UIImage.FromBundle(@"business_contact-48"),
							Title = @"Test User",
							ViewController = secondVC,
						},
						new REMenuActionItem()
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
					},
				},
			};

			//build the default navigation controller and menu controller
			var navigationController = new RENavigationController(aRootVC);
			var menuController = new REFrostedMenuViewController()
			{
				Avatar = UIImage.FromBundle(@"monkey.png"),
				AvatarName = @"Xamarin Monkey",
				Sections = sections,
			};

			//  Setup the frosted view controller
			var frostedViewController = new REFrostedViewController.REFrostedViewController(navigationController, menuController)
			{
				Direction = REFrostedViewControllerDirection.Left,
				LiveBlurBackgroundStyle = REFrostedViewControllerLiveBackgroundStyle.Light,
				LiveBlur = true,
				Delegate = this,
			};

			this.Window.RootViewController = frostedViewController;
			this.Window.BackgroundColor = UIColor.White;
			this.Window.MakeKeyAndVisible();

			return true;
		}
			
		#region IREFrostedViewControllerDelegate implementation

		public void WillAnimateRotationToInterfaceOrientation(REFrostedViewController.REFrostedViewController frostedViewController, UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			
		}

		public void DidRecognizePanGesture(REFrostedViewController.REFrostedViewController frostedViewController, UIPanGestureRecognizer recognizer)
		{
			
		}

		public void WillShowMenuViewController(REFrostedViewController.REFrostedViewController frostedViewController, UIViewController menuViewController)
		{
			
		}

		public void DidShowMenuViewController(REFrostedViewController.REFrostedViewController frostedViewController, UIViewController menuViewController)
		{
			
		}

		public void WillHideMenuViewController(REFrostedViewController.REFrostedViewController frostedViewController, UIViewController menuViewController)
		{
			
		}

		public void DidHideMenuViewController(REFrostedViewController.REFrostedViewController frostedViewController, UIViewController menuViewController)
		{
			
		}

		#endregion
	}
}


