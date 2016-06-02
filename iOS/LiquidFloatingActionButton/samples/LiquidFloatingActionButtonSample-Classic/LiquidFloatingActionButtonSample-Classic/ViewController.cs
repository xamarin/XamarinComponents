using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.UIKit;

using AnimatedButtons;

namespace LiquidFloatingActionButtonSampleClassic
{
    partial class ViewController : UIViewController
    {
        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view, typically from a nib.

            View.BackgroundColor = UIColor.FromRGB(36, 42, 61);

            // the cells
            var cells = new List<LiquidFloatingCell>
            {
                new LiquidFloatingCell(UIImage.FromBundle("ic_cloud")),
                new LiquidFloatingCell(UIImage.FromBundle("ic_system")),
                new LiquidFloatingCell(UIImage.FromBundle("ic_place")),
                new LiquidFloatingCell(UIImage.FromBundle("ic_art")),
                new LiquidFloatingCell(UIImage.FromBundle("ic_brush"))
            };

            // the bottom button
            var bottomRightFrame = new RectangleF(View.Frame.Width - 56f - 16f, View.Frame.Height - 56f - 16f, 56f, 56f);
            var bottomRightButton = new LiquidFloatingActionButton(bottomRightFrame);
            bottomRightButton.AnimateStyle = AnimateStyle.Up;
            bottomRightButton.Cells = cells;
            bottomRightButton.CellSelected += delegate
            {
                bottomRightButton.Close();
            };
            bottomRightButton.Color = UIColor.Orange;
            View.AddSubview(bottomRightButton);

            // the top button
            var topLeftFrame = new RectangleF(16f, 16f, 56f, 56f);
            if (UIDevice.CurrentDevice.CheckSystemVersion(7, 0))
                topLeftFrame.Y += 20;
            var topLeftButton = new LiquidFloatingActionButton(topLeftFrame);
            topLeftButton.AnimateStyle = AnimateStyle.Down;
            topLeftButton.Cells = cells;
            topLeftButton.CellSelected += delegate
            {
                topLeftButton.Close();
            };
            View.AddSubview(topLeftButton);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
