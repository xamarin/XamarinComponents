using System;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using UIKit;

using AnimatedButtons;

namespace TransitionSubmitSample
{
    partial class ViewController : UIViewController
    {
        private TransitionSubmitButton btn;

        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            storyboardButton.TouchUpInside += OnLogin;

            btn = new TransitionSubmitButton();
            btn.Frame = new CGRect(32.0f, View.Frame.Height - 60.0f - 44.0f, View.Frame.Width - 64.0f, 44.0f);
            btn.SetTitle("Sign in", UIControlState.Normal);
            if (btn.TitleLabel != null)
            {
                btn.TitleLabel.Font = UIFont.FromName("HelveticaNeue-Light", 14.0f);
            }
            btn.TouchUpInside += OnLogin;
            View.AddSubview(btn);
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            var button = sender as TransitionSubmitButton;

            // only allow a single click
            button.UserInteractionEnabled = false;

            // do some work, while the button animates
            await button.AnimateAsync(async () => 
            {
                // the work ...
                await Task.Delay(1000);
            });

            // move on
            PerformSegue("loginSegue", this);

            // we are finished, so restore user control
            button.UserInteractionEnabled = true;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            // use the custom animation
            segue.DestinationViewController.TransitioningDelegate = new FadeInTransitioningDelegate(0.5, 0.8f);
        }
    }
}
