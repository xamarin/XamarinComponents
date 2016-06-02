using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Views;

using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Listener;
using UniversalImageLoader.Core.Display;

namespace UniversalImageLoaderSample.Fragments
{
    public class ImageListFragment : AbsListViewBaseFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fr_image_list, container, false);
            listView = rootView.FindViewById<ListView>(Android.Resource.Id.List);
            listView.Adapter = new ImageAdapter(Activity);
            listView.ItemClick += (sender, e) =>
            {
                StartImagePagerActivity(e.Position);
            };
            return rootView;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            AnimateFirstDisplayListener.displayedImages.Clear();
        }

        private class ImageAdapter : BaseAdapter
        {
            private static readonly string[] ImageUrls = Constants.Images;

            private LayoutInflater inflater;
            private IImageLoadingListener animateFirstListener = new AnimateFirstDisplayListener();

            private DisplayImageOptions options;

            public ImageAdapter(Context context)
            {
                inflater = LayoutInflater.From(context);

                options = new DisplayImageOptions.Builder()
                    .ShowImageOnLoading(Resource.Drawable.ic_stub)
                    .ShowImageForEmptyUri(Resource.Drawable.ic_empty)
                    .ShowImageOnFail(Resource.Drawable.ic_error)
                    .CacheInMemory(true)
                    .CacheOnDisk(true)
                    .ConsiderExifParams(true)
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
                View view = convertView;
                ViewHolder holder;
                if (convertView == null)
                {
                    view = inflater.Inflate(Resource.Layout.item_list_image, parent, false);
                    holder = new ViewHolder();
                    holder.text = view.FindViewById<TextView>(Resource.Id.text);
                    holder.image = view.FindViewById<ImageView>(Resource.Id.image);
                    view.Tag = holder;
                }
                else
                {
                    holder = (ViewHolder)view.Tag;
                }

                holder.text.Text = "Item " + (position + 1);

                ImageLoader.Instance.DisplayImage(ImageUrls[position], holder.image, options, animateFirstListener);

                return view;
            }
        }

        private class ViewHolder : Java.Lang.Object
        {
            public TextView text;
            public ImageView image;
        }

        private class AnimateFirstDisplayListener : SimpleImageLoadingListener
        {
            internal static readonly List<string> displayedImages = new List<string>();

            public override void OnLoadingComplete(string imageUri, View view, Bitmap loadedImage)
            {
                if (loadedImage != null)
                {
                    lock (displayedImages)
                    {
                        ImageView imageView = (ImageView)view;
                        bool firstDisplay = !displayedImages.Contains(imageUri);
                        if (firstDisplay)
                        {
                            FadeInBitmapDisplayer.Animate(imageView, 500);
                            displayedImages.Add(imageUri);
                        }
                    }
                }
            }
        }
    }
}
