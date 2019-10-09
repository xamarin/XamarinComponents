using System;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using nfloat = System.Single;
#endif

namespace ChameleonFramework
{
    // @interface Chameleon : NSObject
    [BaseType(typeof(NSObject), Name = "Chameleon")]
    interface Chameleon
    {
        //		// extern double ChameleonVersionNumber;
        //		[Static]
        //		[Field ("ChameleonVersionNumber", "__Internal")]
        //		double ChameleonVersionNumber { get; }
        //
        //		// extern const unsigned char [] ChameleonVersionString;
        //		[Static]
        //		[Field ("ChameleonVersionString", "__Internal")]
        //		byte[] ChameleonVersionString { get; }

        // extern const UIStatusBarStyle UIStatusBarStyleContrast;
        [Static]
        [Field("UIStatusBarStyleContrast", "__Internal")]
        long StatusBarStyleContrast { get; }


        // +(void)setGlobalThemeUsingPrimaryColor:(UIColor *)primaryColor withContentStyle:(UIContentStyle)contentStyle;
        [Static]
        [Export("setGlobalThemeUsingPrimaryColor:withContentStyle:")]
        void SetGlobalTheme(UIColor primaryColor, ContentStyle contentStyle);

        // +(void)setGlobalThemeUsingPrimaryColor:(UIColor *)primaryColor withSecondaryColor:(UIColor *)secondaryColor andContentStyle:(UIContentStyle)contentStyle;
        [Static]
        [Export("setGlobalThemeUsingPrimaryColor:withSecondaryColor:andContentStyle:")]
        void SetGlobalTheme(UIColor primaryColor, UIColor secondaryColor, ContentStyle contentStyle);

        // +(void)setGlobalThemeUsingPrimaryColor:(UIColor *)primaryColor withSecondaryColor:(UIColor *)secondaryColor usingFontName:(NSString *)fontName andContentStyle:(UIContentStyle)contentStyle;
        [Static]
        [Export("setGlobalThemeUsingPrimaryColor:withSecondaryColor:usingFontName:andContentStyle:")]
        void SetGlobalTheme(UIColor primaryColor, UIColor secondaryColor, string fontName, ContentStyle contentStyle);
    }

    // @interface Chameleon (UIColor)
    [Static]
    [BaseType(typeof(NSObject), Name = "UIColor")]
    interface ChameleonColor
    {
        // @property (nonatomic, strong) UIImage * gradientImage;
        [Static]
        [Export("gradientImage", ArgumentSemantic.Strong)]
        UIImage GradientImage();

        // +(UIColor *)flatBlackColor;
        [Static]
        [Export("flatBlackColor")]
        UIColor FlatBlackColor { get; }

        // +(UIColor *)flatBlueColor;
        [Static]
        [Export("flatBlueColor")]
        UIColor FlatBlueColor { get; }

        // +(UIColor *)flatBrownColor;
        [Static]
        [Export("flatBrownColor")]
        UIColor FlatBrownColor { get; }

        // +(UIColor *)flatCoffeeColor;
        [Static]
        [Export("flatCoffeeColor")]
        UIColor FlatCoffeeColor { get; }

        // +(UIColor *)flatForestGreenColor;
        [Static]
        [Export("flatForestGreenColor")]
        UIColor FlatForestGreenColor { get; }

        // +(UIColor *)flatGrayColor;
        [Static]
        [Export("flatGrayColor")]
        UIColor FlatGrayColor { get; }

        // +(UIColor *)flatGreenColor;
        [Static]
        [Export("flatGreenColor")]
        UIColor FlatGreenColor { get; }

        // +(UIColor *)flatLimeColor;
        [Static]
        [Export("flatLimeColor")]
        UIColor FlatLimeColor { get; }

        // +(UIColor *)flatMagentaColor;
        [Static]
        [Export("flatMagentaColor")]
        UIColor FlatMagentaColor { get; }

        // +(UIColor *)flatMaroonColor;
        [Static]
        [Export("flatMaroonColor")]
        UIColor FlatMaroonColor { get; }

        // +(UIColor *)flatMintColor;
        [Static]
        [Export("flatMintColor")]
        UIColor FlatMintColor { get; }

        // +(UIColor *)flatNavyBlueColor;
        [Static]
        [Export("flatNavyBlueColor")]
        UIColor FlatNavyBlueColor { get; }

        // +(UIColor *)flatOrangeColor;
        [Static]
        [Export("flatOrangeColor")]
        UIColor FlatOrangeColor { get; }

        // +(UIColor *)flatPinkColor;
        [Static]
        [Export("flatPinkColor")]
        UIColor FlatPinkColor { get; }

