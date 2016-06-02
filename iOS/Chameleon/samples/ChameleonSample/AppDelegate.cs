using Foundation;
using UIKit;

using ChameleonFramework;

namespace ChameleonSample
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// get the strong colors as the theme
			var image = UIImage.FromBundle ("africa-blue.jpg");
			var colors = ChameleonColorArray.GetColors (image, false);
			Chameleon.SetGlobalTheme (
				colors [2],
				colors [4],
				ContentStyle.Contrast);

			return true;
		}
	}
}
