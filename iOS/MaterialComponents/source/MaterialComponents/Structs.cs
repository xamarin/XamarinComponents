using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using ObjCRuntime;

namespace MaterialComponents
{
	[Native]
	public enum AnimationTimingFunction : ulong
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

	public enum MaterialFeatureHighlightStringId : int
	{
		DismissAccessibilityHint = 0
	}

	[Native]
	public enum ActivityIndicatorMode : long
	{
		Indeterminate,
		Determinate
	}

	[Native]
	public enum BottomAppBarFloatingButtonElevation : long
	{
		Primary = 0,
		Secondary = 1
	}

	[Native]
	public enum BottomAppBarFloatingButtonPosition : long
	{
		Center = 0,
		Leading = 1,
		Trailing = 2
	}

	[Native]
	public enum BottomDrawerState : ulong
	{
		Collapsed = 0,
		Expanded = 1,
		FullScreen = 2
	}

	[Native]
	public enum BottomNavigationBarTitleVisibility : long
	{
		Selected = 0,
		Always = 1,
		Never = 2
	}

	[Native]
	public enum BottomNavigationBarAlignment : long
	{
		Justified = 0,
		JustifiedAdjacentTitles = 1,
		Centered = 2
	}

	[Native]
	public enum ButtonBarLayoutPosition : ulong
	{
		None = 0,
		Leading = 1 << 0,
		Left = Leading,
		Trailing = 1 << 1,
		Right = Trailing
	}

	[Native]
	public enum BarButtonItemLayoutHints : ulong
	{
		None = 0,
		IsFirstButton = 1 << 0,
		IsLastButton = 1 << 1
	}

	[Native]
	public enum CardCellState : long
	{
		Normal = 0,
		Highlighted,
		Selected
	}

	[Native]
	public enum CardCellHorizontalImageAlignment : long
	{
		Right = 0,
		Center,
		Left
	}

	[Native]
	public enum CardCellVerticalImageAlignment : long
	{
		Top = 0,
		Center,
		Bottom
	}

	[Flags]
	[Native]
	public enum ChipFieldDelimiter : ulong
	{
		None = 0,
		Return = 1 << 0,
		Space = 1 << 1,
		DidEndEditing = 1 << 2,
		Default = (Return | DidEndEditing),
		All = 0xFFFFFFFF
	}

	[Native]
	public enum CollectionViewCellAccessoryType : ulong
	{
		None,
		DisclosureIndicator,
		Checkmark,
		DetailButton
	}

	[Flags]
	[Native]
	public enum CollectionViewOrdinalPosition : ulong
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
	public enum CollectionViewCellStyle : ulong
	{
		Default,
		Grouped,
		Card
	}

	[Native]
	public enum CollectionViewCellLayoutType : ulong
	{
		List,
		Grid,
		Custom
	}

	[Native]
	public enum FlexibleHeaderScrollPhase : long
	{
		Shifting,
		Collapsing,
		OverExtending
	}

	[Native]
	public enum FlexibleHeaderShiftBehavior : long
	{
		Disabled,
		Enabled,
		EnabledWithStatusBar
	}

	[Native]
	public enum FlexibleHeaderContentImportance : long
	{
		Default,
		High
	}

	[Native]
	public enum FloatingButtonShape : long
	{
		Default = 0,
		Mini = 1
	}

	[Native]
	public enum FloatingButtonMode : long
	{
		Normal = 0,
		Expanded = 1
	}

	[Native]
	public enum FloatingButtonImageLocation : long
	{
		Leading = 0,
		Trailing = 1
	}

	[Native]
	public enum ActionEmphasis : long
	{
		Low = 0,
		Medium = 1,
		High = 2
	}

	[Native]
	public enum ShapeCornerFamily : long
	{
		Rounded,
		Cut
	}

	[Native]
	public enum ShapeSchemeDefaults : long
	{
		Material201809
	}

	[Native]
	public enum FontTextStyle : long
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

	[Native]
	public enum InkStyle : long
	{
		Bounded,
		Unbounded
	}

	[Native]
	public enum CornerTreatmentValueType : long
	{
		Absolute,
		Percentage
	}

	[Native]
	public enum NavigationBarTitleAlignment : long
	{
		Center,
		Leading
	}

	[Native]
	public enum NavigationBarTitleViewLayoutBehavior : long
	{
		Fill,
		Center
	}

	[Native]
	public enum ProgressViewBackwardAnimationMode : long
	{
		Reset,
		Animate
	}

	[Native]
	public enum ColorSchemeDefaults : long
	{
		Material201804
	}

	[Native]
	public enum SheetState : ulong
	{
		Closed,
		Preferred,
		Extended
	}

	[Native]
	public enum SnackbarAlignment : long
	{
		Center = 0,
		Leading = 1
	}

