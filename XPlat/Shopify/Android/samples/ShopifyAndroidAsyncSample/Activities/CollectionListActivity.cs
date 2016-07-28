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

using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

using Shopify.Buy;
using Shopify.Buy.Model;

using ShopifyAndroidSample.Activities.Base;

namespace ShopifyAndroidSample.Activities
{
	// The first activity in the app flow. Allows the user to browse the list of collections and drill down into a list of products.
	[IntentFilter(
		new[] { Intent.ActionView },
		Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
		DataScheme = "@string/web_return_to_scheme")]
	[Activity(MainLauncher = true, LaunchMode = LaunchMode.SingleTop)]
	public class CollectionListActivity : SampleListActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetTitle(Resource.String.choose_collection);
		}

		protected async override void OnResume()
		{
			base.OnResume();

			// If we haven't already loaded the products from the store, do it now
			if (listView.Adapter == null && !isFetching)
			{
				isFetching = true;
				ShowLoadingDialog(Resource.String.loading_data);

				try
				{
					// Fetch the collections
					var collections = await SampleApplication.GetCollectionsAsync();
					DismissLoadingDialog();
					OnFetchedCollections(collections.ToList());
				}
				catch (ShopifyException ex)
				{
					OnError(ex.Error);
				}

				isFetching = false;
			}
		}

		// Once the collections are fetched from the server, set our listView adapter so that the collections appear on screen.
		private void OnFetchedCollections(List<Collection> collections)
		{
			var collectionTitles = new List<string>();

			// Add an 'All Products' collection just in case there are products that do not belong to a collection
			collectionTitles.Add(GetString(Resource.String.all_products));
			foreach (var collection in collections)
			{
				collectionTitles.Add(collection.Title);
			}

			listView.Adapter = new ArrayAdapter(this, Resource.Layout.simple_list_item, collectionTitles);
			listView.ItemClick += (sender, e) =>
			{
				// When the user picks a collection, launch the product list activity to display the products in that collection.
				var collectionId = e.Position == 0 ? null : collections[e.Position - 1].CollectionId;
				var intent = new Intent(this, typeof(ProductListActivity));
				if (collectionId != null)
				{
					intent.PutExtra(ProductListActivity.ExtraCollectionId, collectionId);
				}

				StartActivity(intent);
			};
		}
	}
}