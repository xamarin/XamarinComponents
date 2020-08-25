using System;
using AppKit;
using Foundation;
using CoreGraphics;
using CoreAnimation;

namespace ITSwitch
{
	[Register("ITSwitchView")]
	public class ITSwitchView : NSControl {

		#region Class Methods

		/// <summary>
		/// Calculates the width of the golden.
		/// </summary>
		/// <returns>The golden width.</returns>
		/// <param name="height">Height.</param>
		public static float CalculateGoldenWidth(float height)
		{
			return height * 1.618f;
		}

		#endregion
		#region Constants

		private const float kAnimationDuration = 0.4f;

		private const float kBorderLineWidth = 1f;

		private const float kGoldenRatio = 1.61803398875f;

		private const float kDecreasedGoldenRatio = 1.38f;

		private const float kEnabledOpacity = 1.0f;

		private const float kDisabledOpacity = 0.5f;


		#endregion

		#region Events

		/// <summary>
		/// The on switch changed.
		/// </summary>
		public event EventHandler OnSwitchChanged;

		#endregion
		#region Fields

		private readonly NSColor kDefaultTintColor = NSColor.FromDeviceRgba(0.27f, 0.86f,0.36f,1.0f);
		private readonly NSColor kKnobBackgroundColor = NSColor.FromCalibratedWhite(1.0f,1.0f);
		private readonly NSColor kDisabledBorderColor = NSColor.FromCalibratedWhite(0.0f, 0.2f);
		private readonly NSColor kDisabledBackgroundColor = NSColor.Clear;
		//private readonly NSColor kInactiveBackgroundColor = NSColor.FromCalibratedWhite(0,0.3f);

		private NSColor mTintColor; 
		private Boolean mOn;
		private Boolean mIsActive;

		Boolean mDragged;
		Boolean mDraggingTowardsOn;