        // +(UIColor *)flatPlumColor;
        [Static]
        [Export("flatPlumColor")]
        UIColor FlatPlumColor { get; }

        // +(UIColor *)flatPowderBlueColor;
        [Static]
        [Export("flatPowderBlueColor")]
        UIColor FlatPowderBlueColor { get; }

        // +(UIColor *)flatPurpleColor;
        [Static]
        [Export("flatPurpleColor")]
        UIColor FlatPurpleColor { get; }

        // +(UIColor *)flatRedColor;
        [Static]
        [Export("flatRedColor")]
        UIColor FlatRedColor { get; }

        // +(UIColor *)flatSandColor;
        [Static]
        [Export("flatSandColor")]
        UIColor FlatSandColor { get; }

        // +(UIColor *)flatSkyBlueColor;
        [Static]
        [Export("flatSkyBlueColor")]
        UIColor FlatSkyBlueColor { get; }

        // +(UIColor *)flatTealColor;
        [Static]
        [Export("flatTealColor")]
        UIColor FlatTealColor { get; }

        // +(UIColor *)flatWatermelonColor;
        [Static]
        [Export("flatWatermelonColor")]
        UIColor FlatWatermelonColor { get; }

        // +(UIColor *)flatWhiteColor;
        [Static]
        [Export("flatWhiteColor")]
        UIColor FlatWhiteColor { get; }

        // +(UIColor *)flatYellowColor;
        [Static]
        [Export("flatYellowColor")]
        UIColor FlatYellowColor { get; }

        // +(UIColor *)flatBlackColorDark;
        [Static]
        [Export("flatBlackColorDark")]
        UIColor FlatBlackColorDark { get; }

        // +(UIColor *)flatBlueColorDark;
        [Static]
        [Export("flatBlueColorDark")]
        UIColor FlatBlueColorDark { get; }

        // +(UIColor *)flatBrownColorDark;
        [Static]
        [Export("flatBrownColorDark")]
        UIColor FlatBrownColorDark { get; }

        // +(UIColor *)flatCoffeeColorDark;
        [Static]
        [Export("flatCoffeeColorDark")]
        UIColor FlatCoffeeColorDark { get; }

        // +(UIColor *)flatForestGreenColorDark;
        [Static]
        [Export("flatForestGreenColorDark")]
        UIColor FlatForestGreenColorDark { get; }

        // +(UIColor *)flatGrayColorDark;
        [Static]
        [Export("flatGrayColorDark")]
        UIColor FlatGrayColorDark { get; }

        // +(UIColor *)flatGreenColorDark;
        [Static]
        [Export("flatGreenColorDark")]
        UIColor FlatGreenColorDark { get; }

        // +(UIColor *)flatLimeColorDark;
        [Static]
        [Export("flatLimeColorDark")]
        UIColor FlatLimeColorDark { get; }

        // +(UIColor *)flatMagentaColorDark;
        [Static]
        [Export("flatMagentaColorDark")]
        UIColor FlatMagentaColorDark { get; }

        // +(UIColor *)flatMaroonColorDark;
        [Static]
        [Export("flatMaroonColorDark")]
        UIColor FlatMaroonColorDark { get; }

        // +(UIColor *)flatMintColorDark;
        [Static]
        [Export("flatMintColorDark")]
        UIColor FlatMintColorDark { get; }

        // +(UIColor *)flatNavyBlueColorDark;
        [Static]
        [Export("flatNavyBlueColorDark")]
        UIColor FlatNavyBlueColorDark { get; }

        // +(UIColor *)flatOrangeColorDark;
        [Static]
        [Export("flatOrangeColorDark")]
        UIColor FlatOrangeColorDark { get; }

        // +(UIColor *)flatPinkColorDark;
        [Static]
        [Export("flatPinkColorDark")]
        UIColor FlatPinkColorDark { get; }

        // +(UIColor *)flatPlumColorDark;
        [Static]
        [Export("flatPlumColorDark")]
        UIColor FlatPlumColorDark { get; }

        // +(UIColor *)flatPowderBlueColorDark;
        [Static]
        [Export("flatPowderBlueColorDark")]
        UIColor FlatPowderBlueColorDark { get; }

        // +(UIColor *)flatPurpleColorDark;
        [Static]
        [Export("flatPurpleColorDark")]
        UIColor FlatPurpleColorDark { get; }

        // +(UIColor *)flatRedColorDark;
        [Static]
        [Export("flatRedColorDark")]
        UIColor FlatRedColorDark { get; }

