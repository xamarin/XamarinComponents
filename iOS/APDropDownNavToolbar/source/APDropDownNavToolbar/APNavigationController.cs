using System;
using UIKit;
using Foundation;
using CoreGraphics;

namespace APDropDownNavToolbar 
{
    /// <summary>
	/// APNavigationController controller
    /// </summary>
    public class APNavigationController : UINavigationController {
        
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="APDropDownNavToolbar.APNavigationController"/> class.
		/// </summary>
        public APNavigationController() {
        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="APDropDownNavToolbar.APNavigationController"/> class.
		/// </summary>
		/// <param name="nibNameOrNil">Nib name or nil.</param>
		/// <param name="nibBundleOrNil">Nib bundle or nil.</param>
		public APNavigationController(String nibNameOrNil, NSBundle nibBundleOrNil) :base(nibNameOrNil, nibBundleOrNil) {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="APDropDownNavToolbar.APNavigationController"/> class.
		/// </summary>
		/// <param name="rootViewController">Root view controller.</param>
		public APNavigationController(UIViewController rootViewController) 
			: base(rootViewController)
		{
			
		}

        #endregion

		#region Properties
		/// <summary>
		/// Gets or sets the drop down toolbar.
		/// </summary>
		/// <value>The drop down toolbar.</value>
		public UIToolbar DropDownToolbar {get; set;}
        
		/// <summary>
		/// Gets or sets the active navigation bar title.
		/// </summary>
		/// <value>The active navigation bar title.</value>
		public String ActiveNavigationBarTitle {get; set;}
        
		/// <summary>
		/// Gets or sets the active bar button title.
		/// </summary>
		/// <value>The active bar button title.</value>
		public String ActiveBarButtonTitle {get; set;}
        
		/// <summary>
		/// Gets or sets a value indicating whether this instance is drop down visible.
		/// </summary>
		/// <value><c>true</c> if this instance is drop down visible; otherwise, <c>false</c>.</value>
		public Boolean IsDropDownVisible {get; set;}
        
		/// <summary>
		/// Gets or sets the original navigation bar title.
		/// </summary>
		/// <value>The original navigation bar title.</value>
		public String OriginalNavigationBarTitle {get; set;}
        
		/// <summary>
		/// Gets or sets the original bar button title.
		/// </summary>
		/// <value>The original bar button title.</value>
		public String OriginalBarButtonTitle {get; set;}
		#endregion

		#region Methods
        
		/// <summary>
		/// Views the did load.
		/// </summary>
        public override void ViewDidLoad() 
		{
			base.ViewDidLoad();

            // 	// Do any additional setup after loading the view.
			this.DropDownToolbar = new UIToolbar(new CGRect(0, 0, this.View.Frame.Size.Width, 44));
			this.DropDownToolbar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			this.DropDownToolbar.TintColor = this.NavigationBar.TintColor;

			this.NavigationBar.Superview.InsertSubviewBelow(this.DropDownToolbar, this.NavigationBar);
	
			if (this.NavigationBar.TopItem != null)
				this.OriginalNavigationBarTitle = this.NavigationBar.TopItem.Title;


        }
        
		/// <summary>
		/// Toggles the drop down.
		/// </summary>
		/// <param name="sender">Sender.</param>
        public void ToggleDropDown(object sender) 
		{
             
             if (this.IsDropDownVisible) 
			{
				this.HideDropDown(sender);
			} else {
				this.ShowDropDown(sender);
			}
        }
        
		/// <summary>
		/// Hides the drop down.
		/// </summary>
		/// <param name="sender">Sender.</param>
        public void HideDropDown(object sender) {
         
            if(this.IsDropDownVisible)
			{
                var frame = this.DropDownToolbar.Frame;

				frame.Y = this.NavigationBar.Frame.Bottom;

				this.DropDownToolbar.Frame = frame;

				UIView.Animate(0.25f,()=>
				{
					var frame2 = this.DropDownToolbar.Frame;
					frame2.Y = 0.0f;
					this.DropDownToolbar.Frame = frame2;
				},()=>
				{
					this.IsDropDownVisible = !this.IsDropDownVisible;
					this.DropDownToolbar.Hidden = true;

				});

				this.NavigationBar.TopItem.Title = this.OriginalBarButtonTitle;

				if (sender != null && sender is UIBarButtonItem)
				{
					((UIBarButtonItem)sender).Title = this.OriginalBarButtonTitle;
				}
					
			}
        }
        
		/// <summary>
		/// Shows the drop down.
		/// </summary>
		/// <param name="sender">Sender.</param>
        public void ShowDropDown(object sender) 
		{

			if(!this.IsDropDownVisible)
			{
				var frame = this.DropDownToolbar.Frame;
				frame.Y = 0.0f;
				this.DropDownToolbar.Hidden = false;
				this.DropDownToolbar.Frame = frame;

				UIView.Animate(0.25f,()=>
				{
					var frame2 = this.DropDownToolbar.Frame;
					frame2.Y = this.NavigationBar.Frame.Bottom;
					this.DropDownToolbar.Frame = frame2;
				},()=>
				{
					this.IsDropDownVisible = !this.IsDropDownVisible;
				});

				if (!String.IsNullOrWhiteSpace(this.ActiveNavigationBarTitle))
					this.NavigationBar.TopItem.Title = this.ActiveNavigationBarTitle;

				if (sender != null && sender is UIBarButtonItem)
				{
					this.OriginalBarButtonTitle = ((UIBarButtonItem)sender).Title;

					if (!String.IsNullOrWhiteSpace(this.ActiveBarButtonTitle))
						((UIBarButtonItem)sender).Title = this.ActiveBarButtonTitle;

				}

			}
				
        }

		#endregion
    }
}
