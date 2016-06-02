using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using BlurBehindSdk;
using System;

namespace BlurBehindSample
{
    [Activity (Label = "BlurBehind Sample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            SetContentView (Resource.Layout.Main);

            var dummyButton = FindViewById<Button> (Resource.Id.dummy_button);

            dummyButton.Click += async (sender, e) => {

                // Perform the blur on this activity
                await BlurBehind.Instance.ExecuteAsync (this);

                // Launch the next activity
                var intent = new Intent (this, typeof (BlurredActivity));
                intent.SetFlags (ActivityFlags.NoAnimation);
                StartActivity (intent);
            };

        }
    }
}


