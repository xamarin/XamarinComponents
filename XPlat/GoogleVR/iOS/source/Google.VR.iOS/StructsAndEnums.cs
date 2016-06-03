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

	public enum renderingMode : uint
	{
		StereoPanning,
		BinauralLowQuality,
		BinauralHighQuality
	}

	public enum materialName : uint
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
		public nfloat left;

		public nfloat right;

		public nfloat top;

		public nfloat bottom;
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
}

