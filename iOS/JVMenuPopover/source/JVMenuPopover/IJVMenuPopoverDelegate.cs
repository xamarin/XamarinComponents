using System;
using Foundation;

namespace JVMenuPopover
{
	public interface IJVMenuPopoverDelegate
	{
		/// <summary>
		/// Called when a menu item is picked
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="indexPath">Index path.</param>
		void MenuPopOverRowSelected(JVMenuPopoverView sender,NSIndexPath indexPath);
	}
}

