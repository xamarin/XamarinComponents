using System;
using System.Drawing;
using Xamarin.Themes.Core.Interfaces;
using System.ComponentModel;
using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace Xamarin.Themes.TrackBeam.Controls
{
	[Register("TBSwitch")]
	public class TBSwitch : UIControl 
	{
		UIImage knobImage;
		UIImage knobImageOff;
		UIImage sliderOff;
		UIImage sliderOn;
		nfloat percent, oldPercent = 0.0f;
		nfloat knobWidth;
		nfloat endcapWidth;

		nfloat scale;
		nfloat drawHeight;
		nfloat animationDuration;
		CGSize lastBoundsSize;
		DateTime endDate = DateTime.MinValue;
		bool mustFlip;

		CGRect boundsRect;

		public nfloat KnobWidth { 
			get { 
				return knobWidth; 
			} 
			set { 
				SetKnobWidth (value); 
			} 
		}

		[Export("IsOn"), Browsable(true)]
		public bool IsOn {
			get {
				return percent > 0.5f;
			}
			set {
				SetOn (value, false);
				SetNeedsDisplay();
			}
		}

		#region Constructors
		public TBSwitch (IntPtr handle) : base (handle)
		{
			InitCommon();
		}

		[Export("initWithFrame:")]
		public TBSwitch (RectangleF aRect): base(aRect)
		{
			InitCommon ();
		}

		[Export("initWithCoder:")]
		public TBSwitch (NSCoder aDecoder): base(aDecoder)
		{
			InitCommon ();
			percent = 1.0f;
		}
		#endregion

		private void InitCommon()
		{
			boundsRect = this.Bounds;

			drawHeight = 32;
			animationDuration = 0.25f;

			this.ContentMode = UIViewContentMode.Redraw;

			SetKnobWidth(20);
			RegenerateImages();

			var image = UIImage.FromBundle("TrackBeamThemeImages/switchOffBackground.png");
			sliderOff = image.CreateResizableImage(new UIEdgeInsets(5, 20, 5, 20));

			scale = UIScreen.MainScreen.Scale;

			this.Opaque = false;
			this.ExclusiveTouch = true;
			this.BackgroundColor = UIColor.Clear;

		}
			
		private void SetKnobWidth (nfloat aFloat)
		{
			knobWidth = (float)Math.Round (aFloat); // whole pixels only
			endcapWidth = (float)Math.Round(knobWidth / 2.0);

			knobImage = GetKnobImage (knobWidth);
			knobImageOff = GetKnobImage (knobWidth);
		}

		private UIImage GetKnobImage (nfloat knobWidth)
		{
			UIImage knobTmpImage = UIImage.FromBundle("TrackBeamThemeImages/switchHandle.png");
			UIImage knobImageStretch = knobTmpImage.CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0));


			var knobRect = new CGRect(0, 0, knobWidth, knobImageStretch.Size.Height);

			UIGraphics.BeginImageContextWithOptions(knobRect.Size, false, scale);
			knobImageStretch.Draw(knobRect);


			var image  = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();	

			return image;

		}

		private void RegenerateImages ()
		{
			UIImage onSwitchImage = UIImage.FromBundle("TrackBeamThemeImages/switchOnBackground.png");
			onSwitchImage = onSwitchImage.CreateResizableImage(new UIEdgeInsets(0, 20, 0, 20));

			UIImage sliderOnBase = onSwitchImage;

			var sliderOnRect = boundsRect;
			sliderOnRect.Height = sliderOnBase.Size.Height + sliderOnRect.Y;

			UIGraphics.BeginImageContextWithOptions(sliderOnRect.Size, false, scale);
			sliderOnBase.Draw(sliderOnRect);

			sliderOn = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

		}

		protected virtual void DrawUnderlayersInRect (CGRect aRect, nfloat offset, nfloat trackWidth)
		{
		}

		public override void Draw (CGRect rect)
		{
			var boundsRect = Bounds;
			boundsRect.Height = drawHeight;
			if (SizeF.Equals (boundsRect.Size, lastBoundsSize)) {
				RegenerateImages ();
				lastBoundsSize = boundsRect.Size;
			}

			nfloat width = boundsRect.Size.Width;
			nfloat drawPercent = percent;
			if (((width - knobWidth) * drawPercent) < 3)
				drawPercent = 0.0f;
			if (((width - knobWidth) * drawPercent) > (width - knobWidth - 3))
				drawPercent = 1.0f;

			if (endDate != DateTime.MinValue) {
				var interval = (endDate - DateTime.Now).TotalSeconds;
				if (interval < 0.0) {
					endDate = DateTime.MinValue;
				} else {
					if (percent == 1f)
						drawPercent = (float)Math.Cos ((interval / animationDuration) * (Math.PI / 2.0));
					else
						drawPercent = 1.0f - (float)Math.Cos ((interval / animationDuration) * (Math.PI / 2.0));
					PerformSelector (new Selector("setNeedsDisplay"), null, 0.0);
				}
			}

			CGContext context = UIGraphics.GetCurrentContext ();
			context.SaveState ();
			UIGraphics.PushContext (context);

			if (drawPercent != 1.0)
			{
				var sliderOffRect = boundsRect;
				sliderOffRect.Height = sliderOff.Size.Height;
				sliderOff.Draw (sliderOffRect);
			}


			if (drawPercent > 0.0f && drawPercent < 1.0f) {		
				nfloat onWidth = knobWidth / 2 + ((width - knobWidth / 2) - knobWidth / 2) * drawPercent;
				var sourceRect = new CGRect (0, 0, onWidth * scale, sliderOn.Size.Height * scale);
				var drawOnRect = new CGRect (0, 0, onWidth, sliderOn.Size.Height);
				CGImage sliderOnSubImage = sliderOn.CGImage.WithImageInRect (sourceRect);
				context.SaveState ();
				context.ScaleCTM (1, -1);
				context.TranslateCTM (0, -drawOnRect.Height);	
				context.DrawImage (drawOnRect, sliderOnSubImage);
				context.RestoreState ();
			}

			if (drawPercent == 1.0) {		
				nfloat onWidth = sliderOn.Size.Width;
				var sourceRect = new CGRect (0, 0, onWidth * scale, sliderOn.Size.Height * scale);
				var drawOnRect = new CGRect (0, 0, onWidth, sliderOn.Size.Height);
				CGImage sliderOnSubImage = sliderOn.CGImage.WithImageInRect (sourceRect);
				context.SaveState ();
				context.ScaleCTM (1, -1);
				context.TranslateCTM (0, -drawOnRect.Height);	
				context.DrawImage (drawOnRect, sliderOnSubImage);
				context.RestoreState ();
			}


			context.SaveState ();
			UIGraphics.PushContext (context);
			var insetClipRect = boundsRect.Inset (4, 4);
			UIGraphics.RectClip (insetClipRect);
			DrawUnderlayersInRect (rect, drawPercent * (boundsRect.Width - knobWidth), boundsRect.Width - knobWidth);
			UIGraphics.PopContext ();
			context.RestoreState ();

			context.ScaleCTM (1, -1);
			context.TranslateCTM (0, -boundsRect.Height);
			CGPoint location = boundsRect.Location;
			UIImage imageToDraw = knobImage;

			if (this.Highlighted)
				imageToDraw = knobImageOff;

			nfloat xlocation;

			if (drawPercent == 0.0f) 
			{
				xlocation = location.X + 3 + (nfloat)Math.Round(drawPercent * (boundsRect.Width - knobWidth + 2));
			} else {
				xlocation = location.X - 5 + (nfloat)Math.Round(drawPercent * (boundsRect.Width - knobWidth + 2));
				xlocation = xlocation < 0.0f ? 0.0f : xlocation; 
			}

			var rectToDraw = new CGRect (xlocation, location.Y + 9.5f, knobWidth, knobImage.Size.Height);
			context.DrawImage (rectToDraw, imageToDraw.CGImage);

			UIGraphics.PopContext ();
			context.RestoreState ();
		}

		public override bool BeginTracking (UITouch touch, UIEvent uievent)
		{
			Highlighted = true;
			oldPercent = percent;
			endDate = DateTime.MinValue;
			mustFlip = true;
			SetNeedsDisplay ();
			SendActionForControlEvents (UIControlEvent.TouchDown);
			return true;
		}

		public override bool ContinueTracking (UITouch touch, UIEvent uievent)
		{
			CGPoint point = touch.LocationInView (this);
			percent = (point.X - knobWidth / 2.0f) / (Bounds.Width - knobWidth);
			if (percent < 0.0f)
				percent = 0.0f;
			if (percent > 1.0f)
				percent = 1.0f;
			if ((oldPercent < 0.25f && percent > 0.5f) || (oldPercent > 0.75f && percent < 0.5f))
				mustFlip = false;
			SetNeedsDisplay ();
			SendActionForControlEvents (UIControlEvent.TouchDragInside);
			return true;
		}

		void FinishEvent ()
		{
			Highlighted = false;
			endDate = DateTime.MinValue;
			float toPercent = (float)Math.Round (1f - oldPercent);
			if (!mustFlip) {
				if (oldPercent < 0.25f) {
					toPercent = percent > 0.5f ? 1f : 0f;
				} else if (oldPercent > 0.75f) {
					toPercent = percent < 0.5f ? 0f : 1f;
				}
			}
			PerformSwitchToPercent (toPercent);
		}

		public override void CancelTracking (UIEvent uievent)
		{
			FinishEvent ();
		}

		public override void EndTracking (UITouch uitouch, UIEvent uievent)
		{
			FinishEvent ();
		}

		public void SetOn (bool isOn, bool animated = false)
		{
			if (animated) {
				float toPercent = isOn ? 1.0f : 0.0f;
				if ((percent < 0.5f && isOn) || (percent > 0.5f && !isOn))
					PerformSwitchToPercent (toPercent);
			} else {
				percent = isOn ? 1.0f : 0.0f;
				SetNeedsDisplay ();
				SendActionForControlEvents (UIControlEvent.ValueChanged);
			}
		}

		void PerformSwitchToPercent (float toPercent)
		{
			endDate = DateTime.Now.AddSeconds (Math.Abs(percent - toPercent) * animationDuration);
			percent = toPercent;
			SetNeedsDisplay ();
			SendActionForControlEvents (UIControlEvent.ValueChanged);
			SendActionForControlEvents (UIControlEvent.TouchUpInside);
		}


	}
}

