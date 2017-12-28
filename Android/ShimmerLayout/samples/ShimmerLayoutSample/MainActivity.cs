using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Supercharge;

namespace ShimmerLayoutSample
{
	[Activity(MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			var shimmerLayout = FindViewById<ShimmerLayout>(Resource.Id.shimmer_layout);
			shimmerLayout.StartShimmerAnimation();

			var shimmerText = FindViewById<ShimmerLayout>(Resource.Id.shimmer_text);
			shimmerText.StartShimmerAnimation();
		}
	}
}
