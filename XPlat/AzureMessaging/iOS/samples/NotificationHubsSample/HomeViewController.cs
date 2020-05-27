using MonoTouch.Dialog;
using UIKit;

namespace AzureMessagingSampleiOS
{
	public class HomeViewController : DialogViewController
	{
		public HomeViewController (bool needsConfig) : base (new RootElement ("Azure Messaging"))
		{
			var msg = needsConfig ? 
				"Please configure AppDelegate.cs with your own HUB_NAME and CONNECTION_STRING values!"
				: "Registering for Remote Notifications...";

			Root.Add (new Section { new StyledMultilineElement (msg) });
		}

		public void ProcessNotification (string title, string message)
		{
			InvokeOnMainThread (() => {
				var av = new UIAlertView (title, message, null, "OK");
				av.Show ();
			});
		}
	}
}

