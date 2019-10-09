using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

#if __IOS__ || __TVOS__
using UIKit;
#elif __MACOS__
using AppKit;
using UIEdgeInsets = AppKit.NSEdgeInsets;
using UIView = AppKit.NSView;
#endif

namespace Masonry
{
	// @interface Constraint : NSObject
	[BaseType (typeof(NSObject), Name = "MASConstraint")]
	interface Constraint
	{
		// -(MASConstraint *(^)(UIEdgeInsets))insets;
		[Export ("insets")]
		Func<UIEdgeInsets, Constraint> Insets { get; }

		// -(MASConstraint *(^)(CGSize))sizeOffset;
		[Export ("sizeOffset")]
		Func<CGSize, Constraint> SizeOffset { get; }

		// -(MASConstraint *(^)(CGPoint))centerOffset;
		[Export ("centerOffset")]
		Func<CGPoint, Constraint> CenterOffset { get; }

		// -(MASConstraint *(^)(CGFloat))offset;
		[Export ("offset")]
		Func<nfloat, Constraint> Offset { get; }

		// -(MASConstraint *(^)(NSValue *))valueOffset;
		[Export ("valueOffset")]
		Func<NSValue, Constraint> ValueOffset { get; }

		// -(MASConstraint *(^)(CGFloat))multipliedBy;
		[Export ("multipliedBy")]
		Func<nfloat, Constraint> MultipliedBy { get; }

		// -(MASConstraint *(^)(CGFloat))dividedBy;
		[Export ("dividedBy")]
		Func<nfloat, Constraint> DividedBy { get; }

		// -(MASConstraint *(^)(MASLayoutPriority))priority;
		[Export ("priority")]
		Func<float, Constraint> Priority { get; }

		// -(MASConstraint *(^)())priorityLow;
		[Export ("priorityLow")]
		Func<Constraint> PriorityLow { get; }

		// -(MASConstraint *(^)())priorityMedium;
		[Export ("priorityMedium")]
		Func<Constraint> PriorityMedium { get; }

		// -(MASConstraint *(^)())priorityHigh;
		[Export ("priorityHigh")]
		Func<Constraint> PriorityHigh { get; }

		// -(MASConstraint *(^)(id))equalTo;
		[Export ("equalTo")]
		Func<NSObject, Constraint> EqualTo { get; }

		// -(MASConstraint *(^)(id))greaterThanOrEqualTo;
		[Export ("greaterThanOrEqualTo")]
		Func<NSObject, Constraint> GreaterThanOrEqualTo { get; }

		// -(MASConstraint *(^)(id))lessThanOrEqualTo;
		[Export ("lessThanOrEqualTo")]
		Func<NSObject, Constraint> LessThanOrEqualTo { get; }

		// -(MASConstraint *)with;
		[Export ("with")]
		Constraint With { get; }

		// -(MASConstraint *)and;
		[Export ("and")]
		Constraint And { get; }

		// -(MASConstraint *)left;
		[Export ("left")]
		Constraint Left { get; }

		// -(MASConstraint *)top;
		[Export ("top")]
		Constraint Top { get; }

		// -(MASConstraint *)right;
		[Export ("right")]
		Constraint Right { get; }

		// -(MASConstraint *)bottom;
		[Export ("bottom")]
		Constraint Bottom { get; }

		// -(MASConstraint *)leading;
		[Export ("leading")]
		Constraint Leading { get; }

		// -(MASConstraint *)trailing;
		[Export ("trailing")]
		Constraint Trailing { get; }

		// -(MASConstraint *)width;
		[Export ("width")]
		Constraint Width { get; }

		// -(MASConstraint *)height;
		[Export ("height")]
		Constraint Height { get; }

		// -(MASConstraint *)centerX;
		[Export ("centerX")]
		Constraint CenterX { get; }

		// -(MASConstraint *)centerY;
		[Export ("centerY")]
		Constraint CenterY { get; }

		// -(MASConstraint *)baseline;
		[Export ("baseline")]
		Constraint Baseline { get; }

		// -(MASConstraint *)firstBaseline;
		[Export ("firstBaseline")]
		Constraint FirstBaseline { get; }

		// -(MASConstraint *)lastBaseline;
		[Export ("lastBaseline")]
		Constraint LastBaseline { get; }

#if __IOS__ || __TVOS__

		// -(MASConstraint *)leftMargin;
		[Export ("leftMargin")]
		Constraint LeftMargin { get; }

		// -(MASConstraint *)rightMargin;
		[Export ("rightMargin")]
		Constraint RightMargin { get; }

		// -(MASConstraint *)topMargin;
		[Export ("topMargin")]
		Constraint TopMargin { get; }

