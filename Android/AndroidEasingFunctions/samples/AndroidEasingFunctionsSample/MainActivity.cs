using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using NineOldAndroids.Animation;

using AndroidEasingFunctions;

namespace AndroidEasingFunctionsSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
	public class MainActivity : AppCompatActivity
	{
		private ListView easingList;
		private EasingAdapter adapter;
		private View target;
		private DrawView history;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_main);

			easingList = FindViewById<ListView> (Resource.Id.easing_list);
			adapter = new EasingAdapter (this);
			easingList.Adapter = adapter;
			target = FindViewById (Resource.Id.target);
			history = FindViewById<DrawView> (Resource.Id.history);
			easingList.ItemClick += (sender, e) => {
				history.Clear ();

				var s = (Skill)e.View.Tag;

				var set = new AnimatorSet ();
				target.TranslationX = 0;
				target.TranslationY = 0;
				set.PlayTogether (
					Glider.Glide (s, 1200, ObjectAnimator.OfFloat (target, "translationY", 0, DrawView.DipToPixels (this, -(160 - 3))), args => {
						history.DrawPoint (args.Time, args.Duration, args.Value - DrawView.DipToPixels (this, 60));
					}));
				set.SetDuration (1200);
				set.Start ();
			};
		}
	}
}
