using Foundation;
using UIKit;

using OpenId.AppAuth;

namespace OpenIdAuthSampleiOS
{
	[Register(nameof(AppDelegate))]
	public class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window { get; set; }

		// The authorization flow session which receives the return URL from SFSafariViewController.
		public IAuthorizationFlowSession CurrentAuthorizationFlow { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}

		// Handles inbound URLs. Checks if the URL matches the redirect URI for a pending
		// AppAuth authorization request.
		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// Sends the URL to the current authorization flow (if any) which will process it if it relates to
			// an authorization response.
			if (CurrentAuthorizationFlow?.ResumeAuthorizationFlow(url) == true)
			{
				return true;
			}

			// Your additional URL handling (if any) goes here.

			return false;
		}
	}
}
