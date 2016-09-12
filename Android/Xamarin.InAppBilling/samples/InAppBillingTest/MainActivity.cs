using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.InAppBilling;
using Xamarin.InAppBilling.Utilities;

namespace InAppBilling
{
	[Activity (Label = "InAppBillingTest", MainLauncher = true)]
	public class MainActivity : Activity, AdapterView.IOnItemSelectedListener, AdapterView.IOnItemClickListener
	{
		#region Private Variables
		private Button _buyButton;
		private Spinner _produtctSpinner;
		private Product _selectedProduct;
		private IList<Product> _products;
		private InAppBillingServiceConnection _serviceConnection;
		private ListView _lvPurchsedItems;
		private PurchaseAdapter _purchasesAdapter;
		#endregion

		#region Override Methods
		/// <summary>
		/// Starts the current <c>Activity</c>
		/// </summary>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
			// Attempt to attach to the Google Play Service
			StartSetup ();

			// Do the base setup
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Attach to the GUI elements
			_produtctSpinner = FindViewById<Spinner> (Resource.Id.productSpinner);
			_buyButton = FindViewById<Button> (Resource.Id.buyButton);
			_lvPurchsedItems = FindViewById<ListView> (Resource.Id.purchasedItemsList);

			// Configure buy button
			_buyButton.Click += (sender, e) => {
				// Ask the open connection's billing handler to purchase the selected product
				if (_selectedProduct!=null)
					_serviceConnection.BillingHandler.BuyProduct(_selectedProduct);
			}; 

			// Configure the purchased items list
			_lvPurchsedItems.OnItemClickListener = this;

			// Configure the available product spinner
			_produtctSpinner.Enabled = false;
			_produtctSpinner.OnItemSelectedListener = this;

			// Initialize the list of available items
			_products = new List<Product> ();

		}

		/// <summary>
		/// Perform any final cleanup before an activity is destroyed.
		/// </summary>
		protected override void OnDestroy ()
		{
			// Are we attached to the Google Play Service?
			if (_serviceConnection != null) {
				// Yes, disconnect
				_serviceConnection.Disconnect ();
			}

			// Call base method
			base.OnDestroy ();
		}

		/// <Docs>The integer request code originally supplied to
		///  startActivityForResult(), allowing you to identify who this
		///  result came from.</Docs>
		/// <param name="data">An Intent, which can return result data to the caller
		///  (various data can be attached to Intent "extras").</param>
		/// <summary>
		/// Raises the activity result event.
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="resultCode">Result code.</param>
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// Ask the open service connection's billing handler to process this request
			_serviceConnection.BillingHandler.HandleActivityResult (requestCode, resultCode, data);

			//TODO: Use a call back to update the purchased items
			UpdatePurchasedItems ();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Loads the purchased items.
		/// </summary>
		private void LoadPurchasedItems ()
		{
			// Ask the open connection's billing handler to get any purchases
			var purchases = _serviceConnection.BillingHandler.GetPurchases (ItemType.Product);

			// Display any existing purchases
			_purchasesAdapter = new PurchaseAdapter (this, purchases);
			_lvPurchsedItems.Adapter = _purchasesAdapter;
		}

		/// <summary>
		/// Updates the purchased items.
		/// </summary>
		private void UpdatePurchasedItems ()
		{
			// Ask the open connection's billing handler to get any purchases
			var purchases = _serviceConnection.BillingHandler.GetPurchases (ItemType.Product);

			// Is there a data adapter for purchases?
			if (_purchasesAdapter != null) {
				// Yes, add new items to adapter
				foreach (var item in purchases) {
					_purchasesAdapter.Items.Add (item);
				}

				// Ask the adapter to display the new items
				_purchasesAdapter.NotifyDataSetChanged ();
			}
		}

