using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using Photos;
using UIKit;

namespace MWPhotoBrowser
{
	interface IPhoto { }

	// @protocol MWPhoto <NSObject>
	[Protocol (Name = "MWPhoto")]
	interface Photo
	{
		// @required @property (nonatomic, strong) UIImage * underlyingImage;
		[Abstract]
		[Export ("underlyingImage", ArgumentSemantic.Strong)]
		UIImage UnderlyingImage { get; set; }

		// @required -(void)loadUnderlyingImageAndNotify;
		[Abstract]
		[Export ("loadUnderlyingImageAndNotify")]
		void LoadUnderlyingImageAndNotify ();

		// @required -(void)performLoadUnderlyingImageAndNotify;
		[Abstract]
		[Export ("performLoadUnderlyingImageAndNotify")]
		void PerformLoadUnderlyingImageAndNotify ();

		// @required -(void)unloadUnderlyingImage;
		[Abstract]
		[Export ("unloadUnderlyingImage")]
		void UnloadUnderlyingImage ();

		// @optional @property (nonatomic) BOOL emptyImage;
		[Export ("emptyImage")]
		bool IsEmptyImage { get; set; }

		// @optional @property (nonatomic) BOOL isVideo;
		[Export ("isVideo")]
		bool IsVideo { get; set; }

		// @optional -(void)getVideoURL:(void (^)(NSURL *))completion;
		[Export ("getVideoURL:")]
		void GetVideoUrl (Action<NSUrl> completion);

		// @optional -(NSString *)caption;
		[Export ("caption")]
		string Caption { get; }

		// @optional -(void)cancelAnyLoading;
		[Export ("cancelAnyLoading")]
		void CancelAnyLoading ();
	}

	// @interface MWCaptionView : UIToolbar
	[BaseType (typeof(UIToolbar), Name = "MWCaptionView")]
	interface CaptionView
	{
		// -(id)initWithPhoto:(id<MWPhoto>)photo;
		[Export ("initWithPhoto:")]
		IntPtr Constructor (IPhoto photo);

		// -(void)setupCaption;
		[Export ("setupCaption")]
		void SetupCaption ();

		// -(CGSize)sizeThatFits:(CGSize)size;
		[Override]
		[Export ("sizeThatFits:")]
		CGSize SizeThatFits (CGSize size);
	}

	// @interface MWPhoto : NSObject <MWPhoto>
	[BaseType (typeof(NSObject), Name = "MWPhoto")]
	interface PhotoBrowserPhoto : Photo
	{
		// @property (nonatomic, strong) NSString * caption;
		[Export ("caption", ArgumentSemantic.Strong)]
		string Caption { get; set; }

		// @property (nonatomic, strong) NSURL * videoURL;
		[Export ("videoURL", ArgumentSemantic.Strong)]
		NSUrl VideoUrl { get; set; }

		// @property (nonatomic) BOOL emptyImage;
		[Export ("emptyImage")]
		bool IsEmptyImage { get; set; }

		// @property (nonatomic) BOOL isVideo;
		[Export ("isVideo")]
		bool IsVideo { get; set; }

		// +(MWPhoto *)photoWithImage:(UIImage *)image;
		[Static]
		[Export ("photoWithImage:")]
		PhotoBrowserPhoto FromImage (UIImage image);

		// +(MWPhoto *)photoWithURL:(NSURL *)url;
		[Static]
		[Export ("photoWithURL:")]
		PhotoBrowserPhoto FromUrl (NSUrl url);

		// +(MWPhoto *)photoWithAsset:(PHAsset *)asset targetSize:(CGSize)targetSize;
		[Static]
		[Export ("photoWithAsset:targetSize:")]
		PhotoBrowserPhoto FromAsset (PHAsset asset, CGSize targetSize);

		// +(MWPhoto *)videoWithURL:(NSURL *)url;
		[Static]
		[Export ("videoWithURL:")]
		PhotoBrowserPhoto FromVideoUrl (NSUrl url);


//		// -(id)initWithImage:(UIImage *)image;
//		[Export ("initWithImage:")]
//		IntPtr Constructor (UIImage image);
//
//		// -(id)initWithURL:(NSURL *)url;
//		[Export ("initWithURL:")]
//		IntPtr Constructor (NSUrl url);
//
//		// -(id)initWithAsset:(PHAsset *)asset targetSize:(CGSize)targetSize;
//		[Export ("initWithAsset:targetSize:")]
//		IntPtr Constructor (PHAsset asset, CGSize targetSize);
//
//		// -(id)initWithVideoURL:(NSURL *)url;
//		[Export ("initWithVideoURL:")]
//		IntPtr Constructor (NSUrl url);
	}

