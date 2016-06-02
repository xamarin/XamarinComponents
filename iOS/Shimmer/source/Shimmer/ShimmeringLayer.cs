using System;
using System.ComponentModel;
using Shimmer;

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

namespace Shimmer
{
    public class ShimmeringLayer : CALayer, IShimmering
    {
        private static NSString ShimmerSlideAnimationKey = (NSString)"slide";
        private static NSString FadeAnimationKey = (NSString)"fade";
        private static NSString EndFadeAnimationKey = (NSString)"fade-end";

        private CALayer _contentLayer;
        private ShimmeringMaskLayer _maskLayer;

        private bool _shimmering;
        private double _shimmeringPauseDuration;
        private nfloat _shimmeringAnimationOpacity;
        private nfloat _shimmeringOpacity;
        private nfloat _shimmeringSpeed;
        private nfloat _shimmeringHighlightLength;
        private ShimmeringDirection _shimmeringDirection;
        private double _shimmeringBeginTime;

        private static CABasicAnimation FadeAnimation(CALayer layer, nfloat opacity, double duration)
        {
            CABasicAnimation animation = CABasicAnimation.FromKeyPath("opacity");
            if (layer.PresentationLayer != null)
            {
                animation.From = NSNumber.FromFloat(layer.Opacity);
            }
            animation.To = NSNumber.FromDouble(opacity);
            animation.FillMode = CAFillMode.Both;
            animation.RemovedOnCompletion = false;
            animation.Duration = duration;
            return animation;
        }

        private static CABasicAnimation ShimmerSlideAnimation(double duration, ShimmeringDirection direction)
        {
            CABasicAnimation animation = CABasicAnimation.FromKeyPath("position");
            animation.To = NSValue.FromCGPoint(CGPoint.Empty);
            animation.Duration = duration;
            animation.RepeatCount = float.MaxValue;
            if (direction == ShimmeringDirection.Left || direction == ShimmeringDirection.Up)
            {
                animation.Speed = -(float)Math.Abs(animation.Speed);
            }

            return animation;
        }

        private static CAAnimation ShimmerSlideRepeat(CAAnimation a, double duration, ShimmeringDirection direction)
        {
            CAAnimation anim = (CAAnimation)a.Copy();
            anim.RepeatCount = float.MaxValue;
            anim.Duration = duration;
            anim.Speed = (direction == ShimmeringDirection.Right || direction == ShimmeringDirection.Down)
                ? Math.Abs(anim.Speed)
                : -Math.Abs(anim.Speed);
            return anim;
        }

