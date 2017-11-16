using System;
using System.Collections.Generic;
using Xamarin.Themes;
using TrackBeamTheme_Sample_iOS.Model;
using Xamarin.Themes.Core;
using Foundation;
using UIKit;

namespace TrackBeamTheme_Sample_iOS.Views.iPhone {
	public partial class ThemeListController : UIViewController {
		List<Track> tracks;
		const string CellIdentifier = "ThemeListCell";

		public ThemeListController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.Title = "Artists";

			tracks = DataLoader.LoadSampleData ();
			tableListView.WeakDelegate = this;
			tableListView.WeakDataSource = this;

			tableListView.BackgroundColor = UIColor.Clear;
			tableListView.SeparatorStyle = UITableViewCellSeparatorStyle.None;

			var itemBtn = new UIBarButtonItem ("Elements", UIBarButtonItemStyle.Plain, null);
			itemBtn.Clicked += (object sender, EventArgs e) => 
			{
				UINavigationController nav = (UINavigationController)this.Storyboard.InstantiateViewController(@"ElemNav");

				this.PresentViewController(nav,true,null);
			};

			NavigationItem.SetLeftBarButtonItem (itemBtn, false);

			ThemeManager.Current.Apply(View);

		}
			
		[Export ("tableView:cellForRowAtIndexPath:")]
		UITableViewCell CellForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var track = tracks [indexPath.Row];

			var cell = tableView.DequeueReusableCell (CellIdentifier) as ThemeListCell;
			
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			cell.Init (track);
			
			return cell;
		}

		[Export ("tableView:heightForRowAtIndexPath:")]
		nfloat HeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 77;
		}

		[Export ("tableView:numberOfRowsInSection:")]
		nint NumberOfRows (UITableView tableView, nint section)
		{
			return tracks.Count;
		}

		[Export ("tableView:didSelectRowAtIndexPath:")]
		void DidSelectRow (UITableView tableView, NSIndexPath indexPath)
		{
			PerformSegue ("detail", this);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);
			var detail = segue.DestinationViewController as DetailThemeController;

			var track = tracks [tableListView.IndexPathForSelectedRow.Row];

			if (detail != null) 
				detail.SetTrack (track);

		}
	}
}
