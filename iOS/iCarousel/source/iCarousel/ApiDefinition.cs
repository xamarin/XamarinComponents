using System;

using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;

#if __IOS__
using UIKit;
#else
using AppKit;
using UIView = AppKit.NSView;
#endif

namespace Carousels
{
	// @interface iCarousel : UIView
	[BaseType (typeof(UIView), Delegates = new [] { "Delegate" }, Events = new [] { typeof(iCarouselDelegate) })]
	interface iCarousel
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// @property (nonatomic, unsafe_unretained) id<iCarouselDataSource> dataSource;
		[Export ("dataSource", ArgumentSemantic.UnsafeUnretained)]
		iCarouselDataSource DataSource { get; set; }

		// @property (nonatomic, unsafe_unretained) id<iCarouselDelegate> delegate;
		[NullAllowed]
		[Export ("delegate", ArgumentSemantic.UnsafeUnretained)]
		iCarouselDelegate Delegate { get; set; }

		// @property (assign, nonatomic) iCarouselType type;
		[Export ("type", ArgumentSemantic.Assign)]
		iCarouselType Type { get; set; }

		// @property (assign, nonatomic) CGFloat perspective;
		[Export ("perspective", ArgumentSemantic.Assign)]
		nfloat Perspective { get; set; }

		// @property (assign, nonatomic) CGFloat decelerationRate;
		[Export ("decelerationRate", ArgumentSemantic.Assign)]
		nfloat DecelerationRate { get; set; }

		// @property (assign, nonatomic) CGFloat scrollSpeed;
		[Export ("scrollSpeed", ArgumentSemantic.Assign)]
		nfloat ScrollSpeed { get; set; }

		// @property (assign, nonatomic) CGFloat bounceDistance;
		[Export ("bounceDistance", ArgumentSemantic.Assign)]
		nfloat BounceDistance { get; set; }

		// @property (assign, nonatomic, getter = isScrollEnabled) BOOL scrollEnabled;
		[Export ("scrollEnabled", ArgumentSemantic.Assign)]
		bool ScrollEnabled { [Bind ("isScrollEnabled")] get; set; }

		// @property (assign, nonatomic, getter = isPagingEnabled) BOOL pagingEnabled;
		[Export ("pagingEnabled", ArgumentSemantic.Assign)]
		bool PagingEnabled { [Bind ("isPagingEnabled")] get; set; }

		// @property (assign, nonatomic, getter = isVertical) BOOL vertical;
		[Export ("vertical", ArgumentSemantic.Assign)]
		bool Vertical { [Bind ("isVertical")] get; set; }

		// @property (readonly, nonatomic, getter = isWrapEnabled) BOOL wrapEnabled;
		[Export ("wrapEnabled")]
		bool WrapEnabled { [Bind ("isWrapEnabled")] get; }

		// @property (assign, nonatomic) BOOL bounces;
		[Export ("bounces", ArgumentSemantic.Assign)]
		bool Bounces { get; set; }

		// @property (assign, nonatomic) CGFloat scrollOffset;
		[Export ("scrollOffset", ArgumentSemantic.Assign)]
		nfloat ScrollOffset { get; set; }

		// @property (readonly, nonatomic) CGFloat offsetMultiplier;
		[Export ("offsetMultiplier")]
		nfloat OffsetMultiplier { get; }

		// @property (assign, nonatomic) CGSize contentOffset;
		[Export ("contentOffset", ArgumentSemantic.Assign)]
		CGSize ContentOffset { get; set; }

		// @property (assign, nonatomic) CGSize viewpointOffset;
		[Export ("viewpointOffset", ArgumentSemantic.Assign)]
		CGSize ViewpointOffset { get; set; }

		// @property (readonly, nonatomic) NSInteger numberOfItems;
		[Export ("numberOfItems")]
		nint NumberOfItems { get; }

		// @property (readonly, nonatomic) NSInteger numberOfPlaceholders;
		[Export ("numberOfPlaceholders")]
		nint NumberOfPlaceholders { get; }

		// @property (assign, nonatomic) NSInteger currentItemIndex;
		[Export ("currentItemIndex", ArgumentSemantic.Assign)]
		nint CurrentItemIndex { get; set; }

		// @property (readonly, nonatomic, strong) UIView * currentItemView;
		[Export ("currentItemView", ArgumentSemantic.Retain)]
		UIView CurrentItemView { get; }

		// @property (readonly, nonatomic, strong) NSArray * indexesForVisibleItems;
		[Export ("indexesForVisibleItems", ArgumentSemantic.Retain)]
		NSObject [] IndexesForVisibleItems { get; }

		// @property (readonly, nonatomic) NSInteger numberOfVisibleItems;
		[Export ("numberOfVisibleItems")]
		nint NumberOfVisibleItems { get; }

		// @property (readonly, nonatomic, strong) NSArray * visibleItemViews;
		[Export ("visibleItemViews", ArgumentSemantic.Retain)]
		NSObject [] VisibleItemViews { get; }

