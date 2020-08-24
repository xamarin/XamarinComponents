using System;
using Android.Graphics;
using Square.Picasso;

namespace PicassoSample
{
    public class GrayscaleTransformation : Java.Lang.Object, ITransformation
    {
        private readonly Picasso picasso;

        public GrayscaleTransformation(Picasso picasso)
        {
            this.picasso = picasso;
        }

        public Bitmap Transform(Bitmap source)
        {
            Bitmap result = Bitmap.CreateBitmap(source.Width, source.Height, source.GetConfig());
            Bitmap noise;
            try
            {
                noise = picasso.Load(Resource.Drawable.noise).Get();
            }
            catch (Exception)
            {
                throw new Exception("Failed to apply transformation! Missing resource.");
            }

            BitmapShader shader = new BitmapShader(noise, Shader.TileMode.Repeat, Shader.TileMode.Repeat);

            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.SetSaturation(0);
            ColorMatrixColorFilter filter = new ColorMatrixColorFilter(colorMatrix);

            Paint paint = new Paint(PaintFlags.AntiAlias);
            paint.SetColorFilter(filter);

            Canvas canvas = new Canvas(result);
            canvas.DrawBitmap(source, 0, 0, paint);

            paint.SetColorFilter(null);
            paint.SetShader(shader);
            paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Multiply));

            canvas.DrawRect(0, 0, canvas.Width, canvas.Height, paint);

            source.Recycle();
            noise.Recycle();

            return result;
        }

        public string Key
        {
            get { return "grayscaleTransformation()"; }
        }
    }
}
