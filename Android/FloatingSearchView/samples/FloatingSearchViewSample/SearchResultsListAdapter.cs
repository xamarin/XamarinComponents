using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

using FloatingSearchViews.Utils;

namespace FloatingSearchViewSample
{
	public class SearchResultsListAdapter : RecyclerView.Adapter
	{
		private List<ColorModel> mDataSet = new List<ColorModel>();
		private int mLastAnimatedItemPosition = -1;

		public class ViewHolder : RecyclerView.ViewHolder
		{
			public readonly TextView mColorName;
			public readonly TextView mColorValue;
			public readonly View mTextContainer;

			public ViewHolder(View view)
				: base(view)
			{
				mColorName = view.FindViewById<TextView>(Resource.Id.color_name);
				mColorValue = view.FindViewById<TextView>(Resource.Id.color_value);
				mTextContainer = view.FindViewById(Resource.Id.text_container);
			}
		}

		public void SwapData(List<ColorModel> mNewDataSet)
		{
			mDataSet = mNewDataSet;
			NotifyDataSetChanged();
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.search_results_list_item, parent, false);
			return new ViewHolder(view);
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var vh = (ViewHolder)holder;

			var colorSuggestion = mDataSet[position];
			vh.mColorName.Text = colorSuggestion.Name;
			vh.mColorValue.Text = colorSuggestion.Hex;

			var color = Color.ParseColor(colorSuggestion.Hex);
			vh.mColorName.SetTextColor(color);
			vh.mColorValue.SetTextColor(color);

			if (mLastAnimatedItemPosition < position)
			{
				AnimateItem(vh.ItemView);
				mLastAnimatedItemPosition = position;
			}
		}

		public override int ItemCount => mDataSet.Count;

		private void AnimateItem(View view)
		{
			view.TranslationY = Util.GetScreenHeight((Activity)view.Context);
			view.Animate()
				.TranslationY(0)
				.SetInterpolator(new DecelerateInterpolator(3.0f))
				.SetDuration(700)
				.Start();
		}
	}
}
