using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using Square.Picasso;

namespace PicassoSample
{
    public abstract class PicassoSampleActivity : AppCompatActivity
    {
        private ToggleButton showHide;
        private FrameLayout sampleContent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.SetContentView(Resource.Layout.picasso_sample_activity);
            sampleContent = FindViewById<FrameLayout>(Resource.Id.sample_content);

            ListView activityList = FindViewById<ListView>(Resource.Id.activity_list);
            PicassoSampleAdapter adapter = new PicassoSampleAdapter(this);
            activityList.Adapter = adapter;
            activityList.ItemClick += (sender, e) =>
            {
                adapter[e.Position].Launch(this);
            };

            showHide = FindViewById<ToggleButton>(Resource.Id.faux_action_bar_control);
            showHide.CheckedChange += (sender, e) =>
            {
                activityList.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
            };
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            Picasso.Get().CancelTag(this);
        }

        public override void OnBackPressed()
        {
            if (showHide.Checked)
            {
                showHide.Checked = false;
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override void SetContentView(int layoutResID)
        {
            LayoutInflater.Inflate(layoutResID, sampleContent);
        }

        public override void SetContentView(View view)
        {
            sampleContent.AddView(view);
        }

        public override void SetContentView(View view, ViewGroup.LayoutParams @params)
        {
            sampleContent.AddView(view, @params);
        }
    }
}
