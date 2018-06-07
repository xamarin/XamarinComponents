using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Stripe.iOS;

namespace Stripe.UIExamples
{
	public class MockCustomer : Customer
	{
		List<ISourceProtocol> mockSources = new List<ISourceProtocol> ();
		ISourceProtocol mockDefaultSource;
		Address mockShippingAddress;

		public MockCustomer ()
		{
			/** 
	         Preload the mock customer with saved cards.
	         last4 values are from test cards: https://stripe.com/docs/testing#cards
	         Not using the "4242" and "4444" numbers, since those are the easiest 
	         to remember and fill.
	        */
			var visa = new NSDictionary (
				"id", "preloaded_visa",
				"exp_month", "10",
				"exp_year", "2020",
				"last4", "1881",
				"brand", "visa"
			);

			var card = Card.GetDecodedObject (visa);
			if (card != null)
				mockSources.Add (card);

			var masterCard = new NSDictionary (
				"id", "preloaded_mastercard",
				"exp_month", "10",
				"exp_year", "2020",
				"last4", "8210",
				"brand", "mastercard"
			);

			card = Card.GetDecodedObject (masterCard);
			if (card != null)
				mockSources.Add (card);

			var amex = new NSDictionary (
				"id", "preloaded_amex",
				"exp_month", "10",
				"exp_year", "2020",
				"last4", "0005",
				"brand", "american express"
			);

			card = Card.GetDecodedObject (amex);

			if (card != null)
				mockSources.Add (card);
		}

		public override ISourceProtocol[] Sources
		{
			get
			{
				return mockSources.ToArray ();
			}
		}

		internal void AddSource (ISourceProtocol source)
		{
			mockSources.Add (source);
		}

		internal void RemoveSource (ISourceProtocol source)
		{
			var index = mockSources.FindIndex (p => p.StripeId == source.StripeId);

			if (index >= 0)
				mockSources.RemoveAt (index);
		}

		public override ISourceProtocol DefaultSource
		{
			get
			{
				return mockDefaultSource;
			}
		}

		internal void SetDefaultSource (ISourceProtocol source)
		{
			mockDefaultSource = source;
		}

		public override Address ShippingAddress
		{
			get
			{
				return mockShippingAddress;
			}
		}

		internal void SetShippingAddress (Address address)
		{
			mockShippingAddress = address;
		}
	}

	class MockCustomerContext : CustomerContext
	{
		MockCustomer customer = new MockCustomer ();

		public override void RetrieveCustomer (CustomerCompletionBlock completion)
		{
			if (completion != null)
				completion (customer, null);
		}

		public override void AttachSourceToCustomer (ISourceProtocol source, STPErrorBlock completion)
		{
			var token = source as Token;
			if (token != null)
			{
				var card = token.Card;
				if (card != null)
					customer.AddSource (card);
			}

			if (completion != null)
				completion (null);
		}

		public override void SelectDefaultCustomerSource (ISourceProtocol source, STPErrorBlock completion)
		{
			if (customer.Sources.Where ( p => p.StripeId == source.StripeId).Any ())
				customer.SetDefaultSource (source);

			if (completion != null)
				completion (null);
		}

		public override void UpdateCustomer (Address shipping, STPErrorBlock completion)
		{
			customer.SetShippingAddress (shipping);

			if (completion != null)
				completion (null);
		}

		public override void DetachSource (ISourceProtocol source, STPErrorBlock completion)
		{
			customer.RemoveSource (source);

			if (completion != null)
				completion (null);
		}
	}
}
