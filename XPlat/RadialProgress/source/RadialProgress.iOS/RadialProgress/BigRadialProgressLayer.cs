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
	internal class BigRadialProgressLayer: RadialProgressLayer {
		const float GlowOffset = 1f;
		const float GlowRadius = 9f;
		const float BorderPadding = 5f;
		const float EndBorderRadius = 105f;
		const float StartBorderRadius = 70f;

		static readonly CGColor GlowColor = UIColor.White.ColorWithAlpha (0.5f).CGColor;
		static readonly CGColor GradientOverlayStartColor = UIColor.White.ColorWithAlpha (0.7f).CGColor;
		static readonly CGColor GradientOverlayEndColor = UIColor.Clear.CGColor;

		public BigRadialProgressLayer () 
			: base (startRadius: StartBorderRadius + BorderPadding,
			        endRadius: EndBorderRadius - BorderPadding,
			        backgroundWidth: 214f,
			        progressLayerWidth: 200f) {
		}

		protected override UIImage GenerateFullProgressImage ()
		{
			UIImage resultImage;
			
			UIGraphics.BeginImageContextWithOptions (Bounds.Size, false, UIScreen.MainScreen.Scale);

			using (var context = UIGraphics.GetCurrentContext ())			
			using (var path = BezierPathGenerator.Bagel (CenterPoint, startRadius, endRadius, 0f, FullCircleAngle)) {
				context.SaveState ();

				context.SetFillColor (Color.CGColor);
				context.AddPath (path.CGPath);
				context.FillPath ();
				
				context.RestoreState ();
				
				context.SaveState ();
				context.AddPath (path.CGPath);
				context.Clip ();

				DrawGradientOverlay (context);
				DrawInnerGlow (context, CenterPoint, startRadius, endRadius, GlowColor, GlowRadius);
				context.RestoreState ();

				resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			}
			
			return resultImage; 
		}

		public override UIImage GenerateBackgroundImage ()
		{
			UIImage resultImage;
			
			UIGraphics.BeginImageContextWithOptions (BackBounds.Size, false, UIScreen.MainScreen.Scale);
			var center = new CGPoint (BackBounds.GetMidX (), BackBounds.GetMidY ());

			using (var context = UIGraphics.GetCurrentContext())
			using (var circlePath = UIBezierPath.FromOval (new CGRect (CGPoint.Empty, BackBounds.Size)))
			using (var borderBagelPath = BezierPathGenerator.Bagel (center, StartBorderRadius, EndBorderRadius, 0f, FullCircleAngle))
			using (var innerBorderBagelPath = BezierPathGenerator.Bagel (center, StartBorderRadius + BorderPadding, EndBorderRadius - BorderPadding, 0f, FullCircleAngle)) {
				context.SaveState ();
				context.SetFillColor (BackCircleBackgroundColor);
				circlePath.Fill ();
				context.RestoreState ();
				
				context.SaveState ();
				context.SetFillColor (BackBorderColor);
				borderBagelPath.Fill ();
				context.RestoreState ();
				
				context.SaveState ();
				context.SetFillColor (BackInnerBorderColor);
				innerBorderBagelPath.Fill ();
				context.RestoreState ();
				
				resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			}
			
			return resultImage; 
		}

		void DrawGradientOverlay (CGContext context)
		{
			var colorSpace = CGColorSpace.CreateDeviceRGB ();
			var gradientColors = new CGColor [] {
				GradientOverlayStartColor,
				GradientOverlayEndColor
			};
			var gradientLocations = new nfloat [] { 0f, 1f };
			var gradient = new CGGradient (colorSpace, gradientColors, gradientLocations);
			
			context.SaveState ();
						
			context.SetBlendMode (CGBlendMode.Overlay);
			context.DrawLinearGradient (gradient, new CGPoint (Bounds.GetMinX (), Bounds.GetMaxY ()), new CGPoint (Bounds.GetMaxX (), Bounds.GetMinY ()), 0);
			
			context.RestoreState ();
		}
		
		void DrawInnerGlow (CGContext context, CGPoint center, nfloat startRadius, nfloat endRadius, CGColor glowColor, nfloat glowRadius)
		{
			using (var innerGlowPath = BezierPathGenerator.Bagel (center, startRadius - glowRadius - GlowOffset, startRadius - GlowOffset, 0f, FullCircleAngle))
			using (var outerGlowPath = BezierPathGenerator.Bagel (center, endRadius + GlowOffset, endRadius + glowRadius + GlowOffset, 0f, FullCircleAngle)) {				
				context.SaveState ();
				
#if __UNIFIED__
				context.SetShadow (CGSize.Empty, glowRadius, glowColor);
#else
				context.SetShadowWithColor (CGSize.Empty, glowRadius, glowColor);
#endif
				context.SetFillColor (Color.CGColor);
				context.AddPath (innerGlowPath.CGPath);
				context.FillPath ();
				context.RestoreState ();
				
				
				context.SaveState ();
#if __UNIFIED__
				context.SetShadow (CGSize.Empty, glowRadius, glowColor);
#else
				context.SetShadowWithColor (CGSize.Empty, glowRadius, glowColor);
#endif
				context.SetFillColor (Color.CGColor);
				
				context.SetFillColor (Color.CGColor);
				context.AddPath (outerGlowPath.CGPath);
				context.FillPath ();
				context.RestoreState ();
			}
		}
	}
}