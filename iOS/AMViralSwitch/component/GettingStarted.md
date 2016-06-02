
**AMViralSwitch** is a `UISwitch` subclass that 'infects' the parent view with the `OnTintColor` 
when the switch is turned on.  
Inspired by [this Dribble](https://dribbble.com/shots/1749645-Contact-Sync) by 
[Ramotion](https://dribbble.com/teams/Ramotion).

## Usage
`AMViralSwitch` is a drop-in replacement of `UISwitch`:  
1. Use `AMViralSwitch` instead of `UISwitch` as the class
2. Set the `OnTintColor` property of the switch

The switch will automatically _infect_ its superview with the selected color.

## Animation

### Animation Duration
Use `AnimationDuration` property to control the animation's speed:

    switch.AnimationDuration = 1.5;


### Animation Completion
You can listen for when either of the `on` and `off` animations complete:

    switch.OnCompleted += delegate {
        Console.WriteLine ("Animation On");
    };
    switch.OffCompleted += delegate {
        Console.WriteLine ("Animation Off");
    };

### Animate Views
You can animate other views alongside the switch _infection_. Typically you'll want to change 
color to views or labels that are inside the same superview. 

To do this, you can make use of the `SetAnimationElementsOn` and `SetAnimationElementsOff`
methods. You can animate `CoreAnimation` properties likes this: 

    switch.SetAnimationElementsOn (
        AnimationElement.Layer (
            someView.Layer, 
            "backgroundColor", 
            UIColor.Clear.CGColor, 
            UIColor.White.CGColor)
    );

To animate the `TextColor` of an `UILabel` the syntax is slightly different:

    switch.SetAnimationElementsOn (
        AnimationElement.TextColor (someLabel, UIColor.White)
    );

Follow the same principle to animate the `tintColor` of your `UIButton`s:

    switch.SetAnimationElementsOn (
        AnimationElement.TintColor (someButton, UIColor.White)
    );

Finally, as the method accepts a `params AnimationElement[]`, you can set and array, or 
multiple:

    switch.SetAnimationElementsOn (
        AnimationElement.TintColor (someButton, UIColor.White),
        AnimationElement.TextColor (someLabel, UIColor.White),
        AnimationElement.Layer (
            someView.Layer, 
            "backgroundColor", 
            UIColor.Clear.CGColor, 
            UIColor.White.CGColor)
    );
