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
    public interface IShimmering
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShimmering"/> is shimmering.
        /// Defaults to <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if shimmering; otherwise, <c>false</c>.</value>
        bool Shimmering { get; set; }

        /// <summary>
        /// Gets or sets the time interval between shimmerings in seconds.
        /// Defaults to 0.4.
        /// </summary>
        /// <value>The time interval between shimmerings in seconds.</value>
        double ShimmeringPauseDuration { get; set; }

        /// <summary>
        /// Gets or sets the opacity of the content while it is shimmering.
        /// Defaults to 0.5.
        /// </summary>
        /// <value>The opacity of the content while it is shimmering.</value>
        nfloat ShimmeringAnimationOpacity { get; set; }

        /// <summary>
        /// Gets or sets the opacity of the content before it is shimmering.
        /// Defaults to 1.0.
        /// </summary>
        /// <value>The opacity of the content before it is shimmering.</value>
        nfloat ShimmeringOpacity { get; set; }

        /// <summary>
        /// Gets or sets the speed of shimmering, in points per second.
        /// Defaults to 230.
        /// </summary>
        /// <value>The speed of shimmering, in points per second.</value>
        nfloat ShimmeringSpeed { get; set; }

        /// <summary>
        /// Gets or sets the length of the shimmering highlight.
        /// Range of [0,1], defaults to 1.0.
        /// </summary>
        /// <value>The length of the shimmering highlight.</value>
        nfloat ShimmeringHighlightLength { get; set; }

        /// <summary>
        /// Gets or sets the direction of the shimmering animation.
        /// Defaults to <see cref="ShimmeringDirection.Right"/>.
        /// </summary>
        /// <value>The direction of the shimmering animation.</value>
        ShimmeringDirection ShimmeringDirection { get; set; }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer begins.
        /// Defaults to 0.1.
        /// </summary>
        /// <value>The duration of the fade used when shimmer begins.</value>
        double ShimmeringBeginFadeDuration { get; set; }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer ends.
        /// Defaults to 0.3.
        /// </summary>
        /// <value>The duration of the fade used when shimmer ends.</value>
        double ShimmeringEndFadeDuration { get; set; }

        /// <summary>
        /// Gets the absolute CoreAnimation media time when the shimmer will fade in.
        /// </summary>
        /// <remarks>
        /// Only valid after setting <see cref="Shimmering"/> to <c>false</c>.
        /// </remarks>
        /// <value>The absolute CoreAnimation media time when the shimmer will fade in.</value>
        double ShimmeringFadeTime { get; }

        /// <summary>
        /// Gets or sets the absolute CoreAnimation media time when the shimmer will begin.
        /// </summary>
        /// <remarks>
        /// Only valid after setting <see cref="Shimmering"/> to <c>true</c>.
        /// </remarks>
        /// <value>The absolute CoreAnimation media time when the shimmer will begin.</value>
        double ShimmeringBeginTime { get; set; }
    }
}
