using System;
using Android.Content;
using Com.Android.Vending.Billing;
using Xamarin.InAppBilling.Utilities;
using Android.App;
using System.Security.Cryptography;
using Android.OS;

namespace Xamarin.InAppBilling
{
	/// <summary>
	/// The <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> class binds your <c>Activity</c> to the Google Play’s In-app Billing 
	/// service to send In-app Billing requests to Google Play from your application. As part of the setup process, 
	/// the <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> also checks if the In-app Billing Version 3 API is supported by Google Play. 
	/// If the API version is not supported, or if an error occured while establishing the service binding, the listener is notified and passed an error message.
	/// </summary>
	public class InAppBillingServiceConnection: Java.Lang.Object, IServiceConnection
	{
		#region Constants
		/// <summary>
		/// The constant identifier from the <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/>
		/// </summary>
		const string Tag = "Iab Helper";
		#endregion

		#region Private Variables
		/// <summary>
		/// The backing store for the activity.
		/// </summary>
		private Activity _activity;

		/// <summary>
		/// The backing store for the public key.
		/// </summary>
		private string _publicKey;
		#endregion

		#region Computed Properties
		/// <summary>
		/// Gets the Google Play <c>InAppBillingService</c> interface.
		/// </summary>
		/// <value>The <c>InAppBillingService</c> attached to this <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> </value>
		public IInAppBillingService Service {
			get;
			private set;
		}

