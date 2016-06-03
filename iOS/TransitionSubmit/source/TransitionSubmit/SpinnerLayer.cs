using System;

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
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    internal class SpinnerLayer : CAShapeLayer
    {
        private static nfloat fPI = (nfloat)Math.PI;

        public SpinnerLayer(CGRect frame)
        {

            Frame = new CGRect(0.0f, 0.0f, frame.Height, frame.Height);
            var radius = (frame.Height / 2.0f) * 0.5f;
            var center = new CGPoint(frame.Height / 2.0f, Bounds.GetMidY());
            var startAngle = -(fPI / 2);
            var endAngle = fPI * 2.0f - (fPI / 2.0f);
            var clockwise = true;
            Path = UIBezierPath.FromArc(center, radius, startAngle, endAngle, clockwise).CGPath;

            FillColor = null;
            StrokeColor = UIColor.White.CGColor;
            LineWidth = 1.0f;

            StrokeEnd = 0.4f;
            Hidden = true;
        }

        public void Animation()
        {
            Hidden = false;

            var rotate = CABasicAnimation.FromKeyPath("transform.rotation.z");
            rotate.From = (NSNumber)0.0f;
            rotate.To = new NSNumber(fPI * 2.0f);
            rotate.Duration = 0.4f;
            rotate.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            rotate.RepeatCount = float.MaxValue;
            rotate.FillMode = CAFillMode.Forwards;
            rotate.RemovedOnCompletion = false;

            AddAnimation(rotate, rotate.KeyPath);
        }

        public void StopAnimation()
        {
            Hidden = true;

            RemoveAllAnimations();
        }
    }
}
