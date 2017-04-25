using System;

#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
#else
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
#endif

namespace RadialProgress {
	internal class BezierPathGenerator {
		const float HalfMathPi = (float)(Math.PI / 2.0);
		
		public static UIBezierPath Bagel (CGPoint center, nfloat startRadius, nfloat endRadius, nfloat startAngle, nfloat endAngle)
		{
			var bagelPath = new UIBezierPath ();
			
			var rotationShift = -HalfMathPi;

			var centerRadius = (startRadius + endRadius) / 2f;
			var roundingArcWidth = (endRadius - startRadius) / 2f;

			bagelPath.AddArc (RotatePoint (center, centerRadius, startAngle + rotationShift), 
			                  roundingArcWidth, -HalfMathPi, -3f * HalfMathPi, true);

			bagelPath.AddArc (center, startRadius, startAngle + rotationShift, endAngle + rotationShift, true);

			bagelPath.AddArc (RotatePoint (center, centerRadius, endAngle + rotationShift),
			                  roundingArcWidth, HalfMathPi + endAngle, 3f * HalfMathPi + endAngle, false);

			bagelPath.AddArc (center, endRadius, endAngle + rotationShift, startAngle + rotationShift, false);

			bagelPath.ClosePath ();
			
			return bagelPath;
		}

		static CGPoint RotatePoint (CGPoint center, nfloat radius, double phi)
		{
			var sinPhi = Math.Sin (phi);
			var cosPhi = Math.Cos (phi);
			
			var x = center.X + radius * cosPhi;
			var y = center.Y + radius * sinPhi;
			
			return new CGPoint ((nfloat)x, (nfloat)y);
		}

	}
}