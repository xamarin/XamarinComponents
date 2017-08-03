using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;

namespace YouTubePlayerSample.Adapters
{
	public class DemoArrayAdapter : ArrayAdapter<DemoListViewItem>
	{
		private readonly LayoutInflater inflater;

		public DemoArrayAdapter(Context context, int textViewResourceId, List<DemoListViewItem> objects)
				  : base(context, textViewResourceId, objects)
		{
			inflater = LayoutInflater.From(context);
		}

		public override View GetView(int position, View view, ViewGroup parent)
		{
			if (view == null)
			{
				view = inflater.Inflate(Resource.Layout.list_item, null);
			}

			var textView = view.FindViewById<TextView>(Resource.Id.list_item_text);
			textView.Text = GetItem(position).Title;
			var disabledText = view.FindViewById<TextView>(Resource.Id.list_item_disabled_text);
			disabledText.Text = GetItem(position).DisabledText;

			if (IsEnabled(position))
			{
				disabledText.Visibility = ViewStates.Invisible;
				textView.SetTextColor(Color.White);
			}
			else
			{
				disabledText.Visibility = ViewStates.Visible;
				textView.SetTextColor(Color.Gray);
			}

			return view;
		}

		public override bool AreAllItemsEnabled() => true;

		public override bool IsEnabled(int position) => GetItem(position).IsEnabled;

		public bool AnyDisabled
		{
			get
			{
				for (int i = 0; i < Count; i++)
				{
					if (!IsEnabled(i))
					{
						return true;
					}
				}
				return false;
			}
		}
	}
}
