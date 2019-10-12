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
using NUnit.Framework;
using Xamarin.ContentPipeline.Tests;
using Xamarin.MacDev;

namespace NativeLibraryDownloaderTests
{
	[TestFixture]
	class Test : TestsBase
	{
		public void AddCoreTargets (ProjectRootElement el)
		{
			var props = Path.Combine (
				Path.GetDirectoryName (GetType ().Assembly.Location),
				"..", "..", "..", "Xamarin.Build.Download", "bin", "Debug", "Xamarin.Build.Download.props"
			);
			el.AddImport (props);
			var targets = Path.Combine (
				Path.GetDirectoryName (GetType ().Assembly.Location),
				"..", "..", "..", "Xamarin.Build.Download", "bin", "Debug", "Xamarin.Build.Download.targets"
			);
			el.AddImport (targets);

		}

		[Test]
		public void NoArchivesOrTargets ()
		{
			var engine = new ProjectCollection ();
			var prel = ProjectRootElement.Create (Path.Combine (TempDir, "project.csproj"), engine);

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);

			var log = new MSBuildTestLogger ();
			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);
		}

		[Test]
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
			Assert.AreEqual (4, errors.Count);
			Assert.AreEqual ("Invalid item ID -----", errors[0]);
			Assert.AreEqual ("Unknown archive kind 'Cabbage' for 'https://www.example.com/bar.zip'", errors[1]);
			Assert.AreEqual ("Unknown archive kind '' for 'https://www.example.com/bar.unknown'", errors[2]);
			Assert.AreEqual ("Missing required Url metadata on item foo-1.2", errors[3]);

			Assert.IsFalse (success);
		}

		[Test]
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

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "ILRepack-2.0.10", "ILRepack.nuspec")));
		}

		[Test]
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
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "GoogleSymbolUtilities-1.0.3", "Libraries", "libGSDK_Overload.a")));
		}

		[Test]
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
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "FacebookAndroid-4.17.0", "facebook-android-sdk.aar")));
		}

		[Test]
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
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "FacebookAndroid-4.17.0", "FacebookAndroid-4.17.0.uncompressed")));
		}

		//in google maps, the tar inside the tgz doesn't match the tgz name
		[Test]
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

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "GMaps-1.11.1", "CHANGELOG")));
		}

		[Test]
		public void TestResourcesAdded ()
		{
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

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (plist));

			//check the referencepath has been replaced by the processed one
			var items = project.GetItems ("ReferencePath");

			var mergedItem = items.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			Assert.IsTrue (mergedItem != null);

			var itemPath = mergedItem.EvaluatedInclude;

			Assert.AreNotEqual (dll, itemPath);

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (itemPath);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.AreEqual (resourceName, resource.Name);
			var ps = PObject.FromStream (resource.GetResourceStream ()) as PDictionary;
			Assert.IsFalse (ps.ContainsKey ("CFBundleExecutable"));
			Assert.Greater (ps.Count, 0);
			var processedAsmMtime = File.GetLastWriteTime (itemPath);

			// check incremental build works
			project = new ProjectInstance (prel);
			log = new MSBuildTestLogger ();
			var newSuccess = BuildProject (engine, project, "_XamariniOSBuildResourceRestore", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			var newItems = project.GetItems ("ReferencePath");
			var newItem = newItems.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			var newItemPath = newItem.EvaluatedInclude;

			Assert.AreEqual (itemPath, newItemPath);
			Assert.AreEqual (processedAsmMtime, File.GetLastWriteTime (newItemPath));
		}


		[Test]
		public void TestAndroidAarAddedFromCache ()
		{
			testAndroidAarAdded (false);
		}

		[Test]
		public void TestAndroidAarAddedFromAndroidSdk ()
		{
			testAndroidAarAdded (true);
		}

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
					{ "Sha1", "89ad37d67a1018c42be36933cec3d7712141d42c" },
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

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			//Assert.IsTrue (File.Exists (aar));

			//check the referencepath has been replaced by the processed one
			var items = project.GetItems ("ReferencePath");

			var mergedItem = items.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			Assert.IsTrue (mergedItem != null);

			var itemPath = mergedItem.EvaluatedInclude;

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (itemPath);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.AreEqual (resourceName, resource.Name);

			// Check that the embedded .aar has an appropriate prefix for each entry's path
			var hasWrongEntryNames = false;
			using (var zip = new ZipArchive (resource.GetResourceStream ())) {
				hasWrongEntryNames = zip.Entries.Any (ze => !ze.FullName.StartsWith ("library_project_imports", System.StringComparison.InvariantCulture));
			}
			Assert.IsFalse (hasWrongEntryNames);

			var processedAsmMtime = File.GetLastWriteTime (itemPath);

			// check incremental build works
			project = new ProjectInstance (prel);
			log = new MSBuildTestLogger ();
			log.Verbosity = Microsoft.Build.Framework.LoggerVerbosity.Diagnostic;
			var newSuccess = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (newSuccess);

			var newItems = project.GetItems ("ReferencePath");
			var newItem = newItems.FirstOrDefault (i => !string.IsNullOrEmpty (i.EvaluatedInclude) && i.EvaluatedInclude != dll);
			Assert.IsTrue (newItem != null);
			var newItemPath = newItem.EvaluatedInclude;
			Assert.AreEqual (itemPath, newItemPath);
			Assert.AreEqual (processedAsmMtime, File.GetLastWriteTime (newItemPath));
		}

		[Test]
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.IsEmpty (itemToDownload);
		}

		[Test]
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.AreEqual (2, itemToDownload.Count);
			Assert.IsTrue (itemToDownload.Any (i => i.GetMetadata ("Url").EvaluatedValue == itemUrl));
			Assert.IsTrue (itemToDownload.Any (i => i.GetMetadata ("Url").EvaluatedValue == zipUrl));
		}

		[Test]
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
			Assert.IsTrue (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.AreEqual (1, itemToDownload.Count);
			Assert.IsTrue (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}

		[Test]
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
			Assert.IsTrue (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.AreEqual (1, itemToDownload.Count);
			Assert.IsTrue (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}

		[Test]
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

				Assert.IsFalse (success);
				Assert.IsNull (log.Errors.FirstOrDefault (err => err.Code == "XBD005"));
			}
		}

		[Test]
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));
		}


		[Test]
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/recyclerview.v7", new Dictionary<string, string> {
					{ "Url", "https://dl-ssl.google.com/android/repository/android_m2repository_r40.zip" },
					{ "ToFile", "recyclerview.v7.aar" },
					{ "RangeStart", "199278205" },
					{ "RangeEnd", "199589731" },
					{ "Md5", "9be3f5e09877f1f308037659cb2a7636" }
				});

			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "_XamarinBuildDownload", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));
			Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "recyclerview.v7", "recyclerview.v7.aar")));
		}



		[Test]
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
				});

			prel.AddItem (
				"XamarinBuildDownloadPartialZip", "androidsupport-25.0.1/recyclerview.v7", new Dictionary<string, string> {
					{ "Url", itemUrl },
					{ "ToFile", "recyclerview.v7.aar" },
					{ "RangeStart", "199278205" },
					{ "RangeEnd", "199589731" },
					{ "Md5", "9be3f5e09877f1f308037659cb2a7636" }
				});
			
			AddCoreTargets (prel);

			var project = new ProjectInstance (prel);
			var log = new MSBuildTestLogger ();

			var success = BuildProject (engine, project, "XamarinBuildDownloadGetItemsToDownload", log);

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);

			var itemToDownload = project.GetItems ("XamarinBuildDownloadItemToDownload");

			Assert.AreEqual (2, itemToDownload.Count);
			Assert.IsTrue (itemToDownload.First ().GetMetadata ("Url").EvaluatedValue == itemUrl);
		}


		[Test]
		public void TestAndroidAarResourceMergeModifyInPlaceAndStampFile ()
		{
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
					{ "Md5", "b44eb88f7cc621ae616744c6646f5b64" }
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
			Assert.IsTrue (success);
			//Assert.IsTrue (File.Exists (Path.Combine (unpackDir, "androidsupport-25.0.1", "cardview.v7", "cardview.v7.aar")));

			success = BuildProject (engine, project, "_XamarinAndroidBuildAarRestore", log);

			Assert.IsTrue (File.Exists (Path.Combine (intermediateDir, "XbdMerge", "Xamarin.Android.Support.v7.CardView.dll.stamp")));

			Assert.IsTrue (File.GetLastWriteTimeUtc (dll) > originalAsmDate);
		}


		[Test]
		public void TestAndroidAarManifestFixup ()
		{
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
					{ "Md5", "f4d814a0a434c09577a9b5a9d62da1f6" }
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

			Assert.IsTrue (success);

			//check the assembly has the processed resource
			var processedAsm = AssemblyDefinition.ReadAssembly (dll);
			var resource = processedAsm.MainModule.Resources.FirstOrDefault () as EmbeddedResource;
			Assert.NotNull (resource);
			Assert.AreEqual (resourceName, resource.Name);

			// Check the embedded .aar to see if all the manifest entries were fixed up
			using (var zip = new ZipArchive (resource.GetResourceStream ())) {

				var manifestEntry = zip.Entries.FirstOrDefault (ze => ze.Name.EndsWith ("AndroidManifest.xml", StringComparison.OrdinalIgnoreCase));

				Assert.IsNotNull (manifestEntry);

				// android: namespace
				XNamespace xns = "http://schemas.android.com/apk/res/android";

				using (var xmlReader = System.Xml.XmlReader.Create (manifestEntry.Open ())) {
					var xdoc = XDocument.Load (xmlReader);

					var anyUnfixed = xdoc.Document.Descendants ()
										.Any (elem => elem.Attribute (xns + "name")?.Value?.StartsWith (".", StringComparison.Ordinal) ?? false);

					Assert.IsFalse (anyUnfixed);
				}
			}
		}


		[Test]
		public void TestProguardText ()
		{
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
					{ "Md5", "1ddf95b31e73f7a79e39df81875797ae" }
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

			var proguardConfigItems = project.GetItems ("ProguardConfiguration");

			AssertNoMessagesOrWarnings (log);
			Assert.IsTrue (success);
			Assert.IsTrue (proguardConfigItems.Any ());
		}
	}
}