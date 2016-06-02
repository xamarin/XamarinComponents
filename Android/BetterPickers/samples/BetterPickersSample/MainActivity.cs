using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using BetterPickers;
using BetterPickers.DatePickers;

namespace BetterPickersSample
{
	[Activity (Label = "BetterPickers Sample", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
        ChoiceListFragment fragment;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            fragment = new ChoiceListFragment ();

            SupportFragmentManager.BeginTransaction ()
                .Replace (Resource.Id.content, fragment)
                .Commit ();
		}
	}
}
