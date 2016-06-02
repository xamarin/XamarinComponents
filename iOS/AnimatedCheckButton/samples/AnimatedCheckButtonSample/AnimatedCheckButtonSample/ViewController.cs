using System;
using UIKit;

namespace AnimatedCheckButtonSample
{
    partial class ViewController : UIViewController
    {
        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            button.CheckedChanged += (sender, e) => {
                Console.WriteLine("Is Checked: " + button.Checked);
            };
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }

        partial void OnCheck(UIButton sender)
        {
            button.Checked = !button.Checked;
        }
    }
}
