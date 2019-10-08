
**Easing Functions** is a collection of animation easing function, to make the creation
of animation easier.

## Usage

Animations can be created quickly:

```csharp
var animator = Glider.Glide (
    Skill.BounceEaseInOut, 
    1200, 
    ObjectAnimator.OfFloat (targetView, "translationY", 0, 100))
animator.SetDuration (1200);
animator.Start ();
```

Multiple animations can also be combined in an `AnimatorSet`:

```csharp
var set = new AnimatorSet ();
set.PlayTogether (
    Glider.Glide (Skill.BounceEaseInOut, 1200, ObjectAnimator.OfFloat (targetView, "translationY", 0, 100)),
    Glider.Glide (Skill.BounceEaseInOut, 1200, ObjectAnimator.OfFloat (targetView, "translationX", 0, 100)));
set.SetDuration (1200);
set.Start ();
```

## Easing Functions

These easing functions are based on the [Easing Functions](http://easings.net/) 
by [Robert Penne](http://robertpenner.com/).

There are 27 easing functions in 10 groups:

 - Back (In/Out/InOut)
 - Bounce (In/Out/InOut)
 - Circ (In/Out/InOut)
 - Cubic (In/Out/InOut)
 - Elastic (In/Out)
 - Expo (In/Out/InOut)
 - Quad (In/Out/InOut)
 - Quint (In/Out/InOut)
 - Sine (In/Out/InOut)
 - Linear
