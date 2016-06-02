using System;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.Collections.Generic;
using Foundation;
using ObjCRuntime;
using JZMultiChoice.Collections;

namespace JZMultiChoice
{
	/// <summary>
	/// JZMultiChoicesCircleButton
	/// </summary>
	public class JZMultiChoicesCircleButton : UIView
	{
		#region Fields 

		private UIButton SmallButton;
		private UIView BackgroundView;
		private float SmallRadius;
		private float BigRadius;
		private CGPoint CenterPoint;
		private UIImage IconImage;
		private float ParallexParameter;

		private bool isTouchDown;
		private bool Parallex;
		private bool IsPerformingTouchUpInsideAnimation;
		private CATextLayer label;

		private UIImageView CallbackIcon;
		private UILabel CallbackMessage;

		private nfloat FullPara;
		private double MidiumPara;
		private double SmallPara;

		private List<UIImageView> IconArray;

		private ChoiceItemCollection mItems;
		#endregion

		#region Properties 
		public double CircleRadius { get; set;}
		public UIColor CircleColor { get; set;}
		public UIViewController ResponderUIVC { get; set;}
		#endregion

		#region Constructors 

		/// <summary>
		/// Initializes a new instance of the <see cref="JZMultiChoice.JZMultiChoicesCircleButton"/> class.
		/// </summary>
		public JZMultiChoicesCircleButton ()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JZMultiChoice.JZMultiChoicesCircleButton"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public JZMultiChoicesCircleButton (CGRect frame) 
			: base(frame)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JZMultiChoice.JZMultiChoicesCircleButton"/> class.
		/// </summary>
		/// <param name="point">Center Point.</param>
		/// <param name="icon">Center Icon</param>
		/// <param name="sRadius">Small radius.</param>
		/// <param name="bRadius">Big radius.</param>
		/// <param name="mMenuItems">Choice items</param>
		/// <param name="isParallex">If set to <c>true</c> is parallex.</param>
		/// <param name="parallex">Parallex amount</param>
		/// <param name="vc">ViewContoller.</param>
		public JZMultiChoicesCircleButton (CGPoint point, UIImage icon
			, float sRadius, float bRadius, ChoiceItemCollection mMenuItems, bool isParallex
			, float parallex, UIViewController vc) : this(UIScreen.MainScreen.Bounds)
		{
			this.SmallRadius = sRadius;
			this.BigRadius = bRadius;
			this.isTouchDown = false;
			this.CenterPoint = point;
			this.IconImage = icon;
			this.ParallexParameter = parallex;
			this.Parallex = isParallex;
			this.ResponderUIVC = vc;

			BackgroundView = new  UIView (new CGRect (point.X - sRadius,point.Y - sRadius, sRadius * 2, sRadius * 2));
			BackgroundView.BackgroundColor = new UIColor(0f,0f,0f,0.7f); 
			BackgroundView.Layer.CornerRadius = sRadius;
			this.Add (BackgroundView);

			SmallButton = new UIButton(new CGRect (point.X - sRadius,point.Y - sRadius, sRadius * 2, sRadius * 2));
			SmallButton.Layer.CornerRadius = sRadius;
			SmallButton.Layer.BackgroundColor = UIColor.FromRGBA(252.0f/255.0f, 81.0f/255.0f, 106.0f/255.0f, 1.0f).CGColor;
			SmallButton.Layer.ShadowColor = UIColor.Black.CGColor;
			SmallButton.Layer.ShadowOffset = new CGSize(0.0f, 6.0f);
			SmallButton.Layer.ShadowOpacity = 0.3f;
			SmallButton.Layer.ShadowRadius = 4.0f;
			SmallButton.Layer.ZPosition = bRadius;

			SmallButton.SetImage (IconImage, UIControlState.Normal);

			SmallButton.AddTarget (TouchDown, UIControlEvent.TouchDown);
			SmallButton.AddTarget (this, new Selector("TouchDrag:withEvent:"), UIControlEvent.TouchDragInside);
			SmallButton.AddTarget (this, new Selector("TouchDrag:withEvent:"), UIControlEvent.TouchDragOutside);
			SmallButton.AddTarget (this, new Selector("TouchUpInside:withEvent:"), UIControlEvent.TouchUpInside);
			SmallButton.AddTarget (TouchUpOutside, UIControlEvent.TouchUpOutside);

			this.Add (SmallButton);

			label = new CATextLayer ();
			label.FontSize =  9.0f;
			label.String = @"Choose:";
			label.FontSize = 40;
			label.SetFont (@"ArialMT");
			label.AlignmentMode = CATextLayer.AlignmentCenter;

			label.ForegroundColor = UIColor.FromWhiteAlpha (1.0f, 0.0f).CGColor;
			label.Frame = new CGRect (-SmallRadius*3, -52, SmallRadius * 8,100);

			var UnScaleFactor = SmallRadius/BigRadius;
			label.Transform = CATransform3D.MakeScale(UnScaleFactor, UnScaleFactor, 1.0f);
			SmallButton.Layer.AddSublayer(label);

			var TransformPara = BigRadius / SmallRadius;
			var MultiChoiceRadius = (BigRadius + SmallRadius)/8/TransformPara;

			this.mItems = mMenuItems;

			IconArray = new List<UIImageView> ();

			var number = mMenuItems.Count;

			foreach (var aItem in this.mItems) 
			{
				var i = mItems.IndexOf (aItem);

				var XOffest = 4 * MultiChoiceRadius * Math.Cos(2*Math.PI*i/number);
				var YOffest = 4 * MultiChoiceRadius * Math.Sin(2*Math.PI*i/number);


				var IconImageView = new UIImageView(new CGRect(sRadius + XOffest - MultiChoiceRadius , sRadius + YOffest - MultiChoiceRadius, MultiChoiceRadius * 2, MultiChoiceRadius * 2));

				IconImageView.Image = aItem.Icon;
				IconImageView.Alpha = 0.0f;
				IconImageView.Hidden = true;
				SmallButton.Add (IconImageView);

				SmallButton.BringSubviewToFront (IconImageView);

				IconImageView.Layer.ContentsScale =  UIScreen.MainScreen.Scale * BigRadius/SmallRadius;

				IconArray.Add (IconImageView);
			}
				


			var UnFullFactor = SmallRadius / this.Frame.Size.Height;
			CallbackIcon = new UIImageView(new CGRect((SmallButton.Frame.Size.Width - BigRadius)/2, (SmallButton.Frame.Size.Height - BigRadius)/2, BigRadius, BigRadius));
			CallbackIcon.Layer.Transform = CATransform3D.MakeScale(UnFullFactor, UnFullFactor, 1.0f);
			CallbackIcon.Image = UIImage.FromBundle("CallbackSuccess");
			CallbackIcon.Alpha = 0.0f;
			SmallButton.Add (CallbackIcon);


			CallbackMessage = new UILabel ();
			CallbackMessage.Text = @"";
			CallbackMessage.Alpha = 0.0f;
			CallbackMessage.Font = UIFont.SystemFontOfSize (20.0f);
			CallbackMessage.Layer.Transform = CATransform3D.MakeScale(UnFullFactor, UnFullFactor, 1.0f);
			CallbackMessage.TextColor = UIColor.White;//[UIColor whiteColor];
			CallbackMessage.TextAlignment = UITextAlignment.Center;
			CallbackMessage.Frame = new CGRect((SmallButton.Frame.Size.Width - SmallRadius/2)/2, (SmallButton.Frame.Size.Height - SmallRadius/4)/2+ 6, SmallRadius/2, SmallRadius/4);
			SmallButton.Add(CallbackMessage);
//
			FullPara = this.Frame.Size.Height / SmallRadius;

			var thning = SmallButton.Layer.ValueForKey (new NSString (@"transform.scale"));

			if (thning != null) {
				var val = (NSNumber)NSNumber.FromObject (thning);
				MidiumPara = val.DoubleValue;
			} else
				MidiumPara = 1.0f;

			SmallPara = 1.0f;
		}

