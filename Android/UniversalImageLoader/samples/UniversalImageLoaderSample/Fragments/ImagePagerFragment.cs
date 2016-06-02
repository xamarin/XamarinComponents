using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Widget;
using Android.Views;

using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Listener;
using UniversalImageLoader.Core.Assist;
using UniversalImageLoader.Core.Display;

namespace UniversalImageLoaderSample.Fragments
{
    public class ImagePagerFragment : BaseFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fr_image_pager, container, false);
            var pager = rootView.FindViewById<ViewPager>(Resource.Id.pager);
            pager.Adapter = new ImageAdapter(Activity);
            pager.CurrentItem = Arguments.GetInt(Constants.Extra.ImagePosition, 0);
            return rootView;
        }

        private class ImageAdapter : PagerAdapter
        {
            private static readonly string[] ImageUrls = Constants.Images;

            private LayoutInflater inflater;
            private DisplayImageOptions options;

            internal ImageAdapter(Context context)
            {
                inflater = LayoutInflater.From(context);

                options = new DisplayImageOptions.Builder()
                    .ShowImageForEmptyUri(Resource.Drawable.ic_empty)
                    .ShowImageOnFail(Resource.Drawable.ic_error)
                    .ResetViewBeforeLoading(true)
                    .CacheOnDisk(true)
                    .ImageScaleType(ImageScaleType.Exactly)
                    .BitmapConfig(Bitmap.Config.Rgb565)
                    .ConsiderExifParams(true)
                    .Displayer(new FadeInBitmapDisplayer(300))
                    .Build();
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
            {
                container.RemoveView((View)@object);
            }

            public override int Count
            {
                get { return ImageUrls.Length; }
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup view, int position)
            {
                var imageLayout = inflater.Inflate(Resource.Layout.item_pager_image, view, false);

                var imageView = imageLayout.FindViewById<ImageView>(Resource.Id.image);
                var spinner = imageLayout.FindViewById<ProgressBar>(Resource.Id.loading);

                ImageLoader.Instance.DisplayImage(
                    ImageUrls[position],
                    imageView,
                    options,
                    new ImageLoadingListener(
                        loadingStarted: delegate
                        {
                            spinner.Visibility = ViewStates.Visible;
                        },
                        loadingComplete: delegate
                        {
                            spinner.Visibility = ViewStates.Gone;
                        },
                        loadingFailed: (imageUri, _view, failReason) =>
                        {
                            string message = null;
                            if (failReason.Type == FailReason.FailType.IoError)
                            {
                                message = "Input/Output error";
                            }
                            else if (failReason.Type == FailReason.FailType.DecodingError)
                            {
                                message = "Image can't be decoded";
                            }
                            else if (failReason.Type == FailReason.FailType.NetworkDenied)
                            {
                                message = "Downloads are denied";
                            }
                            else if (failReason.Type == FailReason.FailType.OutOfMemory)
                            {
                                message = "Out Of Memory error";
                            }
                            else
                            {
                                message = "Unknown error";
                            }
                            Toast.MakeText(view.Context, message, ToastLength.Short).Show();

                            spinner.Visibility = ViewStates.Gone;
                        }));

                view.AddView(imageLayout, 0);
                return imageLayout;
            }

            public override bool IsViewFromObject(View view, Java.Lang.Object @object)
            {
                return view.Equals(@object);
            }

            public override void RestoreState(IParcelable state, Java.Lang.ClassLoader loader)
            {
            }

            public override IParcelable SaveState()
            {
                return null;
            }
        }
    }
}
