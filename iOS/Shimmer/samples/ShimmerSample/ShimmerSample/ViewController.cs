using System;
using CoreGraphics;
using Foundation;
using UIKit;

using Shimmer;

namespace ShimmerSample
{
    partial class ViewController : UIViewController
    {
        private bool _panVertical;
        private nfloat _panStartValue;

        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _shimmeringView.ShimmeringBeginFadeDuration = 0.3;
            _shimmeringView.ShimmeringOpacity = 0.3f;

            UITapGestureRecognizer tapRecognizer = new UITapGestureRecognizer(OnTapped);
            View.AddGestureRecognizer(tapRecognizer);
            UIPanGestureRecognizer panRecognizer = new UIPanGestureRecognizer(OnPanned);
            View.AddGestureRecognizer(panRecognizer);
        }

        private void OnTapped()
        {
            _shimmeringView.IsShimmering = !_shimmeringView.IsShimmering;
        }

        private void OnPanned(UIPanGestureRecognizer panRecognizer)
        {
            CGPoint translation = panRecognizer.TranslationInView(View);
            CGPoint velocity = panRecognizer.VelocityInView(View);
            if (panRecognizer.State == UIGestureRecognizerState.Began)
            {
                _panVertical = (Math.Abs(velocity.Y) > Math.Abs(velocity.X));

                if (_panVertical)
                {
                    _panStartValue = _shimmeringView.ShimmeringSpeed;
                }
                else
                {
                    _panStartValue = _shimmeringView.ShimmeringOpacity;
                }

                AnimateValueLabelVisible(true);
            }
            else if (panRecognizer.State == UIGestureRecognizerState.Changed)
            {
                nfloat directional = (_panVertical ? translation.Y : translation.X);
                nfloat possible = (_panVertical ? View.Bounds.Size.Height : View.Bounds.Size.Width);
                nfloat progress = (directional / possible);
                if (_panVertical)
                {
                    _shimmeringView.ShimmeringSpeed = (nfloat)Math.Max(0.0f, Math.Min(1000.0f, _panStartValue + progress * 200.0f));
                    _valueLabel.Text = string.Format("Speed\n{0:0.0}", _shimmeringView.ShimmeringSpeed);
                }
                else
                {
                    _shimmeringView.ShimmeringOpacity = (nfloat)Math.Max(0.0f, Math.Min(1.0f, _panStartValue + progress * 0.5f));
                    _valueLabel.Text = string.Format("Opacity\n{0:0.00}", _shimmeringView.ShimmeringOpacity);
                }

            }
            else if (panRecognizer.State == UIGestureRecognizerState.Ended ||
                     panRecognizer.State == UIGestureRecognizerState.Cancelled)
            {
                AnimateValueLabelVisible(false);
            }
        }

        private void AnimateValueLabelVisible(bool visible)
        {
            UIViewAnimationOptions options = UIViewAnimationOptions.BeginFromCurrentState;
            Action animations = () => _valueLabel.Alpha = (visible ? 1.0f : 0.0f);
            UIView.Animate(0.5, 0.0, options, animations, null);
        }
    }
}