        private static CAAnimation ShimmerSlideFinish(CAAnimation a)
        {
            CAAnimation anim = (CAAnimation)a.Copy();
            anim.RepeatCount = 0;
            return anim;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShimmeringLayer"/> class.
        /// </summary>
        public ShimmeringLayer()
        {
            ShimmeringPauseDuration = 0.4;
            ShimmeringSpeed = 230.0f;
            ShimmeringHighlightLength = 1.0f;
            ShimmeringAnimationOpacity = 0.5f;
            ShimmeringOpacity = 1.0f;
            ShimmeringDirection = ShimmeringDirection.Right;
            ShimmeringBeginFadeDuration = 0.1;
            ShimmeringEndFadeDuration = 0.3;
            ShimmeringBeginTime = double.MaxValue;
        }

        /// <summary>
        /// Gets or sets the layer to shimmer.
        /// </summary>
        /// <value>The layer to shimmer.</value>
        public CALayer ContentLayer
        {
            get { return _contentLayer; }
            set
            {
                _maskLayer = null;
                _contentLayer = value;
                Sublayers = value != null ? new[] { value } : null;
                UpdateShimmering();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShimmering" /> is shimmering.
        /// Defaults to <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if shimmering; otherwise, <c>false</c>.</value>
        public bool Shimmering
        {
            get { return _shimmering; }
            set
            {
                if (_shimmering != value)
                {
                    _shimmering = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Gets or sets the time interval between shimmerings in seconds.
        /// Defaults to 0.4.
        /// </summary>
        /// <value>The time interval between shimmerings in seconds.</value>
        public double ShimmeringPauseDuration
        {
            get { return _shimmeringPauseDuration; }
            set
            {
                if (_shimmeringPauseDuration != value)
                {
                    _shimmeringPauseDuration = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Gets or sets the opacity of the content while it is shimmering.
        /// Defaults to 0.5.
        /// </summary>
        /// <value>The opacity of the content while it is shimmering.</value>
        public nfloat ShimmeringAnimationOpacity
        {
            get { return _shimmeringAnimationOpacity; }
            set
            {
                if (_shimmeringAnimationOpacity != value)
                {
                    _shimmeringAnimationOpacity = value;
                    UpdateMaskColors();
                }
            }
        }

        /// <summary>
        /// Gets or sets the opacity of the content before it is shimmering.
        /// Defaults to 1.0.
        /// </summary>
        /// <value>The opacity of the content before it is shimmering.</value>
        public nfloat ShimmeringOpacity
        {
            get { return _shimmeringOpacity; }
            set
            {
                if (_shimmeringOpacity != value)
                {
                    _shimmeringOpacity = value;
                    UpdateMaskColors();
                }
            }
        }

        /// <summary>
        /// Gets or sets the speed of shimmering, in points per second.
        /// Defaults to 230.
        /// </summary>
        /// <value>The speed of shimmering, in points per second.</value>
        public nfloat ShimmeringSpeed
        {
            get { return _shimmeringSpeed; }
            set
            {
                if (_shimmeringSpeed != value)
                {
                    _shimmeringSpeed = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Gets or sets the length of the shimmering highlight.
        /// Range of [0,1], defaults to 1.0.
        /// </summary>
        /// <value>The length of the shimmering highlight.</value>
        public nfloat ShimmeringHighlightLength
        {
            get { return _shimmeringHighlightLength; }
            set
            {
                if (_shimmeringHighlightLength != value)
                {
                    _shimmeringHighlightLength = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Gets or sets the direction of the shimmering animation.
        /// Defaults to <see cref="ShimmeringDirection.Right" />.
        /// </summary>
        /// <value>The direction of the shimmering animation.</value>
        public ShimmeringDirection ShimmeringDirection
        {
            get { return _shimmeringDirection; }
            set
            {
                if (_shimmeringDirection != value)
                {
                    _shimmeringDirection = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer begins.
        /// Defaults to 0.1.
        /// </summary>
        /// <value>The duration of the fade used when shimmer begins.</value>
        public double ShimmeringBeginFadeDuration { get; set; }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer ends.
        /// Defaults to 0.3.
        /// </summary>
        /// <value>The duration of the fade used when shimmer ends.</value>
        public double ShimmeringEndFadeDuration { get; set; }

        /// <summary>
        /// Gets the absolute CoreAnimation media time when the shimmer will fade in.
        /// </summary>
        /// <value>The absolute CoreAnimation media time when the shimmer will fade in.</value>
        /// <remarks>Only valid after setting <see cref="Shimmering" /> to <c>false</c>.</remarks>
        public double ShimmeringFadeTime { get; private set; }

        /// <summary>
        /// Gets or sets the absolute CoreAnimation media time when the shimmer will begin.
        /// </summary>
        /// <value>The absolute CoreAnimation media time when the shimmer will begin.</value>
        /// <remarks>Only valid after setting <see cref="Shimmering" /> to <c>true</c>.</remarks>
        public double ShimmeringBeginTime
        {
            get { return _shimmeringBeginTime; }
            set
            {
                if (_shimmeringBeginTime != value)
                {
                    _shimmeringBeginTime = value;
                    UpdateShimmering();
                }
            }
        }

        /// <summary>
        /// Tells the layer to update its layout.
        /// </summary>
        public override void LayoutSublayers()
        {
            base.LayoutSublayers();

            CGRect r = Bounds;
            if (_contentLayer != null)
            {
                _contentLayer.AnchorPoint = new CGPoint(0.5f, 0.5f);
                _contentLayer.Bounds = r;
                _contentLayer.Position = new CGPoint(r.GetMidX(), r.GetMidY());
            }
            if (_maskLayer != null)
            {
                UpdateMaskLayout();
            }
        }

        public override CGRect Bounds
        {
            get { return base.Bounds; }
            set
            {
                CGRect oldBounds = base.Bounds;
                base.Bounds = value;
                if (oldBounds != value)
                {
                    UpdateShimmering();
                }
            }
        }

        private void ClearMask()
        {
            if (_maskLayer == null)
            {
                return;
            }

            bool disableActions = CATransaction.DisableActions;
            CATransaction.DisableActions = true;
            _maskLayer = null;
            if (_contentLayer != null)
            {
                _contentLayer.Mask = null;
            }
            CATransaction.DisableActions = disableActions;
        }

        private void CreateMaskIfNeeded()
        {
            if (Shimmering && _maskLayer == null)
            {
                _maskLayer = new ShimmeringMaskLayer();
                if (_contentLayer != null)
                {
                    _contentLayer.Mask = _maskLayer;
                }
                UpdateMaskColors();
                UpdateMaskLayout();
            }
        }

        private void UpdateMaskColors()
        {
            if (_maskLayer == null)
            {
                return;
            }

            UIColor maskedColor = UIColor.White.ColorWithAlpha(ShimmeringOpacity);
            UIColor unmaskedColor = UIColor.White.ColorWithAlpha(ShimmeringAnimationOpacity);
            _maskLayer.Colors = new[] { maskedColor.CGColor, unmaskedColor.CGColor, maskedColor.CGColor };
        }

        private void UpdateMaskLayout()
        {
            if (_contentLayer == null)
            {
                return;
            }

            nfloat length = 0.0f;
            if (ShimmeringDirection == ShimmeringDirection.Down || ShimmeringDirection == ShimmeringDirection.Up)
            {
                length = _contentLayer.Bounds.Height;
            }
            else
            {
                length = _contentLayer.Bounds.Width;
            }

            if (0 == length)
            {
                return;
            }

            nfloat extraDistance = length + ShimmeringSpeed * (nfloat)ShimmeringPauseDuration;
            nfloat fullShimmerLength = length * 3.0f + extraDistance;
            nfloat travelDistance = length * 2.0f + extraDistance;
            nfloat highlightOutsideLength = (1.0f - ShimmeringHighlightLength) / 2.0f;
            _maskLayer.Locations = new[] {
                NSNumber.FromDouble(highlightOutsideLength),
                0.5,
                1.0 - highlightOutsideLength
            };
            nfloat startPoint = (length + extraDistance) / fullShimmerLength;
            nfloat endPoint = travelDistance / fullShimmerLength;
            _maskLayer.AnchorPoint = CGPoint.Empty;
            if (ShimmeringDirection == ShimmeringDirection.Down || ShimmeringDirection == ShimmeringDirection.Up)
            {
                _maskLayer.StartPoint = new CGPoint(0.0f, startPoint);
                _maskLayer.EndPoint = new CGPoint(0.0f, endPoint);
                _maskLayer.Position = new CGPoint(0.0f, -travelDistance);
                _maskLayer.Bounds = new CGRect(0.0f, 0.0f, _contentLayer.Bounds.Width, fullShimmerLength);
            }
            else
            {
                _maskLayer.StartPoint = new CGPoint(startPoint, 0.0f);
                _maskLayer.EndPoint = new CGPoint(endPoint, 0.0f);
                _maskLayer.Position = new CGPoint(-travelDistance, 0.0f);
                _maskLayer.Bounds = new CGRect(0.0f, 0.0f, fullShimmerLength, _contentLayer.Bounds.Height);
            }

        }

        private void UpdateShimmering()
        {
            CreateMaskIfNeeded();
            if (!Shimmering && _maskLayer == null)
            {
                return;
            }

            LayoutIfNeeded();
            bool disableActions = CATransaction.DisableActions;
            if (!Shimmering)
            {
                if (disableActions)
                {
                    ClearMask();
                }
                else
                {
                    double slideEndTime = 0;
                    CAAnimation slideAnimation = _maskLayer.AnimationForKey(ShimmerSlideAnimationKey);
                    if (slideAnimation != null)
                    {
                        double now = CAAnimation.CurrentMediaTime();
                        double slideTotalDuration = now - slideAnimation.BeginTime;
                        double slideTimeOffset = slideTotalDuration % slideAnimation.Duration;
                        CAAnimation finishAnimation = ShimmerSlideFinish(slideAnimation);
                        finishAnimation.BeginTime = now - slideTimeOffset;
                        slideEndTime = finishAnimation.BeginTime + slideAnimation.Duration;
                        _maskLayer.AddAnimation(finishAnimation, ShimmerSlideAnimationKey);
                    }

                    CABasicAnimation fadeInAnimation = FadeAnimation(_maskLayer.FadeLayer, 1.0f, ShimmeringEndFadeDuration);
                    fadeInAnimation.AnimationStopped += (sender, e) =>
                    {
                        if (e.Finished && ((NSNumber)fadeInAnimation.ValueForKey(EndFadeAnimationKey)).BoolValue)
                        {
                            ClearMask();
                        }
                    };
                    fadeInAnimation.SetValueForKey(NSNumber.FromBoolean(true), EndFadeAnimationKey);
                    fadeInAnimation.BeginTime = slideEndTime;
                    _maskLayer.FadeLayer.AddAnimation(fadeInAnimation, FadeAnimationKey);
                    ShimmeringFadeTime = slideEndTime;
                }

            }
            else
            {
                CABasicAnimation fadeOutAnimation = null;
                if (ShimmeringBeginFadeDuration > 0.0 && !disableActions)
                {
                    fadeOutAnimation = FadeAnimation(_maskLayer.FadeLayer, 0.0f, ShimmeringBeginFadeDuration);
                    _maskLayer.FadeLayer.AddAnimation(fadeOutAnimation, FadeAnimationKey);
                }
                else
                {
                    bool innerDisableActions = CATransaction.DisableActions;
                    CATransaction.DisableActions = true;
                    _maskLayer.FadeLayer.Opacity = 0.0f;
                    _maskLayer.FadeLayer.RemoveAllAnimations();
                    CATransaction.DisableActions = innerDisableActions;
                }

                CAAnimation slideAnimation = _maskLayer.AnimationForKey(ShimmerSlideAnimationKey);
                nfloat length = 0.0f;
                if (_contentLayer != null)
                {
                    if (ShimmeringDirection == ShimmeringDirection.Down || ShimmeringDirection == ShimmeringDirection.Up)
                    {
                        length = _contentLayer.Bounds.Height;
                    }
                    else
                    {
                        length = _contentLayer.Bounds.Width;
                    }
                }

                double animationDuration = (length / ShimmeringSpeed) + ShimmeringPauseDuration;
                if (slideAnimation != null)
                {
                    _maskLayer.AddAnimation(ShimmerSlideRepeat(slideAnimation, animationDuration, ShimmeringDirection), ShimmerSlideAnimationKey);
                }
                else
                {
                    slideAnimation = ShimmerSlideAnimation(animationDuration, ShimmeringDirection);
                    slideAnimation.FillMode = CAFillMode.Forwards;
                    slideAnimation.RemovedOnCompletion = false;
                    if (ShimmeringBeginTime == double.MaxValue)
                    {
                        ShimmeringBeginTime = CAAnimation.CurrentMediaTime() + fadeOutAnimation.Duration;
                    }

                    slideAnimation.BeginTime = ShimmeringBeginTime;
                    _maskLayer.AddAnimation(slideAnimation, ShimmerSlideAnimationKey);
                }
            }
        }
    }
}
