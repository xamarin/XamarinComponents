using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

using VectorCompat;

namespace VectorCompatSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_main);

			// Example of adding MorphButton in java
			var mb = new MorphButton (this);

			if (Build.VERSION.SdkInt > BuildVersionCodes.Lollipop) {
				mb.BackgroundTintList = Resources.GetColorStateList (Resource.Color.background_tint_color);
				mb.ForegroundTintList = Resources.GetColorStateList (Resource.Color.foreground_tint_color);
			}
			mb.SetStartDrawable (Resource.Drawable.ic_pause_to_play);
			mb.SetEndDrawable (Resource.Drawable.ic_play_to_pause);
			mb.StateChanged += (sender, e) => {
				Toast.MakeText (this, "Changed to: " + e.ChangedTo, ToastLength.Short).Show ();
			};

			var ll = FindViewById<LinearLayout> (Resource.Id.base_view);
			var p = new LinearLayout.LayoutParams (ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
			ll.AddView (mb, p);
		}
	}
}
