using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Genetics.Attributes;
using Genetics;
using NineOldAndroids.Animation;

using StickyListHeaders;

namespace StickyListHeadersSample
{
    [Activity(Label = "@string/app_name", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class ExpandableListActivity : AppCompatActivity
    {
        [Splice(Resource.Id.list)]
        private ExpandableStickyListHeadersListView listView;

        private StickyAdapter adapter;
        private Dictionary<IntPtr, int> originalHeightPool = new Dictionary<IntPtr, int>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ExpandableLayout);

            // inject views
            Geneticist.Splice(this);

            // handle header clicks
            listView.HeaderClick += (sender, e) =>
            {
                if (listView.IsHeaderCollapsed(e.HeaderId))
                {
                    listView.Expand(e.HeaderId);
                }
                else
                {
                    listView.Collapse(e.HeaderId);
                }
            };

            //custom expand/collapse animation
            listView.SetAnimExecutor((target, animType) =>
            {
                if (ExpandableStickyListHeadersListView.AnimationExpand == animType &&
                    target.Visibility == ViewStates.Visible)
                {
                    return;
                }
                if (ExpandableStickyListHeadersListView.AnimationCollapse == animType && 
                    target.Visibility != ViewStates.Visible)
                {
                    return;
                }
                if (!originalHeightPool.ContainsKey(target.Handle))
                {
                    originalHeightPool.Add(target.Handle, target.Height);
                }

                var viewHeight = originalHeightPool[target.Handle];
                var animStartY = animType == ExpandableStickyListHeadersListView.AnimationExpand ? 0f : viewHeight;
                var animEndY = animType == ExpandableStickyListHeadersListView.AnimationExpand ? viewHeight : 0f;
                target.Visibility = ViewStates.Visible;
                var lp = target.LayoutParameters;
                var animator = ValueAnimator.OfFloat(animStartY, animEndY);
                animator.SetDuration(200);
                animator.AnimationEnd += delegate
                {
                    if (animType == ExpandableStickyListHeadersListView.AnimationExpand)
                    {
                        target.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        target.Visibility = ViewStates.Gone;
                    }
                    target.LayoutParameters.Height = viewHeight;
                };
                animator.Update += delegate
                {
                    lp.Height = (int)animator.AnimatedValue;
                    target.LayoutParameters = lp;
                    target.RequestLayout();
                };
                animator.Start();
            });

            // set up data
            adapter = new StickyAdapter(this);
            listView.Adapter = adapter;
        }
    }
}
