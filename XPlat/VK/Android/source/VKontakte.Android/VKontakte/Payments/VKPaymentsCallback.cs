using System;
using System.Threading.Tasks;
using Android.Content;

namespace VKontakte.Payments
{
	partial class VKPaymentsCallback
	{
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
	}

	internal class ActionPaymentsCallback : VKPaymentsCallback
	{
		private readonly Action<bool> onUserState;

		public ActionPaymentsCallback (Action<bool> onUserState)
		{
			this.onUserState = onUserState;
		}

		public override void OnUserState (bool userIsVk)
		{
			var handler = onUserState;
			if (handler != null) {
				handler (userIsVk);
			}
		}
	}
}
