using System;

using SidePanels;

namespace SidePanelsSample
{
    partial class LeftViewController : DebugViewController
    {
        public LeftViewController()
        {
        }
        
        public LeftViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            toggleCenter.TouchUpInside += (sender, e) =>
            {
                var controller = this.GetSidePanelController();
                controller.CenterPanelHidden = !controller.CenterPanelHidden;
            };

            toggleRight.TouchUpInside += (sender, e) =>
            {
                var controller = this.GetSidePanelController();
                if (controller.RightPanel == null)
                {
                    var newRight = Storyboard.InstantiateViewController("rightViewController");
                    controller.RightPanel = newRight;
                }
                else
                {
                    controller.RightPanel = null;
                }
            };

            switchCenter.TouchUpInside += (sender, e) =>
            {
                var controller = this.GetSidePanelController();
                var newCenter = Storyboard.InstantiateViewController("centerViewController");
                controller.CenterPanel = newCenter;
            };
        }
    }
}
