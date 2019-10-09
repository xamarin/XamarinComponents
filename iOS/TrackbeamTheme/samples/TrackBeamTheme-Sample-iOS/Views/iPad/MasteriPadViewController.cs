
using System;
using System.Drawing;

using Xamarin.Themes.TrackBeam;
using Xamarin.Themes.Core;
using System.Collections.Generic;
using TrackBeamTheme_Sample_iOS.Model;
using TrackBeamTheme_Sample_iOS.Views.iPhone;
using Foundation;
using UIKit;

namespace TrackBeamTheme_Sample_iOS.Views.iPad
{
	public partial class MasteriPadViewController : UIViewController, IUITableViewDataSource
	{
		List<Track> tracks;

		public MasteriPadViewController(IntPtr handle) : base(handle)
		{
			tracks = DataLoader.LoadSampleData ();

			this.Title = "Tracks";
		}

		static bool UserInterfaceIdiomIsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}
			
		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			//ThemeManager.Current.Apply(tracksTable);
			tracksTable.WeakDelegate = this;
			tracksTable.WeakDataSource = this;
			SetNavigationBarBackground(this.InterfaceOrientation);
			SetBackgroundImage(this.InterfaceOrientation);

			tracksTable.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			tracksTable.SelectRow(NSIndexPath.FromItemSection(0,0), true, UITableViewScrollPosition.None);
			RowSelected(tracksTable, NSIndexPath.FromItemSection(0,0));

			var itemBtn = new UIBarButtonItem ("Elements", UIBarButtonItemStyle.Plain, null);
			itemBtn.Clicked += (object sender, EventArgs e) => 
			{
				var elements = (ElementsFormController)this.Storyboard.InstantiateViewController(@"elementsForm");

//				var elements = new ElementThemeController();
				elements.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

				var nav = new UINavigationController(elements);
				nav.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;

				this.PresentViewController(nav,true,null);
			};

			NavigationItem.SetLeftBarButtonItem (itemBtn, false);
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);

			SetNavigationBarBackground(toInterfaceOrientation);
			SetBackgroundImage(toInterfaceOrientation);
		}

		public void SetNavigationBarBackground(UIInterfaceOrientation orientation)
		{

			var backgroundImage = ThemeManager.IsPortrait(orientation) ? ThemeManager.Current.NavigationBackground(UIBarMetrics.Default) : 
				ThemeManager.Current.NavigationBackground(UIBarMetrics.LandscapePhone);

			this.NavigationController.NavigationBar.SetBackgroundImage(backgroundImage, UIBarMetrics.Default);
			this.NavigationController.NavigationBar.SetBackgroundImage(backgroundImage, UIBarMetrics.LandscapePhone);

		}

		public void SetBackgroundImage(UIInterfaceOrientation orientation)
		{
			ThemeManager.Current.Apply(this, orientation);
		}


		[Export("tableView:numberOfRowsInSection:")]
		public nint RowsInSection(UITableView tableView, nint section)
		{
			return tracks.Count;
		}

		[Export("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(@"SongCell");

			cell.BackgroundColor = indexPath.Row % 2 == 0 ?  UIColor.FromWhiteAlpha(0.0f,0.12f) : UIColor.Clear;
			cell.SelectionStyle = UITableViewCellSelectionStyle.Blue;

			Track song = tracks[indexPath.Row];
			cell.TextLabel.Text = String.Format(@"{0}. {1}", indexPath.Row+1, song.Name);
			cell.TextLabel.TextColor =   UIColor.FromWhiteAlpha(1.0f,1.0f);
			cell.TextLabel.HighlightedTextColor = UIColor.Black;
			cell.TextLabel.BackgroundColor = UIColor.Clear;

			return cell;

		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public virtual void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			Track track = tracks[indexPath.Row];

			trackImage.Image = track.LargeImage;
		}
	}
}

