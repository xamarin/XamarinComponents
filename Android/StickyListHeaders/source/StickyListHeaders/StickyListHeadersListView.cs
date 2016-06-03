using System;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Java.Interop;

namespace StickyListHeaders
{
    partial class StickyListHeadersListView
    {
        private WeakReference setOnItemClickListener;
        private WeakReference setOnItemLongClickListener;
        private WeakReference setOnScrollListener;

        public event EventHandler<AdapterView.ItemClickEventArgs> ItemClick
        {
            add
            {
                EventHelper.AddEventHandler<AdapterView.IOnItemClickListener, IOnItemClickListenerImplementor>(
                    ref setOnItemClickListener,
                    () => new IOnItemClickListenerImplementor(this),
                    SetOnItemClickListener,
                    h => h.Handler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<AdapterView.IOnItemClickListener, IOnItemClickListenerImplementor>(
                    ref setOnItemClickListener,
                   IOnItemClickListenerImplementor.IsEmpty,
                   v => SetOnItemClickListener(null),
                   h => h.Handler -= value);
            }
        }

        public event EventHandler<AdapterView.ItemLongClickEventArgs> ItemLongClick
        {
            add
            {
                EventHelper.AddEventHandler<AdapterView.IOnItemLongClickListener, IOnItemLongClickListenerImplementor>(
                    ref setOnItemLongClickListener,
                    () => new IOnItemLongClickListenerImplementor(this),
                    SetOnItemLongClickListener,
                    h => h.Handler += value);
            }
            remove
            {
                EventHelper.RemoveEventHandler<AdapterView.IOnItemLongClickListener, IOnItemLongClickListenerImplementor>(
                    ref setOnItemLongClickListener,
                   IOnItemLongClickListenerImplementor.IsEmpty,
                   v => SetOnItemLongClickListener(null),
                   h => h.Handler -= value);
            }
        }

        public event EventHandler<AbsListView.ScrollEventArgs> Scroll
        {
            add
            {
                EventHelper.AddEventHandler<AbsListView.IOnScrollListener, IOnScrollListenerImplementor>(
                    ref setOnScrollListener,
                    () => new IOnScrollListenerImplementor(this),
                    SetOnScrollListener,
                    h => h.OnScrollHandler = (EventHandler<AbsListView.ScrollEventArgs>)Delegate.Combine(h.OnScrollHandler, value));
            }
            remove
            {
                EventHelper.RemoveEventHandler<AbsListView.IOnScrollListener, IOnScrollListenerImplementor>(
                    ref setOnScrollListener,
                    IOnScrollListenerImplementor.IsEmpty,
                    v => SetOnScrollListener(null),
                    h => h.OnScrollHandler = (EventHandler<AbsListView.ScrollEventArgs>)Delegate.Remove(h.OnScrollHandler, value));
            }
        }

        public event EventHandler<AbsListView.ScrollStateChangedEventArgs> ScrollStateChanged
        {
            add
            {
                EventHelper.AddEventHandler<AbsListView.IOnScrollListener, IOnScrollListenerImplementor>(
                    ref setOnScrollListener,
                    () => new IOnScrollListenerImplementor(this),
                    SetOnScrollListener,
                    h => h.OnScrollStateChangedHandler = (EventHandler<AbsListView.ScrollStateChangedEventArgs>)Delegate.Combine(h.OnScrollStateChangedHandler, value));
            }
            remove
            {
                EventHelper.RemoveEventHandler<AbsListView.IOnScrollListener, IOnScrollListenerImplementor>(
                    ref setOnScrollListener,
                    IOnScrollListenerImplementor.IsEmpty,
                    v => SetOnScrollListener(null),
                    h => h.OnScrollStateChangedHandler = (EventHandler<AbsListView.ScrollStateChangedEventArgs>)Delegate.Remove(h.OnScrollStateChangedHandler, value));
            }
        }

        private class IOnItemClickListenerImplementor : Java.Lang.Object, AdapterView.IOnItemClickListener
        {
            private object sender;

            public EventHandler<AdapterView.ItemClickEventArgs> Handler;

            public IOnItemClickListenerImplementor(object sender)
            {
                this.sender = sender;
            }

            public static bool IsEmpty(IOnItemClickListenerImplementor value)
            {
                return value.Handler == null;
            }

            public void OnItemClick(AdapterView parent, View view, int position, long id)
            {
                var handler = Handler;
                if (handler != null)
                {
                    handler.Invoke(sender, new AdapterView.ItemClickEventArgs(parent, view, position, id));
                }
            }
        }

        private class IOnItemLongClickListenerImplementor : Java.Lang.Object, AdapterView.IOnItemLongClickListener
        {
            private object sender;

            public EventHandler<AdapterView.ItemLongClickEventArgs> Handler;

            public IOnItemLongClickListenerImplementor(object sender)
            {
                this.sender = sender;
            }

            public static bool IsEmpty(IOnItemLongClickListenerImplementor value)
            {
                return value.Handler == null;
            }

            public bool OnItemLongClick(AdapterView parent, View view, int position, long id)
            {
                var handler = Handler;
                if (handler == null)
                {
                    return false;
                }

                var itemLongClickEventArg = new AdapterView.ItemLongClickEventArgs(true, parent, view, position, id);
                handler.Invoke(sender, itemLongClickEventArg);
                return itemLongClickEventArg.Handled;
            }
        }

        private class IOnScrollListenerImplementor : Java.Lang.Object, AbsListView.IOnScrollListener
        {
            private object sender;

            public EventHandler<AbsListView.ScrollEventArgs> OnScrollHandler;

            public EventHandler<AbsListView.ScrollStateChangedEventArgs> OnScrollStateChangedHandler;

            public IOnScrollListenerImplementor(object sender) : base(JNIEnv.StartCreateInstance("mono/android/widget/AbsListView_OnScrollListenerImplementor", "()V", new JValue[0]), JniHandleOwnership.TransferLocalRef)
            {
                JNIEnv.FinishCreateInstance(base.Handle, "()V", new JValue[0]);
                this.sender = sender;
            }

            internal static bool IsEmpty(IOnScrollListenerImplementor value)
            {
                return (value.OnScrollHandler != null ? false : value.OnScrollStateChangedHandler == null);
            }

            public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
            {
                EventHandler<AbsListView.ScrollEventArgs> onScrollHandler = this.OnScrollHandler;
                if (onScrollHandler != null)
                {
                    onScrollHandler.Invoke(this.sender, new AbsListView.ScrollEventArgs(view, firstVisibleItem, visibleItemCount, totalItemCount));
                }
            }

            public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
            {
                EventHandler<AbsListView.ScrollStateChangedEventArgs> onScrollStateChangedHandler = this.OnScrollStateChangedHandler;
                if (onScrollStateChangedHandler != null)
                {
                    onScrollStateChangedHandler.Invoke(this.sender, new AbsListView.ScrollStateChangedEventArgs(view, scrollState));
                }
            }
        }
    }
}
