using System;
using Android.Graphics;
using Android.Views;

using UniversalImageLoader.Core.Assist;

namespace UniversalImageLoader.Core.Listener
{
    public class ImageLoadingListener : Java.Lang.Object, IImageLoadingListener
    {
        private Action<string, View> loadingCancelled;
        private Action<string, View, Bitmap> loadingComplete;
        private Action<string, View, FailReason> loadingFailed;
        private Action<string, View> loadingStarted;

        public ImageLoadingListener(
            Action<string, View, Bitmap> loadingComplete = null,
            Action<string, View> loadingStarted = null,
            Action<string, View, FailReason> loadingFailed = null,
            Action<string, View> loadingCancelled = null)
        {
            this.loadingComplete = loadingComplete;
            this.loadingStarted = loadingStarted;
            this.loadingFailed = loadingFailed;
            this.loadingCancelled = loadingCancelled;
        }

        void IImageLoadingListener.OnLoadingCancelled(string imageUri, View view)
        {
            var handler = loadingCancelled;
            if (handler != null)
            {
                handler(imageUri, view);
            }
        }

        void IImageLoadingListener.OnLoadingComplete(string imageUri, View view, Bitmap loadedImage)
        {
            var handler = loadingComplete;
            if (handler != null)
            {
                handler(imageUri, view, loadedImage);
            }
        }

        void IImageLoadingListener.OnLoadingFailed(string imageUri, View view, FailReason failReason)
        {
            var handler = loadingFailed;
            if (handler != null)
            {
                handler(imageUri, view, failReason);
            }
        }

        void IImageLoadingListener.OnLoadingStarted(string imageUri, View view)
        {
            var handler = loadingStarted;
            if (handler != null)
            {
                handler(imageUri, view);
            }
        }
    }
}
