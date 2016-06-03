using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using MapKit;
using CoreLocation;

namespace JSQMessagesViewController
{
	interface IMessagesBubbleSizeCalculating {}

	// @protocol JSQMessagesBubbleSizeCalculating <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesBubbleSizeCalculating")]
	interface MessagesBubbleSizeCalculating
	{
		// @required -(CGSize)messageBubbleSizeForMessageData:(id<JSQMessageData>)messageData atIndexPath:(NSIndexPath *)indexPath withLayout:(JSQMessagesCollectionViewFlowLayout *)layout;
		[Abstract]
		[Export ("messageBubbleSizeForMessageData:atIndexPath:withLayout:")]
		CGSize GetMessageBubbleSize (MessageData messageData, NSIndexPath indexPath, MessagesCollectionViewFlowLayout layout);

		// @required -(void)prepareForResettingLayout:(JSQMessagesCollectionViewFlowLayout *)layout;
		[Abstract]
		[Export ("prepareForResettingLayout:")]
		void PrepareForResettingLayout (MessagesCollectionViewFlowLayout layout);
	}

	// @interface JSQMessagesCollectionViewFlowLayout : UICollectionViewFlowLayout
	[BaseType (typeof(UICollectionViewFlowLayout), Name = "JSQMessagesCollectionViewFlowLayout")]
	interface MessagesCollectionViewFlowLayout
	{
		// @property (readonly, nonatomic) JSQMessagesCollectionView * collectionView;
		[Export ("collectionView")]
		MessagesCollectionView CollectionView { get; }

		// @property (nonatomic, strong) id<JSQMessagesBubbleSizeCalculating> bubbleSizeCalculator;
		[Export ("bubbleSizeCalculator", ArgumentSemantic.Strong)]
		IMessagesBubbleSizeCalculating BubbleSizeCalculator { get; set; }

		// @property (assign, nonatomic) BOOL springinessEnabled;
		[Export ("springinessEnabled")]
		bool SpringinessEnabled { get; set; }

		// @property (assign, nonatomic) NSUInteger springResistanceFactor;
		[Export ("springResistanceFactor")]
		nuint SpringResistanceFactor { get; set; }

		// @property (readonly, nonatomic) CGFloat itemWidth;
		[Export ("itemWidth")]
		nfloat ItemWidth { get; }

		// @property (nonatomic, strong) UIFont * messageBubbleFont;
		[Export ("messageBubbleFont", ArgumentSemantic.Strong)]
		UIFont MessageBubbleFont { get; set; }

