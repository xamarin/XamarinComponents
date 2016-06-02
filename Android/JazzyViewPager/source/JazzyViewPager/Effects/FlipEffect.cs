using Android.Views;

namespace Jazzy.Effects
{
    public class FlipEffect : JazzyEffect
    {
        private const bool DefaultFlipAway = true;

        public FlipEffect()
        {
            FlipAway = DefaultFlipAway;
        }

        public FlipEffect(bool flipAway)
        {
            FlipAway = flipAway;
        }

        public bool FlipAway { get; private set; }

        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var dir = FlipAway ? 1f : -1f;
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var rotation = dir * 180.0f * effectOffset;
                    if ((FlipAway && rotation > 90.0f) || (!FlipAway && rotation < -90.0f))
                    {
                        left.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        if (left.Visibility == ViewStates.Invisible)
                        {
                            left.Visibility = ViewStates.Visible;
                        }
                        var translate = positionOffsetPixels;
                        left.PivotX = left.MeasuredWidth * 0.5f;
                        left.PivotY = left.MeasuredHeight * 0.5f;
                        left.TranslationX = translate;
                        left.RotationY = rotation;
                    }
                }
                if (right != null)
                {
                    var rotation = dir * -180.0f * (1 - effectOffset);
                    if ((FlipAway && rotation < -90.0f) || (!FlipAway && rotation > 90.0f))
                    {
                        right.Visibility = ViewStates.Invisible;
                    }
                    else
                    {
                        if (right.Visibility == ViewStates.Invisible)
                        {
                            right.Visibility = ViewStates.Visible;
                        }
                        var translate = -viewPager.Width - viewPager.PageMargin + positionOffsetPixels;
                        right.PivotX = right.MeasuredWidth * 0.5f;
                        right.PivotY = right.MeasuredHeight * 0.5f;
                        right.TranslationX = translate;
                        right.RotationY = rotation;
                    }
                }
            }
            
            JazzyEffects.Stack.Animate(viewPager, left, right, positionOffset, positionOffsetPixels);
        }
    }
}
