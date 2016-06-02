using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using UIKit;
using CoreGraphics;

using SCCatWaitingHUD;

namespace SCCatWaitingHUDSample
{
	partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var loader = new CatWaitingHUD ();
			loader.Title = "Big Cats... ";

			// show/hide the loader
			View.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				if (!loader.IsAnimating) {
					loader.Start ();
				} else {
					loader.Stop ();
				}
			}));
		}
	}
}
