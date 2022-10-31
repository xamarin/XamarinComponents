using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Bumptech.Glide;
using ImageViews.Photo;

namespace PhotoViewSample
{
	[Activity]
	public class ImmersiveSampleActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_immersive);

			var photoView = FindViewById<PhotoView>(Resource.Id.photo_view);
			photoView.PhotoTap += (sender, e) =>
			{
				// FullScreen();
			};

			Glide
				.With(this)
				.Load("http://pbs.twimg.com/media/Bist9mvIYAAeAyQ.jpg")
				.Into(photoView);

			FullScreen();
		}

		public void FullScreen()
		{
			var uiOptions = (SystemUiFlags)(int)Window.DecorView.SystemUiVisibility;
			var newUiOptions = uiOptions;

			if (uiOptions.HasFlag(SystemUiFlags.ImmersiveSticky))
			{
				System.Diagnostics.Debug.WriteLine("Turning immersive mode mode off.");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("Turning immersive mode mode on.");
			}

			// Navigation bar hiding:  Backwards compatible to ICS.
			if (Build.VERSION.SdkInt >= BuildVersionCodes.IceCreamSandwich)
			{
				newUiOptions ^= SystemUiFlags.HideNavigation;
			}

			// Status bar hiding: Backwards compatible to Jellybean
			if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
			{
				newUiOptions ^= SystemUiFlags.Fullscreen;
			}

			// Immersive mode: Backward compatible to KitKat.
			// Note that this flag doesn't do anything by itself, it only augments the behavior
			// of HIDE_NAVIGATION and FLAG_FULLSCREEN.  For the purposes of this sample
			// all three flags are being toggled together.
			// Note that there are two immersive mode UI flags, one of which is referred to as "sticky".
			// Sticky immersive mode differs in that it makes the navigation and status bars
			// semi-transparent, and the UI flag does not get cleared when the user interacts with
			// the screen.
			if (Build.VERSION.SdkInt > BuildVersionCodes.JellyBean)
			{
				newUiOptions ^= SystemUiFlags.ImmersiveSticky;
			}

			Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(int)newUiOptions;
		}
	}
}