`RadialProgressView` is a stylish, circular progress view.  The circle
begins empty, and fills clockwise to indicate progress.

## Examples

To add `RadialProgressView` to your iOS app:

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

To add `RadialProgressView` to your Android app:

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

You can set minimum and maximum limits for `Value` instead of using the
defaults of `0` and `1`:

```csharp
progressView.MinValue = 0;
progressView.MaxValue = 100;
```

Property `IsDone` shows whether `Value` has reached `MaxValue`.  The
`Reset()` method resets `Value` to `MinValue`:

```csharp
if (progressView.IsDone)
	progressView.Reset ();
```

You can choose among three different appearance styles for
`RadialProgressView`: `Big` (default), `Small`, or `Tiny`.

```csharp
var smallProgressView = new RadialProgressView (RadialProgressViewStyle.Small);
```

To hide the progress percentage label:

```csharp
progressView.LabelHidden = true;
```

To change the progress color:

```csharp
progressView.ProgressColor = UIColor.Red;
```

## Adding RadialProgressView to AXML Layouts

On Android, you can also add `RadialProgressView` to your *axml*
layouts:

```xml
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
	android:orientation="vertical" android:layout_width="fill_parent" android:layout_height="fill_parent">
	<radialprogress.RadialProgressView
		android:id="@+id/progressView"
		android:layout_width="200px"
		android:layout_height="200px"
	min_value="0"
	max_value="100"
	progress_type="big"
	hide_label="false"
	progress_color="#FF00FF" />
</LinearLayout>
```
