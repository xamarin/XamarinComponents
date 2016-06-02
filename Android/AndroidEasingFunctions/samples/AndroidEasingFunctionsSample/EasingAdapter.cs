using System;
using Android.Content;
using Android.Views;
using Android.Widget;

using AndroidEasingFunctions;

namespace AndroidEasingFunctionsSample
{
	public class EasingAdapter : BaseAdapter<Skill>
	{
		private readonly Context context;

		public EasingAdapter (Context context)
		{
			this.context = context;
		}

		public override int Count {
			get {
				return Skill.Values ().Length;
			}
		}

		public override Skill this [int index] {
			get {
				return Skill.Values () [index];
			}
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			var skill = this [position];
			var name = skill.Name ();

			var view = convertView ?? LayoutInflater.From (context).Inflate (Resource.Layout.item, null);
			var tv = view.FindViewById<TextView> (Resource.Id.list_item_text);
			tv.Text = name;
			view.Tag = skill;

			return view;
		}
	}
}
