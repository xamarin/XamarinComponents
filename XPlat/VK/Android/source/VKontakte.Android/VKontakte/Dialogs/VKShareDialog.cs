using System;

using VKontakte.API;

namespace VKontakte.Dialogs
{
	partial class VKShareDialog
	{
		public VKShareDialog SetShareDialogListener (Action<int> onComplete, Action onCancel, Action<VKError> onError = null)
		{
			return SetShareDialogListener (new ActionShareDialogListener (onComplete, onCancel, onError));
		}

		Android.App.Activity VKShareDialogDelegate.IDialogFragmentI.Activity => this.Activity;
	}

	internal class ActionShareDialogListener : Java.Lang.Object, VKShareDialog.IVKShareDialogListener
	{
		private readonly Action<int> onComplete;
		private readonly Action onCancel;
		private readonly Action<VKError> onError;

		public ActionShareDialogListener (Action<int> onComplete, Action onCancel, Action<VKError> onError)
		{
			this.onComplete = onComplete;
			this.onCancel = onCancel;
			this.onError = onError;
		}

		public void OnVkShareCancel ()
		{
			var handler = onCancel;
			if (handler != null) {
				handler ();
			}
		}

		public void OnVkShareComplete (int postId)
		{
			var handler = onComplete;
			if (handler != null) {
				handler (postId);
			}
		}

		public void OnVkShareError (VKError error)
		{
			var handler = onError;
			if (handler != null) {
				handler (error);
			}
		}
	}
}
