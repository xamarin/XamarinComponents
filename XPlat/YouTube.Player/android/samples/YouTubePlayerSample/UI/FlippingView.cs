using System;
using Android.Animation;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views.Animations;
using Android.Widget;

namespace YouTubePlayerSample
{
	public class FlippingView : FrameLayout
	{
		private readonly ImageView flipOutView;
		private readonly ImageView flipInView;
		private readonly AnimatorSet animations;

		public FlippingView(Context context, int width, int height)
			: base(context)
		{
			flipOutView = new ImageView(context);
			flipInView = new ImageView(context);

			AddView(flipOutView, width, height);
			AddView(flipInView, width, height);

			flipInView.RotationY = -90;

			var flipOutAnimator = ObjectAnimator.OfFloat(flipOutView, "rotationY", 0, 90);
			flipOutAnimator.SetInterpolator(new AccelerateInterpolator());

			var flipInAnimator = ObjectAnimator.OfFloat(flipInView, "rotationY", -90, 0);
			flipInAnimator.SetInterpolator(new DecelerateInterpolator());

			animations = new AnimatorSet();
			animations.PlaySequentially(flipOutAnimator, flipInAnimator);

			animations.AnimationEnd += (sender, e) =>
			{
				flipOutView.RotationY = 0;
				flipInView.RotationY = -90;

				Flipped?.Invoke(this, EventArgs.Empty);
			};
		}

		public void SetFlipInDrawable(Drawable drawable)
		{
			flipInView.SetImageDrawable(drawable);
		}

		public void SetFlipOutDrawable(Drawable drawable)
		{
			flipOutView.SetImageDrawable(drawable);
		}

		public void SetFlipDuration(int flipDuration)
		{
			animations.SetDuration(flipDuration);
		}

		public void Flip()
		{
			animations.Start();
		}

		public event EventHandler Flipped;
	}
}
