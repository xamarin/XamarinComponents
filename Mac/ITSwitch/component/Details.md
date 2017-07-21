ITSwitch
========

`ITSwitch` is a simple and lightweight replica of iOS 7 `UISwitch` for Mac OS X.

Features
----
  - Ability to change the background color
  - Supports programatic creation and Interface Builder(with caveats)
  - Scalable

Requirements
------------

ITSwitch requires 10.9+ and linking against the QuartzCore.framework. 

Tips
----

You may want to consider setting the width of the view to the golden ratio * height. 

So for example:

```objc
height = 20;
width = height * 1.618;
```

Attribution
----

This component is a port to C# from the original Objective-C repo created by [Ilija Tovilo](https://github.com/iluuu1994/ITSwitch)
