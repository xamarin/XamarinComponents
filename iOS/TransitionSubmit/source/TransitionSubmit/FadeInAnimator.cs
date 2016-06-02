using System;

#if __UNIFIED__
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    public class FadeInAnimator : NSObject, IUIViewControllerAnimatedTransitioning
    {
        private double transitionDuration;
        private nfloat startingAlpha;

        public FadeInAnimator()
            : this(0.5, 0.0f)
        {
        }

        public FadeInAnimator(double transitionDuration, nfloat startingAlpha)
        {
            this.transitionDuration = transitionDuration;
            this.startingAlpha = startingAlpha;
        }

        public double Duration 
        {
            get { return transitionDuration; }
            set { transitionDuration = value; }
        }

        public nfloat StartingAlpha 
        {
            get { return startingAlpha; }
            set { startingAlpha = value; }
        }

        public double TransitionDuration(IUIViewControllerContextTransitioning transitionContext)
        {
            return transitionDuration;
        }

        public void AnimateTransition(IUIViewControllerContextTransitioning transitionContext)
        {
            var containerView = transitionContext.ContainerView;

            var toView = transitionContext.GetViewFor(UITransitionContext.ToViewKey);
            var fromView = transitionContext.GetViewFor(UITransitionContext.FromViewKey);

            toView.Alpha = startingAlpha;
            fromView.Alpha = 0.8f;

            containerView.AddSubview(toView);

            UIView.Animate(TransitionDuration(transitionContext), () =>
            {
                toView.Alpha = 1.0f;
                fromView.Alpha = 0.0f;
            }, () =>
            {
                fromView.Alpha = 1.0f;
                transitionContext.CompleteTransition(true);
            });
        }
    }
}
