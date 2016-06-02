using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views.Animations;
using Android.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;

using RecyclerViewAnimators.Adapters;
using RecyclerViewAnimators.Animators;

namespace RecyclerViewAnimatorsSample
{
	[Activity (Label = "Adapter Sample")]
	public class AdapterSampleActivity : AppCompatActivity
	{
		public static Dictionary<string, BaseItemAnimator> AnimatorTypes = new Dictionary<string, BaseItemAnimator> {
			{ "FadeIn", new FadeInAnimator (new OvershootInterpolator (1f)) },
			{ "FadeInDown", new FadeInDownAnimator (new OvershootInterpolator (1f)) },
			{ "FadeInUp", new FadeInUpAnimator (new OvershootInterpolator (1f)) },
			{ "FadeInLeft", new FadeInLeftAnimator (new OvershootInterpolator (1f)) },
			{ "FadeInRight", new FadeInRightAnimator (new OvershootInterpolator (1f)) },
			{ "Landing", new LandingAnimator (new OvershootInterpolator (1f)) },
			{ "ScaleIn", new ScaleInAnimator (new OvershootInterpolator (1f)) },
			{ "ScaleInTop", new ScaleInTopAnimator (new OvershootInterpolator (1f)) },
			{ "ScaleInBottom", new ScaleInBottomAnimator (new OvershootInterpolator (1f)) },
			{ "ScaleInLeft",  new ScaleInLeftAnimator (new OvershootInterpolator (1f)) },
			{ "ScaleInRight",  new ScaleInRightAnimator (new OvershootInterpolator (1f)) },
			{ "FlipInTopX" ,  new FlipInTopXAnimator (new OvershootInterpolator (1f)) },
			{ "FlipInBottomX", new FlipInBottomXAnimator (new OvershootInterpolator (1f)) },
			{ "FlipInLeftY", new FlipInLeftYAnimator (new OvershootInterpolator (1f)) },
			{ "FlipInRightY", new FlipInRightYAnimator (new OvershootInterpolator (1f)) },
			{ "SlideInLeft", new SlideInLeftAnimator (new OvershootInterpolator (1f)) },
			{ "SlideInRight", new SlideInRightAnimator (new OvershootInterpolator (1f)) },
			{ "SlideInDown", new SlideInDownAnimator (new OvershootInterpolator (1f)) },
			{ "SlideInUp", new SlideInUpAnimator (new OvershootInterpolator (1f)) },
			{ "OvershootInRight", new OvershootInRightAnimator (1.0f) },
			{ "OvershootInLeft", new OvershootInLeftAnimator (1.0f) }
		};

		private static string[] data = {
			"Apple", "Ball", "Camera", "Day", "Egg", "Foo", "Google", "Hello", "Iron", "Japan", "Coke",
			"Dog", "Cat", "Yahoo", "Sony", "Canon", "Fujitsu", "USA", "Nexus", "LINE", "Haskell", "C++",
			"Java", "Go", "Swift", "Objective-c", "Ruby", "PHP", "Bash", "ksh", "C", "Groovy", "Kotlin"
		};

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_animator_sample);

			var toolbar = FindViewById<Toolbar> (Resource.Id.tool_bar);
			SetSupportActionBar (toolbar);
			SupportActionBar.SetDisplayShowTitleEnabled (false);

			var recyclerView = FindViewById<RecyclerView> (Resource.Id.list);

			if (Intent.GetBooleanExtra ("GRID", true)) {
				recyclerView.SetLayoutManager (new GridLayoutManager (this, 2));
			} else {
				recyclerView.SetLayoutManager (new LinearLayoutManager (this));
			}

			recyclerView.SetItemAnimator (new SlideInLeftAnimator ());

			var adapter = new MainAdapter (this, data.ToList ());
			recyclerView.SetAdapter (adapter);

			var spinner = FindViewById<Spinner> (Resource.Id.spinner);
			var spinnerAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1);
			foreach (var type in AnimatorTypes) {
				spinnerAdapter.Add (type.Key);
			}
			spinner.Adapter = spinnerAdapter;
			spinner.ItemSelected += (sender, e) => {
				recyclerView.SetItemAnimator (AnimatorTypes.Values.ToArray () [e.Position]);
				recyclerView.GetItemAnimator ().AddDuration = 500;
				recyclerView.GetItemAnimator ().RemoveDuration = 500;
			};

			FindViewById (Resource.Id.add).Click += (sender, e) => {
				adapter.Add ("newly added item", 1);
			};

			FindViewById (Resource.Id.del).Click += (sender, e) => {
				adapter.Remove (1);
			};
		}
	}
}
