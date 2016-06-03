using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using Photos;
using UIKit;

using SDWebImage;

using MWPhotoBrowser;

namespace MWPhotoBrowserSample
{
	partial class MenuViewController : UITableViewController
	{
		private BrowserDelegate browserDelegate;

		private UISegmentedControl _segmentedControl;

		private List<Tuple<string, string>> menuItems = new List<Tuple<string, string>> ();

		public MenuViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			SDImageCache.SharedImageCache.ClearDisk ();
			SDImageCache.SharedImageCache.ClearMemory ();

			menuItems.AddRange (new [] {
				new Tuple<string, string> ("Single photo", "with caption, no grid button"),
				new Tuple<string, string> ("Multiple photos and video", "with captions"),
				new Tuple<string, string> ("Multiple photo grid", "showing grid first, nav arrows enabled"),
				new Tuple<string, string> ("Photo selections", "selection enabled"),
				new Tuple<string, string> ("Photo selection grid", "selection enabled, start at grid"),
				new Tuple<string, string> ("Web photos", "photos from web"),
				new Tuple<string, string> ("Web photo grid", "showing grid first"),
				new Tuple<string, string> ("Single video", "with auto-play"),
				new Tuple<string, string> ("Web videos", "showing grid first"),
				new Tuple<string, string> ("Library photos and videos", "media from device library")
			});

			Title = "MWPhotoBrowser";

			browserDelegate = new BrowserDelegate (TableView);

			_segmentedControl = new UISegmentedControl (new []{ "Push", "Modal" });
			_segmentedControl.SelectedSegment = 0;
			_segmentedControl.ValueChanged += delegate {
				TableView.ReloadData ();
			};
        
			UIBarButtonItem item = new UIBarButtonItem (_segmentedControl);
			NavigationItem.RightBarButtonItem = item;
			NavigationItem.BackBarButtonItem = new UIBarButtonItem ("Back", UIBarButtonItemStyle.Bordered, null);

			browserDelegate.LoadAssets ();
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection (UITableView tableView, nint section)
		{
			var rows = 9;
			if (browserDelegate.Assets.Count > 0) {
				rows++;
			}
			return rows;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// Create
			var cell = TableView.DequeueReusableCell ("Cell");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "Cell");
			}
			cell.Accessory = _segmentedControl.SelectedSegment == 0 ? UITableViewCellAccessory.DisclosureIndicator : UITableViewCellAccessory.None;

			// Configure
			var tuple = menuItems [indexPath.Row];
			cell.TextLabel.Text = tuple.Item1;
			cell.DetailTextLabel.Text = tuple.Item2;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var browser = new PhotoBrowser (browserDelegate);
			browser.DisplayActionButton = true;
			browser.DisplaySelectionButtons = false;
			browser.DisplayNavArrows = false;
			browser.EnableGrid = true;
			browser.StartOnGrid = false;
			browser.AutoPlayOnAppear = false;

			switch (indexPath.Row) {
			case 0:
				browserDelegate.ShowSinglePhoto (browser);
				break;
			case 1:
				browserDelegate.ShowMultiplePhotos (browser);
				break;
			case 2:
				browserDelegate.ShowMultiplePhotoGrid (browser);
				break;
			case 3:
			case 4:
				browserDelegate.ShowPhotoSelections (browser, indexPath.Row == 4);
				break;
			case 5:
				browserDelegate.ShowWebPhotos (browser);
				break;
			case 6:
				browserDelegate.ShowWebPhotosGrid (browser);
				break;
			case 7:
				browserDelegate.ShowSingleVideo (browser);
				break;
			case 8:
				browserDelegate.ShowWebVideos (browser);
				break;
			case 9:
				browserDelegate.ShowLibrary (browser);
				break;
			}

			// Settings
			browser.EnableSwipeToDismiss = false;
			browser.CurrentIndex = 0;

			// Show
			if (_segmentedControl.SelectedSegment == 0) {
				// Push
				NavigationController.PushViewController (browser, true);
			} else {
				// Modal
				var nc = new UINavigationController (browser);
				nc.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
				PresentViewController (nc, true, null);
			}
			
