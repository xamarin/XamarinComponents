using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

using AnimatedButtons;

namespace LiquidFloatingActionButtonSample
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

            var font = UIFont.SystemFontOfSize(10);
            var cells = new List<LiquidFloatingCell>
            {
                new LiquidFloatingCell(UIImage.FromBundle("ic_cloud"), "cloud").TitleFont(font).TitleColor(UIColor.White),
                new LiquidFloatingCell(UIImage.FromBundle("ic_system"), "system").TitleFont(font).TitleColor(UIColor.White),
                new LiquidFloatingCell(UIImage.FromBundle("ic_place"), "place").TitleFont(font).TitleColor(UIColor.White),
            };

            topLeftButton.Image = UIImage.FromBundle("ic_art");
            topLeftButton.AnimateStyle = AnimateStyle.Down;
            topLeftButton.Cells = cells;
            topLeftButton.CellSelected += delegate
            {
                topLeftButton.Close();
            };

            bottomRightButton.Cells = cells;
            bottomRightButton.CellSelected += delegate
            {
                bottomRightButton.Close();
            };
        }
    }
}
