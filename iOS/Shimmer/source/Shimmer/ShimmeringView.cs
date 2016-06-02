using System;
using System.ComponentModel;
using Shimmer;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;
using ObjCClass = ObjCRuntime.Class;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ObjCClass = MonoTouch.ObjCRuntime.Class;

using CGRect = System.Drawing.RectangleF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace Shimmer
{
    [DesignTimeVisible(true), Category("Controls")]
    [Register("ShimmeringView")]
    public class ShimmeringView : UIView, IShimmering
    {
        private UIView _contentView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShimmeringView"/> class.
        /// </summary>
        public ShimmeringView()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShimmeringView"/> class with the specified frame.
        /// </summary>
        /// <param name="frame">Frame used by the view, expressed in iOS points.</param>
        public ShimmeringView(CGRect frame)
            : base(frame)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShimmeringView"/> class when creating managed 
        /// representations of unmanaged objects. Called by the runtime.
        /// </summary>
        /// <param name="handle">Pointer (handle) to the unmanaged object.</param>
        public ShimmeringView(IntPtr handle)
            : base(handle)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShimmeringView"/> class from the data stored in 
        /// the unarchiver object.
        /// </summary>
        /// <param name="coder">The unarchiver object.</param>
        public ShimmeringView(NSCoder coder)
            : base(coder)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShimmering" /> is shimmering.
        /// Defaults to <c>false</c>.
        /// </summary>
        /// <value><c>true</c> if shimmering; otherwise, <c>false</c>.</value>
        [Browsable(true)]
        [Export("Shimmering")]
        public bool Shimmering
        {
            get { return ShimmeringLayer.Shimmering; }
            set { ShimmeringLayer.Shimmering = value; }
        }

        /// <summary>
        /// Gets or sets the opacity of the content while it is shimmering.
        /// Defaults to 0.5.
        /// </summary>
        /// <value>The opacity of the content while it is shimmering.</value>
        [Browsable(true)]
        [Export("ShimmeringAnimationOpacity")]
        public nfloat ShimmeringAnimationOpacity
        {
            get { return ShimmeringLayer.ShimmeringAnimationOpacity; }
            set { ShimmeringLayer.ShimmeringAnimationOpacity = value; }
        }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer begins.
        /// Defaults to 0.1.
        /// </summary>
        /// <value>The duration of the fade used when shimmer begins.</value>
        [Browsable(true)]
        [Export("ShimmeringBeginFadeDuration")]
        public double ShimmeringBeginFadeDuration
        {
            get { return ShimmeringLayer.ShimmeringBeginFadeDuration; }
            set { ShimmeringLayer.ShimmeringBeginFadeDuration = value; }
        }

        /// <summary>
        /// Gets or sets the absolute CoreAnimation media time when the shimmer will begin.
        /// </summary>
        /// <value>The absolute CoreAnimation media time when the shimmer will begin.</value>
        /// <remarks>Only valid after setting <see cref="Shimmering" /> to <c>true</c>.</remarks>
        [Browsable(true)]
        [Export("ShimmeringBeginTime")]
        public double ShimmeringBeginTime
        {
            get { return ShimmeringLayer.ShimmeringBeginTime; }
            set { ShimmeringLayer.ShimmeringBeginTime = value; }
        }

        /// <summary>
        /// Gets or sets the direction of the shimmering animation.
        /// Defaults to <see cref="ShimmeringDirection.Right" />.
        /// </summary>
        /// <value>The direction of the shimmering animation.</value>
        [Browsable(true)]
        [Export("ShimmeringDirection")]
        public ShimmeringDirection ShimmeringDirection
        {
            get { return ShimmeringLayer.ShimmeringDirection; }
            set { ShimmeringLayer.ShimmeringDirection = value; }
        }

        /// <summary>
        /// Gets or sets the duration of the fade used when shimmer ends.
        /// Defaults to 0.3.
        /// </summary>
        /// <value>The duration of the fade used when shimmer ends.</value>
        [Browsable(true)]
        [Export("ShimmeringEndFadeDuration")]
        public double ShimmeringEndFadeDuration
        {
            get { return ShimmeringLayer.ShimmeringEndFadeDuration; }
            set { ShimmeringLayer.ShimmeringEndFadeDuration = value; }
        }

        /// <summary>
        /// Gets the absolute CoreAnimation media time when the shimmer will fade in.
        /// </summary>
        /// <value>The absolute CoreAnimation media time when the shimmer will fade in.</value>
        /// <remarks>Only valid after setting <see cref="Shimmering" /> to <c>false</c>.</remarks>
        public double ShimmeringFadeTime
        {
            get { return ShimmeringLayer.ShimmeringFadeTime; }
        }

        /// <summary>
        /// Gets or sets the length of the shimmering highlight.
        /// Range of [0,1], defaults to 1.0.
        /// </summary>
        /// <value>The length of the shimmering highlight.</value>
        [Browsable(true)]
        [Export("ShimmeringHighlightLength")]
        public nfloat ShimmeringHighlightLength
        {
            get { return ShimmeringLayer.ShimmeringHighlightLength; }
            set { ShimmeringLayer.ShimmeringHighlightLength = value; }
        }

        /// <summary>
        /// Gets or sets the opacity of the content before it is shimmering.
        /// Defaults to 1.0.
        /// </summary>
        /// <value>The opacity of the content before it is shimmering.</value>
        [Browsable(true)]
        [Export("ShimmeringOpacity")]
        public nfloat ShimmeringOpacity
        {
            get { return ShimmeringLayer.ShimmeringOpacity; }
            set { ShimmeringLayer.ShimmeringOpacity = value; }
        }

        /// <summary>
        /// Gets or sets the time interval between shimmerings in seconds.
        /// Defaults to 0.4.
        /// </summary>
        /// <value>The time interval between shimmerings in seconds.</value>
        [Browsable(true)]
        [Export("ShimmeringPauseDuration")]
        public double ShimmeringPauseDuration
        {
            get { return ShimmeringLayer.ShimmeringPauseDuration; }
            set { ShimmeringLayer.ShimmeringPauseDuration = value; }
        }

        /// <summary>
        /// Gets or sets the speed of shimmering, in points per second.
        /// Defaults to 230.
        /// </summary>
        /// <value>The speed of shimmering, in points per second.</value>
        [Browsable(true)]
        [Export("ShimmeringSpeed")]
        public nfloat ShimmeringSpeed
        {
            get { return ShimmeringLayer.ShimmeringSpeed; }
            set { ShimmeringLayer.ShimmeringSpeed = value; }
        }

        /// <summary>
        /// Tells the view when subviews are added.
        /// </summary>
        /// <param name="uiview">The view that was added as a subview.</param>
        /// <exception cref="System.InvalidOperationException">ShimmeringView can only have a single sub view.</exception>
        public override void SubviewAdded(UIView uiview)
        {
            if (_contentView != null)
            {
                throw new InvalidOperationException("ShimmeringView can only have a single sub view.");
            }

            base.SubviewAdded(uiview);

            _contentView = uiview;
            ShimmeringLayer.ContentLayer = _contentView.Layer;
        }

        /// <summary>
        /// Called prior to the removal of a subview.
        /// </summary>
        /// <param name="uiview">The subview that will be removed.</param>
        public override void WillRemoveSubview(UIView uiview)
        {
            if (_contentView == uiview)
            {
                _contentView = null;
                ShimmeringLayer.ContentLayer = null;
            }

            base.WillRemoveSubview(uiview);
        }

        private ShimmeringLayer ShimmeringLayer
        {
            get { return (ShimmeringLayer)Layer; }
        }

        [Export("layerClass")]
        private static ObjCClass LayerClass()
        {
            return new ObjCClass(typeof(ShimmeringLayer));
        }
    }
}
