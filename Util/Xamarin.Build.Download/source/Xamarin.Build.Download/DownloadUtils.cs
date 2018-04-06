﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Xamarin.Build.Download
{
	public class DownloadUtils
	{
		public DownloadUtils (ILogger logger, string cacheDir)
		{
			Log = logger;
			CacheDir = GetCacheDir (cacheDir);
		}

		public ILogger Log { get; private set; }
		public string CacheDir { get; private set; }

		public IEnumerable<XamarinBuildDownload> ParseDownloadItems (ITaskItem[] items)
		{
			if (items == null || items.Length <= 0)
				return new List<XamarinBuildDownload> ();
			
			var results = new List<XamarinBuildDownload> ();

			foreach (var item in items) {
				var xbd = new XamarinBuildDownload ();

				xbd.Id = item.ItemSpec;
				if (!ValidateId (xbd.Id)) {
					Log.LogCodedError (ErrorCodes.XbdInvalidItemId, "Invalid item ID {0}", xbd.Id);
					continue;
				}
				xbd.Url = item.GetMetadata ("Url");
				if (string.IsNullOrEmpty (xbd.Url)) {
					Log.LogCodedError (ErrorCodes.XbdInvalidUrl, "Missing required Url metadata on item {0}", item.ItemSpec);
					continue;
				}
				xbd.Sha1 = item.GetMetadata ("Sha1");
				xbd.Kind = GetKind (xbd.Url, item.GetMetadata ("Kind"));
				if (xbd.Kind == ArchiveKind.Unknown) {
					//TODO we may be able to determine the kind from the server response
					continue;
				}

				xbd.CustomErrorCode = item.GetMetadata ("CustomErrorCode");
				xbd.CustomErrorMessage = item.GetMetadata ("CustomErrorMessage");

				// By default, use the kind (tgz or zip) as the file extension for the cache file
				var cacheFileExt = xbd.Kind.ToString ().ToLower ();

				// If we have an uncompressed file specified (move file instead of decompressing it)
				if (xbd.Kind == ArchiveKind.Uncompressed) {
					// Get the filename
					xbd.ToFile = item.GetMetadata ("ToFile");
					// If we have a tofile set, try and grab its extension
					if (!string.IsNullOrEmpty (xbd.ToFile))
						cacheFileExt = Path.GetExtension (xbd.ToFile)?.ToLower () ?? string.Empty;
				}

				xbd.CacheFile = Path.Combine (CacheDir, item.ItemSpec + "." + cacheFileExt);
				xbd.DestinationDir = Path.GetFullPath (Path.Combine (CacheDir, item.ItemSpec));

				int lockTimeout = 60;
				if (int.TryParse (item.GetMetadata ("ExclusiveLockTimeout"), out lockTimeout))
					xbd.ExclusiveLockTimeout = lockTimeout;

				results.Add (xbd);
			}

			// Deduplicate possible results by their Id which should be unique always for the given archive
			return results.GroupBy (item => item.Id).Select ((kvp) => kvp.FirstOrDefault ()).ToArray ();
		}

		public List<PartialZipDownload> ParsePartialZipDownloadItems (ITaskItem [] items)
		{
			if (items == null || items.Length <= 0)
				return new List<PartialZipDownload> ();
			
			var result = new List<PartialZipDownload> ();

			foreach (var part in items) {

				var id = part.ItemSpec;
				if (!DownloadUtils.ValidateId (id)) {
					Log.LogCodedError (ErrorCodes.XbdInvalidItemId, "Invalid item ID {0}", id);
					continue;
				}

				var toFile = part.GetMetadata ("ToFile");
				if (string.IsNullOrEmpty (toFile)) {
					Log.LogCodedError (ErrorCodes.XbdInvalidToFile, "Invalid or missing required ToFile metadata on item {0}", id);
					continue;
				}
				var url = part.GetMetadata ("Url");
				if (string.IsNullOrEmpty (url)) {
					Log.LogCodedError (ErrorCodes.XbdInvalidUrl, "Missing required Url metadata on item {0}", id);
					continue;
				}

				var md5 = part.GetMetadata ("Md5");

				long rangeStart = -1L;
				long.TryParse (part.GetMetadata ("RangeStart"), out rangeStart);
				if (rangeStart < 0) {
					Log.LogCodedError (ErrorCodes.XbdInvalidRangeStart, "Invalid or Missing required RangeStart metadata on item {0}", id);
					continue;
				}

				long rangeEnd = -1L;
				long.TryParse (part.GetMetadata ("RangeEnd"), out rangeEnd);
				if (rangeEnd < 0) {
					Log.LogCodedError (ErrorCodes.XbdInvalidRangeEnd, "Invalid or Missing required RangeEnd metadata on item {0}", id);
					continue;
				}

				if (rangeEnd <= rangeStart) {
					Log.LogCodedError (ErrorCodes.XbdInvalidRange, "Invalid RangeStart and RangeEnd values, RangeEnd cannot be less than or equal to RangeStart, on item {0}", id);
					continue;
				}

				var customErrorMsg = part.GetMetadata ("CustomErrorMessage");
				var customErrorCode = part.GetMetadata ("CustomErrorCode");

				result.Add (new PartialZipDownload {
					Id = id,
					ToFile = toFile,
					Url = url,
					Md5 = md5,
					RangeStart = rangeStart,
					RangeEnd = rangeEnd,
					CustomErrorMessage = customErrorMsg,
					CustomErrorCode = customErrorCode,
				});
			}

			// Deduplicate multiple id's of the same value
			var uniqueParts = result.GroupBy (p => p.Id).Select (kvp => kvp.FirstOrDefault ());

			return uniqueParts.ToList ();
		}

		public static string GetCacheDir (string overrideCacheDir = null)
		{
			string cacheDir = overrideCacheDir;

			if (string.IsNullOrEmpty (cacheDir)) {
				if (Platform.IsMac) {
					cacheDir = Path.Combine (
						Environment.GetFolderPath (Environment.SpecialFolder.Personal),
						"Library", "Caches", "XamarinBuildDownload"
					);
				} else {
					cacheDir = Path.Combine (
						Environment.GetFolderPath (Environment.SpecialFolder.LocalApplicationData),
						"XamarinBuildDownloadCache"
					);
				}
			}

			cacheDir = Path.GetFullPath (cacheDir);

			Directory.CreateDirectory (cacheDir);

			return cacheDir;
		}

		//Format is ID-VERSION
		//ID:
		// * must start with a letter
		// * may contain letters, periods, spaces and underscores
		// * must end with a letter or number
		//VERSION:
		// * components separated by periods
		// * each component must consist of letters and numbers
		//
		static readonly Regex idRegex = new Regex (
			@"[A-Za-z]+[A-Za-z\d\._]*[A-Za-z\d]+-[A-Za-z\d]+(\.[A-Za-z\d]+)*(/[A-Za-z]+[A-Za-z\d\._]*[A-Za-z\d]+)?"
			,
			RegexOptions.Compiled
		);

		public bool IsAlreadyDownloaded (XamarinBuildDownload xbd)
		{
			// Skip extraction if the file is already in place
			var flagFile = xbd.DestinationDir + ".unpacked";
			return File.Exists (flagFile) || File.Exists (xbd.CacheFile);
		}

		public bool IsAlreadyDownloaded (string cacheDirectory, PartialZipDownload partialZipDownload)
		{
			return File.Exists (Path.Combine (cacheDirectory, partialZipDownload.Id, partialZipDownload.ToFile));
		}

		public static bool ValidateId (string id)
		{
			try {
				var match = idRegex.Match (id);
				return match.Length == id.Length;
			} catch {
				return false;
			}
		}

		public ArchiveKind GetKind (string urlMetadata, string kindMetadata)
		{

			if (kindMetadata != null) {
				ArchiveKind parsed;
				if (Enum.TryParse (kindMetadata, out parsed) && parsed != ArchiveKind.Unknown) {
					return parsed;
				}
				Log.LogCodedError (ErrorCodes.UnknownArchiveType, "Unknown archive kind '{0}' for '{1}'", kindMetadata, urlMetadata);
				return ArchiveKind.Unknown;
			}

			if (urlMetadata.EndsWith (".zip", StringComparison.OrdinalIgnoreCase)
				|| urlMetadata.EndsWith (".nupkg", StringComparison.OrdinalIgnoreCase)) {
				return ArchiveKind.Zip;
			}

			if (urlMetadata.EndsWith (".tgz", StringComparison.OrdinalIgnoreCase)
				|| urlMetadata.EndsWith (".tar.gz", StringComparison.OrdinalIgnoreCase)) {
				return ArchiveKind.Tgz;
			}

			Log.LogCodedError (ErrorCodes.UnknownArchiveType, "Unable to determine kind of archive '{0}'", urlMetadata);
			return ArchiveKind.Unknown;
		}

        public static void TouchFile (string file)
        {
            if (!File.Exists(file)) {
                using (var f = File.Open(file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    f.Close();
            }

            File.SetLastWriteTimeUtc(file, DateTime.UtcNow);
        }

        public static bool CopyFileWithRetry (string source, string dest, TaskLoggingHelper log)
        {
            for (int i = 0; i < 20; i++)
            {
                try
                {
                    File.Copy(source, dest, true);
                    return true;
                }
                catch
                {
                    log.LogMessage("Failed to copy Assembly to Temp File, waiting to retry...");
                    Thread.Sleep(250);
                }
            }

            log.LogError("Failed to copy file: {0} to {1}", source, dest);
            return false;
        }

		public static Stream ObtainExclusiveFileLock (string file, CancellationToken cancelToken, TimeSpan timeout, TaskLoggingHelper log)
		{
			var linkedCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource (
				cancelToken,
				 new CancellationTokenSource (timeout).Token);
			
			while (!linkedCancelTokenSource.IsCancellationRequested) {
				try {
                    log.LogMessage("Trying to obtain exclusive lock on: {0}", file);
					var lockStream = File.Open (file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    log.LogMessage("Obtained exclusive lock on: {0}", file);
                    return lockStream;
				} catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException) {
                    log?.LogMessage("Waiting for exclusive lock on: {0}", file);
                    Thread.Sleep (100);
				} catch (Exception ex) {
					log?.LogErrorFromException (ex);
                    Thread.Sleep(100);
                }
			}

            if (linkedCancelTokenSource.IsCancellationRequested)
            {
                log.LogMessage("Exclusive lock failed (canceled) on: {0}", file);
            }
			return null;
		}

		public static string HashSha1 (string value)
		{
			using (HashAlgorithm hashAlg = new SHA1Managed ()) {
				var hash = hashAlg.ComputeHash (System.Text.Encoding.ASCII.GetBytes (value));
				return BitConverter.ToString (hash).Replace ("-", string.Empty);
			}
		}

		public static string HashMd5 (string value)
		{
			return HashMd5 (System.Text.Encoding.Default.GetBytes (value));
		}

		public static string HashMd5 (byte[] value)
		{
			using (var md5 = MD5.Create ())
				return BitConverter.ToString (md5.ComputeHash (value)).Replace ("-", "").ToLowerInvariant ();
		}

		public static string HashMd5 (Stream value)
		{
			using (var md5 = MD5.Create ())
				return BitConverter.ToString (md5.ComputeHash (value)).Replace ("-", "").ToLowerInvariant ();
		}

	}
}
