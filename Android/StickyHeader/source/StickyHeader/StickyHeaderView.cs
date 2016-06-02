using System;
using Android.Content;
using Android.Views;
using StickyHeader.Animator;

namespace StickyHeader
{
	public abstract class StickyHeaderView
	{
		private bool downEventDispatched = false;

		protected readonly Context context;
		protected readonly View header;
		protected readonly View view;
		protected readonly HeaderAnimator headerAnimator;
		protected readonly int minHeightHeader;
		protected readonly bool preventTouchBehindHeader;

		protected int heightHeader;
		protected int maxHeaderTransaction;

		protected StickyHeaderView(Context context, View header, View view, int minHeightHeader, HeaderAnimator headerAnimator, bool preventTouchBehindHeader)
		{
			this.context = context;
			this.header = header;
			this.view = view;
			this.minHeightHeader = minHeightHeader;
			this.headerAnimator = headerAnimator;
			this.preventTouchBehindHeader = preventTouchBehindHeader;

			MeasureHeaderHeight();

			headerAnimator.SetupAnimator(header, minHeightHeader, heightHeader, maxHeaderTransaction);

			if (preventTouchBehindHeader)
			{
				header.Touch += OnHeaderTouch;
				header.LongClick += OnHeaderLongClick;
			}
		}

		protected virtual void OnHeaderLongClick(object sender, View.LongClickEventArgs e)
		{
			e.Handled = true;
		}

		protected virtual void OnHeaderTouch(object sender, View.TouchEventArgs e)
		{
			var evt = e.Event;
			var eventConsumed = false;

			if (view != null)
			{
				switch (evt.Action)
				{
					case MotionEventActions.Move:
						if (!downEventDispatched)
						{
							// if moving, create a fake down event for the scrollingView to start the scroll. 
							// the y of the touch in the scrolling view is the y coordinate of the touch in the 
							// header + the translation of the header
							MotionEvent downEvent = MotionEvent.Obtain(
								evt.DownTime - 1, 
								evt.EventTime - 1, 
								MotionEventActions.Down, 
								evt.GetX(), 
								evt.GetY() + header.TranslationY, 
								0);
							view.DispatchTouchEvent(downEvent);
							downEventDispatched = true;
						}

						// dispatching the move event. we need to create a fake motionEvent using a different y 
						// coordinate related to the scrolling view
						MotionEvent moveEvent = MotionEvent.Obtain(
							evt.DownTime, 
							evt.EventTime,
							MotionEventActions.Move,
							evt.GetX(), 
							evt.GetY() + header.TranslationY, 
							0);
						view.DispatchTouchEvent(moveEvent);

						break;
					case MotionEventActions.Up:
						// when action up, dispatch an action cancel to avoid a possible click
						MotionEvent cancelEvent = MotionEvent.Obtain(
							evt.DownTime, 
							evt.EventTime,
							MotionEventActions.Cancel, 
							evt.GetX(), 
							evt.GetY(), 
							0);
						view.DispatchTouchEvent(cancelEvent);
						downEventDispatched = false;
						break;
					case MotionEventActions.Cancel:
						view.DispatchTouchEvent(evt);
						downEventDispatched = false;
						break;
				}

				eventConsumed = true;
			}

			e.Handled = eventConsumed;
		}

		protected virtual void SetHeightHeader(int value)
		{
			heightHeader = value;

			ViewGroup.LayoutParams lpHeader = header.LayoutParameters;
			lpHeader.Height = heightHeader;
			header.LayoutParameters = lpHeader;

			maxHeaderTransaction = minHeightHeader - heightHeader;

			// update heights
			headerAnimator.SetupAnimator(header, minHeightHeader, heightHeader, maxHeaderTransaction);
		}

		private void MeasureHeaderHeight()
		{
			// try use the existing height
			if (header.Height != 0)
			{
				SetHeightHeader(header.Height);
			}
			else
			{
				// try get the height from the layout
				var lp = header.LayoutParameters;
				if (lp != null && lp.Height > 0)
				{
					SetHeightHeader(lp.Height);
				}
				else
				{
					header.ViewTreeObserver.AddOnGlobalLayoutAction(() => 
					{
						if (heightHeader != header.Height)
						{
							SetHeightHeader(header.Height);
						}
					});
				}
			}
		}
	}
}