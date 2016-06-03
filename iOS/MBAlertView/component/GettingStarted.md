Alert View (a.k.a. MBAlertView) is an overlay-style dialog and HUD
library for displaying beautiful alerts and other stylish feedback.

### Showing Alerts

```csharp
using AlertView;
...

public override void ViewDidAppear (bool animated)
{
  base.ViewDidAppear (animated);
  
  var alert = MBAlertView.AlertWithBody (
    body: "Are you sure you want to delete this note? You cannot undo this.",
    buttonTitle: "Cancel",
    handler: () => Console.WriteLine ("Cancel Selected")
  );

  alert.AddButtonWithText (
    text: "Delete", 
    bType:MBAlertViewItemType.Destructive, 
    handler: () => Console.WriteLine ("Delete Selected")
  );

  alert.AddToDisplayQueue ();
}
```

### Showing HUDs

```csharp
using AlertView;
...

public override void ViewDidAppear (bool animated)
{
  base.ViewDidAppear (animated);
  
  MBHUDView.HudWithBody (
    body: "Wait", 
    aType: MBAlertViewHUDType.ActivityIndicator, 
    delay: 4.0f, 
    showNow: true
  );
}
```
