using System;
using ObjCRuntime;

#if __IOS__ || __TVOS__
using UIKit;
#elif __MACOS__
using AppKit;
#endif

namespace Masonry
{
	[Native]
	[Flags]
	public enum Attribute : long
	{
		Left = 1 << (int)NSLayoutAttribute.Left,
		Right = 1 << (int)NSLayoutAttribute.Right,
		Top = 1 << (int)NSLayoutAttribute.Top,
		Bottom = 1 << (int)NSLayoutAttribute.Bottom,
		Leading = 1 << (int)NSLayoutAttribute.Leading,
		Trailing = 1 << (int)NSLayoutAttribute.Trailing,
		Width = 1 << (int)NSLayoutAttribute.Width,
		Height = 1 << (int)NSLayoutAttribute.Height,
		CenterX = 1 << (int)NSLayoutAttribute.CenterX,
		CenterY = 1 << (int)NSLayoutAttribute.CenterY,
		Baseline = 1 << (int)NSLayoutAttribute.Baseline,
		FirstBaseline = 1 << (int)NSLayoutAttribute.FirstBaseline,
		LastBaseline = 1 << (int)NSLayoutAttribute.LastBaseline,

#if __IOS__ || __TVOS__

		LeftMargin = 1 << (int)NSLayoutAttribute.LeftMargin,
		RightMargin = 1 << (int)NSLayoutAttribute.RightMargin,
		TopMargin = 1 << (int)NSLayoutAttribute.TopMargin,
		BottomMargin = 1 << (int)NSLayoutAttribute.BottomMargin,
		LeadingMargin = 1 << (int)NSLayoutAttribute.LeadingMargin,
		TrailingMargin = 1 << (int)NSLayoutAttribute.TrailingMargin,
		CenterXWithinMargins = 1 << (int)NSLayoutAttribute.CenterXWithinMargins,
		CenterYWithinMargins = 1 << (int)NSLayoutAttribute.CenterYWithinMargins

#endif
	}

	[Native]
	public enum AxisType : ulong
	{
		Horizontal,
		Vertical
	}
}