		// -(MASConstraint *)bottomMargin;
		[Export ("bottomMargin")]
		Constraint BottomMargin { get; }

		// -(MASConstraint *)leadingMargin;
		[Export ("leadingMargin")]
		Constraint LeadingMargin { get; }

		// -(MASConstraint *)trailingMargin;
		[Export ("trailingMargin")]
		Constraint TrailingMargin { get; }

		// -(MASConstraint *)centerXWithinMargins;
		[Export ("centerXWithinMargins")]
		Constraint CenterXWithinMargins { get; }

		// -(MASConstraint *)centerYWithinMargins;
		[Export ("centerYWithinMargins")]
		Constraint CenterYWithinMargins { get; }

#endif

		// -(MASConstraint *(^)(id))key;
		[Export ("key")]
		Func<NSObject, Constraint> Key { get; }

		// -(void)setInsets:(UIEdgeInsets)insets;
		[Export ("setInsets:")]
		void SetInsets (UIEdgeInsets insets);

		// -(void)setSizeOffset:(CGSize)sizeOffset;
		[Export ("setSizeOffset:")]
		void SetSizeOffset (CGSize sizeOffset);

		// -(void)setCenterOffset:(CGPoint)centerOffset;
		[Export ("setCenterOffset:")]
		void SetCenterOffset (CGPoint centerOffset);

		// -(void)setOffset:(CGFloat)offset;
		[Export ("setOffset:")]
		void SetOffset (nfloat offset);

#if __MACOS__ && !(__IOS__ || __TVOS__)

		// -(MASConstraint *)animator;
		[Export ("animator")]
		Constraint Animator { get; }

#endif

		// -(void)activate;
		[Export ("activate")]
		void Activate ();

		// -(void)deactivate;
		[Export ("deactivate")]
		void Deactivate ();

		// -(void)install;
		[Export ("install")]
		void Install ();

		// -(void)uninstall;
		[Export ("uninstall")]
		void Uninstall ();
	}

	// @interface AutoboxingSupport (Constraint)
	[Category]
	[BaseType (typeof(Constraint))]
	interface ConstraintExtensions
	{
		// -(MASConstraint *(^)(id))mas_equalTo;
		[Export ("mas_equalTo")]
		Func<NSObject, Constraint> EqualTo ();

		// -(MASConstraint *(^)(id))mas_greaterThanOrEqualTo;
		[Export ("mas_greaterThanOrEqualTo")]
		Func<NSObject, Constraint> GreaterThanOrEqualTo ();

		// -(MASConstraint *(^)(id))mas_lessThanOrEqualTo;
		[Export ("mas_lessThanOrEqualTo")]
		Func<NSObject, Constraint> LessThanOrEqualTo ();

		// -(MASConstraint *(^)(id))mas_offset;
		[Export ("mas_offset")]
		Func<NSObject, Constraint> Offset ();
	}

	// @interface MASCompositeConstraint : Constraint
	[BaseType (typeof(Constraint), Name = "MASCompositeConstraint")]
	interface CompositeConstraint
	{
		// -(id)initWithChildren:(NSArray *)children;
		[Export ("initWithChildren:")]
		IntPtr Constructor (Constraint[] children);
	}

