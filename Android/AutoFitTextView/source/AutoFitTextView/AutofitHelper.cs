using System;
using Java.Interop;

namespace Grantland.Widget
{
	partial class AutofitHelper
	{
		private WeakReference textSizeChange;

		public event EventHandler<TextSizeChangeEventArgs> TextSizeChange {
			add {
				EventHelper.AddEventHandler<IOnTextSizeChangeListener, IOnTextSizeChangeListenerImplementor> (
					ref textSizeChange, 
					new Func<IOnTextSizeChangeListenerImplementor> (() => new IOnTextSizeChangeListenerImplementor (this)), 
					new Action<IOnTextSizeChangeListener> (listener => AddOnTextSizeChangeListener (listener)),
					h => h.Handler = (EventHandler<TextSizeChangeEventArgs>)Delegate.Combine (h.Handler, value));
			}
			remove {
				EventHelper.RemoveEventHandler<IOnTextSizeChangeListener, IOnTextSizeChangeListenerImplementor> (
					ref textSizeChange, 
					IOnTextSizeChangeListenerImplementor.__IsEmpty, 
					new Action<IOnTextSizeChangeListener> (listener => RemoveOnTextSizeChangeListener (listener)), 
					h => h.Handler = (EventHandler<TextSizeChangeEventArgs>)Delegate.Remove (h.Handler, value));
			}
		}

		public AutofitHelper AddOnTextSizeChangeListener (EventHandler<TextSizeChangeEventArgs> handler)
		{
			return AddOnTextSizeChangeListener (new IOnTextSizeChangeListenerImplementor (this) {
				Handler = handler
			});
		}
	}
}
