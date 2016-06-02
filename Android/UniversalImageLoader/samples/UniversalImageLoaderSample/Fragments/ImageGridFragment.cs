using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using Android.Views;

using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Listener;
using UniversalImageLoader.Core.Assist;

namespace UniversalImageLoaderSample.Fragments
{
    public class ImageGridFragment : AbsListViewBaseFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fr_image_grid, container, false);
            listView = rootView.FindViewById<GridView>(Resource.Id.grid);
            listView.Adapter = new ImageAdapter(Activity);
            listView.ItemClick += (sender, e) =>
            {
                StartImagePagerActivity(e.Position);
            };
            return rootView;
        }

        private class ImageAdapter : BaseAdapter
        {
            private static readonly string[] ImageUrls = Constants.Images;
            private LayoutInflater inflater;
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
                    .BitmapConfig(Bitmap.Config.Rgb565)
                    .Build();
            }

            public override int Count
            {
                get { return ImageUrls.Length; }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ViewHolder holder;
                View view = convertView;
                if (view == null)
                {
                    view = inflater.Inflate(Resource.Layout.item_grid_image, parent, false);
                    holder = new ViewHolder();
                    holder.imageView = view.FindViewById<ImageView>(Resource.Id.image);
                    holder.progressBar = view.FindViewById<ProgressBar>(Resource.Id.progress);
                    view.Tag = holder;
                }
                else
                {
                    holder = (ViewHolder)view.Tag;
                }

                ImageLoader.Instance.DisplayImage(
                    ImageUrls[position],
                    holder.imageView,
                    options,
                    new ImageLoadingListener(
                        loadingStarted: delegate
                        {
                            holder.progressBar.Progress = 0;
                            holder.progressBar.Visibility = ViewStates.Visible;
                        },
                        loadingComplete: delegate
                        {
                            holder.progressBar.Visibility = ViewStates.Gone;
                        },
                        loadingFailed: delegate
                        {
                            holder.progressBar.Visibility = ViewStates.Gone;
                        }),
                    new ImageLoadingProgressListener(
                        progressUpdate: (imageUri, _view, current, total) =>
                        {
                            holder.progressBar.Progress = (int)(100.0f * current / total);
                        }));

                return view;
            }
        }

        internal class ViewHolder : Java.Lang.Object
        {
            internal ImageView imageView;
            internal ProgressBar progressBar;
        }
    }
}
