using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using JVMenuPopover;
using System.Collections.Generic;
using CoreAnimation;
using CoreGraphics;

namespace JVMenuPopoverMenuSample
{
	partial class MainViewController : UIViewController
	{
		#region Properties
	
		public CAGradientLayer Gradient {get; set;}
		public UIView ContainerView { get; set;}

		#endregion

		#region Fields

		private JVMenuPopoverViewController _menuController;

		#endregion

		public MainViewController (IntPtr handle) : base (handle)
		{
			this.Title = "JVMenuPopover Sample";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//build the shared menu
			var menuItem = new List<JVMenuItem>()
			{
				new JVMenuActionItem()
				{
					//View exisiting view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"home-48"),
					Title = @"Home",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","Home",null,"OK",null);
						uiAlert.Show();
					},
				},
				new JVMenuActionItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"about-48"),
					Title = @"About Us",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","About Us",null,"OK",null);
						uiAlert.Show();
					},
				},
				new JVMenuActionItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"settings-48"),
					Title = @"Our Service",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","Our Service",null,"OK",null);
						uiAlert.Show();
					},
				},
				new JVMenuActionItem()
				{
					//New view controller, will be reused everytime the item is selected
					Icon = UIImage.FromBundle(@"business_contact-48"),
					Title = @"Contact Us",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","Contact Us",null,"OK",null);
						uiAlert.Show();
					},
				},
				new JVMenuActionItem
				{
					//New view controller, will be recreated afresh everytime the item is selected
					Icon = UIImage.FromBundle(@"ask_question-48"),
					Title = @"Help?",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","Help?",null,"OK",null);
						uiAlert.Show();
					},
				},
				new JVMenuActionItem()
				{
					//Action is called, on the UI thread, everytime the item is selected
					Icon = UIImage.FromBundle(@"ask_question-48"),
					Title = @"Logout",
					Command = ()=>
					{
						var uiAlert = new UIAlertView("Menu Item Clicked","You clicked logout",null,"OK",null);
						uiAlert.Show();
					},
				},
			};

			_menuController = new JVMenuPopoverViewController(menuItem);
		
			this.View.BackgroundColor = UIColor.Clear;

			ContainerView =  new UIView(this.View.Frame);

			// gradient background color
			Gradient = (CAGradientLayer)CAGradientLayer.Create(); 
			Gradient.Frame = this.View.Frame;

			var firstColor =  JVMenuHelper.ColorWithHexString("52EDC7");
			var secondColor = JVMenuHelper.ColorWithHexString("5AC8FB");

			Gradient.Colors = new CoreGraphics.CGColor[]{firstColor.CGColor, secondColor.CGColor};


			ContainerView.Layer.InsertSublayer(Gradient,0);



			this.View.Add(ContainerView);

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(JVMenuPopoverConfig.SharedInstance.MenuImage, UIBarButtonItemStyle.Plain,(s,e)=>
			{
				_menuController.ShowMenuFromController(this);
			});

			NavigationItem.LeftBarButtonItem.TintColor = UIColor.Black;
		}
	}
}
