using Android.Content;
using Android.Support.V7.App;
using Android.Views;

namespace AndroidSwipeLayoutSample
{
	public abstract class BaseActivity : AppCompatActivity
	{
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.my, menu);
			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_listview) {
				StartActivity (new Intent (this, typeof(ListViewExample)));
				return true;
			} else if (id == Resource.Id.action_gridview) {
				StartActivity (new Intent (this, typeof(GridViewExample)));
				return true;
			} else if (id == Resource.Id.action_nested) {
				StartActivity (new Intent (this, typeof(NestedExample)));
				return true;
			} else if (id == Resource.Id.action_recycler) {
				StartActivity (new Intent (this, typeof(RecyclerViewExample)));
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}
	}
}
