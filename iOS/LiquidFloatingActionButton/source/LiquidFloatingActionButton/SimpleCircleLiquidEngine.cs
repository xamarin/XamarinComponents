using System;
using System.Linq;
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
    internal class SimpleCircleLiquidEngine
    {
        private readonly nfloat connectThresh;
        private readonly nfloat angleOpen;
        private CALayer layer;

        public SimpleCircleLiquidEngine()
        {
            layer = new CAShapeLayer();
            connectThresh = 0.3f;
            angleOpen = 1.0f;
            Viscosity = 0.65f;
            Color = UIColor.Red;
        }

        public nfloat AngleThreshold { get; set; }

        public nfloat RadiusThreshold { get; set; }

        public UIColor Color { get; set; }

        public nfloat Viscosity { get; set; }

        public LiquittableCircle[] Push(LiquittableCircle circle, LiquittableCircle other)
        {
            var paths = GenerateConnectedPath(circle, other);
            if (paths != null)
            {
                var layers = paths.Select(ConstructLayer);
                foreach (var l in layers)
                {
                    layer.AddSublayer(l);
                }
                return new[] { circle, other };
            }
            return new LiquittableCircle[0];
        }

        public void Draw(UIView parent)
        {
            parent.Layer.AddSublayer(layer);
        }

        public void Clear()
        {
            layer.RemoveFromSuperLayer();
            if (layer.Sublayers != null)
            {
                foreach (var l in layer.Sublayers)
                {
                    l.RemoveFromSuperLayer();
                }
            }
            layer = new CAShapeLayer();
        }

        private CALayer ConstructLayer(UIBezierPath path)
        {
            var pathBounds = path.CGPath.BoundingBox;

            var shape = new CAShapeLayer();
            shape.FillColor = Color.CGColor;
            shape.Path = path.CGPath;
            shape.Frame = new CGRect(0, 0, pathBounds.Width, pathBounds.Height);

            return shape;
        }

        private Tuple<CGPoint, CGPoint> CircleConnectedPoint(LiquittableCircle circle, LiquittableCircle other, nfloat angle)
        {
            var vec = other.Center.Minus(circle.Center);
            var radian = (nfloat)Math.Atan2(vec.Y, vec.X);
            var p1 = circle.Center.CirclePoint(circle.Radius, radian + angle);
            var p2 = circle.Center.CirclePoint(circle.Radius, radian - angle);
            return new Tuple<CGPoint, CGPoint>(p1, p2);
        }

        private Tuple<CGPoint, CGPoint> CircleConnectedPoint(LiquittableCircle circle, LiquittableCircle other)
        {
            var ratio = CircleRatio(circle, other);
            ratio = (ratio + connectThresh) / (1.0f + connectThresh);
            var angle = CoreGraphicsExtensions.PI2 * angleOpen * ratio;
            return CircleConnectedPoint(circle, other, angle);
        }

        private UIBezierPath[] GenerateConnectedPath(LiquittableCircle circle, LiquittableCircle other)
        {
            if (IsConnected(circle, other))
            {
                var ratio = CircleRatio(circle, other);

                if (ratio >= AngleThreshold && ratio <= 1.0f)
                {
                    var path = NormalPath(circle, other);
                    return path != null ? new[] { path } : null;
                }
                else if (ratio >= 0.0f && ratio < AngleThreshold)
                {
                    return SplitPath(circle, other, ratio);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private UIBezierPath NormalPath(LiquittableCircle circle, LiquittableCircle other)
        {
            var p1p2 = CircleConnectedPoint(circle, other);
            var p3p4 = CircleConnectedPoint(other, circle);
            var p1 = p1p2.Item1;
            var p2 = p1p2.Item2;
            var p3 = p3p4.Item1;
            var p4 = p3p4.Item2;
            var crossed = CoreGraphicsExtensions.Intersection(p1, p3, p2, p4);
            if (crossed != null)
            {
                var path = new UIBezierPath();
                var r = CircleRatio(circle, other);
                path.MoveTo(p1);
                var r1 = p2.Mid(p3);
                var r2 = p1.Mid(p4);
                var rate = (1 - r) / (1 - AngleThreshold) * Viscosity;
                var mul = r1.Mid(crossed.Value).Split(r2, rate);
                var mul2 = r2.Mid(crossed.Value).Split(r1, rate);
                path.AddQuadCurveToPoint(p4, mul);
                path.AddLineTo(p3);
                path.AddQuadCurveToPoint(p2, mul2);
                path.ClosePath();
                return path;
            }
            return null;
        }

        private UIBezierPath[] SplitPath(LiquittableCircle circle, LiquittableCircle other, nfloat ratio)
        {
            var p1p2 = CircleConnectedPoint(circle, other, CoreGraphicsExtensions.DegToRad(60));
            var p3p4 = CircleConnectedPoint(other, circle, CoreGraphicsExtensions.DegToRad(60));
            var p1 = p1p2.Item1;
            var p2 = p1p2.Item2;
            var p3 = p3p4.Item1;
            var p4 = p3p4.Item2;
            var crossed = CoreGraphicsExtensions.Intersection(p1, p3, p2, p4);
            if (crossed != null)
            {
                var d1 = CircleConnectedPoint(circle, other, 0).Item1;
                var d2 = CircleConnectedPoint(other, circle, 0).Item1;
                var r = (ratio - connectThresh) / (AngleThreshold - connectThresh);

                var a1 = d2.Split(crossed.Value, r * r);
                var part = new UIBezierPath();
                part.MoveTo(p1);
                part.AddQuadCurveToPoint(p2, a1);
                part.ClosePath();

                var a2 = d1.Split(crossed.Value, r * r);
                var part2 = new UIBezierPath();
                part2.MoveTo(p3);
                part2.AddQuadCurveToPoint(p4, a2);
                part2.ClosePath();

                return new[] { part, part2 };
            }
            return new UIBezierPath[0];
        }

        private nfloat CircleRatio(LiquittableCircle circle, LiquittableCircle other)
        {
            var distance = other.Center.Minus(circle.Center).Length();
            var ratio = 1.0f - (distance - RadiusThreshold) / (circle.Radius + other.Radius + RadiusThreshold);
            return (nfloat)Math.Min(Math.Max(ratio, 0.0), 1.0);
        }

        private bool IsConnected(LiquittableCircle circle, LiquittableCircle other)
        {
            var distance = circle.Center.Minus(other.Center).Length();
            return distance - circle.Radius - other.Radius < RadiusThreshold;
        }
    }
}
