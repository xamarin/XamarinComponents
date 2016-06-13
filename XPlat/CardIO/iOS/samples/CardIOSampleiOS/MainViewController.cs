using System;
using MonoTouch.Dialog;
#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif
using Card.IO;

namespace CardIOSampleiOS
{
    public class MainViewController : DialogViewController, ICardIOPaymentViewControllerDelegate
	{
		public MainViewController () : base(UITableViewStyle.Plain, new RootElement("card.io"), false)
		{
		}

        CardIOPaymentViewController paymentViewController;

		StyledStringElement elemCardNumber;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            	
			elemCardNumber = new StyledStringElement ("xxxx xxxx xxxx xxxx");

			Root = new RootElement ("card.io") {
				new Section {
					elemCardNumber,
					new StyledStringElement("Enter your Credit Card", () => {
                        paymentViewController = new CardIOPaymentViewController (this);

						NavigationController.PresentViewController(paymentViewController, true, null);
					}) { Accessory = UITableViewCellAccessory.DisclosureIndicator }
				}
			};
		}

        public void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
        {
        	paymentViewController.DismissViewController(true, null);
        }
        public void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
        {
            if (cardInfo == null) {
                elemCardNumber.Caption = "xxxx xxxx xxxx xxxx";
                Console.WriteLine("Cancelled");
            } else {
                elemCardNumber.Caption = cardInfo.CardNumber;
            }

            ReloadData();

            paymentViewController.DismissViewController(true, null);        
        }
	}
}