	// @interface ConstraintMaker : NSObject
	[BaseType (typeof(NSObject), Name = "MASConstraintMaker")]
	interface ConstraintMaker
	{
		// @property (readonly, nonatomic, strong) MASConstraint * left;
		[Export ("left", ArgumentSemantic.Strong)]
		Constraint Left { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * top;
		[Export ("top", ArgumentSemantic.Strong)]
		Constraint Top { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * right;
		[Export ("right", ArgumentSemantic.Strong)]
		Constraint Right { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * bottom;
		[Export ("bottom", ArgumentSemantic.Strong)]
		Constraint Bottom { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * leading;
		[Export ("leading", ArgumentSemantic.Strong)]
		Constraint Leading { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * trailing;
		[Export ("trailing", ArgumentSemantic.Strong)]
		Constraint Trailing { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * width;
		[Export ("width", ArgumentSemantic.Strong)]
		Constraint Width { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * height;
		[Export ("height", ArgumentSemantic.Strong)]
		Constraint Height { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * centerX;
		[Export ("centerX", ArgumentSemantic.Strong)]
		Constraint CenterX { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * centerY;
		[Export ("centerY", ArgumentSemantic.Strong)]
		Constraint CenterY { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * baseline;
		[Export ("baseline", ArgumentSemantic.Strong)]
		Constraint Baseline { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * firstBaseline;
		[Export ("firstBaseline", ArgumentSemantic.Strong)]
		Constraint FirstBaseline { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * lastBaseline;
		[Export ("lastBaseline", ArgumentSemantic.Strong)]
		Constraint LastBaseline { get; }

#if __IOS__ || __TVOS__

		// @property (readonly, nonatomic, strong) MASConstraint * leftMargin;
		[Export ("leftMargin", ArgumentSemantic.Strong)]
		Constraint LeftMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * rightMargin;
		[Export ("rightMargin", ArgumentSemantic.Strong)]
		Constraint RightMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * topMargin;
		[Export ("topMargin", ArgumentSemantic.Strong)]
		Constraint TopMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * bottomMargin;
		[Export ("bottomMargin", ArgumentSemantic.Strong)]
		Constraint BottomMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * leadingMargin;
		[Export ("leadingMargin", ArgumentSemantic.Strong)]
		Constraint LeadingMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * trailingMargin;
		[Export ("trailingMargin", ArgumentSemantic.Strong)]
		Constraint TrailingMargin { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * centerXWithinMargins;
		[Export ("centerXWithinMargins", ArgumentSemantic.Strong)]
		Constraint CenterXWithinMargins { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * centerYWithinMargins;
		[Export ("centerYWithinMargins", ArgumentSemantic.Strong)]
		Constraint CenterYWithinMargins { get; }

#endif

		// @property (readonly, nonatomic, strong) MASConstraint * (^attributes)(MASAttribute);
		[Export ("attributes", ArgumentSemantic.Strong)]
		Func<Attribute, Constraint> Attributes { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * edges;
		[Export ("edges", ArgumentSemantic.Strong)]
		Constraint Edges { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * size;
		[Export ("size", ArgumentSemantic.Strong)]
		Constraint Size { get; }

		// @property (readonly, nonatomic, strong) MASConstraint * center;
		[Export ("center", ArgumentSemantic.Strong)]
		Constraint Center { get; }

		// @property (assign, nonatomic) BOOL updateExisting;
		[Export ("updateExisting")]
		bool UpdateExisting { get; set; }

		// @property (assign, nonatomic) BOOL removeExisting;
		[Export ("removeExisting")]
		bool RemoveExisting { get; set; }

		// -(id)initWithView:(UIView *)view;
		[Export ("initWithView:")]
		IntPtr Constructor (UIView view);

		// -(NSArray *)install;
		[Export ("install")]
		Constraint[] Install { get; }

		// -(MASConstraint *(^)(dispatch_block_t))group;
		[Export ("group")]
		Func<Action, Constraint> Group { get; }
	}

	// @interface MASLayoutConstraint : NSLayoutConstraint
	[BaseType (typeof(NSLayoutConstraint), Name = "MASLayoutConstraint")]
	interface LayoutConstraint
	{
		// @property (nonatomic, strong) id mas_key;
		[Export ("mas_key", ArgumentSemantic.Strong)]
		NSObject Key { get; set; }
	}

	// @interface ViewAttribute : NSObject
	[BaseType (typeof(NSObject), Name = "MASViewAttribute")]
	interface ViewAttribute
	{
		// @property (readonly, nonatomic, weak) UIView * _Nullable view;
		[NullAllowed, Export ("view", ArgumentSemantic.Weak)]
		UIView View { get; }

		// @property (readonly, nonatomic, weak) id _Nullable item;
		[NullAllowed, Export ("item", ArgumentSemantic.Weak)]
		NSObject Item { get; }

		// @property (readonly, assign, nonatomic) NSLayoutAttribute layoutAttribute;
		[Export ("layoutAttribute", ArgumentSemantic.Assign)]
		NSLayoutAttribute LayoutAttribute { get; }

		// -(id)initWithView:(UIView *)view layoutAttribute:(NSLayoutAttribute)layoutAttribute;
		[Export ("initWithView:layoutAttribute:")]
		IntPtr Constructor (UIView view, NSLayoutAttribute layoutAttribute);

		// -(id)initWithView:(UIView *)view item:(id)item layoutAttribute:(NSLayoutAttribute)layoutAttribute;
		[Export ("initWithView:item:layoutAttribute:")]
		IntPtr Constructor (UIView view, NSObject item, NSLayoutAttribute layoutAttribute);

		// -(BOOL)isSizeAttribute;
		[Export ("isSizeAttribute")]
		bool IsSizeAttribute { get; }
	}

	// @interface MASViewConstraint : Constraint <NSCopying>
	[BaseType (typeof(Constraint), Name = "MASViewConstraint")]
	interface ViewConstraint : INSCopying
	{
		// @property (readonly, nonatomic, strong) MASViewAttribute * firstViewAttribute;
		[Export ("firstViewAttribute", ArgumentSemantic.Strong)]
		ViewAttribute FirstViewAttribute { get; }

		// @property (readonly, nonatomic, strong) MASViewAttribute * secondViewAttribute;
		[Export ("secondViewAttribute", ArgumentSemantic.Strong)]
		ViewAttribute SecondViewAttribute { get; }

		// -(id)initWithFirstViewAttribute:(MASViewAttribute *)firstViewAttribute;
		[Export ("initWithFirstViewAttribute:")]
		IntPtr Constructor (ViewAttribute firstViewAttribute);

		// +(NSArray *)installedConstraintsForView:(UIView *)view;
		[Static]
		[Export ("installedConstraintsForView:")]
		Constraint[] InstalledConstraintsForView (UIView view);
	}

	// @interface MASAdditions (UIView)
	[Category]
	[BaseType (typeof(UIView))]
	interface UIViewExtensions
	{
		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_left;
		[Export ("mas_left", ArgumentSemantic.Strong)]
		ViewAttribute Left ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_top;
		[Export ("mas_top", ArgumentSemantic.Strong)]
		ViewAttribute Top ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_right;
		[Export ("mas_right", ArgumentSemantic.Strong)]
		ViewAttribute Right ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_bottom;
		[Export ("mas_bottom", ArgumentSemantic.Strong)]
		ViewAttribute Bottom ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_leading;
		[Export ("mas_leading", ArgumentSemantic.Strong)]
		ViewAttribute Leading ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_trailing;
		[Export ("mas_trailing", ArgumentSemantic.Strong)]
		ViewAttribute Trailing ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_width;
		[Export ("mas_width", ArgumentSemantic.Strong)]
		ViewAttribute Width ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_height;
		[Export ("mas_height", ArgumentSemantic.Strong)]
		ViewAttribute Height ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_centerX;
		[Export ("mas_centerX", ArgumentSemantic.Strong)]
		ViewAttribute CenterX ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_centerY;
		[Export ("mas_centerY", ArgumentSemantic.Strong)]
		ViewAttribute CenterY ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_baseline;
		[Export ("mas_baseline", ArgumentSemantic.Strong)]
		ViewAttribute Baseline ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_firstBaseline;
		[Export ("mas_firstBaseline", ArgumentSemantic.Strong)]
		ViewAttribute FirstBaseline ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_lastBaseline;
		[Export ("mas_lastBaseline", ArgumentSemantic.Strong)]
		ViewAttribute LastBaseline ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * (^mas_attribute)(NSLayoutAttribute);
		[Export ("mas_attribute", ArgumentSemantic.Strong)]
		Func<NSLayoutAttribute, ViewAttribute> Attribute ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideLeft;
		[Export ("mas_safeAreaLayoutGuideLeft", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideLeft ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideTop;
		[Export ("mas_safeAreaLayoutGuideTop", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideTop ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideRight;
		[Export ("mas_safeAreaLayoutGuideRight", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideRight ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideBottom;
		[Export ("mas_safeAreaLayoutGuideBottom", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideBottom ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideLeading;
		[Export ("mas_safeAreaLayoutGuideLeading", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideLeading ();
		
		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideTrailing;
		[Export ("mas_safeAreaLayoutGuideTrailing", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideTrailing ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideWidth;
		[Export ("mas_safeAreaLayoutGuideWidth", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideWidth ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideHeight;
		[Export ("mas_safeAreaLayoutGuideHeight", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideHeight ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideCenterX;
		[Export ("mas_safeAreaLayoutGuideCenterX", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideCenterX ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuideCenterY;
		[Export ("mas_safeAreaLayoutGuideCenterY", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuideCenterY ();

#if __IOS__ || __TVOS__

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_leftMargin;
		[Export ("mas_leftMargin", ArgumentSemantic.Strong)]
		ViewAttribute LeftMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_rightMargin;
		[Export ("mas_rightMargin", ArgumentSemantic.Strong)]
		ViewAttribute RightMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_topMargin;
		[Export ("mas_topMargin", ArgumentSemantic.Strong)]
		ViewAttribute TopMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_bottomMargin;
		[Export ("mas_bottomMargin", ArgumentSemantic.Strong)]
		ViewAttribute BottomMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_leadingMargin;
		[Export ("mas_leadingMargin", ArgumentSemantic.Strong)]
		ViewAttribute LeadingMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_trailingMargin;
		[Export ("mas_trailingMargin", ArgumentSemantic.Strong)]
		ViewAttribute TrailingMargin ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_centerXWithinMargins;
		[Export ("mas_centerXWithinMargins", ArgumentSemantic.Strong)]
		ViewAttribute CenterXWithinMargins ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_centerYWithinMargins;
		[Export ("mas_centerYWithinMargins", ArgumentSemantic.Strong)]
		ViewAttribute CenterYWithinMargins ();

#endif

		// @property (nonatomic, strong) id mas_key;
		[Export ("mas_key", ArgumentSemantic.Strong)]
		NSObject Key ();

		// @property (nonatomic, strong) id mas_key;
		[Export ("setMas_key:", ArgumentSemantic.Strong)]
		void Key (NSObject value);

		// -(instancetype)mas_closestCommonSuperview:(UIView *)view;
		[Export ("mas_closestCommonSuperview:")]
		UIView ClosestCommonSuperview (UIView view);

		// -(NSArray *)mas_makeConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_makeConstraints:")]
		Constraint[] MakeConstraints (Action<ConstraintMaker> block);

		// -(NSArray *)mas_updateConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_updateConstraints:")]
		Constraint[] UpdateConstraints (Action<ConstraintMaker> block);

		// -(NSArray *)mas_remakeConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_remakeConstraints:")]
		Constraint[] RemakeConstraints (Action<ConstraintMaker> block);
	}

#if __IOS__ || __TVOS__

	// @interface MASAdditions (UIViewController)
	[Category]
	[BaseType (typeof(UIViewController))]
	interface UIViewControllerExtensions
	{
		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_topLayoutGuide;
		[Export ("mas_topLayoutGuide", ArgumentSemantic.Strong)]
		ViewAttribute TopLayoutGuide ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_bottomLayoutGuide;
		[Export ("mas_bottomLayoutGuide", ArgumentSemantic.Strong)]
		ViewAttribute BottomLayoutGuide ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_topLayoutGuideTop;
		[Export ("mas_topLayoutGuideTop", ArgumentSemantic.Strong)]
		ViewAttribute TopLayoutGuideTop ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_topLayoutGuideBottom;
		[Export ("mas_topLayoutGuideBottom", ArgumentSemantic.Strong)]
		ViewAttribute TopLayoutGuideBottom ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_bottomLayoutGuideTop;
		[Export ("mas_bottomLayoutGuideTop", ArgumentSemantic.Strong)]
		ViewAttribute BottomLayoutGuideTop ();

		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_bottomLayoutGuideBottom;
		[Export ("mas_bottomLayoutGuideBottom", ArgumentSemantic.Strong)]
		ViewAttribute BottomLayoutGuideBottom ();
		
		// @property (readonly, nonatomic, strong) MASViewAttribute * mas_safeAreaLayoutGuide;
		[Export ("mas_safeAreaLayoutGuide", ArgumentSemantic.Strong)]
		ViewAttribute SafeAreaLayoutGuide ();
	}

#endif

	// @interface MASAdditions (NSArray)
	[Category]
	[BaseType (typeof(NSArray))]
	interface NSArrayExtensions
	{
		// -(NSArray *)mas_makeConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_makeConstraints:")]
		Constraint[] MakeConstraints (Action<ConstraintMaker> block);

		// -(NSArray *)mas_updateConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_updateConstraints:")]
		Constraint[] UpdateConstraints (Action<ConstraintMaker> block);

		// -(NSArray *)mas_remakeConstraints:(void (^)(ConstraintMaker *))block;
		[Export ("mas_remakeConstraints:")]
		Constraint[] RemakeConstraints (Action<ConstraintMaker> block);

		// -(void)mas_distributeViewsAlongAxis:(MASAxisType)axisType withFixedSpacing:(CGFloat)fixedSpacing leadSpacing:(CGFloat)leadSpacing tailSpacing:(CGFloat)tailSpacing;
		[Export ("mas_distributeViewsAlongAxis:withFixedSpacing:leadSpacing:tailSpacing:")]
		void DistributeViewsWithFixedSpacing (AxisType axisType, nfloat fixedSpacing, nfloat leadSpacing, nfloat tailSpacing);

		// -(void)mas_distributeViewsAlongAxis:(MASAxisType)axisType withFixedItemLength:(CGFloat)fixedItemLength leadSpacing:(CGFloat)leadSpacing tailSpacing:(CGFloat)tailSpacing;
		[Export ("mas_distributeViewsAlongAxis:withFixedItemLength:leadSpacing:tailSpacing:")]
		void DistributeViewsWithFixedItemLength (AxisType axisType, nfloat fixedItemLength, nfloat leadSpacing, nfloat tailSpacing);
	}
}
