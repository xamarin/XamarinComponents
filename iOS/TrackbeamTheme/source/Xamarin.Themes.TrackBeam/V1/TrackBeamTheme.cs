using System;
using UIKit;
using Xamarin.Themes.Core;

namespace Xamarin.Themes.V1.TrackBeam
{
	/// <summary>
	/// V1 API for TrackBeam Theme
	/// </summary>
	[Obsolete("Use ThemeManager.Register<T>()")]
	public class TrackBeamTheme
	{
		private static void ConfirmTrackBeamTheme()
		{
			if (!ThemeManager.IsThemeSet)
			{
				ThemeManager.Register<Xamarin.Themes.TrackBeam.TrackBeamTheme>();
			}
			else if (!(ThemeManager.Current is TrackBeam.TrackBeamTheme))
			{
				ThemeManager.Register<Xamarin.Themes.TrackBeam.TrackBeamTheme>();
			}
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (string options = null)
		{
			ConfirmTrackBeamTheme();

			ThemeManager.Current.Apply();
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIView view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UINavigationBar view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UINavigationBar.UINavigationBarAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITabBar view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITabBar.UITabBarAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIToolbar view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIToolbar.UIToolbarAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIBarButtonItem view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIBarButtonItem.UIBarButtonItemAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIButton view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIButton.UIButtonAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISlider view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISlider.UISliderAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UILabel view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITextField view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISegmentedControl view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISegmentedControl.UISegmentedControlAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIProgressView view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIProgressView.UIProgressViewAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISearchBar view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISearchBar.UISearchBarAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISwitch view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UISwitch.UISwitchAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIRefreshControl view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UIRefreshControl.UIRefreshControlAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITableView view, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(view);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITableView.UITableViewAppearance appearance, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(appearance);
		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply (UITableViewController vc, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(vc);

		}

		[Obsolete("Use ThemeManager.Current.Apply()")]
		public static void Apply(UITableViewCell cell, string options = null)
		{
			ConfirmTrackBeamTheme();
			ThemeManager.Current.Apply(cell);
		}
	}
}

