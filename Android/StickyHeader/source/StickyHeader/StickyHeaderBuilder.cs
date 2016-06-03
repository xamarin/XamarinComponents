using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StickyHeader.Animator;

namespace StickyHeader
{
	public abstract class StickyHeaderBuilder
	{
		private readonly Context context;

		private HeaderAnimator animator;
		private View header;
		private int minHeight;
		private bool preventTouchBehindHeader;

		private StickyHeaderBuilder(Context context)
		{
			this.context = context;

			this.animator = null;
			this.header = null;
			this.minHeight = 0;
		}

		public static ListViewBuilder StickTo(ListView listView)
		{
			return new ListViewBuilder(listView);
		}

		public static RecyclerViewBuilder StickTo(RecyclerView recyclerView)
		{
			return new RecyclerViewBuilder(recyclerView);
		}

		public static ScrollViewBuilder StickTo(ScrollView scrollView)
		{
			return new ScrollViewBuilder(scrollView);
		}

		public virtual StickyHeaderBuilder SetHeader(int idResource, ViewGroup view)
		{
			this.header = view.FindViewById(idResource);
			return this;
		}

		public virtual StickyHeaderBuilder SetHeader(View headerView)
		{
			this.header = headerView;
			return this;
		}

		public virtual StickyHeaderBuilder PreventTouchBehindHeader()
		{
			this.preventTouchBehindHeader = true;
			return this;
		}

		public virtual StickyHeaderBuilder SetMinHeightDimension(int dimensionResource)
		{
			this.minHeight = context.Resources.GetDimensionPixelSize(dimensionResource);
			return this;
		}

		public virtual StickyHeaderBuilder SetMinHeight(int pixels)
		{
			this.minHeight = pixels;
			return this;
		}

		public virtual StickyHeaderBuilder SetAnimator(HeaderAnimator headerAnimator)
		{
			this.animator = headerAnimator;
			return this;
		}

		public virtual StickyHeaderBuilder SetAnimator(Func<AnimatorBuilder> headerAnimator)
		{
			this.animator = new SimpleHeaderStickyAnimator(headerAnimator);
			return this;
		}

		public abstract StickyHeaderView Apply();

		public class ListViewBuilder : StickyHeaderBuilder
		{
			private readonly ListView listView;

			public ListViewBuilder(ListView listView) 
				: base(listView.Context)
			{
				this.listView = listView;
			}

			public override StickyHeaderView Apply()
			{
				//if the animator has not been set, the default one is used
				if (animator == null)
				{
					animator = new HeaderStickyAnimator();
				}

				return new StickyHeaderListView(context, header, minHeight, animator, listView, preventTouchBehindHeader);
			}
		}

		public class RecyclerViewBuilder : StickyHeaderBuilder
		{
			private readonly RecyclerView recyclerView;

			public RecyclerViewBuilder(RecyclerView recyclerView)
				: base(recyclerView.Context)
			{
				this.recyclerView = recyclerView;
			}

			public override StickyHeaderView Apply()
			{
				//if the animator has not been set, the default one is used
				if (animator == null)
				{
					animator = new HeaderStickyAnimator();
				}

				return new StickyHeaderRecyclerView(context, header, minHeight, animator, recyclerView, preventTouchBehindHeader);
			}
		}

		public class ScrollViewBuilder : StickyHeaderBuilder
		{
			private readonly ScrollView scrollView;

			public ScrollViewBuilder(ScrollView scrollView)
				: base(scrollView.Context)
			{
				this.scrollView = scrollView;
			}

			public override StickyHeaderView Apply()
			{
				//if the animator has not been set, the default one is used
				if (animator == null)
				{
					animator = new HeaderStickyAnimator();
				}

				return new StickyHeaderScrollView(context, header, minHeight, animator, scrollView,preventTouchBehindHeader);
			}
		}
	}
}