using System;
using System.Collections.Generic;
using CoreGraphics;
using UIKit;

using GPUImage;
using GPUImage.Filters.ImageProcessing;
using GPUImage.Outputs;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class RawDataViewController : UIViewController
	{
		public RawDataViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// load image
			const int bytesPerPixel = 4;
			const int bitsPerComponent = 8;
			UIImage image = UIImage.FromBundle ("xamagon.png");
			int width = (int)image.Size.Width;
			int height = (int)image.Size.Height;
			int bytesPerRow = bytesPerPixel * width;
			byte[] rawDataBytes = new byte[bytesPerRow * height];
			using (var colorSpace = CGColorSpace.CreateDeviceRGB ())
			using (var context = new CGBitmapContext (rawDataBytes, width, height, bitsPerComponent, bytesPerRow, colorSpace, CGImageAlphaInfo.PremultipliedLast)) {
				context.DrawImage (new CGRect (CGPoint.Empty, image.Size), image.CGImage);
			}

			GPUImageRawDataInput rawDataInput = GPUImageRawDataInput.FromBytes (rawDataBytes, image.Size, GPUPixelFormat.Rgba);
			GPUImageGaussianBlurFilter customFilter = new GPUImageGaussianBlurFilter ();
			GPUImageRawDataOutput rawDataOutput = new GPUImageRawDataOutput (image.Size, true);

			// for the processor
			rawDataInput.AddTarget (customFilter);
			customFilter.AddTarget (rawDataOutput);

			// for the view
			customFilter.AddTarget (imageView);

			// process
			rawDataOutput.NewFrameAvailableHandler = () => {
				rawDataOutput.LockFramebufferForReading ();

				byte[] outputBytes = rawDataOutput.ToRawBytesArray ();
				int bytesPerRowOutput = (int)rawDataOutput.BytesPerRowInOutput;

				Console.WriteLine ("Bytes per row: {0}", bytesPerRowOutput);

				List<UIColor> colors = new List<UIColor> ();
				for (int yIndex = 0; yIndex < height; yIndex++) {
					int row = yIndex * bytesPerRowOutput;
					int col = (width / 2) * bytesPerPixel;
					UIColor color = UIColor.FromRGBA (
						                outputBytes [row + col + 0], 
						                outputBytes [row + col + 1], 
						                outputBytes [row + col + 2], 
						                outputBytes [row + col + 3]);
					if (!colors.Contains (color)) {
						colors.Add (color);
					}
				}

				Console.WriteLine ("Colors in image: {0}", colors.Count);

				rawDataOutput.UnlockFramebufferAfterReading ();
			};
			rawDataInput.ProcessData ();
		}
	}
}
