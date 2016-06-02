using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace StickyListHeadersSample
{
    public class UnderlineTextView : TextView
    {
        private readonly Paint paint = new Paint();
        private int underlineHeight = 0;

        public UnderlineTextView(Context context)
            : base(context)
        {
            Init();
        }

        public UnderlineTextView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init();
        }

        public UnderlineTextView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Init();
        }

        public UnderlineTextView(Context context, IAttributeSet attrs, int defStyle, int defStyleRes)
            : base(context, attrs, defStyle, defStyleRes)
        {
            Init();
        }

        protected UnderlineTextView(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
            Init();
        }

        private void Init()
        {
            underlineHeight = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics);
        }

        public override void SetPadding(int left, int top, int right, int bottom)
        {
            base.SetPadding(left, top, right, bottom + underlineHeight);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // Draw the underline the same color as the text
            paint.Color = new Color(TextColors.DefaultColor);
            canvas.DrawRect(0, Height - underlineHeight, Width, Height, paint);
        }
    }
}
