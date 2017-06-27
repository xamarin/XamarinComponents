using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace SDWebImageSampleMac
{
    public partial class ImageViewer : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ImageViewer(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ImageViewer(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        // Shared initialization code
        void Initialize()
        {
        }

        #endregion
    }
}