	interface IPhotoBrowserDelegate { }

	// @protocol MWPhotoBrowserDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MWPhotoBrowserDelegate")]
	interface PhotoBrowserDelegate
	{
		// @required -(NSUInteger)numberOfPhotosInPhotoBrowser:(MWPhotoBrowser *)photoBrowser;
		[Abstract]
		[Export ("numberOfPhotosInPhotoBrowser:")]
		nuint GetPhotoCount (PhotoBrowser photoBrowser);

		// @required -(id<MWPhoto>)photoBrowser:(MWPhotoBrowser *)photoBrowser photoAtIndex:(NSUInteger)index;
		[Abstract]
		[Export ("photoBrowser:photoAtIndex:")]
		IPhoto GetPhoto (PhotoBrowser photoBrowser, nuint index);

		// @optional -(id<MWPhoto>)photoBrowser:(MWPhotoBrowser *)photoBrowser thumbPhotoAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:thumbPhotoAtIndex:")]
		IPhoto GetThumbnail (PhotoBrowser photoBrowser, nuint index);

		// @optional -(MWCaptionView *)photoBrowser:(MWPhotoBrowser *)photoBrowser captionViewForPhotoAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:captionViewForPhotoAtIndex:")]
		CaptionView GetCaptionView (PhotoBrowser photoBrowser, nuint index);

		// @optional -(NSString *)photoBrowser:(MWPhotoBrowser *)photoBrowser titleForPhotoAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:titleForPhotoAtIndex:")]
		string GetTitle (PhotoBrowser photoBrowser, nuint index);

		// @optional -(void)photoBrowser:(MWPhotoBrowser *)photoBrowser didDisplayPhotoAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:didDisplayPhotoAtIndex:")]
		void DidDisplayPhoto (PhotoBrowser photoBrowser, nuint index);

		// @optional -(void)photoBrowser:(MWPhotoBrowser *)photoBrowser actionButtonPressedForPhotoAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:actionButtonPressedForPhotoAtIndex:")]
		void OnActionButtonPressed (PhotoBrowser photoBrowser, nuint index);

		// @optional -(BOOL)photoBrowser:(MWPhotoBrowser *)photoBrowser isPhotoSelectedAtIndex:(NSUInteger)index;
		[Export ("photoBrowser:isPhotoSelectedAtIndex:")]
		bool IsPhotoSelected (PhotoBrowser photoBrowser, nuint index);

		// @optional -(void)photoBrowser:(MWPhotoBrowser *)photoBrowser photoAtIndex:(NSUInteger)index selectedChanged:(BOOL)selected;
		[Export ("photoBrowser:photoAtIndex:selectedChanged:")]
		void OnSelectedChanged (PhotoBrowser photoBrowser, nuint index, bool selected);

		// @optional -(void)photoBrowserDidFinishModalPresentation:(MWPhotoBrowser *)photoBrowser;
		[Export ("photoBrowserDidFinishModalPresentation:")]
		void DidFinishModalPresentation (PhotoBrowser photoBrowser);
	}

	// @interface MWPhotoBrowser : UIViewController <UIScrollViewDelegate, UIActionSheetDelegate>
	[BaseType (typeof(UIViewController), Name = "MWPhotoBrowser")]
	interface PhotoBrowser : IUIScrollViewDelegate, IUIActionSheetDelegate
	{
		// @property (nonatomic, weak) id<MWPhotoBrowserDelegate> _Nullable delegate __attribute__((iboutlet));
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPhotoBrowserDelegate Delegate { get; set; }

		// @property (nonatomic) BOOL zoomPhotosToFill;
		[Export ("zoomPhotosToFill")]
		bool ZoomPhotosToFill { get; set; }

		// @property (nonatomic) BOOL displayNavArrows;
		[Export ("displayNavArrows")]
		bool DisplayNavArrows { get; set; }

		// @property (nonatomic) BOOL displayActionButton;
		[Export ("displayActionButton")]
		bool DisplayActionButton { get; set; }

		// @property (nonatomic) BOOL displaySelectionButtons;
		[Export ("displaySelectionButtons")]
		bool DisplaySelectionButtons { get; set; }

		// @property (nonatomic) BOOL alwaysShowControls;
		[Export ("alwaysShowControls")]
		bool AlwaysShowControls { get; set; }

		// @property (nonatomic) BOOL enableGrid;
		[Export ("enableGrid")]
		bool EnableGrid { get; set; }

		// @property (nonatomic) BOOL enableSwipeToDismiss;
		[Export ("enableSwipeToDismiss")]
		bool EnableSwipeToDismiss { get; set; }

