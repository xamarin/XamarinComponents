﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Win32;
using Xamarin.Components.Ide.Activation;
using Xamarin.MacDev;
using System.Linq;

namespace Xamarin.Build.Download
{
	//based on Xamarin.Android MSBuild targets
	public class XamarinDownloadArchives : AsyncTask, ILogger
	{
		public ITaskItem [] Archives { get; private set; }

		public string DestinationBase {get; set; }

		public string CacheDirectory { get; set; }

		public string User7ZipPath { get; set; }

		DownloadUtils downloadUtils;

		public override bool Execute ()
		{
			downloadUtils = new DownloadUtils (this, CacheDirectory);

			Task.Run (async () => {
				try {
					var items = downloadUtils.ParseDownloadItems (Archives);

					foreach (var item in items) {
						await MakeSureLibraryIsInPlace (item, Token);
					}
				} catch (Exception ex) {
					LogErrorFromException (ex);
				} finally {
					Complete ();
				}
			});

			var result = base.Execute ();

			return result && !Log.HasLoggedErrors;
		}

		/// <summary>
		/// Attempt to determine the archive kind
		/// </summary>
		/// <returns>The archive kind, or Unknown if it could not be be determined.</returns>
		/// <param name="urlMetadata">The 'Url' metadata.</param>
		/// <param name="kindMetadata">The 'Kind' metadata.</param>


