using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace Xamarin.Controls.RadialProgress {
	internal class Painter {
		const float HalfMathPi = (float)(Math.PI / 2.0);
		
		public static UIBezierPath CreateBagelPath (PointF center, float startRadius, float endRadius, float startAngle, float endAngle)
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

		static PointF RotatePoint (PointF center, float radius, double phi)
		{
			var sinPhi = Math.Sin (phi);
			var cosPhi = Math.Cos (phi);
			
			var x = center.X + radius * cosPhi;
			var y = center.Y + radius * sinPhi;
			
			return new PointF ((float)x, (float)y);
		}

	}
}