		// @property (nonatomic) BOOL startOnGrid;
		[Export ("startOnGrid")]
		bool StartOnGrid { get; set; }

		// @property (nonatomic) BOOL autoPlayOnAppear;
		[Export ("autoPlayOnAppear")]
		bool AutoPlayOnAppear { get; set; }

		// @property (nonatomic) NSUInteger delayToHideElements;
		[Export ("delayToHideElements")]
		nuint DelayToHideElements { get; set; }

		// @property (readonly, nonatomic) NSUInteger currentIndex;
		nuint CurrentIndex { [Export ("currentIndex")] get; [Export ("setCurrentPhotoIndex:")] set; }

		// @property (nonatomic, strong) NSString * customImageSelectedIconName;
		[Export ("customImageSelectedIconName", ArgumentSemantic.Strong)]
		string CustomImageSelectedIconName { get; set; }

		// @property (nonatomic, strong) NSString * customImageSelectedSmallIconName;
		[Export ("customImageSelectedSmallIconName", ArgumentSemantic.Strong)]
		string CustomImageSelectedSmallIconName { get; set; }

		// -(id)initWithPhotos:(NSArray *)photosArray;
		[Export ("initWithPhotos:")]
		IntPtr Constructor (Photo[] photosArray);

		// -(id)initWithDelegate:(id<MWPhotoBrowserDelegate>)delegate;
		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPhotoBrowserDelegate @delegate);

		// -(void)reloadData;
		[Export ("reloadData")]
		void ReloadData ();

		// -(void)showNextPhotoAnimated:(BOOL)animated;
		[Export ("showNextPhotoAnimated:")]
		void ShowNextPhoto (bool animated);

