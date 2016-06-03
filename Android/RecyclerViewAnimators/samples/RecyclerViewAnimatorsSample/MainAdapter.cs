using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Square.Picasso;

namespace RecyclerViewAnimatorsSample
{
	public class MainAdapter : RecyclerView.Adapter
	{
		private readonly Context context;
		private readonly List<string> dataset;

		public MainAdapter (Context context, List<string> dataset)
		{
			this.context = context;
			this.dataset = dataset;
		}

		public override int ItemCount {
			get {
				return dataset.Count;
			}
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			var v = LayoutInflater.From (context).Inflate (Resource.Layout.layout_list_item, parent, false);
			return new ViewHolder (v);
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
		{
			var h = (ViewHolder)holder;
			Picasso.With (context).Load (Resource.Drawable.chip).Into (h.Image);
			h.Text.Text = dataset [position];
		}

		public void Remove (int position)
		{
			dataset.RemoveAt (position);
			NotifyItemRemoved (position);
		}

		public void Add (string text, int position)
		{
			dataset.Insert (position, text);
			NotifyItemInserted (position);
		}

		private class ViewHolder : RecyclerView.ViewHolder
		{
			public ImageView Image { get; private set; }

			public TextView Text { get; private set; }

			public ViewHolder (View itemView)
				: base (itemView)
			{
				Image = itemView.FindViewById<ImageView> (Resource.Id.image);
				Text = itemView.FindViewById<TextView> (Resource.Id.text);
			}
		}
	}
}
