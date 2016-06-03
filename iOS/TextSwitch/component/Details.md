
<iframe src="https://appetize.io/embed/4q3gea06fwytz18tb6x56kfna0?device=iphone5s&scale=75&autoplay=true&orientation=portrait&deviceColor=black" 
        width="274px" height="587px" frameborder="0" scrolling="no"
        style="float:right;margin-left:1em;">&nbsp;</iframe>

`TextSwitch` is a text-based switch, similar to the `UISwitch` provided by iOS.

##  Using TextSwitch

The easiest way to make use of `TextSwitch` is by simply adding it to your
view hierachy, either in code or in the designer:

    // create
    var switch = new TextSwitch("Off", "On");
    
    // set some colors
    var selected = UIColor.FromRGB(239, 95, 49);
    switch.BackgroundColor = selected;
    switch.TitleColor = UIColor.White;
    switch.SelectedBackgroundColor = UIColor.White;
    switch.SelectedTitleColor = selected;
    
    // switch change event
    switch.ValueChanged += delegate {
        var on = switch.On;
    };

## Properties

There are several properties that can be set by the designer and in code:

 * **AnimationDuration**  
   Specifies the duration of the switch animation (in seconds).

 * **AnimationSpringDamping**  
   Specifies the spring damping of the switch animation.

 * **AnimationInitialSpringVelocity**  
   Specifies the spring velocity of the switch animation.

 * **LeftTitle**  
   Specifies the text of the left side of the switch (off).

 * **RightTitle**  
   Specifies the text of the right side of the switch (on).

 * **On**  
   Specifies whether the switch is on (right) or off (left).

 * **SelectedBackgroundInset**  
   Specifies the inset of the selection pill.

 * **BackgroundColor**  
   Specifies the color of the switch.

 * **SelectedBackgroundColor**  
   Specifies the color of the selection pill.

 * **TitleColor**  
   Specifies the color of the unselected (off) text.

 * **SelectedTitleColor**  
   Specifies the color of the selected (on) text.

 * **TitleFont**  
   Specifies the font of the switch.

## Events

There is also an event that will be raised when the switch changes:

 * **ValueChanged**  
   Raised when the switch is changed, either on (right) or off (left).
