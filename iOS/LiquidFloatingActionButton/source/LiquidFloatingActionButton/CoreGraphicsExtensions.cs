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
    internal static class CoreGraphicsExtensions
    {
        public static nfloat PI = (nfloat)Math.PI;
        public static nfloat PI2 = (nfloat)Math.PI / 2f;

        public static nfloat RadToDeg(nfloat rad)
        {
            return rad * 180.0f / PI;
        }

        public static nfloat DegToRad(nfloat deg)
        {
            return deg * PI / 180.0f;
        }

        public static nfloat[] LinSpace(nfloat from, nfloat to, int n)
        {
            var values = new nfloat[n];
            for (int i = 0; i < n; i++)
            {
                values[i] = (to - from) * i / (n - 1) + from;
            }
            return values;
        }

        public static void AppendShadow(this CALayer layer)
        {
            layer.ShadowColor = UIColor.Black.CGColor;
            layer.ShadowRadius = 2.0f;
            layer.ShadowOpacity = 0.1f;
            layer.ShadowOffset = new CGSize(4, 4);
            layer.MasksToBounds = false;
        }

        public static void EraseShadow(this CALayer layer)
        {
            layer.ShadowRadius = 0.0f;
            layer.ShadowColor = UIColor.Clear.CGColor;
        }

        public static CGPoint CirclePoint(this CGPoint circleCenter, nfloat radius, nfloat rad)
        {
            var x = circleCenter.X + radius * (nfloat)Math.Cos(rad);
            var y = circleCenter.Y + radius * (nfloat)Math.Sin(rad);
            return new CGPoint(x, y);
        }

        public static CGPoint Plus(this CGPoint self, CGPoint point)
        {
            return new CGPoint(self.X + point.X, self.Y + point.Y);
        }

        public static CGPoint Minus(this CGPoint self, CGPoint point)
        {
            return new CGPoint(self.X - point.X, self.Y - point.Y);
        }

        public static CGPoint MinusX(this CGPoint self, nfloat dx)
        {
            return new CGPoint(self.X - dx, self.Y);
        }

        public static CGPoint MinusY(this CGPoint self, nfloat dy)
        {
            return new CGPoint(self.X, self.Y - dy);
        }

        public static CGPoint Mul(this CGPoint self, nfloat rhs)
        {
            return new CGPoint(self.X * rhs, self.Y * rhs);
        }

        public static CGPoint Div(this CGPoint self, nfloat rhs)
        {
            return new CGPoint(self.X / rhs, self.Y / rhs);
        }

        public static nfloat Length(this CGPoint self)
        {
            return (nfloat)Math.Sqrt(self.X * self.X + self.Y * self.Y);
        }

        public static CGPoint Normalized(this CGPoint self)
        {
            return self.Div(self.Length());
        }

        public static nfloat Dot(this CGPoint self, CGPoint point)
        {
            return self.X * point.X + self.Y * point.Y;
        }

        public static nfloat Cross(this CGPoint self, CGPoint point)
        {
            return self.X * point.Y - self.Y * point.X;
        }

        public static CGPoint Split(this CGPoint self, CGPoint point, nfloat ratio)
        {
            return self.Mul(ratio).Plus(point.Mul(1.0f - ratio));
        }

        public static CGPoint Mid(this CGPoint self, CGPoint point)
        {
            return self.Split(point, 0.5f);
        }

        public static CGPoint? Intersection(CGPoint from, CGPoint to, CGPoint from2, CGPoint to2)
        {
            var ac = new CGPoint(to.X - from.X, to.Y - from.Y);
            var bd = new CGPoint(to2.X - from2.X, to2.Y - from2.Y);
            var ab = new CGPoint(from2.X - from.X, from2.Y - from.Y);
            var bc = new CGPoint(to.X - from2.X, to.Y - from2.Y);

            var area = bd.Cross(ab);
            var area2 = bd.Cross(bc);

            if (Math.Abs(area + area2) >= 0.1)
            {
                var ratio = area / (area + area2);
                return new CGPoint(from.X + ratio * ac.X, from.Y + ratio * ac.Y);
            }

            return null;
        }

        public static UIColor White(this UIColor self, nfloat scale)
        {
            nfloat red, green, blue, alpha;
            self.GetRGBA(out red, out green, out blue, out alpha);
            return new UIColor(
                red: red + (1.0f - red) * scale,
                green: green + (1.0f - green) * scale,
                blue: blue + (1.0f - blue) * scale,
                alpha: 1.0f
            );
        }
    }
}
