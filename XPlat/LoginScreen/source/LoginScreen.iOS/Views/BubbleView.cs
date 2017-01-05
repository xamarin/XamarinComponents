using System.Drawing;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
#else
using MonoTouch.UIKit;
using CGRect = global::System.Drawing.RectangleF;
#endif

using LoginScreen.Utils;

namespace LoginScreen.Views
{
	class BubbleView : UIView
	{
		UIImageView backgroundView;
		UILabel label;

		public BubbleView ()
		{
			UserInteractionEnabled = false;

			backgroundView = new UIImageView (UIImage.FromFile ("Images/alert.png").StretchableImage (28, 0));

			label = new UILabel
			{
				BackgroundColor = UIColor.Clear,
				Font = Fonts.HelveticaNeueMedium (13),
				TextColor = UIColor.White,
				ShadowColor = UIColor.Black.ColorWithAlpha (0.4f),
				ShadowOffset = new SizeF (0f, 1f),
				Frame = new CGRect (backgroundView.Frame.X + 10f, backgroundView.Frame.Y + 2f, backgroundView.Frame.Width, backgroundView.Frame.Height)
			};

			AddSubviews (backgroundView, label);
		}

		public string Text {
			get { return label.Text; }
			set { 
				label.Text = value;
				label.SizeToFit ();
				SetNeedsLayout ();
			}
		}

		public override void LayoutSubviews ()
		{
			backgroundView.Frame = new CGRect (0f, 0f, label.Frame.Width + 20f, Bounds.Height);
			label.Frame = new CGRect (10f, 2f, Bounds.Width, Bounds.Height);
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (disposing) {
				label.Dispose ();
				backgroundView.Image.Dispose ();
				backgroundView.Dispose ();
			}
		}
	}
}