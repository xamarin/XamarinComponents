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
using CoreGraphics;
using Foundation;
using UIKit;

using Shopify;

namespace ShopifyiOSSample
{
	public class ProductListViewController : UITableViewController, IUIViewControllerPreviewingDelegate
	{
		private readonly BUYClient client;
		private readonly BUYCollection collection;

		private BUYProduct[] products;
		private NSUrlSessionDataTask collectionTask;
		private NSUrlSessionDataTask checkoutCreationTask;

		private bool demoProductViewController;
		private BUYThemeStyle themeStyle;
		private UIColor[] themeTintColors;
		private nint themeTintColorSelectedIndex;
		private bool showsProductImageBackground;
		private bool presentViewController;

		private static BUYAddress Address {
			get {
				var address = new BUYAddress ();
				address.Address1 = "150 Elgin Street";
				address.Address2 = "8th Floor";
				address.City = "Ottawa";
				address.Company = "Shopify Inc.";
				address.FirstName = "Egon";
				address.LastName = "Spengler";
				address.Phone = "1-555-555-5555";
				address.CountryCode = "CA";
				address.ProvinceCode = "ON";
				address.Zip = "K1N5T5";
				return address;
			}
		}

		public ProductListViewController (BUYClient client, BUYCollection collection)
			: base (UITableViewStyle.Grouped)
		{
			this.client = client;
			this.collection = collection;
		}

		protected override void Dispose (bool disposing)
		{
			base.Dispose (disposing);

			if (checkoutCreationTask != null) {
				checkoutCreationTask.Cancel ();
			}
			if (collectionTask != null) {
				collectionTask.Cancel ();
			}
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			if (collection != null) {
				Title = collection.Title;
			} else {
				Title = "All Products";
			}

			TableView.RegisterClassForCellReuse (typeof(UITableViewCell), "Cell");
			TableView.RegisterClassForCellReuse (typeof(ProductViewControllerToggleTableViewCell), "ProductViewControllerToggleCell");
			TableView.RegisterClassForCellReuse (typeof(ProductViewControllerThemeStyleTableViewCell), "ThemeStyleCell");
			TableView.RegisterClassForCellReuse (typeof(ProductViewControllerThemeTintColorTableViewCell), "ThemeTintColorCell");
			TableView.RegisterClassForCellReuse (typeof(ProductViewControllerToggleTableViewCell), "ThemeShowsBackgroundToggleCell");
			TableView.RegisterClassForCellReuse (typeof(ProductViewControllerToggleTableViewCell), "ProductViewControllerPresentViewControllerToggleCell");

			themeTintColors = new [] {
				UIColor.FromRGBA (0.48f, 0.71f, 0.36f, 1.0f), 
				UIColor.FromRGBA (0.88f, 0.06f, 0.05f, 1.0f),
				UIColor.FromRGBA (0.02f, 0.54f, 1.0f, 1.0f)
			};
			themeTintColorSelectedIndex = 0;
			showsProductImageBackground = true;
			presentViewController = true;

			if (collection != null) {
				// If we're presenting with a collection, add the ability to sort
				UIBarButtonItem sortBarButtonItem = new UIBarButtonItem ("Sort", UIBarButtonItemStyle.Plain, PresentCollectionSortOptions);
				NavigationItem.RightBarButtonItem = sortBarButtonItem;

				GetCollection (BUYCollectionSort.CollectionDefault);
			} else {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
				client.GetProductsPage (1, (products, page, reachedEnd, error) => {
					UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

					if (error == null && products != null) {
						this.products = products;
						TableView.ReloadData ();
					} else {
						Console.WriteLine ("Error fetching products: {0}", error);
					}
				});
			}
		}

