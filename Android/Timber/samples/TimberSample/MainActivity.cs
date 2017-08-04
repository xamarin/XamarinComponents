using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Genetics;
using Genetics.Attributes;

using TimberLog;

namespace TimberSample
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);
			Geneticist.Splice(this);

			Timber.Tag("LifeCycles");
			Timber.D("Activity Created");
		}

		[SpliceClick(Resource.Id.hello)]
		[SpliceClick(Resource.Id.hey)]
		[SpliceClick(Resource.Id.hi)]
		private void OnGreetingClicked(object sender, EventArgs e)
		{
			var button = sender as Button;

			Timber.I($"A button with ID {button.Id} was clicked to say '{button.Text}'.");

			Toast.MakeText(this, "Check logcat for a greeting!", ToastLength.Short).Show();
		}
	}
}