	[Native]
	public enum TabBarItemState : long
	{
		Normal,
		Selected
	}

	[Native]
	public enum TabBarAlignment : long
	{
		Leading,
		Justified,
		Center,
		CenterSelected
	}

	[Native]
	public enum TabBarItemAppearance : long
	{
		Titles,
		Images,
		TitledImages
	}

	[Native]
	public enum TabBarTextTransform : long
	{
		Automatic = 0,
		None = 1,
		Uppercase = 2
	}

	[Native]
	public enum TextInputTextInsetsMode : ulong
	{
		Never = 0,
		IfContent,
		Always
	}

	[Native]
	public enum TriangleEdgeStyle : ulong
	{
		Handle,
		Cut
	}

	[Native]
	public enum TypographySchemeDefaults : long
	{
		Material201804
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

	//	// BOOL MDCCGFloatEqual (CGFloat a, CGFloat b);
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

	//	// CGFloat MDCCeilScaled (CGFloat value, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCCeilScaled (nfloat value, nfloat scale);

	//	// CGFloat MDCFloorScaled (CGFloat value, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern nfloat MDCFloorScaled (nfloat value, nfloat scale);

	//	// CGRect MDCRectAlignToScale (CGRect rect, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGRect MDCRectAlignToScale (CGRect rect, nfloat scale);

	//	// CGPoint MDCPointRoundWithScale (CGPoint point, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGPoint MDCPointRoundWithScale (CGPoint point, nfloat scale);

	//	// CGSize MDCSizeCeilWithScale (CGSize size, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGSize MDCSizeCeilWithScale (CGSize size, nfloat scale);

	//	// CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, CGFloat scale);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGPoint MDCRoundCenterWithBoundsAndScale (CGPoint center, CGRect bounds, nfloat scale);

	//	// extern UIViewAutoresizing MDFLeadingMarginAutoresizingMaskForLayoutDirection (UIUserInterfaceLayoutDirection layoutDirection);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern UIViewAutoresizing MDFLeadingMarginAutoresizingMaskForLayoutDirection (UIUserInterfaceLayoutDirection layoutDirection);

	//	// extern UIViewAutoresizing MDFTrailingMarginAutoresizingMaskForLayoutDirection (UIUserInterfaceLayoutDirection layoutDirection);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern UIViewAutoresizing MDFTrailingMarginAutoresizingMaskForLayoutDirection (UIUserInterfaceLayoutDirection layoutDirection);

	//	// extern CGRect MDFRectFlippedHorizontally (CGRect frame, CGFloat containerWidth);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern CGRect MDFRectFlippedHorizontally (CGRect frame, nfloat containerWidth);

	//	// extern UIEdgeInsets MDFInsetsFlippedHorizontally (UIEdgeInsets insets);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern UIEdgeInsets MDFInsetsFlippedHorizontally (UIEdgeInsets insets);

	//	// extern UIEdgeInsets MDFInsetsMakeWithLayoutDirection (CGFloat top, CGFloat leading, CGFloat bottom, CGFloat trailing, UIUserInterfaceLayoutDirection layoutDirection);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern UIEdgeInsets MDFInsetsMakeWithLayoutDirection (nfloat top, nfloat leading, nfloat bottom, nfloat trailing, UIUserInterfaceLayoutDirection layoutDirection);

	//	// extern MDMMotionCurve MDMMotionCurveMakeBezier (CGFloat p1x, CGFloat p1y, CGFloat p2x, CGFloat p2y);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern MDMMotionCurve MDMMotionCurveMakeBezier (nfloat p1x, nfloat p1y, nfloat p2x, nfloat p2y);

	//	// extern MDMMotionCurve MDMMotionCurveFromTimingFunction (CAMediaTimingFunction * _Nonnull timingFunction);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern MDMMotionCurve MDMMotionCurveFromTimingFunction (CAMediaTimingFunction timingFunction);

	//	// extern MDMMotionCurve MDMMotionCurveMakeSpring (CGFloat mass, CGFloat tension, CGFloat friction);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern MDMMotionCurve MDMMotionCurveMakeSpring (nfloat mass, nfloat tension, nfloat friction);

	//	// extern MDMMotionCurve MDMMotionCurveMakeSpringWithInitialVelocity (CGFloat mass, CGFloat tension, CGFloat friction, CGFloat initialVelocity);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern MDMMotionCurve MDMMotionCurveMakeSpringWithInitialVelocity (nfloat mass, nfloat tension, nfloat friction, nfloat initialVelocity);

	//	// extern MDMMotionCurve MDMMotionCurveReversedBezier (MDMMotionCurve motionCurve);
	//	[DllImport ("__Internal")]
	//	[Verify (PlatformInvoke)]
	//	static extern MDMMotionCurve MDMMotionCurveReversedBezier (MDMMotionCurve motionCurve);
	//}
}
