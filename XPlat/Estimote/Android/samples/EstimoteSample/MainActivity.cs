using System;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

using EstimoteSdk;

namespace Estimotes.Droid
{
    [Activity(Label="Beacons")]
    public class MainActivity : Activity
    {
        const int REQUEST_ENABLE_BLUETOOTH = 123321;
        const int QUICKACTION_DISTANCEDEMO = 1;
        const int QUICKACTION_NOTIFYDEMO = 2;
        static readonly String Tag = typeof(MainActivity).FullName;
        LeDevicesListAdapter _adapter;
        FindAllBeacons _findAllBeacons;
        QuickAction _quickAction;
        IMenuItem _refreshItem;
        Beacon _selectedBeacon;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.main);
            ActionBar.SetHomeButtonEnabled(true);

            _findAllBeacons = new FindAllBeacons(this);
            _findAllBeacons.BeaconsFound += NewBeaconsFound;

            InitializeQuickAction();

            InitializeListView();
        }

        void InitializeQuickAction()
        {
            _quickAction = new QuickAction(this, QuickActionLayout.Horizontal);
            ActionItemBuilder builder = _quickAction.GetBuilder();

            builder.SetItemId(QUICKACTION_DISTANCEDEMO)
                   .SetTitle("Distance Demo")
                   .SetIcon(Android.Resource.Drawable.StatSysDataBluetooth)
                   .AddToParent();

            builder.SetItemId(QUICKACTION_NOTIFYDEMO)
                   .SetTitle("Notify Demo")
                   .SetIcon(Android.Resource.Drawable.StatNotifyError)
                   .AddToParent();

            _quickAction.ActionItemClicked += HandleActionItemClicked;
        }

        void InitializeListView()
        {
            _adapter = new LeDevicesListAdapter(this);
            ListView list = FindViewById<ListView>(Resource.Id.device_list);
            list.Adapter = _adapter;
            list.ItemClick += (sender, e) =>{
                                  _selectedBeacon = _adapter[e.Position];
                                  View imgView = e.View.FindViewWithTag("beacon_image");
                                  _quickAction.Show(imgView);
                              };
        }

        void HandleActionItemClicked(object sender, ActionItemClickEventArgs e)
        {
            switch (e.ActionItem.ActionId)
            {
                case QUICKACTION_DISTANCEDEMO:
                    this.StartActivityForBeacon<DistanceBeaconActivity>(_selectedBeacon);
                    break;
                case QUICKACTION_NOTIFYDEMO:
                    this.StartActivityForBeacon<NotifyDemoActivity>(_selectedBeacon);
                    break;
                default:
                    Log.Wtf(Tag, "Don't know how to handle the ActionItem {0}.", e.ActionItem.Title);
                    break;
            }
            _selectedBeacon = null;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.scan_menu, menu);
            _refreshItem = menu.FindItem(Resource.Id.refresh);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            if (item.ItemId == Resource.Id.refresh)
            {
                LookForBeacons();
                return true;
            }
            if (item.ItemId == Resource.Id.stop)
            {
                _findAllBeacons.Stop();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStart()
        {
            base.OnStart();
            LookForBeacons();
        }

        protected override void OnPause()
        {
            _selectedBeacon = null;
            base.OnPause();
        }

        protected override void OnStop()
        {
            try
            {
                _findAllBeacons.Stop();
            }
            catch (RemoteException)
            {
                Log.Debug(Tag, "Error while stopping ranging");
            }

            base.OnStop();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == REQUEST_ENABLE_BLUETOOTH)
            {
                if (resultCode == Result.Ok)
                {
                    ActionBar.Subtitle = "Scanning...";
                    _adapter.Update(new Beacon[0]);
                    LookForBeacons();
                }
                else
                {
                    Toast.MakeText(this, "Bluetooth not enabled.", ToastLength.Long).Show();
                    ActionBar.Subtitle = "Bluetooth not enabled.";
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        void LookForBeacons()
        {
            if (!_findAllBeacons.IsBluetoothEnabled)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableBtIntent, REQUEST_ENABLE_BLUETOOTH);
            }
            else
            {
                if (_refreshItem != null)
                {
                    _refreshItem.SetActionView(Resource.Layout.actionbar_indeterminate_progress);
                }
                _findAllBeacons.FindBeacons(this);
            }
        }

        void NewBeaconsFound(object sender, BeaconsFoundEventArgs e)
        {
            _adapter.Update(e.Beacons);
            _findAllBeacons.Stop();
            RunOnUiThread(() =>{
                              if (_refreshItem != null)
                              {
                                  _refreshItem.SetActionView(null);
                                  _refreshItem.SetIcon(Resource.Drawable.ic_refresh);
                              }
                          });
            ActionBar.Subtitle = string.Format("Found {0} beacons.", _adapter.Count);
        }
    }
}
