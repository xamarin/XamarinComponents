//
//  Created by Shopify.
//  Copyright (c) 2016 Shopify Inc. All rights reserved.
//  Copyright (c) 2016 Xamarin Inc. All rights reserved.
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

using Android.OS;
using Android.Widget;

namespace ShopifyAndroidSample.Activities.Base
{
	// Base class for activities with list views in the app.
	public class SampleListActivity : SampleActivity
	{
		protected ListView listView;
		protected bool isFetching;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			OnCreate(savedInstanceState, Resource.Layout.list_activity);
		}

		protected void OnCreate(Bundle savedInstanceState, int layoutId)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(layoutId);

			listView = FindViewById<ListView>(Resource.Id.list_view);
			isFetching = false;
		}
	}
}
