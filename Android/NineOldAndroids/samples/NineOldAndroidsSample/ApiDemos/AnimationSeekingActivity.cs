using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

using NineOldAndroids.Animation;

namespace NineOldAndroidsSample.ApiDemos
{
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "com.yourcompany.nineoldandroids.sample.SAMPLE" })]
    [Activity(Label = "API Demos/Animation Seeking", Theme = "@style/Theme.AppCompat")]
    public class AnimationSeekingActivity : AppCompatActivity
    {
        private const int Duration = 1500;
        private SeekBar seekBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimationSeeking);

            var container = FindViewById<LinearLayout>(Resource.Id.container);

            seekBar = FindViewById<SeekBar>(Resource.Id.seekBar);

            var animView = new MyAnimationView(this, seekBar);
            container.AddView(animView);

            seekBar.Max = Duration;
            seekBar.ProgressChanged += (sender, e) =>
            {
                // prevent seeking on app creation
                if (animView.Height != 0)
                {
                    animView.Seek(e.Progress);
                }
            };

            var starter = FindViewById<Button>(Resource.Id.startButton);
            starter.Click += delegate
            {
                animView.StartAnimation();
            };
        }

        public class MyAnimationView : View
        {
            private const float BallSize = 50f;

            private readonly List<ShapeHolder> balls = new List<ShapeHolder>();
            private ValueAnimator animation = null;
            private ShapeHolder ball = null;
            private SeekBar seekBar = null;
            private float density;

            public MyAnimationView(Context context, SeekBar seekBar)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                this.seekBar = seekBar;
                ball = AddBall(10 * density, 0);
            }

            public void StartAnimation()
            {
                CreateAnimation();
                animation.Start();
            }

            public void Seek(long seekTime)
            {
                CreateAnimation();
                animation.CurrentPlayTime = seekTime;
            }

            protected override void OnDraw(Canvas canvas)
            {
                canvas.Translate(ball.X, ball.Y);
                ball.Shape.Draw(canvas);
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    animation = ObjectAnimator.OfFloat(ball, "y", ball.Y, Height);
                    animation.SetDuration(1500);
                    animation.SetInterpolator(new CycleInterpolator(2));
                    animation.Update += (sender, e) =>
                    {
                        Invalidate();
                        var playtime = (int)animation.CurrentPlayTime;
                        if (seekBar.Progress != playtime)
                        {
                            seekBar.Progress = playtime;
                        }
                    };
                }
            }

            private ShapeHolder AddBall(float x, float y)
            {
                var rnd = new Random();
                var circle = new OvalShape();
                circle.Resize(BallSize * density, BallSize * density);
                var drawable = new ShapeDrawable(circle);
                var shapeHolder = new ShapeHolder(drawable);
                shapeHolder.X = x;
                shapeHolder.Y = y;// - (BallSize / 2f);
                var red = 100 + rnd.Next(155);
                var green = 100 + rnd.Next(155);
                var blue = 100 + rnd.Next(155);
                var color = new Color(red, green, blue);
                var darkColor = new Color(red / 4, green / 4, blue);
                var paint = drawable.Paint;
                var gradient = new RadialGradient(37.5f, 12.5f, 50f, color, darkColor, Shader.TileMode.Clamp);
                paint.SetShader(gradient);
                shapeHolder.Paint = paint;
                balls.Add(shapeHolder);
                return shapeHolder;
            }
        }
    }
}
