using Android.Animation;
using Android.App;
using Android.OS;
using Android.Widget;

using BartoszLipinski;

namespace ViewPropertyObjectAnimatorSample
{
	[Activity(MainLauncher = true)]
	public class MainActivity : Activity
	{
		private ScrollView scrollView;
		private ImageView imageView;
		private AnimatorSet animatorSet;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			scrollView = FindViewById<ScrollView>(Resource.Id.scrollView);
			imageView = FindViewById<ImageView>(Resource.Id.image);

			imageView.Click += (sender, e) =>
			{
				var v = (ImageView)sender;

				var isSelectedAfterClick = !v.Selected;
				v.Selected = isSelectedAfterClick;

				if (isSelectedAfterClick)
					ReverseAnimator();
				else
					Animator();
			};

			var handler = new Handler();
			handler.PostDelayed(() => Animator(), 500);
		}

		private void Animator()
		{
			var paddingTop = Resources.GetDimensionPixelSize(Resource.Dimension.scroll_padding_top);

			var scrollAnimator = ViewPropertyObjectAnimator
				.Animate(scrollView)
				.ScrollY(paddingTop)
				.Get();

			var imageAnimator = ViewPropertyObjectAnimator
				.Animate(imageView)
				.VerticalMargin(140)
				.RightMarginBy(10)
				.Width(600)
				.Height(700)
				.RotationXBy(20)
				.TopPadding(10)
				.RotationY(360)
				.LeftPaddingBy(100)
				.RightPadding(300)
				.Get();

			animatorSet?.Cancel();

			animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(scrollAnimator, imageAnimator);
			animatorSet.SetDuration(2000);
			animatorSet.Start();
		}

		private void ReverseAnimator()
		{
			var width = Resources.GetDimensionPixelSize(Resource.Dimension.image_width);
			var height = Resources.GetDimensionPixelSize(Resource.Dimension.image_height);
			var margin = Resources.GetDimensionPixelSize(Resource.Dimension.image_margin);
			var padding = Resources.GetDimensionPixelSize(Resource.Dimension.image_padding);

			var scrollAnimator = ViewPropertyObjectAnimator
				.Animate(scrollView)
				.ScrollY(0)
				.Get();

			var imageAnimator = ViewPropertyObjectAnimator
				.Animate(imageView)
				.Width(width)
				.Height(height)
				.Margin(margin)
				.Padding(padding)
				.RotationX(0)
				.RotationY(0)
				.Get();

			animatorSet?.Cancel();

			animatorSet = new AnimatorSet();
			animatorSet.PlayTogether(scrollAnimator, imageAnimator);
			animatorSet.SetDuration(2000);
			animatorSet.Start();
		}
	}
}
