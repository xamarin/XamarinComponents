using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using SDWebImageSampleMac.DataModel;

namespace SDWebImageSampleMac
{
    public partial class ImageViewerController : AppKit.NSViewController
    {
        public NSViewController PresentingController
        {
            get;
            set;
        }

        public ImageModel Model
        {
            get;
            set;
        }

        #region Constructors

        // Called when created from unmanaged code
        public ImageViewerController(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ImageViewerController(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Call to load from the XIB/NIB file
        public ImageViewerController() : base("ImageViewer", NSBundle.MainBundle)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion

        //strongly typed view accessor
        public new ImageViewer View
        {
            get
            {
                return (ImageViewer)base.View;
            }
        }

        partial void CloseWindow(NSButton sender)
        {
            
            PresentingController.DismissViewController(this);
        }
    }
}