		/// <summary>
		/// Gets the <see cref="Xamarin.InAppBilling.InAppBillingHandler"/> used to communicate with the Google Play Service
		/// </summary>
		/// <value>The billing handler.</value>
		public InAppBillingHandler BillingHandler {
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the Google Play Service public key used for In-App Billing
		/// </summary>
		/// <value>The public key.</value>
		/// <remarks>NOTE: The key will be encrypted when it is stored in memory.</remarks>
		public string PublicKey {
			get { return Crypto.Decrypt (_publicKey, _activity.PackageName); }
			set { _publicKey = Crypto.Encrypt (value, _activity.PackageName); }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> is connected to the
		/// Google Play service
		/// </summary>
		/// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
		public bool Connected {
			get;
			private set;
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.InAppBilling.InAppBillingServiceConnection"/> class.
		/// </summary>
		/// <param name="activity">Activity.</param>
		/// <param name="publicKey">Public key.</param>
		public InAppBillingServiceConnection (Activity activity, string publicKey)
		{
			// Initialize
			_activity = activity;
			PublicKey = publicKey;
		}
		#endregion 

		#region Public Methods
		/// <summary>
		/// Connect this instance to the Google Play service to support In-App Billing in your application
		/// </summary>
		public void Connect ()
		{
			// Attach to the billing service
			var serviceIntent = new Intent ("com.android.vending.billing.InAppBillingService.BIND");
			serviceIntent.SetPackage ("com.android.vending");
			var availableServices = _activity.PackageManager.QueryIntentServices (serviceIntent, 0);

			// Was the service available?
			if (availableServices == null) {
				// No, there was an error attempting to access in the In-App Billing service
				RaiseOnInAppBillingError(InAppBillingErrorType.BillingNotSupported, "Unable to bind with com.android.vending.billing.InAppBillingService API.");
				Connected = false;
			} else {
				// Was the service available?
				if (availableServices.Count != 0) {
					// Yes, bind to the services
					_activity.BindService (serviceIntent, this, Bind.AutoCreate);
				} else {
					// No, there was an error attempting to access in the In-App Billing service
					RaiseOnInAppBillingError(InAppBillingErrorType.BillingNotSupported, "Unable to access the com.android.vending service.");
					Connected = false;
				}
			}
		}

		/// <summary>
		/// Disconnects this instance from the Google Play service.
		/// </summary>
		/// <remarks>Important: Remember to unbind from the In-app Billing service when you are done with your activity. 
		/// If you don’t unbind, the open service connection could cause your device’s performance to degrade. To unbind 
		/// and free your system resources, call the <c>Disconnect</c> method when your Activity gets destroyed.</remarks>
		public void Disconnect ()
		{
			// Drop our connection to the Google Play Service
			_activity.UnbindService (this);
			Connected = false;
		}

		/// <summary>
		/// Raises the service connected event.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="service">Service.</param>
		public void OnServiceConnected (ComponentName name, IBinder service)
		{
			// Attach to the Google Play InAppBillingService interface
			Logger.Debug ("Billing service connected.");
			Service = IInAppBillingServiceStub.AsInterface (service);

			// Get the name of the current package from the activity
			string packageName = _activity.PackageName;

			// Trap all errors
			try {

				// Check to see if Google Play In-App Billing Version 3 is supported on this device
				int response = Service.IsBillingSupported (Billing.APIVersion, packageName, ItemType.Product);
				if (response != BillingResult.OK) {
					// No, inform caller
					Logger.Debug ("In-app billing version 3 NOT supported for {0}", packageName);
					RaiseOnInAppBillingError(InAppBillingErrorType.BillingNotSupported, String.Format("In-app billing version 3 NOT supported for {0}", packageName));
					Connected = false;

					// Stop processing
					return;
				}

				Logger.Debug ("In-app billing version 3 supported for {0}", packageName);

				// Check to see if Google Play In-App Billing Version 3 subscriptions are supported on this device
				response = Service.IsBillingSupported (Billing.APIVersion, packageName, ItemType.Subscription);
				if (response == BillingResult.OK) {
					// Yes, subscriptions are supported inform caller
					Logger.Debug ("Subscriptions AVAILABLE.");
					Connected = true;
					RaiseOnConnected();

					// Stop processing
					return;
				} else {
					// Report error
					Logger.Debug ("Subscriptions NOT AVAILABLE. Response: {0}", response);
					RaiseOnInAppBillingError(InAppBillingErrorType.SubscriptionsNotSupported, String.Format("Subscriptions NOT AVAILABLE. Response: {0}", response));
					Connected = false;
				}

			} catch (Exception ex) {
				// An unknown error has occurred, inform caller
				Logger.Debug (ex.ToString ());
				RaiseOnInAppBillingError (InAppBillingErrorType.UnknownError, ex.ToString ());
				Connected = false;
			}
		}

		/// <summary>
		/// Raises the service disconnected event.
		/// </summary>
		/// <param name="name">Name.</param>
		public void OnServiceDisconnected (ComponentName name)
		{
			// Mark as disconnected and release service objects
			Connected = false;
			Service = null;
			BillingHandler = null;

			// Inform caller of the disconnection
			RaiseOnDisconnected ();
		}
		#endregion

		#region Events
		/// <summary>
		/// Occurs when on connected.
		/// </summary>
		public delegate void OnConnectedDelegate();

		/// <summary>
		/// Occurs when on connected.
		/// </summary>
		public event OnConnectedDelegate OnConnected;

		/// <summary>
		/// Raises the on connected event.
		/// </summary>
		protected void RaiseOnConnected ()
		{
			// Is this instance connected to the Google Play services?
			if (!Connected) {
				// No, abort
				return;
			}

			// Create a billing handler used to communicate with the Google
			BillingHandler = new InAppBillingHandler (_activity, Service, PublicKey);

			// Inform caller of the connection
			if (this.OnConnected != null)
				this.OnConnected ();
		}

		/// <summary>
		/// Occurs when on disconnected.
		/// </summary>
		public delegate void OnDisconnectedDelegate();

		/// <summary>
		/// Occurs when on disconnected.
		/// </summary>
		public event OnDisconnectedDelegate OnDisconnected;

		/// <summary>
		/// Raises the on disconnected event.
		/// </summary>
		protected virtual void RaiseOnDisconnected ()
		{
			// Inform caller of disconnection
			if (this.OnDisconnected != null)
				this.OnDisconnected ();
		}

		/// <summary>
		/// Occurs when on in app billing error.
		/// </summary>
		/// <param name="error">Error.</param>
		/// <param name="message">Message.</param>
		public delegate void OnInAppBillingErrorDelegate(InAppBillingErrorType error, string message);

		/// <summary>
		/// Occurs when on in app billing error.
		/// </summary>
		public event OnInAppBillingErrorDelegate OnInAppBillingError;

		/// <summary>
		/// Raises the on in app billing error.
		/// </summary>
		/// <param name="error">Error.</param>
		/// <param name="message">Message.</param>
		protected virtual void RaiseOnInAppBillingError (InAppBillingErrorType error, string message)
		{
			// Inform caller of disconnection
			if (this.OnInAppBillingError != null)
				this.OnInAppBillingError (error, message);
		}
		#endregion 
	}
}

