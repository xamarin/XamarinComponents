using System;
using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Widget;

using VKontakte;
using VKontakte.Utils;

[assembly: UsesPermission (Manifest.Permission.Internet)]

namespace VKontakteSampleAndroid
{
	[Application (Theme = "@style/AppTheme")]
	public class VKontakteApplication : Application
	{
		private readonly TokenTracker tokenTracker = new TokenTracker ();

		protected VKontakteApplication (IntPtr javaReference, JniHandleOwnership transfer)
			: base (javaReference, transfer)
		{
		}

		public override void OnCreate ()
		{
			base.OnCreate ();

			// list the app fingerprints
			string[] fingerprints = VKUtil.GetCertificateFingerprint (this, PackageName);
			foreach (var fingerprint in fingerprints) {
				Console.WriteLine ("Detected Fingerprint: " + fingerprint);
			}

			// setup VK
			tokenTracker.StartTracking ();
			VKSdk.Initialize (this).WithPayments ();
		}

		private class TokenTracker : VKAccessTokenTracker
		{
			public override void OnVKAccessTokenChanged (VKAccessToken oldToken, VKAccessToken newToken)
			{
				if (newToken == null) {
					Toast.MakeText (Application.Context, "AccessToken invalidated", ToastLength.Long).Show ();
					Intent intent = new Intent (Application.Context, typeof(LoginActivity));
					intent.SetFlags (ActivityFlags.NewTask | ActivityFlags.ClearTop);
					Application.Context.StartActivity (intent);
				}
			}
		}
	}
}