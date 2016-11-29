using System;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;

using Jazzy.Effects;

namespace Jazzy
{
    public class JazzyViewPager : ViewPager
    {
        private static readonly bool IsHoneycomb;

        private int oldPage;
        private bool outlineEnabled;

        static JazzyViewPager()
        {
            IsHoneycomb = Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb;
        }

        public JazzyViewPager(Context context)
            : this(context, null)
        {
        }

        public JazzyViewPager(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            SetClipChildren(false);

            PagingEnabled = true;
            AnimationDuration = 500;
            OutlineColor = Color.White;
            OutlineEnabled = false;
            TransitionEffect = JazzyEffects.Standard;

            // now style everything!
            var ta = context.ObtainStyledAttributes(attrs, Resource.Styleable.JazzyViewPager);
            OutlineEnabled = ta.GetBoolean(Resource.Styleable.JazzyViewPager_outlineEnabled, false);
            OutlineColor = ta.GetColor(Resource.Styleable.JazzyViewPager_outlineColor, Color.White);
            ta.Recycle();
        }

        public long AnimationDuration { get; set; }

        public IJazzyEffect TransitionEffect { get; set; }

        public bool PagingEnabled { get; set; }

        public bool OutlineEnabled
        {
            get { return outlineEnabled; }
            set
            {
                outlineEnabled = value;
                WrapWithOutlines();
            }
        }

        public Color OutlineColor { get; set; }

        public JazzyState State { get; private set; }

        public JazzyPagerAdapter JazzyPageAdapter
        {
            get { return Adapter as JazzyPagerAdapter; }
        }

        public override void AddView(View child)
        {
            base.AddView(WrapChild(child));
        }

        public override void AddView(View child, int index)
        {
            base.AddView(WrapChild(child), index);
        }

        public override void AddView(View child, ViewGroup.LayoutParams @params)
        {
            base.AddView(WrapChild(child), @params);
        }

        public override void AddView(View child, int width, int height)
        {
            base.AddView(WrapChild(child), width, height);
        }

        public override void AddView(View child, int index, ViewGroup.LayoutParams @params)
        {
            base.AddView(WrapChild(child), index, @params);
        }
        
        public override bool OnInterceptTouchEvent(MotionEvent arg0)
        {
            return PagingEnabled ? base.OnInterceptTouchEvent(arg0) : false;
        }

        private void WrapWithOutlines()
        {
            for (int i = 0; i < ChildCount; i++)
            {
                View v = GetChildAt(i);
                if (!(v is JazzyOutlineContainer))
                {
                    RemoveView(v);
                    base.AddView(WrapChild(v), i);
                }
            }
        }

        private View WrapChild(View child)
        {
            if (!OutlineEnabled || child is JazzyOutlineContainer)
            {
                return child;
            }

            var outline = new JazzyOutlineContainer(Context) { AnimationDuration = AnimationDuration };
            outline.LayoutParameters = GenerateDefaultLayoutParams();
            child.LayoutParameters = new JazzyOutlineContainer.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            outline.AddView(child);
            return outline;
        }
        
        private void ManageLayer(View v, bool enableHardware)
        {
            if (!IsHoneycomb)
            {
                return;
            }

            var layerType = enableHardware ? LayerType.Hardware : LayerType.None;
            if (layerType != v.LayerType)
            {
                v.SetLayerType(layerType, null);
            }
        }

        private void DisableHardwareLayer()
        {
            if (!IsHoneycomb)
            {
                return;
            }
            View v;
            for (int i = 0; i < ChildCount; i++)
            {
                v = GetChildAt(i);
                if (v.LayerType != LayerType.None)
                {
                    v.SetLayerType(LayerType.None, null);
                }
            }
        }

        private void AnimateOutline(View left, View right)
        {
            var l = left as JazzyOutlineContainer;
            var r = right as JazzyOutlineContainer;

            if (l == null || r == null)
            {
                return;
            }

            if (State != JazzyState.Idle)
            {
                if (l != null)
                {
                    ManageLayer(l, true);
                    l.OutlineAlpha = 1.0f;
                }
                if (right != null)
                {
                    ManageLayer(r, true);
                    r.OutlineAlpha = 1.0f;
                }
            }
            else
            {
                if (l != null)
                {
                    l.Start();
                }
                if (r != null)
                {
                    r.Start();
                }
            }
        }

        protected override void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            if (State == JazzyState.Idle && positionOffset > 0)
            {
                oldPage = CurrentItem;
                State = position == oldPage ? JazzyState.GoingRight : JazzyState.GoingLeft;
            }
            bool goingRight = position == oldPage;
            if (State == JazzyState.GoingRight && !goingRight)
            {
                State = JazzyState.GoingLeft;
            }
            else if (State == JazzyState.GoingLeft && goingRight)
            {
                State = JazzyState.GoingRight;
            }

            float effectOffset = IsSmall(positionOffset) ? 0 : positionOffset;

            var adapter = JazzyPageAdapter;
            if (adapter != null)
            {
                var leftView = adapter.FindViewFromObject(position);
                var rightView = adapter.FindViewFromObject(position + 1);

                if (OutlineEnabled)
                {
                    AnimateOutline(leftView, rightView);
                }

                if (State != JazzyState.Idle)
                {
                    if (leftView != null)
                    {
                        ManageLayer(leftView, true);
                    }
                    if (rightView != null)
                    {
                        ManageLayer(rightView, true);
                    }
                }

                if (TransitionEffect != null)
                {
                    TransitionEffect.Animate(this, leftView, rightView, positionOffset, positionOffsetPixels);
                }
            }

            base.OnPageScrolled(position, positionOffset, positionOffsetPixels);

            if (effectOffset == 0)
            {
                DisableHardwareLayer();
                State = JazzyState.Idle;
            }
        }

        private bool IsSmall(float positionOffset)
        {
            return Math.Abs(positionOffset) < 0.0001;
        }
    }
}
