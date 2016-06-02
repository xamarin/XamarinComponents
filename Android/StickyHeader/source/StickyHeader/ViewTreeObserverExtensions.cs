using System;
using Android.Views;

namespace StickyHeader
{
	public static class ViewTreeObserverExtensions
	{
		public static void AddOnGlobalLayoutAction(this ViewTreeObserver observer, Action callback)
		{
			GlobalLayoutSingleFireListener listener = null;
			listener = new GlobalLayoutSingleFireListener(() =>
			{
				if (observer.IsAlive)
				{
					callback();
				}
			});
			observer.AddOnGlobalLayoutListener(listener);
		}

		public static void AddOnGlobalLayoutSingleFire(this ViewTreeObserver observer, Action callback)
		{
			GlobalLayoutSingleFireListener listener = null;
			listener = new GlobalLayoutSingleFireListener(() =>
			{
				if (observer.IsAlive)
				{
					observer.RemoveGlobalOnLayoutListener(listener);
					callback();
				}
			});
			observer.AddOnGlobalLayoutListener(listener);
		}

		private class GlobalLayoutSingleFireListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
		{
			private Action callback;

			public GlobalLayoutSingleFireListener(Action callback)
			{
				this.callback = callback;
			}

			public void OnGlobalLayout()
			{
				callback();
			}
		}
	}
}