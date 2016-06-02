Progress HUD (a.k.a. MBProgressHUD) is a translucent, HUD-style dialog
that shows a progress indicator and a status label. It's the perfect
control for giving the user feedback when work is being done in a
background thread.

Here's an example:

```csharp
using MBProgressHUD;
...

public override void ViewDidLoad ()
{
	base.ViewDidLoad ();
	
	var hud = new MTMBProgressHUD (View) {
		LabelText = "Waiting...",
		RemoveFromSuperViewOnHide = true
	};

	View.AddSubview (hud);
		
	hud.Show (animated: true);
	hud.Hide (animated: true, delay: 5);
}
```

## Details

The main guideline you need to follow when dealing with MBProgressHUD while running long-running
tasks is keeping the main thread work-free, so the UI can be updated promptly. This is accomplished
by using MBProgressHUD on the main thread to show progress, while running heavy tasks on a
background thread.
