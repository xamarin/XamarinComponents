using System;
using System.Drawing;
using MonoTouch.UIKit;

using AnimatedButtons;

namespace TransitionSubmitSampleClassic
{
    partial class ViewController : UIViewController, IUIViewControllerTransitioningDelegate
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
            btn.Frame = new RectangleF(32.0f, View.Frame.Height - 60.0f - 44.0f, View.Frame.Width - 64.0f, 44.0f);
            btn.SetTitle("Sign in", UIControlState.Normal);
            if (btn.TitleLabel != null)
            {
                btn.TitleLabel.Font = UIFont.FromName("HelveticaNeue-Light", 14.0f);
            }
            btn.TouchUpInside += OnLogin;
            View.AddSubview(btn);
        }

        private void OnLogin(object sender, EventArgs e)
        {
            var button = sender as TransitionSubmitButton;
            button.Animate(1, () =>
            {
                var second = new SecondViewController();
                second.TransitioningDelegate = new FadeInTransitioningDelegate(0.5, 0.8f);
                PresentViewController(second, true, null);
            });
        }
    }
}
