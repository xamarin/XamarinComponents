using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace SDWebImageSampleMac.Controllers
{
    public partial class ImageListItem : AppKit.NSView
    {
        #region Constructors

        // Called when created from unmanaged code
        public ImageListItem(IntPtr handle) : base(handle)
        {
            Initialize();
        }

        // Called when created directly from a XIB file
        [Export("initWithCoder:")]
        public ImageListItem(NSCoder coder) : base(coder)
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
