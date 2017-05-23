using System;
using Android.Content;
using Com.Android.Vending.Billing;
using Android.Util;

namespace InAppService
{
	public class InAppBillingServiceConnection: Java.Lang.Object, IServiceConnection
	{
		public InAppBillingServiceConnection (Context context, Action<bool> setupFinished)
		{
			_context = context;
			_setupFinished = setupFinished;
		}

		public IInAppBillingService Service {
			get;
			private set;
		} 

		#region IServiceConnection implementation

		public void OnServiceConnected (ComponentName name, Android.OS.IBinder service)
		{
			LogDebug ("Billing service connected.");
			Service = IInAppBillingServiceStub.AsInterface (service);

			string packageName = _context.PackageName;

			try {
				LogDebug ("Checking for in-app billing V3 support");

				int response = Service.IsBillingSupported (Constants.APIVersion, packageName, ItemType.InApp);
				if (response != BillingResult.OK) {
					SetupFinished(false);
				}

				LogDebug("In-app billing version 3 supported for " + packageName);

				// check for v3 subscriptions support
				response = Service.IsBillingSupported(Constants.APIVersion, packageName, ItemType.Subscription);
				if (response == BillingResult.OK) {
					LogDebug("Subscriptions AVAILABLE.");
					SetupFinished(true);
					return;
				}
				else {
					LogDebug("Subscriptions NOT AVAILABLE. Response: " + response);
				}


			} catch (Exception ex) {
				LogDebug (ex.ToString());
				SetupFinished (false);
			}

			SetupFinished (false);
		}

		public void OnServiceDisconnected (ComponentName name)
		{
			Service = null;
		}

		void SetupFinished(bool isServiceCreated)
		{
			if (_setupFinished != null) {
				_setupFinished (isServiceCreated);
			}
		}

		#endregion


		void LogDebug (String msg)
		{
			Log.Debug (Tag, msg);
		}

		void LogError (String msg)
		{
			Log.Error (Tag, "In-app billing error: " + msg);
		}

		void LogWarn (String msg)
		{
			Log.Warn (Tag, "In-app billing warning: " + msg);
		}

		Action<bool> _setupFinished;
		Context _context;

		const string Tag = "Iab Helper";

	}
}

