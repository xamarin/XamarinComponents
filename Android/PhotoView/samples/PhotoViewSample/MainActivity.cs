using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace PhotoViewSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        public static string[] options =
        {
            "Simple Sample",
            "ViewPager Sample",
            "Rotation Sample"
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var listView = FindViewById<ListView>(Resource.Id.listView);
            listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, options);
            listView.ItemClick += (sender, e) =>
            {
                Type c;
                switch (e.Position)
                {
                    default:
                    case 0:
                        c = typeof(SimpleSampleActivity);
                        break;
                    case 1:
                        c = typeof(ViewPagerSampleActivity);
                        break;
                    case 2:
                        c = typeof(RotationSampleActivity);
                        break;
                }

                StartActivity(new Intent(this, c));
            };
        }
    }
}
