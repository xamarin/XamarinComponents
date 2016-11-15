using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views.Animations;
using Android.Widget;

namespace Jazzy
{
    internal class JazzyOutlineContainer : FrameLayout, IAnimatable
    {
        private Paint outlinePaint;
        private long startTime;
        private const long FrameDuration = 1000 / 60;
        private readonly IInterpolator interpolator = new OutlineInterpolator();

        public JazzyOutlineContainer(Context context)
            : base(context)
        {
            Init();
        }

        public JazzyOutlineContainer(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init();
        }

        public JazzyOutlineContainer(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Init();
        }

        private void Init()
        {
            IsRunning = false;
            AnimationDuration = 500;
            OutlineAlpha = 1.0f;
            outlinePaint = new Paint();
            outlinePaint.AntiAlias = true;
            outlinePaint.StrokeWidth = Utils.Dp2Px(2);
            outlinePaint.Color = Resources.GetColor(Resource.Color.holo_blue);
            outlinePaint.SetStyle(Paint.Style.Stroke);
            var padding = Utils.Dp2Px(10);
            SetPadding(padding, padding, padding, padding);
        }

        public bool IsRunning { get; set; }

        public float OutlineAlpha { get; set; }

        public long AnimationDuration { get; set; }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;
            startTime = AnimationUtils.CurrentAnimationTimeMillis();
            Post(OnUpdate);
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            IsRunning = false;
        }

        protected override void DispatchDraw(Canvas canvas)
        {
            base.DispatchDraw(canvas);

            int offset = Utils.Dp2Px(5);
            var pager = Parent as JazzyViewPager;
            if (pager != null)
            {
                if (outlinePaint.Color != pager.OutlineColor)
                {
                    outlinePaint.Color = pager.OutlineColor;
                }
            }
            outlinePaint.Alpha = (int)(OutlineAlpha * 255);
            Rect rect = new Rect(offset, offset, MeasuredWidth - offset, MeasuredHeight - offset);
            canvas.DrawRect(rect, outlinePaint);
        }

        private void OnUpdate()
        {
            long now = AnimationUtils.CurrentAnimationTimeMillis();
            long duration = now - startTime;
            if (duration >= AnimationDuration)
            {
                OutlineAlpha = 0.0f;
                Invalidate();
                Stop();
                return;
            }
            else
            {
                OutlineAlpha = interpolator.GetInterpolation(1 - duration / (float)AnimationDuration);
                Invalidate();
            }
            PostDelayed(OnUpdate, FrameDuration);
        }

        private class OutlineInterpolator : Java.Lang.Object, IInterpolator
        {
            public float GetInterpolation(float t)
            {
                t -= 1.0f;
                return t * t * t + 1.0f;
            }
        }
    }
}
