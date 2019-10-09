using ObjCRuntime;
using nint = System.Int64;

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
