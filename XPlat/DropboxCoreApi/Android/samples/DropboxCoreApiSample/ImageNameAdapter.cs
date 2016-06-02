using System;
using Android.Widget;
using Android.App;
using Android.Views;

namespace DropboxCoreApiSample
{
	public class ImageNameAdapter : BaseAdapter<string>
	{
		public string[] ImagesName { get; private set; }

		public string[] ImagesPath { get; private set; }

		Activity context;

		public ImageNameAdapter (Activity cont, string[] names, string[] paths)
		{
			context = cont;
			ImagesName = names;
			ImagesPath = paths;
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
				view = context.LayoutInflater.Inflate (Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = ImagesName [position];

			return view;
		}

		public override int Count {
			get { return ImagesName.Length; }
		}

		public override string this [int index] {
			get { return ImagesName [index]; }
		}

		#endregion
	}
}

