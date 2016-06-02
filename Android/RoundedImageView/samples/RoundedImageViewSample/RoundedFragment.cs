using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

using ImageViews.Rounded;

namespace RoundedImageViewSample
{
    public class RoundedFragment : Fragment
    {
        private const string ExampleTypeArgument = "ExampleType";

        public enum ExampleType
        {
            Default,
            Oval,
            SelectCorners
        }

        private ExampleType exampleType;

        public static RoundedFragment GetInstance(ExampleType exampleType)
        {
            RoundedFragment f = new RoundedFragment();
            Bundle args = new Bundle();
            args.PutInt(ExampleTypeArgument, (int)exampleType);
            f.Arguments = args;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Arguments != null)
            {
                exampleType = (ExampleType)(Arguments.GetInt(ExampleTypeArgument));
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Rounded, container, false);

            StreamAdapter adapter = new StreamAdapter(Activity, exampleType);

            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo1, "Tufa at night", "Mono Lake, CA", ImageView.ScaleType.Center));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo2, "Starry night", "Lake Powell, AZ", ImageView.ScaleType.CenterCrop));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo3, "Racetrack playa", "Death Valley, CA", ImageView.ScaleType.CenterInside));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo4, "Napali coast", "Kauai, HI", ImageView.ScaleType.FitCenter));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo5, "Delicate Arch", "Arches, UT", ImageView.ScaleType.FitEnd));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo6, "Signal Hill", "Cape Town, South Africa", ImageView.ScaleType.FitStart));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.photo7, "Majestic", "Grand Teton, WY", ImageView.ScaleType.FitXy));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.black_white_tile, "TileMode", "REPEAT", ImageView.ScaleType.FitXy, Shader.TileMode.Repeat));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.black_white_tile, "TileMode", "CLAMP", ImageView.ScaleType.FitXy, Shader.TileMode.Clamp));
            adapter.Add(new StreamItem(Activity, Resource.Drawable.black_white_tile, "TileMode", "MIRROR", ImageView.ScaleType.FitXy, Shader.TileMode.Mirror));

            view.FindViewById<ListView>(Resource.Id.main_list).Adapter = adapter;
            return view;
        }

        private class StreamItem
        {
            public Bitmap Bitmap { get; set; }
            public string Line1 { get; set; }
            public string Line2 { get; set; }
            public ImageView.ScaleType ScaleType { get; set; }
            public Shader.TileMode TileMode { get; set; }

            public StreamItem(Context c, int resid, string line1, string line2, ImageView.ScaleType scaleType, Shader.TileMode tileMode = null)
            {
                Bitmap = BitmapFactory.DecodeResource(c.Resources, resid);
                Line1 = line1;
                Line2 = line2;
                ScaleType = scaleType;
                TileMode = tileMode ?? Shader.TileMode.Clamp;
            }
        }

        private class StreamAdapter : ArrayAdapter<StreamItem>
        {
            private ExampleType exampleType;
            private LayoutInflater mInflater;

            public StreamAdapter(Context context, ExampleType exampleType)
                : base(context, 0)
            {
                this.exampleType = exampleType;
                mInflater = LayoutInflater.From(Context);
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                ViewGroup view;
                if (convertView == null)
                {
                    if (exampleType == ExampleType.SelectCorners)
                    {
                        view = (ViewGroup)mInflater.Inflate(Resource.Layout.RoundedItemSelect, parent, false);
                    }
                    else
                    {
                        view = (ViewGroup)mInflater.Inflate(Resource.Layout.RoundedItem, parent, false);
                    }
                }
                else
                {
                    view = (ViewGroup)convertView;
                }

                StreamItem item = GetItem(position);

                RoundedImageView iv = (view.FindViewById<RoundedImageView>(Resource.Id.imageView1));
                iv.IsOval = exampleType == ExampleType.Oval;
                iv.SetImageBitmap(item.Bitmap);
                iv.SetScaleType(item.ScaleType);
                iv.TileModeX = item.TileMode;
                iv.TileModeY = item.TileMode;

                view.FindViewById<TextView>(Resource.Id.textView1).Text = item.Line1;
                view.FindViewById<TextView>(Resource.Id.textView2).Text = item.Line2;
                view.FindViewById<TextView>(Resource.Id.textView3).Text = item.ScaleType.ToString();

                return view;
            }
        }
    }
}
