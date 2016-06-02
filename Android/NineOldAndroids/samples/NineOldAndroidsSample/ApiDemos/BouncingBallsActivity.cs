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
    [Activity(Label = "API Demos/Bouncing Balls", Theme = "@style/Theme.AppCompat")]
    public class BouncingBallsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.BouncingBalls);

            var container = FindViewById<LinearLayout>(Resource.Id.container);
            container.AddView(new MyAnimationView(this));
        }

        public class MyAnimationView : View
        {
            private const float BallSize = 50f;

            internal Color Red = new Color(255, 127, 127);
            internal Color Blue = new Color(127, 127, 255);

            public readonly List<ShapeHolder> balls = new List<ShapeHolder>();
            internal AnimatorSet animation = null;
            private float density;

            public MyAnimationView(Context context)
                : base(context)
            {
                density = Context.Resources.DisplayMetrics.Density;

                // Animate background color
                // Note that setting the background color will automatically invalidate the
                // view, so that the animated color, and the bouncing balls, get redisplayed on
                // every frame of the animation.
                var colorAnim = ObjectAnimator.OfInt(this, "backgroundColor", Red, Blue);
                colorAnim.SetDuration(3000);
                colorAnim.SetEvaluator(new ArgbEvaluator());
                colorAnim.RepeatCount = ValueAnimator.Infinite;
                colorAnim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                colorAnim.Start();
            }

            public override bool OnTouchEvent(MotionEvent motionEvent)
            {
                if (motionEvent.Action != MotionEventActions.Down && motionEvent.Action != MotionEventActions.Move)
                {
                    return false;
                }
                var newBall = AddBall(motionEvent.GetX(), motionEvent.GetY());

                // Bouncing animation with squash and stretch
                var startY = newBall.Y;
                var endY = Height - 50f;
                var h = (float)Height;
                var eventY = motionEvent.GetY();
                var duration = (int)(500 * ((h - eventY) / h));

                var bounceAnim = ObjectAnimator.OfFloat(newBall, "y", startY, endY);
                bounceAnim.SetDuration(duration);
                bounceAnim.SetInterpolator(new AccelerateInterpolator());
                var squashAnim1 = ObjectAnimator.OfFloat(newBall, "x", newBall.X, newBall.X - 25f);
                squashAnim1.SetDuration(duration / 4);
                squashAnim1.RepeatCount = 1;
                squashAnim1.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                squashAnim1.SetInterpolator(new DecelerateInterpolator());
                var squashAnim2 = ObjectAnimator.OfFloat(newBall, "width", newBall.Width, newBall.Width + 50);
                squashAnim2.SetDuration(duration / 4);
                squashAnim2.RepeatCount = 1;
                squashAnim2.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                squashAnim2.SetInterpolator(new DecelerateInterpolator());
                var stretchAnim1 = ObjectAnimator.OfFloat(newBall, "y", endY, endY + 25f);
                stretchAnim1.SetDuration(duration / 4);
                stretchAnim1.RepeatCount = 1;
                stretchAnim1.SetInterpolator(new DecelerateInterpolator());
                stretchAnim1.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                var stretchAnim2 = ObjectAnimator.OfFloat(newBall, "height", newBall.Height, newBall.Height - 25);
                stretchAnim2.SetDuration(duration / 4);
                stretchAnim2.RepeatCount = 1;
                stretchAnim2.SetInterpolator(new DecelerateInterpolator());
                stretchAnim2.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                var bounceBackAnim = ObjectAnimator.OfFloat(newBall, "y", endY, startY);
                bounceBackAnim.SetDuration(duration);
                bounceBackAnim.SetInterpolator(new DecelerateInterpolator());

                // Sequence the down/squash&stretch/up animations
                var bouncer = new AnimatorSet();
                bouncer.Play(bounceAnim).Before(squashAnim1);
                bouncer.Play(squashAnim1).With(squashAnim2);
                bouncer.Play(squashAnim1).With(stretchAnim1);
                bouncer.Play(squashAnim1).With(stretchAnim2);
                bouncer.Play(bounceBackAnim).After(stretchAnim2);

                // Fading animation - remove the ball when the animation is done
                var fadeAnim = ObjectAnimator.OfFloat(newBall, "alpha", 1f, 0f);
                fadeAnim.SetDuration(250);
                fadeAnim.AnimationEnd += (sender, e) =>
                {
                    var animator = (ObjectAnimator)e.Animation;
                    balls.Remove((ShapeHolder)animator.Target);
                };

                // Sequence the two animations to play one after the other
                var animatorSet = new AnimatorSet();
                animatorSet.Play(bouncer).Before(fadeAnim);

                // Start the animation
                animatorSet.Start();

                return true;
            }

            internal virtual ShapeHolder AddBall(float x, float y)
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
                Paint paint = drawable.Paint; //new Paint(Paint.ANTI_ALIAS_FLAG);
                RadialGradient gradient = new RadialGradient(37.5f, 12.5f, 50f, color, darkColor, Shader.TileMode.Clamp);
                paint.SetShader(gradient);
                shapeHolder.Paint = paint;
                balls.Add(shapeHolder);
                return shapeHolder;
            }

            protected override void OnDraw(Canvas canvas)
            {
                for (int i = 0; i < balls.Count; ++i)
                {
                    var shapeHolder = balls[i];
                    canvas.Save();
                    canvas.Translate(shapeHolder.X, shapeHolder.Y);
                    shapeHolder.Shape.Draw(canvas);
                    canvas.Restore();
                }
            }
        }
    }
}
