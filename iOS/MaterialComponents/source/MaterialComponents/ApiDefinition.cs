using System;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using NSTextAlignment = UIKit.UITextAlignment;

namespace MaterialComponents
{

    delegate void ActionSheetHandler(ActionSheetAction arg0);

    delegate void RippleCompletionBlock();

    delegate void ActivityIndicatorAnimationHandler(nfloat strokeStart, nfloat strokeEnd);

    delegate void ActionHandler(AlertAction action);

    delegate void FeatureHighlightCompletionHandler(bool accepted);

    delegate void FlexibleHeaderChangeContentInsetsHandler();

    delegate void FlexibleHeaderShadowIntensityChangeHandler(CALayer shadowLayer, nfloat intensity);

    delegate void InkCompletionHandler();

    delegate void EnumerateOverlaysHandler(IOverlay overlay, nuint idx, ref bool stop);

    delegate void SnackbarMessageCompletionHandler(bool arg0);

    delegate void SnackbarMessageActionHandler();


    [Protocol(Name = "MDCElevatable")]
    interface Elevatable
    {
        [Abstract]
        [Export("mdc_currentElevation")]
        nfloat CurrentElevation { get; }

        [Abstract]
        [NullAllowed]
        [Export("mdc_elevationDidChangeBlock", ArgumentSemantic.Copy)]
        Action<IElevatable, nfloat> ElevationDidChangeBlock { get; set; }
    }

    [Protocol(Name = "MDCElevationOverriding")]
    interface ElevationOverriding
    {
        [Abstract]
        [Export("mdc_overrideBaseElevation")]
        nfloat OverrideBaseElevation { get; set; }
    }

    [Category]
    [BaseType(typeof(UIColor))]
    interface UIColor_MaterialElevation
    {
        [Export("mdc_resolvedColorWithElevation:")]
        UIColor ResolvedColorWithElevation(nfloat elevation);

        [Export("mdc_resolvedColorWithTraitCollection:previousTraitCollection:elevation:")]
        UIColor ResolvedColorWithTraitCollection(UITraitCollection traitCollection, UITraitCollection previousTraitCollection, nfloat elevation);

        [Export("mdc_resolvedColorWithTraitCollection:elevation:")]
        UIColor ResolvedColorWithTraitCollection(UITraitCollection traitCollection, nfloat elevation);
    }

    [Category]
    [BaseType(typeof(UIView))]
    interface UIView_MaterialElevationResponding
    {
        [Export("mdc_baseElevation")]
        [Static]
        nfloat BaseElevation { get; }

        [Export("mdc_absoluteElevation")]
        [Static]
        nfloat AbsoluteElevation { get; }

        [Export("mdc_elevationDidChange")]
        void ElevationDidChange();
    }

    [BaseType(typeof(NSObject),
        Name = "MDCCornerTreatment")]
    interface CornerTreatment : INSCopying

    {
        [Export("valueType", ArgumentSemantic.Assign)]
        CornerTreatmentValueType ValueType { get; set; }

        [Export("pathGeneratorForCornerWithAngle:")]
        PathGenerator PathGeneratorForCornerWithAngle(nfloat angle);

        [Export("pathGeneratorForCornerWithAngle:forViewSize:")]
        PathGenerator PathGeneratorForCornerWithAngle(nfloat angle, CGSize size);

        [Static]
        [Export("cornerWithRadius:")]
        RoundedCornerTreatment CreateCornerWithRadius(nfloat value);

        [Static]
        [Export("cornerWithRadius:valueType:")]
        RoundedCornerTreatment CreateCornerWithRadius(nfloat value, CornerTreatmentValueType valueType);

        [Static]
        [Export("cornerWithCut:")]
        CutCornerTreatment CreateCornerWithCut(nfloat value);

        [Static]
        [Export("cornerWithCut:valueType:")]
        CutCornerTreatment CreateCornerWithCut(nfloat value, CornerTreatmentValueType valueType);

        [Static]
        [Export("cornerWithCurve:")]
        CurvedCornerTreatment CreateCornerWithCurve(CGSize value);

        [Static]
        [Export("cornerWithCurve:valueType:")]
        CurvedCornerTreatment CreateCornerWithCurve(CGSize value, CornerTreatmentValueType valueType);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCEdgeTreatment")]
    interface EdgeTreatment : INSCopying

    {
        [Export("pathGeneratorForEdgeWithLength:")]
        PathGenerator GetPathGeneratorForEdge(nfloat length);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCPathGenerator")]
    interface PathGenerator
    {
        [Export("startPoint")]
        CGPoint StartPoint { get; }

        [Export("endPoint")]
        CGPoint EndPoint { get; }

        [Static]
        [Export("pathGenerator")]
        PathGenerator Create();

        [Static]
        [Export("pathGeneratorWithStartPoint:")]
        PathGenerator Create(CGPoint startPoint);

        [Export("addLineToPoint:")]
        void AddLine(CGPoint point);

        [Export("addArcWithCenter:radius:startAngle:endAngle:clockwise:")]
        void AddArc(CGPoint center, nfloat radius, nfloat startAngle, nfloat endAngle, bool clockwise);

        [Export("addArcWithTangentPoint:toPoint:radius:")]
        void AddArc(CGPoint tangentPoint, CGPoint toPoint, nfloat radius);

        [Export("addCurveWithControlPoint1:controlPoint2:toPoint:")]
        void AddCurve(CGPoint controlPoint1, CGPoint controlPoint2, CGPoint toPoint);

        [Export("addQuadCurveWithControlPoint:toPoint:")]
        void AddQuadCurve(CGPoint controlPoint, CGPoint toPoint);

        [Export("appendToCGPath:transform:")]
        void AppendTo(CGPath cgPath, [NullAllowed]  CGAffineTransform transform);
    }

    [Protocol(Name = "MDCShapeGenerating")]
    interface ShapeGenerating : INSCopying

    {
        [return: NullAllowed]
        [Abstract]
        [Export("pathForSize:")]
        CGPath GetPath(CGSize size);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCRectangleShapeGenerator")]
    interface RectangleShapeGenerator : ShapeGenerating

    {
        [Export("topLeftCorner", ArgumentSemantic.Strong)]
        CornerTreatment TopLeftCorner { get; set; }

        [Export("topRightCorner", ArgumentSemantic.Strong)]
        CornerTreatment TopRightCorner { get; set; }

        [Export("bottomLeftCorner", ArgumentSemantic.Strong)]
        CornerTreatment BottomLeftCorner { get; set; }

        [Export("bottomRightCorner", ArgumentSemantic.Strong)]
        CornerTreatment BottomRightCorner { get; set; }

        [Export("topLeftCornerOffset", ArgumentSemantic.Assign)]
        CGPoint TopLeftCornerOffset { get; set; }

        [Export("topRightCornerOffset", ArgumentSemantic.Assign)]
        CGPoint TopRightCornerOffset { get; set; }

        [Export("bottomLeftCornerOffset", ArgumentSemantic.Assign)]
        CGPoint BottomLeftCornerOffset { get; set; }

        [Export("bottomRightCornerOffset", ArgumentSemantic.Assign)]
        CGPoint BottomRightCornerOffset { get; set; }

        [Export("topEdge", ArgumentSemantic.Strong)]
        EdgeTreatment TopEdge { get; set; }

        [Export("rightEdge", ArgumentSemantic.Strong)]
        EdgeTreatment RightEdge { get; set; }

        [Export("bottomEdge", ArgumentSemantic.Strong)]
        EdgeTreatment BottomEdge { get; set; }

        [Export("leftEdge", ArgumentSemantic.Strong)]
        EdgeTreatment LeftEdge { get; set; }

        [Export("setCorners:")]
        void SetCorners(CornerTreatment cornerShape);

        [Export("setEdges:")]
        void SetEdges(EdgeTreatment edgeShape);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCShadowMetrics")]
    interface ShadowMetrics
    {
        [Export("topShadowRadius")]
        nfloat TopShadowRadius { get; }

        [Export("topShadowOffset")]
        CGSize TopShadowOffset { get; }

        [Export("topShadowOpacity")]
        float TopShadowOpacity { get; }

        [Export("bottomShadowRadius")]
        nfloat BottomShadowRadius { get; }

        [Export("bottomShadowOffset")]
        CGSize BottomShadowOffset { get; }

        [Export("bottomShadowOpacity")]
        float BottomShadowOpacity { get; }

        [Static]
        [Export("metricsWithElevation:")]
        ShadowMetrics Create(nfloat elevation);
    }

    [BaseType(typeof(CALayer),
        Name = "MDCShadowLayer")]
    interface ShadowLayer : ICALayerDelegate

    {
        [Export("elevation")]
        nfloat Elevation { get; set; }

        [Export("shadowMaskEnabled")]
        bool ShadowMaskEnabled { [Bind("isShadowMaskEnabled")] get; set; }

        [Export("animateCornerRadius:withTimingFunction:duration:")]
        void AnimateCornerRadius(nfloat cornerRadius, CAMediaTimingFunction timingFunction, double duration);
    }

    [BaseType(typeof(ShadowLayer),
        Name = "MDCShapedShadowLayer")]
    interface ShapedShadowLayer
    {
        [NullAllowed]
        [Export("shapedBackgroundColor", ArgumentSemantic.Strong)]
        UIColor ShapedBackgroundColor { get; set; }

        [NullAllowed]
        [Export("shapedBorderColor", ArgumentSemantic.Strong)]
        UIColor ShapedBorderColor { get; set; }

        [Export("shapedBorderWidth")]
        nfloat ShapedBorderWidth { get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [Export("shapeLayer", ArgumentSemantic.Strong)]
        CAShapeLayer ShapeLayer { get; set; }

        [Export("colorLayer", ArgumentSemantic.Strong)]
        CAShapeLayer ColorLayer { get; set; }
    }

