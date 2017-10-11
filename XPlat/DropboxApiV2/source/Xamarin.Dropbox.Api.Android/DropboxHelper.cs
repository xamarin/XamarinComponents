using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Dropbox.Api;
using Xamarin.Auth;
using Xamarin.Dropbox.Api.Core.Data;
using Xamarin.Dropbox.Api.Helpers;

namespace Xamarin.Dropbox.Api.Android
{
	public class DropBoxHelper : DropBoxHelperBase<Activity>
	{

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:DropboxV2ApiSampleDroid.Helpers.DropboxHelper"/> is authenticated.
		/// </summary>
		/// <value><c>true</c> if is authenticated; otherwise, <c>false</c>.</value>
		public static bool IsAuthenticated
		{
			get
			{
				return new DropBoxHelper().HasAuthToken;
			}
		}

		/// <summary>
		/// Create a new new client
		/// </summary>
		/// <value>The create client.</value>
		public static DropboxClient CreateDropboxClient()
		{
			return new DropBoxHelper().GenerateClient();
		}

		public DropBoxHelper() : base(null)
		{

		}

		public DropBoxHelper(Action postAuthAction)
			: base(postAuthAction)
		{

		}

		/// <summary>
		/// Gets the auth user interface.
		/// </summary>
		/// <returns>The auth user interface.</returns>
		protected override void ShowAuthUI(Activity ctx)
		{
            Context = ctx;

			var auth = BuildAuthenticator();

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += Auth_Completed;
			auth.Error += Auth_Error;
			auth.BrowsingCompleted += Auth_BrowsingCompleted;

			global::Android.Content.Intent ui_object = auth.GetUI(ctx);

			if (auth.IsUsingNativeUI == true)
			{
				// Step 2.2 Customizing the UI - Native UI [OPTIONAL]
				// In order to access CustomTabs API 
				Xamarin.Auth.CustomTabsConfiguration.AreAnimationsUsed = true;
				Xamarin.Auth.CustomTabsConfiguration.IsShowTitleUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.IsUrlBarHidingUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.IsCloseButtonIconUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.IsActionButtonUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.IsActionBarToolbarIconUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.IsDefaultShareMenuItemUsed = false;
				Xamarin.Auth.CustomTabsConfiguration.MenuItemTitle = null;
				Xamarin.Auth.CustomTabsConfiguration.ToolbarColor = global::Android.Graphics.Color.Orange;
			}

			// Step 3 Present/Launch the Login UI
			ctx.StartActivity(ui_object);
		}

        protected override bool HandleNativeException(Exception ex)
        {
            if (ex is global::Android.OS.OperationCanceledException)
            {
                ShowMessage("Task Cancelled", "Task Cancelled");

                return true;
            }

            return false;

        }

        protected override void ShowMessage(string title, string message, bool toast = false)
        {
            if (toast == true)
            {
				Toast.MakeText
						(
							Context,
							message,
							ToastLength.Long
						).Show();
            }
            else
            {
				var builder = new AlertDialog.Builder(Context);
				builder.SetTitle(title);
				builder.SetMessage(message);
				builder.SetPositiveButton("Ok", (o, e) => { });
				builder.Create().Show();
            }

        }

        protected override void DismissAuthController()
        {
            
        }
    }
}
