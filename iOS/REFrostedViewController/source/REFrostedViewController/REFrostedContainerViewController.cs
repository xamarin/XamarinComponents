using System;
using UIKit;
using Foundation;
using CoreGraphics;
using System.Collections.Generic;

namespace REFrostedViewController {
    
    
    public class REFrostedContainerViewController : UIViewController {
        
        #region Fields
        private UIImage _ScreenshotImage;
        
        private REFrostedViewController _FrostedViewController;
        
        private Boolean _AnimateApperance;
        
        private UIView _ContainerView;
        
        private UIImageView _BackgroundImageView;
        
		private List<UIView> _BackgroundViews;
        
        private CGPoint _ContainerOrigin;
        #endregion
        
        #region Properties
        public UIImage ScreenshotImage {
            get {
                return this._ScreenshotImage;
            }
            set {
                this._ScreenshotImage = value;
            }
        }
        
        public REFrostedViewController FrostedViewController {
            get {
                return this._FrostedViewController;
            }
            set {
                this._FrostedViewController = value;
            }
        }
        
        public Boolean AnimateApperance {
            get {
                return this._AnimateApperance;
            }
            set {
                this._AnimateApperance = value;
            }
        }
        
        public UIView ContainerView {
            get {
                return this._ContainerView;
            }
        }
        
        private UIImageView BackgroundImageView {
            get {
                return this._BackgroundImageView;
            }
            set {
                this._BackgroundImageView = value;
            }
        }
        
        private List<UIView> BackgroundViews {
            get {
                return this._BackgroundViews;
            }
            set {
                this._BackgroundViews = value;
            }
        }
        
        private CGPoint ContainerOrigin {
            get {
                return this._ContainerOrigin;
            }
            set {
                this._ContainerOrigin = value;
            }
        }
        #endregion
        
        #region Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
        public override void ViewDidLoad() 
		{
			base.ViewDidLoad();

			this.BackgroundViews = new List<UIView>();

	         for (int i = 0; i < 4; i++) {
				var backgroundView =  new UIView(CGRect.Empty); 
				backgroundView.BackgroundColor = UIColor.Black;
	             backgroundView.Alpha = 0.0f;
				this.Add(backgroundView);

				this.BackgroundViews.Add(backgroundView);
			
				var tapRecognizer = new UITapGestureRecognizer(TapGestureRecognized);
				backgroundView.AddGestureRecognizer(tapRecognizer);

		     }


			_ContainerView = new UIView(new CGRect(0, 0, this.View.Frame.Size.Width, this.View.Frame.Size.Height));
            this.ContainerView.ClipsToBounds = true;
			this.Add(this.ContainerView);

             
            if (this.FrostedViewController.LiveBlur) 
			{
				var toolbar = new UIToolbar(this.View.Bounds);
                toolbar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
                toolbar.BarStyle = (UIBarStyle)this.FrostedViewController.LiveBlurBackgroundStyle;

				this.ContainerView.Add(toolbar);

             } 
			else 
			{
				this.BackgroundImageView = new UIImageView(this.View.Bounds);
				this.ContainerView.Add(this.BackgroundImageView);
		     }

             if (this.FrostedViewController.MenuViewController != null) 
			 {
				this.AddChildViewController(this.FrostedViewController.MenuViewController);

				this.FrostedViewController.MenuViewController.View.Frame = this.ContainerView.Bounds;

				this.ContainerView.Add(this.FrostedViewController.MenuViewController.View);

				this.FrostedViewController.MenuViewController.DidMoveToParentViewController(this);
		     }

			this.View.AddGestureRecognizer(this.FrostedViewController.PanGestureRecognizer);

        }
        
