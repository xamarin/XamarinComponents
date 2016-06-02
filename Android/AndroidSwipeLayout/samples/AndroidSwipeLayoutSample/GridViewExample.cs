using System;
using Android.App;
using Android.OS;
using Android.Widget;

using AndroidSwipeLayout.Util;

using AndroidSwipeLayoutSample.Adapters;

namespace AndroidSwipeLayoutSample
{
	[Activity (Label = "GridView Example")]
	public class GridViewExample : BaseActivity
	{
		private GridView gridView;
		private GridViewAdapter adapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.gridview);

			gridView = FindViewById<GridView> (Resource.Id.gridview);
			adapter = new GridViewAdapter (this);
			adapter.Mode = Attributes.Mode.Multiple;
			gridView.Adapter = adapter;
			gridView.Selected = false;
			gridView.ItemLongClick += (sender, e) => {
				Console.WriteLine ("GridView: OnItemLongClick:" + e.Position);
				e.Handled = false;
			};
			gridView.ItemClick += (sender, e) => {
				Console.WriteLine ("GridView: OnItemClick:" + e.Position);
			};

			gridView.ItemSelected += (sender, e) => {
				Console.WriteLine ("GridView: OnItemSelected:" + e.Position);
			};
		}
	}
}
