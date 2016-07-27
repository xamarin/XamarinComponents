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

using Foundation;
using UIKit;

namespace ShopifyiOSSample
{
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		// Enter your shop domain and API Key
		public const string SHOP_DOMAIN = "xamarinbindingexample.myshopify.com";
		public const string API_KEY = "6149b5dbe23ad2c34bb7098481471e33";
		public const string CHANNEL_ID = "46682499";

		// Enter your merchant ID
		public const string MERCHANT_ID = "merchant.com.xamarintests.xamarinbindings";

//		// Enter your shop domain and API Key
//		public const string SHOP_DOMAIN = "";
//		public const string API_KEY = "";
//		public const string CHANNEL_ID = "";
//
//		// Enter your merchant ID
//		public const string MERCHANT_ID = "";

		public static NSString CheckoutCallbackNotification = (NSString)"CheckoutCallbackNotification";

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			return true;
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			NSNotificationCenter.DefaultCenter.PostNotificationName (
				CheckoutCallbackNotification, 
				null,
				NSDictionary.FromObjectAndKey (url, (NSString)"url"));

			return true;
		}
	}
}
