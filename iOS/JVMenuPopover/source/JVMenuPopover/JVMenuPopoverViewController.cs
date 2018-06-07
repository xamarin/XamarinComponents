using System;
using UIKit;
using JVMenuPopover;
using CoreGraphics;
using Foundation;
using System.Collections.Generic;

namespace JVMenuPopover {
    
	[Register("JVMenuPopoverViewController")]
    /// <summary>
    /// JV menu popover view controller.
    /// </summary>
	public class JVMenuPopoverViewController : UIViewController, IJVMenuPopoverDelegate
	{
		#region Fields

		private JVMenuPopoverView _menuView;
		private UIButton _closeBtn;
		private UIBlurEffect _blurEffect;
		private UIVisualEffectView _blurEffectView;
		private UIVibrancyEffect _vibrancyEffect;
		private UIVisualEffectView _vibrancyEffectView;
		private List<JVMenuItem> _menuItems;
		private UIImage mCancelImage;
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuPopoverViewController"/> class.
		/// </summary>
		public JVMenuPopoverViewController(List<JVMenuItem> menuItems)
		{
			_menuItems = menuItems;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="JVMenuPopover.JVMenuPopoverViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public JVMenuPopoverViewController(IntPtr handle) 
			: base(handle)
		{
			
		}

		#endregion
        
		#region Properties
		/// <summary>
		/// Gets or sets the delegate.
		/// </summary>
		/// <value>The delegate.</value>
		public IJVMenuDelegate Delegate {get; set;}

		/// <summary>
		/// Gets the menu view.
		/// </summary>
		/// <value>The menu view.</value>
        internal JVMenuPopoverView MenuView {
            get 
			{
				if(_menuView == null)
				{
					_menuView = new JVMenuPopoverView(MenuItems,this.View.Frame);
					_menuView.BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f);
					_menuView.AutosizesSubviews = true;
					_menuView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
					_menuView.Delegate = this;
				}
					         
					     
				return _menuView;

            }
        }
        
		/// <summary>
		/// Gets or sets the nav controller.
		/// </summary>
		/// <value>The nav controller.</value>
		private UIViewController NavController {get; set;}
        
		/// <summary>
		/// Gets or sets the current controller.
		/// </summary>
		/// <value>The current controller.</value>
		public UIViewController CurrentController {get; set;}
        

		/// <summary>
		/// Gets or sets a value indicating whether this instance cancel image.
		/// </summary>
		/// <value><c>true</c> if this instance cancel image; otherwise, <c>false</c>.</value>
		public UIImage CancelImage
		{
			get
			{
				if (mCancelImage == null)
					return JVMenuPopoverConfig.SharedInstance.CancelImage;

				return mCancelImage;
			}
			set {mCancelImage = value;}
		}

		/// <summary>
		/// Gets the close button.
		/// </summary>
		/// <value>The close button.</value>
        private UIButton CloseBtn {
            get 
			{

				 
				if(_closeBtn == null)
				{
					var closeImg = CancelImage;
					_closeBtn = UIButton.FromType(UIButtonType.Custom);
					_closeBtn.Frame = new CGRect(15, 28, closeImg.Size.Width, closeImg.Size.Height);
					_closeBtn.BackgroundColor = UIColor.Clear;
					_closeBtn.TintColor = JVMenuPopoverConfig.SharedInstance.TintColor;
					_closeBtn.SetImage(closeImg,UIControlState.Normal);

					_closeBtn.TouchUpInside += OnCloseMenuFromController;

				}
				     
				return _closeBtn;
			
            }
        }

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		private UIImage Image {get; set;}
        
		/// <summary>
		/// Gets or sets the size of the screen.
		/// </summary>
		/// <value>The size of the screen.</value>
		private CGSize ScreenSize {get; set;}
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="JVMenuPopover.JVMenuPopoverViewController"/> done animations.
		/// </summary>
		/// <value><c>true</c> if done animations; otherwise, <c>false</c>.</value>
		private Boolean DoneAnimations {get; set;}
        
		/// <summary>
		/// Gets the blur effect.
		/// </summary>
		/// <value>The blur effect.</value>
        private UIBlurEffect BlurEffect {
            get {

			 
			     if(_blurEffect == null)
					_blurEffect = UIBlurEffect.FromStyle(UIBlurEffectStyle.Dark);
			     
			     return _blurEffect;
			
            }
        }
        
		/// <summary>
		/// Gets the blur effect view.
		/// </summary>
		/// <value>The blur effect view.</value>
        private UIVisualEffectView BlurEffectView {
            get 
			{

			    if(_blurEffectView == null)
				{
					_blurEffectView = new UIVisualEffectView(this.BlurEffect);
					_blurEffectView.Alpha = 0.6f;
					_blurEffectView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
					_blurEffectView.Frame = this.View.Frame;
				}

			     
			     return _blurEffectView;
            }
        }
        
		/// <summary>
		/// Gets the vibrancy effect.
		/// </summary>
		/// <value>The vibrancy effect.</value>
		private UIVibrancyEffect VibrancyEffect {
			get 
			{

			     if(_vibrancyEffect ==  null)
					_vibrancyEffect = UIVibrancyEffect.FromBlurEffect(this.BlurEffect);
			     
			     return _vibrancyEffect;
			
			}
		}

