using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace TPKeyboardAvoidingSample
{
	partial class CollectionViewController : UICollectionViewController
	{
		private const string CellIdentifier = "Cell";

		public CollectionViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override nint GetItemsCount (UICollectionView collectionView, nint section)
		{
			return 30;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = (CollectionViewControllerCell)collectionView.DequeueReusableCell (CellIdentifier, indexPath);

			cell.Label.Text = "Label " + indexPath.Row;
			cell.TextField.Placeholder = "Field " + indexPath.Row;

			return cell;
		}
	}
}