		#endregion

		#region Handlers

		/// <summary>
		/// Hits the test.
		/// </summary>
		/// <returns>The test.</returns>
		/// <param name="point">Point.</param>
		/// <param name="uievent">Uievent.</param>
		public override UIView HitTest (CGPoint point, UIEvent uievent)
		{
			var hitView = base.HitTest (point, uievent);

			if (hitView == this)
				return null;
			else
				return hitView;
		}

		/// <summary>
		/// Sets the touch down.
		/// </summary>
		/// <value>The touch down.</value>
		public void TouchDown(object sender, EventArgs args)
		{
			if (this.IsPerformingTouchUpInsideAnimation) {
				return;
			}
				
			if (!isTouchDown)
			{
				TouchDownAnimation ();

				label.ForegroundColor = UIColor.FromWhiteAlpha (1.0f, 1.0f).CGColor;
			}

			this.isTouchDown = true;
		}

		/// <summary>
		/// Touches down animation.
		/// </summary>
		public void TouchDownAnimation()
		{

			UIView.AnimateNotify (0.1f, 0, UIViewAnimationOptions.BeginFromCurrentState, () => {

				SmallButton.ImageView.Alpha = 0.0f;

			}, (finished) => {

				if (finished)
				{
					SmallButton.SetImage(null, UIControlState.Normal);
				}
			});
				
			var ButtonScaleBigCABasicAnimation = CABasicAnimation.FromKeyPath (@"transform.scale");
			ButtonScaleBigCABasicAnimation.Duration = 0.1f;
			ButtonScaleBigCABasicAnimation.AutoReverses = false;
			ButtonScaleBigCABasicAnimation.From = NSNumber.FromFloat(1.0f);
			ButtonScaleBigCABasicAnimation.To = NSNumber.FromFloat(BigRadius / SmallRadius);
			ButtonScaleBigCABasicAnimation.FillMode = CAFillMode.Forwards; 
			ButtonScaleBigCABasicAnimation.RemovedOnCompletion = false;

			SmallButton.Layer.AddAnimation (ButtonScaleBigCABasicAnimation, @"ButtonScaleBigCABasicAnimation");

			var BackgroundViewScaleBigCABasicAnimation = CABasicAnimation.FromKeyPath (@"transform.scale");
			BackgroundViewScaleBigCABasicAnimation.Duration = 0.1f;
			BackgroundViewScaleBigCABasicAnimation.AutoReverses = false;
			BackgroundViewScaleBigCABasicAnimation.From = NSNumber.FromFloat(1.0f);
			BackgroundViewScaleBigCABasicAnimation.To = NSNumber.FromNFloat (this.Frame.Size.Height / SmallRadius);
			BackgroundViewScaleBigCABasicAnimation.FillMode = CAFillMode.Forwards; 
			BackgroundViewScaleBigCABasicAnimation.RemovedOnCompletion = false;

			BackgroundView.Layer.AddAnimation (BackgroundViewScaleBigCABasicAnimation, @"BackgroundViewScaleBigCABasicAnimation");

			foreach (UIImageView Icon in IconArray)
			{
				this.Layer.RemoveAllAnimations ();
				Icon.Hidden = false;

				UIView.AnimateNotify (0.3f, 0, UIViewAnimationOptions.BeginFromCurrentState, () => {

					Icon.Alpha = 0.7f;

				}, (finished) => {
					
				});
			}

		}

