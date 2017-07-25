using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using ImageViews.Photo;

namespace PhotoViewSample
{
    [Activity(Label = "Simple Sample")]
    public class SimpleSampleActivity : AppCompatActivity
    {
        private Matrix currentMatrix = null;
        private TextView currentMatrixTextView;
        private PhotoViewAttacher attacher;
        private Toast currentToast;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

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
				ShowToast($"Photo Tap! X:{xPercentage:0.00} % Y:{yPercentage:0.00} % ID: {(e.View == null ? 0 : e.View.Id)}");
            };
            attacher.OutsidePhotoTap += (sender, e) =>
            {
                ShowToast("You have a tap event on the place where out of the photo.");
            };
            attacher.SingleFling += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Fling velocityX: {e.VelocityX:0.00}, velocityY: {e.VelocityY:0.00}");
                e.Handled = true;
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
                    ShowToast($"Scaled to: {randomScale:0.00}");
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
                case Resource.Id.extract_visible_bitmap:
                    try
                    {
                        var bmp = attacher.VisibleRectangleBitmap;
                        var downloads = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                        var tmpFile = Java.IO.File.CreateTempFile("photoview", ".png", downloads);
                        using (var stream = File.OpenWrite(tmpFile.AbsolutePath))
                            bmp.Compress(Bitmap.CompressFormat.Png, 90, stream);

                        var share = new Intent(Intent.ActionSend);
                        share.SetType("image/png");
                        share.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(tmpFile));
                        StartActivity(share);
                        ShowToast($"Extracted into: {tmpFile.AbsolutePath}");
                    }
                    catch
                    {
                        ShowToast("Error occured while extracting bitmap");
                    }
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
