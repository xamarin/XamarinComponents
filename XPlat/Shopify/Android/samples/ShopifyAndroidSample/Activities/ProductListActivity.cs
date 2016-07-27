using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Shopify.Buy.Model;
using Shopify.Buy.UI;
using Square.Retrofit;
using Square.Retrofit.Client;

using ShopifyAndroidSample.Activities.Base;
using ShopifyAndroidSample.Dialogs;

namespace ShopifyAndroidSample.Activities
{
	// This activity allows the user to select a product to purchase from a list of all products in a collection.
	[Activity]
	public class ProductListActivity : SampleListActivity
	{
		public const string ExtraCollectionId = "ProductListActivity.EXTRA_COLLECTION_ID";

		private string collectionId;
		private ProductDetailsTheme theme;
		private bool useProductDetailsActivity;
		private View accentColorView;
		private View productViewOptionsContainer;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState, Resource.Layout.product_list_activity);

			SetTitle(Resource.String.choose_product);

			useProductDetailsActivity = false;
			theme = new ProductDetailsTheme(Resources);

			if (Intent.HasExtra(ExtraCollectionId))
			{
				collectionId = Intent.GetStringExtra(ExtraCollectionId);
			}

			productViewOptionsContainer = FindViewById(Resource.Id.product_view_options_container);
			productViewOptionsContainer.Visibility = ViewStates.Gone;

			FindViewById<Switch>(Resource.Id.product_details_activity_switch).CheckedChange += (sender, e) =>
			{
				useProductDetailsActivity = e.IsChecked;
				productViewOptionsContainer.Visibility = e.IsChecked ? ViewStates.Visible : ViewStates.Gone;
			};

			FindViewById<Switch>(Resource.Id.theme_style_switch).CheckedChange += (sender, e) =>
			{
				theme.SetStyle(e.IsChecked ? ProductDetailsTheme.Style.Light : ProductDetailsTheme.Style.Dark);
			};

			FindViewById<Switch>(Resource.Id.product_image_bg_switch).CheckedChange += (sender, e) =>
			{
				theme.SetShowProductImageBackground(e.IsChecked);
			};

			accentColorView = FindViewById(Resource.Id.accent_color_view);
			accentColorView.SetBackgroundColor(new Color(theme.AccentColor));
			accentColorView.Click += delegate
			{
				var cpd = new HSVColorPickerDialog(this, new Color(theme.AccentColor), (color) =>
				{
					theme.AccentColor = color;
					accentColorView.SetBackgroundColor(color);
				});
				cpd.SetTitle(Resource.String.choose_accent_color);
				cpd.Show();
			};
		}

		protected override void OnResume()
		{
			base.OnResume();

			// If we haven't already loaded the products from the store, do it now
			if (listView.Adapter == null && !isFetching)
			{
				isFetching = true;
				ShowLoadingDialog(Resource.String.loading_data);

				var success = new Action<IEnumerable<Product>, Response>((products, response) =>
				{
					isFetching = false;
					DismissLoadingDialog();
					OnFetchedProducts(products.ToList());
				});

				var failure = new Action<RetrofitError>(error =>
				{
					isFetching = false;
					OnError(error);
				});

				if (collectionId != null)
				{
					SampleApplication.GetProducts(collectionId, success, failure);
				}
				else {
					SampleApplication.GetAllProducts(success, failure);
				}
			}
		}

		// Once the products are fetched from the server, set our listView adapter so that the products appear on screen.
		private void OnFetchedProducts(List<Product> products)
		{
			RunOnUiThread(() =>
			{
				var productTitles = products.Select(p => p.Title).ToArray();

				listView.Adapter = new ArrayAdapter(this, Resource.Layout.simple_list_item, productTitles);
				listView.ItemClick += (sender, e) =>
				{
					if (useProductDetailsActivity)
					{
						LaunchProductDetailsActivity(products[e.Position]);
					}
					else {
						CreateCheckout(products[e.Position]);
					}
				};
			});
		}

		private void LaunchProductDetailsActivity(Product product)
		{
			SampleApplication.LaunchProductDetailsActivity(this, product, theme);
		}

		// When the user selects a product, create a new checkout for that product.
		private void CreateCheckout(Product product)
		{
			ShowLoadingDialog(Resource.String.syncing_data);

			SampleApplication.CreateCheckout(product, (checkout, response) =>
			{
				DismissLoadingDialog();

				// If the selected product requires shipping, show the list of shipping rates so the user can pick one.
				// Otherwise, skip to the discounts activity (gift card codes and discount codes).
				if (checkout.RequiresShipping)
				{
					StartActivity(new Intent(this, typeof(ShippingRateListActivity)));
				}
				else {

					StartActivity(new Intent(this, typeof(DiscountActivity)));
				}
			}, OnError);
		}

	}
}
