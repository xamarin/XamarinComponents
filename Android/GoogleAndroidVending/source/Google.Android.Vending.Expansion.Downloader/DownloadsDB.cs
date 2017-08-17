using Android.App;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloadsDB
	{
		public static DownloadsDB GetDB()
		{
			return DownloadsDB.GetDB(Application.Context);
		}
	}
}
