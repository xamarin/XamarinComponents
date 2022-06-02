using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Xamarin.Build.Download
{
	public class XamarinBuildGetArchivesToDownload : Task, ILogger
	{
		public ITaskItem [] Archives { get; private set; }
		public ITaskItem [] PartialZipDownloads { get; private set; }

		[Output]
		public ITaskItem [] ArchivesToDownload { get; private set; }

		public string DestinationBase { get; set; }

		public string CacheDirectory { get; set; }

		public bool AllowUnsecureUrls { get; set; }

		public bool BypassValidation { get; set; }

		DownloadUtils downloadUtils;

		public override bool Execute ()
		{
			var results = new List<ITaskItem> ();

			downloadUtils = new DownloadUtils (this, CacheDirectory, BypassValidation);

			var items = downloadUtils.ParseDownloadItems (Archives, AllowUnsecureUrls);

			if (items != null) {
				foreach (var item in items) {
					if (downloadUtils.IsAlreadyDownloaded (item))
						continue;
					var taskItem = new TaskItem (item.Id);
					taskItem.SetMetadata ("Type", "Download");
					taskItem.SetMetadata ("Url", item.Url);
					taskItem.SetMetadata ("CacheFile", item.CacheFile);

					if (!string.IsNullOrEmpty (item.Sha256))
						taskItem.SetMetadata ("Sha256", item.Sha256);

					results.Add (taskItem);
				}
			}

			var partials = downloadUtils.ParsePartialZipDownloadItems (PartialZipDownloads, AllowUnsecureUrls);
			if (partials != null) {
				foreach (var partialZipDownload in partials) {
					if (downloadUtils.IsAlreadyDownloaded (CacheDirectory, partialZipDownload))
						continue;
					var taskItem = new TaskItem (partialZipDownload.Id);
					taskItem.SetMetadata ("Type", "PartialZipDownload");
					taskItem.SetMetadata ("Url", partialZipDownload.Url);
					taskItem.SetMetadata ("RangeStart", partialZipDownload.RangeStart.ToString ());
					taskItem.SetMetadata ("RangeEnd", partialZipDownload.RangeEnd.ToString ());

					if (!string.IsNullOrEmpty (partialZipDownload.Sha256))
						taskItem.SetMetadata ("Sha256", partialZipDownload.Sha256);

					results.Add (taskItem);
				}
			}

			ArchivesToDownload = results.ToArray ();

			return true;
		}

		public void LogCodedError (string code, string message, params object [] messageArgs)
		{
			base.Log.LogError (message, messageArgs);
		}

		public void LogErrorFromException (Exception exception)
		{
			base.Log.LogErrorFromException (exception);
		}
	}
}
