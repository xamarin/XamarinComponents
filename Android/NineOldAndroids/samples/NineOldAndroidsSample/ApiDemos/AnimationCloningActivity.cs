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
    [Activity(Label = "API Demos/Animation Cloning", Theme = "@style/Theme.AppCompat")]
    public class AnimationCloningActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimationCloning);

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

            private readonly List<ShapeHolder> balls = new List<ShapeHolder>();
            private AnimatorSet animation = null;
            private float density;

            public MyAnimationView(Context context)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                AddBall(10f * density, 25f);
                AddBall(90f * density, 25f);
                AddBall(160f * density, 25f);
                AddBall(230f * density, 25f);
            }

            public void StartAnimation()
            {
                CreateAnimation();
                animation.Start();
            }

            protected override void OnDraw(Canvas canvas)
            {
                for (int i = 0; i < balls.Count; ++i)
                {
                    ShapeHolder shapeHolder = balls[i];
                    canvas.Save();
                    canvas.Translate(shapeHolder.X, shapeHolder.Y);
                    shapeHolder.Shape.Draw(canvas);
                    canvas.Restore();
                }
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    var anim1 = ObjectAnimator.OfFloat(balls[0], "y", 0f, Height - balls[0].Height);
                    anim1.SetDuration(500);
                    var anim2 = anim1.Clone();
                    anim2.SetTarget(balls[1]);
                    anim1.Update += delegate { Invalidate(); };

                    var ball2 = balls[2];
                    var animDown = ObjectAnimator.OfFloat(ball2, "y", 0f, Height - ball2.Height);
                    animDown.SetDuration(500);
                    animDown.SetInterpolator(new AccelerateInterpolator());
                    var animUp = ObjectAnimator.OfFloat(ball2, "y", Height - ball2.Height, 0f);
                    animUp.SetDuration(500);
                    animUp.SetInterpolator(new DecelerateInterpolator());

                    var s1 = new AnimatorSet();
                    s1.PlaySequentially(animDown, animUp);
                    animDown.Update += delegate { Invalidate(); };
                    animUp.Update += delegate { Invalidate(); };
                    var s2 = (AnimatorSet)s1.Clone();
                    s2.SetTarget(balls[3]);

                    animation = new AnimatorSet();
                    animation.PlayTogether(anim1, anim2, s1);
                    animation.PlaySequentially(s1, s2);
                }
            }

            private void AddBall(float x, float y)
            {
                var rnd = new Random();

                var circle = new OvalShape();
                circle.Resize(BallSize * density, BallSize * density);
                var drawable = new ShapeDrawable(circle);
                var shapeHolder = new ShapeHolder(drawable);
                shapeHolder.X = x;
                shapeHolder.Y = y - (BallSize / 2f);
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
            }
        }
    }
}
