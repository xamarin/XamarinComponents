using System;
using System.Runtime.InteropServices;
using CoreGraphics;
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

	[StructLayout (LayoutKind.Sequential)]
	public struct Transition
	{
		public double Duration;

		public double Delay;
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
        public ulong CountOfResourcesCompleted;
        public ulong CountOfBytesCompleted;
        public ulong CountOfTilesCompleted;
        public ulong CountOfTileBytesCompleted;
        public ulong CountOfResourcesExpected;
        public ulong MaximumResourcesExpected;
    }

	[Native]
	public enum ResourceKind : ulong
	{
		Unknown,
		Style,
		Source,
		Tile,
		Glyphs,
		SpriteImage,
		SpriteJson
	}

	[Native]
	public enum InterpolationMode : ulong
	{
		Exponential = 0,
		Interval,
		Categorical,
		Identity
	}

	[Native]
	public enum FillTranslationAnchor : ulong
	{
		Map,
		Viewport
	}

	[Native]
	public enum LineCap : ulong
	{
		Butt,
		Round,
		Square
	}

	[Native]
	public enum LineJoin : ulong
	{
		Bevel,
		Round,
		Miter
	}

	[Native]
	public enum LineTranslationAnchor : ulong
	{
		Map,
		Viewport
	}

	[Native]
	public enum IconRotationAlignment : ulong
	{
		Map,
		Viewport,
		Auto
	}

	[Native]
	public enum IconTextFit : ulong
	{
		None,
		Width,
		Height,
		Both
	}

	[Native]
	public enum SymbolPlacement : ulong
	{
		Point,
		Line
	}

	[Native]
	public enum TextAnchor : ulong
	{
		Center,
		Left,
		Right,
		Top,
		Bottom,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	[Native]
	public enum TextJustification : ulong
	{
		Left,
		Center,
		Right
	}

	[Native]
	public enum TextPitchAlignment : ulong
	{
		Map,
		Viewport,
		Auto
	}

	[Native]
	public enum TextRotationAlignment : ulong
	{
		Map,
		Viewport,
		Auto
	}

	[Native]
	public enum TextTransform : ulong
	{
		None,
		Uppercase,
		Lowercase
	}

	[Native]
	public enum IconTranslationAnchor : ulong
	{
		Map,
		Viewport
	}

	[Native]
	public enum TextTranslationAnchor : ulong
	{
		Map,
		Viewport
	}

	[Native]
	public enum CircleScaleAlignment : ulong
	{
		Map,
		Viewport
	}

	[Native]
	public enum CircleTranslationAnchor : ulong
	{
		Map,
		Viewport
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct StyleLayerDrawingContext
	{
		public CGSize Size;

		public CLLocationCoordinate2D CenterCoordinate;

		public double ZoomLevel;

		public double Direction;

		public nfloat Pitch;

		public nfloat FieldOfView;
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
