using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using Stripe.iOS;

namespace Stripe.UIExamples
{
	public class MockAPIClient : ApiClient
	{
		public override void CreateToken (CardParams card, TokenCompletionBlock completion)
		{
			if (completion == null)
				return;

			// Generate a mock card model using the given card params
			var cardJson = new NSMutableDictionary ();
			cardJson["id"] = new NSString ($"{card.GetNativeHash ()}");
			cardJson["exp_month"] = new NSString ($"{card.ExpMonth}");
			cardJson["exp_year"] = new NSString ($"{card.ExpYear}");
			cardJson["name"] = new NSString (card.Name);
			cardJson["address_line1"] = new NSString (card.Address.Line1);

			cardJson["address_line2"] = new NSString (card.Address.Line2);
			cardJson["address_state"] = new NSString (card.Address.State);

			cardJson["address_zip"] = new NSString (card.Address.PostalCode);
			cardJson["address_country"] = new NSString (card.Address.Country);

			cardJson["last4"] = new NSString (card.Last4);
			var number = card.Number;
			if (!string.IsNullOrEmpty (number))
			{
				var brand = CardValidator.GetBrand (number);
				cardJson["brand"] = new NSString (Card.StringFromBrand (brand));
			}
			cardJson["fingerprint"] = new NSString ($"{card.GetNativeHash ()}");
			cardJson["country"] = new NSString ("US");
			var tokenJson = new NSDictionary (
				new NSString ("id"), new NSString ($"{card.GetNativeHash ()}"),
				new NSString ("object"), new NSString ("token"),
				new NSString ("livemode"), NSObject.FromObject (false),
				new NSString ("created"), NSObject.FromObject (NSDate.Now.SecondsSinceReferenceDate),
				new NSString ("used"), NSObject.FromObject (false),
				new NSString ("card"), cardJson);

			var token = Token.GetDecodedObject (tokenJson);

			DispatchQueue.MainQueue.DispatchAfter (new DispatchTime (DispatchTime.Now, new TimeSpan (0, 0, 0, 0, 600)), () =>
			{
				completion ((Token)token, null);
			});
		}
	}

	class MockAddCardViewController : AddCardViewController
	{
		public MockAddCardViewController (PaymentConfiguration configuration, Theme theme) : base (configuration, theme)
		{
		}

		public ApiClient ApiClient
		{
			[Export ("apiClient")]
			get 
			{
				return new MockAPIClient ();
			} 
		}
	}
}
