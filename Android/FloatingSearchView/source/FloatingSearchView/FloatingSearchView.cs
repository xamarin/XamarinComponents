using System;
using Android.Views;
using Android.Widget;
using Java.Interop;

using FloatingSearchViews.Suggestions;
using FloatingSearchViews.Suggestions.Models;

namespace FloatingSearchViews
{
	partial class FloatingSearchView
	{
		private WeakReference setOnBindSuggestionCallback;
		private WeakReference setOnSuggestionsListHeightChanged;

		public event EventHandler<SearchSuggestionsAdapter.BindSuggestionEventArgs> BindSuggestion
		{
			add
			{
				EventHelper.AddEventHandler<SearchSuggestionsAdapter.IOnBindSuggestionCallback, SearchSuggestionsAdapter.OnBindSuggestionCallbackInvoker>(
					ref setOnBindSuggestionCallback,
					() => new SearchSuggestionsAdapter.OnBindSuggestionCallbackInvoker(this),
					SetOnBindSuggestionCallback,
					h => h.Handler += value);
			}
			remove
			{
				EventHelper.RemoveEventHandler<SearchSuggestionsAdapter.IOnBindSuggestionCallback, SearchSuggestionsAdapter.OnBindSuggestionCallbackInvoker>(
					ref setOnBindSuggestionCallback,
					v => v.Handler == null,
					v => SetOnBindSuggestionCallback(null),
					h => h.Handler -= value);
			}
		}

		public event EventHandler<SuggestionsListHeightChangedEventArgs> SuggestionsListHeightChanged
		{
			add
			{
				EventHelper.AddEventHandler<IOnSuggestionsListHeightChanged, OnSuggestionsListHeightChangedInvoker>(
					ref setOnSuggestionsListHeightChanged,
					() => new OnSuggestionsListHeightChangedInvoker(this),
					SetOnSuggestionsListHeightChanged,
					h => h.Handler += value);
			}
			remove
			{
				EventHelper.RemoveEventHandler<IOnSuggestionsListHeightChanged, OnSuggestionsListHeightChangedInvoker>(
					ref setOnSuggestionsListHeightChanged,
					v => v.Handler == null,
					v => SetOnSuggestionsListHeightChanged(null),
					h => h.Handler -= value);
			}
		}

		internal sealed class OnSuggestionsListHeightChangedInvoker : Java.Lang.Object, IOnSuggestionsListHeightChanged
		{
			private object sender;

			public OnSuggestionsListHeightChangedInvoker(object sender)
			{
				this.sender = sender;
			}

			public EventHandler<SuggestionsListHeightChangedEventArgs> Handler;

			public void OnSuggestionsListHeightChanged(float newHeight)
			{
				Handler?.Invoke(sender, new SuggestionsListHeightChangedEventArgs(newHeight));
			}
		}

		public class SuggestionsListHeightChangedEventArgs : EventArgs
		{
			public SuggestionsListHeightChangedEventArgs(float newHeight)
			{
				NewHeight = newHeight;
			}

			public float NewHeight { get; }
		}
	}
}

namespace FloatingSearchViews.Suggestions
{
	partial class SearchSuggestionsAdapter
	{
		internal sealed class OnBindSuggestionCallbackInvoker : Java.Lang.Object, IOnBindSuggestionCallback
		{
			private object sender;

			public OnBindSuggestionCallbackInvoker(object sender)
			{
				this.sender = sender;
			}

			public EventHandler<BindSuggestionEventArgs> Handler;

			public void OnBindSuggestion(View suggestionView, ImageView leftIcon, TextView textView, ISearchSuggestion item, int itemPosition)
			{
				Handler?.Invoke(sender, new BindSuggestionEventArgs(suggestionView, leftIcon, textView, item, itemPosition));
			}
		}

		public class BindSuggestionEventArgs : EventArgs
		{
			public BindSuggestionEventArgs(View suggestionView, ImageView leftIcon, TextView textView, ISearchSuggestion item, int itemPosition)
			{
				SuggestionView = suggestionView;
				LeftIcon = leftIcon;
				TextView = textView;
				Item = item;
				ItemPosition = itemPosition;
			}

			public View SuggestionView { get; }
			public ImageView LeftIcon { get; }
			public TextView TextView { get; }
			public ISearchSuggestion Item { get; }
			public int ItemPosition { get; }
		}
	}
}
