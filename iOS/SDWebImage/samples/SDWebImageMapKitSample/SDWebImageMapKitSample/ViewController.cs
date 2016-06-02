using System;
using CoreLocation;
using MapKit;
using UIKit;

using SDWebImage;
using Foundation;

namespace SDWebImageMapKitSample
{
	partial class ViewController : UIViewController
	{
		private CLLocationManager locationManager;

		public ViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// get access
			locationManager = new CLLocationManager ();
			locationManager.RequestWhenInUseAuthorization ();

			// create the annotation
			var sdAnnotation = new MKPointAnnotation {
				Title = "SDWebImage Annotation",
			};

			// create the view
			string pId = "sdwebimage";
			mapView.GetViewForAnnotation = (mapView, annotation) => {
				if (annotation is MKUserLocation)
					return null;

				// create annotation view
				var pinView = mapView.DequeueReusableAnnotation (pId);
				if (pinView == null) {
					pinView = new MKAnnotationView (annotation, pId);

					// get the image
					pinView.SetImage (
						new NSUrl ("http://radiotray.sourceforge.net/radio.png"),
						UIImage.FromBundle ("placeholder.png"),
						(image, error, cacheType, imageUrl) => {
							if (error != null) {
								Console.WriteLine ("Error: " + error);
							} else {
								Console.WriteLine ("Done: " + pinView.GetImageUrl ());
							}
						});
				}

				return pinView;
			};

			// add the annotation
			mapView.AddAnnotation (sdAnnotation);

			// move it
			mapView.DidUpdateUserLocation += (sender, e) => {
				var loc = e.UserLocation.Coordinate;
				sdAnnotation.Coordinate = new CLLocationCoordinate2D (loc.Latitude, loc.Longitude);

				mapView.SetCenterCoordinate (sdAnnotation.Coordinate, true);
			};

			// start tracking
			mapView.ShowsUserLocation = true;
		}
	}
}
