using System;
using Foundation;
using UIKit;

using VKontakte;
using VKontakte.Core;
using VKontakte.Image;
using VKontakte.Views;

namespace VKontakteSampleiOS
{
	partial class MainViewController : UIViewController, IVKSdkDelegate, IVKSdkUIDelegate
	{
		public static string[] Permissions = {
			VKPermissions.Friends,
			VKPermissions.Wall,
			VKPermissions.Audio,
			VKPermissions.Photos,
			VKPermissions.NoHttps,
			VKPermissions.Email,
			VKPermissions.Messages
		};

		public MainViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			VKSdk.Initialize (AppDelegate.AppId);
			VKSdk.Instance.RegisterDelegate (this);
			VKSdk.Instance.UiDelegate = this;
			VKSdk.WakeUpSession (Permissions, (state, error) => {
				if (state == VKAuthorizationState.Authorized) {
					StartWorking ();
				} else if (error != null) {
					new UIAlertView (null, error.Description, null, "OK").Show ();
				}
			});
		}

		public void AccessAuthorizationFinished (VKAuthorizationResult result)
		{
			if (result.Token != null) {
				StartWorking ();
			} else if (result.Error != null) {
				new UIAlertView (null, "Access denied\n" + result.Error, null, "OK").Show ();
			}
		}

		public void UserAuthorizationFailed ()
		{
			new UIAlertView (null, "Access denied", null, "OK").Show ();
			NavigationController.PopToRootViewController (true);
		}

		[Export ("vkSdkTokenHasExpired:")]
		public void TokenHasExpired (VKAccessToken expiredToken)
		{
			VKSdk.Authorize (Permissions);
		}

		public void ShouldPresentViewController (UIViewController controller)
		{
			NavigationController.TopViewController.PresentViewController (controller, true, null);
		}

		public void NeedCaptchaEnter (VKError captchaError)
		{
			var vc = VKCaptchaViewController.Create (captchaError);
			vc.PresentIn (this.NavigationController.TopViewController);
		}

		private void StartWorking ()
		{
			PerformSegue ("StartWorking", this);
		}

		partial void OnAuthorize (UIButton sender)
		{
			VKSdk.Authorize (Permissions);
		}

		partial void OnShare (UIButton sender)
		{
			var shareDialog = new VKShareDialogController ();
			shareDialog.Text = "This post was created and posted using the VKontakte Xamarin.iOS SDK. #vksdk #xamarin #ios";
			shareDialog.UploadImages = new [] { VKUploadImage.Create (UIImage.FromBundle ("apple.png"), VKImageParameters.JpegImage (0.8f)) };
			shareDialog.DismissAutomatically = true;

			PresentViewController (shareDialog, true, null);
		}
	}
}
