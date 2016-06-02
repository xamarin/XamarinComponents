using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace RecyclerViewAnimatorsSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
		private bool enabledGrid = false;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_main);

			FindViewById (Resource.Id.btn_animator_sample).Click += (sender, e) => {
				var i = new Intent (this, typeof(AnimatorSampleActivity));
				i.PutExtra ("GRID", enabledGrid);
				StartActivity (i);
			};

			FindViewById (Resource.Id.btn_adapter_sample).Click += (sender, e) => {
				var i = new Intent (this, typeof(AdapterSampleActivity));
				i.PutExtra ("GRID", enabledGrid);
				StartActivity (i);
			};

			FindViewById<SwitchCompat> (Resource.Id.grid).CheckedChange += (sender, e) => {
				enabledGrid = e.IsChecked;
			};
		}
	}
}
