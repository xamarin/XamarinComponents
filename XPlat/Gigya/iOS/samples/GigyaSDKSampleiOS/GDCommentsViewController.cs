using Foundation;
using System;
using UIKit;

using GigyaSDK;

namespace GigyaSDKSampleiOS
{
	public partial class GDCommentsViewController : UIViewController, IGSPluginViewDelegate
	{
		public GDCommentsViewController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var parameters = new NSMutableDictionary();
			parameters["categoryID"] = (NSString)"comments_demo";
			parameters["streamID"] = (NSString)"ios";

			pluginView.WeakDelegate = this;
			pluginView.LoadPlugin("comments.commentsUI", parameters);
		}

		[Export("pluginView:finishedLoadingPluginWithEvent:")]
		public void FinishedLoadingPlugin(GSPluginView pluginView, NSDictionary evt)
		{
			Console.WriteLine("Finished loading plugin");
		}

		[Export("pluginView:firedEvent:")]
		public void FiredEvent(GSPluginView pluginView, NSDictionary evt)
		{
			Console.WriteLine("Plugin event received - {0}", evt["eventName"]);
		}

		[Export("pluginView:didFailWithError:")]
		public void DidFail(GSPluginView pluginView, NSError error)
		{
			Console.WriteLine("Received error from plugin - {0}", error.Description);
		}
	}
}
