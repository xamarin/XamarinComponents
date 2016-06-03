using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

using PullToBounce;

namespace PullToBounceSample
{
    partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            title.Layer.CornerRadius = title.Frame.Height / 2f;
            tableViewWrapper.RefreshStarted += async delegate
            {
                await Task.Delay(2000);
                tableViewWrapper.StopLoadingAnimation();
            };
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
