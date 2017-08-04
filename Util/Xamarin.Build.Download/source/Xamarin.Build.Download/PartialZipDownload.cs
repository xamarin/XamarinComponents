using System;
namespace Xamarin.Build.Download
{
	public class PartialZipDownload
	{
		public string Id { get; set; }
		public string ToFile { get; set; }
		public string Url { get; set; }
		public long RangeStart { get; set; }
		public long RangeEnd { get; set; }
		public string Md5 { get; set; }

		public string CustomErrorMessage { get; set; }
		public string CustomErrorCode { get; set; }
	}
}
