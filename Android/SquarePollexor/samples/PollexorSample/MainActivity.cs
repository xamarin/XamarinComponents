using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using ImageViews.Photo;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Listener;

using Square.Pollexor;

namespace PollexorSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private const string ImageUrl = "http://phototc.com/public/uploads/tours/pax_photo_gallery/Tanz_cheetahs_Andrew_Lerman_photo_tour.jpg";

        private PhotoView imageView;
        private Thumbor thumbor;
        private ImageLoader imageLoader;
        private DisplayImageOptions options;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // set up the image loader
            imageLoader = ImageLoader.Instance;
            options = new DisplayImageOptions.Builder()
                .CacheInMemory(true)
                .Build();

            // to display a preview
            imageView = FindViewById<PhotoView>(Resource.Id.imageView);

            // create the Thumbor instance pointing to the Thumbor server
            thumbor = Thumbor.Create("http://thumbor.thumborize.me/");

            // give us an image to start off with
            GetImage(Resource.Id.resize, GetString(Resource.String.resize));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MainMenu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return GetImage(item.ItemId, item.TitleFormatted.ToString()) || base.OnOptionsItemSelected(item);
        }

        private bool GetImage(int itemId, string title)
        {
            string imageUri = null;
            switch (itemId)
            {
                case Resource.Id.original:
                    imageUri = ImageUrl;
                    break;

                case Resource.Id.resize:
                    imageUri = thumbor
                        .BuildImage(ImageUrl)
                        .Resize(490, 490)
                        .ToUrl();
                    break;

                case Resource.Id.cropGravity:
                    imageUri = thumbor
                        .BuildImage(ImageUrl)
                        .Crop(300, 1000, 900, 1600)
                        .Resize(200, 200)
                        .Align(ThumborUrlBuilder.VerticalAlign.Bottom, ThumborUrlBuilder.HorizontalAlign.Right)
                        .ToUrl();
                    break;

                case Resource.Id.format:
                    imageUri = thumbor
                        .BuildImage(ImageUrl)
                        .Resize(490, 490)
                        .Filter(ThumborUrlBuilder.Format(ThumborUrlBuilder.ImageFormat.Webp))
                        .ToUrl();
                    break;

                case Resource.Id.filter:
                    imageUri = thumbor
                        .BuildImage(ImageUrl)
                        .Resize(490, 490)
                        .Filter(ThumborUrlBuilder.Blur(10))
                        .ToUrl();
                    break;

                case Resource.Id.circular:
                    imageUri = thumbor
                        .BuildImage(ImageUrl)
                        .Resize(490, 490)
                        .Filter(ThumborUrlBuilder.RoundCorner(245))
                        .ToUrl();
                    break;
            }

            if (imageUri != null)
            {
                var progress = ProgressDialog.Show(this, title, "Downloading...", true);

                // Load image
                imageLoader.DisplayImage(
                    imageUri,
                    imageView,
                    options,
                    new ImageLoadingListener(
                        loadingStarted: null,
                        loadingComplete: delegate { progress.Dismiss(); },
                        loadingFailed: delegate { progress.Dismiss(); },
                        loadingCancelled: delegate { progress.Dismiss(); }));
            }

            return imageUri != null;
        }
    }
}

