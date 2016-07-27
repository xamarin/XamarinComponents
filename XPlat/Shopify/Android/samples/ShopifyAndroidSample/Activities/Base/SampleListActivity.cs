using Android.OS;
using Android.Widget;

namespace ShopifyAndroidSample.Activities.Base
{
	// Base class for activities with list views in the app.
	public class SampleListActivity : SampleActivity
	{
		protected ListView listView;
		protected bool isFetching;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			OnCreate(savedInstanceState, Resource.Layout.list_activity);
		}

		protected void OnCreate(Bundle savedInstanceState, int layoutId)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(layoutId);

			listView = FindViewById<ListView>(Resource.Id.list_view);
			isFetching = false;
		}
	}
}
