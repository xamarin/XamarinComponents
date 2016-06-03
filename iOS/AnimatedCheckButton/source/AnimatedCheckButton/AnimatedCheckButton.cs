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
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("AnimatedCheckButton")]
    public class AnimatedCheckButton : UIControl
    {
        private UIColor color = UIColor.White;
        private UIColor skeletonColor = UIColor.White.ColorWithAlpha(0.25f);
        private nfloat pathSize = 70.0f;
        private nfloat circleStrokeStart = 0.0f;
        private nfloat circleStrokeEnd = 0.738f;
        private nfloat checkStrokeStart = 0.8f;
        private nfloat checkStrokeEnd = 0.97f;

        private nfloat lineWidth = 4;
        private nfloat lineWidthBold = 5;

        private CAShapeLayer shape = new CAShapeLayer();
        private CAShapeLayer circle = new CAShapeLayer();
        private CAShapeLayer check = new CAShapeLayer();

        private CAMediaTimingFunction timingFunc = CAMediaTimingFunction.FromControlPoints(0.44f, -0.04f, 0.64f, 1.4f);
        private CAMediaTimingFunction backFunc = CAMediaTimingFunction.FromControlPoints(0.45f, -0.36f, 0.44f, 0.92f);

        private bool isChecked;

        public AnimatedCheckButton()
        {
            Setup();
        }

        public AnimatedCheckButton(IntPtr handle)
            : base(handle)
        {
        }

        public AnimatedCheckButton(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        public AnimatedCheckButton(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            Setup();
        }

        private void Setup()
        {
            // set up events
            TouchUpInside += OnTapped;

            // create the check path
            var path = new CGPath();
            path.MoveToPoint(5.07473346f, 20.2956615f);
            path.AddCurveToPoint(3.1031115f, 24.4497281f, 2f, 29.0960413f, 2f, 34f);
            path.AddCurveToPoint(2f, 51.673112f, 16.326888f, 66f, 34f, 66f);
            path.AddCurveToPoint(51.673112f, 66f, 66f, 51.673112f, 66f, 34f);
            path.AddCurveToPoint(66f, 16.326888f, 51.673112f, 2f, 34f, 2f);
            path.AddCurveToPoint(21.3077047f, 2f, 10.3412842f, 9.38934836f, 5.16807419f, 20.1007094f);
            path.AddLineToPoint(29.9939289f, 43.1625671f);
            path.AddLineToPoint(56.7161293f, 17.3530369f);

            shape.Path = path;
            circle.Path = path;
            check.Path = path;

            shape.StrokeColor = color.CGColor;
            circle.StrokeColor = skeletonColor.CGColor;
            check.StrokeColor = skeletonColor.CGColor;

            shape.StrokeStart = circleStrokeStart;
            shape.StrokeEnd = circleStrokeEnd;
            circle.StrokeStart = circleStrokeStart;
            circle.StrokeEnd = circleStrokeEnd;
            check.StrokeStart = checkStrokeStart;
            check.StrokeEnd = checkStrokeEnd;

            shape.LineWidth = lineWidth;
            circle.LineWidth = lineWidth;
            check.LineWidth = lineWidthBold;

            foreach (var layer in new[] { circle, check, shape })
            {
                layer.FillColor = null;
                layer.MiterLimit = 4;
                layer.LineCap = CAShapeLayer.CapRound;
                layer.MasksToBounds = true;

                var strokingPath = layer.Path.CopyByStrokingPath(4f, CGLineCap.Round, CGLineJoin.Miter, 4f);
                layer.Bounds = strokingPath.BoundingBox;
                layer.Actions = NSDictionary.FromObjectsAndKeys(
                    new[] { NSNull.Null, NSNull.Null, NSNull.Null },
                    new[] { "strokeStart", "strokeEnd", "transform" });
                Layer.AddSublayer(layer);
            }
        }

        [Browsable(true)]
        [Export("Color")]
        public UIColor Color
        {
            get { return color; }
            set
            {
                color = value;

                shape.StrokeColor = color.CGColor;
            }
        }

        [Browsable(true)]
        [Export("SkeletonColor")]
        public UIColor SkeletonColor
        {
            get { return skeletonColor; }
            set
            {
                skeletonColor = value;

                circle.StrokeColor = skeletonColor.CGColor;
                check.StrokeColor = skeletonColor.CGColor;
            }
        }

        [Browsable(true)]
        [Export("Checked")]
        public bool Checked
        {
            get { return isChecked; }
            set
            {
                SetChecked(value, false);
            }
        }

        public void SetChecked(bool value, bool animated)
        {
            if (isChecked == value)
                return;

            isChecked = value;

            if (animated)
            {
                var strokeStart = CABasicAnimation.FromKeyPath("strokeStart");
                var strokeEnd = CABasicAnimation.FromKeyPath("strokeEnd");
                var lineWidthAnim = CABasicAnimation.FromKeyPath("lineWidth");
                if (isChecked)
                {
                    strokeStart.To = NSNumber.FromDouble(checkStrokeStart);
                    strokeStart.Duration = 0.3;
                    strokeStart.TimingFunction = timingFunc;

                    strokeEnd.To = NSNumber.FromDouble(checkStrokeEnd);
                    strokeEnd.Duration = 0.3;
                    strokeEnd.TimingFunction = timingFunc;

                    lineWidthAnim.To = NSNumber.FromDouble(lineWidthBold);
                    lineWidthAnim.BeginTime = 0.2;
                    lineWidthAnim.Duration = 0.1;
                    lineWidthAnim.TimingFunction = timingFunc;
                }
                else
                {
                    strokeStart.To = NSNumber.FromDouble(circleStrokeStart);
                    strokeStart.Duration = 0.2;
                    strokeStart.TimingFunction = backFunc;
                    strokeStart.FillMode = CAFillMode.Backwards;

                    strokeEnd.To = NSNumber.FromDouble(circleStrokeEnd);
                    strokeEnd.Duration = 0.3;
                    strokeEnd.TimingFunction = backFunc;

                    lineWidthAnim.To = NSNumber.FromDouble(lineWidth);
                    lineWidthAnim.Duration = 0.1;
                    lineWidthAnim.TimingFunction = backFunc;
                }
                ApplyAnimationCopy(shape, strokeStart);
                ApplyAnimationCopy(shape, strokeEnd);
                ApplyAnimationCopy(shape, lineWidthAnim);
            }
            else
            {
                if (isChecked)
                {
                    shape.StrokeStart = checkStrokeStart;
                    shape.StrokeEnd = checkStrokeEnd;
                    shape.LineWidth = lineWidthBold;
                }
                else
                {
                    shape.StrokeStart = circleStrokeStart;
                    shape.StrokeEnd = circleStrokeEnd;
                    shape.LineWidth = lineWidth;
                }
            }

            OnCheckedChanged();
        }

        public event EventHandler CheckedChanged;

        protected virtual void OnCheckedChanged()
        {
            var handler = CheckedChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            var size = (nfloat)Math.Min(Frame.Width, Frame.Height);
            var scale = size / pathSize;
            foreach (var layer in new[] { circle, check, shape })
            {
                if (layer != null)
                {
                    layer.Position = new CGPoint(Frame.Width / 2f, Frame.Height / 2f);
                    layer.Transform = CATransform3D.MakeScale(scale, scale, 1);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            TouchUpInside -= OnTapped;

            base.Dispose(disposing);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            SetChecked(!Checked, true);
        }

        private static void ApplyAnimationCopy(CALayer self, CABasicAnimation animation)
        {
            if (self.PresentationLayer != null)
            {
                var copy = animation.Copy() as CABasicAnimation;
                if (copy.From == null)
                {
                    copy.From = self.PresentationLayer.ValueForKeyPath((NSString)copy.KeyPath);
                }
                self.AddAnimation(copy, copy.KeyPath);
                self.SetValueForKeyPath(copy.To, (NSString)copy.KeyPath);
            }
        }
    }
}
