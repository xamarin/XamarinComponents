using System;
using UIKit;

using GPUImage.Filters;
using GPUImage.Filters.ColorProcessing;
using GPUImage.Sources;

namespace GPUImageSample
{
	partial class MultipleViewController : UIViewController
	{
		private GPUImageStillCamera stillCamera;

		private	GPUImageFilter filter1;
		private GPUImageFilter filter2;
		private GPUImageSepiaFilter filter3;

		public MultipleViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			stillCamera = new GPUImageStillCamera ();
			stillCamera.OutputImageOrientation = InterfaceOrientation;

			filter1 = GPUImageFilter.FromFragmentShaderFile ("Shader1");
			filter2 = GPUImageFilter.FromFragmentShaderFile ("Shader2");
			filter3 = new GPUImageSepiaFilter ();

			stillCamera.AddTarget (topLeftImageView);
			stillCamera.AddTarget (filter1);
			filter1.AddTarget (topRightImageView);
			stillCamera.AddTarget (filter2);
			filter2.AddTarget (bottomLeftImageView);
			stillCamera.AddTarget (filter3);
			filter3.AddTarget (bottomRightImageView);
			
			stillCamera.StartCameraCapture ();
		}

		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);

			// update the camera orientation
			stillCamera.OutputImageOrientation = InterfaceOrientation;
		}
	}
}
