
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
    [Activity(Label = "ActivityCardInputMultilineWidget")]
    public class ActivityCardInputMultilineWidget : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_card_input_widget);

            return;
        }
    }
}
