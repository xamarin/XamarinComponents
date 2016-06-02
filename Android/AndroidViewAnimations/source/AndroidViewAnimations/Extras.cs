using System;
using NineOldAndroids.Animation;

namespace AndroidViewAnimations
{
	partial class BaseViewAnimator
	{
		public event EventHandler<Animator.AnimationCancelEventArgs> AnimationCancel {
			add {
				AnimatorAgent.AnimationCancel += value;
			}
			remove {
				AnimatorAgent.AnimationCancel -= value;
			}
		}

		public event EventHandler<Animator.AnimationEndEventArgs> AnimationEnd {
			add {
				AnimatorAgent.AnimationEnd += value;
			}
			remove {
				AnimatorAgent.AnimationEnd -= value;
			}
		}

		public event EventHandler<Animator.AnimationRepeatEventArgs> AnimationRepeat {
			add {
				AnimatorAgent.AnimationRepeat += value;
			}
			remove {
				AnimatorAgent.AnimationRepeat -= value;
			}
		}

		public event EventHandler<Animator.AnimationStartEventArgs> AnimationStart {
			add {
				AnimatorAgent.AnimationStart += value;
			}
			remove {
				AnimatorAgent.AnimationStart -= value;
			}
		}
	}

	partial class YoYo
	{
		partial class AnimationComposer
		{
			public YoYo.AnimationComposer WithListener (
				Action<Animator> onStart,
				Action<Animator> onEnd,
				Action<Animator> onRepeat,
				Action<Animator> onCancel)
			{
				return WithListener (new ActionListener (onStart, onEnd, onRepeat, onCancel));
			}

			public YoYo.AnimationComposer WithStartListener (Action<Animator> onStart)
			{
				return WithListener (onStart, null, null, null);
			}

			public YoYo.AnimationComposer WithEndListener (Action<Animator> onEnd)
			{
				return WithListener (null, onEnd, null, null);
			}

			public YoYo.AnimationComposer WithRepeatListener (Action<Animator> onRepeat)
			{
				return WithListener (null, null, onRepeat, null);
			}

			public YoYo.AnimationComposer WithCancelListener (Action<Animator> onCancel)
			{
				return WithListener (null, null, null, onCancel);
			}

			private class ActionListener : Java.Lang.Object, Animator.IAnimatorListener
			{
				private readonly Action<Animator> onStart;
				private readonly Action<Animator> onEnd;
				private readonly Action<Animator> onRepeat;
				private readonly Action<Animator> onCancel;

				public ActionListener (Action<Animator> onStart, Action<Animator> onEnd, Action<Animator> onRepeat, Action<Animator> onCancel)
				{
					this.onCancel = onCancel;
					this.onRepeat = onRepeat;
					this.onEnd = onEnd;
					this.onStart = onStart;
				}

				public void OnAnimationCancel (Animator animation)
				{
					if (onCancel != null) {
						onCancel (animation);
					}
				}

				public void OnAnimationEnd (Animator animation)
				{
					if (onEnd != null) {
						onEnd (animation);
					}
				}

				public void OnAnimationRepeat (Animator animation)
				{
					if (onRepeat != null) {
						onRepeat (animation);
					}
				}

				public void OnAnimationStart (Animator animation)
				{
					if (onStart != null) {
						onStart (animation);
					}
				}
			}
		}
	}
}
