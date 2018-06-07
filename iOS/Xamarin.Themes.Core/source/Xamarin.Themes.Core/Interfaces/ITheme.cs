using System;
using UIKit;

namespace Xamarin.Themes.Core.Interfaces
{
	public interface ITheme
	{
		/// <summary>
		/// Apply  theme to the whole application
		/// </summary>
		ITheme Apply();

		#region Methods

		/// <summary>
		/// Get the navigation bar backgroound
		/// </summary>
		/// <returns>The background.</returns>
		/// <param name="metrics">Metrics.</param>
		UIImage NavigationBackground(UIBarMetrics metrics);

		#endregion
		#region View Methods
		/// <summary>
		/// Apply the theme to the specified View.
		/// </summary>
		/// <param name="View">View.</param>
		ITheme Apply(UIView View, UIInterfaceOrientation orientation = UIInterfaceOrientation.Portrait);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UINavigationBar view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UIToolbar view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UIBarButtonItem view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UISlider view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UILabel view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UITextField view);

		/// <summary>
		/// Apply the specified tableview.
		/// </summary>
		/// <param name="tableview">Tableview.</param>
		ITheme Apply (UITableView tableview);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply (UIProgressView view);

		/// <summary>
		/// Apply the specified viewController.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		ITheme Apply (UITableViewController viewController);

		/// <summary>
		/// Apply the specified viewController.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		ITheme Apply (UIViewController viewController, UIInterfaceOrientation orientation = UIInterfaceOrientation.Portrait);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UISegmentedControl view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UITabBar view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UISearchBar view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UISwitch view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UIButton view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UIRefreshControl view);

		/// <summary>
		/// Apply the specified view.
		/// </summary>
		/// <param name="view">View.</param>
		ITheme Apply(UITableViewCell cell);
		#endregion

		#region Appearance Methods

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UINavigationBar.UINavigationBarAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UIBarButtonItem.UIBarButtonItemAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UIToolbar.UIToolbarAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UISlider.UISliderAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UILabel.UILabelAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UISegmentedControl.UISegmentedControlAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UIProgressView.UIProgressViewAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UITabBar.UITabBarAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UISearchBar.UISearchBarAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UISwitch.UISwitchAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UITextField.UITextFieldAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UIButton.UIButtonAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply(UIRefreshControl.UIRefreshControlAppearance appearance);

		/// <summary>
		/// Apply the specified appearance.
		/// </summary>
		/// <param name="appearance">Appearance.</param>
		ITheme Apply (UITableView.UITableViewAppearance appearance);
		#endregion

	}
}


