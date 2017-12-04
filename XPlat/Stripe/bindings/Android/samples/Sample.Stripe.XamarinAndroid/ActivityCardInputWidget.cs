
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
using Stripe.Android.View;

namespace Sample.Stripe.XamarinAndroid
{
    [Activity(Label = "ActivityCardInputWidget")]
    public class ActivityCardInputWidget : Activity
    {
        CardInputWidget mCardInputWidget = null;
        Button buttonVerify = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_card_input_widget);

            mCardInputWidget = FindViewById<CardInputWidget>(Resource.Id.card_input_widget);
            buttonVerify = FindViewById<Button>(Resource.Id.buttonVerify);

            buttonVerify.Click += ButtonVerify_Click;

            return;
        }

        void ButtonVerify_Click(object sender, EventArgs e)
        {
            global::Stripe.Android.Model.Card card = mCardInputWidget.Card;
            if (card == null)
            {
                // Do not continue token creation.
            }
            else
            {
                global::Stripe.Android.Stripe stripe = null;
                stripe = new global::Stripe.Android.Stripe(this, "pk_test_6pRNASCoBOKtIshFeQd4XMUh");
                stripe.CreateToken
                            (
                                card,
                                new TokenCallbackImplementation(this)
                            );

            }

            return;
        }
    }
}
