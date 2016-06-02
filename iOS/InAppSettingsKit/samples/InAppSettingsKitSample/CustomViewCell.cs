using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace InAppSettingsKitSample
{
	partial class CustomViewCell : UITableViewCell
	{
		public CustomViewCell (IntPtr handle)
			: base (handle)
		{
		}

		public UITextView TextView {
			get { return textView; }
		}
	}
}
