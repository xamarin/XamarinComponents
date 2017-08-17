using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Support.V7.App;
using Android.Views;

using Google.Android.Vending.Expansion.Downloader;

using IDownloaderServiceConnection = Google.Android.Vending.Expansion.Downloader.IStub;
using Debug = System.Diagnostics.Debug;

namespace DownloaderSample
{
	/// <summary>
	/// The sample downloader activity.
	/// </summary>
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true, Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
	public partial class SampleDownloaderActivity : AppCompatActivity, IDownloaderClient
	{
		/// <summary>
		/// The downloader service.
		/// </summary>
		private IDownloaderService downloaderService;

		/// <summary>
		/// The downloader service connection.
		/// </summary>
		private IDownloaderServiceConnection downloaderServiceConnection;

		/// <summary>
		/// The downloader state.
		/// </summary>
		private DownloaderClientState downloaderState;

		/// <summary>
		/// The is paused.
		/// </summary>
		private bool isPaused;

		/// <summary>
		/// Sets the state of the various controls based on the progressinfo 
		/// object sent from the downloader service.
		/// </summary>
		/// <param name="progress">
		/// The progressinfo object sent from the downloader service.
		/// </param>
		public void OnDownloadProgress(DownloadProgressInfo progress)
		{
			averageSpeedTextView.Text = string.Format("{0} Kb/s", Helpers.GetSpeedString(progress.CurrentSpeed));
			timeRemainingTextView.Text = string.Format("Time remaining: {0}", Helpers.GetTimeRemaining(progress.TimeRemaining));
			progressBar.Max = (int)(progress.OverallTotal >> 8);
			progressBar.Progress = (int)(progress.OverallProgress >> 8);
			progressPercentTextView.Text = string.Format("{0}%", progress.OverallProgress * 100 / progress.OverallTotal);
			progressFractionTextView.Text = Helpers.GetDownloadProgressString(progress.OverallProgress, progress.OverallTotal);
		}

		/// <summary>
		/// The download state should trigger changes in the UI.
		/// It may be useful to show the state as being indeterminate at times.  
		/// </summary>
		/// <param name="newState">
		/// The new state.
		/// </param>
		public void OnDownloadStateChanged(DownloaderClientState newState)
		{
			Debug.WriteLine("newState: " + newState);

			if (downloaderState != newState)
			{
				downloaderState = newState;
				statusTextView.Text = Helpers.GetDownloaderStringFromState(this, newState);
			}

			bool showDashboard = true;
			bool showCellMessage = false;
			bool paused = false;
			bool indeterminate = true;
			switch (newState)
			{
				case DownloaderClientState.Idle:
				case DownloaderClientState.Connecting:
				case DownloaderClientState.FetchingUrl:
					break;
				case DownloaderClientState.Downloading:
					indeterminate = false;
					break;
				case DownloaderClientState.Failed:
				case DownloaderClientState.FailedCanceled:
				case DownloaderClientState.FailedFetchingUrl:
				case DownloaderClientState.FailedUnlicensed:
					paused = true;
					showDashboard = false;
					indeterminate = false;
					break;
				case DownloaderClientState.PausedNeedCellularPermission:
				case DownloaderClientState.PausedWifiDisabledNeedCellularPermission:
					showDashboard = false;
					paused = true;
					indeterminate = false;
					showCellMessage = true;
					break;
				case DownloaderClientState.PausedByRequest:
					paused = true;
					indeterminate = false;
					break;
				case DownloaderClientState.PausedRoaming:
				case DownloaderClientState.PausedSdCardUnavailable:
					paused = true;
					indeterminate = false;
					break;
				default:
					paused = true;
					break;
			}

			if (newState != DownloaderClientState.Completed)
			{
				dashboardView.Visibility = showDashboard ? ViewStates.Visible : ViewStates.Gone;
				useCellDataView.Visibility = showCellMessage ? ViewStates.Visible : ViewStates.Gone;
				progressBar.Indeterminate = indeterminate;
				UpdatePauseButton(paused);
			}
			else
			{
				ValidateExpansionFiles();
			}
		}

