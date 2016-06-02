using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;

namespace AndroidSwipeLayoutSample.Adapters.Utils
{
	public class DividerItemDecoration : RecyclerView.ItemDecoration
	{
		private Drawable mDivider;
		private bool mShowFirstDivider = false;
		private bool mShowLastDivider = false;

		public DividerItemDecoration (Drawable divider)
		{
			mDivider = divider;
		}

		public DividerItemDecoration (Context context, IAttributeSet attrs)
		{
			var a = context.ObtainStyledAttributes (attrs, new []{ Android.Resource.Attribute.ListDivider });
			mDivider = a.GetDrawable (0);
			a.Recycle ();
		}

		public DividerItemDecoration (Drawable divider, bool showFirstDivider, bool showLastDivider)
			: this (divider)
		{
			mShowFirstDivider = showFirstDivider;
			mShowLastDivider = showLastDivider;
		}

		public DividerItemDecoration (Context context, IAttributeSet attrs, bool showFirstDivider, bool showLastDivider)
			: this (context, attrs)
		{
			mShowFirstDivider = showFirstDivider;
			mShowLastDivider = showLastDivider;
		}

		public override void GetItemOffsets (Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
		{
			base.GetItemOffsets (outRect, view, parent, state);

			if (mDivider == null) {
				return;
			}
			if (parent.GetChildPosition (view) < 1) {
				return;
			}
			if (GetOrientation (parent) == LinearLayoutManager.Vertical) {
				outRect.Top = mDivider.IntrinsicHeight;
			} else {
				outRect.Left = mDivider.IntrinsicWidth;
			}
		}

		public override void OnDrawOver (Canvas c, RecyclerView parent, RecyclerView.State state)
		{
			if (mDivider == null) {
				base.OnDrawOver (c, parent, state);
				return;
			}
		
			// Initialization needed to avoid compiler warning
			int left = 0, right = 0, top = 0, bottom = 0, size;
			int orientation = GetOrientation (parent);
			int childCount = parent.ChildCount;
		
			if (orientation == LinearLayoutManager.Vertical) {
				size = mDivider.IntrinsicHeight;
				left = parent.PaddingLeft;
				right = parent.Width - parent.PaddingRight;
			} else { //horizontal
				size = mDivider.IntrinsicWidth;
				top = parent.PaddingTop;
				bottom = parent.Height - parent.PaddingBottom;
			}
		
			for (int i = mShowFirstDivider ? 0 : 1; i < childCount; i++) {
				var child = parent.GetChildAt (i);
				var param = (RecyclerView.LayoutParams)child.LayoutParameters;
		
				if (orientation == LinearLayoutManager.Vertical) {
					top = child.Top - param.TopMargin;
					bottom = top + size;
				} else { //horizontal
					left = child.Left - param.LeftMargin;
					right = left + size;
				}
				mDivider.SetBounds (left, top, right, bottom);
				mDivider.Draw (c);
			}
		
			// show last divider
			if (mShowLastDivider && childCount > 0) {
				var child = parent.GetChildAt (childCount - 1);
				var param = (RecyclerView.LayoutParams)child.LayoutParameters;
				if (orientation == LinearLayoutManager.Vertical) {
					top = child.Bottom + param.BottomMargin;
					bottom = top + size;
				} else { // horizontal
					left = child.Right + param.RightMargin;
					right = left + size;
				}
				mDivider.SetBounds (left, top, right, bottom);
				mDivider.Draw (c);
			}
		}

		private int GetOrientation (RecyclerView parent)
		{
			var layoutManager = parent.GetLayoutManager () as LinearLayoutManager;
			if (layoutManager != null) {
				return layoutManager.Orientation;
			} else {
				throw new ArgumentException ("DividerItemDecoration can only be used with a LinearLayoutManager.");
			}
		}
	}
}
