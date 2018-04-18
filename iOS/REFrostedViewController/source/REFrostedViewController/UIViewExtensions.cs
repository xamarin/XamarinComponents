using System;
using CoreGraphics;
using UIKit;

namespace REFrostedViewController {
    
    
    #region 

	/// <summary>
	/// User interface view extensions.
	/// </summary>
	public static class UIViewExtensions {
        
        #region Methods

		/// <summary>
		/// Build a screenshot.
		/// </summary>
		/// <returns>The screenshot.</returns>
		/// <param name="target">Target.</param>
        public static UIImage Screenshot(this UIView target) 
		{
         
			UIGraphics.BeginImageContextWithOptions(target.Bounds.Size, false, UIScreen.MainScreen.Scale);
             
			if (target.RespondsToSelector(new ObjCRuntime.Selector("drawViewHierarchyInRect:afterScreenUpdates:"))) 
			{
				target.DrawViewHierarchy(target.Bounds,true);
			}
			else 
			{
				target.Layer.RenderInContext(UIGraphics.GetCurrentContext());
			}
             
			UIImage image = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return image;
        }

        #endregion
    }

    #endregion
}
