using System;
using UIKit;

namespace TransitionSubmitSample
{
    partial class SecondViewController : UIViewController
    {
        public SecondViewController()
        {
        }

        public SecondViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var tapRecognizer = new UITapGestureRecognizer(() =>
            {
                DismissViewController(true, null);
            });
            homeBackground.UserInteractionEnabled = true;
            homeBackground.AddGestureRecognizer(tapRecognizer);
        }
    }
}
