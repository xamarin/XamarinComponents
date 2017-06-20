using System;
using System.Collections.Generic;
using AppKit;
using Foundation;
using SDWebImageSampleMac.Controllers;
using SDWebImageSampleMac.DataModel;

namespace SDWebImageSampleMac.Classes
{
    public class CollectionViewDataSource : NSCollectionViewDataSource
    {

        public List<ImageModel> Items
        {
            get;
            set;
        } 

        public CollectionViewDataSource(NSCollectionView parent) 
		{
            Items = new List<ImageModel>();

			// Attach to collection view
			parent.DataSource = this;

		}

		public override nint GetNumberOfSections(NSCollectionView collectionView)
		{
			// There is only one section in this view
			return 1;
		}

        public override NSCollectionViewItem GetItem(NSCollectionView collectionView, NSIndexPath indexPath)
        {
			var item = collectionView.MakeItem("ImageCell", indexPath) as ImageListController;
			item.Model = Items[(int)indexPath.Item];

			return item;
        }

        public override nint GetNumberofItems(NSCollectionView collectionView, nint section)
        {
            return Items.Count;
        }
    }
}
