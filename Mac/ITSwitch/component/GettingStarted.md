ITSwitch
========
  
`ITSwitchView` is a simple and lightweight replica of iOS 7 `UISwitch` for Mac OS X based on NSControl.  
  
Usage
----
  
`ITSwitchView` can be created programatically, as with any `NSView`, and added to a window


	var sWitch = new ITSwitchView(new CGRect(20,103,32,20))
	{
		TintColor = NSColor.Red,
		On = true,
	};
	sWitch.OnSwitchChanged += (object sender, EventArgs e) => 
	{
		Console.WriteLine((sWitch.On) ? "enabled" : "disabled");
	}; 

	this.Window.ContentView.AddSubview(sWitch);

**Interface Builder** 

Unfortunately you can not use `ITSwichView` directly from a Xib file in Interface Builder.  You will need to create a sub-class of `ITSwitchView` and then `Register` it to make it available to Xcode.

The `LocalITSwitch` class can be found in the sample to demonstrate this approach.  

	[Register("LocalITSwitch")]
	public class LocalITSwitch : ITSwitchView
	{
		public LocalITSwitch()
			: base()
		{
			
		}


		public LocalITSwitch(IntPtr ptr) 
			: base(ptr)
		{
			
		}

		public LocalITSwitch(NSCoder coder) : base(coder)
		{
			
		}

		public LocalITSwitch(CGRect frame) : base(frame)
		{
			
		}
	}

**Properties**  
  
`TintColor` can be used to set the background color of the `ITSwitchView`  
  
`IsOn` can be used to set or get the state of the `ITSwitchView`  
  
**Events**  
  
 `OnSwitchChanged` is called when the state of the `ITSwitchView` is changed.
 
**Methods**  
  
 `ITSwitchView.CalculateGoldenWidth` can be used to calculate the ideal width for the specified height.  
 
Tips
----
  
You may want to consider setting the width of the view to the golden ratio * height. 

So for example:

	height = 20;
	width = height * 1.618;
	
  	
Attribution
----

This component is a port to C# from the original Objective-C repo created by [Ilija Tovilo](https://github.com/iluuu1994/ITSwitch)