        // +(UIColor *)flatSandColorDark;
        [Static]
        [Export("flatSandColorDark")]
        UIColor FlatSandColorDark { get; }

        // +(UIColor *)flatSkyBlueColorDark;
        [Static]
        [Export("flatSkyBlueColorDark")]
        UIColor FlatSkyBlueColorDark { get; }

        // +(UIColor *)flatTealColorDark;
        [Static]
        [Export("flatTealColorDark")]
        UIColor FlatTealColorDark { get; }

        // +(UIColor *)flatWatermelonColorDark;
        [Static]
        [Export("flatWatermelonColorDark")]
        UIColor FlatWatermelonColorDark { get; }

        // +(UIColor *)flatWhiteColorDark;
        [Static]
        [Export("flatWhiteColorDark")]
        UIColor FlatWhiteColorDark { get; }

        // +(UIColor *)flatYellowColorDark;
        [Static]
        [Export("flatYellowColorDark")]
        UIColor FlatYellowColorDark { get; }

        // +(UIColor *)randomFlatColor;
        [Static]
        [Export("randomFlatColor")]
        UIColor RandomFlatColor { get; }

        // +(UIColor *)colorWithRandomFlatColorExcludingColorsInArray:(NSArray *)colors;
        [Static]
        [Export("colorWithRandomFlatColorExcludingColorsInArray:")]
        UIColor GetRandomColor(NSObject[] colors);

        // +(UIColor *)colorWithRandomColorInArray:(NSArray *)colors;
        [Static]
        [Export("colorWithRandomColorInArray:")]
        UIColor GetRandomColor(IntPtr colors);

        // +(UIColor *)colorWithRandomFlatColorOfShadeStyle:(UIShadeStyle)shadeStyle;
        [Static]
        [Export("colorWithRandomFlatColorOfShadeStyle:")]
        UIColor GetRandomColor(ShadeStyle shadeStyle);

        // +(UIColor *)colorWithRandomFlatColorOfShadeStyle:(UIShadeStyle)shadeStyle withAlpha:(CGFloat)alpha;
        [Static]
        [Export("colorWithRandomFlatColorOfShadeStyle:withAlpha:")]
        UIColor GetRandomColor(ShadeStyle shadeStyle, nfloat alpha);

        // +(UIColor *)colorWithAverageColorFromImage:(UIImage *)image;
        [Static]
        [Export("colorWithAverageColorFromImage:")]
        UIColor GetImageAverageColor(UIImage image);

        // +(UIColor *)colorWithAverageColorFromImage:(UIImage *)image withAlpha:(CGFloat)alpha;
        [Static]
        [Export("colorWithAverageColorFromImage:withAlpha:")]
        UIColor GetImageAverageColor(UIImage image, nfloat alpha);

        // +(UIColor *)colorWithComplementaryFlatColorOf:(UIColor *)color;
        [Static]
        [Export("colorWithComplementaryFlatColorOf:")]
        UIColor GetComplementaryColor(UIColor color);

        // +(UIColor *)colorWithComplementaryFlatColorOf:(UIColor *)color withAlpha:(CGFloat)alpha;
        [Static]
        [Export("colorWithComplementaryFlatColorOf:withAlpha:")]
        UIColor GetComplementaryColor(UIColor color, nfloat alpha);

        // +(UIColor *)colorWithContrastingBlackOrWhiteColorOn:(UIColor *)backgroundColor isFlat:(BOOL)flat;
        [Static]
        [Export("colorWithContrastingBlackOrWhiteColorOn:isFlat:")]
        UIColor GetContrastingBlackOrWhiteColor(UIColor backgroundColor, bool flat);

        // +(UIColor *)colorWithContrastingBlackOrWhiteColorOn:(UIColor *)backgroundColor isFlat:(BOOL)flat alpha:(CGFloat)alpha;
        [Static]
        [Export("colorWithContrastingBlackOrWhiteColorOn:isFlat:alpha:")]
        UIColor GetContrastingBlackOrWhiteColor(UIColor backgroundColor, bool flat, nfloat alpha);

        // +(UIColor *)colorWithGradientStyle:(UIGradientStyle)gradientStyle withFrame:(CGRect)frame andColors:(NSArray *)colors;
        [Static]
        [Export("colorWithGradientStyle:withFrame:andColors:")]
        UIColor GetGradientColor(GradientStyle gradientStyle, CGRect frame, UIColor[] colors);

        // +(UIColor *)colorWithHexString:(NSString *)string;
        [Static]
        [Export("colorWithHexString:")]
        UIColor GetColor(string @string);

