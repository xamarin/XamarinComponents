using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace PhotoViewSample
{
    [Activity(Label = "Activity Transition Sample")]
    public class ActivityTransitionToSampleActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_transition_to);
        }
    }
}
