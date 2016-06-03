
<iframe src="https://appetize.io/embed/4nxnxqc6hd3k5289n7q5dt3hm8?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

**GPUImage** lets you apply GPU-accelerated filters and other effects to images, 
live camera video, and movies. In comparison to Core Image (part of iOS 5.0), 
GPUImage allows you to write your own custom filters, supports deployment to iOS 
4.0, and has a simpler interface. However, it currently lacks some of the more 
advanced features of Core Image, such as facial detection.

For massively parallel operations like processing images or live video frames, 
GPUs have some significant performance advantages over CPUs. On an iPhone 4, a 
simple image filter can be over 100 times faster to perform on the GPU than an 
equivalent CPU-based filter.

However, running custom filters on the GPU requires a lot of code to set up and 
maintain an OpenGL ES 2.0 rendering target for these filters. I created a [sample 
project to do this][1], and found that there was a lot of boilerplate code I had 
to write in its creation. Therefore, I put together this framework that 
encapsulates a lot of the common tasks you'll encounter when processing images 
and video and made it so that you don't need to care about the OpenGL ES 2.0 
underpinnings.

This framework compares favorably to Core Image when handling video, taking only 
2.5 ms on an iPhone 4 to upload a frame from the camera, apply a gamma filter, 
and display, versus 106 ms for the same operation using Core Image. CPU-based 
processing takes 460 ms, making GPUImage 40X faster than Core Image for this 
operation on this hardware, and 184X faster than CPU-bound processing. On an 
iPhone 4S, GPUImage is only 4X faster than Core Image for this case, and 102X 
faster than CPU-bound processing. However, for more complex operations like 
Gaussian blurs at larger radii, Core Image currently outpaces GPUImage.

## General Architecture

GPUImage uses OpenGL ES 2.0 shaders to perform image and video manipulation much 
faster than could be done in CPU-bound routines. However, it hides the complexity 
of interacting with the OpenGL ES API in a simplified interface. This 
interface lets you define input sources for images and video, attach filters in 
a chain, and send the resulting processed image or video to the screen, to a 
`UIImage`, or to a movie on disk.

Images or frames of video are uploaded from source objects, which are subclasses 
of `GPUImageOutput`. These include `GPUImageVideoCamera` (for live video from an 
iOS camera), `GPUImageStillCamera` (for taking photos with the camera), 
`GPUImagePicture` (for still images), and `GPUImageMovie` (for movies). Source 
objects upload still image frames to OpenGL ES as textures, then hand those 
textures off to the next objects in the processing chain.

Filters and other subsequent elements in the chain conform to the `GPUImageInput` 
protocol, which lets them take in the supplied or processed texture from the 
previous link in the chain and do something with it. Objects one step further 
down the chain are considered targets, and processing can be branched by adding 
multiple targets to a single output or filter.

For example, an application that takes in live video from the camera, converts 
that video to a sepia tone, then displays the video onscreen would set up a 
chain looking something like the following:

    GPUImageVideoCamera -> GPUImageSepiaFilter -> GPUImageView

## Performing Common Tasks

### Filtering Live Video

To filter live video from an iOS device's camera, you can use code like the 
following:

    videoCamera = new GPUImageVideoCamera (
        AVCaptureSession.Preset640x480,
        AVCaptureDevicePosition.Back);
    videoCamera.OutputImageOrientation = InterfaceOrientation;
    
    filter = GPUImageFilter.FromFragmentShaderFile ("CustomShader");
    videoCamera.AddTarget (filter);
    
    videoView = new GPUImageView ();
    filter.AddTarget (imageView);
    
    videoCamera.StartCameraCapture ();

This sets up a video source coming from the iOS device's back-facing camera, 
using a preset that tries to capture at 640x480. This video is captured with 
the interface being in portrait mode, where the landscape-left-mounted camera 
needs to have its video frames rotated before display. A custom filter, using 
code from the file CustomShader.fsh, is then set as the target for the video 
frames from the camera. These filtered video frames are finally displayed 
onscreen with the help of a `UIView` subclass that can present the filtered 
OpenGL ES texture that results from this pipeline.

