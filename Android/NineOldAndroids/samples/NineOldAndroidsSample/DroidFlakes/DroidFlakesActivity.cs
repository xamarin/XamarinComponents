using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace NineOldAndroidsSample.DroidFlakes
{
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "com.yourcompany.nineoldandroids.sample.SAMPLE" })]
    [Activity(Label = "Droid Flakes", Theme = "@style/Theme.AppCompat")]
    public class DroidFlakesActivity : AppCompatActivity
    {
        private FlakeView flakeView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.DroidFlakes);

            var container = FindViewById<LinearLayout>(Resource.Id.container);

            flakeView = new FlakeView(this);
            container.AddView(flakeView);
            Window.SetBackgroundDrawable(new ColorDrawable(Color.Black));

            var more = FindViewById<Button>(Resource.Id.more);
            more.Click += delegate
            {
                flakeView.AddFlakes(flakeView.NumFlakes);
            };
            var less = FindViewById<Button>(Resource.Id.less);
            less.Click += delegate
            {
                flakeView.SubtractFlakes(flakeView.NumFlakes / 2);
            };
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                var accelerated = FindViewById<CheckBox>(Resource.Id.accelerated);
                accelerated.CheckedChange += (sender, e) =>
                {
                    flakeView.SetLayerType(e.IsChecked ? LayerType.None : LayerType.Software, null);
                };
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            flakeView.Pause();
        }

        protected override void OnResume()
        {
            base.OnResume();

            flakeView.Resume();
        }
    }
}