		/// <summary>
		/// Views the will appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
        public override void ViewWillAppear(Boolean animated) 
		{
			base.ViewWillAppear(animated);

                 
            if(!this.FrostedViewController.Visible) 
			{
				if (this.BackgroundImageView != null)
				{
					this.BackgroundImageView.Image = this.ScreenshotImage;
					this.BackgroundImageView.Frame = this.View.Bounds;
				}

                this.FrostedViewController.MenuViewController.View.Frame = this.ContainerView.Bounds;
                     
                if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
				{
					this.SetContainerFrame(new CGRect(-this.FrostedViewController.CalculatedMenuViewSize.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				}

				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
				{
					this.SetContainerFrame(new CGRect(this.View.Frame.Size.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				}
                
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
				{
					this.SetContainerFrame(new CGRect(0, -this.FrostedViewController.CalculatedMenuViewSize.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				}
                
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
				{
					this.SetContainerFrame(new CGRect(0, this.View.Frame.Size.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				}

                if (this.AnimateApperance)
					this.Show();
                     
			}
        }
        
		/// <summary>
		/// Sets the container frame.
		/// </summary>
		/// <param name="frame">Frame.</param>
        private void SetContainerFrame(CGRect frame) {

             var leftBackgroundView = this.BackgroundViews[0];
             var topBackgroundView = this.BackgroundViews[1];
             var bottomBackgroundView = this.BackgroundViews[2];
             var rightBackgroundView = this.BackgroundViews[3];
             
             leftBackgroundView.Frame = new CGRect(0, 0, frame.Location.X, this.View.Frame.Size.Height);
			rightBackgroundView.Frame = new CGRect(frame.Size.Width + frame.Location.X, 0, this.View.Frame.Size.Width - frame.Size.Width - frame.Location.X, this.View.Frame.Size.Height);
             
			topBackgroundView.Frame = new CGRect(frame.Location.X, 0, frame.Size.Width, frame.Location.Y);
			bottomBackgroundView.Frame = new CGRect(frame.Location.X, frame.Size.Height + frame.Location.Y, frame.Size.Width, this.View.Frame.Size.Height);
             
             this.ContainerView.Frame = frame;

			if (this.BackgroundImageView != null)
				this.BackgroundImageView.Frame = new CGRect(- frame.Location.X, - frame.Location.Y, this.View.Bounds.Size.Width, this.View.Bounds.Size.Height);
        }
        
		/// <summary>
		/// Sets the background views alpha.
		/// </summary>
		/// <param name="alpha">Alpha.</param>
        private void SetBackgroundViewsAlpha(nfloat alpha) 
		{
            foreach (UIView view in this.BackgroundViews) 
			{
               view.Alpha = alpha;
			}
        }
        
		/// <summary>
		/// Resizes the size of the to.
		/// </summary>
		/// <param name="size">Size.</param>
        public void ResizeToSize(CGSize size) 
		{
         
             if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
			{
				UIView.Animate(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, 0, size.Width, size.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},null);
			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
			{
				UIView.Animate(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(this.View.Frame.Size.Width - size.Width, 0, size.Width, size.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},null);
			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
			{
				UIView.Animate(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, 0, size.Width, size.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},null);
					
			}
             
			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
			{
				UIView.Animate(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, this.View.Frame.Size.Height - size.Height, size.Width, size.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},null);

			}
        }
        
		/// <summary>
		/// Show this instance.
		/// </summary>
        private void Show() 
		{
			Action<bool> completionBlock = (finished)=>
			{

				if (this.FrostedViewController.Delegate != null)
					this.FrostedViewController.Delegate.DidShowMenuViewController(this.FrostedViewController,this.FrostedViewController.MenuViewController);

			};

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},(finished)=>
				{
					completionBlock(finished);
				});
			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(this.View.Frame.Size.Width - this.FrostedViewController.CalculatedMenuViewSize.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},(finished)=> {completionBlock(finished);});
					
			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},(finished)=>
				{
					completionBlock(finished);
				});
			}

			if (FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, this.View.Frame.Size.Height - this.FrostedViewController.CalculatedMenuViewSize.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

				},(finished)=>
				{
					completionBlock(finished);
				});
			}



        }
        
