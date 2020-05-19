using Android.Content;
using Android.Util;
using Android.Widget;

namespace PicassoSample
{
    /// <summary>
    /// An image view which always remains square with respect to its width.
    /// </summary>
    public class SquaredImageView : ImageView
    {
        public SquaredImageView(Context context)
            : base(context)
        {
        }

        public SquaredImageView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);

            SetMeasuredDimension(MeasuredWidth, MeasuredWidth);
        }
    }
}
