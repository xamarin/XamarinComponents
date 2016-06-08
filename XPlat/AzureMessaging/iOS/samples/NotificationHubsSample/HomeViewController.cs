using System;
using MonoTouch.Dialog;
using UIKit;

namespace AzureMessagingSampleiOS
{
	public class HomeViewController : DialogViewController
	{
		public HomeViewController (bool needsConfig) : base (new RootElement ("Azure Messaging"))
		{
			var msg = needsConfig ? 
				"Please configure AppDelegate.cs with your own HUB_NAME and HUB_LISTEN_SECRET values!"
				: "Registering for Remote Notifications...";

			Root.Add (new Section { new StyledMultilineElement (msg) });
		}

		public void ProcessNotification (string alert)
		{
			InvokeOnMainThread (() => {
				var av = new UIAlertView ("Notification", alert, null, "OK");
				av.Show ();
			});
		}

		public void RegisteredForNotifications (string msg)
		{
			InvokeOnMainThread (() => 
				((StyledMultilineElement)Root [0] [0]).Caption = msg);
		}
	}
}

