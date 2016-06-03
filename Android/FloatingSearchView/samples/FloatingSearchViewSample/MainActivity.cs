using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Graphics;
using Android.Views;
using Android.Widget;

using FloatingSearchViews;

namespace FloatingSearchViewSample
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Theme = "@style/AppTheme")]
	public class MainActivity : AppCompatActivity
	{
		private FloatingSearchView searchView;

		private ViewGroup parentView;
		private TextView colorNameText;
		private TextView colorValueText;

		private DrawerLayout drawerLayout;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.activity_main);

			parentView = FindViewById<ViewGroup> (Resource.Id.parent_view);

			searchView = FindViewById<FloatingSearchView> (Resource.Id.floating_search_view);
			colorNameText = FindViewById<TextView> (Resource.Id.color_name_text);
			colorValueText = FindViewById<TextView> (Resource.Id.color_value_text);

			drawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);

			// sets the background color
			RefreshBackgroundColor ("Xamarin Blue", "#3498DB");

			searchView.QueryChange += async (sender, e) => {
				if (!string.IsNullOrEmpty (e.OldQuery) && string.IsNullOrEmpty (e.NewQuery)) {
					searchView.ClearSuggestions ();
				} else {
					// this shows the top left circular progress
					// you can call it where ever you want, but
					// it makes sense to do it when loading something in
					// the background.
					searchView.ShowProgress ();
				
					// simulates a query call to a data source
					// with a new query.
					var results = await DataHelper.FindAsync (this, e.NewQuery);

					// this will swap the data and
					// render the collapse/expand animations as necessary
					searchView.SwapSuggestions (results);
				
					// let the users know that the background
					// process has completed
					searchView.HideProgress ();
				}
				
				Console.WriteLine ("QueryChange");
			};

			searchView.SuggestionClicked += (sender, e) => {
				var colorSuggestion = (ColorSuggestion)e.SearchSuggestion;
				RefreshBackgroundColor (colorSuggestion.Color.Name, colorSuggestion.Color.Hex);
				
				Console.WriteLine ("SuggestionClicked");
			};

			searchView.SearchAction += (sender, e) => {
				Console.WriteLine ("SearchAction");
			};

			searchView.Focus += async (sender, e) => {
				searchView.SwapSuggestions (await DataHelper.GetHistoryAsync (this, 3));

				Console.WriteLine ("Focus");
			};

			searchView.FocusCleared += (sender, e) => {
				Console.WriteLine ("FocusCleared");
			};

			searchView.MenuItemClick += (sender, e) => {
				// handle menu clicks the same way as you would
				// in a regular activity
				switch (e.MenuItem.ItemId) {
				case Resource.Id.action_show_menu:
					searchView.SetLeftShowMenu (true);
					break;
				case Resource.Id.action_hide_menu:
					searchView.SetLeftShowMenu (false);
					break;
				}
			};

			searchView.MenuOpened += (sender, e) => {
				drawerLayout.OpenDrawer (GravityCompat.Start);
			};

			searchView.MenuClosed += (sender, e) => {
				drawerLayout.CloseDrawer (GravityCompat.Start);
			};

			drawerLayout.DrawerOpened += (sender, e) => {
				// since the drawer might have opened as a results of
				// a click on the left menu, we need to make sure
				// to close it right after the drawer opens, so that
				// it is closed when the drawer is closed.
				searchView.CloseMenu (false);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Android.Content.Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);
		
			// this is needed in order for voice recognition to work
			searchView.OnHostActivityResult (requestCode, resultCode, data);
		}

		private void RefreshBackgroundColor (string colorName, string colorValue)
		{
			var color = Color.ParseColor (colorValue);
			var swatch = new Palette.Swatch (color, 0);
		
			colorNameText.SetTextColor (new Color (swatch.TitleTextColor));
			colorNameText.Text = colorName;
		
			colorValueText.SetTextColor (new Color (swatch.BodyTextColor));
			colorValueText.Text = colorValue;
		
			parentView.SetBackgroundColor (color);
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop) {
				Window.SetStatusBarColor (Color.Argb (
					color.A, 
					Math.Max ((int)(color.R * 0.8f), 0),
					Math.Max ((int)(color.G * 0.8f), 0),
					Math.Max ((int)(color.B * 0.8f), 0)));
			}
		}
	}
}
