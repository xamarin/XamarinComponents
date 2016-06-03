using System;
using Android.Graphics;
using Android.Views;

namespace Jazzy.Effects
{
    public class TabletEffect : JazzyEffect
    {
        private Matrix mMatrix = new Matrix();
        private Camera mCamera = new Camera();
        private float[] mTempFloat2 = new float[2];

        public override void Animate(JazzyViewPager viewPager, View left, View right, float positionOffset, float positionOffsetPixels)
        {
            if (viewPager.State != JazzyState.Idle)
            {
                var effectOffset = GetEffectOffset(positionOffset);
                if (left != null)
                {
                    var rotation = 30.0f * effectOffset;
                    var translate = GetOffsetXForRotation(rotation, left.MeasuredWidth, left.MeasuredHeight);
                    left.PivotX = left.MeasuredWidth * 0.5f;
                    left.PivotY = left.MeasuredHeight * 0.5f;
                    left.TranslationX = translate;
                    left.RotationY = rotation;
                }
                if (right != null)
                {
                    var rotation = -30.0f * (1 - effectOffset);
                    var translate = GetOffsetXForRotation(rotation, right.MeasuredWidth, right.MeasuredHeight);
                    right.PivotX = right.MeasuredWidth * 0.5f;
                    right.PivotY = right.MeasuredHeight * 0.5f;
                    right.TranslationX = translate;
                    right.RotationY = rotation;
                }
            }
        }

        private float GetOffsetXForRotation(float degrees, int width, int height, float offset = 0.5f)
        {
            mMatrix.Reset();
            mCamera.Save();
            mCamera.RotateY(Math.Abs(degrees));
            mCamera.GetMatrix(mMatrix);
            mCamera.Restore();

            mMatrix.PreTranslate(-width * offset, -height * 0.5f);
            mMatrix.PostTranslate(width * offset, height * 0.5f);
            mTempFloat2[0] = width;
            mTempFloat2[1] = height;
            mMatrix.MapPoints(mTempFloat2);
            return (width - mTempFloat2[0]) * (degrees > 0.0f ? 1.0f : -1.0f);
        }
    }
}