The fill mode of the `GPUImageView` can be altered by setting its `FillMode` 
property, so that if the aspect ratio of the source video is different from 
that of the view, the video will either be stretched, centered with black 
bars, or zoomed to fill.

For blending filters and others that take in more than one image, you can 
create multiple outputs and add a single filter as a target for both of 
these outputs. The order with which the outputs are added as targets will 
affect the order in which the input images are blended or otherwise processed.

Also, if you wish to enable microphone audio capture for recording to a movie, 
you'll need to set the `AudioEncodingTarget` of the camera to be your movie writer, 
like for the following:

    videoCamera.AudioEncodingTarget = movieWriter;

### Capturing & Filtering Still Photo

To capture and filter still photos, you can use a process similar to the one for 
filtering video. Instead of a `GPUImageVideoCamera`, you use a 
`GPUImageStillCamera`:

    stillCamera = new GPUImageStillCamera ();
    stillCamera.OutputImageOrientation = InterfaceOrientation;
    
    filter = new GPUImageGammaFilter ();
    stillCamera.AddTarget (filter);
    
    photoView = new GPUImageView ();
    filter.AddTarget (photoView);
    
    stillCamera.StartCameraCapture ();

This will give you a live, filtered feed of the still camera's preview video. 
Note that this preview video is only provided on iOS 4.3 and higher, so you may 
need to set that as your deployment target if you wish to have this functionality.

Once you want to capture a photo, you use a callback or the async method:

    NSData processedJPEG = await stillCamera.CapturePhotoAsJPEGAsync (filter);
    
    var library = new ALAssetsLibrary ();
    var assetURL = await library.WriteImageToSavedPhotosAlbumAsync (
        processedJPEG, stillCamera.CurrentCaptureMetadata);
	
The above code captures a full-size photo processed by the same filter chain used 
in the preview view and saves that photo to disk as a JPEG in the device's asset
library.

Note that the framework currently can't handle images larger than 2048 pixels wide 
or high on older devices (those before the iPhone 4S, iPad 2, or Retina iPad) due 
to texture size limitations. This means that the iPhone 4, whose camera outputs 
still photos larger than this, won't be able to capture photos like this. A 
tiling mechanism is being implemented to work around this. All other devices 
should be able to capture and filter photos using this method.

### Processing Still Images

There are a couple of ways to process a still image and create a result. The 
first way you can do this is by creating a still image source object and manually 
creating a filter chain:

    inputImage = UIImage.FromBundle ("Lambeau.jpg");
    
    imageSource = new GPUImagePicture (inputImage);
    sepiaFilter = new GPUImageSepiaFilter ();
    
    imageSource.AddTarget (sepiaFilter);
    sepiaFilter.UseNextFrameForImageCapture ();
    await imageSource.ProcessImageAsync ();
    
    UIImage currentImage = sepiaFilter.ToImage ();

Note that for a manual capture of an image from a filter, you need to set 
`UseNextFrameForImageCapture` in order to tell the filter that you'll be needing 
to capture from it later. By default, GPUImage reuses framebuffers within 
filters to conserve memory, so if you need to hold on to a filter's framebuffer 
for manual image capture, you need to let it know ahead of time. 

For single filters that you wish to apply to an image, you can simply do the 
following:

    inputImage = UIImage.FromBundle ("Lambeau.jpg");
    
    imageFilter = new GPUImageSketchFilter ();
    UIImage quickFilteredImage = imageFilter.CreateFilteredImage (inputImage);


### Writing Custom Filter

One significant advantage of this framework over Core Image on iOS (as of iOS 5.0) 
is the ability to write your own custom image and video processing filters. These 
filters are supplied as OpenGL ES 2.0 fragment shaders, written in the C-like 
OpenGL Shading Language. 

