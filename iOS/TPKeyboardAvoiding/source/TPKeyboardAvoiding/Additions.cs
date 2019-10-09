using System.ComponentModel;

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
