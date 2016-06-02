using System;
using StickyHeader.Animator;

namespace StickyHeader.Animator
{
	public class BaseStickyHeaderAnimator : HeaderAnimator
	{
		protected float TranslationRatio { get; private set; }

		protected internal override void OnAnimatorAttached()
		{
			//nothing to do
		}

		protected internal override void OnAnimatorReady()
		{
			//nothing to do
		}

		public override void OnScroll(int scrolledY)
		{
			Header.TranslationY = Math.Max(scrolledY, MaxTransaction);
			TranslationRatio = (float)scrolledY / (float)MaxTransaction;
		}
	}
}