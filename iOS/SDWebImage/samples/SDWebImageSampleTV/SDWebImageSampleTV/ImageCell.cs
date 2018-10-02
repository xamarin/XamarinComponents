using System;
using UIKit;

namespace SDWebImageSampleTV
{
	public partial class ImageCell : UICollectionViewCell
	{
		public ImageCell(IntPtr handle)
			: base(handle)
		{
		}

		public UIImageView ImageView => imageView;

		//public UILabel TextLabel => textLabel;
	}
}
