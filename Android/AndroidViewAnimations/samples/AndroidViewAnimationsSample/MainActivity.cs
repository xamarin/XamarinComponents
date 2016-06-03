using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using NineOldAndroids.Animation;

using AndroidViewAnimations;
using Android.Views.Animations;

namespace AndroidViewAnimationsSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light.DarkActionBar")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_main);

			var target = FindViewById (Resource.Id.hello_world);

			// after start, just click mTarget view, rope is not init 
			var rope = YoYo.With (Techniques.FadeIn).Duration (1000).PlayOn (target);

			var listView = FindViewById<ListView> (Resource.Id.list_items);
			listView.Adapter = new EffectAdapter (this);
			listView.ItemClick += (sender, e) => {
				var technique = (Techniques)e.View.Tag;
				rope = YoYo.With (technique)
					.Duration (1200)
					.Interpolate (new AccelerateDecelerateInterpolator ())
					.WithCancelListener (ani => Toast.MakeText (this, "canceled", ToastLength.Short).Show ())
					.PlayOn (target);
			};

			FindViewById (Resource.Id.hello_world).Click += delegate {
				if (rope != null) {
					rope.Stop (true);   
				}
			};
		}
	}
}
