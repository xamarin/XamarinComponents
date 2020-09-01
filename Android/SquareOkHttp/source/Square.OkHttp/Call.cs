using System;
using System.Collections.Generic;
using Android.Runtime;
using System.Threading.Tasks;

namespace Square.OkHttp
{
	partial class Call
    {
        public Task<Response> ExecuteAsync()
        {
            var tcs = new TaskCompletionSource<Response>();

            Enqueue(
                response =>
                {
                    tcs.SetResult(response);
                },
                (request, exception) =>
                {
                    if (IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        tcs.SetException(exception);
                    }
                });

            return tcs.Task;
        }
        
        public void Enqueue(Action<Response> onResponse, Action<Request, Java.IO.IOException> onFailure)
        {
            Enqueue(new ActionCallback(onResponse, onFailure));
        }

        private class ActionCallback : Java.Lang.Object, ICallback
        {
            private readonly Action<Response> onResponse;
            private readonly Action<Request, Java.IO.IOException> onFailure;

            public ActionCallback(Action<Response> onResponse, Action<Request, Java.IO.IOException> onFailure)
            {
                this.onResponse = onResponse;
                this.onFailure = onFailure;
            }

            public void OnResponse(Response response)
            {
                if (onResponse != null)
                {
                    onResponse(response);
                }
            }

            public void OnFailure(Request request, Java.IO.IOException exception)
            {
                if (onFailure != null)
                {
                    onFailure(request, exception);
                }
            }
        }
	}
}