		[Export("TouchDrag:withEvent:")]
		public void TouchDrag(UIButton sender,UIEvent evt)
		{
			UITouch touch = (UITouch)evt.AllTouches.AnyObject;
			CGPoint Point = touch.LocationInView (this);

			var XOffest = Point.X - CenterPoint.X;
			var YOffest = Point.Y - CenterPoint.Y;

			var XDegress = XOffest / this.Frame.Size.Width;
			var YDegress = YOffest / this.Frame.Size.Height;


			var Rotate = CATransform3D.MakeRotation (XDegress, 0, 1, 0).Concat (CATransform3D.MakeRotation (-YDegress, 1, 0, 0));

			if (Parallex)
			{
				SmallButton.Layer.Transform = CATransform3DPerspect(Rotate, new CGPoint(0, 0), BigRadius+ParallexParameter);
			}
			else
			{
				//Do nothing ^_^
			}

			var count = 0;
			String infotext = null;

			foreach (UIImageView Icon in IconArray)
			{

				// Child center relative to parent
				var childPosition = Icon.Layer.PresentationLayer.Position;

				// Parent center relative to UIView
				var parentPosition = SmallButton.Layer.PresentationLayer.Position;
				var parentCenter = new CGPoint(SmallButton.Bounds.Size.Width/2.0, SmallButton.Bounds.Size.Height /2.0);

				// Child center relative to parent center
				var relativePos = new CGPoint(childPosition.X - parentCenter.X, childPosition.Y - parentCenter.Y);


				// Transformed child position based on parent's transform (rotations, scale etc)
				var transformedChildPos = SmallButton.Layer.PresentationLayer.AffineTransform.TransformPoint (relativePos);

				// And finally...
				CGPoint positionInView = new CGPoint(parentPosition.X + transformedChildPos.X, parentPosition.Y + transformedChildPos.Y);

				XOffest = (positionInView.X - this.CenterPoint.X) / SmallRadius * BigRadius;
				YOffest = (positionInView.Y - this.CenterPoint.Y) / SmallRadius * BigRadius;

				var IconCGRectinWorld = new CGRect(this.CenterPoint.X + XOffest - (BigRadius + SmallRadius)/4, this.CenterPoint.Y + YOffest - (BigRadius + SmallRadius)/4, (BigRadius + SmallRadius)/2, (BigRadius + SmallRadius)/2);

				if (IconCGRectinWorld.Contains(Point))
				{
					Icon.Alpha = 1.0f;

					infotext = mItems [count].Title;//  InfoArray[];

				}
				else
				{
					Icon.Alpha =  0.7f;
				}

				count++;
			}

			if (!String.IsNullOrWhiteSpace(infotext))
			{
				label.String = infotext;

			}
			else
			{
				label.String = @"Choose";
			}
		}
			
