A drop-in replacement for UISegmentedControl that mimics a fancy tab control featured in the App Store.

## Example

```csharp
using SegmentedControl;
using System.Drawing;
...

public override void LoadView ()
{
	base.LoadView ();

	var labels = new [] { "Animals", "Vegetables", "Minerals" };
	var segments = new SDSegmentedControl (labels) {
		Frame = new RectangleF (0, 0, 320, 44)
	};
	segments.ValueChanged += (sender, e) => {
		Console.WriteLine ("Selected " + segments.SelectedSegment);
	};

	View.AddSubview (segments);
}
```

## Features

- Segments may contain text, images, or both.
- Animated segment selection.
- Content-aware segment width.
- Scrolls horizontally when there are many segments.
- Customizable appearance via UIAppearance.

# News: iOS Unified API Support