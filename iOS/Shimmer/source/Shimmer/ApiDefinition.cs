using System;
using CoreAnimation;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Shimmer
{
    // @protocol FBShimmering <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject), Name = "FBShimmering")]
    interface Shimmering
    {
        // @required @property (getter = isShimmering, assign, readwrite, nonatomic) BOOL shimmering;
        [Abstract]
        [Export("shimmering")]
        bool IsShimmering { [Bind("isShimmering")] get; set; }

        // @required @property (assign, readwrite, nonatomic) CFTimeInterval shimmeringPauseDuration;
        [Abstract]
        [Export("shimmeringPauseDuration")]
        double ShimmeringPauseDuration { get; set; }

        // @required @property (assign, readwrite, nonatomic) CGFloat shimmeringAnimationOpacity;
        [Abstract]
        [Export("shimmeringAnimationOpacity")]
        nfloat ShimmeringAnimationOpacity { get; set; }

        // @required @property (assign, readwrite, nonatomic) CGFloat shimmeringOpacity;
        [Abstract]
        [Export("shimmeringOpacity")]
        nfloat ShimmeringOpacity { get; set; }

        // @required @property (assign, readwrite, nonatomic) CGFloat shimmeringSpeed;
        [Abstract]
        [Export("shimmeringSpeed")]
        nfloat ShimmeringSpeed { get; set; }

        // @required @property (getter = shimmeringHighlightLength, assign, readwrite, nonatomic, setter = setShimmeringHighlightLength:) CGFloat shimmeringHighlightWidth;
        [Abstract]
        [Export("shimmeringHighlightWidth")]
        nfloat ShimmeringHighlightWidth { [Bind("shimmeringHighlightLength")] get; [Bind("setShimmeringHighlightLength:")] set; }

        // @required @property (assign, readwrite, nonatomic) FBShimmerDirection shimmeringDirection;
        [Abstract]
        [Export("shimmeringDirection", ArgumentSemantic.Assign)]
        ShimmerDirection ShimmeringDirection { get; set; }

        // @required @property (assign, readwrite, nonatomic) CFTimeInterval shimmeringBeginFadeDuration;
        [Abstract]
        [Export("shimmeringBeginFadeDuration")]
        double ShimmeringBeginFadeDuration { get; set; }

        // @required @property (assign, readwrite, nonatomic) CFTimeInterval shimmeringEndFadeDuration;
        [Abstract]
        [Export("shimmeringEndFadeDuration")]
        double ShimmeringEndFadeDuration { get; set; }

        // @required @property (readonly, assign, nonatomic) CFTimeInterval shimmeringFadeTime;
        [Abstract]
        [Export("shimmeringFadeTime")]
        double ShimmeringFadeTime { get; }
    }

    // @interface FBShimmeringLayer : CALayer <FBShimmering>
    [BaseType(typeof(CALayer), Name = "FBShimmeringLayer")]
    interface ShimmeringLayer : IShimmering
    {
        // @property (nonatomic, strong) CALayer * contentLayer;
        [Export("contentLayer", ArgumentSemantic.Strong)]
        CALayer ContentLayer { get; set; }
    }

    // @interface FBShimmeringView : UIView <FBShimmering>
    [BaseType(typeof(UIView), Name = "FBShimmeringView")]
    interface ShimmeringView : IShimmering
    {
        // @property (nonatomic, strong) UIView * contentView;
        [Export("contentView", ArgumentSemantic.Strong)]
        UIView ContentView { get; set; }
    }

    internal interface IShimmering : Shimmering
    {
    }
}
