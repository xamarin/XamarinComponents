//
//  Created by Shopify.
//  Copyright (c) 2016 Shopify Inc. All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
//

using System;
using System.Threading.Tasks;
using Foundation;

using Shopify;

namespace ShopifyiOSSample
{
	public class GetShippingRatesOperation : NSOperation
	{
		private readonly BUYClient client;

		private bool done;
		private NSUrlSessionDataTask task;

		public event Action<GetShippingRatesOperation, BUYShippingRate[]> DidReceiveShippingRates;

		public event Action<GetShippingRatesOperation, NSError> FailedToReceiveShippingRates;

		public GetShippingRatesOperation (BUYClient client, BUYCheckout checkout)
		{
			this.client = client;
			this.Checkout = checkout;
		}

		public BUYCheckout Checkout { get; private set; }

		public BUYShippingRate[] ShippingRates { get; private set; }

		public override bool IsFinished {
			get {
				return base.IsFinished && done;
			}
		}

		public override void Cancel ()
		{
			task.Cancel ();

			base.Cancel ();
		}

		public override void Main ()
		{
			// We're now fetching the rates from Shopify. This will will calculate shipping rates very similarly to how our web checkout.
			// We then turn our BUYShippingRate objects into PKShippingMethods for Apple to present to the user.

			if (!Checkout.RequiresShipping) {
				WillChangeValue ("isFinished");
				done = true;
				DidChangeValue ("isFinished");

				DidReceiveShippingRates?.Invoke (this, null);
			} else {
				PollForShippingRatesAsync ();
			}
		}

		private void PollForShippingRatesAsync ()
		{
			BUYStatus shippingStatus = BUYStatus.Unknown;

			task = client.GetShippingRatesForCheckout (Checkout, async (shippingRates, status, error) => {
				shippingStatus = status;

				if (error != null) {
					WillChangeValue ("isFinished");
					done = true;
					DidChangeValue ("isFinished");

					FailedToReceiveShippingRates?.Invoke (this, error);
				} else if (shippingStatus == BUYStatus.Complete) {
					ShippingRates = shippingRates;

					WillChangeValue ("isFinished");
					done = true;
					DidChangeValue ("isFinished");

					DidReceiveShippingRates (this, ShippingRates);
				} else if (shippingStatus == BUYStatus.Processing) {
					await Task.Delay (500);
					PollForShippingRatesAsync ();
				}
			});
		}

	}
}
