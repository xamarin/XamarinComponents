using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Things.Pio;

namespace SampleThing
{
    [Activity(Label = "Sample Thing")]
    [IntentFilter (new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryLauncher })]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "android.intent.category.IOT_LAUNCHER" })]
    public class MainActivity : Activity
    {
        const string TAG = "SAMPLE_THING";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var peripheralMgr = new PeripheralManagerService();
            foreach (var gpio in peripheralMgr.GpioList)
                Android.Util.Log.Debug(TAG, "Found Peripheral: {0}", gpio);
        }
    }
}

