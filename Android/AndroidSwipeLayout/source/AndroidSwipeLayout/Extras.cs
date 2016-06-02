using System;
using Android.Runtime;

namespace AndroidSwipeLayout.Adapters
{
	// manually handle abstract override

	abstract partial class RecyclerSwipeAdapter
	{
		static Delegate cb_onCreateViewHolder_Landroid_view_ViewGroup_I;
		#pragma warning disable 0169
		static Delegate GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler ()
		{
			if (cb_onCreateViewHolder_Landroid_view_ViewGroup_I == null)
				cb_onCreateViewHolder_Landroid_view_ViewGroup_I = JNINativeWrapper.CreateDelegate ((Func<IntPtr, IntPtr, IntPtr, int, IntPtr>)n_OnCreateViewHolder_Landroid_view_ViewGroup_I);
			return cb_onCreateViewHolder_Landroid_view_ViewGroup_I;
		}

		static IntPtr n_OnCreateViewHolder_Landroid_view_ViewGroup_I (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1)
		{
			global::AndroidSwipeLayout.Adapters.RecyclerSwipeAdapter __this = global::Java.Lang.Object.GetObject<global::AndroidSwipeLayout.Adapters.RecyclerSwipeAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Views.ViewGroup p0 = global::Java.Lang.Object.GetObject<global::Android.Views.ViewGroup> (native_p0, JniHandleOwnership.DoNotTransfer);
			IntPtr __ret = JNIEnv.ToLocalJniHandle (__this.OnCreateViewHolder (p0, p1));
			return __ret;
		}
		#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.daimajia.swipe.adapters']/class[@name='RecyclerSwipeAdapter']/method[@name='onCreateViewHolder' and count(parameter)=2 and parameter[1][@type='android.view.ViewGroup'] and parameter[2][@type='int']]"
		[Register ("onCreateViewHolder", "(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;", "GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler")]
		public abstract override global::Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder (global::Android.Views.ViewGroup parent, int position);

		static Delegate cb_onBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I;
		#pragma warning disable 0169
		static Delegate GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler ()
		{
			if (cb_onBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I == null)
				cb_onBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I = JNINativeWrapper.CreateDelegate ((Action<IntPtr, IntPtr, IntPtr, int>)n_OnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I);
			return cb_onBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I;
		}

		static void n_OnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_I (IntPtr jnienv, IntPtr native__this, IntPtr native_p0, int p1)
		{
			global::AndroidSwipeLayout.Adapters.RecyclerSwipeAdapter __this = global::Java.Lang.Object.GetObject<global::AndroidSwipeLayout.Adapters.RecyclerSwipeAdapter> (jnienv, native__this, JniHandleOwnership.DoNotTransfer);
			global::Android.Support.V7.Widget.RecyclerView.ViewHolder p0 = global::Java.Lang.Object.GetObject<global::Android.Support.V7.Widget.RecyclerView.ViewHolder> (native_p0, JniHandleOwnership.DoNotTransfer);
			__this.OnBindViewHolder (p0, p1);
		}
		#pragma warning restore 0169

		// Metadata.xml XPath method reference: path="/api/package[@name='com.daimajia.swipe.adapters']/class[@name='RecyclerSwipeAdapter']/method[@name='onBindViewHolder' and count(parameter)=2 and parameter[1][@type='android.support.v7.widget.RecyclerView.ViewHolder'] and parameter[2][@type='int']]"
		[Register ("onBindViewHolder", "(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V", "GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler")]
		public abstract override void OnBindViewHolder (global::Android.Support.V7.Widget.RecyclerView.ViewHolder viewHolder, int position);
	}
}

namespace AndroidSwipeLayout
{
	partial class SwipeLayout
	{
		public IOnRevealListener AddRevealListener (int childId, EventHandler<SwipeLayout.RevealEventArgs> listenerEvent)
		{
			var listener = new IOnRevealListenerImplementor (this);
			listener.Handler += listenerEvent;
			AddRevealListener (childId, listener);
			return listener;
		}

		public IOnRevealListener AddRevealListener (int[] childIds, EventHandler<SwipeLayout.RevealEventArgs> listenerEvent)
		{
			var listener = new IOnRevealListenerImplementor (this);
			listener.Handler += listenerEvent;
			AddRevealListener (childIds, listener);
			return listener;
		}
	}
}
