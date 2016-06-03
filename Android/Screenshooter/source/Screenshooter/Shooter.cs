using System;
using Android.App;
using Android.Graphics;
using Android.Views;

namespace Screenshooter
{
    /// <summary>
    /// Represents the ability to capture a screenshot.
    /// </summary>
    public static class Shooter
    {
        /// <summary>
        /// Snaps the specified view in the dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(Dialog dialog, int resourceId)
        {
            return Snap(dialog.FindViewById(resourceId));
        }

        /// <summary>
        /// Snaps the specified view in the activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(Activity activity, int resourceId)
        {
            return Snap(activity.FindViewById(resourceId));
        }

        /// <summary>
        /// Snaps the specified view in the view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="resourceId">The resource identifier.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(View view, int resourceId)
        {
            return Snap(view.FindViewById(resourceId));
        }

        /// <summary>
        /// Snaps the specified dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(Dialog dialog)
        {
            return Snap(dialog.Window.DecorView.RootView);
        }

        /// <summary>
        /// Snaps the specified activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(Activity activity)
        {
            return Snap(activity.Window.DecorView.RootView);
        }

        /// <summary>
        /// Snaps the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns>The screenshot bitmap.</returns>
        public static Bitmap Snap(View view)
        {
            Bitmap bitmap = null;
            if (view != null)
            {
                view.ClearFocus();
                bitmap = CreateBitmapSafely(view.Width, view.Height, Bitmap.Config.Argb8888, 1);
                if (bitmap != null)
                {
                    var canvas = new Canvas(bitmap);
                    view.Draw(canvas);
                }
            }
            return bitmap;
        }

        /// <summary>
        /// Creates the bitmap safely.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="config">The configuration.</param>
        /// <param name="retryCount">The retry count.</param>
        /// <returns>The bitmap.</returns>
        public static Bitmap CreateBitmapSafely(int width, int height, Bitmap.Config config, int retryCount)
        {
            try
            {
                return Bitmap.CreateBitmap(width, height, config);
            }
            catch (OutOfMemoryException)
            {
                return CreateBitmapSafelyWithGc(width, height, config, retryCount);
            }
            catch (Java.Lang.OutOfMemoryError)
            {
                return CreateBitmapSafelyWithGc(width, height, config, retryCount);
            }
        }

        private static Bitmap CreateBitmapSafelyWithGc(int width, int height, Bitmap.Config config, int retryCount)
        {
            if (retryCount > 0)
            {
                Java.Lang.JavaSystem.Gc();
                GC.Collect();
                return CreateBitmapSafely(width, height, config, retryCount - 1);
            }
            return null;
        }
    }
}
