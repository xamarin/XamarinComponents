using System;

using Foundation;
using AppKit;
using ITSwitch;
using CoreGraphics;

namespace ITSwitchSample
{
	public partial class MainWindow : NSWindow
	{

		public MainWindow(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public MainWindow(NSCoder coder) : base(coder)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();

		}
	}
}
