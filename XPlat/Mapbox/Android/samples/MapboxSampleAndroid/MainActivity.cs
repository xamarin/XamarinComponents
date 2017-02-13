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
using Mapbox.Services.Directions.V5.Models;
using Mapbox.Services.Commons.GeoJson;
using System.Collections.Generic;
using Mapbox.Services;
using System.Linq;

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

            Mapbox.MapboxAccountManager.Start(this, GetString(Resource.String.mapboxAccessToken));

            mapView = FindViewById<MapView> (Resource.Id.mapview);
            mapView.StyleUrl = Mapbox.Constants.Style.Light;
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

            var origin = Mapbox.Services.Commons.Models.Position.FromCoordinates(-87.611331, 41.718542);
            var destination = Mapbox.Services.Commons.Models.Position.FromCoordinates(-87.598800, 41.788592);

            // Add origin and destination to the map
            map.AddMarker(new MarkerOptions()
               .SetPosition(new LatLng(origin.Latitude, origin.Longitude))
               .SetTitle("Chicago State University"));
            map.AddMarker(new MarkerOptions()
               .SetPosition(new LatLng(destination.Latitude, destination.Longitude))
               .SetTitle("University of Chicago"));

            var position = new CameraPosition.Builder ()
                                .Target (new LatLng (41.885, -87.679)) // Sets the new camera position
                                .Zoom (9) // Sets the zoom
                                .Build (); // Creates a CameraPosition from the builder
            map.AnimateCamera (CameraUpdateFactory.NewCameraPosition (position), 3000);


            var mdb = new Mapbox.Services.Directions.V5.MapboxDirections.Builder()
                                .SetAccessToken (Mapbox.MapboxAccountManager.Instance.AccessToken)
                                .SetOrigin(origin)
                                .SetDestination(destination)
                                .SetProfile (Mapbox.Services.Directions.V5.DirectionsCriteria.ProfileDriving)
                                .Build();

            var directionsResponse = await mdb.ExecuteCallAsync();
            var route = directionsResponse?.Routes?.FirstOrDefault();
            if (route != null)
                drawRoute(route);
        }

        void drawRoute(DirectionsRoute route)
        {
            // Convert LineString coordinates into LatLng[]
            var lineString = LineString.FromPolyline(route.Geometry, Constants.OsrmPrecisionV5);
            var coordinates = lineString.Coordinates;
            LatLng[] points = new LatLng[coordinates.Count];
            for (int i = 0; i < coordinates.Count; i++)
            {
                points[i] = new LatLng(
                  coordinates[i].Latitude,
                  coordinates[i].Longitude);
            }

            // Draw Points on MapView
            map.AddPolyline(new PolylineOptions()
              .Add(points)
              .SetColor(Android.Graphics.Color.Red)
              .SetWidth(5));
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

