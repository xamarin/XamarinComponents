using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using VideoSplash;

namespace VideoSplashSampleClassic
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

            FillMode = ScalingMode.ResizeAspectFill;
            Repeat = true;
            Sound = true;
            StartTime = 12.0f;
            Duration = 4.0f;
            Alpha = 0.7f;
            BackgroundColor = UIColor.Black;
            VideoUrl = new NSUrl(NSBundle.MainBundle.PathForResource("test", "mp4"), false);

            // Sample UI
            var leftButton = new UIButton(new RectangleF(15, View.Frame.Height / 4 * 3, 140, 42));
            leftButton.SetImage(UIImage.FromBundle("btnSignin"), UIControlState.Normal);
            leftButton.Layer.CornerRadius = 2;
            leftButton.Layer.MasksToBounds = true;
            View.AddSubview(leftButton);

            var rightButton = new UIButton(new RectangleF(165, View.Frame.Height / 4 * 3, 140, 42));
            rightButton.SetImage(UIImage.FromBundle("btnRegister"), UIControlState.Normal);
            rightButton.Layer.CornerRadius = 2;
            rightButton.Layer.MasksToBounds = true;
            View.AddSubview(rightButton);

            var text = new UILabel(new RectangleF(0, View.Frame.Height / 4, 320, 100));
            text.Font = UIFont.FromName("Museo500-Regular", 30);
            text.TextAlignment = UITextAlignment.Center;
            text.TextColor = UIColor.White;
            text.Text = "VideoSplash";
            View.AddSubview(text);
        }
    }
}
