using System;

#if __UNIFIED__
using Foundation;
using CoreGraphics;
#else
using MonoTouch.Foundation;
using nint = System.Int32;
using CGRect = System.Drawing.RectangleF;
#endif

namespace DACircularProgress
{
	partial class LabeledCircularProgressView
	{
		public LabeledCircularProgressView ()
			: this (new CGRect (0.0f, 0.0f, 40.0f, 40.0f))
		{
		}
	}
}
