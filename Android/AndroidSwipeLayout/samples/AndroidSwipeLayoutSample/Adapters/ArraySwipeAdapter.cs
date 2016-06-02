using System.Collections;
using Android.Content;

using AndroidSwipeLayout.Adapters;

namespace AndroidSwipeLayoutSample.Adapters
{
	public class ArraySwipeAdapterSample : ArraySwipeAdapter
	{
		public ArraySwipeAdapterSample (Context context, int resource, int textViewResourceId, IList data)
			: base (context, resource, textViewResourceId, data)
		{
		}

		public override int GetSwipeLayoutResourceId (int position)
		{
			return Resource.Id.swipe;
		}
	}
}
