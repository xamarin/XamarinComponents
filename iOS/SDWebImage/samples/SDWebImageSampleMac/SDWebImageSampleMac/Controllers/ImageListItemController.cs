using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using SDWebImageSampleMac.DataModel;

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
    }
}
