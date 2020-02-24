using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Microsoft.Device.Display;
using Android.Content.Res;

namespace DuoSample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.ScreenLayout | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.SmallestScreenSize)]
    public class MainActivity : AppCompatActivity
    {
        ScreenHelper screenHelper;
        bool isDuo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            screenHelper = new ScreenHelper();
            isDuo = screenHelper.Initialize(this);

            Update();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            screenHelper.OnConfigurationChanged(newConfig);

            Update();
        }

        void Update()
        {
            var isSpanned = screenHelper.IsDualMode;

            var hb = screenHelper.GetHingeBounds();
            var hbd = screenHelper.GetHingeBoundsDip();

            var hingeText = $"Hinge:    x:{hb.Left}, y:{hb.Top}, w:{hb.Width()}, h:{hb.Height()}";
            hingeText += $"\r\nHinge DP: x:{hbd.Left}, y:{hbd.Top}, w:{hbd.Width()}, h:{hbd.Height()}";

            FindViewById<TextView>(Resource.Id.textViewDuo).Text = $"Duo: {isDuo}";
            FindViewById<TextView>(Resource.Id.textViewSpan).Text = $"Spanned: {isSpanned}";
            FindViewById<TextView>(Resource.Id.textViewHinge).Text = hingeText;
        }
    }
}