using System;
using System.Runtime.InteropServices;
using CoreLocation;
using Foundation;
using ObjCRuntime;

namespace Mapbox 
{
    [Native]
    public enum UserTrackingMode : ulong
    {
        None = 0,
        Follow,
        FollowWithHeading,
        FollowWithCourse
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct CoordinateSpan
    {
        public double LatitudeDelta;
        public double LongitudeDelta;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct CoordinateBounds
    {
        public CLLocationCoordinate2D SouthWest;
        public CLLocationCoordinate2D NorthEast;
    }

    [Native]
    public enum AnnotationVerticalAlignment : ulong
    {
        Center = 0,
        Top,
        Bottom
    }

    [Native]
    public enum DebugMaskOptions : ulong
    {
        TileBoundariesMask = 1 << 1,
        TileInfoMask = 1 << 2,
        TimestampsMask = 1 << 3,
        CollisionBoxesMask = 1 << 4,
        OverdrawVisualizationMask = 1 << 5,
    }

    [Native]
    public enum ErrorCode : long
    {
        Unknown = -1,
        NotFound = 1,
        BadServerResponse = 2,
        ConnectionFailed = 3
    }


    [Native]
    public enum OfflinePackState : ulong
    {
        Unknown = 0,
        Inactive = 1,
        Active = 2,
        Complete = 3,
        Invalid = 4,
    } 

    [StructLayout (LayoutKind.Sequential)]
    public struct OfflinePackProgress 
    {
        ulong CountOfResourcesCompleted;
        ulong CountOfBytesCompleted;
        ulong CountOfTilesCompleted;
        ulong CountOfTileBytesCompleted;
        ulong CountOfResourcesExpected;
        ulong MaximumResourcesExpected;
    }

    [Native]
    public enum AnnotationViewDragState : ulong
    {
        None = 0,
        Starting,
        Dragging,
        Canceling,
        Ending
    }

    // Symbols don't exist in libMapbox.a !!!
//    static class CFunctions
//    {
//        // MGLCoordinateSpan MGLCoordinateSpanMake (CLLocationDegrees latitudeDelta, CLLocationDegrees longitudeDelta) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern CoordinateSpan MGLCoordinateSpanMake (double latitudeDelta, double longitudeDelta);
//
//        // BOOL MGLCoordinateSpanEqualToCoordinateSpan (MGLCoordinateSpan span1, MGLCoordinateSpan span2) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern bool MGLCoordinateSpanEqualToCoordinateSpan (CoordinateSpan span1, CoordinateSpan span2);
//
//        // MGLCoordinateBounds MGLCoordinateBoundsMake (CLLocationCoordinate2D sw, CLLocationCoordinate2D ne) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern CoordinateBounds MGLCoordinateBoundsMake (CLLocationCoordinate2D sw, CLLocationCoordinate2D ne);
//
//        // BOOL MGLCoordinateBoundsEqualToCoordinateBounds (MGLCoordinateBounds bounds1, MGLCoordinateBounds bounds2) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern bool MGLCoordinateBoundsEqualToCoordinateBounds (CoordinateBounds bounds1, CoordinateBounds bounds2);
//
//        // MGLCoordinateSpan MGLCoordinateBoundsGetCoordinateSpan (MGLCoordinateBounds bounds) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern CoordinateSpan MGLCoordinateBoundsGetCoordinateSpan (CoordinateBounds bounds);
//
//        // MGLCoordinateBounds MGLCoordinateBoundsOffset (MGLCoordinateBounds bounds, MGLCoordinateSpan offset) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern CoordinateBounds MGLCoordinateBoundsOffset (CoordinateBounds bounds, CoordinateSpan offset);
//
//        // BOOL MGLCoordinateBoundsIsEmpty (MGLCoordinateBounds bounds) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern bool MGLCoordinateBoundsIsEmpty (CoordinateBounds bounds);
//
//        // NSString * _Nonnull MGLStringFromCoordinateBounds (MGLCoordinateBounds bounds) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern NSString MGLStringFromCoordinateBounds (CoordinateBounds bounds);
//
//        // CGFloat MGLRadiansFromDegrees (CLLocationDegrees degrees) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern nfloat MGLRadiansFromDegrees (double degrees);
//
//        // CLLocationDegrees MGLDegreesFromRadians (CGFloat radians) __attribute__((always_inline));
//        [DllImport ("__Internal")]
//        //[Verify (PlatformInvoke)]
//        internal static extern double MGLDegreesFromRadians (nfloat radians);
//    }
}
