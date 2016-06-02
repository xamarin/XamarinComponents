using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace StickyHeaderSample
{
	public class MainFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.HomeLayout, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			var listView = view.FindViewById<ListView>(Resource.Id.listview);
			var toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);

			toolbar.Title = "Sticky Header";
			
			// fragments
			var fragments = new Dictionary<string, Fragment>
			{
				{"List View Sticky Header",new ListViewFragment()},
				{"Parallax Simple Sticky Header",new ParallaxFragment()},
				{"Custom Animation Header",new CustomHeaderFragment()},
				{"Recycler View Header",new RecyclerViewFragment()},
				{"Scroll View Header",new ScrollViewFragment()}
			};
			var items = fragments.Keys.ToArray();

			// items
			listView.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, items);

			// item selection
			listView.ItemClick += (sender, e) =>
			{
				var fragment = fragments[items[e.Position]];
				((MainActivity)Activity).LoadFragment(fragment);
			};
		}
	}
}