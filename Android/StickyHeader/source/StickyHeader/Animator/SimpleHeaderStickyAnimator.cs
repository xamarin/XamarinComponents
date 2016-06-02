using System;

namespace StickyHeader.Animator
{
	public class SimpleHeaderStickyAnimator : HeaderStickyAnimator
	{
		private readonly Func<AnimatorBuilder> builderFunc;

		public SimpleHeaderStickyAnimator(Func<AnimatorBuilder> builder)
		{
			builderFunc = builder;
		}

		public override AnimatorBuilder CreateAnimatorBuilder()
		{
			return builderFunc();
		}
	}
}