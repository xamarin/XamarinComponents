using System;
using UIKit;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;

namespace JVMenuPopover {
    
    
    public class JVMenuPopoverView : UIView {
        
		#region Fields
		private UITableView _tableView;
		private string cellIdentifier = "Cell";
		private List<JVMenuItem> _MenuItems; 
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuPopoverView"/> class.
		/// </summary>
		public JVMenuPopoverView(List<JVMenuItem> items) : base() 
		{
			_MenuItems = items;

			Setup();
        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuPopoverView"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public JVMenuPopoverView(List<JVMenuItem> items, CGRect frame) : base(frame) 
		{
			_MenuItems = items;

			Setup();
        }
        #endregion

		#region Properties

		/// <summary>
		/// Gets or sets the delegate.
		/// </summary>
		/// <value>The delegate.</value>
		internal IJVMenuPopoverDelegate Delegate { get; set;}
        
		/// <summary>
		/// Gets or sets the table view.
		/// </summary>
		/// <value>The table view.</value>
        public UITableView TableView {
            get 
			{
				 
			    if(_tableView == null)
				{
					_tableView = new UITableView(new CGRect(0, 70, this.Frame.Size.Width, this.Frame.Size.Height),UITableViewStyle.Plain);
					//_tableView = new UITableView(new CGRect(0, 70, 320, 480),UITableViewStyle.Plain);
					_tableView.BackgroundColor = UIColor.Clear;

					_tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

					if (UIDevice.CurrentDevice.CheckSystemVersion (8,0)) 
					{
						_tableView.LayoutMargins = UIEdgeInsets.Zero;
					}

					_tableView.Bounces = false;
					_tableView.ScrollEnabled = false;
					_tableView.WeakDelegate = this;
					_tableView.WeakDataSource = this;
					_tableView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;

				}

				     
				return _tableView;

            }
        }
        
		/// <summary>
		/// Gets or sets the size of the screen.
		/// </summary>
		/// <value>The size of the screen.</value>
		private CGSize ScreenSize { get; set;}
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="JVMenuPopover.JVMenuPopoverView"/> done cell animations.
		/// </summary>
		/// <value><c>true</c> if done cell animations; otherwise, <c>false</c>.</value>
		private Boolean DoneCellAnimations { get; set;}
        
		/// <summary>
		/// Gets the menu items.
		/// </summary>
		/// <value>The menu items.</value>
		private List<JVMenuItem> MenuItems  
		{
			get
			{
				if (_MenuItems == null)
					_MenuItems = new List<JVMenuItem>();

				return _MenuItems;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Setup this instance.
		/// </summary>
        private void Setup() 
		{
             
            if(this.Frame.Size.Width == 0)
			{
				this.ScreenSize = JVMenuHelper.GetScreenSize();

				this.Frame = new CGRect(0, 0, ScreenSize.Width, ScreenSize.Height);
			}

                 
			this.BackgroundColor =  UIColor.Black.ColorWithAlpha(0.5f); 

			this.Add(TableView);

        }
        
		/// <summary>
		/// Wills the move to window.
		/// </summary>
		/// <param name="newWindow">New window.</param>
        public override void WillMoveToWindow(UIWindow newWindow) 
		{
			DoneCellAnimations = false;

			base.WillMoveToWindow(newWindow);
        }
        
		#endregion

		#region TableView Datasource and Delegate functions

		[Export("tableView:didEndDisplayingCell:forRowAtIndexPath:")]
        private void DidEndDisplayingCell(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
            // 
            //     // do something after display
        }
        
		[Export("tableView:willDisplayCell:forRowAtIndexPath:")]
        private void WillDisplayCell(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath) {
 
            if (this.DoneCellAnimations)
				return;
			
            var oldFrame = cell.Frame;
			var newFrame = new CGRect(-cell.Frame.Size.Width, cell.Frame.Top, 0, cell.Frame.Size.Height);
               
            var velocity = cell.Frame.Size.Width/100;

			cell.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 0.95f, 0.0001f);
			cell.Frame = newFrame;

			UIView.AnimateNotify(0.3f/1.5f,0.05f * indexPath.Row, 0.67f, velocity,UIViewAnimationOptions.TransitionNone,()=>
			{
				cell.Frame = oldFrame;
				cell.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 1.0f, 1.0f);
				
			},(finished)=>
			{
				var rows = TableView.NumberOfRowsInSection(0);

				if (rows == indexPath.Row+1)
					DoneCellAnimations = true;
			});

        }
        
		[Export("tableView:cellForRowAtIndexPath:")]
        private UITableViewCell CellForRowAtIndexPath(UITableView tableView, NSIndexPath indexPath) 
		{
             
			var cell = tableView.DequeueReusableCell(cellIdentifier);

	        if (cell == null)
				cell = new UITableViewCell(UITableViewCellStyle.Default,cellIdentifier);

			if (TableView.RespondsToSelector(new ObjCRuntime.Selector("setLayoutMargins:")))
				TableView.LayoutMargins = UIEdgeInsets.Zero;

			if (cell.RespondsToSelector(new ObjCRuntime.Selector("setSeparatorInset:")))
				cell.SeparatorInset = UIEdgeInsets.Zero;

			if (cell.RespondsToSelector(new ObjCRuntime.Selector("setPreservesSuperviewLayoutMargins:")))
				cell.PreservesSuperviewLayoutMargins = false;

			if (cell.RespondsToSelector(new ObjCRuntime.Selector("setLayoutMargins:")))
				cell.LayoutMargins = UIEdgeInsets.Zero;
				                 
            // setups cell
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			cell.BackgroundColor = UIColor.Clear;
             
			cell.TextLabel.BackgroundColor = UIColor.Clear;
			cell.TextLabel.Font = UIFont.FromName(JVMenuPopoverConfig.SharedInstance.FontName,JVMenuPopoverConfig.SharedInstance.FontSize);
			cell.TextLabel.TextColor = JVMenuPopoverConfig.SharedInstance.TintColor;
            cell.TextLabel.TextAlignment = UITextAlignment.Left;


			var item = MenuItems[indexPath.Row];

			cell.ImageView.Image = item.Icon;  
			cell.ImageView.TintColor = JVMenuPopoverConfig.SharedInstance.TintColor;
			cell.TextLabel.Text = item.Title;
			                        
			return cell;

        }
        
		[Export("tableView:didSelectRowAtIndexPath:")]
        private void DidSelectRowAtIndexPath(UITableView tableView, NSIndexPath indexPath) 
		{
			tableView.DeselectRow(indexPath,true);

			if (Delegate != null)
			{
				Delegate.MenuPopOverRowSelected(this,indexPath);
			}
				
        }
        
		[Export("tableView:numberOfRowsInSection:")]
        private nint NumberOfRowsInSection(UITableView tableView, nint section) 
		{
			return MenuItems.Count;
        }
        
		[Export("tableView:heightForRowAtIndexPath:")]
        private nfloat HeightForRowAtIndexPath(UITableView tableView, NSIndexPath indexPath) 
		{
			return (nfloat)JVMenuPopoverConfig.SharedInstance.RowHeight;
        }
        
		[Export("tableView:viewForHeaderInSection:")]
		private UIView ViewForHeaderInSection(UITableView tableView, nint section) {

			return new UIView(CGRect.Empty);
        }
        
		[Export("tableView:heightForHeaderInSection:")]
		private nfloat HeightForHeaderInSection(UITableView tableView, nint section) {
			return 0;
        }

		#endregion
    }

}
