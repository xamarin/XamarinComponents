using Android.Content;
using Android.Views;
using Android.Widget;
using AndroidViewAnimations;

using AndroidSwipeLayout;
using AndroidSwipeLayout.Adapters;

namespace AndroidSwipeLayoutSample.Adapters
{
	public class ListViewAdapter : BaseSwipeAdapter
	{
		private Context context;

		public ListViewAdapter (Context context)
		{
			this.context = context;
		}

		public override int GetSwipeLayoutResourceId (int position)
		{
			return Resource.Id.swipe;
		}

		public override View GenerateView (int position, ViewGroup parent)
		{
			var view = LayoutInflater.From (context).Inflate (Resource.Layout.listview_item, null);
			var swipeLayout = view.FindViewById<SwipeLayout> (GetSwipeLayoutResourceId (position));
			swipeLayout.Opened += (sender, e) => {
				YoYo.With (Techniques.Tada)
					.Duration (500)
					.Delay (100)
					.PlayOn (e.Layout.FindViewById (Resource.Id.trash));
			};
			swipeLayout.DoubleClick += (sender, e) => {
				var pos = (int)view.Tag;
				Toast.MakeText (context, "DoubleClick " + pos, ToastLength.Short).Show ();	
			};
			view.FindViewById (Resource.Id.delete).Click += (sender, e) => {
				var pos = (int)view.Tag;
				Toast.MakeText (context, "click delete " + pos, ToastLength.Short).Show ();
			};
			return view;
		}

		public override void FillValues (int position, View convertView)
		{
			convertView.Tag = position;

			var t = (TextView)convertView.FindViewById (Resource.Id.position);
			t.Text = (position + 1) + ".";
		}

		public override int Count {
			get {
				return 50;
			}
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
