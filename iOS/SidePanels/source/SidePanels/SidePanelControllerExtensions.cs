#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace SidePanels
{
    public static class SidePanelControllerExtensions
    {
        public static SidePanelController GetSidePanelController(this UIViewController self)
        {
            UIViewController parent = self.ParentViewController;
            while (parent != null)
            {
                SidePanelController sidePanel = parent as SidePanelController;
                if (sidePanel != null)
                {
                    return sidePanel;
                }
                else if (parent.ParentViewController != null && parent.ParentViewController != parent)
                {
                    parent = parent.ParentViewController;
                }
                else
                {
                    parent = null;
                }
            }
            return null;
        }
    }
}
