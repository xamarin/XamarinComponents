using System;
using System.Linq;
using CoreGraphics;
using Foundation;
using UIKit;

using AMScrollingNavbar;

namespace AMScrollingNavbarSample
{
	partial class CollectionViewController : ScrollingNavigationViewController, IUICollectionViewDataSource, IUICollectionViewDelegate
	{
		public CollectionViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Collection View";

			NavigationItem.RightBarButtonItem = new UIBarButtonItem (UIBarButtonSystemItem.Add, null, null);
			NavigationController.NavigationBar.BarTintColor = UIColor.FromRGBA (0.91f, 0.3f, 0.24f, 1f);

			var cellSize = (Math.Min (UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height) - 2) / 3;
			collectionView.CollectionViewLayout = new UICollectionViewFlowLayout {
				MinimumInteritemSpacing = 1,
				MinimumLineSpacing = 1,
				ItemSize = new CGSize (cellSize, cellSize),
				ScrollDirection = UICollectionViewScrollDirection.Vertical
			};
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			ScrollingNavigationController?.FollowScrollView (collectionView, 50.0);
		}

		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = (UICollectionViewCell)collectionView.DequeueReusableCell ("CollectionViewCell", indexPath);
			cell.BackgroundColor = UIColor.FromRGBA (0.95f, 0.95f, 0.95f, 1f);
			var label = cell.ContentView.Subviews.FirstOrDefault () as UILabel;
			if (label != null) {
				label.Text = "Cell " + indexPath.Row;
				label.TextColor = UIColor.FromRGBA (0.45f, 0.35f, 0.35f, 1f);
			}
			return cell;
		}

		public nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return 100;
		}

		public override bool ShouldScrollToTop (UIScrollView scrollView)
		{
			ScrollingNavigationController?.ShowNavbar (true);
			return true;
		}
	}
}
