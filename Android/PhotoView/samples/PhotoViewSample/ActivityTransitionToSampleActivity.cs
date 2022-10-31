using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;

namespace PhotoViewSample
{
    [Activity(Label = "Activity Transition Sample")]
    public class ActivityTransitionToSampleActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_transition_to);

            //var photoView = FindViewById<PhotoView>(Resource.Id.iv_photo);
            //photoView.SetImageResource(Resource.Drawable.wallpaper);
        }
    }
}
