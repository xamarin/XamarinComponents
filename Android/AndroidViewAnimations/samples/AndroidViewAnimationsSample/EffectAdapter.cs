using System;
using Android.Content;
using Android.Views;
using Android.Widget;

using AndroidViewAnimations;

namespace AndroidViewAnimationsSample
{
	public class EffectAdapter : BaseAdapter<Techniques>
	{
		private readonly Context context;

		public EffectAdapter (Context context)
		{
			this.context = context;
		}

		public override int Count {
			get {
				return Techniques.Values ().Length;
			}
		}

		public override Techniques this [int index] {
			get {
				return Techniques.Values () [index];
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var technique = this [position];
			var name = technique.Name ();

			var view = convertView ?? LayoutInflater.From (context).Inflate (Resource.Layout.item, null);
			var tv = view.FindViewById<TextView> (Resource.Id.list_item_text);
			tv.Text = name;
			view.Tag = technique;

			return view;
		}
	}
}
