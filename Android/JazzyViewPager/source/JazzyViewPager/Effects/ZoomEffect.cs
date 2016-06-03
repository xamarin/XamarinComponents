using Android.Views;

namespace Jazzy.Effects
{
    public class ZoomEffect : JazzyEffect
    {
        private const float DefaultZoomMaximum = 0.5f;

        public ZoomEffect()
        {
            ZoomMaximum = DefaultZoomMaximum;
        }

        public ZoomEffect(float zoomMaximum)
        {
            ZoomMaximum = zoomMaximum;
        }

        public float ZoomMaximum { get; private set; }

        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var scale = ZoomMaximum + (1 - ZoomMaximum) * (1 - effectOffset);
                    left.PivotX = left.MeasuredWidth * 0.5f;
                    left.PivotY = left.MeasuredHeight * 0.5f;
                    left.ScaleX = scale;
                    left.ScaleY = scale;
                }
                if (right != null)
                {
                    var scale = ZoomMaximum + (1 - ZoomMaximum) * effectOffset;
                    right.PivotX = right.MeasuredWidth * 0.5f;
                    right.PivotY = right.MeasuredHeight * 0.5f;
                    right.ScaleX = scale;
                    right.ScaleY = scale;
                }
            }
        }
    }
}
