using Android.Views;

namespace Jazzy.Effects
{
    public class StackEffect : JazzyEffect
    {
        private const float DefaultScaleMaximum = 0.5f;

        public StackEffect()
        {
            ScaleMaximum = DefaultScaleMaximum;
        }

        public StackEffect(float scaleMaximum)
        {
            ScaleMaximum = scaleMaximum;
        }

        public float ScaleMaximum { get; private set; }

        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (right != null)
                {
                    var scale = (1 - ScaleMaximum) * effectOffset + ScaleMaximum;
                    var translate = -viewPager.Width - viewPager.PageMargin + positionOffsetPixels;
                    right.ScaleX = scale;
                    right.ScaleY = scale;
                    right.TranslationX = translate;
                }
                if (left != null)
                {
                    left.BringToFront();
                }
            }
        }
    }
}
