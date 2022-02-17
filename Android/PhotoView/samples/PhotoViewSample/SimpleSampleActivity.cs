using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using ImageViews.Photo;
using Debug = System.Diagnostics.Debug;

namespace PhotoViewSample
{
    [Activity(Label = "Simple Sample")]
    public class SimpleSampleActivity : AppCompatActivity
    {
        private Matrix currentMatrix = null;
        private TextView currentMatrixTextView;
        private Toast currentToast;
        private PhotoView photoView;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_simple_sample);

            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Simple Sample";
            toolbar.SetNavigationIcon(Resource.Drawable.ic_arrow_back_white_24dp);
            toolbar.NavigationClick += (sender, e) => OnBackPressed();
            toolbar.InflateMenu(Resource.Menu.main_menu);
            toolbar.MenuItemClick += OnMenuItemClickd;

            photoView = FindViewById<PhotoView>(Resource.Id.iv_photo);
            currentMatrixTextView = FindViewById<TextView>(Resource.Id.tv_current_matrix);

            var bitmap = await BitmapFactory.DecodeResourceAsync(Resources, Resource.Drawable.wallpaper);
            photoView.SetImageBitmap(bitmap);

            // Lets attach some listeners, not required though!
            photoView.MatrixChange += (sender, e) => { currentMatrixTextView.Text = e.Rect.ToString(); };
            photoView.PhotoTap += (sender, e) =>
            {
                float xPercentage = e.X * 100f;
                float yPercentage = e.Y * 100f;
                ShowToast($"Photo Tap! X:{xPercentage:0.00} % Y:{yPercentage:0.00} % ID: {(e.View == null ? 0 : e.View.Id)}");
            };
            photoView.OutsidePhotoTap += (sender, e) => { ShowToast("You have a tap event on the place where out of the photo."); };
            photoView.SingleFling += (sender, e) =>
            {
                Debug.WriteLine($"Fling velocityX: {e.VelocityX:0.00}, velocityY: {e.VelocityY:0.00}");
                e.Handled = true;
            };
        }

        private void OnMenuItemClickd(object sender, AndroidX.AppCompat.Widget.Toolbar.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_zoom_toggle:
                    photoView.Zoomable = !photoView.Zoomable;
                    e.Item.SetTitle(photoView.Zoomable ? Resource.String.menu_zoom_disable : Resource.String.menu_zoom_enable);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_fit_center:
                    photoView.SetScaleType(ImageView.ScaleType.FitCenter);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_fit_start:
                    photoView.SetScaleType(ImageView.ScaleType.FitStart);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_fit_end:
                    photoView.SetScaleType(ImageView.ScaleType.FitEnd);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_fit_xy:
                    photoView.SetScaleType(ImageView.ScaleType.FitXy);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_scale_center:
                    photoView.SetScaleType(ImageView.ScaleType.Center);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_scale_center_crop:
                    photoView.SetScaleType(ImageView.ScaleType.CenterCrop);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_scale_center_inside:
                    photoView.SetScaleType(ImageView.ScaleType.CenterInside);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_scale_random_animate:
                case Resource.Id.menu_scale_random:
                    var r = new Random();
                    float minScale = photoView.MinimumScale;
                    float maxScale = photoView.MaximumScale;
                    float randomScale = minScale + ((float) r.NextDouble() * (maxScale - minScale));
                    photoView.SetScale(randomScale, e.Item.ItemId == Resource.Id.menu_scale_random_animate);
                    ShowToast($"Scaled to: {randomScale:0.00}");
                    e.Handled = true;
                    break;

                case Resource.Id.menu_matrix_restore:
                    if (currentMatrix == null)
                        ShowToast("You need to capture display matrix first");
                    else
                        photoView.SetDisplayMatrix(currentMatrix);
                    e.Handled = true;
                    break;

                case Resource.Id.menu_matrix_capture:
                    currentMatrix = photoView.DisplayMatrix;
                    e.Handled = true;
                    break;

                default:
                    e.Handled = false;
                    break;
            }
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