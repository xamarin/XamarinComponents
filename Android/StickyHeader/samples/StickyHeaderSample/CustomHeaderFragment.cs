using Android.Animation;
using Android.Content;
using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using StickyHeader;
using StickyHeader.Animator;

namespace StickyHeaderSample
{
	public class CustomHeaderFragment : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.CustomHeaderLayout, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			// header
			var listView = (ListView) View.FindViewById(Resource.Id.listview);
			StickyHeaderBuilder
				.StickTo(listView)
				.SetHeader(Resource.Id.header, (ViewGroup) View)
				.SetMinHeightDimension(Resource.Dimension.min_height_header_materiallike)
				.SetAnimator(new CustomHeaderAnimator(Activity))
				.PreventTouchBehindHeader()
				.Apply();
			
			// items
			var elements = new string[500];
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = "row " + i;
			}
			listView.Adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItem1, elements);
		}

		private class CustomHeaderAnimator : HeaderStickyAnimator
		{
			private readonly Context mContext;
			private bool isCovering;
			private View mHeaderText;
			private int mHeightStartAnimation;
			private int mMinHeightTextHeader;
			private ValueAnimator valueAnimator;

			public CustomHeaderAnimator(Context context)
			{
				mContext = context;
			}

			public override AnimatorBuilder CreateAnimatorBuilder()
			{
				View image = Header.FindViewById(Resource.Id.header_image);
				return AnimatorBuilder.Create().ApplyVerticalParallax(image, 0.5f);
			}

			protected override void OnAnimatorAttached()
			{
				base.OnAnimatorAttached();

				mHeaderText = Header.FindViewById(Resource.Id.header_text_layout);
				var tv = new TypedValue();
				int actionBarHeight = 0;
				if (mContext.Theme.ResolveAttribute(Android.Resource.Attribute.ActionBarSize, tv, true))
				{
					actionBarHeight = TypedValue.ComplexToDimensionPixelSize(tv.Data, mContext.Resources.DisplayMetrics);
				}
				mMinHeightTextHeader = mContext.Resources.GetDimensionPixelSize(Resource.Dimension.min_height_textheader_materiallike);

				mHeightStartAnimation = actionBarHeight + mMinHeightTextHeader;

				valueAnimator = ValueAnimator.OfInt(0);
				valueAnimator.SetDuration(mContext.Resources.GetInteger(Android.Resource.Integer.ConfigShortAnimTime));
				valueAnimator.Update += (sender, e) =>
				{
					ViewGroup.LayoutParams layoutParams = mHeaderText.LayoutParameters;
					layoutParams.Height = (int)e.Animation.AnimatedValue;
					mHeaderText.LayoutParameters = layoutParams;
				};
			}

			public override void OnScroll(int scrolledY)
			{
				base.OnScroll(scrolledY);
				float translatedY = Header.TranslationY;

				float visibleHeightHeader = HeightHeader + translatedY;

				if (visibleHeightHeader <= mHeightStartAnimation && !isCovering)
				{
					valueAnimator.SetIntValues(mHeaderText.Height, mHeightStartAnimation);
					if (valueAnimator.IsRunning)
					{
						valueAnimator.End();
					}
					valueAnimator.Start();

					isCovering = true;
				}
				else if (visibleHeightHeader > mHeightStartAnimation && isCovering)
				{
					valueAnimator.SetIntValues(mHeaderText.Height, mMinHeightTextHeader);
					if (valueAnimator.IsRunning)
					{
						valueAnimator.End();
					}
					valueAnimator.Start();

					isCovering = false;
				}
			}
		}
	}
}