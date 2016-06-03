using System.Threading.Tasks;
using Android.App;
using Android.OS;

using AnimatedLoadingViews;

namespace AnimatedCircleLoadingViewSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private AnimatedCircleLoadingView loading;

		protected  override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			loading = FindViewById<AnimatedCircleLoadingView> (Resource.Id.circle_loading_view);

			// prepare for the work
			loading.StartDeterminate ();

			// do the work
			await Task.Delay (1500);
			for (int i = 0; i <= 100; i++) {
				await Task.Delay (65);
				loading.SetPercent (i);
			}
		}
	}
}