		// @property (assign, nonatomic) CGFloat messageBubbleLeftRightMargin;
		[Export ("messageBubbleLeftRightMargin")]
		nfloat MessageBubbleLeftRightMargin { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets messageBubbleTextViewFrameInsets;
		[Export ("messageBubbleTextViewFrameInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets MessageBubbleTextViewFrameInsets { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets messageBubbleTextViewTextContainerInsets;
		[Export ("messageBubbleTextViewTextContainerInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets MessageBubbleTextViewTextContainerInsets { get; set; }

		// @property (assign, nonatomic) CGSize incomingAvatarViewSize;
		[Export ("incomingAvatarViewSize", ArgumentSemantic.Assign)]
		CGSize IncomingAvatarViewSize { get; set; }

		// @property (assign, nonatomic) CGSize outgoingAvatarViewSize;
		[Export ("outgoingAvatarViewSize", ArgumentSemantic.Assign)]
		CGSize OutgoingAvatarViewSize { get; set; }

		// @property (assign, nonatomic) NSUInteger cacheLimit;
		[Export ("cacheLimit")]
		nuint CacheLimit { get; set; }

		// -(CGSize)messageBubbleSizeForItemAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("messageBubbleSizeForItemAtIndexPath:")]
		CGSize GetMessageBubbleSize (NSIndexPath indexPath);

		// -(CGSize)sizeForItemAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("sizeForItemAtIndexPath:")]
		CGSize GetSize (NSIndexPath indexPath);
	}

	interface IMessagesCollectionViewDelegateFlowLayout {}

	// @protocol JSQMessagesCollectionViewDelegateFlowLayout <UICollectionViewDelegateFlowLayout>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesCollectionViewDelegateFlowLayout")]
	interface MessagesCollectionViewDelegateFlowLayout : IUICollectionViewDelegateFlowLayout
	{
		// @optional -(CGFloat)collectionView:(JSQMessagesCollectionView *)collectionView layout:(JSQMessagesCollectionViewFlowLayout *)collectionViewLayout heightForCellTopLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:layout:heightForCellTopLabelAtIndexPath:")]
		nfloat GetCellHeight (MessagesCollectionView collectionView, MessagesCollectionViewFlowLayout collectionViewLayout, NSIndexPath indexPath);

		// @optional -(CGFloat)collectionView:(JSQMessagesCollectionView *)collectionView layout:(JSQMessagesCollectionViewFlowLayout *)collectionViewLayout heightForMessageBubbleTopLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:layout:heightForMessageBubbleTopLabelAtIndexPath:")]
		nfloat GetMessageBubbleTopLabelHeight (MessagesCollectionView collectionView, MessagesCollectionViewFlowLayout collectionViewLayout, NSIndexPath indexPath);

		// @optional -(CGFloat)collectionView:(JSQMessagesCollectionView *)collectionView layout:(JSQMessagesCollectionViewFlowLayout *)collectionViewLayout heightForCellBottomLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:layout:heightForCellBottomLabelAtIndexPath:")]
		nfloat GetCellBottomLabelHeight (MessagesCollectionView collectionView, MessagesCollectionViewFlowLayout collectionViewLayout, NSIndexPath indexPath);

		// @optional -(void)collectionView:(JSQMessagesCollectionView *)collectionView didTapAvatarImageView:(UIImageView *)avatarImageView atIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:didTapAvatarImageView:atIndexPath:")]
		void TappedAvatarImageView (MessagesCollectionView collectionView, UIImageView avatarImageView, NSIndexPath indexPath);

		// @optional -(void)collectionView:(JSQMessagesCollectionView *)collectionView didTapMessageBubbleAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:didTapMessageBubbleAtIndexPath:")]
		void TappedMessageBubble (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @optional -(void)collectionView:(JSQMessagesCollectionView *)collectionView didTapCellAtIndexPath:(NSIndexPath *)indexPath touchLocation:(CGPoint)touchLocation;
		[Export ("collectionView:didTapCellAtIndexPath:touchLocation:")]
		void TappedCell (MessagesCollectionView collectionView, NSIndexPath indexPath, CGPoint touchLocation);

		// @optional -(void)collectionView:(JSQMessagesCollectionView *)collectionView header:(JSQMessagesLoadEarlierHeaderView *)headerView didTapLoadEarlierMessagesButton:(UIButton *)sender;
		[Export ("collectionView:header:didTapLoadEarlierMessagesButton:")]
		void TappedLoadEarlierMessagesButton (MessagesCollectionView collectionView, MessagesLoadEarlierHeaderView headerView, UIButton sender);
	}

	interface IMessagesCollectionViewDataSource {}

	// @protocol JSQMessagesCollectionViewDataSource <UICollectionViewDataSource>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesCollectionViewDataSource")]
	interface MessagesCollectionViewDataSource : IUICollectionViewDataSource
	{
		// @required -(NSString *)senderDisplayName;
		[Abstract]
		[Export ("senderDisplayName")]
		string SenderDisplayName { get; }

		// @required -(NSString *)senderId;
		[Abstract]
		[Export ("senderId")]
		string SenderId { get; }

		// @required -(id<JSQMessageData>)collectionView:(JSQMessagesCollectionView *)collectionView messageDataForItemAtIndexPath:(NSIndexPath *)indexPath;
		[Abstract]
		[Export ("collectionView:messageDataForItemAtIndexPath:")]
		IMessageData GetMessageData (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @required -(void)collectionView:(JSQMessagesCollectionView *)collectionView didDeleteMessageAtIndexPath:(NSIndexPath *)indexPath;
		[Abstract]
		[Export ("collectionView:didDeleteMessageAtIndexPath:")]
		void MessageDeleted (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @required -(id<JSQMessageBubbleImageDataSource>)collectionView:(JSQMessagesCollectionView *)collectionView messageBubbleImageDataForItemAtIndexPath:(NSIndexPath *)indexPath;
		[Abstract]
		[Export ("collectionView:messageBubbleImageDataForItemAtIndexPath:")]
		IMessageBubbleImageDataSource GetMessageBubbleImageData (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @required -(id<JSQMessageAvatarImageDataSource>)collectionView:(JSQMessagesCollectionView *)collectionView avatarImageDataForItemAtIndexPath:(NSIndexPath *)indexPath;
		[Abstract]
		[Export ("collectionView:avatarImageDataForItemAtIndexPath:")]
		IMessageAvatarImageDataSource GetAvatarImageData (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @optional -(NSAttributedString *)collectionView:(JSQMessagesCollectionView *)collectionView attributedTextForCellTopLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:attributedTextForCellTopLabelAtIndexPath:")]
		NSAttributedString GetCellTopLabelAttributedText (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @optional -(NSAttributedString *)collectionView:(JSQMessagesCollectionView *)collectionView attributedTextForMessageBubbleTopLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:attributedTextForMessageBubbleTopLabelAtIndexPath:")]
		NSAttributedString GetMessageBubbleTopLabelAttributedText (MessagesCollectionView collectionView, NSIndexPath indexPath);

		// @optional -(NSAttributedString *)collectionView:(JSQMessagesCollectionView *)collectionView attributedTextForCellBottomLabelAtIndexPath:(NSIndexPath *)indexPath;
		[Export ("collectionView:attributedTextForCellBottomLabelAtIndexPath:")]
		NSAttributedString GetCellBottomLabelAttributedText (MessagesCollectionView collectionView, NSIndexPath indexPath);
	}

	// @interface JSQMessagesLabel : UILabel
	[BaseType (typeof(UILabel), Name = "JSQMessagesLabel")]
	interface MessagesLabel
	{
		// @property (assign, nonatomic) UIEdgeInsets textInsets;
		[Export ("textInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TextInsets { get; set; }
	}

	// @interface JSQMessagesCellTextView : UITextView
	[BaseType (typeof(UITextView), Name = "JSQMessagesCellTextView")]
	interface MessagesCellTextView
	{
	}

	interface IMessagesCollectionViewCellDelegate {}

	// @protocol JSQMessagesCollectionViewCellDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesCollectionViewCellDelegate")]
	interface MessagesCollectionViewCellDelegate
	{
		// @required -(void)messagesCollectionViewCellDidTapAvatar:(JSQMessagesCollectionViewCell *)cell;
		[Abstract]
		[Export ("messagesCollectionViewCellDidTapAvatar:")]
		void TappedAvatar (MessagesCollectionViewCell cell);

		// @required -(void)messagesCollectionViewCellDidTapMessageBubble:(JSQMessagesCollectionViewCell *)cell;
		[Abstract]
		[Export ("messagesCollectionViewCellDidTapMessageBubble:")]
		void TappedMessageBubble (MessagesCollectionViewCell cell);

		// @required -(void)messagesCollectionViewCellDidTapCell:(JSQMessagesCollectionViewCell *)cell atPosition:(CGPoint)position;
		[Abstract]
		[Export ("messagesCollectionViewCellDidTapCell:atPosition:")]
		void TappedCell (MessagesCollectionViewCell cell, CGPoint position);

		// @required -(void)messagesCollectionViewCell:(JSQMessagesCollectionViewCell *)cell didPerformAction:(SEL)action withSender:(id)sender;
		[Abstract]
		[Export ("messagesCollectionViewCell:didPerformAction:withSender:")]
		void DidPerformAction (MessagesCollectionViewCell cell, Selector action, NSObject sender);
	}

	// @interface JSQMessagesCollectionViewCell : UICollectionViewCell
	[BaseType (typeof(UICollectionViewCell), Name = "JSQMessagesCollectionViewCell")]
	interface MessagesCollectionViewCell
	{
		// @property (nonatomic, weak) id<JSQMessagesCollectionViewCellDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMessagesCollectionViewCellDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, weak) JSQMessagesLabel * _Nullable cellTopLabel;
		[NullAllowed, Export ("cellTopLabel", ArgumentSemantic.Weak)]
		MessagesLabel CellTopLabel { get; }

		// @property (readonly, nonatomic, weak) JSQMessagesLabel * _Nullable messageBubbleTopLabel;
		[NullAllowed, Export ("messageBubbleTopLabel", ArgumentSemantic.Weak)]
		MessagesLabel MessageBubbleTopLabel { get; }

		// @property (readonly, nonatomic, weak) JSQMessagesLabel * _Nullable cellBottomLabel;
		[NullAllowed, Export ("cellBottomLabel", ArgumentSemantic.Weak)]
		MessagesLabel CellBottomLabel { get; }

		// @property (readonly, nonatomic, weak) JSQMessagesCellTextView * _Nullable textView;
		[NullAllowed, Export ("textView", ArgumentSemantic.Weak)]
		MessagesCellTextView TextView { get; }

		// @property (readonly, nonatomic, weak) UIImageView * _Nullable messageBubbleImageView;
		[NullAllowed, Export ("messageBubbleImageView", ArgumentSemantic.Weak)]
		UIImageView MessageBubbleImageView { get; }

		// @property (readonly, nonatomic, weak) UIView * _Nullable messageBubbleContainerView;
		[NullAllowed, Export ("messageBubbleContainerView", ArgumentSemantic.Weak)]
		UIView MessageBubbleContainerView { get; }

		// @property (readonly, nonatomic, weak) UIImageView * _Nullable avatarImageView;
		[NullAllowed, Export ("avatarImageView", ArgumentSemantic.Weak)]
		UIImageView AvatarImageView { get; }

		// @property (readonly, nonatomic, weak) UIView * _Nullable avatarContainerView;
		[NullAllowed, Export ("avatarContainerView", ArgumentSemantic.Weak)]
		UIView AvatarContainerView { get; }

		// @property (nonatomic, weak) UIView * _Nullable mediaView;
		[NullAllowed, Export ("mediaView", ArgumentSemantic.Weak)]
		UIView MediaView { get; set; }

		// @property (readonly, nonatomic, weak) UITapGestureRecognizer * _Nullable tapGestureRecognizer;
		[NullAllowed, Export ("tapGestureRecognizer", ArgumentSemantic.Weak)]
		UITapGestureRecognizer TapGestureRecognizer { get; }

		// +(UINib *)nib;
		[Static]
		[Export ("nib")]
		UINib Nib { get; }

		// +(NSString *)cellReuseIdentifier;
		[Static]
		[Export ("cellReuseIdentifier")]
		string CellReuseIdentifier { get; }

		// +(NSString *)mediaCellReuseIdentifier;
		[Static]
		[Export ("mediaCellReuseIdentifier")]
		string MediaCellReuseIdentifier { get; }

		// +(void)registerMenuAction:(SEL)action;
		[Static]
		[Export ("registerMenuAction:")]
		void RegisterMenuAction (Selector action);
	}

	// @interface JSQMessagesCollectionView : UICollectionView <JSQMessagesCollectionViewCellDelegate>
	[BaseType (typeof(UICollectionView), Name = "JSQMessagesCollectionView")]
	interface MessagesCollectionView : MessagesCollectionViewCellDelegate
	{
		// @property (nonatomic, weak) id<JSQMessagesCollectionViewDataSource> _Nullable dataSource;
		[NullAllowed, Export ("dataSource", ArgumentSemantic.Weak)]
		IMessagesCollectionViewDataSource DataSource { get; set; }

		// @property (nonatomic, weak) id<JSQMessagesCollectionViewDelegateFlowLayout> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMessagesCollectionViewDelegateFlowLayout Delegate { get; set; }

		// @property (nonatomic, strong) JSQMessagesCollectionViewFlowLayout * collectionViewLayout;
		[Export ("collectionViewLayout", ArgumentSemantic.Strong)]
		MessagesCollectionViewFlowLayout CollectionViewLayout { get; set; }

		// @property (assign, nonatomic) BOOL typingIndicatorDisplaysOnLeft;
		[Export ("typingIndicatorDisplaysOnLeft")]
		bool TypingIndicatorDisplaysOnLeft { get; set; }

		// @property (nonatomic, strong) UIColor * typingIndicatorMessageBubbleColor;
		[Export ("typingIndicatorMessageBubbleColor", ArgumentSemantic.Strong)]
		UIColor TypingIndicatorMessageBubbleColor { get; set; }

		// @property (nonatomic, strong) UIColor * typingIndicatorEllipsisColor;
		[Export ("typingIndicatorEllipsisColor", ArgumentSemantic.Strong)]
		UIColor TypingIndicatorEllipsisColor { get; set; }

		// @property (nonatomic, strong) UIColor * loadEarlierMessagesHeaderTextColor;
		[Export ("loadEarlierMessagesHeaderTextColor", ArgumentSemantic.Strong)]
		UIColor LoadEarlierMessagesHeaderTextColor { get; set; }

		// -(JSQMessagesTypingIndicatorFooterView *)dequeueTypingIndicatorFooterViewForIndexPath:(NSIndexPath *)indexPath;
		[Export ("dequeueTypingIndicatorFooterViewForIndexPath:")]
		MessagesTypingIndicatorFooterView DequeueTypingIndicatorFooterViewForIndexPath (NSIndexPath indexPath);

		// -(JSQMessagesLoadEarlierHeaderView *)dequeueLoadEarlierMessagesViewHeaderForIndexPath:(NSIndexPath *)indexPath;
		[Export ("dequeueLoadEarlierMessagesViewHeaderForIndexPath:")]
		MessagesLoadEarlierHeaderView DequeueLoadEarlierMessagesViewHeaderForIndexPath (NSIndexPath indexPath);
	}

	interface IMessagesComposerTextViewPasteDelegate {}

	// @protocol JSQMessagesComposerTextViewPasteDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesComposerTextViewPasteDelegate")]
	interface MessagesComposerTextViewPasteDelegate
	{
		// @required -(BOOL)composerTextView:(JSQMessagesComposerTextView *)textView shouldPasteWithSender:(id)sender;
		[Abstract]
		[Export ("composerTextView:shouldPasteWithSender:")]
		bool ShouldPaste (MessagesComposerTextView textView, NSObject sender);
	}

	// @interface JSQMessagesComposerTextView : UITextView
	[BaseType (typeof(UITextView), Name = "JSQMessagesComposerTextView")]
	interface MessagesComposerTextView
	{
		// @property (copy, nonatomic) NSString * placeHolder;
		[Export ("placeHolder")]
		string PlaceHolder { get; set; }

		// @property (nonatomic, strong) UIColor * placeHolderTextColor;
		[Export ("placeHolderTextColor", ArgumentSemantic.Strong)]
		UIColor PlaceHolderTextColor { get; set; }

		// @property (nonatomic, weak) id<JSQMessagesComposerTextViewPasteDelegate> _Nullable pasteDelegate;
		[NullAllowed, Export ("pasteDelegate", ArgumentSemantic.Weak)]
		IMessagesComposerTextViewPasteDelegate PasteDelegate { get; set; }

		// -(BOOL)hasText;
		[Export ("hasText")]
		bool HasText { get; }
	}

	// @interface JSQMessagesToolbarContentView : UIView
	[BaseType (typeof(UIView), Name = "JSQMessagesToolbarContentView")]
	interface MessagesToolbarContentView
	{
		// @property (readonly, nonatomic, weak) JSQMessagesComposerTextView * _Nullable textView;
		[NullAllowed, Export ("textView", ArgumentSemantic.Weak)]
		MessagesComposerTextView TextView { get; }

		// @property (nonatomic, weak) UIButton * _Nullable leftBarButtonItem;
		[NullAllowed, Export ("leftBarButtonItem", ArgumentSemantic.Weak)]
		UIButton LeftBarButtonItem { get; set; }

		// @property (assign, nonatomic) CGFloat leftBarButtonItemWidth;
		[Export ("leftBarButtonItemWidth")]
		nfloat LeftBarButtonItemWidth { get; set; }

		// @property (assign, nonatomic) CGFloat leftContentPadding;
		[Export ("leftContentPadding")]
		nfloat LeftContentPadding { get; set; }

		// @property (readonly, nonatomic, weak) UIView * _Nullable leftBarButtonContainerView;
		[NullAllowed, Export ("leftBarButtonContainerView", ArgumentSemantic.Weak)]
		UIView LeftBarButtonContainerView { get; }

		// @property (nonatomic, weak) UIButton * _Nullable rightBarButtonItem;
		[NullAllowed, Export ("rightBarButtonItem", ArgumentSemantic.Weak)]
		UIButton RightBarButtonItem { get; set; }

		// @property (assign, nonatomic) CGFloat rightBarButtonItemWidth;
		[Export ("rightBarButtonItemWidth")]
		nfloat RightBarButtonItemWidth { get; set; }

		// @property (assign, nonatomic) CGFloat rightContentPadding;
		[Export ("rightContentPadding")]
		nfloat RightContentPadding { get; set; }

		// @property (readonly, nonatomic, weak) UIView * _Nullable rightBarButtonContainerView;
		[NullAllowed, Export ("rightBarButtonContainerView", ArgumentSemantic.Weak)]
		UIView RightBarButtonContainerView { get; }

		// +(UINib *)nib;
		[Static]
		[Export ("nib")]
		UINib Nib { get; }
	}

	interface IMessagesInputToolbarDelegate {}

	// @protocol JSQMessagesInputToolbarDelegate <UIToolbarDelegate>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesInputToolbarDelegate")]
	interface MessagesInputToolbarDelegate : IUIToolbarDelegate
	{
		// @required -(void)messagesInputToolbar:(JSQMessagesInputToolbar *)toolbar didPressRightBarButton:(UIButton *)sender;
		[Abstract]
		[Export ("messagesInputToolbar:didPressRightBarButton:")]
		void PressedRightBarButton (MessagesInputToolbar toolbar, UIButton sender);

		// @required -(void)messagesInputToolbar:(JSQMessagesInputToolbar *)toolbar didPressLeftBarButton:(UIButton *)sender;
		[Abstract]
		[Export ("messagesInputToolbar:didPressLeftBarButton:")]
		void PressedLeftBarButton (MessagesInputToolbar toolbar, UIButton sender);
	}

	// @interface JSQMessagesInputToolbar : UIToolbar
	[BaseType (typeof(UIToolbar), Name = "JSQMessagesInputToolbar")]
	interface MessagesInputToolbar
	{
		// @property (nonatomic, weak) id<JSQMessagesInputToolbarDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMessagesInputToolbarDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, weak) JSQMessagesToolbarContentView * _Nullable contentView;
		[NullAllowed, Export ("contentView", ArgumentSemantic.Weak)]
		MessagesToolbarContentView ContentView { get; }

		// @property (assign, nonatomic) BOOL sendButtonOnRight;
		[Export ("sendButtonOnRight")]
		bool SendButtonOnRight { get; set; }

		// @property (assign, nonatomic) CGFloat preferredDefaultHeight;
		[Export ("preferredDefaultHeight")]
		nfloat PreferredDefaultHeight { get; set; }

		// @property (assign, nonatomic) NSUInteger maximumHeight;
		[Export ("maximumHeight")]
		nuint MaximumHeight { get; set; }

		// -(void)toggleSendButtonEnabled;
		[Export ("toggleSendButtonEnabled")]
		void ToggleSendButtonEnabled ();

		// -(JSQMessagesToolbarContentView *)loadToolbarContentView;
		[Export ("loadToolbarContentView")]
		MessagesToolbarContentView LoadToolbarContentView ();
	}

	interface IMessagesKeyboardControllerDelegate {}

	// @protocol JSQMessagesKeyboardControllerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesKeyboardControllerDelegate")]
	interface MessagesKeyboardControllerDelegate
	{
		// @required -(void)keyboardController:(JSQMessagesKeyboardController *)keyboardController keyboardDidChangeFrame:(CGRect)keyboardFrame;
		[Abstract]
		[Export ("keyboardController:keyboardDidChangeFrame:")]
		void KeyboardDidChangeFrame (MessagesKeyboardController keyboardController, CGRect keyboardFrame);
	}

	// @interface JSQMessagesKeyboardController : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesKeyboardController")]
	interface MessagesKeyboardController
	{
		// @property (nonatomic, weak) id<JSQMessagesKeyboardControllerDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMessagesKeyboardControllerDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, weak) UITextView * _Nullable textView;
		[NullAllowed, Export ("textView", ArgumentSemantic.Weak)]
		UITextView TextView { get; }

		// @property (readonly, nonatomic, weak) UIView * _Nullable contextView;
		[NullAllowed, Export ("contextView", ArgumentSemantic.Weak)]
		UIView ContextView { get; }

		// @property (readonly, nonatomic, weak) UIPanGestureRecognizer * _Nullable panGestureRecognizer;
		[NullAllowed, Export ("panGestureRecognizer", ArgumentSemantic.Weak)]
		UIPanGestureRecognizer PanGestureRecognizer { get; }

		// @property (assign, nonatomic) CGPoint keyboardTriggerPoint;
		[Export ("keyboardTriggerPoint", ArgumentSemantic.Assign)]
		CGPoint KeyboardTriggerPoint { get; set; }

		// @property (readonly, assign, nonatomic) BOOL keyboardIsVisible;
		[Export ("keyboardIsVisible")]
		bool KeyboardIsVisible { get; }

		// @property (readonly, assign, nonatomic) CGRect currentKeyboardFrame;
		[Export ("currentKeyboardFrame", ArgumentSemantic.Assign)]
		CGRect CurrentKeyboardFrame { get; }

		// -(instancetype)initWithTextView:(UITextView *)textView contextView:(UIView *)contextView panGestureRecognizer:(UIPanGestureRecognizer *)panGestureRecognizer delegate:(id<JSQMessagesKeyboardControllerDelegate>)delegate;
		[Export ("initWithTextView:contextView:panGestureRecognizer:delegate:")]
		IntPtr Constructor (UITextView textView, UIView contextView, UIPanGestureRecognizer panGestureRecognizer, IMessagesKeyboardControllerDelegate @delegate);

		// -(void)beginListeningForKeyboard;
		[Export ("beginListeningForKeyboard")]
		void BeginListeningForKeyboard ();

		// -(void)endListeningForKeyboard;
		[Export ("endListeningForKeyboard")]
		void EndListeningForKeyboard ();
	}

	// @interface JSQMessagesViewController : UIViewController <JSQMessagesCollectionViewDataSource, JSQMessagesCollectionViewDelegateFlowLayout, UITextViewDelegate>
	[BaseType (typeof(UIViewController), Name = "JSQMessagesViewController")]
	interface MessagesViewController : MessagesCollectionViewDataSource, MessagesCollectionViewDelegateFlowLayout, IUITextViewDelegate
	{
		// @property (readonly, nonatomic, weak) JSQMessagesCollectionView * _Nullable collectionView;
		[NullAllowed, Export ("collectionView", ArgumentSemantic.Weak)]
		MessagesCollectionView CollectionView { get; }

		// @property (readonly, nonatomic, weak) JSQMessagesInputToolbar * _Nullable inputToolbar;
		[NullAllowed, Export ("inputToolbar", ArgumentSemantic.Weak)]
		MessagesInputToolbar InputToolbar { get; }

		// @property (nonatomic, strong) JSQMessagesKeyboardController * keyboardController;
		[Export ("keyboardController", ArgumentSemantic.Strong)]
		MessagesKeyboardController KeyboardController { get; set; }

		// @property (copy, nonatomic) NSString * senderDisplayName;
		[Export ("senderDisplayName")]
		string SenderDisplayName { get; set; }

		// @property (copy, nonatomic) NSString * senderId;
		[Export ("senderId")]
		string SenderId { get; set; }

		// @property (assign, nonatomic) BOOL automaticallyScrollsToMostRecentMessage;
		[Export ("automaticallyScrollsToMostRecentMessage")]
		bool AutomaticallyScrollsToMostRecentMessage { get; set; }

		// @property (copy, nonatomic) NSString * outgoingCellIdentifier;
		[Export ("outgoingCellIdentifier")]
		string OutgoingCellIdentifier { get; set; }

		// @property (copy, nonatomic) NSString * outgoingMediaCellIdentifier;
		[Export ("outgoingMediaCellIdentifier")]
		string OutgoingMediaCellIdentifier { get; set; }

		// @property (copy, nonatomic) NSString * incomingCellIdentifier;
		[Export ("incomingCellIdentifier")]
		string IncomingCellIdentifier { get; set; }

		// @property (copy, nonatomic) NSString * incomingMediaCellIdentifier;
		[Export ("incomingMediaCellIdentifier")]
		string IncomingMediaCellIdentifier { get; set; }

		// @property (assign, nonatomic) BOOL showTypingIndicator;
		[Export ("showTypingIndicator")]
		bool ShowTypingIndicator { get; set; }

		// @property (assign, nonatomic) BOOL showLoadEarlierMessagesHeader;
		[Export ("showLoadEarlierMessagesHeader")]
		bool ShowLoadEarlierMessagesHeader { get; set; }

		// @property (assign, nonatomic) CGFloat topContentAdditionalInset;
		[Export ("topContentAdditionalInset")]
		nfloat TopContentAdditionalInset { get; set; }

		// +(UINib *)nib;
		[Static]
		[Export ("nib")]
		UINib Nib { get; }

		// +(instancetype)messagesViewController;
		[Static]
		[Export ("messagesViewController")]
		MessagesViewController CreateMessagesViewController ();

		// -(void)didPressSendButton:(UIButton *)button withMessageText:(NSString *)text senderId:(NSString *)senderId senderDisplayName:(NSString *)senderDisplayName date:(NSDate *)date;
		[Export ("didPressSendButton:withMessageText:senderId:senderDisplayName:date:")]
		void PressedSendButton (UIButton button, string text, string senderId, string senderDisplayName, NSDate date);

		// -(void)didPressAccessoryButton:(UIButton *)sender;
		[Export ("didPressAccessoryButton:")]
		void PressedAccessoryButton (UIButton sender);

		// -(void)finishSendingMessage;
		[Export ("finishSendingMessage")]
		void FinishSendingMessage ();

		// -(void)finishSendingMessageAnimated:(BOOL)animated;
		[Export ("finishSendingMessageAnimated:")]
		void FinishSendingMessage (bool animated);

		// -(void)finishReceivingMessage;
		[Export ("finishReceivingMessage")]
		void FinishReceivingMessage ();

		// -(void)finishReceivingMessageAnimated:(BOOL)animated;
		[Export ("finishReceivingMessageAnimated:")]
		void FinishReceivingMessage (bool animated);

		// -(void)scrollToBottomAnimated:(BOOL)animated;
		[Export ("scrollToBottomAnimated:")]
		void ScrollToBottom (bool animated);
	}

	// @interface JSQMessagesCollectionViewCellIncoming : JSQMessagesCollectionViewCell
	[BaseType (typeof(MessagesCollectionViewCell), Name = "JSQMessagesCollectionViewCellIncoming")]
	interface MessagesCollectionViewCellIncoming
	{
	}

	// @interface JSQMessagesCollectionViewCellOutgoing : JSQMessagesCollectionViewCell
	[BaseType (typeof(MessagesCollectionViewCell), Name = "JSQMessagesCollectionViewCellOutgoing")]
	interface MessagesCollectionViewCellOutgoing
	{
	}

	// @interface JSQMessagesTypingIndicatorFooterView : UICollectionReusableView
	[BaseType (typeof(UICollectionReusableView), Name = "JSQMessagesTypingIndicatorFooterView")]
	interface MessagesTypingIndicatorFooterView
	{
		// +(UINib *)nib;
		[Static]
		[Export ("nib")]
		UINib Nib { get; }

		// +(NSString *)footerReuseIdentifier;
		[Static]
		[Export ("footerReuseIdentifier")]
		string FooterReuseIdentifier { get; }

		// -(void)configureWithEllipsisColor:(UIColor *)ellipsisColor messageBubbleColor:(UIColor *)messageBubbleColor shouldDisplayOnLeft:(BOOL)shouldDisplayOnLeft forCollectionView:(UICollectionView *)collectionView;
		[Export ("configureWithEllipsisColor:messageBubbleColor:shouldDisplayOnLeft:forCollectionView:")]
		void Configure (UIColor ellipsisColor, UIColor messageBubbleColor, bool shouldDisplayOnLeft, UICollectionView collectionView);
	}

	interface IMessagesLoadEarlierHeaderViewDelegate {} 

	// @protocol JSQMessagesLoadEarlierHeaderViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessagesLoadEarlierHeaderViewDelegate")]
	interface MessagesLoadEarlierHeaderViewDelegate
	{
		// @required -(void)headerView:(JSQMessagesLoadEarlierHeaderView *)headerView didPressLoadButton:(UIButton *)sender;
		[Abstract]
		[Export ("headerView:didPressLoadButton:")]
		void PressedLoadButton (MessagesLoadEarlierHeaderView headerView, UIButton sender);
	}

	// @interface JSQMessagesLoadEarlierHeaderView : UICollectionReusableView
	[BaseType (typeof(UICollectionReusableView), Name = "JSQMessagesLoadEarlierHeaderView")]
	interface MessagesLoadEarlierHeaderView
	{
		// @property (nonatomic, weak) id<JSQMessagesLoadEarlierHeaderViewDelegate> _Nullable delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IMessagesLoadEarlierHeaderViewDelegate Delegate { get; set; }

		// @property (readonly, nonatomic, weak) UIButton * _Nullable loadButton;
		[NullAllowed, Export ("loadButton", ArgumentSemantic.Weak)]
		UIButton LoadButton { get; }

		// +(UINib *)nib;
		[Static]
		[Export ("nib")]
		UINib Nib { get; }

		// +(NSString *)headerReuseIdentifier;
		[Static]
		[Export ("headerReuseIdentifier")]
		string HeaderReuseIdentifier { get; }
	}

	// @interface JSQMessagesBubblesSizeCalculator : NSObject <JSQMessagesBubbleSizeCalculating>
	[BaseType (typeof(NSObject), Name = "JSQMessagesBubblesSizeCalculator")]
	interface MessagesBubblesSizeCalculator : MessagesBubbleSizeCalculating
	{
		// -(instancetype)initWithCache:(NSCache *)cache minimumBubbleWidth:(NSUInteger)minimumBubbleWidth usesFixedWidthBubbles:(BOOL)usesFixedWidthBubbles __attribute__((objc_designated_initializer));
		[Export ("initWithCache:minimumBubbleWidth:usesFixedWidthBubbles:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSCache cache, nuint minimumBubbleWidth, bool usesFixedWidthBubbles);
	}

	// @interface JSQMessagesCollectionViewLayoutAttributes : UICollectionViewLayoutAttributes <NSCopying>
	[BaseType (typeof(UICollectionViewLayoutAttributes), Name = "JSQMessagesCollectionViewLayoutAttributes")]
	interface MessagesCollectionViewLayoutAttributes : INSCopying
	{
		// @property (nonatomic, strong) UIFont * messageBubbleFont;
		[Export ("messageBubbleFont", ArgumentSemantic.Strong)]
		UIFont MessageBubbleFont { get; set; }

		// @property (assign, nonatomic) CGFloat messageBubbleContainerViewWidth;
		[Export ("messageBubbleContainerViewWidth")]
		nfloat MessageBubbleContainerViewWidth { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets textViewTextContainerInsets;
		[Export ("textViewTextContainerInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TextViewTextContainerInsets { get; set; }

		// @property (assign, nonatomic) UIEdgeInsets textViewFrameInsets;
		[Export ("textViewFrameInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TextViewFrameInsets { get; set; }

		// @property (assign, nonatomic) CGSize incomingAvatarViewSize;
		[Export ("incomingAvatarViewSize", ArgumentSemantic.Assign)]
		CGSize IncomingAvatarViewSize { get; set; }

		// @property (assign, nonatomic) CGSize outgoingAvatarViewSize;
		[Export ("outgoingAvatarViewSize", ArgumentSemantic.Assign)]
		CGSize OutgoingAvatarViewSize { get; set; }

		// @property (assign, nonatomic) CGFloat cellTopLabelHeight;
		[Export ("cellTopLabelHeight")]
		nfloat CellTopLabelHeight { get; set; }

		// @property (assign, nonatomic) CGFloat messageBubbleTopLabelHeight;
		[Export ("messageBubbleTopLabelHeight")]
		nfloat MessageBubbleTopLabelHeight { get; set; }

		// @property (assign, nonatomic) CGFloat cellBottomLabelHeight;
		[Export ("cellBottomLabelHeight")]
		nfloat CellBottomLabelHeight { get; set; }
	}

	// @interface JSQMessagesCollectionViewFlowLayoutInvalidationContext : UICollectionViewFlowLayoutInvalidationContext
	[BaseType (typeof(UICollectionViewFlowLayoutInvalidationContext), Name = "JSQMessagesCollectionViewFlowLayoutInvalidationContext")]
	interface MessagesCollectionViewFlowLayoutInvalidationContext
	{
		// @property (assign, nonatomic) BOOL invalidateFlowLayoutMessagesCache;
		[Export ("invalidateFlowLayoutMessagesCache")]
		bool InvalidateFlowLayoutMessagesCache { get; set; }

		// +(instancetype)context;
		[Static]
		[Export ("context")]
		MessagesCollectionViewFlowLayoutInvalidationContext Context ();
	}

	interface IMessageMediaData {}

	// @protocol JSQMessageMediaData <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessageMediaData")]
	interface MessageMediaData
	{
		// @required -(UIView *)mediaView;
		[Abstract]
		[Export ("mediaView")]
		UIView MediaView { get; }

		// @required -(CGSize)mediaViewDisplaySize;
		[Abstract]
		[Export ("mediaViewDisplaySize")]
		CGSize MediaViewDisplaySize { get; }

		// @required -(UIView *)mediaPlaceholderView;
		[Abstract]
		[Export ("mediaPlaceholderView")]
		UIView MediaPlaceholderView { get; }

		// @required -(NSUInteger)mediaHash;
		[Abstract]
		[Export ("mediaHash")]
		nuint MediaHash { get; }
	}

	interface IMessageData {}

	// @protocol JSQMessageData <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessageData")]
	interface MessageData
	{
		// @required -(NSString *)senderId;
		[Abstract]
		[Export ("senderId")]
		string SenderId { get; }

		// @required -(NSString *)senderDisplayName;
		[Abstract]
		[Export ("senderDisplayName")]
		string SenderDisplayName { get; }

		// @required -(NSDate *)date;
		[Abstract]
		[Export ("date")]
		NSDate Date { get; }

		// @required -(BOOL)isMediaMessage;
		[Abstract]
		[Export ("isMediaMessage")]
		bool IsMediaMessage { get; }

		// @required -(NSUInteger)messageHash;
		[Abstract]
		[Export ("messageHash")]
		nuint MessageHash { get; }

		// @optional -(NSString *)text;
		[Export ("text")]
		string Text { get; }

		// @optional -(id<JSQMessageMediaData>)media;
		[Export ("media")]
		MessageMediaData Media { get; }
	}

	// @interface JSQMessage : NSObject <JSQMessageData, NSCoding, NSCopying>
	[BaseType (typeof(NSObject), Name = "JSQMessage")]
	interface Message : MessageData, INSCoding, INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * senderId;
		[Export ("senderId")]
		string SenderId { get; }

		// @property (readonly, copy, nonatomic) NSString * senderDisplayName;
		[Export ("senderDisplayName")]
		string SenderDisplayName { get; }

		// @property (readonly, copy, nonatomic) NSDate * date;
		[Export ("date", ArgumentSemantic.Copy)]
		NSDate Date { get; }

		// @property (readonly, assign, nonatomic) BOOL isMediaMessage;
		[Export ("isMediaMessage")]
		bool IsMediaMessage { get; }

		// @property (readonly, copy, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// @property (readonly, copy, nonatomic) id<JSQMessageMediaData> media;
		[Export ("media", ArgumentSemantic.Copy)]
		MessageMediaData Media { get; }

		// +(instancetype)messageWithSenderId:(NSString *)senderId displayName:(NSString *)displayName text:(NSString *)text;
		[Static]
		[Export ("messageWithSenderId:displayName:text:")]
		Message Create (string senderId, string displayName, string text);

		// -(instancetype)initWithSenderId:(NSString *)senderId senderDisplayName:(NSString *)senderDisplayName date:(NSDate *)date text:(NSString *)text;
		[Export ("initWithSenderId:senderDisplayName:date:text:")]
		IntPtr Constructor (string senderId, string senderDisplayName, NSDate date, string text);

		// +(instancetype)messageWithSenderId:(NSString *)senderId displayName:(NSString *)displayName media:(id<JSQMessageMediaData>)media;
		[Static]
		[Export ("messageWithSenderId:displayName:media:")]
		Message Create (string senderId, string displayName, IMessageMediaData media);

		// -(instancetype)initWithSenderId:(NSString *)senderId senderDisplayName:(NSString *)senderDisplayName date:(NSDate *)date media:(id<JSQMessageMediaData>)media;
		[Export ("initWithSenderId:senderDisplayName:date:media:")]
		IntPtr Constructor (string senderId, string senderDisplayName, NSDate date, IMessageMediaData media);
	}

	// @interface JSQMediaItem : NSObject <JSQMessageMediaData, NSCoding, NSCopying>
	[BaseType (typeof(NSObject), Name = "JSQMediaItem")]
	interface MediaItem : MessageMediaData, INSCoding, INSCopying
	{
		// @property (assign, nonatomic) BOOL appliesMediaViewMaskAsOutgoing;
		[Export ("appliesMediaViewMaskAsOutgoing")]
		bool AppliesMediaViewMaskAsOutgoing { get; set; }

		// -(instancetype)initWithMaskAsOutgoing:(BOOL)maskAsOutgoing;
		[Export ("initWithMaskAsOutgoing:")]
		IntPtr Constructor (bool maskAsOutgoing);

		// -(void)clearCachedMediaViews;
		[Export ("clearCachedMediaViews")]
		void ClearCachedMediaViews ();
	}

	// @interface JSQPhotoMediaItem : JSQMediaItem <JSQMessageMediaData, NSCoding, NSCopying>
	[BaseType (typeof(MediaItem), Name = "JSQPhotoMediaItem")]
	interface PhotoMediaItem : MessageMediaData, INSCoding, INSCopying
	{
		// @property (copy, nonatomic) UIImage * image;
		[Export ("image", ArgumentSemantic.Copy)]
		UIImage Image { get; set; }

		// -(instancetype)initWithImage:(UIImage *)image;
		[Export ("initWithImage:")]
		IntPtr Constructor (UIImage image);
	}

	// typedef void (^JSQLocationMediaItemCompletionBlock)();
	delegate void LocationMediaItemCompletionBlock ();

	// @interface JSQLocationMediaItem : JSQMediaItem <JSQMessageMediaData, MKAnnotation, NSCoding, NSCopying>
	[BaseType (typeof(MediaItem), Name = "JSQLocationMediaItem")]
	interface LocationMediaItem : MessageMediaData, IMKAnnotation, INSCoding, INSCopying
	{
		// @property (copy, nonatomic) CLLocation * location;
		[Export ("location", ArgumentSemantic.Copy)]
		CLLocation Location { get; set; }

		// @property (readonly, nonatomic) CLLocationCoordinate2D coordinate;
		[Export ("coordinate")]
		CLLocationCoordinate2D Coordinate { get; }

		// -(instancetype)initWithLocation:(CLLocation *)location;
		[Export ("initWithLocation:")]
		IntPtr Constructor (CLLocation location);

		// -(void)setLocation:(CLLocation *)location withCompletionHandler:(JSQLocationMediaItemCompletionBlock)completion;
		[Export ("setLocation:withCompletionHandler:")]
		void SetLocation (CLLocation location, LocationMediaItemCompletionBlock completion);

		// -(void)setLocation:(CLLocation *)location region:(MKCoordinateRegion)region withCompletionHandler:(JSQLocationMediaItemCompletionBlock)completion;
		[Export ("setLocation:region:withCompletionHandler:")]
		void SetLocation (CLLocation location, MKCoordinateRegion region, LocationMediaItemCompletionBlock completion);
	}

	// @interface JSQVideoMediaItem : JSQMediaItem <JSQMessageMediaData, NSCoding, NSCopying>
	[BaseType (typeof(MediaItem), Name = "JSQVideoMediaItem")]
	interface VideoMediaItem : MessageMediaData, INSCoding, INSCopying
	{
		// @property (nonatomic, strong) NSURL * fileURL;
		[Export ("fileURL", ArgumentSemantic.Strong)]
		NSUrl FileURL { get; set; }

		// @property (assign, nonatomic) BOOL isReadyToPlay;
		[Export ("isReadyToPlay")]
		bool IsReadyToPlay { get; set; }

		// -(instancetype)initWithFileURL:(NSURL *)fileURL isReadyToPlay:(BOOL)isReadyToPlay;
		[Export ("initWithFileURL:isReadyToPlay:")]
		IntPtr Constructor (NSUrl fileURL, bool isReadyToPlay);
	}

	interface IMessageBubbleImageDataSource {}

	// @protocol JSQMessageBubbleImageDataSource <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessageBubbleImageDataSource")]
	interface MessageBubbleImageDataSource
	{
		// @required -(UIImage *)messageBubbleImage;
		[Abstract]
		[Export ("messageBubbleImage")]
		UIImage MessageBubbleImage { get; }

		// @required -(UIImage *)messageBubbleHighlightedImage;
		[Abstract]
		[Export ("messageBubbleHighlightedImage")]
		UIImage MessageBubbleHighlightedImage { get; }
	}

	// @interface JSQMessagesBubbleImage : NSObject <JSQMessageBubbleImageDataSource, NSCopying>
	[BaseType (typeof(NSObject), Name = "JSQMessagesBubbleImage")]
	interface MessagesBubbleImage : MessageBubbleImageDataSource, INSCopying
	{
		// @property (readonly, nonatomic, strong) UIImage * messageBubbleImage;
		[Export ("messageBubbleImage", ArgumentSemantic.Strong)]
		UIImage MessageBubbleImage { get; }

		// @property (readonly, nonatomic, strong) UIImage * messageBubbleHighlightedImage;
		[Export ("messageBubbleHighlightedImage", ArgumentSemantic.Strong)]
		UIImage MessageBubbleHighlightedImage { get; }

		// -(instancetype)initWithMessageBubbleImage:(UIImage *)image highlightedImage:(UIImage *)highlightedImage;
		[Export ("initWithMessageBubbleImage:highlightedImage:")]
		IntPtr Constructor (UIImage image, UIImage highlightedImage);
	}

	interface IMessageAvatarImageDataSource {}

	// @protocol JSQMessageAvatarImageDataSource <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "JSQMessageAvatarImageDataSource")]
	interface MessageAvatarImageDataSource
	{
		// @required -(UIImage *)avatarImage;
		[Abstract]
		[Export ("avatarImage")]
		UIImage AvatarImage { get; }

		// @required -(UIImage *)avatarHighlightedImage;
		[Abstract]
		[Export ("avatarHighlightedImage")]
		UIImage AvatarHighlightedImage { get; }

		// @required -(UIImage *)avatarPlaceholderImage;
		[Abstract]
		[Export ("avatarPlaceholderImage")]
		UIImage AvatarPlaceholderImage { get; }
	}

	// @interface JSQMessagesAvatarImage : NSObject <JSQMessageAvatarImageDataSource, NSCopying>
	[BaseType (typeof(NSObject), Name = "JSQMessagesAvatarImage")]
	interface MessagesAvatarImage : MessageAvatarImageDataSource, INSCopying
	{
		// @property (nonatomic, strong) UIImage * avatarImage;
		[Export ("avatarImage", ArgumentSemantic.Strong)]
		UIImage AvatarImage { get; set; }

		// @property (nonatomic, strong) UIImage * avatarHighlightedImage;
		[Export ("avatarHighlightedImage", ArgumentSemantic.Strong)]
		UIImage AvatarHighlightedImage { get; set; }

		// @property (readonly, nonatomic, strong) UIImage * avatarPlaceholderImage;
		[Export ("avatarPlaceholderImage", ArgumentSemantic.Strong)]
		UIImage AvatarPlaceholderImage { get; }

		// +(instancetype)avatarWithImage:(UIImage *)image;
		[Static]
		[Export ("avatarWithImage:")]
		MessagesAvatarImage Create (UIImage image);

		// +(instancetype)avatarImageWithPlaceholder:(UIImage *)placeholderImage;
		[Static]
		[Export ("avatarImageWithPlaceholder:")]
		MessagesAvatarImage CreatePlaceholder (UIImage placeholderImage);

		// -(instancetype)initWithAvatarImage:(UIImage *)avatarImage highlightedImage:(UIImage *)highlightedImage placeholderImage:(UIImage *)placeholderImage;
		[Export ("initWithAvatarImage:highlightedImage:placeholderImage:")]
		IntPtr Constructor (UIImage avatarImage, UIImage highlightedImage, UIImage placeholderImage);
	}

	// @interface JSQMessagesAvatarImageFactory : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesAvatarImageFactory")]
	interface MessagesAvatarImageFactory
	{
		// +(JSQMessagesAvatarImage *)avatarImageWithPlaceholder:(UIImage *)placeholderImage diameter:(NSUInteger)diameter;
		[Static]
		[Export ("avatarImageWithPlaceholder:diameter:")]
		MessagesAvatarImage CreatePlaceholderAvatarImage (UIImage placeholderImage, nuint diameter);

		// +(JSQMessagesAvatarImage *)avatarImageWithImage:(UIImage *)image diameter:(NSUInteger)diameter;
		[Static]
		[Export ("avatarImageWithImage:diameter:")]
		MessagesAvatarImage CreateAvatarImage (UIImage image, nuint diameter);

		// +(UIImage *)circularAvatarImage:(UIImage *)image withDiameter:(NSUInteger)diameter;
		[Static]
		[Export ("circularAvatarImage:withDiameter:")]
		UIImage CreateCircularAvatar (UIImage image, nuint diameter);

		// +(UIImage *)circularAvatarHighlightedImage:(UIImage *)image withDiameter:(NSUInteger)diameter;
		[Static]
		[Export ("circularAvatarHighlightedImage:withDiameter:")]
		UIImage CreateCircularAvatarHighlightedImage (UIImage image, nuint diameter);

		// +(JSQMessagesAvatarImage *)avatarImageWithUserInitials:(NSString *)userInitials backgroundColor:(UIColor *)backgroundColor textColor:(UIColor *)textColor font:(UIFont *)font diameter:(NSUInteger)diameter;
		[Static]
		[Export ("avatarImageWithUserInitials:backgroundColor:textColor:font:diameter:")]
		MessagesAvatarImage CreateAvatarImage (string userInitials, UIColor backgroundColor, UIColor textColor, UIFont font, nuint diameter);
	}

	// @interface JSQMessagesBubbleImageFactory : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesBubbleImageFactory")]
	interface MessagesBubbleImageFactory
	{
		// -(instancetype)initWithBubbleImage:(UIImage *)bubbleImage capInsets:(UIEdgeInsets)capInsets;
		[Export ("initWithBubbleImage:capInsets:")]
		IntPtr Constructor (UIImage bubbleImage, UIEdgeInsets capInsets);

		// -(JSQMessagesBubbleImage *)outgoingMessagesBubbleImageWithColor:(UIColor *)color;
		[Export ("outgoingMessagesBubbleImageWithColor:")]
		MessagesBubbleImage CreateOutgoingMessagesBubbleImage (UIColor color);

		// -(JSQMessagesBubbleImage *)incomingMessagesBubbleImageWithColor:(UIColor *)color;
		[Export ("incomingMessagesBubbleImageWithColor:")]
		MessagesBubbleImage CreateIncomingMessagesBubbleImage (UIColor color);
	}

	// @interface JSQMessagesMediaViewBubbleImageMasker : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesMediaViewBubbleImageMasker")]
	interface MessagesMediaViewBubbleImageMasker
	{
		// @property (readonly, nonatomic, strong) JSQMessagesBubbleImageFactory * bubbleImageFactory;
		[Export ("bubbleImageFactory", ArgumentSemantic.Strong)]
		MessagesBubbleImageFactory BubbleImageFactory { get; }

		// -(instancetype)initWithBubbleImageFactory:(JSQMessagesBubbleImageFactory *)bubbleImageFactory;
		[Export ("initWithBubbleImageFactory:")]
		IntPtr Constructor (MessagesBubbleImageFactory bubbleImageFactory);

		// -(void)applyOutgoingBubbleImageMaskToMediaView:(UIView *)mediaView;
		[Export ("applyOutgoingBubbleImageMaskToMediaView:")]
		void ApplyOutgoingBubbleImageMask (UIView mediaView);

		// -(void)applyIncomingBubbleImageMaskToMediaView:(UIView *)mediaView;
		[Export ("applyIncomingBubbleImageMaskToMediaView:")]
		void ApplyIncomingBubbleImageMask (UIView mediaView);

		// +(void)applyBubbleImageMaskToMediaView:(UIView *)mediaView isOutgoing:(BOOL)isOutgoing;
		[Static]
		[Export ("applyBubbleImageMaskToMediaView:isOutgoing:")]
		void ApplyBubbleImageMask (UIView mediaView, bool isOutgoing);
	}

	// @interface JSQMessagesTimestampFormatter : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesTimestampFormatter")]
	interface MessagesTimestampFormatter
	{
		// @property (readonly, nonatomic, strong) NSDateFormatter * dateFormatter;
		[Export ("dateFormatter", ArgumentSemantic.Strong)]
		NSDateFormatter DateFormatter { get; }

		// @property (copy, nonatomic) NSDictionary * dateTextAttributes;
		[Export ("dateTextAttributes", ArgumentSemantic.Copy)]
		NSDictionary DateTextAttributes { get; set; }

		// @property (copy, nonatomic) NSDictionary * timeTextAttributes;
		[Export ("timeTextAttributes", ArgumentSemantic.Copy)]
		NSDictionary TimeTextAttributes { get; set; }

		// +(JSQMessagesTimestampFormatter *)sharedFormatter;
		[Static]
		[Export ("sharedFormatter")]
		MessagesTimestampFormatter SharedFormatter { get; }

		// -(NSString *)timestampForDate:(NSDate *)date;
		[Export ("timestampForDate:")]
		string GetTimestamp (NSDate date);

		// -(NSAttributedString *)attributedTimestampForDate:(NSDate *)date;
		[Export ("attributedTimestampForDate:")]
		NSAttributedString GetAttributedTimestamp (NSDate date);

		// -(NSString *)timeForDate:(NSDate *)date;
		[Export ("timeForDate:")]
		string GetTime (NSDate date);

		// -(NSString *)relativeDateForDate:(NSDate *)date;
		[Export ("relativeDateForDate:")]
		string GetRelativeDate (NSDate date);
	}

	// @interface JSQMessagesToolbarButtonFactory : NSObject
	[BaseType (typeof(NSObject), Name = "JSQMessagesToolbarButtonFactory")]
	interface MessagesToolbarButtonFactory
	{
		// +(UIButton *)defaultAccessoryButtonItem;
		[Static]
		[Export ("defaultAccessoryButtonItem")]
		UIButton CreateDefaultAccessoryButtonItem ();

		// +(UIButton *)defaultSendButtonItem;
		[Static]
		[Export ("defaultSendButtonItem")]
		UIButton CreateDefaultSendButtonItem ();
	}

	// typedef void (^JSQSystemSoundPlayerCompletionBlock)();
	delegate void SystemSoundPlayerCompletionBlock ();

	// @interface JSQSystemSoundPlayer : NSObject
	[BaseType (typeof(NSObject), Name = "JSQSystemSoundPlayer")]
	interface SystemSoundPlayer
	{
		// @property (readonly, assign, nonatomic) BOOL on;
		[Export ("on")]
		bool On { get; }

		// @property (nonatomic, strong) NSBundle * bundle;
		[Export ("bundle", ArgumentSemantic.Strong)]
		NSBundle Bundle { get; set; }

		// +(JSQSystemSoundPlayer *)sharedPlayer;
		[Static]
		[Export ("sharedPlayer")]
		SystemSoundPlayer SharedPlayer { get; }

		// -(void)toggleSoundPlayerOn:(BOOL)on;
		[Export ("toggleSoundPlayerOn:")]
		void ToggleSoundPlayerOn (bool on);

		// -(void)playSoundWithFilename:(NSString *)filename fileExtension:(NSString *)fileExtension;
		[Export ("playSoundWithFilename:fileExtension:")]
		void PlaySound (string filename, string fileExtension);

		// -(void)playSoundWithFilename:(NSString *)filename fileExtension:(NSString *)fileExtension completion:(JSQSystemSoundPlayerCompletionBlock)completionBlock;
		[Export ("playSoundWithFilename:fileExtension:completion:")]
		void PlaySound (string filename, string fileExtension, SystemSoundPlayerCompletionBlock completionBlock);

		// -(void)playAlertSoundWithFilename:(NSString *)filename fileExtension:(NSString *)fileExtension;
		[Export ("playAlertSoundWithFilename:fileExtension:")]
		void PlayAlertSound (string filename, string fileExtension);

		// -(void)playAlertSoundWithFilename:(NSString *)filename fileExtension:(NSString *)fileExtension completion:(JSQSystemSoundPlayerCompletionBlock)completionBlock;
		[Export ("playAlertSoundWithFilename:fileExtension:completion:")]
		void PlayAlertSound (string filename, string fileExtension, SystemSoundPlayerCompletionBlock completionBlock);

		// -(void)playVibrateSound;
		[Export ("playVibrateSound")]
		void PlayVibrateSound ();

		// -(void)stopAllSounds;
		[Export ("stopAllSounds")]
		void StopAllSounds ();

		// -(void)stopSoundWithFilename:(NSString *)filename;
		[Export ("stopSoundWithFilename:")]
		void StopSound (string filename);

		// -(void)preloadSoundWithFilename:(NSString *)filename fileExtension:(NSString *)fileExtension;
		[Export ("preloadSoundWithFilename:fileExtension:")]
		void PreloadSound (string filename, string fileExtension);

		// +(void)jsq_playMessageReceivedSound;
		[Static]
		[Export ("jsq_playMessageReceivedSound")]
		void PlayMessageReceivedSound ();

		// +(void)jsq_playMessageReceivedAlert;
		[Static]
		[Export ("jsq_playMessageReceivedAlert")]
		void PlayMessageReceivedAlert ();

		// +(void)jsq_playMessageSentSound;
		[Static]
		[Export ("jsq_playMessageSentSound")]
		void PlayMessageSentSound ();

		// +(void)jsq_playMessageSentAlert;
		[Static]
		[Export ("jsq_playMessageSentAlert")]
		void PlayMessageSentAlert ();
	}

	// @interface JSQMessages (NSString)
	[Category]
	[BaseType (typeof(NSString), Name = "NSString_JSQMessages")]
	interface NSStringExtensions
	{
		// -(NSString *)jsq_stringByTrimingWhitespace;
		[Export ("jsq_stringByTrimingWhitespace")]
		string StringByTrimingWhitespace ();
	}

	// @interface JSQMessages (UIColor)
	[Category]
	[BaseType (typeof(UIColor), Name = "JSQMessages")]
	interface UIColorExtensions
	{
//
//		See fixed_class_ptr in Additions.cs 
//		The generator has a bug
//		// +(UIColor *)jsq_messageBubbleGreenColor;
//		[Static]
//		[Export ("jsq_messageBubbleGreenColor")]
//		UIColor MessageBubbleGreenColor { get; }
//
//		// +(UIColor *)jsq_messageBubbleBlueColor;
//		[Static]
//		[Export ("jsq_messageBubbleBlueColor")]
//		UIColor MessageBubbleBlueColor { get; }
//
//		// +(UIColor *)jsq_messageBubbleRedColor;
//		[Static]
//		[Export ("jsq_messageBubbleRedColor")]
//		UIColor MessageBubbleRedColor { get; }
//
//		// +(UIColor *)jsq_messageBubbleLightGrayColor;
//		[Static]
//		[Export ("jsq_messageBubbleLightGrayColor")]
//		UIColor MessageBubbleLightGrayColor { get; }

		// -(UIColor *)jsq_colorByDarkeningColorWithValue:(CGFloat)value;
		[Export ("jsq_colorByDarkeningColorWithValue:")]
		UIColor ColorByDarkeningColorWithValue (nfloat value);
	}

	// @interface JSQMessages (UIImage)
	[Category]
	[BaseType (typeof(UIImage), Name = "UIImage_JSQMessages")]
	interface UIImageExtensions
	{
		// -(UIImage *)jsq_imageMaskedWithColor:(UIColor *)maskColor;
		[Export ("jsq_imageMaskedWithColor:")]
		UIImage ImageMaskedWithColor (UIColor maskColor);
//
//		See fixed_class_ptr in Additions.cs 
//		The generator has a bug
//		// +(UIImage *)jsq_bubbleRegularImage;
//		[Static]
//		[Export ("jsq_bubbleRegularImage")]
//		UIImage BubbleRegularImage { get; }
//
//		// +(UIImage *)jsq_bubbleRegularTaillessImage;
//		[Static]
//		[Export ("jsq_bubbleRegularTaillessImage")]
//		UIImage BubbleRegularTaillessImage { get; }
//
//		// +(UIImage *)jsq_bubbleRegularStrokedImage;
//		[Static]
//		[Export ("jsq_bubbleRegularStrokedImage")]
//		UIImage BubbleRegularStrokedImage { get; }
//
//		// +(UIImage *)jsq_bubbleRegularStrokedTaillessImage;
//		[Static]
//		[Export ("jsq_bubbleRegularStrokedTaillessImage")]
//		UIImage BubbleRegularStrokedTaillessImage { get; }
//
//		// +(UIImage *)jsq_bubbleCompactImage;
//		[Static]
//		[Export ("jsq_bubbleCompactImage")]
//		UIImage BubbleCompactImage { get; }
//
//		// +(UIImage *)jsq_bubbleCompactTaillessImage;
//		[Static]
//		[Export ("jsq_bubbleCompactTaillessImage")]
//		UIImage BubbleCompactTaillessImage { get; }
//
//		// +(UIImage *)jsq_defaultAccessoryImage;
//		[Static]
//		[Export ("jsq_defaultAccessoryImage")]
//		UIImage DefaultAccessoryImage { get; }
//
//		// +(UIImage *)jsq_defaultTypingIndicatorImage;
//		[Static]
//		[Export ("jsq_defaultTypingIndicatorImage")]
//		UIImage DefaultTypingIndicatorImage { get; }
//
//		// +(UIImage *)jsq_defaultPlayImage;
//		[Static]
//		[Export ("jsq_defaultPlayImage")]
//		UIImage DefaultPlayImage { get; }
	}

	// @interface JSQMessages (UIView)
	[Category]
	[BaseType (typeof(UIView), Name = "UIView_JSQMessages")]
	interface UIViewExtensions
	{
		// -(void)jsq_pinSubview:(UIView *)subview toEdge:(NSLayoutAttribute)attribute;
		[Export ("jsq_pinSubview:toEdge:")]
		void PinSubview (UIView subview, NSLayoutAttribute attribute);

		// -(void)jsq_pinAllEdgesOfSubview:(UIView *)subview;
		[Export ("jsq_pinAllEdgesOfSubview:")]
		void PinAllEdgesOfSubview (UIView subview);
	}

	// @interface JSQMessages (NSBundle)
	[Category]
	[BaseType (typeof(NSBundle), Name = "NSBundle_JSQMessages")]
	interface NSBundleExtensions
	{
		// +(NSBundle *)jsq_messagesBundle;
		[Static]
		[Export ("jsq_messagesBundle")]
		NSBundle MessagesBundle { get; }

		// +(NSBundle *)jsq_messagesAssetBundle;
		[Static]
		[Export ("jsq_messagesAssetBundle")]
		NSBundle MessagesAssetBundle { get; }

		// +(NSString *)jsq_localizedStringForKey:(NSString *)key;
		[Static]
		[Export ("jsq_localizedStringForKey:")]
		string LocalizedString (string key);
	}

	// I am guessing all of these Constants should be internal.  

	//	[Verify (ConstantsInterfaceAssociation)] TODO
	//	partial interface Constants
	//	{
	//		// extern const CGFloat kJSQMessagesCollectionViewCellLabelHeightDefault;
	//		[Field ("kJSQMessagesCollectionViewCellLabelHeightDefault")]
	//		nfloat kJSQMessagesCollectionViewCellLabelHeightDefault { get; }
	//
	//		// extern const CGFloat kJSQMessagesCollectionViewAvatarSizeDefault;
	//		[Field ("kJSQMessagesCollectionViewAvatarSizeDefault")]
	//		nfloat kJSQMessagesCollectionViewAvatarSizeDefault { get; }
	//	}

	//	[Verify (ConstantsInterfaceAssociation)] TODO
	//	partial interface Constants
	//	{
	//		// extern const CGFloat kJSQMessagesToolbarContentViewHorizontalSpacingDefault;
	//		[Field ("kJSQMessagesToolbarContentViewHorizontalSpacingDefault")]
	//		nfloat kJSQMessagesToolbarContentViewHorizontalSpacingDefault { get; }
	//	}

	//	[Verify (ConstantsInterfaceAssociation)] TODO
	//	partial interface Constants
	//	{
	//		// extern const CGFloat kJSQMessagesTypingIndicatorFooterViewHeight;
	//		[Field ("kJSQMessagesTypingIndicatorFooterViewHeight")]
	//		nfloat kJSQMessagesTypingIndicatorFooterViewHeight { get; }
	//	}

	//	[Verify (ConstantsInterfaceAssociation)] TODO
	//	partial interface Constants
	//	{
	//		// extern const CGFloat kJSQMessagesLoadEarlierHeaderViewHeight;
	//		[Field ("kJSQMessagesLoadEarlierHeaderViewHeight")]
	//		nfloat kJSQMessagesLoadEarlierHeaderViewHeight { get; }
	//	}

	//	[Verify (ConstantsInterfaceAssociation)] TODO
	//	partial interface Constants
	//	{
	//		// extern NSString *const kJSQSystemSoundTypeCAF;
	//		[Field ("kJSQSystemSoundTypeCAF")]
	//		NSString kJSQSystemSoundTypeCAF { get; }
	//
	//		// extern NSString *const kJSQSystemSoundTypeAIF;
	//		[Field ("kJSQSystemSoundTypeAIF")]
	//		NSString kJSQSystemSoundTypeAIF { get; }
	//
	//		// extern NSString *const kJSQSystemSoundTypeAIFF;
	//		[Field ("kJSQSystemSoundTypeAIFF")]
	//		NSString kJSQSystemSoundTypeAIFF { get; }
	//
	//		// extern NSString *const kJSQSystemSoundTypeWAV;
	//		[Field ("kJSQSystemSoundTypeWAV")]
	//		NSString kJSQSystemSoundTypeWAV { get; }
	//	}

}

