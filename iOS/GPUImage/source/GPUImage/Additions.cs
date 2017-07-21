using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if __UNIFIED__
using AVFoundation;
using CoreAnimation;
using CoreFoundation;
using CoreGraphics;
using CoreMedia;
using CoreVideo;
using Foundation;
using ObjCRuntime;
using OpenGLES;
using UIKit;

#else
using MonoTouch.AVFoundation;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreFoundation;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreMedia;
using MonoTouch.CoreVideo;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.OpenGLES;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nint = System.Int32;
using nuint = System.UInt32;
using nfloat = System.Single;
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
	partial class GLProgram
	{
		public GLProgram (string vertexShader, bool vertexShaderIsFilename, string fragmentShaderFilename)
		{
			if (vertexShaderIsFilename) {
				Handle = InitWithVertexShaderFilenameAndFragmentShaderFilename (vertexShader, fragmentShaderFilename);
			} else {
				Handle = InitWithVertexShaderStringAndFragmentShaderFilename (vertexShader, fragmentShaderFilename);
			}
		}

		public static GLProgram FromShaderStrings (string vertexShader, string fragmentShader)
		{
			return new GLProgram (vertexShader, fragmentShader);
		}

		public static GLProgram FromShaderFiles (string vertexShaderFilename, string fragmentShaderFilename)
		{
			return new GLProgram (vertexShaderFilename, true, fragmentShaderFilename);
		}
	}

	partial class GPUImageFramebuffer
	{
		public byte[] ToByteBufferArray ()
		{
			var array = new byte [(int)Size.Width * (int)Size.Height * Marshal.SizeOf<float> ()];
			Marshal.Copy (ByteBuffer, array, 0, array.Length);
			return array;
		}
	}
}

namespace GPUImage.Filters
{
	partial class GPUImageFilter
	{
		public GPUImageFilter (string fragmentShader, bool fragmentShaderIsFilename)
		{
			if (fragmentShaderIsFilename) {
				Handle = InitWithFragmentShaderFromFile (fragmentShader);
			} else {
				Handle = InitWithFragmentShaderFromString (fragmentShader);
			}
		}

		public static float[] TextureCoordinates (GPUImageRotationMode rotationMode)
		{
			var result = TextureCoordinatesInternal (rotationMode);

			var array = new float [8];
			Marshal.Copy (result, array, 0, array.Length);
			return array;
		}

		public unsafe void RenderToTexture (float[] vertices, float[] textureCoordinates)
		{
			fixed (float* v = &vertices [0]) {
				fixed (float* t = &textureCoordinates [0]) {
					RenderToTexture ((IntPtr)v, (IntPtr)t);
				}
			}
		}

		public unsafe void SetFloatArray (float[] array, int count, string uniformName)
		{
			fixed (float* a = &array [0]) {
				SetFloatArray ((IntPtr)a, count, uniformName);
			}
		}

		public unsafe void SetFloatArray (float[] array, int count, int uniform, GLProgram shaderProgram)
		{
			fixed (float* a = &array [0]) {
				SetFloatArray ((IntPtr)a, count, uniform, shaderProgram);
			}
		}

		public static GPUImageFilter FromFragmentShaderString (string fragmentShader)
		{
			return new GPUImageFilter (fragmentShader, false);
		}

		public static GPUImageFilter FromFragmentShaderFile (string fragmentShaderFilename)
		{
			return new GPUImageFilter (fragmentShaderFilename, true);
		}

		public static GPUImageFilter FromShaderStrings (string fragmentShader, string vertexShader)
		{
			return new GPUImageFilter (fragmentShader, vertexShader);
		}
	}
}

namespace GPUImage.Outputs
{
	partial class GPUImageRawDataOutput
	{
		public byte[] ToRawBytesArray ()
		{
			var bytes = RawBytesForImage;
			var imageSize = MaximumOutputSize;
			var bpr = (int)BytesPerRowInOutput;
			const int sizeofGLubyte = 1;
			var array = new byte [(int)imageSize.Height * bpr * sizeofGLubyte];
			Marshal.Copy (bytes, array, 0, array.Length);
			return array;
		}
	}
}

namespace GPUImage.Filters.ImageProcessing
{
	partial class GPUImageLineGenerator
	{
		public unsafe void RenderLines (float[] lineSlopeAndIntercepts, nuint numberOfLines, CMTime frameTime)
		{
			fixed (float* l = &lineSlopeAndIntercepts [0]) {
				RenderLines ((IntPtr)l, numberOfLines, frameTime);
			}
		}
	}

	partial class GPUImageCrosshairGenerator
	{
		unsafe public void RenderCrosshairs (float[] crosshairCoordinates, nuint numberOfCrosshairs, CMTime frameTime)
		{
			fixed (float* c = &crosshairCoordinates [0]) {
				RenderCrosshairs ((IntPtr)c, numberOfCrosshairs, frameTime);
			}
		}
	}
}

namespace GPUImage.Sources
{
	partial class GPUImageVideoCamera
	{
		public static float[] ColorConversion601 { 
			get {
				var array = new float [9];
				Marshal.Copy (ColorConversion601Internal, array, 0, array.Length);
				return array;
			} 
		}

		public static float[] ColorConversion601FullRange { 
			get {
				var array = new float [9];
				Marshal.Copy (ColorConversion601FullRangeInternal, array, 0, array.Length);
				return array;
			} 
		}

		public static float[] ColorConversion709 { 
			get {
				var array = new float [9];
				Marshal.Copy (ColorConversion709Internal, array, 0, array.Length);
				return array;
			} 
		}
	}

	partial class GPUImageRawDataInput : IDisposable
	{
		private GCHandle bytesHandle;

		unsafe public void UpdateData (byte[] bytesToUpload, CGSize imageSize)
		{
			fixed (byte* l = &bytesToUpload [0]) {
				UpdateData ((IntPtr)l, imageSize);
			}
		}

		public static GPUImageRawDataInput FromBytes (byte[] bytesToUpload, CGSize imageSize)
		{
			var handle = GCHandle.Alloc (bytesToUpload, GCHandleType.Pinned);
			var ptr = handle.AddrOfPinnedObject ();

			var instance = new GPUImageRawDataInput (ptr, imageSize);
			instance.bytesHandle = handle;

			return instance;
		}

		public static GPUImageRawDataInput FromBytes (byte[] bytesToUpload, CGSize imageSize, GPUPixelFormat pixelFormat)
		{
			var handle = GCHandle.Alloc (bytesToUpload, GCHandleType.Pinned);
			var ptr = handle.AddrOfPinnedObject ();

			var instance = new GPUImageRawDataInput (ptr, imageSize, pixelFormat);
			instance.bytesHandle = handle;

			return instance;
		}

		public static GPUImageRawDataInput FromBytes (byte[] bytesToUpload, CGSize imageSize, GPUPixelFormat pixelFormat, GPUPixelType pixelType)
		{
			var handle = GCHandle.Alloc (bytesToUpload, GCHandleType.Pinned);
			var ptr = handle.AddrOfPinnedObject ();

			var instance = new GPUImageRawDataInput (ptr, imageSize, pixelFormat, pixelType);
			instance.bytesHandle = handle;

			return instance;
		}

		protected override void Dispose (bool disposing)
		{
			if (bytesHandle.IsAllocated) {
				bytesHandle.Free ();
			}
		}
	}
}