		// @property (readonly, nonatomic) CGFloat itemWidth;
		[Export ("itemWidth")]
		nfloat ItemWidth { get; }

		// @property (readonly, nonatomic, strong) UIView * contentView;
		[Export ("contentView", ArgumentSemantic.Retain)]
		UIView ContentView { get; }

		// @property (readonly, nonatomic) CGFloat toggle;
		[Export ("toggle")]
		nfloat Toggle { get; }

		// @property (assign, nonatomic) CGFloat autoscroll;
		[Export ("autoscroll", ArgumentSemantic.Assign)]
		nfloat Autoscroll { get; set; }

		// @property (assign, nonatomic) BOOL stopAtItemBoundary;
		[Export ("stopAtItemBoundary", ArgumentSemantic.Assign)]
		bool StopAtItemBoundary { get; set; }

		// @property (assign, nonatomic) BOOL scrollToItemBoundary;
		[Export ("scrollToItemBoundary", ArgumentSemantic.Assign)]
		bool ScrollToItemBoundary { get; set; }

		// @property (assign, nonatomic) BOOL ignorePerpendicularSwipes;
		[Export ("ignorePerpendicularSwipes", ArgumentSemantic.Assign)]
		bool IgnorePerpendicularSwipes { get; set; }

		// @property (assign, nonatomic) BOOL centerItemWhenSelected;
		[Export ("centerItemWhenSelected", ArgumentSemantic.Assign)]
		bool CenterItemWhenSelected { get; set; }

		// @property (readonly, nonatomic, getter = isDragging) BOOL dragging;
		[Export ("dragging")]
		bool Dragging { [Bind ("isDragging")] get; }

		// @property (readonly, nonatomic, getter = isDecelerating) BOOL decelerating;
		[Export ("decelerating")]
		bool Decelerating { [Bind ("isDecelerating")] get; }

		// @property (readonly, nonatomic, getter = isScrolling) BOOL scrolling;
		[Export ("scrolling")]
		bool Scrolling { [Bind ("isScrolling")] get; }

		// -(void)scrollByOffset:(CGFloat)offset duration:(NSTimeInterval)duration;
		[Export ("scrollByOffset:duration:")]
		void ScrollBy (nfloat offset, double duration);

		// -(void)scrollToOffset:(CGFloat)offset duration:(NSTimeInterval)duration;
		[Export ("scrollToOffset:duration:")]
		void ScrollTo (nfloat offset, double duration);

		// -(void)scrollByNumberOfItems:(NSInteger)itemCount duration:(NSTimeInterval)duration;
		[Export ("scrollByNumberOfItems:duration:")]
		void ScrollByNumberOfItems (nint itemCount, double duration);

		// -(void)scrollToItemAtIndex:(NSInteger)index duration:(NSTimeInterval)duration;
		[Export ("scrollToItemAtIndex:duration:")]
		void ScrollToItemAt (nint index, double duration);

		// -(void)scrollToItemAtIndex:(NSInteger)index animated:(BOOL)animated;
		[Export ("scrollToItemAtIndex:animated:")]
		void ScrollToItemAt (nint index, bool animated);

		// -(UIView *)itemViewAtIndex:(NSInteger)index;
		[Export ("itemViewAtIndex:")]
		UIView GetItemViewAt (nint index);

		// -(NSInteger)indexOfItemView:(UIView *)view;
		[Export ("indexOfItemView:")]
		nint GetIndexOfItemView (UIView view);

		// -(NSInteger)indexOfItemViewOrSubview:(UIView *)view;
		[Export ("indexOfItemViewOrSubview:")]
		nint GetIndexOfItemViewOrSubview (UIView view);

		// -(CGFloat)offsetForItemAtIndex:(NSInteger)index;
		[Export ("offsetForItemAtIndex:")]
		nfloat GetOffsetForItemAt (nint index);

		// -(UIView *)itemViewAtPoint:(CGPoint)point;
		[Export ("itemViewAtPoint:")]
		UIView GetItemViewAt (CGPoint point);

		// -(void)removeItemAtIndex:(NSInteger)index animated:(BOOL)animated;
		[Export ("removeItemAtIndex:animated:")]
		void RemoveItemAt (nint index, bool animated);

		// -(void)insertItemAtIndex:(NSInteger)index animated:(BOOL)animated;
		[Export ("insertItemAtIndex:animated:")]
		void InsertItemAt (nint index, bool animated);

		// -(void)reloadItemAtIndex:(NSInteger)index animated:(BOOL)animated;
		[Export ("reloadItemAtIndex:animated:")]
		void ReloadItemAt (nint index, bool animated);

