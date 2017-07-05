using System;

using Foundation;
using AppKit;
using ITSwitch;
using CoreGraphics;

namespace ITSwitchSample
{
	public partial class MainWindowController : NSWindowController
	{
		public MainWindowController(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
		}

		public MainWindowController() : base("MainWindow")
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

			var sWitch = new ITSwitchView(new CGRect(20,103,32,20))
			{
				TintColor = NSColor.Red,
				IsOn = true,
			};

			sWitch.OnSwitchChanged += (object sender, EventArgs e) => 
			{
				Console.WriteLine((sWitch.IsOn) ? "enabled" : "disabled");
			}; 


			this.Window.ContentView.AddSubview(sWitch);
			this.Window.ContentView.AddSubview(new ITSwitchView(new CGRect(60,103,32,20)));
			this.Window.ContentView.AddSubview(new ITSwitchView(new CGRect(100,103,32,20))
			{
				TintColor = NSColor.Blue,
				IsOn = true,
			});
		}

		public new MainWindow Window
		{
			get { return (MainWindow)base.Window; }
		}
	}
}
