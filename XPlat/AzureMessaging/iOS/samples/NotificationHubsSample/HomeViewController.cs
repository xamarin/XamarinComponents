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
			InvokeOnMainThread(() => {
				var avc = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
				var action = UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null);
				avc.AddAction(action);
				PresentViewController(avc, true, null);
			});
		}
	}
}

