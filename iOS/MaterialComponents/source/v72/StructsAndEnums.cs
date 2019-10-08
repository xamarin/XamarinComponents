using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using ObjCRuntime;

[Native]
public enum MDCActivityIndicatorMode : long
{
	Indeterminate,
	Determinate
}

[Native]
public enum MDCColorSchemeDefaults : long
{
	MDCColorSchemeDefaultsMaterial201804
}

[Native]
public enum MDCAnimationTimingFunction : ulong
{
	Standard,
	Deceleration,
	Acceleration,
	Sharp,
	EaseInOut = Standard,
	EaseOut = Deceleration,
	EaseIn = Acceleration,
	Translate = Standard,
	TranslateOnScreen = Deceleration,
	TranslateOffScreen = Acceleration,
	FadeIn = Deceleration,
	FadeOut = Acceleration
}

[Native]
public enum MDCFlexibleHeaderScrollPhase : long
{
	Shifting,
	Collapsing,
	OverExtending
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
public enum MDCNavigationBarTitleAlignment : long
{
	Center,
	Leading
}

[Native]
public enum MDCNavigationBarTitleViewLayoutBehavior : long
{
	Fill,
	Center
}

[Native]
public enum MDCTypographySchemeDefaults : long
{
	MDCTypographySchemeDefaultsMaterial201804
}

[Native]
public enum MDCInkStyle : long
{
	Bounded,
	Unbounded
}

[Native]
public enum MDCCornerTreatmentValueType : long
{
	Absolute,
	Percentage
}

[Native]
public enum MDCFloatingButtonShape : long
{
	Default = 0,
	Mini = 1
}

[Native]
public enum MDCFloatingButtonMode : long
{
	Normal = 0,
	Expanded = 1
}

[Native]
public enum MDCFloatingButtonImageLocation : long
{
	Leading = 0,
	Trailing = 1
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
public enum MDCSheetState : ulong
{
	Closed,
	Preferred,
	Extended
}

[Native]
public enum MDCTriangleEdgeStyle : ulong
{
	Handle,
	Cut
}

[Native]
public enum MDCShapeCornerFamily : long
{
	Rounded,
	Cut
}

[Native]
public enum MDCShapeSchemeDefaults : long
{
	MDCShapeSchemeDefaultsMaterial201809
}

[Flags]
[Native]
public enum MDCButtonBarLayoutPosition : ulong
{
	None = 0x0,
	Leading = 1uL << 0,
	Left = Leading,
	Trailing = 1uL << 1,
	Right = Trailing
}

[Flags]
[Native]
public enum MDCBarButtonItemLayoutHints : ulong
{
	None = 0x0,
	IsFirstButton = 1uL << 0,
	IsLastButton = 1uL << 1
}

[Native]
public enum MDCCardCellState : long
{
	Normal = 0,
	Highlighted,
	Selected
}

[Native]
public enum MDCCardCellHorizontalImageAlignment : long
{
	Right = 0,
	Center,
	Left
}

[Native]
public enum MDCCardCellVerticalImageAlignment : long
{
	Top = 0,
	Center,
	Bottom
}

[Native]
public enum MDCTextInputTextInsetsMode : ulong
{
	Never = 0,
	IfContent,
	Always
}

[Flags]
[Native]
public enum MDCChipFieldDelimiter : ulong
{
	None = 0x0,
	Return = 1uL << 0,
	Space = 1uL << 1,
	DidEndEditing = 1uL << 2,
	Default = (Return | DidEndEditing),
	All = 0xffffffffL
}

[Native]
public enum MDCCollectionViewCellAccessoryType : ulong
{
	None,
	DisclosureIndicator,
	Checkmark,
	DetailButton
}

[Flags]
[Native]
public enum MDCCollectionViewOrdinalPosition : ulong
{
	VerticalTop = 1uL << 0,
	VerticalCenter = 1uL << 1,
	VerticalBottom = 1uL << 2,
	VerticalTopBottom = (VerticalTop | VerticalBottom),
	HorizontalLeft = 1uL << 10,
	HorizontalCenter = 1uL << 11,
	HorizontalRight = 1uL << 12
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
public enum MDCActionEmphasis : long
{
	Low = 0,
	Medium = 1,
	High = 2
}

public enum MaterialFeatureHighlightStringId : uint
{
	Str_MaterialFeatureHighlightDismissAccessibilityHint = 0
}

[Flags]
[Native]
public enum MDFTextAccessibilityOptions : ulong
{
	None = 0x0,
	LargeFont = 1uL << 0,
	PreserveAlpha = 1uL << 1,
	PreferDarker = 1uL << 2,
	PreferLighter = 1uL << 3,
	EnhancedContrast = 1uL << 4
}

[Native]
public enum MDCBottomDrawerState : ulong
{
	Collapsed = 0,
	Expanded = 1,
	FullScreen = 2
}

[Native]
public enum MDCProgressViewBackwardAnimationMode : long
{
	Reset,
	Animate
}

[Native]
public enum MDCSnackbarAlignment : long
{
	Center = 0,
	Leading = 1
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
public enum MDCTabBarTextTransform : long
{
	Automatic = 0,
	None = 1,
	Uppercase = 2
}

[Native]
public enum MDCTabBarItemState : long
{
	Normal,
	Selected
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

static class CFunctions
{
	// CGFloat MDCSin (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCSin (nfloat value);

	// CGFloat MDCCos (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCCos (nfloat value);

	// CGFloat MDCAtan2 (CGFloat y, CGFloat x);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCAtan2 (nfloat y, nfloat x);

	// CGFloat MDCCeil (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCCeil (nfloat value);

	// CGFloat MDCFabs (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCFabs (nfloat value);

	// CGFloat MDCDegreesToRadians (CGFloat degrees);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCDegreesToRadians (nfloat degrees);

	// BOOL MDCCGFloatEqual (CGFloat a, CGFloat b);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern bool MDCCGFloatEqual (nfloat a, nfloat b);

	// CGFloat MDCFloor (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCFloor (nfloat value);

	// CGFloat MDCHypot (CGFloat x, CGFloat y);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCHypot (nfloat x, nfloat y);

	// BOOL MDCCGFloatIsExactlyZero (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern bool MDCCGFloatIsExactlyZero (nfloat value);

	// CGFloat MDCPow (CGFloat value, CGFloat power);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCPow (nfloat value, nfloat power);

	// CGFloat MDCRint (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCRint (nfloat value);

	// CGFloat MDCRound (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCRound (nfloat value);

	// CGFloat MDCSqrt (CGFloat value);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCSqrt (nfloat value);

	// CGFloat MDCCeilScaled (CGFloat value, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCCeilScaled (nfloat value, nfloat scale);

	// CGFloat MDCFloorScaled (CGFloat value, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCFloorScaled (nfloat value, nfloat scale);

	// CGRect MDCRectAlignToScale (CGRect rect, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern CGRect MDCRectAlignToScale (CGRect rect, nfloat scale);

	// CGPoint MDCPointRoundWithScale (CGPoint point, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern CGPoint MDCPointRoundWithScale (CGPoint point, nfloat scale);

	// CGSize MDCSizeCeilWithScale (CGSize size, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern CGSize MDCSizeCeilWithScale (CGSize size, nfloat scale);

	// CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, CGFloat scale);
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, nfloat scale);

	// extern CGFloat MDCDeviceTopSafeAreaInset ();
	[DllImport ("__Internal")]
	[Verify (PlatformInvoke)]
	static extern nfloat MDCDeviceTopSafeAreaInset ();
}