			// Deselect
			TableView.DeselectRow (indexPath, true);
		}

		private class BrowserDelegate : PhotoBrowserDelegate
		{
			private UITableView tableView;

			private bool[] _selections;

			private List<PHAsset> assets = new List<PHAsset> ();
			private List<IPhoto> photos = new List<IPhoto> ();
			private List<IPhoto> thumbs = new List<IPhoto> ();

			public BrowserDelegate (UITableView tableView)
			{
				this.tableView = tableView;
			}

			public List<PHAsset> Assets { 
				get { return assets; } 
			}

			public override nuint GetPhotoCount (PhotoBrowser photoBrowser)
			{
				return (nuint)photos.Count;
			}

			public override IPhoto GetPhoto (PhotoBrowser photoBrowser, nuint index)
			{
				return photos [(int)index];
			}

			public override IPhoto GetThumbnail (PhotoBrowser photoBrowser, nuint index)
			{
				return thumbs [(int)index];
			}

			public override bool IsPhotoSelected (PhotoBrowser photoBrowser, nuint index)
			{
				return _selections [index];
			}

			public override void OnSelectedChanged (PhotoBrowser photoBrowser, nuint index, bool selected)
			{
				_selections [index] = selected;
				Console.WriteLine ("Photo at index {0} selected ? {1}.", index, selected);
			}

			public override void DidDisplayPhoto (PhotoBrowser photoBrowser, nuint index)
			{
				Console.WriteLine ("Did start viewing photo at index {0}.", index);
			}

			public void LoadAssets ()
			{
				// Load Assets
				if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0)) {
					// Check library permissions
					var status = PHPhotoLibrary.AuthorizationStatus;
					if (status == PHAuthorizationStatus.NotDetermined) {
						PHPhotoLibrary.RequestAuthorization (s => {
							if (s == PHAuthorizationStatus.Authorized) {
								PerformLoadAssets ();
							}
						});
					} else if (status == PHAuthorizationStatus.Authorized) {
						PerformLoadAssets ();
					}
				}
			}

			private async void PerformLoadAssets ()
			{
				var options = new PHFetchOptions ();
				options.SortDescriptors = new [] { new NSSortDescriptor ("creationDate", false) };
				assets = await Task.Run (() => PHAsset.FetchAssets (options).Cast<PHAsset> ().ToList ());
				if (assets.Count > 0) {
					tableView.ReloadData ();
				}
			}

			public void ShowSinglePhoto (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2", "jpg"));
				photo.Caption = "The London Eye is a giant Ferris wheel situated on the banks of the River Thames, in London, England.";
				photos.Add (photo);

				browser.EnableGrid = false;
			}

			public void ShowMultiplePhotos (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo5", "jpg"));
				photo.Caption = "Fireworks";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2", "jpg"));
				photo.Caption = "The London Eye is a giant Ferris wheel situated on the banks of the River Thames, in London, England.";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo3", "jpg"));
				photo.Caption = "York Floods";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("video_thumb", "jpg"));
				photo.VideoUrl = new NSUrl (NSBundle.MainBundle.PathForResource ("video", "mp4"), false);
				photo.Caption = "Big Buck Bunny";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo4", "jpg"));
				photo.Caption = "Campervan";
				photos.Add (photo);

				browser.EnableGrid = false;
			}

			public void ShowMultiplePhotoGrid (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				// Photos

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo5", "jpg"));
				photo.Caption = "White Tower";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2", "jpg"));
				photo.Caption = "The London Eye is a giant Ferris wheel situated on the banks of the River Thames, in London, England.";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo3", "jpg"));
				photo.Caption = "York Floods";
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo4", "jpg"));
				photo.Caption = "Campervan";
				photos.Add (photo);

				// Thumbs

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo5t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo3t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo4t", "jpg"));
				thumbs.Add (photo);

				browser.StartOnGrid = true;
				browser.DisplayNavArrows = true;
			}

			public void ShowPhotoSelections (PhotoBrowser browser, bool showGrid)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				// Photos

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo4", "jpg"));
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo1", "jpg"));
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2", "jpg"));
				photos.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo3", "jpg"));
				photos.Add (photo);

				// Thumbs

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo4t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo1t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo2t", "jpg"));
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("photo3t", "jpg"));
				thumbs.Add (photo);

				browser.DisplayActionButton = false;
				browser.DisplaySelectionButtons = true;
				browser.StartOnGrid = showGrid;
				browser.EnableGrid = false;

				// Reset selections
				_selections = Enumerable.Repeat (false, photos.Count).ToArray ();
			}

			public void ShowWebPhotos (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				// Photos

				photos.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3567/3523321514_371d9ac42f_b.jpg")));
				photos.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3629/3339128908_7aecabc34b_b.jpg")));
				photos.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3364/3338617424_7ff836d55f_b.jpg")));
				photos.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3590/3329114220_5fbc5bc92b_b.jpg")));
				photos.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2449/4052876281_6e068ac860_b.jpg")));

				// Thumbs

				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3567/3523321514_371d9ac42f_q.jpg")));
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3629/3339128908_7aecabc34b_q.jpg")));
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3364/3338617424_7ff836d55f_q.jpg")));
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3590/3329114220_5fbc5bc92b_q.jpg")));
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2449/4052876281_6e068ac860_q.jpg")));
			}

			public void ShowWebPhotosGrid (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				// Photos & thumbs
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3779/9522424255_28a5a9d99c_b.jpg"));
				photo.Caption = "Tube";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3779/9522424255_28a5a9d99c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3777/9522276829_fdea08ffe2_b.jpg"));
				photo.Caption = "Flat White at Elliot's";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3777/9522276829_fdea08ffe2_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm9.static.flickr.com/8379/8530199945_47b386320f_b.jpg"));
				photo.Caption = "Woburn Abbey";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm9.static.flickr.com/8379/8530199945_47b386320f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm9.static.flickr.com/8364/8268120482_332d61a89e_b.jpg"));
				photo.Caption = "Frosty walk";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm9.static.flickr.com/8364/8268120482_332d61a89e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm8.static.flickr.com/7109/7604416018_f23733881b_b.jpg"));
				photo.Caption = "Jury's Inn";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm8.static.flickr.com/7109/7604416018_f23733881b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm7.static.flickr.com/6002/6020924733_b21874f14c_b.jpg"));
				photo.Caption = "Heavy Rain";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm7.static.flickr.com/6002/6020924733_b21874f14c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm5.static.flickr.com/4012/4501918517_5facf1a8c4_b.jpg"));
				photo.Caption = "iPad Application Sketch Template v1";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm5.static.flickr.com/4012/4501918517_5facf1a8c4_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2667/4072710001_f36316ddc7_b.jpg"));
				photo.Caption = "Grotto of the Madonna";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2667/4072710001_f36316ddc7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2449/4052876281_6e068ac860_b.jpg"));
				photo.Caption = "Beautiful Eyes";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2449/4052876281_6e068ac860_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3528/4052875665_53e5b4dc61_b.jpg"));
				photo.Caption = "Cousin Portrait";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3528/4052875665_53e5b4dc61_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3520/3846053408_6ecf775a3e_b.jpg"));
				photo.Caption = "iPhone Application Sketch Template v1.3";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3520/3846053408_6ecf775a3e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3624/3559209373_003152b4fd_b.jpg"));
				photo.Caption = "Door Knocker of Capitanía General";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3624/3559209373_003152b4fd_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3551/3523421738_30455b63e0_b.jpg"));
				photo.Caption = "Parroquia Sta Maria del Mar";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3551/3523421738_30455b63e0_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3224/3523355044_6551552f93_b.jpg"));
				photo.Caption = "Central Atrium in Casa Batlló";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3224/3523355044_6551552f93_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3567/3523321514_371d9ac42f_b.jpg"));
				photo.Caption = "Gaudí's Casa Batlló spiral ceiling";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3567/3523321514_371d9ac42f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3629/3339128908_7aecabc34b_b.jpg"));
				photo.Caption = "The Royal Albert Hall";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3629/3339128908_7aecabc34b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3338/3339119002_e0d8ec2f2e_b.jpg"));
				photo.Caption = "Midday & Midnight at the RAH";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3338/3339119002_e0d8ec2f2e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3364/3338617424_7ff836d55f_b.jpg"));
				photo.Caption = "Westminster Bridge";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3364/3338617424_7ff836d55f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3604/3328356821_5503b332aa_b.jpg"));
				photo.Caption = "Prime Meridian Sculpture";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3604/3328356821_5503b332aa_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3590/3329114220_5fbc5bc92b_b.jpg"));
				photo.Caption = "Docklands";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3590/3329114220_5fbc5bc92b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3602/3329107762_64a1454080_b.jpg"));
				photo.Caption = "Planetarium";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3602/3329107762_64a1454080_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3122/3143635211_80b29ab354_b.jpg"));
				photo.Caption = "Eurostar Perspective";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3122/3143635211_80b29ab354_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3091/3144451298_db6f6da3f9_b.jpg"));
				photo.Caption = "The Meeting Place";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3091/3144451298_db6f6da3f9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3110/3143623585_a12fa172fc_b.jpg"));
				photo.Caption = "Embrace";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3110/3143623585_a12fa172fc_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3107/3143613445_d9562105ea_b.jpg"));
				photo.Caption = "See to the Sky with the Station Saver";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3107/3143613445_d9562105ea_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3203/3144431930_db55ee05a2_b.jpg"));
				photo.Caption = "Sir John Betjeman";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3203/3144431930_db55ee05a2_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3102/3143588227_5d0d806b43_b.jpg"));
				photo.Caption = "St Pancras, London";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3102/3143588227_5d0d806b43_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3194/2987143528_2ee4a9e3cc_b.jpg"));
				photo.Caption = "Shelter from the Storm";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3194/2987143528_2ee4a9e3cc_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3219/2983541189_467dc559ed_b.jpg"));
				photo.Caption = "Alexander, Molly & George";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3219/2983541189_467dc559ed_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3277/2978593618_25a24b5348_b.jpg"));
				photo.Caption = "It's Eerie Underground";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3277/2978593618_25a24b5348_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3043/2977609977_241fe844be_b.jpg"));
				photo.Caption = "VW Camper";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3043/2977609977_241fe844be_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3257/2871822885_890c7d969d_b.jpg"));
				photo.Caption = "York Floods - September 2008";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3257/2871822885_890c7d969d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3170/2860137277_ecefb94bb9_b.jpg"));
				photo.Caption = "Still Standing";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3170/2860137277_ecefb94bb9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3209/2799943935_81840a6dcc_b.jpg"));
				photo.Caption = "The Edge of the World";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3209/2799943935_81840a6dcc_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3269/2800788836_8ae7c988a9_b.jpg"));
				photo.Caption = "Beautiful Bark";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3269/2800788836_8ae7c988a9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3080/2800766694_5c87a0238c_b.jpg"));
				photo.Caption = "What's the name of this flower?";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3080/2800766694_5c87a0238c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3110/2799879647_f9ee50054e_b.jpg"));
				photo.Caption = "Flamborough Lighthouse";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3110/2799879647_f9ee50054e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3176/2575404273_6f2f135801_b.jpg"));
				photo.Caption = "Looking into London's Eye";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3176/2575404273_6f2f135801_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3147/2575402639_3e1e60a0e7_b.jpg"));
				photo.Caption = "Large Ben";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3147/2575402639_3e1e60a0e7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3002/2576229168_276565ac08_b.jpg"));
				photo.Caption = "The Leaning Tower of London";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3002/2576229168_276565ac08_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3290/2530710337_90d3160da0_b.jpg"));
				photo.Caption = "Monkey Features";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3290/2530710337_90d3160da0_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2342/2499163392_0c8125cbf7_b.jpg"));
				photo.Caption = "Metal and Stone";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2342/2499163392_0c8125cbf7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3241/2499162188_1097d7280f_b.jpg"));
				photo.Caption = "York Minster Interior";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3241/2499162188_1097d7280f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3162/2499161250_4100c907ee_b.jpg"));
				photo.Caption = "Colour Below a Heart";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3162/2499161250_4100c907ee_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2411/2386560315_c0b237ed0e_b.jpg"));
				photo.Caption = "Tremendous Tulip";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2411/2386560315_c0b237ed0e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2140/2375307082_ea04b45d8f_b.jpg"));
				photo.Caption = "Rose Reflection";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2140/2375307082_ea04b45d8f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3035/2375304830_e894d29141_b.jpg"));
				photo.Caption = "Cliffords Tower, York";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3035/2375304830_e894d29141_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3197/2367543868_af828a6543_b.jpg"));
				photo.Caption = "Cog & Chain";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3197/2367543868_af828a6543_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2286/2367542014_6d8145711c_b.jpg"));
				photo.Caption = "Rocket";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2286/2367542014_6d8145711c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3217/2323979971_3a6209b41e_b.jpg"));
				photo.Caption = "Snowdrops";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3217/2323979971_3a6209b41e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3182/2324797228_dec69be7b4_b.jpg"));
				photo.Caption = "Castle Howard Fountain";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3182/2324797228_dec69be7b4_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2266/2323974229_e21e0e3fe1_b.jpg"));
				photo.Caption = "Castle Howard House Lines";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2266/2323974229_e21e0e3fe1_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3018/2323969673_e6d9cc74d7_b.jpg"));
				photo.Caption = "Castle Howard Wide";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3018/2323969673_e6d9cc74d7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3103/2323967485_694a897d5f_b.jpg"));
				photo.Caption = "Castle Howard House";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3103/2323967485_694a897d5f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3086/2324784818_8cd6123633_b.jpg"));
				photo.Caption = "Castle Howard Fountain";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3086/2324784818_8cd6123633_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2318/2324783136_56bed1f7ab_b.jpg"));
				photo.Caption = "Castle Howard House Back";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2318/2324783136_56bed1f7ab_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2228/2323959487_113c1c26fe_b.jpg"));
				photo.Caption = "Castle Howard House Side";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2228/2323959487_113c1c26fe_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3058/2324776658_da476bbb32_b.jpg"));
				photo.Caption = "Castle Howard";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm4.static.flickr.com/3058/2324776658_da476bbb32_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2359/2323944883_a277e1becf_b.jpg"));
				photo.Caption = "Peacock";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2359/2323944883_a277e1becf_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2172/2323940209_ae2d69fb51_b.jpg"));
				photo.Caption = "Castle Howard Fountain";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2172/2323940209_ae2d69fb51_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2143/2268578601_464af2fabc_b.jpg"));
				photo.Caption = "Outlook";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2143/2268578601_464af2fabc_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2165/2268575867_d3c1bc5b65_b.jpg"));
				photo.Caption = "Stones & Sand";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2165/2268575867_d3c1bc5b65_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2149/2269364904_bc4a63f3e0_b.jpg"));
				photo.Caption = "Pebbles and Stones";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2149/2269364904_bc4a63f3e0_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2176/2268569547_99197322f9_b.jpg"));
				photo.Caption = "Fisherman";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2176/2268569547_99197322f9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2095/2268567981_ac142a0409_b.jpg"));
				photo.Caption = "Walking on Water";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2095/2268567981_ac142a0409_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2074/2268528659_e7f1d60e8f_b.jpg"));
				photo.Caption = "Viking Boat York";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2074/2268528659_e7f1d60e8f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2326/2268524733_5f40d784d4_b.jpg"));
				photo.Caption = "Hot Air Balloon Colours";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2326/2268524733_5f40d784d4_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2022/2268522293_470659cdec_b.jpg"));
				photo.Caption = "Hot Air Balloon Lift Off";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2022/2268522293_470659cdec_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2420/2268520049_d33bb30b6f_b.jpg"));
				photo.Caption = "Hot Air Balloon Take Off";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2420/2268520049_d33bb30b6f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2403/2269308580_24e5e8cb1d_b.jpg"));
				photo.Caption = "Hot Air Balloon High";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2403/2269308580_24e5e8cb1d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2040/2268515857_7708793db8_b.jpg"));
				photo.Caption = "Hot Air Balloon Blue Sky";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2040/2268515857_7708793db8_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2176/2137556633_ce7f55d97c_b.jpg"));
				photo.Caption = "Christmas Lights";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2176/2137556633_ce7f55d97c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2256/2138335766_532c98183b_b.jpg"));
				photo.Caption = "Christmas Dinner";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2256/2138335766_532c98183b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2309/2137552857_b3a866d66a_b.jpg"));
				photo.Caption = "Christmas Tree";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2309/2137552857_b3a866d66a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2151/2137550333_30a80de9dd_b.jpg"));
				photo.Caption = "Christmas Gifts";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2151/2137550333_30a80de9dd_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2217/1800632013_f5b6f430ea_b.jpg"));
				photo.Caption = "Firework Flower";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2217/1800632013_f5b6f430ea_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2025/1800630921_05c119b257_b.jpg"));
				photo.Caption = "Fireworks 1";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2025/1800630921_05c119b257_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2283/1800622021_a69274fe8e_b.jpg"));
				photo.Caption = "Sunset";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2283/1800622021_a69274fe8e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2296/1800493695_05e8f99119_b.jpg"));
				photo.Caption = "Morning Fields";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm3.static.flickr.com/2296/1800493695_05e8f99119_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1318/1258684849_5423b2b0a7_b.jpg"));
				photo.Caption = "Garden Window";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1318/1258684849_5423b2b0a7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1169/1214182813_92ef4b864e_b.jpg"));
				photo.Caption = "Storm Clouds";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1169/1214182813_92ef4b864e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1060/1214995776_c6ccc7b589_b.jpg"));
				photo.Caption = "South Light";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1060/1214995776_c6ccc7b589_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1271/1213967453_8f5927b62a_b.jpg"));
				photo.Caption = "Cala Gal Dana, Panoramic";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1271/1213967453_8f5927b62a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1052/1214735762_1fa7af8cf9_b.jpg"));
				photo.Caption = "Coloured Glass";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1052/1214735762_1fa7af8cf9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1108/1214725784_392c7236cb_b.jpg"));
				photo.Caption = "Well";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1108/1214725784_392c7236cb_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1261/1214721656_3e50b51adb_b.jpg"));
				photo.Caption = "Ciutadella";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1261/1214721656_3e50b51adb_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1422/1213857765_dd59feadff_b.jpg"));
				photo.Caption = "Columbus";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1422/1213857765_dd59feadff_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1075/1214720084_9ec6163320_b.jpg"));
				photo.Caption = "Watch Tower";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1075/1214720084_9ec6163320_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1413/1213849449_689f6c5b34_b.jpg"));
				photo.Caption = "White Tower";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1413/1213849449_689f6c5b34_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1310/1213846529_9a1fc08f0f_b.jpg"));
				photo.Caption = "White & Blue";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1310/1213846529_9a1fc08f0f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1320/1213843939_6f5594ffca_b.jpg"));
				photo.Caption = "Jesus, Monte Toro";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1320/1213843939_6f5594ffca_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1290/1213774167_804edea2a8_b.jpg"));
				photo.Caption = "York Minster";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1290/1213774167_804edea2a8_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1340/1214632114_7587edf8dc_b.jpg"));
				photo.Caption = "York Minster";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1340/1214632114_7587edf8dc_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1126/1213763123_b2e0ad8954_b.jpg"));
				photo.Caption = "York Minster";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1126/1213763123_b2e0ad8954_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1157/1213707107_43444cf13b_b.jpg"));
				photo.Caption = "Water Plant";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1157/1213707107_43444cf13b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1033/1082797343_87d812258f_b.jpg"));
				photo.Caption = "Micklegate, York";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1033/1082797343_87d812258f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1106/1083643878_f9082c3f58_b.jpg"));
				photo.Caption = "Tea Rooms";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1106/1083643878_f9082c3f58_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1188/1082780169_3d189a56d5_b.jpg"));
				photo.Caption = "York Minster";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1188/1082780169_3d189a56d5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1274/1083627286_9bb558047b_b.jpg"));
				photo.Caption = "Constantine";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1274/1083627286_9bb558047b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1276/1082759563_41eb4412d7_b.jpg"));
				photo.Caption = "York Minster";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1276/1082759563_41eb4412d7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1326/1082740813_e6c9b5fc87_b.jpg"));
				photo.Caption = "I Do";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1326/1082740813_e6c9b5fc87_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1407/1083588060_c10e3abcb3_b.jpg"));
				photo.Caption = "Hanging Basket";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1407/1083588060_c10e3abcb3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1268/1083499110_80bfba3a27_b.jpg"));
				photo.Caption = "Hover Fly";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1268/1083499110_80bfba3a27_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1022/1082562763_03ac6b462a_b.jpg"));
				photo.Caption = "Pylon";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1022/1082562763_03ac6b462a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1154/1083348824_790abf6c45_b.jpg"));
				photo.Caption = "Drenched Cars";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1154/1083348824_790abf6c45_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1356/1082760752_204933b13c_b.jpg"));
				photo.Caption = "Wedding Cake";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1356/1082760752_204933b13c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1310/1081997284_f45d03e3e9_b.jpg"));
				photo.Caption = "Wedding Rings";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1310/1081997284_f45d03e3e9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1080940411_1dbfd6e577_b.jpg"));
				photo.Caption = "Brides Mother";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1080940411_1dbfd6e577_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1352/1041732121_d1a1dab44b_b.jpg"));
				photo.Caption = "Wedding Roses";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1352/1041732121_d1a1dab44b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1246/1041698487_fd91c19893_b.jpg"));
				photo.Caption = "Butterfly Bridesmaid";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1246/1041698487_fd91c19893_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1168/1042508712_877068e029_b.jpg"));
				photo.Caption = "Bridesmaid Stairs";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1168/1042508712_877068e029_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1406/1037917833_087badcaaf_b.jpg"));
				photo.Caption = "Red & White Night";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1406/1037917833_087badcaaf_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1202/1037911739_a2d684d0d3_b.jpg"));
				photo.Caption = "Red & White Night Portrait";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1202/1037911739_a2d684d0d3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1336/1038760638_42b348543d_b.jpg"));
				photo.Caption = "Red & White Night Sign";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1336/1038760638_42b348543d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1182/1038749330_6eeceea376_b.jpg"));
				photo.Caption = "White Whip Light";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1182/1038749330_6eeceea376_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1121/1038747138_ed4388600e_b.jpg"));
				photo.Caption = "Red Long Exp";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1121/1038747138_ed4388600e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1174/1037890363_5918979b27_b.jpg"));
				photo.Caption = "M6 Sign";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1174/1037890363_5918979b27_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1160/1037797119_e535914f06_b.jpg"));
				photo.Caption = "Sunlit Leaf";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1160/1037797119_e535914f06_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1037/1037789909_d320b9d759_b.jpg"));
				photo.Caption = "Water Covered Leaf";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1037/1037789909_d320b9d759_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1293/1037658807_048f125b28_b.jpg"));
				photo.Caption = "Transparent Light Leaf";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1293/1037658807_048f125b28_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1111/1038457510_2aa6edc766_b.jpg"));
				photo.Caption = "Winter Nettle";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1111/1038457510_2aa6edc766_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1418/1037593249_87a705a0e9_b.jpg"));
				photo.Caption = "Frost Edged Leaf";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1418/1037593249_87a705a0e9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1416/1038320230_14bb31307c_b.jpg"));
				photo.Caption = "Large Sunset";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1416/1038320230_14bb31307c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1382/1038307382_19d06a3c7f_b.jpg"));
				photo.Caption = "Band Stand at Night";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1382/1038307382_19d06a3c7f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1187/1037444897_9c6bb617bd_b.jpg"));
				photo.Caption = "Stafford Street at Night";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1187/1037444897_9c6bb617bd_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1020112582_892713cc72_b.jpg"));
				photo.Caption = "Villa";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1020112582_892713cc72_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1438/1019238601_78d489ab5d_b.jpg"));
				photo.Caption = "Offshore Silhouette 2";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1438/1019238601_78d489ab5d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1234/1020080322_572763d9f1_b.jpg"));
				photo.Caption = "Perfect Circle";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1234/1020080322_572763d9f1_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1016/1019200909_574df6bf54_b.jpg"));
				photo.Caption = "VW Golf";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1016/1019200909_574df6bf54_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1114/1020056296_c4dc0d50b1_b.jpg"));
				photo.Caption = "Spanish Villa House";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1114/1020056296_c4dc0d50b1_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1197/1020049432_4db0f8efd5_b.jpg"));
				photo.Caption = "Bend in the Road";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1197/1020049432_4db0f8efd5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1189/1020036286_f8cf41ac69_b.jpg"));
				photo.Caption = "DoF Blue Chair";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1189/1020036286_f8cf41ac69_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1267/1019167123_e55e1bc56f_b.jpg"));
				photo.Caption = "Villa";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1267/1019167123_e55e1bc56f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1116/1019157385_8faf3e3573_b.jpg"));
				photo.Caption = "Yellow Flower";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1116/1019157385_8faf3e3573_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1191/1019151951_774b7b5402_b.jpg"));
				photo.Caption = "Villa";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1191/1019151951_774b7b5402_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1363/1019139793_b55ce199d0_b.jpg"));
				photo.Caption = "Luxury Spa at La Manga Club";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1363/1019139793_b55ce199d0_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1038/1019127559_0d0a57733f_b.jpg"));
				photo.Caption = "Lonely Boat";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1038/1019127559_0d0a57733f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1329/1019973422_b575f4961c_b.jpg"));
				photo.Caption = "Garden Chair";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1329/1019973422_b575f4961c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1168/1019745098_545403ede6_b.jpg"));
				photo.Caption = "Rust for Sale!";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1168/1019745098_545403ede6_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1394/1019735146_8875d55079_b.jpg"));
				photo.Caption = "Safety";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1394/1019735146_8875d55079_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1223/1019727992_af62bf470d_b.jpg"));
				photo.Caption = "Monster VW";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1223/1019727992_af62bf470d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1297/1019721148_796385dcb6_b.jpg"));
				photo.Caption = "Yeah it is!";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1297/1019721148_796385dcb6_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1093/1018853687_a162a984ca_b.jpg"));
				photo.Caption = "VDub Little Buggers Club";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1093/1018853687_a162a984ca_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1069/1018846077_9576590764_b.jpg"));
				photo.Caption = "Shiny Alloys";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1069/1018846077_9576590764_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1229/1018838599_3f581eb852_b.jpg"));
				photo.Caption = "VW Headlights";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1229/1018838599_3f581eb852_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1187/1018830961_a4f97ea5a7_b.jpg"));
				photo.Caption = "VW Vinyl";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1187/1018830961_a4f97ea5a7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1224/1019677202_116cc992e5_b.jpg"));
				photo.Caption = "VW Dude";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1224/1019677202_116cc992e5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1207/1019670340_a6268b294a_b.jpg"));
				photo.Caption = "Large VW Logo";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1207/1019670340_a6268b294a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1275/1018793735_a4172e3675_b.jpg"));
				photo.Caption = "IMG_1781";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1275/1018793735_a4172e3675_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1129/1018777423_1238c3249a_b.jpg"));
				photo.Caption = "IMG_1770";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1129/1018777423_1238c3249a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1144/1019556448_4cd8a965f5_b.jpg"));
				photo.Caption = "Piano DoF";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1144/1019556448_4cd8a965f5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1373/1018689833_03ae27b7c9_b.jpg"));
				photo.Caption = "Festive Ice";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1373/1018689833_03ae27b7c9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1132/1019542722_3723ac58f2_b.jpg"));
				photo.Caption = "Festivities";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1132/1019542722_3723ac58f2_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1162/1019517888_7982bcd84f_b.jpg"));
				photo.Caption = "Christmas Lights";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1162/1019517888_7982bcd84f_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1081/1019239012_d174b5daf3_b.jpg"));
				photo.Caption = "Water Steps";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1081/1019239012_d174b5daf3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1241/1019218632_10255f1d43_b.jpg"));
				photo.Caption = "Water Steps Frozen in Time";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1241/1019218632_10255f1d43_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1029/1018206545_6e8e0ec1e5_b.jpg"));
				photo.Caption = "Daffo your Dill";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1029/1018206545_6e8e0ec1e5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1261/1018910192_48fe1f5b6b_b.jpg"));
				photo.Caption = "Cock";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1261/1018910192_48fe1f5b6b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1228/1017960551_1a3d1b0a9c_b.jpg"));
				photo.Caption = "Fountain CU";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1228/1017960551_1a3d1b0a9c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1077/1017926975_1c9e8953e3_b.jpg"));
				photo.Caption = "Chatsworth";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1077/1017926975_1c9e8953e3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1213/1018743552_7d0c410416_b.jpg"));
				photo.Caption = "Gardins";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1213/1018743552_7d0c410416_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1342/1017867885_5e90984ce5_b.jpg"));
				photo.Caption = "Water Sun Steps";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1342/1017867885_5e90984ce5_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1047/1018622228_056b0dfcbf_b.jpg"));
				photo.Caption = "Daffodil CU";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1047/1018622228_056b0dfcbf_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1415/1018615064_937b34cca7_b.jpg"));
				photo.Caption = "Twin Daffodil";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1415/1018615064_937b34cca7_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1436/1017097897_73a265e346_b.jpg"));
				photo.Caption = "Bird Sculpture";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1436/1017097897_73a265e346_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1004/1017051687_e281a77270_b.jpg"));
				photo.Caption = "Flower with Water Drops";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1004/1017051687_e281a77270_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1150/1017031825_53ba6f6ab9_b.jpg"));
				photo.Caption = "Bats";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1150/1017031825_53ba6f6ab9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1381/1017008087_c0eb892b47_b.jpg"));
				photo.Caption = "Stream Slow";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1381/1017008087_c0eb892b47_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1067/1017708680_82a427f18e_b.jpg"));
				photo.Caption = "Dog Sculpture";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1067/1017708680_82a427f18e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1438/1016801251_4817ff3b67_b.jpg"));
				photo.Caption = "Beads of Water on Grass";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1438/1016801251_4817ff3b67_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1398/1017654416_d9e7ad4370_b.jpg"));
				photo.Caption = "Beads of Water on Grass CU";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1398/1017654416_d9e7ad4370_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1035/1011726328_79c46b6af3_b.jpg"));
				photo.Caption = "Global Fence";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1035/1011726328_79c46b6af3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1129/1011721614_8a5de7701c_b.jpg"));
				photo.Caption = "Barb Wire Fence";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1129/1011721614_8a5de7701c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1152/1010860101_e9ac61fe2c_b.jpg"));
				photo.Caption = "Barb Wire";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1152/1010860101_e9ac61fe2c_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1276/1011685542_9e5bc95aaf_b.jpg"));
				photo.Caption = "Church Light";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1276/1011685542_9e5bc95aaf_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1008/1011667622_94607012d3_b.jpg"));
				photo.Caption = "My Car in Snow";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1008/1011667622_94607012d3_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1408/1011656768_62713e265a_b.jpg"));
				photo.Caption = "Frost";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1408/1011656768_62713e265a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1351/1011575570_433c62399d_b.jpg"));
				photo.Caption = "Memorial";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1351/1011575570_433c62399d_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1284/1011559728_ad59fa81ce_b.jpg"));
				photo.Caption = "Cracked Cloud Sun";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1284/1011559728_ad59fa81ce_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1045/1010555983_6cca0484e9_b.jpg"));
				photo.Caption = "Frosted Grass CU";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1045/1010555983_6cca0484e9_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1300/1010519417_00b75cc013_b.jpg"));
				photo.Caption = "Water Leaves";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1300/1010519417_00b75cc013_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1227/1010469785_4111f8b6ae_b.jpg"));
				photo.Caption = "Sunset Tree";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1227/1010469785_4111f8b6ae_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1099/1011324558_c8f4802d4b_b.jpg"));
				photo.Caption = "Sunset";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1099/1011324558_c8f4802d4b_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1224/1011283712_5750c5ba8e_b.jpg"));
				photo.Caption = "Mannequin Half Light";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1224/1011283712_5750c5ba8e_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1384/1010423817_56b81c6f24_b.jpg"));
				photo.Caption = "Mannequin Side Light";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1384/1010423817_56b81c6f24_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1429/1011276778_e97457682a_b.jpg"));
				photo.Caption = "Mannequin";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1429/1011276778_e97457682a_q.jpg")));
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1010416375_fe91e5ce22_b.jpg"));
				photo.Caption = "Mannequin DoF";
				photos.Add (photo);
				thumbs.Add (PhotoBrowserPhoto.FromUrl (new NSUrl ("http://farm2.static.flickr.com/1235/1010416375_fe91e5ce22_q.jpg")));

				browser.StartOnGrid = true;
			}

			public void ShowSingleVideo (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				photo = PhotoBrowserPhoto.FromFilePath (NSBundle.MainBundle.PathForResource ("video_thumb", "jpg"));
				photo.VideoUrl = new NSUrl (NSBundle.MainBundle.PathForResource ("video", "mp4"));
				photos.Add (photo);

				browser.EnableGrid = false;
				browser.AutoPlayOnAppear = true;
			}

			public void ShowWebVideos (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				PhotoBrowserPhoto photo;

				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-15/e15/11192696_824079697688618_1761661_n.jpg"));
				photo.VideoUrl = new NSUrl ("https://scontent.cdninstagram.com/hphotos-xpa1/t50.2886-16/11200303_1440130956287424_1714699187_n.mp4");
				photos.Add (photo);
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xpt1/t51.2885-15/s150x150/e15/11192696_824079697688618_1761661_n.jpg"));
				photo.IsVideo = true;
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xaf1/t51.2885-15/e15/11240463_963135443745570_1519872157_n.jpg"));
				photo.VideoUrl = new NSUrl ("https://scontent.cdninstagram.com/hphotos-xfa1/t50.2886-16/11237510_945154435524423_2137519922_n.mp4");
				photos.Add (photo);
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xaf1/t51.2885-15/s150x150/e15/11240463_963135443745570_1519872157_n.jpg"));
				photo.IsVideo = true;
				thumbs.Add (photo);

				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xaf1/t51.2885-15/e15/11313531_1625089227727682_169403963_n.jpg"));
				photo.VideoUrl = new NSUrl ("https://scontent.cdninstagram.com/hphotos-xfa1/t50.2886-16/11336249_1783839318509644_116225363_n.mp4");
				photos.Add (photo);
				photo = PhotoBrowserPhoto.FromUrl (new NSUrl ("https://scontent.cdninstagram.com/hphotos-xaf1/t51.2885-15/s150x150/e15/11313531_1625089227727682_169403963_n.jpg"));
				photo.IsVideo = true;
				thumbs.Add (photo);

				browser.StartOnGrid = true;
			}

			public void ShowLibrary (PhotoBrowser browser)
			{
				photos = new List<IPhoto> ();
				thumbs = new List<IPhoto> ();

				var screen = UIScreen.MainScreen;
				var scale = screen.Scale;

				// Sizing is very rough... more thought required in a real implementation
				var imageSize = Math.Max (screen.Bounds.Width, screen.Bounds.Height) * 1.5f;
				var imageTargetSize = new CGSize (imageSize * scale, imageSize * scale);
				var thumbTargetSize = new CGSize (imageSize / 3.0f * scale, imageSize / 3.0f * scale);
				foreach (var asset in assets) {
					photos.Add (PhotoBrowserPhoto.FromAsset (asset, imageTargetSize));
					thumbs.Add (PhotoBrowserPhoto.FromAsset (asset, thumbTargetSize));
				}
			}
		}
	}
}
