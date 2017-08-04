using System;
using System.Drawing;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
#endif


namespace LoginScreen.Utils
{
	static class UIKitExtensions
	{
		public static CGRect ApplyInsets(this CGRect rectangle, UIEdgeInsets insets)
		{
			return CGRect.FromLTRB (
				rectangle.Left + insets.Left,
				rectangle.Top + insets.Top,
				rectangle.Right - insets.Right,
				rectangle.Bottom - insets.Bottom);
		}

		public static CGPoint GetContentCenter(this UIScrollView scrollView)
		{
			return new CGPoint
			{
				X = scrollView.ContentSize.Width > scrollView.Bounds.Width ? scrollView.ContentSize.Width / 2 : scrollView.Bounds.GetMidX (),
				Y = scrollView.ContentSize.Height > scrollView.Bounds.Height ? scrollView.ContentSize.Height / 2 : scrollView.Bounds.GetMidY ()
			};
		}

		public static CGRect GetAreaAboveKeyboard (this UIView view, CGRect keyboardFrame)
		{
			var intersection = CGRect.Intersect (view.Window.ConvertRectToView (keyboardFrame, view), view.Bounds);
			return new CGRect (view.Bounds.Location, view.Bounds.Size - new CGSize (0, intersection.Height));
		}

		public static UIViewAnimationOptions ToAnimationOptions (this UIViewAnimationCurve curve)
		{
		
			var val = (int)curve;

			val = val << 16;

			return (UIViewAnimationOptions)val;
		}
	}
}

