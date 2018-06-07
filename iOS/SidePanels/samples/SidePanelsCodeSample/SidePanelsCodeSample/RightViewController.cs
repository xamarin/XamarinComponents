using CoreGraphics;
using UIKit;

using SidePanels;

namespace SidePanelsCodeSample
{
    partial class RightViewController : DebugViewController
    {
        private UIImageView background;
        private UILabel label;
        private UIButton hide;
        private UIButton show;

        public RightViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            background = new UIImageView(View.Bounds);
            background.Image = UIImage.FromBundle("background.png");
            background.ContentMode = UIViewContentMode.ScaleToFill;
            background.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
            View.AddSubview(background);

            label = new UILabel();
            label.Frame = new CGRect(20.0f, 90.0f, 200.0f, 40.0f);
            label.Font = UIFont.BoldSystemFontOfSize(24);
            label.BackgroundColor = UIColor.Clear;
            label.Text = "Right Panel";
            label.SizeToFit();
            label.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleBottomMargin;
            View.AddSubview(label);


            hide = new UIButton(UIButtonType.RoundedRect);
            hide.Frame = new CGRect(20.0f, 140.0f, 200.0f, 40.0f);
            hide.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            hide.SetTitle("Hide Center", UIControlState.Normal);
            hide.TouchUpInside += (sender, e) =>
            {
                this.GetSidePanelController().CenterPanelHidden = true;
                hide.Hidden = true;
                show.Hidden = false;
            };
            View.AddSubview(hide);

            show = new UIButton(UIButtonType.RoundedRect);
            show.Frame = hide.Frame;
            show.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            show.SetTitle("Show Center", UIControlState.Normal);
            show.TouchUpInside += (sender, e) =>
            {
                this.GetSidePanelController().CenterPanelHidden = false;
                hide.Hidden = false;
                show.Hidden = true;
            };
            show.Hidden = true;
            View.AddSubview(show);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var rightPanelVisibleWidth = this.GetSidePanelController().RightPanelVisibleWidth;
            label.Center = new CGPoint(((View.Bounds.Size.Width - rightPanelVisibleWidth) + rightPanelVisibleWidth / 2.0f), 65.0f);
        }
    }
}
