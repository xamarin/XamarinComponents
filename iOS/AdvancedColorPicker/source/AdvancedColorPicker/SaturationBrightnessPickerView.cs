using System;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
	internal class SaturationBrightnessPickerView : UIView
	{
		public SaturationBrightnessPickerView()
		{
		}

		public nfloat Hue { get; set; }

		public nfloat Saturation { get; set; }

		public nfloat Brightness { get; set; }

		public event EventHandler ColorPicked;

		public override void Draw(CGRect rect)
		{
			using (var colorSpace = CGColorSpace.CreateDeviceRGB())
			{
				var context = UIGraphics.GetCurrentContext();
				var gradLocations = new nfloat[] { 0.0f, 1.0f };

				var gradColors = new[] { UIColor.FromHSBA(Hue, 1, 1, 1).CGColor, new CGColor(1, 1, 1, 1) };
				using (var gradient = new CGGradient(colorSpace, gradColors, gradLocations))
				{
					context.DrawLinearGradient(gradient, new CGPoint(rect.Size.Width, 0), new CGPoint(0, 0), CGGradientDrawingOptions.DrawsBeforeStartLocation);
				}

				gradColors = new[] { new CGColor(0, 0, 0, 0), new CGColor(0, 0, 0, 1) };
				using (var gradient = new CGGradient(colorSpace, gradColors, gradLocations))
				{
					context.DrawLinearGradient(gradient, new CGPoint(0, 0), new CGPoint(0, rect.Size.Height), CGGradientDrawingOptions.DrawsBeforeStartLocation);
				}
			}
		}

		public override void TouchesBegan(NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			HandleTouches(touches, evt);
		}

		public override void TouchesMoved(NSSet touches, UIEvent evt)
		{
			base.TouchesMoved(touches, evt);
			HandleTouches(touches, evt);
		}

		private void HandleTouches(NSSet touches, UIEvent evt)
		{
			var touch = (UITouch)evt.TouchesForView(this).AnyObject;
			var pos = touch.LocationInView(this);

			var w = Frame.Size.Width;
			var h = Frame.Size.Height;

			if (pos.X < 0)
				Saturation = 0;
			else if (pos.X > w)
				Saturation = 1;
			else
				Saturation = pos.X / w;

			if (pos.Y < 0)
				Brightness = 1;
			else if (pos.Y > h)
				Brightness = 0;
			else
				Brightness = 1 - (pos.Y / h);

			OnColorPicked();
		}

		protected virtual void OnColorPicked()
		{
			var handler = ColorPicked;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
