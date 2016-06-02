using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace SCCatWaitingHUD
{
	partial class CatWaitingHUD
	{
		public CatWaitingHUD ()
			: this (CGRect.Empty)
		{
		}

		public void Start (string title)
		{
			Start (true, title);
		}

		public void Start (string title, nfloat duration)
		{
			Start (true, title, duration);
		}

		public void Start (nfloat duration)
		{
			Start (true, Title, duration);
		}

		public void Start (bool interactionEnabled, nfloat duration)
		{
			Start (interactionEnabled, Title, duration);
		}
	}
}
