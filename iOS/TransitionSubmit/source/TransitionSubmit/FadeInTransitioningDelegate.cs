using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;

using nfloat = System.Single;
#endif

namespace AnimatedButtons
{
    public class FadeInTransitioningDelegate : UIViewControllerTransitioningDelegate
    {
        private readonly FadeInAnimator animator;

        public FadeInTransitioningDelegate()
        {
            animator = new FadeInAnimator();
        }

        public FadeInTransitioningDelegate(double transitionDuration, nfloat startingAlpha)
        {
            animator = new FadeInAnimator(transitionDuration, startingAlpha);
        }

        public double Duration 
        {
            get { return animator.Duration; }
            set { animator.Duration = value; }
        }

        public nfloat StartingAlpha 
        {
            get { return animator.StartingAlpha; }
            set { animator.StartingAlpha = value; }
        }

#if __UNIFIED__
        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForPresentedController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            return animator;
        }
#else
        public override IUIViewControllerAnimatedTransitioning PresentingController(UIViewController presented, UIViewController presenting, UIViewController source)
        {
            return animator;
        }
#endif

        public override IUIViewControllerAnimatedTransitioning GetAnimationControllerForDismissedController(UIViewController dismissed)
        {
            return null;
        }
    }
}
