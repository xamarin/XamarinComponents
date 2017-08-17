using System;
using System.Diagnostics;
using Foundation;
using UIKit;

using TwitterImagePipeline;

namespace TwitterImagePipelineDemo
{
	[Register(nameof(AppDelegate))]
	public partial class AppDelegate : UIApplicationDelegate //, ITIPLogger, ITIPImagePipelineObserver, ITIPImageAdditionalCache
	{
		private int opCount = 0;
		private UITabBarController tabBarController;

		public static AppDelegate Current => (AppDelegate)UIApplication.SharedApplication.Delegate;

		public override UIWindow Window { get; set; }

		public TIPImagePipeline ImagePipeline { get; private set; }

		public bool UsePlaceholder { get; set; } = false;
		public int SearchCount { get; set; } = 50;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
#if DEBUG // the logger should be removed for release builds
			TIPGlobalConfiguration.SharedInstance.Logger = new Logger();
#endif
			TIPGlobalConfiguration.SharedInstance.SerializeCGContextAccess = true;
			TIPGlobalConfiguration.SharedInstance.ClearMemoryCachesOnApplicationBackgroundEnabled = true;
			TIPGlobalConfiguration.SharedInstance.AddImagePipelineObserver(new Observer());

			ImagePipeline = new TIPImagePipeline("Twitter.Example");
			ImagePipeline.AdditionalCaches = new[] { new AdditionalCache() };

			TwitterApi.SharedInstance.WorkStarted += delegate { IncrementNetworkOperations(); };
			TwitterApi.SharedInstance.WorkFinished += delegate { DecrementNetworkOperations(); };

			// appearance
			var lightBlueColor = UIColor.FromRGB(150f / 255f, 215f / 255f, 1);
			UISearchBar.Appearance.BarTintColor = lightBlueColor;
			UISearchBar.Appearance.TintColor = UIColor.White;
			UITextField.AppearanceWhenContainedIn(typeof(UISearchBar)).TintColor = lightBlueColor;
			UINavigationBar.Appearance.BarTintColor = lightBlueColor;
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
			UITabBar.Appearance.BarTintColor = lightBlueColor;
			UITabBar.Appearance.TintColor = UIColor.White;
			UISlider.Appearance.MinimumTrackTintColor = lightBlueColor;
			UISlider.Appearance.TintColor = lightBlueColor;
			UIWindow.Appearance.TintColor = lightBlueColor;

			// the UI
			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			// Image from https://icons8.com
			var firstNavController = new UINavigationController(new TwitterSearchViewController());
			firstNavController.TabBarItem = new UITabBarItem("Search", UIImage.FromBundle("Search"), 1);
			var secondNavController = new UINavigationController(new SettingsViewController());
			secondNavController.TabBarItem = new UITabBarItem("Settings", UIImage.FromBundle("Settings"), 2);
			var thirdNavController = new UINavigationController(new InspectorViewController());
			thirdNavController.TabBarItem = new UITabBarItem("Inspector", UIImage.FromBundle("Inspector"), 3);

			tabBarController = new UITabBarController();
			tabBarController.ViewControllers = new[] {
				firstNavController,
				secondNavController,
				thirdNavController
			};

			Window.RootViewController = tabBarController;
			Window.BackgroundColor = UIColor.Orange;
			Window.MakeKeyAndVisible();

			return true;
		}

		public static void IncrementNetworkOperations()
		{
			if (++AppDelegate.Current.opCount > 0)
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}

		public static void DecrementNetworkOperations()
		{
			if (--AppDelegate.Current.opCount <= 0)
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
		}
	}

	public class AdditionalCache : TIPImageAdditionalCache
	{
		private Lazy<UIImage> placeholderImage = new Lazy<UIImage>(() => UIImage.FromBundle("placeholder.jpg"));

		public override void RetrieveImageForUrl(NSUrl url, TIPImageAdditionalCacheFetchCompletion completion)
		{
			UIImage image = null;
			if (url.Scheme == "placeholder" && url.Host == "placeholder.com" && url.LastPathComponent == "placeholder.jpg")
			{
				image = placeholderImage.Value;
			}
			completion(image);
		}
	}

	public class Observer : TIPImagePipelineObserver
	{
		public override void DidStartDownloadingImage(TIPImageFetchOperation op, NSUrl url)
		{
			AppDelegate.IncrementNetworkOperations();
		}

		public override void DidFinishDownloadingImage(TIPImageFetchOperation op, NSUrl url, string type, nuint byteSize, CoreGraphics.CGSize dimensions, bool wasResumed)
		{
			AppDelegate.DecrementNetworkOperations();
		}
	}

	public class Logger : TIPSimpleLogger
	{
		public override void Log(TIPLogLevel level, string file, string function, int line, string message)
		{
			string levelString = null;
			switch (level)
			{
				case TIPLogLevel.Emergency:
				case TIPLogLevel.Alert:
				case TIPLogLevel.Critical:
				case TIPLogLevel.Error:
					levelString = "ERR";
					break;
				case TIPLogLevel.Warning:
					levelString = "WRN";
					break;
				case TIPLogLevel.Notice:
				case TIPLogLevel.Information:
					levelString = "INF";
					break;
				case TIPLogLevel.Debug:
					levelString = "DBG";
					break;
			}

			Debug.WriteLine($"[{levelString}]: {message}");
		}

		public override bool CanLog(TIPLogLevel level) => true; // we want everything!
	}
}
