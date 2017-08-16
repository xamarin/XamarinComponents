using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

using FloatingSearchViews;

using Fragment = Android.Support.V4.App.Fragment;

namespace FloatingSearchViewSample
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity, BaseExampleFragment.IBaseExampleFragmentCallbacks
	{
		private DrawerLayout drawerLayout;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
			navigationView.NavigationItemSelected += (sender, e) =>
			{
				drawerLayout.CloseDrawer(GravityCompat.Start);
				switch (e.MenuItem.ItemId)
				{
					case Resource.Id.sliding_list_example:
						ShowFragment(new SlidingSearchResultsExampleFragment());
						break;
					case Resource.Id.sliding_search_bar_example:
						ShowFragment(new SlidingSearchViewExampleFragment());
						break;
					case Resource.Id.scrolling_search_bar_example:
						ShowFragment(new ScrollingSearchExampleFragment());
						break;
				}
				e.Handled = true;
			};

			ShowFragment(new SlidingSearchResultsExampleFragment());
		}

		public void OnAttachSearchViewToDrawer(FloatingSearchView searchView)
		{
			searchView.AttachNavigationDrawerToMenuButton(drawerLayout);
		}

		public override void OnBackPressed()
		{
			var fragments = SupportFragmentManager.Fragments;
			var currentFragment = (BaseExampleFragment)fragments[fragments.Count - 1];

			if (!currentFragment.OnActivityBackPress())
			{
				base.OnBackPressed();
			}
		}

		private void ShowFragment(Fragment fragment)
		{
			SupportFragmentManager
				.BeginTransaction()
				.Replace(Resource.Id.fragment_container, fragment)
				.Commit();
		}
	}
}
