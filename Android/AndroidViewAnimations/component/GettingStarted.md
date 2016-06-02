
**Android View Animations** is a collection of animation view animations, to make the creation
of animations easier and privde a set of commonly-used animations.

## Usage

Making use of the animations is just like playing with a yo-yo:

    var myView = FindViewById (Resource.Id.myView)
    
    YoYo.With (Techniques.Tada)
        .Duration (700)
        .PlayOn (myView);

To animate a view without using `YoYo`, you can make use of the `Techniques` directly:

    var animator = Techniques.Tada.Animator;
    animator.SetDuration (1200);
    animator.Animate (target);

In the instance where you need to access the actual animator that will
be executing, use the `AnimatorAgent` property:

    animator.AnimatorAgent.SetDuration (2000);

### Effects
There are over 60 animations in 7 different styles:
 - Attension  
   `Flash`, `Pulse`, `RubberBand`, `Shake`, `Swing`, `Wobble`, `Bounce`, `Tada`, `StandUp`, `Wave`
 - Special  
   `Hinge`, `RollIn`, `RollOut`,`Landing`,`TakingOff`,`DropOut`
 - Bounce  
   `BounceIn`, `BounceInDown`, `BounceInLeft`, `BounceInRight`, `BounceInUp`
 - Fade  
   `FadeIn`, `FadeInUp`, `FadeInDown`, `FadeInLeft`, `FadeInRight`  
   `FadeOut`, `FadeOutDown`, `FadeOutLeft`, `FadeOutRight`, `FadeOutUp`
 - Flip  
   `FlipInX`, `FlipOutX`, `FlipOutY`
 - Rotate  
   `RotateIn`, `RotateInDownLeft`, `RotateInDownRight`, `RotateInUpLeft`, `RotateInUpRight`  
   `RotateOut`, `RotateOutDownLeft`, `RotateOutDownRight`, `RotateOutUpLeft`, `RotateOutUpRight`
 - Slide  
   `SlideInLeft`, `SlideInRight`, `SlideInUp`, `SlideInDown`  
   `SlideOutLeft`, `SlideOutRight`, `SlideOutUp`, `SlideOutDown`
 - Zoom  
   `ZoomIn`, `ZoomInDown`, `ZoomInLeft`, `ZoomInRight`, `ZoomInUp`  
   `ZoomOut`, `ZoomOutDown`, `ZoomOutLeft`, `ZoomOutRight`, `ZoomOutUp`
