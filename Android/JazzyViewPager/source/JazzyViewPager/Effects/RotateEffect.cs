using System;
using Android.Views;

namespace Jazzy.Effects
{
    public class RotateEffect : JazzyEffect
    {
        private const bool DefaultRotateUp = false;
        private const float DefaultRotationMaximum = 15.0f;

        public RotateEffect()
        {
            RotateUp = DefaultRotateUp;
            RotationMaximum = DefaultRotationMaximum;
        }

        public RotateEffect(bool up)
        {
            RotateUp = up;
            RotationMaximum = DefaultRotationMaximum;
        }

        public RotateEffect(bool up, float rotationMaximum)
        {
            RotateUp = up;
            RotationMaximum = rotationMaximum;
        }

        public bool RotateUp { get; private set; }

        public float RotationMaximum { get; private set; }

        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                var height = viewPager.MeasuredHeight;
                if (left != null)
                {
                    var rotation = (RotateUp ? 1 : -1) * (RotationMaximum * effectOffset);
                    var translate = (RotateUp ? -1 : 1) * (float)(height - height * Math.Cos(rotation * Math.PI / 180.0f));
                    left.PivotX = left.MeasuredWidth * 0.5f;
                    left.PivotY = RotateUp ? 0 : left.MeasuredHeight;
                    left.TranslationY = translate;
                    left.Rotation = rotation;
                }
                if (right != null)
                {
                    var rotation = (RotateUp ? 1 : -1) * (-RotationMaximum + RotationMaximum * effectOffset);
                    var translate = (RotateUp ? -1 : 1) * (float)(height - height * Math.Cos(rotation * Math.PI / 180.0f));
                    right.PivotX = right.MeasuredWidth * 0.5f;
                    right.PivotY = RotateUp ? 0 : right.MeasuredHeight;
                    right.TranslationY = translate;
                    right.Rotation = rotation;
                }
            }
        }
    }
}
