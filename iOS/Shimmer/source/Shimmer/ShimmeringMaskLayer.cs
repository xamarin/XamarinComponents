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
    internal class ShimmeringMaskLayer : CAGradientLayer
    {
        public CALayer FadeLayer { get; private set; }

        public ShimmeringMaskLayer()
        {
            FadeLayer = new CALayer();
            FadeLayer.BackgroundColor = UIColor.White.CGColor;
            AddSublayer(FadeLayer);

            RemoveAllAnimations();
        }

        public override void LayoutSublayers()
        {
            base.LayoutSublayers();

            CGRect r = Bounds;
            FadeLayer.Bounds = r;
            FadeLayer.Position = new CGPoint(r.GetMidX(), r.GetMidY());
        }
    }
}
