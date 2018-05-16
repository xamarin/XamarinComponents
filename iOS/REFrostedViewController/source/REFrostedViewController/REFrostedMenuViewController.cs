using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;

namespace REFrostedViewController
{

	public class REFrostedMenuViewController : UITableViewController
	{
		#region Fields
		private List<REMenuItemSection> mSections;
		private UIColor mTintColor;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the sections.
		/// </summary>
		/// <value>The sections.</value>
		public List<REMenuItemSection> Sections
		{
			get
			{
				if (mSections == null)
					mSections = new List<REMenuItemSection>();

				return mSections;
			}
			set {mSections = value; TableView.ReloadData();}
		}

		/// <summary>
		/// Gets or sets the color of the tint.
		/// </summary>
		/// <value>The color of the tint.</value>
		public UIColor TintColor
		{
			get
			{
				if (mTintColor == null)
					mTintColor = UIColor.FromRGBA(62/255.0f,68/255.0f,75/255.0f,1.0f);
				
				return mTintColor;
			}
			set
			{
				mTintColor = value;

				TableView.ReloadData();
			}

		}

		/// <summary>
		/// Gets or sets the titler.
		/// </summary>
		/// <value>The titler.</value>
		public String AvatarName
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the icon.
		/// </summary>
		/// <value>The icon.</value>
		public UIImage Avatar
		{
			get;
			set;
		}
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REFrostedMenuViewController"/> class.
		/// </summary>
		/// <param name="style">Style.</param>
		public REFrostedMenuViewController() 
			: base(UITableViewStyle.Plain)
		{

		}

		#endregion
			
		#region Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.TableView.SeparatorColor = new UIColor(150/255.0f,161/255.0f,177/255.0f,1.0f);
			this.TableView.Opaque = false;
			this.TableView.BackgroundColor = UIColor.Clear;


			var headerView = new UIView(new CGRect(0, 0, 0, 184.0f));


			var imageView = new UIImageView(new CGRect(0, 40, 100, 100));
			imageView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			imageView.Image = Avatar;
			imageView.Layer.MasksToBounds = true;
			imageView.Layer.CornerRadius = 50.0f;
			imageView.Layer.BorderColor = UIColor.White.CGColor;
			imageView.Layer.BorderWidth = 3.0f;
			imageView.Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			imageView.Layer.ShouldRasterize = true;
			imageView.ClipsToBounds = true;

			var label = new UILabel(new CGRect(0, 150, 0, 24));
			label.Text = AvatarName;
			label.Font = UIFont.FromName(@"HelveticaNeue",21);
			label.BackgroundColor = UIColor.Clear;
			label.TextColor = TintColor;
			label.SizeToFit();
			label.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;

			headerView.Add(imageView);
			headerView.Add(label);

			this.TableView.TableHeaderView = headerView;


			// Register the TableView's data source
			TableView.Source = new REFrostedViewControllerSource(this);
		}
			
		public virtual void DidSelectItem(int section, int row)
		{
			
		}

		#endregion

	}

	internal class REFrostedViewControllerSource : UITableViewSource
	{
		#region Fields
		public readonly NSString CellKey = new NSString("REFrostedMenuViewControllerCell");
		private REFrostedMenuViewController mMenuController;
		#endregion


		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REFrostedViewControllerSource"/> class.
		/// </summary>
		/// <param name="menuController">Menu controller.</param>
		/// <param name="items">Items.</param>
		internal REFrostedViewControllerSource(REFrostedMenuViewController menuController)
		{
			mMenuController = menuController;
		}

		#endregion


		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <returns>The of sections.</returns>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections(UITableView tableView)
		{
			return mMenuController.Sections.Count;
		}

		/// <summary>
		/// Rowses the in section.
		/// </summary>
		/// <returns>The in section.</returns>
		/// <param name="tableview">Tableview.</param>
		/// <param name="section">Section.</param>
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			var aSection = mMenuController.Sections[(int)section];

			return aSection.Items.Count;
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

			var aSection = mMenuController.Sections[(int)section];

			if (String.IsNullOrWhiteSpace(aSection.Title))
				return null;
			
			//     
			var view = new UIView(new CGRect(0, 0, tableView.Frame.Size.Width, 34));
			view.BackgroundColor =  UIColor.FromRGBA(167/255.0f,167/255.0f,167/255.0f,0.6f);

			UILabel label = new UILabel(new CGRect(10, 8, 0, 0));
			label.Text = aSection.Title;
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
			cell.TextLabel.TextColor =  mMenuController.TintColor;
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

			cell.ImageView.Image = null;

			var aSection = mMenuController.Sections[indexPath.Section];

			var aItem = aSection.Items[indexPath.Row];

			var aIcon = aItem.Icon;


			if (aIcon != null)
			{
				cell.ImageView.Image = aIcon.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate); 
				cell.ImageView.TintColor = mMenuController.TintColor;
			}

			cell.TextLabel.Text = aItem.Title;

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


			var aSection = mMenuController.Sections[indexPath.Section];

			var item = aSection.Items[indexPath.Row];


			if (item is REMenuActionItem 
				&& ((REMenuActionItem)item).Command != null)
			{
				this.BeginInvokeOnMainThread(((REMenuActionItem)item).Command);
			}
			else if (item is REMenuViewControllerItem 
				&& ((REMenuViewControllerItem)item).HasViewController)
			{
				var aVC = ((REMenuViewControllerItem)item).ViewController;

				var navigationController = new RENavigationController(aVC);

				mMenuController.FrostedViewController().ContentViewController = navigationController;
			}
				
			mMenuController.FrostedViewController().HideMenuViewController();
		}
	}
}

