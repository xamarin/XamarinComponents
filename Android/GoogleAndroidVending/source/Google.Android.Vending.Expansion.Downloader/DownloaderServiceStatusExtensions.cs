namespace Google.Android.Vending.Expansion.Downloader
{
	public static class DownloaderServiceStatusExtensions
	{
		public static bool IsClientError(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusClientError((int)status);

		public static bool IsCompleted(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusCompleted((int)status);

		public static bool IsError(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusError((int)status);

		public static bool IsInformational(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusInformational((int)status);

		public static bool IsRedirect(this DownloaderServiceStatus status)
			=> (int)status >= 300 && (int)status <= 399;

		public static bool IsServerError(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusServerError((int)status);

		public static bool IsSuccess(this DownloaderServiceStatus status)
			=> DownloaderService.IsStatusSuccess((int)status);
	}
}
