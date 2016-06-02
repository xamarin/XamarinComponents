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
    internal class BallView : UIView
    {
        private CircleLayer circleLayer;
        private UIView circleMoveView;

        public BallView()
        {
            circleMoveView = new UIView();
            AddSubview(circleMoveView);

            circleLayer = new CircleLayer();
            circleMoveView.Layer.AddSublayer(circleLayer);

            CircleSize = 40f;
            MoveUpDistance = 0f;
            Color = UIColor.White;
        }

        public nfloat CircleSize
        {
            get { return circleLayer.CircleSize; }
            set
            {
                circleLayer.CircleSize = value;
                SetNeedsLayout();
            }
        }

        public CAMediaTimingFunction TimingFunction
        {
            get { return circleLayer.TimingFunction; }
            set { circleLayer.TimingFunction = value; }
        }

        public double MoveUpDuration
        {
            get { return circleLayer.MoveUpDuration; }
            set { circleLayer.MoveUpDuration = value; }
        }

        public nfloat MoveUpDistance
        {
            get { return circleLayer.MoveUpDistance; }
            set
            {
                circleLayer.MoveUpDistance = value;
                SetNeedsLayout();
            }
        }

        public UIColor Color
        {
            get { return circleLayer.Color; }
            set { circleLayer.Color = value; }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            circleMoveView.Frame = new CGRect(0, 0, MoveUpDistance, MoveUpDistance);
            circleMoveView.Center = new CGPoint(Frame.Width / 2f, Frame.Height + CircleSize / 2f);
            circleLayer.Frame = new CGRect(CGPoint.Empty, circleMoveView.Frame.Size);
        }

        public void StartAnimation()
        {
            circleLayer.StartAnimation();
        }

        public void EndAnimation(Action complition = null)
        {
            circleLayer.EndAnimation(complition);
        }
    }
}
