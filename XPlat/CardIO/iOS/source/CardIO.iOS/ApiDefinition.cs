using System;

using Foundation;
using UIKit;
using ObjCRuntime;
using CoreGraphics;

namespace Card.IO 
{
    // @interface CardIOCreditCardInfo : NSObject <NSCopying>
    [BaseType (typeof (NSObject), Name="CardIOCreditCardInfo")]
    interface CreditCardInfo : INSCopying {

        // @property (readwrite, copy, nonatomic) NSString * cardNumber;
        [Export ("cardNumber")]
        string CardNumber { get; set; }

        // @property (readonly, copy, nonatomic) NSString * redactedCardNumber;
        [Export ("redactedCardNumber")]
        string RedactedCardNumber { get; }

        // @property (assign, readwrite, nonatomic) NSUInteger expiryMonth;
        [Export ("expiryMonth", ArgumentSemantic.UnsafeUnretained)]
        nuint ExpiryMonth { get; set; }

        // @property (assign, readwrite, nonatomic) NSUInteger expiryYear;
        [Export ("expiryYear", ArgumentSemantic.UnsafeUnretained)]
        nuint ExpiryYear { get; set; }

        // @property (readwrite, copy, nonatomic) NSString * cvv;
        [Export ("cvv")]
        string Cvv { get; set; }

        // @property (readwrite, copy, nonatomic) NSString * postalCode;
        [Export ("postalCode")]
        string PostalCode { get; set; }

        /// Cardholder Name.
        /// @note May be nil, if cardholder name was not requested.
        // @property(nonatomic, copy, readwrite) NSString *cardholderName;
        [Export ("cardholderName")]
        string CardholderName { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL scanned;
        [Export ("scanned", ArgumentSemantic.UnsafeUnretained)]
        bool Scanned { get; set; }

        // @property (readwrite, nonatomic, strong) UIImage * cardImage;
        [Export ("cardImage", ArgumentSemantic.Retain)]
        UIImage CardImage { get; set; }

        // @property (readonly, assign, nonatomic) CardIOCreditCardType cardType;
        [Export ("cardType", ArgumentSemantic.UnsafeUnretained)]
        CreditCardType CardType { get; }

        // +(NSString *)displayStringForCardType:(CardIOCreditCardType)cardType usingLanguageOrLocale:(NSString *)languageOrLocale;
        [Static, Export ("displayStringForCardType:usingLanguageOrLocale:")]
        string GetDisplayString (CreditCardType cardType, string languageOrLocale);

        // +(UIImage *)logoForCardType:(CardIOCreditCardType)cardType;
        [Static, Export ("logoForCardType:")]
        UIImage GetLogo (CreditCardType cardType);
    }

    // @protocol CardIOViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface CardIOViewDelegate {

        // @required -(void)cardIOView:(CardIOView *)cardIOView didScanCard:(CardIOCreditCardInfo *)cardInfo;
        [Export ("cardIOView:didScanCard:")]
        [Abstract]
        void DidScanCard (CardIOView cardIOView, CreditCardInfo cardInfo);
    }

    // @interface CardIOView : UIView
    [BaseType (typeof (UIView))]
    interface CardIOView {

        // @property (readwrite, nonatomic, weak) id<CardIOViewDelegate> delegate;
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        NSObject WeakDelegate { get; set; }

        // @property (readwrite, nonatomic, weak) id<CardIOViewDelegate> delegate;
        [Wrap ("WeakDelegate")]
        CardIOViewDelegate Delegate { get; set; }

        // @property (readwrite, copy, nonatomic) NSString * languageOrLocale;
        [Export ("languageOrLocale")]
        string LanguageOrLocale { get; set; }

