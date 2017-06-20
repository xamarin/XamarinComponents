using System;
using AppKit;
using Foundation;
using SDWebImageSampleMac.DataModel;
using SDWebImageSampleMac.Views;

namespace SDWebImageSampleMac.Controllers
{
    public class ImageListController : NSCollectionViewItem
    {
		#region Private Variables
		/// <summary>
		/// The person that will be displayed.
		/// </summary>
		private ImageModel _model;
		#endregion

		public new SDImageView View
		{
			get
			{
				return (SDImageView)base.View;
			}
		}

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
    }
}
