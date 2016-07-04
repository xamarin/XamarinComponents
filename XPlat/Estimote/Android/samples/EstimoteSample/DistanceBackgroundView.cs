using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;

namespace Estimotes.Droid
{
    public class DistanceBackgroundView : View
    {
        Drawable _drawable;

        public DistanceBackgroundView(Context context) :
            base(context)
        {
            Initialize(context);
        }

        public DistanceBackgroundView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public DistanceBackgroundView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        void Initialize(Context context)
        {
            _drawable = context.Resources.GetDrawable(Resource.Drawable.bg_distance);
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            int width = _drawable.IntrinsicWidth * canvas.Height / _drawable.IntrinsicHeight;
            int deltaX = (width - canvas.Width) / 2;
            _drawable.SetBounds(-deltaX, 0, width - deltaX, canvas.Height);
            _drawable.Draw(canvas);
        }
    }
}
