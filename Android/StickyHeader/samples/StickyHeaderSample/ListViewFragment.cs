using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using StickyHeader;

namespace StickyHeaderSample
{
	public class ListViewFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.ListViewLayout, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			// header
			var container = View.FindViewById<FrameLayout>(Resource.Id.layout_container);
			var listView = container.FindViewById<ListView>(Resource.Id.listview);
			StickyHeaderBuilder
				.StickTo(listView)
				.SetHeader(Resource.Id.header, container)
				.SetMinHeight(250)
				.PreventTouchBehindHeader()
				.Apply();

			// items
			var elements = new string[500];
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = "row " + i;
			}

			listView.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, elements);
		}
	}
}