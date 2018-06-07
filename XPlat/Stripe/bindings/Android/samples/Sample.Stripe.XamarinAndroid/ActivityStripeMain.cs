
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
    [Activity(Label = "ActivityStripeMain", MainLauncher = true, Icon = "@mipmap/icon")]
    public class ActivityStripeMain : Activity
    {
        Button buttonCardInputManual = null;
        Button buttonCardInputWidget = null;
        Button buttonCardInputMultilineWidget = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView (Resource.Layout.layout_stripe_main);

            buttonCardInputManual = FindViewById<Button>(Resource.Id.buttonCardInputManual);
            buttonCardInputWidget = FindViewById<Button>(Resource.Id.buttonCardInputWidget);
            buttonCardInputMultilineWidget = FindViewById<Button>(Resource.Id.buttonCardInputMultilineWidget);


            buttonCardInputManual.Click += delegate 
            {
                this.StartActivity(typeof(ActivityCardInputManual));
            };

            buttonCardInputWidget.Click += delegate 
            {
                this.StartActivity(typeof(ActivityCardInputWidget));
            };

            buttonCardInputMultilineWidget.Click += delegate
            {
                this.StartActivity(typeof(ActivityCardInputMultilineWidget));
            };

            return;
        }
    }
}
