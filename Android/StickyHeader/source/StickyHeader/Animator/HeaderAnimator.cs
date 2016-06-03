using System;
using Android.Views;

namespace StickyHeader.Animator
{
	public abstract class HeaderAnimator
	{
		public View Header { get; private set; }

		public int MinHeightHeader { get; private set; }

		public int HeightHeader { get; private set; }

		public int MaxTransaction { get; private set; }

		public abstract void OnScroll(int scrolledY);

		/// <summary>
		///     Called by the <seealso cref="StickyHeaderView" /> to set the <seealso cref="HeaderAnimator" /> up
		/// </summary>
		internal virtual void SetupAnimator(View header, int minHeightHeader, int heightHeader, int maxTransaction)
		{
			Header = header;
			MinHeightHeader = minHeightHeader;
			HeightHeader = heightHeader;
			MaxTransaction = maxTransaction;

			OnAnimatorAttached();
			header.ViewTreeObserver.AddOnGlobalLayoutSingleFire(() => OnAnimatorReady());
		}

		/// <summary>
		///     Called after that the animator is attached to the header
		/// </summary>
		protected internal abstract void OnAnimatorAttached();

		/// <summary>
		///     Called after that the animator is attached and ready
		/// </summary>
		protected internal abstract void OnAnimatorReady();

		public static float Clamp(float value, float min, float max)
		{
			return Math.Max(min, Math.Min(value, max));
		}
	}
}