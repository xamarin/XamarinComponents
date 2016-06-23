using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.App;
using Mapbox.Maps;
using Mapbox.Annotations;
using Mapbox.Geometry;
using Mapbox.Camera;

[assembly: UsesPermission (Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission (Android.Manifest.Permission.Internet)]
[assembly: UsesPermission (Android.Manifest.Permission.WriteExternalStorage)]

[assembly: UsesPermission (Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesPermission (Android.Manifest.Permission.AccessWifiState)]

namespace MapboxSampleAndroid
{
    [Activity (Label = "Mapbox SDK", MainLauncher = true, Theme="@style/Theme.AppCompat.Light")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        MapboxMap map;
        MapView mapView;

        protected override async void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            mapView = FindViewById<MapView> (Resource.Id.mapview);
            mapView.AccessToken = GetString (Resource.String.mapboxAccessToken);
            mapView.StyleUrl = Mapbox.Constants.Style.Emerald;
            mapView.OnCreate (bundle);

            // Get the map instance
            map = await mapView.GetMapAsync ();
            map.MarkerClick += async (sender, e) => {
                e.Handled = false;
                Toast.MakeText (this, "Marker Click: " + e.Marker.Title, ToastLength.Short).Show ();
            };
            map.InfoWindowClick += async (sender, e) => {
                Toast.MakeText (this, "Info Window Click: " + e.Marker.Title, ToastLength.Short).Show ();
            };
            map.AddMarker (new MarkerOptions ()
                           .SetTitle ("Test Marker")
                           .SetPosition (new LatLng (41.885, -87.679)));
            
            var position = new CameraPosition.Builder ()
                                .Target (new LatLng (41.885, -87.679)) // Sets the new camera position
                                .Zoom (11) // Sets the zoom
                                .Build (); // Creates a CameraPosition from the builder
            map.AnimateCamera (CameraUpdateFactory.NewCameraPosition (position), 3000);
        }

        protected override void OnPause ()
        {            
            base.OnPause ();
            mapView.OnPause ();
        }

        protected override void OnResume ()
        {            
            base.OnResume ();
            if (mapView != null)
            mapView.OnResume ();
        }

        public override void OnLowMemory ()
        {
            base.OnLowMemory ();
            mapView.OnLowMemory ();
        }

        protected override void OnDestroy ()
        {            
            base.OnDestroy ();
            mapView.OnDestroy ();
        }

        protected override void OnSaveInstanceState (Bundle outState)
        {            
            base.OnSaveInstanceState (outState);
            mapView.OnSaveInstanceState (outState);
        }
    }
}

