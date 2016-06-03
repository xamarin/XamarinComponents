using System;
using MonoTouch.UIKit;

using SidePanels;

namespace SidePanelsClassicSample
{
    partial class CenterViewController : DebugViewController
    {
        public CenterViewController()
        {
        }
        
        public CenterViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Random rnd = new Random();

            View.BackgroundColor = UIColor.FromRGB(
                (float)rnd.NextDouble(),
                (float)rnd.NextDouble(),
                (float)rnd.NextDouble());
        }
    }
}
