using System;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using nfloat = global::System.Single;
#endif

using SidePanels;

namespace JASidePanels
{
    [Obsolete("Use SidePanelStyle instead.", true)]
    public enum JASidePanelStyle : uint
    {
        SingleActive = SidePanelStyle.SingleActive,
        MultipleActive = SidePanelStyle.MultipleActive
    }

    [Obsolete("Use SidePanelState instead.", true)]
    public enum JASidePanelState : uint
    {
        CenterVisible = SidePanelState.CenterVisible,
        LeftVisible = SidePanelState.LeftVisible,
        RightVisible = SidePanelState.RightVisible
    }

    [Obsolete("Use SidePanelController instead.")]
    public class JASidePanelController : SidePanelController
    {
        public new JASidePanelStyle Style 
        {
            get { return (JASidePanelStyle)base.Style; } 
            set { base.Style = (SidePanelStyle)value; }
        }

        [Obsolete("Use LeftPanelPercentWidth instead.")]
        public nfloat LeftGapPercentage 
        {
            get { return base.LeftPanelPercentWidth; } 
            set { base.LeftPanelPercentWidth = value; }
        }

        [Obsolete("Use LeftPanelFixedWidth instead.")]
        public nfloat LeftFixedWidth 
        {
            get { return base.LeftPanelFixedWidth; } 
            set { base.LeftPanelFixedWidth = value; }
        }

        [Obsolete("Use LeftPanelVisibleWidth instead.")]
        public nfloat LeftVisibleWidth 
        {
            get { return base.LeftPanelVisibleWidth; } 
        }

        [Obsolete("Use RightPanelPercentWidth instead.")]
        public nfloat RightGapPercentage 
        {
            get { return base.RightPanelPercentWidth; } 
            set { base.RightPanelPercentWidth = value; }
        }

        [Obsolete("Use RightPanelFixedWidth instead.")]
        public nfloat RightFixedWidth 
        {
            get { return base.RightPanelFixedWidth; } 
            set { base.RightPanelFixedWidth = value; }
        }

        [Obsolete("Use RightPanelVisibleWidth instead.")]
        public nfloat RightVisibleWidth 
        {
            get { return base.RightPanelVisibleWidth; } 
        }

        public override void StylePanel(UIView panel)
        {
            panel.Layer.CornerRadius = 6.0f;

            base.StylePanel(panel);
        }

        [Obsolete("Use ShowLeftPanel instead.")]
        public void ShowLeftPanelAnimated(bool animated)
        {
            base.ShowLeftPanel(animated);
        }

        [Obsolete("Use ShowRightPanel instead")]
        public void ShowRightPanelAnimated(bool animated)
        {
            base.ShowRightPanel(animated);
        }

        [Obsolete("Use ShowCenterPanel instead.")]
        public void ShowCenterPanelAnimated(bool animated)
        {
            base.ShowCenterPanel(animated);
        }

        [Obsolete("Use ToggleLeftPanel() instead.")]
        public void ToggleLeftPanel(NSObject sender)
        {
            base.ToggleLeftPanel();
        }

        [Obsolete("Use ToggleRightPanel() instead.")]
        public void ToggleRightPanel(NSObject sender)
        {
            base.ToggleRightPanel();
        }

        [Obsolete("Use the DefaultImage property instead.")]
        public static new UIImage DefaultImage()
        {
            return SidePanelController.DefaultImage;
        }

        [Obsolete("Use GetLeftButtonForCenterPanel() instead.")]
        public UIBarButtonItem LeftButtonForCenterPanel()
        {
            return base.GetLeftButtonForCenterPanel();
        }
    }

    [Obsolete("Use SidePanelControllerExtensions instead.")]
    public static class JASidePanel
    {
        [Obsolete("Use the extension method SidePanelControllerExtensions.GetSidePanelController(UIViewController) instead.")]
        public static JASidePanelController GetSidePanelController(this UIViewController This)
        {
            var spc = SidePanelControllerExtensions.GetSidePanelController(This);
            return spc as JASidePanelController;
        }
    }
}
