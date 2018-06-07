using System;
using UIKit;
using Foundation;
using CoreGraphics;
using REFrostedViewController;

namespace REFrostedViewController 
{
    
    /// <summary>
	/// REFrostedViewController view controller
    /// </summary>
    public class REFrostedViewController : UIViewController 
	{
        
        #region Fields
        private UIPanGestureRecognizer _PanGestureRecognizer;
        
        private Boolean _PanGestureEnabled;
        
        private REFrostedViewControllerDirection _Direction;
        
        private nfloat _BackgroundFadeAmount;
        
        private UIColor _BlurTintColor;
        
        private nfloat _BlurRadius;
        
        private nfloat _BlurSaturationDeltaFactor;
        
        private double _AnimationDuration;
        
        private Boolean _LimitMenuViewSize;
        
        private CGSize _MenuViewSize;
        
        private Boolean _LiveBlur;
        
        private REFrostedViewControllerLiveBackgroundStyle _LiveBlurBackgroundStyle;
        
        private IREFrostedViewControllerDelegate _Delegate;
        
        private UIViewController _ContentViewController;
        
        private UIViewController _MenuViewController;
        
        private nfloat _ImageViewWidth;
        
        private UIImage _Image;
        
        private UIImageView _ImageView;
        
        private Boolean _Visible;
        
        private REFrostedContainerViewController _ContainerViewController;
        
        private Boolean _AutomaticSize;
        
        private CGSize _CalculatedMenuViewSize;

        #endregion
        