A custom filter is initialized with code like

    filter = GPUImageFilter.FromFragmentShaderFile ("CustomShader");

where the extension used for the fragment shader is .fsh. Additionally, 
you can use the `FromFragmentShaderString` static method to provide the fragment 
shader as a string, if you would not like to ship your fragment shaders in your 
application bundle.

Fragment shaders perform their calculations for each pixel to be rendered at 
that filter stage. They do this using the OpenGL Shading Language (GLSL), a 
C-like language with additions specific to 2-D and 3-D graphics. An example 
of a fragment shader is the following sepia-tone filter:

    varying highp vec2 textureCoordinate;
    uniform sampler2D inputImageTexture;
    
    void main()
    {
        lowp vec4 textureColor = texture2D(inputImageTexture, textureCoordinate);
        lowp vec4 outputColor;
        outputColor.r = (textureColor.r * 0.393) + (textureColor.g * 0.769) + (textureColor.b * 0.189);
        outputColor.g = (textureColor.r * 0.349) + (textureColor.g * 0.686) + (textureColor.b * 0.168);    
        outputColor.b = (textureColor.r * 0.272) + (textureColor.g * 0.534) + (textureColor.b * 0.131);
        outputColor.a = 1.0;
        
        gl_FragColor = outputColor;
    }

For an image filter to be usable within the GPUImage framework, the first two 
lines that take in the textureCoordinate varying (for the current coordinate 
within the texture, normalized to 1.0) and the inputImageTexture uniform (for 
the actual input image frame texture) are required.

The remainder of the shader grabs the color of the pixel at this location in 
the passed-in texture, manipulates it in such a way as to produce a sepia tone, 
and writes that pixel color out to be used in the next stage of the processing 
pipeline.

### Filtering & Re-Encoding Movies

Movies can be loaded into the framework via the `GPUImageMovie` class, filtered, 
and then written out using a `GPUImageMovieWriter`. `GPUImageMovieWriter` is 
also fast enough to record video in realtime from an iPhone 4's camera at 640x480, 
so a direct filtered video source can be fed into it. 

Currently, `GPUImageMovieWriter` is fast enough to record live 720p video at up 
to 20 FPS on the iPhone 4, and both 720p and 1080p video at 30 FPS on the 
iPhone 4S (as well as on the new iPad).

The following is an example of how you would load a sample movie, pass it 
through a pixellation filter, then record the result to disk as a 480 x 640 
h.264 movie:

    movieFile = new GPUImageMovie (sampleURL);
    filter = new GPUImagePixellateFilter ();
    
    movieFile.AddTarget (filter);
    
    var documents = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
    var pathToMovie = Path.Combine (documents, "Movie.m4v");
    if (File.Exists (pathToMovie)) {
        File.Delete (pathToMovie);
    }
    var movieURL = new NSUrl (pathToMovie, false);
    
    movieWriter = new GPUImageMovieWriter (movieURL, new CGSize (640.0f, 480.0f));
    filter.AddTarget (movieWriter);
    
    movieWriter.ShouldPassthroughAudio = true;
    movieFile.AudioEncodingTarget = movieWriter;
    
    movieFile.EnableSynchronizedEncoding (movieWriter);
    
    movieWriter.StartRecording ();
    movieFile.StartProcessing ();

Once recording is finished, you need to remove the movie recorder from the 
filter chain and close off the recording using code like the following:

    movieWriter.CompletionHandler = async () => {
        filter.RemoveTarget (movieWriter);
        await movieWriter.FinishRecordingAsync ();
    };

A movie won't be usable until it has been finished off, so if this is 
interrupted before this point, the recording will be lost.

### Interacting with OpenGL ES

GPUImage can both export and import textures from OpenGL ES through the use of 
its `GPUImageTextureOutput` and `GPUImageTextureInput` classes, respectively. This 
lets you record a movie from an OpenGL ES scene that is rendered to a framebuffer 
object with a bound texture, or filter video or images and then feed them into 
OpenGL ES as a texture to be displayed in the scene.

