using System;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;

using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nint = System.Int32;
using nuint = System.UInt32;
using nfloat = System.Single;
#endif

namespace SlackHQ
{
	// @interface SLKInputAccessoryView : UIView
	[BaseType (typeof(UIView), Name = "SLKInputAccessoryView")]
	interface SlackInputAccessoryView
	{
		// @property (readonly, nonatomic, weak) UIView * _Nullable keyboardViewProxy;
		[NullAllowed, Export ("keyboardViewProxy", ArgumentSemantic.Weak)]
		UIView KeyboardViewProxy { get; }
	}

	// @interface SLKTextInputbar : UIToolbar
	[BaseType (typeof(UIToolbar), Name = "SLKTextInputbar")]
	interface SlackTextInputbar
	{
		// extern NSString *const SLKTextInputbarDidMoveNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextInputbarDidMoveNotification", "__Internal")]
		NSString DidMoveNotification { get; }


		// @property (nonatomic, strong) SLKTextView * textView;
		[Export ("textView", ArgumentSemantic.Strong)]
		SlackTextView TextView { get; set; }

		// @property (nonatomic, strong) SLKInputAccessoryView * inputAccessoryView;
		[Export ("inputAccessoryView", ArgumentSemantic.Strong)]
		SlackInputAccessoryView SlackInputAccessoryView { get; }

		// @property (nonatomic, strong) UIButton * leftButton;
		[Export ("leftButton", ArgumentSemantic.Strong)]
		UIButton LeftButton { get; set; }

		// @property (nonatomic, strong) UIButton * rightButton;
		[Export ("rightButton", ArgumentSemantic.Strong)]
		UIButton RightButton { get; set; }

		// @property (readwrite, nonatomic) BOOL autoHideRightButton;
		[Export ("autoHideRightButton")]
		bool AutoHideRightButton { get; set; }
        
        // @property (nonatomic, assign) BOOL bounces;
		[Export ("bounces", ArgumentSemantic.Assign)]
		bool Bounces { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets contentInset;
		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }

		// @property (readonly, nonatomic) CGFloat minimumInputbarHeight;
		[Export ("minimumInputbarHeight")]
		nfloat MinimumInputbarHeight { get; }

		// @property (readonly, nonatomic) CGFloat appropriateHeight;
		[Export ("appropriateHeight")]
		nfloat AppropriateHeight { get; }

		// -(instancetype)initWithTextViewClass:(Class)textViewClass;
		[Export ("initWithTextViewClass:")]
		IntPtr Constructor (Class textViewClass);

		// @property (nonatomic, strong) UIView * editorContentView;
		[Export ("editorContentView", ArgumentSemantic.Strong)]
		UIView EditorContentView { get; set; }

		// @property (nonatomic, strong) UILabel * editorTitle;
		[Export ("editorTitle", ArgumentSemantic.Strong)]
		UILabel EditorTitle { get; set; }

		// @property (nonatomic, strong) UIButton * editorLeftButton;
		[Export ("editorLeftButton", ArgumentSemantic.Strong)]
		UIButton EditorLeftButton { get; set; }

		// @property (nonatomic, strong) UIButton * editorRightButton;
		[Export ("editorRightButton", ArgumentSemantic.Strong)]
		UIButton EditorRightButton { get; set; }

		// @property (assign, nonatomic) CGFloat editorContentViewHeight;
		[Export ("editorContentViewHeight")]
		nfloat EditorContentViewHeight { get; set; }

		// @property (getter = isEditing, nonatomic) BOOL editing;
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; set; }

		// -(BOOL)canEditText:(NSString *)text;
		[Export ("canEditText:")]
		bool CanEditText (string text);

		// -(void)beginTextEditing;
		[Export ("beginTextEditing")]
		void BeginTextEditing ();

		// -(void)endTextEdition;
		[Export ("endTextEdition")]
		void EndTextEditing ();

		// @property (readonly, nonatomic) UILabel * charCountLabel;
		[Export ("charCountLabel")]
		UILabel CharCountLabel { get; }

		// @property (readwrite, nonatomic) NSUInteger maxCharCount;
		[Export ("maxCharCount")]
		nuint MaxCharCount { get; set; }

		// @property (assign, nonatomic) SLKCounterStyle counterStyle;
		[Export ("counterStyle", ArgumentSemantic.Assign)]
		CounterStyle CounterStyle { get; set; }

		// @property (assign, nonatomic) SLKCounterPosition counterPosition;
		[Export ("counterPosition", ArgumentSemantic.Assign)]
		CounterPosition CounterPosition { get; set; }

		// @property (readonly, nonatomic) BOOL limitExceeded;
		[Export ("limitExceeded")]
		bool LimitExceeded { get; }

