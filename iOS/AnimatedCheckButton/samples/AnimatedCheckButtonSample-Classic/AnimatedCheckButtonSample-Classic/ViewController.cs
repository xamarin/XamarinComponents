using System;
using System.Drawing;
using MonoTouch.UIKit;

using AnimatedButtons;

namespace AnimatedCheckButtonSampleClassic
{
    partial class ViewController : UIViewController, IUIViewControllerTransitioningDelegate
    {
        private AnimatedCheckButton button;

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

            View.BackgroundColor = UIColor.FromRGBA(0.176471f, 0.701961f, 0.203922f, 1.0f);

            button = new AnimatedCheckButton();
            button.Frame = new RectangleF(0, 0, 140, 140);
            button.Center = View.Center;
            View.AddSubview(button);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
