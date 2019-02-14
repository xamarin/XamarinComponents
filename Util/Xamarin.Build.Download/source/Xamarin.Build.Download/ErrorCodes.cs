using System;
namespace Xamarin.Build.Download
{
	public static class ErrorCodes
	{
		public const string DownloadFailed = "XBD001";
		public const string ExtractionFailed = "XBD002";
		public const string DownloadedFileMissing = "XBD003";
		public const string DirectoryCreateFailed = "XBD005";
		public const string DirectoryDeleteFailed = "XBD006";
		public const string ExclusiveLockTimeout = "XBD008";
		public const string PartialDownloadFailed = "XBD009";

		public const string UnknownArchiveType = "XBD010";

		public const string XbdInvalidItemId = "XBD020";
		public const string XbdInvalidUrl = "XBD021";
		public const string XbdInvalidToFile = "XBD022";

		public const string XbdInvalidRange = "XBD030";
		public const string XbdInvalidRangeStart = "XBD031";
		public const string XbdInvalidRangeEnd = "XBD032";

		public const string XbdUnsecureUrl = "XBD040";
	}
}
