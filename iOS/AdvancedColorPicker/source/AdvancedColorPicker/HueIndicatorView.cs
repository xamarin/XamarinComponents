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
	internal class HueIndicatorView : UIView
	{
		public HueIndicatorView()
		{
			BackgroundColor = UIColor.Clear;
		}

		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			var context = UIGraphics.GetCurrentContext();

			var indicatorLength = rect.Size.Height / 3f;
			var halfLength = (indicatorLength / 2);

			context.SetFillColor(UIColor.Black.CGColor);
			context.SetStrokeColor(UIColor.White.CGColor);
			context.SetLineWidth(0.5f);
			context.SetShadow(new CGSize(0, 0), 4);

			var pos = rect.Width / 2f;

			context.MoveTo(pos - halfLength, -1);
			context.AddLineToPoint(pos + halfLength, -1);
			context.AddLineToPoint(pos, indicatorLength);
			context.AddLineToPoint(pos - halfLength, -1);

			context.MoveTo(pos - halfLength, rect.Size.Height + 1);
			context.AddLineToPoint(pos + halfLength, rect.Size.Height + 1);
			context.AddLineToPoint(pos, rect.Size.Height - indicatorLength);
			context.AddLineToPoint(pos - halfLength, rect.Size.Height + 1);

			context.ClosePath();
			context.DrawPath(CGPathDrawingMode.FillStroke);
		}
	}
}