		[Export("TouchUpInside:withEvent:")]
		public void TouchUpInside(UIButton sender,UIEvent evt)
		{
			UITouch touch = (UITouch)evt.AllTouches.AnyObject;
			CGPoint Point = touch.LocationInView (this);

			label.ForegroundColor = UIColor.FromWhiteAlpha (1.0f, 0.0f).CGColor;

			bool isTouchUpInsideButton = false;
			int indexTouchUpInsideButton = 0;
			int count = 0;

			if (isTouchDown)
			{

				foreach (UIImageView Icon in IconArray)
				{

					// Child center relative to parent
					var childPosition = Icon.Layer.PresentationLayer.Position;

					// Parent center relative to UIView
					var parentPosition = SmallButton.Layer.PresentationLayer.Position;

					var parentCenter = new CGPoint(SmallButton.Bounds.Size.Width/2.0, SmallButton.Bounds.Size.Height /2.0);

					// Child center relative to parent center
					var relativePos = new CGPoint(childPosition.X - parentCenter.X, childPosition.Y - parentCenter.Y);

					// Transformed child position based on parent's transform (rotations, scale etc)
					var transformedChildPos = SmallButton.Layer.PresentationLayer.AffineTransform.TransformPoint(relativePos);

					// And finally...
					var positionInView = new CGPoint(parentPosition.X + transformedChildPos.X, parentPosition.Y + transformedChildPos.Y);

					//NSLog(@"positionInView %@",NSStringFromCGPoint(positionInView));

					//NSLog(@"View'S position %@",NSStringFromCGPoint(self.layer.position));

					var XOffest = (positionInView.X - this.CenterPoint.X) / SmallRadius*BigRadius;
					var YOffest = (positionInView.Y - this.CenterPoint.Y) / SmallRadius*BigRadius;

					var IconCGRectinWorld = new CGRect(this.CenterPoint.X + XOffest - (BigRadius + SmallRadius)/4, this.CenterPoint.Y + YOffest - (BigRadius + SmallRadius)/4, (BigRadius + SmallRadius)/2, (BigRadius + SmallRadius)/2);


					if (IconCGRectinWorld.Contains(Point))
					{
						isTouchUpInsideButton = true;
						indexTouchUpInsideButton = count;
					}

					count++;
				}

				if (isTouchUpInsideButton)
				{
					var aItem = mItems[indexTouchUpInsideButton];

					if (!aItem.DisableActionAnimation) {
						TouchUpInsideAnimation ();	
					} 
					else 
					{
						TouchUpAnimation();	
					}

					if (ResponderUIVC != null) 
					{
						ResponderUIVC.BeginInvokeOnMainThread (() => {

							if (aItem.Action != null)
								aItem.Action();
						});
					}
						
				}
				else
				{
					TouchUpAnimation();
				}
			}
           
			this.isTouchDown = false;
		}

