using Google.Android.Vending.Expansion.Downloader;

namespace SimpleDownloaderSample
{
	public static class DownloaderClientStateExtensions
	{
		public static bool IsIndeterminate(this DownloaderClientState newState)
		{
			bool indeterminate = false;
			switch (newState)
			{
				case DownloaderClientState.Idle:
				case DownloaderClientState.Connecting:
				case DownloaderClientState.FetchingUrl:
					indeterminate = true;
					break;
			}
			return indeterminate;
		}

		public static bool IsPaused(this DownloaderClientState newState)
		{
			bool paused = true;
			switch (newState)
			{
				case DownloaderClientState.Idle:
				case DownloaderClientState.Connecting:
				case DownloaderClientState.FetchingUrl:
				case DownloaderClientState.Downloading:
					paused = false;
					break;
			}
			return paused;
		}

		public static bool CanShowProgress(this DownloaderClientState newState)
		{
			bool showDashboard = true;
			switch (newState)
			{
				case DownloaderClientState.Failed:
				case DownloaderClientState.FailedCanceled:
				case DownloaderClientState.FailedFetchingUrl:
				case DownloaderClientState.FailedUnlicensed:
				case DownloaderClientState.PausedNeedCellularPermission:
				case DownloaderClientState.PausedWifiDisabledNeedCellularPermission:
					showDashboard = false;
					break;
			}
			return showDashboard;
		}

		public static bool IsWaitingForCellApproval(this DownloaderClientState newState)
		{
			bool showCellMessage = false;
			switch (newState)
			{
				case DownloaderClientState.PausedNeedCellularPermission:
				case DownloaderClientState.PausedWifiDisabledNeedCellularPermission:
					showCellMessage = true;
					break;
			}
			return showCellMessage;
		}
	}
}
