using System;
using System.Threading.Tasks;

using VKontakte.API.Models;

namespace VKontakte.API
{
	partial class VKRequest
	{
		public void ExecuteWithListener (
			Action<VKResponse> onComplete, 
			Action<VKError> onError = null, 
			Action<VKProgressType, long, long> onProgress = null, 
			Action<VKRequest, int, int> attemptFailed = null)
		{
			ExecuteWithListener (new ActionRequestListener (onComplete, onError, onProgress, attemptFailed));
		}

		public Task<VKResponse> ExecuteAsync (
			Action<VKProgressType, long, long> onProgress = null, 
			Action<VKRequest, int, int> attemptFailed = null)
		{
			var tcs = new TaskCompletionSource<VKResponse> ();
			ExecuteWithListener (response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)), onProgress, attemptFailed);
			return tcs.Task;
		}

		public virtual void ExecuteAfterRequest (
			VKRequest request,
			Action<VKResponse> onComplete, 
			Action<VKError> onError = null, 
			Action<VKProgressType, long, long> onProgress = null, 
			Action<VKRequest, int, int> attemptFailed = null)
		{
			ExecuteAfterRequest (request, new ActionRequestListener (onComplete, onError, onProgress, attemptFailed));
		}

		public Task<VKResponse> ExecuteAfterRequestAsync (
			VKRequest request,
			Action<VKProgressType, long, long> onProgress = null, 
			Action<VKRequest, int, int> attemptFailed = null)
		{
			var tcs = new TaskCompletionSource<VKResponse> ();
			ExecuteAfterRequest (request, response => tcs.SetResult (response), error => tcs.SetException (new VKException (error)), onProgress, attemptFailed);
			return tcs.Task;
		}

		public void SetModelClass<T> ()
			where T : VKApiModel
		{
			SetModelClass (typeof(T));
		}

		public void SetModelClass (Type modelType)
		{
			SetModelClass (Java.Lang.Class.FromType (modelType));
		}

		public void SetRequestListener (
			Action<VKResponse> onComplete, 
			Action<VKError> onError = null, 
			Action<VKProgressType, long, long> onProgress = null, 
			Action<VKRequest, int, int> attemptFailed = null)
		{
			SetRequestListener (new ActionRequestListener (onComplete, onError, onProgress, attemptFailed));
		}

		internal class ActionRequestListener : VKRequestListener
		{
			private readonly Action<VKResponse> onComplete;
			private readonly Action<VKError> onError;
			private readonly Action<VKProgressType, long, long> onProgress;
			private readonly Action<VKRequest, int, int> attemptFailed;

			public ActionRequestListener (
				Action<VKResponse> onComplete, 
				Action<VKError> onError, 
				Action<VKProgressType, long, long> onProgress, 
				Action<VKRequest, int, int> attemptFailed)
			{
				this.onComplete = onComplete;
				this.onError = onError;
				this.onProgress = onProgress;
				this.attemptFailed = attemptFailed;
			}

			public override void AttemptFailed (VKRequest request, int attemptNumber, int totalAttempts)
			{
				var handler = attemptFailed;
				if (handler != null) {
					handler (request, attemptNumber, totalAttempts);
				}
			}

			public override void OnComplete (VKResponse response)
			{
				var handler = onComplete;
				if (handler != null) {
					handler (response);
				}
			}

			public override void OnError (VKError error)
			{
				var handler = onError;
				if (handler != null) {
					handler (error);
				}
			}

			public override void OnProgress (VKProgressType progressType, long bytesLoaded, long bytesTotal)
			{
				var handler = onProgress;
				if (handler != null) {
					handler (progressType, bytesLoaded, bytesTotal);
				}
			}
		}
	}
}
