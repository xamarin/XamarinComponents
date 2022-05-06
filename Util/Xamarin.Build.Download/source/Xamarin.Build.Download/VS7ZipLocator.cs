using Microsoft.Build.Framework;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xamarin.Build.Download
{
	public static class VS7ZipLocator
	{
		const string VS_2017_RELATIVE_PATH_TO_7Z = @"Common7\IDE\Extensions\Xamarin\VisualStudio\7-Zip\7z.exe";
		const string VS_2019_RELATIVE_PATH_TO_7Z = @"Common7\IDE\Extensions\Xamarin.VisualStudio\7-Zip\7z.exe";

		public static string Locate7Zip(string vsInstallRoot)
		{
			var path = FindLegacy();

			if (!string.IsNullOrEmpty(path))
				return path;

			return FindNew(vsInstallRoot);
		}

		static string FindNew(string vsInstallRoot)
		{
			string tryPath = null;

			// Try for 2017 or 2019 VSInstallRoot Paths plus the known relative path
			// Since the relative paths for 2019 are different than 2019
			if (!string.IsNullOrEmpty(vsInstallRoot))
			{
				// VS 2019
				tryPath = Path.Combine(vsInstallRoot, VS_2019_RELATIVE_PATH_TO_7Z);
				if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
					return tryPath;

				// VS 2017
				tryPath = Path.Combine(vsInstallRoot, VS_2017_RELATIVE_PATH_TO_7Z);
				if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
					return tryPath;
			}

			// Fall back to trying some hard coded likely paths
			tryPath = FindInVs("2019", VS_2019_RELATIVE_PATH_TO_7Z);
			if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
				return tryPath;

			tryPath = FindInVs("2017", VS_2017_RELATIVE_PATH_TO_7Z);
			if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
				return tryPath;

			// Try for local installation of 7z
			tryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "7-Zip", "7z.exe");
			if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
				return tryPath;

			// Try for local installation of 7z
			tryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "7-Zip", "7z.exe");
			if (!string.IsNullOrEmpty(tryPath) && File.Exists(tryPath))
				return tryPath;

			return null;
		}

		static string FindInVs(string vsYear, string relativePathTo7z)
		{
			foreach (var drive in DriveInfo.GetDrives())
			{
				var root = $@"{drive.Name}Program Files (x86)\Microsoft Visual Studio\{vsYear}";

				if (Directory.Exists(root))
				{
					var sxsDirs = Directory.GetDirectories(root, "*", SearchOption.TopDirectoryOnly);

					if (sxsDirs != null && sxsDirs.Length > 0)
					{
						foreach (var sxsDir in sxsDirs)
						{
							var path7z = Path.Combine(sxsDir, relativePathTo7z);
							if (File.Exists(path7z))
								return path7z;
						}
					}
				}
			}

			return null;
		}

		static string FindLegacy()
		{
			using (var topKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Xamarin\XamarinVS"))
			{
				string version;
				if (topKey != null && (version = (topKey.GetValue("InstalledVersion") as string)) != null)
				{
					using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Xamarin\VisualStudio"))
					{
						if (key != null)
						{
							foreach (var skName in key.GetSubKeyNames())
							{
								using (var sk = key.OpenSubKey(skName))
								{
									if (sk == null)
										continue;
									var path = sk.GetValue("Path") as string;
									if (path == null)
										continue;
									path = Path.Combine(path, version, "7-Zip", "7z.exe");
									if (File.Exists(path))
									{
										return path;
									}
								}
							}
						}
					}
				}
			}

			using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio"))
			{
				if (key != null)
				{
					foreach (var skName in key.GetSubKeyNames())
					{
						if (skName == null || !skName.EndsWith("_Config"))
							continue;

						using (var sk = key.OpenSubKey(skName + @"\Packages\{296e6a4e-2bd5-44b7-a96d-8ee3d9cda2f6}"))
						{
							if (sk == null)
								continue;

							var path = sk.GetValue("CodeBase") as string;

							if (path == null)
								continue;

							var sZipPath = Path.Combine(Path.GetDirectoryName(path), "7-Zip", "7z.exe");
							if (File.Exists(sZipPath))
							{
								return sZipPath;
							}
						}
					}
				}
			}

			using (var topKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Xamarin\XamarinVS"))
			{
				string version;
				if (topKey != null && (version = (topKey.GetValue("InstalledVersion") as string)) != null)
				{
					using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Xamarin\VisualStudio"))
					{
						if (key != null)
						{
							foreach (var skName in key.GetSubKeyNames())
							{
								using (var sk = key.OpenSubKey(skName))
								{
									if (sk == null)
										continue;
									var path = sk.GetValue("Path") as string;
									if (path == null)
										continue;
									path = Path.Combine(path, "Xamarin", version, "7-Zip", "7z.exe");
									if (File.Exists(path))
									{
										return path;
									}
								}
							}
						}
					}
				}
			}

			return null;
		}
	}
}
