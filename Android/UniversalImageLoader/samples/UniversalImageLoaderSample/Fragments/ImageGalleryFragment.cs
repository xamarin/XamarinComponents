using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Views;

using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Display;

namespace UniversalImageLoaderSample.Fragments
{
    public class ImageGalleryFragment : BaseFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fr_image_gallery, container, false);
            var gallery = rootView.FindViewById<Gallery>(Resource.Id.gallery);
            gallery.Adapter = new ImageAdapter(Activity);
            gallery.ItemClick += (sender, e) =>
            {
                StartImagePagerActivity(e.Position);
            };
            return rootView;
        }

        private void StartImagePagerActivity(int position)
        {
            var intent = new Intent(Activity, typeof(SimpleImageActivity));
            intent.PutExtra(Constants.Extra.FragmentIndex, (int)ImageFragments.Pager);
            intent.PutExtra(Constants.Extra.ImagePosition, position);
            StartActivity(intent);
        }

        private class ImageAdapter : BaseAdapter
        {
            private static readonly string[] ImageUrls = Constants.Images;
            private LayoutInflater inflater;
            private DisplayImageOptions options;

            internal ImageAdapter(Context context)
            {
                inflater = LayoutInflater.From(context);

                options = new DisplayImageOptions.Builder()
                    .ShowImageOnLoading(Resource.Drawable.ic_stub)
                    .ShowImageForEmptyUri(Resource.Drawable.ic_empty)
                    .ShowImageOnFail(Resource.Drawable.ic_error)
                    .CacheInMemory(true)
                    .CacheOnDisk(true)
                    .ConsiderExifParams(true)
                    .BitmapConfig(Bitmap.Config.Rgb565)
                    .Displayer(new RoundedBitmapDisplayer(20))
                    .Build();
            }

            public override int Count
            {
                get { return ImageUrls.Length; }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return position;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var imageView = convertView as ImageView;
                if (imageView == null)
                {
                    imageView = (ImageView)inflater.Inflate(Resource.Layout.item_gallery_image, parent, false);
                }
                ImageLoader.Instance.DisplayImage(ImageUrls[position], imageView, options);
                return imageView;
            }
        }
    }
}