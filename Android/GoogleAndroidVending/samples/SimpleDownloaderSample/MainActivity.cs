using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;

using Google.Android.Vending.Expansion.Downloader;

using IDownloaderServiceConnection = Google.Android.Vending.Expansion.Downloader.IStub;

namespace SimpleDownloaderSample
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
	public class MainActivity : AppCompatActivity, IDownloaderClient
	{
		private View dashboardView;
		private Button openWiFiSettingsButton;
		private Button pauseButton;
		private ProgressBar progressBar;
		private TextView progressFractionTextView;
		private Button resumeOnCellDataButton;
		private TextView statusTextView;
		private View useCellDataView;
		private IDownloaderService downloaderService;
		private IDownloaderServiceConnection downloaderServiceConnection;
		private DownloaderClientState downloaderState;
		private bool isPaused;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Main);

			progressBar = FindViewById<ProgressBar>(Resource.Id.prgProgress);
			statusTextView = FindViewById<TextView>(Resource.Id.lblStatus);
			progressFractionTextView = FindViewById<TextView>(Resource.Id.lblProgress);
			dashboardView = FindViewById(Resource.Id.dashboard);
			useCellDataView = FindViewById(Resource.Id.approve);
			pauseButton = FindViewById<Button>(Resource.Id.btnPause);
			openWiFiSettingsButton = FindViewById<Button>(Resource.Id.btnWifi);
			resumeOnCellDataButton = FindViewById<Button>(Resource.Id.btnResumeCell);

			pauseButton.Click += delegate
			{
				if (isPaused)
					downloaderService.RequestContinueDownload();
				else
					downloaderService.RequestPauseDownload();
				UpdatePauseButton(!isPaused);
			};
			openWiFiSettingsButton.Click += delegate
			{
				StartActivity(new Intent(Settings.ActionWifiSettings));
			};
			resumeOnCellDataButton.Click += delegate
			{
				downloaderService.SetDownloadFlags(DownloaderServiceFlags.DownloadOverCellular);
				downloaderService.RequestContinueDownload();
				useCellDataView.Visibility = ViewStates.Gone;
			};

			dashboardView.Visibility = ViewStates.Gone;
			useCellDataView.Visibility = ViewStates.Gone;

			var delivered = AreExpansionFilesDelivered();

			if (delivered)
			{
				statusTextView.Text = "Download Complete!";
			}
			else if (!GetExpansionFiles())
			{
				downloaderServiceConnection = DownloaderClientMarshaller.CreateStub(this, typeof(SampleDownloaderService));
			}
		}

		protected override void OnResume()
		{
			if (downloaderServiceConnection != null)
			{
				downloaderServiceConnection.Connect(this);
			}

			base.OnResume();
		}

		protected override void OnStop()
		{
			if (downloaderServiceConnection != null)
			{
				downloaderServiceConnection.Disconnect(this);
			}

			base.OnStop();
		}

		public void OnDownloadProgress(DownloadProgressInfo progress)
		{
			progressBar.Max = (int)(progress.OverallTotal >> 8);
			progressBar.Progress = (int)(progress.OverallProgress >> 8);
			progressFractionTextView.Text = Helpers.GetDownloadProgressString(progress.OverallProgress, progress.OverallTotal);
		}

		public void OnDownloadStateChanged(DownloaderClientState newState)
		{
			if (downloaderState != newState)
			{
				downloaderState = newState;
				statusTextView.Text = Helpers.GetDownloaderStringFromState(this, newState);
			}

			if (newState != DownloaderClientState.Completed)
			{
				dashboardView.Visibility = newState.CanShowProgress() ? ViewStates.Visible : ViewStates.Gone;
				useCellDataView.Visibility = newState.IsWaitingForCellApproval() ? ViewStates.Visible : ViewStates.Gone;
				progressBar.Indeterminate = newState.IsIndeterminate();
				UpdatePauseButton(newState.IsPaused());
			}
			else
			{
				statusTextView.Text = "Download Complete!";
				dashboardView.Visibility = ViewStates.Gone;
				useCellDataView.Visibility = ViewStates.Gone;
			}
		}

		public void OnServiceConnected(Messenger m)
		{
			downloaderService = DownloaderServiceMarshaller.CreateProxy(m);
			downloaderService.OnClientUpdated(downloaderServiceConnection.GetMessenger());
		}

		private bool AreExpansionFilesDelivered()
		{
			var downloads = DownloadsDB.GetDB().GetDownloads() ?? new DownloadInfo[0];
			return downloads.Any() && downloads.All(x => Helpers.DoesFileExist(this, x.FileName, x.TotalBytes, false));
		}

		private bool GetExpansionFiles()
		{
			bool result = false;

			// Build the intent that launches this activity.
			var launchIntent = Intent;
			var intent = new Intent(this, typeof(MainActivity));
			intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTop);
			intent.SetAction(launchIntent.Action);

			if (launchIntent.Categories != null)
			{
				foreach (string category in launchIntent.Categories)
				{
					intent.AddCategory(category);
				}
			}

			// Build PendingIntent used to open this activity when user 
			// taps the notification.
			var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent);

			// Request to start the download
			var startResult = DownloaderService.StartDownloadServiceIfRequired(this, pendingIntent, typeof(SampleDownloaderService));

			// The DownloaderService has started downloading the files, 
			// show progress otherwise, the download is not needed so  we 
			// fall through to starting the actual app.
			if (startResult != DownloadServiceRequirement.NoDownloadRequired)
			{
				downloaderServiceConnection = DownloaderClientMarshaller.CreateStub(this, typeof(SampleDownloaderService));

				result = true;
			}

			return result;
		}

		private void UpdatePauseButton(bool paused)
		{
			isPaused = paused;
			pauseButton.SetText(paused ? Resource.String.text_button_resume : Resource.String.text_button_pause);
		}
	}
}
