using System.Collections.Generic;
using Android.Support.V4.View;
using Android.Views;

namespace Jazzy
{
    public abstract class JazzyPagerAdapter : PagerAdapter
    {
        private readonly Dictionary<int, Java.Lang.Object> objects;
        private readonly JazzyViewPager jazzyViewPager;

        public JazzyPagerAdapter(JazzyViewPager jazzy)
        {
            objects = new Dictionary<int, Java.Lang.Object>();
            jazzyViewPager = jazzy;
        }

        public JazzyViewPager JazzyViewPager
        {
            get { return jazzyViewPager; }
        }
        
        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            var v = view as JazzyOutlineContainer;
            if (v != null)
            {
                return v.GetChildAt(0) == obj;
            }
            else
            {
                return view == obj;
            }
        }

        public virtual void SetObjectForPosition(Java.Lang.Object obj, int position)
        {
            objects[position] = obj;
        }

        public virtual View FindViewFromObject(int position)
        {
            if (!objects.ContainsKey(position))
            {
                return null;
            }
            var o = objects[position];
            var a = JazzyViewPager.Adapter;
            for (int i = 0; i < JazzyViewPager.ChildCount; i++)
            {
                var v = JazzyViewPager.GetChildAt(i);
                if (a.IsViewFromObject(v, o))
                {
                    return v;
                }
            }
            return null;
        }
    }
}
