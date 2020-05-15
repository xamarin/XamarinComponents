using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Widget;

using Square.Picasso;
using Android.Views;

namespace PicassoSample
{
    public class SampleGridViewAdapter : BaseAdapter<string>
    {
        private readonly Context context;
        private readonly List<string> urls = new List<string>();

        public SampleGridViewAdapter(Context context)
        {
            this.context = context;

            // Ensure we get a different ordering of images on each run.
            var copy = Data.Urls.ToList();
            Shuffle(copy);

            // Triple up the list.
            urls.AddRange(copy);
            urls.AddRange(copy);
            urls.AddRange(copy);
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            SquaredImageView view = convertView as SquaredImageView;
            if (view == null)
            {
                view = new SquaredImageView(context);
                view.SetScaleType(ImageView.ScaleType.CenterCrop);
            }

            // Get the image URL for the current position.
            string url = this[position];

            // Trigger the download of the URL asynchronously into the image view.
            Picasso.Get()
                   .Load(url)
                   .Placeholder(Resource.Drawable.placeholder)
                   .Error(Resource.Drawable.error)
                   .Fit()
                   .Tag(context)
                   .Into(view);

            return view;
        }

        public override int Count
        {
            get { return urls.Count; }
        }

        public override string this[int position]
        {
            get { return urls[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
