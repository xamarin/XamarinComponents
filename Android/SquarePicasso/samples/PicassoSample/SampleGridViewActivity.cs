using Android.App;
using Android.OS;
using Android.Widget;

using Square.Picasso;

namespace PicassoSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat")]
    public class SampleGridViewActivity : PicassoSampleActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.sample_gridview_activity);

            GridView gv = FindViewById<GridView>(Resource.Id.grid_view);
            gv.Adapter = new SampleGridViewAdapter(this);
            gv.ScrollStateChanged += (sender, e) =>
            {
                Picasso picasso = Picasso.Get();
                if (e.ScrollState == ScrollState.Idle || e.ScrollState == ScrollState.TouchScroll)
                {
                    picasso.ResumeTag(this);
                }
                else
                {
                    picasso.PauseTag(this);
                }
            };
        }
    }
}
