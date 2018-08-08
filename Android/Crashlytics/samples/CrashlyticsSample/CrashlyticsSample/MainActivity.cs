using Android.App;
using Android.Widget;
using Android.OS;
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
            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());
            SetContentView(Resource.Layout.Main);
            var btnForceCrash = FindViewById<Button>(Resource.Id.btnCrashlytics);
            var btnLogin = FindViewById<Button>(Resource.Id.btnFabric);

            btnLogin.Click += (sender, args) =>
            {
                // TODO: Use the current user's information
                // You can call any combination of these three methods
                Crashlytics.Crashlytics.SetUserIdentifier("12345");
                Crashlytics.Crashlytics.SetUserIdentifier("user@fabric.io");
                Crashlytics.Crashlytics.SetUserName("Test User");
            };

            btnForceCrash.Click += (sender, args) =>
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Force Crash");
                alert.SetMessage("This is a Crash!");
                alert.SetNegativeButton("Ok", (senderAlert, EventArgs) =>
                {
                    Toast.MakeText(this, "Ok", ToastLength.Short).Show();
                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };

        }
    }
}