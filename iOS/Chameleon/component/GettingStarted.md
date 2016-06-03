**Chameleon** is a lightweight, yet powerful, color framework for iOS. It is 
built on the idea that software applications should function effortlessly while
simultaneously maintaining their beautiful interfaces.

With Chameleon, you can easily stop tinkering with RGB values, wasting hours 
figuring out the right color combinations to use in your app, and worrying 
about whether your text will be readable on the various background colors of 
your app. 

## Features

![Features](http://i.imgur.com/BCSA9Ez.png)

### Flat Colors

Chameleon features over 24 hand-picked colors that come in both light and 
dark shades:

    var color = ChameleonColor.FlatGreen;

There are two ways to generate a random flat color. If you have no preference 
as to whether you want a light shade or a dark shade, you can do the following:

    var color = ChameleonColor.GetRandomColor();

Otherwise, you can perform the following method call to specify whether it 
should return either a light or dark shade:

    var color = ChameleonColor.GetRandomColor(ShadeStyle.Light);


![Swatches](http://i.imgur.com/Hs8ICtJ.png)

#### Lighter and Darker Colors

Sometimes all you need is a color a shade lighter or a shade darker. 
Well for those rare, but crucial moments, Chameleon's got you covered. 
You can now lighten any color the following way:

    var lighter = color.LightenByPercentage(0.5f);

You can also generate a darker version of a color:

    var darker = color.DarkenByPercentage(0.5f);

#### Complementary Colors

To generate a complementary color, perform the following method call, 
remembering to specify the color whose complement you want:

    var complement = ChameleonColor.GetComplementaryColor(color);

### Contrasting Text

With a plethora of color choices available for text, it's difficult to choose 
one that all users will appreciate and be able to read. Whether you're in doubt 
of your text and tint color choices, or afraid to let users customize their 
profile colors because it may disturb the legibility or usability of the app, 
you no longer have to worry. With Chameleon, you can ensure that all text 
stands out independent of the background color.

The contrasting color feature returns either a dark color a light color 
depending on what the Chameleon algorithm believes is a better choice:

    var contrast = ChameleonColor.GetContrastingBlackOrWhiteColor(color, false);

![Status Bar](http://s29.postimg.org/i1syd7bkn/Contrast.gif)

### Colors From Images

Chameleon allows you to seamlessly extract non-flat or flat color schemes from 
images without hassle. You can also generate the average color from an image with 
ease. You can now mold the UI colors of a profile, or product based on an image!

    var colors = ChameleonColorArray.GetColors(image, false);

To extract the average color from an image, you can also do:

    var average = ChameleonColor.GetImageAverageColor(image);

![Colors from images](http://i.imgur.com/6JjFzHo.png)

## Theme Methods

With Chameleon, you can now specify a global color theme with simply one line 
of code (It even takes care of dealing with the status bar style as well)! 
Here's one of three methods to get you started. `ContentStyle` allows you to 
decide whether text and a few other elements should be white, black, or 
whichever contrasts more over any UI element's `BackgroundColor`. 

To set a global theme, you can do the following in your app delegate:

    Chameleon.SetGlobalTheme(
		ChameleonColor.FlatMint,
        ChameleonColor.FlatBlue,
		ContentStyle.Contrast);

But what if you want a different theme for a specific `UIViewController`? No 
problem, Chameleon allows you to override the global theme in any 
`UIViewController` and `UINavigationController`, by simply doing the following:

    this.SetTheme(
		ChameleonColor.FlatMint,
        ChameleonColor.FlatBlue,
		ContentStyle.Contrast);

### Contrasting Status Bar Styles

Many apps on the market, even the most popular ones, overlook this aspect of 
a beautiful app: the status bar style. Chameleon has done something no other 
framework has... it has created a new status bar style: 
`StatusBarStyle.Contrast`. Whether you have a `UIViewController` embedded in 
a `UINavigationController`, or not, you can do the following:

    this.SetStatusBarStyle (StatusBarStyle.Contrast);

### Navigation Bar Hairline

If you're seeking a true flat look, you can hide the hairline at the bottom 
of the navigation bar by doing the following: 

    NavigationController.HideNavigationBarHairline(true);
	
![No Hairline](http://i.imgur.com/tjwx53y.png)
