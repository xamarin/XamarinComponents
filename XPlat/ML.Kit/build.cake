#tool nuget:?package=XamarinComponent

#addin nuget:?package=Cake.XCode
#addin nuget:?package=Cake.Xamarin.Build
#addin nuget:?package=Cake.Xamarin
#addin nuget:?package=Cake.FileHelpers

var TARGET = Argument ("t", Argument ("target", "ci"));

Dictionary<string, string> URLS_ARTIFACT_FILES= new Dictionary<string, string>()
{
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/barcode-scanning/16.0.0/barcode-scanning-16.0.0.aar",
		$"./externals/android/barcode-scanning-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/common/16.0.0/common-16.0.0.aar",
		$"./externals/android/common-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/face-detection/16.0.0/face-detection-16.0.0.aar",
		$"./externals/android/face-detection-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/image-labeling/16.0.0/image-labeling-16.0.0.aar",
		$"./externals/android/image-labeling-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/image-labeling-automl/16.0.0/image-labeling-automl-16.0.0.aar",
		$"./externals/android/image-labeling-automl-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/image-labeling-common/16.0.0/image-labeling-common-16.0.0.aar",
		$"./externals/android/image-labeling-common-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/image-labeling-custom/16.0.0/image-labeling-custom-16.0.0.aar",
		$"./externals/android/image-labeling-custom-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/language-id/16.0.0/language-id-16.0.0.aar",
		$"./externals/android/language-id-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/linkfirebase/16.0.0/linkfirebase-16.0.0.aar",
		$"./externals/android/linkfirebase-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/object-detection/16.0.0/object-detection-16.0.0.aar",
		$"./externals/android/object-detection-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/object-detection-common/16.0.0/object-detection-common-16.0.0.aar",
		$"./externals/android/object-detection-common-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/object-detection-custom/16.0.0/object-detection-custom-16.0.0.aar",
		$"./externals/android/object-detection-custom-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/smart-reply/16.0.0/smart-reply-16.0.0.aar",
		$"./externals/android/smart-reply-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/translate/16.0.0/translate-16.0.0.aar",
		$"./externals/android/translate-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/vision-common/16.0.0/vision-common-16.0.0.aar",
		$"./externals/android/vision-common-16.0.0.aar"
	},
	{
		$"https://dl.google.com/android/maven2/com/google/mlkit/vision-internal-vkp/16.0.0/vision-internal-vkp-16.0.0.aar",
		$"./externals/android/vision-internal-vkp-16.0.0.aar"
	},
};


Dictionary<string, List<string>> CocoaPods = new Dictionary<string, List<string>>
{
	{
		"TextDetection",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision', '6.25.0'",
			"# If using an on-device API:",
			"pod 'Firebase/MLVisionTextModel', '6.25.0'",
			"  use_frameworks!",
			"end",
		}
	},
	{
		"FaceDetection",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision', '6.25.0'",
			"# If you want to detect face contours (landmark detection and classification",
			"# don't require this additional model):",
			"pod 'Firebase/MLVisionFaceModel', '6.25.0'",
			"  use_frameworks!",
			"end",
		}
	},
	{
		"BarcodeDetection",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision'",
			"pod 'Firebase/MLVisionBarcodeModel'",
			"  use_frameworks!",
			"end",
		}
	},
	{
		"ImageLabeling",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision', '6.25.0'",
			"# If using the on-device API:",
			"pod 'Firebase/MLVisionLabelModel', '6.25.0'",
			"  use_frameworks!",
			"end",
		}
	},
	{
		"ObjectDetectionAndTracking",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision', '6.25.0'",
			"pod 'Firebase/MLVisionObjectDetection', '6.25.0'",
			"  use_frameworks!",
			"end",
		}
	},
	{
		"LandmarksDetection",
		new List<string>
		{
			"platform :ios, '12.0'",
			"install! 'cocoapods', :integrate_targets => false",
			"target 'Xamarin' do",
			"pod 'Firebase/MLVision', '6.25.0'",
			"  use_frameworks!",
			"end",
		}
	},
};

Task ("externals")
	.Does
	(
		() =>
		{
			Information("externals ...");
			EnsureDirectoryExists("./externals/android");

			foreach(KeyValuePair<string, string> URL_ARTIFACT_FILE in URLS_ARTIFACT_FILES)
			{
				string URL = URL_ARTIFACT_FILE.Key;
				string ARTIFACT_FILE = URL_ARTIFACT_FILE.Value;

				Information($"    downloading ...");
				Information($"                {URL}");
				Information($"    to ");
				Information($"                {ARTIFACT_FILE}");
				if ( ! string.IsNullOrEmpty(URL) && ! FileExists(ARTIFACT_FILE))
				{
					DownloadFile (URL, ARTIFACT_FILE);
					DownloadFile (URL.Replace(".aar", ".pom"), ARTIFACT_FILE.Replace(".aar", ".pom"));
				}
			}

			EnsureDirectoryExists("./externals/ios");
			if ( ! IsRunningOnWindows() )
			{
				foreach((string key, List<string> value) in CocoaPods)
				{
					if (CocoaPodVersion () < new System.Version (1, 0))
					{
						value.RemoveAt (1);
					}

					EnsureDirectoryExists($"./externals/ios/{key}");
					FileWriteLines ($"./externals/ios/{key}/Podfile", value.ToArray ());
					CocoaPodRepoUpdate ();

					CocoaPodInstall 
						(
							$"./externals/ios/{key}", 
							new CocoaPodInstallSettings { NoIntegrate = true }
						);
				}
				
			}
      
			return;
		}
	);

Task("libs")
	.IsDependentOn("externals")
	.Does
	(
		() =>
		{
			MSBuild
			(
				"./Android/source/Xamarin.GoogleFirebase.ML.Kit.sln",
				c => 
				{
					c.Configuration = "Release";
					c.Restore = true;
					c.MaxCpuCount = 0;
					c.Properties.Add("DesignTimeBuild", new [] { "false" });
					c.EnableBinaryLogger($"./output/libs.binlog");
					c.AddFileLogger
								(
									new MSBuildFileLogger 
									{
										LogFile = "./output/errors.txt",
										MSBuildFileLoggerOutput = MSBuildFileLoggerOutput.ErrorsOnly   
									}
								);
				}
			);

			return;
		}
	);

Task ("clean")
	.Does
	(
		() =>
		{
			if (DirectoryExists ("./externals/"))
			{
				DeleteDirectory ("./externals/", true);
			}
			if (DirectoryExists ("./output/"))
			{
				DeleteDirectory ("./output/", true);
			}
		}
	);

Task("nuget")
	.IsDependentOn("libs")
	.Does
	(
		() =>
		{
			EnsureDirectoryExists("./output");

			MSBuild
			(
				"./Android/source/Xamarin.GoogleFirebase.ML.Kit.sln",
				configuration =>
					configuration
						.SetConfiguration("Release")
						.WithTarget("Pack")
						//.WithProperty("PackageVersion", NUGET_VERSION)
						.WithProperty("PackageOutputPath", "../../output")
			);
		}
);

Task("ci")
	.IsDependentOn("libs")
	.IsDependentOn("nuget")
	.Does
	(
		() => {}
	);

RunTarget (TARGET);

