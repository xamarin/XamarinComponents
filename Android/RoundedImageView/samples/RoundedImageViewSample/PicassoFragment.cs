using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using Fragment = Android.Support.V4.App.Fragment;

using ImageViews.Rounded;

namespace RoundedImageViewSample
{
    public class PicassoFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Rounded, container, false);

            PicassoAdapter adapter = new PicassoAdapter(Activity);
            view.FindViewById<ListView>(Resource.Id.main_list).Adapter = adapter;

			adapter.Add(new PicassoItem("https://lh6.googleusercontent.com/-55osAWw3x0Q/URquUtcFr5I/AAAAAAAAAbs/rWlj1RUKrYI/s1024/A%252520Photographer.jpg", ImageView.ScaleType.Center));
			adapter.Add(new PicassoItem("https://lh4.googleusercontent.com/--dq8niRp7W4/URquVgmXvgI/AAAAAAAAAbs/-gnuLQfNnBA/s1024/A%252520Song%252520of%252520Ice%252520and%252520Fire.jpg", ImageView.ScaleType.CenterCrop));
			adapter.Add(new PicassoItem("https://lh5.googleusercontent.com/-7qZeDtRKFKc/URquWZT1gOI/AAAAAAAAAbs/hqWgteyNXsg/s1024/Another%252520Rockaway%252520Sunset.jpg", ImageView.ScaleType.CenterInside));
			adapter.Add(new PicassoItem("https://lh3.googleusercontent.com/--L0Km39l5J8/URquXHGcdNI/AAAAAAAAAbs/3ZrSJNrSomQ/s1024/Antelope%252520Butte.jpg", ImageView.ScaleType.FitCenter));
			adapter.Add(new PicassoItem("https://lh6.googleusercontent.com/-8HO-4vIFnlw/URquZnsFgtI/AAAAAAAAAbs/WT8jViTF7vw/s1024/Antelope%252520Hallway.jpg", ImageView.ScaleType.FitEnd));
			adapter.Add(new PicassoItem("https://lh4.googleusercontent.com/-WIuWgVcU3Qw/URqubRVcj4I/AAAAAAAAAbs/YvbwgGjwdIQ/s1024/Antelope%252520Walls.jpg", ImageView.ScaleType.FitStart));
			adapter.Add(new PicassoItem("https://lh6.googleusercontent.com/-UBmLbPELvoQ/URqucCdv0kI/AAAAAAAAAbs/IdNhr2VQoQs/s1024/Apre%2525CC%252580s%252520la%252520Pluie.jpg", ImageView.ScaleType.FitXy));

            return view;
        }

        public class PicassoItem
        {
            public string mUrl { get; set; }

            public ImageView.ScaleType mScaleType { get; set; }

            public PicassoItem(string url, ImageView.ScaleType scaleType)
            {
                mUrl = url;
                mScaleType = scaleType;
            }
        }

        public class PicassoAdapter : ArrayAdapter<PicassoItem>
        {
            private LayoutInflater mInflater;
            private ITransformation mTransformation;

            public PicassoAdapter(Context context)
                : base(context, 0)
            {
                mInflater = LayoutInflater.From(Context);
                mTransformation = new RoundedTransformationBuilder()
                    .CornerRadiusDp(30)
                    .BorderColor(Color.Black)
                    .BorderWidthDp(3)
                    .Oval(false)
                    .Build();
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ViewGroup view;
                if (convertView == null)
                {
                    view = (ViewGroup)mInflater.Inflate(Resource.Layout.PicassoItem, parent, false);
                }
                else
                {
                    view = (ViewGroup)convertView;
                }

                PicassoItem item = GetItem(position);

                ImageView imageView = view.FindViewById<ImageView>(Resource.Id.imageView1);
                imageView.SetScaleType(item.mScaleType);

                Picasso.With(Context)
                    .Load(item.mUrl)
                    .Fit()
                    .Transform(mTransformation)
                    .Into(imageView);

                view.FindViewById<TextView>(Resource.Id.textView3).Text = item.mScaleType.ToString();
                return view;
            }
        }
    }
}
