using System;

#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;
#else
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif

namespace RadialProgress {
	/// <summary>
	/// Radial progress view.
	/// </summary>
	public class RadialProgressView: UIView {
		const float DefaultMinValue = 0f;
		const float DefaultMaxValue = 1f;

		CGPoint CenterPoint;
		nfloat currentValue = 0f;
		UILabel percentageLabel;
		UIImageView backgroundImageView;
		RadialProgressLayer radialProgressLayer;
		RadialProgressViewStyle progressType;
		UIColor progressColor;

		bool labelHidden;

		/// <summary>
		/// Gets or sets the minimum value
		/// </summary>
		/// <value>
		/// The minimum value.
		/// </value>
		public nfloat MinValue { get; set; }

		/// <summary>
		/// Gets or sets the max value.
		/// </summary>
		/// <value>
		/// The max value.
		/// </value>
		public nfloat MaxValue { get; set; }

		/// <summary>
		/// Gets or sets the color of the progress indicator
		/// </summary>
		/// <value>
		/// The color of the progress indicator.
		/// </value>
		public UIColor ProgressColor {
			get {
				return progressColor;
			}
			set {
				progressColor = value;
				radialProgressLayer.Color = ProgressColor;
			}
		}

		/// <summary>
		/// Gets or sets the label text delegate. If null, default function will be used (in %%)
		/// </summary>
		/// <value>The label text delegate.</value>
		public Func<nfloat, string> LabelTextDelegate { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether label is hidden.
		/// </summary>
		/// <value>
		/// <c>true</c> if label hidden; otherwise, <c>false</c>.
		/// </value>
		public bool LabelHidden { 
			get {
				return progressType != RadialProgressViewStyle.Tiny ? labelHidden : true;
			}
			set {
				if (labelHidden != value) {
					labelHidden = value;
					
					if (!labelHidden && percentageLabel == null) {
						InitPercentageLabel ();
					}
					if (percentageLabel != null) {
						percentageLabel.Hidden = value;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets progress value.
		/// </summary>
		/// <value>
		/// Progress value.
		/// </value>
		public nfloat Value {
			get {
				return currentValue;
			}
			set {
				if (value > MaxValue) {
					value = MaxValue;
				}
				
				if (value < MinValue) {
					value = MinValue;
				}
				
				if (currentValue != value) {
					currentValue = value;
					
					InvokeOnMainThread (OnCurrentValueChanged);
				}
			}
		}


		public bool IsDone {
			get { return Value == MaxValue; }
		}

		/// <summary>
		/// </summary>
		/// <param name='progressType'>
		/// Progress type.
		/// </param>
		public RadialProgressView (Func<nfloat, string> labelText = null, RadialProgressViewStyle progressType = RadialProgressViewStyle.Big)
		{
			this.progressType = progressType;
			LabelTextDelegate = labelText;
			radialProgressLayer = GetRadialLayerByType (progressType);
			Bounds = radialProgressLayer.BackBounds;
			
			CenterPoint = new CGPoint (Bounds.GetMidX (), Bounds.GetMidY ());
			BackgroundColor = UIColor.Clear;
			UserInteractionEnabled = false;
			
			MinValue = DefaultMinValue;
			MaxValue = DefaultMaxValue;
			
			InitSubviews ();
		}
		
		public void Reset ()
		{
			currentValue = MinValue;
			OnCurrentValueChanged ();
		}

		UIFont FontForProgressType (RadialProgressViewStyle progressType)
		{
			switch (progressType) {
			case RadialProgressViewStyle.Big:
				return UIFont.FromName ("Helvetica-Bold", 76f);
			case RadialProgressViewStyle.Small:
				return UIFont.FromName ("Helvetica-Bold", 12f);
			default:
				return UIFont.FromName ("Helvetica-Bold", 76f);
			}
		}

		void InitSubviews ()
		{

			backgroundImageView = new UIImageView (radialProgressLayer.GenerateBackgroundImage ());
			
			AddSubview (backgroundImageView);
			
			if (!LabelHidden) {
				InitPercentageLabel ();
			}			

			Layer.InsertSublayerAbove (radialProgressLayer, backgroundImageView.Layer);
		}

		RadialProgressLayer GetRadialLayerByType (RadialProgressViewStyle progressType)
		{
			switch (progressType) {
			case RadialProgressViewStyle.Big:
				return new BigRadialProgressLayer();
			case RadialProgressViewStyle.Small:
				return new SmallRadialProgressLayer();
			case RadialProgressViewStyle.Tiny:
				return new TinyRadialProgressLayer();
			default:
				return new BigRadialProgressLayer();
			}
		}
		
		void InitPercentageLabel ()
		{
			if (percentageLabel != null)
				return;
			
			percentageLabel = new UILabel ();
			
			percentageLabel.BackgroundColor = UIColor.Clear;
			percentageLabel.TextColor = UIColor.White;
			percentageLabel.ShadowColor = UIColor.Black.ColorWithAlpha (0.71f);
			percentageLabel.ShadowOffset = new CGSize (1f, 1f);
			percentageLabel.Font = FontForProgressType (progressType);
			percentageLabel.TextAlignment = UITextAlignment.Center;
			
			percentageLabel.Text = "100";
			percentageLabel.SizeToFit ();
			percentageLabel.Text = String.Empty;
			
			AddSubview (percentageLabel);
		}
		
		void OnCurrentValueChanged ()
		{
			var percentage = CalculatePercentage (currentValue);
			
			radialProgressLayer.Color = ProgressColor;
			radialProgressLayer.Percentage = percentage;
			radialProgressLayer.SetNeedsDisplay ();
			
			if (!LabelHidden) {
				percentageLabel.Text = (LabelTextDelegate == null) ? Math.Floor (percentage).ToString ().PadLeft (2, '0') : LabelTextDelegate(currentValue);
			}
		}
		
		public override void LayoutSubviews ()
		{
			if (progressType != RadialProgressViewStyle.Tiny) {
				percentageLabel.Center = CenterPoint;
			}
			backgroundImageView.Center = CenterPoint;
			radialProgressLayer.Position = CenterPoint;
		}
		
		nfloat CalculatePercentage (nfloat currentValue)
		{
			var fullDistance = MaxValue - MinValue;
			var currentDistance = MaxValue - currentValue;

			if (fullDistance == 0f) return 0f;

			return (1 - currentDistance / fullDistance) * 100f;
		}
	}
}