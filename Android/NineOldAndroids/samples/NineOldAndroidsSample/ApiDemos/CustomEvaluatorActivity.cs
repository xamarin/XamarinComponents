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
    [Activity(Label = "API Demos/Custom Evaluator", Theme = "@style/Theme.AppCompat")]
    public class CustomEvaluatorActivity : AppCompatActivity
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

        public class XYHolder : Java.Lang.Object
        {
            public XYHolder(float x, float y)
            {
                X = x;
                Y = y;
            }

            public float X { get; set; }

            public float Y { get; set; }
        }

        public class BallXYHolder : Java.Lang.Object
        {
            public BallXYHolder(ShapeHolder ball)
            {
                Ball = ball;
            }

            public ShapeHolder Ball { get; set; }

            public virtual XYHolder XY
            {
                [Export("getXY")] get { return new XYHolder(Ball.X, Ball.Y); }
                [Export("setXY")]
                set
                {
                    Ball.X = value.X;
                    Ball.Y = value.Y;
                }
            }
        }

        public class XYEvaluator : Java.Lang.Object, ITypeEvaluator
        {
            public virtual Java.Lang.Object Evaluate(float fraction, Java.Lang.Object startValue, Java.Lang.Object endValue)
            {
                var startXY = (XYHolder)startValue;
                var endXY = (XYHolder)endValue;
                var x = startXY.X + fraction * (endXY.X - startXY.X);
                var y = startXY.Y + fraction * (endXY.Y - startXY.Y);
                return new XYHolder(x, y);
            }
        }

        public class MyAnimationView : View
        {
            private const float BallSize = 50f;

            private ObjectAnimator animation = null;
            private ShapeHolder ball = null;
            private BallXYHolder ballHolder = null;
            private float density;

            public MyAnimationView(Context context)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                ball = CreateBall(25, 25);
                ballHolder = new BallXYHolder(ball);
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    var startXY = new XYHolder(0f, 0f);
                    var endXY = new XYHolder(300f * density, 500f * density);
                    animation = ObjectAnimator.OfObject(ballHolder, "xY", new XYEvaluator(), endXY);
                    animation.SetDuration(1500);
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
