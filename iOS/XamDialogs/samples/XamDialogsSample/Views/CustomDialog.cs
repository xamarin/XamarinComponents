using System;
using XamDialogs;
using UIKit;
using CoreGraphics;

namespace XamDialogsSample
{
	public class CustomDialog : XamDialogView
	{


		#region implemented abstract members of DHDialogView

		protected override bool CanSubmit ()
		{
			return true;
		}

		protected override void HandleCancel ()
		{
			
		}

		protected override void HandleSubmit ()
		{
			
		}

		#endregion

		protected override UIKit.UIView ContentView 
		{
			get 
			{
				var aView = new UIView (new CGRect (0, 0, 320, 240));

				aView.BackgroundColor = UIColor.Red;

				return aView;
			}
		}


		public CustomDialog () 
			: base(XamDialogType.CustomView)
		{

		}
	}
}

