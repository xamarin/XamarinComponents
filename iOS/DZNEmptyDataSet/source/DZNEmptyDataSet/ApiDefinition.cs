using System;

#if __UNIFIED__
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
#else
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using CGRect = System.Drawing.RectangleF;
using CGSize = System.Drawing.SizeF;
using CGPoint = System.Drawing.PointF;
using nfloat = System.Single;
#endif

namespace DZNEmptyDataSet
{
	// @interface EmptyDataSet (UIScrollView)
	[Category]
	[BaseType (typeof(UIScrollView))]
	interface UIScrollViewExtensions
	{
		// @property (nonatomic, weak) id<DZNEmptyDataSetSource> _Nullable emptyDataSetSource __attribute__((iboutlet));
		[NullAllowed, Export ("emptyDataSetSource", ArgumentSemantic.Weak)]
		IEmptyDataSetSource GetEmptyDataSetSource ();

		// @property (nonatomic, weak) id<DZNEmptyDataSetSource> _Nullable emptyDataSetSource __attribute__((iboutlet));
		[Export ("setEmptyDataSetSource:", ArgumentSemantic.Weak)]
		void SetEmptyDataSetSource ([NullAllowed] IEmptyDataSetSource emptyDataSetSource);

		// @property (nonatomic, weak) id<DZNEmptyDataSetDelegate> _Nullable emptyDataSetDelegate __attribute__((iboutlet));
		[NullAllowed, Export ("emptyDataSetDelegate", ArgumentSemantic.Weak)]
		IEmptyDataSetDelegate GetEmptyDataSetDelegate ();

		// @property (nonatomic, weak) id<DZNEmptyDataSetDelegate> _Nullable emptyDataSetDelegate __attribute__((iboutlet));
		[Export ("setEmptyDataSetDelegate:", ArgumentSemantic.Weak)]
		void SetEmptyDataSetDelegate ([NullAllowed] IEmptyDataSetDelegate emptyDataSetDelegate);

		// @property (readonly, getter = isEmptyDataSetVisible, nonatomic) BOOL emptyDataSetVisible;
		[Export ("isEmptyDataSetVisible")]
		bool IsEmptyDataSetVisible ();

		// -(void)reloadEmptyDataSet;
		[Export ("reloadEmptyDataSet")]
		void ReloadEmptyDataSet ();
	}

	interface IEmptyDataSetSource
	{

	}

	// @protocol DZNEmptyDataSetSource <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "DZNEmptyDataSetSource")]
	interface EmptyDataSetSource
	{
		// @optional -(NSAttributedString *)titleForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("titleForEmptyDataSet:")]
		NSAttributedString GetTitle (UIScrollView scrollView);

		// @optional -(NSAttributedString *)descriptionForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("descriptionForEmptyDataSet:")]
		NSAttributedString GetDescription (UIScrollView scrollView);

		// @optional -(UIImage *)imageForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("imageForEmptyDataSet:")]
		UIImage GetImage (UIScrollView scrollView);

		// @optional -(UIColor *)imageTintColorForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("imageTintColorForEmptyDataSet:")]
		UIColor GetImageTintColor (UIScrollView scrollView);

		// @optional -(CAAnimation *)imageAnimationForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("imageAnimationForEmptyDataSet:")]
		CAAnimation GetImageAnimation (UIScrollView scrollView);

		// @optional -(NSAttributedString *)buttonTitleForEmptyDataSet:(UIScrollView *)scrollView forState:(UIControlState)state;
		[Export ("buttonTitleForEmptyDataSet:forState:")]
		NSAttributedString GetButtonTitle (UIScrollView scrollView, UIControlState state);

		// @optional -(UIImage *)buttonImageForEmptyDataSet:(UIScrollView *)scrollView forState:(UIControlState)state;
		[Export ("buttonImageForEmptyDataSet:forState:")]
		UIImage GetButtonImage (UIScrollView scrollView, UIControlState state);

