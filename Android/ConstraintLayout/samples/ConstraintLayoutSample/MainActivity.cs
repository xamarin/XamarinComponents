using Android.App;
using Android.Widget;
using Android.OS;

namespace ConstraintLayoutSample
{
    [Activity (Label = "Constraint Layout Sample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.activity_main_done);
        }
    }
}

