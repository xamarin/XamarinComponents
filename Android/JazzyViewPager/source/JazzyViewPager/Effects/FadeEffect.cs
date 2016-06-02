using Android.Views;

namespace Jazzy.Effects
{
    public class FadeEffect : JazzyEffect
    {
        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var translate = positionOffsetPixels;
                    left.Alpha = 1 - effectOffset;
                    left.TranslationX = translate;
                }
                if (right != null)
                {
                    right.Alpha = effectOffset;
                    var translate = -viewPager.Width - viewPager.PageMargin + positionOffsetPixels;
                    right.TranslationX = translate;
                }
            }
        }
    }
}
