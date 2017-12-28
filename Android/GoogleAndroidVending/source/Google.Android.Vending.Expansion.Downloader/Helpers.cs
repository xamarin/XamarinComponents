using System;
using Android.App;
using Android.Content;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class Helpers
	{
		public static string GetDownloaderStringFromState(Context context, DownloaderClientState state)
		{
			return context.GetString(GetDownloaderStringResourceIdFromState(state));
		}

		[Obsolete("Use GetDownloaderStringResourceIdFromState instead.")]
		public static int GetDownloaderStringFromState(DownloaderClientState state)
		{
			return GetDownloaderStringResourceIdFromState(state);
		}
	}
}
