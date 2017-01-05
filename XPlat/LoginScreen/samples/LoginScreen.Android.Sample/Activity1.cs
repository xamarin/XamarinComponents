using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using LoginScreen;

namespace LoginScreen.Android.Sample
{
	[Activity (Label = "LoginScreen.Android.Sample", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Main);

			FindViewById<Button>(Resource.Id.loginButton).Click += (sender, e) => LoginScreenControl<TestCredentialsProvider>.Activate(this);
		}
	}
}


