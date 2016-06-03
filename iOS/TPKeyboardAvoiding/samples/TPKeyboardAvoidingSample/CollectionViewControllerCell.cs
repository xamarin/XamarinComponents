using System;
using UIKit;

namespace TPKeyboardAvoidingSample
{
	partial class CollectionViewControllerCell : UICollectionViewCell
	{
		public CollectionViewControllerCell (IntPtr handle)
			: base (handle)
		{
		}

		public UILabel Label { get { return label; } }

		public UITextField  TextField { get { return textField; } }
	}
}
