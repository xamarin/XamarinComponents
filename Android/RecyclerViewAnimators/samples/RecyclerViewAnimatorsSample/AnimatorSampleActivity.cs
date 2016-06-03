using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views.Animations;

using RecyclerViewAnimators.Adapters;
using RecyclerViewAnimators.Animators;

namespace RecyclerViewAnimatorsSample
{
	[Activity (Label = "Animator Sample")]
	public class AnimatorSampleActivity : AppCompatActivity
	{
		private static readonly string[] data = {
			"Apple", "Ball", "Camera", "Day", "Egg", "Foo", "Google", "Hello", "Iron", "Japan", "Coke",
			"Dog", "Cat", "Yahoo", "Sony", "Canon", "Fujitsu", "USA", "Nexus", "LINE", "Haskell", "C++",
			"Java", "Go", "Swift", "Objective-c", "Ruby", "PHP", "Bash", "ksh", "C", "Groovy", "Kotlin",
			"Chip", "Japan", "U.S.A", "San Francisco", "Paris", "Tokyo", "Silicon Valley", "London",
			"Spain", "China", "Taiwan", "Asia", "New York", "France", "Kyoto", "Android", "Google", "C#",
			"iPhone", "iPad", "iPod", "Wasabeef", "Xamarin", "South Africa", "Cape Town", "Microsoft"
		};

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_adapter_sample);

			var recyclerView = FindViewById<RecyclerView> (Resource.Id.list);

			if (Intent.GetBooleanExtra ("GRID", true)) {
				recyclerView.SetLayoutManager (new GridLayoutManager (this, 2));
			} else {
				recyclerView.SetLayoutManager (new LinearLayoutManager (this));
			}

			recyclerView.SetItemAnimator (new FadeInAnimator ());
			var adapter = new MainAdapter (this, data.ToList ());
			var alphaAdapter = new AlphaInAnimationAdapter (adapter);
			var scaleAdapter = new ScaleInAnimationAdapter (alphaAdapter);
			scaleAdapter.SetFirstOnly (false);
			scaleAdapter.SetInterpolator (new OvershootInterpolator ());
			recyclerView.SetAdapter (scaleAdapter);
		}
	}
}
