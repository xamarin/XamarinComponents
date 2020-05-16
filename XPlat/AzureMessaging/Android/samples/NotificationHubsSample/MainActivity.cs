using Android.App;
using Android.Widget;
using Android.OS;
using WindowsAzure.Messaging.NotificationHubs;
using Android.Content;

namespace AzureMessagingSampleAndroid
{

    [Activity(Label = "Notification Hubs Sample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView textViewMsg;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            textViewMsg = FindViewById<TextView>(Resource.Id.textViewMsg);

            // TODO: Customize these values to your own
            const string CONNECTION_STRING = "YOUR-HUB-CONNECTION-STRING";
            const string HUB_NAME = "YOUR-HUB-NAME";

            #region Check to ensure user has used their own Connection String and Hub Name
            if (CONNECTION_STRING == "YOUR-HUB-CONNECTION-STRING")
            {
                Log("You must change the CONNECTION_STRING to your own for the sample to work!");
                return;
            }

            if (HUB_NAME == "YOUR-HUB-NAME")
            {
                Log("You must enter your own Hub Name for the sample to work!");
                return;
            }
            #endregion

            Log("Registering for Remote Notifications...");
            NotificationHub.Initialize(Application, HUB_NAME, CONNECTION_STRING);

            Log("Device Token: {0}", NotificationHub.PushChannel);

            NotificationHub.SetListener(new NotificationListener());

        }

        void Log(string format, params object[] args)
        {
            Android.Util.Log.Debug("AZURE-NOTIFICATION-HUBS", format, args);
            textViewMsg.Text += string.Format(format, args) + "\r\n";
        }
    }

    public partial class NotificationListener : Java.Lang.Object, INotificationListener
    {
        void INotificationListener.OnPushNotificationReceived(Context context, INotificationMessage message)
        {
            //Push Notification arrived - print out the keys/values
            var data = message.Data;
            if (data != null)
            {
                foreach (var entity in data)
                {
                    Android.Util.Log.Debug("AZURE-NOTIFICATION-HUBS", "Key: {0}, Value: {1}", entity.Key, entity.Value);
                }
            }
        }
    }
}