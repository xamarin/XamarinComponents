using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif

using MonoTouch.Dialog;

using SDWebImage;

namespace SDWebImageMTDialogSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		DialogViewController dvcController;
		UINavigationController navController;
		List<string> objects;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			InitListOfImages ();
			var root = new RootElement ("SDWebImage Sample") {
				new Section ()
			};
			int count = 0;

			foreach (var item in objects) {
				count++;
				string url = item;

				var imgElement = new ImageLoaderStringElement (
						caption: string.Format ("Image #{0}", count),
						tapped: () => { HandleTapped (url); },
						imageUrl: new NSUrl (url), 
						placeholder: UIImage.FromBundle ("placeholder")
					);

				root[0].Add (imgElement);
			}

			dvcController = new DialogViewController (UITableViewStyle.Plain, root);
			dvcController.NavigationItem.RightBarButtonItem = new UIBarButtonItem ("Clear Cache", UIBarButtonItemStyle.Plain, ClearCache);
			navController = new UINavigationController (dvcController);
			window.RootViewController = navController;
			window.MakeKeyAndVisible ();
			
			return true;
		}

		void HandleTapped (string url)
		{
			string largeImageURL = url.Replace ("small", "source");

			var dvcDetails = new DetailViewController (new NSUrl (largeImageURL));
			navController.PushViewController (dvcDetails, true);
		}

		void ClearCache (object sender, EventArgs e)
		{
			SDWebImageManager.SharedManager.ImageCache.ClearMemory ();
			SDWebImageManager.SharedManager.ImageCache.ClearDisk ();
		}

		void InitListOfImages ()
		{
			objects = new List<string> ()
			{
				@"http://assets.sbnation.com/assets/2512203/dogflops.gif",
				@"https://raw.githubusercontent.com/liyong03/YLGIFImage/master/YLGIFImageDemo/YLGIFImageDemo/joy.gif",
				@"http://www.ioncannon.net/wp-content/uploads/2011/06/test2.webp",
				@"http://www.ioncannon.net/wp-content/uploads/2011/06/test9.webp",
				@"http://littlesvr.ca/apng/images/SteamEngine.webp",
				@"http://littlesvr.ca/apng/images/world-cup-2014-42.webp",
				@"https://nr-platform.s3.amazonaws.com/uploads/platform/published_extension/branding_icon/275/AmazonS3.png"
			};

			for (int i = 0; i < 100; i++)
			{
				objects.Add (string.Format ("https://s3.amazonaws.com/fast-image-cache/demo-images/FICDDemoImage{0}.jpg", i.ToString ("D3")));
			}
		}
	}
}

