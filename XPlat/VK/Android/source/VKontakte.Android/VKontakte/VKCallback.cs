using Android.Runtime;

using VKontakte.API;

namespace VKontakte
{
	public abstract class VKCallback<T> : Java.Lang.Object, IVKCallback
		where T : class, IJavaObject
	{
		void IVKCallback.OnResult (Java.Lang.Object result)
		{
			OnResult (result.JavaCast<T> ());
		}

		public abstract void OnResult (T result);

		public abstract void OnError (VKError error);
	}
}
