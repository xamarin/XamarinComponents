using System;
using UIKit;
using Estimote;
using Foundation;
using CoreLocation;

namespace BeaconExample
{
	public enum Demo
	{
		Distance,
		Proximity,
		Notifications
	}
	public class BeaconScanViewController : UITableViewController
	{
		BeaconManager beaconManager;
		UtilityManager utilityManager;
		CLBeaconRegion region;
		CLBeacon[] beacons;
		Demo demo;
		public BeaconScanViewController (Demo demo)
		{
			this.demo = demo;
		}
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = "Select Beacon";
			
            utilityManager = new UtilityManager ();
            beaconManager = new BeaconManager ();
			beaconManager.ReturnAllRangedBeaconsAtOnce = true;
            region = new CLBeaconRegion (AppDelegate.BeaconUUID, "BeaconSample");

            beaconManager.AuthorizationStatusChanged += (sender, e) => 
                StartRangingBeacons ();
            beaconManager.RangedBeacons += (sender, e) => 
            {
                beacons = e.Beacons;
                TableView.ReloadData();
            };
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidLoad ();
			
            StartRangingBeacons ();
		}

		private void StartRangingBeacons()
		{
			var status = BeaconManager.AuthorizationStatus;
			if (status == CLAuthorizationStatus.NotDetermined)
			{
				if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
					/*
             * No need to explicitly request permission in iOS < 8, will happen automatically when starting ranging.
             */
					beaconManager.StartRangingBeaconsInRegion(region);

				} else {
					/*
             * Request permission to use Location Services. (new in iOS 8)
             * We ask for "always" authorization so that the Notification Demo can benefit as well.
             * Also requires NSLocationAlwaysUsageDescription in Info.plist file.
             *
             * For more details about the new Location Services authorization model refer to:
             * https://community.estimote.com/hc/en-us/articles/203393036-Estimote-SDK-and-iOS-8-Location-Services
             */
					beaconManager.RequestAlwaysAuthorization ();
				}
			}
			else if(status == CLAuthorizationStatus.Authorized)
			{
				beaconManager.StartRangingBeaconsInRegion(region);

			}
			else if(status == CLAuthorizationStatus.Denied)
			{
				new UIAlertView ("Location Access Denied", "You have denied access to location services. Change this in app settings.", null, "OK").Show ();
			}
			else if(status == CLAuthorizationStatus.Restricted)
			{
				new UIAlertView ("Location Not Available", "You have no access to location services.", null, "OK").Show ();
			}
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			beaconManager.StopRangingBeaconsInRegion (region);
			utilityManager.StopEstimoteBeaconDiscovery ();
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			if (beacons == null)
				return 0;
			else
				return beacons.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell ("cellID");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "cellId");
			}

			var beacon = beacons [indexPath.Row];

			cell.TextLabel.Text = string.Format ("major: {0}, Minor {1}", beacon.Major, beacon.Minor);
			//cell.DetailTextLabel.Text = "Distance: " + beacon.Distance.ToString();

			return cell;
		}

		public async override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			switch (demo) {
			case Demo.Distance:
				NavigationController.PushViewController (new DistanceDemoViewController (beacons [indexPath.Row]), true);
				break;
			case Demo.Proximity:
				NavigationController.PushViewController (new ProximityDemoViewController (beacons [indexPath.Row]), true);
				break;
			case Demo.Notifications:
				var storyboard = UIStoryboard.FromName ("MainStoryboard", null);
				var vc = storyboard.InstantiateViewController ("NotificationViewController") as NotificationViewController;
				vc.Beacon = beacons [indexPath.Row];
				NavigationController.PushViewController (vc, true);
				break;
			}

		}

	}
}

