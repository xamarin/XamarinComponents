using System;
using Android.Views;

namespace Jazzy.Effects
{
    public abstract class JazzyEffect : IJazzyEffect
    {
        public abstract void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels);

        protected float GetEffectOffset(float positionOffset)
        {
            return IsSmall(positionOffset) ? 0 : positionOffset;
        }

        protected bool IsSmall(float positionOffset)
        {
            return Math.Abs(positionOffset) < 0.0001;
        }
    }
}