The one caution with this approach is that the textures used in these processes 
must be shared between GPUImage's OpenGL ES context and any other context via a 
share group or something similar.

## Built-in Filters

There are currently 125 built-in filters, divided into the following categories:

### Color Adjustments

- **GPUImageBrightnessFilter**: Adjusts the brightness of the image
- **GPUImageExposureFilter**: Adjusts the exposure of the image
- **GPUImageContrastFilter**: Adjusts the contrast of the image
- **GPUImageSaturationFilter**: Adjusts the saturation of an image
- **GPUImageGammaFilter**: Adjusts the gamma of an image
- **GPUImageLevelsFilter**: Photoshop-like levels adjustment. The min, max, minOut 
  and maxOut parameters are floats in the range [0, 1]. 
- **GPUImageColorMatrixFilter**: Transforms the colors of an image by applying a 
  matrix to them
- **GPUImageRGBFilter**: Adjusts the individual RGB channels of an image
- **GPUImageHueFilter**: Adjusts the hue of an image
- **GPUImageWhiteBalanceFilter**: Adjusts the white balance of an image.
- **GPUImageToneCurveFilter**: Adjusts the colors of an image based on spline 
  curves for each color channel.
- **GPUImageHighlightShadowFilter**: Adjusts the shadows and highlights of an 
  image
- **GPUImageLookupFilter**: Uses an RGB color lookup image to remap the colors 
  in an image. ([lookup.png][lookup])
- **GPUImageAmatorkaFilter**: A photo filter based on a [Photoshop action by 
  Amatorka][2]. ([lookup_amatorka.png][lookup_amatorka])
- **GPUImageMissEtikateFilter**: A photo filter based on a [Photoshop action by 
  Miss Etikate][3]. ([lookup_miss_etikate.png][lookup_miss_etikate])
- **GPUImageSoftEleganceFilter**: Another lookup-based color remapping filter. 
  ([lookup_soft_elegance_1.png][lookup_soft_elegance_1] and [lookup_soft_elegance_2.png][lookup_soft_elegance_2])
- **GPUImageColorInvertFilter**: Inverts the colors of an image
- **GPUImageGrayscaleFilter**: Converts an image to grayscale (a slightly faster 
  implementation of the saturation filter, without the ability to vary the 
  color contribution)
- **GPUImageMonochromeFilter**: Converts the image to a single-color version, 
  based on the luminance of each pixel
- **GPUImageFalseColorFilter**: Uses the luminance of the image to mix between 
  two user-specified colors
- **GPUImageHazeFilter**: Used to add or remove haze (similar to a UV filter)
- **GPUImageSepiaFilter**: Simple sepia tone filter
- **GPUImageOpacityFilter**: Adjusts the alpha channel of the incoming image
- **GPUImageSolidColorGenerator**: This outputs a generated image with a solid 
  color.
- **GPUImageLuminanceThresholdFilter**: Pixels with a luminance above the 
  threshold will appear white, and those below will be black
- **GPUImageAdaptiveThresholdFilter**: Determines the local luminance around a 
  pixel, then turns the pixel black if it is below that local luminance and 
  white if above.
- **GPUImageAverageLuminanceThresholdFilter**: This applies a thresholding 
  operation where the threshold is continually adjusted based on the average 
  luminance of the scene.
- **GPUImageHistogramFilter**: This analyzes the incoming image and creates 
  an output histogram with the frequency at which each color value occurs. 
- **GPUImageHistogramGenerator**: This is a special filter, in that it's 
  primarily intended to work with the `GPUImageHistogramFilter`. It generates 
  an output representation of the color histograms generated by 
  `GPUImageHistogramFilter`.
- **GPUImageAverageColor**: This processes an input image and determines the 
  average color of the scene, by averaging the RGBA components for each 
  pixel in the image.
- **GPUImageLuminosity**: Like the `GPUImageAverageColor`, this reduces an image 
  to its average luminosity.
