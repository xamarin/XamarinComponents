
using System;

using Foundation;
using UIKit;
using REFrostedViewController;

namespace REFrostedViewControllerExample.Controllers
{
	public class DEMOSecondViewController : UIViewController
	{
		
		public DEMOSecondViewController() 
			: base()
		{
			
		}
			

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.Title = @"Second Controller";

			this.View.BackgroundColor = UIColor.Orange;

			this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Menu",UIBarButtonItemStyle.Plain,(s,e)=>
			{
				if (this.NavigationController is RENavigationController)
				{
					((RENavigationController)this.NavigationController).ShowMenu();
				}
			});
				
		}
	}
}

