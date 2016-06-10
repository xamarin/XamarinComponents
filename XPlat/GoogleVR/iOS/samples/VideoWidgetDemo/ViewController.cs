using System;
using UIKit;
using Google.VR;
using Foundation;

namespace VideoWidgetDemo
{
	public partial class ViewController : UIViewController, IGVRVideoViewDelegate
	{
		
		public bool IsPaused {
			get;
			set;
		}

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string sourceText = @"Source: ";
			NSMutableAttributedString attributedText = new NSMutableAttributedString (@"Wikipedia");
			attributedText.AddAttribute (UIStringAttributeKey.Link, new NSString( @"https://en.wikipedia.org/wiki/Gorilla"), new NSRange (sourceText.Length, attributedText.Length - sourceText.Length));

			attributionTextView.AttributedText = attributedText;

			videoView.Delegate = this;
			videoView.EnableFullscreenButton = true;
			videoView.EnableCardboardButton = true;

			IsPaused = false;
				
			videoView.LoadFromUrl (  NSUrl.FromFilename ("congo.mp4"));

		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


		public void DidUpdatePosition (GVRVideoView videoView, double position)
		{
			if (position == videoView.Duration) {
				videoView.SeekTo (0);
				videoView.Resume ();
			}
		}
	}
}

