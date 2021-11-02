using System;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;

using Google.Places;
using System.Collections.Generic;
using Android.Gms.Tasks;
using PlacesSample.Models;


namespace PlacesSample {
	[Activity (Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, IOnSuccessListener, IOnFailureListener, IOnCompleteListener {

		const string apiKey = "";
							   
		Button btnSearch;
		ProgressBar loading;
		ListView places;
		IPlacesClient placesClient;
		List<PlaceData> placesData;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			Xamarin.Essentials.Platform.Init (this, savedInstanceState);
			SetContentView (Resource.Layout.activity_main);
			Title = "Google Places Sample";

			if (string.IsNullOrWhiteSpace (apiKey)) {
				Toast.MakeText (this, "No API key defined in MainActivity.cs", ToastLength.Long).Show ();
				return;
			}

			if (!PlacesApi.IsInitialized)
				PlacesApi.Initialize (this, apiKey);

			placesClient = PlacesApi.CreateClient (this);
			placesData = new List<PlaceData> ();

			InitializeComponents ();
		}

		public override bool OnCreateOptionsMenu (IMenu	menu)
		{
			MenuInflater.Inflate (Resource.Menu.menu_main, menu);
			return true;
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.action_settings) {	
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}

		public override void OnRequestPermissionsResult (int requestCode, string [] permissions, [GeneratedEnum] Android.Content.PM.Permission [] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult (requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult (requestCode, permissions, grantResults);
		}

		void InitializeComponents ()
		{
			btnSearch = FindViewById<Button> (Resource.Id.btnSearch);
			loading = FindViewById<ProgressBar> (Resource.Id.loading);
			places = FindViewById<ListView> (Resource.Id.lstPlaces);

			btnSearch.Click += BtnSearch_Click;
		}

		private void BtnSearch_Click (object sender, EventArgs e)
		{
			if (ContextCompat.CheckSelfPermission (this, Manifest.Permission.AccessWifiState) != Permission.Granted ||
				ContextCompat.CheckSelfPermission (this, Manifest.Permission.AccessFineLocation) != Permission.Granted) {
				Toast.MakeText (this, "Both ACCESS_WIFI_STATE & ACCESS_FINE_LOCATION permissions are required.", ToastLength.Short).Show ();
			}

			if (CheckPermission (Manifest.Permission.AccessFineLocation))
				FindCurrentPlaceWithPermissions ();
		}

		bool CheckPermission (string permission)
		{
			bool hasPermission = ContextCompat.CheckSelfPermission (this, permission) == Permission.Granted;
			if (!hasPermission)
				ActivityCompat.RequestPermissions (this, new [] { permission }, 0);
			return hasPermission;
		}

		void FindCurrentPlaceWithPermissions ()
		{
			loading.Visibility = ViewStates.Visible;

			List<Place.Field> placeFields = new List<Place.Field> { Place.Field.Name, Place.Field.Address };

			FindCurrentPlaceRequest currentPlaceRequest = FindCurrentPlaceRequest.NewInstance (placeFields);
			var currentPlaceTask = placesClient.FindCurrentPlace (currentPlaceRequest);
			currentPlaceTask.AddOnSuccessListener (this, this);
			currentPlaceTask.AddOnFailureListener (this, this);
			currentPlaceTask.AddOnCompleteListener (this, this);
		}

		public void OnSuccess (Java.Lang.Object result)
		{
			placesData.Clear ();
			var findCurrentPlaceResponse = (FindCurrentPlaceResponse)result;

			foreach (var placeLikelihood in findCurrentPlaceResponse.PlaceLikelihoods)
				placesData.Add (new PlaceData {
					Name = placeLikelihood.Place.Name,
					Address = placeLikelihood.Place.Address
				});

			places.Adapter = new PlacesAdapter (this, placesData.ToArray ());
		}

		public void OnFailure (Java.Lang.Exception e) => Toast.MakeText (this, e.Message, ToastLength.Long).Show ();

		public void OnComplete (Task task) => loading.Visibility = ViewStates.Invisible;
	}
}

