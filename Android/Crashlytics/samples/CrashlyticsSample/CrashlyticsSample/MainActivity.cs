using Android.App;
using Android.Widget;
using Android.OS;
using Crashlytics.Devtools;
using Fabric;
using Java.Lang;
using Android.Views;

namespace CrashlyticsSample
{
    [Activity(Label = "CrashlyticsSample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Fabric.Fabric.With(this, new Crashlytics.Devtools.Crashlytics());
            SetContentView(Resource.Layout.Main);
        }

        public void ForceCrash(View view)
        {
            throw new RuntimeException("This is a crash");
        }

    }
}