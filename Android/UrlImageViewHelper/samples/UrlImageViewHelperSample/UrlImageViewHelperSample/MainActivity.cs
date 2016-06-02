using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace UrlImageViewHelperSample
{
	[Activity (Label = "UrlImageViewHelper", MainLauncher = true)]
	public class MainActivity : ListActivity
	{
		PlaceCageAdapter adapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			adapter = new PlaceCageAdapter (this);

			ListView.Adapter = adapter;
		}
	}

	public class PlaceCageAdapter : BaseAdapter<string>
	{
		public PlaceCageAdapter (Activity context)
		{
			Context = context;
		}

		public Activity Context { get;set; }

		public override long GetItemId (int position) { return position; }
		public override string this [int index] { get { return Cats.Urls [index]; } }
		public override int Count { get { return Cats.Urls.Length; } }

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView
			           ?? Context.LayoutInflater.Inflate (Resource.Layout.ListItem, parent, false);

			var img = view.FindViewById<ImageView> (Resource.Id.imageView);
			var txt = view.FindViewById<TextView> (Resource.Id.textView);

			var url = Cats.Urls [position];

			txt.Text = url;

			Koush.UrlImageViewHelper.SetUrlDrawable (img, url);

			return view;
		}
	}
}


