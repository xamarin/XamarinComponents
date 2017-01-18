using System;

#if __UNIFIED__
using CoreGraphics;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
	internal class SaturationBrightnessIndicatorView : UIView
	{
		public SaturationBrightnessIndicatorView()
		{
			BackgroundColor = UIColor.Clear;
		}

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			nfloat margins = 4;
			var drawRect = new CGRect(rect.X + margins, rect.Y + margins, rect.Width - margins * 2, rect.Height - margins * 2);

			var context = UIGraphics.GetCurrentContext();
			context.AddEllipseInRect(drawRect);
			context.AddEllipseInRect(drawRect.Inset(4, 4));
			context.SetFillColor(UIColor.Black.CGColor);
			context.SetStrokeColor(UIColor.White.CGColor);
			context.SetLineWidth(0.5f);
			context.ClosePath();
			context.SetShadow(new CGSize(1, 2), 4);
			context.DrawPath(CGPathDrawingMode.EOFillStroke);
		}
	}
}
