namespace StickyHeader.Animator
{
	public class HeaderStickyAnimator : BaseStickyHeaderAnimator
	{
		private bool hasAnimatorBundles;
		protected AnimatorBuilder animatorBuilder;

		/// <summary>
		///     Override if you want to load the animator builder
		/// </summary>
		public virtual AnimatorBuilder CreateAnimatorBuilder()
		{
			return null;
		}

		protected internal override void OnAnimatorReady()
		{
			base.OnAnimatorReady();
			animatorBuilder = CreateAnimatorBuilder();
			hasAnimatorBundles = (animatorBuilder != null) && (animatorBuilder.HasAnimatorBundles());
		}

		public override void OnScroll(int scrolledY)
		{
			base.OnScroll(scrolledY);

			if (hasAnimatorBundles)
			{
				var ratio = Clamp(TranslationRatio, 0f, 1f);
				animatorBuilder.AnimateOnScroll(ratio, Header.TranslationY);
			}
		}
	}
}
