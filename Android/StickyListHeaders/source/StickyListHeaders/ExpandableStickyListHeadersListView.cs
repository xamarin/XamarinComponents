using System;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Java.Interop;

namespace StickyListHeaders
{
    partial class ExpandableStickyListHeadersListView
    {
        public void SetAnimExecutor(Action<View, int> executor)
        {
            SetAnimExecutor(new AnimExecutor(executor));
        }

        private class AnimExecutor : Java.Lang.Object, IAnimationExecutor
        {
            private readonly Action<View, int> animExecutor;

            public AnimExecutor(Action<View, int> executor)
            {
                animExecutor = executor;
            }

            public void ExecuteAnim(global::Android.Views.View target, int animType)
            {
                if (animExecutor != null)
                {
                    animExecutor(target, animType);
                }
            }
        }
    }
}
