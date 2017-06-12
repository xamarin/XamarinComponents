using Android.Views;
using Android.Widget;

namespace DownloaderSample
{
	/// <summary>
	/// The sample downloader activity.
	/// </summary>
	public partial class SampleDownloaderActivity
	{
		/// <summary>
		/// The average speed text view.
		/// </summary>
		private TextView averageSpeedTextView;

		/// <summary>
		/// The dashboard view.
		/// </summary>
		private View dashboardView;

		/// <summary>
		/// The open wifi settings button.
		/// </summary>
		private Button openWiFiSettingsButton;

		/// <summary>
		/// The pause button.
		/// </summary>
		private Button pauseButton;

		/// <summary>
		/// The progress bar.
		/// </summary>
		private ProgressBar progressBar;

		/// <summary>
		/// The progress fraction text view.
		/// </summary>
		private TextView progressFractionTextView;

		/// <summary>
		/// The progress percent text view.
		/// </summary>
		private TextView progressPercentTextView;

		/// <summary>
		/// The resume on cell data button.
		/// </summary>
		private Button resumeOnCellDataButton;

		/// <summary>
		/// The status text view.
		/// </summary>
		private TextView statusTextView;

		/// <summary>
		/// The time remaining text view.
		/// </summary>
		private TextView timeRemainingTextView;

		/// <summary>
		/// The use cell data view.
		/// </summary>
		private View useCellDataView;

		/// <summary>
		/// Initialize the control variables.
		/// </summary>
		private void InitializeControls()
		{
			this.SetContentView(Resource.Layout.Main);

			this.progressBar = this.FindViewById<ProgressBar>(Resource.Id.progressBar);
			this.statusTextView = this.FindViewById<TextView>(Resource.Id.statusText);
			this.progressFractionTextView = this.FindViewById<TextView>(Resource.Id.progressAsFraction);
			this.progressPercentTextView = this.FindViewById<TextView>(Resource.Id.progressAsPercentage);
			this.averageSpeedTextView = this.FindViewById<TextView>(Resource.Id.progressAverageSpeed);
			this.timeRemainingTextView = this.FindViewById<TextView>(Resource.Id.progressTimeRemaining);
			this.dashboardView = this.FindViewById(Resource.Id.downloaderDashboard);
			this.useCellDataView = this.FindViewById(Resource.Id.approveCellular);
			this.pauseButton = this.FindViewById<Button>(Resource.Id.pauseButton);
			this.openWiFiSettingsButton = this.FindViewById<Button>(Resource.Id.wifiSettingsButton);
			this.resumeOnCellDataButton = this.FindViewById<Button>(Resource.Id.resumeOverCellular);

			this.pauseButton.Click += OnButtonOnClick;
			this.openWiFiSettingsButton.Click += OnOpenWiFiSettingsButtonOnClick;
			this.resumeOnCellDataButton.Click += OnEventHandler;
		}
	}
}
