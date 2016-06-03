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
    internal class CircleLayer : CAShapeLayer
    {
        private SpinnerLayer spinner;
        private Action didEndAnimation;

        public CircleLayer()
        {
            spinner = new SpinnerLayer();

            AddSublayer(spinner);

            Color = UIColor.White;
            LineWidth = 0;
            StrokeEnd = 1;
        }

        public nfloat CircleSize
        {
            get { return spinner.CircleSize; }
            set
            {
                spinner.CircleSize = value;
                SetNeedsLayout();
            }
        }

        public CAMediaTimingFunction TimingFunction { get; set; }

        public double MoveUpDuration { get; set; }

        public nfloat MoveUpDistance { get; set; }

        public UIColor Color
        {
            get { return spinner.Color; }
            set
            {
                spinner.Color = value;

                var color = value.ColorWithAlpha(1f).CGColor;
                FillColor = color;
                StrokeColor = color;
            }
        }

        public override void LayoutSublayers()
        {
            base.LayoutSublayers();

            var radius = CircleSize / 2f;
            var center = new CGPoint(Frame.Width / 2f, Frame.Height / 2f);
            var startAngle = 0f - Helpers.fPI2;
            var endAngle = Helpers.fPI * 2f - Helpers.fPI2;
            var clockwise = true;
            base.Path = UIBezierPath.FromArc(center, radius, startAngle, endAngle, clockwise).CGPath; // TODO center

            spinner.Frame = new CGRect(CGPoint.Empty, Frame.Size);
        }

        public void StartAnimation()
        {
            MoveUp(MoveUpDistance);
            Helpers.CreateScheduledTimer(MoveUpDuration, () =>
            {
                spinner.Animation();
            });
        }

        public void EndAnimation(Action complition = null)
        {
            spinner.StopAnimation();
            MoveDown(MoveUpDistance);
            didEndAnimation = complition;
        }

        public void MoveUp(nfloat distance)
        {
            var move = CABasicAnimation.FromKeyPath("position");

            move.From = NSValue.FromCGPoint(Position);
            move.To = NSValue.FromCGPoint(new CGPoint(Position.X, Position.Y - distance));

            move.Duration = MoveUpDuration;
            move.TimingFunction = TimingFunction;

            move.FillMode = CAFillMode.Forwards;
            move.RemovedOnCompletion = false;

            AddAnimation(move, move.KeyPath);
        }

        public void MoveDown(nfloat distance)
        {
            var move = CABasicAnimation.FromKeyPath("position");

            move.From = NSValue.FromCGPoint(new CGPoint(Position.X, Position.Y - distance));
            move.To = NSValue.FromCGPoint(Position);

            move.Duration = MoveUpDuration;
            move.TimingFunction = TimingFunction;

            move.FillMode = CAFillMode.Forwards;
            move.RemovedOnCompletion = false;
            move.AnimationStopped += delegate
            {
                if (didEndAnimation != null)
                {
                    didEndAnimation();
                }
            };
            AddAnimation(move, move.KeyPath);
        }
    }
}
