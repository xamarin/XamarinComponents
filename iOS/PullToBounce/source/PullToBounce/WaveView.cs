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
    internal class WaveView : UIView
    {
        private CAShapeLayer waveLayer;
        private UIColor color;

        public Action didEndPull;

        public WaveView()
        {
            waveLayer = new CAShapeLayer();
            waveLayer.Clone(Layer);
            waveLayer.LineWidth = 0;
            waveLayer.Path = WavePath(0f, 0f);
            Layer.AddSublayer(waveLayer);

            BounceDuration = 0.4;
            Color = UIColor.White;
        }

        public double BounceDuration { get; set; }

        public UIColor Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;

                    var cgColor = color.CGColor;
                    waveLayer.StrokeColor = cgColor;
                    waveLayer.FillColor = cgColor;
                }
            }
        }

        public void Wave(nfloat y)
        {
            waveLayer.Path = WavePath(0, y);
        }

        public void DidRelease(nfloat amountX, nfloat amountY)
        {
            BounceAnimation(amountX, amountY);
            if (didEndPull != null)
                didEndPull();
        }

        public void BounceAnimation(nfloat positionX, nfloat positionY)
        {
            waveLayer.Path = WavePath(0, 0);
            var bounce = CAKeyFrameAnimation.GetFromKeyPath("path");
            bounce.TimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseIn);
            var values = new[] {
                WavePath(positionX,  positionY),
                WavePath(-(positionX * 0.7f),  -(positionY * 0.7f)),
                WavePath(positionX * 0.4f,  positionY * 0.4f),
                WavePath(-(positionX * 0.3f),  -(positionY * 0.3f)),
                WavePath(positionX * 0.15f,  positionY * 0.15f),
                WavePath(0f,  0f)
            };
            bounce.SetValues(values);
            bounce.Duration = BounceDuration;
            bounce.RemovedOnCompletion = true;
            bounce.FillMode = CAFillMode.Forwards;
            bounce.AnimationStopped += delegate
            {
                waveLayer.Path = WavePath(0f, 0f);
            };
            waveLayer.AddAnimation(bounce, "return");
        }

        public CGPath WavePath(nfloat amountX, nfloat amountY)
        {
            var w = Frame.Width;
            var h = Frame.Height;
            var centerY = (nfloat)0f;
            var bottomY = h;

            var topLeftPoint = new CGPoint(0, centerY);
            var topMidPoint = new CGPoint(w / 2 + amountX, centerY + amountY);
            var topRightPoint = new CGPoint(w, centerY);
            var bottomLeftPoint = new CGPoint(0, bottomY);
            var bottomRightPoint = new CGPoint(w, bottomY);

            var bezierPath = new UIBezierPath();
            bezierPath.MoveTo(bottomLeftPoint);
            bezierPath.AddLineTo(topLeftPoint);
            bezierPath.AddQuadCurveToPoint(topRightPoint, topMidPoint);
            bezierPath.AddLineTo(bottomRightPoint);
            return bezierPath.CGPath;
        }
    }
}
