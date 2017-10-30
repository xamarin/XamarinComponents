using Foundation;
using System;
using UIKit;
using Stripe.iOS;
using PassKit;
using CoreFoundation;

namespace Stripe.UIExamples
{
	partial class BrowseViewController : UITableViewController, IAddCardViewControllerDelegate, IPaymentMethodsViewControllerDelegate, IShippingAddressViewControllerDelegate
	{
		ThemeViewController themeViewController = new ThemeViewController ();
		MockCustomerContext customerContext = new MockCustomerContext ();

		public BrowseViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Stripe UI Examples";

			TableView.TableFooterView = new UIView ();

			TableView.RowHeight = 60;

			NavigationController.NavigationBar.Translucent = false;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			return Enum.GetValues (typeof (Demo)).Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new UITableViewCell (UITableViewCellStyle.Subtitle, null);

			Demo example = (Demo)indexPath.Row;
			cell.TextLabel.Text = example.Title ();
			cell.DetailTextLabel.Text = example.Detail ();

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			UINavigationController navigationController;
			PaymentConfiguration config;
			tableView.DeselectRow (indexPath, true);

			Demo example = (Demo)indexPath.Row;
			var theme = themeViewController.Theme.GetStripeTheme ();

			switch (example)
			{
				case Demo.PaymentCardTextField:
					var cardFieldViewContoller = new CardFieldViewController ();
					cardFieldViewContoller.Theme = theme;
					navigationController = new UINavigationController (cardFieldViewContoller);
					navigationController.NavigationBar.SetStripeTheme (theme);
					PresentViewController (navigationController, true, null);
					break;

				case Demo.AddCardViewController:
					config = new PaymentConfiguration ();
					config.RequiredBillingAddressFields = BillingAddressFields.Full;
					var viewController = new MockAddCardViewController (config, theme);
					viewController.Delegate = this;
					navigationController = new UINavigationController (viewController);
					navigationController.NavigationBar.SetStripeTheme (theme);
					PresentViewController (navigationController, true, null);
					break;

				case Demo.PaymentMethodsViewController:
					config = new PaymentConfiguration ();
					config.AdditionalPaymentMethods = PaymentMethodType.All;
					config.RequiredBillingAddressFields = BillingAddressFields.None;
					config.AppleMerchantIdentifier = "dummy-merchant-id";
					var paymentMethodsViewController = new PaymentMethodsViewController (config, theme, customerContext, this);
					navigationController = new UINavigationController (paymentMethodsViewController);
					navigationController.NavigationBar.SetStripeTheme (theme);
					PresentViewController (navigationController, true, null);
					break;

				case Demo.ShippingInfoViewController:
					config = new PaymentConfiguration ();
					config.RequiredShippingAddressFields = PKAddressField.PostalAddress;
					var shippingAddressViewController = new ShippingAddressViewController (config, theme, "usd", null, null, null);
					shippingAddressViewController.Delegate = this;
					navigationController = new UINavigationController (shippingAddressViewController);
					navigationController.NavigationBar.SetStripeTheme (theme);
					PresentViewController (navigationController, true, null);
					break;

				case Demo.ChangeTheme:
					navigationController = new UINavigationController (themeViewController);
					PresentViewController (navigationController, true, null);
					break;

				default:
					throw new NotImplementedException ();
			}
		}

		public void AddCardViewControllerCancelled (AddCardViewController addCardViewController)
		{
			DismissViewController (true, null);
		}

		public void AddCardViewControllerCreatedToken (AddCardViewController addCardViewController, Token token, STPErrorBlock completion)
		{
			DismissViewController (true, null);
		}

		public void PaymentMethodsViewControllerFailedToLoad (PaymentMethodsViewController paymentMethodsViewController, NSError error)
		{
			DismissViewController (true, null);
		}

		public void PaymentMethodsViewControllerFinished (PaymentMethodsViewController paymentMethodsViewController)
		{
			paymentMethodsViewController.NavigationController.PopViewController (true);
		}

		public void PaymentMethodsViewControllerCancelled (PaymentMethodsViewController paymentMethodsViewController)
		{
			DismissViewController (true, null);
		}

		public void ShippingAddressViewControllerCancelled (ShippingAddressViewController addressViewController)
		{
			DismissViewController (true, null);
		}

		public void ShippingAddressViewControllerEnteredAddress (ShippingAddressViewController addressViewController, Address address, ShippingMethodsCompletionBlock completion)
		{
			var upsGround = new PKShippingMethod
			{
				Amount = new NSDecimalNumber (0),
				Label = "UPS Ground",
				Detail = "Arrives in 3-5 days",
				Identifier = "ups_ground",
			};

			var upsWorldwide = new PKShippingMethod
			{
				Amount = new NSDecimalNumber (10.99),
				Label = "UPS Worldwide Express",
				Detail = "Arrives in 1-3 days",
				Identifier = "ups_worldwide",
			};

			var fedEx = new PKShippingMethod
			{
				Amount = new NSDecimalNumber (5.99),
				Label = "FedEx",
				Detail = "Arrives tomorrow",
				Identifier = "fedex",
			};

			DispatchQueue.MainQueue.DispatchAfter (new DispatchTime (DispatchTime.Now, new TimeSpan (0, 0, 0, 0, 600)), () =>
			{
				if (address.Country == null || address.Country == "US")
				{
					completion (ShippingStatus.Valid, null, new PKShippingMethod[] { upsGround, fedEx }, fedEx);

				}
				else if (address.Country == "AQ")
				{
					var error = new NSError (new NSString ("ShippingError"), 123,
											 new NSDictionary (NSError.LocalizedDescriptionKey, new NSString ("Invalid Shipping Address"),
															   NSError.LocalizedFailureReasonErrorKey, new NSString ("We can't ship to this country.")));

					completion (ShippingStatus.Invalid, error, null, null);

				}
				else
				{
					fedEx.Amount = new NSDecimalNumber (20.99);

					completion (ShippingStatus.Valid, null, new PKShippingMethod[] { upsWorldwide, fedEx }, fedEx);

				}
			});
		}

		public void ShippingAddressViewControllerFinished (ShippingAddressViewController addressViewController, Address address, PKShippingMethod method)
		{
			customerContext.UpdateCustomer (address, null);
			DismissViewController (true, null);
		}
	}

	enum Demo
	{
		PaymentCardTextField = 0,
		AddCardViewController,
		PaymentMethodsViewController,
		ShippingInfoViewController,
		ChangeTheme,
	}

	static class DemoExtensions
	{
		public static string Title (this Demo This)
		{
			switch (This)
			{
				case Demo.PaymentCardTextField:
					return "Card Field";
				case Demo.AddCardViewController:
					return "Card Form with Billing Address";
				case Demo.PaymentMethodsViewController:
					return "Payment Method Picker";
				case Demo.ShippingInfoViewController:
					return "Shipping Info Form";
				case Demo.ChangeTheme:
					return "Change Theme";

				default:
					throw new NotImplementedException ();
			}
		}

		public static string Detail (this Demo This)
		{
			switch (This)
			{
				case Demo.PaymentCardTextField:
					return "STPPaymentCardTextField";
				case Demo.AddCardViewController:
					return "STPAddCardViewController";
				case Demo.PaymentMethodsViewController:
					return "STPPaymentMethodsViewController";
				case Demo.ShippingInfoViewController:
					return "STPShippingInfoViewController";
				case Demo.ChangeTheme:
					return "";

				default:
					throw new NotImplementedException ();
			}
		}
	}
}