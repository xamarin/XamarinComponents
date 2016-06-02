using Android.App;
using Android.OS;
using Android.Widget;

using AndroidSwipeLayout;

namespace AndroidSwipeLayoutSample
{
	[Activity (Label = "Nested Example")]
	public class NestedExample : BaseActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.complicate_layout);

			var swipeLayout = FindViewById<SwipeLayout> (Resource.Id.test_swipe_swipe);
			swipeLayout.DoubleClick += (sender, e) => {
				Toast.MakeText (ApplicationContext, "DoubleClick", ToastLength.Short).Show ();
			};
			swipeLayout.FindViewById (Resource.Id.trash).Click += (sender, e) => {
				Toast.MakeText (ApplicationContext, "Click", ToastLength.Short).Show ();
			};
		}
	}
}
