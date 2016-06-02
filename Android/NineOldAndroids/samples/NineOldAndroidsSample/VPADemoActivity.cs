using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using NineOldAndroids.View;

namespace NineOldAndroidsSample
{
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "com.yourcompany.nineoldandroids.sample.SAMPLE" })]
    [Activity(Label = "View Property Animator", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class VPADemoActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ViewPropertyAnimator);

            var container = FindViewById<LinearLayout>(Resource.Id.container);
            var animatingButton = FindViewById<Button>(Resource.Id.animatingButton);

            FindViewById(Resource.Id.fadeOut).Click += delegate
            {
                ViewPropertyAnimator.Animate(animatingButton).Alpha(0);
            };

            FindViewById(Resource.Id.fadeIn).Click += delegate
            {
                ViewPropertyAnimator.Animate(animatingButton).Alpha(1);
            };

            FindViewById(Resource.Id.moveOver).Click += delegate
            {
                int xValue = container.Width - animatingButton.Width;
                int yValue = container.Height - animatingButton.Height;
                ViewPropertyAnimator.Animate(animatingButton).X(xValue).Y(yValue);
            };

            FindViewById(Resource.Id.moveBack).Click += delegate
            {
                ViewPropertyAnimator.Animate(animatingButton).X(0).Y(0);
            };

            FindViewById(Resource.Id.rotate).Click += delegate
            {
                ViewPropertyAnimator.Animate(animatingButton).RotationYBy(720);
            };

            // Set long default duration for the animator, for the purposes of this demo
            ViewPropertyAnimator.Animate(animatingButton).SetDuration(2000);
        }
    }
}