        // +(UIColor *)colorWithHexString:(NSString *)string withAlpha:(CGFloat)alpha;
        [Static]
        [Export("colorWithHexString:withAlpha:")]
        UIColor GetColor(string @string, nfloat alpha);

        // deprecated: "Use -flatten: instead (First deprecated in Chameleon 2.0)."
        //		// +(UIColor *)colorWithFlatVersionOf:(UIColor *)color __attribute__((deprecated(" Use -flatten: instead (First deprecated in Chameleon 2.0).")));
        //		[Static]
        //		[Export ("colorWithFlatVersionOf:")]
        //		UIColor ColorWithFlatVersionOf (UIColor color);       
    }

    // @interface Chameleon (UIColor)
    [Category]
    [BaseType(typeof(UIColor))]
    interface UIColorExtensions
    {
        // @property (nonatomic, strong) UIImage * gradientImage;
        [Export("gradientImage")]
        UIImage GetGradientImage();

        // @property (nonatomic, strong) UIImage * gradientImage;
        [Export("setGradientImage:")]
        void SetGradientImage(UIImage value);

        // -(UIColor *)flatten;
        [Export("flatten")]
        UIColor Flatten();

        // -(UIColor *)darkenByPercentage:(CGFloat)percentage;
        [Export("darkenByPercentage:")]
        UIColor DarkenByPercentage(nfloat percentage);

        // -(UIColor *)lightenByPercentage:(CGFloat)percentage;
        [Export("lightenByPercentage:")]
        UIColor LightenByPercentage(nfloat percentage);
    }

    // @interface Chameleon (NSArray)
    [Static]
    [BaseType(typeof(NSObject), Name = "NSArray")]
    interface ChameleonColorArray
    {
        // +(NSArray *)arrayOfColorsWithColorScheme:(ColorScheme)colorScheme usingColor:(UIColor *)color withFlatScheme:(BOOL)isFlatScheme;
        [Static]
        [Export("arrayOfColorsWithColorScheme:usingColor:withFlatScheme:")]
        UIColor[] GetColors(ColorScheme colorScheme, UIColor color, bool isFlatScheme);

        // +(NSArray *)arrayOfColorsFromImage:(UIImage *)image withFlatScheme:(BOOL)isFlatScheme;
        [Static]
        [Export("arrayOfColorsFromImage:withFlatScheme:")]
        UIColor[] GetColors(UIImage image, bool isFlatScheme);

        // deprecated: "Use -arrayOfColorsWithColorScheme:usingColor:withFlatScheme: instead (First deprecated in Chameleon 2.0)."
        //		// +(NSArray *)arrayOfColorsWithColorScheme:(ColorScheme)colorScheme with:(UIColor *)color flatScheme:(BOOL)isFlatScheme __attribute__((deprecated(" Use -arrayOfColorsWithColorScheme:usingColor:withFlatScheme: instead (First deprecated in Chameleon 2.0).")));
        //		[Static]
        //		[Export ("arrayOfColorsWithColorScheme:with:flatScheme:")]
        //		[Verify (StronglyTypedNSArray)]
        //		NSObject[] ArrayOfColorsWithColorScheme (ColorScheme colorScheme, UIColor color, bool isFlatScheme);
    }

    // @interface Chameleon (UINavigationController)
    [Category]
    [BaseType(typeof(UINavigationController))]
    interface UINavigationControllerExtensions
    {
        // -(void)setStatusBarStyle:(UIStatusBarStyle)statusBarStyle;
        [Export("setStatusBarStyle:")]
        void SetStatusBarStyle(StatusBarStyle statusBarStyle);

        // @property (assign, nonatomic) BOOL hidesNavigationBarHairline;
        [Export("hidesNavigationBarHairline")]
        bool HidesNavigationBarHairline();

        // @property (assign, nonatomic) BOOL hidesNavigationBarHairline;
        [Export("setHidesNavigationBarHairline:")]
        void HideNavigationBarHairline(bool hide);
    }

    // @interface Chameleon (UIViewController)
    [Category]
    [BaseType(typeof(UIViewController))]
    interface UIViewControllerExtensions
    {
        // -(void)setThemeUsingPrimaryColor:(UIColor *)primaryColor withContentStyle:(UIContentStyle)contentStyle;
        [Export("setThemeUsingPrimaryColor:withContentStyle:")]
        void SetTheme(UIColor primaryColor, ContentStyle contentStyle);

        // -(void)setThemeUsingPrimaryColor:(UIColor *)primaryColor withSecondaryColor:(UIColor *)secondaryColor andContentStyle:(UIContentStyle)contentStyle;
        [Export("setThemeUsingPrimaryColor:withSecondaryColor:andContentStyle:")]
        void SetTheme(UIColor primaryColor, UIColor secondaryColor, ContentStyle contentStyle);

