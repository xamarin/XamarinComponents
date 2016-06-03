using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using NineOldAndroids.View;

using AndroidSwipeLayout;

namespace AndroidSwipeLayoutSample
{
	[Activity (Label = "Android Swipe Layout", MainLauncher = true)]
	public class MainActivity : BaseActivity
	{
		private SwipeLayout sample1;
		private SwipeLayout sample2;
		private SwipeLayout sample3;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.main);

			//sample1

			sample1 = FindViewById<SwipeLayout> (Resource.Id.sample1);
			sample1.SetShowMode (SwipeLayout.ShowMode.PullOut);
			sample1.AddDrag (SwipeLayout.DragEdge.Left, sample1.FindViewById (Resource.Id.bottom_wrapper));
			sample1.AddDrag (SwipeLayout.DragEdge.Right, sample1.FindViewById (Resource.Id.bottom_wrapper_2));
			sample1.AddDrag (SwipeLayout.DragEdge.Top, sample1.FindViewById (Resource.Id.starbott));
			sample1.AddDrag (SwipeLayout.DragEdge.Bottom, sample1.FindViewById (Resource.Id.starbott));
			sample1.AddRevealListener (Resource.Id.delete, (sender, e) => {
			});
			sample1.SurfaceView.Click += (sender, e) => {
				Toast.MakeText (this, "Click on surface", ToastLength.Short).Show ();
			};
			sample1.SurfaceView.LongClick += (sender, e) => {
				Toast.MakeText (this, "longClick on surface", ToastLength.Short).Show ();
				e.Handled = true;
			};
			sample1.FindViewById (Resource.Id.star2).Click += (sender, e) => {
				Toast.MakeText (this, "Star", ToastLength.Short).Show ();
			};
			sample1.FindViewById (Resource.Id.trash2).Click += (sender, e) => {
				Toast.MakeText (this, "Trash Bin", ToastLength.Short).Show ();
			};
			sample1.FindViewById (Resource.Id.magnifier2).Click += (sender, e) => {
				Toast.MakeText (this, "Magnifier", ToastLength.Short).Show ();
			};
			sample1.AddRevealListener (Resource.Id.starbott, (sender, e) => {
				View star = e.Child.FindViewById (Resource.Id.star);
				float d = e.Child.Height / 2 - star.Height / 2;
				ViewHelper.SetTranslationY (star, d * e.Fraction);
				ViewHelper.SetScaleX (star, e.Fraction + 0.6f);
				ViewHelper.SetScaleY (star, e.Fraction + 0.6f);
			});

			//sample2

			sample2 = FindViewById<SwipeLayout> (Resource.Id.sample2);
			sample2.SetShowMode (SwipeLayout.ShowMode.LayDown);
			sample2.AddDrag (SwipeLayout.DragEdge.Right, sample2.FindViewWithTag ("Bottom2"));
			sample2.FindViewById (Resource.Id.star).Click += (sender, e) => {
				Toast.MakeText (this, "Star", ToastLength.Short).Show ();
			};
			sample2.FindViewById (Resource.Id.trash).Click += (sender, e) => {
				Toast.MakeText (this, "Trash Bin", ToastLength.Short).Show ();
			};
			sample2.FindViewById (Resource.Id.magnifier).Click += (sender, e) => {
				Toast.MakeText (this, "Magnifier", ToastLength.Short).Show ();
			};
			sample2.FindViewById (Resource.Id.click).Click += (sender, e) => {
				Toast.MakeText (this, "Yo!", ToastLength.Short).Show ();
			};
			sample2.SurfaceView.Click += (sender, e) => {
				Toast.MakeText (this, "Click on surface", ToastLength.Short).Show ();
			};

			//sample3

			sample3 = FindViewById<SwipeLayout> (Resource.Id.sample3);
			sample3.AddDrag (SwipeLayout.DragEdge.Top, sample3.FindViewWithTag ("Bottom3"));
			sample3.AddRevealListener (Resource.Id.bottom_wrapper_child1, (sender, e) => {
				View star = e.Child.FindViewById (Resource.Id.star);
				float d = e.Child.Height / 2 - star.Height / 2;
				ViewHelper.SetTranslationY (star, d * e.Fraction);
				ViewHelper.SetScaleX (star, e.Fraction + 0.6f);
				ViewHelper.SetScaleY (star, e.Fraction + 0.6f);
				Color c = Evaluate (e.Fraction, Color.ParseColor ("#dddddd"), Color.ParseColor ("#4C535B"));
				e.Child.SetBackgroundColor (c);
			});
			sample3.FindViewById (Resource.Id.bottom_wrapper_child1).Click += (sender, e) => {
				Toast.MakeText (this, "Yo!", ToastLength.Short).Show ();
			};
			sample3.SurfaceView.Click += (sender, e) => {
				Toast.MakeText (this, "Click on surface", ToastLength.Short).Show ();
			};
		}

		/// <summary>
		/// Color transition method.
		/// </summary>
		private Color Evaluate (float fraction, Color startValue, Color endValue)
		{
			int startInt = (int)startValue;
			int startA = (startInt >> 24) & 0xff;
			int startR = (startInt >> 16) & 0xff;
			int startG = (startInt >> 8) & 0xff;
			int startB = startInt & 0xff;

			int endInt = (int)endValue;
			int endA = (endInt >> 24) & 0xff;
			int endR = (endInt >> 16) & 0xff;
			int endG = (endInt >> 8) & 0xff;
			int endB = endInt & 0xff;

			return new Color (
				(startA + (int)(fraction * (endA - startA))),
				(startR + (int)(fraction * (endR - startR))),
				(startG + (int)(fraction * (endG - startG))),
				(startB + (int)(fraction * (endB - startB))));
		}
	}
}
