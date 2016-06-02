using System;
using Android.Views;

namespace UniversalImageLoader.Core.Listener
{
    public class ImageLoadingProgressListener : Java.Lang.Object, IImageLoadingProgressListener
    {
        private Action<string, View, int, int> progressUpdate;

        public ImageLoadingProgressListener(Action<string, View, int, int> progressUpdate)
        {
            this.progressUpdate = progressUpdate;
        }

        void IImageLoadingProgressListener.OnProgressUpdate(string imageUri, View view, int current, int total)
        {
            var handler = progressUpdate;
            if (handler != null)
            {
                handler(imageUri, view, current, total);
            }
        }
    }
}
