using System;
using System.Runtime.CompilerServices;
using System.ComponentModel;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

namespace TPKeyboardAvoiding
{
	public interface IKeyboardAvoiding
	{
		bool FocusNextTextField ();

		void ScrollToActiveTextField ();
	}

	[DesignTimeVisible(true), Category("Controllers & Objects")]
	partial class TPKeyboardAvoidingCollectionView : IKeyboardAvoiding
	{
	}

	[DesignTimeVisible(true), Category("Controllers & Objects")]
	partial class TPKeyboardAvoidingScrollView : IKeyboardAvoiding
	{
	}

	[DesignTimeVisible(true), Category("Controllers & Objects")]
	partial class TPKeyboardAvoidingTableView : IKeyboardAvoiding
	{
	}
}
