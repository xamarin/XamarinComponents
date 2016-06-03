using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace RoundedImageViewSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            Spinner navSpinner = FindViewById<Spinner>(Resource.Id.spinner_nav);

            navSpinner.Adapter = ArrayAdapter.CreateFromResource(
                navSpinner.Context,
                Resource.Array.action_list,
                Android.Resource.Layout.SimpleSpinnerDropDownItem);

            navSpinner.ItemSelected += (sender, e) =>
            {
                Fragment newFragment;
                switch (e.Position)
                {
                    default:
                    case 0: // bitmap
                        newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.Default);
                        break;
                    case 1: // oval
                        newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.Oval);
                        break;
                    case 2: // select
                        newFragment = RoundedFragment.GetInstance(RoundedFragment.ExampleType.SelectCorners);
                        break;
                    case 3: // picasso
                        newFragment = new PicassoFragment();
                        break;
                    case 4: // color
                        newFragment = new ColorFragment();
                        break;
                }

                SupportFragmentManager.BeginTransaction()
                    .Replace(Resource.Id.fragment_container, newFragment)
                    .Commit();
            };

            if (savedInstanceState == null)
            {
                navSpinner.SetSelection(0);
            }
        }
    }
}
