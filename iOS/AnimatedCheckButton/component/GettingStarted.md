
Animated Check Button is an animated `UIControl` that has a nice animated transition between its checked
and unchecked states.

##  Using AnimatedCheckButton

`AnimatedCheckButton` can be used in both storyboards and code-behind. Once instantiated, it can be used
as a check box:

    var frame = new CGRect(0, 0, 140, 140);
    var button = new AnimatedCheckButton(frame);
    
    // wait for events
    button.CheckedChanged += (sender, e) => {
        var isChecked = button.Checked;
    };
    
    // set the state programatically
    button.Checked = true;

## Using Properties

`AnimatedCheckButton` is fully supported in storyboards and in the storyboard designer. 
There are several properties that are available in the designer:

  * `Color`  
    The color of the current state (such as the check mark)
    
  * `SkeletonColor`  
    The color of the state that is available (such as the surrounding circle)
    
  * `Checked`  
    The value representing whether the view is checked or not
    
## Using Events

Along with the various properties, there is support for listening to state changes through events:
    
  * `CheckedChanged`  
    The event that is raised when the checked state changes
