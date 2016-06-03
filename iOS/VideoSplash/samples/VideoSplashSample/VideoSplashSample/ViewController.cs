using System;
using CoreGraphics;
using Foundation;
using UIKit;

using VideoSplash;

namespace VideoSplashSample
{
    partial class ViewController : VideoViewController
    {
        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            StartTime = 12.0f;
            Duration = 4.0f;
            Alpha = 0.7f;
            VideoUrl = new NSUrl(NSBundle.MainBundle.PathForResource("test", "mp4"), false);

            leftButton.Layer.CornerRadius = 2;
            leftButton.Layer.MasksToBounds = true;

            rightButton.Layer.CornerRadius = 2;
            rightButton.Layer.MasksToBounds = true;

            text.Font = UIFont.FromName("Museo500-Regular", 30);
        }
    }
}
