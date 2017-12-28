using System;

using AppKit;
using Foundation;
using ITSwitch;
using CoreGraphics;

namespace ITSwitchSample
{
	public partial class ViewController : NSViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var sWitch = new ITSwitchView (new CGRect (20, 103, 32, 20))
			{
				TintColor = NSColor.Red,
				IsOn = true,
			};

			sWitch.OnSwitchChanged += (object sender, EventArgs e) =>
			{
				Console.WriteLine ((sWitch.IsOn) ? "enabled" : "disabled");
			};


			View.AddSubview (sWitch);
			View.AddSubview (new ITSwitchView (new CGRect (60, 103, 32, 20)));
			View.AddSubview (new ITSwitchView (new CGRect (100, 103, 32, 20))
			{
				TintColor = NSColor.Blue,
				IsOn = true,
			});
		}

		public override NSObject RepresentedObject
		{
			get
			{
				return base.RepresentedObject;
			}
			set
			{
				base.RepresentedObject = value;
				// Update the view, if already loaded.
			}
		}
	}
}
