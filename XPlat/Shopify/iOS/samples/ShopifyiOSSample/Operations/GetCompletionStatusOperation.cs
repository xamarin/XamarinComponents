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
	public class GetCompletionStatusOperation : NSOperation
	{
		private readonly BUYClient client;

		private bool done;
		private NSUrlSessionDataTask task;

		public event Action<GetCompletionStatusOperation, BUYStatus> DidReceiveCompletionStatus;

		public event Action<GetCompletionStatusOperation, NSError> FailedToReceiveCompletionStatus;

		public GetCompletionStatusOperation (BUYClient client, BUYCheckout checkout)
		{
			this.client = client;
			this.Checkout = checkout;
		}

		public BUYCheckout Checkout { get; private set; }

		public BUYStatus CompletionStatus { get; private set; }

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
			PollForCompletionStatusAsync ();
		}

		private void PollForCompletionStatusAsync ()
		{
			if (IsCancelled) {
				return;
			}

			task = client.GetCompletionStatusOfCheckout (Checkout, async (status, error) => {
				if (status == BUYStatus.Processing) {
					await Task.Delay (500);
					PollForCompletionStatusAsync ();
				} else {
					CompletionStatus = status;

					WillChangeValue ("isFinished");
					done = true;
					DidChangeValue ("isFinished");

					if (error != null) {
						FailedToReceiveCompletionStatus?.Invoke (this, error);
					} else {
						DidReceiveCompletionStatus?.Invoke (this, status);
					}
				}
			});
		}
	}
}
