Pop aims to offer utility on three different axes. First, to make commonly needed animations extremely convenient. In addition to the four established static animations, Pop introduces three new primitive animation types: 

 - Spring
 - Decay
 - Custom


*Spring* and *decay* are dynamic animations that help bring your apps to life. *Spring* gives elements their attractive bounce. *Decay* brings movement to an eventual slow halt. Both take velocity as an input and are good candidates for realistically responding to user gestures.

Pop was designed to be an extensible framework. Custom animation allows developers to plug in their own animation code, making it easier to create unique effects.  Pop can animate any property of any Objective-C object.

Pop was constructed with a developer-friendly yet powerful programming model.  It uses the Core Animation interface for starting and stopping animations.  Most notably, it also supports querying running animations – which is key for interrupting animations and building fluid interfaces. 

The fundamental animation type is a `POPAnimation`.  The following code snippet showcases how to animate the bounds of a layer using a spring:

```csharp
var a = POPSpringAnimation.AnimationWithPropertyNamed (POPAnimation.LayerBounds) {
    ToValue = NSValue.FromCGRect (new CGRect (0, 0, 400, 400))
};

layer.AddAnimation (a, "size");
```


If you know how to use Core Animation explicit animations, you know how to use Pop.


## Learn More
To learn more about Pop, visit the official open-source repository: https://github.com/facebook/pop
