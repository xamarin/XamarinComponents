using Android.Views;

namespace Jazzy.Effects
{
    public class CubeEffect : JazzyEffect
    {
        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var rotation = -90.0f * effectOffset;
                    left.PivotX = left.MeasuredWidth;
                    left.PivotY = left.MeasuredHeight * 0.5f;
                    left.RotationY = rotation;
                }
                if (right != null)
                {
                    var rotation = 90.0f * (1 - effectOffset);
                    right.PivotX = 0;
                    right.PivotY = right.MeasuredHeight * 0.5f;
                    right.RotationY = rotation;
                }
            }
        }
    }
}
