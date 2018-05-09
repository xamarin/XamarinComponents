using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;

using VKontakte.API;
using VKontakte.Payments;

namespace VKontakte
{
	partial class VKSdk
	{
		public static bool OnActivityResult (int requestCode, Result resultCode, Intent data, VKCallback<VKAccessToken> vkCallback)
		{
			return OnActivityResult (requestCode, resultCode, data, (IVKCallback)vkCallback);
		}

		public static bool OnActivityResult (int requestCode, Result resultCode, Intent data, Action<VKAccessToken> onResult, Action<VKError> onError = null)
		{
			return OnActivityResult (requestCode, resultCode, data, new ActionCallback<VKAccessToken> (onResult, onError));
		}

		public static Task<VKAccessToken> OnActivityResultAsync (int requestCode, Result resultCode, Intent data)
		{
			bool result;
			return OnActivityResultAsync (requestCode, resultCode, data, out result);
		}

		public static Task<VKAccessToken> OnActivityResultAsync (int requestCode, Result resultCode, Intent data, out bool result)
		{
			var tcs = new TaskCompletionSource<VKAccessToken> ();
			result = OnActivityResult (requestCode, resultCode, data, response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)));
			return tcs.Task;
		}

		public static void RequestUserState (Context ctx, Action<bool> onUserState)
		{
			RequestUserState (ctx, new ActionPaymentsCallback (onUserState));
		}

		public static Task<bool> RequestUserStateAsync (Context ctx)
		{
			var tcs = new TaskCompletionSource<bool> ();
			RequestUserState (ctx, userIsVk => tcs.SetResult (userIsVk));
			return tcs.Task;
		}

		public static bool WakeUpSession (Context context, VKCallback<LoginState> loginStateCallback)
		{
			return WakeUpSession (context, (IVKCallback)loginStateCallback);
		}

		public static bool WakeUpSession (Context context, Action<LoginState> onResult, Action<VKError> onError = null)
		{
			return WakeUpSession (context, new ActionCallback<LoginState> (onResult, onError));
		}
	}

	internal class ActionCallback<T> : VKCallback<T>
		where T : class, IJavaObject
	{
		private readonly Action<T> onResult;
		private readonly Action<VKError> onError;

		public ActionCallback (Action<T> onResult, Action<VKError> onError)
		{
			this.onResult = onResult;
			this.onError = onError;
		}

		public override void OnResult (T result)
		{
			var handler = onResult;
			if (handler != null) {
				handler (result);
			}
		}

		public override void OnError (VKError error)
		{
			var handler = onError;
			if (handler != null) {
				handler (error);
			}
		}
	}
}