		bool IsValidDownload(string cachedHashFile, string fileToHash, string expectedHash)
		{
			// If an expected hash wasn't provided, we skip this check and assume OK
			if (string.IsNullOrEmpty (expectedHash))
				return true;

			var cachedHashFileExists = File.Exists (cachedHashFile);

			// Use a cached hash value to compare against so we don't always recalculate the hash
			if (cachedHashFileExists) {
				var cachedHash = File.ReadAllText (cachedHashFile);

				// See if our cached hash value matches what's expected
				if (string.Compare (cachedHash, expectedHash, StringComparison.InvariantCultureIgnoreCase) == 0)
					return true;
			}

			// Calculate the hash of the file
			var hash = HashFile (fileToHash).Replace ("-", string.Empty);

			LogDebugMessage ("File :{0}", fileToHash);
			LogDebugMessage ("SHA1 : {0}", hash);
			LogDebugMessage ("Expected SHA1 : {0}", expectedHash);

			// Check to see if the new hash matches expected value
			if (string.Compare (hash, expectedHash, StringComparison.InvariantCultureIgnoreCase) == 0) {

				// Try to cache the hash file to avoid calculating it in the future
				try {
					File.WriteAllText (cachedHashFile, hash);
				} catch { }

				// Everything checks out
				return true;
			}

			// If the hash didn't match, let's try to delete the cache file if it exists
			// since we know that it's not valid anyway, no sense trying to use it again
			if (cachedHashFileExists) {
				try {
					File.Delete (cachedHashFile);
				} catch { }
			}

			return false;
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

		async Task<bool> MakeSureLibraryIsInPlace (XamarinBuildDownload xbd, CancellationToken token)
		{
			// Skip extraction if the file is already in place
			var flagFile = xbd.DestinationDir + ".unpacked";
			if (File.Exists (flagFile))
				return true;

			try {
				Directory.CreateDirectory (xbd.DestinationDir);
			} catch (Exception ex) {
				LogCodedError (ThisOrThat (xbd.CustomErrorCode, ErrorCodes.DirectoryCreateFailed), ThisOrThat (xbd.CustomErrorMessage, () => string.Format ("Failed to create directory '{0}'.", xbd.DestinationDir)));
				LogMessage ("Directory creation failure reason: " + ex.ToString (), MessageImportance.High);
				return false;
			}

			var lockFile = xbd.DestinationDir + ".locked";
			using (var lockStream = DownloadUtils.ObtainExclusiveFileLock (lockFile, Token, TimeSpan.FromSeconds (xbd.ExclusiveLockTimeout), Log)) {

				if (lockStream == null) {
					LogCodedError (ErrorCodes.ExclusiveLockTimeout, "Timed out waiting for exclusive file lock on: {0}", lockFile);
					LogMessage ("Timed out waiting for an exclusive file lock on: " + lockFile, MessageImportance.High);
					return false;
				}

				if (!File.Exists (xbd.CacheFile) || !IsValidDownload (xbd.DestinationDir + ".sha1", xbd.CacheFile, xbd.Sha1)) {
					try {
						int progress = -1;
						DownloadProgressChangedEventHandler downloadHandler = (o, e) => {
							if (e.ProgressPercentage % 10 != 0 || progress == e.ProgressPercentage)
								return;
							progress = e.ProgressPercentage;
							LogMessage (
								"\t({0}/{1}b), total {2:F1}%", e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage
							);
						};
						using (var client = new WebClient ()) {
							client.DownloadProgressChanged += downloadHandler;
							LogMessage ("  Downloading {0} to {1}", xbd.Url, xbd.CacheFile);
							client.DownloadFileTaskAsync (xbd.Url, xbd.CacheFile).Wait (token);
							LogMessage ("  Downloading Complete");
							client.DownloadProgressChanged -= downloadHandler;
						}
					} catch (Exception e) {
						LogCodedError (ThisOrThat (xbd.CustomErrorCode, ErrorCodes.DownloadFailed), ThisOrThat (xbd.CustomErrorMessage, () => string.Format ("Download failed. Please download {0} to a file called {1}.", xbd.Url, xbd.CacheFile)));
						LogMessage ("Download failure reason: " + e.GetBaseException ().Message, MessageImportance.High);
						File.Delete (xbd.CacheFile);
						return false;
					}
				}

				if (!File.Exists (xbd.CacheFile)) {
					LogCodedError (ThisOrThat (xbd.CustomErrorCode, ErrorCodes.DownloadedFileMissing), ThisOrThat (xbd.CustomErrorMessage, () => string.Format ("Downloaded file '{0}' is missing.", xbd.CacheFile)));
					return false;
				}

				if (xbd.Kind == ArchiveKind.Uncompressed) {

					var uncompressedCacheFile = xbd.CacheFile;
					if (!string.IsNullOrEmpty (xbd.ToFile))
						uncompressedCacheFile = xbd.ToFile;

					File.Move (xbd.CacheFile, Path.Combine (xbd.DestinationDir, Path.GetFileName (uncompressedCacheFile)));
					File.WriteAllText (flagFile, "This marks that the extraction completed successfully");
					return true;
				} else {
					if (await ExtractArchive (xbd, flagFile, token)) {
						File.WriteAllText (flagFile, "This marks that the extraction completed successfully");
						return true;
					}
				}
			}

			// We will attempt to delete the lock file when we're done
			try {
				if (File.Exists (lockFile))
					File.Delete (lockFile);
			} catch { }

			return false;
		}

		async Task<bool> ExtractArchive (XamarinBuildDownload xbd, string flagFile, CancellationToken token)
		{
			ProcessStartInfo psi = CreateExtractionArgs (xbd.CacheFile, xbd.DestinationDir, xbd.Kind);

			try {
				LogMessage ("Extracting {0} to {1}", xbd.CacheFile, xbd.DestinationDir);
				var output = new StringWriter ();
				int returnCode = await ProcessUtils.StartProcess (psi, output, output, token);
				if (returnCode == 0) {
					//with 7zip, tgz just gets uncompressed to a tar. now extract the tar.
					if (xbd.Kind == ArchiveKind.Tgz && Platform.IsWindows) {
						returnCode = await ExtractTarOnWindows (xbd, output, token);
						if (returnCode == 0)
							return true;
					} else {
						return true;
					}
				}
				LogCodedError (
					ThisOrThat (xbd.CustomErrorCode, ErrorCodes.ExtractionFailed),
					ThisOrThat (xbd.CustomErrorMessage, () => string.Format ("Unpacking failed. Please download '{0}' and extract it to the '{1}' directory " +
																			 "and create an empty file called '{2}'.", xbd.Url, xbd.DestinationDir, flagFile)));
				LogMessage ("Unpacking failure reason: " + output.ToString (), MessageImportance.High);
			} catch (Exception ex) {
				LogErrorFromException (ex);
			}

			//something went wrong, clean up so we try again next time
			try {
				Directory.Delete (xbd.DestinationDir, true);
			} catch (Exception ex) {
				LogCodedError (ErrorCodes.DirectoryDeleteFailed, "Failed to delete directory '{0}'.", xbd.DestinationDir);
				LogErrorFromException (ex);
			}
			return false;
		}

		async Task<int> ExtractTarOnWindows (XamarinBuildDownload xbd, StringWriter output, CancellationToken token)
		{
			var tarFile = GetTarFileName (xbd);
			var psi = CreateExtractionArgs (tarFile, xbd.DestinationDir, xbd.Kind, true);
			var returnCode = await ProcessUtils.StartProcess (psi, output, output, token);
			if (returnCode == 7) {
				LogMessage ("7Zip command line parse did not work.  Trying without -snl-");
				psi = CreateExtractionArgs (tarFile, xbd.DestinationDir, xbd.Kind, false);
				returnCode = await ProcessUtils.StartProcess (psi, output, output, token);
			}
			File.Delete (tarFile);
			return returnCode;
		}

		ProcessStartInfo CreateExtractionArgs (string file, string contentDir, ArchiveKind kind, bool ignoreTarSymLinks = false)
		{
			ProcessArgumentBuilder args = null;
			switch (kind) {
			case ArchiveKind.Tgz:
				if (Platform.IsWindows)
					args = Build7ZipExtractionArgs (file, contentDir, User7ZipPath, ignoreTarSymLinks);
				else
					args = BuildTgzExtractionArgs (file, contentDir);
				break;
				case ArchiveKind.Zip:
				if (Platform.IsWindows)
					args = Build7ZipExtractionArgs (file, contentDir, User7ZipPath, false);
				else
					args = BuildZipExtractionArgs (file, contentDir);
				break;
			default:
				throw new ArgumentException ("kind");
			}
			return new ProcessStartInfo (args.ProcessPath, args.ToString ()) {
				WorkingDirectory = contentDir,
				CreateNoWindow = true
			};
		}

		static ProcessArgumentBuilder Build7ZipExtractionArgs (string file, string contentDir, string user7ZipPath, bool ignoreTarSymLinks)
		{
			var args = new ProcessArgumentBuilder (Get7ZipPath (user7ZipPath));
			//if it's a tgz, we have a two-step extraction. for the gzipped layer, extract without paths
			if (file.EndsWith (".gz", StringComparison.OrdinalIgnoreCase) || file.EndsWith (".tgz", StringComparison.OrdinalIgnoreCase))
				args.Add ("e");
			else
				args.Add ("x");

			// Symbolic Links give us problems.  You need to run as admin to extract them.
			// Adding this flag will ignore links
			if (ignoreTarSymLinks)
				args.Add ("-snl-");

			args.AddQuoted ("-o" + contentDir);
			args.AddQuoted (file);
			return args;
		}

		static string Get7ZipPath (string user7ZipPath)
		{
			if (!string.IsNullOrEmpty (user7ZipPath) && File.Exists (user7ZipPath))
				return user7ZipPath;

			using (var topKey = Registry.LocalMachine.OpenSubKey (@"SOFTWARE\Xamarin\XamarinVS"))
			{
				string version;
				if (topKey != null && (version = (topKey.GetValue ("InstalledVersion") as string)) != null)
				{
					using (var key = Registry.LocalMachine.OpenSubKey (@"SOFTWARE\Xamarin\VisualStudio"))
					{
						foreach (var skName in key.GetSubKeyNames ())
						{
							using (var sk = key.OpenSubKey (skName))
							{
								if (sk == null)
									continue;
								var path = sk.GetValue ("Path") as string;
								if (path == null)
									continue;
								path = Path.Combine (path, version, "7-Zip", "7z.exe");
								if (File.Exists (path))
								{
									return path;
								}
							}
						}
					}
				}
			}

			using (var key = Registry.CurrentUser.OpenSubKey (@"SOFTWARE\Microsoft\VisualStudio"))
			{
				foreach (var skName in key.GetSubKeyNames ())
				{
					if (skName == null || !skName.EndsWith ("_Config"))
						continue;

					using (var sk = key.OpenSubKey (skName + @"\Packages\{296e6a4e-2bd5-44b7-a96d-8ee3d9cda2f6}"))
					{
						if (sk == null)
							continue;

						var path = sk.GetValue ("CodeBase") as string;

						if (path == null)
							continue;

						var sZipPath = Path.Combine (Path.GetDirectoryName (path), "7-Zip", "7z.exe");
						if (File.Exists (sZipPath))
						{
							return sZipPath;
						}
					}
				}
			}

			using (var topKey = Registry.CurrentUser.OpenSubKey (@"SOFTWARE\Xamarin\XamarinVS"))
			{
				string version; 
				if (topKey != null && (version = (topKey.GetValue ("InstalledVersion") as string)) != null)
				{
					using (var key = Registry.CurrentUser.OpenSubKey (@"SOFTWARE\Xamarin\VisualStudio"))
					{
						foreach (var skName in key.GetSubKeyNames ())
						{
							using (var sk = key.OpenSubKey (skName))
							{
								if (sk == null)
									continue;
								var path = sk.GetValue ("Path") as string;
								if (path == null)
									continue;
								path = Path.Combine (path, "Xamarin", version, "7-Zip", "7z.exe");
								if (File.Exists (path))
								{
									return path;
								}
							}
						}
					}
				}
			}

			throw new Exception ("Could not find 7zip.exe in Xamarin installation");
		}

		static ProcessArgumentBuilder BuildZipExtractionArgs (string file, string contentDir)
		{
			var args = new ProcessArgumentBuilder ("/usr/bin/unzip");
			args.Add ("-o", "-q");
			args.AddQuoted (file);
			return args;
		}

		static ProcessArgumentBuilder BuildTgzExtractionArgs (string file, string contentDir)
		{
			var args = new ProcessArgumentBuilder ("/usr/bin/tar");
			args.Add ("-x", "-f");
			args.AddQuoted (file);
			return args;
		}

		static string GetTarFileName (XamarinBuildDownload xbd)
		{
			var tarPath =  Path.Combine (xbd.DestinationDir, Path.ChangeExtension (Path.GetFileName (xbd.CacheFile), "tar"));

			if (File.Exists (tarPath))
				return tarPath;

			var foundFile = Directory.EnumerateFiles (xbd.DestinationDir, "*.tar", SearchOption.TopDirectoryOnly).FirstOrDefault ();

			return string.IsNullOrEmpty (foundFile) ? tarPath : foundFile;
		}

		public static string HashFile (string filename)
		{
			using (HashAlgorithm hashAlg = new SHA1Managed ()) {
				return HashFile (filename, hashAlg);
			}
		}

		public static string HashFile (string filename, HashAlgorithm hashAlg)
		{
			using (Stream file = new FileStream (filename, FileMode.Open, FileAccess.Read)) {
				byte[] hash = hashAlg.ComputeHash (file);
				return BitConverter.ToString (hash);
			}
		}
	}
}
