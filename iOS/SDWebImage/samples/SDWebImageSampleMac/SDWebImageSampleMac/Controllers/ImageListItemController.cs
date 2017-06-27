using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using SDWebImage;
using SDWebImageSampleMac.DataModel;
using SDWebImageSampleMac.Classes;

namespace SDWebImageSampleMac.Controllers
{
    public partial class ImageListItemController : NSCollectionViewItem
    {
        #region Private Variables
        /// <summary>
        /// The person that will be displayed.
        /// </summary>
        private ImageModel _model;
        #endregion

        [Export("Model")]
        public ImageModel Model
        {
            get { return _model; }
            set
            {
                WillChangeValue("Model");
                _model = value;
                DidChangeValue("Model");

                if (_model != null)
                {
                    LoadImage();
                }
            }
        }

        public NSColor BackgroundColor
        {
            get { return Background.FillColor; }
            set { Background.FillColor = value; }
        }

		public override bool Selected
		{
			get
			{
				return base.Selected;
			}
			set
			{
				base.Selected = value;

				// Set background color based on the selection state
				if (value)
				{
					BackgroundColor = NSColor.DarkGray;
				}
				else
				{
					BackgroundColor = NSColor.LightGray;
				}
			}
		}

        #region Constructors

        // Called when created from unmanaged code
        public ImageListItemController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ImageListItemController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ImageListItemController() : base("ImageListItem", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
          // BackgroundColor = NSColor.DarkGray;
        }

        void LoadImage()
        {
            View.ImageView.SetImage(new NSUrl(Model.ImageUrl));
        }
        #endregion

        //strongly typed view accessor
        public new ImageListItem View
        {
            get
            {
                return (ImageListItem)base.View;
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            BackgroundColor = NSColor.LightGray;

        }

        public void DidDoubleClick()
        {
            if (this.CollectionView.Delegate != null && this.CollectionView.Delegate is CollectionViewDelegate)
            {
                ((CollectionViewDelegate)this.CollectionView.Delegate).DidDoubleClick((Model));
            }

        }
    }
}
