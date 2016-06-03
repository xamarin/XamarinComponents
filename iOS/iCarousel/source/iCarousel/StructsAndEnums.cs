#if __UNIFIED__
using ObjCRuntime;
using nint = System.Int64;
#else
using MonoTouch.ObjCRuntime;
using nint = System.Int32;
#endif

namespace Carousels {

	[Native]
	public enum iCarouselType : nint {
		Linear = 0,
		Rotary,
		InvertedRotary,
		Cylinder,
		InvertedCylinder,
		Wheel,
		InvertedWheel,
		CoverFlow,
		CoverFlow2,
		TimeMachine,
		InvertedTimeMachine,
		Custom
	}

	[Native]
	public enum iCarouselOption : nint {
		Wrap = 0,
		ShowBackfaces,
		OffsetMultiplier,
		VisibleItems,
		Count,
		Arc,
		Angle,
		Radius,
		Tilt,
		Spacing,
		FadeMin,
		FadeMax,
		FadeRange,
		FadeMinAlpha
	}
}