		// @optional -(UIImage *)buttonBackgroundImageForEmptyDataSet:(UIScrollView *)scrollView forState:(UIControlState)state;
		[Export ("buttonBackgroundImageForEmptyDataSet:forState:")]
		UIImage GetButtonBackgroundImage (UIScrollView scrollView, UIControlState state);

		// @optional -(UIColor *)backgroundColorForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("backgroundColorForEmptyDataSet:")]
		UIColor GetBackgroundColor (UIScrollView scrollView);

		// @optional -(UIView *)customViewForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("customViewForEmptyDataSet:")]
		UIView GetCustomView (UIScrollView scrollView);

		// @optional -(CGFloat)verticalOffsetForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("verticalOffsetForEmptyDataSet:")]
		nfloat GetVerticalOffset (UIScrollView scrollView);

		// @optional -(CGFloat)spaceHeightForEmptyDataSet:(UIScrollView *)scrollView;
		[Export ("spaceHeightForEmptyDataSet:")]
		nfloat GetSpaceHeight (UIScrollView scrollView);
	}

	interface IEmptyDataSetDelegate
	{

	}

	// @protocol DZNEmptyDataSetDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "DZNEmptyDataSetDelegate")]
	interface EmptyDataSetDelegate
	{
		// @optional - (BOOL)emptyDataSetShouldFadeIn:(UIScrollView *)scrollView;
		[Export ("emptyDataSetShouldFadeIn:")]
		bool EmptyDataSetShouldFadeIn (UIScrollView scrollView);

		// @optional -(BOOL)emptyDataSetShouldDisplay:(UIScrollView *)scrollView;
		[Export ("emptyDataSetShouldDisplay:")]
		bool EmptyDataSetShouldDisplay (UIScrollView scrollView);

		// @optional -(BOOL)emptyDataSetShouldAllowTouch:(UIScrollView *)scrollView;
		[Export ("emptyDataSetShouldAllowTouch:")]
		bool EmptyDataSetShouldAllowTouch (UIScrollView scrollView);

		// @optional -(BOOL)emptyDataSetShouldAllowScroll:(UIScrollView *)scrollView;
		[Export ("emptyDataSetShouldAllowScroll:")]
		bool EmptyDataSetShouldAllowScroll (UIScrollView scrollView);

		// @optional -(BOOL)emptyDataSetShouldAnimateImageView:(UIScrollView *)scrollView;
		[Export ("emptyDataSetShouldAnimateImageView:")]
		bool EmptyDataSetShouldAnimateImageView (UIScrollView scrollView);

		// @optional -(void)emptyDataSet:(UIScrollView *)scrollView didTapView:(UIView *)view;
		[Export ("emptyDataSet:didTapView:")]
		void EmptyDataSetDidTapView (UIScrollView scrollView, UIView view);

		// @optional -(void)emptyDataSet:(UIScrollView *)scrollView didTapButton:(UIButton *)button;
		[Export ("emptyDataSet:didTapButton:")]
		void EmptyDataSetDidTapButton (UIScrollView scrollView, UIButton button);

		// @optional -(void)emptyDataSetWillAppear:(UIScrollView *)scrollView;
		[Export ("emptyDataSetWillAppear:")]
		void EmptyDataSetWillAppear (UIScrollView scrollView);

		// @optional -(void)emptyDataSetDidAppear:(UIScrollView *)scrollView;
		[Export ("emptyDataSetDidAppear:")]
		void EmptyDataSetDidAppear (UIScrollView scrollView);

		// @optional -(void)emptyDataSetWillDisappear:(UIScrollView *)scrollView;
		[Export ("emptyDataSetWillDisappear:")]
		void EmptyDataSetWillDisappear (UIScrollView scrollView);

		// @optional -(void)emptyDataSetDidDisappear:(UIScrollView *)scrollView;
		[Export ("emptyDataSetDidDisappear:")]
		void EmptyDataSetDidDisappear (UIScrollView scrollView);
	}
}
