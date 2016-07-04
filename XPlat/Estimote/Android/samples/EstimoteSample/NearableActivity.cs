
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
using EstimoteSdk;
using System.Linq;

namespace Estimotes.Droid
{
    [Activity(Label = "Nearables")]            
    public class NearableActivity : ListActivity, BeaconManager.IServiceReadyCallback
    {
        IMenuItem refreshItem;
        BeaconManager beaconManager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            beaconManager = new BeaconManager(this);
            beaconManager.Nearable += (sender, e) => 
                {
                    if(e.Nearables.Count == 0)
                        return;
                    
                    RunOnUiThread(()=>
                        {
                            var items = e.Nearables.Select(n => "Id: " + n.Identifier + "Proximity: " + Utils.ComputeProximity(n));
                            ListAdapter = new ArrayAdapter<string>(this, 
                                Android.Resource.Layout.SimpleListItem1, 
                                Android.Resource.Id.Text1, 
                                items.ToArray());
                          

                               
                            ActionBar.Subtitle = string.Format("Found {0} nearables.", items.Count());
                        });


                };



        }

        private void Stop()
        {
            if (!isScanning)
                return;
            
            isScanning = false;

            beaconManager.StopNearableDiscovery(scanId);
            refreshItem.SetActionView(null);
            refreshItem.SetIcon(Resource.Drawable.ic_refresh);
        }

        protected override void OnStop()
        {
            base.OnStop();
            Stop();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.scan_menu, menu);
            refreshItem = menu.FindItem(Resource.Id.refresh);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
                case Resource.Id.refresh:
                    LookForNearables();
                    return true;
                case Resource.Id.stop:
                    Stop();
                    return true;

            }
            return base.OnOptionsItemSelected(item);
        }


        string scanId;
        bool isScanning;
        private void LookForNearables()
        {
            if (isScanning)
                return;

            isScanning = true;

            beaconManager.Connect(this);
            refreshItem.SetActionView(Resource.Layout.actionbar_indeterminate_progress);
            
        }

        public void OnServiceReady()
        {
            scanId = beaconManager.StartNearableDiscovery();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            beaconManager.Disconnect();
        }
    }
}

