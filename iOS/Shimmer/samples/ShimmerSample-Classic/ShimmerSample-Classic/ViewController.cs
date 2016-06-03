using System;
using System.CodeDom.Compiler;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Shimmer;

namespace ShimmerSampleClassic
{
    partial class ViewController : UIViewController
    {
        private UIImageView _wallpaperView;
        private ShimmeringView _shimmeringView;
        private UIView _contentView;
        private UILabel _logoLabel;
        private UILabel _valueLabel;
        private float _panStartValue;
        private bool _panVertical;

        public ViewController()
        {
        }

        public ViewController(IntPtr handle)
            : base(handle)
        {
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            RectangleF shimmeringFrame = View.Bounds;
            shimmeringFrame.Y = shimmeringFrame.Height * 0.68f;
            shimmeringFrame.Height = shimmeringFrame.Height * 0.32f;
            _shimmeringView.Frame = shimmeringFrame;
        }

        public override bool PrefersStatusBarHidden()
        {
            return true;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Black;
            _wallpaperView = new UIImageView(View.Bounds);
            _wallpaperView.Image = UIImage.FromBundle("Wallpaper");
            _wallpaperView.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.AddSubview(_wallpaperView);

            RectangleF valueFrame = View.Bounds;
            valueFrame.Height = valueFrame.Height * 0.25f;
            _valueLabel = new UILabel(valueFrame);
            _valueLabel.Font = UIFont.FromName("HelveticaNeue-UltraLight", 32.0f);
            _valueLabel.TextColor = UIColor.White;
            _valueLabel.TextAlignment = UITextAlignment.Center;
            _valueLabel.Lines = 0;
            _valueLabel.Alpha = 0.0f;
            _valueLabel.BackgroundColor = UIColor.Clear;
            View.AddSubview(_valueLabel);

            _shimmeringView = new ShimmeringView();
            _shimmeringView.Shimmering = true;
            _shimmeringView.ShimmeringBeginFadeDuration = 0.3;
            _shimmeringView.ShimmeringOpacity = 0.3f;
            View.AddSubview(_shimmeringView);

            _logoLabel = new UILabel(_shimmeringView.Bounds);
            _logoLabel.Text = "Shimmer";
            _logoLabel.Font = UIFont.FromName("HelveticaNeue-UltraLight", 60.0f);
            _logoLabel.TextColor = UIColor.White;
            _logoLabel.TextAlignment = UITextAlignment.Center;
            _logoLabel.BackgroundColor = UIColor.Clear;
            _shimmeringView.AddSubview(_logoLabel);

            UITapGestureRecognizer tapRecognizer = new UITapGestureRecognizer(OnTapped);
            View.AddGestureRecognizer(tapRecognizer);
            UIPanGestureRecognizer panRecognizer = new UIPanGestureRecognizer(OnPanned);
            View.AddGestureRecognizer(panRecognizer);
        }

        private void OnTapped()
        {
            _shimmeringView.Shimmering = !_shimmeringView.Shimmering;
        }

        private void OnPanned(UIPanGestureRecognizer panRecognizer)
        {
            PointF translation = panRecognizer.TranslationInView(View);
            PointF velocity = panRecognizer.VelocityInView(View);
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
                float directional = (_panVertical ? translation.Y : translation.X);
                float possible = (_panVertical ? View.Bounds.Size.Height : View.Bounds.Size.Width);
                float progress = (directional / possible);
                if (_panVertical)
                {
                    _shimmeringView.ShimmeringSpeed = (float)Math.Max(0.0f, Math.Min(1000.0f, _panStartValue + progress * 200.0f));
                    _valueLabel.Text = string.Format("Speed\n{0:0.0}", _shimmeringView.ShimmeringSpeed);
                }
                else
                {
                    _shimmeringView.ShimmeringOpacity = (float)Math.Max(0.0f, Math.Min(1.0f, _panStartValue + progress * 0.5f));
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
            UIView.Animate(
                0.5,
                0.0,
                UIViewAnimationOptions.BeginFromCurrentState,
                () => _valueLabel.Alpha = (visible ? 1.0f : 0.0f),
                null);
        }
    }
}
