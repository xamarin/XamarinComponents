using System;
using System.Collections.Generic;
using System.Linq;

#if __UNIFIED__
using Foundation;
using UIKit;
using ObjCRuntime;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;
using nuint = global::System.UInt32;
using NSUrlConnectionDataDelegate = MonoTouch.Foundation.NSUrlConnectionDelegate;
#endif

using MBProgressHUD;
using MonoTouch.Dialog;

namespace MBProgressHUDDemo
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.

	// Custom NSUrlConnectionDelegate that handles NSUrlConnection Events
	public class MyNSUrlConnectionDelegete : NSUrlConnectionDataDelegate
	{
		AppDelegate del;
		MTMBProgressHUD hud;

		public MyNSUrlConnectionDelegete (AppDelegate del, MTMBProgressHUD hud)
		{
			this.del = del;
			this.hud = hud;
		}

		public override void ReceivedResponse (NSUrlConnection connection, NSUrlResponse response)
		{
			del.expectedLength = response.ExpectedContentLength;
			del.currentLength = 0;
			hud.Mode = MBProgressHUDMode.Determinate;
		}

		public override void ReceivedData (NSUrlConnection connection, NSData data)
		{
			del.currentLength += data.Length;
			hud.Progress = (del.currentLength / (float) del.expectedLength);
		}

		public override void FinishedLoading (NSUrlConnection connection)
		{
			BeginInvokeOnMainThread ( ()=> {
				hud.CustomView = new UIImageView (UIImage.FromBundle ("37x-Checkmark.png"));
			});
			hud.Mode = MBProgressHUDMode.CustomView;
			hud.Hide(true, 2);
		}

		public override void FailedWithError (NSUrlConnection connection, NSError error)
		{
			hud.Hide(true);
		}
	}
}
