using System;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreGraphics;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace SWTableViewCells
{
	//  NSMutableArray+SWUtilityButtons.h

	[Category]
	[BaseType (typeof(NSMutableArray))]
	interface SWUtilityButtonsForNSMutableArray
	{
		// @required - (void)sw_addUtilityButtonWithColor:(UIColor)color title:(NSString *)title;
		[Export ("sw_addUtilityButtonWithColor:title:")]
		void AddUtilityButton (UIColor color, string title);

		// @required - (void)sw_addUtilityButtonWithColor:(UIColor *)color attributedTitle:(NSAttributedString *)title;
		[Export ("sw_addUtilityButtonWithColor:title:")]
		void AddUtilityButton (UIColor color, NSAttributedString title);

		// @required - (void)sw_addUtilityButtonWithColor:(UIColor)color icon:(id)icon;
		[Export ("sw_addUtilityButtonWithColor:icon:")]
		void AddUtilityButton (UIColor color, UIImage icon);

		// @required - (void)sw_addUtilityButtonWithColor:(UIColor)color normalIcon:(id)normalIcon selectedIcon:(id)selectedIcon;
		[Export ("sw_addUtilityButtonWithColor:normalIcon:selectedIcon:")]
		void AddUtilityButton (UIColor color, UIImage normalIcon, UIImage selectedIcon);
	}

	[Category]
	[BaseType (typeof(NSArray))]
	interface SWUtilityButtonsForNSArray
	{
		// @required - (BOOL)sw_isEqualToButtons:(NSArray *)buttons;
		[Export ("sw_isEqualToButtons:")]
		bool IsEqualToButtons (UIButton[] buttons);
	}


	//  SWCellScrollView.h

	[BaseType (typeof(UIScrollView), Name = "SWCellScrollView")]
	interface SWCellScrollView : IUIGestureRecognizerDelegate
	{
	}


	//  SWLongPressGestureRecognizer.h

	[BaseType (typeof(UILongPressGestureRecognizer), Name = "SWLongPressGestureRecognizer")]
	interface SWLongPressGestureRecognizer
	{
	}


	//  SWTableViewCell.h

	interface ISWTableViewCellDelegate
	{
	}

	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "SWTableViewCellDelegate")]
	interface SWTableViewCellDelegate
	{
		// @optional - (void)swipeableTableViewCell:(SWTableViewCell *)cell didTriggerLeftUtilityButtonWithIndex:(NSInteger)index;
		[Export ("swipeableTableViewCell:didTriggerLeftUtilityButtonWithIndex:")]
		void DidTriggerLeftUtilityButton (SWTableViewCell cell, nint index);

		// @optional - (void)swipeableTableViewCell:(SWTableViewCell *)cell didTriggerRightUtilityButtonWithIndex:(NSInteger)index;
		[Export ("swipeableTableViewCell:didTriggerRightUtilityButtonWithIndex:")]
		void DidTriggerRightUtilityButton (SWTableViewCell cell, nint index);

		// @optional - (void)swipeableTableViewCell:(SWTableViewCell *)cell scrollingToState:(SWCellState)state;
		[Export ("swipeableTableViewCell:scrollingToState:")]
		void ScrollingToState (SWTableViewCell cell, SWCellState state);

		// @optional - (BOOL)swipeableTableViewCellShouldHideUtilityButtonsOnSwipe:(SWTableViewCell *)cell;
		[Export ("swipeableTableViewCellShouldHideUtilityButtonsOnSwipe:")]
		bool ShouldHideUtilityButtonsOnSwipe (SWTableViewCell cell);

		// @optional - (BOOL)swipeableTableViewCell:(SWTableViewCell *)cell canSwipeToState:(SWCellState)state;
		[Export ("swipeableTableViewCell:canSwipeToState:")]
		bool CanSwipeToState (SWTableViewCell cell, SWCellState state);

		// @optional - (void)swipeableTableViewCellDidEndScrolling:(SWTableViewCell *)cell;
		[Export ("swipeableTableViewCellDidEndScrolling:")]
		void DidEndScrolling (SWTableViewCell cell);
	}

	[BaseType (typeof(UITableViewCell), Name = "SWTableViewCell")]
	interface SWTableViewCell
	{
		// @property (copy, nonatomic) NSArray * leftUtilityButtons;
		[Export ("leftUtilityButtons", ArgumentSemantic.Copy)]
		UIButton [] LeftUtilityButtons { get; set; }

		// @property (copy, nonatomic) NSArray * rightUtilityButtons;
		[Export ("rightUtilityButtons", ArgumentSemantic.Copy)]
		UIButton [] RightUtilityButtons { get; set; }

		// @property (nonatomic, weak) id<SWTableViewCellDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.Weak)]
		ISWTableViewCellDelegate Delegate { get; set; }

		// @required - (void)setRightUtilityButtons:(NSArray *)rightUtilityButtons WithButtonWidth:(CGFloat)width;
		[Export ("setRightUtilityButtons:WithButtonWidth:")]
		void SetRightUtilityButtons (UIButton[] rightUtilityButtons, nfloat width);

		// @required - (void)setLeftUtilityButtons:(NSArray *)leftUtilityButtons WithButtonWidth:(CGFloat)width;
		[Export ("setLeftUtilityButtons:WithButtonWidth:")]
		void SetLeftUtilityButtons (UIButton[] leftUtilityButtons, nfloat width);

		// @required - (void)hideUtilityButtonsAnimated:(BOOL)animated;
		[Export ("hideUtilityButtonsAnimated:")]
		void HideUtilityButtons (bool animated);

		// @required - (void)showLeftUtilityButtonsAnimated:(BOOL)animated;
		[Export ("showLeftUtilityButtonsAnimated:")]
		void ShowLeftUtilityButtons (bool animated);

		// @required - (void)showRightUtilityButtonsAnimated:(BOOL)animated;
		[Export ("showRightUtilityButtonsAnimated:")]
		void ShowRightUtilityButtons (bool animated);

		// @required - (BOOL)isUtilityButtonsHidden;
		[Export ("isUtilityButtonsHidden")]
		bool IsUtilityButtonsHidden { get; }
	}


	//  SWUtilityButtonTapGestureRecognizer.h

	[BaseType (typeof(UITapGestureRecognizer), Name = "SWUtilityButtonTapGestureRecognizer")]
	interface SWUtilityButtonTapGestureRecognizer
	{
		// @property (nonatomic) NSUInteger buttonIndex;
		[Export ("buttonIndex")]
		nuint ButtonIndex { get; set; }
	}


	//  SWUtilityButtonView.h

	[BaseType (typeof(UIView), Name = "SWUtilityButtonView")]
	interface SWUtilityButtonView
	{
		// @required - (id)initWithUtilityButtons:(NSArray *)utilityButtons parentCell:(SWTableViewCell *)parentCell utilityButtonSelector:(SEL)utilityButtonSelector;
		[Export ("initWithUtilityButtons:parentCell:utilityButtonSelector:")]
		IntPtr Constructor (NSObject[] utilityButtons, SWTableViewCell parentCell, Selector utilityButtonSelector);

		// @required - (id)initWithFrame:(CGRect)frame utilityButtons:(NSArray *)utilityButtons parentCell:(SWTableViewCell *)parentCell utilityButtonSelector:(SEL)utilityButtonSelector;
		[Export ("initWithFrame:utilityButtons:parentCell:utilityButtonSelector:")]
		IntPtr Constructor (CGRect frame, NSObject[] utilityButtons, SWTableViewCell parentCell, Selector utilityButtonSelector);

		// @property (readonly, nonatomic, weak) SWTableViewCell * parentCell;
		[Export ("parentCell", ArgumentSemantic.Weak)]
		SWTableViewCell ParentCell { get; }

		// @property (copy, nonatomic) NSArray * utilityButtons;
		[Export ("utilityButtons", ArgumentSemantic.Copy)]
		NSObject [] UtilityButtons { get; set; }

		// @property (assign, nonatomic) SEL utilityButtonSelector;
		[Export ("utilityButtonSelector", ArgumentSemantic.UnsafeUnretained)]
		Selector UtilityButtonSelector { get; set; }

		// @required - (void)setUtilityButtons:(NSArray *)utilityButtons WithButtonWidth:(CGFloat)width;
		[Export ("setUtilityButtons:WithButtonWidth:")]
		void SetUtilityButtons (NSObject[] utilityButtons, nfloat width);

		// @required - (void)pushBackgroundColors;
		[Export ("pushBackgroundColors")]
		void PushBackgroundColors ();

		// @required - (void)popBackgroundColors;
		[Export ("popBackgroundColors")]
		void PopBackgroundColors ();
	}
}