    [BaseType(typeof(UIView),
        Name = "MDCShapedView")]
    interface ShapedView
    {
        [Export("elevation")]
        nfloat Elevation { get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [DesignatedInitializer]
        [Export("initWithFrame:shapeGenerator:")]
        IntPtr Constructor(CGRect frame, [NullAllowed]  IShapeGenerating shapeGenerator);

        [Wrap("this (frame, null)")]
        IntPtr Constructor(CGRect frame);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(UIViewController),
        Name = "MDCBottomSheetController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(BottomSheetControllerDelegate) })]
    interface BottomSheetController : IElevatable, IElevationOverriding

    {
        [Export("contentViewController", ArgumentSemantic.Strong)]
        UIViewController ContentViewController { get; }

        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [Export("shouldFlashScrollIndicatorsOnAppearance")]
        bool ShouldFlashScrollIndicatorsOnAppearance { get; set; }

        [Export("dismissOnBackgroundTap")]
        bool DismissOnBackgroundTap { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [Export("isScrimAccessibilityElement")]
        bool IsScrimAccessibilityElement { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityLabel")]
        string ScrimAccessibilityLabel { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityHint")]
        string ScrimAccessibilityHint { get; set; }

        [Export("scrimAccessibilityTraits")]
        ulong ScrimAccessibilityTraits { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IBottomSheetControllerDelegate Delegate { get; set; }

        [Export("state")]
        SheetState State { get; }

        [Export("elevation")]
        double Elevation { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomSheetController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("initWithContentViewController:")]
        IntPtr Constructor(UIViewController contentViewController);

        [Export("setShapeGenerator:forState:")]
        void SetShapeGenerator([NullAllowed] IShapeGenerating shapeGenerator, SheetState state);

        [return: NullAllowed]
        [Export("shapeGeneratorForState:")]
        IShapeGenerating ShapeGenerator(SheetState state);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomSheetControllerDelegate")]
    interface BottomSheetControllerDelegate
    {
        [EventArgs("BottomSheetControllerBottomSheetDismissed")]
        [EventName("BottomSheetDismissed")]
        [Export("bottomSheetControllerDidDismissBottomSheet:")]
        void DidDismissBottomSheet(BottomSheetController controller);

        [EventArgs("BottomSheetControllerBottomSheetStateChanged")]
        [EventName("BottomSheetStateChanged")]
        [Export("bottomSheetControllerStateChanged:state:")]
        void BottomSheetControllerStateChanged(BottomSheetController controller, SheetState state);

        [EventArgs("BottomSheetControllerBottomSheetDidChangeYOffset")]
        [EventName("BottomSheetDidChangeYOffset")]
        [Export("bottomSheetControllerDidChangeYOffset:yOffset:")]
        void BottomSheetControllerDidChangeYOffset(BottomSheetController controller, nfloat yOffset);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomSheetPresentationControllerDelegate")]
    interface BottomSheetPresentationControllerDelegate : IUIAdaptivePresentationControllerDelegate

    {
        [EventArgs("BottomSheetPresentationControllerPrepareForPresentation")]
        [Export("prepareForBottomSheetPresentation:")]
        void PrepareForPresentation(BottomSheetPresentationController bottomSheet);

        [EventArgs("BottomSheetPresentationControllerDismissed")]
        [EventName("Dismissed")]
        [Export("bottomSheetPresentationControllerDidDismissBottomSheet:")]
        void DidDismiss(BottomSheetPresentationController bottomSheet);

        [EventArgs("BottomSheetPresentationControllerWillChangeState")]
        [Export("bottomSheetWillChangeState:sheetState:")]
        void WillChangeState(BottomSheetPresentationController bottomSheet, SheetState sheetState);

        [EventArgs("BottomSheetPresentationControllerDidChangeYOffset")]
        [EventName("DidChangeYOffset")]
        [Export("bottomSheetDidChangeYOffset:yOffset:")]
        void BottomSheetDidChangeYOffset(BottomSheetPresentationController bottomSheet, nfloat yOffset);
    }

    [BaseType(typeof(UIPresentationController),
        Name = "MDCBottomSheetPresentationController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(BottomSheetPresentationControllerDelegate) })]
    interface BottomSheetPresentationController
    {
        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [Export("dismissOnBackgroundTap")]
        bool DismissOnBackgroundTap { get; set; }

        [Export("preferredSheetHeight")]
        nfloat PreferredSheetHeight { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [Export("isScrimAccessibilityElement")]
        bool IsScrimAccessibilityElement { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityLabel")]
        string ScrimAccessibilityLabel { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityHint")]
        string ScrimAccessibilityHint { get; set; }

        [Export("scrimAccessibilityTraits")]
        ulong ScrimAccessibilityTraits { get; set; }

        [New]
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IBottomSheetPresentationControllerDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomSheetPresentationController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCBottomSheetTransitionController")]
    interface BottomSheetTransitionController : IUIViewControllerAnimatedTransitioning, IUIViewControllerTransitioningDelegate

    {
        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [Export("dismissOnBackgroundTap")]
        bool DismissOnBackgroundTap { get; set; }

        [Export("preferredSheetHeight")]
        nfloat PreferredSheetHeight { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [Export("isScrimAccessibilityElement")]
        bool IsScrimAccessibilityElement { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityLabel")]
        string ScrimAccessibilityLabel { get; set; }

        [NullAllowed]
        [Export("scrimAccessibilityHint")]
        string ScrimAccessibilityHint { get; set; }

        [Export("scrimAccessibilityTraits")]
        ulong ScrimAccessibilityTraits { get; set; }
    }

    [Category]
    [BaseType(typeof(UIViewController))]
    interface UIViewController_MaterialBottomSheet
    {
        [return: NullAllowed]
        [Export("mdc_bottomSheetPresentationController")]
        BottomSheetPresentationController GetBottomSheetPresentationController();

        [return: NullAllowed]
        [Wrap("GetBottomSheetPresentationController(This)")]
        BottomSheetPresentationController MdcGetBottomSheetPresentationController();
    }

    [BaseType(typeof(UIViewController),
        Name = "MDCActionSheetController")]
    interface ActionSheetController : IElevatable, IElevationOverriding

    {
        [Export("actions", ArgumentSemantic.Copy)]
        ActionSheetAction Actions { get; }

        [NullAllowed]
        [Export("title")]
        string Title { get; set; }

        [NullAllowed]
        [Export("message")]
        string Message { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<ActionSheetController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [Export("messageFont", ArgumentSemantic.Strong)]
        UIFont MessageFont { get; set; }

        [NullAllowed]
        [Export("actionFont", ArgumentSemantic.Strong)]
        UIFont ActionFont { get; set; }

        [Export("backgroundColor", ArgumentSemantic.Strong)]
        UIColor BackgroundColor { get; set; }

        [NullAllowed]
        [Export("titleTextColor", ArgumentSemantic.Strong)]
        UIColor TitleTextColor { get; set; }

        [NullAllowed]
        [Export("messageTextColor", ArgumentSemantic.Strong)]
        UIColor MessageTextColor { get; set; }

        [NullAllowed]
        [Export("actionTextColor", ArgumentSemantic.Strong)]
        UIColor ActionTextColor { get; set; }

        [NullAllowed]
        [Export("actionTintColor", ArgumentSemantic.Strong)]
        UIColor ActionTintColor { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("imageRenderingMode", ArgumentSemantic.Assign)]
        UIImageRenderingMode ImageRenderingMode { get; set; }

        [Export("showsHeaderDivider")]
        bool ShowsHeaderDivider { get; set; }

        [Export("headerDividerColor", ArgumentSemantic.Copy)]
        UIColor HeaderDividerColor { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [Export("alwaysAlignTitleLeadingEdges")]
        bool AlwaysAlignTitleLeadingEdges { get; set; }

        [Export("transitionController", ArgumentSemantic.Strong)]
        BottomSheetTransitionController TransitionController { get; }

        [Static]
        [Export("actionSheetControllerWithTitle:message:")]
        ActionSheetController ActionSheetControllerWithTitle([NullAllowed] string title, [NullAllowed]  string message);

        [Static]
        [Export("actionSheetControllerWithTitle:")]
        ActionSheetController ActionSheetControllerWithTitle([NullAllowed] string title);

        [Export("addAction:")]
        void AddAction(ActionSheetAction action);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCActionSheetAction")]
    interface ActionSheetAction : INSCopying, IUIAccessibilityIdentification

    {
        [Export("title")]
        string Title { get; }

        [NullAllowed]
        [Export("image")]
        UIImage Image { get; }

        [NullAllowed]
        [Export("accessibilityIdentifier")]
        string AccessibilityIdentifier { get; set; }

        [NullAllowed]
        [Export("titleColor", ArgumentSemantic.Copy)]
        UIColor TitleColor { get; set; }

        [NullAllowed]
        [Export("tintColor", ArgumentSemantic.Copy)]
        UIColor TintColor { get; set; }

        [Static]
        [Export("actionWithTitle:image:handler:")]
        ActionSheetAction ActionWithTitle(string title, [NullAllowed]  UIImage image, [NullAllowed]  ActionSheetHandler handler);

        [NullAllowed]
        [Export("inkColor", ArgumentSemantic.Strong)]
        [Static]
        UIColor InkColor { get; set; }
    }

    [Advice("This class will soon be deprecated. Consider using IColorScheming interface instead.")]
    [Protocol(Name = "MDCColorScheme")]
    [BaseType(typeof(NSObject))]
    interface ColorScheme
    {
        [Abstract]
        [Export("primaryColor")]
        UIColor PrimaryColor { get; }

        [Export("primaryLightColor")]
        UIColor PrimaryLightColor { get; }

        [Export("primaryDarkColor")]
        UIColor PrimaryDarkColor { get; }

        [Export("secondaryColor")]
        UIColor SecondaryColor { get; }

        [Export("secondaryLightColor")]
        UIColor SecondaryLightColor { get; }

        [Export("secondaryDarkColor")]
        UIColor SecondaryDarkColor { get; }
    }

    [Obsolete("This class will soon be deprecated. Consider using SemanticColorScheme class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBasicColorScheme")]
    interface BasicColorScheme : ColorScheme, INSCopying

    {
        [Export("primaryColor", ArgumentSemantic.Strong)]
        UIColor PrimaryColor { get; }

        [Export("primaryLightColor", ArgumentSemantic.Strong)]
        UIColor PrimaryLightColor { get; }

        [Export("primaryDarkColor", ArgumentSemantic.Strong)]
        UIColor PrimaryDarkColor { get; }

        [Export("secondaryColor", ArgumentSemantic.Strong)]
        UIColor SecondaryColor { get; }

        [Export("secondaryLightColor", ArgumentSemantic.Strong)]
        UIColor SecondaryLightColor { get; }

        [Export("secondaryDarkColor", ArgumentSemantic.Strong)]
        UIColor SecondaryDarkColor { get; }

        [DesignatedInitializer]
        [Export("initWithPrimaryColor:primaryLightColor:primaryDarkColor:secondaryColor:secondaryLightColor:secondaryDarkColor:")]
        IntPtr Constructor(UIColor primaryColor, UIColor primaryLightColor, UIColor primaryDarkColor, UIColor secondaryColor, UIColor secondaryLightColor, UIColor secondaryDarkColor);

        [Export("initWithPrimaryColor:")]
        IntPtr Constructor(UIColor primaryColor);

        [Export("initWithPrimaryColor:primaryLightColor:primaryDarkColor:")]
        IntPtr Constructor(UIColor primaryColor, UIColor primaryLightColor, UIColor primaryDarkColor);

        [Export("initWithPrimaryColor:secondaryColor:")]
        IntPtr Constructor(UIColor primaryColor, UIColor secondaryColor);
    }

    [Obsolete("This class will soon be deprecated. Consider using IColorScheming interface instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTonalColorScheme")]
    interface TonalColorScheme : ColorScheme, INSCopying

    {
        [Export("primaryColor", ArgumentSemantic.Strong)]
        UIColor PrimaryColor { get; }

        [Export("primaryLightColor", ArgumentSemantic.Strong)]
        UIColor PrimaryLightColor { get; }

        [Export("primaryDarkColor", ArgumentSemantic.Strong)]
        UIColor PrimaryDarkColor { get; }

        [Export("secondaryColor", ArgumentSemantic.Strong)]
        UIColor SecondaryColor { get; }

        [Export("secondaryLightColor", ArgumentSemantic.Strong)]
        UIColor SecondaryLightColor { get; }

        [Export("secondaryDarkColor", ArgumentSemantic.Strong)]
        UIColor SecondaryDarkColor { get; }

        [Export("primaryTonalPalette", ArgumentSemantic.Strong)]
        TonalPalette PrimaryTonalPalette { get; }

        [Export("secondaryTonalPalette", ArgumentSemantic.Strong)]
        TonalPalette SecondaryTonalPalette { get; }

        [DesignatedInitializer]
        [Export("initWithPrimaryTonalPalette:secondaryTonalPalette:")]
        IntPtr Constructor(TonalPalette primaryTonalPalette, TonalPalette secondaryTonalPalette);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTonalPalette")]
    interface TonalPalette : INSCopying

    {
        [Export("colors", ArgumentSemantic.Copy)]
        UIColor[] Colors { get; }

        [Export("mainColorIndex")]
        nuint MainColorIndex { get; }

        [Export("lightColorIndex")]
        nuint LightColorIndex { get; }

        [Export("darkColorIndex")]
        nuint DarkColorIndex { get; }

        [Export("mainColor", ArgumentSemantic.Strong)]
        UIColor MainColor { get; }

        [Export("lightColor", ArgumentSemantic.Strong)]
        UIColor LightColor { get; }

        [Export("darkColor", ArgumentSemantic.Strong)]
        UIColor DarkColor { get; }

        [DesignatedInitializer]
        [Export("initWithColors:mainColorIndex:lightColorIndex:darkColorIndex:")]
        IntPtr Constructor(UIColor colors, nuint mainColorIndex, nuint lightColorIndex, nuint darkColorIndex);
    }

    [Protocol(Name = "MDCColorScheming")]
    interface ColorScheming
    {
        [Abstract]
        [Export("primaryColor", ArgumentSemantic.Copy)]
        UIColor PrimaryColor { get; }

        [Abstract]
        [Export("primaryColorVariant", ArgumentSemantic.Copy)]
        UIColor PrimaryColorVariant { get; }

        [Abstract]
        [Export("secondaryColor", ArgumentSemantic.Copy)]
        UIColor SecondaryColor { get; }

        [Abstract]
        [Export("errorColor", ArgumentSemantic.Copy)]
        UIColor ErrorColor { get; }

        [Abstract]
        [Export("surfaceColor", ArgumentSemantic.Copy)]
        UIColor SurfaceColor { get; }

        [Abstract]
        [Export("backgroundColor", ArgumentSemantic.Copy)]
        UIColor BackgroundColor { get; }

        [Abstract]
        [Export("onPrimaryColor", ArgumentSemantic.Copy)]
        UIColor OnPrimaryColor { get; }

        [Abstract]
        [Export("onSecondaryColor", ArgumentSemantic.Copy)]
        UIColor OnSecondaryColor { get; }

        [Abstract]
        [Export("onSurfaceColor", ArgumentSemantic.Copy)]
        UIColor OnSurfaceColor { get; }

        [Abstract]
        [Export("onBackgroundColor", ArgumentSemantic.Copy)]
        UIColor OnBackgroundColor { get; }

        [Abstract]
        [Export("elevationOverlayColor", ArgumentSemantic.Copy)]
        UIColor ElevationOverlayColor { get; }

        [Abstract]
        [Export("elevationOverlayEnabledForDarkMode")]
        bool ElevationOverlayEnabledForDarkMode { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCSemanticColorScheme")]
    interface SemanticColorScheme : ColorScheming, INSCopying

    {
        [Export("primaryColor", ArgumentSemantic.Copy)]
        UIColor PrimaryColor { get; set; }

        [Export("primaryColorVariant", ArgumentSemantic.Copy)]
        UIColor PrimaryColorVariant { get; set; }

        [Export("secondaryColor", ArgumentSemantic.Copy)]
        UIColor SecondaryColor { get; set; }

        [Export("errorColor", ArgumentSemantic.Copy)]
        UIColor ErrorColor { get; set; }

        [Export("surfaceColor", ArgumentSemantic.Copy)]
        UIColor SurfaceColor { get; set; }

        [Export("backgroundColor", ArgumentSemantic.Copy)]
        UIColor BackgroundColor { get; set; }

        [Export("onPrimaryColor", ArgumentSemantic.Copy)]
        UIColor OnPrimaryColor { get; set; }

        [Export("onSecondaryColor", ArgumentSemantic.Copy)]
        UIColor OnSecondaryColor { get; set; }

        [Export("onSurfaceColor", ArgumentSemantic.Copy)]
        UIColor OnSurfaceColor { get; set; }

        [Export("onBackgroundColor", ArgumentSemantic.Copy)]
        UIColor OnBackgroundColor { get; set; }

        [Export("elevationOverlayColor", ArgumentSemantic.Copy)]
        UIColor ElevationOverlayColor { get; set; }

        [Export("elevationOverlayEnabledForDarkMode")]
        bool ElevationOverlayEnabledForDarkMode { get; set; }

        [Export("initWithDefaults:")]
        IntPtr Constructor(ColorSchemeDefaults defaults);

        [Static]
        [Export("blendColor:withBackgroundColor:")]
        UIColor BlendColor(UIColor color, UIColor backgroundColor);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCShapeCategory")]
    interface ShapeCategory : INSCopying

    {
        [Export("topLeftCorner", ArgumentSemantic.Copy)]
        CornerTreatment TopLeftCorner { get; set; }

        [Export("topRightCorner", ArgumentSemantic.Copy)]
        CornerTreatment TopRightCorner { get; set; }

        [Export("bottomLeftCorner", ArgumentSemantic.Copy)]
        CornerTreatment BottomLeftCorner { get; set; }

        [Export("bottomRightCorner", ArgumentSemantic.Copy)]
        CornerTreatment BottomRightCorner { get; set; }

        [Export("initCornersWithFamily:andSize:")]
        IntPtr Constructor(ShapeCornerFamily cornerFamily, nfloat cornerSize);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCShapeScheming")]
    interface ShapeScheming
    {
        [Abstract]
        [Export("smallComponentShape")]
        ShapeCategory SmallComponentShape { get; }

        [Abstract]
        [Export("mediumComponentShape")]
        ShapeCategory MediumComponentShape { get; }

        [Abstract]
        [Export("largeComponentShape")]
        ShapeCategory LargeComponentShape { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCShapeScheme")]
    interface ShapeScheme : ShapeScheming

    {
        [Export("smallComponentShape", ArgumentSemantic.Assign)]
        ShapeCategory SmallComponentShape { get; set; }

        [Export("mediumComponentShape", ArgumentSemantic.Assign)]
        ShapeCategory MediumComponentShape { get; set; }

        [Export("largeComponentShape", ArgumentSemantic.Assign)]
        ShapeCategory LargeComponentShape { get; set; }

        [Export("initWithDefaults:")]
        IntPtr Constructor(ShapeSchemeDefaults defaults);
    }

    [Obsolete("This class will soon be deprecated. Consider using ITypographyScheming interface instead.")]
    [Protocol(Name = "MDCFontScheme")]
    [BaseType(typeof(NSObject))]
    interface FontScheme
    {
        [Abstract]
        [NullAllowed]
        [Export("headline1", ArgumentSemantic.Strong)]
        UIFont Headline1 { get; }

        [Abstract]
        [NullAllowed]
        [Export("headline2", ArgumentSemantic.Strong)]
        UIFont Headline2 { get; }

        [Abstract]
        [NullAllowed]
        [Export("headline3", ArgumentSemantic.Strong)]
        UIFont Headline3 { get; }

        [Abstract]
        [NullAllowed]
        [Export("headline4", ArgumentSemantic.Strong)]
        UIFont Headline4 { get; }

        [Abstract]
        [NullAllowed]
        [Export("headline5", ArgumentSemantic.Strong)]
        UIFont Headline5 { get; }

        [Abstract]
        [NullAllowed]
        [Export("headline6", ArgumentSemantic.Strong)]
        UIFont Headline6 { get; }

        [Abstract]
        [NullAllowed]
        [Export("subtitle1", ArgumentSemantic.Strong)]
        UIFont Subtitle1 { get; }

        [Abstract]
        [NullAllowed]
        [Export("subtitle2", ArgumentSemantic.Strong)]
        UIFont Subtitle2 { get; }

        [Abstract]
        [NullAllowed]
        [Export("body1", ArgumentSemantic.Strong)]
        UIFont Body1 { get; }

        [Abstract]
        [NullAllowed]
        [Export("body2", ArgumentSemantic.Strong)]
        UIFont Body2 { get; }

        [Abstract]
        [NullAllowed]
        [Export("caption", ArgumentSemantic.Strong)]
        UIFont Caption { get; }

        [Abstract]
        [NullAllowed]
        [Export("button", ArgumentSemantic.Strong)]
        UIFont Button { get; }

        [Abstract]
        [NullAllowed]
        [Export("overline", ArgumentSemantic.Strong)]
        UIFont Overline { get; }
    }

    [Obsolete("This class will soon be deprecated. Consider using TypographyScheme class instead.")]
    [BaseType(typeof(NSObject),
        Name = "MDCBasicFontScheme")]
    interface BasicFontScheme : FontScheme

    {
        [NullAllowed]
        [Export("headline1", ArgumentSemantic.Assign)]
        UIFont Headline1 { get; set; }

        [NullAllowed]
        [Export("headline2", ArgumentSemantic.Assign)]
        UIFont Headline2 { get; set; }

        [NullAllowed]
        [Export("headline3", ArgumentSemantic.Assign)]
        UIFont Headline3 { get; set; }

        [NullAllowed]
        [Export("headline4", ArgumentSemantic.Assign)]
        UIFont Headline4 { get; set; }

        [NullAllowed]
        [Export("headline5", ArgumentSemantic.Assign)]
        UIFont Headline5 { get; set; }

        [NullAllowed]
        [Export("headline6", ArgumentSemantic.Assign)]
        UIFont Headline6 { get; set; }

        [NullAllowed]
        [Export("subtitle1", ArgumentSemantic.Assign)]
        UIFont Subtitle1 { get; set; }

        [NullAllowed]
        [Export("subtitle2", ArgumentSemantic.Assign)]
        UIFont Subtitle2 { get; set; }

        [NullAllowed]
        [Export("body1", ArgumentSemantic.Assign)]
        UIFont Body1 { get; set; }

        [NullAllowed]
        [Export("body2", ArgumentSemantic.Assign)]
        UIFont Body2 { get; set; }

        [NullAllowed]
        [Export("caption", ArgumentSemantic.Assign)]
        UIFont Caption { get; set; }

        [NullAllowed]
        [Export("button", ArgumentSemantic.Assign)]
        UIFont Button { get; set; }

        [NullAllowed]
        [Export("overline", ArgumentSemantic.Assign)]
        UIFont Overline { get; set; }
    }

    [Protocol(Name = "MDCTypographyScheming")]
    [BaseType(typeof(NSObject))]
    interface TypographyScheming
    {
        [Abstract]
        [Export("headline1", ArgumentSemantic.Copy)]
        UIFont Headline1 { get; }

        [Abstract]
        [Export("headline2", ArgumentSemantic.Copy)]
        UIFont Headline2 { get; }

        [Abstract]
        [Export("headline3", ArgumentSemantic.Copy)]
        UIFont Headline3 { get; }

        [Abstract]
        [Export("headline4", ArgumentSemantic.Copy)]
        UIFont Headline4 { get; }

        [Abstract]
        [Export("headline5", ArgumentSemantic.Copy)]
        UIFont Headline5 { get; }

        [Abstract]
        [Export("headline6", ArgumentSemantic.Copy)]
        UIFont Headline6 { get; }

        [Abstract]
        [Export("subtitle1", ArgumentSemantic.Copy)]
        UIFont Subtitle1 { get; }

        [Abstract]
        [Export("subtitle2", ArgumentSemantic.Copy)]
        UIFont Subtitle2 { get; }

        [Abstract]
        [Export("body1", ArgumentSemantic.Copy)]
        UIFont Body1 { get; }

        [Abstract]
        [Export("body2", ArgumentSemantic.Copy)]
        UIFont Body2 { get; }

        [Abstract]
        [Export("caption", ArgumentSemantic.Copy)]
        UIFont Caption { get; }

        [Abstract]
        [Export("button", ArgumentSemantic.Copy)]
        UIFont Button { get; }

        [Abstract]
        [Export("overline", ArgumentSemantic.Copy)]
        UIFont Overline { get; }

        [Abstract]
        [Export("useCurrentContentSizeCategoryWhenApplied")]
        bool UseCurrentContentSizeCategoryWhenApplied { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTypographyScheme")]
    interface TypographyScheme : TypographyScheming, INSCopying

    {
        [Export("headline1", ArgumentSemantic.Copy)]
        UIFont Headline1 { get; set; }

        [Export("headline2", ArgumentSemantic.Copy)]
        UIFont Headline2 { get; set; }

        [Export("headline3", ArgumentSemantic.Copy)]
        UIFont Headline3 { get; set; }

        [Export("headline4", ArgumentSemantic.Copy)]
        UIFont Headline4 { get; set; }

        [Export("headline5", ArgumentSemantic.Copy)]
        UIFont Headline5 { get; set; }

        [Export("headline6", ArgumentSemantic.Copy)]
        UIFont Headline6 { get; set; }

        [Export("subtitle1", ArgumentSemantic.Copy)]
        UIFont Subtitle1 { get; set; }

        [Export("subtitle2", ArgumentSemantic.Copy)]
        UIFont Subtitle2 { get; set; }

        [Export("body1", ArgumentSemantic.Copy)]
        UIFont Body1 { get; set; }

        [Export("body2", ArgumentSemantic.Copy)]
        UIFont Body2 { get; set; }

        [Export("caption", ArgumentSemantic.Copy)]
        UIFont Caption { get; set; }

        [Export("button", ArgumentSemantic.Copy)]
        UIFont Button { get; set; }

        [Export("overline", ArgumentSemantic.Copy)]
        UIFont Overline { get; set; }

        [Export("useCurrentContentSizeCategoryWhenApplied")]
        bool UseCurrentContentSizeCategoryWhenApplied { get; set; }

        [Export("initWithDefaults:")]
        IntPtr Constructor(TypographySchemeDefaults defaults);
    }

    [Protocol(Name = "MDCContainerScheming")]
    interface ContainerScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("typographyScheme")]
        TypographyScheming TypographyScheme { get; }

        [Abstract]
        [NullAllowed]
        [Export("shapeScheme")]
        ShapeScheming ShapeScheme { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCContainerScheme")]
    interface ContainerScheme : IContainerScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        SemanticColorScheme ColorScheme { get; set; }

        [Export("typographyScheme", ArgumentSemantic.Assign)]
        TypographyScheme TypographyScheme { get; set; }

        [NullAllowed]
        [Export("shapeScheme", ArgumentSemantic.Assign)]
        ShapeScheme ShapeScheme { get; set; }
    }

    [Category]
    [BaseType(typeof(ActionSheetController),
        Name = "MDCActionSheetController_MaterialTheming")]
    interface ActionSheetController_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [BaseType(typeof(UIView),
        Name = "MDCActivityIndicator",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(ActivityIndicatorDelegate) })]
    interface ActivityIndicator
    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IActivityIndicatorDelegate Delegate { get; set; }

        [Export("animating")]
        bool Animating { [Bind("isAnimating")] get; set; }

        [Export("radius")]
        nfloat Radius { get; set; }

        [Export("strokeWidth")]
        nfloat StrokeWidth { get; set; }

        [Export("trackEnabled")]
        bool TrackEnabled { get; set; }

        [Export("indicatorMode", ArgumentSemantic.Assign)]
        ActivityIndicatorMode IndicatorMode { get; set; }

        [Export("progress")]
        float Progress { get; set; }

        [Export("cycleColors", ArgumentSemantic.Copy)]
        UIColor[] CycleColors { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<ActivityIndicator, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setIndicatorMode:animated:")]
        void SetIndicatorMode(ActivityIndicatorMode mode, bool animated);

        [Export("setProgress:animated:")]
        void SetProgress(float progress, bool animated);

        [Export("startAnimating")]
        void StartAnimating();

        [Export("startAnimatingWithTransition:cycleStartIndex:")]
        void StartAnimating(ActivityIndicatorTransition startTransition, nint cycleStartIndex);

        [Export("stopAnimating")]
        void StopAnimating();

        [Export("stopAnimatingWithTransition:")]
        void StopAnimatingWithTransition(ActivityIndicatorTransition stopTransition);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCActivityIndicatorDelegate")]
    interface ActivityIndicatorDelegate
    {
        [EventArgs("ActivityIndicatorFinished")]
        [EventName("AnimationFinished")]
        [Export("activityIndicatorAnimationDidFinish:")]
        void AnimationDidFinish(ActivityIndicator activityIndicator);

        [EventArgs("ActivityIndicatorFinished")]
        [EventName("ModeTransitionFinished")]
        [Export("activityIndicatorModeTransitionDidFinish:")]
        void ModeTransitionDidFinish(ActivityIndicator activityIndicator);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCActivityIndicatorTransition")]
    interface ActivityIndicatorTransition
    {
        [Export("animation", ArgumentSemantic.Copy)]
        ActivityIndicatorAnimationHandler Animation { get; set; }

        [NullAllowed]
        [Export("completion", ArgumentSemantic.Copy)]
        Action Completion { get; set; }

        [Export("duration")]
        double Duration { get; set; }

        [DesignatedInitializer]
        [Export("initWithAnimation:")]
        IntPtr Constructor(ActivityIndicatorAnimationHandler animation);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCActivityIndicatorColorThemer")]
    interface ActivityIndicatorColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toActivityIndicator:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, ActivityIndicator activityIndicator);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toActivityIndicator:")]
        void ApplyColorScheme(IColorScheme colorScheme, ActivityIndicator activityIndicator);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(UIViewController),
        Name = "MDCAppBarContainerViewController")]
    interface AppBarContainerViewController
    {
        [Export("appBarViewController", ArgumentSemantic.Strong)]
        AppBarViewController AppBarViewController { get; }

        [Export("contentViewController", ArgumentSemantic.Strong)]
        UIViewController ContentViewController { get; }

        [Export("topLayoutGuideAdjustmentEnabled")]
        bool TopLayoutGuideAdjustmentEnabled { [Bind("isTopLayoutGuideAdjustmentEnabled")] get; set; }

        [Obsolete("This API will eventually be deprecated. Use AppBarViewController property instead.")]
        [Export("appBar", ArgumentSemantic.Strong)]
        AppBar AppBar { get; }

        [DesignatedInitializer]
        [Export("initWithContentViewController:")]
        IntPtr Constructor(UIViewController contentViewController);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCAppBarNavigationControllerDelegate")]
    interface AppBarNavigationControllerDelegate : IUINavigationControllerDelegate

    {
        [EventArgs("AppBarNavigationControllerWillAddAppBarViewController")]
        [Export("appBarNavigationController:willAddAppBarViewController:asChildOfViewController:")]
        void WillAddAppBarViewController(AppBarNavigationController navigationController, AppBarViewController appBarViewController, UIViewController viewController);

        [return: NullAllowed]
        [Export("appBarNavigationController:trackingScrollViewForViewController:suggestedTrackingScrollView:")]
        UIScrollView TrackScrollView(AppBarNavigationController navigationController, UIViewController viewController, [NullAllowed]  UIScrollView scrollView);

        [EventArgs("AppBarNavigationControllerWillAddAppBar")]
        [Obsolete("This method will soon be deprecated. Please use WillAddAppBarViewController method instead.")]
        [Export("appBarNavigationController:willAddAppBar:asChildOfViewController:")]
        void WillAddAppBar(AppBarNavigationController navigationController, AppBar appBar, UIViewController viewController);
    }

    [BaseType(typeof(UINavigationController),
        Name = "MDCAppBarNavigationController")]
    interface AppBarNavigationController
    {
        [New]
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IAppBarNavigationControllerDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlockForAppBarController", ArgumentSemantic.Copy)]
        Action<FlexibleHeaderViewController, UITraitCollection> TraitCollectionDidChangeBlockForAppBarController { get; set; }

        [return: NullAllowed]
        [Export("appBarViewControllerForViewController:")]
        AppBarViewController GetAppBarViewController(UIViewController viewController);

        [Obsolete("This method will eventually be deprecated. Use GetAppBarViewController method instead.")]
        [return: NullAllowed]
        [Export("appBarForViewController:")]
        AppBar GetAppBar(UIViewController viewController);
    }

    [BaseType(typeof(UIViewController),
        Name = "MDCFlexibleHeaderContainerViewController")]
    interface FlexibleHeaderContainerViewController
    {
        [Export("headerViewController", ArgumentSemantic.Strong)]
        FlexibleHeaderViewController HeaderViewController { get; }

        [NullAllowed]
        [Export("contentViewController", ArgumentSemantic.Strong)]
        UIViewController ContentViewController { get; set; }

        [Export("topLayoutGuideAdjustmentEnabled")]
        bool TopLayoutGuideAdjustmentEnabled { [Bind("isTopLayoutGuideAdjustmentEnabled")] get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<FlexibleHeaderContainerViewController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [DesignatedInitializer]
        [Export("initWithContentViewController:")]
        IntPtr Constructor([NullAllowed] UIViewController contentViewController);
    }

    [BaseType(typeof(UIView),
        Name = "MDCFlexibleHeaderView",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(FlexibleHeaderViewDelegate) })]
    interface FlexibleHeaderView : IElevatable, IElevationOverriding

    {
        [Export("shadowLayer", ArgumentSemantic.Strong)]
        CALayer ShadowLayer { get; set; }

        [Export("shadowColor", ArgumentSemantic.Copy)]
        UIColor ShadowColor { get; set; }

        [Export("prefersStatusBarHidden")]
        bool PrefersStatusBarHidden { get; }

        [Export("scrollPhase")]
        FlexibleHeaderScrollPhase ScrollPhase { get; }

        [Export("scrollPhaseValue")]
        nfloat ScrollPhaseValue { get; }

        [Export("scrollPhasePercentage")]
        nfloat ScrollPhasePercentage { get; }

        [Export("minimumHeight")]
        nfloat MinimumHeight { get; set; }

        [Export("maximumHeight")]
        nfloat MaximumHeight { get; set; }

        [Export("minMaxHeightIncludesSafeArea")]
        bool MinMaxHeightIncludesSafeArea { get; set; }

        [Export("topSafeAreaGuide")]
        NSObject TopSafeAreaGuide { get; }

        [Export("canOverExtend")]
        bool CanOverExtend { get; set; }

        [Export("visibleShadowOpacity")]
        float VisibleShadowOpacity { get; set; }

        [Export("resetShadowAfterTrackingScrollViewIsReset")]
        bool ResetShadowAfterTrackingScrollViewIsReset { get; set; }

        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [Export("observesTrackingScrollViewScrollEvents")]
        bool ObservesTrackingScrollViewScrollEvents { get; set; }

        [Export("inFrontOfInfiniteContent")]
        bool InFrontOfInfiniteContent { [Bind("isInFrontOfInfiniteContent")] get; set; }

        [Export("sharedWithManyScrollViews")]
        bool SharedWithManyScrollViews { get; set; }

        [Introduced(PlatformName.iOS, 11, 0)]
        [Introduced(PlatformName.TvOS, 11, 0)]
        [Export("disableContentInsetAdjustmentWhenContentInsetAdjustmentBehaviorIsNever")]
        bool DisableContentInsetAdjustmentWhenContentInsetAdjustmentBehaviorIsNever { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IFlexibleHeaderViewDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<FlexibleHeaderView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [Export("canAlwaysExpandToMaximumHeight")]
        bool CanAlwaysExpandToMaximumHeight { get; set; }

        [Export("shiftBehavior", ArgumentSemantic.Assign)]
        FlexibleHeaderShiftBehavior ShiftBehavior { get; set; }

        [Export("headerContentImportance", ArgumentSemantic.Assign)]
        FlexibleHeaderContentImportance HeaderContentImportance { get; set; }

        [Export("trackingScrollViewIsBeingScrubbed")]
        bool TrackingScrollViewIsBeingScrubbed { get; set; }

        [Export("contentIsTranslucent")]
        bool ContentIsTranslucent { get; set; }

        [Export("statusBarHintCanOverlapHeader")]
        bool StatusBarHintCanOverlapHeader { get; set; }

        [Export("setShadowLayer:intensityDidChangeBlock:")]
        void SetShadowLayer(CALayer shadowLayer, FlexibleHeaderShadowIntensityChangeHandler block);

        [Advice("Do not invoke this method if ObservesTrackingScrollViewScrollEvents property is set to true.")]
        [Export("trackingScrollViewDidScroll")]
        void TrackingScrollViewDidScroll();

        [Export("trackingScrollViewDidChangeAdjustedContentInset:")]
        void TrackingScrollViewDidChangeAdjustedContentInset([NullAllowed] UIScrollView trackingScrollView);

        [Export("trackingScrollWillChangeToScrollView:")]
        void TrackingScrollWillChangeToScrollView([NullAllowed] UIScrollView scrollView);

        [Export("interfaceOrientationWillChange")]
        void InterfaceOrientationWillChange();

        [Export("interfaceOrientationIsChanging")]
        void InterfaceOrientationIsChanging();

        [Export("interfaceOrientationDidChange")]
        void InterfaceOrientationDidChange();

        [Export("viewWillTransitionToSize:withTransitionCoordinator:")]
        void ViewWillTransitionToSize(CGSize size, IUIViewControllerTransitionCoordinator coordinator);

        [Export("changeContentInsets:")]
        void ChangeContentInsets(FlexibleHeaderChangeContentInsetsHandler block);

        [Export("forwardTouchEventsForView:")]
        void ForwardTouchEventsForView(UIView view);

        [Export("stopForwardingTouchEventsForView:")]
        void StopForwardingTouchEventsForView(UIView view);

        [Export("hideViewWhenShifted:")]
        void HideViewWhenShifted(UIView view);

        [Export("stopHidingViewWhenShifted:")]
        void StopHidingViewWhenShifted(UIView view);

        [Export("shiftHeaderOnScreenAnimated:")]
        void ShiftHeaderOnScreen(bool animated);

        [Export("shiftHeaderOffScreenAnimated:")]
        void ShiftHeaderOffScreen(bool animated);

        [Advice("Do not invoke this method if ObservesTrackingScrollViewScrollEvents property is set to true.")]
        [Export("trackingScrollViewDidEndDraggingWillDecelerate:")]
        void TrackingScrollViewDidEndDragging(bool willDecelerate);

        [Advice("Do not invoke this method if ObservesTrackingScrollViewScrollEvents property is set to true.")]
        [Export("trackingScrollViewDidEndDecelerating")]
        void TrackingScrollViewDidEndDecelerating();

        [Advice("Do not invoke this method if ObservesTrackingScrollViewScrollEvents property is set to true.")]
        [Export("trackingScrollViewWillEndDraggingWithVelocity:targetContentOffset:")]
        bool TrackingScrollViewWillEndDragging(CGPoint velocity, ref CGPoint targetContentOffset);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCFlexibleHeaderViewDelegate")]
    interface FlexibleHeaderViewDelegate
    {
        [EventArgs("FlexibleHeaderView")]
        [Abstract]
        [Export("flexibleHeaderViewNeedsStatusBarAppearanceUpdate:")]
        void NeedsStatusBarAppearanceUpdate(FlexibleHeaderView headerView);

        [EventArgs("FlexibleHeaderView")]
        [EventName("FrameChanged")]
        [Abstract]
        [Export("flexibleHeaderViewFrameDidChange:")]
        void FrameDidChange(FlexibleHeaderView headerView);
    }

    [BaseType(typeof(UIViewController),
        Name = "MDCFlexibleHeaderViewController",
        Delegates = new[] { "LayoutDelegate" },
        Events = new[] { typeof(FlexibleHeaderViewLayoutDelegate) })]
    interface FlexibleHeaderViewController : IUIScrollViewDelegate, IUITableViewDelegate

    {
        [Export("headerView", ArgumentSemantic.Strong)]
        FlexibleHeaderView HeaderView { get; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<FlexibleHeaderViewController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [NullAllowed]
        [Export("layoutDelegate", ArgumentSemantic.Weak)]
        IFlexibleHeaderViewLayoutDelegate LayoutDelegate { get; set; }

        [Wrap("WeakSafeAreaDelegate")]
        [NullAllowed]
        IFlexibleHeaderSafeAreaDelegate SafeAreaDelegate { get; set; }

        [NullAllowed]
        [Export("safeAreaDelegate", ArgumentSemantic.Weak)]
        NSObject WeakSafeAreaDelegate { get; set; }

        [Export("topLayoutGuideAdjustmentEnabled")]
        bool TopLayoutGuideAdjustmentEnabled { [Bind("isTopLayoutGuideAdjustmentEnabled")] get; set; }

        [NullAllowed]
        [Export("topLayoutGuideViewController", ArgumentSemantic.Weak)]
        UIViewController TopLayoutGuideViewController { get; set; }

        [Export("inferTopSafeAreaInsetFromViewController")]
        bool InferTopSafeAreaInsetFromViewController { get; set; }

        [Export("useAdditionalSafeAreaInsetsForWebKitScrollViews")]
        bool UseAdditionalSafeAreaInsetsForWebKitScrollViews { get; set; }

        [New]
        [Export("prefersStatusBarHidden")]
        bool PrefersStatusBarHidden { get; }

        [New]
        [Export("preferredStatusBarStyle")]
        UIStatusBarStyle PreferredStatusBarStyle { get; }

        [Export("inferPreferredStatusBarStyle")]
        bool InferPreferredStatusBarStyle { get; set; }

        [Export("updateTopLayoutGuide")]
        void UpdateTopLayoutGuide();
    }

    [Protocol(Name = "MDCFlexibleHeaderSafeAreaDelegate")]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject))]
    interface FlexibleHeaderSafeAreaDelegate
    {
        [return: NullAllowed]
        [Abstract]
        [Export("flexibleHeaderViewControllerTopSafeAreaInsetViewController:")]
        UIViewController FlexibleHeaderViewControllerTopSafeAreaInsetViewController(FlexibleHeaderViewController flexibleHeaderViewController);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCFlexibleHeaderViewLayoutDelegate")]
    interface FlexibleHeaderViewLayoutDelegate
    {
        [EventArgs("FlexibleHeaderViewLayoutFrameChanged")]
        [EventName("FrameChanged")]
        [Abstract]
        [Export("flexibleHeaderViewController:flexibleHeaderViewFrameDidChange:")]
        void FrameDidChange(FlexibleHeaderViewController flexibleHeaderViewController, FlexibleHeaderView flexibleHeaderView);
    }

    [BaseType(typeof(UIView),
        Name = "MDCHeaderStackView")]
    interface HeaderStackView
    {
        [NullAllowed]
        [Export("topBar", ArgumentSemantic.Strong)]
        UIView TopBar { get; set; }

        [NullAllowed]
        [Export("bottomBar", ArgumentSemantic.Strong)]
        UIView BottomBar { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<HeaderStackView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }
    }

    [Protocol(Name = "MDCUINavigationItemObservables")]
    [BaseType(typeof(NSObject))]
    interface UINavigationItemObservables
    {
        [Abstract]
        [NullAllowed]
        [Export("title")]
        string Title { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("titleView", ArgumentSemantic.Strong)]
        UIView TitleView { get; set; }

        [Abstract]
        [Export("hidesBackButton")]
        bool HidesBackButton { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("leftBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] LeftBarButtonItems { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("rightBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] RightBarButtonItems { get; set; }

        [Abstract]
        [Export("leftItemsSupplementBackButton")]
        bool LeftItemsSupplementBackButton { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("leftBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem LeftBarButtonItem { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("rightBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem RightBarButtonItem { get; set; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCNavigationBarTextColorAccessibilityMutator")]
    interface NavigationBarTextColorAccessibilityMutator
    {
        [Export("mutate:")]
        void Mutate(NavigationBar navBar);
    }

    [BaseType(typeof(UIView),
        Name = "MDCNavigationBar")]
    interface NavigationBar : IElevatable, IElevationOverriding

    {
        [NullAllowed]
        [Export("title")]
        string Title { get; set; }

        [NullAllowed]
        [Export("titleView", ArgumentSemantic.Strong)]
        UIView TitleView { get; set; }

        [Export("titleViewLayoutBehavior", ArgumentSemantic.Assign)]
        NavigationBarTitleViewLayoutBehavior TitleViewLayoutBehavior { get; set; }

        [Export("titleInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets TitleInsets { get; set; }

        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [Export("allowAnyTitleFontSize")]
        bool AllowAnyTitleFontSize { get; set; }

        [NullAllowed]
        [Export("titleTextColor", ArgumentSemantic.Strong)]
        UIColor TitleTextColor { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("uppercasesButtonTitles")]
        bool UppercasesButtonTitles { get; set; }

        [NullAllowed]
        [Export("leadingBarItemsTintColor", ArgumentSemantic.Strong)]
        UIColor LeadingBarItemsTintColor { get; set; }

        [NullAllowed]
        [Export("trailingBarItemsTintColor", ArgumentSemantic.Strong)]
        UIColor TrailingBarItemsTintColor { get; set; }

        [NullAllowed]
        [Export("backItem", ArgumentSemantic.Strong)]
        UIBarButtonItem BackItem { get; set; }

        [Export("hidesBackButton")]
        bool HidesBackButton { get; set; }

        [NullAllowed]
        [Export("leadingBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] LeadingBarButtonItems { get; set; }

        [NullAllowed]
        [Export("trailingBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] TrailingBarButtonItems { get; set; }

        [Export("leadingItemsSupplementBackButton")]
        bool LeadingItemsSupplementBackButton { get; set; }

        [NullAllowed]
        [Export("leadingBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem LeadingBarButtonItem { get; set; }

        [NullAllowed]
        [Export("trailingBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem TrailingBarButtonItem { get; set; }

        [Export("titleAlignment", ArgumentSemantic.Assign)]
        NavigationBarTitleAlignment TitleAlignment { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<NavigationBar, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [NullAllowed]
        [Export("leftBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] LeftBarButtonItems { get; set; }

        [NullAllowed]
        [Export("rightBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] RightBarButtonItems { get; set; }

        [NullAllowed]
        [Export("leftBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem LeftBarButtonItem { get; set; }

        [NullAllowed]
        [Export("rightBarButtonItem", ArgumentSemantic.Strong)]
        UIBarButtonItem RightBarButtonItem { get; set; }

        [Export("leftItemsSupplementBackButton")]
        bool LeftItemsSupplementBackButton { get; set; }

        [Obsolete("This property will be deprecated in future, please use TitleFont and TitleTextColor properties instead.")]
        [NullAllowed]
        [Export("titleTextAttributes", ArgumentSemantic.Copy)]
        NSDictionary<NSString, NSObject> TitleTextAttributes { get; set; }

        [Export("setButtonsTitleFont:forState:")]
        void SetButtonsTitleFont([NullAllowed] UIFont font, UIControlState state);

        [return: NullAllowed]
        [Export("buttonsTitleFontForState:")]
        UIFont GetButtonsTitleFont(UIControlState state);

        [Export("setButtonsTitleColor:forState:")]
        void SetButtonsTitleColor([NullAllowed] UIColor color, UIControlState state);

        [return: NullAllowed]
        [Export("buttonsTitleColorForState:")]
        UIColor GetButtonsTitleColor(UIControlState state);

        [Export("rectForLeadingBarButtonItem:inCoordinateSpace:")]
        CGRect RectForLeadingBarButtonItem(UIBarButtonItem item, UICoordinateSpace coordinateSpace);

        [Export("rectForTrailingBarButtonItem:inCoordinateSpace:")]
        CGRect RectForTrailingBarButtonItem(UIBarButtonItem item, UICoordinateSpace coordinateSpace);

        [Export("observeNavigationItem:")]
        void ObserveNavigationItem(UINavigationItem navigationItem);

        [Export("unobserveNavigationItem")]
        void UnobserveNavigationItem();

        [NullAllowed]
        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }
    }

    [BaseType(typeof(FlexibleHeaderViewController),
        Name = "MDCAppBarViewController")]
    interface AppBarViewController
    {
        [Export("navigationBar", ArgumentSemantic.Strong)]
        NavigationBar NavigationBar { get; set; }

        [Export("headerStackView", ArgumentSemantic.Strong)]
        HeaderStackView HeaderStackView { get; set; }
    }

    [Obsolete("This API will be deprecated in favor of AppBarViewController class.")]
    [BaseType(typeof(NSObject),
        Name = "MDCAppBar")]
    interface AppBar
    {
        [Export("headerViewController", ArgumentSemantic.Strong)]
        FlexibleHeaderViewController HeaderViewController { get; }

        [Export("appBarViewController", ArgumentSemantic.Strong)]
        AppBarViewController AppBarViewController { get; }

        [Export("navigationBar", ArgumentSemantic.Strong)]
        NavigationBar NavigationBar { get; }

        [Export("headerStackView", ArgumentSemantic.Strong)]
        HeaderStackView HeaderStackView { get; }

        [Export("inferTopSafeAreaInsetFromViewController")]
        bool InferTopSafeAreaInsetFromViewController { get; set; }

        [Export("addSubviewsToParent")]
        void AddSubviewsToParent();
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAppBarColorThemer")]
    interface AppBarColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Export("applySemanticColorScheme:toAppBar:")]
        [Static]
        void ApplySemanticColorScheme(IColorScheming colorScheme, AppBar appBar);

        [Obsolete("This method will soon be deprecated.")]
        [Export("applySurfaceVariantWithColorScheme:toAppBar:")]
        [Static]
        void ApplySurfaceVariant(IColorScheming colorScheme, AppBar appBar);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Export("applyColorScheme:toAppBar:")]
        [Static]
        void ApplyColorScheme(IColorScheme colorScheme, AppBar appBar);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toAppBarViewController:")]
        void ApplyColorScheme(IColorScheming colorScheme, AppBarViewController appBarViewController);

        [Obsolete("This method will soon be deprecated.")]
        [Export("applySurfaceVariantWithColorScheme:toAppBarViewController:")]
        [Static]
        void ApplySurfaceVariant(IColorScheming colorScheme, AppBarViewController appBarViewController);
    }

    [Category]
    [BaseType(typeof(AppBarViewController),
        Name = "MDCAppBarViewController_MaterialTheming")]
    interface AppBarViewController_MaterialTheming
    {
        [Export("applyPrimaryThemeWithScheme:")]
        void ApplyPrimaryThemeWithScheme(IContainerScheming containerScheme);

        [Export("applySurfaceThemeWithScheme:")]
        void ApplySurfaceThemeWithScheme(IContainerScheming containerScheme);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAppBarTypographyThemer")]
    interface AppBarTypographyThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toAppBar:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, AppBar appBar);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toAppBarViewController:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, AppBarViewController appBarViewController);
    }

    [BaseType(typeof(UIGestureRecognizer),
        Name = "MDCInkGestureRecognizer")]
    interface InkGestureRecognizer
    {
        [Export("dragCancelDistance")]
        nfloat DragCancelDistance { get; set; }

        [Export("cancelOnDragOut")]
        bool CancelOnDragOut { get; set; }

        [Export("targetBounds", ArgumentSemantic.Assign)]
        CGRect TargetBounds { get; set; }


        [Export("isTouchWithinTargetBounds")]
        bool IsTouchWithinTargetBounds { get; }

        [Export("touchStartLocationInView:")]
        CGPoint GetTouchStartLocation(UIView view);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCInkTouchController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(InkTouchControllerDelegate) })]
    interface InkTouchController : IUIGestureRecognizerDelegate

    {
        [NullAllowed]
        [Export("view", ArgumentSemantic.Weak)]
        UIView View { get; }

        [Export("defaultInkView", ArgumentSemantic.Strong)]
        InkView DefaultInkView { get; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IInkTouchControllerDelegate Delegate { get; set; }

        [Export("delaysInkSpread")]
        bool DelaysInkSpread { get; set; }

        [Export("dragCancelDistance")]
        nfloat DragCancelDistance { get; set; }

        [Export("cancelsOnDragOut")]
        bool CancelsOnDragOut { get; set; }

        [Export("requiresFailureOfScrollViewGestures")]
        bool RequiresFailureOfScrollViewGestures { get; set; }

        [Export("targetBounds", ArgumentSemantic.Assign)]
        CGRect TargetBounds { get; set; }

        [Export("gestureRecognizer", ArgumentSemantic.Strong)]
        InkGestureRecognizer GestureRecognizer { get; }

        [DesignatedInitializer]
        [Export("initWithView:")]
        IntPtr Constructor(UIView view);

        [Export("addInkView")]
        void AddInkView();

        [Export("cancelInkTouchProcessing")]
        void CancelInkTouchProcessing();

        [return: NullAllowed]
        [Export("inkViewAtTouchLocation:")]
        InkView GetInkView(CGPoint location);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCInkTouchControllerDelegate")]
    interface InkTouchControllerDelegate
    {
        [EventArgs("InkTouchControllerInsertInkView")]
        [Export("inkTouchController:insertInkView:intoView:")]
        void InsertInkView(InkTouchController inkTouchController, UIView inkView, UIView view);

        [NoDefaultValue]
        [DelegateName("InkTouchControllerGetInkViewAtTouchLocation")]
        [return: NullAllowed]
        [Export("inkTouchController:inkViewAtTouchLocation:")]
        InkView GetInkViewAtTouchLocation(InkTouchController inkTouchController, CGPoint location);

        [DefaultValue(true)]
        [DelegateName("InkTouchControllerShouldProcessInkTouches")]
        [Export("inkTouchController:shouldProcessInkTouchesAtTouchLocation:")]
        bool ShouldProcessInkTouches(InkTouchController inkTouchController, CGPoint location);

        [EventArgs("InkTouchControllerInkViewProcessed")]
        [EventName("InkViewProcessed")]
        [Export("inkTouchController:didProcessInkView:atTouchLocation:")]
        void DidProcessInkView(InkTouchController inkTouchController, InkView inkView, CGPoint location);
    }

    [BaseType(typeof(UIView),
        Name = "MDCInkView",
        Delegates = new[] { "AnimationDelegate" },
        Events = new[] { typeof(InkViewDelegate) })]
    interface InkView
    {
        [NullAllowed]
        [Export("animationDelegate", ArgumentSemantic.Weak)]
        IInkViewDelegate AnimationDelegate { get; set; }

        [Export("inkStyle", ArgumentSemantic.Assign)]
        InkStyle InkStyle { get; set; }

        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }

        [Export("defaultInkColor", ArgumentSemantic.Strong)]
        UIColor DefaultInkColor { get; }

        [Export("maxRippleRadius")]
        nfloat MaxRippleRadius { get; set; }

        [Export("usesLegacyInkRipple")]
        bool UsesLegacyInkRipple { get; set; }

        [Export("usesCustomInkCenter")]
        bool UsesCustomInkCenter { get; set; }

        [Export("customInkCenter", ArgumentSemantic.Assign)]
        CGPoint CustomInkCenter { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<InkView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("startTouchBeganAnimationAtPoint:completion:")]
        void StartTouchBeganAnimation(CGPoint point, [NullAllowed]  InkCompletionHandler completionBlock);

        [Export("startTouchEndedAnimationAtPoint:completion:")]
        void StartTouchEndedAnimation(CGPoint point, [NullAllowed]  InkCompletionHandler completionBlock);

        [Export("cancelAllAnimationsAnimated:")]
        void CancelAllAnimations(bool animated);

        [Export("startTouchBeganAtPoint:animated:withCompletion:")]
        void StartTouchBegan(CGPoint point, bool animated, [NullAllowed]  InkCompletionHandler completionBlock);

        [Export("startTouchEndAtPoint:animated:withCompletion:")]
        void StartTouchEnd(CGPoint point, bool animated, [NullAllowed]  InkCompletionHandler completionBlock);

        [Static]
        [Export("injectedInkViewForView:")]
        InkView GetInjectedInkView(UIView view);
    }

    [BaseType(typeof(UIButton),
        Name = "MDCButton")]
    interface Button : INativeObject

    {
        [Export("inkStyle", ArgumentSemantic.Assign)]
        InkStyle InkStyle { get; set; }

        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }

        [Export("inkMaxRippleRadius")]
        nfloat InkMaxRippleRadius { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("disabledAlpha")]
        nfloat DisabledAlpha { get; set; }

        [Export("uppercaseTitle")]
        bool UppercaseTitle { [Bind("isUppercaseTitle")] get; set; }

        [Export("hitAreaInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets HitAreaInsets { get; set; }

        [Export("minimumSize", ArgumentSemantic.Assign)]
        CGSize MinimumSize { get; set; }

        [Export("maximumSize", ArgumentSemantic.Assign)]
        CGSize MaximumSize { get; set; }

        [NullAllowed]
        [Export("underlyingColorHint", ArgumentSemantic.Strong)]
        UIColor UnderlyingColorHint { get; set; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [Export("accessibilityTraitsIncludesButton")]
        bool AccessibilityTraitsIncludesButton { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<Button, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [return: NullAllowed]
        [Export("backgroundColorForState:")]
        UIColor GetBackgroundColor(UIControlState state);

        [Export("setBackgroundColor:forState:")]
        void SetBackgroundColor([NullAllowed] UIColor backgroundColor, UIControlState state);

        [Export("setBackgroundColor:")]
        void SetBackgroundColor([NullAllowed] UIColor backgroundColor);

        [return: NullAllowed]
        [Export("titleFontForState:")]
        UIFont GetTitleFont(UIControlState state);

        [Export("setTitleFont:forState:")]
        void SetTitleFont([NullAllowed] UIFont font, UIControlState state);

        [Export("setEnabled:animated:")]
        void SetEnabled(bool enabled, bool animated);

        [Export("elevationForState:")]
        nfloat GetElevation(UIControlState state);

        [Export("setElevation:forState:")]
        void SetElevation(nfloat elevation, UIControlState state);

        [return: NullAllowed]
        [Export("borderColorForState:")]
        UIColor GetBorderColor(UIControlState state);

        [Export("setBorderColor:forState:")]
        void SetBorderColor([NullAllowed] UIColor borderColor, UIControlState state);

        [return: NullAllowed]
        [Export("imageTintColorForState:")]
        UIColor GetImageTintColor(UIControlState state);

        [Export("setImageTintColor:forState:")]
        void SetImageTintColor([NullAllowed] UIColor imageTintColor, UIControlState state);

        [Export("borderWidthForState:")]
        nfloat GetBorderWidth(UIControlState state);

        [Export("setBorderWidth:forState:")]
        void SetBorderWidth(nfloat borderWidth, UIControlState state);

        [Export("setShadowColor:forState:")]
        void SetShadowColor([NullAllowed] UIColor shadowColor, UIControlState state);

        [return: NullAllowed]
        [Export("shadowColorForState:")]
        UIColor GetShadowColor(UIControlState state);

        [Wrap("SetElevation ((nfloat)elevation, state)")]
        void SetElevation(double elevation, UIControlState state);
    }

    [Obsolete("This class will be deprecated soon. Consider using TextButtonThemer with a Button instead.")]
    [BaseType(typeof(Button),
        Name = "MDCFlatButton")]
    interface FlatButton
    {
        [Export("hasOpaqueBackground")]
        bool HasOpaqueBackground { get; set; }
    }

    [BaseType(typeof(Button),
        Name = "MDCFloatingButton")]
    interface FloatingButton
    {
        [Export("mode", ArgumentSemantic.Assign)]
        FloatingButtonMode Mode { get; set; }

        [Export("imageLocation", ArgumentSemantic.Assign)]
        FloatingButtonImageLocation ImageLocation { get; set; }

        [Export("imageTitleSpace")]
        nfloat ImageTitleSpace { get; set; }

        [Static]

        [Export("defaultDimension")]
        nfloat DefaultDimension { get; }

        [Static]

        [Export("miniDimension")]
        nfloat MiniDimension { get; }

        [Static]
        [Export("floatingButtonWithShape:")]
        FloatingButton Create(FloatingButtonShape shape);

        [DesignatedInitializer]
        [Export("initWithFrame:shape:")]
        IntPtr Constructor(CGRect frame, FloatingButtonShape shape);

        [Export("initWithFrame:")]
        IntPtr Constructor(CGRect frame);

        [Export("setMinimumSize:forShape:inMode:")]
        void SetMinimumSize(CGSize minimumSize, FloatingButtonShape shape, FloatingButtonMode mode);

        [Export("setMaximumSize:forShape:inMode:")]
        void SetMaximumSize(CGSize maximumSize, FloatingButtonShape shape, FloatingButtonMode mode);

        [Export("setContentEdgeInsets:forShape:inMode:")]
        void SetContentEdgeInsets(UIEdgeInsets contentEdgeInsets, FloatingButtonShape shape, FloatingButtonMode mode);

        [Export("setHitAreaInsets:forShape:inMode:")]
        void SetHitAreaInsets(UIEdgeInsets hitAreaInsets, FloatingButtonShape shape, FloatingButtonMode mode);

        [Export("expand:completion:")]
        void Expand(bool animated, [NullAllowed]  Action completion);

        [Export("collapse:completion:")]
        void Collapse(bool animated, [NullAllowed]  Action completion);
    }

    [Obsolete("This class will be deprecated soon. Consider using ContainedButtonThemer with a Button instead.")]
    [BaseType(typeof(Button),
        Name = "MDCRaisedButton")]
    interface RaisedButton
    { }

    [BaseType(typeof(UIView),
        Name = "MDCBannerView")]
    interface BannerView : IElevatable, IElevationOverriding

    {
        [Export("bannerViewLayoutStyle", ArgumentSemantic.Assign)]
        BannerViewLayoutStyle BannerViewLayoutStyle { get; set; }

        [Export("textView", ArgumentSemantic.Strong)]
        UITextView TextView { get; }

        [Export("imageView", ArgumentSemantic.Strong)]
        UIImageView ImageView { get; }

        [Export("leadingButton", ArgumentSemantic.Strong)]
        Button LeadingButton { get; }

        [Export("trailingButton", ArgumentSemantic.Strong)]
        Button TrailingButton { get; }

        [Export("showsDivider")]
        bool ShowsDivider { get; set; }

        [Export("dividerColor", ArgumentSemantic.Strong)]
        UIColor DividerColor { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BannerView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }
    }

    [Category]
    [BaseType(typeof(BannerView),
        Name = "MDCBannerView_MaterialTheming")]
    interface BannerView_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [BaseType(typeof(UIView),
        Name = "MDCBottomAppBarView")]
    interface BottomAppBarView : IElevatable, IElevationOverriding

    {
        [Export("floatingButtonHidden")]
        bool FloatingButtonHidden { [Bind("isFloatingButtonHidden")] get; set; }

        [Export("floatingButtonElevation", ArgumentSemantic.Assign)]
        BottomAppBarFloatingButtonElevation FloatingButtonElevation { get; set; }

        [Export("floatingButtonPosition", ArgumentSemantic.Assign)]
        BottomAppBarFloatingButtonPosition FloatingButtonPosition { get; set; }

        [Export("floatingButton", ArgumentSemantic.Strong)]
        FloatingButton FloatingButton { get; }

        [Export("floatingButtonVerticalOffset")]
        nfloat FloatingButtonVerticalOffset { get; set; }

        [NullAllowed]
        [Export("leadingBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] LeadingBarButtonItems { get; set; }

        [NullAllowed]
        [Export("trailingBarButtonItems", ArgumentSemantic.Copy)]
        UIBarButtonItem[] TrailingBarButtonItems { get; set; }

        [NullAllowed]
        [Export("barTintColor", ArgumentSemantic.Strong)]
        UIColor BarTintColor { get; set; }

        [Export("leadingBarItemsTintColor", ArgumentSemantic.Strong)]
        UIColor LeadingBarItemsTintColor { get; set; }

        [Export("trailingBarItemsTintColor", ArgumentSemantic.Strong)]
        UIColor TrailingBarItemsTintColor { get; set; }

        [NullAllowed]
        [Export("shadowColor", ArgumentSemantic.Strong)]
        UIColor ShadowColor { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomAppBarView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [Export("setFloatingButtonHidden:animated:")]
        void SetFloatingButtonHidden(bool floatingButtonHidden, bool animated);

        [Export("setFloatingButtonElevation:animated:")]
        void SetFloatingButtonElevation(BottomAppBarFloatingButtonElevation floatingButtonElevation, bool animated);

        [Export("setFloatingButtonPosition:animated:")]
        void SetFloatingButtonPosition(BottomAppBarFloatingButtonPosition floatingButtonPosition, bool animated);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomAppBarColorThemer")]
    interface BottomAppBarColorThemer
    {
        [Static]
        [Export("applySurfaceVariantWithSemanticColorScheme:toBottomAppBarView:")]
        void ApplySurfaceVariant(IColorScheming colorScheme, BottomAppBarView bottomAppBarView);

        [Wrap("ApplySurfaceVariant (colorScheme, bottomAppBarView)")]
        [Obsolete("Use ApplySurfaceVariant instead.")]
        [Static]
        void ApplySurfaceVariantWithSemanticColorScheme(IColorScheming colorScheme, BottomAppBarView bottomAppBarView);

        [Obsolete("This method will soon be deprecated. Use ApplySurfaceVariantWithSemanticColorScheme instead.")]
        [Static]
        [Export("applyColorScheme:toBottomAppBarView:")]
        void ApplyColorScheme(IColorScheme colorScheme, BottomAppBarView bottomAppBarView);
    }

    [BaseType(typeof(UIView),
        Name = "MDCBottomNavigationBar",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(BottomNavigationBarDelegate) })]
    interface BottomNavigationBar : IElevatable, IElevationOverriding

    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IBottomNavigationBarDelegate Delegate { get; set; }

        [Export("titleVisibility", ArgumentSemantic.Assign)]
        BottomNavigationBarTitleVisibility TitleVisibility { get; set; }

        [Export("alignment", ArgumentSemantic.Assign)]
        BottomNavigationBarAlignment Alignment { get; set; }

        [Export("items", ArgumentSemantic.Copy)]
        UITabBarItem[] Items { get; set; }

        [NullAllowed]
        [Export("selectedItem", ArgumentSemantic.Weak)]
        UITabBarItem SelectedItem { get; set; }

        [Export("itemTitleFont", ArgumentSemantic.Strong)]
        UIFont ItemTitleFont { get; set; }

        [Export("selectedItemTintColor", ArgumentSemantic.Strong)]
        UIColor SelectedItemTintColor { get; set; }

        [Export("selectedItemTitleColor", ArgumentSemantic.Strong)]
        UIColor SelectedItemTitleColor { get; set; }

        [Export("unselectedItemTintColor", ArgumentSemantic.Strong)]
        UIColor UnselectedItemTintColor { get; set; }

        [NullAllowed]
        [Export("barTintColor", ArgumentSemantic.Strong)]
        UIColor BarTintColor { get; set; }

        [New]
        [NullAllowed]
        [Export("backgroundColor", ArgumentSemantic.Copy)]
        UIColor BackgroundColor { get; set; }

        [Export("backgroundBlurEffectStyle", ArgumentSemantic.Assign)]
        UIBlurEffectStyle BackgroundBlurEffectStyle { get; set; }

        [Export("backgroundBlurEnabled")]
        bool BackgroundBlurEnabled { [Bind("isBackgroundBlurEnabled")] get; set; }

        [Export("itemsContentVerticalMargin")]
        nfloat ItemsContentVerticalMargin { get; set; }

        [Export("itemsContentHorizontalMargin")]
        nfloat ItemsContentHorizontalMargin { get; set; }

        [iOS(9, 0)]
        [Export("barItemsBottomAnchor")]
        NSLayoutYAxisAnchor BarItemsBottomAnchor { get; }

        [Export("truncatesLongTitles")]
        bool TruncatesLongTitles { get; set; }

        [Export("elevation")]
        nfloat Elevation { get; set; }

        [Export("shadowColor", ArgumentSemantic.Copy)]
        UIColor ShadowColor { get; set; }

        [Export("titlesNumberOfLines")]
        nint TitlesNumberOfLines { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomNavigationBar, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [return: NullAllowed]
        [Export("viewForItem:")]
        UIView GetView(UITabBarItem item);

        [Export("sizeThatFitsIncludesSafeArea")]
        [Static]
        bool SizeThatFitsIncludesSafeArea { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomNavigationBarDelegate")]
    interface BottomNavigationBarDelegate : IUINavigationBarDelegate

    {
        [DefaultValue(true)]
        [DelegateName("BottomNavigationBarShouldSelectItem")]
        [Export("bottomNavigationBar:shouldSelectItem:")]
        bool ShouldSelectItem(BottomNavigationBar bottomNavigationBar, UITabBarItem item);

        [EventArgs("BottomNavigationBarItemSelected")]
        [EventName("ItemSelected")]
        [Export("bottomNavigationBar:didSelectItem:")]
        void DidSelectItem(BottomNavigationBar bottomNavigationBar, UITabBarItem item);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomNavigationBarColorThemer")]
    interface BottomNavigationBarColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toBottomNavigation:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, BottomNavigationBar bottomNavigation);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toBottomNavigationBar:")]
        void ApplyColorScheme(IColorScheme colorScheme, BottomNavigationBar bottomNavigationBar);
    }

    [Category]
    [BaseType(typeof(BottomNavigationBar),
        Name = "MDCBottomNavigationBar_MaterialTheming")]
    interface BottomNavigationBar_MaterialTheming
    {
        [Export("applyPrimaryThemeWithScheme:")]
        void ApplyPrimaryThemeWithScheme(IContainerScheming scheme);

        [Export("applySurfaceThemeWithScheme:")]
        void ApplySurfaceThemeWithScheme(IContainerScheming scheme);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomNavigationBarTypographyThemer")]
    interface BottomNavigationBarTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toBottomNavigationBar:")]
        void ApplyTypographyScheme (ITypographyScheming typographyScheme, BottomNavigationBar bottomNavigationBar);
    }

    [BaseType(typeof(CornerTreatment),
        Name = "MDCCurvedCornerTreatment")]
    interface CurvedCornerTreatment
    {
        [Export("size", ArgumentSemantic.Assign)]
        CGSize Size { get; set; }

        [DesignatedInitializer]
        [Export("initWithSize:")]
        IntPtr Constructor(CGSize size);
    }

    [BaseType(typeof(CornerTreatment),
        Name = "MDCCutCornerTreatment")]
    interface CutCornerTreatment
    {
        [Export("cut")]
        nfloat Cut { get; set; }

        [DesignatedInitializer]
        [Export("initWithCut:")]
        IntPtr Constructor(nfloat cut);
    }

    [BaseType(typeof(CornerTreatment),
        Name = "MDCRoundedCornerTreatment")]
    interface RoundedCornerTreatment
    {
        [Export("radius")]
        nfloat Radius { get; set; }

        [DesignatedInitializer]
        [Export("initWithRadius:")]
        IntPtr Constructor(nfloat radius);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCCurvedRectShapeGenerator")]
    interface CurvedRectShapeGenerator : ShapeGenerating

    {
        [Export("cornerSize", ArgumentSemantic.Assign)]
        CGSize CornerSize { get; set; }

        [DesignatedInitializer]
        [Export("initWithCornerSize:")]
        IntPtr Constructor(CGSize cornerSize);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCPillShapeGenerator")]
    interface PillShapeGenerator : ShapeGenerating

    { }

    [BaseType(typeof(NSObject),
        Name = "MDCSlantedRectShapeGenerator")]
    interface SlantedRectShapeGenerator : ShapeGenerating

    {
        [Export("slant")]
        nfloat Slant { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(EdgeTreatment),
        Name = "MDCTriangleEdgeTreatment")]
    interface TriangleEdgeTreatment
    {
        [Export("size")]
        nfloat Size { get; set; }

        [Export("style", ArgumentSemantic.Assign)]
        TriangleEdgeStyle Style { get; set; }

        [DesignatedInitializer]
        [Export("initWithSize:style:")]
        IntPtr Constructor(nfloat size, TriangleEdgeStyle style);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomSheetControllerShapeThemer")]
    interface BottomSheetControllerShapeThemer
    {
        [Static]
        [Export("applyShapeScheme:toBottomSheetController:")]
        void ApplyShapeScheme(IShapeScheming shapeScheme, BottomSheetController bottomSheetController);
    }

    [BaseType(typeof(UIView),
        Name = "MDCButtonBar",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(ButtonBarDelegate) })]
    interface ButtonBar
    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IButtonBarDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("items", ArgumentSemantic.Copy)]
        UIBarButtonItem[] Items { get; set; }

        [Export("buttonTitleBaseline")]
        nfloat ButtonTitleBaseline { get; set; }

        [Export("uppercasesButtonTitles")]
        bool UppercasesButtonTitles { get; set; }

        [Export("layoutPosition", ArgumentSemantic.Assign)]
        ButtonBarLayoutPosition LayoutPosition { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<ButtonBar, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("rectForItem:inCoordinateSpace:")]
        CGRect RectForItem(UIBarButtonItem item, UICoordinateSpace coordinateSpace);

        [Export("setButtonsTitleFont:forState:")]
        void SetButtonsTitleFont([NullAllowed] UIFont font, UIControlState state);

        [return: NullAllowed]
        [Export("buttonsTitleFontForState:")]
        UIFont GetButtonsTitleFont(UIControlState state);

        [Export("setButtonsTitleColor:forState:")]
        void SetButtonsTitleColor([NullAllowed] UIColor color, UIControlState state);

        [return: NullAllowed]
        [Export("buttonsTitleColorForState:")]
        UIColor GetButtonsTitleColor(UIControlState state);

        [New]
        [Export("sizeThatFits:")]
        CGSize SizeThatFits(CGSize size);

        [NullAllowed]
        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonBarDelegate")]
    interface ButtonBarDelegate
    {
        [EventArgs("ButtonBarDidInvalidateIntrinsicContentSize")]
        [EventName("IntrinsicContentSizeInvalidated")]
        [Export("buttonBarDidInvalidateIntrinsicContentSize:")]
        void DidInvalidateIntrinsicContentSize(ButtonBar buttonBar);

        [DefaultValue(null)]
        [DelegateName("ButtonBarViewForItem")]
        [Abstract]
        [Export("buttonBar:viewForItem:layoutHints:")]
        UIView ViewForItem(ButtonBar buttonBar, UIBarButtonItem barButtonItem, BarButtonItemLayoutHints layoutHints);
    }

    [BaseType(typeof(FlatButton),
        Name = "MDCButtonBarButton")]
    interface ButtonBarButton
    {
        [New]
        [Export("setTitleFont:forState:")]
        void SetTitleFont([NullAllowed] UIFont font, UIControlState state);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonBarColorThemer")]
    interface ButtonBarColorThemer
    {
        [Static]
        [Export("applyColorScheme:toButtonBar:")]
        void ApplyColorScheme(IColorScheme colorScheme, ButtonBar buttonBar);

        [Static]
        [Export("applySemanticColorScheme:toButtonBar:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, ButtonBar buttonBar);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonBarTypographyThemer")]
    interface ButtonBarTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toButtonBar:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, ButtonBar buttonBar);
    }

    [Protocol(Name = "MDCButtonScheming")]
    interface ButtonScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("shapeScheme")]
        IShapeScheming ShapeScheme { get; }

        [Abstract]
        [Export("typographyScheme")]
        ITypographyScheming TypographyScheme { get; }

        [Abstract]
        [Export("cornerRadius")]
        nfloat CornerRadius { get; }

        [Abstract]
        [Export("minimumHeight")]
        nfloat MinimumHeight { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCButtonScheme")]
    interface ButtonScheme : ButtonScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        IColorScheming ColorScheme { get; set; }

        [Export("shapeScheme", ArgumentSemantic.Assign)]
        IShapeScheming ShapeScheme { get; set; }

        [Export("typographyScheme", ArgumentSemantic.Assign)]
        ITypographyScheming TypographyScheme { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("minimumHeight")]
        nfloat MinimumHeight { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCContainedButtonThemer")]
    interface ContainedButtonThemer
    {
        [Static]
        [Export("applyScheme:toButton:")]
        void ApplyScheme(IButtonScheming scheme, Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFloatingActionButtonThemer")]
    interface FloatingActionButtonThemer
    {
        [Static]
        [Export("applyScheme:toButton:")]
        void ApplyScheme(IButtonScheming scheme, FloatingButton button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCOutlinedButtonThemer")]
    interface OutlinedButtonThemer
    {
        [Static]
        [Export("applyScheme:toButton:")]
        void ApplyScheme(IButtonScheming scheme, Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTextButtonThemer")]
    interface TextButtonThemer
    {
        [Static]
        [Export("applyScheme:toButton:")]
        void ApplyScheme(IButtonScheming scheme, Button button);
    }

    [Obsolete("This class will soon be deprecated. Please consider using one of the more specific ButtonColorThemer classes instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonColorThemer")]
    interface ButtonColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toButton:")]
        void ApplySemanticColorSchemeToButton(IColorScheming colorScheme, Button button);

        [Static]
        [Export("applySemanticColorScheme:toFlatButton:")]
        void ApplySemanticColorSchemeToFlatButton(IColorScheming colorScheme, Button flatButton);

        [Static]
        [Export("applySemanticColorScheme:toRaisedButton:")]
        void ApplySemanticColorSchemeToRaisedButton(IColorScheming colorScheme, Button raisedButton);

        [Static]
        [Export("applySemanticColorScheme:toFloatingButton:")]
        void ApplySemanticColorSchemeToFloatingButton(IColorScheming colorScheme, FloatingButton floatingButton);

        [Static]
        [Export("applyColorScheme:toButton:")]
        void ApplyColorScheme(IColorScheme colorScheme, Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCContainedButtonColorThemer")]
    interface ContainedButtonColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toButton:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, Button button);
    }


    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFloatingButtonColorThemer")]
    interface FloatingButtonColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toButton:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, FloatingButton button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCOutlinedButtonColorThemer")]
    interface OutlinedButtonColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toButton:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, Button button);
    }


    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTextButtonColorThemer")]
    interface TextButtonColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toButton:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonShapeThemer")]
    interface ButtonShapeThemer
    {
        [Static]
        [Export("applyShapeScheme:toButton:")]
        void ApplyShapeScheme(IShapeScheming shapeScheme, Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFloatingButtonShapeThemer")]
    interface FloatingButtonShapeThemer
    {
        [Static]
        [Export("applyShapeScheme:toButton:")]
        void ApplyShapeScheme (IShapeScheming shapeScheme, FloatingButton button);
    }


    [Category]
    [BaseType(typeof(Button),
        Name = "MDCButton_MaterialTheming")]
    interface Button_MaterialTheming
    {
        [Export("applyContainedThemeWithScheme:")]
        void ApplyContainedThemeWithScheme(IContainerScheming scheme);

        [Export("applyOutlinedThemeWithScheme:")]
        void ApplyOutlinedThemeWithScheme(IContainerScheming scheme);

        [Export("applyTextThemeWithScheme:")]
        void ApplyTextThemeWithScheme(IContainerScheming scheme);
    }

    [Category]
    [BaseType(typeof(FloatingButton),
        Name = "MDCFloatingButton_MaterialTheming")]
    interface FloatingButton_MaterialTheming
    {
        [Export("applySecondaryThemeWithScheme:")]
        void ApplySecondaryThemeWithScheme(IContainerScheming scheme);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonTitleColorAccessibilityMutator")]
    interface ButtonTitleColorAccessibilityMutator
    {
        [Static]
        [Export("changeTitleColorOfButton:")]
        void ChangeTitleColor(Button button);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCButtonTypographyThemer")]
    interface ButtonTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toButton:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, Button button);
    }

    [BaseType(typeof(UIView),
        Name = "MDCRippleView")]
    interface RippleView
    {
        [Wrap("WeakRippleViewDelegate")]
        [NullAllowed]
        IRippleViewDelegate RippleViewDelegate { get; set; }

        [NullAllowed]
        [Export("rippleViewDelegate", ArgumentSemantic.Weak)]
        NSObject WeakRippleViewDelegate { get; set; }

        [Export("rippleStyle", ArgumentSemantic.Assign)]
        RippleStyle RippleStyle { get; set; }

        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("maximumRadius")]
        nfloat MaximumRadius { get; set; }

        [Export("activeRippleColor", ArgumentSemantic.Strong)]
        UIColor ActiveRippleColor { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<RippleView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("cancelAllRipplesAnimated:completion:")]
        void CancelAllRipplesAnimated(bool animated, [NullAllowed]  RippleCompletionBlock completion);

        [Export("fadeInRippleAnimated:completion:")]
        void FadeInRippleAnimated(bool animated, [NullAllowed]  RippleCompletionBlock completion);

        [Export("fadeOutRippleAnimated:completion:")]
        void FadeOutRippleAnimated(bool animated, [NullAllowed]  RippleCompletionBlock completion);

        [Export("beginRippleTouchDownAtPoint:animated:completion:")]
        void BeginRippleTouchDownAtPoint(CGPoint point, bool animated, [NullAllowed]  RippleCompletionBlock completion);

        [Export("beginRippleTouchUpAnimated:completion:")]
        void BeginRippleTouchUpAnimated(bool animated, [NullAllowed]  RippleCompletionBlock completion);
    }

    [Protocol(Name = "MDCRippleViewDelegate")]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject))]
    interface RippleViewDelegate
    {
        [Export("rippleTouchDownAnimationDidBegin:")]
        void RippleTouchDownAnimationDidBegin(RippleView rippleView);

        [Export("rippleTouchDownAnimationDidEnd:")]
        void RippleTouchDownAnimationDidEnd(RippleView rippleView);

        [Export("rippleTouchUpAnimationDidBegin:")]
        void RippleTouchUpAnimationDidBegin(RippleView rippleView);

        [Export("rippleTouchUpAnimationDidEnd:")]
        void RippleTouchUpAnimationDidEnd(RippleView rippleView);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCRippleTouchController")]
    interface RippleTouchController : IUIGestureRecognizerDelegate

    {
        [NullAllowed]
        [Export("view", ArgumentSemantic.Weak)]
        UIView View { get; }

        [Export("rippleView", ArgumentSemantic.Strong)]
        RippleView RippleView { get; }

        [Wrap("WeakDelegate")]
        [NullAllowed]
        IRippleTouchControllerDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        [Export("gestureRecognizer", ArgumentSemantic.Strong)]
        UILongPressGestureRecognizer GestureRecognizer { get; }

        [Export("shouldProcessRippleWithScrollViewGestures")]
        bool ShouldProcessRippleWithScrollViewGestures { get; set; }

        [Export("initWithView:")]
        IntPtr Constructor(UIView view);

        [Export("addRippleToView:")]
        void AddRippleToView(UIView view);
    }

    [Protocol(Name = "MDCRippleTouchControllerDelegate")]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject))]
    interface RippleTouchControllerDelegate
    {
        [Export("rippleTouchController:shouldProcessRippleTouchesAtTouchLocation:")]
        bool RippleTouchController(RippleTouchController rippleTouchController, CGPoint location);

        [Export("rippleTouchController:didProcessRippleView:atTouchLocation:")]
        void RippleTouchController(RippleTouchController rippleTouchController, RippleView rippleView, CGPoint location);

        [Export("rippleTouchController:insertRippleView:intoView:")]
        void RippleTouchController(RippleTouchController rippleTouchController, RippleView rippleView, UIView view);
    }

    [BaseType(typeof(RippleView),
        Name = "MDCStatefulRippleView")]
    interface StatefulRippleView
    {
        [Export("selected")]
        bool Selected { [Bind("isSelected")] get; set; }

        [Export("rippleHighlighted")]
        bool RippleHighlighted { [Bind("isRippleHighlighted")] get; set; }

        [Export("dragged")]
        bool Dragged { [Bind("isDragged")] get; set; }

        [Export("allowsSelection")]
        bool AllowsSelection { get; set; }

        [Export("setRippleColor:forState:")]
        void SetRippleColor([NullAllowed] UIColor rippleColor, RippleState state);

        [return: NullAllowed]
        [Export("rippleColorForState:")]
        UIColor RippleColorForState(RippleState state);

        [Export("touchesBegan:withEvent:")]
        void TouchesBegan([NullAllowed] NSSet<UITouch> touches, [NullAllowed]  UIEvent @event);

        [Export("touchesMoved:withEvent:")]
        void TouchesMoved([NullAllowed] NSSet<UITouch> touches, [NullAllowed]  UIEvent @event);

        [Export("touchesEnded:withEvent:")]
        void TouchesEnded([NullAllowed] NSSet<UITouch> touches, [NullAllowed]  UIEvent @event);

        [Export("touchesCancelled:withEvent:")]
        void TouchesCancelled([NullAllowed] NSSet<UITouch> touches, [NullAllowed]  UIEvent @event);
    }

    [BaseType(typeof(UIControl),
        Name = "MDCCard")]
    interface Card : IElevatable, IElevationOverriding

    {
        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("inkView", ArgumentSemantic.Strong)]
        InkView InkView { get; }

        [Export("rippleView", ArgumentSemantic.Strong)]
        StatefulRippleView RippleView { get; }

        [Export("interactable")]
        bool Interactable { [Bind("isInteractable")] get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<Card, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [Export("setShadowElevation:forState:")]
        void SetShadowElevation(nfloat shadowElevation, UIControlState state);

        [Export("shadowElevationForState:")]
        nfloat GetShadowElevation(UIControlState state);

        [Export("setBorderWidth:forState:")]
        void SetBorderWidth(nfloat borderWidth, UIControlState state);

        [Export("borderWidthForState:")]
        nfloat GetBorderWidth(UIControlState state);

        [Export("setBorderColor:forState:")]
        void SetBorderColor([NullAllowed] UIColor borderColor, UIControlState state);

        [return: NullAllowed]
        [Export("borderColorForState:")]
        UIColor GetBorderColorForState(UIControlState state);

        [Export("setShadowColor:forState:")]
        void SetShadowColor([NullAllowed] UIColor shadowColor, UIControlState state);

        [return: NullAllowed]
        [Export("shadowColorForState:")]
        UIColor GetShadowColor(UIControlState state);

        [Wrap("SetShadowElevation ((nfloat)shadowElevation, state)")]
        void SetShadowElevation(double shadowElevation, UIControlState state);
    }

    [BaseType(typeof(UICollectionViewCell),
        Name = "MDCCardCollectionCell")]
    interface CardCollectionCell : IElevatable, IElevationOverriding

    {
        [Export("selectable")]
        bool Selectable { [Bind("isSelectable")] get; set; }

        [Export("dragged")]
        bool Dragged { [Bind("isDragged")] get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("inkView", ArgumentSemantic.Strong)]
        InkView InkView { get; }

        [Export("rippleView", ArgumentSemantic.Strong)]
        StatefulRippleView RippleView { get; }

        [Export("interactable")]
        bool Interactable { [Bind("isInteractable")] get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("state")]
        CardCellState State { get; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<CardCollectionCell, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setShadowElevation:forState:")]
        void SetShadowElevation(nfloat shadowElevation, CardCellState state);

        [Export("shadowElevationForState:")]
        nfloat GetShadowElevation(CardCellState state);

        [Export("setBorderWidth:forState:")]
        void SetBorderWidth(nfloat borderWidth, CardCellState state);

        [Export("borderWidthForState:")]
        nfloat GetBorderWidth(CardCellState state);

        [Export("setBorderColor:forState:")]
        void SetBorderColor([NullAllowed] UIColor borderColor, CardCellState state);

        [return: NullAllowed]
        [Export("borderColorForState:")]
        UIColor GetBorderColor(CardCellState state);

        [Export("setShadowColor:forState:")]
        void SetShadowColor([NullAllowed] UIColor shadowColor, CardCellState state);

        [return: NullAllowed]
        [Export("shadowColorForState:")]
        UIColor GetShadowColor(CardCellState state);

        [return: NullAllowed]
        [Export("imageForState:")]
        UIImage GetImage(CardCellState state);

        [Export("setImage:forState:")]
        void SetImage([NullAllowed] UIImage image, CardCellState state);

        [Export("horizontalImageAlignmentForState:")]
        CardCellHorizontalImageAlignment GetHorizontalImageAlignment(CardCellState state);

        [Export("setHorizontalImageAlignment:forState:")]
        void SetHorizontalImageAlignment(CardCellHorizontalImageAlignment horizontalImageAlignment, CardCellState state);

        [Export("verticalImageAlignmentForState:")]
        CardCellVerticalImageAlignment GetVerticalImageAlignment(CardCellState state);

        [Export("setVerticalImageAlignment:forState:")]
        void SetVerticalImageAlignment(CardCellVerticalImageAlignment verticalImageAlignment, CardCellState state);

        [return: NullAllowed]
        [Export("imageTintColorForState:")]
        UIColor GetImageTintColor(CardCellState state);

        [Export("setImageTintColor:forState:")]
        void SetImageTintColor([NullAllowed] UIColor imageTintColor, CardCellState state);

        [Wrap("SetShadowElevation ((nfloat)shadowElevation, state)")]
        void SetShadowElevation(double shadowElevation, CardCellState state);
    }

    [Category]
    [BaseType(typeof(UICollectionViewController))]
    interface UICollectionViewController_MDCCardReordering
    {
        [Export("mdc_setupCardReordering")]
        void SetupCardReordering();

        [Wrap("SetupCardReordering(This)")]
        void MdcSetupCardReordering();
    }

    [Protocol(Name = "MDCCardScheming")]
    interface CardScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("shapeScheme")]
        IShapeScheming ShapeScheme { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCCardScheme")]
    interface CardScheme : CardScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        IColorScheming ColorScheme { get; set; }

        [Export("shapeScheme", ArgumentSemantic.Assign)]
        IShapeScheming ShapeScheme { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCCardThemer")]
    interface CardThemer
    {
        [Static]
        [Export("applyScheme:toCard:")]
        void ApplyScheme(ICardScheming scheme, Card card);

        [Static]
        [Export("applyScheme:toCardCell:")]
        void ApplyScheme(ICardScheming scheme, CardCollectionCell cardCell);

        [Static]
        [Export("applyOutlinedVariantWithScheme:toCard:")]
        void ApplyOutlinedVariant(ICardScheming scheme, Card card);

        [Static]
        [Export("applyOutlinedVariantWithScheme:toCardCell:")]
        void ApplyOutlinedVariantWithScheme(ICardScheming scheme, CardCollectionCell cardCell);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCCardsColorThemer")]
    interface CardsColorThemer
    {
        [Static]
        [Export("applySemanticColorScheme:toCard:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, Card card);

        [Static]
        [Export("applySemanticColorScheme:toCardCell:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, CardCollectionCell cardCell);

        [Static]
        [Export("applyOutlinedVariantWithColorScheme:toCard:")]
        void ApplyOutlinedVariant(IColorScheming colorScheme, Card card);

        [Static]
        [Export("applyOutlinedVariantWithColorScheme:toCardCell:")]
        void ApplyOutlinedVariant(IColorScheming colorScheme, CardCollectionCell cardCell);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCCardsShapeThemer")]
    interface CardsShapeThemer
    {
        [Static]
        [Export("applyShapeScheme:toCard:")]
        void ApplyShapeScheme(IShapeScheming shapeScheme, Card card);

        [Static]
        [Export("applyShapeScheme:toCardCell:")]
        void ApplyShapeScheme(IShapeScheming shapeScheme, CardCollectionCell cardCell);
    }

    [Category]
    [BaseType(typeof(Card),
        Name = "MDCCard_MaterialTheming")]
    interface Card_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);

        [Export("applyOutlinedThemeWithScheme:")]
        void ApplyOutlinedThemeWithScheme(IContainerScheming scheme);
    }

    [Category]
    [BaseType(typeof(CardCollectionCell),
        Name = "MDCCardCollectionCell_MaterialTheming")]
    interface CardCollectionCell_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);

        [Export("applyOutlinedThemeWithScheme:")]
        void ApplyOutlinedThemeWithScheme(IContainerScheming scheme);
    }

    [BaseType(typeof(UICollectionViewCell),
        Name = "MDCChipCollectionViewCell")]
    interface ChipCollectionViewCell
    {
        [Export("chipView", ArgumentSemantic.Strong)]
        ChipView ChipView { get; }

        [Export("alwaysAnimateResize")]
        bool AlwaysAnimateResize { get; set; }

        [Export("createChipView")]
        ChipView CreateChipView();
    }

    [BaseType(typeof(UICollectionViewFlowLayout),
        Name = "MDCChipCollectionViewFlowLayout")]
    interface ChipCollectionViewFlowLayout
    { }

    [BaseType(typeof(UIControl),
        Name = "MDCChipView")]
    interface ChipView : IElevatable, IElevationOverriding

    {
        [Export("imageView")]
        UIImageView ImageView { get; }

        [Export("selectedImageView")]
        UIImageView SelectedImageView { get; }

        [NullAllowed]
        [Export("accessoryView", ArgumentSemantic.Strong)]
        UIView AccessoryView { get; set; }

        [Export("titleLabel")]
        UILabel TitleLabel { get; }

        [Export("contentPadding", ArgumentSemantic.Assign)]
        UIEdgeInsets ContentPadding { get; set; }

        [Export("imagePadding", ArgumentSemantic.Assign)]
        UIEdgeInsets ImagePadding { get; set; }

        [Export("accessoryPadding", ArgumentSemantic.Assign)]
        UIEdgeInsets AccessoryPadding { get; set; }

        [Export("titlePadding", ArgumentSemantic.Assign)]
        UIEdgeInsets TitlePadding { get; set; }

        [NullAllowed]
        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("rippleAllowsSelection")]
        bool RippleAllowsSelection { get; set; }

        [NullAllowed]
        [Export("shapeGenerator", ArgumentSemantic.Strong)]
        IShapeGenerating ShapeGenerator { get; set; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [Export("minimumSize", ArgumentSemantic.Assign)]
        CGSize MinimumSize { get; set; }

        [Export("hitAreaInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets HitAreaInsets { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<ChipView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [return: NullAllowed]
        [Export("backgroundColorForState:")]
        UIColor GetBackgroundColor(UIControlState state);

        [Export("setBackgroundColor:forState:")]
        void SetBackgroundColor([NullAllowed] UIColor backgroundColor, UIControlState state);

        [return: NullAllowed]
        [Export("borderColorForState:")]
        UIColor GetBorderColor(UIControlState state);

        [Export("setBorderColor:forState:")]
        void SetBorderColor([NullAllowed] UIColor borderColor, UIControlState state);

        [Export("borderWidthForState:")]
        nfloat GetBorderWidth(UIControlState state);

        [Export("setBorderWidth:forState:")]
        void SetBorderWidth(nfloat borderWidth, UIControlState state);

        [Export("elevationForState:")]
        nfloat GetElevation(UIControlState state);

        [Export("setElevation:forState:")]
        void SetElevation(nfloat elevation, UIControlState state);

        [return: NullAllowed]
        [Export("inkColorForState:")]
        UIColor GetInkColor(UIControlState state);

        [Export("setInkColor:forState:")]
        void SetInkColor([NullAllowed] UIColor inkColor, UIControlState state);

        [return: NullAllowed]
        [Export("shadowColorForState:")]
        UIColor GetShadowColor(UIControlState state);

        [Export("setShadowColor:forState:")]
        void SetShadowColor([NullAllowed] UIColor shadowColor, UIControlState state);

        [return: NullAllowed]
        [Export("titleColorForState:")]
        UIColor GetTitleColor(UIControlState state);

        [Export("setTitleColor:forState:")]
        void SetTitleColor([NullAllowed] UIColor titleColor, UIControlState state);

        [Wrap("SetElevation ((nfloat)elevation, state)")]
        void SetElevation(double elevation, UIControlState state);
    }

    [BaseType(typeof(UITextView),
        Name = "MDCIntrinsicHeightTextView")]
    interface IntrinsicHeightTextView
    { }

    [Protocol(Name = "MDCTextInput")]
    interface TextInput
    {
        [Abstract]
        [NullAllowed]
        [Export("attributedPlaceholder", ArgumentSemantic.Copy)]
        NSAttributedString AttributedPlaceholder { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("attributedText", ArgumentSemantic.Copy)]
        NSAttributedString AttributedText { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("borderPath", ArgumentSemantic.Copy)]
        UIBezierPath BorderPath { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("borderView", ArgumentSemantic.Strong)]
        TextInputBorderView BorderView { get; set; }

        [Abstract]
        [Export("clearButton", ArgumentSemantic.Strong)]
        UIButton ClearButton { get; }

        [Abstract]
        [Export("clearButtonMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode ClearButtonMode { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("cursorColor", ArgumentSemantic.Strong)]
        UIColor CursorColor { get; set; }

        [Abstract]
        [Export("editing")]
        bool Editing { [Bind("isEditing")] get; }

        [Abstract]
        [Export("enabled")]
        bool Enabled { [Bind("isEnabled")] get; set; }

        [Abstract]
        [NullAllowed]
        [Export("font", ArgumentSemantic.Strong)]
        UIFont Font { get; set; }

        [Abstract]
        [Export("hidesPlaceholderOnInput")]
        bool HidesPlaceholderOnInput { get; set; }

        [Abstract]
        [Export("leadingUnderlineLabel", ArgumentSemantic.Strong)]
        UILabel LeadingUnderlineLabel { get; }

        [Abstract]
        [NullAllowed]
        [Export("placeholder")]
        string Placeholder { get; set; }

        [Abstract]
        [Export("placeholderLabel", ArgumentSemantic.Strong)]
        UILabel PlaceholderLabel { get; }

        [Abstract]
        [NullAllowed]
        [Export("positioningDelegate", ArgumentSemantic.Weak)]
        ITextInputPositioningDelegate PositioningDelegate { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("text")]
        string Text { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("textColor", ArgumentSemantic.Strong)]
        UIColor TextColor { get; set; }

        [Abstract]
        [Export("textInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets TextInsets { get; }

        [Abstract]
        [Export("textInsetsMode", ArgumentSemantic.Assign)]
        TextInputTextInsetsMode TextInsetsMode { get; set; }

        [Abstract]
        [Export("trailingUnderlineLabel", ArgumentSemantic.Strong)]
        UILabel TrailingUnderlineLabel { get; }

        [Abstract]
        [NullAllowed]
        [Export("trailingView", ArgumentSemantic.Strong)]
        UIView TrailingView { get; set; }

        [Abstract]
        [Export("trailingViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode TrailingViewMode { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("underline", ArgumentSemantic.Strong)]
        TextInputUnderlineView Underline { get; }

        [Abstract]
        [Export("hasTextContent")]
        bool HasTextContent { get; }

        [Abstract]
        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Abstract]
        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Abstract]
        [Export("clearText")]
        void ClearText();
    }

    [Protocol(Name = "MDCLeadingViewTextInput")]
    interface LeadingViewTextInput : TextInput

    {
        [Abstract]
        [NullAllowed]
        [Export("leadingView", ArgumentSemantic.Strong)]
        UIView LeadingView { get; set; }

        [Abstract]
        [Export("leadingViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode LeadingViewMode { get; set; }
    }

    [Protocol(Name = "MDCMultilineTextInput")]
    interface MultilineTextInput : TextInput

    {
        [Abstract]
        [Export("expandsOnOverflow")]
        bool ExpandsOnOverflow { get; set; }

        [Abstract]
        [Export("minimumLines")]
        nuint MinimumLines { get; set; }
    }

    [BaseType(typeof(UIView),
        Name = "MDCMultilineTextField")]
    interface MultilineTextField : TextInput, MultilineTextInput

    {
        [Export("adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [Export("expandsOnOverflow")]
        bool ExpandsOnOverflow { get; set; }

        [NullAllowed]
        [Export("layoutDelegate", ArgumentSemantic.Weak)]
        IMultilineTextInputLayoutDelegate LayoutDelegate { get; set; }

        [NullAllowed]
        [Export("multilineDelegate", ArgumentSemantic.Weak)]
        IMultilineTextInputDelegate MultilineDelegate { get; set; }

        [NullAllowed]
        [Export("placeholder")]
        string Placeholder { get; set; }

        [Export("textInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets TextInsets { get; }

        [NullAllowed]
        [Export("textView", ArgumentSemantic.Strong)]
        IntrinsicHeightTextView TextView { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<MultilineTextField, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCMultilineTextInputLayoutDelegate")]
    interface MultilineTextInputLayoutDelegate
    {
        [Export("multilineTextField:didChangeContentSize:")]
        void DidChangeContentSize(IMultilineTextInput multilineTextField, CGSize size);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCMultilineTextInputDelegate")]
    interface MultilineTextInputDelegate
    {
        [Export("multilineTextFieldShouldClear:")]
        bool MultilineTextFieldShouldClear(ITextInput textField);
    }

    [BaseType(typeof(UITextField),
        Name = "MDCTextField")]
    interface TextField : TextInput, LeadingViewTextInput

    {
        [Export("inputLayoutStrut", ArgumentSemantic.Strong)]
        UILabel InputLayoutStrut { get; }

        [NullAllowed]
        [Export("leadingView", ArgumentSemantic.Strong)]
        UIView LeadingView { get; set; }

        [Export("leadingViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode LeadingViewMode { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<TextField, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Notification]
        [Field("MDCTextFieldTextDidSetTextNotification", "__Internal")]
        NSString TextDidSetTextNotification { get; }

        [Notification]
        [Field("MDCTextInputDidToggleEnabledNotification", "__Internal")]
        NSString TextInputDidToggleEnabledNotification { get; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCTextInputPositioningDelegate")]
    interface TextInputPositioningDelegate
    {
        [Export("textInsets:")]
        UIEdgeInsets TextInsets(UIEdgeInsets defaultInsets);

        [Export("editingRectForBounds:defaultRect:")]
        CGRect GetEditingRect(CGRect bounds, CGRect defaultRect);

        [Export("leadingViewRectForBounds:defaultRect:")]
        CGRect GetLeadingViewRect(CGRect bounds, CGRect defaultRect);

        [Export("textInputDidLayoutSubviews")]
        void DidLayoutSubviews();

        [Export("textInputDidUpdateConstraints")]
        void DidUpdateConstraints();

        [Export("trailingViewRectForBounds:defaultRect:")]
        CGRect GetTrailingViewRect(CGRect bounds, CGRect defaultRect);

        [Export("leadingViewTrailingPaddingConstant")]
        nfloat GetLeadingViewTrailingPaddingConstant();

        [Export("trailingViewTrailingPaddingConstant")]
        nfloat GetTrailingViewTrailingPaddingConstant();
    }

    [BaseType(typeof(UIView),
        Name = "MDCTextInputBorderView")]
    interface TextInputBorderView : INSCopying

    {
        [NullAllowed]
        [Export("borderFillColor", ArgumentSemantic.Strong)]
        UIColor BorderFillColor { get; set; }

        [NullAllowed]
        [Export("borderPath", ArgumentSemantic.Strong)]
        UIBezierPath BorderPath { get; set; }

        [NullAllowed]
        [Export("borderStrokeColor", ArgumentSemantic.Strong)]
        UIColor BorderStrokeColor { get; set; }
    }

    [Protocol(Name = "MDCTextInputCharacterCounter")]
    [BaseType(typeof(NSObject))]
    interface TextInputCharacterCounter
    {
        [Abstract]
        [Export("characterCountForTextInput:")]
        nuint CharacterCount([NullAllowed] ITextInput textInput);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTextInputAllCharactersCounter")]
    interface TextInputAllCharactersCounter : TextInputCharacterCounter

    { }

    [Protocol(Name = "MDCTextInputController")]
    interface TextInputController : INSCopying, TextInputPositioningDelegate

    {
        [Abstract]
        [Export("activeColor", ArgumentSemantic.Strong)]
        UIColor ActiveColor { get; set; }

        [Abstract]
        [Static]
        [Export("activeColorDefault", ArgumentSemantic.Strong)]
        UIColor ActiveColorDefault { get; set; }

        [Abstract]
        [Export("characterCounter", ArgumentSemantic.Weak)]
        ITextInputCharacterCounter CharacterCounter { get; set; }

        [Abstract]
        [Export("characterCountMax")]
        nuint CharacterCountMax { get; set; }

        [Abstract]
        [Export("characterCountViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode CharacterCountViewMode { get; set; }

        [Abstract]
        [Export("disabledColor", ArgumentSemantic.Strong)]
        UIColor DisabledColor { get; set; }

        [Abstract]
        [Static]
        [Export("disabledColorDefault", ArgumentSemantic.Strong)]
        UIColor DisabledColorDefault { get; set; }

        [Abstract]
        [Export("errorColor", ArgumentSemantic.Strong)]
        UIColor ErrorColor { get; set; }

        [Abstract]
        [Static]
        [Export("errorColorDefault", ArgumentSemantic.Strong)]
        UIColor ErrorColorDefault { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("errorText")]
        string ErrorText { get; }

        [Abstract]
        [NullAllowed]
        [Export("helperText")]
        string HelperText { get; set; }

        [Abstract]
        [Export("inlinePlaceholderColor", ArgumentSemantic.Strong)]
        UIColor InlinePlaceholderColor { get; set; }

        [Abstract]
        [Static]
        [Export("inlinePlaceholderColorDefault", ArgumentSemantic.Strong)]
        UIColor InlinePlaceholderColorDefault { get; set; }

        [Abstract]
        [Export("textInputFont", ArgumentSemantic.Strong)]
        UIFont TextInputFont { get; set; }

        [Abstract]
        [NullAllowed]
        [Static]
        [Export("textInputFontDefault", ArgumentSemantic.Strong)]
        UIFont TextInputFontDefault { get; set; }

        [Abstract]
        [Export("inlinePlaceholderFont", ArgumentSemantic.Strong)]
        UIFont InlinePlaceholderFont { get; set; }

        [Abstract]
        [Static]
        [Export("inlinePlaceholderFontDefault", ArgumentSemantic.Strong)]
        UIFont InlinePlaceholderFontDefault { get; set; }

        [Abstract]
        [Export("leadingUnderlineLabelFont", ArgumentSemantic.Strong)]
        UIFont LeadingUnderlineLabelFont { get; set; }

        [Abstract]
        [Static]
        [Export("leadingUnderlineLabelFontDefault", ArgumentSemantic.Strong)]
        UIFont LeadingUnderlineLabelFontDefault { get; set; }

        [Abstract]
        [Export("leadingUnderlineLabelTextColor", ArgumentSemantic.Strong)]
        UIColor LeadingUnderlineLabelTextColor { get; set; }

        [Abstract]
        [Static]
        [Export("leadingUnderlineLabelTextColorDefault", ArgumentSemantic.Strong)]
        UIColor LeadingUnderlineLabelTextColorDefault { get; set; }

        [Abstract]
        [Export("normalColor", ArgumentSemantic.Strong)]
        UIColor NormalColor { get; set; }

        [Abstract]
        [Static]
        [Export("normalColorDefault", ArgumentSemantic.Strong)]
        UIColor NormalColorDefault { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("placeholderText")]
        string PlaceholderText { get; set; }

        [Abstract]
        [Export("roundedCorners", ArgumentSemantic.Assign)]
        UIRectCorner RoundedCorners { get; set; }

        [Abstract]
        [Static]
        [Export("roundedCornersDefault", ArgumentSemantic.Assign)]
        UIRectCorner RoundedCornersDefault { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("textInput", ArgumentSemantic.Strong)]
        ITextInput TextInput { get; set; }

        [Abstract]
        [Export("textInputClearButtonTintColor", ArgumentSemantic.Strong)]
        UIColor TextInputClearButtonTintColor { get; set; }

        [Abstract]
        [NullAllowed]
        [Static]
        [Export("textInputClearButtonTintColorDefault", ArgumentSemantic.Strong)]
        UIColor TextInputClearButtonTintColorDefault { get; set; }

        [Abstract]
        [Export("trailingUnderlineLabelFont", ArgumentSemantic.Strong)]
        UIFont TrailingUnderlineLabelFont { get; set; }

        [Abstract]
        [Static]
        [Export("trailingUnderlineLabelFontDefault", ArgumentSemantic.Strong)]
        UIFont TrailingUnderlineLabelFontDefault { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("trailingUnderlineLabelTextColor", ArgumentSemantic.Strong)]
        UIColor TrailingUnderlineLabelTextColor { get; set; }

        [Abstract]
        [NullAllowed]
        [Static]
        [Export("trailingUnderlineLabelTextColorDefault", ArgumentSemantic.Strong)]
        UIColor TrailingUnderlineLabelTextColorDefault { get; set; }

        [Abstract]
        [Export("underlineHeightActive")]
        nfloat UnderlineHeightActive { get; set; }

        [Abstract]
        [Static]
        [Export("underlineHeightActiveDefault")]
        nfloat UnderlineHeightActiveDefault { get; set; }

        [Abstract]
        [Export("underlineHeightNormal")]
        nfloat UnderlineHeightNormal { get; set; }

        [Abstract]
        [Static]
        [Export("underlineHeightNormalDefault")]
        nfloat UnderlineHeightNormalDefault { get; set; }

        [Abstract]
        [Export("underlineViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode UnderlineViewMode { get; set; }

        [Abstract]
        [Static]
        [Export("underlineViewModeDefault", ArgumentSemantic.Assign)]
        UITextFieldViewMode UnderlineViewModeDefault { get; set; }

        [Abstract]
        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Abstract]
        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Abstract]
        [Static]
        [Export("mdc_adjustsFontForContentSizeCategoryDefault")]
        bool AdjustsFontForContentSizeCategoryDefault { get; set; }

        [Obsolete("Use AdjustsFontForContentSizeCategoryDefault instead")]
        [Static]
        [Wrap("AdjustsFontForContentSizeCategoryDefault")]
        bool MdcAdjustsFontForContentSizeCategoryDefault { get; set; }

        [Abstract]
        [Export("setErrorText:errorAccessibilityValue:")]
        void SetErrorText([NullAllowed] string errorText, [NullAllowed]  string errorAccessibilityValue);

        [Abstract]
        [Export("setHelperText:helperAccessibilityLabel:")]
        void SetHelperText([NullAllowed] string helperText, [NullAllowed]  string helperAccessibilityLabel);
    }

    [Protocol(Name = "MDCTextInputControllerFloatingPlaceholder")]
    interface TextInputControllerFloatingPlaceholder : TextInputController

    {
        [Abstract]
        [Export("floatingPlaceholderActiveColor", ArgumentSemantic.Strong)]
        UIColor FloatingPlaceholderActiveColor { get; set; }

        [Abstract]
        [Static]
        [Export("floatingPlaceholderActiveColorDefault", ArgumentSemantic.Strong)]
        UIColor FloatingPlaceholderActiveColorDefault { get; set; }

        [Abstract]
        [Export("floatingPlaceholderNormalColor", ArgumentSemantic.Strong)]
        UIColor FloatingPlaceholderNormalColor { get; set; }

        [Abstract]
        [Static]
        [Export("floatingPlaceholderNormalColorDefault", ArgumentSemantic.Strong)]
        UIColor FloatingPlaceholderNormalColorDefault { get; set; }

        [Abstract]
        [Export("floatingPlaceholderOffset")]
        UIOffset FloatingPlaceholderOffset { get; }

        [Abstract]
        [Export("floatingPlaceholderScale", ArgumentSemantic.Strong)]
        NSNumber FloatingPlaceholderScale { get; set; }

        [Abstract]
        [Static]
        [Export("floatingPlaceholderScaleDefault")]
        nfloat FloatingPlaceholderScaleDefault { get; set; }

        [Abstract]
        [Export("floatingEnabled")]
        bool FloatingEnabled { [Bind("isFloatingEnabled")] get; set; }

        [Abstract]
        [Static]
        [Export("floatingEnabledDefault")]
        bool FloatingEnabledDefault { [Bind("isFloatingEnabledDefault")] get; set; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTextInputControllerBase")]
    interface TextInputControllerBase : TextInputControllerFloatingPlaceholder

    {
        [NullAllowed]
        [Export("borderFillColor", ArgumentSemantic.Strong)]
        UIColor BorderFillColor { get; set; }

        [Static]
        [Export("borderFillColorDefault", ArgumentSemantic.Strong)]
        UIColor BorderFillColorDefault { get; set; }

        [Export("expandsOnOverflow")]
        bool ExpandsOnOverflow { get; set; }

        [Export("minimumLines")]
        nuint MinimumLines { get; set; }

        [Field("MDCTextInputControllerBaseDefaultBorderRadius", "__Internal")]
        nfloat DefaultBorderRadius { get; }

        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerBase),
        Name = "MDCTextInputControllerFilled")]
    interface TextInputControllerFilled
    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTextInputControllerFullWidth")]
    interface TextInputControllerFullWidth : TextInputController

    {
        [Export("backgroundColor", ArgumentSemantic.Strong)]
        UIColor BackgroundColor { get; set; }

        [Static]
        [Export("backgroundColorDefault", ArgumentSemantic.Strong)]
        UIColor BackgroundColorDefault { get; set; }

        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerBase),
        Name = "MDCTextInputControllerLegacyDefault")]
    interface TextInputControllerLegacyDefault : TextInputController

    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerFullWidth),
        Name = "MDCTextInputControllerLegacyFullWidth")]
    interface TextInputControllerLegacyFullWidth : TextInputController

    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerBase),
        Name = "MDCTextInputControllerOutlined")]
    interface TextInputControllerOutlined
    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerBase),
        Name = "MDCTextInputControllerOutlinedTextArea")]
    interface TextInputControllerOutlinedTextArea
    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(TextInputControllerBase),
        Name = "MDCTextInputControllerUnderline")]
    interface TextInputControllerUnderline
    {
        [Export("initWithTextInput:")]
        IntPtr Constructor([NullAllowed] ITextInput input);
    }

    [BaseType(typeof(UIView),
        Name = "MDCTextInputUnderlineView")]
    interface TextInputUnderlineView : INSCopying

    {
        [Export("color", ArgumentSemantic.Strong)]
        UIColor Color { get; set; }

        [Export("disabledColor", ArgumentSemantic.Strong)]
        UIColor DisabledColor { get; set; }

        [Export("enabled")]
        bool Enabled { get; set; }

        [Export("lineHeight")]
        nfloat LineHeight { get; set; }
    }

    [BaseType(typeof(UIView),
        Name = "MDCChipField",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(ChipFieldDelegate) })]
    interface ChipField
    {
        [Export("textField")]
        TextField TextField { get; }

        [Export("chipHeight")]
        nfloat ChipHeight { get; set; }

        [Export("showPlaceholderWithChips")]
        bool ShowPlaceholderWithChips { get; set; }

        [Export("showChipsDeleteButton")]
        bool ShowChipsDeleteButton { get; set; }

        [Export("delimiter", ArgumentSemantic.Assign)]
        ChipFieldDelimiter Delimiter { get; set; }

        [Export("minTextFieldWidth")]
        nfloat MinTextFieldWidth { get; set; }

        [Export("chips", ArgumentSemantic.Copy)]
        ChipView[] Chips { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IChipFieldDelegate Delegate { get; set; }

        [Export("contentEdgeInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets ContentEdgeInsets { get; set; }

        [Field("MDCChipFieldDefaultMinTextFieldWidth", "__Internal")]
        nfloat DefaultMinTextFieldWidth { get; }

        [Internal]
        [Field("MDCChipFieldDefaultContentEdgeInsets", "__Internal")]
        IntPtr _DefaultContentEdgeInsets { get; }

        [Export("addChip:")]
        void AddChip(ChipView chip);

        [Export("removeChip:")]
        void RemoveChip(ChipView chip);

        [Export("removeSelectedChips")]
        void RemoveSelectedChips();

        [Export("clearTextInput")]
        void ClearTextInput();

        [Export("selectChip:")]
        void SelectChip(ChipView chip);

        [Export("deselectAllChips")]
        void DeselectAllChips();

        [Export("focusTextFieldForAccessibility")]
        void FocusTextFieldForAccessibility();
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCChipFieldDelegate")]
    interface ChipFieldDelegate
    {
        [EventArgs("ChipFieldEditingStarted")]
        [EventName("EditingStarted")]
        [Export("chipFieldDidBeginEditing:")]
        void DidBeginEditing(ChipField chipField);

        [EventArgs("ChipFieldEditingEnded")]
        [EventName("EditingEnded")]
        [Export("chipFieldDidEndEditing:")]
        void DidEndEditing(ChipField chipField);

        [EventArgs("ChipFieldChipAdded")]
        [EventName("ChipAdded")]
        [Export("chipField:didAddChip:")]
        void DidAddChip(ChipField chipField, ChipView chip);

        [DefaultValue(true)]
        [DelegateName("ChipFieldShouldAddChip")]
        [Export("chipField:shouldAddChip:")]
        bool ShouldAddChip(ChipField chipField, ChipView chip);

        [EventArgs("ChipFieldChipRemoved")]
        [EventName("ChipRemoved")]
        [Export("chipField:didRemoveChip:")]
        void DidRemoveChip(ChipField chipField, ChipView chip);

        [EventArgs("ChipFieldHeightChanged")]
        [EventName("HeightChanged")]
        [Export("chipFieldHeightDidChange:")]
        void HeightDidChange(ChipField chipField);

        [EventArgs("ChipFieldInputChanged")]
        [EventName("InputChanged")]
        [Export("chipField:didChangeInput:")]
        void DidChangeInput(ChipField chipField, [NullAllowed]  string input);

        [EventArgs("ChipFieldChipTapped")]
        [EventName("ChipTapped")]
        [Export("chipField:didTapChip:")]
        void DidTapChip(ChipField chipField, ChipView chip);

        [DefaultValue(true)]
        [DelegateName("ChipFieldShouldReturn")]
        [Export("chipFieldShouldReturn:")]
        bool ShouldReturn(ChipField chipField);

        [DefaultValue(true)]
        [DelegateName("ChipFieldShouldBecomeFirstResponder")]
        [Export("chipFieldShouldBecomeFirstResponder:")]
        bool ShouldBecomeFirstResponder(ChipField chipField);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCChipViewScheme")]
    interface ChipViewScheme : ChipViewScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        IColorScheming ColorScheme { get; set; }

        [Export("shapeScheme", ArgumentSemantic.Assign)]
        IShapeScheming ShapeScheme { get; set; }

        [Export("typographyScheme", ArgumentSemantic.Assign)]
        ITypographyScheming TypographyScheme { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCChipViewThemer")]
    interface ChipViewThemer
    {
        [Static]
        [Export("applyScheme:toChipView:")]
        void ApplyScheme(IChipViewScheming scheme, ChipView chip);

        [Static]
        [Export("applyOutlinedVariantWithScheme:toChipView:")]
        void ApplyOutlinedVariant(IChipViewScheming scheme, ChipView chip);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCChipViewColorThemer")]
    interface ChipViewColorThemer
    {
        [Obsolete("This method will soon be deprecated")]
        [Static]
        [Export("applySemanticColorScheme:toChipView:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, ChipView chipView);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toStrokedChipView:")]
        void ApplySemanticColorSchemeToStrokedChipView(IColorScheming colorScheme, ChipView strokedChipView);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyOutlinedVariantWithColorScheme:toChipView:")]
        void ApplyOutlinedVariant(IColorScheming colorScheme, ChipView chipView);

    }

    [Obsolete("This class will soon be deprecated. Please consider using ChipViewTypographyThemer class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCChipViewFontThemer")]
    interface ChipViewFontThemer
    {
        [Static]
        [Export("applyFontScheme:toChipView:")]
        void ApplyFontScheme(IFontScheme fontScheme, ChipView chipView);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCChipViewShapeThemer")]
    interface ChipViewShapeThemer
    {
        [Static]
        [Export("applyShapeScheme:toChipView:")]
        void ApplyShapeScheme(IShapeScheming shapeScheme, ChipView chipView);
    }

    [Category]
    [BaseType(typeof(ChipView),
        Name = "MDCChipView_MaterialTheming")]
    interface ChipView_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);

        [Export("applyOutlinedThemeWithScheme:")]
        void ApplyOutlinedThemeWithScheme(IContainerScheming scheme);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCChipViewTypographyThemer")]
    interface ChipViewTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toChipView:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, ChipView chipView);
    }

    [BaseType(typeof(UICollectionViewCell),
        Name = "MDCCollectionViewCell")]
    interface CollectionViewCell
    {
        [Export("accessoryType", ArgumentSemantic.Assign)]
        CollectionViewCellAccessoryType AccessoryType { get; set; }

        [NullAllowed]
        [Export("accessoryView", ArgumentSemantic.Strong)]
        UIView AccessoryView { get; set; }

        [Export("accessoryInset", ArgumentSemantic.Assign)]
        UIEdgeInsets AccessoryInset { get; set; }

        [Export("shouldHideSeparator")]
        bool ShouldHideSeparator { get; set; }

        [Export("separatorInset", ArgumentSemantic.Assign)]
        UIEdgeInsets SeparatorInset { get; set; }

        [Export("allowsCellInteractionsWhileEditing")]
        bool AllowsCellInteractionsWhileEditing { get; set; }

        [Export("editing")]
        bool Editing { [Bind("isEditing")] get; set; }

        [Export("editingSelectorColor", ArgumentSemantic.Strong)]
        UIColor EditingSelectorColor { get; set; }

        [NullAllowed]
        [Export("inkView", ArgumentSemantic.Strong)]
        InkView InkView { get; set; }

        [Field("kSelectedCellAccessibilityHintKey", "__Internal")]
        NSString SelectedCellAccessibilityHintKey { get; }

        [Field("kDeselectedCellAccessibilityHintKey", "__Internal")]
        NSString DeselectedCellAccessibilityHintKey { get; }

        [Field("MDCCollectionViewCellStyleCardSectionInset", "__Internal")]
        nfloat StyleCardSectionInset { get; }

        [Export("setEditing:animated:")]
        void SetEditing(bool editing, bool animated);
    }

    [BaseType(typeof(CollectionViewCell),
        Name = "MDCCollectionViewTextCell")]
    interface CollectionViewTextCell
    {
        [NullAllowed]
        [Export("textLabel", ArgumentSemantic.Strong)]
        UILabel TextLabel { get; }

        [NullAllowed]
        [Export("detailTextLabel", ArgumentSemantic.Strong)]
        UILabel DetailTextLabel { get; }

        [NullAllowed]
        [Export("imageView", ArgumentSemantic.Strong)]
        UIImageView ImageView { get; }

        [Field("MDCCellDefaultOneLineHeight", "__Internal")]
        nfloat DefaultOneLineHeight { get; }

        [Field("MDCCellDefaultOneLineWithAvatarHeight", "__Internal")]
        nfloat DefaultOneLineWithAvatarHeight { get; }

        [Field("MDCCellDefaultTwoLineHeight", "__Internal")]
        nfloat DefaultTwoLineHeight { get; }

        [Field("MDCCellDefaultThreeLineHeight", "__Internal")]
        nfloat DefaultThreeLineHeight { get; }
    }

    [BaseType(typeof(UICollectionViewLayoutAttributes),
        Name = "MDCCollectionViewLayoutAttributes")]
    interface CollectionViewLayoutAttributes : INSCopying

    {
        [Export("editing")]
        bool Editing { [Bind("isEditing")] get; set; }

        [Export("shouldShowReorderStateMask")]
        bool ShouldShowReorderStateMask { get; set; }

        [Export("shouldShowSelectorStateMask")]
        bool ShouldShowSelectorStateMask { get; set; }

        [Export("shouldShowGridBackground")]
        bool ShouldShowGridBackground { get; set; }

        [NullAllowed]
        [Export("backgroundImage", ArgumentSemantic.Strong)]
        UIImage BackgroundImage { get; set; }

        [Export("backgroundImageViewInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets BackgroundImageViewInsets { get; set; }

        [Export("isGridLayout")]
        bool IsGridLayout { get; set; }

        [Export("sectionOrdinalPosition", ArgumentSemantic.Assign)]
        CollectionViewOrdinalPosition SectionOrdinalPosition { get; set; }

        [NullAllowed]
        [Export("separatorColor", ArgumentSemantic.Strong)]
        UIColor SeparatorColor { get; set; }

        [Export("separatorInset", ArgumentSemantic.Assign)]
        UIEdgeInsets SeparatorInset { get; set; }

        [Export("separatorLineHeight")]
        nfloat SeparatorLineHeight { get; set; }

        [Export("shouldHideSeparators")]
        bool ShouldHideSeparators { get; set; }

        [Export("willAnimateCellsOnAppearance")]
        bool WillAnimateCellsOnAppearance { get; set; }

        [Export("animateCellsOnAppearanceDuration")]
        double AnimateCellsOnAppearanceDuration { get; set; }

        [Export("animateCellsOnAppearanceDelay")]
        double AnimateCellsOnAppearanceDelay { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCCollectionViewEditingDelegate")]
    interface CollectionViewEditingDelegate
    {
        [Export("collectionViewAllowsEditing:")]
        bool AllowsEditing(UICollectionView collectionView);

        [Export("collectionViewWillBeginEditing:")]
        void WillBeginEditing(UICollectionView collectionView);

        [Export("collectionViewDidBeginEditing:")]
        void EditingStarted(UICollectionView collectionView);

        [Export("collectionViewWillEndEditing:")]
        void WillEndEditing(UICollectionView collectionView);

        [Export("collectionViewDidEndEditing:")]
        void EditingEnded(UICollectionView collectionView);

        [Export("collectionView:canEditItemAtIndexPath:")]
        bool CanEditItem(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:canSelectItemDuringEditingAtIndexPath:")]
        bool CanSelectItemDuringEditing(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionViewAllowsReordering:")]
        bool AllowsReordering(UICollectionView collectionView);

        [Export("collectionView:canMoveItemAtIndexPath:")]
        bool CanMoveItem(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:canMoveItemAtIndexPath:toIndexPath:")]
        bool CanMoveItem(UICollectionView collectionView, NSIndexPath indexPath, NSIndexPath newIndexPath);

        [Export("collectionView:willMoveItemAtIndexPath:toIndexPath:")]
        void WillMoveItem(UICollectionView collectionView, NSIndexPath indexPath, NSIndexPath newIndexPath);

        [Export("collectionView:didMoveItemAtIndexPath:toIndexPath:")]
        void ItemMoved(UICollectionView collectionView, NSIndexPath indexPath, NSIndexPath newIndexPath);

        [Export("collectionView:willBeginDraggingItemAtIndexPath:")]
        void WillBeginDraggingItem(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:didEndDraggingItemAtIndexPath:")]
        void DraggingItemEnded(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:willDeleteItemsAtIndexPaths:")]
        void WillDeleteItems(UICollectionView collectionView, NSIndexPath indexPaths);

        [Export("collectionView:didDeleteItemsAtIndexPaths:")]
        void ItemsDeleted(UICollectionView collectionView, NSIndexPath indexPaths);

        [Export("collectionView:willDeleteSections:")]
        void WillDeleteSections(UICollectionView collectionView, NSIndexSet sections);

        [Export("collectionView:didDeleteSections:")]
        void SectionsDeleted(UICollectionView collectionView, NSIndexSet sections);

        [Export("collectionViewAllowsSwipeToDismissItem:")]
        bool AllowsSwipeToDismissItem(UICollectionView collectionView);

        [Export("collectionView:canSwipeToDismissItemAtIndexPath:")]
        bool CanSwipeToDismissItem(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:canSwipeInDirection:toDismissItemAtIndexPath:")]
        bool CollectionView(UICollectionView collectionView, UISwipeGestureRecognizerDirection swipeDirection, NSIndexPath indexPath);

        [Export("collectionView:willBeginSwipeToDismissItemAtIndexPath:")]
        void WillBeginSwipeToDismissItem(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:didEndSwipeToDismissItemAtIndexPath:")]
        void SwipeToDismissItemEnded(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:didCancelSwipeToDismissItemAtIndexPath:")]
        void SwipeToDismissItemCanceled(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionViewAllowsSwipeToDismissSection:")]
        bool AllowsSwipeToDismissSection(UICollectionView collectionView);

        [Export("collectionView:canSwipeToDismissSection:")]
        bool CanSwipeToDismissSection(UICollectionView collectionView, nint section);

        [Export("collectionView:willBeginSwipeToDismissSection:")]
        void WillBeginSwipeToDismissSection(UICollectionView collectionView, nint section);

        [Export("collectionView:didEndSwipeToDismissSection:")]
        void SwipeToDismissSectionEnded(UICollectionView collectionView, nint section);

        [Export("collectionView:didCancelSwipeToDismissSection:")]
        void SwipeToDismissSectionCanceled(UICollectionView collectionView, nint section);
    }

    [Protocol(Name = "MDCCollectionViewStyling")]
    [BaseType(typeof(NSObject))]
    interface CollectionViewStyling
    {
        [Abstract]
        [NullAllowed]
        [Export("collectionView", ArgumentSemantic.Weak)]
        UICollectionView CollectionView { get; }

        [Abstract]
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ICollectionViewStylingDelegate Delegate { get; set; }

        [Abstract]
        [Export("shouldInvalidateLayout")]
        bool ShouldInvalidateLayout { get; set; }

        [Abstract]
        [Export("cellBackgroundColor", ArgumentSemantic.Strong)]
        UIColor CellBackgroundColor { get; set; }

        [Abstract]
        [Export("cellLayoutType", ArgumentSemantic.Assign)]
        CollectionViewCellLayoutType CellLayoutType { get; set; }

        [Abstract]
        [Export("gridColumnCount")]
        nint GridColumnCount { get; set; }

        [Abstract]
        [Export("gridPadding")]
        nfloat GridPadding { get; set; }

        [Abstract]
        [Export("cellStyle", ArgumentSemantic.Assign)]
        CollectionViewCellStyle CellStyle { get; set; }

        [Abstract]
        [Export("cardBorderRadius")]
        nfloat CardBorderRadius { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("separatorColor", ArgumentSemantic.Strong)]
        UIColor SeparatorColor { get; set; }

        [Abstract]
        [Export("separatorInset", ArgumentSemantic.Assign)]
        UIEdgeInsets SeparatorInset { get; set; }

        [Abstract]
        [Export("separatorLineHeight")]
        nfloat SeparatorLineHeight { get; set; }

        [Abstract]
        [Export("shouldHideSeparators")]
        bool ShouldHideSeparators { get; set; }

        [Abstract]
        [Export("allowsItemInlay")]
        bool AllowsItemInlay { get; set; }

        [Abstract]
        [Export("allowsMultipleItemInlays")]
        bool AllowsMultipleItemInlays { get; set; }

        [Abstract]
        [NullAllowed]

        [Export("indexPathsForInlaidItems")]
        NSIndexPath[] IndexPathsForInlaidItems { get; }

        [Abstract]
        [Export("shouldAnimateCellsOnAppearance")]
        bool ShouldAnimateCellsOnAppearance { get; set; }

        [Abstract]
        [Export("willAnimateCellsOnAppearance")]
        bool WillAnimateCellsOnAppearance { get; }

        [Abstract]
        [Export("animateCellsOnAppearancePadding")]
        nfloat AnimateCellsOnAppearancePadding { get; }

        [Abstract]
        [Export("animateCellsOnAppearanceDuration")]
        double AnimateCellsOnAppearanceDuration { get; }

        [Abstract]
        [Export("setCellStyle:animated:")]
        void SetCellStyle(CollectionViewCellStyle cellStyle, bool animated);

        [Abstract]
        [Export("cellStyleAtSectionIndex:")]
        CollectionViewCellStyle GetCellStyle(nint section);

        [Abstract]
        [Export("backgroundImageViewOutsetsForCellWithAttribute:")]
        UIEdgeInsets GetBackgroundImageViewOutsetsForCell(CollectionViewLayoutAttributes attr);

        [return: NullAllowed]
        [Abstract]
        [Export("backgroundImageForCellLayoutAttributes:")]
        UIImage GetBackgroundImageForCell(CollectionViewLayoutAttributes attr);

        [Abstract]
        [Export("shouldHideSeparatorForCellLayoutAttributes:")]
        bool ShouldHideSeparatorForCellLayoutAttributes(CollectionViewLayoutAttributes attr);

        [Abstract]
        [Export("isItemInlaidAtIndexPath:")]
        bool IsItemInlaid(NSIndexPath indexPath);

        [Abstract]
        [Export("applyInlayToItemAtIndexPath:animated:")]
        void ApplyInlayToItem(NSIndexPath indexPath, bool animated);

        [Abstract]
        [Export("removeInlayFromItemAtIndexPath:animated:")]
        void RemoveInlayFromItem(NSIndexPath indexPath, bool animated);

        [Abstract]
        [Export("applyInlayToAllItemsAnimated:")]
        void ApplyInlayToAllItems(bool animated);

        [Abstract]
        [Export("removeInlayFromAllItemsAnimated:")]
        void RemoveInlayFromAllItems(bool animated);

        [Abstract]
        [Export("resetIndexPathsForInlaidItems")]
        void ResetIndexPathsForInlaidItems();

        [Abstract]
        [Export("beginCellAppearanceAnimation")]
        void BeginCellAppearanceAnimation();
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCCollectionViewStylingDelegate")]
    interface CollectionViewStylingDelegate
    {
        [Export("collectionView:cellHeightAtIndexPath:")]
        nfloat GetCellHeight(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:cellStyleForSection:")]
        CollectionViewCellStyle CellStyleForSection(UICollectionView collectionView, nint section);

        [return: NullAllowed]
        [Export("collectionView:cellBackgroundColorAtIndexPath:")]
        UIColor GetCellBackgroundColor(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:shouldHideItemBackgroundAtIndexPath:")]
        bool ShouldHideItemBackground(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:shouldHideHeaderBackgroundForSection:")]
        bool ShouldHideHeaderBackground(UICollectionView collectionView, nint section);

        [Export("collectionView:shouldHideFooterBackgroundForSection:")]
        bool ShouldHideFooterBackground(UICollectionView collectionView, nint section);

        [Export("collectionView:shouldHideItemSeparatorAtIndexPath:")]
        bool ShouldHideItemSeparator(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:shouldHideHeaderSeparatorForSection:")]
        bool ShouldHideHeaderSeparator(UICollectionView collectionView, nint section);

        [Export("collectionView:shouldHideFooterSeparatorForSection:")]
        bool ShouldHideFooterSeparator(UICollectionView collectionView, nint section);

        [Export("collectionView:didApplyInlayToItemAtIndexPaths:")]
        void InlayToItemApplied(UICollectionView collectionView, NSIndexPath indexPaths);

        [Export("collectionView:didRemoveInlayFromItemAtIndexPaths:")]
        void InlayFromItemRemoved(UICollectionView collectionView, NSIndexPath indexPaths);

        [Export("collectionView:hidesInkViewAtIndexPath:")]
        bool HidesInkView(UICollectionView collectionView, NSIndexPath indexPath);

        [return: NullAllowed]
        [Export("collectionView:inkColorAtIndexPath:")]
        UIColor GetInkColor(UICollectionView collectionView, NSIndexPath indexPath);

        [Export("collectionView:inkTouchController:inkViewAtIndexPath:")]
        InkView GetInkView(UICollectionView collectionView, InkTouchController inkTouchController, NSIndexPath indexPath);
    }

    [BaseType(typeof(UICollectionViewController),
        Name = "MDCCollectionViewController")]
    interface CollectionViewController : CollectionViewEditingDelegate, CollectionViewStylingDelegate, IUICollectionViewDelegateFlowLayout

    {
        [Export("styler", ArgumentSemantic.Strong)]
        ICollectionViewStyling Styler { get; }

        [Export("editor", ArgumentSemantic.Strong)]
        ICollectionViewEditing Editor { get; }

        [New]
        [RequiresSuper]
        [Export("collectionView:didHighlightItemAtIndexPath:")]
        void ItemHighlighted(UICollectionView collectionView, NSIndexPath indexPath);

        [New]
        [RequiresSuper]
        [Export("collectionView:didUnhighlightItemAtIndexPath:")]
        void ItemUnhighlighted(UICollectionView collectionView, NSIndexPath indexPath);

        [New]
        [RequiresSuper]
        [Export("collectionView:didSelectItemAtIndexPath:")]
        void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath);

        [New]
        [RequiresSuper]
        [Export("collectionView:didDeselectItemAtIndexPath:")]
        void ItemDeselected(UICollectionView collectionView, NSIndexPath indexPath);

        [RequiresSuper]
        [Export("collectionViewWillBeginEditing:")]
        void WillBeginEditing(UICollectionView collectionView);

        [RequiresSuper]
        [Export("collectionViewWillEndEditing:")]
        void WillEndEditing(UICollectionView collectionView);

        [Export("cellWidthAtSectionIndex:")]
        nfloat GetCellWidth(nint section);
    }

    [Protocol(Name = "MDCCollectionViewEditing")]
    [BaseType(typeof(NSObject))]
    interface CollectionViewEditing
    {
        [Abstract]
        [NullAllowed]
        [Export("collectionView", ArgumentSemantic.Weak)]
        UICollectionView CollectionView { get; }

        [Abstract]
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ICollectionViewEditingDelegate Delegate { get; set; }

        [Abstract]
        [NullAllowed]
        [Export("reorderingCellIndexPath", ArgumentSemantic.Strong)]
        NSIndexPath ReorderingCellIndexPath { get; }

        [Abstract]
        [NullAllowed]
        [Export("dismissingCellIndexPath", ArgumentSemantic.Strong)]
        NSIndexPath DismissingCellIndexPath { get; }

        [Abstract]
        [Export("dismissingSection")]
        nint DismissingSection { get; }

        [Abstract]
        [Export("minimumPressDuration")]
        double MinimumPressDuration { get; set; }

        [Abstract]
        [Export("editing")]
        bool Editing { [Bind("isEditing")] get; set; }

        [Abstract]
        [Export("setEditing:animated:")]
        void SetEditing(bool editing, bool animated);

        [Abstract]
        [Export("updateReorderCellPosition")]
        void UpdateReorderCellPosition();
    }

    [BaseType(typeof(UICollectionViewFlowLayout),
        Name = "MDCCollectionViewFlowLayout")]
    interface CollectionViewFlowLayout
    { }

    [BaseType(typeof(UIViewController),
        Name = "MDCAlertController")]
    interface AlertController : IElevatable, IElevationOverriding

    {
        [NullAllowed]
        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [NullAllowed]
        [Export("titleColor", ArgumentSemantic.Strong)]
        UIColor TitleColor { get; set; }

        [Export("titleAlignment", ArgumentSemantic.Assign)]
        NSTextAlignment TitleAlignment { get; set; }

        [NullAllowed]
        [Export("titleIcon", ArgumentSemantic.Strong)]
        UIImage TitleIcon { get; set; }

        [NullAllowed]
        [Export("titleIconTintColor", ArgumentSemantic.Strong)]
        UIColor TitleIconTintColor { get; set; }

        [NullAllowed]
        [Export("messageFont", ArgumentSemantic.Strong)]
        UIFont MessageFont { get; set; }

        [NullAllowed]
        [Export("messageColor", ArgumentSemantic.Strong)]
        UIColor MessageColor { get; set; }

        [NullAllowed]
        [Export("buttonFont", ArgumentSemantic.Strong)]
        UIFont ButtonFont { get; set; }

        [NullAllowed]
        [Export("buttonTitleColor", ArgumentSemantic.Strong)]
        UIColor ButtonTitleColor { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [NullAllowed]
        [Export("backgroundColor", ArgumentSemantic.Strong)]
        UIColor BackgroundColor { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("elevation")]
        nfloat Elevation { get; set; }

        [Export("shadowColor", ArgumentSemantic.Copy)]
        UIColor ShadowColor { get; set; }

        [New]
        [NullAllowed]
        [Export("title")]
        string Title { get; set; }

        [NullAllowed]
        [Export("message")]
        string Message { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<AlertController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [Export("actions")]
        AlertAction[] Actions { get; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [Static]
        [Export("alertControllerWithTitle:message:")]
        AlertController Create([NullAllowed] string title, [NullAllowed]  string message);

        [Export("addAction:")]
        void AddAction(AlertAction action);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAlertAction")]
    interface AlertAction : INSCopying, IUIAccessibilityIdentification

    {
        [NullAllowed]
        [Export("title")]
        string Title { get; }

        [Export("emphasis")]
        ActionEmphasis Emphasis { get; }

        [NullAllowed]
        [Export("accessibilityIdentifier")]
        string AccessibilityIdentifier { get; set; }

        [Static]
        [Export("actionWithTitle:handler:")]
        AlertAction Create(string title, [NullAllowed]  ActionHandler handler);

        [Static]
        [Export("actionWithTitle:emphasis:handler:")]
        AlertAction Create(string title, ActionEmphasis emphasis, [NullAllowed]  ActionHandler handler);
    }

    [BaseType(typeof(UIView),
        Name = "MDCAlertControllerView")]
    interface AlertControllerView
    {
        [NullAllowed]
        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [NullAllowed]
        [Export("titleColor", ArgumentSemantic.Strong)]
        UIColor TitleColor { get; set; }

        [Export("titleAlignment", ArgumentSemantic.Assign)]
        NSTextAlignment TitleAlignment { get; set; }

        [NullAllowed]
        [Export("titleIcon", ArgumentSemantic.Strong)]
        UIImage TitleIcon { get; set; }

        [NullAllowed]
        [Export("titleIconTintColor", ArgumentSemantic.Strong)]
        UIColor TitleIconTintColor { get; set; }

        [NullAllowed]
        [Export("messageFont", ArgumentSemantic.Strong)]
        UIFont MessageFont { get; set; }

        [NullAllowed]
        [Export("messageColor", ArgumentSemantic.Strong)]
        UIColor MessageColor { get; set; }

        [NullAllowed]
        [Export("buttonFont", ArgumentSemantic.Strong)]
        UIFont ButtonFont { get; set; }

        [NullAllowed]
        [Export("buttonColor", ArgumentSemantic.Strong)]
        UIColor ButtonColor { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [return: NullAllowed]
        [Export("buttonForAction:")]
        Button GetButton(AlertAction action);
    }

    [Protocol(Name = "MDCDialogPresentationControllerDelegate")]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject))]
    interface DialogPresentationControllerDelegate
    {
        [Export("dialogPresentationControllerDidDismiss:")]
        void DialogPresentationControllerDidDismiss(DialogPresentationController dialogPresentationController);
    }

    [BaseType(typeof(UIPresentationController),
        Name = "MDCDialogPresentationController")]
    interface DialogPresentationController
    {
        [Wrap("WeakDialogPresentationControllerDelegate")]
        [NullAllowed]
        IDialogPresentationControllerDelegate DialogPresentationControllerDelegate { get; set; }

        [NullAllowed]
        [Export("dialogPresentationControllerDelegate", ArgumentSemantic.Weak)]
        NSObject WeakDialogPresentationControllerDelegate { get; set; }

        [Export("dismissOnBackgroundTap")]
        bool DismissOnBackgroundTap { get; set; }

        [Export("dialogCornerRadius")]
        nfloat DialogCornerRadius { get; set; }

        [Export("dialogElevation")]
        nfloat DialogElevation { get; set; }

        [Export("dialogShadowColor", ArgumentSemantic.Copy)]
        UIColor DialogShadowColor { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<DialogPresentationController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [New]
        [Export("sizeForChildContentContainer:withParentContainerSize:")]
        CGSize GetSizeForChildContentContainer(IUIContentContainer container, CGSize parentSize);

        [Export("frameOfPresentedViewInContainerView")]
        CGRect GetFrameOfPresentedViewInContainerView();
    }

    [BaseType(typeof(NSObject),
        Name = "MDCDialogTransitionController")]
    interface DialogTransitionController : IUIViewControllerAnimatedTransitioning, IUIViewControllerTransitioningDelegate
    { }

    [Category]
    [BaseType(typeof(UIViewController))]
    interface UIViewController_MaterialDialogs
    {
        [return: NullAllowed]
        [Export("mdc_dialogPresentationController")]
        DialogPresentationController GetDialogPresentationController();

        [return: NullAllowed]
        [Wrap("GetDialogPresentationController(This)")]
        DialogPresentationController MdcGetDialogPresentationController();
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAlertColorThemer")]
    interface AlertColorThemer
    {
        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:")]
        void ApplyColorScheme(IColorScheme colorScheme);

        [Static]
        [Export("applySemanticColorScheme:toAlertController:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, AlertController alertController);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCAlertScheming")]
    interface AlertScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("typographyScheme")]
        ITypographyScheming TypographyScheme { get; }

        [Abstract]
        [Export("buttonScheme")]
        IButtonScheming ButtonScheme { get; }

        [Abstract]
        [Export("cornerRadius")]
        nfloat CornerRadius { get; }

        [Abstract]
        [Export("elevation")]
        nfloat Elevation { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCAlertScheme")]
    interface AlertScheme : AlertScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        IColorScheming ColorScheme { get; set; }

        [Export("typographyScheme", ArgumentSemantic.Assign)]
        ITypographyScheming TypographyScheme { get; set; }

        [Export("buttonScheme", ArgumentSemantic.Assign)]
        IButtonScheming ButtonScheme { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("elevation")]
        nfloat Elevation { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAlertControllerThemer")]
    interface AlertControllerThemer
    {
        [Static]
        [Export("applyScheme:toAlertController:")]
        void ApplyScheme(IAlertScheming alertScheme, AlertController alertController);
    }

    [Category]
    [BaseType(typeof(AlertController),
        Name = "MDCAlertController_MaterialTheming")]
    interface AlertController_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [Category]
    [BaseType(typeof(DialogPresentationController),
        Name = "MDCDialogPresentationController_MaterialTheming")]
    interface DialogPresentationController_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCAlertTypographyThemer")]
    interface AlertTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toAlertController:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, AlertController alertController);
    }

    [BaseType(typeof(UIView),
        Name = "MDCFeatureHighlightView")]
    interface FeatureHighlightView
    {
        [NullAllowed]
        [Export("innerHighlightColor", ArgumentSemantic.Strong)]
        UIColor InnerHighlightColor { get; set; }

        [NullAllowed]
        [Export("outerHighlightColor", ArgumentSemantic.Strong)]
        UIColor OuterHighlightColor { get; set; }

        [NullAllowed]
        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [NullAllowed]
        [Export("titleColor", ArgumentSemantic.Strong)]
        UIColor TitleColor { get; set; }

        [NullAllowed]
        [Export("bodyFont", ArgumentSemantic.Strong)]
        UIFont BodyFont { get; set; }

        [NullAllowed]
        [Export("bodyColor", ArgumentSemantic.Strong)]
        UIColor BodyColor { get; set; }

        [Export("mdc_legacyFontScaling")]
        bool LegacyFontScaling { get; [Bind("mdc_setLegacyFontScaling:")]set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<FeatureHighlightView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(UIViewController),
        Name = "MDCFeatureHighlightViewController")]
    interface FeatureHighlightViewController
    {
        [NullAllowed]
        [Export("titleText")]
        string TitleText { get; set; }

        [NullAllowed]
        [Export("bodyText")]
        string BodyText { get; set; }

        [Export("outerHighlightColor", ArgumentSemantic.Strong)]
        UIColor OuterHighlightColor { get; set; }

        [Export("innerHighlightColor", ArgumentSemantic.Strong)]
        UIColor InnerHighlightColor { get; set; }

        [NullAllowed]
        [Export("titleColor", ArgumentSemantic.Strong)]
        UIColor TitleColor { get; set; }

        [NullAllowed]
        [Export("bodyColor", ArgumentSemantic.Strong)]
        UIColor BodyColor { get; set; }

        [NullAllowed]
        [Export("titleFont", ArgumentSemantic.Strong)]
        UIFont TitleFont { get; set; }

        [NullAllowed]
        [Export("bodyFont", ArgumentSemantic.Strong)]
        UIFont BodyFont { get; set; }

        [Export("mdc_legacyFontScaling")]
        bool LegacyFontScaling { get; [Bind("mdc_setLegacyFontScaling:")]set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<FeatureHighlightViewController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Field("kMDCFeatureHighlightOuterHighlightAlpha", "__Internal")]
        nfloat FeatureHighlightOuterHighlightAlpha { get; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [DesignatedInitializer]
        [Export("initWithHighlightedView:andShowView:completion:")]
        IntPtr Constructor(UIView highlightedView, UIView displayedView, [NullAllowed]  FeatureHighlightCompletionHandler completion);

        [Export("initWithHighlightedView:completion:")]
        IntPtr Constructor(UIView highlightedView, [NullAllowed]  FeatureHighlightCompletionHandler completion);

        [Export("acceptFeature")]
        void AcceptFeature();

        [Export("rejectFeature")]
        void RejectFeature();
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFeatureHighlightColorThemer")]
    interface FeatureHighlightColorThemer
    {
        [Obsolete(" This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toFeatureHighlightView:")]
        void ApplyColorScheme(IColorScheme colorScheme, FeatureHighlightView featureHighlightView);

        [Obsolete(" This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toFeatureHighlightViewController:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, FeatureHighlightViewController featureHighlightViewController);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFeatureHighlightAccessibilityMutator")]
    interface FeatureHighlightAccessibilityMutator
    {
        [Static]
        [Export("mutate:")]
        void Mutate(FeatureHighlightViewController featureHighlightViewController);
    }

    [Obsolete("This class will soon be deprecated. Please consider using FeatureHighlightTypographyThemer class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFeatureHighlightFontThemer")]
    interface FeatureHighlightFontThemer
    {
        [Obsolete(" This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toFeatureHighlightView:")]
        void ApplyFontScheme(IFontScheme fontScheme, FeatureHighlightView featureHighlightView);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFeatureHighlightTypographyThemer")]
    interface FeatureHighlightTypographyThemer
    {
        [Obsolete(" This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toFeatureHighlightViewController:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, FeatureHighlightViewController featureHighlightViewController);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFlexibleHeaderColorThemer")]
    interface FlexibleHeaderColorThemer
    {
        [Obsolete(" This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toFlexibleHeaderView:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, FlexibleHeaderView flexibleHeaderView);

        [Obsolete(" This method will soon be deprecated.")]
        [Static]
        [Export("applySurfaceVariantWithColorScheme:toFlexibleHeaderView:")]
        void ApplySurfaceVariant (IColorScheming colorScheme, FlexibleHeaderView flexibleHeaderView);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toFlexibleHeaderView:")]
        void ApplyColorScheme (IColorScheme colorScheme, FlexibleHeaderView flexibleHeaderView);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toMDCFlexibleHeaderController:")]
        void ApplyColorScheme(IColorScheme colorScheme, FlexibleHeaderViewController flexibleHeaderController);
    }

    [Obsolete("This class will soon be deprecated. There will be no replacement API. Consider theming your flexible header view or app bar instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCHeaderStackViewColorThemer")]
    interface HeaderStackViewColorThemer
    {
        [Static]
        [Export("applyColorScheme:toHeaderStackView:")]
        void ApplyColorScheme(IColorScheme colorScheme, HeaderStackView headerStackView);
    }

    [Obsolete("This class will soon be deprecated. There is no direct replacement. Ink color needs to be set by the owning component in a context - specific manner.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCInkColorThemer")]
    interface InkColorThemer
    {
        [Static]
        [Export("applyColorScheme:toInkView:")]
        void ApplyColorScheme(IColorScheme colorScheme, InkView inkView);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCLibraryInfo")]
    interface LibraryInfo
    {
        [Static]
        [Export("versionString")]
        string VersionString { get; }
    }

    [BaseType(typeof(UICollectionViewCell),
        Name = "MDCBaseCell")]
    interface BaseCell : IElevatable, IElevationOverriding

    {
        [Export("elevation")]
        nfloat Elevation { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BaseCell, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Obsolete(" This property will soon be deprecated.")]
        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }
    }

    [BaseType(typeof(BaseCell),
        Name = "MDCSelfSizingStereoCell")]
    interface SelfSizingStereoCell
    {
        [Export("leadingImageView", ArgumentSemantic.Strong)]
        UIImageView LeadingImageView { get; }

        [Export("trailingImageView", ArgumentSemantic.Strong)]
        UIImageView TrailingImageView { get; }

        [Export("titleLabel", ArgumentSemantic.Strong)]
        UILabel TitleLabel { get; }

        [Export("detailLabel", ArgumentSemantic.Strong)]
        UILabel DetailLabel { get; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCListColorThemer")]
    interface ListColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toSelfSizingStereoCell:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, SelfSizingStereoCell cell);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toBaseCell:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, BaseCell cell);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCListScheming")]
    interface ListScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("typographyScheme")]
        ITypographyScheming TypographyScheme { get; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCListScheme")]
    interface ListScheme : ListScheming

    {
        [Export("colorScheme", ArgumentSemantic.Assign)]
        IColorScheming ColorScheme { get; set; }

        [Export("typographyScheme", ArgumentSemantic.Assign)]
        ITypographyScheming TypographyScheme { get; set; }
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCListThemer")]
    interface ListThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyScheme:toSelfSizingStereoCell:")]
        void ApplyScheme (IListScheming scheme, SelfSizingStereoCell cell);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyScheme:toBaseCell:")]
        void ApplyScheme (IListScheming scheme, BaseCell cell);
    }

    [Category]
    [BaseType(typeof(BaseCell),
        Name = "MDCBaseCell_MaterialTheming")]
    interface BaseCell_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [Category]
    [BaseType(typeof(SelfSizingStereoCell),
        Name = "MDCSelfSizingStereoCell_MaterialTheming")]
    interface SelfSizingStereoCell_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCListTypographyThemer")]
    interface ListTypographyThemer
    {
        [Obsolete("This property has been deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toSelfSizingStereoCell:")]
        void ApplyTypographyScheme (ITypographyScheming typographyScheme, SelfSizingStereoCell cell);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCMaskedTransitionController")]
    interface MaskedTransitionController : IUIViewControllerTransitioningDelegate

    {
        [NullAllowed]
        [Export("sourceView", ArgumentSemantic.Strong)]
        UIView SourceView { get; }

        [NullAllowed]
        [Export("calculateFrameOfPresentedView", ArgumentSemantic.Copy)]
        Func<UIPresentationController, CGRect> CalculateFrameOfPresentedView { get; set; }

        [Export("initWithSourceView:")]
        IntPtr Constructor([NullAllowed] UIView sourceView);

        [DesignatedInitializer]
        [Export("init")]
        IntPtr Constructor();
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCNavigationBarColorThemer")]
    interface NavigationBarColorThemer
    {
        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme instead.")]
        [Static]
        [Export("applyColorScheme:toNavigationBar:")]
        void ApplyColorScheme (IColorScheme colorScheme, NavigationBar navigationBar);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toNavigationBar:")]
        void ApplySemanticColorScheme (IColorScheming colorScheme, NavigationBar navigationBar);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySurfaceVariantWithColorScheme:toNavigationBar:")]
        void ApplySurfaceVariant (IColorScheming colorScheme, NavigationBar navigationBar);

    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCNavigationBarTypographyThemer")]
    interface NavigationBarTypographyThemer
    {
        [Static]
        [Export("applyTypographyScheme:toNavigationBar:")]
        void ApplyTypographyScheme (ITypographyScheming typographyScheme, NavigationBar navigationBar);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomDrawerHeader")]
    interface BottomDrawerHeader
    {
        [Export("updateDrawerHeaderTransitionRatio:")]
        void UpdateDrawerHeaderTransitionRatio(nfloat transitionToTopRatio);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomDrawerPresentationControllerDelegate")]
    interface BottomDrawerPresentationControllerDelegate : IUIAdaptivePresentationControllerDelegate

    {
        [EventArgs("BottomDrawerPresentationControllerWillChangeState")]
        [Abstract]
        [Export("bottomDrawerWillChangeState:drawerState:")]
        void WillChangeState(BottomDrawerPresentationController presentationController, BottomDrawerState drawerState);

        [EventArgs("BottomDrawerPresentationControllerTopTransitionRatio")]
        [Abstract]
        [Export("bottomDrawerTopTransitionRatio:transitionRatio:")]
        void TopTransitionRatio(BottomDrawerPresentationController presentationController, nfloat transitionRatio);
    }

    [BaseType(typeof(UIPresentationController),
        Name = "MDCBottomDrawerPresentationController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(BottomDrawerPresentationControllerDelegate) })]
    interface BottomDrawerPresentationController
    {
        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IBottomDrawerPresentationControllerDelegate Delegate { get; set; }

        [Export("topHandleHidden")]
        bool TopHandleHidden { [Bind("isTopHandleHidden")] get; set; }

        [NullAllowed]
        [Export("topHandleColor", ArgumentSemantic.Strong)]
        UIColor TopHandleColor { get; set; }

        [Export("maximumInitialDrawerHeight")]
        nfloat MaximumInitialDrawerHeight { get; set; }

        [Export("shouldIncludeSafeAreaInContentHeight")]
        bool ShouldIncludeSafeAreaInContentHeight { get; set; }

        [Export("contentReachesFullscreen")]
        bool ContentReachesFullscreen { get; }

        [Export("drawerShadowColor", ArgumentSemantic.Strong)]
        UIColor DrawerShadowColor { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomDrawerPresentationController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setContentOffsetY:animated:")]
        void SetContentOffsetY(nfloat contentOffsetY, bool animated);

        [Export("expandToFullscreenWithDuration:completion:")]
        void ExpandToFullscreenWithDuration(nfloat duration, [NullAllowed]  Action<bool> completion);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCBottomDrawerTransitionController")]
    interface BottomDrawerTransitionController : IUIViewControllerAnimatedTransitioning, IUIViewControllerTransitioningDelegate

    {
        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }
    }

    [BaseType(typeof(UIViewController),
        Name = "MDCBottomDrawerViewController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(BottomDrawerViewControllerDelegate) })]
    interface BottomDrawerViewController : IBottomDrawerPresentationControllerDelegate

    {
        [NullAllowed]
        [Export("contentViewController", ArgumentSemantic.Assign)]
        UIViewController ContentViewController { get; set; }

        [NullAllowed]
        [Export("headerViewController", ArgumentSemantic.Assign)]
        BottomDrawerHeader HeaderViewController { get; set; }

        [NullAllowed]
        [Export("trackingScrollView", ArgumentSemantic.Weak)]
        UIScrollView TrackingScrollView { get; set; }

        [Export("drawerState")]
        BottomDrawerState DrawerState { get; }

        [NullAllowed]
        [Export("scrimColor", ArgumentSemantic.Strong)]
        UIColor ScrimColor { get; set; }

        [Export("topHandleHidden")]
        bool TopHandleHidden { [Bind("isTopHandleHidden")] get; set; }

        [NullAllowed]
        [Export("topHandleColor", ArgumentSemantic.Strong)]
        UIColor TopHandleColor { get; set; }

        [Export("maximumInitialDrawerHeight")]
        nfloat MaximumInitialDrawerHeight { get; set; }

        [Export("shouldIncludeSafeAreaInContentHeight")]
        bool ShouldIncludeSafeAreaInContentHeight { get; set; }

        [Export("drawerShadowColor", ArgumentSemantic.Strong)]
        UIColor DrawerShadowColor { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IBottomDrawerViewControllerDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<BottomDrawerViewController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setTopCornersRadius:forDrawerState:")]
        void SetTopCornersRadius(nfloat radius, BottomDrawerState drawerState);

        [Export("topCornersRadiusForDrawerState:")]
        nfloat GetTopCornersRadius(BottomDrawerState drawerState);

        [Export("setContentOffsetY:animated:")]
        void SetContentOffsetY(nfloat contentOffsetY, bool animated);

        [Export("expandToFullscreenWithDuration:completion:")]
        void ExpandToFullscreenWithDuration(nfloat duration, [NullAllowed]  Action<bool> completion);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomDrawerViewControllerDelegate")]
    interface BottomDrawerViewControllerDelegate
    {
        [EventArgs("BottomDrawerViewControllerDidChangeTopInset")]
        [EventName("TopInsetChanged")]
        [Abstract]
        [Export("bottomDrawerControllerDidChangeTopInset:topInset:")]
        void DidChangeTopInset(BottomDrawerViewController controller, nfloat topInset);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCBottomDrawerColorThemer")]
    interface BottomDrawerColorThemer
    {
        [Obsolete("The method will shortly be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toBottomDrawer:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, BottomDrawerViewController bottomDrawer);
    }

    [BaseType(typeof(UIWindow),
        Name = "MDCOverlayWindow")]
    interface OverlayWindow
    {
        [Export("activateOverlay:withLevel:")]
        void ActivateOverlay(UIView overlay, double level);

        [Export("deactivateOverlay:")]
        void DeactivateOverlay(UIView overlay);
    }

    [BaseType(typeof(UIControl),
        Name = "MDCPageControl")]
    interface PageControl : IUIScrollViewDelegate

    {
        [Export("numberOfPages")]
        nint NumberOfPages { get; set; }

        [Export("currentPage")]
        nint CurrentPage { get; set; }

        [Export("hidesForSinglePage")]
        bool HidesForSinglePage { get; set; }

        [NullAllowed]
        [Export("pageIndicatorTintColor", ArgumentSemantic.Strong)]
        UIColor PageIndicatorTintColor { get; set; }

        [NullAllowed]
        [Export("currentPageIndicatorTintColor", ArgumentSemantic.Strong)]
        UIColor CurrentPageIndicatorTintColor { get; set; }

        [Export("defersCurrentPageDisplay")]
        bool DefersCurrentPageDisplay { get; set; }

        [Export("respectsUserInterfaceLayoutDirection")]
        bool RespectsUserInterfaceLayoutDirection { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<PageControl, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setCurrentPage:animated:")]
        void SetCurrentPage(nint currentPage, bool animated);

        [Export("updateCurrentPageDisplay")]
        void UpdateCurrentPageDisplay();

        [Static]
        [Export("sizeForNumberOfPages:")]
        CGSize GetSizeForNumberOfPages(nint pageCount);

        [Export("scrollViewDidScroll:")]
        void ScrollViewDidScroll(UIScrollView scrollView);

        [Export("scrollViewDidEndDecelerating:")]
        void ScrollViewDidEndDecelerating(UIScrollView scrollView);

        [Export("scrollViewDidEndScrollingAnimation:")]
        void ScrollViewDidEndScrollingAnimation(UIScrollView scrollView);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCPageControlColorThemer")]
    interface PageControlColorThemer
    {
        [Obsolete("This method will soon be deprecated. There is no replacement yet.")]
        [Static]
        [Export("applyColorScheme:toPageControl:")]
        void ApplyColorScheme(IColorScheme colorScheme, PageControl pageControl);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCPalette")]
    interface Palette
    {
        [Static]
        [Export("redPalette", ArgumentSemantic.Strong)]
        Palette RedPalette { get; }

        [Static]
        [Export("pinkPalette", ArgumentSemantic.Strong)]
        Palette PinkPalette { get; }

        [Static]
        [Export("purplePalette", ArgumentSemantic.Strong)]
        Palette PurplePalette { get; }

        [Static]
        [Export("deepPurplePalette", ArgumentSemantic.Strong)]
        Palette DeepPurplePalette { get; }

        [Static]
        [Export("indigoPalette", ArgumentSemantic.Strong)]
        Palette IndigoPalette { get; }

        [Static]
        [Export("bluePalette", ArgumentSemantic.Strong)]
        Palette BluePalette { get; }

        [Static]
        [Export("lightBluePalette", ArgumentSemantic.Strong)]
        Palette LightBluePalette { get; }

        [Static]
        [Export("cyanPalette", ArgumentSemantic.Strong)]
        Palette CyanPalette { get; }

        [Static]
        [Export("tealPalette", ArgumentSemantic.Strong)]
        Palette TealPalette { get; }

        [Static]
        [Export("greenPalette", ArgumentSemantic.Strong)]
        Palette GreenPalette { get; }

        [Static]
        [Export("lightGreenPalette", ArgumentSemantic.Strong)]
        Palette LightGreenPalette { get; }

        [Static]
        [Export("limePalette", ArgumentSemantic.Strong)]
        Palette LimePalette { get; }

        [Static]
        [Export("yellowPalette", ArgumentSemantic.Strong)]
        Palette YellowPalette { get; }

        [Static]
        [Export("amberPalette", ArgumentSemantic.Strong)]
        Palette AmberPalette { get; }

        [Static]
        [Export("orangePalette", ArgumentSemantic.Strong)]
        Palette OrangePalette { get; }

        [Static]
        [Export("deepOrangePalette", ArgumentSemantic.Strong)]
        Palette DeepOrangePalette { get; }

        [Static]
        [Export("brownPalette", ArgumentSemantic.Strong)]
        Palette BrownPalette { get; }

        [Static]
        [Export("greyPalette", ArgumentSemantic.Strong)]
        Palette GreyPalette { get; }

        [Static]
        [Export("blueGreyPalette", ArgumentSemantic.Strong)]
        Palette BlueGreyPalette { get; }

        [Export("tint50")]
        UIColor Tint50 { get; }

        [Export("tint100")]
        UIColor Tint100 { get; }

        [Export("tint200")]
        UIColor Tint200 { get; }

        [Export("tint300")]
        UIColor Tint300 { get; }

        [Export("tint400")]
        UIColor Tint400 { get; }

        [Export("tint500")]
        UIColor Tint500 { get; }

        [Export("tint600")]
        UIColor Tint600 { get; }

        [Export("tint700")]
        UIColor Tint700 { get; }

        [Export("tint800")]
        UIColor Tint800 { get; }

        [Export("tint900")]
        UIColor Tint900 { get; }

        [NullAllowed]
        [Export("accent100")]
        UIColor Accent100 { get; }

        [NullAllowed]
        [Export("accent200")]
        UIColor Accent200 { get; }

        [NullAllowed]
        [Export("accent400")]
        UIColor Accent400 { get; }

        [NullAllowed]
        [Export("accent700")]
        UIColor Accent700 { get; }

        [Field("MDCPaletteTint50Name", "__Internal")]
        NSString Tint50Name { get; }

        [Field("MDCPaletteTint100Name", "__Internal")]
        NSString Tint100Name { get; }

        [Field("MDCPaletteTint200Name", "__Internal")]
        NSString Tint200Name { get; }

        [Field("MDCPaletteTint300Name", "__Internal")]
        NSString Tint300Name { get; }

        [Field("MDCPaletteTint400Name", "__Internal")]
        NSString Tint400Name { get; }

        [Field("MDCPaletteTint500Name", "__Internal")]
        NSString Tint500Name { get; }

        [Field("MDCPaletteTint600Name", "__Internal")]
        NSString Tint600Name { get; }

        [Field("MDCPaletteTint700Name", "__Internal")]
        NSString Tint700Name { get; }

        [Field("MDCPaletteTint800Name", "__Internal")]
        NSString Tint800Name { get; }

        [Field("MDCPaletteTint900Name", "__Internal")]
        NSString Tint900Name { get; }

        [Field("MDCPaletteAccent100Name", "__Internal")]
        NSString Accent100Name { get; }

        [Field("MDCPaletteAccent200Name", "__Internal")]
        NSString Accent200Name { get; }

        [Field("MDCPaletteAccent400Name", "__Internal")]
        NSString Accent400Name { get; }

        [Field("MDCPaletteAccent700Name", "__Internal")]
        NSString Accent700Name { get; }

        [Static]
        [Export("paletteGeneratedFromColor:")]
        Palette FromColor(UIColor target500Color);

        [Static]
        [Export("paletteWithTints:accents:")]
        Palette FromTints(NSDictionary<NSString, UIColor> tints, [NullAllowed]  NSDictionary<NSString, UIColor> accents);

        [Export("initWithTints:accents:")]
        IntPtr Constructor(NSDictionary<NSString, UIColor> tints, [NullAllowed]  NSDictionary<NSString, UIColor> accents);
    }

    [BaseType(typeof(UIView),
        Name = "MDCProgressView")]
    interface ProgressView
    {
        [Export("progressTintColor", ArgumentSemantic.Strong)]
        UIColor ProgressTintColor { get; set; }

        [Export("trackTintColor", ArgumentSemantic.Strong)]
        UIColor TrackTintColor { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("progress")]
        float Progress { get; set; }

        [Export("backwardProgressAnimationMode", ArgumentSemantic.Assign)]
        ProgressViewBackwardAnimationMode BackwardProgressAnimationMode { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<ProgressView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("setProgress:animated:completion:")]
        void SetProgress(float progress, bool animated, [NullAllowed]  Action<bool> completion);

        [Export("setHidden:animated:completion:")]
        void SetHidden(bool hidden, bool animated, [NullAllowed]  Action<bool> completion);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCProgressViewColorThemer")]
    interface ProgressViewColorThemer
    {
        [Obsolete("This method will soon be deprecated. There is no replacement yet.")]
        [Static]
        [Export("applyColorScheme:toProgressView:")]
        void ApplyColorScheme(IColorScheme colorScheme, ProgressView progressView);
    }

    [Category]
    [BaseType(typeof(ProgressView),
        Name = "MDCProgressView_MaterialTheming")]
    interface ProgressView_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [BaseType(typeof(UIControl),
        Name = "MDCSlider")]
    interface Slider : IElevatable, IElevationOverriding

    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ISliderDelegate Delegate { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("thumbRadius")]
        nfloat ThumbRadius { get; set; }

        [Export("thumbElevation")]
        nfloat ThumbElevation { get; set; }

        [Export("thumbShadowColor", ArgumentSemantic.Strong)]
        UIColor ThumbShadowColor { get; set; }

        [Export("numberOfDiscreteValues")]
        nuint NumberOfDiscreteValues { get; set; }

        [Export("value")]
        nfloat Value { get; set; }

        [Export("minimumValue")]
        nfloat MinimumValue { get; set; }

        [Export("maximumValue")]
        nfloat MaximumValue { get; set; }

        [Export("continuous")]
        bool Continuous { [Bind("isContinuous")] get; set; }

        [Export("filledTrackAnchorValue")]
        nfloat FilledTrackAnchorValue { get; set; }

        [Export("shouldDisplayDiscreteValueLabel")]
        bool ShouldDisplayDiscreteValueLabel { get; set; }

        [Export("valueLabelTextColor", ArgumentSemantic.Strong)]
        UIColor ValueLabelTextColor { get; set; }

        [Export("valueLabelBackgroundColor", ArgumentSemantic.Strong)]
        UIColor ValueLabelBackgroundColor { get; set; }

        [Export("thumbHollowAtStart")]
        bool ThumbHollowAtStart { [Bind("isThumbHollowAtStart")] get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<Slider, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Obsolete("This API is planned for deprecation. Use SetThumbColor and SetTrackBackgroundColor methods instead.")]
        [Export("disabledColor", ArgumentSemantic.Strong)]
        UIColor DisabledColor { get; set; }

        [Obsolete("This API is planned for deprecation. Use InkColor property, SetThumbColor and SetTrackFillColor methods instead.")]
        [Export("color", ArgumentSemantic.Strong)]
        UIColor Color { get; set; }

        [Obsolete("This API is planned for deprecation. Use SetTrackBackgroundColor method instead.")]
        [Export("trackBackgroundColor", ArgumentSemantic.Strong)]
        UIColor TrackBackgroundColor { get; set; }

        [Export("hapticsEnabled")]
        bool HapticsEnabled { get; set; }

        [Export("shouldEnableHapticsForAllDiscreteValues")]
        bool ShouldEnableHapticsForAllDiscreteValues { get; set; }

        [Export("statefulAPIEnabled")]
        bool StatefulApiEnabled { [Bind("isStatefulAPIEnabled")] get; set; }

        [Export("setThumbColor:forState:")]
        void SetThumbColor([NullAllowed] UIColor thumbColor, UIControlState state);

        [return: NullAllowed]
        [Export("thumbColorForState:")]
        UIColor GetThumbColor(UIControlState state);

        [Export("setTrackFillColor:forState:")]
        void SetTrackFillColor([NullAllowed] UIColor fillColor, UIControlState state);

        [return: NullAllowed]
        [Export("trackFillColorForState:")]
        UIColor GetTrackFillColor(UIControlState state);

        [Export("setTrackBackgroundColor:forState:")]
        void SetTrackBackgroundColor([NullAllowed] UIColor backgroundColor, UIControlState state);

        [return: NullAllowed]
        [Export("trackBackgroundColorForState:")]
        UIColor GetTrackBackgroundColor(UIControlState state);

        [Export("setFilledTrackTickColor:forState:")]
        void SetFilledTrackTickColor([NullAllowed] UIColor tickColor, UIControlState state);

        [return: NullAllowed]
        [Export("filledTrackTickColorForState:")]
        UIColor GetFilledTrackTickColor(UIControlState state);

        [Export("setBackgroundTrackTickColor:forState:")]
        void SetBackgroundTrackTickColor([NullAllowed] UIColor tickColor, UIControlState state);

        [return: NullAllowed]
        [Export("backgroundTrackTickColorForState:")]
        UIColor GetBackgroundTrackTickColor(UIControlState state);

        [Export("setValue:animated:")]
        void SetValue(nfloat value, bool animated);

        [Obsolete("This property will be deprecated soon.")]
        [NullAllowed]
        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCSliderDelegate")]
    interface SliderDelegate
    {
        [Export("slider:shouldJumpToValue:")]
        bool ShouldJumpToValue(Slider slider, nfloat value);

        [Export("slider:displayedStringForValue:")]
        string DisplayedStringForValue(Slider slider, nfloat value);

        [Export("slider:accessibilityLabelForValue:")]
        string AccessibilityLabelForValue(Slider slider, nfloat value);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCSliderColorThemer")]
    interface SliderColorThemer
    {
        [Obsolete("This property will soon be deprecated. Consider using SemanticColorScheme class instead.")]
        [Static]
        [Export("defaultSliderLightColorScheme")]
        BasicColorScheme DefaultSliderLightColorScheme { get; }

        [Obsolete("This property will soon be deprecated. Consider using SemanticColorScheme class instead.")]
        [Static]
        [Export("defaultSliderDarkColorScheme")]
        BasicColorScheme DefaultSliderDarkColorScheme { get; }

        [Obsolete("This property will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toSlider:")]
        void ApplyColorScheme(IColorScheme colorScheme, Slider slider);

        [Obsolete("This property will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toSlider:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, Slider slider);

    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarManagerDelegate")]
    interface SnackbarManagerDelegate
    {
        [Abstract]
        [Export("willPresentSnackbarWithMessageView:")]
        void WillPresentSnackbar([NullAllowed] SnackbarMessageView messageView);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarManager")]
    interface SnackbarManager : IElevationOverriding

    {
        [Static]
        [Export("defaultManager", ArgumentSemantic.Strong)]
        SnackbarManager DefaultManager { get; }

        [Export("alignment", ArgumentSemantic.Assign)]
        SnackbarAlignment Alignment { get; set; }


        [Export("hasMessagesShowingOrQueued")]
        bool HasMessagesShowingOrQueued { get; }

        [NullAllowed]
        [Export("snackbarMessageViewBackgroundColor", ArgumentSemantic.Strong)]
        UIColor SnackbarMessageViewBackgroundColor { get; set; }

        [NullAllowed]
        [Export("snackbarMessageViewShadowColor", ArgumentSemantic.Strong)]
        UIColor SnackbarMessageViewShadowColor { get; set; }

        [Export("messageElevation")]
        double MessageElevation { get; set; }

        [NullAllowed]
        [Export("messageTextColor", ArgumentSemantic.Strong)]
        UIColor MessageTextColor { get; set; }

        [NullAllowed]
        [Export("messageFont", ArgumentSemantic.Strong)]
        UIFont MessageFont { get; set; }

        [NullAllowed]
        [Export("buttonFont", ArgumentSemantic.Strong)]
        UIFont ButtonFont { get; set; }

        [Export("uppercaseButtonTitle")]
        bool UppercaseButtonTitle { get; set; }

        [Export("disabledButtonAlpha")]
        nfloat DisabledButtonAlpha { get; set; }

        [NullAllowed]
        [Export("buttonInkColor", ArgumentSemantic.Strong)]
        UIColor ButtonInkColor { get; set; }

        [Export("shouldApplyStyleChangesToVisibleSnackbars")]
        bool ShouldApplyStyleChangesToVisibleSnackbars { get; set; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [Export("shouldEnableAccessibilityViewIsModal")]
        bool ShouldEnableAccessibilityViewIsModal { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ISnackbarManagerDelegate Delegate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlockForMessageView", ArgumentSemantic.Copy)]
        Action<SnackbarMessageView, UITraitCollection> TraitCollectionDidChangeBlockForMessageView { get; set; }

        [NullAllowed]
        [Export("mdc_elevationDidChangeBlockForMessageView", ArgumentSemantic.Copy)]
        Action<IElevatable, nfloat> ElevationDidChangeBlockForMessageView { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [Export("showMessage:")]
        void ShowMessage([NullAllowed] SnackbarMessage message);

        [Export("setPresentationHostView:")]
        void SetPresentationHostView([NullAllowed] UIView hostView);

        [Export("dismissAndCallCompletionBlocksWithCategory:")]
        void DismissAndCallCompletionBlocks([NullAllowed] string category);

        [Export("setBottomOffset:")]
        void SetBottomOffset(nfloat offset);

        [return: NullAllowed]
        [Export("suspendMessagesWithCategory:")]
        ISnackbarSuspensionToken SuspendMessages([NullAllowed] string category);

        [Export("resumeMessagesWithToken:")]
        void ResumeMessages([NullAllowed] ISnackbarSuspensionToken token);

        [return: NullAllowed]
        [Export("buttonTitleColorForState:")]
        UIColor GetButtonTitleColor(UIControlState state);

        [Export("setButtonTitleColor:forState:")]
        void SetButtonTitleColor([NullAllowed] UIColor titleColor, UIControlState state);

        [return: NullAllowed]
        [Export("suspendAllMessages")]
        ISnackbarSuspensionToken SuspendAllMessages();
    }

    [Protocol(Name = "MDCSnackbarSuspensionToken")]
    [BaseType(typeof(NSObject))]
    interface SnackbarSuspensionToken
    { }

    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarMessage")]
    interface SnackbarMessage : INSCopying, IUIAccessibilityIdentification

    {
        [Static]
        [Export("usesLegacySnackbar")]
        bool UsesLegacySnackbar { get; set; }

        [NullAllowed]
        [Export("text")]
        string Text { get; set; }

        [NullAllowed]
        [Export("attributedText", ArgumentSemantic.Copy)]
        NSAttributedString AttributedText { get; set; }

        [NullAllowed]
        [Export("action", ArgumentSemantic.Strong)]
        SnackbarMessageAction Action { get; set; }

        [NullAllowed]
        [Export("buttonTextColor", ArgumentSemantic.Strong)]
        UIColor ButtonTextColor { get; set; }

        [Export("duration")]
        double Duration { get; set; }

        [NullAllowed]
        [Export("completionHandler", ArgumentSemantic.Copy)]
        SnackbarMessageCompletionHandler CompletionHandler { get; set; }

        [NullAllowed]
        [Export("category")]
        string Category { get; set; }

        [NullAllowed]
        [Export("accessibilityLabel")]
        string AccessibilityLabel { get; set; }

        [NullAllowed]
        [Export("accessibilityHint")]
        string AccessibilityHint { get; set; }

        [NullAllowed]
        [Export("voiceNotificationText")]
        string VoiceNotificationText { get; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Field("MDCSnackbarMessageDurationMax", "__Internal")]
        double DurationMax { get; }

        [Field("MDCSnackbarMessageBoldAttributeName", "__Internal")]
        NSString BoldAttributeName { get; }

        [Static]
        [Export("messageWithText:")]
        SnackbarMessage Create(string text);

        [Static]
        [Export("messageWithAttributedText:")]
        SnackbarMessage Create(NSAttributedString attributedText);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarMessageAction")]
    interface SnackbarMessageAction : IUIAccessibilityIdentification, INSCopying

    {
        [NullAllowed]
        [Export("title")]
        string Title { get; set; }

        [NullAllowed]
        [Export("handler", ArgumentSemantic.Copy)]
        SnackbarMessageActionHandler Handler { get; set; }
    }

    [BaseType(typeof(UIView),
        Name = "MDCSnackbarMessageView")]
    interface SnackbarMessageView : IElevatable, IElevationOverriding

    {
        [NullAllowed]
        [Export("snackbarMessageViewBackgroundColor", ArgumentSemantic.Strong)]
        UIColor SnackbarMessageViewBackgroundColor { get; set; }

        [NullAllowed]
        [Export("snackbarMessageViewShadowColor", ArgumentSemantic.Strong)]
        UIColor SnackbarMessageViewShadowColor { get; set; }

        [NullAllowed]
        [Export("messageTextColor", ArgumentSemantic.Strong)]
        UIColor MessageTextColor { get; set; }

        [NullAllowed]
        [Export("messageFont", ArgumentSemantic.Strong)]
        UIFont MessageFont { get; set; }

        [NullAllowed]
        [Export("buttonFont", ArgumentSemantic.Strong)]
        UIFont ButtonFont { get; set; }

        [NullAllowed]
        [Export("actionButtons", ArgumentSemantic.Strong)]
        NSMutableArray<Button> ActionButtons { get; set; }

        [Export("elevation")]
        double Elevation { get; set; }

        [NullAllowed]
        [Export("accessibilityLabel")]
        string AccessibilityLabel { get; set; }

        [NullAllowed]
        [Export("accessibilityHint")]
        string AccessibilityHint { get; set; }

        [Export("adjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable")]
        bool AdjustsFontForContentSizeCategoryWhenScaledFontIsUnavailable { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<SnackbarMessageView, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Export("mdc_adjustsFontForContentSizeCategory")]
        bool AdjustsFontForContentSizeCategory { get; [Bind("mdc_setAdjustsFontForContentSizeCategory:")]set; }

        [Obsolete("Use AdjustsFontForContentSizeCategory instead")]
        [Wrap("AdjustsFontForContentSizeCategory")]
        bool MdcAdjustsFontForContentSizeCategory { get; set; }

        [return: NullAllowed]
        [Export("buttonTitleColorForState:")]
        UIColor GetButtonTitleColor(UIControlState state);

        [Export("setButtonTitleColor:forState:")]
        void SetButtonTitleColor([NullAllowed] UIColor titleColor, UIControlState state);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarColorThemer")]
    interface SnackbarColorThemer
    {
        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applySemanticColorScheme:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme);

        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applySemanticColorScheme:toSnackbarManager:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, SnackbarManager snackbarManager);

        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applyColorScheme:toSnackbarMessageView:")]
        void ApplyColorScheme(IColorScheme colorScheme, SnackbarMessageView snackbarMessageView);
    }

    [Obsolete("This class will soon be deprecated. Please consider using SnackbarTypographyThemer class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarFontThemer")]
    interface SnackbarFontThemer
    {
        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applyFontScheme:toSnackbarMessageView:")]
        void ApplyFontScheme (IFontScheme fontScheme, SnackbarMessageView snackbarMessageView);

        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applyFontScheme:")]
        void ApplyFontScheme (IFontScheme fontScheme);

    }

    [Obsolete("This class will be deprecated soon.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCSnackbarTypographyThemer")]
    interface SnackbarTypographyThemer
    {
        [Obsolete("This method will be deprecated soon.")]
        [Static]
        [Export("applyTypographyScheme:")]
        void ApplyTypographyScheme (ITypographyScheming typographyScheme);
    }

    [BaseType(typeof(UIView),
        Name = "MDCTabBar",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(TabBarDelegate) })]
    interface TabBar : IUIBarPositioning, IElevatable, IElevationOverriding

    {
        [Export("items", ArgumentSemantic.Copy)]
        UITabBarItem[] Items { get; set; }

        [NullAllowed]
        [Export("selectedItem", ArgumentSemantic.Strong)]
        UITabBarItem SelectedItem { get; set; }

        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ITabBarDelegate Delegate { get; set; }

        [New]
        [Export("tintColor", ArgumentSemantic.Strong)]
        UIColor TintColor { get; set; }

        [NullAllowed]
        [Export("selectedItemTintColor", ArgumentSemantic.Assign)]
        UIColor SelectedItemTintColor { get; set; }

        [Export("unselectedItemTintColor", ArgumentSemantic.Assign)]
        UIColor UnselectedItemTintColor { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [Export("rippleColor", ArgumentSemantic.Assign)]
        UIColor RippleColor { get; set; }

        [Export("bottomDividerColor", ArgumentSemantic.Assign)]
        UIColor BottomDividerColor { get; set; }

        [Export("selectedItemTitleFont", ArgumentSemantic.Strong)]
        UIFont SelectedItemTitleFont { get; set; }

        [Export("unselectedItemTitleFont", ArgumentSemantic.Strong)]
        UIFont UnselectedItemTitleFont { get; set; }

        [NullAllowed]
        [Export("barTintColor", ArgumentSemantic.Assign)]
        UIColor BarTintColor { get; set; }

        [Export("alignment", ArgumentSemantic.Assign)]
        TabBarAlignment Alignment { get; set; }

        [Export("itemAppearance", ArgumentSemantic.Assign)]
        TabBarItemAppearance ItemAppearance { get; set; }

        [Obsolete("This property will be deprecated in a future release. Use TitleTextTransform property instead.")]
        [Export("displaysUppercaseTitles")]
        bool DisplaysUppercaseTitles { get; set; }

        [Export("titleTextTransform", ArgumentSemantic.Assign)]
        TabBarTextTransform TitleTextTransform { get; set; }

        [Export("selectionIndicatorTemplate", ArgumentSemantic.Assign)]
        TabBarIndicatorTemplate SelectionIndicatorTemplate { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<TabBar, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Static]
        [Export("defaultHeightForBarPosition:itemAppearance:")]
        nfloat GetDefaultHeight(UIBarPosition position, TabBarItemAppearance appearance);

        [Static]
        [Export("defaultHeightForItemAppearance:")]
        nfloat GetDefaultHeight(TabBarItemAppearance appearance);

        [Export("setSelectedItem:animated:")]
        void SetSelectedItem([NullAllowed] UITabBarItem selectedItem, bool animated);

        [Export("setAlignment:animated:")]
        void SetAlignment(TabBarAlignment alignment, bool animated);

        [Export("setTitleColor:forState:")]
        void SetTitleColor([NullAllowed] UIColor color, TabBarItemState state);

        [return: NullAllowed]
        [Export("titleColorForState:")]
        UIColor GetTitleColor(TabBarItemState state);

        [Export("setImageTintColor:forState:")]
        void SetImageTintColor([NullAllowed] UIColor color, TabBarItemState state);

        [return: NullAllowed]
        [Export("imageTintColorForState:")]
        UIColor GetImageTintColor(TabBarItemState state);

        [return: NullAllowed]
        [Export("accessibilityElementForItem:")]
        NSObject GetAccessibilityElement(UITabBarItem item);

        [Wrap("WeakSizeClassDelegate")]
        [NullAllowed]
        [Static]
        ITabBarSizeClassDelegate SizeClassDelegate { get; set; }

        [NullAllowed]
        [Export("sizeClassDelegate", ArgumentSemantic.Weak)]
        [Static]
        NSObject WeakSizeClassDelegate { get; set; }

        [Wrap("WeakDisplayDelegate")]
        [NullAllowed]
        [Static]
        ITabBarDisplayDelegate DisplayDelegate { get; set; }

        [NullAllowed]
        [Export("displayDelegate", ArgumentSemantic.Weak)]
        [Static]
        NSObject WeakDisplayDelegate { get; set; }

        [Obsolete("This property will be deprecated soon.")]
        [Export("inkColor", ArgumentSemantic.Assign)]
        UIColor InkColor { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCTabBarDelegate")]
    interface TabBarDelegate : IUIBarPositioningDelegate

    {
        [DefaultValue(true)]
        [DelegateName("TabBarShouldSelectItem")]
        [Export("tabBar:shouldSelectItem:")]
        bool ShouldSelectItem(TabBar tabBar, UITabBarItem item);

        [EventArgs("TabBarWillSelectItem")]
        [Obsolete("Will be deprecated. Use ShouldSelectItem method instead.")]
        [Export("tabBar:willSelectItem:")]
        void WillSelectItem(TabBar tabBar, UITabBarItem item);

        [EventArgs("TabBarItemSelected")]
        [EventName("ItemSelected")]
        [Export("tabBar:didSelectItem:")]
        void DidSelectItem(TabBar tabBar, UITabBarItem item);

        [Export("inkColor", ArgumentSemantic.Assign)]
        [Static]
        UIColor InkColor { get; set; }
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTabBarIndicatorAttributes")]
    interface TabBarIndicatorAttributes : INSCopying

    {
        [NullAllowed]
        [Export("path", ArgumentSemantic.Assign)]
        UIBezierPath Path { get; set; }
    }

    [Protocol(Name = "MDCTabBarIndicatorContext")]
    [BaseType(typeof(NSObject))]
    interface TabBarIndicatorContext
    {
        [Abstract]
        [Export("item")]
        UITabBarItem Item { get; }

        [Abstract]
        [Export("bounds")]
        CGRect Bounds { get; }

        [Abstract]
        [Export("contentFrame")]
        CGRect ContentFrame { get; }
    }

    [Protocol(Name = "MDCTabBarIndicatorTemplate")]
    [BaseType(typeof(NSObject))]
    interface TabBarIndicatorTemplate
    {
        [Abstract]
        [Export("indicatorAttributesForContext:")]
        TabBarIndicatorAttributes IndicatorAttributesForContext(ITabBarIndicatorContext context);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCTabBarUnderlineIndicatorTemplate")]
    interface TabBarUnderlineIndicatorTemplate : TabBarIndicatorTemplate

    { }

    [BaseType(typeof(UIViewController),
        Name = "MDCTabBarViewController",
        Delegates = new[] { "Delegate" },
        Events = new[] { typeof(TabBarControllerDelegate) })]
    interface TabBarViewController : TabBarDelegate, IUIBarPositioningDelegate

    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ITabBarControllerDelegate Delegate { get; set; }

        [Export("viewControllers", ArgumentSemantic.Copy)]
        UIViewController[] ViewControllers { get; set; }

        [NullAllowed]
        [Export("selectedViewController", ArgumentSemantic.Weak)]
        UIViewController SelectedViewController { get; set; }

        [NullAllowed]
        [Export("tabBar")]
        TabBar TabBar { get; }

        [Export("tabBarHidden")]
        bool TabBarHidden { get; set; }

        [NullAllowed]
        [Export("traitCollectionDidChangeBlock", ArgumentSemantic.Copy)]
        Action<TabBarViewController, UITraitCollection> TraitCollectionDidChangeBlock { get; set; }

        [Field("MDCTabBarViewControllerAnimationDuration", "__Internal")]
        nfloat AnimationDuration { get; }

        [Export("setTabBarHidden:animated:")]
        void SetTabBarHidden(bool hidden, bool animated);
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCTabBarControllerDelegate")]
    interface TabBarControllerDelegate
    {
        [DefaultValue(true)]
        [DelegateName("TabBarControllerShouldSelectViewController")]
        [Export("tabBarController:shouldSelectViewController:")]
        bool ShouldSelectViewController(TabBarViewController tabBarController, UIViewController viewController);

        [EventArgs("TabBarControllerViewControllerSelected")]
        [EventName("ViewControllerSelected")]
        [Export("tabBarController:didSelectViewController:")]
        void DidSelectViewController(TabBarViewController tabBarController, UIViewController viewController);
    }

    [Protocol(Name = "MDCTabBarDisplayDelegate")]
    interface TabBarDisplayDelegate
    {
        [Abstract]
        [Export("tabBar:willDisplayItem:")]
        void WillDisplayItem(TabBar tabBar, UITabBarItem item);

        [Abstract]
        [Export("tabBar:didEndDisplayingItem:")]
        void DidEndDisplayingItem(TabBar tabBar, UITabBarItem item);
    }


    [Protocol(Name = "MDCTabBarSizeClassDelegate")]
    interface TabBarSizeClassDelegate
    {
        [Export("horizontalSizeClassForObject:")]
        long HorizontalSizeClassForObject(IUITraitEnvironment @object);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTabBarColorThemer")]
    interface TabBarColorThemer
    {
        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Export("applyColorScheme:toTabBar:")]
        void ApplyColorScheme (IColorScheme colorScheme, TabBar tabBar);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toTabs:")]
        void ApplySemanticColorScheme (IColorScheming colorScheme, TabBar tabBar);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySurfaceVariantWithColorScheme:toTabs:")]
        void ApplySurfaceVariant (IColorScheming colorScheme, TabBar tabBar);
    }

    [Obsolete("This class will soon be deprecated. Please consider using TabBarTypographyThemer class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTabBarFontThemer")]
    interface TabBarFontThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toTabBar:")]
        void ApplyFontScheme (IFontScheme fontScheme, TabBar tabBar);
    }

    [Category]
    [BaseType(typeof(TabBar),
        Name = "MDCTabBar_MaterialTheming")]
    interface TabBar_MaterialTheming
    {
        [Export("applyPrimaryThemeWithScheme:")]
        void ApplyPrimaryThemeWithScheme(IContainerScheming scheme);

        [Export("applySurfaceThemeWithScheme:")]
        void ApplySurfaceThemeWithScheme(IContainerScheming scheme);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTabBarTypographyThemer")]
    interface TabBarTypographyThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toTabBar:")]
        void ApplyTypographyScheme (ITypographyScheming typographyScheme, TabBar tabBar);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFilledTextFieldColorThemer")]
    interface FilledTextFieldColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toTextInputControllerFilled:")]
        void ApplySemanticColorScheme (IColorScheming colorScheme, TextInputControllerFilled textInputControllerFilled);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCOutlinedTextFieldColorThemer")]
    interface OutlinedTextFieldColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toTextInputController:")]
        void ApplySemanticColorScheme(IColorScheming colorScheme, ITextInputController textInputController);
    }

    [Obsolete("This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTextFieldColorThemer")]
    interface TextFieldColorThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toTextInputController:")]
        void ApplySemanticColorScheme (IColorScheming colorScheme, ITextInputController textInputController);

        [Wrap("ApplySemanticColorScheme (colorScheme, textInputController)")]
        [Obsolete("Use ApplySemanticColorScheme instead.")]
        [Static]
        void ApplySemanticColorSchemeToTextInput (IColorScheming colorScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toAllTextInputControllersOfClass:")]
        void ApplySemanticColorSchemeToAll (IColorScheming colorScheme, Class textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Wrap("ApplySemanticColorSchemeToAll (colorScheme, new Class (textInputControllerType))")]
        void ApplySemanticColorSchemeToAll (IColorScheming colorScheme, Type textInputControllerType);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applySemanticColorScheme:toTextInput:")]
        void ApplySemanticColorScheme (IColorScheming colorScheme, ITextInput textInput);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyColorScheme:toTextInputController:")]
        void ApplyColorScheme (IColorScheme colorScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorScheme method instead.")]
        [Static]
        [Wrap("ApplyColorScheme (colorScheme, textInputController)")]
        void ApplyColorSchemeToTextInputController(IColorScheme colorScheme, ITextInputController textInputController);


        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyColorScheme:toAllTextInputControllersOfClass:")]
        void ApplyColorScheme (IColorScheme colorScheme, Class textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Wrap("ApplyColorScheme (colorScheme, new Class (textInputControllerType))")]
        void ApplyColorScheme (IColorScheme colorScheme, Type textInputControllerType);

        [Static]
        [Wrap("ApplySemanticColorScheme (colorScheme, textInputController)")]
        [Obsolete("Use ApplySemanticColorScheme instead.")]
        void ApplySemanticColorSchemeToTextInputController(IColorScheming colorScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated. Consider using ApplySemanticColorSchemeToAll method instead.")]
        [Static]
        [Export("applyColorScheme:toAllTextInputControllersOfClass:")]
        void ApplyColorSchemeoAllTextInputControllersOfClass(IColorScheme colorScheme, Class textInputControllerClass);

        [Static]
        [Wrap("ApplySemanticColorScheme (colorScheme, textInput)")]
        [Obsolete("Use ApplySemanticColorScheme instead.")]
        void ApplySemanticColorSchemeToTextInput(IColorScheming colorScheme, ITextInput textInput);

    }

    [BaseType(typeof(UITextField),
    Name = "MDCBaseTextField")]
    interface BaseTextField
    {
        [Export("label", ArgumentSemantic.Strong)]
        UILabel Label { get; }

        [Export("labelBehavior", ArgumentSemantic.Assign)]
        TextControlLabelBehavior LabelBehavior { get; set; }

        [NullAllowed]
        [Export("leadingView", ArgumentSemantic.Strong)]
        UIView LeadingView { get; set; }

        [NullAllowed]
        [Export("trailingView", ArgumentSemantic.Strong)]
        UIView TrailingView { get; set; }

        [Export("leadingViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode LeadingViewMode { get; set; }

        [Export("trailingViewMode", ArgumentSemantic.Assign)]
        UITextFieldViewMode TrailingViewMode { get; set; }
    }

    [Obsolete(" This class will soon be deprecated. Please consider using TextFieldTypographyThemer class instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTextFieldFontThemer")]
    interface TextFieldFontThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toTextInputController:")]
        void ApplyFontScheme(IFontScheme fontScheme, ITextInputController textInputController);

        [Wrap("ApplyFontScheme (fontScheme, textInputController)")]
        [Obsolete("Use ApplyFontScheme instead.")]
        [Static]
        void ApplyFontSchemeToTextInputController(IFontScheme fontScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toAllTextInputControllersOfClass:")]
        void ApplyFontSchemeToAll(IFontScheme fontScheme, Class textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Wrap("ApplyFontSchemeToAll (fontScheme, new Class (textInputControllerType))")]
        void ApplyFontSchemeToAll(IFontScheme fontScheme, Type textInputControllerType);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toTextField:")]
        void ApplyFontScheme(IFontScheme fontScheme, [NullAllowed]  TextField textField);

        [Wrap("ApplyFontScheme (fontScheme, textField)")]
        [Obsolete("Use ApplyFontScheme instead.")]
        [Static]
        void ApplyFontSchemeToTextField(IFontScheme fontScheme, TextField textField);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toTextInputController:")]
        void ApplyFontScheme(FontScheme fontScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toAllTextInputControllersOfClass:")]
        void ApplyFontSchemeToAll(FontScheme fontScheme, ITextInputController textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyFontScheme:toTextField:")]
        void ApplyFontScheme(FontScheme fontScheme, [NullAllowed]  TextField textField);
    }

    [Category]
    [BaseType(typeof(TextInputControllerFilled),
        Name = "MDCTextInputControllerFilled_MaterialTheming")]
    interface TextInputControllerFilled_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [Category]
    [BaseType(typeof(TextInputControllerOutlined),
        Name = "MDCTextInputControllerOutlined_MaterialTheming")]
    interface TextInputControllerOutlined_MaterialTheming
    {
        [Export("applyThemeWithScheme:")]
        void ApplyThemeWithScheme(IContainerScheming scheme);
    }

    [Obsolete(" This class will soon be deprecated.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTextFieldTypographyThemer")]
    interface TextFieldTypographyThemer
    {
        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toTextInputController:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toAllTextInputControllersOfClass:")]
        void ApplyTypographySchemeToAll(ITypographyScheming typographyScheme, Class textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Wrap("ApplyTypographySchemeToAll (typographyScheme, new Class (textInputControllerType))")]
        [Static]
        void ApplyTypographySchemeToAll(ITypographyScheming typographyScheme, Type textInputControllerType);

        [Wrap("ApplyTypographyScheme (typographyScheme, textInputController)")]
        [Obsolete("Use ApplyTypographyScheme instead.")]
        [Static]
        void ApplyTypographySchemeToTextInputController(ITypographyScheming typographyScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toTextInput:")]
        void ApplyTypographyScheme(ITypographyScheming typographyScheme, ITextInput textInput);

        [Wrap("ApplyTypographyScheme (typographyScheme, textInput)")]
        [Obsolete("Use ApplyTypographyScheme instead.")]
        [Static]
        void ApplyTypographySchemeToTextInput(ITypographyScheming typographyScheme, ITextInput textInput);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toTextInputController:")]
        void ApplyTypographyScheme(TypographyScheming typographyScheme, ITextInputController textInputController);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toAllTextInputControllersOfClass:")]
        void ApplyTypographySchemeToAll(TypographyScheming typographyScheme, ITextInputController textInputControllerClass);

        [Obsolete("This method will soon be deprecated.")]
        [Static]
        [Export("applyTypographyScheme:toTextInput:")]
        void ApplyTypographyScheme(TypographyScheming typographyScheme, ITextInput textInput);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCFontScaler")]
    interface FontScaler
    {
        [DesignatedInitializer]
        [Export("initForMaterialTextStyle:")]
        IntPtr Constructor(string textStyle);

        [Static]
        [Export("scalerForMaterialTextStyle:")]
        FontScaler ScalerForMaterialTextStyle(string textStyle);

        [Export("scaledFontWithFont:")]
        UIFont ScaledFontWithFont(UIFont font);

        [Export("scaledValueForValue:")]
        nfloat ScaledValueForValue(nfloat value);
    }

    [Obsolete("This interface will soon be deprecated. Consider using TypographyScheme from the schemes/Typography component instead.")]
    [Protocol(Name = "MDCTypographyFontLoading")]
    [BaseType(typeof(NSObject))]
    interface TypographyFontLoading
    {
        [return: NullAllowed]
        [Abstract]
        [Export("lightFontOfSize:")]
        UIFont GetLightFont(nfloat fontSize);

        [Abstract]
        [Export("regularFontOfSize:")]
        UIFont GetRegularFont(nfloat fontSize);

        [return: NullAllowed]
        [Abstract]
        [Export("mediumFontOfSize:")]
        UIFont GetMediumFont(nfloat fontSize);

        [Export("boldFontOfSize:")]
        UIFont GetBoldFont(nfloat fontSize);

        [Export("italicFontOfSize:")]
        UIFont GetItalicFont(nfloat fontSize);

        [return: NullAllowed]
        [Export("boldItalicFontOfSize:")]
        UIFont GetBoldItalicFont(nfloat fontSize);

        [Export("boldFontFromFont:")]
        UIFont GetBoldFont(UIFont font);

        [Export("italicFontFromFont:")]
        UIFont GetItalicFont(UIFont font);

        [Export("isLargeForContrastRatios:")]
        bool IsLargeForContrastRatios(UIFont font);
    }

    [Obsolete("This class will soon be deprecated. Consider using TypographyScheme from the schemes/Typography component instead.")]
    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCTypography")]
    interface Typography
    {
        [Static]

        [Export("fontLoader")]
        ITypographyFontLoading FontLoader { get; set; }

        [Static]

        [Export("display4Font")]
        UIFont Display4Font { get; }

        [Static]

        [Export("display4FontOpacity")]
        nfloat Display4FontOpacity { get; }

        [Static]

        [Export("display3Font")]
        UIFont Display3Font { get; }

        [Static]

        [Export("display3FontOpacity")]
        nfloat Display3FontOpacity { get; }

        [Static]

        [Export("display2Font")]
        UIFont Display2Font { get; }

        [Static]

        [Export("display2FontOpacity")]
        nfloat Display2FontOpacity { get; }

        [Static]

        [Export("display1Font")]
        UIFont Display1Font { get; }

        [Static]

        [Export("display1FontOpacity")]
        nfloat Display1FontOpacity { get; }

        [Static]

        [Export("headlineFont")]
        UIFont HeadlineFont { get; }

        [Static]

        [Export("headlineFontOpacity")]
        nfloat HeadlineFontOpacity { get; }

        [Static]

        [Export("titleFont")]
        UIFont TitleFont { get; }

        [Static]

        [Export("titleFontOpacity")]
        nfloat TitleFontOpacity { get; }

        [Static]

        [Export("subheadFont")]
        UIFont SubheadFont { get; }

        [Static]

        [Export("subheadFontOpacity")]
        nfloat SubheadFontOpacity { get; }

        [Static]

        [Export("body2Font")]
        UIFont Body2Font { get; }

        [Static]

        [Export("body2FontOpacity")]
        nfloat Body2FontOpacity { get; }

        [Static]

        [Export("body1Font")]
        UIFont Body1Font { get; }

        [Static]

        [Export("body1FontOpacity")]
        nfloat Body1FontOpacity { get; }

        [Static]

        [Export("captionFont")]
        UIFont CaptionFont { get; }

        [Static]

        [Export("captionFontOpacity")]
        nfloat CaptionFontOpacity { get; }

        [Static]

        [Export("buttonFont")]
        UIFont ButtonFont { get; }

        [Static]

        [Export("buttonFontOpacity")]
        nfloat ButtonFontOpacity { get; }

        [Static]
        [Export("boldFontFromFont:")]
        UIFont GetBoldFont(UIFont font);

        [Static]
        [Export("italicFontFromFont:")]
        UIFont GetItalicFont(UIFont font);

        [Static]
        [Export("isLargeForContrastRatios:")]
        bool IsLargeForContrastRatios(UIFont font);
    }

    [Obsolete("This class will soon be deprecated. Consider using TypographyScheme from the schemes/Typography component instead.")]
    [BaseType(typeof(NSObject),
        Name = "MDCSystemFontLoader")]
    interface SystemFontLoader : TypographyFontLoading
    { }

    [Category]
    [BaseType(typeof(UIFont))]
    interface UIFont_MaterialScalable
    {
        [NullAllowed]
        [Export("mdc_scalingCurve", ArgumentSemantic.Copy)]
        [Static]
        NSDictionary<NSString, NSNumber> ScalingCurve { get; [Bind("mdc_setScalingCurve:")]set; }

        [Export("mdc_scaledFontAtDefaultSize")]
        [Static]
        UIFont ScaledFontAtDefaultSize { get; }

        [Export("mdc_scaledFontForCurrentSizeCategory")]
        [Static]
        UIFont ScaledFontForCurrentSizeCategory { get; }

        [Export("mdc_scaledFontForSizeCategory:")]
        UIFont ScaledFontForSizeCategory(string sizeCategory);

        [Export("mdc_scaledFontForTraitEnvironment:")]
        UIFont ScaledFontForTraitEnvironment(IUITraitEnvironment traitEnvironment);
    }

    [Category]
    [BaseType(typeof(UIFont))]
    interface UIFont_MaterialSimpleEquality
    {
        [Export("mdc_isSimplyEqual:")]
        bool IsSimplyEqual(UIFont font);

        [Wrap("IsSimplyEqual(This, font)")]
        bool MdcIsSimplyEqual(UIFont font);
    }

    [Category]
    [BaseType(typeof(UIFont))]
    interface UIFont_MaterialTypography
    {
        [Export("mdc_fontSizedForMaterialTextStyle:scaledForDynamicType:")]
        UIFont GetFontSized(FontTextStyle style, bool scaled);

        [Wrap("GetFontSized(This, style,scaled)")]
        UIFont MdcGetFontSized(FontTextStyle style, bool scaled);
    }

    [Category]
    [BaseType(typeof(UIColor))]
    interface UIColor_MaterialBlending
    {
        [Static]
        [Export("mdc_blendColor:withBackgroundColor:")]
        UIColor BlendColor(UIColor color, UIColor backgroundColor);
    }

    [Category]
    [BaseType(typeof(UIColor))]
    interface UIColor_MaterialDynamic
    {
        [Static]
        [Export("colorWithUserInterfaceStyleDarkColor:defaultColor:")]
        UIColor ColorWithUserInterfaceStyleDarkColor(UIColor darkColor, UIColor defaultColor);

        [Export("mdc_resolvedColorWithTraitCollection:")]
        UIColor ResolvedColorWithTraitCollection(UITraitCollection traitCollection);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCIcons")]
    interface Icons
    {
        [Static]
        [Export("pathFor_ic_arrow_back")]
        string IcArrowBackPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_arrow_back")]
        UIImage IcArrowBackImage { get; }

        [Static]
        [Export("pathFor_ic_check_circle")]
        string IcCheckCirclePath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_check_circle")]
        UIImage IcCheckCircleImage { get; }

        [Static]
        [Export("pathFor_ic_check")]
        string IcCheckPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_check")]
        UIImage IcCheckImage { get; }

        [Static]
        [Export("pathFor_ic_chevron_right")]
        string PathForIcChevronRight { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_chevron_right")]
        UIImage ImageForIcChevronRight { get; }

        [Static]
        [Export("pathFor_ic_color_lens")]
        string IcColorLensPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_color_lens")]
        UIImage IcColorLensImage { get; }

        [Static]
        [Export("pathFor_ic_help_outline")]
        string IcHelpOutlinePath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_help_outline")]
        UIImage IcHelpOutlineImage { get; }

        [Static]
        [Export("pathFor_ic_info")]
        string IcInfoPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_info")]
        UIImage IcInfoImage { get; }

        [Static]
        [Export("pathFor_ic_more_horiz")]
        string IcMoreHorizPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_more_horiz")]
        UIImage IcMoreHorizImage { get; }

        [Static]
        [Export("pathFor_ic_radio_button_unchecked")]
        string IcRadioButtonUncheckedPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_radio_button_unchecked")]
        UIImage IcRadioButtonUncheckedImage { get; }

        [Static]
        [Export("pathFor_ic_reorder")]
        string IcReorderPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_reorder")]
        UIImage IcReorderImage { get; }

        [Static]
        [Export("pathFor_ic_settings")]
        string IcSettingsPath { get; }

        [NullAllowed]
        [Static]
        [Export("imageFor_ic_settings")]
        UIImage IcSettingsImage { get; }

        [Static]
        [Export("ic_arrow_backUseNewStyle:")]
        void IcArrowBackUseNewStyle(bool useNewStyle);

        [Static]
        [Export("pathForIconName:withBundleName:")]
        string GetPath(string iconName, string bundleName);

        [Static]
        [return: NullAllowed]
        [Export("bundleNamed:")]
        NSBundle GetBundleNamed(string bundleName);
    }

    [DisableDefaultCtor]
    [BaseType(typeof(NSObject),
        Name = "MDCKeyboardWatcher")]
    interface KeyboardWatcher
    {
        [Export("visibleKeyboardHeight")]
        nfloat VisibleKeyboardHeight { get; }

        [Notification]
        [Field("MDCKeyboardWatcherKeyboardWillShowNotification", "__Internal")]
        NSString KeyboardWillShowNotification { get; }

        [Notification]
        [Field("MDCKeyboardWatcherKeyboardWillHideNotification", "__Internal")]
        NSString KeyboardWillHideNotification { get; }

        [Notification]
        [Field("MDCKeyboardWatcherKeyboardWillChangeFrameNotification", "__Internal")]
        NSString KeyboardWillChangeFrameNotification { get; }

        [Static]
        [Export("sharedKeyboardWatcher")]
        KeyboardWatcher SharedInstance { get; }

        [Static]
        [Export("animationDurationFromKeyboardNotification:")]
        double AnimationDurationFromNotification(NSNotification notification);

        [Static]
        [Export("animationCurveOptionFromKeyboardNotification:")]
        UIViewAnimationOptions AnimationCurveOptionFromNotification(NSNotification notification);
    }

    [BaseType(typeof(NSObject),
        Name = "MDCOverlayObserver")]
    interface OverlayObserver
    {
        [Static]
        [Export("observerForScreen:")]
        OverlayObserver FromScreen(UIScreen screen);

        [Export("addTarget:action:")]
        void AddTarget(NSObject target, Selector action);

        [Export("removeTarget:action:")]
        void RemoveTarget(NSObject target, Selector action);

        [Export("removeTarget:")]
        void RemoveTarget(NSObject target);
    }

    [Protocol(Name = "MDCOverlay")]
    [BaseType(typeof(NSObject))]
    interface Overlay
    {
        [Abstract]
        [Export("identifier")]
        string Identifier { get; }

        [Abstract]
        [Export("frame")]
        CGRect Frame { get; }
    }

    [Protocol(Name = "MDCOverlayTransitioning")]
    [BaseType(typeof(NSObject))]
    interface OverlayTransitioning
    {
        [Abstract]
        [Export("duration")]
        double Duration { get; }

        [Abstract]
        [Export("customTimingFunction")]
        CAMediaTimingFunction CustomTimingFunction { get; }

        [Abstract]
        [Export("animationCurve")]
        UIViewAnimationCurve AnimationCurve { get; }

        [Abstract]
        [Export("compositeFrame")]
        CGRect CompositeFrame { get; }

        [Abstract]
        [Export("compositeFrameInView:")]
        CGRect GetCompositeFrame(UIView targetView);

        [Abstract]
        [Export("enumerateOverlays:")]
        void EnumerateOverlays(EnumerateOverlaysHandler handler);

        [Abstract]
        [Export("animateAlongsideTransition:")]
        void AnimateAlongsideTransition(Action animations);

        [Abstract]
        [Export("animateAlongsideTransitionWithOptions:animations:completion:")]
        void AnimateAlongsideTransition(UIViewAnimationOptions options, Action animations, Action<bool> completion);
    }

    [BaseType(typeof(UIControl),
        Name = "MDCThumbTrack")]
    interface ThumbTrack
    {
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        IThumbTrackDelegate Delegate { get; set; }

        [Export("thumbEnabledColor", ArgumentSemantic.Strong)]
        UIColor ThumbEnabledColor { get; set; }

        [NullAllowed]
        [Export("thumbDisabledColor", ArgumentSemantic.Strong)]
        UIColor ThumbDisabledColor { get; set; }

        [Export("trackOnColor", ArgumentSemantic.Strong)]
        UIColor TrackOnColor { get; set; }

        [NullAllowed]
        [Export("trackOffColor", ArgumentSemantic.Strong)]
        UIColor TrackOffColor { get; set; }

        [NullAllowed]
        [Export("trackDisabledColor", ArgumentSemantic.Strong)]
        UIColor TrackDisabledColor { get; set; }

        [NullAllowed]
        [Export("trackOnTickColor", ArgumentSemantic.Strong)]
        UIColor TrackOnTickColor { get; set; }

        [NullAllowed]
        [Export("trackOffTickColor", ArgumentSemantic.Strong)]
        UIColor TrackOffTickColor { get; set; }

        [Export("enableRippleBehavior")]
        bool EnableRippleBehavior { get; set; }

        [NullAllowed]
        [Export("rippleColor", ArgumentSemantic.Strong)]
        UIColor RippleColor { get; set; }

        [Export("valueLabelTextColor", ArgumentSemantic.Strong)]
        UIColor ValueLabelTextColor { get; set; }

        [Export("valueLabelBackgroundColor", ArgumentSemantic.Strong)]
        UIColor ValueLabelBackgroundColor { get; set; }

        [Export("numDiscreteValues")]
        nuint NumDiscreteValues { get; set; }

        [Export("value")]
        nfloat Value { get; set; }

        [Export("minimumValue")]
        nfloat MinimumValue { get; set; }

        [Export("maximumValue")]
        nfloat MaximumValue { get; set; }

        [Export("thumbPosition", ArgumentSemantic.Assign)]
        CGPoint ThumbPosition { get; }

        [Export("trackHeight")]
        nfloat TrackHeight { get; set; }

        [Export("thumbRadius")]
        nfloat ThumbRadius { get; set; }

        [Export("thumbElevation")]
        nfloat ThumbElevation { get; set; }

        [Export("thumbShadowColor", ArgumentSemantic.Strong)]
        UIColor ThumbShadowColor { get; set; }

        [Export("thumbIsSmallerWhenDisabled")]
        bool ThumbIsSmallerWhenDisabled { get; set; }

        [Export("thumbIsHollowAtStart")]
        bool ThumbIsHollowAtStart { get; set; }

        [Export("thumbGrowsWhenDragging")]
        bool ThumbGrowsWhenDragging { get; set; }

        [Export("thumbRippleMaximumRadius")]
        nfloat ThumbRippleMaximumRadius { get; set; }

        [Export("shouldDisplayRipple")]
        bool ShouldDisplayRipple { get; set; }

        [Export("shouldDisplayDiscreteDots")]
        bool ShouldDisplayDiscreteDots { get; set; }

        [Export("shouldDisplayDiscreteValueLabel")]
        bool ShouldDisplayDiscreteValueLabel { get; set; }

        [Export("shouldDisplayFilledTrack")]
        bool ShouldDisplayFilledTrack { get; set; }

        [Export("disabledTrackHasThumbGaps")]
        bool DisabledTrackHasThumbGaps { get; set; }

        [Export("trackEndsAreRounded")]
        bool TrackEndsAreRounded { get; set; }

        [Export("trackEndsAreInset")]
        bool TrackEndsAreInset { get; set; }

        [Export("filledTrackAnchorValue")]
        nfloat FilledTrackAnchorValue { get; set; }

        [NullAllowed]
        [Export("thumbView", ArgumentSemantic.Strong)]
        ThumbView ThumbView { get; set; }

        [Export("continuousUpdateEvents")]
        bool ContinuousUpdateEvents { get; set; }

        [Export("panningAllowedOnEntireControl")]
        bool PanningAllowedOnEntireControl { get; set; }

        [Export("tapsAllowedOnThumb")]
        bool TapsAllowedOnThumb { get; set; }

        [Obsolete("This API will be deprecated. Use ThumbEnabledColor, TrackOnColor, and InkColor properties instead.")]
        [NullAllowed]
        [Export("primaryColor", ArgumentSemantic.Strong)]
        UIColor PrimaryColor { get; set; }

        [Export("initWithFrame:onTintColor:")]
        IntPtr Constructor(CGRect frame, [NullAllowed]  UIColor onTintColor);

        [Export("setValue:animated:")]
        void SetValue(nfloat value, bool animated);

        [Export("setValue:animated:animateThumbAfterMove:userGenerated:completion:")]
        void SetValue(nfloat value, bool animated, bool animateThumbAfterMove, bool userGenerated, [NullAllowed]  Action completion);

        [Export("setIcon:")]
        void SetIcon([NullAllowed] UIImage icon);

        [Obsolete("This property will be deprecated soon.")]
        [NullAllowed]
        [Export("inkColor", ArgumentSemantic.Strong)]
        UIColor InkColor { get; set; }

        [Obsolete("This property will be deprecated soon.")]
        [Export("shouldDisplayInk")]
        bool ShouldDisplayInk { get; set; }

        [Obsolete("This property will be deprecated soon.")]
        [Export("thumbMaxRippleRadius")]
        nfloat ThumbMaxRippleRadius { get; set; }
    }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject),
        Name = "MDCThumbTrackDelegate")]
    interface ThumbTrackDelegate
    {
        [Export("thumbTrack:stringForValue:")]
        string GetStringForValue(ThumbTrack thumbTrack, nfloat value);

        [Export("thumbTrack:shouldJumpToValue:")]
        bool ShouldJumpToValue(ThumbTrack thumbTrack, nfloat value);

        [Export("thumbTrack:willJumpToValue:")]
        void WillJumpToValue(ThumbTrack thumbTrack, nfloat value);

        [Export("thumbTrack:willAnimateToValue:")]
        void WillAnimateToValue(ThumbTrack thumbTrack, nfloat value);

        [Export("thumbTrack:didAnimateToValue:")]
        void DidAnimateToValue(ThumbTrack thumbTrack, nfloat value);
    }

    [BaseType(typeof(UIView),
        Name = "MDCThumbView")]
    interface ThumbView
    {
        [Export("elevation")]
        nfloat Elevation { get; set; }

        [Export("borderWidth")]
        nfloat BorderWidth { get; set; }

        [Export("cornerRadius")]
        nfloat CornerRadius { get; set; }

        [Export("shadowColor", ArgumentSemantic.Strong)]
        UIColor ShadowColor { get; set; }

        [Export("setIcon:")]
        void SetIcon([NullAllowed] UIImage icon);
    }

    [BaseType(typeof(UIView),
        Name = "MDCNumericValueLabel")]
    interface NumericValueLabel
    {
        [New]
        [Export("backgroundColor", ArgumentSemantic.Retain)]
        UIColor BackgroundColor { get; set; }

        [Export("textColor", ArgumentSemantic.Retain)]
        UIColor TextColor { get; set; }

        [Export("fontSize")]
        nfloat FontSize { get; set; }

        [Export("text")]
        string Text { get; set; }
    }

    [Static]
    interface CAMediaTimingFunctionAnimationTiming
    {
        [Static]
        [return: NullAllowed]
        [Export("mdc_functionWithType:")]
        CAMediaTimingFunction GetFunction(AnimationTimingFunction type);
    }

    [Static]
    interface MaterialComponentsConstants
    {
        [Field("MaterialComponentsVersionNumber", "__Internal")]
        double VersionNumber { get; }

        [Internal]
        [Field("MaterialComponentsVersionString", "__Internal")]
        IntPtr _VersionString { get; }
    }

    interface IElevatable { }

    interface IElevationOverriding { }

    interface IRippleViewDelegate { }

    interface IFlexibleHeaderSafeAreaDelegate { }

    interface IActivityIndicatorDelegate
    { }

    interface ITabBarDisplayDelegate { }

    interface ITabBarSizeClassDelegate { }

    interface IRippleTouchControllerDelegate { }

    interface IDialogPresentationControllerDelegate { }

    interface IContainerScheming
    { }

    interface IShapeScheming
    { }

    interface IAlertScheming
    { }

    interface IAppBarNavigationControllerDelegate
    { }

    interface IListScheming
    { }

    interface IBottomDrawerHeader
    { }

    interface IBottomDrawerPresentationControllerDelegate
    { }

    interface IBottomDrawerViewControllerDelegate
    { }

    interface IBottomNavigationBarDelegate
    { }

    interface IBottomSheetControllerDelegate
    { }

    interface IBottomSheetPresentationControllerDelegate
    { }

    interface IButtonBarDelegate
    { }

    interface IButtonScheming
    { }

    interface ICardScheming
    { }

    interface IChipFieldDelegate
    { }

    interface IChipViewScheming
    { }

    [Protocol(Name = "MDCChipViewScheming")]
    interface ChipViewScheming
    {
        [Abstract]
        [Export("colorScheme")]
        IColorScheming ColorScheme { get; }

        [Abstract]
        [Export("shapeScheme")]
        IShapeScheming ShapeScheme { get; }

        [Abstract]
        [Export("typographyScheme")]
        ITypographyScheming TypographyScheme { get; }
    }

    interface ICollectionViewEditing
    { }

    interface ICollectionViewEditingDelegate
    { }

    interface ICollectionViewStyling
    { }

    interface ICollectionViewStylingDelegate
    { }

    interface IFlexibleHeaderViewDelegate
    { }

    interface IFlexibleHeaderViewLayoutDelegate
    { }

    interface IInkTouchControllerDelegate
    { }

    interface IInkViewDelegate
    { }

    [Protocol]
    [Model(AutoGeneratedName = true)]
    [BaseType(typeof(NSObject))]
    interface InkViewDelegate
    {
        [EventArgs("InkViewInkAnimationStarted")]
        [EventName("InkAnimationStarted")]
        [Export("inkAnimationDidStart:")]
        void InkAnimationDidStart(InkView inkView);

        [EventArgs("InkViewInkAnimationEnded")]
        [EventName("InkAnimationEnded")]
        [Export("inkAnimationDidEnd:")]
        void InkAnimationDidEnd(InkView inkView);
    }

    interface IColorScheme
    { }

    interface IFontScheme
    { }

    interface IMultilineTextInputLayoutDelegate
    { }

    interface IMultilineTextInputDelegate
    { }

    interface IUINavigationItemObservables
    { }

    interface IOverlay
    { }

    interface IOverlayTransitioning
    { }

    interface IColorScheming
    { }

    interface IShapeGenerating
    { }

    interface ISliderDelegate
    { }

    interface ISnackbarManagerDelegate
    { }

    interface ISnackbarSuspensionToken
    { }

    interface ITabBarDelegate
    { }

    interface ITabBarIndicatorContext
    { }

    interface ITabBarIndicatorTemplate
    { }

    interface ITabBarControllerDelegate
    { }

    [Static]
    partial interface Constants11
    { }

    interface ITextInputPositioningDelegate
    { }

    interface ITextInput
    { }

    interface ILeadingViewTextInput
    { }

    interface IMultilineTextInput
    { }

    interface ITextInputCharacterCounter
    { }

    interface ITextInputController
    { }

    interface ITextInputControllerFloatingPlaceholder
    { }

    interface IThumbTrackDelegate
    { }

    interface ITypographyFontLoading
    { }

    interface ITypographyScheming
    { }

    [Static]
    interface UIApplicationAppExtensions
    {
        [Static]
        [Export("mdc_safeSharedApplication")]
        UIApplication SafeSharedApplication { get; }

        [Static]
        [Wrap("SafeSharedApplication")]
        UIApplication MdcSafeSharedApplication { get; }

        [Static]
        [Export("mdc_isAppExtension")]
        bool IsAppExtension { get; }

        [Static]
        [Wrap("IsAppExtension")]
        bool MdcIsAppExtension { get; }
    }

    [Static]
    interface UIFontMaterialTypography
    {
        [Static]
        [Export("mdc_preferredFontForMaterialTextStyle:")]
        UIFont GetPreferredFont(FontTextStyle style);

        [Static]
        [Wrap("GetPreferredFont(style)")]
        UIFont MdcGetPreferredFont(FontTextStyle style);

        [Static]
        [Export("mdc_standardFontForMaterialTextStyle:")]
        UIFont GetStandardFont(FontTextStyle style);

        [Static]
        [Wrap("GetStandardFont(style)")]
        UIFont MdcGetStandardFont(FontTextStyle style);
    }

    [Static]
    interface UIFontDescriptorMaterialTypography
    {
        [Static]
        [Export("mdc_preferredFontDescriptorForMaterialTextStyle:")]
        UIFontDescriptor GetPreferredFontDescriptor(FontTextStyle style);

        [Static]
        [Wrap("GetPreferredFontDescriptor(style)")]
        UIFontDescriptor MdcGetPreferredFontDescriptor(FontTextStyle style);

        [Static]
        [Export("mdc_standardFontDescriptorForMaterialTextStyle:")]
        UIFontDescriptor GetStandardFontDescriptor(FontTextStyle style);

        [Static]
        [Wrap("GetStandardFontDescriptor(style)")]
        UIFontDescriptor MdcGetStandardFontDescriptor(FontTextStyle style);
    }

    [Static]
    interface UIViewMDCTimingFunction
    {
        [Static]
        [Export("mdc_animateWithTimingFunction:duration:delay:options:animations:completion:")]
        void Animate([NullAllowed] CAMediaTimingFunction timingFunction, double duration, double delay, UIViewAnimationOptions options, Action animations, [NullAllowed]  Action<bool> completion);

        [Wrap("Animate (timingFunction, duration, delay, options, animations, completion)")]
        [Obsolete("Use Animate instead.")]
        [Static]
        void MdcAnimate([NullAllowed] CAMediaTimingFunction timingFunction, double duration, double delay, UIViewAnimationOptions options, Action animations, [NullAllowed] Action<bool> completion);
    }

    [Static]
    interface NSLocaleMaterialRtl
    {
        [Static]
        [Export("mdf_isDefaultLanguageLTR")]
        bool MdfIsDefaultLanguageLtr();

        [Static]
        [Export("mdf_isDefaultLanguageRTL")]
        bool MdfIsDefaultLanguageRtl();
    }

    [Category]
    [BaseType(typeof(NSString))]
    interface NSStringMaterialBidi
    {
        [Export("mdf_calculatedLanguageDirection")]
        NSLocaleLanguageDirection MdfCalculatedLanguageDirection();

        [Export("mdf_stringWithBidiEmbedding:")]
        string MdfGetStringWithBidiEmbedding(NSLocaleLanguageDirection languageDirection);

        [Export("mdf_stringWithBidiEmbedding")]
        string MdfGetStringWithBidiEmbedding();

        [Export("mdf_stringWithStereoReset:context:")]
        string MdfGetStringWithStereoReset(NSLocaleLanguageDirection direction, NSLocaleLanguageDirection contextDirection);

        [Export("mdf_stringWithBidiMarkersStripped")]
        string MdfGetStringWithBidiMarkersStripped();
    }

    [Category]
    [BaseType(typeof(UIImage))]
    interface UIImage_MaterialRtl
    {
        [Export("mdf_imageWithHorizontallyFlippedOrientation")]
        UIImage MdfGetImageWithHorizontallyFlippedOrientation();
    }

    [Category]
    [BaseType(typeof(UIView))]
    interface UIView_MaterialRtl
    {
        [Export("mdf_semanticContentAttribute")]
        UISemanticContentAttribute MdfGetSemanticContentAttribute();

        [Export("mdf_setSemanticContentAttribute:")]
        void MdfSetSemanticContentAttribute(UISemanticContentAttribute value);

        [Export("mdf_effectiveUserInterfaceLayoutDirection")]
        UIUserInterfaceLayoutDirection MdfGetEffectiveUserInterfaceLayoutDirection();
    }

    [Static]
    interface UIViewMaterialRtl
    {
        [Static]
        [Export("mdf_userInterfaceLayoutDirectionForSemanticContentAttribute:")]
        UIUserInterfaceLayoutDirection MdfGetUserInterfaceLayoutDirection(UISemanticContentAttribute semanticContentAttribute);

        [Static]
        [Export("mdf_userInterfaceLayoutDirectionForSemanticContentAttribute:relativeToLayoutDirection:")]
        UIUserInterfaceLayoutDirection MdfGetUserInterfaceLayoutDirection(UISemanticContentAttribute semanticContentAttribute, UIUserInterfaceLayoutDirection layoutDirection);
    }
}