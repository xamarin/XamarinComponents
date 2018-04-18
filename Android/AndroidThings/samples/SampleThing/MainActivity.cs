using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Things.Pio;

namespace SampleThing
{
    [Activity(Label = "Xamarin Android Thing")]
    [IntentFilter (new[] { Intent.ActionMain }, Categories = new[] { Intent.CategoryLauncher })]
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "android.intent.category.IOT_LAUNCHER" })]
    public class MainActivity : Activity
    {
        const string TAG = "SAMPLE_THING";

        ListView listView;
        ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            listView = FindViewById<ListView>(Resource.Id.listView1);

            var peripheralMgr = PeripheralManager.Instance;

            var gpios = peripheralMgr.GpioList;
            foreach (var gpio in gpios)
                Android.Util.Log.Debug(TAG, "Found Peripheral: {0}", gpio);

            adapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleListItem1, gpios);

            listView.Adapter = adapter;
        }
    }
}

