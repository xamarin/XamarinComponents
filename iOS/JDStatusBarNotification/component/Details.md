**JDStatusBarNotification** allows an app to show messages on top of the status bar. 

## Usage

### Show Notification

We can use the `Show()` method to show a notification over the status bar:

    StatusBarNotification.Show("Status Message!");

We can also show a timed message:

    // show a message for 5 seconds
    StatusBarNotification.Show("Status Message!", 5.0);

In all instances, the return value will be the notification view. You can just ignore it, but 
if you need further customization, this is where you can access the view:

    var view = StatusBarNotification.Show("Message!");

### Dismiss Notification

We can dismiss the current message with the `Dismiss()` method:

    StatusBarNotification.Dismiss();

We can also add a delay before the dismissal:

    // dismiss after 5 seconds
    StatusBarNotification.Dismiss(5.0);
    
### Show Progress

In the cases where there is progress information, we can use the `ShowProgress()` method: 

    // show progress for 75% (between 0.0 and 1.0)
    StatusBarNotification.ShowProgress(0.75f);
    
### Show Activity

If there is something happening, but there is no progress information available, 
we can use the `ShowActivityIndicator()` method:

    StatusBarNotification.ShowActivityIndicator(true, UIActivityIndicatorViewStyle.Gray);

For various styles and colors, we can use any of the values provided by the native iOS 
enumeration: `UIActivityIndicatorViewStyle`
    
## Styling

There are several provided styles and colors. They are obtained from the `StatusBarStyles` type:

 - `Default` - This style is used when no (or invalid) style is specified
 - `Dark`
 - `Success`
 - `Warning`
 - `Error`
 - `Matrix`

We can apply a style to the notification using the style name:

    StatusBarNotification.Show("Status Message!", StatusBarStyles.Success);

### Custom Styles

We can also create our own styles entirely by adding a style entry using the `AddStyle()` 
method:

    // create a new style with the key "custom-style-key"
    StatusBarNotification.AddStyle("custom-style-key", style => { 
        style.BarColor = new UIColor(0.797f, 0.000f, 0.662f, 1.000f);
        style.TextColor = UIColor.White;
        style.AnimationType = StatusBarAnimationType.Fade;
        style.Font = UIFont.FromName("SnellRoundhand-Bold", 17.0f);
        style.ProgressBarColor = new UIColor(0.986f, 0.062f, 0.598f, 1.000f);
        style.ProgressBarHeight = 20.0f;
        return style;
    });

Use this new style with the `Show()` methods:

    StatusBarNotification.Show("Status Message!", "custom-style-key");

If we want to change the default style throughout the app, we ca use the `SetDefaultStyle()`
method is the same way:

    StatusBarNotification.SetDefaultStyle(style => { 
        // set up the default style
        return style;
    });

There are several properties available for customization on the `StatusBarStyle` type:

 - `BarColor` is a `UIColor`
 - `TextColor` is a `UIColor`
 - `Font` is a `UIFont`
 - `AnimationType` is one of the `StatusBarAnimationType`
    - `None` - won't animate
    - `Move` - will move in from the top, and move out again to the top
    - `Bounce` - will fall down from the top and bounce a little bit
    - `Fade` - will fade in and fade out
 - `TextShadow` is a `NSShadow`
 - `TextVerticalPositionAdjustment` is a `nfloat`
 - `ProgressBarColor` is a `UIColor`
 - `ProgressBarHeight` is a `nfloat`
 - `ProgressBarPosition` one of the `StatusBarProgressBarPosition`
    - `Bottom` - will be at the bottom of the status bar
    - `Center` - will be at the center of the status bar
    - `Top` - will be at the top of the status bar
    - `Below` - will be below the status bar (the prograss bar won't move with the statusbar in this case)
    - `NavBar` - will be below the navigation bar (the prograss bar won't move with the statusbar in this case)


## Beware

[@goelv][goelv] / [@dskyu][dskyu] / [@graceydb][graceydb] informed me (see [#15][15], 
[#30][30], [#49][49]), that his app got rejected because of a status bar overlay 
(for violating 10.1/10.3). 

So don't overuse it. Although I haven't heard of any other cases.

[goelv]: https://github.com/goelv
[dskyu]: https://github.com/dskyu
[graceydb]: https://github.com/graceydb
[15]: https://github.com/jaydee3/JDStatusBarNotification/issues/15
[30]: https://github.com/jaydee3/JDStatusBarNotification/issues/30
[49]: https://github.com/jaydee3/JDStatusBarNotification/issues/49
