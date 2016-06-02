using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using NineOldAndroids.Animation;
using System;
using System.Collections.Generic;

namespace NineOldAndroidsSample.DroidFlakes
{
    public class FlakeView : View
    {
        private Bitmap droid; // The bitmap that all flakes use
        private int numFlakes = 0; // Current number of flakes
        private List<Flake> flakes = new List<Flake>(); // List of current flakes

        // Animator used to drive all separate flake animations. Rather than have potentially
        // hundreds of separate animators, we just use one and then update all flakes for each
        // frame of that single animation.
        private ValueAnimator animator = ValueAnimator.OfFloat(0, 1);
        private long startTime, prevTime; // Used to track elapsed time for animations and fps
        private int frames = 0; // Used to track frames per second
        private Paint textPaint; // Used for rendering fps text
        private float fps = 0; // frames per second
        private Matrix m = new Matrix(); // Matrix used to translate/rotate each flake during rendering
        private string fpsString = "";
        private string numFlakesString = "";

        /// <summary>
        /// Constructor. Create objects used throughout the life of the View: the Paint and
        /// the animator
        /// </summary>
        public FlakeView(Context context)
            : base(context)
        {
            droid = BitmapFactory.DecodeResource(Resources, Resource.Drawable.droid);
            textPaint = new Paint(PaintFlags.AntiAlias);
            textPaint.Color = Color.White;
            textPaint.TextSize = 24;

            // This listener is where the action is for the flake animations. Every frame of the
            // animation, we calculate the elapsed time and update every flake's position and rotation
            // according to its speed.
            animator.Update += (sender, e) =>
            {
                var nowTime = DateTime.UtcNow.Ticks;
                var secs = (float)TimeSpan.FromTicks(nowTime - prevTime).TotalSeconds;
                prevTime = nowTime;
                for (var i = 0; i < numFlakes; ++i)
                {
                    var flake = flakes[i];
                    flake.Y += (flake.Speed * secs);
                    if (flake.Y > Height)
                    {
                        // If a flake falls off the bottom, send it back to the top
                        flake.Y = 0 - flake.Height;
                    }
                    flake.Rotation = flake.Rotation + (flake.RotationSpeed * secs);
                }
                // Force a redraw to see the flakes in their new positions and orientations
                Invalidate();
            };
            animator.RepeatCount = ValueAnimator.Infinite;
            animator.SetDuration(3000);
        }

        public int NumFlakes
        {
            get { return numFlakes; }
            private set
            {
                numFlakes = value;
                numFlakesString = "numFlakes: " + numFlakes;
            }
        }


        /// <summary>
        /// Add the specified number of droidflakes.
        /// </summary>
        public void AddFlakes(int quantity)
        {
            for (int i = 0; i < quantity; ++i)
            {
                flakes.Add(Flake.CreateFlake(Width, droid));
            }
            NumFlakes = numFlakes + quantity;
        }

        /// <summary>
        /// Subtract the specified number of droidflakes. We just take them off the end of the
        /// list, leaving the others unchanged.
        /// </summary>
        public void SubtractFlakes(int quantity)
        {
            for (var i = 0; i < quantity; ++i)
            {
                var index = numFlakes - i - 1;
                flakes.RemoveAt(index);
            }
            NumFlakes = numFlakes - quantity;
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            // Reset list of droidflakes, then restart it with 8 flakes
            flakes.Clear();
            numFlakes = 0;
            AddFlakes(8);
            // Cancel animator in case it was already running
            animator.Cancel();
            // Set up fps tracking and start the animation
            startTime = DateTime.UtcNow.Ticks;
            prevTime = startTime;
            frames = 0;
            animator.Start();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // For each flake: back-translate by half its size (this allows it to rotate around its center),
            // rotate by its current rotation, translate by its location, then draw its bitmap
            for (var i = 0; i < numFlakes; ++i)
            {
                var flake = flakes[i];
                m.SetTranslate(-flake.Width / 2, -flake.Height / 2);
                m.PostRotate(flake.Rotation);
                m.PostTranslate(flake.Width / 2 + flake.X, flake.Height / 2 + flake.Y);
                canvas.DrawBitmap(flake.Bitmap, m, null);
            }
            // fps counter: count how many frames we draw and once a second calculate the
            // frames per second
            ++frames;
            var nowTime = DateTime.UtcNow.Ticks;
            var deltaTime = nowTime - startTime;
            if (deltaTime > 1000)
            {
                var secs = (float)TimeSpan.FromTicks(deltaTime).TotalSeconds;
                fps = frames / secs;
                fpsString = "fps: " + fps;
                startTime = nowTime;
                frames = 0;
            }
            canvas.DrawText(numFlakesString, Width - 200, Height - 50, textPaint);
            canvas.DrawText(fpsString, Width - 200, Height - 80, textPaint);
        }

        public virtual void Pause()
        {
            // Make sure the animator's not spinning in the background when the activity is paused.
            animator.Cancel();
        }

        public virtual void Resume()
        {
            animator.Start();
        }
    }
}