		/// <summary>
		/// Touchs up outside.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void TouchUpOutside(object sender, EventArgs args)
		{
			if (isTouchDown)
			{
				TouchUpAnimation ();

				label.ForegroundColor = UIColor.FromWhiteAlpha (1.0f, 0.0f).CGColor;
			}

			this.isTouchDown = false;
		}
			
		/// <summary>
		/// Touchs up inside animation.
		/// </summary>
		public void TouchUpInsideAnimation()
		{
			this.IsPerformingTouchUpInsideAnimation = true;

			foreach (UIImageView Icon in IconArray)
			{
				UIView.AnimateNotify(0.1f,0.0f,UIViewAnimationOptions.BeginFromCurrentState,()=>
					{
						Icon.Alpha = 0.0f;

					},(finished) => 
					{
						if (finished) 
						{
							Icon.Hidden = true;
						}
					});
			}

			var BackgroundViewScaleSmallCABasicAnimation = CABasicAnimation.FromKeyPath (@"transform.scale");
			BackgroundViewScaleSmallCABasicAnimation.Duration = 0.1f;
			BackgroundViewScaleSmallCABasicAnimation.AutoReverses = false;
			BackgroundViewScaleSmallCABasicAnimation.To = NSNumber.FromDouble(SmallPara);
			BackgroundViewScaleSmallCABasicAnimation.From = NSNumber.FromDouble(MidiumPara);
			BackgroundViewScaleSmallCABasicAnimation.FillMode = CAFillMode.Forwards;
			BackgroundViewScaleSmallCABasicAnimation.RemovedOnCompletion = false;
			BackgroundViewScaleSmallCABasicAnimation.BeginTime = 0.0f;

			BackgroundView.Layer.AddAnimation (BackgroundViewScaleSmallCABasicAnimation, @"BackgroundViewScaleSmallCABasicAnimation");

			var ButtonScaleFullCABasicAnimation = CABasicAnimation.FromKeyPath (@"transform.scale");
			ButtonScaleFullCABasicAnimation.Duration = 0.2f;
			ButtonScaleFullCABasicAnimation.AutoReverses = false;
			ButtonScaleFullCABasicAnimation.To = NSNumber.FromNFloat(FullPara);
			ButtonScaleFullCABasicAnimation.From = NSNumber.FromDouble(MidiumPara);
			ButtonScaleFullCABasicAnimation.FillMode = CAFillMode.Forwards;
			ButtonScaleFullCABasicAnimation.RemovedOnCompletion = false;
			ButtonScaleFullCABasicAnimation.BeginTime = 0.0f;

			var animGroup = CAAnimationGroup.CreateAnimation ();
			animGroup.Animations = new CAAnimation[]{ ButtonScaleFullCABasicAnimation };
			animGroup.Duration = 0.4f;
			animGroup.RemovedOnCompletion = false;
			animGroup.AutoReverses = false;
            animGroup.FillMode = CAFillMode.Forwards;

			CATransaction.Begin ();
			CATransaction.CompletionBlock = () => {

				IsPerformingTouchUpInsideAnimation = false;

			};

			SmallButton.Layer.AddAnimation (animGroup, @"ButtonScaleAnimation");
			CATransaction.Commit ();


			CATransform3D Rotate = CATransform3D.MakeRotation(0, 0, 1, 0).Concat(CATransform3D.MakeRotation(0, 1, 0, 0));
			if (Parallex)
			{
				SmallButton.Layer.Transform = CATransform3DPerspect(Rotate, new CGPoint(0, 0), BigRadius+ParallexParameter);
			}
			else
			{
				//Do nothing ^_^
			}
		}

