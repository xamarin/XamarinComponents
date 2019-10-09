using System;
using UIKit;
using APDropDownNavToolbar;

namespace APDropDownNavToolbarSample
{
	public class DemoViewController : UIViewController
	{
		public DemoViewController()
		{
			
		}

		/// <summary>
		/// Gets or sets the nav controller.
		/// </summary>
		/// <value>The nav controller.</value>
		public APNavigationController NavController 
		{
			get;
			set;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.View.BackgroundColor = UIColor.White;

			if (this.NavigationController != null 
				&& this.NavigationController is APNavigationController)
			{
				NavController = (APNavigationController)this.NavigationController;
				NavController.ActiveBarButtonTitle = "Hide";
				NavController.ActiveNavigationBarTitle = "Tool bar visible";

				this.NavigationItem.RightBarButtonItem = new UIBarButtonItem("Show", UIBarButtonItemStyle.Plain, DidSelectShow);
			}
		}

		/// <summary>
		/// Dids the select show.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="evt">Evt.</param>
		public void DidSelectShow(object sender, EventArgs evt)
		{
			this.NavController.DropDownToolbar.Items = new UIBarButtonItem[]
			{
				new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
				new UIBarButtonItem(UIBarButtonSystemItem.Edit),
				new UIBarButtonItem(UIBarButtonSystemItem.Action),
			};

		    if(this.NavController.IsDropDownVisible)
			{
				this.NavController.HideDropDown(sender);
			}
	        else
			{
				this.NavController.ShowDropDown(sender);
			}
		}
	}
}

