using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;

using UniversalImageLoader.Core;

using UniversalImageLoaderSample.Fragments;

namespace UniversalImageLoaderSample
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private const string TEST_FILE_NAME = "Universal Image Loader @#&=+-_.,!()~'%20.png";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ac_home);

            var sdCardImage = "/mnt/sdcard/" + TEST_FILE_NAME;
            if (!File.Exists(sdCardImage))
            {
                Task.Run(() =>
                {
                    try
                    {
                        using (var stream = Assets.Open(TEST_FILE_NAME))
                        using (var dest = File.Open(sdCardImage, FileMode.Create, FileAccess.Write))
                        {
                            var buffer = new byte[8192];
                            int read;
                            while ((read = stream.Read(buffer, 0, buffer.Length)) != -1)
                            {
                                dest.Write(buffer, 0, read);
                            }
                        }
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Can't copy test image onto SD card");
                    }
                });
            }

            FindViewById(Resource.Id.imageListButton).Click += delegate
            {
                var intent = new Intent(this, typeof(SimpleImageActivity));
                intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.List);
                StartActivity(intent);
            };

            FindViewById(Resource.Id.imageGridButton).Click += delegate
            {
                var intent = new Intent(this, typeof(SimpleImageActivity));
                intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.Grid);
                StartActivity(intent);
            };

            FindViewById(Resource.Id.imagePagerButton).Click += delegate
            {
                var intent = new Intent(this, typeof(SimpleImageActivity));
                intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.Pager);
                StartActivity(intent);
            };

            FindViewById(Resource.Id.imageGalleryButton).Click += delegate
            {
                var intent = new Intent(this, typeof(SimpleImageActivity));
                intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.Gallery);
                StartActivity(intent);
            };

            FindViewById(Resource.Id.fragmentsButton).Click += delegate
            {
                var intent = new Intent(this, typeof(ComplexImageActivity));
                StartActivity(intent);
            };
        }

        public override void OnBackPressed()
        {
            ImageLoader.Instance.Stop();

            base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.item_clear_memory_cache:
                    ImageLoader.Instance.ClearMemoryCache();
                    return true;
                case Resource.Id.item_clear_disc_cache:
                    ImageLoader.Instance.ClearDiskCache();
                    return true;
                default:
                    return false;
            }
        }
    }
}
