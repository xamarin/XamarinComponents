using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using EstimoteSdk;

using Java.Util.Concurrent;

namespace Estimotes.Droid
{
    [Activity(Label = "NotifyDemoActivity")]
    public class NotifyDemoActivity : Activity, BeaconManager.IServiceReadyCallback
    {
        static readonly int NOTIFICATION_ID = 123321;
        BeaconManager _beaconManager;
        NotificationManager _notificationManager;
        Region _region;

        public void OnServiceReady()
        {
            _beaconManager.StartMonitoring(_region);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.notify_demo);

            _region = this.GetBeaconAndRegion().Item2;

            _notificationManager = (NotificationManager)GetSystemService(NotificationService);
            _beaconManager = new BeaconManager(this);

            // Default values are 5s of scanning and 25s of waiting time to save CPU cycles.
            // In order for this demo to be more responsive and immediate we lower down those values.
            _beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);

            _beaconManager.EnteredRegion += (sender, e) => PostNotification("Entered region");
            _beaconManager.ExitedRegion += (sender, e) => PostNotification("Exited region");
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _notificationManager.Cancel(NOTIFICATION_ID);
            _beaconManager.Connect(this);
        }

        protected override void OnDestroy()
        {
            _notificationManager.Cancel(NOTIFICATION_ID);
            _beaconManager.Disconnect();

            base.OnDestroy();
        }

        void PostNotification(string message)
        {
            Intent notifyIntent = new Intent(this, GetType());
            notifyIntent.SetFlags(ActivityFlags.SingleTop);

            PendingIntent pendingIntent = PendingIntent.GetActivities(this, 0, new[] { notifyIntent }, PendingIntentFlags.UpdateCurrent);

            Notification notification = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.beacon_gray)
                .SetContentTitle("Estimote")
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent)
                .Build();

            notification.Defaults |= NotificationDefaults.Lights;
            notification.Defaults |= NotificationDefaults.Sound;

            _notificationManager.Notify(NOTIFICATION_ID, notification);
            TextView statusTextView = FindViewById<TextView>(Resource.Id.status);
            statusTextView.Text = message;
        }
    }
}
