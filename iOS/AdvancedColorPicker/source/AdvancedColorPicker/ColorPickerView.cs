using System;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGPoint = System.Drawing.PointF;
using CGSize = System.Drawing.SizeF;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace AdvancedColorPicker
{
	/// <summary>
	/// The view that represents a color picker.
	/// </summary>
	public class ColorPickerView : UIView
	{
		private CGSize satBrightIndicatorSize;
		private HuePickerView huePicker;
		private SaturationBrightnessPickerView satbrightPicker;
		private SelectedColorPreviewView preview;
		private HueIndicatorView hueIndicator;
		private SaturationBrightnessIndicatorView satBrightIndicator;

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerView"/> class.
		/// </summary>
		public ColorPickerView()
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerView"/> class 
		/// with the specified frame.
		/// </summary>
		/// <param name="frame">The frame used by the view, expressed in iOS points.</param>
		public ColorPickerView(CGRect frame)
			: base(frame)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ColorPickerView"/> class 
		/// with the specified initial selected color.
		/// </summary>
		/// <param name="color">The initial selected color.</param>
		public ColorPickerView(UIColor color)
			: base()
		{
			Initialize();
			SelectedColor = color;
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		protected void Initialize()
		{
			satBrightIndicatorSize = new CGSize(28, 28);

			var selectedColorViewHeight = (nfloat)60;
			var viewSpace = (nfloat)1;

			preview = new SelectedColorPreviewView();
			preview.Frame = new CGRect(0, 0, Bounds.Width, selectedColorViewHeight);
			preview.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			preview.Layer.ShadowOpacity = 0.6f;
			preview.Layer.ShadowOffset = new CGSize(0, 7);
			preview.Layer.ShadowColor = UIColor.Black.CGColor;

			satbrightPicker = new SaturationBrightnessPickerView();
			satbrightPicker.Frame = new CGRect(0, selectedColorViewHeight + viewSpace, Bounds.Width, Bounds.Height - selectedColorViewHeight - selectedColorViewHeight - viewSpace - viewSpace);
			satbrightPicker.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			satbrightPicker.ColorPicked += HandleColorPicked;
			satbrightPicker.AutosizesSubviews = true;

			huePicker = new HuePickerView();
			huePicker.Frame = new CGRect(0, Bounds.Bottom - selectedColorViewHeight, Bounds.Width, selectedColorViewHeight);
			huePicker.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleTopMargin;
			huePicker.HueChanged += HandleHueChanged;

			var pos = huePicker.Frame.Width * huePicker.Hue;
			hueIndicator = new HueIndicatorView();
			hueIndicator.Frame = new CGRect(pos - 10, huePicker.Bounds.Y - 2, 20, huePicker.Bounds.Height + 2);
			hueIndicator.UserInteractionEnabled = false;
			hueIndicator.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin;
			huePicker.AddSubview(hueIndicator);

			var pos2 = new CGPoint(satbrightPicker.Saturation * satbrightPicker.Frame.Size.Width,
								   satbrightPicker.Frame.Size.Height - (satbrightPicker.Brightness * satbrightPicker.Frame.Size.Height));
			satBrightIndicator = new SaturationBrightnessIndicatorView();
			satBrightIndicator.Frame = new CGRect(pos2.X - satBrightIndicatorSize.Width / 2, pos2.Y - satBrightIndicatorSize.Height / 2, satBrightIndicatorSize.Width, satBrightIndicatorSize.Height);
			satBrightIndicator.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;
			satBrightIndicator.UserInteractionEnabled = false;
			satbrightPicker.AddSubview(satBrightIndicator);

			AddSubviews(satbrightPicker, huePicker, preview);
		}

		/// <summary>
		/// Gets or sets the selected color.
		/// </summary>
		/// <value>The selected color.</value>
		public UIColor SelectedColor
		{
			get
			{
				return UIColor.FromHSB(satbrightPicker.Hue, satbrightPicker.Saturation, satbrightPicker.Brightness);
			}
			set
			{
				nfloat hue = 0, brightness = 0, saturation = 0, alpha = 0;
				value?.GetHSBA(out hue, out saturation, out brightness, out alpha);
				huePicker.Hue = hue;
				satbrightPicker.Hue = hue;
				satbrightPicker.Brightness = brightness;
				satbrightPicker.Saturation = saturation;
				preview.BackgroundColor = value;

				PositionIndicators();

				satbrightPicker.SetNeedsDisplay();
				huePicker.SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Occurs when the a color is selected.
		/// </summary>
		public event EventHandler<ColorPickedEventArgs> ColorPicked;

		/// <summary>
		/// Lays out subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			PositionIndicators();
		}

		private void PositionIndicators()
		{
			PositionHueIndicatorView();
			PositionSatBrightIndicatorView();
		}

		private void PositionSatBrightIndicatorView()
		{
			Animate(0.3f, 0f, UIViewAnimationOptions.AllowUserInteraction, () =>
			{
				var x = satbrightPicker.Saturation * satbrightPicker.Frame.Size.Width;
				var y = satbrightPicker.Frame.Size.Height - (satbrightPicker.Brightness * satbrightPicker.Frame.Size.Height);
				var pos = new CGPoint(x, y);
				var rect = new CGRect(
					pos.X - satBrightIndicatorSize.Width / 2,
					pos.Y - satBrightIndicatorSize.Height / 2,
					satBrightIndicatorSize.Width,
					satBrightIndicatorSize.Height);
				satBrightIndicator.Frame = rect;
			}, null);
		}

		private void PositionHueIndicatorView()
		{
			Animate(0.3f, 0f, UIViewAnimationOptions.AllowUserInteraction, () =>
			{
				var pos = huePicker.Frame.Width * huePicker.Hue;
				var rect = new CGRect(
					pos - 10,
					huePicker.Bounds.Y - 2,
					20,
					huePicker.Bounds.Height + 2);
				hueIndicator.Frame = rect;
			}, () =>
			{
				hueIndicator.Hidden = false;
			});
		}

		private void HandleColorPicked(object sender, EventArgs e)
		{
			PositionSatBrightIndicatorView();
			preview.BackgroundColor = UIColor.FromHSB(satbrightPicker.Hue, satbrightPicker.Saturation, satbrightPicker.Brightness);

			OnColorPicked(new ColorPickedEventArgs(SelectedColor));
		}

		private void HandleHueChanged(object sender, EventArgs e)
		{
			PositionHueIndicatorView();
			satbrightPicker.Hue = huePicker.Hue;
			satbrightPicker.SetNeedsDisplay();

			HandleColorPicked(sender, e);
		}

		/// <summary>
		/// Handles the <see cref="E:ColorPicked" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ColorPickedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnColorPicked(ColorPickedEventArgs e)
		{
			var handler = ColorPicked;
			if (handler != null)
			{
				handler(this, e);
			}
		}
	}
}
