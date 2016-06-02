using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using SidePanels;

namespace SidePanelsClassicSample
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
                    var newRight = (UIViewController)Storyboard.InstantiateViewController("rightViewController");
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
                var newCenter = (UIViewController)Storyboard.InstantiateViewController("centerViewController");
                controller.CenterPanel = newCenter;
            };
        }
    }
}
