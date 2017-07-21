`RadialProgressView` is a stylish, circular progress view for iOS
and Android. The circle begins empty, and fills clockwise to indicate
progress.

To use `RadialProgressView` from iOS:

```csharp
using RadialProgress;
...
public override void ViewDidLoad()
{
	base.ViewDidLoad();

	var progressView = new RadialProgressView {
		Center = new PointF (View.Center.X, View.Center.Y - 100)
	};
	View.AddSubview (progressView);
}
```

From Android:

```csharp
using RadialProgress;
...
protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	var progressView = new RadialProgressView (this);
	AddContentView (progressView, new ViewGroup.LayoutParams (200, 200));
}
```

Set the view's `Value` property to values between `0` and `1` to update
progress percentage:

```csharp
progressView.Value = 0.5f;
```
