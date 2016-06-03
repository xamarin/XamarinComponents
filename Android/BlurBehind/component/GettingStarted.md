It's easy to add a blurred and transparent background to your Activity.  BlurBehind provides an easy way to have this effect, with customization, for your Activity without API level restrictions.

On the first activity, you should call  `BlurBehind.Instance.ExecuteAsync (this)` to initiate the blurring.  Once the blurring is completed, you can launch another activity that will appear over the first (now blurred) activity.

```csharp
button.Click += async (sender, e) => {

    // Perform the blur on this activity
    await BlurBehind.Instance.ExecuteAsync (this);

    // Launch the next activity
    var intent = new Intent (this, typeof (BlurredActivity));
    intent.SetFlags (ActivityFlags.NoAnimation);
    StartActivity (intent);
};
```

In your second activity, use `BlurBehind.Instance.SetBackground (this)` to set the second activity's background to be the blurred version of the first activity:

```csharp
BlurBehind.Instance
    .WithAlpha (80)
    .WithFilterColor (Color.ParseColor ("#0075c0"))
    .SetBackground (this);
```
