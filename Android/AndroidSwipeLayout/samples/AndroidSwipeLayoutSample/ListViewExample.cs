using System;
using Android.App;
using Android.OS;
using Android.Widget;

using AndroidSwipeLayout;
using AndroidSwipeLayout.Util;

using AndroidSwipeLayoutSample.Adapters;

namespace AndroidSwipeLayoutSample
{
	[Activity (Label = "ListView Example")]
	public class ListViewExample : BaseActivity
	{
		private ListView listView;
		private ListViewAdapter adapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.listview);

			// The following comment is the sample usage of ArraySwipeAdapter.
			//var adapterData = new [] {
			//	"Activity", "Service", "Content Provider", "Intent", "BroadcastReceiver", "ADT", "Sqlite3", "HttpClient",
			//	"DDMS", "Android Studio", "Fragment", "Loader", "Activity", "Service", "Content Provider", "Intent",
			//	"BroadcastReceiver", "ADT", "Sqlite3", "HttpClient", "Activity", "Service", "Content Provider", "Intent",
			//	"BroadcastReceiver", "ADT", "Sqlite3", "HttpClient"
			//};
			//listView.Adapter = new ArraySwipeAdapter (this, Resource.Layout.listview_item, Resource.Id.position, adapterData);

			listView = FindViewById<ListView> (Resource.Id.listview);
			adapter = new ListViewAdapter (this);
			listView.Adapter = adapter;
			adapter.Mode = Attributes.Mode.Single;
			listView.ItemClick += (sender, e) => {
				((SwipeLayout)(listView.GetChildAt (e.Position - listView.FirstVisiblePosition))).Open (true);
			};
			listView.Touch += (sender, e) => {
				Console.WriteLine ("ListView: OnTouch");
				e.Handled = false;
			};
			listView.ItemLongClick += (sender, e) => {
				Toast.MakeText (this, "OnItemLongClickListener", ToastLength.Short).Show ();
				e.Handled = true;
			};
			listView.ScrollStateChanged += (sender, e) => {
				Console.WriteLine ("ListView: OnScrollStateChanged");
			};
			listView.ItemSelected += (sender, e) => {
				Console.WriteLine ("ListView: OnItemSelected:" + e.Position);
			};
			listView.NothingSelected += (sender, e) => {
				Console.WriteLine ("ListView: OnNothingSelected:");
			};
		}
	}
}
