using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "Simple Sample", Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class SimpleSampleActivity : AppCompatActivity
    {
        private Matrix currentMatrix = null;
        private TextView currentMatrixTextView;
        private PhotoViewAttacher attacher;
        private Toast currentToast;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Sample);

            var imageView = FindViewById<ImageView>(Resource.Id.iv_photo);
            currentMatrixTextView = FindViewById<TextView>(Resource.Id.tv_current_matrix);

            var bitmap = await BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.wallpaper);
            imageView.SetImageBitmap(bitmap);

            // The MAGIC happens here!
            attacher = new PhotoViewAttacher(imageView);

            // Lets attach some listeners, not required though!
            attacher.MatrixChange += (sender, e) =>
            {
                currentMatrixTextView.Text = e.Rect.ToString();
            };
            attacher.PhotoTap += (sender, e) =>
            {
                float xPercentage = e.X * 100f;
                float yPercentage = e.Y * 100f;
                ShowToast(string.Format("Photo Tap! X:{0:0.00} % Y:{1:0.00} % ID: {2}", xPercentage, yPercentage, e.View == null ? 0 : e.View.Id));
            };
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Need to call clean-up
            attacher.Cleanup();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            if (attacher != null)
            {
                var zoomToggle = menu.FindItem(Resource.Id.menu_zoom_toggle);
                zoomToggle.SetTitle(attacher.Zoomable ? Resource.String.menu_zoom_disable : Resource.String.menu_zoom_enable);
            }

            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_zoom_toggle:
                    attacher.Zoomable = !attacher.Zoomable;
                    return true;
                case Resource.Id.menu_scale_fit_center:
                    attacher.SetScaleType(ImageView.ScaleType.FitCenter);
                    return true;
                case Resource.Id.menu_scale_fit_start:
                    attacher.SetScaleType(ImageView.ScaleType.FitStart);
                    return true;
                case Resource.Id.menu_scale_fit_end:
                    attacher.SetScaleType(ImageView.ScaleType.FitEnd);
                    return true;
                case Resource.Id.menu_scale_fit_xy:
                    attacher.SetScaleType(ImageView.ScaleType.FitXy);
                    return true;
                case Resource.Id.menu_scale_scale_center:
                    attacher.SetScaleType(ImageView.ScaleType.Center);
                    return true;
                case Resource.Id.menu_scale_scale_center_crop:
                    attacher.SetScaleType(ImageView.ScaleType.CenterCrop);
                    return true;
                case Resource.Id.menu_scale_scale_center_inside:
                    attacher.SetScaleType(ImageView.ScaleType.CenterInside);
                    return true;
                case Resource.Id.menu_scale_random_animate:
                case Resource.Id.menu_scale_random:
                    var r = new Random();
                    float minScale = attacher.MinimumScale;
                    float maxScale = attacher.MaximumScale;
                    float randomScale = minScale + ((float)r.NextDouble() * (maxScale - minScale));
                    attacher.SetScale(randomScale, item.ItemId == Resource.Id.menu_scale_random_animate);
                    ShowToast(string.Format("Scaled to: {0:0.00}", randomScale));
                    return true;
                case Resource.Id.menu_matrix_restore:
                    if (currentMatrix == null)
                        ShowToast("You need to capture display matrix first");
                    else
                        attacher.SetDisplayMatrix(currentMatrix);
                    return true;
                case Resource.Id.menu_matrix_capture:
                    currentMatrix = attacher.DisplayMatrix;
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowToast(string text)
        {
            if (null != currentToast)
            {
                currentToast.Cancel();
            }

            currentToast = Toast.MakeText(this, text, ToastLength.Short);
            currentToast.Show();
        }
    }
}
