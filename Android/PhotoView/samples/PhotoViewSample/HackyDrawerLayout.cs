using Android.Content;
using Android.Util;
using Android.Views;
using AndroidX.DrawerLayout.Widget;

namespace PhotoViewSample
{
    public class HackyDrawerLayout : DrawerLayout
    {
        public HackyDrawerLayout(Context context)
            : base(context)
        {
        }

        public HackyDrawerLayout(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            // Hacky fix for Issue #4 and
            // http://code.google.com/p/android/issues/detail?id=18990
            // ScaleGestureDetector seems to mess up the touch events, which means that
            // ViewGroups which make use of onInterceptTouchEvent throw a lot of
            // IllegalArgumentException: pointerIndex out of range.
            //
            // There's not much I can do in my code for now, but we can mask the result by
            // just catching the problem and ignoring it.
            try
            {
                return base.OnInterceptTouchEvent(ev);
            }
            catch (Java.Lang.IllegalArgumentException ex)
            {
                ex.PrintStackTrace();
                return false;
            }
        }
    }
}
