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

namespace PullToBounce
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("PullToBounceWrapper")]
    public class PullToBounceWrapper : UIView
    {
        private static NSString observerContext = new NSString();
        private static NSString contentOffsetKeyPath = (NSString)"contentOffset";

        private BounceView bounceView;
        private nfloat pullDistance;
        private UIScrollView scrollView;

        public PullToBounceWrapper()
        {
            Setup();
        }

        public PullToBounceWrapper(IntPtr handle)
            : base(handle)
        {
            Setup();
        }

        public PullToBounceWrapper(NSCoder coder)
            : base(coder)
        {
            Setup();
        }

        public PullToBounceWrapper(CGRect frame)
            : base(frame)
        {
            Setup();
        }

        private void Setup()
        {
            bounceView = new BounceView();
            AddSubview(bounceView);

            WaveBounceDuration = 0.8;
            BallSize = 36f;
            PullDistance = 96f;
            BendDistance = 40f;
            BallMovementTimingFunction = CAMediaTimingFunction.FromControlPoints(0.49f, 0.13f, 0.29f, 1.61f);
            BallMoveUpDuration = 0.25;
        }

        public override void SubviewAdded(UIView uiview)
        {
            base.SubviewAdded(uiview);

            var scroll = uiview as UIScrollView;
            if (scroll != null)
            {
                if (scrollView != null)
                {
                    throw new InvalidOperationException("Cannot add multiple UIScrollView sub views to PullToBounceWrapper.");
                }
                scrollView = scroll;

                scrollView.AddObserver(this, contentOffsetKeyPath, NSKeyValueObservingOptions.Initial, observerContext.Handle);
            }
        }

        public override void WillRemoveSubview(UIView uiview)
        {
            if (uiview == scrollView)
            {
                scrollView.RemoveObserver(this, contentOffsetKeyPath, observerContext.Handle);
                scrollView = null;
            }

            base.WillRemoveSubview(uiview);
        }

        [Browsable(true)]
        [Export("BallColor")]
        public UIColor BallColor
        {
            get { return bounceView.BallColor; }
            set { bounceView.BallColor = value; }
        }

        [Browsable(true)]
        [Export("WaveColor")]
        public UIColor WaveColor
        {
            get { return bounceView.WaveColor; }
            set { bounceView.WaveColor = value; }
        }

        [Browsable(true)]
        [Export("WaveBounceDuration")]
        public double WaveBounceDuration
        {
            get { return bounceView.WaveBounceDuration; }
            set { bounceView.WaveBounceDuration = value; }
        }

        [Browsable(true)]
        [Export("BallMoveUpDuration")]
        public double BallMoveUpDuration
        {
            get { return bounceView.BallMoveUpDuration; }
            set { bounceView.BallMoveUpDuration = value; }
        }

        public CAMediaTimingFunction BallMovementTimingFunction
        {
            get { return bounceView.BallMovementTimingFunction; }
            set { bounceView.BallMovementTimingFunction = value; }
        }

        [Browsable(true)]
        [Export("BallSize")]
        public nfloat BallSize
        {
            get { return bounceView.BallSize; }
            set
            {
                bounceView.BallSize = value;
                bounceView.BallMoveUpDistance = BallMoveUpDistance;
            }
        }

        [Browsable(true)]
        [Export("PullDistance")]
        public nfloat PullDistance
        {
            get { return pullDistance; }
            set
            {
                pullDistance = value;
                bounceView.BallMoveUpDistance = BallMoveUpDistance;
            }
        }

        [Browsable(true)]
        [Export("BendDistance")]
        public nfloat BendDistance { get; set; }

        private nfloat StopPosition
        {
            get { return PullDistance + BendDistance; }
        }

        private nfloat BallMoveUpDistance
        {
            get { return PullDistance / 2f + BallSize / 2f; }
        }

        public UIScrollView ScrollView
        {
            get { return scrollView; }
        }

        public event EventHandler RefreshStarted;

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            SendSubviewToBack(bounceView);
            bounceView.Frame = new CGRect(CGPoint.Empty, Frame.Size);
        }

        protected override void Dispose(bool disposing)
        {
            if (scrollView != null)
            {
                scrollView.RemoveObserver(this, contentOffsetKeyPath, observerContext.Handle);
            }

            base.Dispose(disposing);
        }

        protected virtual void OnRefreshStarted()
        {
            var handler = RefreshStarted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void ScrollViewDidScroll()
        {
            if (ScrollView != null)
            {
                if (ScrollView.ContentOffset.Y < 0)
                {
                    var y = -ScrollView.ContentOffset.Y;
                    if (y < PullDistance)
                    {
                        bounceView.Frame = new CGRect(new CGPoint(bounceView.Frame.X, y), bounceView.Frame.Size);
                        bounceView.Wave(0);
                        ScrollView.Alpha = (PullDistance - y) / PullDistance;
                    }
                    else if (y < StopPosition)
                    {
                        bounceView.Wave(y - PullDistance);
                        ScrollView.Alpha = 0;
                    }
                    else if (y > StopPosition)
                    {
                        ScrollView.ScrollEnabled = false;
                        ScrollView.SetContentOffset(new CGPoint(ScrollView.ContentOffset.X, -StopPosition), false);
                        bounceView.Frame = new CGRect(new CGPoint(bounceView.Frame.X, PullDistance), bounceView.Frame.Size);
                        bounceView.Wave(StopPosition - PullDistance);
                        bounceView.DidRelease(StopPosition - PullDistance);
                        OnRefreshStarted();
                        ScrollView.Alpha = 0;
                    }
                }
                else
                {
                    bounceView.Frame = new CGRect(new CGPoint(bounceView.Frame.X, 0), bounceView.Frame.Size);
                    ScrollView.Alpha = 1;
                }
            }
        }

        public void StopLoadingAnimation()
        {
            bounceView.EndingAnimation(() =>
            {
                if (ScrollView != null)
                {
                    ScrollView.SetContentOffset(new CGPoint(0, 0), true);
                    ScrollView.ScrollEnabled = true;
                }
            });
        }

        public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            if (observerContext.Handle == context && keyPath == contentOffsetKeyPath && ofObject == ScrollView)
            {
                ScrollViewDidScroll();
            }
            else
            {
                base.ObserveValue(keyPath, ofObject, change, context);
            }
        }
    }
}
