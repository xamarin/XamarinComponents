using System;
using UIKit;

namespace Stripe.UIExamples
{
	class ThemeViewController : UITableViewController
	{
		public ThemeExample Theme { get; set; } = ThemeExample.Default;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Change Theme";

			TableView.TableFooterView = new UIView ();

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Cancel, Cancel);
		}

		void Cancel (object sender, EventArgs e)
		{
			DismissViewController (true, null);
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return Enum.GetValues (typeof (ThemeExample)).Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = new UITableViewCell (UITableViewCellStyle.Default, null);

			var theme = (ThemeExample)indexPath.Row;
			cell.TextLabel.Text = Enum.GetName (typeof (ThemeExample), theme);
			cell.Accessory = theme == Theme  ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		public override void RowSelected (UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);

			var theme = (ThemeExample)indexPath.Row;
			Theme = theme;

			tableView.ReloadSections (new Foundation.NSIndexSet ((nuint)indexPath.Section), UITableViewRowAnimation.Automatic);

			DismissViewController (true, null);
		}
	}

	enum ThemeExample
	{
		Default = 0,
		CustomDark = 1,
		CustomLight = 2,
	}

	static class ThemeExampleExtensions
	{
		public static Stripe.iOS.Theme GetStripeTheme (this ThemeExample This)
		{
			Stripe.iOS.Theme theme = Stripe.iOS.Theme.DefaultTheme;

			switch (This)
			{
				case ThemeExample.Default:
					return theme;
				case ThemeExample.CustomDark:
					theme.PrimaryBackgroundColor = new UIColor (red: 66.0f / 255.0f, green: 69.0f / 255.0f, blue: 112.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.SecondaryBackgroundColor = theme.PrimaryBackgroundColor;
					theme.PrimaryForegroundColor = UIColor.White;
					theme.SecondaryForegroundColor = new UIColor (red: 130.0f / 255.0f, green: 147.0f / 255.0f, blue: 168.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.AccentColor = new UIColor (red: 14.0f / 255.0f, green: 211.0f / 255.0f, blue: 140.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.ErrorColor = new UIColor (red: 237.0f / 255.0f, green: 83.0f / 255.0f, blue: 69.0f / 255.0f, alpha: 255.0f / 255.0f);
					return theme;
				case ThemeExample.CustomLight:
					theme.PrimaryBackgroundColor = new UIColor (red: 230.0f / 255.0f, green: 235.0f / 255.0f, blue: 241.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.SecondaryBackgroundColor = UIColor.White;
					theme.PrimaryForegroundColor = new UIColor (red: 55.0f / 255.0f, green: 53.0f / 255.0f, blue: 100.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.SecondaryForegroundColor = new UIColor (red: 148.0f / 255.0f, green: 163.0f / 255.0f, blue: 179.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.AccentColor = new UIColor (red: 101.0f / 255.0f, green: 101.0f / 255.0f, blue: 232.0f / 255.0f, alpha: 255.0f / 255.0f);
					theme.ErrorColor = new UIColor (red: 240.0f / 255.0f, green: 2.0f / 255.0f, blue: 36.0f / 255.0f, alpha: 255.0f / 255.0f);
					return theme;
				default:
					throw new NotImplementedException ();
			}
		}
	}
}
