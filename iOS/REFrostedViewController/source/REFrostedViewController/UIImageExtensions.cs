using System;
using UIKit;
using CoreGraphics;
using Accelerate;

namespace REFrostedViewController {
    
    
    #region 

	public static class UIImageExtensions {
        
        #region Methods

		/// <summary>
		/// Applies the blur with radius.
		/// </summary>
		/// <returns>The blur with radius.</returns>
		/// <param name="target">Target.</param>
		/// <param name="blurRadius">Blur radius.</param>
		/// <param name="tintColor">Tint color.</param>
		/// <param name="saturationDeltaFactor">Saturation delta factor.</param>
		/// <param name="maskImage">Mask image.</param>
        public static UIImage ApplyBlurWithRadius(this UIImage target, nfloat blurRadius, UIColor tintColor, nfloat saturationDeltaFactor, UIImage maskImage) {

		     // Check pre-conditions.
		     if (target.Size.Width < 1 || target.Size.Height < 1) 
			 {
				throw new Exception(String.Format(@"*** error: invalid size: (%.2 x %.2f). Both dimensions must be >= 1: %@", target.Size.Width, target.Size.Height, target));
		     }
		     if (target.CGImage == null) {
				throw new Exception(String.Format(@"*** error: image must be backed by a CGImage: %@", target));
		     }

		     if (maskImage != null 
				&& maskImage.CGImage == null) 
			 {
				throw new Exception(String.Format(@"*** error: maskImage must be backed by a CGImage: %@", maskImage));
			 }

			//     
			var imageRect = new CGRect(CGPoint.Empty, target.Size);

		    var effectImage = target;
			//     
			bool hasBlur = blurRadius > float.Epsilon;
			bool hasSaturationChange = Math.Abs(saturationDeltaFactor - 1d) > float.Epsilon;


		    if (hasBlur || hasSaturationChange) 
			{
		         UIGraphics.BeginImageContextWithOptions(target.Size, false, UIScreen.MainScreen.Scale);

		         var effectInContext = UIGraphics.GetCurrentContext();
				 effectInContext.ScaleCTM(1.0f, -1.0f);

				effectInContext.TranslateCTM(0, -target.Size.Height);
				effectInContext.DrawImage(imageRect, target.CGImage);


				var gctx = effectInContext.AsBitmapContext();

				vImageBuffer effectInBuffer = new vImageBuffer();

				effectInBuffer.Data     = gctx.Data;
				effectInBuffer.Width    = (int)gctx.Width;
				effectInBuffer.Height   = (int)gctx.Height;
				effectInBuffer.BytesPerRow = (int)gctx.BytesPerRow;        


			     UIGraphics.BeginImageContextWithOptions(target.Size, false, UIScreen.MainScreen.Scale);

     			 var effectOutContext = UIGraphics.GetCurrentContext();
				 var gctxOut = effectOutContext.AsBitmapContext();

				vImageBuffer effectOutBuffer = new vImageBuffer();
				effectOutBuffer.Data     = gctxOut.Data;
				effectOutBuffer.Width    = (int)gctxOut.Width;
				effectOutBuffer.Height   = (int)gctxOut.Height;
				effectOutBuffer.BytesPerRow = (int)gctxOut.BytesPerRow;
				         
		        if (hasBlur) 
				{
		             // A description of how to compute the box kernel width from the Gaussian
		             // radius (aka standard deviation) appears in the SVG spec:
		             // http://www.w3.org/TR/SVG/filters.html#feGaussianBlurElement
		             //
		             // For larger values of 's' (s >= 2.0), an approximation can be used: Three
		             // successive box-blurs build a piece-wise quadratic convolution kernel, which
		             // approximates the Gaussian kernel to within roughly 3%.
		             //
		             // let d = floor(s * 3*sqrt(2*pi)/4 + 0.5)
		             //
		             // ... if d is odd, use three box-blurs of size 'd', centered on the output pixel.
		             //
		             var inputRadius = blurRadius * UIScreen.MainScreen.Scale;


					var radius = (uint)Math.Floor(inputRadius * 3f * Math.Sqrt(2 * Math.PI) / 4 + 0.5);

		             if (radius % 2 != 1) {
		                 radius += 1; // force radius to be odd so that the three box-blur methodology works.
		             }


					vImage.BoxConvolveARGB8888(ref effectInBuffer, ref effectOutBuffer, IntPtr.Zero, 0, 0, radius, radius, Pixel8888.Zero, vImageFlags.EdgeExtend);
					vImage.BoxConvolveARGB8888(ref effectOutBuffer, ref effectInBuffer, IntPtr.Zero, 0, 0, radius, radius, Pixel8888.Zero, vImageFlags.EdgeExtend);
					vImage.BoxConvolveARGB8888(ref effectInBuffer, ref effectOutBuffer, IntPtr.Zero, 0, 0, radius, radius, Pixel8888.Zero, vImageFlags.EdgeExtend);
		         }

		         bool effectImageBuffersAreSwapped = false;

		         if (hasSaturationChange) 
				 {
		             var s = saturationDeltaFactor;
					var floatingPointSaturationMatrix = new nfloat[]{
		                 0.0722f + 0.9278f * s,  0.0722f - 0.0722f * s,  0.0722f - 0.0722f * s,  0f,
		                 0.7152f - 0.7152f * s,  0.7152f + 0.2848f * s,  0.7152f - 0.7152f * s,  0f,
		                 0.2126f - 0.2126f * s,  0.2126f - 0.2126f * s,  0.2126f + 0.7873f * s,  0f,
		                 0f,                    0f,                    0f,  1f,
		             };

		             var divisor = 256;

					var matrixSize = floatingPointSaturationMatrix.Length/sizeof(float);
		             
					var saturationMatrix = new short[matrixSize];

		             for (int i = 0; i < matrixSize; ++i) {
						saturationMatrix[i] = (short)Math.Round(floatingPointSaturationMatrix[i] * divisor);
		             }

		             if (hasBlur) {
						vImage.MatrixMultiplyARGB8888(ref effectOutBuffer, ref effectInBuffer, saturationMatrix, divisor, null, null, vImageFlags.NoFlags);
		                 effectImageBuffersAreSwapped = true;
		             }
		             else {
						vImage.MatrixMultiplyARGB8888(ref effectInBuffer, ref effectOutBuffer, saturationMatrix, divisor, null, null, vImageFlags.NoFlags);
		             }
		         }

		         if (!effectImageBuffersAreSwapped)
		             effectImage = UIGraphics.GetImageFromCurrentImageContext();
				
		         UIGraphics.EndImageContext();
		         
		         if (effectImageBuffersAreSwapped)
		             effectImage = UIGraphics.GetImageFromCurrentImageContext();
				
		         UIGraphics.EndImageContext();
		     }


			      
		     // Set up output context.
			 UIGraphics.BeginImageContextWithOptions(target.Size, false, UIScreen.MainScreen.Scale);
		     var outputContext = UIGraphics.GetCurrentContext();
			 outputContext.ScaleCTM(1.0f, -1.0f);
			 outputContext.TranslateCTM(0, -target.Size.Height);
		     
		     // Draw base image.
			 outputContext.DrawImage(imageRect, target.CGImage);
		     
		     // Draw effect image.
		     if (hasBlur) {
				outputContext.SaveState();

		         if (maskImage != null) 
				 {
					outputContext.ClipToMask(imageRect, maskImage.CGImage);
		         }

				 outputContext.DrawImage(imageRect, effectImage.CGImage);
				 outputContext.RestoreState();

		     }
			    
			// Add in color tint.
		     if (tintColor != null) 
			{
				outputContext.SaveState();
				outputContext.SetFillColor(tintColor.CGColor);
				outputContext.FillRect(imageRect);
				outputContext.RestoreState();

		     }

		     UIImage outputImage = UIGraphics.GetImageFromCurrentImageContext();
		     UIGraphics.EndImageContext();
			//     
			return outputImage;
        }
        #endregion
    }
    #endregion
}