		// -(void)reloadData;
		[Export ("reloadData")]
		void ReloadData ();
	}

	// @protocol iCarouselDataSource <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface iCarouselDataSource
	{

		// @required -(NSInteger)numberOfItemsInCarousel:(iCarousel *)carousel;
		[Export ("numberOfItemsInCarousel:")]
		[Abstract]
		nint GetNumberOfItems (iCarousel carousel);

		// @required -(UIView *)carousel:(iCarousel *)carousel viewForItemAtIndex:(NSInteger)index reusingView:(UIView *)view;
		[Export ("carousel:viewForItemAtIndex:reusingView:")]
		[Abstract]
		UIView GetViewForItem (iCarousel carousel, nint index, [NullAllowed] UIView view);

		// @optional -(NSInteger)numberOfPlaceholdersInCarousel:(iCarousel *)carousel;
		[Export ("numberOfPlaceholdersInCarousel:")]
		nint GetNumberOfPlaceholders (iCarousel carousel);

		// @optional -(UIView *)carousel:(iCarousel *)carousel placeholderViewAtIndex:(NSInteger)index reusingView:(UIView *)view;
		[Export ("carousel:placeholderViewAtIndex:reusingView:")]
		UIView GetPlaceholderView (iCarousel carousel, nint index, [NullAllowed] UIView view);
	}

	// @protocol iCarouselDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface iCarouselDelegate
	{

		// @optional -(void)carouselWillBeginScrollingAnimation:(iCarousel *)carousel;
		[Export ("carouselWillBeginScrollingAnimation:")]
		[EventName("ScrollAnimationBegin")]
		void OnScrollAnimationBegin (iCarousel carousel);

		// @optional -(void)carouselDidEndScrollingAnimation:(iCarousel *)carousel;
		[Export ("carouselDidEndScrollingAnimation:")]
		[EventName("ScrollAnimationEnd")]
		void OnScrollAnimationEnd (iCarousel carousel);

		// @optional -(void)carouselDidScroll:(iCarousel *)carousel;
		[Export ("carouselDidScroll:")]
		[EventName("ScrollEnd")]
		void OnScrollEnd (iCarousel carousel);

		// @optional -(void)carouselCurrentItemIndexDidChange:(iCarousel *)carousel;
		[Export ("carouselCurrentItemIndexDidChange:")]
		[EventName("CurrentItemIndexChanged")]
		void OnCurrentItemIndexChanged (iCarousel carousel);

		// @optional -(void)carouselWillBeginDragging:(iCarousel *)carousel;
		[Export ("carouselWillBeginDragging:")]
		[EventName("DragStart")]
		void OnDragStart (iCarousel carousel);

		// @optional -(void)carouselDidEndDragging:(iCarousel *)carousel willDecelerate:(BOOL)decelerate;
		[Export ("carouselDidEndDragging:willDecelerate:")]
		[EventArgs ("iCarouselDragEnd")]
		[EventName("DragEnd")]
		void OnDragEnd (iCarousel carousel, bool decelerate);

		// @optional -(void)carouselWillBeginDecelerating:(iCarousel *)carousel;
		[Export ("carouselWillBeginDecelerating:")]
		[EventName("DecelerationBegin")]
		void OnDecelerationBegin (iCarousel carousel);

		// @optional -(void)carouselDidEndDecelerating:(iCarousel *)carousel;
		[Export ("carouselDidEndDecelerating:")]
		[EventName("DecelerationEnd")]
		void OnDecelerationEnd (iCarousel carousel);

		// @optional -(BOOL)carousel:(iCarousel *)carousel shouldSelectItemAtIndex:(NSInteger)index;
		[Export ("carousel:shouldSelectItemAtIndex:")]
		[DelegateName ("iCarouselSelectItemCondition"), DefaultValue ("true")]
		bool ShouldSelectItem (iCarousel carousel, nint index);

		// @optional -(void)carousel:(iCarousel *)carousel didSelectItemAtIndex:(NSInteger)index;
		[Export ("carousel:didSelectItemAtIndex:")]
		[EventArgs ("iCarouselItemSelected")]
		[EventName("ItemSelected")]
		void OnItemSelected (iCarousel carousel, nint index);

		// @optional -(CGFloat)carouselItemWidth:(iCarousel *)carousel;
		[Export ("carouselItemWidth:")]
		[DelegateName ("iCarouselItemWidthCondition"), DefaultValue ("0")]
		nfloat GetItemWidth (iCarousel carousel);

		// @optional -(CATransform3D)carousel:(iCarousel *)carousel itemTransformForOffset:(CGFloat)offset baseTransform:(CATransform3D)transform;
		[Export ("carousel:itemTransformForOffset:baseTransform:")]
		[DelegateName ("iCarouselItemTransformCondition"), DefaultValue ("transform")]
		CATransform3D GetItemTransform (iCarousel carousel, nfloat offset, CATransform3D transform);

		// @optional -(CGFloat)carousel:(iCarousel *)carousel valueForOption:(iCarouselOption)option withDefault:(CGFloat)value;
		[Export ("carousel:valueForOption:withDefault:")]
		[DelegateName ("iCarouselValueCondition"), DefaultValue ("value")]
		nfloat GetValue (iCarousel carousel, iCarouselOption option, nfloat value);
	}
}