- **GPUImageChromaKeyFilter**: For a given color in the image, sets the alpha 
  channel to 0.

### Image Processing

- **GPUImageTransformFilter**: This applies an arbitrary 2-D or 3-D transformation 
  to an image
- **GPUImageCropFilter**: This crops an image to a specific region, then passes 
  only that region on to the next stage in the filter
- **GPUImageLanczosResamplingFilter**: This lets you up- or downsample an image 
  using Lanczos resampling
- **GPUImageSharpenFilter**: Sharpens the image
- **GPUImageUnsharpMaskFilter**: Applies an unsharp mask
- **GPUImageGaussianBlurFilter**: A hardware-optimized, variable-radius Gaussian 
  blur
- **GPUImageBoxBlurFilter**: A hardware-optimized, variable-radius box blur
- **GPUImageSingleComponentGaussianBlurFilter**: A modification of the 
  `GPUImageGaussianBlurFilter` that operates only on the red component
- **GPUImageGaussianSelectiveBlurFilter**: A Gaussian blur that preserves 
  focus within a circular region
- **GPUImageGaussianBlurPositionFilter**: The inverse of the 
  `GPUImageGaussianSelectiveBlurFilter`, applying the blur only within a certain 
  circle
- **GPUImageiOSBlurFilter**: An attempt to replicate the background blur used on 
  iOS 7 in places like the control center.
- **GPUImageMedianFilter**: Takes the median value of the three color components, 
  over a 3x3 area
- **GPUImageBilateralFilter**: A bilateral blur, which tries to blur similar color 
  values while preserving sharp edges
- **GPUImageTiltShiftFilter**: A simulated tilt shift lens effect
- **GPUImage3x3ConvolutionFilter**: Runs a 3x3 convolution kernel against the 
  image
- **GPUImageSobelEdgeDetectionFilter**: Sobel edge detection, with edges 
  highlighted in white
- **GPUImagePrewittEdgeDetectionFilter**: Prewitt edge detection, with edges 
  highlighted in white
- **GPUImageThresholdEdgeDetectionFilter**: Performs Sobel edge detection, but 
  applies a threshold instead of giving gradual strength values
- **GPUImageCannyEdgeDetectionFilter**: This uses the full Canny process to 
  highlight one-pixel-wide edges
- **GPUImageHarrisCornerDetectionFilter**: Runs the Harris corner detection 
  algorithm on an input image, and produces an image with those corner points 
  as white pixels and everything else black.
- **GPUImageNobleCornerDetectionFilter**: Runs the Noble variant on the Harris 
  corner detector. It behaves as described above for the Harris detector.
- **GPUImageShiTomasiCornerDetectionFilter**: Runs the Shi-Tomasi feature 
  detector. It behaves as described above for the Harris detector.
- **GPUImageNonMaximumSuppressionFilter**: Currently used only as part of the 
  Harris corner detection filter
- **GPUImageXYDerivativeFilter**: An internal component within the Harris corner 
  detection filter
- **GPUImageCrosshairGenerator**: This draws a series of crosshairs on an image, 
  most often used for identifying machine vision features.
- **GPUImageDilationFilter**: This performs an image dilation operation, where 
  the maximum intensity of the red channel in a rectangular neighborhood is 
  used for the intensity of this pixel.
- **GPUImageRGBDilationFilter**: This is the same as the `GPUImageDilationFilter`, 
  except that this acts on all color channels, not just the red channel.
- **GPUImageErosionFilter**: This performs an image erosion operation, where the 
  minimum intensity of the red channel in a rectangular neighborhood is used for 
  the intensity of this pixel.
- **GPUImageRGBErosionFilter**: This is the same as the `GPUImageErosionFilter`, 
  except that this acts on all color channels, not just the red channel.
- **GPUImageOpeningFilter**: This performs an erosion on the red channel of an 
  image, followed by a dilation of the same radius.
- **GPUImageRGBOpeningFilter**: This is the same as the `GPUImageOpeningFilter`, 
  except that this acts on all color channels, not just the red channel.
