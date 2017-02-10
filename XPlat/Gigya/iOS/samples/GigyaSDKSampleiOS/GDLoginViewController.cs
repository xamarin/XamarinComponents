using Foundation;
using System;
using UIKit;

using GigyaSDK;

namespace GigyaSDKSampleiOS
{
	public partial class GDLoginViewController : UIViewController
	{
		private bool didAlreadyCheckIfUserIsLoggedIn;

		public GDLoginViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			// If already logged in, we want to move to the next view controller
			if (Gigya.IsSessionValid && !didAlreadyCheckIfUserIsLoggedIn)
			{
				var tabBarController = Storyboard.InstantiateViewController("TabBarViewController");
				PresentViewControllerAsync(tabBarController, true);
			}

			didAlreadyCheckIfUserIsLoggedIn = true;
		}

		async partial void LoginPressed(UIButton sender)
		{
			var providers = new[] { "microsoft", "facebook", "twitter", "googleplus", "linkedin", "vkontakte", "yahoo" };

			try
			{
				var user = await Gigya.ShowLoginProvidersDialogAsync(this, providers, null);

				var tabBarController = (UITabBarController)Storyboard.InstantiateViewController("TabBarViewController");
				var accountViewController = (GDAccountViewController)tabBarController.ViewControllers[2];
				accountViewController.User = user;
				await PresentViewControllerAsync(tabBarController, true);
			}
			catch (NSErrorException ex)
			{
				// If the login was canceled by the user - do nothing. Otherwise, display an error.
				if (ex.Error.Code != (int)GSErrorCode.CanceledByUser)
				{
					Console.WriteLine("Error: {0}", ex.Error.UserInfo);
					var alert = new UIAlertView("Error", "An error has occured. Please try again later.", null, "OK");
					alert.Show();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: {0}", ex.Message);
			}
		}
	}
}
