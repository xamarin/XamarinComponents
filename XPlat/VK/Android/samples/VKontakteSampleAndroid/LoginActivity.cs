using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Fragment = Android.Support.V4.App.Fragment;

using VKontakte;

namespace VKontakteSampleAndroid
{
	/// <summary>
	/// Activity which displays a login screen to the user, offering registration as well.
	/// </summary>
	[Register ("com.samples.vkontakte.vkontaktesampleandroid.LoginActivity")]
	[IntentFilter (new [] { Intent.ActionView }, DataScheme = "vk5167570", Categories = new[] {
		Intent.CategoryBrowsable,
		Intent.CategoryDefault
	})]
	[Activity (Label = "VKontakte", MainLauncher = true, WindowSoftInputMode = SoftInput.AdjustResize)]
	public class LoginActivity : AppCompatActivity
	{
		private bool isResumed = false;

		// Scope is set of required permissions for your application
		// (see vk.com api permissions documentation: https://vk.com/dev/permissions)
		private static string[] MyScopes = {
			VKScope.Friends,
			VKScope.Wall,
			VKScope.Photos,
			VKScope.Nohttps,
			VKScope.Messages,
			VKScope.Docs
		};

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_start);

			VKSdk.WakeUpSession (this, 
				response => {
					if (isResumed) {
						if (response == VKSdk.LoginState.LoggedOut) {
							ShowLogin ();
						} else if (response == VKSdk.LoginState.LoggedIn) {
							ShowLogout ();
						}
					}
				},
				error => {
					Console.WriteLine ("WakeUpSession error: " + error);
				});
		}

		private void ShowLogout ()
		{
			SupportFragmentManager
				.BeginTransaction ()
				.Replace (Resource.Id.container, new LogoutFragment ())
				.CommitAllowingStateLoss ();
		}

		private void ShowLogin ()
		{
			SupportFragmentManager
				.BeginTransaction ()
				.Replace (Resource.Id.container, new LoginFragment ())
				.CommitAllowingStateLoss ();
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			isResumed = true;
			if (VKSdk.IsLoggedIn) {
				ShowLogout ();
			} else {
				ShowLogin ();
			}
		}

		protected override void OnPause ()
		{
			isResumed = false;

			base.OnPause ();
		}

		protected override async void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			bool vkResult;
			var task = VKSdk.OnActivityResultAsync (requestCode, resultCode, data, out vkResult);
			if (!vkResult) {
				base.OnActivityResult (requestCode, resultCode, data);
			}

			try {
				var token = await task;
				Console.WriteLine ("User passed Authorization: " + token.AccessToken);
				StartTestActivity ();
			} catch (VKException ex) {
				Console.WriteLine ("User didn't pass Authorization: " + ex);
			}
		}

		private void StartTestActivity ()
		{
			StartActivity (new Intent (this, typeof(TestActivity)));
		}

		private class LoginFragment : Fragment
		{
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				var view = inflater.Inflate (Resource.Layout.fragment_login, container, false);
				view.FindViewById (Resource.Id.sign_in_button).Click += delegate {
					VKSdk.Login (Activity, MyScopes);
				};
				return view;
			}
		}

		private class LogoutFragment : Fragment
		{
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				var view = inflater.Inflate (Resource.Layout.fragment_logout, container, false);
				view.FindViewById (Resource.Id.continue_button).Click += delegate {
					((LoginActivity)Activity).StartTestActivity ();
				};
				view.FindViewById (Resource.Id.logout).Click += delegate {
					VKSdk.Logout ();
					if (!VKSdk.IsLoggedIn) {
						((LoginActivity)Activity).ShowLogin ();
					}
				};
				return view;
			}
		}
	}
}
