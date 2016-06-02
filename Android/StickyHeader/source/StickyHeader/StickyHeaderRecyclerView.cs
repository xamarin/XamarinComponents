using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;
using StickyHeader.Animator;

namespace StickyHeader
{
	public class StickyHeaderRecyclerView : StickyHeaderView
	{
		public StickyHeaderRecyclerView(Context context, View header, int minHeightHeader, HeaderAnimator headerAnimator, RecyclerView recyclerView, bool preventTouchBehindHeader)
			: base(context, header, recyclerView, minHeightHeader, headerAnimator, preventTouchBehindHeader)
		{
			SetupItemDecorator();

			// scroll events
			recyclerView.AddOnScrollListener(new RecyclerScrollListener(this));
		}

		protected override void SetHeightHeader(int value)
		{
			base.SetHeightHeader(value);
			recyclerView.InvalidateItemDecorations();
		}

		private RecyclerView recyclerView
		{
			get { return (RecyclerView)view; }
		}

		private void SetupItemDecorator()
		{
			var layoutManager = recyclerView.GetLayoutManager();
			if (layoutManager is StaggeredGridLayoutManager)
			{
				var manager = layoutManager as StaggeredGridLayoutManager;
				switch (manager.Orientation)
				{
					case LinearLayoutManager.Vertical:
						recyclerView.AddItemDecoration(new StaggeredGridItemDecoration(this));
						break;
					case LinearLayoutManager.Horizontal:
						//TODO
						break;
				}
			}
			else if (layoutManager is GridLayoutManager)
			{
				var manager = layoutManager as GridLayoutManager;
				switch (manager.Orientation)
				{
					case LinearLayoutManager.Vertical:
						recyclerView.AddItemDecoration(new GridItemDecoration(this));
						break;
					case LinearLayoutManager.Horizontal:
						//TODO
						break;
				}
			}
			else if (layoutManager is LinearLayoutManager)
			{
				var manager = layoutManager as LinearLayoutManager;
				switch (manager.Orientation)
				{
					case LinearLayoutManager.Vertical:
						recyclerView.AddItemDecoration(new LinearItemDecoration(this));
						break;
					case LinearLayoutManager.Horizontal:
						//TODO
						break;
				}
			}
		}

		private class StaggeredGridItemDecoration : RecyclerView.ItemDecoration
		{
			private readonly StickyHeaderRecyclerView headerView;

			public StaggeredGridItemDecoration(StickyHeaderRecyclerView headerView)
			{
				this.headerView = headerView;
			}

			public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
			{
				base.GetItemOffsets(outRect, view, parent, state);

				var layoutManager = (StaggeredGridLayoutManager)parent.GetLayoutManager();
				int position = parent.GetChildAdapterPosition(view);
				if (position < layoutManager.SpanCount)
				{
					outRect.Top = headerView.heightHeader;
				}
			}
		}

		private class GridItemDecoration : RecyclerView.ItemDecoration
		{
			private readonly StickyHeaderRecyclerView headerView;

			public GridItemDecoration(StickyHeaderRecyclerView headerView)
			{
				this.headerView = headerView;
			}

			public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
			{
				base.GetItemOffsets(outRect, view, parent, state);

				var layoutManager = (GridLayoutManager)parent.GetLayoutManager();
				int position = parent.GetChildAdapterPosition(view);
				if (position < layoutManager.SpanCount)
				{
					outRect.Top = headerView.heightHeader;
				}
			}
		}

		private class LinearItemDecoration : RecyclerView.ItemDecoration
		{
			private readonly StickyHeaderRecyclerView headerView;

			public LinearItemDecoration(StickyHeaderRecyclerView headerView)
			{
				this.headerView = headerView;
			}

			public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
			{
				base.GetItemOffsets(outRect, view, parent, state);

				int position = parent.GetChildAdapterPosition(view);
				if (position == 0)
				{
					outRect.Top = headerView.heightHeader;
				}
			}
		}

		private class RecyclerScrollListener : RecyclerView.OnScrollListener
		{
			private readonly StickyHeaderRecyclerView headerView;
			private int scrolledY;

			public RecyclerScrollListener(StickyHeaderRecyclerView headerView)
			{
				this.headerView = headerView;
				this.scrolledY = int.MinValue;
			}

			public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
			{
				base.OnScrolled(recyclerView, dx, dy);
				
				if (scrolledY == int.MinValue)
				{
					var child = recyclerView.GetChildAt(0);
					var positionFirstItem = recyclerView.GetChildAdapterPosition(child);
					var heightDecorator = positionFirstItem == 0 ? 0 : headerView.heightHeader;
					var offset = recyclerView.ComputeVerticalScrollOffset();
					scrolledY = offset + heightDecorator;
				}
				else
				{
					scrolledY += dy;
				}

				headerView.headerAnimator.OnScroll(-scrolledY);
			}
		}
	}
}