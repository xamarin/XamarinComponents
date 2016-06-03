using System;
using System.Linq;
using Android.Graphics;
using Android.Views;
using Android.Views.Animations;

namespace Explosions
{
    internal class ExplosionAnimator
    {
        public const long DefaultDuration = 600;

        private static readonly IInterpolator DefaultInterpolator = new AccelerateInterpolator(0.6f);
        private const float StartValue = 0.0f;
        private const float EndValue = 1.4f;
        private static readonly float X = Utils.Dp2Px(5);
        private static readonly float Y = Utils.Dp2Px(20);
        private static readonly float V = Utils.Dp2Px(2);
        private static readonly float W = Utils.Dp2Px(1);

        private readonly Random random = new Random();

        private DateTime animationStartTime;

        private Rect bounds;
        private View explosionView;
        private Paint paint;
        private Particle[] particles;

        public ExplosionAnimator(View explosionView, Bitmap bitmap, Rect bounds)
        {
            this.bounds = bounds;
            this.explosionView = explosionView;
            paint = new Paint();
            int partLen = 15;
            particles = new Particle[partLen * partLen];

            int w = bitmap.Width / (partLen + 2);
            int h = bitmap.Height / (partLen + 2);
            for (int i = 0; i < partLen; i++)
            {
                for (int j = 0; j < partLen; j++)
                {
                    var color = bitmap.GetPixel((j + 1) * w, (i + 1) * h);
                    particles[(i * partLen) + j] = GenerateParticle(new Color(color));
                }
            }
            Interpolator = DefaultInterpolator;
            Duration = DefaultDuration;
        }

        public long StartDelay { get; set; }

        public long Duration { get; set; }

        public IInterpolator Interpolator { get; set; }

        public event EventHandler AnimationEnd;

        protected void OnAnimationEnd()
        {
            var handler = AnimationEnd;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private Particle GenerateParticle(Color color)
        {
            var particle = new Particle();
            particle.color = color;
            particle.radius = V;
            if (random.NextFloat() < 0.2f)
            {
                particle.baseRadius = V + ((X - V) * random.NextFloat());
            }
            else
            {
                particle.baseRadius = W + ((V - W) * random.NextFloat());
            }

            var nextFloat = random.NextFloat();

            particle.top = bounds.Height() * ((0.18f * random.NextFloat()) + 0.2f);
            if (nextFloat >= 0.2f)
            {
                particle.top = particle.top + ((particle.top * 0.2f) * random.NextFloat());
            }

            particle.bottom = bounds.Height() * (random.NextFloat() - 0.5f) * 1.8f;
            if (nextFloat >= 0.2f)
            {
                particle.bottom = nextFloat < 0.8f ? particle.bottom * 0.6f : particle.bottom * 0.3f;
            }

            particle.mag = 4.0f * particle.top / particle.bottom;
            particle.neg = (-particle.mag) / particle.bottom;

            var x = bounds.CenterX() + (Y * (random.NextFloat() - 0.5f));
            particle.baseCx = x;
            particle.cx = x;

            var y = bounds.CenterY() + (Y * (random.NextFloat() - 0.5f));
            particle.baseCy = y;
            particle.cy = y;

            particle.life = EndValue / 10 * random.NextFloat();
            particle.overflow = 0.4f * random.NextFloat();
            particle.alpha = 1f;

            return particle;
        }

        public bool Draw(Canvas canvas)
        {
            // not yet started
            var delta = (float)(DateTime.UtcNow - animationStartTime).TotalMilliseconds;
            if (delta < StartDelay)
            {
                explosionView.PostInvalidateDelayed(10);
                return false;
            }

            // ending
            delta -= StartDelay;
            if (delta > Duration)
            {
                OnAnimationEnd();
                return false;
            }

            // exploding
            var interpolatedTime = Interpolator.GetInterpolation(delta / Duration);
            var value = StartValue + ((EndValue - StartValue) * interpolatedTime);
            foreach (Particle particle in particles.ToArray())
            {
                particle.Advance(value);
                if (particle.alpha > 0f)
                {
                    paint.Color = particle.color;
                    paint.Alpha = (int)(particle.color.A * particle.alpha);
                    canvas.DrawCircle(particle.cx, particle.cy, particle.radius, paint);
                }
            }

            // loop
            explosionView.Invalidate();
            return true;
        }

        public void Start()
        {
            animationStartTime = DateTime.UtcNow;

            explosionView.Invalidate(bounds);
        }

        private class Particle
        {
            public Color color;
            public float alpha;
            public float cx;
            public float cy;
            public float radius;
            public float baseCx;
            public float baseCy;
            public float baseRadius;
            public float top;
            public float bottom;
            public float mag;
            public float neg;
            public float life;
            public float overflow;

            public void Advance(float factor)
            {
                var normalization = factor / EndValue;
                if (normalization < life || normalization > 1f - overflow)
                {
                    alpha = 0f;
                    return;
                }
                normalization = (normalization - life) / (1f - life - overflow);
                var f2 = normalization * EndValue;
                var f = normalization >= 0.7f ? (normalization - 0.7f) / 0.3f : 0f;
                alpha = 1f - f;
                f = bottom * f2;
                cx = baseCx + f;
                cy = baseCy - neg * f * f - f * mag;
                radius = V + (baseRadius - V) * f2;
            }
        }
    }
}
