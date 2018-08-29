using System;

using CoreGraphics;
using Foundation;
using UIKit;

using MaterialComponents;

namespace MaterialSample {
	public partial class MenuCollectionViewController : UICollectionViewController, IUICollectionViewDelegateFlowLayout, IInkTouchControllerDelegate {
		InkTouchController inkTouchController;

		protected MenuCollectionViewController (IntPtr handle) : base (handle) { }

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.

			Title = "Material Components for iOS";

			if (UIDevice.CurrentDevice.CheckSystemVersion (11, 0))
				CollectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Always;

			CollectionView.BackgroundColor = UIColor.FromWhiteAlpha (0.9f, 1);

			inkTouchController = new InkTouchController (CollectionView) {
				DelaysInkSpread = true,
				Delegate = this
			};
			inkTouchController.AddInkView ();
		}

		#region Internal Functionality

		InkView GetInkView (UIView view)
		{
			var foundInkView = InkView.GetInjectedInkView (view);
			foundInkView.InkStyle = InkStyle.Bounded;
			foundInkView.InkColor = UIColor.FromWhiteAlpha (0.9f, 0.3f);
			return foundInkView;
		}

		#endregion

		#region UICollectionView Data Source

		[Export ("numberOfSectionsInCollectionView:")]
		public new nint NumberOfSections (UICollectionView collectionView) => 1;
		public override nint GetItemsCount (UICollectionView collectionView, nint section) => SamplesManager.SharedInstance.Components.Length;

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var component = SamplesManager.SharedInstance.Components [indexPath.Row];

			var cell = collectionView.DequeueReusableCell (MenuCollectionViewCell.Key, indexPath) as MenuCollectionViewCell;
			cell.BackgroundColor = UIColor.White;
			cell.SampleTitle = component.Title;

			InkView.GetInjectedInkView (View).CancelAllAnimations (false);

			return cell;
		}

		#endregion

		#region UICollectionView Delegate

		[Export ("collectionView:didSelectItemAtIndexPath:")]
		public new void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var viewController = Storyboard.InstantiateViewController (nameof (SamplesListTableViewController)) as SamplesListTableViewController;
			viewController.Component = SamplesManager.SharedInstance.Components [indexPath.Row];
			NavigationController.PushViewController (viewController, true);
		}

		#endregion

		#region UICollectionView Delegate Flow Layout

		[Export ("collectionView:layout:sizeForItemAtIndexPath:")]
		public CGSize GetSizeForItem (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
		{
			var dividerWidth = 1;
			var safeInsets = 0d;

			if (UIDevice.CurrentDevice.CheckSystemVersion (11, 0))
				safeInsets = View.SafeAreaInsets.Left + View.SafeAreaInsets.Right;

			var columns = 0;
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
				columns = UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait ? 2 : 3;
			else
				columns = 4;

			var dividers = columns + 1;
			var cellWidth = (View.Frame.Width - dividers * dividerWidth - safeInsets) / columns;
			return new CGSize (cellWidth, cellWidth);
		}

		#endregion

		#region InkTouchController Delegate

		[Export ("inkTouchController:shouldProcessInkTouchesAtTouchLocation:")]
		public bool ShouldProcessInkTouches (InkTouchController inkTouchController, CGPoint location) => CollectionView.IndexPathForItemAtPoint (location) != null;

		[Export ("inkTouchController:inkViewAtTouchLocation:")]
		InkView GetInkViewAtTouchLocation (InkTouchController inkTouchController, CGPoint location)
		{
			var indexPath = CollectionView.IndexPathForItemAtPoint (location);

			if (indexPath == null)
				return new InkView ();

			var cell = CollectionView.CellForItem (indexPath);
			return GetInkView (cell);
		}

		#endregion
	}
}

