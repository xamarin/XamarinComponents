using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Fragment = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;

using ElasticProgressBar;

namespace ElasticProgressBarSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme.NoActionBar")]
	public class MainActivity : AppCompatActivity
	{
		private ElasticDownloadView elasticDownloadView;
		private CoordinatorLayout coordinatorLayout;
		private FloatingActionButton fabStart;
		private FloatingActionButton fabCancel;

		private CancellationTokenSource cts;

		private bool firstRun = true;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// VARIOUS OPTIONS (before creation)
			// OptionView.BackgroundColorSquare = Resource.Color.blue;
			// OptionView.NoBackground = true;
			// OptionView.ColorCloud = Resource.Color.red;
			// OptionView.ColorProgressBar = Resource.Color.red;
			// OptionView.ColorProgressBarInProgress = Resource.Color.darkblue;
			// OptionView.ColorProgressBarText = Resource.Color.darkblue;
			// OptionView.ColorSuccess = Resource.Color.darkblue;
			// OptionView.ColorFail = Resource.Color.orange;
			// OptionView.NoIntro = true;

			SetContentView (Resource.Layout.activity_main);
		
			SetSupportActionBar (FindViewById<Toolbar> (Resource.Id.toolbar));

			coordinatorLayout = FindViewById<CoordinatorLayout> (Resource.Id.coordinatorLayout);
			elasticDownloadView = FindViewById<ElasticDownloadView> (Resource.Id.elastic_download_view);

			fabStart = FindViewById<FloatingActionButton> (Resource.Id.fabStart);
			fabStart.Click += StartProgress;

			fabCancel = FindViewById<FloatingActionButton> (Resource.Id.fabCancel);
			fabCancel.Visibility = ViewStates.Invisible;
			fabCancel.Click += CancelProgress;
		}

		private async void StartProgress (object sender, EventArgs e)
		{
			Snackbar.Make (coordinatorLayout, "Progress Bar Started!", Snackbar.LengthLong).Show ();
			fabCancel.Visibility = ViewStates.Visible;
			fabStart.Visibility = ViewStates.Invisible;

			// cancel any previous tasks
			if (cts != null) {
				cts.Cancel ();
			}
			cts = new CancellationTokenSource ();

			try {
				elasticDownloadView.Progress = 0;

				// intro delay
				if (firstRun) {
					firstRun = false;
					elasticDownloadView.StartIntro ();
					await Task.Delay (1000, cts.Token);
				}

				// progress
				var progress = 0;
				while (progress < 100) {
					await Task.Delay (1000, cts.Token);
					progress += 10;
					elasticDownloadView.Progress = progress;
				}

				// finish
				elasticDownloadView.Success ();
			} catch (Exception) {
				// fail
				elasticDownloadView.Fail ();
			}
			cts = null;
		}

		private void CancelProgress (object sender, EventArgs e)
		{
			Snackbar.Make (coordinatorLayout, "Progress Bar Reloaded!", Snackbar.LengthLong).Show ();
			fabCancel.Visibility = ViewStates.Invisible;
			fabStart.Visibility = ViewStates.Visible;

			// cancel any previous tasks
			if (cts != null) {
				cts.Cancel ();
			} else {
				elasticDownloadView.Progress = 0;
			}
		}
	}
}
