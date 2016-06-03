using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using StickyHeader.Animator;

namespace StickyHeader
{
	public class StickyHeaderScrollView : StickyHeaderView
	{
		public StickyHeaderScrollView(Context context, View header, int minHeightHeader, HeaderAnimator headerAnimator, ScrollView scrollView, bool preventTouchBehindHeader)
			: base(context, header, scrollView, minHeightHeader, headerAnimator, preventTouchBehindHeader)
		{

			// scroll events
			scrollView.ViewTreeObserver.AddOnGlobalLayoutSingleFire(() => headerAnimator.OnScroll(-scrollView.ScrollY));
			scrollView.ViewTreeObserver.ScrollChanged += (sender, e) => headerAnimator.OnScroll(-scrollView.ScrollY);

			// some properties
			scrollView.SetClipToPadding(false);
		}

		private ScrollView scrollView
		{
			get { return (ScrollView)view; }
		}

		protected override void SetHeightHeader(int value)
		{
			base.SetHeightHeader(value);

			// creating a placeholder
			// adding a padding to the scrollview behind the header
			scrollView.SetPadding(
				scrollView.PaddingLeft,
				scrollView.PaddingTop + heightHeader,
				scrollView.PaddingRight,
				scrollView.PaddingBottom);
		}
	}
}