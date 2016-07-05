using System;
using UIKit;
using Estimote;
using CoreGraphics;
using CoreLocation;

namespace BeaconExample
{
	public class ProximityDemoViewController : UIViewController
	{
		CLBeacon beacon;
		BeaconManager beaconManager;
		CLBeaconRegion beaconRegion;
		UIImageView imageView;
		UILabel zoneLabel;
		public ProximityDemoViewController (CLBeacon beacon)
		{
			this.beacon = beacon;
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Title = "Proximity Demo";

			View.BackgroundColor = UIColor.White;
			zoneLabel = new UILabel (new CGRect (0, 100, View.Frame.Size.Width, 40));
			zoneLabel.TextAlignment = UITextAlignment.Center;
			View.AddSubview (zoneLabel);


			imageView = new UIImageView (new CGRect (0, 64, View.Frame.Size.Width, View.Frame.Size.Height - 64));
			imageView.ContentMode = UIViewContentMode.Center;
			View.AddSubview (imageView);

			//beacon setup
			beaconManager = new BeaconManager ();
			beaconRegion = new CLBeaconRegion (beacon.ProximityUuid,ushort.Parse( beacon.Major.ToString()), ushort.Parse(beacon.Minor.ToString()), "RegionIdentifier");

			beaconManager.StartRangingBeaconsInRegion (beaconRegion);

            beaconManager.RangedBeacons += (sender, e) => 
            {
                if(e.Beacons.Length == 0 )
                    return;

                zoneLabel.Text = TextForProximity(e.Beacons[0].Proximity);
                imageView.Image = ImageForProximity(e.Beacons[0].Proximity);
            };
		}

		private string TextForProximity(CLProximity proximity)
		{
			switch (proximity) {
			case CLProximity.Far:
				return "Far";
			case CLProximity.Immediate:
				return "Immediate";
			case CLProximity.Near:
				return "Near";
			default:
				return "Unknown";
			}
		}

		private UIImage ImageForProximity(CLProximity proximity)
		{
			switch (proximity) {
			case CLProximity.Far:
				return UIImage.FromFile ("far_image");
			case CLProximity.Immediate:
				return UIImage.FromFile ("near_image");
			case CLProximity.Near:
				return UIImage.FromFile ("immediate_image");
			default:
				return UIImage.FromFile ("unknown_image");
			}
		}

		public override void ViewDidDisappear (bool animated)
		{
			beaconManager.StopRangingBeaconsInRegion (beaconRegion);
			base.ViewDidDisappear (animated);
		}
	}
}

