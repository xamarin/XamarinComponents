using System.Drawing;


#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = global::System.Drawing.RectangleF;
#endif

using LoginScreen.Utils;

namespace LoginScreen.Views
{
	class CustomTextField : UITextField
	{
		UIEdgeInsets textInset = UIEdgeInsets.Zero;
		UIFont placeholderFont = UIFont.SystemFontOfSize (UIFont.SystemFontSize);
		UIColor placeholderColor = UIColor.Black.ColorWithAlpha (0.3f);

		public UIEdgeInsets TextInset {
			get { return textInset; }
			set {
				if (!value.Equals (textInset)) {
					textInset = value;
					SetNeedsDisplay ();
				}
			}
		}

		public UIFont PlaceholderFont {
			get { return placeholderFont; }
			set {
				if (value != placeholderFont) {
					placeholderFont = value;
					SetNeedsDisplay ();
				}
			}
		}

		public UIColor PlaceholderColor {
			get { return placeholderColor; }
			set {
				if (value != placeholderColor) {
					placeholderColor = value;
					SetNeedsDisplay ();
				}
			}
		}

		public override CGRect TextRect (CGRect forBounds)
		{
			return base.TextRect (forBounds).ApplyInsets (TextInset);
		}

		public override CGRect EditingRect (CGRect forBounds)
		{
			return base.EditingRect (forBounds).ApplyInsets (TextInset);
		}

		public override void DrawPlaceholder (CGRect rect)
		{
			if (PlaceholderColor != null &&
			    PlaceholderFont != null &&
			    Placeholder != null) {
				PlaceholderColor.SetFill ();
				new NSString (Placeholder).DrawString (rect, PlaceholderFont, UILineBreakMode.TailTruncation);
			}
		}
	}
}

