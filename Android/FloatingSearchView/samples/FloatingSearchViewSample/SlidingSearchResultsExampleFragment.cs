using System;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.Content.Res;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;

using FloatingSearchViews;
using FloatingSearchViews.Utils;

namespace FloatingSearchViewSample
{
	public class SlidingSearchResultsExampleFragment : BaseExampleFragment
	{
		private const int FIND_SUGGESTION_SIMULATED_DELAY = 250;

		private FloatingSearchView mSearchView;

		private RecyclerView mSearchResultsList;
		private SearchResultsListAdapter mSearchResultsAdapter;

		private bool mIsDarkSearchTheme = false;

		private string mLastQuery = "";

		public SlidingSearchResultsExampleFragment()
		{
			// required empty public constructor
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			return inflater.Inflate(Resource.Layout.fragment_sliding_search_results_example_fragment, container, false);
		}

		public override void OnViewCreated(View view, Bundle savedInstanceState)
		{
			base.OnViewCreated(view, savedInstanceState);

			mSearchView = view.FindViewById<FloatingSearchView>(Resource.Id.floating_search_view);
			mSearchResultsList = view.FindViewById<RecyclerView>(Resource.Id.search_results_list);

			SetupFloatingSearch();
			SetupResultsList();
			SetupDrawer();
		}

		private void SetupFloatingSearch()
		{
			mSearchView.QueryChange += async (sender, e) =>
			{
				Console.WriteLine("QueryChange");

				if (!string.IsNullOrEmpty(e.OldQuery) && string.IsNullOrEmpty(e.NewQuery))
				{
					mSearchView.ClearSuggestions();
				}
				else
				{
					// this shows the top left circular progress
					// you can call it where ever you want, but
					// it makes sense to do it when loading something in
					// the background.
					mSearchView.ShowProgress();

					// simulates a query call to a data source
					// with a new query.
					var results = await DataHelper.FindSuggestionsAsync(Activity, e.NewQuery, 5, FIND_SUGGESTION_SIMULATED_DELAY);

					// this will swap the data and
					// render the collapse/expand animations as necessary
					mSearchView.SwapSuggestions(results);

					// let the users know that the background
					// process has completed
					mSearchView.HideProgress();
				}
			};

			mSearchView.SearchAction += async (sender, e) =>
			{
				Console.WriteLine("SearchAction");

				mLastQuery = e.CurrentQuery;

				var results = await DataHelper.FindColorsAsync(Activity, e.CurrentQuery);
				mSearchResultsAdapter.SwapData(results);
			};

			mSearchView.SuggestionClicked += async (sender, e) =>
			{
				Console.WriteLine("SuggestionClicked");

				mLastQuery = e.SearchSuggestion.GetBody();

				var results = await DataHelper.FindColorsAsync(Activity, e.SearchSuggestion.GetBody());
				mSearchResultsAdapter.SwapData(results);
			};

			mSearchView.Focus += async (sender, e) =>
			{
				Console.WriteLine("Focus");

				mSearchView.SwapSuggestions(await DataHelper.GetHistoryAsync(Activity, 3));
			};

			mSearchView.FocusCleared += (sender, e) =>
			{
				Console.WriteLine("FocusCleared");

				// set the title of the bar so that when focus is returned a new query begins
				mSearchView.SetSearchBarTitle(mLastQuery);

				// you can also set setSearchText(...) to make keep the query there when not focused and when focus returns
				// mSearchView.SetSearchText(searchSuggestion.getBody());
			};

			// handle menu clicks the same way as you would in a regular activity
			mSearchView.MenuItemClick += (sender, e) =>
			{
				Console.WriteLine("MenuItemClick");

				if (e.MenuItem.ItemId == Resource.Id.action_change_colors)
				{
					mIsDarkSearchTheme = true;

					// demonstrate setting colors for items
					mSearchView.SetBackgroundColor(Color.ParseColor("#787878"));
					mSearchView.SetViewTextColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetHintTextColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetActionMenuOverflowColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetMenuItemIconColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetLeftActionIconColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetClearBtnColor(Color.ParseColor("#e9e9e9"));
					mSearchView.SetDividerColor(Color.ParseColor("#BEBEBE"));
					mSearchView.SetLeftActionIconColor(Color.ParseColor("#e9e9e9"));
				}
				else
				{
					// just print action
					Toast.MakeText(Activity.ApplicationContext, e.MenuItem.TitleFormatted.ToString(), ToastLength.Short).Show();
				}
			};

			// use this listener to listen to menu clicks when app:floatingSearch_leftAction="showHome"
			mSearchView.HomeActionClick += (sender, e) =>
			{
				Console.WriteLine("HomeActionClick");
			};

			// Here you have access to the left icon and the text of a given suggestion
			// item after as it is bound to the suggestion list. You can utilize this
			// callback to change some properties of the left icon and the text. For example, you
			// can load the left icon images using your favorite image loading library, or change text color.
			//
			// Important:
			// Keep in mind that the suggestion list is a RecyclerView, so views are reused for different
			// items in the list.
			mSearchView.BindSuggestion += (sender, e) =>
			{
				var colorSuggestion = (ColorSuggestion)e.Item;

				var textColor = mIsDarkSearchTheme ? "#ffffff" : "#000000";
				var textLight = mIsDarkSearchTheme ? "#bfbfbf" : "#787878";

				if (colorSuggestion.IsHistory)
				{
					e.LeftIcon.SetImageDrawable(ResourcesCompat.GetDrawable(Resources, Resource.Drawable.ic_history_black_24dp, null));
					Util.SetIconColor(e.LeftIcon, Color.ParseColor(textColor));
					e.LeftIcon.Alpha = 0.36f;
				}
				else
				{
					e.LeftIcon.Alpha = 0.0f;
					e.LeftIcon.SetImageDrawable(null);
				}

				e.TextView.SetTextColor(Color.ParseColor(textColor));
				var text = colorSuggestion.GetBody();
				if (string.IsNullOrEmpty(mSearchView.Query))
				{
					e.TextView.Text = text;
				}
				else
				{
					text = text.Replace(mSearchView.Query, $"<font color=\"{textLight}\">{mSearchView.Query}</font>");
					e.TextView.TextFormatted = Html.FromHtml(text);
				}
			};

			// listen for when suggestion list expands/shrinks in order to move down/up the
			// search results list
			mSearchView.SuggestionsListHeightChanged += (sender, e) =>
			{
				mSearchResultsList.TranslationY = e.NewHeight;
			};

			// When the user types some text into the search field, a clear button (and 'x' to the
			// right) of the search text is shown.
			//
			// This listener provides a callback for when this button is clicked.
			mSearchView.ClearSearchAction += (sender, e) =>
			{
				Console.WriteLine("ClearSearchAction");
			};
		}

		public override bool OnActivityBackPress()
		{
			// if mSearchView.setSearchFocused(false) causes the focused search
			// to close, then we don't want to close the activity. if mSearchView.setSearchFocused(false)
			// returns false, we know that the search was already closed so the call didn't change the focus
			// state and it makes sense to call supper onBackPressed() and close the activity
			if (!mSearchView.SetSearchFocused(false))
			{
				return false;
			}
			return true;
		}

		private void SetupResultsList()
		{
			mSearchResultsAdapter = new SearchResultsListAdapter();
			mSearchResultsList.SetAdapter(mSearchResultsAdapter);
			mSearchResultsList.SetLayoutManager(new LinearLayoutManager(Context));
		}

		private void SetupDrawer()
		{
			AttachSearchViewActivityDrawer(mSearchView);
		}
	}
}
