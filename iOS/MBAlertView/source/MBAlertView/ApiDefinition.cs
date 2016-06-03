using System;
using System.Drawing;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreAnimation;
using CoreGraphics;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;

using CGSize = global::System.Drawing.SizeF;
#endif

namespace AlertView
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     PointF Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://docs.xamarin.com/ios/advanced_topics/binding_objective-c_types
	//

	public delegate void MBAlertViewDismissalHandler ();
	public delegate void MBAlertViewButtonHandler ();
	public delegate void MBAlertViewItemHandler ();
	public delegate void MBFlatAlertButtonHandler ();

	[BaseType (typeof(UIViewController))]
	public interface MBAlertAbstract
	{

		[Notification]
		[Field ("MBAlertDidAppearNotification", "__Internal")]
		NSString DidAppearNotification { get; }

		[Notification]
		[Field ("MBAlertDidDismissNotification", "__Internal")]
		NSString DidDismissNotification { get; }

		[Export ("uponDismissalBlock", ArgumentSemantic.Copy)]
		MBAlertViewDismissalHandler UponDismissalHandler { get; set; }

		[Export ("addsToWindow", ArgumentSemantic.Assign)]
		bool AddsToWindow { get; set; }

		[Export ("shouldPerformBlockAfterDismissal", ArgumentSemantic.Assign)]
		bool ShouldPerformHandlerAfterDismissal { get; set; }

		[Export ("hideTimer", ArgumentSemantic.Retain)]
		NSTimer HideTimer { get; set; }

		[Export ("parentView", ArgumentSemantic.Retain)]
		UIView ParentView { get; set; }

		[Export ("animationType")]
		MBAlertAnimationType animationType { get; set; }

		[Export ("dismiss")]
		void Dismiss ();

		[Export ("addToDisplayQueue")]
		void AddToDisplayQueue ();

		[Export ("show")]
		void Show ();

		[Export ("addToWindow")]
		void AddToWindow ();

		[Export ("performLayout")]
		void PerformLayout ();

		[Export ("isOnScreen")]
		bool IsOnScreen ();

		[Static, Export ("alertIsVisible")]
		bool AlertIsVisible { get; }

		[Static, Export ("dismissCurrentHUD")]
		void DismissCurrentHUD ();

		[Static, Export ("dismissCurrentHUDAfterDelay:")]
		void DismissCurrentHUD (float delay);
	}

	// .Classic

	[BaseType (typeof(MBAlertAbstract))]
	interface MBAlertView : IUIGestureRecognizerDelegate
	{

		[Field ("MBAlertViewMaxHUDDisplayTime", "__Internal")]
		float MaxHUDDisplayTime { get; set; }

		[Field ("MBAlertViewDefaultHUDHideDelay", "__Internal")]
		float DefaultHUDHideDelay { get; set; }

		[Export ("iconOffset", ArgumentSemantic.Assign)]
		CGSize IconOffset { get; set; }

		[Export ("bodyText", ArgumentSemantic.Copy)]
		string BodyText { get; set; }

		[Export ("bodyFont")]
		UIFont BodyFont { get; set; }

		[Export ("imageView", ArgumentSemantic.Retain)]
		UIImageView ImageView { get; set; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		[Export ("backgroundAlpha")]
		float BackgroundAlpha { get; set; }

		[Export ("addButtonWithText:type:block:")]
		void AddButtonWithText (string text, MBAlertViewItemType bType, [NullAllowed] MBAlertViewButtonHandler handler);

		[Static, Export ("alertWithBody:cancelTitle:cancelBlock:")]
		MBAlertView AlertWithBody (string body, [NullAllowed] string buttonTitle, [NullAllowed] MBAlertViewButtonHandler handler);

		[Static, Export ("halfScreenSize")]
		CGSize HalfScreenSize { get; }
	}

	[BaseType (typeof(NSObject))]
	public interface MBAlertViewItem
	{

		[Export ("block", ArgumentSemantic.Copy)]
		NSObject Block { get; set; }

		[Export ("title", ArgumentSemantic.Copy)]
		string ButtonTitle { get; set; }

		[Export ("type")]
		MBAlertViewItemType Type { get; set; }

		[Export ("initWithTitle:type:block:")]
		IntPtr Constructor (string text, MBAlertViewItemType type, MBAlertViewItemHandler block);
	}

	[BaseType (typeof(UIButton))]
	public interface MBAlertViewButton
	{
		[Export ("title", ArgumentSemantic.Copy)]
		string ButtonTitle { get; set; }

		[Export ("alertButtonType")]
		MBAlertViewItemType AlertButtonType { get; set; }

		[Export ("initWithTitle:")]
		IntPtr Constructor (string title);
	}

	// .Hud

	[BaseType (typeof (UIView))]
	public interface MBCheckMarkView {

		[Export ("color", ArgumentSemantic.Retain)]
		UIColor Color { get; set; }

		[Export ("size")]
		MBCheckmarkSize Size { get; set; }

		[Static, Export ("checkMarkWithSize:color:")]
		MBCheckMarkView CheckMarkWithSize (MBCheckmarkSize size, UIColor color);
	}

	[BaseType (typeof(MBAlertView))]
	interface MBHUDView
	{
		[Export ("hudType")]
		MBAlertViewHUDType HudType { get; set; }

		[Export ("hudHideDelay")]
		float HudHideDelay { get; set; }

		[Export ("bodyOffset", ArgumentSemantic.Assign)]
		SizeF BodyOffset { get; set; }

		[Export ("iconLabel")]
		UILabel IconLabel { get; set; }

		[Export ("backgroundColor")]
		UIColor BackgroundColor { get; set; }

		[Export ("itemColor", ArgumentSemantic.Retain)]
		UIColor ItemColor { get; set; }

		[Static, Export ("hudWithBody:type:hidesAfter:show:")]
		MBHUDView HudWithBody (string body, MBAlertViewHUDType aType, float delay, bool showNow);
	}

	[BaseType (typeof (UIView))]
	public interface MBSpinningCircle {

		[Export ("color", ArgumentSemantic.Retain)]
		UIColor Color { get; set; }

		[Export ("isAnimating")]
		bool IsAnimating { get; set; }

		[Export ("hasGlow")]
		bool HasGlow { get; set; }

		[Export ("speed")]
		float Speed { get; set; }

		[Export ("circleSize")]
		NSSpinningCircleSize CircleSize { get; set; }

		[Static, Export ("circleWithSize:color:")]
		MBSpinningCircle CircleWithSize (NSSpinningCircleSize size, UIColor color);
	}

	// .Flat


	[BaseType (typeof (MBAlertAbstract))]
	public partial interface MBFlatAlertAbstract {

		[Export ("backgroundView", ArgumentSemantic.Retain)]
		UIView BackgroundView { get; set; }

		[Export ("addDismissAnimation")]
		void AddDismissAnimation ();

		[Export ("viewToApplyPresentationAnimationsOn")]
		UIView ViewToApplyPresentationAnimationsOn { get; }

		[Export ("configureBackgroundView")]
		void ConfigureBackgroundView ();

		[Static, Export ("flatDismissAnimation")]
		CAAnimation FlatDismissAnimation { get; }
	}

	[BaseType (typeof (UIButton))]
	public partial interface MBFlatAlertButton {

		[Export ("action", ArgumentSemantic.Copy)]
		MBFlatAlertButtonHandler Action { get; set; }

		[Export ("title", ArgumentSemantic.Copy)]
		string ButtonTitle { get; set; }

		[Export ("textLabel", ArgumentSemantic.Retain)]
		UILabel TextLabel { get; set; }

		[Export ("type")]
		MBFlatAlertButtonType Type { get; set; }

		[Export ("textColor", ArgumentSemantic.Retain)]
		UIColor TextColor { get; set; }

		[Export ("hasRightStroke")]
		bool HasRightStroke { get; set; }

		[Export ("hasBottomStroke")]
		bool HasBottomStroke { get; set; }

		[Static, Export ("defaultTextColor")]
		UIColor DefaultTextColor { get; }

		[Static, Export ("buttonWithTitle:type:action:")]
		MBFlatAlertButton ButtonWithTitle (string title, MBFlatAlertButtonType type, MBFlatAlertButtonHandler action);
	}

	[BaseType (typeof (MBFlatAlertAbstract))]
	public partial interface MBFlatAlertView {

		[Export ("alertTitle", ArgumentSemantic.Copy)]
		string AlertTitle { get; set; }

		[Export ("detailText", ArgumentSemantic.Copy)]
		string DetailText { get; set; }

		[Export ("titleLabel", ArgumentSemantic.Retain)]
		UILabel TitleLabel { get; set; }

		[Export ("detailsLabel", ArgumentSemantic.Retain)]
		UILabel DetailsLabel { get; set; }

		[Export ("contentView")]
		UIView ContentView { get; }

		[Export ("contentViewHeight")]
		float ContentViewHeight { get; set; }

		[Export ("isRounded")]
		bool IsRounded { get; set; }

		[Export ("horizontalMargin")]
		float HorizontalMargin { get; set; }

		[Export ("dismissesOnButtonPress")]
		bool DismissesOnButtonPress { get; set; }

		[Export ("addButtonWithTitle:type:action:")]
		void AddButtonWithTitle (string title, MBFlatAlertButtonType type, MBFlatAlertButtonHandler action);

		[Export ("addButton:")]
		void AddButton (MBFlatAlertButton button);

		[Static, Export ("alertWithTitle:detailText:cancelTitle:cancelBlock:")]
		MBFlatAlertView AlertWithTitle (string title, string detailText, [NullAllowed] string cancelTitle, [NullAllowed] MBFlatAlertButtonHandler cancelBlock);
	}
}

