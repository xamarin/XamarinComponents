using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using ImageViews.Photo;
using Android.Support.V4.View;
using Android.Runtime;
using Android.Util;

namespace PhotoViewSample
{
    public class HackyViewPager : ViewPager
    {
        public HackyViewPager(Context context)
            : base(context)
        {
            IsLocked = false;
        }

        public HackyViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            IsLocked = false;
        }

        public bool IsLocked { get; set; }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (!IsLocked)
            {
                return base.OnInterceptTouchEvent(ev);
            }
            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return !IsLocked && base.OnTouchEvent(e);
        }

        public void ToggleLock()
        {
            IsLocked = !IsLocked;
        }
    }
}
