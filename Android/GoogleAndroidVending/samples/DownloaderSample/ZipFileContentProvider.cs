using Android.App;
using Android.Content;
using Google.Android.Vending.Expansion.ZipFile;

namespace DownloaderSample
{
	[ContentProvider(new[] { ContentProviderAuthority }, Exported = false)]
	[MetaData(APEZProvider.MetaData.MainVersion, Value = "14")]
	[MetaData(APEZProvider.MetaData.PatchVersion, Value = "14")]
	public class ZipFileContentProvider : APEZProvider
	{
		public const string ContentProviderAuthority = "DownloaderSample.ZipFileContentProvider";

		public override string Authority => ContentProviderAuthority;
	}
}
