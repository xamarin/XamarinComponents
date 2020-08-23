# Pollexor Details

<iframe src="https://appetize.io/embed/9ng707xc4z3ubpqgcu42906k7c?device=nexus5&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="300px" height="597px" frameborder="0" scrolling="no"
        style="float: right;margin-left:24px;">&nbsp;</iframe>

> Thumbor image service client which allows you to build URIs in an expressive 
> fashion using a fluent API.

Thumbor is a smart imaging service. It enables on-demand crop, resizing 
and flipping of images.

It also features a VERY smart detection of important points in the image 
for better cropping and resizing, using state-of-the-art face and feature 
detection algorithms (more on that in Detection Algorithms).

Using thumbor is very easy (after it is running). All you have to do is 
access it using an URL for an image.

## Usage

To make use of create the `Thumbor` instance pointing to the Thumbor server:

    var thumbor = Thumbor.Create("http://thumbor.example.org/");

We can obtain a resized image using the `BuildImage` method:

    // produces: /unsafe/490x490/http://example.com/image.png
    var imageUri = thumbor
        .BuildImage("http://example.com/image.png")
        .Resize(490, 490)
        .ToUrl();

## Encryped Servers

To handle servers with encryption, we canpass in a key:

    var thumbor = Thumbor.Create("http://thumbor.example.org/", "KEY");

## More Options

We can change the format (png/jpg/webp/gif) of the returned image (using a filter):

    var imageUri = thumbor
        .BuildImage(ImageUrl)
        .Resize(490, 490)
        .Filter(ThumborUrlBuilder.Format(ThumborUrlBuilder.ImageFormat.Webp))
        .ToUrl();
    // produces: /unsafe/490x490/filters:format(webp)/http://example.com/image.png

We can apply filters to the image, such as rounded corners:

    var imageUri = thumbor
        .BuildImage(ImageUrl)
        .Resize(490, 490)
        .Filter(ThumborUrlBuilder.RoundCorner(245))
        .ToUrl();
    // produces: /unsafe/490x490/filters:round_corner(245,255,255,255)/http://example.com/image.png

*Note: If you are using a version of Thumbor older than 3.0 you must add `.Legacy()` 
to ensure the encryption used will be supported by the server.*