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
    internal class BounceView : UIView
    {
        private BallView ballView;
        private WaveView waveView;
        private nfloat ballViewHeight;

        public BounceView()
        {
            ballView = new BallView();
            waveView = new WaveView();

            ballView.Hidden = true;

            AddSubview(ballView);
            AddSubview(waveView);

            waveView.didEndPull = () =>
            {
                Helpers.CreateScheduledTimer(0.2, () =>
                {
                    ballView.Hidden = false;
                    ballView.StartAnimation();
                });
            };

            BallSize = 28f;
            BallMovementTimingFunction = CAMediaTimingFunction.FromName(CAMediaTimingFunction.EaseOut);
            BallMoveUpDuration = 0.2;
            BallMoveUpDistance = 32f * 1.5f;
            BallViewHeight = 100f;
            WaveBounceDuration = 0.8;
            BallColor = UIColor.White;
            WaveColor = UIColor.White;
        }

        public nfloat BallSize
        {
            get { return ballView.CircleSize; }
            set { ballView.CircleSize = value; }
        }

        public CAMediaTimingFunction BallMovementTimingFunction
        {
            get { return ballView.TimingFunction; }
            set { ballView.TimingFunction = value; }
        }

        public double BallMoveUpDuration
        {
            get { return ballView.MoveUpDuration; }
            set { ballView.MoveUpDuration = value; }
        }

        public nfloat BallMoveUpDistance
        {
            get { return ballView.MoveUpDistance; }
            set { ballView.MoveUpDistance = value; }
        }

        public nfloat BallViewHeight
        {
            get { return ballViewHeight; }
            set
            {
                ballViewHeight = value;
                SetNeedsLayout();
            }
        }

        public double WaveBounceDuration
        {
            get { return waveView.BounceDuration; }
            set { waveView.BounceDuration = value; }
        }

        public UIColor BallColor
        {
            get { return ballView.Color; }
            set { ballView.Color = value; }
        }

        public UIColor WaveColor
        {
            get { return waveView.Color; }
            set { waveView.Color = value; }
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            ballView.Frame = new CGRect(0, -(ballViewHeight + 1), Frame.Width, ballViewHeight);
            waveView.Frame = new CGRect(CGPoint.Empty, Frame.Size);
        }

        public void EndingAnimation(Action complition = null)
        {
            ballView.EndAnimation(() =>
            {
                ballView.Hidden = true;
                if (complition != null)
                    complition();
            });
        }

        public void Wave(nfloat y)
        {
            waveView.Wave(y);
        }

        public void DidRelease(nfloat y)
        {
            waveView.DidRelease(0, y);
        }
    }
}
