using System;
using Foundation;

namespace AMScrollingNavbarSample
{
	[Register ("LargeTitleViewController")]
	partial class LargeTitleViewController : ScrollViewController
	{
		public LargeTitleViewController (IntPtr handle)
			: base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Title = "Large Title";

			if (NavigationController?.NavigationBar != null) {
				NavigationController.NavigationBar.PrefersLargeTitles = true;
			}
		}
	}
}
