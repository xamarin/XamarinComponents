using System;
using UIKit;
using Foundation;

namespace JVMenuPopover
{
	/// <summary>
	/// IJV menu delegate.
	/// </summary>
	public interface IJVMenuDelegate
	{
		/// <summary>
		/// Did pick the item
		/// </summary>
		/// <param name="item">Item.</param>
		void DidPickItem(UINavigationController navController, JVMenuItem item);

		/// <summary>
		/// Sets the new view controller.
		/// </summary>
		/// <param name="navController">Nav controller.</param>
		/// <param name="indexPath">Index path.</param>
		void SetNewViewController(UINavigationController navController, NSIndexPath indexPath);
	
		/// <summary>
		/// Closes the menu.
		/// </summary>
		/// <param name="menuController">Menu controller.</param>
		void CloseMenu(JVMenuPopoverViewController menuController);

	}
}

