using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using CoreGraphics;

namespace PullToBounceSample
{
    partial class TableViewCell : UITableViewCell
    {
        public TableViewCell(IntPtr handle) 
            : base(handle)
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            line1.Layer.CornerRadius = line1.Frame.Height / 2f;
            line2.Layer.CornerRadius = line2.Frame.Height / 2f;
            line3.Layer.CornerRadius = line3.Frame.Height / 2f;

            picture.Layer.CornerRadius = 10f;

            line2.Frame = new CGRect(line2.Frame.Location, new CGSize(Frame.Width / 4f, line2.Frame.Height));
        }
    }
}
