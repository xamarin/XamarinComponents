using Android.Views;

namespace Jazzy.Effects
{
    public class AccordionEffect : JazzyEffect
    {
        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    left.PivotX = left.MeasuredWidth;
                    left.PivotY = 0;
                    left.ScaleX = 1 - effectOffset;
                }
                if (right != null)
                {
                    right.PivotX = 0;
                    right.PivotY = 0;
                    right.ScaleX = effectOffset;
                }
            }
        }
    }
}