		CALayer mRootLayer;
		CALayer mBackgroundLayer;
		CALayer mKnobLayer;
		CALayer mKnobInsideLayer;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ITSwitch.ITSwitchView"/> class.
		/// </summary>
		public ITSwitchView() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ITSwitch.ITSwitchView"/> class.
		/// </summary>
		/// <param name="ptr">Ptr.</param>
		public ITSwitchView(IntPtr ptr) 
			: base(ptr)
		{
			
			SetUp();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ITSwitch.ITSwitchView"/> class.
		/// </summary>
		/// <param name="coder">Coder.</param>
		public ITSwitchView(NSCoder coder) 
			: base(coder)
		{
			SetUp();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ITSwitch.ITSwitchView"/> class.
		/// </summary>
		/// <param name="frame">Frame.</param>
		public ITSwitchView(CGRect frame) 
			: base(frame)
		{
			SetUp();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether this instance can become key view.
		/// </summary>
		/// <value><c>true</c> if this instance can become key view; otherwise, <c>false</c>.</value>
		public override bool CanBecomeKeyView
		{
			get
			{
				return NSApplication.SharedApplication.FullKeyboardAccessEnabled;
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ITSwitch.Demo.ITSwitch.ITSwitch"/> is on.
		/// </summary>
		/// <value><c>true</c> if on; otherwise, <c>false</c>.</value>
		public Boolean IsOn 
		{
			get {return mOn;}
			set
			{
				if (mOn != value)
				{
					mOn = value;

					this.ReloadLayer();
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the tint.
		/// </summary>
		/// <value>The color of the tint.</value>
		public NSColor TintColor 
		{
			get
			{
				if (mTintColor == null)
					return kDefaultTintColor;

				return mTintColor;
			}
			set
			{
				mTintColor = value;

				this.ReloadLayer();
			}
		}

		/// <summary>
		/// Gets or sets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public override CGRect Frame
		{
			get
			{
				return base.Frame;
			}
			set
			{
				base.Frame = value;

				this.ReloadLayerSize();
			}
		}

		/// <summary>
		/// Gets the focus ring mask bounds.
		/// </summary>
		/// <value>The focus ring mask bounds.</value>
		public override CGRect FocusRingMaskBounds
		{
			get
			{
				return this.Bounds;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ITSwitch.Demo.ITSwitch.ITSwitch"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Sets up.
		/// </summary>
		private void SetUp() 
		{
			this.AutoresizesSubviews = true;
			this.Enabled = true;

			SetUpLayers();
		}

		/// <summary>
		/// Sets up layers.
		/// </summary>
		private void SetUpLayers() {


			mRootLayer = CALayer.Create();
			this.Layer = mRootLayer;
			this.WantsLayer = true;

			mBackgroundLayer = CALayer.Create();
			mBackgroundLayer.AutoresizinMask = CAAutoresizingMask.WidthSizable | CAAutoresizingMask.HeightSizable;
			mBackgroundLayer.Bounds = mRootLayer.Bounds;
			mBackgroundLayer.AnchorPoint = new CGPoint(0.0f,0.0f);
			mBackgroundLayer.BorderWidth = kBorderLineWidth;
			mRootLayer.AddSublayer(mBackgroundLayer);

			mKnobLayer = CALayer.Create();
			mKnobLayer.Frame = RectForKnob();
			mKnobLayer.AutoresizinMask = CAAutoresizingMask.WidthSizable;
			mKnobLayer.BackgroundColor = kKnobBackgroundColor.CGColor;
			mKnobLayer.ShadowColor = NSColor.Black.CGColor;
			mKnobLayer.ShadowOffset = new CGSize(0,-2.0f);
			mKnobLayer.ShadowRadius = 1.0f;
			mKnobLayer.ShadowOpacity = 0.3f;
			mRootLayer.AddSublayer(mKnobLayer);

			mKnobInsideLayer = CALayer.Create();
			mKnobInsideLayer.Frame = mKnobLayer.Bounds;
			mKnobInsideLayer.AutoresizinMask = CAAutoresizingMask.WidthSizable | CAAutoresizingMask.HeightSizable;
			mKnobInsideLayer.ShadowColor = NSColor.Black.CGColor;
			mKnobInsideLayer.ShadowOffset = new CGSize(0,0);
			mKnobInsideLayer.BackgroundColor = NSColor.White.CGColor;
			mKnobInsideLayer.ShadowRadius = 1.0f;
			mKnobInsideLayer.ShadowOpacity = 0.35f;
			mKnobLayer.AddSublayer(mKnobInsideLayer);

			// Initial
			ReloadLayerSize();
			ReloadLayer();
		}

		/// <summary>
		/// Acceptses the first mouse.
		/// </summary>
		/// <returns><c>true</c>, if first mouse was acceptsed, <c>false</c> otherwise.</returns>
		/// <param name="theEvent">The event.</param>
		public override Boolean AcceptsFirstMouse(NSEvent theEvent) {

			return true;
		}

		/// <summary>
		/// Draws the focus ring mask.
		/// </summary>
		public override void DrawFocusRingMask() 
		{
			var cornerRadius = this.Bounds.Height/2.0f;
			var path = NSBezierPath.FromRoundedRect(this.Bounds,cornerRadius,cornerRadius);
			NSColor.Black.Set();
			path.Fill();
		}

		/// <summary>
		/// Reloads the layer.
		/// </summary>
		private void ReloadLayer() 
		{
			CATransaction.Begin();
			CATransaction.AnimationDuration = kAnimationDuration;

			// ------------------------------- Animate Colors
			if ((mDragged && mDraggingTowardsOn) || (!mDragged && mOn)) {
				mBackgroundLayer.BorderColor = this.TintColor.CGColor;
				mBackgroundLayer.BackgroundColor = this.TintColor.CGColor;
			} else {
				mBackgroundLayer.BorderColor = kDisabledBorderColor.CGColor;
				mBackgroundLayer.BackgroundColor = kDisabledBackgroundColor.CGColor;
			}
			// ------------------------------- Animate Enabled-Disabled state
			mRootLayer.Opacity = (Enabled) ? kEnabledOpacity : kDisabledOpacity;

			// ------------------------------- Animate Frame
			if (!mDragged)
			{
				var function = CAMediaTimingFunction.FromControlPoints(0.25f,1.5f,0.5f,1.0f);
				CATransaction.AnimationTimingFunction = function;
			}

			mKnobLayer.Frame = RectForKnob();
			mKnobInsideLayer.Frame = mKnobLayer.Bounds;

			CATransaction.Commit();

		}

		/// <summary>
		/// Reloads the size of the layer.
		/// </summary>
		private void ReloadLayerSize() 
		{
			CATransaction.Begin();
			CATransaction.DisableActions = true;

			mKnobLayer.Frame = RectForKnob();
			mKnobInsideLayer.Frame = mKnobLayer.Bounds;

			mBackgroundLayer.CornerRadius = mBackgroundLayer.Bounds.Height / 2.0f;
			mKnobLayer.CornerRadius = mKnobLayer.Bounds.Height / 2.0f;
			mKnobInsideLayer.CornerRadius = mKnobLayer.Bounds.Height / 2.0f;

			CATransaction.Commit();
		}

		/// <summary>
		/// Knobs the size of the height for.
		/// </summary>
		/// <returns>The height for size.</returns>
		/// <param name="size">Size.</param>
		private nfloat KnobHeightForSize(CGSize size) {

			return size.Height - (kBorderLineWidth * 2.0f);
		}

		/// <summary>
		/// kBorderLineWidth
		/// </summary>
		/// <returns>The for knob.</returns>
		private CGRect RectForKnob() 
		{
			var height = KnobHeightForSize(mBackgroundLayer.Bounds.Size);

			var width = (!this.mIsActive) ? (mBackgroundLayer.Bounds.Width - 2.0f * kBorderLineWidth) * 1.0f / kGoldenRatio :
				(mBackgroundLayer.Bounds.Width - 2.0f * kBorderLineWidth) * 1.0f / kDecreasedGoldenRatio;

			var x = ((!mDragged && !mOn) || (mDragged && !mDraggingTowardsOn)) 
				? kBorderLineWidth : mBackgroundLayer.Bounds.Width - width - kBorderLineWidth;

			return new CGRect(x,kBorderLineWidth, width,height);
		}

		/// <summary>
		/// Acceptses the first responder.
		/// </summary>
		/// <returns><c>true</c>, if first responder was acceptsed, <c>false</c> otherwise.</returns>
		public override Boolean AcceptsFirstResponder() {
			return NSApplication.SharedApplication.FullKeyboardAccessEnabled;
		}

		/// <summary>
		/// Mouses down.
		/// </summary>
		/// <param name="theEvent">The event.</param>
		public override void MouseDown(NSEvent theEvent) {

			if (!Enabled)
				return;


			mIsActive = true;

			this.ReloadLayer();
		}

		/// <summary>
		/// Mouses the dragged.
		/// </summary>
		/// <param name="theEvent">The event.</param>
		public override void MouseDragged(NSEvent theEvent) 
		{
			if (!Enabled)
				return;

			mDragged = true;

			var draggingPoint =  this.ConvertPointFromView(theEvent.LocationInWindow, null);
			mDraggingTowardsOn = draggingPoint.X >= this.Bounds.Width / 2.0f;

			this.ReloadLayer();
		}

		/// <summary>
		/// Mouses up.
		/// </summary>
		/// <param name="theEvent">The event.</param>
		public override void MouseUp(NSEvent theEvent) 
		{
			if (!Enabled)
				return;


			mIsActive = false;

			var isOn =  (!mDragged) ? !IsOn : mDraggingTowardsOn;
			var invokeTargetAction = (isOn != mOn);

			mOn = isOn;

			if (invokeTargetAction)
				InvokedChangeDetection();

			mDragged = false;
			mDraggingTowardsOn = false;

			this.ReloadLayer();

		}

		/// <summary>
		/// Moves the left.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void MoveLeft(Object sender) 
		{
			if (mOn)
			{
				mOn = false;

				InvokedChangeDetection();
			}
		}

		/// <summary>
		/// Moves the right.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void MoveRight(Object sender) 
		{
			if (mOn == false)
			{
				mOn = true;

				InvokedChangeDetection();
			}
		}

		public override Boolean PerformKeyEquivalent(NSEvent theEvent) {

			var handledKeyEquivalent = false;

			if (this.Window.FirstResponder == this)
			{
				var ch = theEvent.KeyCode;

				if (ch == 49)
				{
					mOn = !this.mOn;

					InvokedChangeDetection();
					handledKeyEquivalent = true;
				}
			}

			return handledKeyEquivalent;

		}


		private void InvokedChangeDetection() 
		{

			if (OnSwitchChanged != null)
			{
				OnSwitchChanged(this,EventArgs.Empty);
			}
//			else if (this.Target != null && this.Action != null)
//			{
//				var at = this.Target.Class;
//
//
//				NSMethodSignature *signature = [[self.target class] instanceMethodSignatureForSelector:self.action];
//				NSInvocation *invocation = [NSInvocation invocationWithMethodSignature:signature];
//				[invocation setTarget:self.target];
//				[invocation setSelector:self.action];
//				[invocation setArgument:(void *)&self atIndex:2];
//
//				[invocation invoke];
//			}
//				

			
//			if (this.Target != null && this.Action != null)
//			{
//				//NSMethodSignature
//				// 
//				//     if (self.target && self.action) {

//
//			}

		}
			
		#endregion
	}
}

