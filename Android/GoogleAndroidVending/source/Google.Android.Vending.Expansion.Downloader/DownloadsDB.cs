using System.Collections.Generic;
using System.Linq;
using Android.App;
using Google.Android.Vending.Licensing;

namespace Google.Android.Vending.Expansion.Downloader
{
	partial class DownloadsDB
	{
		public static DownloadsDB GetDB()
		{
			return DownloadsDB.GetDB(Application.Context);
		}

		public static List<DownloadInfo> GetDownloadsList()
		{
			return GetDB().GetDownloads()?.ToList() ?? new List<DownloadInfo>();
		}
	}

	partial class DownloadInfo
	{
		public DownloadStatus Status
		{
			get { return (DownloadStatus)StatusInternal; }
			set { StatusInternal = (int)value; }
		}

		public APKExpansionPolicy.ExpansionFileType ExpansionFileType
		{
			get { return (APKExpansionPolicy.ExpansionFileType)Index; }
			set { Index = (int)value; }
		}
	}
}
