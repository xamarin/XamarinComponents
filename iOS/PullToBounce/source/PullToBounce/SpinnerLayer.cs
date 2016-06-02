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

namespace PullToBounce
{
    internal class SpinnerLayer : CAShapeLayer
    {
        private nfloat circleSize;

        public SpinnerLayer()
        {
            FillColor = null;
            Color = UIColor.White;
            LineWidth = 2;
            LineCap = CAShapeLayer.CapRound;

            StrokeStart = 0;
            StrokeEnd = 0;
            Hidden = true;
        }

        public nfloat CircleSize
        {
            get { return circleSize; }
            set
            {
                circleSize = value;
                SetNeedsLayout();
            }
        }

        public UIColor Color
        {
            get { return new UIColor(StrokeColor); }
            set { StrokeColor = value.ColorWithAlpha(1).CGColor; }
        }

        public override void LayoutSublayers()
        {
            base.LayoutSublayers();

            var radius = (CircleSize / 2f) * 1.2f;
            var center = new CGPoint(Frame.Width / 2f, Frame.Y + Frame.Height / 2f);
            var startAngle = 0 - Helpers.fPI2;
            var endAngle = (Helpers.fPI * 2 - Helpers.fPI2) + Helpers.fPI / 8;
            var clockwise = true;
            Path = UIBezierPath.FromArc(center, radius, startAngle, endAngle, clockwise).CGPath;
        }

        public void Animation()
        {
            Hidden = false;
            var rotate = CABasicAnimation.FromKeyPath("transform.rotation.z");
            rotate.From = NSNumber.FromDouble(0.0);
            rotate.To = NSNumber.FromDouble(Helpers.fPI * 2);
            rotate.Duration = 1.0;
            rotate.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.Linear);
            rotate.RepeatCount = float.MaxValue;
            rotate.FillMode = CAFillMode.Forwards;
            rotate.RemovedOnCompletion = false;
            AddAnimation(rotate, rotate.KeyPath);

            StrokeEndAnimation();
        }

        public void StrokeEndAnimation()
        {
            var endPoint = CABasicAnimation.FromKeyPath("strokeEnd");
            endPoint.From = NSNumber.FromDouble(0.0);
            endPoint.To = NSNumber.FromDouble(1.0);
            endPoint.Duration = 0.8;
            endPoint.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);
            endPoint.RepeatCount = 1;
            endPoint.FillMode = CAFillMode.Forwards;
            endPoint.RemovedOnCompletion = false;
            endPoint.AnimationStopped += delegate
            {
                if (!Hidden)
                {
                    StrokeStartAnimation();
                }
            };
            AddAnimation(endPoint, endPoint.KeyPath);
        }

        public void StrokeStartAnimation()
        {
            var startPoint = CABasicAnimation.FromKeyPath("strokeStart");
            startPoint.From = NSNumber.FromDouble(0.0);
            startPoint.To = NSNumber.FromDouble(1.0);
            startPoint.Duration = 0.8;
            startPoint.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);
            startPoint.RepeatCount = 1;
            startPoint.AnimationStopped += delegate
            {
                if (!Hidden)
                {
                    StrokeEndAnimation();
                }
            };
            AddAnimation(startPoint, startPoint.KeyPath);
        }

        public void StopAnimation()
        {
            Hidden = true;
            RemoveAllAnimations();
        }
    }
}
