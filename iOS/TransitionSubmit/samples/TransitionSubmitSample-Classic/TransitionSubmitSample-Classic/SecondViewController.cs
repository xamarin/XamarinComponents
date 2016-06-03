using MonoTouch.UIKit;

namespace TransitionSubmitSampleClassic
{
    partial class SecondViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var homeBackground = new UIImageView(UIImage.FromBundle("Home"));
            homeBackground.Frame = View.Frame;
            View.AddSubview(homeBackground);

            var tapRecognizer = new UITapGestureRecognizer(() =>
            {
                DismissViewController(true, null);
            });
            homeBackground.UserInteractionEnabled = true;
            homeBackground.AddGestureRecognizer(tapRecognizer);
        }
    }
}
