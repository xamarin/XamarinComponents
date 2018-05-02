using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace Google.VR
{
	[Native]
	public enum GVRWidgetDisplayMode : long
	{
		Embedded = 1,
		Fullscreen,
		FullscreenVR
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GVRHeadRotation
	{
		public nfloat Yaw;
		public nfloat Pitch;
	}

	public enum GVRRenderingMode : uint
	{
		StereoPanning,
		BinauralLowQuality,
		BinauralHighQuality
	}

	public enum GVRMaterialName : uint
	{
		Transparent,
		AcousticCeilingTiles,
		BrickBare,
		BrickPainted,
		ConcreteBlockCoarse,
		ConcreteBlockPainted,
		CurtainHeavy,
		FiberGlassInsulation,
		GlassThin,
		GlassThick,
		Grass,
		LinoleumOnConcrete,
		Marble,
		Metal,
		ParquetOnConcrete,
		PlasterRough,
		PlasterSmooth,
		PlywoodPanel,
		PolishedConcreteOrTile,
		Sheetrock,
		WaterOrIceSurface,
		WoodCeiling,
		WoodPanel
	}

	public enum GVRDistanceRolloffModel : uint
	{
		Logarithmic,
		Linear,
		None
	}

	[Native]
	public enum GVREye : long
	{
		LeftEye,
		RightEye,
		CenterEye
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GVRFieldOfView
	{
		public nfloat Left;

		public nfloat Right;

		public nfloat Top;

		public nfloat Bottom;
	}

	[Native]
	public enum GVRUserEvent : long
	{
		BackButton,
		Tilt,
		Trigger
	}

	public enum GVRPanoramaImageType
	{
		Mono = 1,
		StereoOverUnder
	}

	public enum GVRPanoramaVideoType
	{
		Mono = 1,
		StereoOverUnder
	}
}

