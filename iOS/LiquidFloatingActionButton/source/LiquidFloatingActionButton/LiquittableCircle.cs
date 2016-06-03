using System;
using System.ComponentModel;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    public class LiquittableCircle : UIView
    {
        private CGPoint[] points = new CGPoint[0];
        private CAShapeLayer circleLayer = new CAShapeLayer();

        private UIColor color = UIColor.Red;

        public LiquittableCircle()
        {
            DrawCircle();
            Layer.AddSublayer(circleLayer);
            Opaque = false;
        }

        public nfloat Radius
        {
            get { return Frame.Width / 2f; }
        }

        public UIColor Color
        {
            get { return color; }
            set
            {
                color = value;
                DrawCircle();
            }
        }
        
        private void DrawCircle()
        {
            var bezierPath = UIBezierPath.FromOval(new CGRect(CGPoint.Empty, Frame.Size));
            circleLayer.LineWidth = 3.0f;
            circleLayer.FillColor = Color.CGColor;
            circleLayer.Path = bezierPath.CGPath;
        }
        
        internal CGPoint CirclePoint(nfloat rad)
        {
            return Center.CirclePoint(Radius, rad);
        }

        public override void Draw(CGRect rect)
        {
            DrawCircle();
        }
    }
}