		private void PresentCollectionSortOptions (object sender, EventArgs e)
		{
			UIAlertController alertController = UIAlertController.Create ("Collection Sort", null, UIAlertControllerStyle.ActionSheet);

			alertController.AddAction (UIAlertAction.Create ("Default", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.CollectionDefault);
			}));
			alertController.AddAction (UIAlertAction.Create ("Best Selling", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.BestSelling);
			}));
			alertController.AddAction (UIAlertAction.Create ("Title - Ascending", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.TitleAscending);
			}));
			alertController.AddAction (UIAlertAction.Create ("Title - Descending", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.TitleDescending);
			}));
			alertController.AddAction (UIAlertAction.Create ("Price - Ascending", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.PriceAscending);
			}));
			alertController.AddAction (UIAlertAction.Create ("Price - Descending", UIAlertActionStyle.Default, delegate {
				GetCollection (BUYCollectionSort.PriceDescending);
			}));
		    
			// Note: The BUYCollectionSort.CreatedAscending and BUYCollectionSort.CreatedDescending are currently not supported

			if (TraitCollection.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
				UIPopoverPresentationController popPresenter = alertController.PopoverPresentationController;
				popPresenter.BarButtonItem = (UIBarButtonItem)sender;
			}
		    
			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
		    
			PresentViewController (alertController, true, null);
		}

		private void GetCollection (BUYCollectionSort collectionSort)
		{
			if (collectionTask != null) {
				collectionTask.Cancel ();
			}

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			collectionTask = client.GetProductsPage (1, collection.CollectionId, collectionSort, (products, page, reachedEnd, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				if (error == null && products != null) {
					this.products = products;
					TableView.ReloadData ();
				} else {
					Console.WriteLine ("Error fetching products: {0}", error);
				}
			});
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 2;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			switch (section) {
			case 0:
				return demoProductViewController ? 5 : 1;
			case 1:
				return products == null ? 0 : products.Length;
			default:
				return 0;
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = null;

			switch (indexPath.Section) {
			case 0:
				switch (indexPath.Row) {
				case 0:
					{
						var toggleCell = (ProductViewControllerToggleTableViewCell)tableView.DequeueReusableCell ("ProductViewControllerToggleCell", indexPath);
						toggleCell.ToggleSwitch.ValueChanged -= ToggleProductViewControllerDemo;
							
						toggleCell.TextLabel.Text = "Demo BUYProductViewController";
						toggleCell.ToggleSwitch.On = demoProductViewController;
						toggleCell.ToggleSwitch.ValueChanged += ToggleProductViewControllerDemo;

						cell = toggleCell;
					}
					break;
				case 1:
					{
						var themeCell = (ProductViewControllerThemeStyleTableViewCell)TableView.DequeueReusableCell ("ThemeStyleCell", indexPath);
						themeCell.SegmentedControl.ValueChanged -= ToggleProductViewControllerThemeStyle;

						themeCell.SegmentedControl.SelectedSegment = (int)themeStyle;
						themeCell.SegmentedControl.ValueChanged += ToggleProductViewControllerThemeStyle;

						cell = themeCell;
					}
					break;
				case 2:
					{
						var themeCell = (ProductViewControllerThemeTintColorTableViewCell)TableView.DequeueReusableCell ("ThemeTintColorCell", indexPath);
						themeCell.SegmentedControl.ValueChanged -= ToggleProductViewControllerTintColorSelection;

						themeCell.SegmentedControl.SelectedSegment = themeTintColorSelectedIndex;
						themeCell.SegmentedControl.ValueChanged += ToggleProductViewControllerTintColorSelection;

						cell = themeCell;
					}
					break;
				case 3:
					{
						var toggleCell = (ProductViewControllerToggleTableViewCell)TableView.DequeueReusableCell ("ThemeShowsBackgroundToggleCell", indexPath);
						toggleCell.ToggleSwitch.ValueChanged -= ToggleShowsProductImageBackground;

						toggleCell.TextLabel.Text = "Product Image in Background";
						toggleCell.ToggleSwitch.On = showsProductImageBackground;
						toggleCell.ToggleSwitch.ValueChanged += ToggleShowsProductImageBackground;

						cell = toggleCell;
					}
					break;
				case 4:
					{
						var toggleCell = (ProductViewControllerToggleTableViewCell)TableView.DequeueReusableCell ("ProductViewControllerPresentViewControllerToggleCell", indexPath);
						toggleCell.ToggleSwitch.ValueChanged -= TogglePresentViewController;

						toggleCell.TextLabel.Text = "Modal Presentation";
						toggleCell.ToggleSwitch.On = presentViewController;
						toggleCell.ToggleSwitch.ValueChanged -= TogglePresentViewController;

						cell = toggleCell;
					}
					break;
				}
				break;
			case 1:
				{
					cell = TableView.DequeueReusableCell ("Cell", indexPath);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
					var product = products [indexPath.LongRow];
					cell.TextLabel.Text = product.Title;
				}
				break;
			}

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section > 0) {
				var product = products [indexPath.Row];
				if (demoProductViewController) {
					TableView.DeselectRow (indexPath, true);
					DemoProductViewControllerWithProduct (product);
				} else {
					DemoNativeFlowWithProduct (product);
				}
			}
		}

		private void DemoNativeFlowWithProduct (BUYProduct product)
		{
			if (checkoutCreationTask != null && checkoutCreationTask.State == NSUrlSessionTaskState.Running) {
				checkoutCreationTask.Cancel ();
			}

			var cart = new BUYCart ();
			cart.AddVariant (product.Variants [0]);

			var checkout = new BUYCheckout (cart);

			// Apply billing and shipping address, as well as email to the checkout
			checkout.ShippingAddress = Address;
			checkout.BillingAddress = Address;
			checkout.Email = "banana@testasaurus.com";

			client.UrlScheme = "xamarinsample://";

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			checkoutCreationTask = client.CreateCheckout (checkout, (chkout, error) => {
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				if (error == null && chkout != null) {
					var shippingController = new ShippingRatesTableViewController (client, chkout);
					NavigationController.PushViewController (shippingController, true);
				} else {
					Console.WriteLine ("Error creating checkout: {0}", error);
				}
			});
		}

		private void DemoProductViewControllerWithProduct (BUYProduct product)
		{
			var productViewController = GetProductViewController ();
			productViewController.LoadWithProduct (product, (success, error) => {
				if (error == null) {
					if (presentViewController) {
						productViewController.PresentPortraitInViewController (this);
					} else {
						NavigationController.PushViewController (productViewController, true);
					}
				}
			});
		}

		private BUYProductViewController GetProductViewController ()
		{
			var theme = new BUYTheme ();
			theme.Style = themeStyle;
			theme.TintColor = themeTintColors [themeTintColorSelectedIndex];
			theme.ShowsProductImageBackground = showsProductImageBackground;

			var productViewController = new BUYProductViewController (client, theme);
			productViewController.MerchantId = AppDelegate.MERCHANT_ID;
			return productViewController;
		}

		private void ToggleProductViewControllerDemo (object sender, EventArgs e)
		{
			demoProductViewController = ((UISwitch)sender).On;

			TableView.ReloadSections (NSIndexSet.FromIndex (0), UITableViewRowAnimation.Automatic);

//			// Add 3D Touch peek and pop for product previewing
//			if (demoProductViewController == true && [[UITraitCollection class] respondsToSelector:@selector(traitCollectionWithForceTouchCapability:)] && self.traitCollection.forceTouchCapability == UIForceTouchCapabilityAvailable) {
//				[self registerForPreviewingWithDelegate:self sourceView:self.tableView];
//			} else if ([[UITraitCollection class] respondsToSelector:@selector(traitCollectionWithForceTouchCapability:)] && self.traitCollection.forceTouchCapability == UIForceTouchCapabilityAvailable) {
//				[self registerForPreviewingWithDelegate:self sourceView:self.tableView];
//			}
		}

		private void ToggleProductViewControllerThemeStyle (object sender, EventArgs e)
		{
			themeStyle = (BUYThemeStyle)(int)((UISegmentedControl)sender).SelectedSegment;
		}

		private void ToggleProductViewControllerTintColorSelection (object sender, EventArgs e)
		{
			themeTintColorSelectedIndex = ((UISegmentedControl)sender).SelectedSegment;
		}

		private void ToggleShowsProductImageBackground (object sender, EventArgs e)
		{
			showsProductImageBackground = ((UISwitch)sender).On;
		}

		private void TogglePresentViewController (object sender, EventArgs e)
		{
			presentViewController = ((UISwitch)sender).On;
		}

		public UIViewController GetViewControllerForPreview (IUIViewControllerPreviewing previewingContext, CGPoint location)
		{
			var indexPath = TableView.IndexPathForRowAtPoint (location);
			var cell = TableView.CellAt (indexPath);
			if (cell == null || demoProductViewController == false) {
				return null;
			}

			var product = products [indexPath.Row];
			var productViewController = GetProductViewController ();
			productViewController.LoadWithProduct (product, null);

			previewingContext.SourceRect = cell.Frame;

			return productViewController;
		}

		public void CommitViewController (IUIViewControllerPreviewing previewingContext, UIViewController viewControllerToCommit)
		{
			if (presentViewController) {
				PresentViewController (viewControllerToCommit, true, null);
			} else {
				NavigationController.PushViewController (viewControllerToCommit, true);
			}
		}
	}
}
