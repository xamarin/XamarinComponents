using System.Collections.Generic;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using AndroidViewAnimations;

using AndroidSwipeLayout;
using AndroidSwipeLayout.Adapters;

namespace AndroidSwipeLayoutSample.Adapters
{
	public class RecyclerViewAdapter : RecyclerSwipeAdapter
	{
		private Context context;
		private List<string> dataset;

		public RecyclerViewAdapter (Context context, List<string> objects)
		{
			this.context = context;
			this.dataset = objects;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
		{
			var view = LayoutInflater.From (parent.Context).Inflate (Resource.Layout.recyclerview_item, parent, false);
			var holder = new SimpleViewHolder (view);
			holder.SwipeLayout.SetShowMode (SwipeLayout.ShowMode.LayDown);
			holder.SwipeLayout.Opened += (sender, e) => {
				YoYo.With (Techniques.Tada)
					.Duration (500)
					.Delay (100)
					.PlayOn (e.Layout.FindViewById (Resource.Id.trash));
			};
			holder.SwipeLayout.DoubleClick += (sender, e) => {
				Toast.MakeText (context, "DoubleClick " + holder.AdapterPosition, ToastLength.Short).Show ();
			};
			holder.ButtonDelete.Click += (sender, e) => {
				MItemManager.RemoveShownLayouts (holder.SwipeLayout);
				dataset.RemoveAt (holder.AdapterPosition);
				NotifyItemRemoved (holder.AdapterPosition);
				NotifyItemRangeChanged (holder.AdapterPosition, dataset.Count);
				MItemManager.CloseAllItems ();
				Toast.MakeText (holder.ButtonDelete.Context, "Deleted " + holder.TextViewData.Text + "!", ToastLength.Short).Show ();
			};
			return holder;
		}

		public override void OnBindViewHolder (RecyclerView.ViewHolder viewHolder, int position)
		{
			var holder = (SimpleViewHolder)viewHolder;
			var item = dataset [position];
			holder.TextViewPos.Text = (position + 1) + ".";
			holder.TextViewData.Text = item;
			MItemManager.BindView (holder.ItemView, position);
		}

		public override int ItemCount {
			get{ return dataset.Count; }
		}

		public override int GetSwipeLayoutResourceId (int position)
		{
			return Resource.Id.swipe;
		}

		private  class SimpleViewHolder : RecyclerView.ViewHolder
		{
			public SwipeLayout SwipeLayout;
			public TextView TextViewPos;
			public TextView TextViewData;
			public Button ButtonDelete;

			public SimpleViewHolder (View itemView)
				: base (itemView)
			{
				SwipeLayout = itemView.FindViewById<SwipeLayout> (Resource.Id.swipe);
				TextViewPos = itemView.FindViewById<TextView> (Resource.Id.position);
				TextViewData = itemView.FindViewById<TextView> (Resource.Id.text_data);
				ButtonDelete = itemView.FindViewById<Button> (Resource.Id.delete);

				itemView.Click += (sender, e) => {
					Toast.MakeText (ItemView.Context, "OnItemSelected: " + TextViewData.Text, ToastLength.Short).Show ();
				};
			}
		}
	}
}
