using Foundation;
using UIKit;
using Mapbox;

namespace MapBoxSampleiOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window {
            get;
            set;
        }

        const string MAPBOX_ACCESS_TOKEN = "YOUR-ACCESS-TOKEN";

        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
            if (MAPBOX_ACCESS_TOKEN == "YOUR-ACCESS-TOKEN") {
                new UIAlertView ("Change Access Token", "You need to change the MAPBOX_ACCESS_TOKEN to your own token from MapBox!", null, "OK")
                    .Show ();
            }

            // Set your own Access Token 
            AccountManager.AccessToken = MAPBOX_ACCESS_TOKEN;

            return true;
        }
    }
}


