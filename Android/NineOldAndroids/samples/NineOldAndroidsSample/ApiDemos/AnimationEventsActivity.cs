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
    [Activity(Label = "API Demos/Animation Events", Theme = "@style/Theme.AppCompat")]
    public class AnimationEventsActivity : AppCompatActivity
    {
        private static Color Off = new Color(255, 255, 255, 152);
        private static Color On = new Color(255, 255, 255, 255);

        internal TextView startText, repeatText, cancelText, endText;
        internal TextView startTextAnimator, repeatTextAnimator, cancelTextAnimator, endTextAnimator;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AnimatorEvents);

            var container = FindViewById<LinearLayout>(Resource.Id.container);

            var animView = new MyAnimationView(this);
            container.AddView(animView);

            startText = FindViewById<TextView>(Resource.Id.startText);
            startText.SetTextColor(Off);
            repeatText = FindViewById<TextView>(Resource.Id.repeatText);
            repeatText.SetTextColor(Off);
            cancelText = FindViewById<TextView>(Resource.Id.cancelText);
            cancelText.SetTextColor(Off);
            endText = FindViewById<TextView>(Resource.Id.endText);
            endText.SetTextColor(Off);
            startTextAnimator = FindViewById<TextView>(Resource.Id.startTextAnimator);
            startTextAnimator.SetTextColor(Off);
            repeatTextAnimator = FindViewById<TextView>(Resource.Id.repeatTextAnimator);
            repeatTextAnimator.SetTextColor(Off);
            cancelTextAnimator = FindViewById<TextView>(Resource.Id.cancelTextAnimator);
            cancelTextAnimator.SetTextColor(Off);
            endTextAnimator = FindViewById<TextView>(Resource.Id.endTextAnimator);
            endTextAnimator.SetTextColor(Off);
            var endCB = FindViewById<CheckBox>(Resource.Id.endCB);

            var starter = FindViewById<Button>(Resource.Id.startButton);
            starter.Click += delegate
            {
                animView.StartAnimation(endCB.Checked);
            };

            var canceler = FindViewById<Button>(Resource.Id.cancelButton);
            canceler.Click += delegate
            {
                animView.CancelAnimation();
            };

            var ender = FindViewById<Button>(Resource.Id.endButton);
            ender.Click += delegate
            {
                animView.EndAnimation();
            };
        }

        public class MyAnimationView : View
        {
            private const float BallSize = 50f;

            private readonly List<ShapeHolder> balls = new List<ShapeHolder>();
            private AnimatorSet animation;
            private ShapeHolder ball = null;
            private bool endImmediately = false;
            private float density;
            private AnimationEventsActivity activity;

            public MyAnimationView(AnimationEventsActivity context)
                : base(context)
            {
                activity = context;
                density = Context.Resources.DisplayMetrics.Density;
                ball = CreateBall(25, 25);
            }

            public void StartAnimation(bool endImmediately)
            {
                this.endImmediately = endImmediately;
                activity.startText.SetTextColor(Off);
                activity.repeatText.SetTextColor(Off);
                activity.cancelText.SetTextColor(Off);
                activity.endText.SetTextColor(Off);
                activity.startTextAnimator.SetTextColor(Off);
                activity.repeatTextAnimator.SetTextColor(Off);
                activity.cancelTextAnimator.SetTextColor(Off);
                activity.endTextAnimator.SetTextColor(Off);
                CreateAnimation();
                animation.Start();
            }

            public void CancelAnimation()
            {
                CreateAnimation();
                animation.Cancel();
            }

            public void EndAnimation()
            {
                CreateAnimation();
                animation.End();
            }

            protected override void OnDraw(Canvas canvas)
            {
                canvas.Save();
                canvas.Translate(ball.X, ball.Y);
                ball.Shape.Draw(canvas);
                canvas.Restore();
            }

            private void CreateAnimation()
            {
                if (animation == null)
                {
                    var yAnim = ObjectAnimator.OfFloat(ball, "y", ball.Y, Height - 50f);
                    yAnim.SetDuration(1000);
                    yAnim.RepeatCount = 0;
                    yAnim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                    yAnim.SetInterpolator(new AccelerateInterpolator(2f));
                    yAnim.Update += delegate { Invalidate(); };
                    yAnim.AnimationStart += delegate
                    {
                        activity.startTextAnimator.SetTextColor(On);
                        if (endImmediately)
                        {
                            animation.End();
                        }
                    };
                    yAnim.AnimationEnd += delegate
                    {
                        activity.endTextAnimator.SetTextColor(On);
                    };
                    yAnim.AnimationCancel += delegate
                    {
                        activity.cancelTextAnimator.SetTextColor(On);
                    };
                    yAnim.AnimationRepeat += delegate
                    {
                        activity.repeatTextAnimator.SetTextColor(On);
                    };

                    var xAnim = ObjectAnimator.OfFloat(ball, "x", ball.X, ball.X + 300);
                    xAnim.SetDuration(1000);
                    xAnim.StartDelay = 0;
                    xAnim.RepeatCount = 0;
                    xAnim.RepeatMode = ValueAnimatorRepeatMode.Reverse;
                    xAnim.SetInterpolator(new AccelerateInterpolator(2f));

                    var alphaAnim = ObjectAnimator.OfFloat(ball, "alpha", 1.0f, 0.5f);
                    alphaAnim.SetDuration(1000);

                    animation = new AnimatorSet();
                    animation.PlayTogether(yAnim, xAnim, alphaAnim);
                    animation.AnimationStart += delegate
                    {
                        activity.startText.SetTextColor(On);
                        if (endImmediately)
                        {
                            animation.End();
                        }
                    };
                    animation.AnimationEnd += delegate
                    {
                        activity.endText.SetTextColor(On);
                    };
                    animation.AnimationCancel += delegate
                    {
                        activity.cancelText.SetTextColor(On);
                    };
                    animation.AnimationRepeat += delegate
                    {
                        activity.repeatText.SetTextColor(On);
                    };
                }
            }

            private ShapeHolder CreateBall(float x, float y)
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
                var paint = drawable.Paint; //new Paint(Paint.ANTI_ALIAS_FLAG);
                var gradient = new RadialGradient(37.5f, 12.5f, 50f, color, darkColor, Shader.TileMode.Clamp);
                paint.SetShader(gradient);
                shapeHolder.Paint = paint;
                return shapeHolder;
            }
        }
    }
}
