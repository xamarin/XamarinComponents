using System;

using SidePanels;

namespace SidePanelsSample
{
    partial class MainViewController : SidePanelController
    {
        public MainViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            CenterPanel = Storyboard.InstantiateViewController("centerViewController");
            LeftPanel = Storyboard.InstantiateViewController("leftViewController");
            RightPanel = Storyboard.InstantiateViewController("rightViewController");

            const float cornerRadius = 8.0f;
            StateChanged += (sender, e) =>
            {
                CenterPanel.View.Layer.CornerRadius = e.NewState == SidePanelState.CenterVisible ? 0.0f : cornerRadius;
            };
            Panning += (sender, e) =>
            {
                CenterPanel.View.Layer.CornerRadius = cornerRadius;
            };
            PanCompleted += (sender, e) =>
            {
                CenterPanel.View.Layer.CornerRadius = State == SidePanelState.CenterVisible ? 0.0f : cornerRadius;
            };
        }
    }
}
