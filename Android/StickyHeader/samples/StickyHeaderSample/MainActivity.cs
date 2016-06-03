using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Fragment = Android.Support.V4.App.Fragment;

namespace StickyHeaderSample
{
	[Activity(Label = "Sticky Header", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/MyTheme")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.MainLayout);

			if (savedInstanceState == null)
			{
				SupportFragmentManager
					.BeginTransaction()
					.Add(Resource.Id.layout_container, new MainFragment())
					.Commit();
			}
		}

		public void LoadFragment(Fragment fragment)
		{
			SupportFragmentManager
				.BeginTransaction()
				.Replace(Resource.Id.layout_container, fragment)
				.AddToBackStack(fragment.GetType().Name)
				.Commit();
		}
	}
}