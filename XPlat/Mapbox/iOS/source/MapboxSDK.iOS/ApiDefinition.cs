using System;
using CoreAnimation;
using CoreGraphics;
using CoreLocation;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Mapbox
{
    [Static]
    partial interface Constants
    {
        // extern double MapboxVersionNumber;
        [Field ("MapboxVersionNumber", "__Internal")]
        double MapboxVersionNumber { get; }

        // extern NSString *const _Nonnull MGLErrorDomain;
        [Field ("MGLErrorDomain", "__Internal")]
        NSString ErrorDomain { get; }
    }

    [Static]
    partial interface OfflinePackConstants
    {
        [Field ("MGLOfflinePackProgressChangedNotification", "__Internal")]
        NSString ProgressChangedNotification { get; }

        [Field ("MGLOfflinePackErrorNotification", "__Internal")]
        NSString ErrorNotification { get; }

        [Field ("MGLOfflinePackMaximumMapboxTilesReachedNotification", "__Internal")]
        NSString MaximumMapboxTilesReachedNotification { get; }

        [Field ("MGLOfflinePackUserInfoKeyState", "__Internal")]
        NSString UserInfoKeyState { get; }

        [Field ("MGLOfflinePackStateUserInfoKey", "__Internal")]
        NSString StateUserInfoKey { get; }

        [Field ("MGLOfflinePackUserInfoKeyProgress", "__Internal")]
        NSString UserInfoKeyProgress { get; }

        [Field ("MGLOfflinePackProgressUserInfoKey", "__Internal")]
        NSString ProgressUserInfoKey { get; }

        [Field ("MGLOfflinePackUserInfoKeyError", "__Internal")]
        NSString UserInfoKeyError { get; }

        [Field ("MGLOfflinePackErrorUserInfoKey", "__Internal")]
        NSString ErrorUserInfoKey { get; }

        [Field ("MGLOfflinePackUserInfoKeyMaximumCount", "__Internal")]
        NSString UserInfoKeyMaximumCount { get; }

        [Field ("MGLOfflinePackMaximumCountUserInfoKey", "__Internal")]
        NSString MaximumCountUserInfoKey { get; }
    }

    [Static]
    //[Verify (ConstantsInterfaceAssociation)]
    partial interface MapViewDecelerationRateConstants
    {
        // extern const CGFloat MGLMapViewDecelerationRateNormal;
        [Field ("MGLMapViewDecelerationRateNormal", "__Internal")]
        nfloat Normal { get; }

        // extern const CGFloat MGLMapViewDecelerationRateFast;
        [Field ("MGLMapViewDecelerationRateFast", "__Internal")]
        nfloat Fast { get; }

        // extern const CGFloat MGLMapViewDecelerationRateImmediate;
        [Field ("MGLMapViewDecelerationRateImmediate", "__Internal")]
        nfloat Immediate { get; }
    }

    // typedef void (^MGLOfflinePackAdditionCompletionHandler)(MGLOfflinePack * _Nullable, NSError * _Nullable);
    delegate void OfflinePackAdditionCompletion ([NullAllowed] OfflinePack pack, [NullAllowed] NSError error);

    // typedef void (^MGLOfflinePackRemovalCompletionHandler)(NSError * _Nullable);
    delegate void OfflinePackRemovalCompletion ([NullAllowed] NSError error);


    // @interface MGLAccountManager : NSObject
    [BaseType (typeof (NSObject), Name = "MGLAccountManager")]
    interface AccountManager
    {
        // +(NSString * _Nullable)accessToken;
        // +(void)setAccessToken:(NSString * _Nullable)accessToken;
        [Static]
        [NullAllowed, Export ("accessToken")]
        //[Verify (MethodToProperty)]
        string AccessToken { [NullAllowed, Export ("accessToken")]get; [NullAllowed, Export ("setAccessToken:")]set; }

        // +(BOOL)mapboxMetricsEnabledSettingShownInApp __attribute__((deprecated("Telemetry settings are now always shown in the ℹ️ menu.")));
        [Obsolete("Telemetry settings are now always shown in the ℹ️ menu.")]
        [Static]
        [Export ("mapboxMetricsEnabledSettingShownInApp")]
        //[Verify (MethodToProperty)]
        bool MetricsEnabledSettingShownInApp { get; }
    }

    interface IAnnotation { }

    // @protocol MGLAnnotation <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "MGLAnnotation")]
    interface Annotation
    {
        // @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
        [Abstract]
        [Export ("coordinate")]
        CLLocationCoordinate2D Coordinate { get; set; }

        // @optional @property (readonly, copy, nonatomic) NSString * _Nullable title;
        [NullAllowed, Export ("title")]
        string Title { get; set; }

        // @optional @property (readonly, copy, nonatomic) NSString * _Nullable subtitle;
        [NullAllowed, Export ("subtitle")]
        string Subtitle { get; set; }
    }

    // @interface MGLAnnotationImage : NSObject <NSSecureCoding>
    [BaseType (typeof (NSObject), Name = "MGLAnnotationImage")]
    interface AnnotationImage : INSSecureCoding
    {
        // +(instancetype _Nonnull)annotationImageWithImage:(UIImage * _Nonnull)image reuseIdentifier:(NSString * _Nonnull)reuseIdentifier;
        [Static]
        [Export ("annotationImageWithImage:reuseIdentifier:")]
        AnnotationImage Create (UIImage image, string reuseIdentifier);

        // @property (nonatomic, strong) UIImage * _Nullable image;
        [NullAllowed, Export ("image", ArgumentSemantic.Strong)]
        UIImage Image { get; set; }

        // @property (readonly, nonatomic) NSString * _Nonnull reuseIdentifier;
        [Export ("reuseIdentifier")]
        string ReuseIdentifier { get; }

        // @property (getter = isEnabled, nonatomic) BOOL enabled;
        [Export ("enabled")]
        bool Enabled { [Bind ("isEnabled")] get; set; }
    }

    // @interface MGLAnnotationView : UIView <NSSecureCoding>
    [BaseType (typeof (UIView), Name = "MGLAnnotationView")]
    interface AnnotationView : INSSecureCoding
    {
        // -(instancetype _Nonnull)initWithReuseIdentifier:(NSString * _Nullable)reuseIdentifier;
        [Export ("initWithReuseIdentifier:")]
        IntPtr Constructor ([NullAllowed] string reuseIdentifier);

        // -(void)prepareForReuse;
        [Export ("prepareForReuse")]
        void PrepareForReuse ();

        // @property (nonatomic) id<MGLAnnotation> _Nullable annotation;
        [NullAllowed, Export ("annotation", ArgumentSemantic.Assign)]
        Annotation Annotation { get; set; }

        // @property (readonly, nonatomic) NSString * _Nullable reuseIdentifier;
        [NullAllowed, Export ("reuseIdentifier")]
        string ReuseIdentifier { get; }

        // @property (nonatomic) CGVector centerOffset;
        [Export ("centerOffset", ArgumentSemantic.Assign)]
        CGVector CenterOffset { get; set; }

        // @property (assign, nonatomic) BOOL scalesWithViewingDistance;
        [Export ("scalesWithViewingDistance")]
        bool ScalesWithViewingDistance { get; set; }

        // @property (getter = isSelected, assign, nonatomic) BOOL selected;
        [Export ("selected")]
        bool Selected { [Bind ("isSelected")] get; set; }

        // -(void)setSelected:(BOOL)selected animated:(BOOL)animated;
        [Export ("setSelected:animated:")]
        void SetSelected (bool selected, bool animated);

        // @property (getter = isEnabled, assign, nonatomic) BOOL enabled;
        [Export ("enabled")]
        bool Enabled { [Bind ("isEnabled")] get; set; }

        // @property (getter = isDraggable, assign, nonatomic) BOOL draggable;
        [Export ("draggable")]
        bool Draggable { [Bind ("isDraggable")] get; set; }

        // @property (readonly, nonatomic) MGLAnnotationViewDragState dragState;
        [Export ("dragState")]
        AnnotationViewDragState DragState { get; }

        // -(void)setDragState:(MGLAnnotationViewDragState)dragState animated:(BOOL)animated __attribute__((objc_requires_super));
        [Export ("setDragState:animated:")]
        //[RequiresSuper]
        void SetDragState (AnnotationViewDragState dragState, bool animated);
    }


    // @interface MGLMapView : UIView
    [BaseType (typeof (UIView), Name = "MGLMapView")]
    interface MapView
    {
        // -(instancetype _Nonnull)initWithFrame:(CGRect)frame;
        [Export ("initWithFrame:")]
        IntPtr Constructor (CGRect frame);

        // -(instancetype _Nonnull)initWithFrame:(CGRect)frame styleURL:(NSURL * _Nullable)styleURL;
        [Export ("initWithFrame:styleURL:")]
        IntPtr Constructor (CGRect frame, [NullAllowed] NSUrl styleURL);

        // @property (nonatomic, weak) id<MGLMapViewDelegate> _Nullable delegate __attribute__((iboutlet));
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        IMapViewDelegate Delegate { get; set; }

        // @property (readonly, nonatomic) MGLStyle * _Nullable style;
        [NullAllowed, Export ("style")]
        Style Style { get; }

        // @property (readonly, nonatomic) NSArray<NSURL *> * _Nonnull bundledStyleURLs __attribute__((deprecated("Call the relevant class method of MGLStyle for the URL of a particular default style.")));
        [Obsolete ("Call the relevant class method of Style for the URL of a particular default style.")]
        [Export ("bundledStyleURLs")]
        NSUrl[] BundledStyleURLs { get; }

        // @property (nonatomic) NSURL * _Null_unspecified styleURL;
        [Export ("styleURL", ArgumentSemantic.Assign)]
        NSUrl StyleURL { get; set; }

        // -(void)reloadStyle:(id _Nonnull)sender __attribute__((ibaction));
        [Export ("reloadStyle:")]
        void ReloadStyle (NSObject sender);

        // @property (readonly, nonatomic) UIImageView * _Nonnull compassView;
        [Export ("compassView")]
        UIImageView CompassView { get; }

        // @property (readonly, nonatomic) UIImageView * _Nonnull logoView;
        [Export ("logoView")]
        UIImageView LogoView { get; }

        // @property (readonly, nonatomic) UIButton * _Nonnull attributionButton;
        [Export ("attributionButton")]
        UIButton AttributionButton { get; }

        // @property (nonatomic) NSArray<NSString *> * _Nonnull styleClasses __attribute__((deprecated("Use style.styleClasses.")));
        [Obsolete ("Use Style.StyleClasses")]
        [Export ("styleClasses", ArgumentSemantic.Assign)]
        string[] StyleClasses { get; set; }

        // -(BOOL)hasStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("Use style.hasStyleClass:.")));
        [Obsolete ("Use Style.HasStyleClass")]
        [Export ("hasStyleClass:")]
        bool HasStyleClass (string styleClass);

        // -(void)addStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("Use style.addStyleClass:.")));
        [Obsolete ("Use Style.addStyleClass")]
        [Export ("addStyleClass:")]
        void AddStyleClass (string styleClass);

        // -(void)removeStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("Use style.removeStyleClass:.")));
        [Obsolete ("Use Style.RemoveStyleClass")]
        [Export ("removeStyleClass:")]
        void RemoveStyleClass (string styleClass);

        // @property (assign, nonatomic) BOOL showsUserLocation;
        [Export ("showsUserLocation")]
        bool ShowsUserLocation { get; set; }

        // @property (readonly, getter = isUserLocationVisible, assign, nonatomic) BOOL userLocationVisible;
        [Export ("userLocationVisible")]
        bool UserLocationVisible { [Bind ("isUserLocationVisible")] get; }

        // @property (readonly, nonatomic) MGLUserLocation * _Nullable userLocation;
        [NullAllowed, Export ("userLocation")]
        UserLocation UserLocation { get; }

        // @property (assign, nonatomic) MGLUserTrackingMode userTrackingMode;
        [Export ("userTrackingMode", ArgumentSemantic.Assign)]
        UserTrackingMode UserTrackingMode { get; set; }

        // -(void)setUserTrackingMode:(MGLUserTrackingMode)mode animated:(BOOL)animated;
        [Export ("setUserTrackingMode:animated:")]
        void SetUserTrackingMode (UserTrackingMode mode, bool animated);

        // @property (assign, nonatomic) MGLAnnotationVerticalAlignment userLocationVerticalAlignment;
        [Export ("userLocationVerticalAlignment", ArgumentSemantic.Assign)]
        AnnotationVerticalAlignment UserLocationVerticalAlignment { get; set; }

        // -(void)setUserLocationVerticalAlignment:(MGLAnnotationVerticalAlignment)alignment animated:(BOOL)animated;
        [Export ("setUserLocationVerticalAlignment:animated:")]
        void SetUserLocationVerticalAlignment (AnnotationVerticalAlignment alignment, bool animated);

        // @property (assign, nonatomic) BOOL displayHeadingCalibration;
        [Export ("displayHeadingCalibration")]
        bool DisplayHeadingCalibration { get; set; }

        // @property (assign, nonatomic) CLLocationCoordinate2D targetCoordinate;
        [Export ("targetCoordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D TargetCoordinate { get; set; }

        // -(void)setTargetCoordinate:(CLLocationCoordinate2D)targetCoordinate animated:(BOOL)animated;
        [Export ("setTargetCoordinate:animated:")]
        void SetTargetCoordinate (CLLocationCoordinate2D targetCoordinate, bool animated);

        // @property (getter = isZoomEnabled, nonatomic) BOOL zoomEnabled;
        [Export ("zoomEnabled")]
        bool ZoomEnabled { [Bind ("isZoomEnabled")] get; set; }

        // @property (getter = isScrollEnabled, nonatomic) BOOL scrollEnabled;
        [Export ("scrollEnabled")]
        bool ScrollEnabled { [Bind ("isScrollEnabled")] get; set; }

        // @property (getter = isRotateEnabled, nonatomic) BOOL rotateEnabled;
        [Export ("rotateEnabled")]
        bool RotateEnabled { [Bind ("isRotateEnabled")] get; set; }

        // @property (getter = isPitchEnabled, nonatomic) BOOL pitchEnabled;
        [Export ("pitchEnabled")]
        bool PitchEnabled { [Bind ("isPitchEnabled")] get; set; }

        // @property (nonatomic) CGFloat decelerationRate;
        [Export ("decelerationRate")]
        nfloat DecelerationRate { get; set; }

        // @property (nonatomic) CLLocationCoordinate2D centerCoordinate;
        [Export ("centerCoordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D CenterCoordinate { get; set; }

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)coordinate animated:(BOOL)animated;
        [Export ("setCenterCoordinate:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D coordinate, bool animated);

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel animated:(BOOL)animated;
        [Export ("setCenterCoordinate:zoomLevel:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, bool animated);

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated;
        [Export ("setCenterCoordinate:zoomLevel:direction:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated);

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("setCenterCoordinate:zoomLevel:direction:animated:completionHandler:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated, [NullAllowed] Action completion);

        // @property (nonatomic) double zoomLevel;
        [Export ("zoomLevel")]
        double ZoomLevel { get; set; }

        // -(void)setZoomLevel:(double)zoomLevel animated:(BOOL)animated;
        [Export ("setZoomLevel:animated:")]
        void SetZoomLevel (double zoomLevel, bool animated);

        // @property (nonatomic) double minimumZoomLevel;
        [Export ("minimumZoomLevel")]
        double MinimumZoomLevel { get; set; }

        // @property (nonatomic) double maximumZoomLevel;
        [Export ("maximumZoomLevel")]
        double MaximumZoomLevel { get; set; }

        // @property (nonatomic) CLLocationDirection direction;
        [Export ("direction")]
        double Direction { get; set; }

        // -(void)setDirection:(CLLocationDirection)direction animated:(BOOL)animated;
        [Export ("setDirection:animated:")]
        void SetDirection (double direction, bool animated);

        // -(void)resetNorth __attribute__((ibaction));
        [Export ("resetNorth")]
        void ResetNorth ();

        // -(void)resetPosition __attribute__((ibaction));
        [Export ("resetPosition")]
        void ResetPosition ();

        // @property (nonatomic) MGLCoordinateBounds visibleCoordinateBounds;
        [Export ("visibleCoordinateBounds", ArgumentSemantic.Assign)]
        CoordinateBounds VisibleCoordinateBounds { get; set; }

        // -(void)setVisibleCoordinateBounds:(MGLCoordinateBounds)bounds animated:(BOOL)animated;
        [Export ("setVisibleCoordinateBounds:animated:")]
        void SetVisibleCoordinateBounds (CoordinateBounds bounds, bool animated);

        // -(void)setVisibleCoordinateBounds:(MGLCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("setVisibleCoordinateBounds:edgePadding:animated:")]
        void SetVisibleCoordinateBounds (CoordinateBounds bounds, UIEdgeInsets insets, bool animated);

        // -(void)setVisibleCoordinates:(const CLLocationCoordinate2D * _Nonnull)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("setVisibleCoordinates:count:edgePadding:animated:")]
        void SetVisibleCoordinates (IntPtr coordinates, nuint count, UIEdgeInsets insets, bool animated);

        // -(void)setVisibleCoordinates:(const CLLocationCoordinate2D * _Nonnull)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets direction:(CLLocationDirection)direction duration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("setVisibleCoordinates:count:edgePadding:direction:duration:animationTimingFunction:completionHandler:")]
        void SetVisibleCoordinates (IntPtr coordinates, nuint count, UIEdgeInsets insets, double direction, double duration, [NullAllowed] CAMediaTimingFunction function, [NullAllowed] Action completion);

        // -(void)showAnnotations:(NSArray<id<MGLAnnotation>> * _Nonnull)annotations animated:(BOOL)animated;
        [Export ("showAnnotations:animated:")]
        void ShowAnnotations (IAnnotation[] annotations, bool animated);

        // -(void)showAnnotations:(NSArray<id<MGLAnnotation>> * _Nonnull)annotations edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("showAnnotations:edgePadding:animated:")]
        void ShowAnnotations (IAnnotation[] annotations, UIEdgeInsets insets, bool animated);

        // @property (copy, nonatomic) MGLMapCamera * _Nonnull camera;
        [Export ("camera", ArgumentSemantic.Copy)]
        MapCamera Camera { get; set; }

        // -(void)setCamera:(MGLMapCamera * _Nonnull)camera animated:(BOOL)animated;
        [Export ("setCamera:animated:")]
        void SetCamera (MapCamera camera, bool animated);

        // -(void)setCamera:(MGLMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function;
        [Export ("setCamera:withDuration:animationTimingFunction:")]
        void SetCamera (MapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function);

        // -(void)setCamera:(MGLMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("setCamera:withDuration:animationTimingFunction:completionHandler:")]
        void SetCamera (MapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function, [NullAllowed] Action completion);

        // -(void)flyToCamera:(MGLMapCamera * _Nonnull)camera completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("flyToCamera:completionHandler:")]
        void FlyToCamera (MapCamera camera, [NullAllowed] Action completion);

        // -(void)flyToCamera:(MGLMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("flyToCamera:withDuration:completionHandler:")]
        void FlyToCamera (MapCamera camera, double duration, [NullAllowed] Action completion);

        // -(void)flyToCamera:(MGLMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration peakAltitude:(CLLocationDistance)peakAltitude completionHandler:(void (^ _Nullable)(void))completion;
        [Export ("flyToCamera:withDuration:peakAltitude:completionHandler:")]
        void FlyToCamera (MapCamera camera, double duration, double peakAltitude, [NullAllowed] Action completion);

        // -(MGLMapCamera * _Nonnull)cameraThatFitsCoordinateBounds:(MGLCoordinateBounds)bounds;
        [Export ("cameraThatFitsCoordinateBounds:")]
        MapCamera GetCameraThatFits (CoordinateBounds bounds);

        // -(MGLMapCamera * _Nonnull)cameraThatFitsCoordinateBounds:(MGLCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets;
        [Export ("cameraThatFitsCoordinateBounds:edgePadding:")]
        MapCamera GetCameraThatFits (CoordinateBounds bounds, UIEdgeInsets insets);

        // -(CGPoint)anchorPointForGesture:(UIGestureRecognizer * _Nonnull)gesture;
        [Export ("anchorPointForGesture:")]
        CGPoint GetAnchorPoint (UIGestureRecognizer gesture);

        // @property (assign, nonatomic) UIEdgeInsets contentInset;
        [Export ("contentInset", ArgumentSemantic.Assign)]
        UIEdgeInsets ContentInset { get; set; }

        // -(void)setContentInset:(UIEdgeInsets)contentInset animated:(BOOL)animated;
        [Export ("setContentInset:animated:")]
        void SetContentInset (UIEdgeInsets contentInset, bool animated);

        // -(CLLocationCoordinate2D)convertPoint:(CGPoint)point toCoordinateFromView:(UIView * _Nullable)view;
        [Export ("convertPoint:toCoordinateFromView:")]
        CLLocationCoordinate2D ConvertPoint (CGPoint point, [NullAllowed] UIView view);

        // -(CGPoint)convertCoordinate:(CLLocationCoordinate2D)coordinate toPointToView:(UIView * _Nullable)view;
        [Export ("convertCoordinate:toPointToView:")]
        CGPoint ConvertCoordinate (CLLocationCoordinate2D coordinate, [NullAllowed] UIView view);

        // -(MGLCoordinateBounds)convertRect:(CGRect)rect toCoordinateBoundsFromView:(UIView * _Nullable)view;
        [Export ("convertRect:toCoordinateBoundsFromView:")]
        CoordinateBounds ConvertRectToCoordinateBounds (CGRect rect, [NullAllowed] UIView view);

        // -(CGRect)convertCoordinateBounds:(MGLCoordinateBounds)bounds toRectToView:(UIView * _Nullable)view;
        [Export ("convertCoordinateBounds:toRectToView:")]
        CGRect ConvertCoordinateBoundsToRect (CoordinateBounds bounds, [NullAllowed] UIView view);

        // -(CLLocationDistance)metersPerPointAtLatitude:(CLLocationDegrees)latitude;
        [Export ("metersPerPointAtLatitude:")]
        double MetersPerPointAtLatitude (double latitude);

        // -(CLLocationDistance)metersPerPixelAtLatitude:(CLLocationDegrees)latitude __attribute__((deprecated("Use -metersPerPointAtLatitude:.")));
        [Obsolete ("Use MetersPerPointAtLatitude instead")]
        [Export ("metersPerPixelAtLatitude:")]
        double MetersPerPixelAtLatitude (double latitude);

        // @property (readonly, nonatomic) NSArray<id<MGLAnnotation>> * _Nullable annotations;
        [NullAllowed, Export ("annotations")]
        IAnnotation[] Annotations { get; }

        // @property (readonly, nonatomic) NSArray<id<MGLAnnotation>> * _Nullable visibleAnnotations;
        [NullAllowed, Export ("visibleAnnotations")]
        IAnnotation[] VisibleAnnotations { get; }

        // -(void)addAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("addAnnotation:")]
        void AddAnnotation (IAnnotation annotation);

        // -(void)addAnnotations:(NSArray<id<MGLAnnotation>> * _Nonnull)annotations;
        [Export ("addAnnotations:")]
        void AddAnnotations (IAnnotation[] annotations);

        // -(void)removeAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("removeAnnotation:")]
        void RemoveAnnotation (IAnnotation annotation);

        // -(void)removeAnnotations:(NSArray<id<MGLAnnotation>> * _Nonnull)annotations;
        [Export ("removeAnnotations:")]
        void RemoveAnnotations (IAnnotation[] annotations);

        // -(MGLAnnotationView * _Nullable)viewForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("viewForAnnotation:")]
        [return: NullAllowed]
        AnnotationView ViewForAnnotation (IAnnotation annotation);

        // -(__kindof MGLAnnotationImage * _Nullable)dequeueReusableAnnotationImageWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("dequeueReusableAnnotationImageWithIdentifier:")]
        AnnotationImage DequeueReusableAnnotationImage (string identifier);

        // -(__kindof MGLAnnotationView * _Nullable)dequeueReusableAnnotationViewWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("dequeueReusableAnnotationViewWithIdentifier:")]
        AnnotationView DequeueReusableAnnotationView (string identifier);

        // -(NSArray<id<MGLAnnotation>> * _Nullable)visibleAnnotationsInRect:(CGRect)rect;
        [Export ("visibleAnnotationsInRect:")]
        [return: NullAllowed]
        IAnnotation[] GetVisibleAnnotations (CGRect rect);

        // @property (copy, nonatomic) NSArray<id<MGLAnnotation>> * _Nonnull selectedAnnotations;
        [Export ("selectedAnnotations", ArgumentSemantic.Copy)]
        IAnnotation[] SelectedAnnotations { get; set; }

        // -(void)selectAnnotation:(id<MGLAnnotation> _Nonnull)annotation animated:(BOOL)animated;
        [Export ("selectAnnotation:animated:")]
        void SelectAnnotation (IAnnotation annotation, bool animated);

        // -(void)deselectAnnotation:(id<MGLAnnotation> _Nullable)annotation animated:(BOOL)animated;
        [Export ("deselectAnnotation:animated:")]
        void DeselectAnnotation ([NullAllowed] IAnnotation annotation, bool animated);

        // -(void)addOverlay:(id<MGLOverlay> _Nonnull)overlay;
        [Export ("addOverlay:")]
        void AddOverlay (IOverlay overlay);

        // -(void)addOverlays:(NSArray<id<MGLOverlay>> * _Nonnull)overlays;
        [Export ("addOverlays:")]
        void AddOverlays (IOverlay[] overlays);

        // -(void)removeOverlay:(id<MGLOverlay> _Nonnull)overlay;
        [Export ("removeOverlay:")]
        void RemoveOverlay (IOverlay overlay);

        // -(void)removeOverlays:(NSArray<id<MGLOverlay>> * _Nonnull)overlays;
        [Export ("removeOverlays:")]
        void RemoveOverlays (IOverlay[] overlays);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point;
        [Export ("visibleFeaturesAtPoint:")]
        Feature[] GetVisibleFeaturesAtPoint (CGPoint point);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers;
        [Export ("visibleFeaturesAtPoint:inStyleLayersWithIdentifiers:")]
        Feature[] GetVisibleFeaturesAtPoint (CGPoint point, [NullAllowed] string [] styleLayerIdentifiers);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesAtPoint:(CGPoint)point inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate;
        [Export ("visibleFeaturesAtPoint:inStyleLayersWithIdentifiers:predicate:")]
        Feature[] GetVisibleFeaturesAtPoint (CGPoint point, [NullAllowed] string [] styleLayerIdentifiers, [NullAllowed] NSPredicate predicate);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect;
        [Export ("visibleFeaturesInRect:")]
        Feature[] GetVisibleFeaturesInRect (CGRect rect);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers;
        [Export ("visibleFeaturesInRect:inStyleLayersWithIdentifiers:")]
        Feature[] GetVisibleFeaturesInRect (CGRect rect, [NullAllowed] string [] styleLayerIdentifiers);

        // -(NSArray<id<MGLFeature>> * _Nonnull)visibleFeaturesInRect:(CGRect)rect inStyleLayersWithIdentifiers:(NSSet<NSString *> * _Nullable)styleLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate;
        [Export ("visibleFeaturesInRect:inStyleLayersWithIdentifiers:predicate:")]
        Feature[] GetVisibleFeaturesInRect (CGRect rect, [NullAllowed] string [] styleLayerIdentifiers, [NullAllowed] NSPredicate predicate);

        // @property (nonatomic) MGLMapDebugMaskOptions debugMask;
        [Export ("debugMask", ArgumentSemantic.Assign)]
        DebugMaskOptions DebugMask { get; set; }

        // @property (getter = isDebugActive, nonatomic) BOOL debugActive __attribute__((deprecated("Use -debugMask and -setDebugMask:.")));
        [Obsolete ("Use DebugMask instead")]
        [Export ("debugActive")]
        bool DebugActive { [Bind ("isDebugActive")] get; set; }

        // -(void)toggleDebug __attribute__((deprecated("Use -setDebugMask:.")));
        [Obsolete ("Use DebugMask instead")]
        [Export ("toggleDebug")]
        void ToggleDebug ();

        // -(void)emptyMemoryCache __attribute__((deprecated("")));
        [Export ("emptyMemoryCache")]
        void EmptyMemoryCache ();

        // Adding the code for MGLMapView_IBAdditions here

        // @property (nonatomic) double latitude;
        [Export ("latitude")]
        double Latitude { get; set; }

        // @property (nonatomic) double longitude;
        [Export ("longitude")]
        double Longitude { get; set; }

        // @property (nonatomic) BOOL allowsZooming;
        [Export ("allowsZooming")]
        bool AllowsZooming { get; set; }

        // @property (nonatomic) BOOL allowsScrolling;
        [Export ("allowsScrolling")]
        bool AllowsScrolling { get; set; }

        // @property (nonatomic) BOOL allowsRotating;
        [Export ("allowsRotating")]
        bool AllowsRotating { get; set; }

        // @property (nonatomic) BOOL allowsTilting;
        [Export ("allowsTilting")]
        bool AllowsTilting { get; set; }
    }

    interface IMapViewDelegate { }

    // @protocol MGLMapViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "MGLMapViewDelegate")]
    interface MapViewDelegate
    {
        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView regionWillChangeAnimated:(BOOL)animated;
        [Export ("mapView:regionWillChangeAnimated:")]
        void RegionWillChange (MapView mapView, bool animated);

        // @optional -(void)mapViewRegionIsChanging:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewRegionIsChanging:")]
        void RegionIsChanging (MapView mapView);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView regionDidChangeAnimated:(BOOL)animated;
        [Export ("mapView:regionDidChangeAnimated:")]
        void RegionDidChange (MapView mapView, bool animated);

        // @optional -(BOOL)mapView:(MGLMapView * _Nonnull)mapView shouldChangeFromCamera:(MGLMapCamera * _Nonnull)oldCamera toCamera:(MGLMapCamera * _Nonnull)newCamera;
        [Export ("mapView:shouldChangeFromCamera:toCamera:")]
        bool ShouldChange (MapView mapView, MapCamera oldCamera, MapCamera newCamera);

        // @optional -(void)mapViewWillStartLoadingMap:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewWillStartLoadingMap:")]
        void WillStartLoadingMap (MapView mapView);

        // @optional -(void)mapViewDidFinishLoadingMap:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewDidFinishLoadingMap:")]
        void DidFinishLoadingMap (MapView mapView);

        // @optional -(void)mapViewDidFailLoadingMap:(MGLMapView * _Nonnull)mapView withError:(NSError * _Nonnull)error;
        [Export ("mapViewDidFailLoadingMap:withError:")]
        void DidFailLoadingMap (MapView mapView, NSError error);

        // @optional -(void)mapViewWillStartRenderingMap:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewWillStartRenderingMap:")]
        void WillStartRenderingMap (MapView mapView);

        // @optional -(void)mapViewDidFinishRenderingMap:(MGLMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered;
        [Export ("mapViewDidFinishRenderingMap:fullyRendered:")]
        void DidFinishRenderingMap (MapView mapView, bool fullyRendered);

        // @optional -(void)mapViewWillStartRenderingFrame:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewWillStartRenderingFrame:")]
        void WillStartRenderingFrame (MapView mapView);

        // @optional -(void)mapViewDidFinishRenderingFrame:(MGLMapView * _Nonnull)mapView fullyRendered:(BOOL)fullyRendered;
        [Export ("mapViewDidFinishRenderingFrame:fullyRendered:")]
        void DidFinishRenderingFrame (MapView mapView, bool fullyRendered);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didFinishLoadingStyle:(MGLStyle * _Nonnull)style;
        [Export ("mapView:didFinishLoadingStyle:")]
        void DidFinishLoadingStyle (MapView mapView, Style style);

        // @optional -(void)mapViewWillStartLocatingUser:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewWillStartLocatingUser:")]
        void WillStartLocatingUser (MapView mapView);

        // @optional -(void)mapViewDidStopLocatingUser:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewDidStopLocatingUser:")]
        void DidStopLocatingUser (MapView mapView);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didUpdateUserLocation:(MGLUserLocation * _Nullable)userLocation;
        [Export ("mapView:didUpdateUserLocation:")]
        void DidUpdateUserLocation (MapView mapView, [NullAllowed] UserLocation userLocation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didFailToLocateUserWithError:(NSError * _Nonnull)error;
        [Export ("mapView:didFailToLocateUserWithError:")]
        void DidFailToLocateUser (MapView mapView, NSError error);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didChangeUserTrackingMode:(MGLUserTrackingMode)mode animated:(BOOL)animated;
        [Export ("mapView:didChangeUserTrackingMode:animated:")]
        void DidChangeUserTrackingMode (MapView mapView, UserTrackingMode mode, bool animated);

        // @optional -(MGLAnnotationImage * _Nullable)mapView:(MGLMapView * _Nonnull)mapView imageForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:imageForAnnotation:")]
        [return: NullAllowed]
        AnnotationImage GetImage (MapView mapView, IAnnotation annotation);

        // @optional -(CGFloat)mapView:(MGLMapView * _Nonnull)mapView alphaForShapeAnnotation:(MGLShape * _Nonnull)annotation;
        [Export ("mapView:alphaForShapeAnnotation:")]
        nfloat GetAlpha (MapView mapView, Shape annotation);

        // @optional -(UIColor * _Nonnull)mapView:(MGLMapView * _Nonnull)mapView strokeColorForShapeAnnotation:(MGLShape * _Nonnull)annotation;
        [Export ("mapView:strokeColorForShapeAnnotation:")]
        UIColor GetStrokeColor (MapView mapView, Shape annotation);

        // @optional -(UIColor * _Nonnull)mapView:(MGLMapView * _Nonnull)mapView fillColorForPolygonAnnotation:(MGLPolygon * _Nonnull)annotation;
        [Export ("mapView:fillColorForPolygonAnnotation:")]
        UIColor GetFillColor (MapView mapView, Polygon annotation);

        // @optional -(CGFloat)mapView:(MGLMapView * _Nonnull)mapView lineWidthForPolylineAnnotation:(MGLPolyline * _Nonnull)annotation;
        [Export ("mapView:lineWidthForPolylineAnnotation:")]
        nfloat GetLineWidth (MapView mapView, Polyline annotation);

        // @optional -(MGLAnnotationView * _Nullable)mapView:(MGLMapView * _Nonnull)mapView viewForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:viewForAnnotation:")]
        [return: NullAllowed]
        AnnotationView GetAnnotationView (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didAddAnnotationViews:(NSArray<MGLAnnotationView *> * _Nonnull)annotationViews;
        [Export ("mapView:didAddAnnotationViews:")]
        void DidAddAnnotationViews (MapView mapView, AnnotationView[] annotationViews);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didSelectAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:didSelectAnnotation:")]
        void DidSelectAnnotation (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didDeselectAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:didDeselectAnnotation:")]
        void DidDeselectAnnotation (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didSelectAnnotationView:(MGLAnnotationView * _Nonnull)annotationView;
        [Export ("mapView:didSelectAnnotationView:")]
        void DidSelectAnnotationView (MapView mapView, AnnotationView annotationView);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didDeselectAnnotationView:(MGLAnnotationView * _Nonnull)annotationView;
        [Export ("mapView:didDeselectAnnotationView:")]
        void DidDeselectAnnotationView (MapView mapView, AnnotationView annotationView);

        // @optional -(BOOL)mapView:(MGLMapView * _Nonnull)mapView annotationCanShowCallout:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:annotationCanShowCallout:"), DelegateName("CanShowCallout"), DefaultValue(false)]
        bool CanShowCallout (MapView mapView, IAnnotation annotation);

        // @optional -(UIView<MGLCalloutView> * _Nullable)mapView:(MGLMapView * _Nonnull)mapView calloutViewForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:calloutViewForAnnotation:")]
        [return: NullAllowed]
        ICalloutView GetCalloutView (MapView mapView, IAnnotation annotation);

        // @optional -(UIView * _Nullable)mapView:(MGLMapView * _Nonnull)mapView leftCalloutAccessoryViewForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:leftCalloutAccessoryViewForAnnotation:")]
        [return: NullAllowed]
        UIView LeftCalloutAccessoryView (MapView mapView, IAnnotation annotation);

        // @optional -(UIView * _Nullable)mapView:(MGLMapView * _Nonnull)mapView rightCalloutAccessoryViewForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:rightCalloutAccessoryViewForAnnotation:")]
        [return: NullAllowed]
        UIView RightCalloutAccessoryView (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView annotation:(id<MGLAnnotation> _Nonnull)annotation calloutAccessoryControlTapped:(UIControl * _Nonnull)control;
        [Export ("mapView:annotation:calloutAccessoryControlTapped:"), EventArgs("UIControlTapped")]
        void CalloutAccessoryControlTapped (MapView mapView, IAnnotation annotation, UIControl control);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView tapOnCalloutForAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:tapOnCalloutForAnnotation:")]
        void TapOnCallout (MapView mapView, IAnnotation annotation);
    }

    // @interface MGLShape : NSObject <MGLAnnotation, NSSecureCoding>
    [BaseType (typeof (NSObject), Name = "MGLShape")]
    interface Shape : Annotation, INSSecureCoding
    {
        // +(MGLShape * _Nullable)shapeWithData:(NSData * _Nonnull)data encoding:(NSStringEncoding)encoding error:(NSError * _Nullable * _Nullable)outError;
        [Static]
        [Export ("shapeWithData:encoding:error:")]
        [return: NullAllowed]
        Shape From (NSData data, NSStringEncoding encoding, [NullAllowed] out NSError outError);

        //// @property (copy, nonatomic) NSString * _Nullable title;
        //[NullAllowed, Export ("title")]
        //string Title { get; set; }

        //// @property (copy, nonatomic) NSString * _Nullable subtitle;
        //[NullAllowed, Export ("subtitle")]
        //string Subtitle { get; set; }

        // -(NSData * _Nonnull)geoJSONDataUsingEncoding:(NSStringEncoding)encoding;
        [Export ("geoJSONDataUsingEncoding:")]
        NSData GetGeoJsonData (NSStringEncoding encoding);
    }

    // @interface DistanceFormatter : NSLengthFormatter
    [BaseType (typeof(NSLengthFormatter), Name = "MGLDistanceFormatter")]
    interface DistanceFormatter
    {
        // -(NSString * _Nonnull)stringFromDistance:(CLLocationDistance)distance;
        [Export ("stringFromDistance:")]
        string GetStringFromDistance (double distance);
    }

    // @interface MGLMultiPoint : MGLShape
    [BaseType (typeof (Shape), Name = "MGLMultiPoint")]
    interface MultiPoint
    {
        // @property (readonly, nonatomic) CLLocationCoordinate2D * _Nonnull coordinates __attribute__((objc_returns_inner_pointer));
        [Export ("coordinates")]
        IntPtr Coordinates { get; }

        // @property (readonly, nonatomic) NSUInteger pointCount;
        [Export ("pointCount")]
        nuint PointCount { get; }

        // -(void)getCoordinates:(CLLocationCoordinate2D * _Nonnull)coords range:(NSRange)range;
        [Export ("getCoordinates:range:")]
        void _GetCoordinates (IntPtr coords, NSRange range);

        // -(void)setCoordinates:(CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Export ("setCoordinates:count:")]
        void _SetCoordinates (IntPtr coords, nuint count);

        // -(void)insertCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count atIndex:(NSUInteger)index;
        [Export ("insertCoordinates:count:atIndex:")]
        void _InsertCoordinates (IntPtr coords, nuint count, nuint index);

        // -(void)appendCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Export ("appendCoordinates:count:")]
        void _AppendCoordinates (IntPtr coords, nuint count);

        // -(void)replaceCoordinatesInRange:(NSRange)range withCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords;
        [Export ("replaceCoordinatesInRange:withCoordinates:")]
        void _ReplaceCoordinatesInRange (NSRange range, IntPtr coords);

        // -(void)replaceCoordinatesInRange:(NSRange)range withCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Export ("replaceCoordinatesInRange:withCoordinates:count:")]
        void _ReplaceCoordinatesInRange (NSRange range, IntPtr coords, nuint count);

        // -(void)removeCoordinatesInRange:(NSRange)range;
        [Export ("removeCoordinatesInRange:")]
        void RemoveCoordinatesInRange (NSRange range);
    }

    [Static]
    //[Verify (ConstantsInterfaceAssociation)]
    partial interface CoordinateSpanConstants
    {
        // extern const MGLCoordinateSpan MGLCoordinateSpanZero __attribute__((visibility("default")));
        [Field ("MGLCoordinateSpanZero", "__Internal")]
        IntPtr _Zero { get; }
    }

    interface IOverlay { }

    // @protocol MGLOverlay <MGLAnnotation>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "MGLOverlay")]
    interface Overlay : Annotation
    {
        // @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
        //[Abstract]
        //[Export ("coordinate")]
        //CLLocationCoordinate2D Coordinate { get; }

        // @required @property (readonly, nonatomic) MGLCoordinateBounds overlayBounds;
        [Abstract]
        [Export ("overlayBounds")]
        CoordinateBounds OverlayBounds { get; }

        // @required -(BOOL)intersectsOverlayBounds:(MGLCoordinateBounds)overlayBounds;
        [Abstract]
        [Export ("intersectsOverlayBounds:")]
        bool IntersectsOverlayBounds (CoordinateBounds overlayBounds);
    }

    // @interface MGLPointAnnotation : MGLShape
    [BaseType (typeof (Shape), Name = "MGLPointAnnotation")]
    interface PointAnnotation : Shape
    {
        // @property (assign, nonatomic) CLLocationCoordinate2D coordinate;
        [Export ("coordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D Coordinate { get; set; }
    }

    // @interface MGLPolygon : MGLMultiPoint <MGLOverlay>
    [BaseType (typeof (MultiPoint), Name = "MGLPolygon")]
    interface Polygon : Overlay
    {
        // @property (readonly, nonatomic) NSArray<MGLPolygon *> * _Nullable interiorPolygons;
        [NullAllowed, Export ("interiorPolygons")]
        Polygon[] InteriorPolygons { get; }

        // +(instancetype _Nonnull)polygonWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Static]
        [Export ("polygonWithCoordinates:count:")]
        Polygon WithCoordinates (IntPtr coords, nuint count);

        // +(instancetype _Nonnull)polygonWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count interiorPolygons:(NSArray<MGLPolygon *> * _Nullable)interiorPolygons;
        [Static]
        [Export ("polygonWithCoordinates:count:interiorPolygons:")]
        Polygon WithCoordinates (IntPtr coords, nuint count, [NullAllowed] Polygon[] interiorPolygons);
    }

    // @interface MultiPolygon : MGLShape <MGLOverlay>
    [BaseType (typeof (Shape), Name = "MGLMultiPolygon")]
    interface MultiPolygon : Overlay
    {
        // @property (readonly, copy, nonatomic) NSArray<MGLPolygon *> * _Nonnull polygons;
        [Export ("polygons", ArgumentSemantic.Copy)]
        Polygon[] Polygons { get; }

        // +(instancetype _Nonnull)multiPolygonWithPolygons:(NSArray<MGLPolygon *> * _Nonnull)polygons;
        [Static]
        [Export ("multiPolygonWithPolygons:")]
        MultiPolygon WithPolygons (Polygon[] polygons);
    }

    // @interface MGLPolyline : MGLMultiPoint <MGLOverlay>
    [BaseType (typeof (MultiPoint), Name = "MGLPolyline")]
    interface Polyline : Overlay
    {
        // +(instancetype _Nonnull)polylineWithCoordinates:(const CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Static]
        [Export ("polylineWithCoordinates:count:")]
        Polyline WithCoordinates (IntPtr coords, nuint count);
    }

    // @interface MultiPolyline : MGLShape <MGLOverlay>
    [BaseType (typeof (Shape), Name = "MGLMultiPolyline")]
    interface MultiPolyline : Overlay
    {
        // @property (readonly, copy, nonatomic) NSArray<MGLPolyline *> * _Nonnull polylines;
        [Export ("polylines", ArgumentSemantic.Copy)]
        Polyline[] Polylines { get; }

        // +(instancetype _Nonnull)multiPolylineWithPolylines:(NSArray<MGLPolyline *> * _Nonnull)polylines;
        [Static]
        [Export ("multiPolylineWithPolylines:")]
        MultiPolyline WithPolylines (Polyline[] polylines);
    }

    // @interface MGLUserLocation : NSObject <MGLAnnotation, NSSecureCoding>
    [BaseType (typeof (NSObject), Name = "MGLUserLocation")]
    interface UserLocation : Annotation, INSSecureCoding
    {
        // @property (readonly, nonatomic) CLLocation * _Nullable location;
        [NullAllowed, Export ("location")]
        CLLocation Location { get; }

        // @property (readonly, getter = isUpdating, nonatomic) BOOL updating;
        [Export ("updating")]
        bool Updating { [Bind ("isUpdating")] get; }

        // @property (readonly, nonatomic) CLHeading * _Nullable heading;
        [NullAllowed, Export ("heading")]
        CLHeading Heading { get; }

        // @property (copy, nonatomic) NSString * _Nonnull title;
        [Export ("title")]
        string Title { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable subtitle;
        [NullAllowed, Export ("subtitle")]
        string Subtitle { get; set; }
    }

    // @interface MGLMapCamera : NSObject <NSSecureCoding, NSCopying>
    [BaseType (typeof (NSObject), Name = "MGLMapCamera")]
    interface MapCamera : INSSecureCoding, INSCopying
    {
        // @property (nonatomic) CLLocationCoordinate2D centerCoordinate;
        [Export ("centerCoordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D CenterCoordinate { get; set; }

        // @property (nonatomic) CLLocationDirection heading;
        [Export ("heading")]
        double Heading { get; set; }

        // @property (nonatomic) CGFloat pitch;
        [Export ("pitch")]
        nfloat Pitch { get; set; }

        // @property (nonatomic) CLLocationDistance altitude;
        [Export ("altitude")]
        double Altitude { get; set; }

        // +(instancetype _Nonnull)camera;
        [Static]
        [Export ("camera")]
        MapCamera Camera ();

        // +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate fromEyeCoordinate:(CLLocationCoordinate2D)eyeCoordinate eyeAltitude:(CLLocationDistance)eyeAltitude;
        [Static]
        [Export ("cameraLookingAtCenterCoordinate:fromEyeCoordinate:eyeAltitude:")]
        MapCamera CameraLookingAtCenterCoordinate (CLLocationCoordinate2D centerCoordinate, CLLocationCoordinate2D eyeCoordinate, double eyeAltitude);

        // +(instancetype _Nonnull)cameraLookingAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate fromDistance:(CLLocationDistance)distance pitch:(CGFloat)pitch heading:(CLLocationDirection)heading;
        [Static]
        [Export ("cameraLookingAtCenterCoordinate:fromDistance:pitch:heading:")]
        MapCamera CameraLookingAtCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double distance, nfloat pitch, double heading);

        // -(BOOL)isEqualToMapCamera:(MGLMapCamera * _Nonnull)otherCamera;
        [Export ("isEqualToMapCamera:")]
        bool Equals (MapCamera otherCamera);
    }

    // @interface Style : NSObject
    [BaseType (typeof (NSObject), Name = "MGLStyle")]
    interface Style
    {
        // +(NSURL * _Nonnull)streetsStyleURL __attribute__((deprecated("Use -streetsStyleURLWithVersion:.")));
        [Obsolete ("Use StreetsStyle (version) method instead")]
        [Static]
        [Export ("streetsStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Streets { get; }

        // +(NSURL * _Nonnull)streetsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("streetsStyleURLWithVersion:")]
        NSUrl StreetsStyle (nint version);

        // +(NSURL * _Nonnull)emeraldStyleURL __attribute__((deprecated("Create an NSURL object with the string “mapbox://styles/mapbox/emerald-v8”.")));
        [Obsolete ("Create an NSURL object with the string 'mapbox://styles/mapbox/emerald-v8' instead.")]
        [Static]
        [Export ("emeraldStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Emerald { get; }

        // +(NSURL * _Nonnull)outdoorsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("outdoorsStyleURLWithVersion:")]
        NSUrl OutdoorsStyle (nint version);

        // +(NSURL * _Nonnull)lightStyleURL __attribute__((deprecated("Use -lightStyleURLWithVersion:.")));
        [Obsolete ("Use LightStyle (version) method instead")]
        [Static]
        [Export ("lightStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Light { get; }

        // +(NSURL * _Nonnull)lightStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("lightStyleURLWithVersion:")]
        NSUrl LightStyle (nint version);

        // +(NSURL * _Nonnull)darkStyleURL __attribute__((deprecated("Use -darkStyleURLWithVersion:.")));
        [Obsolete ("Use DarkStyle (version) method instead")]
        [Static]
        [Export ("darkStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Dark { get; }

        // +(NSURL * _Nonnull)darkStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("darkStyleURLWithVersion:")]
        NSUrl DarkStyle (nint version);

        // +(NSURL * _Nonnull)satelliteStyleURL __attribute__((deprecated("Use -satelliteStyleURLWithVersion:.")));
        [Obsolete ("Use SatelliteStyle (version) method instead")]
        [Static]
        [Export ("satelliteStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Satellite { get; }

        // +(NSURL * _Nonnull)satelliteStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("satelliteStyleURLWithVersion:")]
        NSUrl SatelliteStyle (nint version);

        // +(NSURL * _Nonnull)hybridStyleURL __attribute__((deprecated("Use -satelliteStreetsStyleURLWithVersion:.")));
        [Obsolete ("Use SatelliteStreetsStyle (version) method instead")]
        [Static]
        [Export ("hybridStyleURL")]
        //[Verify (MethodToProperty)]
        NSUrl Hybrid { get; }

        // +(NSURL * _Nonnull)satelliteStreetsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("satelliteStreetsStyleURLWithVersion:")]
        NSUrl SatelliteStreetsStyle (nint version);

        // @property (readonly, copy) NSString * _Nullable name;
        [NullAllowed, Export ("name")]
        string Name { get; }

        // @property (nonatomic, strong) NSSet<__kindof MGLSource *> * _Nonnull sources;
        [Export ("sources", ArgumentSemantic.Strong)]
        Source[] Sources { get; set; }

        // @property (nonatomic) MGLTransition transition;
        [Export ("transition", ArgumentSemantic.Assign)]
        Transition Transition { get; set; }

        // -(MGLSource * _Nullable)sourceWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("sourceWithIdentifier:")]
        [return: NullAllowed]
        Source GetSource (string identifier);

        // -(void)addSource:(MGLSource * _Nonnull)source;
        [Export ("addSource:")]
        void AddSource (Source source);

        // -(void)removeSource:(MGLSource * _Nonnull)source;
        [Export ("removeSource:")]
        void RemoveSource (Source source);

        // @property (nonatomic, strong) NSArray<__kindof MGLStyleLayer *> * _Nonnull layers;
        [Export ("layers", ArgumentSemantic.Strong)]
        StyleLayer[] Layers { get; set; }

        // -(MGLStyleLayer * _Nullable)layerWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("layerWithIdentifier:")]
        [return: NullAllowed]
        StyleLayer GetLayer (string identifier);

        // -(void)addLayer:(MGLStyleLayer * _Nonnull)layer;
        [Export ("addLayer:")]
        void AddLayer (StyleLayer layer);

        // -(void)insertLayer:(MGLStyleLayer * _Nonnull)layer atIndex:(NSUInteger)index;
        [Export ("insertLayer:atIndex:")]
        void InsertLayer (StyleLayer layer, nuint index);

        // -(void)insertLayer:(MGLStyleLayer * _Nonnull)layer belowLayer:(MGLStyleLayer * _Nonnull)sibling;
        [Export ("insertLayer:belowLayer:")]
        void InsertLayerBelow (StyleLayer layer, StyleLayer sibling);

        // -(void)insertLayer:(MGLStyleLayer * _Nonnull)layer aboveLayer:(MGLStyleLayer * _Nonnull)sibling;
        [Export ("insertLayer:aboveLayer:")]
        void InsertLayerAbove (StyleLayer layer, StyleLayer sibling);

        // -(void)removeLayer:(MGLStyleLayer * _Nonnull)layer;
        [Export ("removeLayer:")]
        void RemoveLayer (StyleLayer layer);

        // @property (nonatomic) NSArray<NSString *> * _Nonnull styleClasses __attribute__((deprecated("This property will be removed in a future release.")));
        [Obsolete("This property will be removed in a future release.")]
        [Export ("styleClasses", ArgumentSemantic.Assign)]
        string[] StyleClasses { get; set; }

        // -(BOOL)hasStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("This method will be removed in a future release.")));
        [Obsolete("This property will be removed in a future release.")]
        [Export ("hasStyleClass:")]
        bool HasStyleClass (string styleClass);

        // -(void)addStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("This method will be removed in a future release.")));
        [Obsolete("This property will be removed in a future release.")]
        [Export ("addStyleClass:")]
        void AddStyleClass (string styleClass);

        // -(void)removeStyleClass:(NSString * _Nonnull)styleClass __attribute__((deprecated("This method will be removed in a future release.")));
        [Obsolete("This property will be removed in a future release.")]
        [Export ("removeStyleClass:")]
        void RemoveStyleClass (string styleClass);

        // -(UIImage * _Nullable)imageForName:(NSString * _Nonnull)name;
        [Export ("imageForName:")]
        [return: NullAllowed]
        UIImage GetImage (string name);

        // -(void)setImage:(UIImage * _Nonnull)image forName:(NSString * _Nonnull)name;
        [Export ("setImage:forName:")]
        void SetImage (UIImage image, string name);

        // -(void)removeImageForName:(NSString * _Nonnull)name;
        [Export ("removeImageForName:")]
        void RemoveImage (string name);
    }

    interface ICalloutView { }

    // @protocol MGLCalloutView <NSObject>
    [Protocol (Name = "MGLCalloutView"), Model]
    [BaseType (typeof(NSObject))]
    interface CalloutView
    {
        // @required @property (nonatomic, strong) id<MGLAnnotation> _Nonnull representedObject;
        [Abstract]
        [Export ("representedObject", ArgumentSemantic.Strong)]
        IAnnotation RepresentedObject { get; set; }

        // @required @property (nonatomic, strong) UIView * _Nonnull leftAccessoryView;
        [Abstract]
        [Export ("leftAccessoryView", ArgumentSemantic.Strong)]
        UIView LeftAccessoryView { get; set; }

        // @required @property (nonatomic, strong) UIView * _Nonnull rightAccessoryView;
        [Abstract]
        [Export ("rightAccessoryView", ArgumentSemantic.Strong)]
        UIView RightAccessoryView { get; set; }

        // @required @property (nonatomic, weak) id<MGLCalloutViewDelegate> _Nullable delegate;
        [Abstract]
        [NullAllowed]
        [Export("delegate", ArgumentSemantic.Weak)]
        ICalloutViewDelegate Delegate { get; set; }

        // @required -(void)presentCalloutFromRect:(CGRect)rect inView:(UIView * _Nonnull)view constrainedToView:(UIView * _Nonnull)constrainedView animated:(BOOL)animated;
        [Abstract]
        [Export ("presentCalloutFromRect:inView:constrainedToView:animated:")]
        void PresentCallout (CGRect fromRect, UIView inView, UIView constrainedToView, bool animated);

        // @required -(void)dismissCalloutAnimated:(BOOL)animated;
        [Abstract]
        [Export ("dismissCalloutAnimated:")]
        void DismissCallout (bool animated);

        // @optional @property (readonly, getter = isAnchoredToAnnotation, assign, nonatomic) BOOL anchoredToAnnotation;
        [Export ("anchoredToAnnotation")]
        bool IsAnchoredToAnnotation { [Bind ("isAnchoredToAnnotation")] get; }

        // @optional @property (readonly, assign, nonatomic) BOOL dismissesAutomatically;
        [Export ("dismissesAutomatically")]
        bool DismissesAutomatically { get; }
    }

    interface ICalloutViewDelegate { }

    // @protocol MGLCalloutViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "MGLCalloutViewDelegate")]
    interface CalloutViewDelegate
    {
        // @optional -(BOOL)calloutViewShouldHighlight:(UIView<MGLCalloutView> * _Nonnull)calloutView;
        [Export ("calloutViewShouldHighlight:")]
        bool ShouldHighlight (ICalloutView calloutView);

        // @optional -(void)calloutViewTapped:(UIView<MGLCalloutView> * _Nonnull)calloutView;
        [Export ("calloutViewTapped:")]
        void Tapped (ICalloutView calloutView);

        // @optional -(void)calloutViewWillAppear:(UIView<MGLCalloutView> * _Nonnull)calloutView;
        [Export ("calloutViewWillAppear:")]
        void WillAppear (ICalloutView calloutView);

        // @optional -(void)calloutViewDidAppear:(UIView<MGLCalloutView> * _Nonnull)calloutView;
        [Export ("calloutViewDidAppear:")]
        void DidAppear (ICalloutView calloutView);

    }

    interface IOfflineRegion { }

    // @protocol MGLOfflineRegion <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject), Name = "MGLOfflineRegion")]
    interface OfflineRegion
    {

    }

    // @interface TilePyramidOfflineRegion : NSObject <MGLOfflineRegion, NSSecureCoding, NSCopying>
    [BaseType (typeof (NSObject), Name = "MGLTilePyramidOfflineRegion")]
    [DisableDefaultCtor]
    interface TilePyramidOfflineRegion : OfflineRegion, INSSecureCoding, INSCopying
    {
        // @property (readonly, nonatomic) NSURL * _Nonnull styleURL;
        [Export ("styleURL")]
        NSUrl StyleUrl { get; }

        // @property (readonly, nonatomic) MGLCoordinateBounds bounds;
        [Export ("bounds")]
        CoordinateBounds Bounds { get; }

        // @property (readonly, nonatomic) double minimumZoomLevel;
        [Export ("minimumZoomLevel")]
        double MinimumZoomLevel { get; }

        // @property (readonly, nonatomic) double maximumZoomLevel;
        [Export ("maximumZoomLevel")]
        double MaximumZoomLevel { get; }

        // -(instancetype _Nonnull)initWithStyleURL:(NSURL * _Nullable)styleURL bounds:(MGLCoordinateBounds)bounds fromZoomLevel:(double)minimumZoomLevel toZoomLevel:(double)maximumZoomLevel __attribute__((objc_designated_initializer));
        [Export ("initWithStyleURL:bounds:fromZoomLevel:toZoomLevel:")]
        [DesignatedInitializer]
        IntPtr Constructor ([NullAllowed] NSUrl styleURL, CoordinateBounds bounds, double minimumZoomLevel, double maximumZoomLevel);
    }

    // @interface PointCollection : MGLShape <MGLOverlay>
    [BaseType(typeof(Shape), Name = "MGLPointCollection")]
    [DisableDefaultCtor]
    interface PointCollection : Overlay, Annotation
    {
        // +(instancetype)pointCollectionWithCoordinates:(const CLLocationCoordinate2D *)coords count:(NSUInteger)count;
        [Static]
        [Export ("pointCollectionWithCoordinates:count:")]
        PointCollection _From (IntPtr coords, nuint count);

        // @property (readonly, nonatomic) CLLocationCoordinate2D * coordinates __attribute__((objc_returns_inner_pointer));
        [Export ("coordinates")]
        IntPtr Coordinates { get; }

        // @property (readonly, nonatomic) NSUInteger pointCount;
        [Export ("pointCount")]
        nuint PointCount { get; }

        // -(void)getCoordinates:(CLLocationCoordinate2D *)coords range:(NSRange)range;
        [Export ("getCoordinates:range:")]
        void _GetCoordinates (IntPtr coords, NSRange range);
    }

    // @interface ShapeCollection : MGLShape
    [BaseType(typeof(Shape), Name = "MGLShapeCollection")]
    [DisableDefaultCtor]
    interface ShapeCollection
    {
        // @property (readonly, copy, nonatomic) NSArray<MGLShape *> * _Nonnull shapes;
        [Export ("shapes", ArgumentSemantic.Copy)]
        Shape[] Shapes { get; }

        // +(instancetype _Nonnull)shapeCollectionWithShapes:(NSArray<MGLShape *> * _Nonnull)shapes;
        [Static]
        [Export ("shapeCollectionWithShapes:")]
        ShapeCollection From (Shape[] shapes);
    }

    // @interface OfflinePack : NSObject
    [BaseType (typeof (NSObject), Name = "MGLOfflinePack")]
    interface OfflinePack
    {
        // @property (readonly, nonatomic) id<MGLOfflineRegion> _Nonnull region;
        [Export ("region")]
        IOfflineRegion Region { get; }

        // @property (readonly, nonatomic) NSData * _Nonnull context;
        [Export ("context")]
        NSData Context { get; }

        // @property (readonly, nonatomic) MGLOfflinePackState state;
        [Export ("state")]
        OfflinePackState State { get; }

        // @property (readonly, nonatomic) MGLOfflinePackProgress progress;
        [Export ("progress")]
        OfflinePackProgress Progress { get; }

        // -(void)resume;
        [Export ("resume")]
        void Resume ();

        // -(void)suspend;
        [Export ("suspend")]
        void Suspend ();

        // -(void)requestProgress;
        [Export ("requestProgress")]
        void RequestProgress ();
    }

    // @interface OfflineStorage : NSObject
    [BaseType (typeof (NSObject), Name = "MGLOfflineStorage")]
    interface OfflineStorage
    {
        // +(instancetype _Nonnull)sharedOfflineStorage;
        [Static]
        [Export ("sharedOfflineStorage")]
        OfflineStorage Shared { get; }

        // @property (nonatomic, weak) id<MGLOfflineStorageDelegate> _Nullable delegate __attribute__((iboutlet));
        [NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        IOfflineStorageDelegate Delegate { get; set; }

        // @property (readonly, nonatomic, strong) NSArray<MGLOfflinePack *> * _Nullable packs;
        [NullAllowed, Export ("packs", ArgumentSemantic.Strong)]
        OfflinePack[] Packs { get; }

        // -(void)addPackForRegion:(id<MGLOfflineRegion> _Nonnull)region withContext:(NSData * _Nonnull)context completionHandler:(MGLOfflinePackAdditionCompletionHandler _Nullable)completion;
        [Export ("addPackForRegion:withContext:completionHandler:")]
        void AddPack (IOfflineRegion region, NSData context, [NullAllowed] OfflinePackAdditionCompletion completionHandler);

        // -(void)removePack:(MGLOfflinePack * _Nonnull)pack withCompletionHandler:(MGLOfflinePackRemovalCompletionHandler _Nullable)completion;
        [Export ("removePack:withCompletionHandler:")]
        void RemovePack (OfflinePack pack, [NullAllowed] OfflinePackRemovalCompletion completionHandler);

        // -(void)reloadPacks;
        [Export ("reloadPacks")]
        void ReloadPacks ();

        // -(void)setMaximumAllowedMapboxTiles:(uint64_t)maximumCount;
        [Export ("setMaximumAllowedMapboxTiles:")]
        void SetMaxAllowedMapboxTiles (ulong maximumCount);

        // @property (readonly, nonatomic) unsigned long long countOfBytesCompleted;
        [Export ("countOfBytesCompleted")]
        ulong CountOfBytesCompleted { get; }
    }

    // @interface MGLClockDirectionFormatter : NSFormatter
    [BaseType (typeof (NSFormatter), Name = "MGLClockDirectionFormatter")]
    interface ClockDirectionFormatter
    {
        // @property (nonatomic) NSFormattingUnitStyle unitStyle;
        [Export ("unitStyle", ArgumentSemantic.Assign)]
        NSFormattingUnitStyle UnitStyle { get; set; }

        // -(NSString * _Nonnull)stringFromDirection:(CLLocationDirection)direction;
        [Export ("stringFromDirection:")]
        string StringFromDirection (double direction);

        // -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
        [Export ("getObjectValue:forString:errorDescription:")]
        bool GetObjectValue ([NullAllowed] out NSObject obj, string str, [NullAllowed] out NSString error);
    }

    // @interface MGLCompassDirectionFormatter : NSFormatter
    [BaseType (typeof (NSFormatter), Name = "MGLCompassDirectionFormatter")]
    interface CompassDirectionFormatter
    {
        // @property (nonatomic) NSFormattingUnitStyle unitStyle;
        [Export ("unitStyle", ArgumentSemantic.Assign)]
        NSFormattingUnitStyle UnitStyle { get; set; }

        // -(NSString * _Nonnull)stringFromDirection:(CLLocationDirection)direction;
        [Export ("stringFromDirection:")]
        string StringFromDirection (double direction);

        // -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
        [Export ("getObjectValue:forString:errorDescription:")]
        bool GetObjectValue ([NullAllowed] out NSObject obj, string str, [NullAllowed] out NSString error);
    }

    // @interface CoordinateFormatter : NSFormatter
    [BaseType (typeof(NSFormatter), Name = "MGLCoordinateFormatter")]
    interface CoordinateFormatter
    {
        // @property (nonatomic) BOOL allowsMinutes;
        [Export ("allowsMinutes")]
        bool AllowsMinutes { get; set; }

        // @property (nonatomic) BOOL allowsSeconds;
        [Export ("allowsSeconds")]
        bool AllowsSeconds { get; set; }

        // @property (nonatomic) NSFormattingUnitStyle unitStyle;
        [Export ("unitStyle", ArgumentSemantic.Assign)]
        NSFormattingUnitStyle UnitStyle { get; set; }

        // -(NSString * _Nonnull)stringFromCoordinate:(CLLocationCoordinate2D)coordinate;
        [Export ("stringFromCoordinate:")]
        string StringFromCoordinate (CLLocationCoordinate2D coordinate);

        // -(BOOL)getObjectValue:(id  _Nullable * _Nullable)obj forString:(NSString * _Nonnull)string errorDescription:(NSString * _Nullable * _Nullable)error;
        [Export ("getObjectValue:forString:errorDescription:")]
        bool GetObjectValue ([NullAllowed] out NSObject obj, string str, [NullAllowed] out NSString error);
    }

    interface IFeature { }

    // @protocol MGLFeature <MGLAnnotation>
    [Protocol, Model]
    [BaseType (typeof (NSObject), Name = "MGLFeature")]
    interface Feature : Annotation
    {
        // @required @property (copy, nonatomic) id _Nullable identifier;
        [Abstract]
        [NullAllowed, Export ("identifier", ArgumentSemantic.Copy)]
        NSObject Identifier { get; set; }

        // @required @property (copy, nonatomic) NSDictionary<NSString *,id> * _Nonnull attributes;
        [Abstract]
        [Export ("attributes", ArgumentSemantic.Copy)]
        NSDictionary<NSString, NSObject> Attributes { get; set; }

        // @required -(id _Nullable)attributeForKey:(NSString * _Nonnull)key;
        [Abstract]
        [Export ("attributeForKey:")]
        [return: NullAllowed]
        NSObject GetAttribute (string key);

        // @required -(NSDictionary<NSString *,id> * _Nonnull)geoJSONDictionary;
        [Abstract]
        [Export ("geoJSONDictionary")]
        //[Verify (MethodToProperty)]
        NSDictionary<NSString, NSObject> GeoJsonDictionary { get; }
    }

    // @interface MGLPointFeature : MGLPointAnnotation<MGLFeature>
    [BaseType (typeof (PointAnnotation), Name="MGLPointFeature")]
    interface PointFeature : Feature
    {
    }

    // @interface MGLPolylineFeature : MGLPolyline<MGLFeature>
    [BaseType (typeof (Polyline), Name = "MGLPolylineFeature")]
    interface PolylineFeature : Feature
    {
    }

    // @interface MGLPolygonFeature : MGLPolygon <MGLFeature>
    [BaseType (typeof (Polygon), Name = "MGLPolygonFeature")]
    interface PolygonFeature : Feature
    {
    }

    // @interface MGLPointCollectionFeature : MGLPointCollection <MGLFeature>
    [BaseType(typeof(PointCollection), Name = "MGLPointCollectionFeature")]
    interface PointCollectionFeature : Feature
    {
    }

    // @interface MGLMultiPolylineFeature : MGLMultiPolyline <MGLFeature>
    [BaseType(typeof(MultiPolyline), Name = "MGLMultiPolylineFeature")]
    interface MultiPolylineFeature : Feature
    {
    }

    // @interface MGLMultiPolygonFeature : MGLMultiPolygon <MGLFeature>
    [BaseType(typeof(MultiPolygon), Name = "MGLMultiPolygonFeature")]
    interface MultiPolygonFeature : Feature
    {
    }

    //@interface MGLShapeCollectionFeature : MGLShapeCollection<MGLFeature>
    [BaseType(typeof(ShapeCollection), Name = "MGLShapeCollectionFeature")]
    [DisableDefaultCtor]
    interface ShapeCollectionFeature : Feature
    {
        // @property (readonly, copy, nonatomic) NSArray<MGLShape<MGLFeature> *> * _Nonnull shapes;
        [Export ("shapes", ArgumentSemantic.Copy)]
        Shape[] Shapes { get; }

        // +(instancetype _Nonnull)shapeCollectionWithShapes:(NSArray<MGLShape<MGLFeature> *> * _Nonnull)shapes;
        [Static]
        [Export ("shapeCollectionWithShapes:")]
        ShapeCollectionFeature WithShapes (Shape[] shapes);
    }

    interface IOfflineStorageDelegate { }

    // @protocol MGLOfflineStorageDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject), Name = "MGLOfflineStorageDelegate")]
    interface OfflineStorageDelegate
    {
        // @required -(NSURL * _Nonnull)offlineStorage:(MGLOfflineStorage * _Nonnull)storage URLForResourceOfKind:(MGLResourceKind)kind withURL:(NSURL * _Nonnull)url;
        [Abstract]
        [Export ("offlineStorage:URLForResourceOfKind:withURL:")]
        NSUrl UrlForResource (OfflineStorage storage, ResourceKind kind, NSUrl url);
    }

    // @interface StyleLayer : NSObject
    [BaseType (typeof(NSObject), Name = "MGLStyleLayer")]
    [DisableDefaultCtor]
    interface StyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("initWithIdentifier:")]
        IntPtr Constructor (string identifier);

        // @property (readonly, copy, nonatomic) NSString * _Nonnull identifier;
        [Export ("identifier")]
        string Identifier { get; }

        // @property (getter = isVisible, assign, nonatomic) BOOL visible;
        [Export ("visible")]
        bool IsVisible { [Bind ("isVisible")] get; set; }

        // @property (assign, nonatomic) float maximumZoomLevel;
        [Export ("maximumZoomLevel")]
        float MaximumZoomLevel { get; set; }

        // @property (assign, nonatomic) float minimumZoomLevel;
        [Export ("minimumZoomLevel")]
        float MinimumZoomLevel { get; set; }
    }

    // @interface ForegroundStyleLayer : MGLStyleLayer
    [BaseType(typeof(StyleLayer), Name = "MGLForegroundStyleLayer")]
    [DisableDefaultCtor]
    interface ForegroundStyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MGLSource * _Nonnull)source __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:source:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, Source source);

        // @property (readonly, nonatomic) NSString * _Nullable sourceIdentifier;
        [NullAllowed, Export ("sourceIdentifier")]
        string SourceIdentifier { get; }
    }

    // @interface VectorStyleLayer : MGLForegroundStyleLayer
    [BaseType(typeof(ForegroundStyleLayer), Name = "MGLVectorStyleLayer")]
    interface VectorStyleLayer
    {
        // @property (nonatomic) NSString * _Nullable sourceLayerIdentifier;
        [NullAllowed, Export ("sourceLayerIdentifier")]
        string SourceLayerIdentifier { get; set; }

        // @property (nonatomic) NSPredicate * _Nullable predicate;
        [NullAllowed, Export ("predicate", ArgumentSemantic.Assign)]
        NSPredicate Predicate { get; set; }
    }


	// @interface NSValue (MGLAdditions)
	[Category]
	[BaseType(typeof(NSValue))]
	interface Additions_NSValue
	{
		// @property (readonly) CLLocationCoordinate2D MGLCoordinateValue;
		[Export("MGLCoordinateValue")]
		CLLocationCoordinate2D GetCoordinate();

		// @property (readonly) MGLCoordinateSpan MGLCoordinateSpanValue;
		[Export("MGLCoordinateSpanValue")]
		CoordinateSpan GetCoordinateSpan();

		// @property (readonly) MGLCoordinateBounds MGLCoordinateBoundsValue;
		[Export("MGLCoordinateBoundsValue")]
		CoordinateBounds GetCoordinateBounds();

		// @property (readonly) MGLOfflinePackProgress MGLOfflinePackProgressValue;
		[Export("MGLOfflinePackProgressValue")]
		OfflinePackProgress GetOfflinePackProgress();

		// @property (readonly) MGLTransition MGLTransitionValue;
		[Export("MGLTransitionValue")]
		Transition GetTransition();
	}

    // @interface NSValue (MGLAdditions)
	//[BaseType(typeof(NSValue))]
	//[DisableDefaultCtor]
    //interface NSValueExt
    //{
    //    // +(instancetype _Nonnull)valueWithMGLCoordinate:(CLLocationCoordinate2D)coordinate;
    //    [Static]
    //    [Export ("valueWithMGLCoordinate:")]
    //    NSValue GetValue (CLLocationCoordinate2D coordinate);

    //    // +(instancetype _Nonnull)valueWithMGLCoordinateSpan:(MGLCoordinateSpan)span;
    //    [Static]
    //    [Export ("valueWithMGLCoordinateSpan:")]
    //    NSValue GetValue (CoordinateSpan span);

    //    // +(instancetype _Nonnull)valueWithMGLCoordinateBounds:(MGLCoordinateBounds)bounds;
    //    [Static]
    //    [Export ("valueWithMGLCoordinateBounds:")]
    //    NSValue GetValue (CoordinateBounds bounds);

    //    // +(NSValue * _Nonnull)valueWithMGLOfflinePackProgress:(MGLOfflinePackProgress)progress;
    //    [Static]
    //    [Export ("valueWithMGLOfflinePackProgress:")]
    //    NSValue GetValue (OfflinePackProgress progress);

    //    // +(NSValue * _Nonnull)valueWithMGLTransition:(MGLTransition)transition;
    //    [Static]
    //    [Export ("valueWithMGLTransition:")]
    //    NSValue GetValue (Transition transition);
    //}

    // audit-objc-generics: @interface StyleValue<T> : NSObject
    [BaseType (typeof(NSObject), Name = "MGLStyleValue")]
    [Advice("Common raw values include:\n\nNSNumber (for Boolean values and floating-point numbers)\nNSValue (for CGVector, NSEdgeInsets, UIEdgeInsets, and enumerations)\nNSString\nNSColor or UIColor\nNSArray")]
    interface StyleValue : INSObjectProtocol
    {
        // +(instancetype _Nonnull)valueWithRawValue:(T _Nonnull)rawValue;
        [Static]
        [Export ("valueWithRawValue:")]
        StyleValue ValueWithRawValue (NSObject rawValue);

        // +(instancetype _Nonnull)valueWithStops:(NSDictionary<NSNumber *,MGLStyleValue<T> *> * _Nonnull)stops __attribute__((deprecated("Use +[MGLStyleValue valueWithInterpolationMode:cameraStops:options:]")));
        [Obsolete("Use StyleValue.ValueWithInterpolationMode() .")]
        [Static]
        [Export ("valueWithStops:")]
        StyleValue ValueWithStops (NSDictionary<NSNumber, StyleValue> stops);

        // +(instancetype _Nonnull)valueWithInterpolationBase:(CGFloat)interpolationBase stops:(NSDictionary<NSNumber *,MGLStyleValue<T> *> * _Nonnull)stops __attribute__((deprecated("Use +[MGLStyleValue valueWithInterpolationMode:cameraStops:options:]")));
        [Static]
        [Export ("valueWithInterpolationBase:stops:")]
        StyleValue ValueWithInterpolationBase (nfloat interpolationBase, NSDictionary<NSNumber, StyleValue> stops);

        // +(instancetype _Nonnull)valueWithInterpolationMode:(MGLInterpolationMode)interpolationMode cameraStops:(NSDictionary<id,MGLStyleValue<T> *> * _Nonnull)cameraStops options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("valueWithInterpolationMode:cameraStops:options:")]
        StyleValue ValueWithInterpolationMode (InterpolationMode interpolationMode, NSDictionary<NSObject, StyleValue> cameraStops, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // +(instancetype _Nonnull)valueWithInterpolationMode:(MGLInterpolationMode)interpolationMode sourceStops:(NSDictionary<id,MGLStyleValue<T> *> * _Nullable)sourceStops attributeName:(NSString * _Nonnull)attributeName options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("valueWithInterpolationMode:sourceStops:attributeName:options:")]
        StyleValue ValueWithInterpolationMode (InterpolationMode interpolationMode, [NullAllowed] NSDictionary<NSObject, StyleValue> sourceStops, string attributeName, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // +(instancetype _Nonnull)valueWithInterpolationMode:(MGLInterpolationMode)interpolationMode compositeStops:(NSDictionary<id,NSDictionary<id,MGLStyleValue<T> *> *> * _Nonnull)compositeStops attributeName:(NSString * _Nonnull)attributeName options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("valueWithInterpolationMode:compositeStops:attributeName:options:")]
        StyleValue ValueWithInterpolationMode (InterpolationMode interpolationMode, NSDictionary<NSObject, NSDictionary<NSObject, StyleValue>> compositeStops, string attributeName, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // extern const MGLStyleFunctionOption _Nonnull MGLStyleFunctionOptionInterpolationBase __attribute__((visibility("default")));
        [Field("MGLStyleFunctionOptionInterpolationBase", "__Internal")]
        NSString InterpolationBase { get; }

        // extern const MGLStyleFunctionOption _Nonnull MGLStyleFunctionOptionDefaultValue __attribute__((visibility("default")));
        [Field("MGLStyleFunctionOptionDefaultValue", "__Internal")]
        NSString DefaultValue { get; }
    }

    // audit-objc-generics: @interface ConstantStyleValue<T> : MGLStyleValue
    [BaseType(typeof(StyleValue), Name = "MGLConstantStyleValue")]
    [DisableDefaultCtor]
    interface ConstantStyleValue
    {
        // +(instancetype _Nonnull)valueWithRawValue:(T _Nonnull)rawValue;
        [Static]
        [Export ("valueWithRawValue:")]
        ConstantStyleValue GetValue (NSObject rawValue);

        // -(instancetype _Nonnull)initWithRawValue:(T _Nonnull)rawValue __attribute__((objc_designated_initializer));
        [Export ("initWithRawValue:")]
        [DesignatedInitializer]
        IntPtr Constructor (NSObject rawValue);

        // @property (nonatomic) T _Nonnull rawValue;
        [Export ("rawValue", ArgumentSemantic.Assign)]
        NSObject RawValue { get; set; }
    }

    // audit-objc-generics: @interface StyleFunction<T> : MGLStyleValue
    [BaseType(typeof(StyleValue), Name = "MGLStyleFunction")]
    interface StyleFunction
    {
        // +(instancetype _Nonnull)functionWithStops:(NSDictionary<NSNumber *,MGLStyleValue<T> *> * _Nonnull)stops __attribute__((deprecated("Use +[MGLStyleValue valueWithInterpolationMode:cameraStops:options:]")));
        [Obsolete("Use StyleValue.ValueWithInterpolationMode().")]
        [Static]
        [Export ("functionWithStops:")]
        StyleFunction From (NSDictionary<NSNumber, StyleValue> stops);

        // +(instancetype _Nonnull)functionWithInterpolationBase:(CGFloat)interpolationBase stops:(NSDictionary<NSNumber *,MGLStyleValue<T> *> * _Nonnull)stops __attribute__((deprecated("Use +[MGLStyleValue valueWithInterpolationMode:cameraStops:options:]")));
        [Static]
        [Export ("functionWithInterpolationBase:stops:")]
        StyleFunction From (nfloat interpolationBase, NSDictionary<NSNumber, StyleValue> stops);

        // -(instancetype _Nonnull)initWithInterpolationBase:(CGFloat)interpolationBase stops:(NSDictionary<NSNumber *,MGLStyleValue<T> *> * _Nonnull)stops __attribute__((deprecated("Use +[MGLStyleValue valueWithInterpolationMode:cameraStops:options:]")));
        [Obsolete("Use StyleValue.ValueWithInterpolationMode().")]
        [Export ("initWithInterpolationBase:stops:")]
        IntPtr Constructor (nfloat interpolationBase, NSDictionary<NSNumber, StyleValue> stops);

        // @property (nonatomic) MGLInterpolationMode interpolationMode;
        [Export ("interpolationMode", ArgumentSemantic.Assign)]
        InterpolationMode InterpolationMode { get; set; }

        // @property (copy, nonatomic) NSDictionary * _Nullable stops;
        [NullAllowed, Export ("stops", ArgumentSemantic.Copy)]
        NSDictionary Stops { get; set; }

        // @property (nonatomic) CGFloat interpolationBase;
        [Export ("interpolationBase")]
        nfloat InterpolationBase { get; set; }
    }

    // audit-objc-generics: @interface CameraStyleFunction<T> : MGLStyleFunction
    [BaseType(typeof(StyleFunction), Name = "MGLCameraStyleFunction")]
    interface CameraStyleFunction
    {
        // +(instancetype _Nonnull)functionWithInterpolationMode:(MGLInterpolationMode)interpolationMode stops:(NSDictionary<id,MGLStyleValue<T> *> * _Nonnull)stops options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("functionWithInterpolationMode:stops:options:")]
        CameraStyleFunction From (InterpolationMode interpolationMode, NSDictionary<NSObject, StyleValue> stops, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // @property (copy, nonatomic) NSDictionary<id,MGLStyleValue<T> *> * _Nonnull stops;
        [Export ("stops", ArgumentSemantic.Copy)]
        NSDictionary<NSObject, StyleValue> Stops { get; set; }
    }

    // audit-objc-generics: @interface SourceStyleFunction<T> : MGLStyleFunction
    [BaseType(typeof(StyleFunction), Name = "MGLSourceStyleFunction")]
    interface SourceStyleFunction
    {
        // +(instancetype _Nonnull)functionWithInterpolationMode:(MGLInterpolationMode)interpolationMode stops:(NSDictionary<id,MGLStyleValue<T> *> * _Nullable)stops attributeName:(NSString * _Nonnull)attributeName options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("functionWithInterpolationMode:stops:attributeName:options:")]
        SourceStyleFunction From (InterpolationMode interpolationMode, [NullAllowed] NSDictionary<NSObject, StyleValue> stops, string attributeName, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // @property (copy, nonatomic) NSString * _Nonnull attributeName;
        [Export ("attributeName")]
        string AttributeName { get; set; }

        // @property (copy, nonatomic) NSDictionary<id,MGLStyleValue<T> *> * _Nullable stops;
        [NullAllowed, Export ("stops", ArgumentSemantic.Copy)]
        NSDictionary<NSObject, StyleValue> Stops { get; set; }

        // @property (nonatomic) MGLStyleValue<T> * _Nullable defaultValue;
        [NullAllowed, Export ("defaultValue", ArgumentSemantic.Assign)]
        StyleValue DefaultValue { get; set; }
    }

    // audit-objc-generics: @interface CompositeStyleFunction<T> : MGLStyleFunction
    [BaseType(typeof(StyleFunction), Name = "MGLCompositeStyleFunction")]
    interface CompositeStyleFunction
    {
        // +(instancetype _Nonnull)functionWithInterpolationMode:(MGLInterpolationMode)interpolationMode stops:(NSDictionary<id,NSDictionary<id,MGLStyleValue<T> *> *> * _Nonnull)stops attributeName:(NSString * _Nonnull)attributeName options:(NSDictionary<MGLStyleFunctionOption,id> * _Nullable)options;
        [Static]
        [Export ("functionWithInterpolationMode:stops:attributeName:options:")]
        CompositeStyleFunction From (InterpolationMode interpolationMode, NSDictionary<NSObject, NSDictionary<NSObject, StyleValue>> stops, string attributeName, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // @property (copy, nonatomic) NSString * _Nonnull attributeName;
        [Export ("attributeName")]
        string AttributeName { get; set; }

        // @property (copy, nonatomic) NSDictionary<id,NSDictionary<id,MGLStyleValue<T> *> *> * _Nonnull stops;
        [Export ("stops", ArgumentSemantic.Copy)]
        NSDictionary<NSObject, NSDictionary<NSObject, StyleValue>> Stops { get; set; }

        // @property (nonatomic) MGLStyleValue<T> * _Nullable defaultValue;
        [NullAllowed, Export ("defaultValue", ArgumentSemantic.Assign)]
        StyleValue DefaultValue { get; set; }
    }

    // @interface FillStyleLayer : MGLVectorStyleLayer
    [BaseType(typeof(VectorStyleLayer), Name = "MGLFillStyleLayer")]
    interface FillStyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MGLSource * _Nonnull)source __attribute__((objc_designated_initializer));
        [Export("initWithIdentifier:source:")]
        [DesignatedInitializer]
        IntPtr Constructor(string identifier, Source source);

        // @property (getter = isFillAntialiased, nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified fillAntialiased;
        [Export ("fillAntialiased", ArgumentSemantic.Assign)]
        StyleValue FillAntialiased { [Bind ("isFillAntialiased")] get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified fillColor;
        [Export ("fillColor", ArgumentSemantic.Assign)]
        StyleValue FillColor { get; set; }

        // @property (nonatomic) MGLTransition fillColorTransition;
        [Export ("fillColorTransition", ArgumentSemantic.Assign)]
        Transition FillColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified fillOpacity;
        [Export ("fillOpacity", ArgumentSemantic.Assign)]
        StyleValue FillOpacity { get; set; }

        // @property (nonatomic) MGLTransition fillOpacityTransition;
        [Export ("fillOpacityTransition", ArgumentSemantic.Assign)]
        Transition FillOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified fillOutlineColor;
        [Export ("fillOutlineColor", ArgumentSemantic.Assign)]
        StyleValue FillOutlineColor { get; set; }

        // @property (nonatomic) MGLTransition fillOutlineColorTransition;
        [Export ("fillOutlineColorTransition", ArgumentSemantic.Assign)]
        Transition FillOutlineColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSString *> * _Null_unspecified fillPattern;
        [Export ("fillPattern", ArgumentSemantic.Assign)]
        StyleValue FillPattern { get; set; }

        // @property (nonatomic) MGLTransition fillPatternTransition;
        [Export ("fillPatternTransition", ArgumentSemantic.Assign)]
        Transition FillPatternTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified fillTranslation;
        [Export ("fillTranslation", ArgumentSemantic.Assign)]
        StyleValue FillTranslation { get; set; }

        // @property (nonatomic) MGLTransition fillTranslationTransition;
        [Export ("fillTranslationTransition", ArgumentSemantic.Assign)]
        Transition FillTranslationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified fillTranslationAnchor;
        [Export ("fillTranslationAnchor", ArgumentSemantic.Assign)]
        StyleValue FillTranslationAnchor { get; set; }
    }

	// @interface FillStyleLayerAdditions (NSValue)
	[Category]
	[BaseType(typeof(NSValue))]
	interface NSValue_MGLFillStyleLayerAdditions
	{
		// @property (readonly) MGLFillTranslationAnchor MGLFillTranslationAnchorValue;
		[Export("MGLFillTranslationAnchorValue")]
		FillTranslationAnchor GetFillTranslationAnchor();
	}

    // @interface FillStyleLayerAdditions (NSValue)
	//[BaseType(typeof(NSValue))]
	//[DisableDefaultCtor]
    //interface FillStyleLayerExt
    //{
    //    // +(instancetype _Nonnull)valueWithMGLFillTranslationAnchor:(MGLFillTranslationAnchor)fillTranslationAnchor;
    //    [Static]
    //    [Export ("valueWithMGLFillTranslationAnchor:")]
    //    NSValue GetValue (FillTranslationAnchor fillTranslationAnchor);
    //}

    // @interface LineStyleLayer : MGLVectorStyleLayer
    [BaseType(typeof(VectorStyleLayer), Name = "MGLLineStyleLayer")]
    interface LineStyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MGLSource * _Nonnull)source __attribute__((objc_designated_initializer));
        [Export("initWithIdentifier:source:")]
        [DesignatedInitializer]
        IntPtr Constructor(string identifier, Source source);

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified lineCap;
        [Export ("lineCap", ArgumentSemantic.Assign)]
        StyleValue LineCap { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified lineJoin;
        [Export ("lineJoin", ArgumentSemantic.Assign)]
        StyleValue LineJoin { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineMiterLimit;
        [Export ("lineMiterLimit", ArgumentSemantic.Assign)]
        StyleValue LineMiterLimit { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineRoundLimit;
        [Export ("lineRoundLimit", ArgumentSemantic.Assign)]
        StyleValue LineRoundLimit { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineBlur;
        [Export ("lineBlur", ArgumentSemantic.Assign)]
        StyleValue LineBlur { get; set; }

        // @property (nonatomic) MGLTransition lineBlurTransition;
        [Export ("lineBlurTransition", ArgumentSemantic.Assign)]
        Transition LineBlurTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified lineColor;
        [Export ("lineColor", ArgumentSemantic.Assign)]
        StyleValue LineColor { get; set; }

        // @property (nonatomic) MGLTransition lineColorTransition;
        [Export ("lineColorTransition", ArgumentSemantic.Assign)]
        Transition LineColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSArray<NSNumber *> *> * _Null_unspecified lineDashPattern;
        [Export ("lineDashPattern", ArgumentSemantic.Assign)]
        StyleValue LineDashPattern { get; set; }

        // @property (nonatomic) MGLTransition lineDashPatternTransition;
        [Export ("lineDashPatternTransition", ArgumentSemantic.Assign)]
        Transition LineDashPatternTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineGapWidth;
        [Export ("lineGapWidth", ArgumentSemantic.Assign)]
        StyleValue LineGapWidth { get; set; }

        // @property (nonatomic) MGLTransition lineGapWidthTransition;
        [Export ("lineGapWidthTransition", ArgumentSemantic.Assign)]
        Transition LineGapWidthTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineOffset;
        [Export ("lineOffset", ArgumentSemantic.Assign)]
        StyleValue LineOffset { get; set; }

        // @property (nonatomic) MGLTransition lineOffsetTransition;
        [Export ("lineOffsetTransition", ArgumentSemantic.Assign)]
        Transition LineOffsetTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineOpacity;
        [Export ("lineOpacity", ArgumentSemantic.Assign)]
        StyleValue LineOpacity { get; set; }

        // @property (nonatomic) MGLTransition lineOpacityTransition;
        [Export ("lineOpacityTransition", ArgumentSemantic.Assign)]
        Transition LineOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSString *> * _Null_unspecified linePattern;
        [Export ("linePattern", ArgumentSemantic.Assign)]
        StyleValue LinePattern { get; set; }

        // @property (nonatomic) MGLTransition linePatternTransition;
        [Export ("linePatternTransition", ArgumentSemantic.Assign)]
        Transition LinePatternTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified lineTranslation;
        [Export ("lineTranslation", ArgumentSemantic.Assign)]
        StyleValue LineTranslation { get; set; }

        // @property (nonatomic) MGLTransition lineTranslationTransition;
        [Export ("lineTranslationTransition", ArgumentSemantic.Assign)]
        Transition LineTranslationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified lineTranslationAnchor;
        [Export ("lineTranslationAnchor", ArgumentSemantic.Assign)]
        StyleValue LineTranslationAnchor { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified lineWidth;
        [Export ("lineWidth", ArgumentSemantic.Assign)]
        StyleValue LineWidth { get; set; }

        // @property (nonatomic) MGLTransition lineWidthTransition;
        [Export ("lineWidthTransition", ArgumentSemantic.Assign)]
        Transition LineWidthTransition { get; set; }
    }

    // @interface LineStyleLayerAdditions (NSValue)
    [Category]
    [BaseType (typeof(NSValue))]
    interface NSValue_MGLLineStyleLayerAdditions
    {
		// @property (readonly) MGLLineCap MGLLineCapValue;
		[Export("MGLLineCapValue")]
		LineCap GetLineCap();

		// @property (readonly) MGLLineJoin MGLLineJoinValue;
		[Export("MGLLineJoinValue")]
		LineJoin GetLineJoin();

		// @property (readonly) MGLLineTranslationAnchor MGLLineTranslationAnchorValue;
		[Export("MGLLineTranslationAnchorValue")]
		LineTranslationAnchor GetLineTranslationAnchor();
    }

	// @interface LineStyleLayerAdditions (NSValue)
	//[BaseType(typeof(NSValue))]
	//[DisableDefaultCtor]
	//interface MGLLineStyleLayerExt
	//{
	//	// +(instancetype _Nonnull)valueWithMGLLineCap:(MGLLineCap)lineCap;
	//	[Static]
	//	[Export("valueWithMGLLineCap:")]
	//	NSValue GetValue(LineCap lineCap);

	//	// +(instancetype _Nonnull)valueWithMGLLineJoin:(MGLLineJoin)lineJoin;
	//	[Static]
	//	[Export("valueWithMGLLineJoin:")]
	//	NSValue GetValue(LineJoin lineJoin);

	//	// +(instancetype _Nonnull)valueWithMGLLineTranslationAnchor:(MGLLineTranslationAnchor)lineTranslationAnchor;
	//	[Static]
	//	[Export("valueWithMGLLineTranslationAnchor:")]
	//	NSValue GetValue(LineTranslationAnchor lineTranslationAnchor);
	//}

    // @interface SymbolStyleLayer : MGLVectorStyleLayer
    [BaseType(typeof(VectorStyleLayer), Name = "MGLSymbolStyleLayer")]
    interface SymbolStyleLayer
    {
		// -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MGLSource * _Nonnull)source __attribute__((objc_designated_initializer));
		[Export("initWithIdentifier:source:")]
		[DesignatedInitializer]
		IntPtr Constructor(string identifier, Source source);

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconAllowsOverlap;
        [Export ("iconAllowsOverlap", ArgumentSemantic.Assign)]
        StyleValue IconAllowsOverlap { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconIgnoresPlacement;
        [Export ("iconIgnoresPlacement", ArgumentSemantic.Assign)]
        StyleValue IconIgnoresPlacement { get; set; }

        // @property (nonatomic) MGLStyleValue<NSString *> * _Null_unspecified iconImageName;
        [Export ("iconImageName", ArgumentSemantic.Assign)]
        StyleValue IconImageName { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconOffset;
        [Export ("iconOffset", ArgumentSemantic.Assign)]
        StyleValue IconOffset { get; set; }

        // @property (getter = isIconOptional, nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconOptional;
        [Export ("iconOptional", ArgumentSemantic.Assign)]
        StyleValue IconOptional { [Bind ("isIconOptional")] get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconPadding;
        [Export ("iconPadding", ArgumentSemantic.Assign)]
        StyleValue IconPadding { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconRotation;
        [Export ("iconRotation", ArgumentSemantic.Assign)]
        StyleValue IconRotation { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconRotationAlignment;
        [Export ("iconRotationAlignment", ArgumentSemantic.Assign)]
        StyleValue IconRotationAlignment { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconScale;
        [Export ("iconScale", ArgumentSemantic.Assign)]
        StyleValue IconScale { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconTextFit;
        [Export ("iconTextFit", ArgumentSemantic.Assign)]
        StyleValue IconTextFit { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconTextFitPadding;
        [Export ("iconTextFitPadding", ArgumentSemantic.Assign)]
        StyleValue IconTextFitPadding { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified keepsIconUpright;
        [Export ("keepsIconUpright", ArgumentSemantic.Assign)]
        StyleValue KeepsIconUpright { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified keepsTextUpright;
        [Export ("keepsTextUpright", ArgumentSemantic.Assign)]
        StyleValue KeepsTextUpright { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified maximumTextAngle;
        [Export ("maximumTextAngle", ArgumentSemantic.Assign)]
        StyleValue MaximumTextAngle { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified maximumTextWidth;
        [Export ("maximumTextWidth", ArgumentSemantic.Assign)]
        StyleValue MaximumTextWidth { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified symbolAvoidsEdges;
        [Export ("symbolAvoidsEdges", ArgumentSemantic.Assign)]
        StyleValue SymbolAvoidsEdges { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified symbolPlacement;
        [Export ("symbolPlacement", ArgumentSemantic.Assign)]
        StyleValue SymbolPlacement { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified symbolSpacing;
        [Export ("symbolSpacing", ArgumentSemantic.Assign)]
        StyleValue SymbolSpacing { get; set; }

        // @property (nonatomic) MGLStyleValue<NSString *> * _Null_unspecified text;
        [Export ("text", ArgumentSemantic.Assign)]
        StyleValue Text { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textAllowsOverlap;
        [Export ("textAllowsOverlap", ArgumentSemantic.Assign)]
        StyleValue TextAllowsOverlap { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textAnchor;
        [Export ("textAnchor", ArgumentSemantic.Assign)]
        StyleValue TextAnchor { get; set; }

        // @property (nonatomic) MGLStyleValue<NSArray<NSString *> *> * _Null_unspecified textFontNames;
        [Export ("textFontNames", ArgumentSemantic.Assign)]
        StyleValue TextFontNames { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textFontSize;
        [Export ("textFontSize", ArgumentSemantic.Assign)]
        StyleValue TextFontSize { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textIgnoresPlacement;
        [Export ("textIgnoresPlacement", ArgumentSemantic.Assign)]
        StyleValue TextIgnoresPlacement { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textJustification;
        [Export ("textJustification", ArgumentSemantic.Assign)]
        StyleValue TextJustification { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textLetterSpacing;
        [Export ("textLetterSpacing", ArgumentSemantic.Assign)]
        StyleValue TextLetterSpacing { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textLineHeight;
        [Export ("textLineHeight", ArgumentSemantic.Assign)]
        StyleValue TextLineHeight { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textOffset;
        [Export ("textOffset", ArgumentSemantic.Assign)]
        StyleValue TextOffset { get; set; }

        // @property (getter = isTextOptional, nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textOptional;
        [Export ("textOptional", ArgumentSemantic.Assign)]
        StyleValue TextOptional { [Bind ("isTextOptional")] get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textPadding;
        [Export ("textPadding", ArgumentSemantic.Assign)]
        StyleValue TextPadding { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textPitchAlignment;
        [Export ("textPitchAlignment", ArgumentSemantic.Assign)]
        StyleValue TextPitchAlignment { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textRotation;
        [Export ("textRotation", ArgumentSemantic.Assign)]
        StyleValue TextRotation { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textRotationAlignment;
        [Export ("textRotationAlignment", ArgumentSemantic.Assign)]
        StyleValue TextRotationAlignment { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textTransform;
        [Export ("textTransform", ArgumentSemantic.Assign)]
        StyleValue TextTransform { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified iconColor;
        [Export ("iconColor", ArgumentSemantic.Assign)]
        StyleValue IconColor { get; set; }

        // @property (nonatomic) MGLTransition iconColorTransition;
        [Export ("iconColorTransition", ArgumentSemantic.Assign)]
        Transition IconColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconHaloBlur;
        [Export ("iconHaloBlur", ArgumentSemantic.Assign)]
        StyleValue IconHaloBlur { get; set; }

        // @property (nonatomic) MGLTransition iconHaloBlurTransition;
        [Export ("iconHaloBlurTransition", ArgumentSemantic.Assign)]
        Transition IconHaloBlurTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified iconHaloColor;
        [Export ("iconHaloColor", ArgumentSemantic.Assign)]
        StyleValue IconHaloColor { get; set; }

        // @property (nonatomic) MGLTransition iconHaloColorTransition;
        [Export ("iconHaloColorTransition", ArgumentSemantic.Assign)]
        Transition IconHaloColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconHaloWidth;
        [Export ("iconHaloWidth", ArgumentSemantic.Assign)]
        StyleValue IconHaloWidth { get; set; }

        // @property (nonatomic) MGLTransition iconHaloWidthTransition;
        [Export ("iconHaloWidthTransition", ArgumentSemantic.Assign)]
        Transition IconHaloWidthTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified iconOpacity;
        [Export ("iconOpacity", ArgumentSemantic.Assign)]
        StyleValue IconOpacity { get; set; }

        // @property (nonatomic) MGLTransition iconOpacityTransition;
        [Export ("iconOpacityTransition", ArgumentSemantic.Assign)]
        Transition IconOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconTranslation;
        [Export ("iconTranslation", ArgumentSemantic.Assign)]
        StyleValue IconTranslation { get; set; }

        // @property (nonatomic) MGLTransition iconTranslationTransition;
        [Export ("iconTranslationTransition", ArgumentSemantic.Assign)]
        Transition IconTranslationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified iconTranslationAnchor;
        [Export ("iconTranslationAnchor", ArgumentSemantic.Assign)]
        StyleValue IconTranslationAnchor { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified textColor;
        [Export ("textColor", ArgumentSemantic.Assign)]
        StyleValue TextColor { get; set; }

        // @property (nonatomic) MGLTransition textColorTransition;
        [Export ("textColorTransition", ArgumentSemantic.Assign)]
        Transition TextColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textHaloBlur;
        [Export ("textHaloBlur", ArgumentSemantic.Assign)]
        StyleValue TextHaloBlur { get; set; }

        // @property (nonatomic) MGLTransition textHaloBlurTransition;
        [Export ("textHaloBlurTransition", ArgumentSemantic.Assign)]
        Transition TextHaloBlurTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified textHaloColor;
        [Export ("textHaloColor", ArgumentSemantic.Assign)]
        StyleValue TextHaloColor { get; set; }

        // @property (nonatomic) MGLTransition textHaloColorTransition;
        [Export ("textHaloColorTransition", ArgumentSemantic.Assign)]
        Transition TextHaloColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textHaloWidth;
        [Export ("textHaloWidth", ArgumentSemantic.Assign)]
        StyleValue TextHaloWidth { get; set; }

        // @property (nonatomic) MGLTransition textHaloWidthTransition;
        [Export ("textHaloWidthTransition", ArgumentSemantic.Assign)]
        Transition TextHaloWidthTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified textOpacity;
        [Export ("textOpacity", ArgumentSemantic.Assign)]
        StyleValue TextOpacity { get; set; }

        // @property (nonatomic) MGLTransition textOpacityTransition;
        [Export ("textOpacityTransition", ArgumentSemantic.Assign)]
        Transition TextOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textTranslation;
        [Export ("textTranslation", ArgumentSemantic.Assign)]
        StyleValue TextTranslation { get; set; }

        // @property (nonatomic) MGLTransition textTranslationTransition;
        [Export ("textTranslationTransition", ArgumentSemantic.Assign)]
        Transition TextTranslationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified textTranslationAnchor;
        [Export ("textTranslationAnchor", ArgumentSemantic.Assign)]
        StyleValue TextTranslationAnchor { get; set; }
    }

    // @interface SymbolStyleLayerAdditions (NSValue)
    [Category]
    [BaseType (typeof(NSValue))]
    interface NSValue_MGLSymbolStyleLayerAdditions
    {
        // @property (readonly) MGLIconRotationAlignment MGLIconRotationAlignmentValue;
        [Export ("MGLIconRotationAlignmentValue")]
        IconRotationAlignment GetIconRotationAlignment();

        // @property (readonly) MGLIconTextFit MGLIconTextFitValue;
        [Export ("MGLIconTextFitValue")]
        IconTextFit GetIconTextFit();

        // @property (readonly) MGLSymbolPlacement MGLSymbolPlacementValue;
        [Export ("MGLSymbolPlacementValue")]
        SymbolPlacement GetSymbolPlacement();

        // @property (readonly) MGLTextAnchor MGLTextAnchorValue;
        [Export ("MGLTextAnchorValue")]
        TextAnchor GetTextAnchor();

        // @property (readonly) MGLTextJustification MGLTextJustificationValue;
        [Export ("MGLTextJustificationValue")]
        TextJustification GetTextJustification();

        // @property (readonly) MGLTextPitchAlignment MGLTextPitchAlignmentValue;
        [Export ("MGLTextPitchAlignmentValue")]
        TextPitchAlignment GetTextPitchAlignment();

        // @property (readonly) MGLTextRotationAlignment MGLTextRotationAlignmentValue;
        [Export ("MGLTextRotationAlignmentValue")]
        TextRotationAlignment GetTextRotationAlignment();

        // @property (readonly) MGLTextTransform MGLTextTransformValue;
        [Export ("MGLTextTransformValue")]
        TextTransform GetTextTransform();

        // @property (readonly) MGLIconTranslationAnchor MGLIconTranslationAnchorValue;
        [Export ("MGLIconTranslationAnchorValue")]
        IconTranslationAnchor GetIconTranslationAnchor();

        // @property (readonly) MGLTextTranslationAnchor MGLTextTranslationAnchorValue;
        [Export ("MGLTextTranslationAnchorValue")]
        TextTranslationAnchor GetTextTranslationAnchor();
    }

	// @interface SymbolStyleLayerAdditions (NSValue)
	//[BaseType(typeof(NSValue))]
	//[DisableDefaultCtor]
	//interface SymbolStyleLayerAdditionsExt
	//{
	//	// +(instancetype _Nonnull)valueWithMGLIconRotationAlignment:(MGLIconRotationAlignment)iconRotationAlignment;
	//	[Static]
	//	[Export("valueWithMGLIconRotationAlignment:")]
	//	NSValue GetValue(IconRotationAlignment iconRotationAlignment);

	//	// +(instancetype _Nonnull)valueWithMGLIconTextFit:(MGLIconTextFit)iconTextFit;
	//	[Static]
	//	[Export("valueWithMGLIconTextFit:")]
	//	NSValue GetValue(IconTextFit iconTextFit);

	//	// +(instancetype _Nonnull)valueWithMGLSymbolPlacement:(MGLSymbolPlacement)symbolPlacement;
	//	[Static]
	//	[Export("valueWithMGLSymbolPlacement:")]
	//	NSValue GetValue(SymbolPlacement symbolPlacement);

	//	// +(instancetype _Nonnull)valueWithMGLTextAnchor:(MGLTextAnchor)textAnchor;
	//	[Static]
	//	[Export("valueWithMGLTextAnchor:")]
	//	NSValue GetValue(TextAnchor textAnchor);

	//	// +(instancetype _Nonnull)valueWithMGLTextJustification:(MGLTextJustification)textJustification;
	//	[Static]
	//	[Export("valueWithMGLTextJustification:")]
	//	NSValue GetValue(TextJustification textJustification);

	//	// +(instancetype _Nonnull)valueWithMGLTextPitchAlignment:(MGLTextPitchAlignment)textPitchAlignment;
	//	[Static]
	//	[Export("valueWithMGLTextPitchAlignment:")]
	//	NSValue GetValue(TextPitchAlignment textPitchAlignment);

	//	// +(instancetype _Nonnull)valueWithMGLTextRotationAlignment:(MGLTextRotationAlignment)textRotationAlignment;
	//	[Static]
	//	[Export("valueWithMGLTextRotationAlignment:")]
	//	NSValue GetValue(TextRotationAlignment textRotationAlignment);

	//	// +(instancetype _Nonnull)valueWithMGLTextTransform:(MGLTextTransform)textTransform;
	//	[Static]
	//	[Export("valueWithMGLTextTransform:")]
	//	NSValue GetValue(TextTransform textTransform);

	//	// +(instancetype _Nonnull)valueWithMGLIconTranslationAnchor:(MGLIconTranslationAnchor)iconTranslationAnchor;
	//	[Static]
	//	[Export("valueWithMGLIconTranslationAnchor:")]
	//	NSValue GetValue(IconTranslationAnchor iconTranslationAnchor);

	//	// +(instancetype _Nonnull)valueWithMGLTextTranslationAnchor:(MGLTextTranslationAnchor)textTranslationAnchor;
	//	[Static]
	//	[Export("valueWithMGLTextTranslationAnchor:")]
	//	NSValue GetValue(TextTranslationAnchor textTranslationAnchor);
	//}


    // @interface RasterStyleLayer : MGLForegroundStyleLayer
    [BaseType(typeof(ForegroundStyleLayer), Name = "MGLRasterStyleLayer")]
    interface RasterStyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier source:(MGLSource * _Nonnull)source;
        [Export("initWithIdentifier:source:")]
        IntPtr Constructor(string identifier, Source source);

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified maximumRasterBrightness;
        [Export ("maximumRasterBrightness", ArgumentSemantic.Assign)]
        StyleValue MaximumRasterBrightness { get; set; }

        // @property (nonatomic) MGLTransition maximumRasterBrightnessTransition;
        [Export ("maximumRasterBrightnessTransition", ArgumentSemantic.Assign)]
        Transition MaximumRasterBrightnessTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified minimumRasterBrightness;
        [Export ("minimumRasterBrightness", ArgumentSemantic.Assign)]
        StyleValue MinimumRasterBrightness { get; set; }

        // @property (nonatomic) MGLTransition minimumRasterBrightnessTransition;
        [Export ("minimumRasterBrightnessTransition", ArgumentSemantic.Assign)]
        Transition MinimumRasterBrightnessTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified rasterContrast;
        [Export ("rasterContrast", ArgumentSemantic.Assign)]
        StyleValue RasterContrast { get; set; }

        // @property (nonatomic) MGLTransition rasterContrastTransition;
        [Export ("rasterContrastTransition", ArgumentSemantic.Assign)]
        Transition RasterContrastTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified rasterFadeDuration;
        [Export ("rasterFadeDuration", ArgumentSemantic.Assign)]
        StyleValue RasterFadeDuration { get; set; }

        // @property (nonatomic) MGLTransition rasterFadeDurationTransition;
        [Export ("rasterFadeDurationTransition", ArgumentSemantic.Assign)]
        Transition RasterFadeDurationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified rasterHueRotation;
        [Export ("rasterHueRotation", ArgumentSemantic.Assign)]
        StyleValue RasterHueRotation { get; set; }

        // @property (nonatomic) MGLTransition rasterHueRotationTransition;
        [Export ("rasterHueRotationTransition", ArgumentSemantic.Assign)]
        Transition RasterHueRotationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified rasterOpacity;
        [Export ("rasterOpacity", ArgumentSemantic.Assign)]
        StyleValue RasterOpacity { get; set; }

        // @property (nonatomic) MGLTransition rasterOpacityTransition;
        [Export ("rasterOpacityTransition", ArgumentSemantic.Assign)]
        Transition RasterOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified rasterSaturation;
        [Export ("rasterSaturation", ArgumentSemantic.Assign)]
        StyleValue RasterSaturation { get; set; }

        // @property (nonatomic) MGLTransition rasterSaturationTransition;
        [Export ("rasterSaturationTransition", ArgumentSemantic.Assign)]
        Transition RasterSaturationTransition { get; set; }
    }

    // @interface CircleStyleLayer : MGLVectorStyleLayer
    [BaseType(typeof(VectorStyleLayer), Name = "MGLCircleStyleLayer")]
    interface CircleStyleLayer
    {
        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified circleBlur;
        [Export ("circleBlur", ArgumentSemantic.Assign)]
        StyleValue CircleBlur { get; set; }

        // @property (nonatomic) MGLTransition circleBlurTransition;
        [Export ("circleBlurTransition", ArgumentSemantic.Assign)]
        Transition CircleBlurTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified circleColor;
        [Export ("circleColor", ArgumentSemantic.Assign)]
        StyleValue CircleColor { get; set; }

        // @property (nonatomic) MGLTransition circleColorTransition;
        [Export ("circleColorTransition", ArgumentSemantic.Assign)]
        Transition CircleColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified circleOpacity;
        [Export ("circleOpacity", ArgumentSemantic.Assign)]
        StyleValue CircleOpacity { get; set; }

        // @property (nonatomic) MGLTransition circleOpacityTransition;
        [Export ("circleOpacityTransition", ArgumentSemantic.Assign)]
        Transition CircleOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified circleRadius;
        [Export ("circleRadius", ArgumentSemantic.Assign)]
        StyleValue CircleRadius { get; set; }

        // @property (nonatomic) MGLTransition circleRadiusTransition;
        [Export ("circleRadiusTransition", ArgumentSemantic.Assign)]
        Transition CircleRadiusTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified circleScaleAlignment;
        [Export ("circleScaleAlignment", ArgumentSemantic.Assign)]
        StyleValue CircleScaleAlignment { get; set; }

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified circleStrokeColor;
        [Export ("circleStrokeColor", ArgumentSemantic.Assign)]
        StyleValue CircleStrokeColor { get; set; }

        // @property (nonatomic) MGLTransition circleStrokeColorTransition;
        [Export ("circleStrokeColorTransition", ArgumentSemantic.Assign)]
        Transition CircleStrokeColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified circleStrokeOpacity;
        [Export ("circleStrokeOpacity", ArgumentSemantic.Assign)]
        StyleValue CircleStrokeOpacity { get; set; }

        // @property (nonatomic) MGLTransition circleStrokeOpacityTransition;
        [Export ("circleStrokeOpacityTransition", ArgumentSemantic.Assign)]
        Transition CircleStrokeOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified circleStrokeWidth;
        [Export ("circleStrokeWidth", ArgumentSemantic.Assign)]
        StyleValue CircleStrokeWidth { get; set; }

        // @property (nonatomic) MGLTransition circleStrokeWidthTransition;
        [Export ("circleStrokeWidthTransition", ArgumentSemantic.Assign)]
        Transition CircleStrokeWidthTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified circleTranslation;
        [Export ("circleTranslation", ArgumentSemantic.Assign)]
        StyleValue CircleTranslation { get; set; }

        // @property (nonatomic) MGLTransition circleTranslationTransition;
        [Export ("circleTranslationTransition", ArgumentSemantic.Assign)]
        Transition CircleTranslationTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSValue *> * _Null_unspecified circleTranslationAnchor;
        [Export ("circleTranslationAnchor", ArgumentSemantic.Assign)]
        StyleValue CircleTranslationAnchor { get; set; }
    }

    // @interface MGLCustomStyleLayerAdditions (MGLMapView)
    [Category]
	[BaseType(typeof(NSValue))]
    interface NSValue_MGLCircleStyleLayerAdditions
    {
		// @property (readonly) MGLCircleScaleAlignment MGLCircleScaleAlignmentValue;
		[Export("MGLCircleScaleAlignmentValue")]
		CircleScaleAlignment GetCircleScaleAlignment();

        // @property (readonly) MGLCircleTranslationAnchor MGLCircleTranslationAnchorValue;
        [Export ("MGLCircleTranslationAnchorValue")]
        CircleTranslationAnchor GetCircleTranslationAnchor();
    }

	// @interface MGLCustomStyleLayerAdditions (MGLMapView)
	//[BaseType(typeof(NSValue))]
	//[DisableDefaultCtor]
	//interface CircleStyleLayerExt
	//{
	//	// +(instancetype _Nonnull)valueWithMGLCircleScaleAlignment:(MGLCircleScaleAlignment)circleScaleAlignment;
	//	[Static]
	//	[Export("valueWithMGLCircleScaleAlignment:")]
	//	NSValue GetValue(CircleScaleAlignment circleScaleAlignment);

	//	// +(instancetype _Nonnull)valueWithMGLCircleTranslationAnchor:(MGLCircleTranslationAnchor)circleTranslationAnchor;
	//	[Static]
	//	[Export("valueWithMGLCircleTranslationAnchor:")]
	//	NSValue GetValue(CircleTranslationAnchor circleTranslationAnchor);
	//}

    // @interface BackgroundStyleLayer : MGLStyleLayer
    [BaseType(typeof(StyleLayer), Name = "MGLBackgroundStyleLayer")]
    interface BackgroundStyleLayer
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier);

        // @property (nonatomic) MGLStyleValue<UIColor *> * _Null_unspecified backgroundColor;
        [Export ("backgroundColor", ArgumentSemantic.Assign)]
        StyleValue BackgroundColor { get; set; }

        // @property (nonatomic) MGLTransition backgroundColorTransition;
        [Export ("backgroundColorTransition", ArgumentSemantic.Assign)]
        Transition BackgroundColorTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSNumber *> * _Null_unspecified backgroundOpacity;
        [Export ("backgroundOpacity", ArgumentSemantic.Assign)]
        StyleValue BackgroundOpacity { get; set; }

        // @property (nonatomic) MGLTransition backgroundOpacityTransition;
        [Export ("backgroundOpacityTransition", ArgumentSemantic.Assign)]
        Transition BackgroundOpacityTransition { get; set; }

        // @property (nonatomic) MGLStyleValue<NSString *> * _Null_unspecified backgroundPattern;
        [Export ("backgroundPattern", ArgumentSemantic.Assign)]
        StyleValue BackgroundPattern { get; set; }

        // @property (nonatomic) MGLTransition backgroundPatternTransition;
        [Export ("backgroundPatternTransition", ArgumentSemantic.Assign)]
        Transition BackgroundPatternTransition { get; set; }
    }

    // @interface OpenGLStyleLayer : MGLStyleLayer
    [BaseType(typeof(StyleLayer), Name = "MGLOpenGLStyleLayer")]
    interface OpenGLStyleLayer
    {
        // @property (readonly, nonatomic, weak) MGLMapView * _Nullable mapView;
        [NullAllowed, Export ("mapView", ArgumentSemantic.Weak)]
        MapView MapView { get; }

        // -(void)didMoveToMapView:(MGLMapView * _Nonnull)mapView;
        [Export ("didMoveToMapView:")]
        void DidMoveToMapView (MapView mapView);

        // -(void)willMoveFromMapView:(MGLMapView * _Nonnull)mapView;
        [Export ("willMoveFromMapView:")]
        void WillMoveFromMapView (MapView mapView);

        // -(void)drawInMapView:(MGLMapView * _Nonnull)mapView withContext:(MGLStyleLayerDrawingContext)context;
        [Export ("drawInMapView:withContext:")]
        void DrawInMapView (MapView mapView, StyleLayerDrawingContext context);

        // -(void)setNeedsDisplay;
        [Export ("setNeedsDisplay")]
        void SetNeedsDisplay ();
    }

    // @interface Source : NSObject
    [BaseType (typeof(NSObject), Name = "MGLSource")]
    [DisableDefaultCtor]
    interface Source
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("initWithIdentifier:")]
        IntPtr Constructor (string identifier);

        // @property (copy, nonatomic) NSString * _Nonnull identifier;
        [Export ("identifier")]
        string Identifier { get; set; }
    }

    [Static]
    ////[Verify (ConstantsInterfaceAssociation)]
    partial interface TileSourceOptionConstants
    {
        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionMinimumZoomLevel __attribute__((visibility("default")));
        [Field ("MGLTileSourceOptionMinimumZoomLevel", "__Internal")]
        NSString MinimumZoomLevel { get; }

        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionMaximumZoomLevel __attribute__((visibility("default")));
        [Field ("MGLTileSourceOptionMaximumZoomLevel", "__Internal")]
        NSString MaximumZoomLevel { get; }

        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionAttributionHTMLString __attribute__((visibility("default")));
        [Field ("MGLTileSourceOptionAttributionHTMLString", "__Internal")]
        NSString AttributionHTMLString { get; }

        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionAttributionInfos __attribute__((visibility("default")));
        [Field ("MGLTileSourceOptionAttributionInfos", "__Internal")]
        NSString AttributionInfos { get; }

        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionTileCoordinateSystem __attribute__((visibility("default")));
        [Field ("MGLTileSourceOptionTileCoordinateSystem", "__Internal")]
        NSString TileCoordinateSystem { get; }

        // extern const MGLTileSourceOption _Nonnull MGLTileSourceOptionTileSize __attribute__((visibility("default")));
        [Field("MGLTileSourceOptionTileSize", "__Internal")]
        NSString TileSize { get; }
    }

    // @interface TileSource : MGLSource
    [BaseType(typeof(Source), Name = "MGLTileSource")]
    [DisableDefaultCtor]
    interface TileSource
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSURL * _Nonnull)configurationURL;
        [Export ("initWithIdentifier:configurationURL:")]
        IntPtr Constructor (string identifier, NSUrl configurationURL);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier tileURLTemplates:(NSArray<NSString *> * _Nonnull)tileURLTemplates options:(NSDictionary<MGLTileSourceOption,id> * _Nullable)options;
        [Export ("initWithIdentifier:tileURLTemplates:options:")]
        IntPtr Constructor (string identifier, string[] tileURLTemplates, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // @property (readonly, copy, nonatomic) NSURL * _Nullable configurationURL;
        [NullAllowed, Export ("configurationURL", ArgumentSemantic.Copy)]
        NSUrl ConfigurationURL { get; }

        // @property (readonly, copy, nonatomic) NSArray<MGLAttributionInfo *> * _Nonnull attributionInfos;
        [Export ("attributionInfos", ArgumentSemantic.Copy)]
        AttributionInfo[] AttributionInfos { get; }
    }

    // @interface VectorSource : MGLTileSource
    [BaseType (typeof(TileSource), Name = "MGLVectorSource")]
    interface VectorSource
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSURL * _Nonnull)configurationURL __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:configurationURL:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, NSUrl configurationURL);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier tileURLTemplates:(NSArray<NSString *> * _Nonnull)tileURLTemplates options:(NSDictionary<MGLTileSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:tileURLTemplates:options:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, string[] tileURLTemplates, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // -(NSArray<id<MGLFeature>> * _Nonnull)featuresInSourceLayersWithIdentifiers:(NSSet<NSString *> * _Nonnull)sourceLayerIdentifiers predicate:(NSPredicate * _Nullable)predicate;
        [Export ("featuresInSourceLayersWithIdentifiers:predicate:")]
        IFeature[] GetFeatures (NSSet<NSString> sourceLayerIdentifiers, [NullAllowed] NSPredicate predicate);
    }

    [Static]
    //[Verify (ConstantsInterfaceAssociation)]
    partial interface ShapeSourceOptionConstants
    {
        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionClustered __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionClustered", "__Internal")]
        NSString Clustered { get; }

        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionClusterRadius __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionClusterRadius", "__Internal")]
        NSString ClusterRadius { get; }

        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionMaximumZoomLevelForClustering __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionMaximumZoomLevelForClustering", "__Internal")]
        NSString MaximumZoomLevelForClustering { get; }

        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionMaximumZoomLevel __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionMaximumZoomLevel", "__Internal")]
        NSString MaximumZoomLevel { get; }

        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionBuffer __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionBuffer", "__Internal")]
        NSString Buffer { get; }

        // extern const MGLShapeSourceOption _Nonnull MGLShapeSourceOptionSimplificationTolerance __attribute__((visibility("default")));
        [Field ("MGLShapeSourceOptionSimplificationTolerance", "__Internal")]
        NSString SimplificationTolerance { get; }
    }

    // @interface ShapeSource : MGLSource
    [BaseType (typeof(Source), Name = "MGLShapeSource")]
    interface ShapeSource
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier URL:(NSURL * _Nonnull)url options:(NSDictionary<MGLShapeSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:URL:options:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier shape:(MGLShape * _Nullable)shape options:(NSDictionary<MGLShapeSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:shape:options:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, [NullAllowed] Shape shape, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier features:(NSArray<MGLShape<MGLFeature> *> * _Nonnull)features options:(NSDictionary<MGLShapeSourceOption,id> * _Nullable)options;
        [Export ("initWithIdentifier:features:options:")]
        IntPtr Constructor (string identifier, IFeature[] features, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier shapes:(NSArray<MGLShape *> * _Nonnull)shapes options:(NSDictionary<MGLShapeSourceOption,id> * _Nullable)options;
        [Export ("initWithIdentifier:shapes:options:")]
        IntPtr Constructor (string identifier, Shape[] shapes, [NullAllowed] NSDictionary<NSString, NSObject> options);

        // @property (copy, nonatomic) MGLShape * _Nullable shape;
        [NullAllowed, Export ("shape", ArgumentSemantic.Copy)]
        Shape Shape { get; set; }

        // @property (copy, nonatomic) NSURL * _Nullable URL;
        [NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
        NSUrl Url { get; set; }

        // -(NSArray<id<MGLFeature>> * _Nonnull)featuresMatchingPredicate:(NSPredicate * _Nullable)predicate;
        [Export ("featuresMatchingPredicate:")]
        IFeature[] GetFeatures ([NullAllowed] NSPredicate predicate);
    }

    // @interface RasterSource : MGLTileSource
    [BaseType (typeof(TileSource), Name = "MGLRasterSource")]
    interface RasterSource
    {
        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSURL * _Nonnull)configurationURL;
        [Export ("initWithIdentifier:configurationURL:")]
        IntPtr Constructor (string identifier, NSUrl configurationURL);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier configurationURL:(NSURL * _Nonnull)configurationURL tileSize:(CGFloat)tileSize __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:configurationURL:tileSize:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, NSUrl configurationURL, nfloat tileSize);

        // -(instancetype _Nonnull)initWithIdentifier:(NSString * _Nonnull)identifier tileURLTemplates:(NSArray<NSString *> * _Nonnull)tileURLTemplates options:(NSDictionary<MGLTileSourceOption,id> * _Nullable)options __attribute__((objc_designated_initializer));
        [Export ("initWithIdentifier:tileURLTemplates:options:")]
        [DesignatedInitializer]
        IntPtr Constructor (string identifier, string[] tileURLTemplates, [NullAllowed] NSDictionary<NSString, NSObject> options);
    }

    // @interface UserLocationAnnotationView : MGLAnnotationView
    [BaseType (typeof(AnnotationView), Name = "MGLUserLocationAnnotationView")]
    interface UserLocationAnnotationView
    {
        // @property (readonly, nonatomic, weak) MGLMapView * _Nullable mapView;
        [NullAllowed, Export ("mapView", ArgumentSemantic.Weak)]
        MapView MapView { get; }

        // @property (readonly, nonatomic, weak) MGLUserLocation * _Nullable userLocation;
        [NullAllowed, Export ("userLocation", ArgumentSemantic.Weak)]
        UserLocation UserLocation { get; }

        // @property (readonly, nonatomic, weak) CALayer * _Nullable hitTestLayer;
        [NullAllowed, Export ("hitTestLayer", ArgumentSemantic.Weak)]
        CALayer HitTestLayer { get; }

        // -(void)update;
        [Export ("update")]
        void Update ();
    }

    // @interface AttributionInfo : NSObject
    [BaseType (typeof(NSObject), Name = "MGLAttributionInfo")]
    interface AttributionInfo
    {
        // -(instancetype _Nonnull)initWithTitle:(NSAttributedString * _Nonnull)title URL:(NSURL * _Nullable)URL;
        [Export ("initWithTitle:URL:")]
        IntPtr Constructor (NSAttributedString title, [NullAllowed] NSUrl Url);

        // @property (nonatomic) NSAttributedString * _Nonnull title;
        [Export ("title", ArgumentSemantic.Assign)]
        NSAttributedString Title { get; set; }

        // @property (nonatomic) NSURL * _Nullable URL;
        [NullAllowed, Export ("URL", ArgumentSemantic.Assign)]
        NSUrl Url { get; set; }

        // @property (getter = isFeedbackLink, nonatomic) BOOL feedbackLink;
        [Export ("feedbackLink")]
        bool IsFeedbackLink { [Bind ("isFeedbackLink")] get; set; }

        // -(NSURL * _Nullable)feedbackURLAtCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel;
        [Export ("feedbackURLAtCenterCoordinate:zoomLevel:")]
        [return: NullAllowed]
        NSUrl GetFeedbackUrl (CLLocationCoordinate2D centerCoordinate, double zoomLevel);
    }


}