- **GPUImageClosingFilter**: This performs a dilation on the red channel of an 
  image, followed by an erosion of the same radius.
- **GPUImageRGBClosingFilter**: This is the same as the `GPUImageClosingFilter`, 
  except that this acts on all color channels, not just the red channel.
- **GPUImageLocalBinaryPatternFilter**: This performs a comparison of intensity 
  of the red channel of the 8 surrounding pixels and that of the central one, 
  encoding the comparison results in a bit string that becomes this pixel 
  intensity.
- **GPUImageLowPassFilter**: This applies a low pass filter to incoming video 
  frames.
- **GPUImageHighPassFilter**: This applies a high pass filter to incoming video 
  frames.
- **GPUImageMotionDetector**: This is a motion detector based on a high-pass 
  filter.
- **GPUImageHoughTransformLineDetector**: Detects lines in the image using a 
  Hough transform into parallel coordinate space.
- **GPUImageLineGenerator**: A helper class that generates lines which can 
  overlay the scene.
- **GPUImageMotionBlurFilter**: Applies a directional motion blur to an image
- **GPUImageZoomBlurFilter**: Applies a directional motion blur to an image

### Blending Modes

- **GPUImageChromaKeyBlendFilter**: Selectively replaces a color in the first 
  image with the second image
- **GPUImageDissolveBlendFilter**: Applies a dissolve blend of two images
- **GPUImageMultiplyBlendFilter**: Applies a multiply blend of two images
- **GPUImageAddBlendFilter**: Applies an additive blend of two images
- **GPUImageSubtractBlendFilter**: Applies a subtractive blend of two images
- **GPUImageDivideBlendFilter**: Applies a division blend of two images
- **GPUImageOverlayBlendFilter**: Applies an overlay blend of two images
- **GPUImageDarkenBlendFilter**: Blends two images by taking the minimum value 
  of each color component between the images
- **GPUImageLightenBlendFilter**: Blends two images by taking the maximum value 
  of each color component between the images
- **GPUImageColorBurnBlendFilter**: Applies a color burn blend of two images
- **GPUImageColorDodgeBlendFilter**: Applies a color dodge blend of two images
- **GPUImageScreenBlendFilter**: Applies a screen blend of two images
- **GPUImageExclusionBlendFilter**: Applies an exclusion blend of two images
- **GPUImageDifferenceBlendFilter**: Applies a difference blend of two images
- **GPUImageHardLightBlendFilter**: Applies a hard light blend of two images
- **GPUImageSoftLightBlendFilter**: Applies a soft light blend of two images
- **GPUImageAlphaBlendFilter**: Blends the second image over the first, based 
  on the second's alpha channel
- **GPUImageSourceOverBlendFilter**: Applies a source over blend of two images
- **GPUImageColorBurnBlendFilter**: Applies a color burn blend of two images
- **GPUImageColorDodgeBlendFilter**: Applies a color dodge blend of two images
- **GPUImageNormalBlendFilter**: Applies a normal blend of two images
- **GPUImageColorBlendFilter**: Applies a color blend of two images
- **GPUImageHueBlendFilter**: Applies a hue blend of two images
- **GPUImageSaturationBlendFilter**: Applies a saturation blend of two images
- **GPUImageLuminosityBlendFilter**: Applies a luminosity blend of two images
- **GPUImageLinearBurnBlendFilter**: Applies a linear burn blend of two images
- **GPUImagePoissonBlendFilter**: Applies a Poisson blend of two images
- **GPUImageMaskFilter**: Masks one image using another

### Visual Effects

- **GPUImagePixellateFilter**: Applies a pixellation effect on an image or video
- **GPUImagePolarPixellateFilter**: Applies a pixellation effect on an image or 
  video, based on polar coordinates instead of Cartesian ones
- **GPUImagePolkaDotFilter**: Breaks an image up into colored dots within a 
  regular grid
