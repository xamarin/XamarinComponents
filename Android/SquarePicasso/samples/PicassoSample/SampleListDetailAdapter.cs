using System.Collections.Generic;
using Android.Widget;
using Android.Content;
using Android.Views;

using Square.Picasso;

namespace PicassoSample
{
    public class SampleListDetailAdapter : BaseAdapter<string>
    {
        private readonly Context context;
        private readonly List<string> urls = new List<string>();

        public SampleListDetailAdapter(Context context)
        {
            this.context = context;

            urls.AddRange(Data.Urls);
        }

        public override View GetView(int position, View view, ViewGroup parent)
        {
            ViewHolder holder;
            if (view == null)
            {
                view = LayoutInflater.From(context).Inflate(Resource.Layout.sample_list_detail_item, parent, false);
                holder = new ViewHolder
                {
                    image = view.FindViewById<ImageView>(Resource.Id.photo),
                    text = view.FindViewById<TextView>(Resource.Id.url)
                };
                view.Tag = holder;
            }
            else
            {
                holder = (ViewHolder)view.Tag;
            }

            // Get the image URL for the current position.
            string url = this[position];

            holder.text.Text = url;

            // Trigger the download of the URL asynchronously into the image view.
            Picasso.Get()
                   .Load(url)
                   .Placeholder(Resource.Drawable.placeholder)
                   .Error(Resource.Drawable.error)
                   .ResizeDimen(Resource.Dimension.list_detail_image_size, Resource.Dimension.list_detail_image_size)
                   .CenterInside()
                   .Tag(context)
                   .Into(holder.image);

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

        private class ViewHolder : Java.Lang.Object
        {
            internal ImageView image;
            internal TextView text;
        }
    }
}
