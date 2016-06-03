
Animated Shape Button is an animated `UIControl` that has a beautifle animated image mask between its checked
and unchecked states.

##  Using AnimatedShapeButton

`AnimatedShapeButton` can be used in both storyboards and code-behind. Once instantiated, it can be used
in a check box manner:

    var frame = new CGRect(0, 0, 140, 140);
    var button = new AnimatedShapeButton(frame);
    
    // wait for events
    button.CheckedChanged += (sender, e) => {
        var isChecked = button.Checked;
    };
    
    // set the state programatically
    button.Checked = true;

## Using Properties

`AnimatedShapeButton` is fully supported in storyboards and in the storyboard designer. 
There are several properties that are available in the designer:

  * `Image`  
    The mask (transparent png) image used to provide the shape
    
  * `Color`  
    The color of the checked state
    
  * `SkeletonColor`  
    The color of the state that is available
    
  * `CircleColor`  
    The color of the circle wave around the shape during the animation
    
  * `LinesColor`  
    The color of the little explosion lines during the animation
    
  * `Duration`  
    The value representing the animation duration/speed
    
## Using Events

Along with the various properties, there is support for listening to state changes through events:
    
  * `CheckedChanged`  
    The event that is raised when the checked state changes
