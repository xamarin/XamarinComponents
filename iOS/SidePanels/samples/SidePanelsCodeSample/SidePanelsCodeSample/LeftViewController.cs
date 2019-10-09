using System;
using CoreGraphics;
using UIKit;

using SidePanels;

namespace SidePanelsCodeSample
{
    partial class LeftViewController : DebugViewController
    {
        private UIImageView background;
        private UILabel label;
        private UIButton hide;
        private UIButton show;
        private UIButton removeRightPanel;
        private UIButton addRightPanel;
        private UIButton changeCenterPanel;

        public LeftViewController()
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
            label.Text = "Left Panel";
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


            removeRightPanel = new UIButton(UIButtonType.RoundedRect);
            removeRightPanel.Frame = new CGRect(20.0f, 190.0f, 200.0f, 40.0f);
            removeRightPanel.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            removeRightPanel.SetTitle("Remove Right Panel", UIControlState.Normal);
            removeRightPanel.TouchUpInside += (sender, e) =>
            {
                this.GetSidePanelController().RightPanel = null;
                removeRightPanel.Hidden = true;
                addRightPanel.Hidden = false;
            };
            View.AddSubview(removeRightPanel);

            addRightPanel = new UIButton(UIButtonType.RoundedRect);
            addRightPanel.Frame = removeRightPanel.Frame;
            addRightPanel.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            addRightPanel.SetTitle("Add Right Panel", UIControlState.Normal);
            addRightPanel.TouchUpInside += (sender, e) =>
            {
                this.GetSidePanelController().RightPanel = new RightViewController();
                removeRightPanel.Hidden = false;
                addRightPanel.Hidden = true;
            };
            addRightPanel.Hidden = true;
            View.AddSubview(addRightPanel);


            changeCenterPanel = new UIButton(UIButtonType.RoundedRect);
            changeCenterPanel.Frame = new CGRect(20.0f, 240.0f, 200.0f, 40.0f);
            changeCenterPanel.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin;
            changeCenterPanel.SetTitle("Change Center Panel", UIControlState.Normal);
            changeCenterPanel.TouchUpInside += (sender, e) =>
            {
                this.GetSidePanelController().CenterPanel = new UINavigationController(new CenterViewController());
            };
            View.AddSubview(changeCenterPanel);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            label.Center = new CGPoint((int)(this.GetSidePanelController().LeftPanelVisibleWidth / 2.0f), 65.0f);
        }
    }
}