		/// <summary>
		/// Create the remote service and marshaler.
		/// </summary>
		/// <remarks>
		/// This is how we pass the client information back to the service so 
		/// the client can be properly notified of changes. 
		/// Do this every time we reconnect to the service.
		/// </remarks>
		/// <param name="m">
		/// The messenger to use.
		/// </param>
		public void OnServiceConnected(Messenger m)
		{
			downloaderService = DownloaderServiceMarshaller.CreateProxy(m);
			downloaderService.OnClientUpdated(downloaderServiceConnection.GetMessenger());
		}

		/// <summary>
		/// Called when the activity is first created; we wouldn't create a 
		/// layout in the case where we have the file and are moving to another
		/// activity without downloading.
		/// </summary>
		/// <param name="savedInstanceState">
		/// The saved instance state.
		/// </param>
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Before we do anything, are the files we expect already here and 
			// delivered (presumably by Market) 
			// For free titles, this is probably worth doing. (so no Market 
			// request is necessary)
			var delivered = AreExpansionFilesDelivered();

			if (delivered)
			{
				StartActivity(typeof(ZipTestActivity));
				Finish();
			}

			if (!GetExpansionFiles())
			{
				InitializeDownloadUi();
			}
		}

		/// <summary>
		/// Re-connect the stub to our service on resume.
		/// </summary>
		protected override void OnResume()
		{
			if (downloaderServiceConnection != null)
			{
				downloaderServiceConnection.Connect(this);
			}

			base.OnResume();
		}

		/// <summary>
		/// Disconnect the stub from our service on stop.
		/// </summary>
		protected override void OnStop()
		{
			if (downloaderServiceConnection != null)
			{
				downloaderServiceConnection.Disconnect(this);
			}

			base.OnStop();
		}

		/// <summary>
		/// Go through each of the Expansion APK files defined in the project 
		/// and determine if the files are present and match the required size. 
		/// </summary>
		/// <remarks>
		/// Free applications should definitely consider doing this, as this 
		/// allows the application to be launched for the first time without
		/// having a network connection present.
		/// Paid applications that use LVL should probably do at least one LVL 
		/// check that requires the network to be present, so this is not as
		/// necessary.
		/// </remarks>
		/// <returns>
		/// True if they are present, otherwise False;
		/// </returns>
		private bool AreExpansionFilesDelivered()
		{
			var downloads = DownloadsDB.GetDB().GetDownloads() ?? new DownloadInfo[0];
			return downloads.Any() && downloads.All(x => Helpers.DoesFileExist(this, x.FileName, x.TotalBytes, false));
		}

		/// <summary>
		/// The do validate zip files.
		/// </summary>
		/// <param name="state">
		/// The state.
		/// </param>
		private void DoValidateZipFiles(object state)
		{
			var downloadInfos = DownloadsDB.GetDB().GetDownloads() ?? new DownloadInfo[0];
			var downloads = downloadInfos.Select(x => Helpers.GenerateSaveFileName(this, x.FileName)).ToArray();

			var result = downloads.Any();
			var progress = downloads.Length;
			foreach (var file in downloads)
			{
				progress--;
				result = result && IsValidZipFile(file);
				RunOnUiThread(
					delegate
						{
							OnDownloadProgress(new DownloadProgressInfo(downloads.Length, downloads.Length - progress, 0, 0));
						});
			}

			RunOnUiThread(
				delegate
					{
						pauseButton.Click += delegate
							{
								Finish();
								StartActivity(typeof(ZipTestActivity));
							};

						dashboardView.Visibility = ViewStates.Visible;
						useCellDataView.Visibility = ViewStates.Gone;

						if (result)
						{
							statusTextView.SetText(Resource.String.text_validation_complete);
							pauseButton.SetText(Android.Resource.String.Ok);
						}
						else
						{
							statusTextView.SetText(Resource.String.text_validation_failed);
							pauseButton.SetText(Android.Resource.String.Cancel);
						}
					});
		}

