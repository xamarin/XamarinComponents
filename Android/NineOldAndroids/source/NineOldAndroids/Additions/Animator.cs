using System;
using Java.Interop;

namespace NineOldAndroids.Animation
{
    public partial class Animator
    {
        private WeakReference animatorListener;

        public event EventHandler<AnimationStartEventArgs> AnimationStart
        {
            add
            {
                EventHelper.AddEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        () => new IAnimatorListenerImplementor(this),
                        AddListener,
                        h => h.OnAnimationStartHandler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        IAnimatorListenerImplementor.__IsEmpty,
                        RemoveListener,
                        h => h.OnAnimationStartHandler -= value);
            }
        }

        public event EventHandler<AnimationRepeatEventArgs> AnimationRepeat
        {
            add
            {
                EventHelper.AddEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        () => new IAnimatorListenerImplementor(this),
                        AddListener,
                        h => h.OnAnimationRepeatHandler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        IAnimatorListenerImplementor.__IsEmpty,
                        RemoveListener,
                        h => h.OnAnimationRepeatHandler -= value);
            }
        }

        public event EventHandler<AnimationEndEventArgs> AnimationEnd
        {
            add
            {
                EventHelper.AddEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        () => new IAnimatorListenerImplementor(this),
                        AddListener,
                        h => h.OnAnimationEndHandler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        IAnimatorListenerImplementor.__IsEmpty,
                        RemoveListener,
                        h => h.OnAnimationEndHandler -= value);
            }
        }

        public event EventHandler<AnimationCancelEventArgs> AnimationCancel
        {
            add
            {
                EventHelper.AddEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        () => new IAnimatorListenerImplementor(this),
                        AddListener,
                        h => h.OnAnimationCancelHandler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<IAnimatorListener, IAnimatorListenerImplementor>(
                        ref animatorListener,
                        IAnimatorListenerImplementor.__IsEmpty,
                        RemoveListener,
                        h => h.OnAnimationCancelHandler -= value);
            }
        }
    }
}
