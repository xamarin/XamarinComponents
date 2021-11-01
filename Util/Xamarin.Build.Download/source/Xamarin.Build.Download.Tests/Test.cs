// Copyright (c) 2015-2016 Xamarin Inc.

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Mono.Cecil;
using Xamarin.ContentPipeline.Tests;
using Xamarin.MacDev;
using Xunit;

namespace NativeLibraryDownloaderTests
{
	public class Test : TestsBase
	{
		public static string Configuration = "Release";
		public static readonly string[] DEFAULT_IGNORE_PATTERNS = { "*.overridetasks", "*.tasks" };

		void AddCoreTargets (ProjectRootElement el)
		{
			var baseDir = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;

			var props = Path.Combine (baseDir, "..", "..", "source", "Xamarin.Build.Download", "bin", Configuration, "netstandard20", "Xamarin.Build.Download.props");

			if (!File.Exists(props))
				props = Path.Combine(baseDir, "..", "Xamarin.Build.Download.props");

			el.AddImport (props);
			var targets = Path.Combine (baseDir, "..", "..", "source", "Xamarin.Build.Download", "bin", Configuration, "netstandard20", "Xamarin.Build.Download.targets");
			if (!File.Exists(targets))
				targets = Path.Combine(baseDir, "..", "Xamarin.Build.Download.targets");

			el.AddImport (targets);

		}

		[Fact]
		public void NoArchivesOrTargets ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);