		/// <summary>
		/// The get expansion files.
		/// </summary>
		/// <returns>
		/// The get expansion files.
		/// </returns>
		private bool GetExpansionFiles()
		{
			bool result = false;

			try
			{
				// Build the intent that launches this activity.
				Intent launchIntent = Intent;
				var intent = new Intent(this, typeof(SampleDownloaderActivity));
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
				PendingIntent pendingIntent = PendingIntent.GetActivity(
					this, 0, intent, PendingIntentFlags.UpdateCurrent);

				// Request to start the download
				DownloadServiceRequirement startResult = DownloaderService.StartDownloadServiceIfRequired(
					this, pendingIntent, typeof(SampleDownloaderService));

				// The DownloaderService has started downloading the files, 
				// show progress otherwise, the download is not needed so  we 
				// fall through to starting the actual app.
				if (startResult != DownloadServiceRequirement.NoDownloadRequired)
				{
					InitializeDownloadUi();
					result = true;
				}
			}
			catch (PackageManager.NameNotFoundException e)
			{
				Debug.WriteLine("Cannot find own package! MAYDAY!");
				e.PrintStackTrace();
			}

			return result;
		}

		/// <summary>
		/// If the download isn't present, we initialize the download UI. This ties
		/// all of the controls into the remote service calls.
		/// </summary>
		private void InitializeDownloadUi()
		{
			InitializeControls();
			downloaderServiceConnection = DownloaderClientMarshaller.CreateStub(
				this, typeof(SampleDownloaderService));
		}

		/// <summary>
		/// The is valid zip file.
		/// </summary>
		/// <param name="filename">
		/// The filename.
		/// </param>
		/// <returns>
		/// The is valid zip file.
		/// </returns>
		private bool IsValidZipFile(string filename)
		{
			try
			{
				using (var zip = ZipFile.OpenRead(filename))
				{
					return zip != null;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// The on button on click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void OnButtonOnClick(object sender, EventArgs e)
		{
			if (isPaused)
			{
				downloaderService.RequestContinueDownload();
			}
			else
			{
				downloaderService.RequestPauseDownload();
			}

			UpdatePauseButton(!isPaused);
		}

		/// <summary>
		/// The on event handler.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		private void OnEventHandler(object sender, EventArgs args)
		{
			downloaderService.SetDownloadFlags(DownloaderServiceFlags.DownloadOverCellular);
			downloaderService.RequestContinueDownload();
			useCellDataView.Visibility = ViewStates.Gone;
		}

		/// <summary>
		/// The on open wi fi settings button on click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void OnOpenWiFiSettingsButtonOnClick(object sender, EventArgs e)
		{
			StartActivity(new Intent(Settings.ActionWifiSettings));
		}

		/// <summary>
		/// Update the pause button.
		/// </summary>
		/// <param name="paused">
		/// Is the download paused.
		/// </param>
		private void UpdatePauseButton(bool paused)
		{
			isPaused = paused;
			int stringResourceId = paused ? Resource.String.text_button_resume : Resource.String.text_button_pause;
			pauseButton.SetText(stringResourceId);
		}

		/// <summary>
		/// Perfom a check to see if the expansion files are vanid zip files.
		/// </summary>
		private void ValidateExpansionFiles()
		{
			// Pre execute
			dashboardView.Visibility = ViewStates.Visible;
			useCellDataView.Visibility = ViewStates.Gone;
			statusTextView.SetText(Resource.String.text_verifying_download);
			pauseButton.SetText(Resource.String.text_button_cancel_verify);

			ThreadPool.QueueUserWorkItem(DoValidateZipFiles);
		}
	}
}
