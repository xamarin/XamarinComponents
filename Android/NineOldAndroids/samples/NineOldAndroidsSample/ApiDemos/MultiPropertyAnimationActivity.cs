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
using Java.Interop;

using NineOldAndroids.Animation;

namespace NineOldAndroidsSample.ApiDemos
{
    [IntentFilter(new[] { Intent.ActionMain }, Categories = new[] { "com.yourcompany.nineoldandroids.sample.SAMPLE" })]
    [Activity(Label = "API Demos/Multi-Property Animation", Theme = "@style/Theme.AppCompat")]
    public class MultiPropertyAnimationActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AnimatorCustomEvaluator);

            var container = FindViewById<LinearLayout>(Resource.Id.container);

            var animView = new MyAnimationView(this);
            container.AddView(animView);

            var starter = FindViewById<Button>(Resource.Id.startButton);
            starter.Click += delegate
            {
                animView.StartAnimation();
            };
        }

        public class MyAnimationView : View
        {
            private const float BallSize = 50f;
            private const int Duration = 1500;

            private AnimatorSet animation = null;
            private List<ShapeHolder> balls = new List<ShapeHolder>();
            private float density;

            public MyAnimationView(Context context)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                AddBall(10f * density,  BallSize);
                AddBall(90f * density,  BallSize);
                AddBall(160f * density, BallSize);
                AddBall(230f * density, BallSize);
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    ShapeHolder ball;

                    ball = balls[0];
                    var yBouncer = ObjectAnimator.OfFloat(ball, "y", ball.Y, Height - BallSize);
                    yBouncer.SetDuration(Duration);
                    yBouncer.SetInterpolator(new CycleInterpolator(2));
                    yBouncer.Update += delegate
                    {
                        Invalidate();
                    };

                    ball = balls[1];
                    var pvhY = PropertyValuesHolder.OfFloat("y", ball.Y, Height - BallSize);
                    var pvhAlpha = PropertyValuesHolder.OfFloat("alpha", 1.0f, 0f);
                    var yAlphaBouncer = ObjectAnimator.OfPropertyValuesHolder(ball, pvhY, pvhAlpha);
                    yAlphaBouncer.SetDuration(Duration / 2);
                    yAlphaBouncer.SetInterpolator(new AccelerateInterpolator());
                    yAlphaBouncer.RepeatCount = 1;
                    yAlphaBouncer.RepeatMode = ValueAnimatorRepeatMode.Reverse;

                    ball = balls[2];
                    var pvhW = PropertyValuesHolder.OfFloat("width", ball.Width, ball.Width * 2);
                    var pvhH = PropertyValuesHolder.OfFloat("height", ball.Height, ball.Height * 2);
                    var pvTX = PropertyValuesHolder.OfFloat("x", ball.X, ball.X - BallSize / 2f);
                    var pvTY = PropertyValuesHolder.OfFloat("y", ball.Y, ball.Y - BallSize / 2f);
                    var whxyBouncer = ObjectAnimator.OfPropertyValuesHolder(ball, pvhW, pvhH, pvTX, pvTY);
                    whxyBouncer.SetDuration(Duration / 2);
                    whxyBouncer.RepeatCount = 1;
                    whxyBouncer.RepeatMode = ValueAnimatorRepeatMode.Reverse;

                    ball = balls[3];
                    pvhY = PropertyValuesHolder.OfFloat("y", ball.Y, Height - BallSize);
                    var ballX = ball.X;
                    var kf0 = Keyframe.OfFloat(0f, ballX);
                    var kf1 = Keyframe.OfFloat(.5f, ballX + 100f);
                    var kf2 = Keyframe.OfFloat(1f, ballX + 50f);
                    var pvhX = PropertyValuesHolder.OfKeyframe("x", kf0, kf1, kf2);
                    var yxBouncer = ObjectAnimator.OfPropertyValuesHolder(ball, pvhY, pvhX);
                    yxBouncer.SetDuration(Duration / 2);
                    yxBouncer.RepeatCount = 1;
                    yxBouncer.RepeatMode = ValueAnimatorRepeatMode.Reverse;

                    animation = new AnimatorSet();
                    animation.PlayTogether(yBouncer, yAlphaBouncer, whxyBouncer, yxBouncer);
                }
            }

            public void StartAnimation()
            {
                CreateAnimation();
                animation.Start();
            }

            protected override void OnDraw(Canvas canvas)
            {
                foreach (ShapeHolder ball in balls)
                {
                    canvas.Translate(ball.X, ball.Y);
                    ball.Shape.Draw(canvas);
                    canvas.Translate(-ball.X, -ball.Y);
                }
            }

            private ShapeHolder CreateBall(float x, float y)
            {
                var circle = new OvalShape();
                circle.Resize(BallSize * density, BallSize * density);
                var drawable = new ShapeDrawable(circle);
                var shapeHolder = new ShapeHolder(drawable);
                shapeHolder.X = x;
                shapeHolder.Y = y - (BallSize / 2f);
                return shapeHolder;
            }

            private void AddBall(float x, float y, Color color)
            {
                var shapeHolder = CreateBall(x, y);
                shapeHolder.Color = color;
                balls.Add(shapeHolder);
            }

            private void AddBall(float x, float y)
            {
                var rnd = new Random();
                var shapeHolder = CreateBall(x, y);
                var red = 100 + rnd.Next(155);
                var green = 100 + rnd.Next(155);
                var blue = 100 + rnd.Next(155);
                var color = new Color(red, green, blue);
                var darkColor = new Color(red / 4, green / 4, blue);
                var paint = shapeHolder.Shape.Paint;
                var gradient = new RadialGradient(37.5f, 12.5f, 50f, color, darkColor, Shader.TileMode.Clamp);
                paint.SetShader(gradient);
                balls.Add(shapeHolder);
            }
        }
    }
}
