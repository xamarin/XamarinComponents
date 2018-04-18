using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using CoreGraphics;
using REFrostedViewController;

namespace REFrostedViewControllerExample.Controllers
{
	/// <summary>
	/// Example menu view controller
	/// </summary>
	public class DEMOMenuViewController : UITableViewController
	{
		

		public DEMOMenuViewController(UITableViewStyle style) 
			: base(style)
		{
			
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TableView.SeparatorColor = new UIColor(150/255.0f,161/255.0f,177/255.0f,1.0f);
			this.TableView.Opaque = false;
			this.TableView.BackgroundColor = UIColor.Clear;


			var headerView = new UIView(new CGRect(0, 0, 0, 184.0f));


			var imageView = new UIImageView(new CGRect(0, 40, 100, 100));
			imageView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
		    imageView.Image = UIImage.FromBundle(@"avatar.jpg");
			imageView.Layer.MasksToBounds = true;
			imageView.Layer.CornerRadius = 50.0f;
			imageView.Layer.BorderColor = UIColor.White.CGColor;
			imageView.Layer.BorderWidth = 3.0f;
			imageView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			imageView.Layer.ShouldRasterize = true;
			imageView.ClipsToBounds = true;

			var label = new UILabel(new CGRect(0, 150, 0, 24));
			label.Text = @"Roman Efimov";
			label.Font = UIFont.FromName(@"HelveticaNeue",21);
			label.BackgroundColor = UIColor.Clear;
			label.TextColor = new UIColor(62/255.0f,68/255.0f,75/255.0f,1.0f);
			label.SizeToFit();
			label.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;

			headerView.Add(imageView);
			headerView.Add(label);

			this.TableView.TableHeaderView = headerView;
    

			// Register the TableView's data source
			TableView.Source = new DEMOMenuViewControllerSource(this);
		}
			
	}

	public class DEMOMenuViewControllerSource : UITableViewSource
	{
		public readonly NSString CellKey = new NSString("DEMOMenuViewControllerCell");

		private DEMOMenuViewController mMenuController;

		public DEMOMenuViewControllerSource(DEMOMenuViewController menuController)
		{
			mMenuController = menuController;
		}

		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <returns>The of sections.</returns>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections(UITableView tableView)
		{
			return 2;
		}

		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return 3;
		}

		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 54;
		}

//		public override string TitleForHeader(UITableView tableView, nint section)
//		{
//			return "Header";
//		}
//
//		public override string TitleForFooter(UITableView tableView, nint section)
//		{
//			return "Footer";
//		}

		/// <summary>
		/// Gets the height for header.
		/// </summary>
		/// <returns>The height for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			if (section == 0)
		         return 0;
		     
		     return 34;
		}

		/// <summary>
		/// Gets the view for header.
		/// </summary>
		/// <returns>The view for header.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="section">Section.</param>
		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			// 
		     if (section == 0)
		         return null;
			//     
			var view = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, 34));
			view.BackgroundColor =  UIColor.FromRGBA(167/255.0f,167/255.0f,167/255.0f,0.6f);
		     
			UILabel label = new UILabel(new CGRect(10, 8, 0, 0));
		     label.Text = @"Friends Online";
			label.Font = UIFont.SystemFontOfSize(15);
			label.TextColor = UIColor.White;
		     label.BackgroundColor = UIColor.Clear;
			label.SizeToFit();

			view.Add(label);
		     
		     return view;
		}

		/// <summary>
		/// Wills the display.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="cell">Cell.</param>
		/// <param name="indexPath">Index path.</param>
		public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			// 
			cell.BackgroundColor = UIColor.Clear;
			cell.TextLabel.TextColor =  UIColor.FromRGBA(62/255.0f,68/255.0f,75/255.0f,1.0f);
			cell.TextLabel.Font = UIFont.FromName(@"HelveticaNeue",17);
		}

		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(CellKey);

			if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Default,CellKey);
				   
		    if (indexPath.Section == 0) 
			{
				var titles = new List<String>(){@"Home", @"Profile", @"Chats"};
		         cell.TextLabel.Text = titles[indexPath.Row];
		     } 
			else 
			{
				var titles = new List<String>(){@"John Appleseed", @"John Doe", @"Test User"};
		         cell.TextLabel.Text = titles[indexPath.Row];
			}
				


			return cell;
		}

		/// <summary>
		/// Rows the selected.
		/// </summary>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath,true);

		     if (indexPath.Section == 0 && indexPath.Row == 0) {
				var homeViewController = new DEMOHomeViewController();
				var navigationController = new RENavigationController(homeViewController);

				mMenuController.FrostedViewController().ContentViewController = navigationController;
		     } else {
				var secondViewController = new DEMOSecondViewController();
				var navigationController = new RENavigationController(secondViewController);

				mMenuController.FrostedViewController().ContentViewController = navigationController;
		     }

			mMenuController.FrostedViewController().HideMenuViewController();
		}
	}
}

