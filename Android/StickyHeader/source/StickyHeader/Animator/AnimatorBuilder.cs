using System;
using System.Collections.Generic;
using System.Drawing;
using Android.Views;
using Android.Views.Animations;

namespace StickyHeader.Animator
{
	public class AnimatorBuilder
	{
		public const float DefaultVelocityAnimator = 0.5f;

		private readonly IList<AnimatorBundle> listAnimatorBundles;
		private float mLastTranslationApplied = float.NaN;

		private AnimatorBuilder()
		{
			listAnimatorBundles = new List<AnimatorBundle>(2);
		}

		public static AnimatorBuilder Create()
		{
			return new AnimatorBuilder();
		}

		public virtual AnimatorBuilder ApplyScale(View view, RectangleF finalRect)
		{
			return ApplyScale(view, finalRect, null);
		}

		public virtual AnimatorBuilder ApplyScale(View view, RectangleF finalRect, IInterpolator interpolator)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}

			var from = new RectangleF(view.Left, view.Top, view.Right, view.Bottom);
			var scaleX = finalRect.Width / from.Width;
			var scaleY = finalRect.Height / from.Height;

			return ApplyScale(view, scaleX, scaleY, interpolator);
		}

		public virtual AnimatorBuilder ApplyScale(View view, float scaleX, float scaleY)
		{
			return ApplyScale(view, scaleX, scaleY, null);
		}

		public virtual AnimatorBuilder ApplyScale(View view, float scaleX, float scaleY, IInterpolator interpolator)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}

			AnimatorBundle animatorScaleX = AnimatorBundle.Create(AnimatorBundle.TypeAnimation.ScaleX, view, interpolator, view.ScaleX, scaleX);
			AnimatorBundle animatorScaleY = AnimatorBundle.Create(AnimatorBundle.TypeAnimation.ScaleY, view, interpolator, view.ScaleY, scaleY);

			AdjustTranslation(view);

			listAnimatorBundles.Add(animatorScaleX);
			listAnimatorBundles.Add(animatorScaleY);

			return this;
		}

		/// <summary>
		///     Translate the top-left point of the view to finalPoint
		/// </summary>
		/// <param name="view"> </param>
		/// <param name="finalPoint">
		///     @return
		/// </param>
		public virtual AnimatorBuilder ApplyTranslation(View view, PointF finalPoint)
		{
			return ApplyTranslation(view, finalPoint, null);
		}

		public virtual AnimatorBuilder ApplyTranslation(View view, PointF finalPoint, IInterpolator interpolator)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}

			var from = new PointF(view.Left, view.Top);
			var translationX = finalPoint.X - from.X;
			var translationY = finalPoint.Y - from.Y;

			return ApplyTranslation(view, translationX, translationY, interpolator);
		}

		public virtual AnimatorBuilder ApplyTranslation(View view, float translateX, float translateY)
		{
			return ApplyTranslation(view, translateX, translateY, null);
		}

		public virtual AnimatorBuilder ApplyTranslation(View view, float translateX, float translateY, IInterpolator interpolator)
		{
			if (view == null)
			{
				throw new ArgumentNullException("view");
			}

			AnimatorBundle animatorTranslationX = AnimatorBundle.Create(AnimatorBundle.TypeAnimation.TranslationX, view, interpolator, view.TranslationX, translateX);
			AnimatorBundle animatorTranslationY = AnimatorBundle.Create(AnimatorBundle.TypeAnimation.TranslationY, view, interpolator, view.TranslationY, translateY);

			AdjustTranslation(view);

			listAnimatorBundles.Add(animatorTranslationX);
			listAnimatorBundles.Add(animatorTranslationY);

			return this;
		}

		public virtual AnimatorBuilder ApplyFade(View viewToFade)
		{
			return ApplyFade(viewToFade, 1.0f, null);
		}

		public virtual AnimatorBuilder ApplyFade(View viewToFade, float fade)
		{
			return ApplyFade(viewToFade, fade, null);
		}

		public virtual AnimatorBuilder ApplyFade(View viewToFade, float fade, IInterpolator interpolator)
		{
			if (viewToFade == null)
			{
				throw new ArgumentNullException("viewToFade");
			}

			float startAlpha = viewToFade.Alpha;

			listAnimatorBundles.Add(AnimatorBundle.Create(AnimatorBundle.TypeAnimation.Fade, viewToFade, interpolator, startAlpha, fade));

			return this;
		}

		/// <param name="viewToParallax"> </param>
		/// <param name="velocityParallax">
		///     the velocity to apply to the view in order to show the parallax effect. choose a velocity between 0 and 1 for
		///     better results
		///     @return
		/// </param>
		public virtual AnimatorBuilder ApplyVerticalParallax(View viewToParallax, float velocityParallax)
		{
			if (viewToParallax == null)
			{
				throw new ArgumentNullException("viewToParallax");
			}

			listAnimatorBundles.Add(AnimatorBundle.Create(AnimatorBundle.TypeAnimation.Parallax, viewToParallax, null, 0f, -velocityParallax));

			return this;
		}

		public virtual AnimatorBuilder ApplyVerticalParallax(View viewToParallax)
		{
			return ApplyVerticalParallax(viewToParallax, DefaultVelocityAnimator);
		}

		private void AdjustTranslation(View viewAnimated)
		{
			AnimatorBundle animatorScaleX = null;
			AnimatorBundle animatorScaleY = null;
			AnimatorBundle animatorTranslationX = null;
			AnimatorBundle animatorTranslationY = null;

			foreach (AnimatorBundle animator in listAnimatorBundles)
			{
				if (viewAnimated == animator.mView)
				{
					switch (animator.mTypeAnimation)
					{
						case AnimatorBundle.TypeAnimation.ScaleX:
							animatorScaleX = animator;
							break;
						case AnimatorBundle.TypeAnimation.ScaleY:
							animatorScaleY = animator;
							break;
						case AnimatorBundle.TypeAnimation.TranslationX:
							animatorTranslationX = animator;
							break;
						case AnimatorBundle.TypeAnimation.TranslationY:
							animatorTranslationY = animator;
							break;
					}
				}
			}

			if (animatorTranslationX != null && animatorScaleX != null)
			{
				animatorTranslationX.mDelta += animatorTranslationX.mView.Width * (animatorScaleX.mDelta / 2f);
			}

			if (animatorTranslationY != null && animatorScaleY != null)
			{
				animatorTranslationY.mDelta += animatorTranslationY.mView.Height * (animatorScaleY.mDelta / 2f);
			}
		}

		protected internal virtual void AnimateOnScroll(float boundedRatioTranslationY, float translationY)
		{

			if (mLastTranslationApplied == boundedRatioTranslationY)
			{
				return;
			}

			mLastTranslationApplied = boundedRatioTranslationY;

			foreach (AnimatorBundle animatorBundle in listAnimatorBundles)
			{
				float interpolatedTranslation = animatorBundle.mInterpolator == null ? boundedRatioTranslationY : animatorBundle.mInterpolator.GetInterpolation(boundedRatioTranslationY);
				float valueAnimation = animatorBundle.mFromValue + (animatorBundle.mDelta * interpolatedTranslation);

				switch (animatorBundle.mTypeAnimation)
				{

					case AnimatorBundle.TypeAnimation.ScaleX:
						animatorBundle.mView.ScaleX = valueAnimation;
						break;
					case AnimatorBundle.TypeAnimation.ScaleY:
						animatorBundle.mView.ScaleY = valueAnimation;
						break;
					case AnimatorBundle.TypeAnimation.Fade:
						animatorBundle.mView.Alpha = valueAnimation; //TODO performance issues?
						break;
					case AnimatorBundle.TypeAnimation.TranslationX:
						animatorBundle.mView.TranslationX = valueAnimation;
						break;
					case AnimatorBundle.TypeAnimation.TranslationY:
						animatorBundle.mView.TranslationY = valueAnimation - translationY;
						break;
					case AnimatorBundle.TypeAnimation.Parallax:
						animatorBundle.mView.TranslationY = animatorBundle.mDelta * translationY;
						break;

				}
			}
		}

		public virtual bool HasAnimatorBundles()
		{
			return listAnimatorBundles.Count > 0;
		}

		//public static RectangleF BuildViewRect(View view)
		//{
		//	//TODO get coordinates related to the header
		//	return new RectangleF(view.Left, view.Top, view.Right, view.Bottom);
		//}

		//public static PointF BuildPointView(View view)
		//{
		//	return new PointF(view.Left, view.Top);
		//}

		//public static float CalculateScaleX(RectangleF from, RectangleF to)
		//{
		//	return 1f - to.Width/@from.Width;
		//}

		//public static float CalculateScaleY(RectangleF from, RectangleF to)
		//{
		//	return 1f - to.Height/@from.Height;
		//}

		//public static float CalculateTranslationX(PointF from, PointF to)
		//{
		//	return to.X - from.X;
		//}

		//public static float CalculateTranslationY(PointF from, PointF to)
		//{
		//	return to.Y - from.Y;
		//}

		public class AnimatorBundle
		{
			public enum TypeAnimation
			{
				ScaleX,
				ScaleY,
				Fade,
				TranslationX,
				TranslationY,
				Parallax
			}

			internal readonly TypeAnimation mTypeAnimation;
			internal float mFromValue;
			internal float mDelta;
			internal View mView;
			internal IInterpolator mInterpolator;

			internal AnimatorBundle(TypeAnimation typeAnimation)
			{
				mTypeAnimation = typeAnimation;
			}

			public static AnimatorBundle Create(TypeAnimation typeAnimation, View view, IInterpolator interpolator, float from, float to)
			{
				var animatorBundle = new AnimatorBundle(typeAnimation);

				animatorBundle.mView = view;
				animatorBundle.mFromValue = from;
				animatorBundle.mDelta = to - from;
				animatorBundle.mInterpolator = interpolator;

				return animatorBundle;
			}
		}
	}
}