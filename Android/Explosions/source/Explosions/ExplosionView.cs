using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Views.Animations;

namespace Explosions
{
    /// <summary>
    /// Represents the overlay that controls <see cref="View" /> explosions.
    /// </summary>
    public class ExplosionView : View
    {
        private readonly IList<ExplosionAnimator> explosions = new List<ExplosionAnimator>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplosionView"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ExplosionView(Context context)
            : base(context)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplosionView"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attributes.</param>
        public ExplosionView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExplosionView"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attributes.</param>
        /// <param name="defStyleAttr">The default style attribute.</param>
        public ExplosionView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        private void Init()
        {
            ExpansionLimit = Utils.Dp2Px(32);
        }

        /// <summary>
        /// Gets or sets the expansion limit when exploding.
        /// </summary>
        /// <value>The expansion limit.</value>
        public int ExpansionLimit { get; set; }

        /// <summary>
        /// Called when the view is being drawn.
        /// </summary>
        /// <param name="canvas">The canvas on which the background will be drawn.</param>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            foreach (ExplosionAnimator explosion in explosions.ToArray())
            {
                explosion.Draw(canvas);
            }
        }

        /// <summary>
        /// Explodes the specified bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="bounds">The bounds.</param>
        /// <param name="startDelay">The start delay.</param>
        /// <param name="duration">The duration.</param>
        public void Explode(Bitmap bitmap, Rect bounds, long startDelay, long duration)
        {
            var explosion = new ExplosionAnimator(this, bitmap, bounds);
            explosion.AnimationEnd += delegate
            {
                explosions.Remove(explosion);
            };
            explosion.StartDelay = startDelay;
            explosion.Duration = duration;
            explosions.Add(explosion);
            explosion.Start();
        }

        /// <summary>
        /// Explodes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void Explode(View view)
        {
            Explode(view, ExplosionAnimator.DefaultDuration, null);
        }

        /// <summary>
        /// Explodes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="duration">The duration of the explosion.</param>
        public void Explode(View view, long duration)
        {
            Explode(view, duration, null);
        }

        /// <summary>
        /// Explodes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="onHidden">The callback that is invoked when the view is no longer visible.</param>
        public void Explode(View view, Action onHidden)
        {
            Explode(view, ExplosionAnimator.DefaultDuration, onHidden);
        }

        /// <summary>
        /// Explodes the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="duration">The duration of the explosion.</param>
        /// <param name="onHidden">The callback that is invoked when the view is no longer visible.</param>
        public void Explode(View view, long duration, Action onHidden)
        {
            const float vibrateAmount = 0.05f;
            var preDuration = (long)(duration * 0.25f);
            var explosionDuration = (long)(duration * 0.75f);
            var startDelay = (long)(duration * 0.15f);

            // get the bounds
            var r = new Rect();
            view.GetGlobalVisibleRect(r);
            var location = new int[2];
            GetLocationOnScreen(location);
            r.Offset(-location[0], -location[1]);
            r.Inset(-ExpansionLimit, -ExpansionLimit);

            var set = new AnimationSet(true);

            // vibrate
            var vibrate = new VibrateAnimation(view.Width * vibrateAmount, view.Height * vibrateAmount);
            vibrate.Duration = preDuration;
            set.AddAnimation(vibrate);

            // implode
            var alpha = new AlphaAnimation(1.0f, 0.0f);
            alpha.Duration = preDuration;
            alpha.StartOffset = startDelay;
            set.AddAnimation(alpha);
            var scale = new ScaleAnimation(
                1.0f, 0.0f,
                1.0f, 0.0f,
                Dimension.RelativeToSelf, 0.5f,
                Dimension.RelativeToSelf, 0.5f);
            scale.Duration = preDuration;
            scale.StartOffset = startDelay;
            set.AddAnimation(scale);

            // apply on complete
            set.AnimationEnd += delegate
            {
                view.Visibility = ViewStates.Invisible;
                if (onHidden != null)
                {
                    onHidden();
                }
            };
            view.StartAnimation(set);

            // explode
            Explode(Utils.CreateBitmapFromView(view), r, startDelay, explosionDuration);
        }

        /// <summary>
        /// Resets the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        public void Reset(View view)
        {
            view.Visibility = ViewStates.Visible;
        }

        /// <summary>
        /// Attaches the explosion overlay to the specified activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>The ExplosionView that was attached.</returns>
        public static ExplosionView Attach(Activity activity)
        {
            var rootView = (ViewGroup)activity.Window.DecorView;
            var explosionField = new ExplosionView(activity);
            var layoutParams = new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent);
            rootView.AddView(explosionField, layoutParams);
            return explosionField;
        }
    }
}
