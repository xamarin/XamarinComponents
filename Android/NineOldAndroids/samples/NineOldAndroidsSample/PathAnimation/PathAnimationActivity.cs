using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Java.Interop;

using NineOldAndroids.Animation;
using NineOldAndroids.View.Animation;

namespace NineOldAndroidsSample.PathAnimation
{
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "com.yourcompany.nineoldandroids.sample.SAMPLE" })]
    [Activity(Label = "Path Animation", Theme = "@style/Theme.AppCompat")]
    public class PathAnimationActivity : AppCompatActivity
    {
        private Button button;
        private AnimatorProxy buttonProxy;
        private PathEvaluator evaluator = new PathEvaluator();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PathAnimation);

            button = FindViewById<Button>(Resource.Id.button);
            buttonProxy = AnimatorProxy.Wrap(button);

            var density = Resources.DisplayMetrics.Density;

            // Set up the path we're animating along
            var path = new AnimatorPath();
            path.MoveTo(0 * density, 0 * density);
            path.LineTo(0 * density, 300 * density);
            path.CurveTo(100 * density, 0 * density, 
                         300 * density, 900 * density, 
                         400 * density, 500 * density);

            // Set up the animation
            var points = path.Points.Select(p => (Java.Lang.Object)p).ToArray();
            var anim = ObjectAnimator.OfObject(this, "buttonLocation", new PathEvaluator(), points);
            anim.SetDuration(1000);

            button.Click += delegate
            {
                anim.Start();
            };
        }

        /// <summary>
        /// We need this setter to translate between the information the animator
        /// produces (a new "PathPoint" describing the current animated location)
        /// and the information that the button requires (an xy location). The
        /// setter will be called by the ObjectAnimator given the 'buttonLocation'
        /// property string.
        /// </summary>
        [Export("setButtonLocation")]
        public void SetButtonLocation(PathPoint value)
        {
            buttonProxy.TranslationX = value.X;
            buttonProxy.TranslationY = value.Y;
        }
    }
}
