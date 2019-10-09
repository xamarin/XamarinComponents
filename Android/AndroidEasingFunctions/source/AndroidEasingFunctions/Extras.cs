using System;
using Android.Animation;

namespace AndroidEasingFunctions
{
	partial class Glider
	{
		public static ValueAnimator Glide (Skill skill, float duration, ValueAnimator animator, params Action<BaseEasingMethod.EasingEventArgs>[] listeners)
		{
			BaseEasingMethod.IEasingListenerImplementor impl = null;
			if (listeners != null) {
				impl = new BaseEasingMethod.IEasingListenerImplementor (null);
				impl.Handler += (sender, e) => {
					foreach (var listener in listeners) {
						if (listener != null) {
							listener (e);
						}
					}	
				};
			}

			return Glide (skill, duration, animator, impl);
		}
	}
}
