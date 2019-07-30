using System;
using Android.App;
using Android.Views;
using Android.Widget;

namespace PlacesSample.Models {
	public class PlacesAdapter : BaseAdapter<PlaceData> {
		Activity context; 
		PlaceData [] placesData;

		public PlacesAdapter (Activity context, PlaceData [] placesData)
		{
			this.context = context;
			this.placesData = placesData;
		}

		public override PlaceData this [int position] => placesData [position];

		public override int Count => placesData.Length;

		public override long GetItemId (int position) => position;

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var view = convertView ?? context.LayoutInflater.Inflate (Resource.Layout.list_view_place_item, null);

			var placeData = placesData [position];
			view.FindViewById<TextView> (Resource.Id.txtName).Text = placeData.Name;
			view.FindViewById<TextView> (Resource.Id.txtAddress).Text = placeData.Address;

			return view;
		}
	}
}
