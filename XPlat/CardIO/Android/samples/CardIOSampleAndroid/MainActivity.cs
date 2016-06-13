using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Card.IO;

[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]

[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
[assembly: UsesFeature("android.hardware.camera.flash", Required = false)]

namespace CardIOSampleAndroid
{
	[Activity (Label = "card.io sample", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				var intent = new Intent(this, typeof(CardIOActivity));
				intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true); 	
				intent.PutExtra(CardIOActivity.ExtraRequireCvv, true); 		
				intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false); 
				intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);

				StartActivityForResult (intent, 101);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (data != null) {
				 
				var card = data.GetParcelableExtra (CardIOActivity.ExtraScanResult)
					.JavaCast<CreditCard>();

				FindViewById<TextView> (Resource.Id.textViewCardNumber).Text = card.FormattedCardNumber;
			}
		}
	}
}


