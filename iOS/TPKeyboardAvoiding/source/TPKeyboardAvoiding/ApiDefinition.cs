using Foundation;
using UIKit;

namespace TPKeyboardAvoiding
{
	// @interface TPKeyboardAvoidingCollectionView : UICollectionView <UITextFieldDelegate, UITextViewDelegate>
	[BaseType (typeof(UICollectionView), Name = "TPKeyboardAvoidingCollectionView")]
	interface TPKeyboardAvoidingCollectionView : IUITextFieldDelegate, IUITextViewDelegate
	{
		// -(BOOL)focusNextTextField;
		[Export ("focusNextTextField")]
		bool FocusNextTextField ();

		// -(void)scrollToActiveTextField;
		[Export ("scrollToActiveTextField")]
		void ScrollToActiveTextField ();
	}

	// @interface TPKeyboardAvoidingScrollView : UIScrollView <UITextFieldDelegate, UITextViewDelegate>
	[BaseType (typeof(UIScrollView), Name = "TPKeyboardAvoidingScrollView")]
	interface TPKeyboardAvoidingScrollView : IUITextFieldDelegate, IUITextViewDelegate
	{
		// -(void)contentSizeToFit;
		[Export ("contentSizeToFit")]
		void ContentSizeToFit ();

		// -(BOOL)focusNextTextField;
		[Export ("focusNextTextField")]
		bool FocusNextTextField ();

		// -(void)scrollToActiveTextField;
		[Export ("scrollToActiveTextField")]
		void ScrollToActiveTextField ();
	}

	// @interface TPKeyboardAvoidingTableView : UITableView <UITextFieldDelegate, UITextViewDelegate>
	[BaseType (typeof(UITableView), Name = "TPKeyboardAvoidingTableView")]
	interface TPKeyboardAvoidingTableView : IUITextFieldDelegate, IUITextViewDelegate
	{
		// -(BOOL)focusNextTextField;
		[Export ("focusNextTextField")]
		bool FocusNextTextField ();

		// -(void)scrollToActiveTextField;
		[Export ("scrollToActiveTextField")]
		void ScrollToActiveTextField ();
	}

	// @interface TPKeyboardAvoidingAdditions (UIScrollView)
	[Category]
	[BaseType (typeof(UIScrollView))]
	interface UIScrollViewExtensions
	{
		// -(BOOL)TPKeyboardAvoiding_focusNextTextField;
		[Export ("TPKeyboardAvoiding_focusNextTextField")]
		bool FocusNextTextField ();

		// -(void)TPKeyboardAvoiding_scrollToActiveTextField;
		[Export ("TPKeyboardAvoiding_scrollToActiveTextField")]
		void ScrollToActiveTextField ();

//		// -(void)TPKeyboardAvoiding_keyboardWillShow:(NSNotification *)notification;
//		[Export ("TPKeyboardAvoiding_keyboardWillShow:")]
//		void TPKeyboardAvoiding_keyboardWillShow (NSNotification notification);
//
//		// -(void)TPKeyboardAvoiding_keyboardWillHide:(NSNotification *)notification;
//		[Export ("TPKeyboardAvoiding_keyboardWillHide:")]
//		void TPKeyboardAvoiding_keyboardWillHide (NSNotification notification);
//
//		// -(void)TPKeyboardAvoiding_updateContentInset;
//		[Export ("TPKeyboardAvoiding_updateContentInset")]
//		void TPKeyboardAvoiding_updateContentInset ();
//
//		// -(void)TPKeyboardAvoiding_updateFromContentSizeChange;
//		[Export ("TPKeyboardAvoiding_updateFromContentSizeChange")]
//		void TPKeyboardAvoiding_updateFromContentSizeChange ();
//
//		// -(void)TPKeyboardAvoiding_assignTextDelegateForViewsBeneathView:(UIView *)view;
//		[Export ("TPKeyboardAvoiding_assignTextDelegateForViewsBeneathView:")]
//		void TPKeyboardAvoiding_assignTextDelegateForViewsBeneathView (UIView view);
//
//		// -(UIView *)TPKeyboardAvoiding_findFirstResponderBeneathView:(UIView *)view;
//		[Export ("TPKeyboardAvoiding_findFirstResponderBeneathView:")]
//		UIView TPKeyboardAvoiding_findFirstResponderBeneathView (UIView view);
//
//		// -(CGSize)TPKeyboardAvoiding_calculatedContentSizeFromSubviewFrames;
//		[Export ("TPKeyboardAvoiding_calculatedContentSizeFromSubviewFrames")]
//		CGSize TPKeyboardAvoiding_calculatedContentSizeFromSubviewFrames ();
	}
}
