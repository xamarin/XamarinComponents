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
	internal class TinyRadialProgressLayer: RadialProgressLayer {
		const float borderPadding = 0.5f;

		public TinyRadialProgressLayer () 
			: base (startRadius: 7f,
			        endRadius: 9f,
			        backgroundWidth: 22f,
			        progressLayerWidth: 21f)
		{
		}

		public override UIImage GenerateBackgroundImage ()
		{
			UIImage resultImage;

			var center = new CGPoint (BackBounds.GetMidX (), BackBounds.GetMidY ());
			UIGraphics.BeginImageContextWithOptions (BackBounds.Size, false, UIScreen.MainScreen.Scale);
			
			using (var context = UIGraphics.GetCurrentContext ())
				
			using (var borderBagelPath = BezierPathGenerator.Bagel (center, startRadius - borderPadding, endRadius + borderPadding, 0f, FullCircleAngle)) {
				context.SaveState ();
				context.SetFillColor (BackCircleBackgroundColor);
				borderBagelPath.Fill ();
				context.RestoreState ();
				
				resultImage = UIGraphics.GetImageFromCurrentImageContext ();
			}
			
			return resultImage; 
		}
	}
}