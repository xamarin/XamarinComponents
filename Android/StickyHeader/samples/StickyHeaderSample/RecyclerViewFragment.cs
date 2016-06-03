using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using StickyHeader;

namespace StickyHeaderSample
{
	public class RecyclerViewFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.RecyclerViewLayout, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			// header
			var recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerview);
			recyclerView.SetLayoutManager(new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false));
			recyclerView.HasFixedSize = true;
			StickyHeaderBuilder
				.StickTo(recyclerView)
				.SetHeader(Resource.Id.header, (ViewGroup) View)
				.SetMinHeightDimension(Resource.Dimension.min_height_header)
				.PreventTouchBehindHeader()
				.Apply();

			// items
			var elements = new List<string>(500);
			for (int i = 0; i < 500; i++)
			{
				elements.Add("row " + i);
			}
			recyclerView.SetAdapter(new SimpleRecyclerAdapter(Activity, elements));
		}

		private class SimpleRecyclerAdapter : RecyclerView.Adapter
		{
			private readonly Context context;
			private readonly List<string> elements;

			public SimpleRecyclerAdapter(Context context, List<string> elements)
			{
				this.context = context;
				this.elements = elements;
			}

			public override int ItemCount
			{
				get { return elements.Count; }
			}

			public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int i)
			{
				LayoutInflater layoutInflater = LayoutInflater.From(context);
				View view = layoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, viewGroup, false);
				return new SimpleViewHolder(this, elements, view);
			}

			public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int i)
			{
				var holder = (SimpleViewHolder) viewHolder;
				holder.SetText(elements[i]);
			}

			private class SimpleViewHolder : RecyclerView.ViewHolder
			{
				private readonly TextView textView;

				public SimpleViewHolder(SimpleRecyclerAdapter adapter, List<string> elements, View itemView)
					: base(itemView)
				{
					textView = (TextView) itemView.FindViewById(Android.Resource.Id.Text1);

					textView.Click += delegate
					{
						var pos = AdapterPosition;
						elements.Insert(pos, "(+) row " + pos);
						adapter.NotifyItemInserted(pos);
					};
					textView.LongClick += delegate
					{
						var pos = AdapterPosition;
						elements.RemoveAt(pos);
						adapter.NotifyItemRemoved(pos);
					};
				}

				public void SetText(string text)
				{
					textView.Text = text;
				}
			}
		}
	}
}