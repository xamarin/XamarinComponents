using System;
using AppKit;
using Foundation;

namespace SDWebImageSampleMac.Classes
{
	public class CollectionViewDelegate : NSCollectionViewDelegateFlowLayout
	{
		#region Computed Properties
		/// <summary>
		/// Gets or sets the parent view controller.
		/// </summary>
		/// <value>The parent view controller.</value>
		public ViewController ParentViewController { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MacCollectionNew.CollectionViewDelegate"/> class.
		/// </summary>
		/// <param name="parentViewController">Parent view controller.</param>
		public CollectionViewDelegate(ViewController parentViewController)
		{
			// Initialize
			ParentViewController = parentViewController;
		}
		#endregion

		#region Override Methods
		/// <summary>
		/// Handles one or more items being selected.
		/// </summary>
		/// <param name="collectionView">The parent Collection view.</param>
		/// <param name="indexPaths">The Index paths of the items being selected.</param>
		public override void ItemsSelected(NSCollectionView collectionView, NSSet indexPaths)
		{
			// Dereference path
			var paths = indexPaths.ToArray<NSIndexPath>();
			var index = (int)paths[0].Item;

			// Save the selected item
			//ParentViewController.PersonSelected = ParentViewController.Datasource.Data[index];

		}

		/// <summary>
		/// Handles one or more items being deselected.
		/// </summary>
		/// <param name="collectionView">The parent Collection view.</param>
		/// <param name="indexPaths">The Index paths of the items being deselected.</param>
		public override void ItemsDeselected(NSCollectionView collectionView, NSSet indexPaths)
		{
			// Dereference path
			var paths = indexPaths.ToArray<NSIndexPath>();
			var index = paths[0].Item;

			// Clear selection
			//ParentViewController.PersonSelected = null;
		}
		#endregion
	}
}
