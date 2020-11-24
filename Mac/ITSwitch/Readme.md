# ITSwitch

`ITSwitchView` is a simple and lightweight replica of iOS 7 `UISwitch` for macOS based on `NSControl`.

## Usage

`ITSwitchView` can be created programatically, as with any `NSView`, and added to a window

```csharp
var itSwitch = new ITSwitchView(new CGRect(20, 103, 32, 20)) {
	TintColor = NSColor.Red,
	On = true,
};

itSwitch.OnSwitchChanged += (object sender, EventArgs e) => {
	Console.WriteLine(itSwitch.On ? "enabled" : "disabled");
};

this.Window.ContentView.AddSubview(itSwitch);
```

**Properties**

`TintColor` can be used to set the background color of the `ITSwitchView`

`IsOn` can be used to set or get the state of the `ITSwitchView`

**Events**

 `OnSwitchChanged` is called when the state of the `ITSwitchView` is changed.

**Methods**

 `ITSwitchView.CalculateGoldenWidth` can be used to calculate the ideal width for the specified height.

## Tips

You may want to consider setting the width of the view to the golden ratio * height.

So for example:

```
height = 20;
width = height * 1.618;
```

## Attribution

This component is a port to C# from the original Objective-C repo created by [Ilija Tovilo](https://github.com/iluuu1994/ITSwitch)
