using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;

using Blurring;

namespace BlurringSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            var layout = FindViewById(Resource.Id.layout);
            var blurringView = FindViewById<BlurringView>(Resource.Id.blurringView);
            var blurSwitch = FindViewById<SwitchCompat>(Resource.Id.blurSwitch);

            var density = Resources.DisplayMetrics.Density;
            blurringView.BlurRadius = (int)(2 * density) + 1;
            blurringView.DownsampleFactor = (int)density + 1;
            blurringView.BlurredView = layout;

            blurSwitch.CheckedChange += (sender, e) =>
            {
                blurringView.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            };
            blurSwitch.Checked = true;
        }
    }
}
