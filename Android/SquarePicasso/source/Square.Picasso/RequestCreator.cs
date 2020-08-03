using Android.Widget;
using System;

namespace Square.Picasso
{
    partial class RequestCreator
    {
        public virtual void Into(ImageView imageView, Action onSuccess, Action<Java.Lang.Exception> onError)
        {
            Into(imageView, new ActionCallback(onSuccess, onError));
        }

        private class ActionCallback : Java.Lang.Object, ICallback
        {
            private readonly Action onSuccess;
            private readonly Action<Java.Lang.Exception> onError;

            public ActionCallback(Action onSuccess, Action<Java.Lang.Exception> onError)
            {
                this.onSuccess = onSuccess;
                this.onError = onError;
            }

            public void OnSuccess() =>
                onSuccess?.Invoke();

            public void OnError(Java.Lang.Exception exc) =>
                onError?.Invoke(exc);
        }
    }
}