			var log = new MSBuildTestLogger ();
			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log);
			Assert.True (success);
		}

		[Fact]
		public void InvalidArchiveMetadata ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem ("XamarinBuildDownload", "-----");

			prel.AddItem (
				"XamarinBuildDownload", "foo-1.2", new Dictionary<string, string> {
				});

			prel.AddItem (
				"XamarinBuildDownload", "bar-1.9", new Dictionary<string, string> {
					{ "Url", "https://www.example.com/bar.zip" },
					{ "Kind", "Cabbage" }
				});

			prel.AddItem (
				"XamarinBuildDownload", "baz-1.9", new Dictionary<string, string> {
					{ "Url", "https://www.example.com/bar.unknown" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			var errors = log.Errors.Select (e => e.Message).ToList ();
			Assert.Equal (4, errors.Count);
			Assert.Equal ("Invalid item ID -----", errors[0]);
			Assert.Equal ("Unknown archive kind 'Cabbage' for 'https://www.example.com/bar.zip'", errors[1]);
			Assert.Equal ("Unknown archive kind '' for 'https://www.example.com/bar.unknown'", errors[2]);
			Assert.Equal ("Missing required Url metadata on item foo-1.2", errors[3]);

			Assert.False (success);
		}

		[Fact]
		public void TestZipDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "ILRepack-2.0.10", new Dictionary<string, string> {
					{ "Url", "https://www.nuget.org/api/v2/package/ILRepack/2.0.10" },
					{ "Kind", "Zip" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);


			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "ILRepack-2.0.10", "ILRepack.nuspec")));
		}

		[Fact]
		public void TestTgzDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "GoogleSymbolUtilities-1.0.3", new Dictionary<string, string> {
					{ "Url", "https://www.gstatic.com/cpdc/a060f37adbad54ea-GoogleSymbolUtilities-1.0.3.tar.gz" },
					{ "Kind", "Tgz" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "GoogleSymbolUtilities-1.0.3", "Libraries", "libGSDK_Overload.a")));
		}

		[Fact]
		public void TestUncompressedNamedDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", "https://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar" },
					{ "Kind", "Uncompressed" },
					{ "ToFile", "facebook-android-sdk.aar" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "FacebookAndroid-4.17.0", "facebook-android-sdk.aar")));
		}

		[Fact]
		public void TestUncompressedUnnamedDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", "https://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar" },
					{ "Kind", "Uncompressed" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "FacebookAndroid-4.17.0", "FacebookAndroid-4.17.0.uncompressed")));
		}

		//in google maps, the tar inside the tgz doesn't match the tgz name
		[Fact]
		public void TestGMapsDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "GMaps-1.11.1", new Dictionary<string, string> {
					{ "Url", "https://www.gstatic.com/cpdc/c0e534927c0c955e-GoogleMaps-1.11.1.tar.gz" },
					{ "Kind", "Tgz" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "GMaps-1.11.1", "CHANGELOG")));
		}

		[Fact]
		public void TestCastAssemblyResources ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Foo", new Version (1, 0, 0, 0)),
				"Main",
				ModuleKind.Dll
			);
			var dll = Path.Combine (TempDir, "Foo.dll");
			asm.Write (dll);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "Xamarin.iOS");
			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", Path.Combine (TempDir, "obj"));

			prel.AddItem (
				"XamarinBuildDownload", "AppInvites-1.0.2", new Dictionary<string, string> {
					{ "Url", "https://www.gstatic.com/cpdc/278f79fcd3b365e3-AppInvites-1.0.2.tar.gz" },
					{ "Kind", "Tgz" }
				});

			prel.AddItem (
				"ReferencePath",
				dll
			);

			var bundlePath = Path.Combine (unpackDir, "AppInvites-1.0.2", "Frameworks", "GINInvite.framework", "Versions", "A", "Resources", "GINInviteResources.bundle");

			var plist = Path.Combine (bundlePath, "Info.plist");
			string resourceName = "__monotouch_content_GINInviteResources.bundle_fInfo.plist";
			prel.AddItem (
				"RestoreAssemblyResource",
				plist,
				new Dictionary<string,string> {
					{ "AssemblyName", "Foo" },
					{ "LogicalName", resourceName }
				}
			);

			var image = Path.Combine (bundlePath, "ic_sms_24@3x.png");
			resourceName = "__monotouch_content_GINInviteResources.bundle_fic__sms__24%403x.png";
			prel.AddItem (
				"RestoreAssemblyResource",
				image,
				new Dictionary<string, string> {
					{ "AssemblyName", "Foo" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildCastAssemblyResources", log);

			var ignoreMessages = new List<string> { "Enumeration yielded no results" };
			ignoreMessages.AddRange (DEFAULT_IGNORE_PATTERNS);
			AssertNoMessagesOrWarnings (log, ignoreMessages.ToArray());
			Assert.True (success);

			var plistExists = File.Exists (plist);
			Assert.True (plistExists);

			var imageExists = File.Exists (image);
			Assert.True (imageExists);

			// Check if BundleResources were generated correctly.
			var bundleResources = project.GetItems ("BundleResource");
			Assert.True (bundleResources != null);

			// Check if Optimize metadata was generated correctly.
			var imageResource = bundleResources.Single (b => b.GetMetadataValue ("Identity").ToLower ().EndsWith (".png"));
			Assert.True (imageResource.GetMetadataValue ("Optimize") == "False");
		}

		[Fact]
		public void TestAndroidAarAdded()
		{
			var unpackDir = GetTempPath("unpacked");
			var artifactXbdId = "gpsbasement-16.2.0";

			var r = AndroidAarAdd(unpackDir, artifactXbdId, "https://dl.google.com/dl/android/maven2/com/google/android/gms/play-services-basement/16.2.0/play-services-basement-16.2.0.aar", true);

			AssertNoMessagesOrWarnings(r.logs, DEFAULT_IGNORE_PATTERNS);
			Assert.True(r.success);

			var aarPath = Path.Combine(unpackDir, artifactXbdId, artifactXbdId + ".aar");
			Assert.True(File.Exists(aarPath));

			Assert.Contains(r.project.Items, i => i.ItemType == "AndroidAarLibrary" && i.EvaluatedInclude == aarPath);
		}

		[Fact]
		public void TestAndroidAarIdeTooOld()
		{
			var unpackDir = GetTempPath("unpacked");
			var artifactXbdId = "gpsbasement-16.2.0";

			var r = AndroidAarAdd(unpackDir, artifactXbdId, "https://dl.google.com/dl/android/maven2/com/google/android/gms/play-services-basement/16.2.0/play-services-basement-16.2.0.aar", false);

			// Check for the error
			Assert.NotEmpty(r.logs.Errors);

			Assert.False(r.success);
		}

		(bool success, ProjectInstance project, MSBuildTestLogger logs) AndroidAarAdd(string unpackDir, string artifactXbdId, string url, bool androidAarLibraryAvailableItem)
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);


			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "MonoAndroid");
			prel.SetProperty ("TargetFrameworkVersion", "v9.0");
			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", Path.Combine (TempDir, "obj"));

			if (androidAarLibraryAvailableItem)
				prel.AddItem("AvailableItemName", "AndroidAarLibrary");


			var item = prel.AddItem (
				"XamarinBuildDownload", artifactXbdId, new Dictionary<string, string> {
					{ "Url", url },
					{ "Kind", "Uncompressed" },
					{ "ToFile", artifactXbdId + ".aar" }
				});

			prel.AddItem (
				"XamarinBuildDownloadAndroidAarLibrary",
				"$(XamarinBuildDownloadDir)" + artifactXbdId + "\\" + artifactXbdId + ".aar",
				new Dictionary<string, string> { }
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();
			log.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Diagnostic;

			var success = BuildProject (engine, project, "_XamarinBuildDownloadAarInclude", log);

			return (success, project, log);
		}

		[Fact]
		public void TestDisallowUnsafeGetItemsToDownload ()
		{
			var itemUrl = "http://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar";
			var zipUrl = "http://dl-ssl.google.com/android/repository/android_m2repository_r40.zip";

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "Kind", "Uncompressed" },
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/cardview.v7", new Dictionary<string, string> {
					{ "Url", zipUrl },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Empty (itemToDownload);
		}

		[Fact]
		public void TestAllowUnsafeGetItemsToDownload ()
		{
			var itemUrl = "http://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar";
			var zipUrl = "http://dl-ssl.google.com/android/repository/android_m2repository_r40.zip";

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("XamarinBuildDownloadAllowUnsecure", "true");

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "Kind", "Uncompressed" },
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/cardview.v7", new Dictionary<string, string> {
					{ "Url", zipUrl },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Equal (2, itemToDownload.Count);
			Assert.Contains(itemToDownload, i => i.GetMetadata ("Url").EvaluatedValue == itemUrl);
			Assert.Contains(itemToDownload, i => i.GetMetadata ("Url").EvaluatedValue == zipUrl);
		}

		[Fact]
		public void TestGetItemsToDownload ()
		{
			var itemUrl = "https://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar";

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "Kind", "Uncompressed" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Equal (1, itemToDownload.Count);
			Assert.True (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}

		[Fact]
		public void TestDeduplicateGetItemsToDownload ()
		{
			var itemUrl = "https://search.maven.org/remotecontent?filepath=com/facebook/android/facebook-android-sdk/4.17.0/facebook-android-sdk-4.17.0.aar";

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "Kind", "Uncompressed" },
				});

			prel.AddItem (
				"XamarinBuildDownload", "FacebookAndroid-4.17.0", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "Kind", "Uncompressed" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Equal (1, itemToDownload.Count);
			Assert.True (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}

		[Fact]
		public void TestTimesOutWaitingOnExclusiveLock ()
		{
			var unpackDir = GetTempPath ("unpacked");
			System.IO.Directory.CreateDirectory (unpackDir);

			var lockFile = Path.Combine(unpackDir, "GMaps-1.11.1.locked");
			using (var lockStream = File.Open (lockFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None)) {

				var engine = new ProjectCollection ();
				var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

				prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

				prel.AddItem (
					"XamarinBuildDownload", "GMaps-1.11.1", new Dictionary<string, string> {
					{ "Url", "https://www.gstatic.com/cpdc/c0e534927c0c955e-GoogleMaps-1.11.1.tar.gz" },
					{ "Kind", "Tgz" },
					{ "ExclusiveLockTimeout", "1" }
					});

				AddCoreTargets (prel);

				var project = new ProjectInstance (prel);
				var log = new MSBuildTestLogger ();

				var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

				Assert.False (success);
				Assert.Null (log.Errors.FirstOrDefault (err => err.Code == "XBD005"));
			}
		}

		[Fact]
		public void TestAndroidSupportSinglePartialZipDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/cardview.v7", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip" },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));
		}


		[Fact]
		public void TestAndroidSupportMultiplePartialZipDownload ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/cardview.v7", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip" },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/recyclerview.v7", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip" },
					{ "ToFile", "recyclerview.v7.aar" },
					{ "RangeStart", "199278205" },
					{ "RangeEnd", "199589731" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));
			Assert.True (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "recyclerview.v7", "recyclerview.v7.aar")));
		}



		[Fact]
		public void TestGetPartialZipItemsToDownload ()
		{
			var itemUrl = "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip";

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/cardview.v7", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/recyclerview.v7", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "ToFile", "recyclerview.v7.aar" },
					{ "RangeStart", "199278205" },
					{ "RangeEnd", "199589731" },
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Equal (2, itemToDownload.Count);
			Assert.True (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}

		[Fact]
		public void TestPathGreaterThan260Chars ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var unpackDir = GetTempPath ("unpacked");

			for (var i = 1; unpackDir.Length < 260; i++)
				unpackDir = Path.Combine (unpackDir, $"segment{i}");

			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);

			prel.AddItem (
				"XamarinBuildDownload", "GAppM-8.8.0", new Dictionary<string, string> {
					{ "Url", "https://dl.google.com/firebase/ios/analytics/86849febfdc4ff13/GoogleAppMeasurement-8.8.0.tar.gz" },
					{ "Kind", "Tgz" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			Assert.True (File.Exists (Path.Combine (unpackDir, "GAppM-8.8.0", "GoogleAppMeasurement-8.8.0", "dummy.txt")));
		}
	}
}