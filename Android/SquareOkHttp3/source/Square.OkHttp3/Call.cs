using System;
using System.Collections.Generic;
using Android.Runtime;
using System.Threading.Tasks;

namespace Square.OkHttp3
{
    public static class CallExtensions
    {
        public static Task<Response> ExecuteAsync(this ICall call)
        {
            var tcs = new TaskCompletionSource<Response>();

            call.Enqueue(
                (c, response) =>
                {
                    tcs.SetResult(response);
                },
                (c, exception) =>
                {
                    if (call.IsCanceled)
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
        
        public static void Enqueue(this ICall call, Action<ICall, Response> onResponse, Action<ICall, Java.IO.IOException> onFailure)
        {
            call.Enqueue(new ActionCallback(onResponse, onFailure));
        }

        private class ActionCallback : Java.Lang.Object, global::Square.OkHttp3.ICallback
        {
            private readonly Action<ICall, Response> onResponse;
            private readonly Action<ICall, Java.IO.IOException> onFailure;

            public ActionCallback(Action<ICall, Response> onResponse, Action<ICall, Java.IO.IOException> onFailure)
            {
                this.onResponse = onResponse;
                this.onFailure = onFailure;
            }

            public void OnResponse(ICall call, Response response)
            {
                if (onResponse != null)
                {
                    onResponse(call, response);
                }
            }

            public void OnFailure(ICall call, Java.IO.IOException exception)
            {
                if (onFailure != null)
                {
                    onFailure(call, exception);
                }
            }
        }
    }
}
