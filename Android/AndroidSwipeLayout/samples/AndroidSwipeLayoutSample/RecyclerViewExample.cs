using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using RecyclerViewAnimators.Animators;

using AndroidSwipeLayout.Util;
using AndroidSwipeLayoutSample.Adapters;
using AndroidSwipeLayoutSample.Adapters.Utils;

namespace AndroidSwipeLayoutSample
{
	[Activity (Label = "RecyclerView Example")]
	public class RecyclerViewExample : BaseActivity
	{
		// RecyclerView: The new recycler view replaces the list view. Its more modular and therefore we
		// must implement some of the functionality ourselves and attach it to our recyclerview.
		//
		// 1) Position items on the screen: This is done with LayoutManagers
		// 2) Animate & Decorate views: This is done with ItemAnimators & ItemDecorators
		// 3) Handle any touch events apart from scrolling: This is now done in our adapter's ViewHolder

		private RecyclerView recyclerView;
		private RecyclerViewAdapter adapter;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.recyclerview);

			recyclerView = FindViewById<RecyclerView> (Resource.Id.recycler_view);

			// Layout Managers:
			recyclerView.SetLayoutManager (new LinearLayoutManager (this));

			// Item Decorator:
			recyclerView.AddItemDecoration (new DividerItemDecoration (Resources.GetDrawable (Resource.Drawable.divider)));
			recyclerView.SetItemAnimator (new FadeInLeftAnimator ());

			// Adapter:
			var adapterData = new [] {
				"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", 
				"Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", 
				"Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", 
				"Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", 
				"Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", 
				"New Hampshire", "New Jersey", "New Mexico", "New York", 
				"North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", 
				"Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", 
				"Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", 
				"West Virginia", "Wisconsin", "Wyoming"
			};
			adapter = new RecyclerViewAdapter (this, adapterData.ToList ());
			adapter.Mode = Attributes.Mode.Single;
			recyclerView.SetAdapter (adapter);

			// Listeners
			recyclerView.SetOnScrollListener (new ScrollListener ());
		}

		/// <summary>
		/// Substitute for our onScrollListener for RecyclerView
		/// </summary>
		private class ScrollListener : RecyclerView.OnScrollListener
		{
			public override void OnScrollStateChanged (RecyclerView recyclerView, int newState)
			{
				base.OnScrollStateChanged (recyclerView, newState);
				Console.WriteLine ("RecyclerView: OnScrollStateChanged");
			}

			public override void OnScrolled (RecyclerView recyclerView, int dx, int dy)
			{
				base.OnScrolled (recyclerView, dx, dy);
				// Could hide open views here if you wanted.
			}
		}
	}
}
