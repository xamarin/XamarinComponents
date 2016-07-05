
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

namespace Estimotes.Droid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]            
    public class EstimoteSamples : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var items = new string []{ "Beacons Discovery", "Nearable Discovery", "Eddystone Discovery" };

            ListAdapter = new ArrayAdapter<string>(this, 
                Android.Resource.Layout.SimpleListItem1, 
                Android.Resource.Id.Text1,
                items);
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            switch (position)
            {
                case 0:
                    StartActivity(typeof(MainActivity));
                    break;
                case 1:
                    StartActivity(typeof(NearableActivity));
                    break;
                case 2:
                    StartActivity(typeof(EddystoneActivity));
                    break;
            }
        }
    }
}

