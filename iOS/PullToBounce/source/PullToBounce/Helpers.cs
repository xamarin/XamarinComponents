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
    internal static class Helpers
    {
        public static nfloat fPI = (nfloat)Math.PI;
        public static nfloat fPI2 = (nfloat)Math.PI / 2f;

        public static void CreateScheduledTimer(double delay, Action action)
        {
#if __UNIFIED__
            NSTimer.CreateScheduledTimer(delay, timer =>
            {
                action();
            });
#else
            NSTimer.CreateScheduledTimer(delay, () =>
            {
                action();
            });
#endif
        }
    }
}