		// @property (readwrite, nonatomic, strong) UIColor * charCountLabelNormalColor;
		[Export ("charCountLabelNormalColor", ArgumentSemantic.Strong)]
		UIColor CharCountLabelNormalColor { get; set; }

		// @property (readwrite, nonatomic, strong) UIColor * charCountLabelWarningColor;
		[Export ("charCountLabelWarningColor", ArgumentSemantic.Strong)]
		UIColor CharCountLabelWarningColor { get; set; }
	}

	// @interface SLKTextView : UITextView
	[BaseType (typeof(UITextView), Name = "SLKTextView")]
	interface SlackTextView
	{
		// extern NSString *const SLKTextViewTextWillChangeNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewTextWillChangeNotification", "__Internal")]
		NSString TextWillChangeNotification { get; }

		// extern NSString *const SLKTextViewContentSizeDidChangeNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewContentSizeDidChangeNotification", "__Internal")]
		NSString ContentSizeDidChangeNotification { get; }

		// extern NSString *const SLKTextViewSelectedRangeDidChangeNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewSelectedRangeDidChangeNotification", "__Internal")]
		NSString SelectedRangeDidChangeNotification { get; }

		// extern NSString *const SLKTextViewDidPasteItemNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewDidPasteItemNotification", "__Internal")]
		NSString DidPasteItemNotification { get; }

		// extern NSString *const SLKTextViewDidShakeNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewDidShakeNotification", "__Internal")]
		NSString DidShakeNotification { get; }

		// extern NSString *const SLKTextViewPastedItemContentType __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewPastedItemContentType", "__Internal")]
		NSString PastedItemContentType { get; }

		// extern NSString *const SLKTextViewPastedItemMediaType __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewPastedItemMediaType", "__Internal")]
		NSString PastedItemMediaType { get; }

		// extern NSString *const SLKTextViewPastedItemData __attribute__((visibility("default")));
		[Static]
		[Field ("SLKTextViewPastedItemData", "__Internal")]
		NSString PastedItemData { get; }


        // @property (nonatomic, weak) id<SLKTextViewDelegate,UITextViewDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		ISlackTextViewDelegate Delegate { get; set; }

		// @property (copy, nonatomic) NSString * placeholder;
		[Export ("placeholder")]
		string Placeholder { get; set; }

		// @property (copy, nonatomic) UIColor * placeholderColor;
		[Export ("placeholderColor", ArgumentSemantic.Copy)]
		UIColor PlaceholderColor { get; set; }

		// @property (readwrite, nonatomic) NSUInteger maxNumberOfLines;
		[Export ("maxNumberOfLines")]
		nuint MaxNumberOfLines { get; set; }

		// @property (readonly, nonatomic) NSUInteger numberOfLines;
		[Export ("numberOfLines")]
		nuint NumberOfLines { get; }

		// @property (nonatomic) SLKPastableMediaType pastableMediaTypes;
		[Export ("pastableMediaTypes", ArgumentSemantic.Assign)]
		PastableMediaType PastableMediaTypes { get; set; }

		// @property (readonly, nonatomic) BOOL isExpanding;
		[Export ("isExpanding")]
		bool IsExpanding { get; }

		// @property (readwrite, nonatomic) BOOL didNotResignFirstResponder;
		[Export ("didNotResignFirstResponder")]
		bool DidNotResignFirstResponder { get; set; }

		// @property (getter = isLoupeVisible, nonatomic) BOOL loupeVisible;
		[Export ("loupeVisible")]
		bool LoupeVisible { [Bind ("isLoupeVisible")] get; set; }

		// @property (readonly, getter = isTrackpadEnabled, nonatomic) BOOL trackpadEnabled;
		[Export ("trackpadEnabled")]
		bool TrackpadEnabled { [Bind ("isTrackpadEnabled")] get; }

		// @property (getter = isTypingSuggestionEnabled, nonatomic) BOOL typingSuggestionEnabled;
		[Export ("typingSuggestionEnabled")]
		bool TypingSuggestionEnabled { [Bind ("isTypingSuggestionEnabled")] get; set; }

		// @property (readwrite, nonatomic) BOOL undoManagerEnabled;
		[Export ("undoManagerEnabled")]
		bool UndoManagerEnabled { get; set; }

		// @property (getter = isDynamicTypeEnabled, nonatomic) BOOL dynamicTypeEnabled;
		[Export ("dynamicTypeEnabled")]
		bool DynamicTypeEnabled { [Bind ("isDynamicTypeEnabled")] get; set; }

		// @property (nonatomic, readonly, getter=isFormattingEnabled) BOOL formattingEnabled;
		[Export ("formattingEnabled")]
		bool FormattingEnabled { [Bind ("isFormattingEnabled")] get; set; }

		// @property (nonatomic, readonly) NSArray *registeredSymbols;
		[Export ("registeredSymbols")]
		string[] RegisteredSymbols { get; }

		// -(void)refreshFirstResponder;
		[Export ("refreshFirstResponder")]
		void RefreshFirstResponder ();

		// -(void)refreshInputViews;
		[Export ("refreshInputViews")]
		void RefreshInputViews ();

		// - (void)didPressArrowKey:(UIKeyCommand *)keyCommand;
		[Export ("didPressArrowKey:")]
		void DidPressArrowKey (UIKeyCommand keyCommand);
        
        // - (void)registerMarkdownFormattingSymbol:(NSString *)symbol withTitle:(NSString *)title;
		[Export ("registerMarkdownFormattingSymbol:withTitle:")]
		void RegisterMarkdownFormattingSymbol (string symbol, string title);

        // - (void)observeKeyInput:(NSString *)input modifiers:(UIKeyModifierFlags)modifiers title:(NSString *)title completion:(void (^)(UIKeyCommand *keyCommand))completion;
		[Export ("observeKeyInput:modifiers:title:completion:")]
		void ObserveKeyInput (string input, UIKeyModifierFlags modifiers, string title, Action<UIKeyCommand> completion);

		// +(CGFloat)pointSizeDifferenceForCategory:(NSString *)category;
		[Static]
		[Export ("pointSizeDifferenceForCategory:")]
		nfloat SlackPointSizeDifference (string category);
	}

	interface ISlackTextViewDelegate {}

	// @protocol SLKTextViewDelegate <UITextViewDelegate>
	[Protocol, Model]
	[BaseType (typeof(UITextViewDelegate), Name = "SLKTextViewDelegate")]
	interface SlackTextViewDelegate
	{
		// @optional - (BOOL)textView:(SLKTextView *)textView shouldOfferFormattingForSymbol:(NSString *)symbol;
		[Export ("textView:shouldOfferFormattingForSymbol:")]
		bool ShouldOfferFormattingForSymbol (SlackTextView textView, string symbol);

		// @optional (BOOL)textView:(SLKTextView *)textView shouldInsertSuffixForFormattingWithSymbol:(NSString *)symbol prefixRange:(NSRange)prefixRange;
		[Export ("textView:shouldInsertSuffixForFormattingWithSymbol:prefixRange:")]
		bool ShouldInsertSuffixForFormattingWithSymbol (SlackTextView textView, string symbol, NSRange prefixRange);
	}

	interface ISlackTypingIndicator { }

	// @protocol SLKTypingIndicatorProtocol <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "SLKTypingIndicatorProtocol")]
	interface SlackTypingIndicator
	{
		// @required @property (getter = isVisible, nonatomic) BOOL visible;
		[Abstract]
		[Export ("visible")]
		bool Visible { [Bind ("isVisible")] get; set; }

		// @optional -(void)dismissIndicator;
		[Export ("dismissIndicator")]
		void DismissIndicator ();
	}

	// @interface SLKTypingIndicatorView : UIView <SLKTypingIndicatorProtocol>
	[BaseType (typeof(UIView), Name = "SLKTypingIndicatorView")]
	interface SlackTypingIndicatorView : ISlackTypingIndicator
	{
		// @property (readwrite, nonatomic) NSTimeInterval interval;
		[Export ("interval")]
		double Interval { get; set; }

		// @property (readwrite, nonatomic) BOOL canResignByTouch;
		[Export ("canResignByTouch")]
		bool CanResignByTouch { get; set; }

		// @property (nonatomic, strong) UIColor * textColor;
		[Export ("textColor", ArgumentSemantic.Strong)]
		UIColor TextColor { get; set; }

		// @property (nonatomic, strong) UIFont * textFont;
		[Export ("textFont", ArgumentSemantic.Strong)]
		UIFont TextFont { get; set; }

		// @property (nonatomic, strong) UIFont * highlightFont;
		[Export ("highlightFont", ArgumentSemantic.Strong)]
		UIFont HighlightFont { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets contentInset;
		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }

		// -(void)insertUsername:(NSString *)username;
		[Export ("insertUsername:")]
		void InsertUsername (string username);

		// -(void)removeUsername:(NSString *)username;
		[Export ("removeUsername:")]
		void RemoveUsername (string username);
	}

	// @interface SLKTextViewController : UIViewController <UITextViewDelegate, UITableViewDelegate, UITableViewDataSource, UICollectionViewDelegate, UICollectionViewDataSource, UIGestureRecognizerDelegate, UIAlertViewDelegate>
	// TODO (remove when CI is updated) [Introduced (PlatformName.iOS, 7, 0)]
	[BaseType (typeof(UIViewController), Name = "SLKTextViewController")]
	interface SlackTextViewController : SlackTextViewDelegate, IUITableViewDelegate, IUITableViewDataSource, IUICollectionViewDelegate, IUICollectionViewDataSource, IUIGestureRecognizerDelegate, IUIAlertViewDelegate
	{
		// extern NSString *const SLKKeyboardWillShowNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKKeyboardWillShowNotification", "__Internal")]
		NSString KeyboardWillShowNotification { get; }

		// extern NSString *const SLKKeyboardDidShowNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKKeyboardDidShowNotification", "__Internal")]
		NSString KeyboardDidShowNotification { get; }

		// extern NSString *const SLKKeyboardWillHideNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKKeyboardWillHideNotification", "__Internal")]
		NSString KeyboardWillHideNotification { get; }

		// extern NSString *const SLKKeyboardDidHideNotification __attribute__((visibility("default")));
		[Static]
		[Field ("SLKKeyboardDidHideNotification", "__Internal")]
		NSString KeyboardDidHideNotification { get; }


		// @property (readonly, nonatomic) UITableView * tableView;
		[Export ("tableView")]
		UITableView TableView { get; }

		// @property (readonly, nonatomic) UICollectionView * collectionView;
		[Export ("collectionView")]
		UICollectionView CollectionView { get; }

		// @property (readonly, nonatomic) UIScrollView * scrollView;
		[Export ("scrollView")]
		UIScrollView ScrollView { get; }

		// @property (readonly, nonatomic) SLKTextInputbar * textInputbar;
		[Export ("textInputbar")]
		SlackTextInputbar TextInputbar { get; }

		// @property (readonly, nonatomic) SLKTypingIndicatorView * typingIndicatorView;
		[Export ("typingIndicatorView")]
		SlackTypingIndicatorView TypingIndicatorView { get; }

		// @property (readonly, nonatomic) UIView<SLKTypingIndicatorProtocol> * typingIndicatorProxyView;
		[Export ("typingIndicatorProxyView")]
		ISlackTypingIndicator TypingIndicatorProxyView { get; }

		// @property (readonly, nonatomic) UIGestureRecognizer * singleTapGesture;
		[Export ("singleTapGesture")]
		UIGestureRecognizer SingleTapGesture { get; }

		// @property (readonly, nonatomic) UIPanGestureRecognizer * verticalPanGesture;
		[Export ("verticalPanGesture")]
		UIPanGestureRecognizer VerticalPanGesture { get; }

		// @property (assign, nonatomic) BOOL bounces;
		[Export ("bounces")]
		bool Bounces { get; set; }

		// @property (assign, nonatomic) BOOL shakeToClearEnabled;
		[Export ("shakeToClearEnabled")]
		bool ShakeToClearEnabled { get; set; }

		// @property (getter = isKeyboardPanningEnabled, assign, nonatomic) BOOL keyboardPanningEnabled;
		[Export ("keyboardPanningEnabled")]
		bool KeyboardPanningEnabled { [Bind ("isKeyboardPanningEnabled")] get; set; }

		// @property (readonly, getter = isExternalKeyboardDetected, nonatomic) BOOL externalKeyboardDetected;
		[Export ("externalKeyboardDetected")]
		bool ExternalKeyboardDetected { [Bind ("isExternalKeyboardDetected")] get; }

		// @property (readonly, getter = isKeyboardUndocked, nonatomic) BOOL keyboardUndocked;
		[Export ("keyboardUndocked")]
		bool KeyboardUndocked { [Bind ("isKeyboardUndocked")] get; }

		// @property (assign, nonatomic) BOOL shouldClearTextAtRightButtonPress;
		[Export ("shouldClearTextAtRightButtonPress")]
		bool ShouldClearTextAtRightButtonPress { get; set; }

		// @property (assign, nonatomic) BOOL shouldScrollToBottomAfterKeyboardShows;
		[Export ("shouldScrollToBottomAfterKeyboardShows")]
		bool ShouldScrollToBottomAfterKeyboardShows { get; set; }

		// @property (getter = isInverted, assign, nonatomic) BOOL inverted;
		[Export ("inverted")]
		bool Inverted { [Bind ("isInverted")] get; set; }

		// @property (getter = isPresentedInPopover, assign, nonatomic) BOOL presentedInPopover;
		[Export ("presentedInPopover")]
		bool PresentedInPopover { [Bind ("isPresentedInPopover")] get; set; }

		// @property (readonly, nonatomic) SLKTextView * textView;
		[Export ("textView")]
		SlackTextView TextView { get; }

		// @property (readonly, nonatomic) UIButton * leftButton;
		[Export ("leftButton")]
		UIButton LeftButton { get; }

		// @property (readonly, nonatomic) UIButton * rightButton;
		[Export ("rightButton")]
		UIButton RightButton { get; }

		// -(instancetype)initWithTableViewStyle:(UITableViewStyle)style __attribute__((objc_designated_initializer));
		[Export ("initWithTableViewStyle:")]
		[DesignatedInitializer]
		IntPtr Constructor (UITableViewStyle style);

		// -(instancetype)initWithCollectionViewLayout:(UICollectionViewLayout *)layout __attribute__((objc_designated_initializer));
		[Export ("initWithCollectionViewLayout:")]
		[DesignatedInitializer]
		IntPtr Constructor (UICollectionViewLayout layout);

		// -(instancetype)initWithScrollView:(UIScrollView *)scrollView __attribute__((objc_designated_initializer));
		[Export ("initWithScrollView:")]
		[DesignatedInitializer]
		IntPtr Constructor (UIScrollView scrollView);

//		// -(instancetype)initWithCoder:(NSCoder *)decoder __attribute__((objc_designated_initializer));
//		[Export ("initWithCoder:")]
//		[DesignatedInitializer]
//		IntPtr Constructor (NSCoder decoder);

		// +(UITableViewStyle)tableViewStyleForCoder:(NSCoder *)decoder;
		[Static]
		[Export ("tableViewStyleForCoder:")]
		UITableViewStyle GetTableViewStyle (NSCoder decoder);

		// +(UICollectionViewLayout *)collectionViewLayoutForCoder:(NSCoder *)decoder;
		[Static]
		[Export ("collectionViewLayoutForCoder:")]
		UICollectionViewLayout GetCollectionViewLayout (NSCoder decoder);

		// -(void)presentKeyboard:(BOOL)animated;
		[Export ("presentKeyboard:")]
		void PresentKeyboard (bool animated);

		// -(void)dismissKeyboard:(BOOL)animated;
		[Export ("dismissKeyboard:")]
		void DismissKeyboard (bool animated);

		// -(BOOL)forceTextInputbarAdjustmentForResponder:(UIResponder *)responder;
		[Export ("forceTextInputbarAdjustmentForResponder:")]
		bool ForceTextInputbarAdjustmentForResponder (UIResponder responder);

		// -(BOOL)ignoreTextInputbarAdjustment __attribute__((objc_requires_super));
		[Export ("ignoreTextInputbarAdjustment")]
//		[RequiresSuper]
		bool IgnoreTextInputbarAdjustment { get; }

		// -(void)didChangeKeyboardStatus:(SLKKeyboardStatus)status;
		[Export ("didChangeKeyboardStatus:")]
		void DidChangeKeyboardStatus (KeyboardStatus status);

		// -(void)textWillUpdate __attribute__((objc_requires_super));
		[Export ("textWillUpdate")]
//		[RequiresSuper]
		void TextWillUpdate ();

		// -(void)textDidUpdate:(BOOL)animated __attribute__((objc_requires_super));
		[Export ("textDidUpdate:")]
//		[RequiresSuper]
		void TextDidUpdate (bool animated);

		// -(void)textSelectionDidChange __attribute__((objc_requires_super));
		[Export ("textSelectionDidChange")]
//		[RequiresSuper]
		void TextSelectionDidChange ();

		// -(void)didPressLeftButton:(id)sender;
		[Export ("didPressLeftButton:")]
		void DidPressLeftButton (NSObject sender);

		// -(void)didPressRightButton:(id)sender __attribute__((objc_requires_super));
		[Export ("didPressRightButton:")]
//		[RequiresSuper]
		void DidPressRightButton (NSObject sender);

		// -(BOOL)canPressRightButton;
		[Export ("canPressRightButton")]
		bool CanPressRightButton { get; }

		// -(void)didPasteMediaContent:(NSDictionary *)userInfo;
		[Export ("didPasteMediaContent:")]
		void DidPasteMediaContent (NSDictionary userInfo);

		// -(BOOL)canShowTypingIndicator;
		[Export ("canShowTypingIndicator")]
		bool CanShowTypingIndicator { get; }

		// -(void)willRequestUndo;
		[Export ("willRequestUndo")]
		void WillRequestUndo ();

		// -(void)didPressReturnKey:(id)sender __attribute__((objc_requires_super));
		[Export ("didPressReturnKey:")]
//		[RequiresSuper]
		void DidPressReturnKey (UIKeyCommand sender);

		// -(void)didPressEscapeKey:(id)sender __attribute__((objc_requires_super));
		[Export ("didPressEscapeKey:")]
//		[RequiresSuper]
		void DidPressEscapeKey (UIKeyCommand sender);

		// -(void)didPressArrowKey:(id)sender __attribute__((objc_requires_super));
		[Export ("didPressArrowKey:")]
//		[RequiresSuper]
		void DidPressArrowKey (UIKeyCommand sender);

		// @property (getter = isTextInputbarHidden, nonatomic) BOOL textInputbarHidden;
		[Export ("textInputbarHidden")]
		bool TextInputbarHidden { [Bind ("isTextInputbarHidden")] get; set; }

		// -(void)setTextInputbarHidden:(BOOL)hidden animated:(BOOL)animated;
		[Export ("setTextInputbarHidden:animated:")]
		void SetTextInputbarHidden (bool hidden, bool animated);

		// @property (readonly, getter = isEditing, nonatomic) BOOL editing;
		[Override]
		[Export ("editing")]
		bool Editing { [Bind ("isEditing")] get; set; }

		// -(void)editText:(NSString *)text __attribute__((objc_requires_super));
		[Export ("editText:")]
//		[RequiresSuper]
		void EditText (string text);

		// -(void)didCommitTextEditing:(id)sender __attribute__((objc_requires_super));
		[Export ("didCommitTextEditing:")]
//		[RequiresSuper]
		void DidCommitTextEditing (NSObject sender);

		// -(void)didCancelTextEditing:(id)sender __attribute__((objc_requires_super));
		[Export ("didCancelTextEditing:")]
//		[RequiresSuper]
		void DidCancelTextEditing (NSObject sender);

		// @property (readonly, nonatomic) UITableView * autoCompletionView;
		[Export ("autoCompletionView")]
		UITableView AutoCompletionView { get; }

		// @property (readonly, getter = isAutoCompleting, nonatomic) BOOL autoCompleting;
		[Export ("autoCompleting")]
		bool AutoCompleting { [Bind ("isAutoCompleting")] get; }

		// @property (readonly, copy, nonatomic) NSString * foundPrefix;
		[Export ("foundPrefix")]
		string FoundPrefix { get; }

		// @property (readonly, nonatomic) NSRange foundPrefixRange;
		[Export ("foundPrefixRange")]
		NSRange FoundPrefixRange { get; }

		// @property (readonly, copy, nonatomic) NSString * foundWord;
		[Export ("foundWord")]
		string FoundWord { get; }

		// @property (readonly, copy, nonatomic) NSArray * registeredPrefixes;
		[Export ("registeredPrefixes", ArgumentSemantic.Copy)]
		string[] RegisteredPrefixes { get; }

		// -(void)registerPrefixesForAutoCompletion:(NSArray *)prefixes;
		[Export ("registerPrefixesForAutoCompletion:")]
		void RegisterPrefixesForAutoCompletion (string[] prefixes);

		// -(void)didChangeAutoCompletionPrefix:(NSString *)prefix andWord:(NSString *)word;
		[Export ("didChangeAutoCompletionPrefix:andWord:")]
		void DidChangeAutoCompletionPrefix (string prefix, string word);

		// -(void)showAutoCompletionView:(BOOL)show;
		[Export ("showAutoCompletionView:")]
		void ShowAutoCompletionView (bool show);

		// -(BOOL)canShowAutoCompletion __attribute__((deprecated("Override -didChangeAutoCompletionPrefix:andWord: instead")));
		[Export ("canShowAutoCompletion")]
		bool CanShowAutoCompletion { get; }

		// -(CGFloat)heightForAutoCompletionView;
		[Export ("heightForAutoCompletionView")]
		nfloat HeightForAutoCompletionView { get; }

		// -(CGFloat)maximumHeightForAutoCompletionView;
		[Export ("maximumHeightForAutoCompletionView")]
		nfloat MaximumHeightForAutoCompletionView { get; }

		// -(void)cancelAutoCompletion;
		[Export ("cancelAutoCompletion")]
		void CancelAutoCompletion ();

		// -(void)acceptAutoCompletionWithString:(NSString *)string;
		[Export ("acceptAutoCompletionWithString:")]
		void AcceptAutoCompletion (string @string);

		// -(void)acceptAutoCompletionWithString:(NSString *)string keepPrefix:(BOOL)keepPrefix;
		[Export ("acceptAutoCompletionWithString:keepPrefix:")]
		void AcceptAutoCompletion (string @string, bool keepPrefix);

		// -(NSString *)keyForTextCaching;
		[Export ("keyForTextCaching")]
		string KeyForTextCaching { get; }

		// -(void)clearCachedText;
		[Export ("clearCachedText")]
		void ClearCachedText ();

		// +(void)clearAllCachedText;
		[Static]
		[Export ("clearAllCachedText")]
		void ClearAllCachedText ();

		// -(void)registerClassForTextView:(Class)aClass;
		[Export ("registerClassForTextView:")]
		void RegisterClassForTextView (Class aClass);

		// -(void)registerClassForTypingIndicatorView:(Class)aClass;
		[Export ("registerClassForTypingIndicatorView:")]
		void RegisterClassForTypingIndicatorView (Class aClass);


		// -(BOOL)textView:(UITextView *)textView shouldChangeTextInRange:(NSRange)range replacementText:(NSString *)text __attribute__((objc_requires_super));
		[Export ("textView:shouldChangeTextInRange:replacementText:")]
//		[RequiresSuper]
		bool ShouldChangeText (UITextView textView, NSRange range, string text);

        // - (BOOL)textView:(SLKTextView *)textView shouldInsertSuffixForFormattingWithSymbol:(NSString *)symbol prefixRange:(NSRange)prefixRange NS_REQUIRES_SUPER;
		[Export ("textView:shouldInsertSuffixForFormattingWithSymbol:prefixRange:")]
//		[RequiresSuper]
		bool ShouldInsertSuffixForFormattingWithSymbol (SlackTextView textView, string symbol, NSRange prefixRange);

		// -(BOOL)scrollViewShouldScrollToTop:(UIScrollView *)scrollView __attribute__((objc_requires_super));
		[Export ("scrollViewShouldScrollToTop:")]
//		[RequiresSuper]
		bool ShouldScrollToTop (UIScrollView scrollView);

		// -(void)scrollViewDidEndDragging:(UIScrollView *)scrollView willDecelerate:(BOOL)decelerate __attribute__((objc_requires_super));
		[Export ("scrollViewDidEndDragging:willDecelerate:")]
//		[RequiresSuper]
		void DraggingEnded (UIScrollView scrollView, bool decelerate);

		// -(void)scrollViewDidEndDecelerating:(UIScrollView *)scrollView __attribute__((objc_requires_super));
		[Export ("scrollViewDidEndDecelerating:")]
//		[RequiresSuper]
		void DecelerationEnded (UIScrollView scrollView);

		// -(void)scrollViewDidScroll:(UIScrollView *)scrollView __attribute__((objc_requires_super));
		[Export ("scrollViewDidScroll:")]
//		[RequiresSuper]
		void Scrolled (UIScrollView scrollView);


		// -(BOOL)gestureRecognizerShouldBegin:(UIGestureRecognizer *)gestureRecognizer __attribute__((objc_requires_super));
		[Export ("gestureRecognizerShouldBegin:")]
//		[RequiresSuper]
		bool ShouldBegin (UIGestureRecognizer gestureRecognizer);


		// -(void)alertView:(UIAlertView *)alertView clickedButtonAtIndex:(NSInteger)buttonIndex __attribute__((objc_requires_super));
		[Export ("alertView:clickedButtonAtIndex:")]
//		[RequiresSuper]
		void Clicked (UIAlertView alertView, nint buttonIndex);


		// -(void)loadView __attribute__((objc_requires_super));
		[Override]
		[Export ("loadView")]
//		[RequiresSuper]
		void LoadView ();

		// -(void)viewDidLoad __attribute__((objc_requires_super));
		[Override]
		[Export ("viewDidLoad")]
//		[RequiresSuper]
		void ViewDidLoad ();

		// -(void)viewWillAppear:(BOOL)animated __attribute__((objc_requires_super));
		[Override]
		[Export ("viewWillAppear:")]
//		[RequiresSuper]
		void ViewWillAppear (bool animated);

		// -(void)viewDidAppear:(BOOL)animated __attribute__((objc_requires_super));
		[Override]
		[Export ("viewDidAppear:")]
//		[RequiresSuper]
		void ViewDidAppear (bool animated);

		// -(void)viewWillDisappear:(BOOL)animated __attribute__((objc_requires_super));
		[Override]
		[Export ("viewWillDisappear:")]
//		[RequiresSuper]
		void ViewWillDisappear (bool animated);

		// -(void)viewDidDisappear:(BOOL)animated __attribute__((objc_requires_super));
		[Override]
		[Export ("viewDidDisappear:")]
//		[RequiresSuper]
		void ViewDidDisappear (bool animated);

		// -(void)viewWillLayoutSubviews __attribute__((objc_requires_super));
		[Override]
		[Export ("viewWillLayoutSubviews")]
//		[RequiresSuper]
		void ViewWillLayoutSubviews ();

		// -(void)viewDidLayoutSubviews __attribute__((objc_requires_super));
		[Override]
		[Export ("viewDidLayoutSubviews")]
//		[RequiresSuper]
		void ViewDidLayoutSubviews ();
	}

	// @interface SLKAdditions (SLKTextView)
	[Category]
	[BaseType (typeof(SlackTextView))]
	interface SlackTextViewExtensions
	{
		// -(void)slk_clearText:(BOOL)clearUndo;
		[Export ("slk_clearText:")]
		void SlackClearText (bool clearUndo);

		// -(void)slk_scrollToBottomAnimated:(BOOL)animated;
		[Export ("slk_scrollToBottomAnimated:")]
		void SlackScrollToBottom (bool animated);

		// -(void)slk_scrollToCaretPositonAnimated:(BOOL)animated;
		[Export ("slk_scrollToCaretPositonAnimated:")]
		void SlackScrollToCaretPositon (bool animated);

		// -(void)slk_insertNewLineBreak;
		[Export ("slk_insertNewLineBreak")]
		void SlackInsertNewLineBreak ();

		// -(void)slk_insertTextAtCaretRange:(NSString *)text;
		[Export ("slk_insertTextAtCaretRange:")]
		void SlackInsertText (string text);

		// -(NSRange)slk_insertText:(NSString *)text inRange:(NSRange)range;
		[Export ("slk_insertText:inRange:")]
		NSRange SlackInsertText (string text, NSRange range);

		// -(NSString *)slk_wordAtCaretRange:(NSRangePointer)range;
		[Export ("slk_wordAtCaretRange:")]
		string SlackWord (out NSRange range);

		// - (NSString *)slk_wordAtRange:(NSRange)range rangeInText:(NSRangePointer)rangePointer;
		[Export ("slk_wordAtRange:rangeInText:")]
		string SlackWord (NSRange searchRange, out NSRange range);

		// -(void)slk_prepareForUndo:(NSString *)description;
		[Export ("slk_prepareForUndo:")]
		void SlackPrepareForUndo (string description);
	}

	// @interface SLKAdditions (UIScrollView)
	[Category]
	[BaseType (typeof(UIScrollView))]
	interface UIScrollViewExtensions
	{
		// @property (readonly, nonatomic) BOOL slk_isAtTop;
		[Export ("slk_isAtTop")]
		bool SlackIsAtTop ();

		// @property (readonly, nonatomic) BOOL slk_isAtBottom;
		[Export ("slk_isAtBottom")]
		bool SlackIsAtBottom ();

		// @property (readonly, nonatomic) CGRect slk_visibleRect;
		[Export ("slk_visibleRect")]
		CGRect SlackVisibleRect ();

		// -(void)slk_scrollToTopAnimated:(BOOL)animated;
		[Export ("slk_scrollToTopAnimated:")]
		void SlackScrollToTop (bool animated);

		// -(void)slk_scrollToBottomAnimated:(BOOL)animated;
		[Export ("slk_scrollToBottomAnimated:")]
		void SlackScrollToBottom (bool animated);

		// -(void)slk_stopScrolling;
		[Export ("slk_stopScrolling")]
		void SlackStopScrolling ();
	}

	// @interface SLKAdditions (UIView)
	[Category]
	[BaseType (typeof(UIView))]
	interface UIViewExtensions
	{
		// -(void)slk_animateLayoutIfNeededWithBounce:(BOOL)bounce options:(UIViewAnimationOptions)options animations:(void (^)(void))animations;
		[Export ("slk_animateLayoutIfNeededWithBounce:options:animations:")]
		void SlackAnimateLayoutIfNeeded (bool bounce, UIViewAnimationOptions options, Action animations);

		// -(void)slk_animateLayoutIfNeededWithBounce:(BOOL)bounce options:(UIViewAnimationOptions)options animations:(void (^)(void))animations completion:(void (^)(BOOL))completion;
		[Export ("slk_animateLayoutIfNeededWithBounce:options:animations:completion:")]
		void SlackAnimateLayoutIfNeeded (bool bounce, UIViewAnimationOptions options, Action animations, Action<bool> completion);

		// -(void)slk_animateLayoutIfNeededWithDuration:(NSTimeInterval)duration bounce:(BOOL)bounce options:(UIViewAnimationOptions)options animations:(void (^)(void))animations completion:(void (^)(BOOL))completion;
		[Export ("slk_animateLayoutIfNeededWithDuration:bounce:options:animations:completion:")]
		void SlackAnimateLayoutIfNeeded (double duration, bool bounce, UIViewAnimationOptions options, Action animations, Action<bool> completion);

		// -(NSArray *)slk_constraintsForAttribute:(NSLayoutAttribute)attribute;
		[Export ("slk_constraintsForAttribute:")]
		NSLayoutConstraint[] SlackConstraintsForAttribute (NSLayoutAttribute attribute);

		// -(NSLayoutConstraint *)slk_constraintForAttribute:(NSLayoutAttribute)attribute firstItem:(id)first secondItem:(id)second;
		[Export ("slk_constraintForAttribute:firstItem:secondItem:")]
		NSLayoutConstraint SlackConstraintForAttribute (NSLayoutAttribute attribute, NSObject first, NSObject second);
	}

	// @interface SLKAdditions (UIResponder)
	[Category]
	[BaseType (typeof(UIResponder))]
	interface UIResponderExtensions
	{
		// +(instancetype)slk_currentFirstResponder;
		[Static]
		[Export ("slk_currentFirstResponder")]
		UIResponder SlackCurrentFirstResponder ();
	}
}
