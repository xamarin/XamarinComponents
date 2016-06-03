using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;

using ProgressBars;

namespace NumberProgressBarSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		private CancellationTokenSource cts;

		protected override async void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.activity_main);
		
			var numberbar1 = FindViewById<NumberProgressBar> (Resource.Id.numberbar1);

			try {
				// start progress
				cts = new CancellationTokenSource ();
				await Task.Delay (1000, cts.Token);
				while (!cts.IsCancellationRequested) {
				
					// update
					numberbar1.IncrementProgressBy (1);

					// restart
					await Task.Delay (100, cts.Token);
					if (numberbar1.Progress >= 100) {
						numberbar1.Progress = 0;
						await Task.Delay (1000, cts.Token);
					}
				}
			} catch (TaskCanceledException) {
				
			}
		}

		protected override void OnDestroy ()
		{
			// cancel progress
			cts.Cancel ();

			base.OnDestroy ();
		}
	}
}