		/// <summary>
		/// Hide this instance.
		/// </summary>
        private void Hide() 
		{
			this.HideWithCompletionHandler(null);
        }
        
		/// <summary>
		/// Hides the with completion handler.
		/// </summary>
		/// <param name="completionHandler">Completion handler.</param>
        public void HideWithCompletionHandler(Action completionHandler) {
            

			Action completionBlock = ()=>
			{
			
				if (this.FrostedViewController.Delegate != null)
					this.FrostedViewController.Delegate.DidHideMenuViewController(this.FrostedViewController,this.FrostedViewController.MenuViewController);
				

				if (completionHandler != null)
					completionHandler();
			};
				
			if (this.FrostedViewController.Delegate != null)
				this.FrostedViewController.Delegate.WillHideMenuViewController(this.FrostedViewController,this.FrostedViewController.MenuViewController);

            if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(-this.FrostedViewController.CalculatedMenuViewSize.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(0);

				},(finished)=>
				{
					this.FrostedViewController.Visible = false;
					this.HideController();

					completionBlock();
				});

			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(this.View.Frame.Size.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(0);

				},(finished)=>
				{
					this.FrostedViewController.Visible = false;
					this.HideController();

					completionBlock();
				});
					
			}
             
			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
			{
				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, -this.FrostedViewController.CalculatedMenuViewSize.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(0);

				},(finished)=>
				{
					this.FrostedViewController.Visible = false;
					this.HideController();

					completionBlock();
				});
					
			}
             
			if (FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
			{

				UIView.AnimateNotify(this.FrostedViewController.AnimationDuration,()=>
				{
					this.SetContainerFrame(new CGRect(0, this.View.Frame.Size.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
					this.SetBackgroundViewsAlpha(0);

				},(finished)=>
				{
					this.FrostedViewController.Visible = false;
					this.HideController();

					completionBlock();
				});

			}
        }
        
		/// <summary>
		/// Refreshs the background image.
		/// </summary>
        public void RefreshBackgroundImage() {
             
            this.BackgroundImageView.Image = this.ScreenshotImage;
        }
        
		/// <summary>
		/// Taps the gesture recognized.
		/// </summary>
		/// <param name="recognizer">Recognizer.</param>
        private void TapGestureRecognized(UITapGestureRecognizer recognizer) {

			this.Hide();
        }
        
		/// <summary>
		/// Pans the gesture recognized.
		/// </summary>
		/// <param name="recognizer">Recognizer.</param>
        public void PanGestureRecognized(UIPanGestureRecognizer recognizer) 
		{

			if (this.FrostedViewController.Delegate != null)
				this.FrostedViewController.Delegate.DidRecognizePanGesture(this.FrostedViewController, recognizer);
			
		                 
			if (!this.FrostedViewController.PanGestureEnabled)
				return;
            

			var point = recognizer.TranslationInView(this.View);

                
            if (recognizer.State == UIGestureRecognizerState.Began) 
				this.ContainerOrigin = this.ContainerView.Frame.Location;


		    if (recognizer.State == UIGestureRecognizerState.Changed) 
			{
		         var frame = this.ContainerView.Frame;

		         if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
				 {
					frame.X = this.ContainerOrigin.X + point.X;
		          
					if (frame.Location.X > 0) 
					{
		                 frame.X = 0;
		                 
						if (!this.FrostedViewController.LimitMenuViewSize) 
						 {
							frame.Width = this.FrostedViewController.CalculatedMenuViewSize.Width + this.ContainerOrigin.X + point.X;
		                     
							if (frame.Size.Width > this.View.Frame.Size.Width)
		                         frame.Width = this.View.Frame.Size.Width;
		                 }
		             }
		         }
		         
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
				 {
		             frame.X = this.ContainerOrigin.X + point.X;
					if (frame.Location.X < this.View.Frame.Size.Width - this.FrostedViewController.CalculatedMenuViewSize.Width) {
						frame.X = this.View.Frame.Size.Width - this.FrostedViewController.CalculatedMenuViewSize.Width;
		             
						if (!this.FrostedViewController.LimitMenuViewSize) 
						 {
		                     frame.X = this.ContainerOrigin.X + point.X;
		                     if (frame.Location.X < 0)
		                         frame.X = 0;
		                     frame.Width = this.View.Frame.Size.Width - frame.Location.X;
		                 }
		             }
		         }
		         
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
				 {
		            frame.Y = this.ContainerOrigin.Y + point.Y;
		            
					if (frame.Location.Y > 0) 
					{
		                frame.Y = 0;
		             
						if (!this.FrostedViewController.LimitMenuViewSize) 
						{
							frame.Height = this.FrostedViewController.CalculatedMenuViewSize.Height + this.ContainerOrigin.Y + point.Y;
		                     if (frame.Size.Height > this.View.Frame.Size.Height)
		                         frame.Height = this.View.Frame.Size.Height;
		                 }
		             }
		         }
		         
		         if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
				 {
		            frame.Y = this.ContainerOrigin.Y + point.Y;
		            
					if (frame.Location.Y < this.View.Frame.Size.Height - this.FrostedViewController.CalculatedMenuViewSize.Height) 
					{
						frame.Y = this.View.Frame.Size.Height - this.FrostedViewController.CalculatedMenuViewSize.Height;
		             
						if (!this.FrostedViewController.LimitMenuViewSize) 
						{
		                     frame.Y = this.ContainerOrigin.Y + point.Y;
		                     if (frame.Location.Y < 0)
		                         frame.Y = 0;
		                     frame.Height = this.View.Frame.Size.Height - frame.Location.Y;
		                 }
		             }
		         }
		         
				SetContainerFrame(frame);

		     }
		     
		    if (recognizer.State == UIGestureRecognizerState.Ended) 
			{
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
				{
					if (recognizer.VelocityInView(this.View).X < 0)
					{

						Hide();
					}
					else
					{
						Show();
					}
						
		         }
		         
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
				{
					if (recognizer.VelocityInView(this.View).X < 0)
					{
						Show();
					}
					else
					{
						Hide();
					}
						
		         }
		         
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
				{
					if (recognizer.VelocityInView(this.View).Y < 0)
					{

						Hide();
					}
					else
					{
						Show();
					}
		         }
		         
				if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
				{
					if (recognizer.VelocityInView(this.View).Y < 0)
					{
						Show();
					}
					else
					{
						Hide();
					}
						
		         }
		     }
  
        }
        
		/// <summary>
		/// Fixs the duration of the layout with.
		/// </summary>
		/// <param name="duration">Duration.</param>
        private void FixLayoutWithDuration(double duration) {
         
			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Left) 
			{
				this.SetContainerFrame(new CGRect(0, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);
			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Right) 
			{
				this.SetContainerFrame(new CGRect(this.View.Frame.Size.Width - this.FrostedViewController.CalculatedMenuViewSize.Width, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);
			}
             
			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Top) 
			{
				this.SetContainerFrame(new CGRect(0, 0, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

			}

			if (this.FrostedViewController.Direction == REFrostedViewControllerDirection.Bottom) 
			{
				this.SetContainerFrame(new CGRect(0, this.View.Frame.Size.Height - this.FrostedViewController.CalculatedMenuViewSize.Height, this.FrostedViewController.CalculatedMenuViewSize.Width, this.FrostedViewController.CalculatedMenuViewSize.Height));
				this.SetBackgroundViewsAlpha(this.FrostedViewController.BackgroundFadeAmount);

			}
        }
        
		/// <summary>
		/// Wills the animate rotation.
		/// </summary>
		/// <param name="toInterfaceOrientation">To interface orientation.</param>
		/// <param name="duration">Duration.</param>
		public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillAnimateRotation(toInterfaceOrientation, duration);

			this.FixLayoutWithDuration(duration);

		}

        #endregion
    }
}
