using System;

using AppKit;
using CoreGraphics;
using Foundation;
using SDWebImageSampleMac.Classes;
using SDWebImageSampleMac.Controllers;
using SDWebImageSampleMac.DataModel;

namespace SDWebImageSampleMac
{
    public partial class ViewController : NSViewController
    {
        private ImageModel _imageSelected;

		/// <summary>
		/// Gets or sets the datasource that provides the data to display in the 
		/// Collection View.
		/// </summary>
		/// <value>The datasource.</value>
		public CollectionViewDataSource Datasource { get; set; }


        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            ConfigureCollectionView();
            PopulateWithData();
        }

		[Export("ImageSelected")]
		public ImageModel ImageSelected
		{
			get { return _imageSelected; }
			set
			{
				WillChangeValue("PersonSelected");
				_imageSelected = value;
				DidChangeValue("PersonSelected");
				RaiseSelectionChanged();
			}
		}

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

		private void ConfigureCollectionView()
		{
			ImageCollection.RegisterClassForItem(typeof(ImageListItemController), "ImageCell");

			// Create a flow layout
			var flowLayout = new NSCollectionViewFlowLayout()
			{
				ItemSize = new CGSize(150, 150),
				SectionInset = new NSEdgeInsets(10, 10, 10, 20),
				MinimumInteritemSpacing = 10,
				MinimumLineSpacing = 10
			};
			ImageCollection.WantsLayer = true;

			// Setup collection view
			ImageCollection.CollectionViewLayout = flowLayout;
			ImageCollection.Delegate = new CollectionViewDelegate(this);

		}

		private void PopulateWithData()
		{
			// Make datasource
			Datasource = new CollectionViewDataSource(ImageCollection);

            Datasource.Fill();

			// Populate collection view
			ImageCollection.ReloadData();
		}

		#region Events
		/// <summary>
		/// Selection changed delegate.
		/// </summary>
		public delegate void SelectionChangedDelegate();

		/// <summary>
		/// Occurs when selection changed.
		/// </summary>
		public event SelectionChangedDelegate SelectionChanged;

		/// <summary>
		/// Raises the selection changed event.
		/// </summary>
		internal void RaiseSelectionChanged()
		{
			// Inform caller
			if (this.SelectionChanged != null) SelectionChanged();
		}


		#endregion

		public override void PrepareForSegue(NSStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue(segue, sender);

			// Take action based on segue type
			switch (segue.Identifier)
			{
				case "ViewerSegue":
					var editor = segue.DestinationController as ImageViewerController;
					//editor.Presentor = this;
					//editor.CanEdit = shouldEdit;
					//editor.Person = PersonSelected;
					break;
			}
		}

        public void ViewImage(ImageModel model)
        {


            this.PresentViewControllerAsSheet(new ImageViewerController()
            {
                Model = model,
                PresentingController = this,
            });
        }
	}
}
