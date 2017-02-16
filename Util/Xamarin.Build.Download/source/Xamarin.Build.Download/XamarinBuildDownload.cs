using System;
using Microsoft.Build.Utilities;

namespace Xamarin.Build.Download
{
	public class XamarinBuildDownload
	{
		public string Id { get; set; }
		public ArchiveKind Kind { get; set; }
		public string Url { get; set; }
		public string Sha1 { get; set; }

		public string ToFile { get; set; }
		public int ExclusiveLockTimeout { get; set; } = 60;

		public string CustomErrorMessage { get; set; }
		public string CustomErrorCode { get; set; }

		public string CacheFile { get; set; }
		public string DestinationDir { get; set; }
	}

	public enum ArchiveKind
	{
		Unknown = 0,
		Zip,
		Tgz,
		Uncompressed,
	}
}
