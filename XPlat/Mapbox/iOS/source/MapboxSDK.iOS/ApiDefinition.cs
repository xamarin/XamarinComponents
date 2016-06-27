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
    //[Verify (ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern NSString *const _Nonnull MGLErrorDomain;
        [Field ("MGLErrorDomain", "__Internal")]
        NSString ErrorDomain { get; }

        // extern const MGLCoordinateSpan MGLCoordinateSpanZero;
        //[Field ("MGLCoordinateSpanZero", "__Internal")]
        //CoordinateSpan CoordinateSpanZero { get; }

        [Field ("MGLOfflinePackProgressChangedNotification", "__Internal")]
        NSString OfflinePackProgressChangedNotification { get; }

        [Field ("MGLOfflinePackErrorNotification", "__Internal")]
        NSString OfflinePackErrorNotification { get; }

        [Field ("MGLOfflinePackMaximumMapboxTilesReachedNotification", "__Internal")]
        NSString OfflinePackMaximumMapboxTilesReachedNotification { get; }

        [Field ("MGLOfflinePackStateUserInfoKey", "__Internal")]
        NSString OfflinePackStateUserInfoKey { get; }

        [Field ("MGLOfflinePackProgressUserInfoKey", "__Internal")]
        NSString OfflinePackProgressUserInfoKey { get; }

        [Field ("MGLOfflinePackErrorUserInfoKey", "__Internal")]
        NSString OfflinePackErrorUserInfoKey { get; }

        [Field ("MGLOfflinePackMaximumCountUserInfoKey", "__Internal")]
        NSString OfflinePackMaximumCountUserInfoKey { get; }
    }

    delegate void OfflinePackAdditionCompletion (OfflinePack pack, NSError error);
    
    delegate void OfflinePackRemovalCompletion (NSError error);
    

    // @interface MGLAccountManager : NSObject
    [BaseType (typeof(NSObject), Name="MGLAccountManager")]
    interface AccountManager
    {
        // +(NSString * _Nullable)accessToken;
        // +(void)setAccessToken:(NSString * _Nullable)accessToken;
        [Static]
        [NullAllowed, Export ("accessToken")]
        string AccessToken { [NullAllowed, Export ("accessToken")]get; [NullAllowed, Export ("setAccessToken:")]set; }

        // +(BOOL)mapboxMetricsEnabledSettingShownInApp;
        // +(void)setMapboxMetricsEnabledSettingShownInApp:(BOOL)showsOptOut __attribute__((unavailable("Set MGLMapboxMetricsEnabledSettingShownInApp in Info.plist.")));
        [Static]        
        [Export ("mapboxMetricsEnabledSettingShownInApp")]
        bool MetricsEnabledSettingShownInApp { get; set; }
    }

    interface IAnnotation { }

    // @protocol MGLAnnotation <NSObject>
    [Protocol]
    [BaseType (typeof(NSObject), Name="MGLAnnotation")]
    interface Annotation
    {
        // @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
        [Export ("coordinate")]
        CLLocationCoordinate2D Coordinate { get; }

        // @optional @property (readonly, copy, nonatomic) NSString * _Nullable title;
        [NullAllowed, Export ("title")]
        string Title { get; }

        // @optional @property (readonly, copy, nonatomic) NSString * _Nullable subtitle;
        [NullAllowed, Export ("subtitle")]
        string Subtitle { get; }
    }

    // @interface MGLAnnotationImage : NSObject
    [BaseType (typeof(NSObject), Name="MGLAnnotationImage")]
    interface AnnotationImage
    {
        // @property (readonly, nonatomic) UIImage * _Nonnull image;
        [Export ("image")]
        UIImage Image { get; }

        // @property (readonly, nonatomic) NSString * _Nonnull reuseIdentifier;
        [Export ("reuseIdentifier")]
        string ReuseIdentifier { get; }

        [Export ("enabled")]
        bool Enabled { [Bind ("isEnabled")] get; set; }

        // +(instancetype _Nonnull)annotationImageWithImage:(UIImage * _Nonnull)image reuseIdentifier:(NSString * _Nonnull)reuseIdentifier;
        [Static]
        [Export ("annotationImageWithImage:reuseIdentifier:")]
        AnnotationImage Create (UIImage image, string reuseIdentifier);
    }


    // @interface MGLMapView : UIView
    [BaseType (typeof(UIView), Name="MGLMapView")]
    interface MapView
    {
        // -(instancetype _Nonnull)initWithFrame:(CGRect)frame;
        [Export ("initWithFrame:")]
        IntPtr Constructor (CGRect frame);

        // -(instancetype _Nonnull)initWithFrame:(CGRect)frame styleURL:(NSURL * _Nullable)styleURL;
        [Export ("initWithFrame:styleURL:")]
        IntPtr Constructor (CGRect frame, [NullAllowed] NSUrl styleURL);

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

        // @property (readonly, nonatomic) UIImageView * _Nonnull compassView;
        [Export ("compassView")]
        UIImageView CompassView { get; }

        // @property (readonly, nonatomic) UIImageView * _Nonnull logoView;
        [Export ("logoView")]
        UIImageView LogoView { get; }

        // @property (readonly, nonatomic) UIButton * _Nonnull attributionButton;
        [Export ("attributionButton")]
        UIButton AttributionButton { get; }

        //[Wrap ("WeakDelegate")]
        [Export ("delegate", ArgumentSemantic.Weak)]
        [NullAllowed]
        IMapViewDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<MGLMapViewDelegate> _Nullable delegate __attribute__((iboutlet));
        //[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
        //NSObject WeakDelegate { get; set; }

        // @property (nonatomic) CLLocationCoordinate2D centerCoordinate;
        [Export ("centerCoordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D CenterCoordinate { get; set; }

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)coordinate animated:(BOOL)animated;
        [Export ("setCenterCoordinate:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D coordinate, bool animated);

        // @property (nonatomic) double zoomLevel;
        [Export ("zoomLevel")]
        double ZoomLevel { get; set; }

        // @property (nonatomic) double minimumZoomLevel;
        [Export ("minimumZoomLevel")]
        double MinimumZoomLevel { get; set; }

        // @property (nonatomic) double maximumZoomLevel;
        [Export ("maximumZoomLevel")]
        double MaximumZoomLevel { get; set; }

        // -(void)setZoomLevel:(double)zoomLevel animated:(BOOL)animated;
        [Export ("setZoomLevel:animated:")]
        void SetZoomLevel (double zoomLevel, bool animated);

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel animated:(BOOL)animated;
        [Export ("setCenterCoordinate:zoomLevel:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, bool animated);

        // -(void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated;
        [Export ("setCenterCoordinate:zoomLevel:direction:animated:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated);

        // - (void)setCenterCoordinate:(CLLocationCoordinate2D)centerCoordinate zoomLevel:(double)zoomLevel direction:(CLLocationDirection)direction animated:(BOOL)animated completionHandler:(nullable void (^)(void))completion;
        [Export ("setCenterCoordinate:zoomLevel:direction:animated:completionHandler:")]
        void SetCenterCoordinate (CLLocationCoordinate2D centerCoordinate, double zoomLevel, double direction, bool animated, [NullAllowed]Action completionHandler);

        // @property (nonatomic) MGLCoordinateBounds visibleCoordinateBounds;
        [Export ("visibleCoordinateBounds", ArgumentSemantic.Assign)]
        CoordinateBounds VisibleCoordinateBounds { get; set; }

        // -(void)setVisibleCoordinateBounds:(MGLCoordinateBounds)bounds animated:(BOOL)animated;
        [Export ("setVisibleCoordinateBounds:animated:")]
        void SetVisibleCoordinateBounds (CoordinateBounds bounds, bool animated);

        // -(void)setVisibleCoordinateBounds:(MGLCoordinateBounds)bounds edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("setVisibleCoordinateBounds:edgePadding:animated:")]
        void SetVisibleCoordinateBounds (CoordinateBounds bounds, UIEdgeInsets insets, bool animated);

        // -(void)setVisibleCoordinates:(CLLocationCoordinate2D * _Nonnull)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("setVisibleCoordinates:count:edgePadding:animated:")]
        void SetVisibleCoordinates (IntPtr coordinates, nuint count, UIEdgeInsets insets, bool animated);

        // - (void)setVisibleCoordinates:(CLLocationCoordinate2D *)coordinates count:(NSUInteger)count edgePadding:(UIEdgeInsets)insets direction:(CLLocationDirection)direction duration:(NSTimeInterval)duration animationTimingFunction:(nullable CAMediaTimingFunction *)function completionHandler:(nullable void (^)(void))completion;
        [Export ("setVisibleCoordinates:count:edgePadding:direction:duration:animationTimingFunction:completionHandler:")]
        void SetVisibleCoordinates (IntPtr coordinates, nuint count, UIEdgeInsets insets,double direction, double duration, [NullAllowed] CAMediaTimingFunction animationTimingFunction, [NullAllowed] Action completion);

        // -(void)showAnnotations:(NSArray<id<MGLAnnotation>> * _Nonnull)annotations animated:(BOOL)animated;
        [Export ("showAnnotations:animated:")]
        void ShowAnnotations (IAnnotation[] annotations, bool animated);

        // - (void)showAnnotations:(NS_ARRAY_OF(id <MGLAnnotation>) *)annotations edgePadding:(UIEdgeInsets)insets animated:(BOOL)animated;
        [Export ("showAnnotations:edgePadding:animated:")]
        void ShowAnnotations (IAnnotation[] annotations, UIEdgeInsets insets, bool animated);

        // @property (nonatomic) CLLocationDirection direction;
        [Export ("direction")]
        double Direction { get; set; }

        // -(void)setDirection:(CLLocationDirection)direction animated:(BOOL)animated;
        [Export ("setDirection:animated:")]
        void SetDirection (double direction, bool animated);

        // -(void)resetNorth __attribute__((ibaction));
        [Export ("resetNorth")]
        void ResetNorth ();

        // @property (copy, nonatomic) MGLMapCamera * _Nonnull camera;
        [Export ("camera", ArgumentSemantic.Copy)]
        MapCamera Camera { get; set; }

        // -(void)setCamera:(MGLMapCamera * _Nonnull)camera animated:(BOOL)animated;
        [Export ("setCamera:animated:")]
        void SetCamera (MapCamera camera, bool animated);

        // -(void)setCamera:(MGLMapCamera * _Nonnull)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(CAMediaTimingFunction * _Nullable)function;
        [Export ("setCamera:withDuration:animationTimingFunction:")]
        void SetCamera (MapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function);

        // - (void)setCamera:(MGLMapCamera *)camera withDuration:(NSTimeInterval)duration animationTimingFunction:(nullable CAMediaTimingFunction *)function completionHandler:(nullable void (^)(void))completion;
        [Export ("setCamera:withDuration:animationTimingFunction:completionHandler:")]
        void SetCamera (MapCamera camera, double duration, [NullAllowed] CAMediaTimingFunction function, [NullAllowed] Action completion);

        // - (void)flyToCamera:(MGLMapCamera *)camera completionHandler:(nullable void (^)(void))completion;
        [Export ("flyToCamera:completionHandler:")]
        void FlyToCamera (MapCamera camera, [NullAllowed]Action completion);

        // - (void)flyToCamera:(MGLMapCamera *)camera withDuration:(NSTimeInterval)duration peakAltitude:(CLLocationDistance)peakAltitude completionHandler:(nullable void (^)(void))completion;
        [Export ("flyToCamera:withDuration:peakAltitude:completionHandler:")]
        void FlyToCamera (MapCamera camera, double duration, double peakAltitude, [NullAllowed] Action completion);

        // @property (nonatomic, assign) UIEdgeInsets contentInset;
        [Export ("contentInset", ArgumentSemantic.Assign)]
        UIEdgeInsets ContentInset { get; set; }

        // - (void)setContentInset:(UIEdgeInsets)contentInset animated:(BOOL)animated;
        [Export ("setContentInset:animated:")]
        void SetContentInset (UIEdgeInsets contentInset, bool animated);

        // -(CLLocationCoordinate2D)convertPoint:(CGPoint)point toCoordinateFromView:(UIView * _Nullable)view;
        [Export ("convertPoint:toCoordinateFromView:")]
        CLLocationCoordinate2D ConvertPoint (CGPoint point, [NullAllowed] UIView view);

        // -(CGPoint)convertCoordinate:(CLLocationCoordinate2D)coordinate toPointToView:(UIView * _Nullable)view;
        [Export ("convertCoordinate:toPointToView:")]
        CGPoint ConvertCoordinate (CLLocationCoordinate2D coordinate, [NullAllowed] UIView view);

        // - (MGLCoordinateBounds)convertRect:(CGRect)rect toCoordinateBoundsFromView:(nullable UIView *)view;
        [Export ("convertRect:toCoordinateBoundsFromView:")]
        CoordinateBounds  ConvertRectToCoordinateBounds (CGRect rect, [NullAllowed] UIView view);

        // - (CGRect)convertCoordinateBounds:(MGLCoordinateBounds)bounds toRectToView:(nullable UIView *)view;
        [Export ("convertCoordinateBounds:toRectToView:")]
        CGRect ConvertCoordinateBoundsToRect (CoordinateBounds bonds, [NullAllowed] UIView view);

        // -(CLLocationDistance)metersPerPixelAtLatitude:(CLLocationDegrees)latitude;
        [Obsolete ("Use MetersPerPointAtLatitude instead")]
        [Export ("metersPerPixelAtLatitude:")]
        double MetersPerPixelAtLatitude (double latitude);

        // - (CLLocationDistance)metersPerPointAtLatitude:(CLLocationDegrees)latitude;
        [Export ("metersPerPointAtLatitude:")]
        double MetersPerPointAtLatitude (double latitude);


        // @property (nonatomic) NSString * _Nullable styleID;
        //[NullAllowed, Export ("styleID")]
        //string StyleID { get; set; }

        // @property (readonly, nonatomic) NSArray<NSURL *> * _Nonnull bundledStyleURLs;
        //[Export ("bundledStyleURLs")]
        //NSUrl[] BundledStyleURLs { get; }

        // @property (nonatomic) NSURL * _Null_unspecified styleURL;
        [Export ("styleURL", ArgumentSemantic.Assign)]
        [NullAllowed]
        NSUrl StyleURL { get; set; }

        // @property (nonatomic) NSArray<NSString *> * _Nonnull styleClasses;
        [Export ("styleClasses", ArgumentSemantic.Assign)]
        string[] StyleClasses { get; set; }

        // -(BOOL)hasStyleClass:(NSString * _Nonnull)styleClass;
        [Export ("hasStyleClass:")]
        bool HasStyleClass (string styleClass);

        // -(void)addStyleClass:(NSString * _Nonnull)styleClass;
        [Export ("addStyleClass:")]
        void AddStyleClass (string styleClass);

        // -(void)removeStyleClass:(NSString * _Nonnull)styleClass;
        [Export ("removeStyleClass:")]
        void RemoveStyleClass (string styleClass);

        // @property (readonly, nonatomic) NSArray<id<MGLAnnotation>> * _Nullable annotations;
        [NullAllowed, Export ("annotations")]
        IAnnotation[] Annotations { get; }

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

        // -(MGLAnnotationImage * _Nullable)dequeueReusableAnnotationImageWithIdentifier:(NSString * _Nonnull)identifier;
        [Export ("dequeueReusableAnnotationImageWithIdentifier:")]
        [return: NullAllowed]
        AnnotationImage DequeueReusableAnnotationImage (string identifier);

        // @property (copy, nonatomic) NSArray<id<MGLAnnotation>> * _Nonnull selectedAnnotations;
        [Export ("selectedAnnotations", ArgumentSemantic.Copy)]
        IAnnotation[] SelectedAnnotations { get; set; }

        // -(void)selectAnnotation:(id<MGLAnnotation> _Nonnull)annotation animated:(BOOL)animated;
        [Export ("selectAnnotation:animated:")]
        void SelectAnnotation (IAnnotation annotation, bool animated);

        // -(void)deselectAnnotation:(id<MGLAnnotation> _Nonnull)annotation animated:(BOOL)animated;
        [Export ("deselectAnnotation:animated:")]
        void DeselectAnnotation (IAnnotation annotation, bool animated);

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

        //- (void)setUserTrackingMode:(MGLUserTrackingMode)mode animated:(BOOL)animated;
        [Export ("setUserTrackingMode:animated:")]
        void SetUserTrackingMode (UserTrackingMode mode, bool animated);

        // @property (nonatomic, assign) MGLAnnotationVerticalAlignment userLocationVerticalAlignment;
        [Export ("userLocationVerticalAlignment", ArgumentSemantic.Assign)]
        AnnotationVerticalAlignment UserLocationVerticalAlignment { get; set; }

        // - (void)setUserLocationVerticalAlignment:(MGLAnnotationVerticalAlignment)alignment animated:(BOOL)animated;
        [Export ("setUserLocationVerticalAlignment:animated:")]
        void SetUserLocationVerticalAlignment (AnnotationVerticalAlignment alignment, bool animated);

        // @property (nonatomic, assign) CLLocationCoordinate2D targetCoordinate;
        [Export ("targetCoordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D TargetCoordinate { get; set; }

        // - (void)setTargetCoordinate:(CLLocationCoordinate2D)targetCoordinate animated:(BOOL)animated;
        [Export ("setTargetCoordinate:animated:")]
        void SetTargetCoordinate (CLLocationCoordinate2D targetCoordinate, bool animated);

        // @property (assign, nonatomic) BOOL displayHeadingCalibration;
        [Export ("displayHeadingCalibration")]
        bool DisplayHeadingCalibration { get; set; }

        // @property (getter = isDebugActive, nonatomic) BOOL debugActive;
        [Obsolete ("Use DebugMask and SetDebugMask instead")]
        [Export ("debugActive")]
        bool DebugActive { [Bind ("isDebugActive")] get; set; }

        // -(void)toggleDebug;
        [Obsolete ("Use DebugMask instead")]
        [Export ("toggleDebug")]
        void ToggleDebug ();

        // @property (nonatomic) MGLMapDebugMaskOptions debugMask;
        [Export ("debugMask")]
        DebugMaskOptions DebugMask { get; set; }

        // -(void)emptyMemoryCache;
        [Export ("emptyMemoryCache")]
        void EmptyMemoryCache ();

        // -(void)resetPosition;
        [Export ("resetPosition")]
        void ResetPosition ();
    }

    interface IMapViewDelegate { }

    // @protocol MGLMapViewDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject), Name="MGLMapViewDelegate")]
    interface MapViewDelegate
    {
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

        // @optional -(BOOL)mapView:(MGLMapView * _Nonnull)mapView annotationCanShowCallout:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:annotationCanShowCallout:")]
        bool CanShowCallout (MapView mapView, IAnnotation annotation);

        // - (nullable UIView <MGLCalloutView> *)mapView:(MGLMapView *)mapView calloutViewForAnnotation:(id <MGLAnnotation>)annotation;
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

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView regionWillChangeAnimated:(BOOL)animated;
        [Export ("mapView:regionWillChangeAnimated:")]
        void RegionWillChange (MapView mapView, bool animated);

        // @optional -(void)mapViewRegionIsChanging:(MGLMapView * _Nonnull)mapView;
        [Export ("mapViewRegionIsChanging:")]
        void RegionIsChanging (MapView mapView);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView regionDidChangeAnimated:(BOOL)animated;
        [Export ("mapView:regionDidChangeAnimated:")]
        void RegionDidChange (MapView mapView, bool animated);

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

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView annotation:(id<MGLAnnotation> _Nonnull)annotation calloutAccessoryControlTapped:(UIControl * _Nonnull)control;
        [Export ("mapView:annotation:calloutAccessoryControlTapped:")]
        void CalloutAccessoryControlTapped (MapView mapView, IAnnotation annotation, UIControl control);

        // - (void)mapView:(MGLMapView *)mapView tapOnCalloutForAnnotation:(id <MGLAnnotation>)annotation;
        [Export ("mapView:tapOnCalloutForAnnotation:")]
        void TapOnCallout (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didSelectAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:didSelectAnnotation:")]
        void DidSelectAnnotation (MapView mapView, IAnnotation annotation);

        // @optional -(void)mapView:(MGLMapView * _Nonnull)mapView didDeselectAnnotation:(id<MGLAnnotation> _Nonnull)annotation;
        [Export ("mapView:didDeselectAnnotation:")]
        void DidDeselectAnnotation (MapView mapView, IAnnotation annotation);
    }

    // @interface MGLShape : NSObject <MGLAnnotation>
    [BaseType (typeof(NSObject), Name="MGLShape")]
    interface Shape : Annotation
    {
        // @property (copy, nonatomic) NSString * _Nullable title;
        [NullAllowed, Export ("title")]
        string Title { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable subtitle;
        [NullAllowed, Export ("subtitle")]
        string Subtitle { get; set; }

        // ONLY FOR NON-iOS (OSX) if we ever bind for that
        // @property (nonatomic, copy, nullable) NSString *toolTip;
        //[NullAllowed, Export ("toolTip")]
        //string ToolTip { get; set; }
    }

    // @interface MGLMultiPoint : MGLShape
    [BaseType (typeof(Shape), Name="MGLMultiPoint")]
    interface MultiPoint
    {
        // @property (readonly, nonatomic) NSUInteger pointCount;
        [Export ("pointCount")]
        nuint PointCount { get; }

        // -(void)getCoordinates:(CLLocationCoordinate2D * _Nonnull)coords range:(NSRange)range;
        [Export ("getCoordinates:range:")]
        void GetCoordinates (IntPtr coordsStructArrayPointer, NSRange range);
    }

    interface IOverlay { }

    // @protocol MGLOverlay <MGLAnnotation>
    [Protocol]
    [BaseType (typeof (Annotation), Name="MGLOverlay")]
    interface Overlay
    {
        // @required @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
        [Abstract]
        [Export ("coordinate")]
        CLLocationCoordinate2D Coordinate { get; }

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
    [BaseType (typeof(Shape), Name="MGLPointAnnotation")]
    interface PointAnnotation
    {
        // @property (assign, nonatomic) CLLocationCoordinate2D coordinate;
        [Export ("coordinate", ArgumentSemantic.Assign)]
        CLLocationCoordinate2D Coordinate { get; set; }
    }

    // @interface MGLPolygon : MGLMultiPoint <MGLOverlay>
    [BaseType (typeof(MultiPoint), Name="MGLPolygon")]
    interface Polygon : Overlay
    {
        // Need to manually do this since it's an array of coordinates which are a value type
        // and cannot be converted to NSArray, see Additions.cs
        // +(instancetype _Nonnull)polygonWithCoordinates:(CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Static]
        [Export ("polygonWithCoordinates:count:")]
        Polygon WithCoordinates (IntPtr coords, nuint count);
    }

    // @interface MGLPolyline : MGLMultiPoint <MGLOverlay>
    [BaseType (typeof(MultiPoint), Name="MGLPolyline")]
    interface Polyline : Overlay
    {
        // +(instancetype _Nonnull)polylineWithCoordinates:(CLLocationCoordinate2D * _Nonnull)coords count:(NSUInteger)count;
        [Static]
        [Export ("polylineWithCoordinates:count:")]
        Polyline WithCoordinates (IntPtr coords, nuint count);
    }

    // @interface MGLUserLocation : NSObject <MGLAnnotation>
    [BaseType (typeof(NSObject), Name="MGLUserLocation")]
    interface UserLocation : Annotation
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
    [BaseType (typeof(NSObject), Name="MGLMapCamera")]
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
    }

    [DisableDefaultCtor]
    [BaseType (typeof (NSObject), Name = "MGLStyle")]
    interface Style 
    {
        /** Returns the Streets style URL.
*   Mapbox Streets is a complete base map, perfect for incorporating your own data. */
        //+ (NSURL *)streetsStyleURL;
        [Obsolete ("Use StreetsStyle (version) method instead")]
        [Static]
        [Export ("streetsStyleURL")]
        NSUrl Streets { get; }

        /** Returns the Emerald style URL.
*   Emerald is a versatile style with emphasis on road networks and public transportation. */
        //+ (NSURL *)emeraldStyleURL;
        [Obsolete ("Create an NSURL object with the string 'mapbox://styles/mapbox/emerald-v8' instead.")]
        [Static]
        [Export ("emeraldStyleURL")]
        NSUrl Emerald { get; }

        /** Returns the Light style URL.
*   Light is a subtle, light-colored backdrop for data visualizations. */
//        + (NSURL *)lightStyleURL;
        [Obsolete ("Use LightStyle (version) method instead")]
        [Static]
        [Export ("lightStyleURL")]
        NSUrl Light { get; }

        /** Returns the Dark style URL.
*   Dark is a subtle, dark-colored backdrop for data visualizations. */
        //+ (NSURL *)darkStyleURL;
        [Obsolete ("Use DarkStyle (version) method instead")]
        [Static]
        [Export ("darkStyleURL")]
        NSUrl Dark { get; }

        /** Returns the Satellite style URL.
*   Mapbox Satellite is a beautiful global satellite and aerial imagery layer. */
        //+ (NSURL *)satelliteStyleURL;
        [Obsolete ("Use SatelliteStyle (version) method instead")]
        [Static]
        [Export ("satelliteStyleURL")]
        NSUrl Satellite { get; }

        /** Returns the Hybrid style URL.
*   Hybrid combines the global satellite and aerial imagery of Mapbox Satellite with unobtrusive labels. */
        //+ (NSURL *)hybridStyleURL;
        [Obsolete ("Use SatelliteStreetsStyle (version) method instead")]
        [Static]
        [Export ("hybridStyleURL")]
        NSUrl Hybrid { get; }


        // + (NSURL*)streetsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("streetsStyleURLWithVersion:")]
        NSUrl StreetsStyle (nint version);

        // + (NSURL*)outdoorsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("outdoorsStyleURLWithVersion:")]
        NSUrl OutdoorsStyle (nint version);

        // + (NSURL*)lightStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("lightStyleURLWithVersion:")]
        NSUrl LightStyle (nint version);

        // + (NSURL*)darkStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("darkStyleURLWithVersion:")]
        NSUrl DarkStyle (nint version);

        // + (NSURL*)satelliteStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("satelliteStyleURLWithVersion:")]
        NSUrl SatelliteStyle (nint version);

        // + (NSURL*)satelliteStreetsStyleURLWithVersion:(NSInteger)version;
        [Static]
        [Export ("satelliteStreetsStyleURLWithVersion:")]
        NSUrl SatelliteStreetsStyle (nint version);

//        - (instancetype)init NS_UNAVAILABLE;
    }

//    // @interface IBAdditions (MGLMapView)
//    [Category]
//    [BaseType (typeof(MapView), Name="MGLMapView_IBAdditions")]
//    interface MapView_IBAdditions
//    {
//        // @property (nonatomic) NSString * _Nullable styleID;
//        [NullAllowed, Export ("styleID")]
//        string StyleID { get; set; }
//
//        // @property (nonatomic) double latitude;
//        [Export ("latitude")]
//        double Latitude { get; set; }
//
//        // @property (nonatomic) double longitude;
//        [Export ("longitude")]
//        double Longitude { get; set; }
//
//        // @property (nonatomic) double zoomLevel;
//        [Export ("zoomLevel")]
//        double ZoomLevel { get; set; }
//
//        // @property (nonatomic) BOOL allowsZooming;
//        [Export ("allowsZooming")]
//        bool AllowsZooming { get; set; }
//
//        // @property (nonatomic) BOOL allowsScrolling;
//        [Export ("allowsScrolling")]
//        bool AllowsScrolling { get; set; }
//
//        // @property (nonatomic) BOOL allowsRotating;
//        [Export ("allowsRotating")]
//        bool AllowsRotating { get; set; }
//
//        // @property (nonatomic) BOOL showsUserLocation;
//        [Export ("showsUserLocation")]
//        bool ShowsUserLocation { get; set; }
//    }

    interface ICalloutView { }

    [Protocol (Name="MGLCalloutView")]
    interface CalloutView 
    {
        // @property (nonatomic, strong) id <MGLAnnotation> representedObject;
        [Abstract]
        [Export ("representedObject", ArgumentSemantic.Strong)]
        IAnnotation RepresentedObject { get; set; }

        // @property (nonatomic, strong) UIView *leftAccessoryView;
        [Abstract]
        [Export ("leftAccessoryView", ArgumentSemantic.Strong)]
        UIView LeftAccessoryView { get; set; }

        // @property (nonatomic, strong) UIView *rightAccessoryView;
        [Abstract]
        [Export ("rightAccessoryView", ArgumentSemantic.Strong)]
        UIView RightAccessoryView { get; set; }

        // @property (nonatomic, weak) id<MGLCalloutViewDelegate> delegate;
        [Abstract]
        [NullAllowed]
        [Export ("delegate", ArgumentSemantic.Weak)]
        ICalloutViewDelegate Delegate { get; set; }

        // - (void)presentCalloutFromRect:(CGRect)rect inView:(UIView *)view constrainedToView:(UIView *)constrainedView animated:(BOOL)animated;
        [Abstract]
        [Export ("presentCalloutFromRect:inView:constrainedToView:animated:")]
        void PresentCallout (CGRect fromRect, UIView inView, UIView constrainedToView, bool animated);

        // - (void)dismissCalloutAnimated:(BOOL)animated;
        [Abstract]
        [Export ("dismissCalloutAnimated:")]
        void DismissCallout (bool animated);
    }

    interface ICalloutViewDelegate { }

    [Protocol, Model]
    [BaseType (typeof (NSObject), Name="MGLCalloutViewDelegate")]
    interface CalloutViewDelegate
    {
        // - (BOOL)calloutViewShouldHighlight:(UIView<MGLCalloutView> *)calloutView;
        [Export ("calloutViewShouldHighlight:")]
        bool ShouldHighlight (ICalloutView calloutView);

        // - (void)calloutViewTapped:(UIView<MGLCalloutView> *)calloutView;
        [Export ("calloutViewTapped:")]
        void Tapped (ICalloutView calloutView);
    }

    interface IOfflineRegion { }

    [Protocol]
    [BaseType (typeof (NSObject), Name="MGLOfflineRegion")]
    interface OfflineRegion
    {

    }

    [DisableDefaultCtor]
    [BaseType (typeof (NSObject), Name="MGLTilePyramidOfflineRegion")]
    interface TilePyramidOfflineRegion : OfflineRegion, INSSecureCoding, INSCopying
    {
        // @property (nonatomic, readonly) NSURL *styleURL;
        [Export ("styleURL")]
        NSUrl StyleUrl { get; }

        [Export ("bounds")]
        CoordinateBounds Bounds { get; }

        [Export ("minimumZoomLevel")]
        double MinimumZoomLevel { get; }

        [Export ("maximumZoomLevel")]
        double MaximumZoomLevel { get; }

        // - (instancetype)initWithStyleURL:(nullable NSURL *)styleURL bounds:(MGLCoordinateBounds)bounds fromZoomLevel:(double)minimumZoomLevel toZoomLevel:(double)maximumZoomLevel NS_DESIGNATED_INITIALIZER;
        [Export ("initWithStyleURL:bounds:fromZoomLevel:toZoomLevel:")]
        IntPtr Constructor ([NullAllowed]NSUrl styleUrl, CoordinateBounds bounds, double minZoomLevel, double maxZoomLevel);
    }

    [BaseType (typeof (NSObject), Name="MGLOfflinePack")]
    interface OfflinePack 
    {
        [Export ("region")]
        IOfflineRegion Region { get; }

        // @property (nonatomic, readonly) NSData *context;
        [Export ("context")]
        NSData Context { get; }

        // @property (nonatomic, readonly) MGLOfflinePackState state;
        [Export ("state")]
        OfflinePackState State { get; }

        // @property (nonatomic, readonly) MGLOfflinePackProgress progress;
        [Export ("progress")]
        OfflinePackProgress Progress { get; }

        // - (void)resume;
        [Export ("resume")]
        void Resume ();

        // - (void)suspend;
        [Export ("suspend")]
        void Suspend ();

        // - (void)requestProgress;
        [Export ("requestProgress")]
        void RequestProgress ();
    }
 

    [Category, BaseType (typeof (NSValue))]
    partial interface MGLOfflinePackAdditions_NSValue
    {
        [Export ("valueWithMGLOfflinePackProgress:")]
        NSValue FromOfflinePackProgress (OfflinePackProgress progress);

        //[Export ("MGLOfflinePackProgressValue")]
        //OfflinePackProgress OfflinePackProgressValue { get; }
    }
 

    [BaseType (typeof (NSObject), Name="MGLOfflineStorage")]
    interface OfflineStorage
    {
        [Static]
        [Export ("sharedOfflineStorage")]
        OfflineStorage Shared { get; }

        // @property (nonatomic, strong, readonly, nullable) NS_ARRAY_OF(MGLOfflinePack *) *packs;
        [NullAllowed]
        [Export ("packs", ArgumentSemantic.Strong)]
        OfflinePack[] Packs { get; }

        // - (void)addPackForRegion:(id <MGLOfflineRegion>)region withContext:(NSData *)context completionHandler:(nullable MGLOfflinePackAdditionCompletionHandler)completion;
        [Export ("addPackForRegion:withContext:completionHandler:")]
        void AddPack (IOfflineRegion region, NSData context, [NullAllowed]OfflinePackAdditionCompletion completionHandler);

        // - (void)removePack:(MGLOfflinePack *)pack withCompletionHandler:(nullable MGLOfflinePackRemovalCompletionHandler)completion;
        [Export ("removePack:withCompletionHandler:")]
        void RemovePack (OfflinePack pack, [NullAllowed]OfflinePackRemovalCompletion completionHandler);

        // - (void)reloadPacks;
        [Export ("reloadPacks")]
        void ReloadPacks ();

        // - (void)setMaximumAllowedMapboxTiles:(uint64_t)maximumCount;
        [Export ("setMaximumAllowedMapboxTiles:")]
        void SetMaxAllowedMapboxTiles (ulong maximumCount);
    }

}
