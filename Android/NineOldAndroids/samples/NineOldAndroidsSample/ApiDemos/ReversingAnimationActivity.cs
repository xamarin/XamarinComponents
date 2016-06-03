using System;
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
    [Activity(Label = "API Demos/Reversing Animation", Theme = "@style/Theme.AppCompat")]
    public class ReversingAnimationActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AnimationReversing);

            var container = FindViewById<LinearLayout>(Resource.Id.container);

            var animView = new MyAnimationView(this);
            container.AddView(animView);

            var starter = FindViewById<Button>(Resource.Id.startButton);
            starter.Click += delegate
            {
                animView.StartAnimation();
            };

            var reverse = FindViewById<Button>(Resource.Id.reverseButton);
            reverse.Click += delegate
            {
                animView.ReverseAnimation();
            };
        }
        
        public class MyAnimationView : View
        {
            private const float BallSize = 50f;

            private ObjectAnimator animation = null;
            private ShapeHolder ball = null;
            private float density;

            public MyAnimationView(Context context)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                ball = CreateBall(25, 25);
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    animation = ObjectAnimator.OfFloat(ball, "y", ball.Y, Height - 50f);
                    animation.SetDuration(1500);
                    animation.SetInterpolator(new AccelerateInterpolator(2f));
                    animation.Update += delegate
                    {
                        Invalidate();
                    };
                }
            }

            public void StartAnimation()
            {
                CreateAnimation();
                animation.Start();
            }

            public void ReverseAnimation()
            {
                CreateAnimation();
                animation.Reverse();
            }

            public void Seek(long seekTime)
            {
                CreateAnimation();
                animation.CurrentPlayTime = seekTime;
            }

            private ShapeHolder CreateBall(float x, float y)
            {
                var rnd = new Random();
                var circle = new OvalShape();
                circle.Resize(BallSize * density, BallSize * density);
                var drawable = new ShapeDrawable(circle);
                var shapeHolder = new ShapeHolder(drawable);
                shapeHolder.X = x - 25f;
                shapeHolder.Y = y - 25f;
                var red = 100 + rnd.Next(155);
                var green = 100 + rnd.Next(155);
                var blue = 100 + rnd.Next(155);
                var color = new Color(red, green, blue);
                var darkColor = new Color(red / 4, green / 4, blue);
                var paint = drawable.Paint;
                var gradient = new RadialGradient(37.5f, 12.5f, 50f, color, darkColor, Shader.TileMode.Clamp);
                paint.SetShader(gradient);
                shapeHolder.Paint = paint;
                return shapeHolder;
            }

            protected override void OnDraw(Canvas canvas)
            {
                canvas.Save();
                canvas.Translate(ball.X, ball.Y);
                ball.Shape.Draw(canvas);
                canvas.Restore();
            }
        }
    }
}
