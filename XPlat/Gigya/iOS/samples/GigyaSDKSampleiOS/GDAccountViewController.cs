using Foundation;
using System;
using UIKit;
using SDWebImage;

using GigyaSDK;

namespace GigyaSDKSampleiOS
{
	public partial class GDAccountViewController : UIViewController, IGSSocializeDelegate
	{
		public GDAccountViewController(IntPtr handle)
			: base(handle)
		{
			Gigya.SocializeDelegate = this;
		}

		public GSUser User { get; set; }

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			Gigya.SocializeDelegate = null;
		}

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

			// If there is a user, update the display. Otherwise fetch the user info
			if (User != null)
			{
				UpdateUserInfo();
			}
			else
			{
				var request = GSRequest.Create("socialize.getUserInfo");

				// GSUser inherits from GSResponse. socialize.getUserInfo will always return GSUser if completed successfully
				User = (GSUser)await request.SendAsync();

				UpdateUserInfo();
			}
		}

		private void UpdateUserInfo()
		{
			nameLabel.Text = User.FirstName;
			profileImageView.SetImage(User.PhotoUrl);
		}

		// IGSSessionDelegate methods

		[Export("userInfoDidChange:")]
		public virtual void UserInfoDidChange(GSUser user)
		{
			User = user;
			UpdateUserInfo();
		}

		// Touch events

		partial void AddConnectionPressed(UIButton sender)
		{
			// No need to await here because we are using IGSSessionDelegate
			Gigya.ShowAddConnectionProvidersDialogAsync(this, new[] { "microsoft", "facebook", "twitter", "googleplus", "linkedin", "vkontakte", "yahoo" }, null);
		}

		async partial void LogoutPressed(UIButton sender)
		{
			try
			{
				await Gigya.LogoutAsync();
				await TabBarController.PresentingViewController.DismissViewControllerAsync(true);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error logging out: {0}", ex.Message);
			}
		}
	}
}
