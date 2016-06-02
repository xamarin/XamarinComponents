
### Show Notification
HDNotification uses a shared instance to display notifications. All methods are static and can be found on `HDNotificationView`.

By default the notification will hide after 7 seconds.
```csharp
HDNotificationView.ShowNotification (
				image: UIImage.FromBundle ("sampleIcon"),
				title: "My Title",
				message: "My Message"
			);
```

You can also turn off AutoHide and manually hide the notification:
```csharp
HDNotificationView.ShowNotification (
				image: UIImage.FromBundle ("sampleIcon"),
				title: "No Auto Hide",
				message: "We will have to manually dismiss",
				isAutoHide: false
			);
			
//Later on in code you can dismiss it manually:
HDNotificationView.HideNotification();
```

Finally, you can register for a touch event on the notification:
```csharp
HDNotificationView.ShowNotification (
				image: UIImage.FromBundle ("sampleIcon"),
				title: "Touch Events!",
				message: "We will dismiss automatically or on touch",
				isAutoHide: true,
				onTouch: () => {
					HDNotificationView.HideNotification ();
				}
			);
```

### Hide Notification
You are able to hide the notification that is visible and:
```csharp
HDNotificationView.HideNotification ();
```

Also, you can get notified when the notification is completely hidden:
```csharp
HDNotificationView.HideNotification (()=>
  {
    //It is gone!
  });
```



