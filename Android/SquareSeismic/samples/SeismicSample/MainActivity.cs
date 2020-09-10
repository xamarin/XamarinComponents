using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Views;
using Android.Widget;

using Square.Seismic;

namespace SeismicSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // set up the sensor
            var sensorManager = SensorManager.FromContext(this);
            var shakeDetector = new ShakeDetector(() =>
            {
                Toast.MakeText(this, "Don't shake me, bro!", ToastLength.Short).Show();
            });
            shakeDetector.Start(sensorManager);

            // create the UI
            var textView = new TextView(this);
            textView.Gravity = GravityFlags.Center;
            textView.Text = "Shake me, bro!";

            // set the UI
            SetContentView(
                textView,
                new ViewGroup.LayoutParams(
                    ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent));
        }
    }
}
