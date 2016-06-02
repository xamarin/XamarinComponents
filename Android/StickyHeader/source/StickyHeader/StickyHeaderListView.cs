using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using StickyHeader.Animator;

namespace StickyHeader
{
	public class StickyHeaderListView : StickyHeaderView
	{
		protected readonly View fakeHeader;

		public StickyHeaderListView(Context context, View header, int minHeightHeader, HeaderAnimator headerAnimator, ListView listView, bool preventTouchBehindHeader)
			: base(context, header, listView, minHeightHeader, headerAnimator, preventTouchBehindHeader)
		{
			// fake header
			var lp = new AbsListView.LayoutParams(ViewGroup.LayoutParams.MatchParent, heightHeader);
			fakeHeader = new Space(context)
			{
				LayoutParameters = lp
			};
			this.listView.AddHeaderView(fakeHeader);
		
			// scroll events
			this.listView.Scroll += (sender, e) =>
			{
				var scrolledY = -CalculateScrollYList();
				headerAnimator.OnScroll(scrolledY);
			};
		}

		private ListView listView
		{
			get { return (ListView)view; }
		}

		protected override void SetHeightHeader(int value)
		{
			base.SetHeightHeader(value);

			if (fakeHeader != null)
			{
				var layoutParam = fakeHeader.LayoutParameters;
				layoutParam.Height = heightHeader;
				fakeHeader.LayoutParameters = layoutParam;
			}
		}

		protected virtual int CalculateScrollYList()
		{
			View c = listView.GetChildAt(0);
			if (c == null)
			{
				return 0;
			}

			//TODO support more than 1 header?

			int firstVisiblePosition = listView.FirstVisiblePosition;
			int top = c.Top;

			int headerHeight = 0;
			if (firstVisiblePosition >= 1)
			{
				//TODO >= number of header
				headerHeight = heightHeader;
			}

			return -top + firstVisiblePosition*c.Height + headerHeight;
		}
	}
}