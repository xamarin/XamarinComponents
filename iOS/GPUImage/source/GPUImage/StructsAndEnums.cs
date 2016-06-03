using System;
using System.Runtime.InteropServices;

#if __UNIFIED__
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using Foundation;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMedia;
using MonoTouch.CoreVideo;
using MonoTouch.Foundation;
#endif

using GPUImage;
using GPUImage.Sources;
using GPUImage.Pipeline;
using GPUImage.Filters;
using GPUImage.Filters.ColorProcessing;
using GPUImage.Filters.ImageProcessing;
using GPUImage.Filters.Blends;
using GPUImage.Filters.Effects;
using GPUImage.Outputs;

namespace GPUImage
{
	[StructLayout (LayoutKind.Sequential)]
	public struct GPUTextureOptions
	{
		public uint minFilter;

		public uint magFilter;

		public uint wrapS;

		public uint wrapT;

		public uint internalFormat;

		public uint format;

		public uint type;
	}

	public enum GPUImageRotationMode : uint
	{
		NoRotation,
		RotateLeft,
		RotateRight,
		FlipVertical,
		FlipHorizonal,
		RotateRightFlipVertical,
		RotateRightFlipHorizontal,
		Rotate180
	}

//	static class CFunctions
//	{
//		// extern void runOnMainQueueWithoutDeadlocking (void (^block)());
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void runOnMainQueueWithoutDeadlocking (Action block);
//
//		// extern void runSynchronouslyOnVideoProcessingQueue (void (^block)());
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void runSynchronouslyOnVideoProcessingQueue (Action block);
//
//		// extern void runAsynchronouslyOnVideoProcessingQueue (void (^block)());
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void runAsynchronouslyOnVideoProcessingQueue (Action block);
//
//		// extern void runSynchronouslyOnContextQueue (GPUImageContext *context, void (^block)());
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void runSynchronouslyOnContextQueue (GPUImageContext context, Action block);
//
//		// extern void runAsynchronouslyOnContextQueue (GPUImageContext *context, void (^block)());
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void runAsynchronouslyOnContextQueue (GPUImageContext context, Action block);
//
//		// extern void reportAvailableMemoryForGPUImage (NSString *tag);
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern void reportAvailableMemoryForGPUImage (NSString tag);
//
//		// extern void stillImageDataReleaseCallback (void *releaseRefCon, const void *baseAddress);
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern unsafe void stillImageDataReleaseCallback (void* releaseRefCon, void* baseAddress);
//
//		// extern void GPUImageCreateResizedSampleBuffer (CVPixelBufferRef cameraFrame, CGSize finalSize, CMSampleBufferRef *sampleBuffer);
//		[DllImport ("__Internal")]
//		[Verify (PlatformInvoke)]
//		static extern unsafe void GPUImageCreateResizedSampleBuffer (CVPixelBufferRef* cameraFrame, CGSize finalSize, CMSampleBufferRef** sampleBuffer);
//	}

	public enum GPUImageFillModeType : uint
	{
		Stretch,
		PreserveAspectRatio,
		PreserveAspectRatioAndFill
	}

	public enum GPUPixelFormat : uint
	{
		Bgra = 32993,
		Rgba = 6408,
		Rgb = 6407,
		Luminance = 6409
	}

	public enum GPUPixelType : uint
	{
		UByte = 5121,
		Float = 5126
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GPUByteColorVector
	{
		public byte red;

		public byte green;

		public byte blue;

		public byte alpha;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GPUVector4
	{
		public float one;

		public float two;

		public float three;

		public float four;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GPUVector3
	{
		public float one;

		public float two;

		public float three;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GPUMatrix4x4
	{
		public GPUVector4 one;

		public GPUVector4 two;

		public GPUVector4 three;

		public GPUVector4 four;
	}

	[StructLayout (LayoutKind.Sequential)]
	public struct GPUMatrix3x3
	{
		public GPUVector3 one;

		public GPUVector3 two;

		public GPUVector3 three;
	}

	public enum GPUImageHistogramType : uint
	{
		Red,
		Green,
		Blue,
		Rgb,
		Luminance
	}

	public enum GPUImageFASTDetectorType : uint
	{
		kGPUImageFAST12Contiguous,
		NonMaximumSuppressed
	}
}
