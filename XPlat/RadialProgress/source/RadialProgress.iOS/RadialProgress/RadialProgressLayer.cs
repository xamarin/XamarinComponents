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
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif

namespace RadialProgress {
	internal abstract class RadialProgressLayer: CALayer {
		protected const float FullCircleAngle = (float)Math.PI * 2;

		protected CGPoint CenterPoint;
		UIImage fullProgressImage;

		protected CGSize BoundsSize { get; set; }
		public CGRect BackBounds { get; set; }
		public nfloat Percentage { get; set; }

		protected nfloat endRadius;
		protected nfloat startRadius;
		protected nfloat backgroundWidth;
		protected nfloat progressLayerWidth;

		static readonly UIColor DefaultFillColor = UIColor.FromRGB (114, 184, 255);

		protected static readonly CGColor BackBorderColor = UIColor.Black.ColorWithAlpha (0.41f).CGColor;
		protected static readonly CGColor BackInnerBorderColor = UIColor.White.ColorWithAlpha (0.2f).CGColor;
		protected static readonly CGColor BackCircleBackgroundColor = UIColor.Black.ColorWithAlpha (0.5f).CGColor;

		UIColor color;
		
		public UIColor Color { 
			get { return color ?? DefaultFillColor;	}
			set
			{
				if (color != value) {
					color = value; 
					fullProgressImage = GenerateFullProgressImage ();
				}
			}
		}

		public RadialProgressLayer (nfloat startRadius, nfloat endRadius, nfloat backgroundWidth, nfloat progressLayerWidth)
			: this ()
		{
			this.startRadius = startRadius;
			this.endRadius = endRadius;
			this.backgroundWidth = backgroundWidth;
			this.progressLayerWidth = progressLayerWidth;

			Bounds = new CGRect (CGPoint.Empty, new CGSize (progressLayerWidth, progressLayerWidth));
			BackBounds = new CGRect (CGPoint.Empty, new CGSize (backgroundWidth, backgroundWidth));
			
			CenterPoint = new CGPoint (Bounds.GetMidX (), Bounds.GetMidY ());
			fullProgressImage = GenerateFullProgressImage ();
		}

		public RadialProgressLayer ()
		{
			BackgroundColor = UIColor.Clear.CGColor;
			ContentsScale = UIScreen.MainScreen.Scale;
		}


		public override void DrawInContext (CGContext context)
		{
			var progressAngle = CalculateProgressAngle (Percentage);
			
			using (var path = BezierPathGenerator.Bagel (CenterPoint, startRadius, endRadius, 0f, progressAngle)) {
				context.AddPath (path.CGPath);
				context.Clip ();
				context.DrawImage (Bounds, fullProgressImage.CGImage);
			}
		}

		protected virtual UIImage GenerateFullProgressImage () {
			UIImage resultImage;
			
			UIGraphics.BeginImageContextWithOptions (Bounds.Size, false, UIScreen.MainScreen.Scale);
			
			using (var context = UIGraphics.GetCurrentContext())			
			using (var path = BezierPathGenerator.Bagel (CenterPoint, startRadius, endRadius, 0f, FullCircleAngle)) {
				context.SaveState ();
				
				context.SetFillColor (Color.CGColor);
				context.AddPath (path.CGPath);
				context.FillPath ();
				
				context.RestoreState ();
								
				resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			}
			
			return resultImage; 
		}
		
		
		nfloat CalculateProgressAngle (nfloat percentage)
		{
			return (nfloat)Math.PI / 50f * percentage;
		}

		public abstract UIImage GenerateBackgroundImage ();
	}
}