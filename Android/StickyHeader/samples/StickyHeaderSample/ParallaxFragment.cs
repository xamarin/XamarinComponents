using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using StickyHeader;
using StickyHeader.Animator;

namespace StickyHeaderSample
{
	public class ParallaxFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.ParallaxLayout, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			// header
			var listView = view.FindViewById<ListView>(Resource.Id.listview);
			StickyHeaderBuilder
				.StickTo(listView)
				.SetHeader(Resource.Id.header, (ViewGroup) View)
				.SetMinHeightDimension(Resource.Dimension.min_height_header)
				.SetAnimator(() =>
				{
					var image = View.FindViewById(Resource.Id.header_image);
					return AnimatorBuilder
						.Create()
						.ApplyVerticalParallax(image);
				})
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