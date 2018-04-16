using System;
using UIKit;
using JVMenuPopover;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;

namespace JVMenuPopover
{
	[Register("JVMenuViewController")]
	public class JVMenuViewController : UIViewController,IJVMenuDelegate
	{
		#region Properties

		/// <summary>
		/// Gets the menu controller.
		/// </summary>
		/// <value>The menu controller.</value>
		private JVMenuPopoverViewController menuController
		{
			get
			{
				if(_menuController == null)
				{
					
					var items = (_menuItems != null) ? _menuItems : JVMenuPopoverConfig.SharedInstance.MenuItems;
						
					_menuController = new JVMenuPopoverViewController(items);
					_menuController.Delegate = this;
				}

				return _menuController;

			}
		}

		/// <summary>
		/// Sets the menu button image.
		/// </summary>
		/// <value>The menu button image.</value>
		public UIImage MenuButtonImage
		{
			get
			{
				if (_menuImg == null)
					return JVMenuPopoverConfig.SharedInstance.MenuImage;

				return _menuImg;
			}
			set 
			{
				_menuImg = value;
			}

		}

		#endregion

		#region Fields

		private UIImage _menuImg;

		private JVMenuPopoverViewController _menuController;
		private List<JVMenuItem> _menuItems;
		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuViewController"/> class.
		/// </summary>
		public JVMenuViewController() 
			: base()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuViewController"/> class.
		/// </summary>
		/// <param name="menuItems">Menu items to override the shared menu items</param>
		public JVMenuViewController(List<JVMenuItem> menuItems) 
			: this()
		{
			_menuItems = menuItems;
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public JVMenuViewController(IntPtr handle) 
			: base(handle)
		{
			
		}

		#endregion

		#region Methods

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			this.NavigationController.WeakDelegate = this;
		

			//self.navigationController.delegate = self;
			NavigationItem.LeftBarButtonItem = new UIBarButtonItem(MenuButtonImage, UIBarButtonItemStyle.Plain,(s,e)=>
			{
				menuController.ShowMenuFromController(this);
			});
					
			// make the navigation bar transparent
			NavigationController.NavigationBar.SetBackgroundImage(new UIImage(),UIBarMetrics.Default);


			NavigationController.NavigationBar.ShadowImage = new UIImage();
			NavigationController.NavigationBar.Translucent = true;
			NavigationController.View.BackgroundColor = UIColor.Clear;
			NavigationController.NavigationBar.BackgroundColor = UIColor.Clear;
		}

		#endregion

		#region IJVMenuDelegate implementation

		/// <summary>
		/// Did pick the item
		/// </summary>
		/// <param name="item">Item.</param>
		public virtual void DidPickItem(UINavigationController navController, JVMenuItem item)
		{
			if (item is JVMenuActionItem 
				&& ((JVMenuActionItem)item).Command != null)
			{
				this.BeginInvokeOnMainThread(((JVMenuActionItem)item).Command);
			}
			else if (item is JVMenuViewControllerItem 
				&& ((JVMenuViewControllerItem)item).HasViewController)
			{
				var aVC = ((JVMenuViewControllerItem)item).ViewController;

				if (aVC != menuController.CurrentController)
				{
					aVC.View.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 0.6f, 0.6f);
					navController.ViewControllers = new UIViewController[]{aVC};
				}
			}
		}

		public virtual void SetNewViewController(UINavigationController navController, Foundation.NSIndexPath indexPath)
		{

		}
			
		public void CloseMenu(JVMenuPopoverViewController menuController)
		{
			NavigationController.PopToViewController(menuController, false);
		}

		#endregion


		[Export("navigationController:willShowViewController:animated:")]
		public virtual void WillShowViewController(UINavigationController navigationController, UIViewController viewController, bool animated)
		{
			UIView.Animate(0.3f/1.5f,()=>
			{
				viewController.View.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 1.0f, 1.0f);
			});
		}
	}
}

