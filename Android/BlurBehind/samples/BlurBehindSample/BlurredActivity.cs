
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
using BlurBehindSdk;
using Android.Graphics;

namespace BlurBehindSample
{
    [Activity (Label = "Blurred")]
    public class BlurredActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView(Resource.Layout.Blurred);

            BlurBehind.Instance
                .WithAlpha (80)
                .WithFilterColor (Color.ParseColor ("#0075c0"))
                .SetBackground (this);
        }
    }
}