		// -(void)showPreviousPhotoAnimated:(BOOL)animated;
		[Export ("showPreviousPhotoAnimated:")]
		void ShowPreviousPhoto (bool animated);
	}

	// @interface MWGridViewController : UICollectionViewController
	[BaseType (typeof(UICollectionViewController), Name = "MWGridViewController")]
	interface GridViewController
	{
		// @property (assign, nonatomic) MWPhotoBrowser * browser;
		[Export ("browser", ArgumentSemantic.Assign)]
		PhotoBrowser Browser { get; set; }

		// @property (nonatomic) BOOL selectionMode;
		[Export ("selectionMode")]
		bool SelectionMode { get; set; }

		// @property (nonatomic) CGPoint initialContentOffset;
		[Export ("initialContentOffset", ArgumentSemantic.Assign)]
		CGPoint InitialContentOffset { get; set; }

		// -(void)adjustOffsetsAsRequired;
		[Export ("adjustOffsetsAsRequired")]
		void AdjustOffsetsAsRequired ();
	}

	// @interface MWGridCell : UICollectionViewCell
	[BaseType (typeof(UICollectionViewCell), Name = "MWGridCell")]
	interface GridCell
	{
		// @property (nonatomic, weak) MWGridViewController * _Nullable gridController;
		[NullAllowed, Export ("gridController", ArgumentSemantic.Weak)]
		GridViewController GridController { get; set; }

		// @property (nonatomic) NSUInteger index;
		[Export ("index")]
		nuint Index { get; set; }

		// @property (nonatomic) id<MWPhoto> photo;
		[Export ("photo", ArgumentSemantic.Assign)]
		IPhoto Photo { get; set; }

		// @property (nonatomic) BOOL selectionMode;
		[Export ("selectionMode")]
		bool SelectionMode { get; set; }

		// @property (nonatomic) BOOL isSelected;
		[Export ("isSelected")]
		bool IsSelected { get; set; }

		// -(void)displayImage;
		[Export ("displayImage")]
		void DisplayImage ();
	}

	// @interface MWTapDetectingImageView : UIImageView
	[BaseType (typeof(UIImageView), Name = "MWTapDetectingImageView")]
	interface TapDetectingImageView
	{
		// @property (nonatomic, weak) id<MWTapDetectingImageViewDelegate> _Nullable tapDelegate;
		[NullAllowed, Export ("tapDelegate", ArgumentSemantic.Weak)]
		ITapDetectingImageViewDelegate TapDelegate { get; set; }
	}

	interface ITapDetectingImageViewDelegate { }

	// @protocol MWTapDetectingImageViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MWTapDetectingImageViewDelegate")]
	interface TapDetectingImageViewDelegate
	{
		// @optional -(void)imageView:(UIImageView *)imageView singleTapDetected:(UITouch *)touch;
		[Export ("imageView:singleTapDetected:")]
		void SingleTapDetected (UIImageView imageView, UITouch touch);

		// @optional -(void)imageView:(UIImageView *)imageView doubleTapDetected:(UITouch *)touch;
		[Export ("imageView:doubleTapDetected:")]
		void DoubleTapDetected (UIImageView imageView, UITouch touch);

		// @optional -(void)imageView:(UIImageView *)imageView tripleTapDetected:(UITouch *)touch;
		[Export ("imageView:tripleTapDetected:")]
		void TripleTapDetected (UIImageView imageView, UITouch touch);
	}

	// @interface MWTapDetectingView : UIView
	[BaseType (typeof(UIView), Name = "MWTapDetectingView")]
	interface TapDetectingView
	{
		// @property (nonatomic, weak) id<MWTapDetectingViewDelegate> _Nullable tapDelegate;
		[NullAllowed, Export ("tapDelegate", ArgumentSemantic.Weak)]
		ITapDetectingViewDelegate TapDelegate { get; set; }
	}

	interface ITapDetectingViewDelegate { }

	// @protocol MWTapDetectingViewDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MWTapDetectingViewDelegate")]
	interface TapDetectingViewDelegate
	{
		// @optional -(void)view:(UIView *)view singleTapDetected:(UITouch *)touch;
		[Export ("view:singleTapDetected:")]
		void SingleTapDetected (UIView view, UITouch touch);

		// @optional -(void)view:(UIView *)view doubleTapDetected:(UITouch *)touch;
		[Export ("view:doubleTapDetected:")]
		void DoubleTapDetected (UIView view, UITouch touch);

		// @optional -(void)view:(UIView *)view tripleTapDetected:(UITouch *)touch;
		[Export ("view:tripleTapDetected:")]
		void TripleTapDetected (UIView view, UITouch touch);
	}

	// @interface MWZoomingScrollView : UIScrollView <UIScrollViewDelegate, MWTapDetectingImageViewDelegate, MWTapDetectingViewDelegate>
	[BaseType (typeof(UIScrollView), Name = "MWZoomingScrollView")]
	interface ZoomingScrollView : IUIScrollViewDelegate, ITapDetectingImageViewDelegate, ITapDetectingViewDelegate
	{
		// @property NSUInteger index;
		[Export ("index")]
		nuint Index { get; set; }

		// @property (nonatomic) id<MWPhoto> photo;
		[Export ("photo", ArgumentSemantic.Assign)]
		IPhoto Photo { get; set; }

		// @property (nonatomic, weak) MWCaptionView * _Nullable captionView;
		[NullAllowed, Export ("captionView", ArgumentSemantic.Weak)]
		CaptionView CaptionView { get; set; }

		// @property (nonatomic, weak) UIButton * _Nullable selectedButton;
		[NullAllowed, Export ("selectedButton", ArgumentSemantic.Weak)]
		UIButton SelectedButton { get; set; }

		// @property (nonatomic, weak) UIButton * _Nullable playButton;
		[NullAllowed, Export ("playButton", ArgumentSemantic.Weak)]
		UIButton PlayButton { get; set; }

		// -(id)initWithPhotoBrowser:(MWPhotoBrowser *)browser;
		[Export ("initWithPhotoBrowser:")]
		IntPtr Constructor (PhotoBrowser browser);

		// -(void)displayImage;
		[Export ("displayImage")]
		void DisplayImage ();

		// -(void)displayImageFailure;
		[Export ("displayImageFailure")]
		void DisplayImageFailure ();

		// -(void)setMaxMinZoomScalesForCurrentBounds;
		[Export ("setMaxMinZoomScalesForCurrentBounds")]
		void SetMaxMinZoomScalesForCurrentBounds ();

		// -(void)prepareForReuse;
		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		// -(BOOL)displayingVideo;
		[Export ("displayingVideo")]
		bool IsDisplayingVideo { get; }

		// -(void)setImageHidden:(BOOL)hidden;
		[Export ("setImageHidden:")]
		void SetImageHidden (bool hidden);
	}

//	// @interface MWPhotoBrowser (UIImage)
//	[Static]
//	[BaseType (typeof(NSObject), Name = "UIImage")]
//	interface PhotoBrowserImage
//	{
//		// +(UIImage *)imageForResourcePath:(NSString *)path ofType:(NSString *)type inBundle:(NSBundle *)bundle;
//		[Static]
//		[Export ("imageForResourcePath:ofType:inBundle:")]
//		UIImage CreateImageForResourcePath (string path, string type, NSBundle bundle);
//
//		// +(UIImage *)clearImageWithSize:(CGSize)size;
//		[Static]
//		[Export ("clearImageWithSize:")]
//		UIImage CreateClearImage (CGSize size);
//	}
}