		/// <summary>
		/// Connects to the Google Play Service and gets a list of products that are available
		/// for purchase.
		/// </summary>
		/// <returns>The inventory.</returns>
		private async Task GetInventory ()
		{
			// Ask the open connection's billing handler to return a list of avilable products for the 
			// given list of items.
			// NOTE: We are asking for the Reserved Test Product IDs that allow you to test In-App
			// Billing without actually making a purchase.
			_products = await _serviceConnection.BillingHandler.QueryInventoryAsync (new List<string> {
				"appra_01_test",
				"appra_02_sub",
				ReservedTestProductIDs.Purchased
			}, ItemType.Product);

			// Were any products returned?
			if (_products == null) {
				// No, abort
				return;
			}

			// Enable the list of products
			_produtctSpinner.Enabled = (_products.Count > 0);

			// Populate list of available products
			var items = _products.Select (p => p.Title).ToList ();
			_produtctSpinner.Adapter = 
				new ArrayAdapter<string> (this, 
				                          Android.Resource.Layout.SimpleSpinnerItem,
				                          items);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Starts the setup of this Android application by connection to the Google Play Service
		/// to handle In-App purchases.
		/// </summary>
		public void StartSetup ()
		{
			// A Licensing and In-App Billing public key is required before an app can communicate with
			// Google Play, however you DON'T want to store the key in plain text with the application.
			// The Unify command provides a simply way to obfuscate the key by breaking it into two or
			// or more parts, specifying the order to reassemlbe those parts and optionally providing
			// a set of key/value pairs to replace in the final string. 
			string value = Security.Unify (
				new string [] { "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtpopeFYhDOOsufNhYe2PY97azBQBJoqGSP/XxsgzQgj3M0MQQ0WE0WDwPKoxlo/MIuoadVR5q2ZRs3rTl",
					"UsH9vYUPr/z0/O8kn5anQHshQkHkeHsA8wbyJGGmwcXqvZr1fnzFOFyHL46s47vOJBLc5x30oieJ02dNxdBcy0oFEJtJS5Ng5sm6YUv9ifgxYOqMr/K61HNw6tP0j",
					"vi6vLv2ro/KXO0ADVxcDxEg+Pk16xVNKgnei8M09PJCgA9bWNyeSsD+85Jj9+OZGsS/DN7O7nrGuKx8oAg/lE6a2eCQHh9lXxSPlAFAMH2FB8aNxUeJxkByW+6l/S",
					"yqvVlOeYxAwIDAQAB" },
				new int [] { 0, 1, 2, 3 });

			// Create a new connection to the Google Play Service
			_serviceConnection = new InAppBillingServiceConnection (this, value);
			_serviceConnection.OnConnected += () => {
				// Attach to the various error handlers to report issues
				_serviceConnection.BillingHandler.OnGetProductsError += (int responseCode, Bundle ownedItems) => {
					Console.WriteLine("Error getting products");
				};

				_serviceConnection.BillingHandler.OnInvalidOwnedItemsBundleReturned += (Bundle ownedItems) => {
					Console.WriteLine("Invalid owned items bundle returned");
				};

				_serviceConnection.BillingHandler.OnProductPurchasedError += (int responseCode, string sku) => {
					Console.WriteLine("Error purchasing item {0}",sku);
				};

				_serviceConnection.BillingHandler.OnPurchaseConsumedError += (int responseCode, string token) => {
					Console.WriteLine("Error consuming previous purchase");
				};

				_serviceConnection.BillingHandler.InAppBillingProcesingError += (message) => {
					Console.WriteLine("In app billing processing error {0}",message);
				};

				// Load inventory or available products
				GetInventory();

				// Load any items already purchased
				LoadPurchasedItems();
			};

			// Attempt to connect to the service
			_serviceConnection.Connect ();

		}
		#endregion

		#region User Interaction Routines
		/// <summary>
		/// Handle the user selecting an item from the list of available products
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <param name="view">View.</param>
		/// <param name="position">Position.</param>
		/// <param name="id">Identifier.</param>
		public void OnItemSelected (AdapterView parent, Android.Views.View view, int position, long id)
		{
			// Grab the selecting product
			_selectedProduct = _products [position];
		}

		/// <summary>
		/// Handle nothing being selected
		/// </summary>
		/// <param name="parent">Parent.</param>
		public void OnNothingSelected (AdapterView parent)
		{
			// Do nothing
		}

		/// <summary>
		/// Handle the user consuming a previously purchased item
		/// </summary>
		/// <param name="parent">Parent.</param>
		/// <param name="view">View.</param>
		/// <param name="position">Position.</param>
		/// <param name="id">Identifier.</param>
		public void OnItemClick (AdapterView parent, Android.Views.View view, int position, long id)
		{
			// Access item being clicked on
			string productid = ((TextView)view).Text;
			var purchases = _purchasesAdapter.Items;
			var purchasedItem = purchases.FirstOrDefault (p => p.ProductId == productid);

			// Was anyting selected?
			if (purchasedItem != null) {
				// Yes, attempt to consume the given product
				bool result = _serviceConnection.BillingHandler.ConsumePurchase (purchasedItem);

				// Was the product consumed?
				if (result) {
					// Yes, update interface
					_purchasesAdapter.Items.Remove (purchasedItem);
					_purchasesAdapter.NotifyDataSetChanged ();
				}
			}
		}
		#endregion

	}
}


