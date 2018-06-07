using System;
using UIKit;
using REFrostedViewController;

namespace REFrostedViewControllerExample.Controllers
{
	public class DEMOHomeViewController : UIViewController
	{
		public override void ViewDidLoad() 
		{
			base.ViewDidLoad();

			this.Title = @"Home Controller";

			this.NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Menu",UIBarButtonItemStyle.Plain,(s,e)=>
			{
				if (this.NavigationController is RENavigationController)
				{
					((RENavigationController)this.NavigationController).ShowMenu();
				}
			});
 
			var aImage =  UIImage.FromBundle("ball.png");
			var imageView =  new UIImageView(this.View.Bounds);
			imageView.Image = aImage;
			imageView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			imageView.ContentMode = UIViewContentMode.ScaleAspectFill;

			this.Add(imageView);

		}
	}
}

