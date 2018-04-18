using System;
using Foundation;
using CoreGraphics;
using UIKit;
using CoreAnimation;

namespace JVMenuPopover {
    
    /// <summary>
	/// JVMenuHelper 
    /// </summary>
    public class JVMenuHelper : NSObject {
        
		public static CGSize GetScreenSize() {
             
			CGSize screenSize = UIScreen.MainScreen.Bounds.Size;
                 
//                 if ((NSFoundationVersionNumber <= NSFoundationVersionNumber_iOS_7_1) && UIInterfaceOrientationIsLandscape([UIApplication sharedApplication].statusBarOrientation))
//                     return CGSizeMake(screenSize.height, screenSize.width);
                 
            return screenSize;
        }
        
		/// <summary>
		/// Images the with image.
		/// </summary>
		/// <returns>The with image.</returns>
		/// <param name="sourceImage">Source image.</param>
		/// <param name="i_width">I width.</param>
        public static UIImage ImageWithImage(UIImage sourceImage, Double i_width) {
             
                 var oldWidth = sourceImage.Size.Width;
                 var scaleFactor = i_width / oldWidth;
                 
			     var newHeight = sourceImage.Size.Height * scaleFactor;
                 var newWidth = oldWidth * scaleFactor;
                 
                 UIGraphics.BeginImageContext(new CGSize(newWidth, newHeight));

			     sourceImage.Draw(new CGRect(0, 0, newWidth, newHeight));
             
                 var newImage = UIGraphics.GetImageFromCurrentImageContext();
                 UIGraphics.EndImageContext();
                 
                 return newImage;
        }
        
		/// <summary>
		/// Colors the with hex string.
		/// </summary>
		/// <returns>The with hex string.</returns>
		/// <param name="hexValue">Hex value.</param>
		/// <param name="alpha">Alpha.</param>
		public static UIColor ColorWithHexString (string hexValue, float alpha = 1.0f)
		{
			var colorString = hexValue.Replace ("#", "");
			if (alpha > 1.0f) {
				alpha = 1.0f;
			} else if (alpha < 0.0f) {
				alpha = 0.0f;
			}

			float red, green, blue;

			switch (colorString.Length) 
			{
				case 3 : // #RGB
					{
						red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
						green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
						blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
						return UIColor.FromRGBA(red, green, blue, alpha);
					}
				case 6 : // #RRGGBB
					{
						red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
						green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
						blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
						return UIColor.FromRGBA(red, green, blue, alpha);
					}   

				default :
					throw new ArgumentOutOfRangeException(string.Format("Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB", hexValue));

			}
		}
			      
		/// <summary>
		/// Colors the with RGB hex.
		/// </summary>
		/// <returns>The with RGB hex.</returns>
		/// <param name="hex">Hex.</param>
        public static UIColor ColorWithRGBHex(UInt32 hex) {
             
                 //converts a hex number into a colour
            var r = (hex >> 16) & 0xFF;
			var g = (hex >> 8) & 0xFF;
			var b = (hex) & 0xFF;
                 
			return UIColor.FromRGBA(r / 255.0f, g / 255.0f, b / 255.0f, 1.0f);
        }
        
		/// <summary>
		/// Removes the layer from view.
		/// </summary>
		/// <param name="view">View.</param>
        public static void RemoveLayerFromView(UIView view) {
             
			CAGradientLayer layerToRemove = null;

            foreach (CALayer aLayer in view.Layer.Sublayers) 
			{
				if (aLayer is CAGradientLayer)
				{
					layerToRemove = (CAGradientLayer)aLayer;
				}
			}
                 
			if (layerToRemove != null)
				layerToRemove.RemoveFromSuperLayer();
        }
        
		/// <summary>
		/// Tops the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
        public static UIViewController TopViewController() 
		{
			return TopViewController(UIApplication.SharedApplication.KeyWindow.RootViewController);
        }
        
		/// <summary>
		/// Tops the view controller.
		/// </summary>
		/// <returns>The view controller.</returns>
		/// <param name="rootViewController">Root view controller.</param>
        public static UIViewController TopViewController(UIViewController rootViewController) 
		{
			if (rootViewController.PresentedViewController == null)
				return rootViewController;

			if (rootViewController.PresentedViewController is UINavigationController)
			{
				var navigationController = (UINavigationController)rootViewController.PresentedViewController;

				var lastViewController = navigationController.ViewControllers[navigationController.ViewControllers.Length-1];

				return TopViewController(lastViewController);

			}

			var presentedViewController = (UIViewController)rootViewController.PresentedViewController;

			return TopViewController(presentedViewController);
        }
        
		/// <summary>
		/// Takes the screen shot of view.
		/// </summary>
		/// <returns>The screen shot of view.</returns>
		/// <param name="view">View.</param>
		/// <param name="updated">If set to <c>true</c> updated.</param>
        public static UIImage TakeScreenShotOfView(UIView view, Boolean updated) 
		{
		   UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, false, UIScreen.MainScreen.Scale);
            
		   view.DrawViewHierarchy(view.Bounds,updated);
                 
           UIImage image = UIGraphics.GetImageFromCurrentImageContext();
                 
           UIGraphics.EndImageContext();
                 
           return image;
        }

        /// <summary>
        /// Changes the color of the image.
        /// </summary>
        /// <returns>The image color.</returns>
        /// <param name="img">Image.</param>
        /// <param name="color">Color.</param>
        public static UIImage ChangeImageColor(UIImage img, UIColor color) {
         
			if (color != null)
			{
				UIGraphics.BeginImageContextWithOptions(img.Size, false, (nfloat)img.CurrentScale);

				var context = UIGraphics.GetCurrentContext();

				context.TranslateCTM(0, img.Size.Height);
				context.ScaleCTM(1.0f, -1.0f);
				context.SetBlendMode(CGBlendMode.Normal);

				CGRect rect = new CGRect(0, 0, img.Size.Width, img.Size.Height);

				context.ClipToMask(rect,img.CGImage);


				color.SetFill();

				context.FillRect(rect);


				UIImage newImage = UIGraphics.GetImageFromCurrentImageContext();

				UIGraphics.EndImageContext();

				return newImage;
			}

             
            return img;
        }
       
		/// <summary>
		/// Images the with image.
		/// </summary>
		/// <returns>The with image.</returns>
		/// <param name="image">Image.</param>
		/// <param name="size">Size.</param>
        public static UIImage ImageWithImage(UIImage image, CGSize size) {
         
             UIGraphics.BeginImageContext(size);
         
			image.Draw(new CGRect(0, 0, size.Width, size.Height));

             var destImage = UIGraphics.GetImageFromCurrentImageContext();
             
             UIGraphics.EndImageContext();
             
             return destImage;
        }

		/// <summary>
		/// Applies the blurr with efftect style.
		/// </summary>
		/// <returns>The blurr with efftect style.</returns>
		/// <param name="style">Style.</param>
		/// <param name="frame">Frame.</param>
		public static UIVisualEffectView ApplyBlurrWithEfftectStyle(UIBlurEffectStyle style, CGRect frame) {
			 
			     //only apply the blur if the user hasn't disabled transparency effects
			if(!UIAccessibility.IsReduceTransparencyEnabled)
			{
				var blurEffect = UIBlurEffect.FromStyle(style); 

				var blurEffectView = new UIVisualEffectView(blurEffect);
				blurEffectView.Alpha = 0.6f;
				blurEffectView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
				blurEffectView.Frame = frame;

				return blurEffectView;
			}
		    else 
			{
		         // ios 7 implementation
			}
			     
			return null;
		}

    }
}