		/// <summary>
		/// Touchs up animation.
		/// </summary>
		public void TouchUpAnimation()
		{
			foreach (UIImageView Icon in IconArray)
			{
				UIView.AnimateNotify(0.1f, 0.0f,UIViewAnimationOptions.BeginFromCurrentState, ()=>
					{
						Icon.Alpha = 0.0f;
					}
					,(finished) => 
						{
							Icon.Hidden = true;
						});
			}

			var ButtonScaleSmallCABasicAnimation = CABasicAnimation.FromKeyPath(@"transform.scale");
			ButtonScaleSmallCABasicAnimation.Duration = 0.2f;
			ButtonScaleSmallCABasicAnimation.AutoReverses = false;
			ButtonScaleSmallCABasicAnimation.To = NSNumber.FromFloat (1.0f);
			ButtonScaleSmallCABasicAnimation.From = NSNumber.FromFloat (BigRadius / SmallRadius);
			ButtonScaleSmallCABasicAnimation.FillMode = CAFillMode.Forwards;
			ButtonScaleSmallCABasicAnimation.RemovedOnCompletion = false;

			SmallButton.Layer.AddAnimation (ButtonScaleSmallCABasicAnimation, @"ButtonScaleSmallCABasicAnimation");


			var BackgroundViewScaleSmallCABasicAnimation = CABasicAnimation.FromKeyPath(@"transform.scale");
			BackgroundViewScaleSmallCABasicAnimation.Duration = 0.1f;
			BackgroundViewScaleSmallCABasicAnimation.AutoReverses = false;
			BackgroundViewScaleSmallCABasicAnimation.To = NSNumber.FromFloat (1.0f);
			BackgroundViewScaleSmallCABasicAnimation.From = NSNumber.FromNFloat (this.Frame.Size.Height / SmallRadius);
			BackgroundViewScaleSmallCABasicAnimation.FillMode = CAFillMode.Forwards;
			BackgroundViewScaleSmallCABasicAnimation.RemovedOnCompletion = false;


			BackgroundView.Layer.AddAnimation (BackgroundViewScaleSmallCABasicAnimation, @"BackgroundViewScaleSmallCABasicAnimation");

			var Rotate = CATransform3D.MakeRotation(0, 0, 1, 0).Concat(CATransform3D.MakeRotation(0, 1, 0, 0));
			if (Parallex)
			{
				SmallButton.Layer.Transform = CATransform3DPerspect(Rotate, new CGPoint(0, 0), BigRadius+ParallexParameter);
			}
			else
			{
				//Do nothing ^_^
			}

			SmallButton.SetImage (IconImage, UIControlState.Normal);

			UIView.AnimateNotify(0.1f, 0.0f,UIViewAnimationOptions.BeginFromCurrentState, ()=>
				{
					SmallButton.ImageView.Alpha = 1.0f;

				}
				,(finished) => 
				{
					
				});
					
		
		}

		private CATransform3D CATransform3DMakePerspective(CGPoint center, float disZ)
		{
			CATransform3D transToCenter = CATransform3D.MakeTranslation(-center.X, -center.Y, 0);
			CATransform3D transBack = CATransform3D.MakeTranslation(center.X, center.Y, 0);
			CATransform3D scale = CATransform3D.Identity;

			scale.m34 = -1.0f/disZ; 

			return transToCenter.Concat(scale).Concat(transBack);
		}

