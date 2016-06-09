using Foundation;
using UIKit;
using CoreGraphics;

using SegmentedControl;

namespace SDSegmentedControlSample
{
	public class ViewController : UIViewController
	{
		public override void LoadView()
		{
			base.LoadView();

			View.BackgroundColor = UIColor.White;

			// the main browser control
			var browser = new UIWebView();
			browser.Frame = new CGRect(0, 55, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height - 64);
			browser.AutosizesSubviews = true;

			// the segmented control
			var niceSegmentedCtrl = new SDSegmentedControl(new[] { "Google", "Bing", "Yahoo" });
			niceSegmentedCtrl.Frame = new CGRect(0, 10, 320, 44);
			niceSegmentedCtrl.SetImage(UIImage.FromBundle("google"), 0);
			niceSegmentedCtrl.SetImage(UIImage.FromBundle("bicon"), 1);
			niceSegmentedCtrl.SetImage(UIImage.FromBundle("yahoo"), 2);
			niceSegmentedCtrl.ValueChanged += (sender, e) =>
			{
				switch (niceSegmentedCtrl.SelectedSegment)
				{
					case 0:
						browser.LoadRequest(new NSUrlRequest(new NSUrl("https://google.com")));
						break;
					case 1:
						browser.LoadRequest(new NSUrlRequest(new NSUrl("https://bing.com")));
						break;
					case 2:
						browser.LoadRequest(new NSUrlRequest(new NSUrl("https://yahoo.com")));
						break;
				}
			};

			View.AddSubviews(niceSegmentedCtrl, browser);

			browser.LoadRequest(new NSUrlRequest(new NSUrl("https://google.com")));
		}
	}
}