        // @property (readwrite, retain, nonatomic) UIColor * guideColor;
        [Export ("guideColor", ArgumentSemantic.Retain)]
        UIColor GuideColor { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL useCardIOLogo;
        [Export ("useCardIOLogo", ArgumentSemantic.UnsafeUnretained)]
        bool UseCardIOLogo { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL hideCardIOLogo;
        [Export ("hideCardIOLogo", ArgumentSemantic.UnsafeUnretained)]
        bool HideCardIOLogo { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL allowFreelyRotatingCardGuide;
        [Export ("allowFreelyRotatingCardGuide", ArgumentSemantic.UnsafeUnretained)]
        bool AllowFreelyRotatingCardGuide { get; set; }

        // @property (readwrite, copy, nonatomic) NSString * scanInstructions;
        [Export ("scanInstructions")]
        string ScanInstructions { get; set; }

        // @property (readwrite, retain, nonatomic) UIView * scanOverlayView;
        [Export ("scanOverlayView", ArgumentSemantic.Retain)]
        UIView ScanOverlayView { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL scanExpiry;
        [Export ("scanExpiry", ArgumentSemantic.UnsafeUnretained)]
        bool ScanExpiry { get; set; }

        // @property (assign, readwrite, nonatomic) CardIODetectionMode detectionMode;
        [Export ("detectionMode", ArgumentSemantic.UnsafeUnretained)]
        DetectionMode DetectionMode { get; set; }

        // @property (assign, readwrite, nonatomic) CGFloat scannedImageDuration;
        [Export ("scannedImageDuration", ArgumentSemantic.UnsafeUnretained)]
        nfloat ScannedImageDuration { get; set; }

        // @property (readonly, assign, nonatomic) CGRect cameraPreviewFrame;
        [Export ("cameraPreviewFrame", ArgumentSemantic.UnsafeUnretained)]
        CGRect CameraPreviewFrame { get; }
    }

    // @protocol CardIOPaymentViewControllerDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject))]
    interface CardIOPaymentViewControllerDelegate {

        // @required -(void)userDidCancelPaymentViewController:(CardIOPaymentViewController *)paymentViewController;
        [Export ("userDidCancelPaymentViewController:")]
        [Abstract]
        void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController);

        // @required -(void)userDidProvideCreditCardInfo:(CardIOCreditCardInfo *)cardInfo inPaymentViewController:(CardIOPaymentViewController *)paymentViewController;
        [Export ("userDidProvideCreditCardInfo:inPaymentViewController:")]
        [Abstract]
        void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController);
    }

    partial interface ICardIOPaymentViewControllerDelegate { }

    // @interface CardIOPaymentViewController : UINavigationController
    [BaseType (typeof (UINavigationController))]
    interface CardIOPaymentViewController {

        // -(id)initWithPaymentDelegate:(id<CardIOPaymentViewControllerDelegate>)aDelegate;
        [Export ("initWithPaymentDelegate:")]
        IntPtr Constructor (ICardIOPaymentViewControllerDelegate aDelegate);

        // -(id)initWithPaymentDelegate:(id<CardIOPaymentViewControllerDelegate>)aDelegate scanningEnabled:(BOOL)scanningEnabled;
        [Export ("initWithPaymentDelegate:scanningEnabled:")]
        IntPtr Constructor (ICardIOPaymentViewControllerDelegate aDelegate, bool scanningEnabled);

        // @property (readwrite, copy, nonatomic) NSString * languageOrLocale;
        [Export ("languageOrLocale")]
        string LanguageOrLocale { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL keepStatusBarStyle;
        [Export ("keepStatusBarStyle", ArgumentSemantic.UnsafeUnretained)]
        bool KeepStatusBarStyle { get; set; }

        // @property (assign, readwrite, nonatomic) UIBarStyle navigationBarStyle;
        [Export ("navigationBarStyle", ArgumentSemantic.UnsafeUnretained)]
        UIBarStyle NavigationBarStyle { get; set; }

        // @property (readwrite, retain, nonatomic) UIColor * navigationBarTintColor;
        [Export ("navigationBarTintColor", ArgumentSemantic.Retain)]
        UIColor NavigationBarTintColor { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL disableBlurWhenBackgrounding;
        [Export ("disableBlurWhenBackgrounding", ArgumentSemantic.UnsafeUnretained)]
        bool DisableBlurWhenBackgrounding { get; set; }

        // @property (readwrite, retain, nonatomic) UIColor * guideColor;
        [Export ("guideColor", ArgumentSemantic.Retain)]
        UIColor GuideColor { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL suppressScanConfirmation;
        [Export ("suppressScanConfirmation", ArgumentSemantic.UnsafeUnretained)]
        bool SuppressScanConfirmation { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL suppressScannedCardImage;
        [Export ("suppressScannedCardImage", ArgumentSemantic.UnsafeUnretained)]
        bool SuppressScannedCardImage { get; set; }

        /// After a successful scan, card.io will display an image of the card with
        /// the computed card number superimposed. This property controls how long (in seconds)
        /// that image will be displayed.
        /// Set this to 0.0 to suppress the display entirely.
        /// Defaults to 0.1.
        // @property(nonatomic, assign, readwrite) CGFloat scannedImageDuration;
        [Export ("scannedImageDuration")]
        nfloat ScannedImageDuration { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL maskManualEntryDigits;
        [Export ("maskManualEntryDigits", ArgumentSemantic.UnsafeUnretained)]
        bool MaskManualEntryDigits { get; set; }

        // @property (readwrite, copy, nonatomic) NSString * scanInstructions;
        [Export ("scanInstructions")]
        string ScanInstructions { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL hideCardIOLogo;
        [Export ("hideCardIOLogo", ArgumentSemantic.UnsafeUnretained)]
        bool HideCardIOLogo { get; set; }

        // @property (readwrite, retain, nonatomic) UIView * scanOverlayView;
        [Export ("scanOverlayView", ArgumentSemantic.Retain)]
        UIView ScanOverlayView { get; set; }

        // @property (assign, readwrite, nonatomic) CardIODetectionMode detectionMode;
        [Export ("detectionMode", ArgumentSemantic.UnsafeUnretained)]
        DetectionMode DetectionMode { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL collectExpiry;
        [Export ("collectExpiry", ArgumentSemantic.UnsafeUnretained)]
        bool CollectExpiry { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL collectCVV;
        [Export ("collectCVV", ArgumentSemantic.UnsafeUnretained)]
        bool CollectCVV { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL collectPostalCode;
        [Export ("collectPostalCode", ArgumentSemantic.UnsafeUnretained)]
        bool CollectPostalCode { get; set; }

        // @property(nonatomic, assign, readwrite) BOOL restrictPostalCodeToNumericOnly;
        [Export ("restrictPostalCodeToNumericOnly", ArgumentSemantic.UnsafeUnretained)]
        bool RestrictPostalCodeToNumericOnly { get; set; }

        /// Set to YES if you need to collect the cardholder name. Defaults to NO.
        // @property(nonatomic, assign, readwrite) BOOL collectCardholderName;
        [Export ("collectCardholderName", ArgumentSemantic.UnsafeUnretained)]
        bool CollectCardholderName { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL scanExpiry;
        [Export ("scanExpiry", ArgumentSemantic.UnsafeUnretained)]
        bool ScanExpiry { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL useCardIOLogo;
        [Export ("useCardIOLogo", ArgumentSemantic.UnsafeUnretained)]
        bool UseCardIOLogo { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL allowFreelyRotatingCardGuide;
        [Export ("allowFreelyRotatingCardGuide", ArgumentSemantic.UnsafeUnretained)]
        bool AllowFreelyRotatingCardGuide { get; set; }

        // @property (assign, readwrite, nonatomic) BOOL disableManualEntryButtons;
        [Export ("disableManualEntryButtons", ArgumentSemantic.UnsafeUnretained)]
        bool DisableManualEntryButtons { get; set; }

        // @property (readwrite, nonatomic, weak) id<CardIOPaymentViewControllerDelegate> paymentDelegate;
        [Export ("paymentDelegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        NSObject WeakPaymentDelegate { get; set; }

        // @property (readwrite, nonatomic, weak) id<CardIOPaymentViewControllerDelegate> paymentDelegate;
        [Wrap ("WeakPaymentDelegate")]
        CardIOPaymentViewControllerDelegate PaymentDelegate { get; set; }
    }

    /// Methods with names that do not conflict with Apple's private APIs.
    // @interface CardIOPaymentViewController (NonConflictingAPINames)
    [Category, BaseType (typeof (CardIOPaymentViewController))]
    interface CardIOPaymentViewController_NonConflictingAPINames
    {
        // @property (nonatomic, assign, readwrite) BOOL keepStatusBarStyleForCardIO;
        [Export ("keepStatusBarStyleForCardIO")]
        bool GetKeepStatusBarStyleForCardIO ();

        [Export ("setKeepStatusBarStyleForCardIO:")]
        void SetKeepStatusBarStyleForCardIO (bool keep);

        // @property (nonatomic, assign, readwrite) UIBarStyle navigationBarStyleForCardIO;
        [Export ("navigationBarStyleForCardIO")]
        UIBarStyle GetNavigationBarStyleForCardIO ();

        [Export ("setNavigationBarStyleForCardIO:")]
        void SetNavigationBarStyleForCardIO (UIBarStyle navigationBarStyle);

        // @property (nonatomic, retain, readwrite) UIColor* navigationBarTintColorForCardIO;
        [Export ("navigationBarTintColorForCardIO", ArgumentSemantic.Retain)]
        UIColor NavigationBarTintColorForCardIO ();

        [Export ("setNavigationBarTintColorForCardIO:")]
        void SetNavigationBarTintColorForCardIO (UIColor tintColor);
    }

    // @interface CardIOUtilities : NSObject
    [BaseType (typeof (NSObject), Name="CardIOUtilities")]
    interface Utilities {

        // +(NSString *)libraryVersion;
        [Static, Export ("libraryVersion")]
        string LibraryVersion ();

        // +(BOOL)canReadCardWithCamera;
        [Static, Export ("canReadCardWithCamera")]
        bool CanReadCardWithCamera ();

        // +(void)preload;
        [Static, Export ("preload")]
        void Preload ();

        // +(UIImageView *)blurredScreenImageView;
        [Static, Export ("blurredScreenImageView")]
        UIImageView BlurredScreenImageView ();


        // Methods with names that do not conflict with Apple's private APIs.
        //@interface CardIOUtilities (NonConflictingAPINames)

        // + (NSString*)cardIOLibraryVersion;
        [Static, Export ("cardIOLibraryVersion")]
        string CardIOLibraryVersion { get; }

        // + (void)preloadCardIO;
        [Static, Export ("preloadCardIO")]
        void PreloadCardIO ();
    }
}



