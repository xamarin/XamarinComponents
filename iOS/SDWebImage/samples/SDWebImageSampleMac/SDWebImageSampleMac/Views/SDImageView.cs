using System;
using AppKit;
using Foundation;

namespace SDWebImageSampleMac.Views
{
    [Register("SDImageView")]
    public partial class SDImageView : NSView
    {
		#region Constructors
		// Called when created from unmanaged code
		public SDImageView(IntPtr handle) : base(handle)
        {
			Initialize();
		}

		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public SDImageView(NSCoder coder) : base(coder)
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
