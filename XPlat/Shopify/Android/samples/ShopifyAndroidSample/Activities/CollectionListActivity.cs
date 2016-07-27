using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
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

		protected override void OnResume()
		{
			base.OnResume();

			// If we haven't already loaded the products from the store, do it now
			if (listView.Adapter == null && !isFetching)
			{
				isFetching = true;
				ShowLoadingDialog(Resource.String.loading_data);

				// Fetch the collections
				SampleApplication.GetCollections(
					(collections, response) =>
					{
						isFetching = false;
						DismissLoadingDialog();
						OnFetchedCollections(collections.ToList());
					},
					(error) =>
					{
						isFetching = false;
						OnError(error);
					});
			}
		}

		// Once the collections are fetched from the server, set our listView adapter so that the collections appear on screen.
		private void OnFetchedCollections(List<Collection> collections)
		{
			RunOnUiThread(() =>
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
			});
		}
	}
}