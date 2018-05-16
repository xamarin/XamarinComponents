using System;
using UIKit;
using CoreGraphics;
using REFrostedViewController;

namespace REFrostedViewController {
    
  
	public static class UIViewControllerExtensions {
        
        #region Fields
        //private static REFrostedViewController _FrostedViewController;
        #endregion
        
        #region Methods

		/// <summary>
		/// Redisplays the controller.
		/// </summary>
		/// <param name="target">Target.</param>
		/// <param name="controller">Controller.</param>
		/// <param name="frame">Frame.</param>
		public static void DisplayController(this UIViewController target, UIViewController controller, CGRect frame) 
		{
			target.AddChildViewController(controller);
			controller.View.Frame = frame;
			target.View.Add(controller.View);
			controller.DidMoveToParentViewController(target);
        }
        
		/// <summary>
		/// Rehides the controller.
		/// </summary>
		/// <param name="controller">Controller.</param>
		public static void HideController(this UIViewController controller) 
		{

			controller.WillMoveToParentViewController(null);
			controller.View.RemoveFromSuperview();
			controller.RemoveFromParentViewController();
        }
        
		/// <summary>
		/// Frosteds the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
		/// <param name="target">Target.</param>
        public static REFrostedViewController FrostedViewController(this UIViewController target) 
		{

		     var iter = target.ParentViewController;

		    while (iter != null) 
			{
				if (iter is REFrostedViewController) 
				{
		             return (REFrostedViewController)iter;

		         } else if (iter.ParentViewController != null 
					&& iter.ParentViewController != iter) 
				 {
		             iter = iter.ParentViewController;
		         } 
				else 
				{
		             iter = null;
		        }
		     }

		     return null;
		
        }
        #endregion
    }

}
