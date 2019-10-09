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

		public void AddCoreTargets (ProjectRootElement el)
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
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
		public void TestResourcesAdded ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (IsWindows)
				return;

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Foo", new System.Version (1, 0, 0, 0)),
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

			var plist = Path.Combine (unpackDir, "AppInvites-1.0.2", "Frameworks", "GINInvite.framework", "Versions", "A", "Resources", "GINInviteResources.bundle", "Info.plist");

			const string resourceName = "monotouch_content_GINInviteResources.bundle_fInfo.plist";
			prel.AddItem (
				"RestoreAssemblyResource",
				plist,
				new Dictionary<string,string> {
					{ "AssemblyName", "Foo" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamariniOSBuildResourceRestore", log);

			var ignoreMessages = new List<string> { "Enumeration yielded no results" };
			ignoreMessages.AddRange (DEFAULT_IGNORE_PATTERNS);
			AssertNoMessagesOrWarnings (log, ignoreMessages.ToArray());
			Assert.True (success);

			var plistExists = File.Exists (plist);
			Assert.True (plistExists);

			//check the referencepath has been replaced by the processed one
			var items = project.GetItems ("ReferencePath");

			var mergedItem = items.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude));
			Assert.True (mergedItem != null);

			var itemPath = mergedItem.EvaluatedInclude;

			//Assert.NotEqual (dll, itemPath);

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (itemPath);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.Equal (resourceName, resource.Name);
			var ps = PObject.FromStream (resource.GetResourceStream ()) as PDictionary;
			Assert.False (ps.ContainsKey ("CFBundleExecutable"));
			Assert.True (ps.Count > 0);
			var processedAsmMtime = File.GetLastWriteTime (itemPath);

			// check incremental build works
			project = new ProjectInstance (prel);
			log = new MSBuildTestLogger ();
			var newSuccess = BuildProject (engine, project, "_XamariniOSBuildResourceRestore", log);

			AssertNoMessagesOrWarnings (log, ignoreMessages.ToArray());
			Assert.True (success);

			var newItems = project.GetItems ("ReferencePath");
			var newItem = newItems.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude));
			var newItemPath = newItem.EvaluatedInclude;

			Assert.Equal (itemPath, newItemPath);
			Assert.Equal (processedAsmMtime, File.GetLastWriteTime (newItemPath));
		}


		/*[Fact]
		public void TestAndroidAarAddedFromCache ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (!IsWindows)
				testAndroidAarAdded (false);
		}

		[Fact]
		public void TestAndroidAarAddedFromAndroidSdk ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (!IsWindows)
				testAndroidAarAdded (true);
		}*/

		public void testAndroidAarAdded (bool useAndroidSdk)
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Xamarin.Android.Support.v7.CardView", new System.Version (1, 0, 0, 0)),
				"Main",
				ModuleKind.Dll
			);
			var dll = Path.Combine (TempDir, "Xamarin.Android.Support.v7.CardView.dll");
			asm.Write (dll);

			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "MonoAndroid");
			prel.SetProperty ("TargetFrameworkVersion", "v7.0");

			var aarPathInSdk = "$(AndroidSdkPath)\\extras\\android\\m2repository\\com\\android\\support\\cardview-v7\\25.0.0\\cardview-v7-25.0.0.aar";

			if (useAndroidSdk)
				prel.SetProperty ("AndroidSdkPath", Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "Library", "Developer", "Xamarin", "android-sdk-mac_x86"));

			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", Path.Combine (TempDir, "obj"));

			var item = prel.AddItem (
				"XamarinBuildDownload", "androidsupport-25.0.0", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r39.zip" },
					{ "Kind", "Zip" },
				});

			if (useAndroidSdk)
				item.Condition = "!Exists('" + aarPathInSdk + "')";

			prel.AddItem (
				"ReferencePath",
				dll
			);

			const string resourceName = "__AndroidLibraryProjects__.zip";
			var aarPath = "$(XamarinBuildDownloadDir)androidsupport-25.0.0\\m2repository\\com\\android\\support\\cardview-v7\\25.0.0\\cardview-v7-25.0.0.aar";

			prel.AddItem (
				"XamarinBuildDownloadRestoreAssemblyAar",
				useAndroidSdk ? aarPathInSdk : aarPath,
				new Dictionary<string, string> {
					{ "AssemblyName", "Xamarin.Android.Support.v7.CardView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();
			log.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Diagnostic;

			var success = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			AssertNoMessagesOrWarnings (log, DEFAULT_IGNORE_PATTERNS);
			Assert.True (success);

			//Assert.IsTrue (File.Exists (aar));

			//check the referencepath has been replaced by the processed one
			var items = project.GetItems ("ReferencePath");

			var mergedItem = items.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			Assert.True (mergedItem != null);

			var itemPath = mergedItem.EvaluatedInclude;

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (itemPath);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.Equal (resourceName, resource.Name);

			// Check that the embedded .aar has an appropriate prefix for each entry's path
			var hasWrongEntryNames = false;
			using (var zip = new ZipArchive (resource.GetResourceStream ())) {
				hasWrongEntryNames = zip.Entries.Any (ze => !ze.FullName.StartsWith ("library_project_imports", System.StringComparison.InvariantCulture));
			}
			Assert.False (hasWrongEntryNames);

			var processedAsmMtime = File.GetLastWriteTime (itemPath);

			// check incremental build works
			project = new ProjectInstance (prel);
			log = new MSBuildTestLogger ();
			log.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Diagnostic;
			var newSuccess = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			AssertNoMessagesOrWarnings (log);
			Assert.True (newSuccess);

			var newItems = project.GetItems ("ReferencePath");
			var newItem = newItems.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			Assert.True (newItem != null);
			var newItemPath = newItem.EvaluatedInclude;
			Assert.Equal (itemPath, newItemPath);
			Assert.Equal (processedAsmMtime, File.GetLastWriteTime (newItemPath));
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
			Assert.True (itemToDownload.Any (i => i.GetMetadata ("Url").EvaluatedValue == itemUrl));
			Assert.True (itemToDownload.Any (i => i.GetMetadata ("Url").EvaluatedValue == zipUrl));
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
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

			AssertNoMessagesOrWarnings (log);
			Assert.True (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.Equal (2, itemToDownload.Count);
			Assert.True (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}


		[Fact]
		public void TestAndroidAarResourceMergeModifyInPlaceAndStampFile ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (IsWindows)
				return;

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var intermediateDir = Path.Combine (TempDir, "obj");

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Xamarin.Android.Support.v7.CardView", new System.Version (1, 0, 0, 0)),
				"Main",
				ModuleKind.Dll
			);
			var dll = Path.Combine (TempDir, "Xamarin.Android.Support.v7.CardView.dll");
			asm.Write (dll);
			prel.AddItem ("ReferencePath", dll);

			var originalAsmDate = File.GetLastWriteTimeUtc (dll);


			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "MonoAndroid");
			prel.SetProperty ("TargetFrameworkVersion", "v7.0");
			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", intermediateDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.0/cardview.v7", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip" },
					{ "ToFile", "cardview.v7.aar" },
					{ "RangeStart", "196438127" },
					{ "RangeEnd", "196460160" },
				});


			const string resourceName = "__AndroidLibraryProjects__.zip";
			var aarPath = "$(XamarinBuildDownloadDir)androidsupport-25.0.0\\cardview.v7\\cardview.v7.aar";

			prel.AddItem (
				"XamarinBuildDownloadRestoreAssemblyAar",
				aarPath,
				new Dictionary<string, string> {
					{ "AssemblyName", "Xamarin.Android.Support.v7.CardView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			//var success = BuildProject (engine, project, "_XamarinBuildDownload", log);
			var success = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

//			AssertNoMessagesOrWarnings (log);
			Assert.True (success);
			//Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));

			success = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			Assert.True (File.Exists (Path.Combine (intermediateDir, "XbdMerge", "Xamarin.Android.Support.v7.CardView.dll.stamp")));

			Assert.True (File.GetLastWriteTimeUtc (dll) > originalAsmDate);
		}


		[Fact]
		public void TestAndroidAarManifestFixup ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (IsWindows)
				return;

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var intermediateDir = Path.Combine (TempDir, "obj");

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Xamarin.GooglePlayServices.Auth", new System.Version (1, 0, 0, 0)),
				"Main",
				ModuleKind.Dll
			);
			var dll = Path.Combine (TempDir, "Xamarin.GooglePlayServices.Auth.dll");
			asm.Write (dll);
			prel.AddItem ("ReferencePath", dll);

			var originalAsmDate = File.GetLastWriteTimeUtc (dll);


			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "MonoAndroid");
			prel.SetProperty ("TargetFrameworkVersion", "v7.0");
			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", intermediateDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "playservices-10.2.1/playservicesauth", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/google_m2repository_gms_v9_1_rc07_wear_2_0_1_rc3.zip" },
					{ "ToFile", "play-services-auth-10.2.1.aar" },
					{ "RangeStart", "12694130" },
					{ "RangeEnd", "12770642" },
				});


			const string resourceName = "__AndroidLibraryProjects__.zip";
			var aarPath = "$(XamarinBuildDownloadDir)playservices-10.2.1\\playservicesauth\\play-services-auth-10.2.1.aar";

			prel.AddItem (
				"XamarinBuildDownloadRestoreAssemblyAar",
				aarPath,
				new Dictionary<string, string> {
					{ "AssemblyName", "Xamarin.GooglePlayServices.Auth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			Assert.True (success);

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (dll);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.Equal (resourceName, resource.Name);

			// Check the embedded .aar to see if all the manifest entries were fixed up
			using (var zip = new ZipArchive (resource.GetResourceStream ())) {

				var manifestEntry = zip.Entries.FirstOrDefault (ze => ze.Name.EndsWith ("AndroidManifest.xml", StringComparison.OrdinalIgnoreCase));

				Assert.NotNull (manifestEntry);

				// android: namespace
				XNamespace xns = "http://schemas.android.com/apk/res/android";

				using (var xmlReader = System.Xml.XmlReader.Create (manifestEntry.Open ())) {
					var xdoc = XDocument.Load (xmlReader);

					var anyUnfixed = xdoc.Document.Descendants ()
										.Any (elem => elem.Attribute (xns + "name")?.Value?.StartsWith (".", StringComparison.Ordinal) ?? false);

					Assert.False (anyUnfixed);
				}
			}
		}


		[Fact]
		public void TestProguardText ()
		{
			// Tests won't run on windows due to file locking issues with assemblies
			if (IsWindows)
				return;

			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			var intermediateDir = Path.Combine (TempDir, "obj");

			var asm = AssemblyDefinition.CreateAssembly (
				new AssemblyNameDefinition ("Xamarin.GooglePlayServices.Basement", new System.Version (1, 0, 0, 0)),
				"Main",
				ModuleKind.Dll
			);
			var dll = Path.Combine (TempDir, "Xamarin.GooglePlayServices.Basement.dll");
			asm.Write (dll);
			prel.AddItem ("ReferencePath", dll);


			var unpackDir = GetTempPath ("unpacked");
			prel.SetProperty ("XamarinBuildDownloadDir", unpackDir);
			prel.SetProperty ("TargetFrameworkIdentifier", "MonoAndroid");
			prel.SetProperty ("TargetFrameworkVersion", "v7.0");
			prel.SetProperty ("OutputType", "Exe");
			prel.SetProperty ("IntermediateOutputPath", intermediateDir);

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "playservices-10.2.1/playservicesbasement", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/google_m2repository_gms_v9_1_rc07_wear_2_0_1_rc3.zip" },
					{ "ToFile", "play-services-basement-10.2.1.aar" },
					{ "RangeStart", "100833740" },
					{ "RangeEnd", "101168014" },
				});

			const string resourceName = "__AndroidLibraryProjects__.zip";
			var aarPath = "$(XamarinBuildDownloadDir)playservices-10.2.1\\playservicesbasement\\play-services-basement-10.2.1.aar";

			prel.AddItem (
				"XamarinBuildDownloadRestoreAssemblyAar",
				aarPath,
				new Dictionary<string, string> {
					{ "AssemblyName", "Xamarin.GooglePlayServices.Basement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" },
					{ "LogicalName", resourceName }
				}
			);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinAndroidBuildAarProguardConfigs", log);

			var ignorePatterns = new List<string> { "Enumeration yielded no results" };
			ignorePatterns.AddRange (DEFAULT_IGNORE_PATTERNS);

			AssertNoMessagesOrWarnings (log, ignorePatterns.ToArray());
			Assert.True (success);

			var proguardDir = Path.Combine (intermediateDir, "XbdMerge", "proguard");

			var proguardOutputFiles = Directory.GetFiles (proguardDir);

			Assert.True (proguardOutputFiles != null && proguardOutputFiles.Any (), "No proguard file found in intermediate output.");
		}
	}
}