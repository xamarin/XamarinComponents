using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using ObjCRuntime;

namespace MaterialComponents
{
	[Native]
	public enum MDCAnimationTimingFunction : ulong
	{
		EaseInOut,
		EaseOut,
		EaseIn,
		Sharp,
		Translate = EaseInOut,
		TranslateOnScreen = EaseOut,
		TranslateOffScreen = EaseIn,
		FadeIn = EaseOut,
		FadeOut = EaseIn
	}

	[Native]
	public enum MDCActivityIndicatorMode : long
	{
		Indeterminate,
		Determinate
	}

	[Native]
	public enum MDCFlexibleHeaderShiftBehavior : long
	{
		Disabled,
		Enabled,
		EnabledWithStatusBar
	}

	[Native]
	public enum MDCFlexibleHeaderContentImportance : long
	{
		Default,
		High
	}

	[Native]
	public enum MDCFlexibleHeaderScrollPhase : long
	{
		Shifting,
		Collapsing,
		OverExtending
	}

	[Native]
	public enum MDCNavigationBarTitleAlignment : long
	{
		Center,
		Leading
	}

	[Native]
	public enum MDCInkStyle : long
	{
		Bounded,
		Unbounded
	}

	[Native]
	public enum MDCFloatingButtonShape : long
	{
		Default = 0,
		Mini = 1,
		LargeIcon = 2
	}

	[Native]
	public enum MDCBottomAppBarFloatingButtonElevation : long
	{
		Primary = 0,
		Secondary = 1
	}

	[Native]
	public enum MDCBottomAppBarFloatingButtonPosition : long
	{
		Center = 0,
		Leading = 1,
		Trailing = 2
	}

	[Native]
	public enum MDCBottomNavigationBarTitleVisibility : long
	{
		Selected = 0,
		Always = 1,
		Never = 2
	}

	[Native]
	public enum MDCBottomNavigationBarAlignment : long
	{
		Justified = 0,
		JustifiedAdjacentTitles = 1,
		Centered = 2
	}

	[Native]
	public enum MDCButtonBarLayoutPosition : ulong
	{
		None = 0,
		Leading = 1 << 0,
		Left = Leading,
		Trailing = 1 << 1,
		Right = Trailing
	}

	[Native]
	public enum MDCBarButtonItemLayoutHints : ulong
	{
		None = 0,
		IsFirstButton = 1 << 0,
		IsLastButton = 1 << 1
	}

	[Native]
	public enum MDCCollectionViewCellAccessoryType : ulong
	{
		None,
		DisclosureIndicator,
		Checkmark,
		DetailButton
	}

	[Native]
	public enum MDCCollectionViewCellStyle : ulong
	{
		Default,
		Grouped,
		Card
	}

	[Native]
	public enum MDCCollectionViewCellLayoutType : ulong
	{
		List,
		Grid,
		Custom
	}

	[Native]
	public enum MDCCollectionViewOrdinalPosition : ulong
	{
		VerticalTop = 1 << 0,
		VerticalCenter = 1 << 1,
		VerticalBottom = 1 << 2,
		VerticalTopBottom = (VerticalTop | VerticalBottom),
		HorizontalLeft = 1 << 10,
		HorizontalCenter = 1 << 11,
		HorizontalRight = 1 << 12
	}

	[Native]
	public enum MDCFontTextStyle : long
	{
		Body1,
		Body2,
		Caption,
		Headline,
		Subheadline,
		Title,
		Display1,
		Display2,
		Display3,
		Display4,
		Button
	}

	//static class CFunctions
	//{
	//	// extern CGFloat MDCDeviceTopSafeAreaInset ();
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCDeviceTopSafeAreaInset ();

	//	// CGFloat MDCSin (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCSin (nfloat value);

	//	// CGFloat MDCCos (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCCos (nfloat value);

	//	// CGFloat MDCAtan2 (CGFloat y, CGFloat x);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCAtan2 (nfloat y, nfloat x);

	//	// CGFloat MDCCeil (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCCeil (nfloat value);

	//	// CGFloat MDCFabs (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCFabs (nfloat value);

	//	// CGFloat MDCDegreesToRadians (CGFloat degrees);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCDegreesToRadians (nfloat degrees);

	//	// _Bool MDCCGFloatEqual (CGFloat a, CGFloat b);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern bool MDCCGFloatEqual (nfloat a, nfloat b);

	//	// CGFloat MDCFloor (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCFloor (nfloat value);

	//	// CGFloat MDCHypot (CGFloat x, CGFloat y);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCHypot (nfloat x, nfloat y);

	//	// BOOL MDCCGFloatIsExactlyZero (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern bool MDCCGFloatIsExactlyZero (nfloat value);

	//	// CGFloat MDCPow (CGFloat value, CGFloat power);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCPow (nfloat value, nfloat power);

	//	// CGFloat MDCRint (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCRint (nfloat value);

	//	// CGFloat MDCRound (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCRound (nfloat value);

	//	// CGFloat MDCSqrt (CGFloat value);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCSqrt (nfloat value);

	//	// CGRect MDCRectAlignToScale (CGRect rect, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGRect MDCRectAlignToScale (CGRect rect, nfloat scale);

	//	// CGPoint MDCPointRoundWithScale (CGPoint point, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGPoint MDCPointRoundWithScale (CGPoint point, nfloat scale);

	//	// CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, nfloat scale);
	//}

	[Native]
	public enum MDCTextInputTextInsetsMode : ulong
	{
		Never = 0,
		IfContent,
		Always
	}

	[Native]
	public enum MDCProgressViewBackwardAnimationMode : long
	{
		Reset,
		Animate
	}

	[Native]
	public enum MDCTabBarAlignment : long
	{
		Leading,
		Justified,
		Center,
		CenterSelected
	}

	[Native]
	public enum MDCTabBarItemAppearance : long
	{
		Titles,
		Images,
		TitledImages
	}

	[Native]
	public enum MDCTriangleEdgeStyle : ulong
	{
		Handle,
		Cut
	}

}