        #region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REFrostedViewController"/> class.
		/// </summary>
		public REFrostedViewController() : base()
		{
			CommonInit();
        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REFrostedViewController"/> class.
		/// </summary>
		/// <param name="decoder">Decoder.</param>
		public REFrostedViewController(NSCoder decoder) : base(decoder) 
		{
			CommonInit();
        }
        
		/// <summary>
		/// Initializes a new instance of the <see cref="REFrostedViewController.REFrostedViewController"/> class.
		/// </summary>
		/// <param name="contentViewController">Content view controller.</param>
		/// <param name="menuViewController">Menu view controller.</param>
		public REFrostedViewController(UIViewController contentViewController, UIViewController menuViewController) 
			: this() 
		{
			
	         _ContentViewController = contentViewController;
	         _MenuViewController = menuViewController;

        }
        #endregion
        
        #region Properties

		/// <summary>
		/// Gets the pan gesture recognizer.
		/// </summary>
		/// <value>The pan gesture recognizer.</value>
        public UIPanGestureRecognizer PanGestureRecognizer {
            get {
                return this._PanGestureRecognizer;
            }
        }
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="REFrostedViewController.REFrostedViewController"/> pan
		/// gesture enabled.
		/// </summary>
		/// <value><c>true</c> if pan gesture enabled; otherwise, <c>false</c>.</value>
        public Boolean PanGestureEnabled {
            get {
                return this._PanGestureEnabled;
            }
            set {
                this._PanGestureEnabled = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the direction.
		/// </summary>
		/// <value>The direction.</value>
        public REFrostedViewControllerDirection Direction {
            get {
                return this._Direction;
            }
            set {
                this._Direction = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the background fade amount.
		/// </summary>
		/// <value>The background fade amount.</value>
        public nfloat BackgroundFadeAmount {
            get {
                return this._BackgroundFadeAmount;
            }
            set {
                this._BackgroundFadeAmount = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the color of the blur tint.
		/// </summary>
		/// <value>The color of the blur tint.</value>
        public UIColor BlurTintColor {
            get {
                return this._BlurTintColor;
            }
            set {
                this._BlurTintColor = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the blur radius.
		/// </summary>
		/// <value>The blur radius.</value>
        public nfloat BlurRadius {
            get {
                return this._BlurRadius;
            }
            set {
                this._BlurRadius = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the blur saturation delta factor.
		/// </summary>
		/// <value>The blur saturation delta factor.</value>
        public nfloat BlurSaturationDeltaFactor {
            get {
                return this._BlurSaturationDeltaFactor;
            }
            set {
                this._BlurSaturationDeltaFactor = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the duration of the animation.
		/// </summary>
		/// <value>The duration of the animation.</value>
        public double AnimationDuration {
            get {
                return this._AnimationDuration;
            }
            set {
                this._AnimationDuration = value;
            }
        }
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="REFrostedViewController.REFrostedViewController"/> limit
		/// menu view size.
		/// </summary>
		/// <value><c>true</c> if limit menu view size; otherwise, <c>false</c>.</value>
        public Boolean LimitMenuViewSize {
            get {
                return this._LimitMenuViewSize;
            }
            set {
                this._LimitMenuViewSize = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the size of the menu view.
		/// </summary>
		/// <value>The size of the menu view.</value>
        public CGSize MenuViewSize {
            get {
                return this._MenuViewSize;
            }
            set 
			{
                this._MenuViewSize = value;

				this.AutomaticSize = false;
            }
        }
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="REFrostedViewController.REFrostedViewController"/> live blur.
		/// </summary>
		/// <value><c>true</c> if live blur; otherwise, <c>false</c>.</value>
        public Boolean LiveBlur {
            get {
                return this._LiveBlur;
            }
            set {
                this._LiveBlur = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the live blur background style.
		/// </summary>
		/// <value>The live blur background style.</value>
        public REFrostedViewControllerLiveBackgroundStyle LiveBlurBackgroundStyle {
            get {
                return this._LiveBlurBackgroundStyle;
            }
            set {
                this._LiveBlurBackgroundStyle = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the delegate.
		/// </summary>
		/// <value>The delegate.</value>
        public IREFrostedViewControllerDelegate Delegate {
            get {
                return this._Delegate;
            }
            set {
                this._Delegate = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the content view controller.
		/// </summary>
		/// <value>The content view controller.</value>
        public UIViewController ContentViewController {
            get {
                return this._ContentViewController;
            }
            set 
			{
				if (this._ContentViewController == null) {
					this._ContentViewController = value;
			         return;
		         }

				this._ContentViewController.RemoveFromParentViewController();
				this._ContentViewController.View.RemoveFromSuperview();

				//     
				//     
				if (value != null) 
				{
					this.AddChildViewController(value);

					value.View.Frame = this.ContainerViewController.View.Frame;

					this.View.InsertSubview(value.View,0);

					value.DidMoveToParentViewController(this);

		         }

				this._ContentViewController = value;

				if (this.RespondsToSelector(new ObjCRuntime.Selector("setNeedsStatusBarAppearanceUpdate")))
					this.SetNeedsStatusBarAppearanceUpdate();
            }
        }
        
		/// <summary>
		/// Gets or sets the menu view controller.
		/// </summary>
		/// <value>The menu view controller.</value>
        public UIViewController MenuViewController {
            get {
                return this._MenuViewController;
            }
            set 
			{
				if (_MenuViewController != null) 
				{
					_MenuViewController.View.RemoveFromSuperview();
					_MenuViewController.RemoveFromParentViewController();
			     }


				this._MenuViewController = value;

				var frame = _MenuViewController.View.Frame;

				_MenuViewController.WillMoveToParentViewController(null);
				_MenuViewController.RemoveFromParentViewController();
				_MenuViewController.View.RemoveFromSuperview();
				this._MenuViewController = value;


				//
				if (_MenuViewController == null)
			         return;

				this.ContainerViewController.AddChildViewController(_MenuViewController);

				_MenuViewController.View.Frame = frame;


				//
				this.ContainerViewController.ContainerView.Add(_MenuViewController.View);

				_MenuViewController.DidMoveToParentViewController(this);
            }
        }
        
		/// <summary>
		/// Gets or sets the width of the image view.
		/// </summary>
		/// <value>The width of the image view.</value>
        private nfloat ImageViewWidth {
            get {
                return this._ImageViewWidth;
            }
            set {
                this._ImageViewWidth = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
        private UIImage Image {
            get {
                return this._Image;
            }
            set {
                this._Image = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the image view.
		/// </summary>
		/// <value>The image view.</value>
        private UIImageView ImageView {
            get {
                return this._ImageView;
            }
            set {
                this._ImageView = value;
            }
        }
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="REFrostedViewController.REFrostedViewController"/> is visible.
		/// </summary>
		/// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public Boolean Visible {
            get {
                return this._Visible;
            }
            set {
                this._Visible = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the container view controller.
		/// </summary>
		/// <value>The container view controller.</value>
        private REFrostedContainerViewController ContainerViewController {
            get {
                return this._ContainerViewController;
            }
            set {
                this._ContainerViewController = value;
            }
        }
        
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="REFrostedViewController.REFrostedViewController"/>
		/// automatic size.
		/// </summary>
		/// <value><c>true</c> if automatic size; otherwise, <c>false</c>.</value>
        private Boolean AutomaticSize {
            get {
                return this._AutomaticSize;
            }
            set {
                this._AutomaticSize = value;
            }
        }
        
		/// <summary>
		/// Gets or sets the calculated size of the menu view.
		/// </summary>
		/// <value>The calculated size of the menu view.</value>
        public CGSize CalculatedMenuViewSize {
            get {
                return this._CalculatedMenuViewSize;
            }
            set {
                this._CalculatedMenuViewSize = value;
            }
        }
        #endregion
        
        #region Methods

		/// <summary>
		/// Common initialisation
		/// </summary>
        private void CommonInit() 
		{
            
			  this.WantsFullScreenLayout = true;

			 _PanGestureEnabled = true;
			 _AnimationDuration = 0.35f;
			 _BackgroundFadeAmount = 0.3f;
			  _BlurTintColor = UIColor.FromWhiteAlpha(1.0f,0.75f); 
			 _BlurSaturationDeltaFactor = 1.8f;
			 _BlurRadius = 10.0f;
			 _ContainerViewController =  new REFrostedContainerViewController();
			 _ContainerViewController.FrostedViewController = this;
			 _MenuViewSize = CGSize.Empty;
			 _LiveBlur = true;

			 
			_PanGestureRecognizer = new UIPanGestureRecognizer(_ContainerViewController.PanGestureRecognized); 

			 _AutomaticSize = true;
		}
        
		/// <summary>
		/// Views the did load.
		/// </summary>
        public override void ViewDidLoad() 
		{
			base.ViewDidLoad();

			this.DisplayController(this.ContentViewController,this.View.Bounds);
        }
        
		/// <summary>
		/// Childs the view controller for status bar style.
		/// </summary>
		/// <returns>The view controller for status bar style.</returns>
        public override UIViewController ChildViewControllerForStatusBarStyle() 
		{
			return this.ContentViewController;
        }
        
		/// <summary>
		/// Childs the view controller for status bar hidden.
		/// </summary>
		/// <returns>The view controller for status bar hidden.</returns>
        public override UIViewController ChildViewControllerForStatusBarHidden() 
		{
			return this.ContentViewController;
        }
			        
        /// <summary>
        /// Presents the menu view controller.
        /// </summary>
        public void PresentMenuViewController() 
		{
			PresentMenuViewControllerWithAnimatedApperance(true);
        }
        
		/// <summary>
		/// Presents the menu view controller with animated apperance.
		/// </summary>
		/// <param name="animateApperance">If set to <c>true</c> animate apperance.</param>
        public void PresentMenuViewControllerWithAnimatedApperance(Boolean animateApperance) 
		{

			//
			if (this.Delegate != null)
			{
				if (this.Delegate is UIViewController)
				{
					this.Delegate.WillShowMenuViewController(this,this.MenuViewController);
				}
			}


            this.ContainerViewController.AnimateApperance = animateApperance;


            if (this.AutomaticSize) 
			{
                 if (this.Direction == REFrostedViewControllerDirection.Left || this.Direction == REFrostedViewControllerDirection.Right)
					this.CalculatedMenuViewSize = new CGSize(this.ContentViewController.View.Frame.Size.Width - 50.0f, this.ContentViewController.View.Frame.Size.Height);
                 
                 if (this.Direction == REFrostedViewControllerDirection.Top || this.Direction == REFrostedViewControllerDirection.Bottom)
					this.CalculatedMenuViewSize = new CGSize(this.ContentViewController.View.Frame.Size.Width, this.ContentViewController.View.Frame.Size.Height - 50.0f);
             } else {
				this.CalculatedMenuViewSize = new CGSize(_MenuViewSize.Width > 0 ? _MenuViewSize.Width : this.ContentViewController.View.Frame.Size.Width,
					      _MenuViewSize.Height > 0 ? _MenuViewSize.Height : this.ContentViewController.View.Frame.Size.Height);
		     }

            if (!this.LiveBlur) 
			{
                if (this.BlurTintColor == null) 
				{
					this.BlurTintColor = UIColor.FromWhiteAlpha(1.0f,0.75f);
				}

				this.ContainerViewController.ScreenshotImage = this.ContentViewController.View.Screenshot().ApplyBlurWithRadius(BlurRadius,BlurTintColor,BlurSaturationDeltaFactor,null);
		     }

			this.DisplayController(this.ContainerViewController, this.ContainerViewController.View.Frame);
            
			this.Visible = true;
        }
        
		/// <summary>
		/// Hides the menu view controller with completion handler.
		/// </summary>
		/// <param name="completionHandler">Completion handler.</param>
        public void HideMenuViewControllerWithCompletionHandler(Action completionHandler) 
		{
			if (!this.Visible)
				return;

			if (!this.LiveBlur)
			{
				this.ContainerViewController.ScreenshotImage = ContainerViewController.View.Screenshot().ApplyBlurWithRadius(BlurRadius, BlurTintColor, BlurSaturationDeltaFactor, null);
				this.ContainerViewController.RefreshBackgroundImage();
			}

			this.ContainerViewController.HideWithCompletionHandler(completionHandler);
        }
        
		/// <summary>
		/// Resizes the size of the menu view controller to.
		/// </summary>
		/// <param name="size">Size.</param>
        private void ResizeMenuViewControllerToSize(CGSize size) 
		{
            
	        if (!this.LiveBlur) 
	        {
				this.ContainerViewController.ScreenshotImage = ContainerViewController.View.Screenshot().ApplyBlurWithRadius(BlurRadius, BlurTintColor, BlurSaturationDeltaFactor, null);
	        
				this.ContainerViewController.RefreshBackgroundImage();

			}

			this.ContainerViewController.ResizeToSize(size);
        }
        
		/// <summary>
		/// Hides the menu view controller.
		/// </summary>
        public void HideMenuViewController() 
		{
			HideMenuViewControllerWithCompletionHandler(null);
        }
        
		/// <summary>
		/// Pans the gesture recognized.
		/// </summary>
		/// <param name="recognizer">Recognizer.</param>
        public void PanGestureRecognized(UIPanGestureRecognizer recognizer) 
		{
			if (this.Delegate != null)
			{
				this.Delegate.DidRecognizePanGesture(this,recognizer);
			}

   
	         if (!this.PanGestureEnabled)
	             return;
                 
            if (recognizer.State == UIGestureRecognizerState.Began)
			{
				this.PresentMenuViewControllerWithAnimatedApperance(true);
			}

			this.ContainerViewController.PanGestureRecognized(recognizer);
        }
        
		/// <summary>
		/// Shoulds the autorotate.
		/// </summary>
		/// <returns><c>true</c>, if autorotate was shoulded, <c>false</c> otherwise.</returns>
        public override Boolean ShouldAutorotate() 
		{
			return this.ContentViewController.ShouldAutorotate();
        }
        
		/// <summary>
		/// Wills the animate rotation.
		/// </summary>
		/// <param name="toInterfaceOrientation">To interface orientation.</param>
		/// <param name="duration">Duration.</param>
		public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation(toInterfaceOrientation, duration);

			if (this.Delegate != null)
			{
				this.Delegate.WillAnimateRotationToInterfaceOrientation(this,toInterfaceOrientation,duration);
			}
				
		     if (this.Visible) 
		     {
		         if (this.AutomaticSize) 
		          {
					if (this.Direction == REFrostedViewControllerDirection.Left || this.Direction == REFrostedViewControllerDirection.Right)
		                 this.CalculatedMenuViewSize = new CGSize(this.View.Bounds.Size.Width - 50.0f, this.View.Bounds.Size.Height);
		             
					if (this.Direction == REFrostedViewControllerDirection.Top || this.Direction == REFrostedViewControllerDirection.Bottom)
		                 this.CalculatedMenuViewSize = new CGSize(this.View.Bounds.Size.Width, this.View.Bounds.Size.Height - 50.0f);
		         } 
		          else 
		          {
		             this.CalculatedMenuViewSize = new CGSize(_MenuViewSize.Width > 0 ? _MenuViewSize.Width : this.View.Bounds.Size.Width,
		                                                      _MenuViewSize.Height > 0 ? _MenuViewSize.Height : this.View.Bounds.Size.Height);
		         }       
	          }
		
		}
			
		/// <summary>
		/// Dids the rotate.
		/// </summary>
		/// <param name="fromInterfaceOrientation">From interface orientation.</param>
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			if (!this.Visible)
				this.CalculatedMenuViewSize = CGSize.Empty;
				
		}

        #endregion
    }
}