		/// <summary>
		/// Gets the vibrancy effect view.
		/// </summary>
		/// <value>The vibrancy effect view.</value>
        private UIVisualEffectView VibrancyEffectView {
            get 
			{
			 
			     if(_vibrancyEffectView == null)
					_vibrancyEffectView = new UIVisualEffectView(VibrancyEffect);
			         _vibrancyEffectView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			         _vibrancyEffectView.Frame = this.View.Frame;
			     
			     return _vibrancyEffectView;
            }
        }

		/// <summary>
		/// Gets the menu items.
		/// </summary>
		/// <value>The menu items.</value>
		private  List<JVMenuItem> MenuItems
		{
			get
			{
				if (_menuItems == null)
					_menuItems = new List<JVMenuItem>();

				return _menuItems;
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()  
		{
			base.ViewDidLoad();

			ControllerSetup();
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}
        
		public override void DidReceiveMemoryWarning() 
		{

			base.DidReceiveMemoryWarning();

        }
        
        private void ControllerSetup() 
		{
             // get main screen size
			this.ScreenSize = JVMenuHelper.GetScreenSize();
         
			this.View.Frame = new CGRect(0, 0, this.ScreenSize.Width, this.ScreenSize.Height);
			this.View.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			this.View.BackgroundColor = UIColor.Clear;
             
			this.Add(MenuView);
			this.Add(CloseBtn);  
        }
        
		/// <summary>
		/// Shows the menu from controller.
		/// </summary>
		/// <param name="viewController">View controller.</param>
        public void ShowMenuFromController(UIViewController viewController) 
		{
			if(this.DoneAnimations)
				return;

			this.NavController = JVMenuHelper.TopViewController();

			this.CurrentController = viewController;

			var currFrame = MenuView.Frame;

			var newFrame = this.View.Frame;

			UIView.AnimateNotify(0.15f,()=>
			{
				if (this.NavController != this.CurrentController)
				{
					this.CurrentController.View.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 0.6f, 0.6f);
				}
				
			},(bool finished) =>
			{
				if (finished)
				{
					this.Image = JVMenuHelper.TakeScreenShotOfView(this.NavController.View,false);

					this.View.BackgroundColor = UIColor.FromPatternImage(this.Image);

					if (UIDevice.CurrentDevice.CheckSystemVersion (8,0)) 
					{
						if (!UIAccessibility.IsReduceTransparencyEnabled)
						{
							//VibrancyEffectView.ContentView.Add(MenuView);
							//VibrancyEffectView.ContentView.Add(CloseBtn);
							//BlurEffectView.ContentView.Add(VibrancyEffectView);

							this.View.InsertSubview(BlurEffectView,0);
						}
					}
						
					this.DoneAnimations = true;
					this.CloseBtn.Alpha = 0.0f;
					this.MenuView.Alpha = 0.0f;

					this.NavController.PresentViewController(this,false,()=>
					{
						UIView.Animate(0.15f,0.0f, UIViewAnimationOptions.CurveEaseInOut,()=>
						{
							this.CloseBtn.Alpha = 1.0f;
							this.MenuView.Alpha = 1.0f;

							this.MenuView.TableView.ReloadData();

						},null);
					});
				}

			});

			UIApplication.SharedApplication.SetStatusBarHidden(true,UIStatusBarAnimation.Fade);
        }
        
		/// <summary>
		/// Closes the menu from controller.
		/// </summary>
		/// <param name="viewController">View controller.</param>
		public void CloseMenuFromController(UIViewController viewController)
		{
			// if we haven't finished show menu animations then return to avoid overlaps or interruptions
			if(!this.DoneAnimations)
				return;

			UIView.Animate(0.3f/1.5f,()=>
			{
				this.CurrentController.View.Transform = CGAffineTransform.Scale(CGAffineTransform.MakeIdentity(), 1.0f,1.0f);
			},() =>
			{
				this.DoneAnimations = false;

				UIApplication.SharedApplication.SetStatusBarHidden(false,UIStatusBarAnimation.Fade);

				this.NavController.DismissViewController(false,()=>
				{
							if (this.NavController != this.CurrentController)
							{
								this.CurrentController.DismissViewController(false,null);
							}
					
				});
			});
		}

		private void OnCloseMenuFromController (object sender, EventArgs e)
		{
			CloseMenuFromController(this);
		}
	        
        private void CloseMenu() 
		{
			if (this.Delegate != null)
				this.Delegate.CloseMenu(this);
        }

		#region IJVMenuPopoverDelegate implementation

		public void MenuPopOverRowSelected(JVMenuPopoverView sender, NSIndexPath indexPath)
		{
			CloseMenuFromController (null);

			JVMenuItem selItem = null;

			if (indexPath.Row < MenuItems.Count)
				selItem = MenuItems[indexPath.Row];

			if (this.Delegate != null)
			{
				if (selItem != null
					&& (selItem is JVMenuViewControllerItem 
						|| selItem is JVMenuActionItem))
				{
					if (this.NavController is UINavigationController)
					{
						this.Delegate.DidPickItem((UINavigationController)this.NavController, selItem);
					}


					return;
				}


				if (this.NavController is UINavigationController) {
					this.Delegate.SetNewViewController((UINavigationController)this.NavController, indexPath);
				}


			}
			else
			{
				if (selItem != null)
				{
					if (selItem is JVMenuActionItem 
						&& ((JVMenuActionItem)selItem).Command != null)
					{
						this.BeginInvokeOnMainThread(((JVMenuActionItem)selItem).Command);
					}

				}

			}


		}

		#endregion
		#endregion
    }
}
