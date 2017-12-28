using System;
using UIKit;

namespace YouTubePlayerSample
{
	public partial class MainNavigationController : UINavigationController
	{
		public MainNavigationController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UINavigationBar.Appearance.TintColor = UIColor.White;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}
