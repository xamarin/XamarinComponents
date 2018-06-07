using System;
using UIKit;
using Foundation;

namespace REFrostedViewController
{
	public class RENavigationController : UINavigationController
	{
		#region Fields
		private REFrostedMenuViewController _MenuViewController;
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the menu view controller.
		/// </summary>
		/// <value>The menu view controller.</value>
		public REFrostedMenuViewController MenuViewController {
			get {
				return this._MenuViewController;
			}
			set {
				this._MenuViewController = value;
			}
		}
		#endregion

		public RENavigationController(UIViewController rootVc) 
			:base(rootVc)
		{

		}
		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//this.View.AddGestureRecognizer(new UIGestureRecognizer(this, new Selector("panGestureRecognized:")));

		}

		/// <summary>
		/// Shows the menu.
		/// </summary>
		public void ShowMenu() 
		{

			this.View.EndEditing(true);
			this.FrostedViewController().View.EndEditing(true);

			this.FrostedViewController().PresentMenuViewController();
		}

		/// <summary>
		/// Pans the gesture recognized.
		/// </summary>
		/// <param name="sender">Sender.</param>
		[Export("panGestureRecognized:")]
		public void PanGestureRecognized(UIPanGestureRecognizer sender) 
		{
			this.View.EndEditing(true);
			this.FrostedViewController().View.EndEditing(true);

			this.FrostedViewController().PanGestureRecognized(sender);

		}
	}
}

