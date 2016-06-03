using System;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;

namespace Explosions
{
    internal static class Utils
    {
        private static readonly float density = Resources.System.DisplayMetrics.Density;

        public static float NextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static int Dp2Px(int dp)
        {
            return (int)Math.Round(dp * density);
        }

        public static Bitmap CreateBitmapFromView(View view)
        {
            // try a shortcut if it is an image view
            var iv = view as ImageView;
            if (iv != null)
            {
                var drawable = iv.Drawable as BitmapDrawable;
                if (drawable != null)
                {
                    return drawable.Bitmap;
                }
            }

            // otherwise just draw the entire view
            view.ClearFocus();
            var bitmap = CreateBitmapSafely(view.Width, view.Height, Bitmap.Config.Argb8888, 1);
            if (bitmap != null)
            {
                var canvas = new Canvas(bitmap);
                view.Draw(canvas);
            }
            return bitmap;
        }

        public static Bitmap CreateBitmapSafely(int width, int height, Bitmap.Config config, int retryCount)
        {
            try
            {
                return Bitmap.CreateBitmap(width, height, config);
            }
            catch (OutOfMemoryException e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
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
}
