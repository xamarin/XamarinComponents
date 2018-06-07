using System;
using System.Threading.Tasks;

using VKontakte.API.Models;

namespace VKontakte.API
{
	partial class VKBatchRequest
	{
		public void ExecuteWithListener (
			Action<VKResponse[]> onComplete, 
			Action<VKError> onError = null)
		{
			ExecuteWithListener (new ActionBatchRequestListener (onComplete, onError));
		}

		public Task<VKResponse[]> ExecuteAsync ()
		{
			var tcs = new TaskCompletionSource<VKResponse[]> ();
			ExecuteWithListener (response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)));
			return tcs.Task;
		}

		internal class ActionBatchRequestListener : VKBatchRequestListener
		{
			private readonly Action<VKResponse[]> onComplete;
			private readonly Action<VKError> onError;

			public ActionBatchRequestListener (
				Action<VKResponse[]> onComplete, 
				Action<VKError> onError)
			{
				this.onComplete = onComplete;
				this.onError = onError;
			}

			public override void OnComplete (VKResponse[] responses)
			{
				var handler = onComplete;
				if (handler != null) {
					handler (responses);
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
}
