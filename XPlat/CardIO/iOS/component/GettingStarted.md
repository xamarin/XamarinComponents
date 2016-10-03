## Adding card.io to your iOS app

The most simple way to integrate card.io into your iOS app is to use the `CardIOPaymentViewController` just as you would any other UIViewController.  You will need to pass in an instance of `ICardIOPaymentViewControllerDelegate` to the constructor so you can handle when a card is scanned, or the user has cancelled scanning.

```
// Create and Show the View Controller
var paymentViewController = new CardIOPaymentViewController (this);

// Display the card.io interface
PresentViewController(paymentViewController, true);
```

You can implement `ICardIOPaymentViewControllerDelegate` to capture the results:

```
public void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
{
    Console.WriteLine("Scanning Canceled!");
}
public void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
{
    if (cardInfo == null) {
        Console.WriteLine("Scanning Canceled!");
    } else {
        Console.WriteLine("Card Scanned: " + cardInfo.CardNumber);
    }	

    paymentViewController.DismissViewController(true, null);        
}
```

### iOS 10+ Requires NSCameraUsageDescription

Starting in iOS 10, Apple requires that if your app wants to access the camera, your `Info.plist` must specify the reason you wish to access it, which will be displayed to the user when they are prompted to allow or deny your app permission to use the camera.

This means your `Info.plist` file should contain a string item with a key of `NSCameraUsageDescription` and a value containing a description of why you want to use the camera, to show the user.

