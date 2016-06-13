
## Adding card.io to your Android app

Your application must be set to use a minimum Android SDK level of 8 (Froyo 2.2) or higher to use card.io.

There are two activities that card.io provides.  They need to be registered by adding them to your `AndroidManifest.xml` by adding the following lines inside of the `<application>...</application>` tags:

```
<application>
    <activity android:name="io.card.payment.CardIOActivity" 
        android:configChanges="keyboardHidden|orientation" />
    <activity android:name="io.card.payment.DataEntryActivity" />
</application>  
```

You will also need to add permissions that card.io requires to your manifest.  You can add them directly to the manifest file, or you can add the following C# code to your project:

```
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]
```

You should also declare some of the features your app is using.  Again you can do this directly in the manifest, or by adding this C# code to your project:

```
[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
[assembly: UsesFeature("android.hardware.camera.flash", Required = false)]
```


You can start scanning by starting the CardIOActivity for a result:

```
var intent = new Intent(this, typeof(CardIOActivity));
intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true); 	
intent.PutExtra(CardIOActivity.ExtraRequireCvv, true); 		
intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false); 
intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);

StartActivityForResult (intent, 101);
```

In your activity you should override the `OnActivityResult` to receive the result from the activity we just started:

```
protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
{
	base.OnActivityResult (requestCode, resultCode, data);

	if (data != null) {

		// Be sure to JavaCast to a CreditCard (normal cast won't work)		 
		var card = data.GetParcelableExtra (CardIOActivity.ExtraScanResult)
						.JavaCast<CreditCard> ();
				
		Console.WriteLine ("Scanned: " + card.FormattedCardNumber);
	}
}
```
