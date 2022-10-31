using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Build.Framework;

namespace Xamarin.Build.Download
{
	public class XamarinDownloadPartialZips : AsyncTask, ILogger
	{
		public ITaskItem [] Parts { get; private set; }

		public string DestinationBase { get; set; }

		public string CacheDirectory { get; set; }

		public bool AllowUnsecureUrls { get; set; }

		public bool IsAndroid { get; set; }

		HttpClient http;

		public override bool Execute ()
		{
			LogMessage ("Starting XamarinDownloadPartialZips");
			LogMessage ("DestinationBase: {0}", DestinationBase);
			LogMessage ("CacheDirectory: {0}", CacheDirectory);

			Task.Run (async () => {
				List<PartialZipDownload> parts = null;
				try {
					http = new HttpClient ();

					var cacheDir = DownloadUtils.GetCacheDir (CacheDirectory);
					var downloadUtils = new DownloadUtils (this, cacheDir);

					parts = downloadUtils.ParsePartialZipDownloadItems (Parts, AllowUnsecureUrls);

					await DownloadAll (cacheDir, parts).ConfigureAwait (false);

				} catch (Exception ex) {
					LogErrorFromException (ex);

					// Log Custom error if one was specified in the partial download info
					var firstPart = parts?.FirstOrDefault ();
					if (!string.IsNullOrEmpty (firstPart?.CustomErrorCode) && !string.IsNullOrEmpty (firstPart?.CustomErrorMessage))
						LogCodedError (firstPart.CustomErrorCode, firstPart.CustomErrorMessage);
				} finally {
					Complete ();
				}
			});

			var result = base.Execute ();

			return result && !Log.HasLoggedErrors;
		}

		async Task DownloadAll (string cacheDirectory, List<PartialZipDownload> parts)
		{
			// Get the parts all grouped by their URL so we can batch requests with multiple ranges
			// instead of making a request to the same url for each part
			// also only grab the parts that don't already locally exist
			var uniqueUrls = parts
				.Where (p => !File.Exists (Path.Combine (cacheDirectory, p.Id, p.ToFile)))
				.GroupBy (p => p.Url);

			// For each unique url...
			foreach (var partsByUrl in uniqueUrls) {
				var downloadUrl = partsByUrl.Key;

				LogMessage ("Downloading Partial Zip parts from: " + downloadUrl);

				try {
					// Create a lock file based on the hash of the URL we are downloading from
					// Since we could download a multipart request, we are locking on the url from any other process downloading from it
					var lockFile = Path.Combine (cacheDirectory, DownloadUtils.Crc64 (downloadUrl) + ".locked");

					using (var lockStream = DownloadUtils.ObtainExclusiveFileLock (lockFile, base.Token, TimeSpan.FromSeconds (30))) {

						if (lockStream == null) {
							LogCodedError (ErrorCodes.ExclusiveLockTimeout, "Timed out waiting for exclusive file lock on: {0}", lockFile);
							LogMessage ("Timed out waiting for an exclusive file lock on: " + lockFile, MessageImportance.High);

							// Log Custom error if one was specified in the partial download info
							var firstPart = partsByUrl.FirstOrDefault ();
							if (!string.IsNullOrEmpty (firstPart?.CustomErrorCode) && !string.IsNullOrEmpty (firstPart?.CustomErrorMessage))
								LogCodedError (firstPart.CustomErrorCode, firstPart.CustomErrorMessage);
							
							return;
						}

						try {
							await Download (cacheDirectory, partsByUrl.Key, partsByUrl.ToList ()).ConfigureAwait (false);
						} catch (Exception ex) {
							LogCodedError (ErrorCodes.PartialDownloadFailed, "Partial Download Failed for one or more parts");
							LogErrorFromException (ex);

							// Log Custom error if one was specified in the partial download info
							var firstPart = partsByUrl.FirstOrDefault ();
							if (!string.IsNullOrEmpty (firstPart?.CustomErrorCode) && !string.IsNullOrEmpty (firstPart?.CustomErrorMessage))
								LogCodedError (firstPart.CustomErrorCode, firstPart.CustomErrorMessage);
						}
					}

					try {
						if (File.Exists (lockFile))
							File.Delete (lockFile);
					} catch { }
				} catch (Exception ex) {

					LogCodedError (ErrorCodes.PartialDownloadFailed, "Partial Download Failed for one or more parts");
					LogErrorFromException (ex);

					// Log Custom error if one was specified in the partial download info
					var firstPart = partsByUrl.FirstOrDefault ();
					if (!string.IsNullOrEmpty (firstPart?.CustomErrorCode) && !string.IsNullOrEmpty (firstPart?.CustomErrorMessage))
						LogCodedError (firstPart.CustomErrorCode, firstPart.CustomErrorMessage);
				}
			}
		}

		async Task Download (string cacheDirectory, string url, List<PartialZipDownload> parts)
		{
			HttpResponseMessage resp;

			foreach (var part in parts) {
				var req = new HttpRequestMessage (HttpMethod.Get, url);

				// This seems to fix a weird issue where killing the connection on mono on macOS
				// causes the request to hang indefinitely
				req.Headers.ConnectionClose = true;

				req.Headers.Range = new RangeHeaderValue (part.RangeStart, part.RangeEnd);

				resp = await http.SendAsync (req).ConfigureAwait (false);

				using (var partStream = await resp.Content.ReadAsStreamAsync ().ConfigureAwait (false))
					await ExtractPartAndValidate (part, partStream, cacheDirectory).ConfigureAwait (false);
			}
		}

		string GetOutputPath (string cacheDirectory, PartialZipDownload part)
		{
			var outputPath = new FileInfo (Path.Combine (cacheDirectory, part.Id, part.ToFile));
			outputPath.Directory.Create ();
			return outputPath.FullName;
		}

		async Task<bool> ExtractPartAndValidate (PartialZipDownload part, Stream partInputStream, string cacheDirectory)
		{
			string fileHash = null;

			var outputPath = GetOutputPath (cacheDirectory, part);

			using (var iis = new System.IO.Compression.DeflateStream (partInputStream, System.IO.Compression.CompressionMode.Decompress))
			//using (var iis = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream (partInputStream, new ICSharpCode.SharpZipLib.Zip.Compression.Inflater (true)))
			using (var fs = File.Open (outputPath, FileMode.Create)) {
				await iis.CopyToAsync (fs).ConfigureAwait (false);
				await fs.FlushAsync ().ConfigureAwait (false);

				fs.Seek (0, SeekOrigin.Begin);
				fileHash = DownloadUtils.HashSha256 (fs);
				LogDebugMessage ("Hash of Downloaded File: {0}", fileHash);

				fs.Close ();
			}

			if (!string.IsNullOrEmpty (part.Sha256) && !part.Sha256.Equals (fileHash, StringComparison.InvariantCultureIgnoreCase)) {

				// TODO: HANDLE 
				LogMessage ("File SHA256 Hash was invalid, deleting file: {0}", part.ToFile);
				File.Delete (outputPath);
				return false;
			}

			return true;
		}

		string ThisOrThat (string strOne, string strTwo)
		{
			if (string.IsNullOrEmpty (strOne))
				return strTwo;

			return strOne;
		}

		string ThisOrThat (string strOne, Func<string> strTwoFunc)
		{
			if (string.IsNullOrEmpty (strOne))
				return strTwoFunc ();

			return strOne;
		}
	}
}
