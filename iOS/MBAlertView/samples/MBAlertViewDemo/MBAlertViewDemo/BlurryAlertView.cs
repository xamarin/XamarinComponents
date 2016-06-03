using System;
using System.Drawing;
using System.Linq;

#if __UNIFIED__
using CoreGraphics;
using CoreImage;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.CoreImage;
using MonoTouch.UIKit;

using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
#endif

using AlertView;

namespace MBAlertViewDemo
{
	public class BlurryAlertView : MBAlertView
	{
		UIImage background;
		UIImageView imageView;

		public override void AddToWindow ()
		{
			background = CreateBlurImage (UIImage.FromBundle ("background.png"));

			imageView = new UIImageView (background);
			imageView.TranslatesAutoresizingMaskIntoConstraints = false;

			base.AddToWindow ();

			UIWindow window = UIApplication.SharedApplication.Windows[0];
			window.InsertSubview (imageView, 0);
			window.AddConstraints (new [] {
				NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, window, NSLayoutAttribute.Left, 1.0f, 0),
				NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, window, NSLayoutAttribute.Top, 1.0f, 0),
				NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, window, NSLayoutAttribute.Right, 1.0f, 0),
				NSLayoutConstraint.Create (imageView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, window, NSLayoutAttribute.Bottom, 1.0f, 0)
			});
		}

		public override void Dismiss ()
		{
			base.Dismiss ();

			imageView.RemoveFromSuperview ();
			imageView.Dispose ();
			background.Dispose ();
		}

		public static BlurryAlertView AlertWithBody (string body, string cancelTitle, MBAlertViewButtonHandler cancelBlock)
		{
			BlurryAlertView alert = new BlurryAlertView ();
			alert.BodyText = body;
			if (!string.IsNullOrEmpty (cancelTitle)) {
				alert.AddButtonWithText (cancelTitle, MBAlertViewItemType.Default, cancelBlock);
			}
			return alert;
		}

		private static UIImage CreateBlurImage (UIImage image)
		{
			using (CIImage inputImage = new CIImage (image))
			using (CIGaussianBlur blur = new CIGaussianBlur ())
			using (CIContext context = CIContext.FromOptions (new CIContextOptions { UseSoftwareRenderer = false })) {
				blur.Image = inputImage;
				blur.Radius = 3;
				using (CIImage outputImage = blur.OutputImage)
				using (CIImage cgImage = context.CreateCGImage (outputImage, new CGRect (new CGPoint (0, 0), image.Size))) {
					return UIImage.FromImage (cgImage);
				}
			}
		}

		private static UIImage CreateScreenshot (UIView view)
		{
			UIGraphics.BeginImageContext(view.Frame.Size);
			view.DrawViewHierarchy(view.Frame, true);
			UIImage image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return image;
		}
	}
}
