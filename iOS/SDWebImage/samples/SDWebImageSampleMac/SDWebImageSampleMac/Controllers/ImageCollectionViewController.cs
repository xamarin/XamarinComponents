using System;
using AppKit;
using SDWebImageSampleMac.Classes;

namespace SDWebImageSampleMac.Controllers
{
    public class ImageCollectionViewControllerOld : NSViewController
    {
        public CollectionViewDataSource Datasource { get; set; }

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MacCollectionNew.ViewController"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public ImageCollectionViewControllerOld(IntPtr handle) : base(handle)
        {
            
		}

        #endregion

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ConfigureViewCollection();

        }

        private void ConfigureViewCollection()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
