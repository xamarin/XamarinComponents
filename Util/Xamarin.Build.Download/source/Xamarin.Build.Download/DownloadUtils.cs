using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Xamarin.Build.Download
{
	public class DownloadUtils
	{
		public DownloadUtils(ILogger logger, string cacheDir, bool bypassValidation)
		{
			Log = logger;
			CacheDir = GetCacheDir(cacheDir);
			BypassValidation = bypassValidation;
		}

		public ILogger Log { get; private set; }
		public string CacheDir { get; private set; }
		public bool BypassValidation { get; private set; }

		public IEnumerable<XamarinBuildDownload> ParseDownloadItems (ITaskItem[] items, bool allowUnsecureUrls)
		{
			if (items == null || items.Length <= 0)
				return new List<XamarinBuildDownload> ();
			
			var results = new List<XamarinBuildDownload> ();

			foreach (var item in items) {
                try
                {
					var xbd = new XamarinBuildDownload();

					xbd.Id = item.ItemSpec;
					if (!ValidateId(xbd.Id, this.BypassValidation))
					{
						Log.LogCodedError(ErrorCodes.XbdInvalidItemId, "Invalid item ID {0}", xbd.Id);
						continue;
					}
					xbd.Url = item.GetMetadata("Url");
					if (string.IsNullOrEmpty(xbd.Url))
					{
						Log.LogCodedError(ErrorCodes.XbdInvalidUrl, "Missing required Url metadata on item {0}", item.ItemSpec);
						continue;
					}
					xbd.Sha256 = item.GetMetadata("Sha256");
					xbd.Kind = GetKind(xbd.Url, item.GetMetadata("Kind"));
					if (xbd.Kind == ArchiveKind.Unknown)
					{
						//TODO we may be able to determine the kind from the server response
						continue;
					}
					if (!EnsureSecureUrl(item, xbd.Url, allowUnsecureUrls))
					{
						continue;
					}

					xbd.CustomErrorCode = item.GetMetadata("CustomErrorCode");
					xbd.CustomErrorMessage = item.GetMetadata("CustomErrorMessage");

					// By default, use the kind (tgz or zip) as the file extension for the cache file
					var cacheFileExt = xbd.Kind.ToString().ToLower();

					// If we have an uncompressed file specified (move file instead of decompressing it)
					if (xbd.Kind == ArchiveKind.Uncompressed)
					{
						// Get the filename
						xbd.ToFile = item.GetMetadata("ToFile");
						// If we have a tofile set, try and grab its extension
						if (!string.IsNullOrEmpty(xbd.ToFile))
							cacheFileExt = Path.GetExtension(xbd.ToFile)?.ToLower() ?? string.Empty;
					}

					xbd.CacheFile = Path.Combine(CacheDir, item.ItemSpec.TrimEnd('.') + "." + cacheFileExt.TrimStart('.'));
					xbd.DestinationDir = Path.GetFullPath(Path.Combine(CacheDir, item.ItemSpec));

					int lockTimeout = 60;
					if (int.TryParse(item.GetMetadata("ExclusiveLockTimeout"), out lockTimeout))
						xbd.ExclusiveLockTimeout = lockTimeout;

					results.Add(xbd);
				}
				catch(Exception ex)
                {
					Log.LogErrorFromException(ex);
                }
				
			}

			// Deduplicate possible results by their Id which should be unique always for the given archive
			return results.GroupBy (item => item.Id).Select ((kvp) => kvp.FirstOrDefault ()).ToArray ();
		}

		public List<PartialZipDownload> ParsePartialZipDownloadItems (ITaskItem [] items, bool allowUnsecureUrls)
		{
			if (items == null || items.Length <= 0)
				return new List<PartialZipDownload> ();
			
			var result = new List<PartialZipDownload> ();

			foreach (var part in items) {

				var id = part.ItemSpec;
				if (!DownloadUtils.ValidateId (id, this.BypassValidation)) {
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
				if (!EnsureSecureUrl (part, url, allowUnsecureUrls)) {
					continue;
				}

				var sha256 = part.GetMetadata ("Sha256");

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
					Sha256 = sha256,
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

		public bool EnsureSecureUrl (ITaskItem item, string url, bool allowUnsecureUrls)
		{
			if (!allowUnsecureUrls) {
				if (url.StartsWith ("http:", StringComparison.OrdinalIgnoreCase)) {
					Log.LogCodedError (ErrorCodes.XbdUnsecureUrl, "Unsecure download url '{0}' not allowed, unless 'XamarinBuildDownloadAllowUnsecure' is set to 'true' for the project, for {1}", url, item.ItemSpec);
					return false;
				}
			}

			return true;
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

		public static bool ValidateId (string id, bool bypassValidation = false)
		{
			// Always return true if we choose to bypass the validation
			if (bypassValidation)
				return true;

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

		public static Stream ObtainExclusiveFileLock (string file, CancellationToken cancelToken, TimeSpan timeout, ILogger log = null)
		{
			var linkedCancelTokenSource = CancellationTokenSource.CreateLinkedTokenSource (
				cancelToken,
				new CancellationTokenSource (timeout).Token);
			
			while (!linkedCancelTokenSource.IsCancellationRequested) {
				try {
					var lockStream = File.Open (file, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
					return lockStream;
				} catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException) {
					Thread.Sleep (100);
				} catch (Exception ex) {
					log?.LogErrorFromException (ex);
				}
			}

			return null;
		}

		public static string Crc64(string s)
		{
			var bytes = Encoding.UTF8.GetBytes(s);
			return HashBytes(bytes);
		}

		public static string HashBytes(byte[] bytes)
		{
			using (var hashAlg = new Crc64())
			{
				byte[] hash = hashAlg.ComputeHash(bytes);
				return ToHexString(hash);
			}
		}

		public static string ToHexString(byte[] hash)
		{
			char[] array = new char[hash.Length * 2];
			for (int i = 0, j = 0; i < hash.Length; i += 1, j += 2)
			{
				byte b = hash[i];
				array[j] = GetHexValue(b / 16);
				array[j + 1] = GetHexValue(b % 16);
			}
			return new string(array);
		}

		static char GetHexValue(int i) => (char)(i < 10 ? i + 48 : i - 10 + 65);

		public static string HashSha256 (string value)
		{
			return HashSha256 (Encoding.UTF8.GetBytes (value));
		}

		public static string HashSha256 (byte[] value)
		{
			using (var sha256 = new SHA256Managed())
			{
				var hash = new StringBuilder();
				var hashData = sha256.ComputeHash(value);
				foreach (var b in hashData)
					hash.Append(b.ToString("x2"));

				return hash.ToString();
			}
		}

		public static string HashSha256 (Stream value)
		{
			using (var sha256 = new SHA256Managed())
			{
				var hash = new StringBuilder();
				var hashData = sha256.ComputeHash(value);
				foreach (var b in hashData)
					hash.Append(b.ToString("x2"));

				return hash.ToString();
			}
		}
	}
}