- **GPUImageHalftoneFilter**: Applies a halftone effect to an image, like 
  news print
- **GPUImageCrosshatchFilter**: This converts an image into a black-and-white 
  crosshatch pattern
- **GPUImageSketchFilter**: Converts video to look like a sketch. This is just 
  the Sobel edge detection filter with the colors inverted
- **GPUImageThresholdSketchFilter**: Same as the sketch filter, only the edges 
  are thresholded instead of being grayscale
- **GPUImageToonFilter**: This uses Sobel edge detection to place a black border 
  around objects, and then it quantizes the colors present in the image to give 
  a cartoon-like quality to the image.
- **GPUImageSmoothToonFilter**: This uses a similar process as the 
  `GPUImageToonFilter`, only it precedes the toon effect with a Gaussian blur 
  to smooth out noise.
- **GPUImageEmbossFilter**: Applies an embossing effect on the image
- **GPUImagePosterizeFilter**: This reduces the color dynamic range into the 
  number of steps specified, leading to a cartoon-like simple shading of the 
  image.
- **GPUImageSwirlFilter**: Creates a swirl distortion on the image
- **GPUImageBulgeDistortionFilter**: Creates a bulge distortion on the image
- **GPUImagePinchDistortionFilter**: Creates a pinch distortion of the image
- **GPUImageStretchDistortionFilter**: Creates a stretch distortion of the image
- **GPUImageSphereRefractionFilter**: Simulates the refraction through a glass 
  sphere
- **GPUImageGlassSphereFilter**: Same as the `GPUImageSphereRefractionFilter`, only 
  the image is not inverted and there's a little bit of frosting at the edges of 
  the glass
- **GPUImageVignetteFilter**: Performs a vignetting effect, fading out the 
  image at the edges
- **GPUImageKuwaharaFilter**: Kuwahara image abstraction. This produces an oil-
  painting-like image, but it is extremely computationally expensive
- **GPUImageKuwaharaRadius3Filter**: A modified version of the Kuwahara filter, 
  optimized to work over just a radius of three pixels
- **GPUImagePerlinNoiseFilter**: Generates an image full of Perlin noise
- **GPUImageCGAColorspaceFilter**: Simulates the colorspace of a CGA monitor
- **GPUImageMosaicFilter**: This filter takes an input tileset, the tiles must 
  ascend in luminance.
- **GPUImageJFAVoronoiFilter**: Generates a Voronoi map, for use in a later stage.
- **GPUImageVoronoiConsumerFilter**: Takes in the Voronoi map, and uses that 
  to filter an incoming image.

You can also easily write your own custom filters using the C-like OpenGL 
Shading Language, as described above.

[1]: http://www.sunsetlakesoftware.com/2010/10/22/gpu-accelerated-video-processing-mac-and-ios
[2]: http://amatorka.deviantart.com/art/Amatorka-Action-2-121069631
[3]: http://miss-etikate.deviantart.com/art/Photoshop-Action-15-120151961
[4]: http://medusa.fit.vutbr.cz/public/data/papers/2011-SCCG-Dubska-Real-Time-Line-Detection-Using-PC-and-OpenGL.pdf
[5]: http://medusa.fit.vutbr.cz/public/data/papers/2011-CVPR-Dubska-PClines.pdf

[lookup]: https://github.com/BradLarson/GPUImage/blob/0.1.7/framework/Resources/lookup.png
[lookup_amatorka]: https://github.com/BradLarson/GPUImage/blob/0.1.7/framework/Resources/lookup_amatorka.png
[lookup_miss_etikate]: https://github.com/BradLarson/GPUImage/blob/0.1.7/framework/Resources/lookup_miss_etikate.png
[lookup_soft_elegance_1]: https://github.com/BradLarson/GPUImage/blob/0.1.7/framework/Resources/lookup_soft_elegance_1.png
[lookup_soft_elegance_2]: https://github.com/BradLarson/GPUImage/blob/0.1.7/framework/Resources/lookup_soft_elegance_2.png
