using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

using Screenshooter;

namespace ScreenshooterSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
    public class MainActivity : AppCompatActivity
    {
        private Bitmap currentBitmap;

        private ImageView imageView;
        private TextView screenshotSize;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            imageView = FindViewById<ImageView>(Resource.Id.imageView);
            screenshotSize = FindViewById<TextView>(Resource.Id.screenshotSize);

            var snapApp = FindViewById<Button>(Resource.Id.snapApp);
            snapApp.Click += delegate
            {
                // snap entire activities
                ShowSnap(Shooter.Snap(this));
            };

            var snapButtons = FindViewById<Button>(Resource.Id.snapButtons);
            snapButtons.Click += delegate
            {
                // snap by resource ID
                ShowSnap(Shooter.Snap(this, Resource.Id.buttons));
            };

            var snapThisButton = FindViewById<Button>(Resource.Id.snapThisButton);
            snapThisButton.Click += delegate
            {
                // snap a view
                ShowSnap(Shooter.Snap(snapThisButton));
            };
        }

        private void ShowSnap(Bitmap bitmap)
        {
            var temp = currentBitmap;

            // set image
            currentBitmap = bitmap;
            imageView.SetImageBitmap(bitmap);

            if (bitmap != null)
            {
                screenshotSize.Text = string.Format("{0} x {1}", bitmap.Width, bitmap.Height);
            }
            else
            {
                screenshotSize.Text = "Out Of Memory";
            }

            // destroy old
            if (temp != null && temp.IsRecycled)
            {
                temp.Recycle();
            }
        }

        protected override void OnDestroy()
        {
            if (currentBitmap != null && currentBitmap.IsRecycled)
            {
                currentBitmap.Recycle();
            }
            currentBitmap = null;

            base.OnDestroy();
        }
    }
}

