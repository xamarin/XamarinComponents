
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Sample.Stripe.XamarinAndroid
{
    [Activity(Label = "ActivityCardInputManual")]
    public class ActivityCardInputManual : Activity
    {
        EditText editTextCardNumber = null;
        EditText editTextExpirationYear = null;
        EditText editTextExpirationMonth = null;
        EditText editTextCVC = null;
        Button buttonVerify = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_card_input_manual);

            editTextCardNumber = FindViewById<EditText>(Resource.Id.editTextCardNumber);
            editTextExpirationYear = FindViewById<EditText>(Resource.Id.editTextExpirationYear);
            editTextExpirationMonth = FindViewById<EditText>(Resource.Id.editTextExpirationMonth);
            editTextCVC = FindViewById<EditText>(Resource.Id.editTextCVC);
            buttonVerify = FindViewById<Button>(Resource.Id.buttonVerify);

            buttonVerify.Click += (object sender, EventArgs e) =>
            {
                string card_number = editTextCardNumber.Text;
                string cvc = editTextCVC.Text;
                string year = editTextExpirationYear.Text;
                string month = editTextExpirationMonth.Text;

                this.Verify(card_number, month, year, cvc);

                return;
            };

            return;
        }

        public void Verify(String cardNumber, String cardExpMonth, String cardExpYear, String cardCVC)
        {
            global::Stripe.Android.Model.Card card = null;

            card = new global::Stripe.Android.Model.Card
                                                    (
                                                        cardNumber,
                                                        Java.Lang.Integer.GetInteger(cardExpMonth),
                                                        Java.Lang.Integer.GetInteger(cardExpYear),
                                                        cardCVC
                                                    );

            bool valid_card = card.ValidateCard();
            bool valid_number = card.ValidateNumber();
            bool validate_expiry_date = card.ValidateExpiryDate();
            bool valid_cvc = card.ValidateCVC();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Validity checks");
            sb.AppendLine($"Number       : {card.Number}");
            sb.AppendLine($"Brand        : {card.Brand}");
            sb.AppendLine($"MM/YYYY      : {card.ExpMonth}/{card.ExpYear}");
            sb.AppendLine($"CVC          : {card.CVC}");
            sb.AppendLine($"Number last 4: {card.Last4}");
            sb.AppendLine($"Number  valid: {valid_number}");
            sb.AppendLine($"Date    valid: {validate_expiry_date}");
            sb.AppendLine($"CVC     valid: {valid_cvc}");
            sb.AppendLine($"CARD    valid: {valid_card}");

            Toast.MakeText(this, sb.ToString(), ToastLength.Long).Show();

            return;
        }
    }
}
