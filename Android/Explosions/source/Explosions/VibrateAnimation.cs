using System;
using Android.Views.Animations;

namespace Explosions
{
    internal class VibrateAnimation : Animation
    {
        private readonly Random random = new Random();

        private float horizontal;
        private float vertical;

        public VibrateAnimation(float horizontal, float vertical)
        {
            this.horizontal = horizontal;
            this.vertical = vertical;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            var x = random.NextFloat() - 0.5f;
            var y = random.NextFloat() - 0.5f;
            t.Matrix.SetTranslate(x * horizontal, y * vertical);
        }
    }
}