		private CATransform3D CATransform3DPerspect(CATransform3D t, CGPoint center, float disZ)
		{
			return t.Concat(CATransform3DMakePerspective(center, disZ));
		}

		#endregion

		/// <summary>
		/// Shows the complete screen.
		/// </summary>
		/// <param name="message">Message to display</param>
		/// <param name="wasSuccessful">If set to <c>true</c> was process successful.</param>
		public void ShowCompleteScreen(string message, bool wasSuccessful)
		{
			var image = (wasSuccessful) ? UIImage.FromBundle ("CallbackSuccess") : UIImage.FromBundle ("CallbackWrong");
			CallbackIcon.Image = image;
			CallbackMessage.Text = message;

			UIView.AnimateNotify (0.3f, 0.0f, UIViewAnimationOptions.BeginFromCurrentState, () => { CallbackMessage.Alpha = 1.0f;}, (finished) => {});
			UIView.AnimateNotify (0.3f, 0.0f, UIViewAnimationOptions.BeginFromCurrentState, () => { CallbackIcon.Alpha = 1.0f;}, (finished) => {});

			var ButtonScaleKeepCABasicAnimation = CABasicAnimation.FromKeyPath(@"transform.scale");
			ButtonScaleKeepCABasicAnimation.Duration = 2.0f;
			ButtonScaleKeepCABasicAnimation.AutoReverses = false;
			ButtonScaleKeepCABasicAnimation.From = NSNumber.FromNFloat(FullPara);
			ButtonScaleKeepCABasicAnimation.To = NSNumber.FromNFloat(FullPara);
			ButtonScaleKeepCABasicAnimation.FillMode = CAFillMode.Forwards;
			ButtonScaleKeepCABasicAnimation.RemovedOnCompletion = false;
			ButtonScaleKeepCABasicAnimation.BeginTime = 0.0f;

			var ButtonScaleSmallCABasicAnimation = CABasicAnimation.FromKeyPath(@"transform.scale");
			ButtonScaleSmallCABasicAnimation.Duration = 0.2f;
			ButtonScaleSmallCABasicAnimation.AutoReverses = false;
			ButtonScaleSmallCABasicAnimation.From = NSNumber.FromNFloat(FullPara);
			ButtonScaleSmallCABasicAnimation.To = NSNumber.FromDouble(SmallPara);
			ButtonScaleSmallCABasicAnimation.FillMode = CAFillMode.Forwards;
			ButtonScaleSmallCABasicAnimation.RemovedOnCompletion = false;
			ButtonScaleSmallCABasicAnimation.BeginTime = 2.0f;

			SmallButton.Layer.AddAnimation (ButtonScaleSmallCABasicAnimation, @"ButtonScaleAnimation");

			var animGroup = CAAnimationGroup.CreateAnimation();
			animGroup.Animations = new CAAnimation[] { ButtonScaleKeepCABasicAnimation, ButtonScaleSmallCABasicAnimation };
			animGroup.Duration = 2.2f;
			animGroup.RemovedOnCompletion = false;
			animGroup.AutoReverses = false;
			animGroup.FillMode = CAFillMode.Forwards;


			CATransaction.Begin ();

			CATransaction.CompletionBlock = () => {

				UIView.AnimateNotify (0.1f, 0.0f, UIViewAnimationOptions.BeginFromCurrentState, () => { CallbackIcon.Alpha = 0.0f;}, (finished) => {});
				UIView.AnimateNotify (0.1f, 0.0f, UIViewAnimationOptions.BeginFromCurrentState, () => { CallbackMessage.Alpha = 0.0f;}, (finished) => {});

				SmallButton.SetImage(IconImage, UIControlState.Normal);

				UIView.AnimateNotify (0.1f, 0.0f, UIViewAnimationOptions.BeginFromCurrentState, () => { SmallButton.ImageView.Alpha = 1.0f;}, (finished) => {});
			};

			SmallButton.Layer.AddAnimation(animGroup,@"ButtonScaleAnimation");
			CATransaction.Commit();
		}
	}
}

