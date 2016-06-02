using Android.Content;
using Android.Views;
using Android.Widget;

using AndroidSwipeLayout.Adapters;

namespace AndroidSwipeLayoutSample.Adapters
{
	public class GridViewAdapter : BaseSwipeAdapter
	{
		private Context context;

		public GridViewAdapter (Context context)
		{
			this.context = context;
		}

		public override int GetSwipeLayoutResourceId (int position)
		{
			return Resource.Id.swipe;
		}

		public override View GenerateView (int position, ViewGroup parent)
		{
			var view = LayoutInflater.From (context).Inflate (Resource.Layout.grid_item, null);
			view.FindViewById (Resource.Id.trash).Click += (sender, e) => {
				var pos = (int)view.Tag;
				Toast.MakeText (context, "click delete " + pos, ToastLength.Short).Show ();
			};
			return view;
		}

		public override void FillValues (int position, View convertView)
		{
			convertView.Tag = position;

			var t = convertView.FindViewById<TextView> (Resource.Id.position);
			t.Text = (position + 1) + ".";
		}

		public override int Count {
			get { return 50; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return position;
		}
	}
}
