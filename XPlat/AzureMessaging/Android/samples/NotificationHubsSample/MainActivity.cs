using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using WindowsAzure.Messaging;


[assembly: Permission (Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")] //, ProtectionLevel = Android.Content.PM.Protection.Signature)]
[assembly: UsesPermission (Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission (Name = "com.google.android.c2dm.permission.RECEIVE")]

//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission (Android.Manifest.Permission.GetAccounts)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission (Android.Manifest.Permission.ReceiveBootCompleted)]

namespace AzureMessagingSampleAndroid
{
    [Activity (Label = "Notification Hubs Sample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        TextView textViewMsg;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            textViewMsg = FindViewById<TextView> (Resource.Id.textViewMsg);

            #region Check to ensure user has used their own GCM Service, Hub Name, and Hub Secret
            if (MyRegistrationService.GCM_SENDER_ID == "YOUR-SENDER-ID") {
                Log ("You must change the SampleGcmBroadcastReceiver.SENDER_IDS to your own Sender ID for the sample to work!");
                return;
            }

            if (MyRegistrationService.HUB_NAME == "YOUR-HUB-NAME") {
                Log ("You must enter your own Hub Name and Hub Listen Secret for the sample to work!");
                return;
            }
            #endregion

            Log ("Registering for Remote Notifications...");

            MyRegistrationService.Register (this);
        }

        protected override void OnStart ()
        {
            base.OnStart ();

            MyRegistrationService.TokenRefreshed += TokenUpdated;
        }

        protected override void OnStop ()
        {
            MyRegistrationService.TokenRefreshed -= TokenUpdated;

            base.OnStop ();
        }

        void TokenUpdated (string token)
        {
            Log ("GCM Token: {0}", token);
        }

        void Log (string format, params object[] args)
        {
            Android.Util.Log.Debug (MyRegistrationService.TAG, format, args);
            textViewMsg.Text += string.Format (format, args) + "\r\n";
        }
    }

    [Service]
    public class MyRegistrationService : IntentService
    {
        static NotificationHub hub;

        public const string TAG = "AZURE-NOTIFICATION-HUBS";

        // TODO: Customize these values to your own
        public const string GCM_SENDER_ID = "YOUR-SENDER-ID";
        public const string HUB_NAME = "YOUR-HUB-NAME";
        public const string HUB_LISTEN_SECRET = "YOUR-HUB-LISTEN-SECRET";

        public static event Action<string> TokenRefreshed;

        static void Init (Context context)
        {
            if (hub == null) {
                var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess (
                    new Java.Net.URI ("sb://" + HUB_NAME + "-ns.servicebus.windows.net/"),
                    HUB_LISTEN_SECRET);

                hub = new NotificationHub (HUB_NAME, cs, context);
            }
        }

        public static void Register (Context context)
        {
            Init (context);

            context.StartService (new Intent (context, typeof (MyRegistrationService)));
        }

        public static void Unregister (Context context)
        {
            Init (context);

            hub.Unregister ();
        }

        protected override void OnHandleIntent (Intent intent)
        {
            // Get the new token and send to the server
            var instanceID = InstanceID.GetInstance (Application.Context);
            var token = instanceID.GetToken (GCM_SENDER_ID, GoogleCloudMessaging.InstanceIdScope);

            // Register our GCM token with Azure notification hub servers
            if (hub != null) {
                var registration = hub.Register (token, "TEST");

                Android.Util.Log.Debug (TAG, "Azure Registered: {0} -> {1}",
                                        registration.PNSHandle, registration.NotificationHubPath);
            }

            // Fire the event for any UI subscribed to it
            TokenRefreshed?.Invoke (token);

            Android.Util.Log.Debug (TAG, "GCM OnTokenRefresh: {0}", token);
        }
    }

    [Service (Exported = false)]
    [IntentFilter (new [] { InstanceID.IntentFilterAction })]
    public class MyInstanceIDListenerService : InstanceIDListenerService
    {
        public override void OnTokenRefresh ()
        {
            MyRegistrationService.Register (this);
        }
    }

    [Service (Exported = false)]
    [IntentFilter (new [] { GoogleCloudMessaging.IntentFilterActionReceive })]
    public class MyGcmListenerService : GcmListenerService
    {
        public override void OnMessageReceived (string from, Bundle data)
        {
            //Push Notification arrived - print out the keys/values
            if (data != null) {
                foreach (var key in data.KeySet ())
                    Android.Util.Log.Debug (MyRegistrationService.TAG,
                                            "Key: {0}, Value: {1}", key, data.GetString (key));
            }

            /**
             * Production applications would usually process the message here.
             * Eg: - Syncing with server.
             *     - Store message in local database.
             *     - Update UI.
             */

            /**
            * In some cases it may be useful to show a notification indicating to the user
            * that a message was received.
            */
        }
    }
}

namespace Android.Gms.Gcm
{
    [BroadcastReceiver (
        Name = "com.google.android.gms.gcm.GcmReceiver",
        Exported = true,
        Permission = "com.google.android.c2dm.permission.SEND")]
    [IntentFilter (new [] { "com.google.android.c2dm.intent.RECEIVE", "com.google.android.c2dm.intent.REGISTRATION" },
        Categories = new [] { "@PACKAGE_NAME@" })]
    partial class GcmReceiver { }
}