        // -(void)setThemeUsingPrimaryColor:(UIColor *)primaryColor withSecondaryColor:(UIColor *)secondaryColor usingFontName:(NSString *)fontName andContentStyle:(UIContentStyle)contentStyle;
        [Export("setThemeUsingPrimaryColor:withSecondaryColor:usingFontName:andContentStyle:")]
        void SetTheme(UIColor primaryColor, UIColor secondaryColor, string fontName, ContentStyle contentStyle);

        // -(void)setStatusBarStyle:(UIStatusBarStyle)statusBarStyle;
        [Export("setStatusBarStyle:")]
        void SetStatusBarStyle(StatusBarStyle statusBarStyle);
    }

    // @interface Chameleon (UIButton)
    [Category]
    [BaseType(typeof(UIButton))]
    interface UIButtonExtensions
    {
        // -(void)setSubstituteFontName:(NSString *)name __attribute__((annotate("ui_appearance_selector")));
        [Export("setSubstituteFontName:")]
        void SetFont(string name);
    }

    // @interface Chameleon (UILabel)
    [Category]
    [BaseType(typeof(UILabel))]
    interface UILabelExtensions
    {
        // -(void)setSubstituteFontName:(NSString *)name __attribute__((annotate("ui_appearance_selector")));
        [Export("setSubstituteFontName:")]
        void SetFont(string name);
    }

    // @interface ChameleonPrivate (UIColor)
    [Category]
    [BaseType(typeof(UIColor))]
    interface UIColor_ChameleonPrivate
    {
        // @property (readwrite, nonatomic) NSUInteger count;
        [Export("count")]
        nuint Count();

        // +(UIColor *)colorFromImage:(UIImage *)image atPoint:(CGPoint)point;
        [Static]
        [Export("colorFromImage:atPoint:")]
        UIColor ColorFromImage(UIImage image, CGPoint point);

        // -(UIColor *)colorWithMinimumSaturation:(CGFloat)saturation;
        [Export("colorWithMinimumSaturation:")]
        UIColor ColorWithMinimumSaturation(nfloat saturation);

        // -(BOOL)isDistinct:(UIColor *)color;
        [Export("isDistinct:")]
        bool IsDistinct(UIColor color);

        // -(BOOL)getValueForX:(CGFloat *)X valueForY:(CGFloat *)Y valueForZ:(CGFloat *)Z alpha:(CGFloat *)alpha;
        [Export("getValueForX:valueForY:valueForZ:alpha:")]
        unsafe bool GetValueForX(IntPtr X, IntPtr Y, IntPtr Z, IntPtr alpha);

        // -(BOOL)getLightness:(CGFloat *)L valueForA:(CGFloat *)A valueForB:(CGFloat *)B alpha:(CGFloat *)alpha;
        [Export("getLightness:valueForA:valueForB:alpha:")]
        unsafe bool GetLightness(IntPtr L, IntPtr A, IntPtr B, IntPtr alpha);
    }

    // @interface ChameleonPrivate (UIImage)
    [Category]
    [BaseType(typeof(UIImage))]
    interface UIImage_ChameleonPrivate
    {
        // +(UIImage *)imageWithImage:(UIImage *)image scaledToSize:(CGSize)newSize;
        [Static]
        [Export("imageWithImage:scaledToSize:")]
        UIImage ImageWithImage(UIImage image, CGSize newSize);

        // -(UIImage *)imageScaledToSize:(CGSize)newSize;
        [Export("imageScaledToSize:")]
        UIImage ImageScaledToSize(CGSize newSize);
    }

    // @interface ChameleonPrivate (UIView)
    [Category]
    [BaseType(typeof(UIView))]
    interface UIView_ChameleonPrivate
    {
        // -(BOOL)isTopViewInWindow;
        [Export("isTopViewInWindow")]
        bool IsTopViewInWindow();

        // -(UIView *)findTopMostViewForPoint:(CGPoint)point;
        [Export("findTopMostViewForPoint:")]
        UIView FindTopMostViewForPoint(CGPoint point);
    }

    // @interface UIViewAppearance_Swift (UIView)
    [Category]
    [BaseType(typeof(UIView))]
    interface UIView_UIViewAppearance_Swift
    {
        // +(instancetype)appearanceWhenContainedWithin:(NSArray *)containers;
        [Static]
        [Export("appearanceWhenContainedWithin:")]

        UIView AppearanceWhenContainedWithin(NSObject[] containers);
    }
}
