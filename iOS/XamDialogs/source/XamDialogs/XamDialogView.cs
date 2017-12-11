using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using CoreGraphics;

namespace XamDialogs
{
	public abstract class XamDialogView : UIView
	{

		#region Fields

		private String _Title;

		private String _Message;

		private String _SubmitButtonText;

		private String _CancelButtonText;

		private XamDialogType _BoxType;

		private UIBlurEffectStyle _BlurEffectStyle = UIBlurEffectStyle.Light;

		private UIVisualEffectView _VisualEffectView;

		private UIView _ActualBox;

		private NSObject mKeyboardDidSHowNotification;
		private NSObject mKeyboardDidHideNotification;

		private UIColor mTitleLabelColor;
		private UIColor mButtonsLabelColor;
		private UIColor mMessageLabelColor;

		private UIView mBackingView;
		private UIColor mBackingColor;
		#endregion

		#region Properties

		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected abstract UIView ContentView {get;}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title {
			get {
				return this._Title;
			}
			set {
				this._Title = value;
			}
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message {
			get {
				return this._Message;
			}
			set {
				this._Message = value;
			}
		}

		/// <summary>
		/// Gets or sets the submit button text.
		/// </summary>
		/// <value>The submit button text.</value>
		public String SubmitButtonText {
			get {
				return this._SubmitButtonText;
			}
			set {
				this._SubmitButtonText = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance cancel button text.
		/// </summary>
		/// <value><c>true</c> if this instance cancel button text; otherwise, <c>false</c>.</value>
		public String CancelButtonText {
			get {
				return this._CancelButtonText;
			}
			set {
				this._CancelButtonText = value;
			}
		}

		/// <summary>
		/// Gets the type of the box.
		/// </summary>
		/// <value>The type of the box.</value>
		public XamDialogType BoxType {
			get {
				return this._BoxType;
			}
			private set {
				this._BoxType = value;
			}
		}
			
		/// <summary>
		/// Gets or sets the blur effect style.
		/// </summary>
		/// <value>The blur effect style.</value>
		public UIBlurEffectStyle BlurEffectStyle {
			get {
				return this._BlurEffectStyle;
			}
			set {
				this._BlurEffectStyle = value;
			}
		}
			
		private UIVisualEffectView VisualEffectView {
			get {
				return this._VisualEffectView;
			}
			set {
				this._VisualEffectView = value;
			}
		}
			
		private UIView ActualBox {
			get {
				return this._ActualBox;
			}
			set {
				this._ActualBox = value;
			}
		}
			
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="XamDialogs.DHDialogView"/> constantly update any value changes
		/// </summary>
		/// <value><c>true</c> if constant updates; otherwise, <c>false</c>.</value>
		public bool ConstantUpdates {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the button mode.
		/// </summary>
		/// <value>The button mode.</value>
		public ButtonMode ButtonMode {
			get;
			set;
		}

		/// <summary>
		/// Occurs when on cancel.
		/// </summary>
		public event EventHandler OnCancel = delegate {};

		/// <summary>
		/// Gets or sets the color of the title label.
		/// </summary>
		/// <value>The color of the title label.</value>
		public UIColor TitleLabelTextColor {
			get 
			{
				if (mTitleLabelColor == null)
					return (this.BlurEffectStyle == UIBlurEffectStyle.Dark) ? UIColor.White : UIColor.Black;
				
				return mTitleLabelColor; 
			}
			set 
			{
				
				mTitleLabelColor = value; }
		}

		public UIColor ButtonsTextColor
		{
			get
			{
				if (mButtonsLabelColor == null)
					return TitleLabelTextColor;

				return mButtonsLabelColor;
			}
			set
			{

				mButtonsLabelColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the message label text.
		/// </summary>
		/// <value>The color of the message label text.</value>
		public UIColor MessageLabelTextColor {
			get 
			{
				if (mMessageLabelColor == null)
					return (this.BlurEffectStyle == UIBlurEffectStyle.Dark) ? UIColor.White : UIColor.Black;

				return mMessageLabelColor; 
			}
			set 
			{

				mMessageLabelColor = value; }
		}
			
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="XamDialogs.DHDialogView"/> disable background overlay.
		/// </summary>
		/// <value><c>true</c> if disable background overlay; otherwise, <c>false</c>.</value>
		public bool DisableBackgroundOverlay {
			get;
			set;
		}
			
		/// <summary>
		/// Gets or sets the color of the background overlay.
		/// </summary>
		/// <value>The color of the background overlay.</value>
		public UIColor BackgroundOverlayColor {
			get 
			{
				if (mBackingColor == null)
					return UIColor.FromWhiteAlpha (0.1f, 0.5f);
				return mBackingColor; 
			}
			set { mBackingColor = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MKInputBoxView.MKInputBoxView"/> class.
		/// </summary>
		/// <param name="boxType">Box type.</param>
		public XamDialogView (XamDialogType boxType)
			: base ()
		{
			var actualBoxHeight = 155.0f;
			var window = UIApplication.SharedApplication.Windows[UIApplication.SharedApplication.Windows.Length - 1];

			var allFrame = window.Frame;

			var boxFrame = new CGRect (0, 0, Math.Min (325, window.Frame.Size.Width - 50), actualBoxHeight);

			this.Frame = allFrame;

			this.BoxType = boxType;
			this.BackgroundColor = UIColor.FromWhiteAlpha (1.0f, 0.0f);
			this.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleHeight;
			this.ActualBox = new UIView (boxFrame);

			this.ActualBox.Center = new CGPoint (window.Center.X, window.Center.Y);

			this.Center = new CGPoint (window.Center.X, window.Center.Y);

			this.Add (this.ActualBox);

		}

		#endregion

		#region Methods


		/// <summary>
		/// Show the dialog on the specified view
		/// </summary>
		private void Show (UIView aView)
		{
                
			this.Alpha = 0.0f;

			SetupView ();

			UIView.Animate (0.3f, () => {
				this.Alpha = 1.0f;
			});

			aView.Add (this);
			aView.BringSubviewToFront (this);

			UIDevice.CurrentDevice.BeginGeneratingDeviceOrientationNotifications ();


			NSNotificationCenter.DefaultCenter.AddObserver (new NSString ("UIDeviceOrientationDidChangeNotification"), DeviceOrientationDidChange);

			mKeyboardDidSHowNotification = UIKeyboard.Notifications.ObserveDidShow ((s, e) => {
				KeyboardDidShow (e.Notification);
			});


			mKeyboardDidHideNotification = UIKeyboard.Notifications.ObserveDidHide ((s, e) => {
				KeyboardDidHide (e.Notification);

			});

		}

		/// <summary>
		/// Show the specified aView.
		/// </summary>
		/// <param name="aView">A view.</param>
		public void Show (UIViewController viewContoller)
		{
            Show (viewContoller.View);
		}

        [Obsolete("Use Show(UIViewController) instead. Will not work on iOS 11 and later")]
		/// <summary>
		/// Show this instance.
		/// </summary>
		public void Show ()
		{
			var window = UIApplication.SharedApplication.Windows[UIApplication.SharedApplication.Windows.Length - 1];

			/* If the topmost window is "UIRemoteKeyboardWindow" (happens e.g. on iOS 10.0 simulator), the dialog would be invisible (but still clickable).
			To prevent this, switch to KeyWindow and the dialog will be ok (visible and clickable). */
			if (!string.IsNullOrEmpty(window.Description) && window.Description.Contains("UIRemoteKeyboardWindow"))
				window = UIApplication.SharedApplication.KeyWindow;
		
			Show (window);
		}

		/// <summary>
		/// Hide this instance.
		/// </summary>
		public void Hide ()
		{
			UIView.AnimateNotify (0.3f, () => {
				this.Alpha = 0.0f;
			}, (finished) => {
				this.RemoveFromSuperview ();

				UIDevice.CurrentDevice.EndGeneratingDeviceOrientationNotifications ();

				NSNotificationCenter.DefaultCenter.RemoveObserver (new NSString ("UIDeviceOrientationDidChangeNotification"));

				mKeyboardDidSHowNotification.Dispose ();
				mKeyboardDidHideNotification.Dispose ();
			});

		}

		/// <summary>
		/// Setups the view.
		/// </summary>
		private void SetupView ()
		{
			
			// 
			this.ActualBox.Layer.CornerRadius = 4.0f;
			this.ActualBox.Layer.MasksToBounds = true;

			// 
			UIColor messageLabelTextColor = null;
			UIColor elementBackgroundColor = null;
			UIColor buttonBackgroundColor = null;

			// 
			var style = this.BlurEffectStyle;

			// 
			switch (style) {
			case UIBlurEffectStyle.Dark:
				{
					messageLabelTextColor = UIColor.White;
					elementBackgroundColor = UIColor.FromWhiteAlpha (1.0f, 0.07f);
					buttonBackgroundColor = UIColor.FromWhiteAlpha (1.0f, 0.07f);
				}
				break;
			default:
				{
					messageLabelTextColor = UIColor.Black;
					elementBackgroundColor = UIColor.FromWhiteAlpha (1.0f, 0.50f);
					buttonBackgroundColor = UIColor.FromWhiteAlpha (1.0f, 0.2f);
				}

				break;
			}


			this.VisualEffectView = new UIVisualEffectView (UIBlurEffect.FromStyle (style));

			var padding = 10.0f;
			var width = this.ActualBox.Frame.Size.Width - padding * 2;


			UILabel titleLabel = new UILabel (new CGRect (padding, padding, width, 20));

			titleLabel.Font = UIFont.BoldSystemFontOfSize (17.0f);
			titleLabel.Text = this.Title;
			titleLabel.TextAlignment = UITextAlignment.Center;
			titleLabel.TextColor = TitleLabelTextColor;


			this.VisualEffectView.ContentView.Add (titleLabel);

			var messageLabel = new UILabel (new CGRect (padding, padding + titleLabel.Frame.Size.Height + 5, width, 31.5));
			messageLabel.Lines = 2;
			messageLabel.Font = UIFont.SystemFontOfSize (13.0f);
			messageLabel.Text = this.Message;
			messageLabel.TextAlignment = UITextAlignment.Center;
			messageLabel.TextColor = messageLabelTextColor;
			messageLabel.SizeToFit ();

			//center the frame
			var mFrame = messageLabel.Frame;
			mFrame.Width = width; 
			messageLabel.Frame = mFrame;

			this.VisualEffectView.ContentView.Add (messageLabel);

			var aView = this.ContentView;

			var conFrame = aView.Frame;
			conFrame.Y = messageLabel.Frame.Y + messageLabel.Frame.Height + padding / 1.5f;
			conFrame.X = (this.ActualBox.Frame.Size.Width / 2) - (conFrame.Width / 2);
			aView.Frame = conFrame;

			var buttonHeight = 40.0f;
			var aPos = messageLabel.Frame.Bottom + buttonHeight;

			CGRect extendedFrame = this.ActualBox.Frame;
			extendedFrame.Height = aPos + padding;
			extendedFrame.Height += aView.Frame.Height;
			this.ActualBox.Frame = extendedFrame;

			this.VisualEffectView.ContentView.Add (aView);
			// 
			foreach (UIView element in aView.Subviews) 
			{
				if (element is UITextField) 
				{
					element.Layer.BorderColor = UIColor.FromWhiteAlpha (0.0f, 0.1f).CGColor;
					element.Layer.BorderWidth = 0.5f;
					element.BackgroundColor = elementBackgroundColor;
				}

			}
			// 

			var buttonWidth = this.ActualBox.Frame.Size.Width / 2;


			switch (ButtonMode) 
			{

			case ButtonMode.OkAndCancel:
				{
					// 
					UIButton cancelButton = new UIButton (new CGRect (0, this.ActualBox.Frame.Size.Height - buttonHeight, buttonWidth, buttonHeight));
					cancelButton.SetTitle (!(String.IsNullOrWhiteSpace (this.CancelButtonText)) ? this.CancelButtonText : @"Cancel", UIControlState.Normal);
					cancelButton.TouchUpInside += (object sender, EventArgs e) => {
						CancelButtonTapped ();

					};


					cancelButton.TitleLabel.Font = UIFont.SystemFontOfSize (16.0f);
						cancelButton.SetTitleColor (ButtonsTextColor, UIControlState.Normal);
					cancelButton.SetTitleColor (UIColor.Gray, UIControlState.Highlighted);
					cancelButton.BackgroundColor = buttonBackgroundColor;

					cancelButton.Layer.BorderColor = UIColor.FromWhiteAlpha (0.0f, 0.1f).CGColor;
					cancelButton.Layer.BorderWidth = 0.5f;
					this.VisualEffectView.ContentView.Add (cancelButton);


					// 
					var submitButton = new UIButton (new CGRect (buttonWidth, this.ActualBox.Frame.Size.Height - buttonHeight, buttonWidth, buttonHeight));

					submitButton.SetTitle (!(String.IsNullOrWhiteSpace (this.SubmitButtonText)) ? this.SubmitButtonText : @"OK", UIControlState.Normal);
					submitButton.TouchUpInside += (object sender, EventArgs e) => {
						SubmitButtonTapped ();
					};

					submitButton.TitleLabel.Font = UIFont.SystemFontOfSize (16.0f);

					submitButton.SetTitleColor (ButtonsTextColor, UIControlState.Normal);
					submitButton.SetTitleColor (UIColor.Gray, UIControlState.Highlighted);


					submitButton.BackgroundColor = buttonBackgroundColor;
					submitButton.Layer.BorderColor = UIColor.FromWhiteAlpha (0.0f, 0.1f).CGColor;
					submitButton.Layer.BorderWidth = 0.5f;


					this.VisualEffectView.ContentView.Add (submitButton);
				}
				break;
			case ButtonMode.Ok:
				{
					
					var submitButton = new UIButton (new CGRect (0, this.ActualBox.Frame.Size.Height - buttonHeight, this.ActualBox.Frame.Size.Width, buttonHeight));

					submitButton.SetTitle (!(String.IsNullOrWhiteSpace (this.SubmitButtonText)) ? this.SubmitButtonText : @"OK", UIControlState.Normal);
					submitButton.TouchUpInside += (object sender, EventArgs e) => {
						SubmitButtonTapped ();
					};

					submitButton.TitleLabel.Font = UIFont.SystemFontOfSize (16.0f);

					submitButton.SetTitleColor (ButtonsTextColor, UIControlState.Normal);
					submitButton.SetTitleColor (UIColor.Gray, UIControlState.Highlighted);


					submitButton.BackgroundColor = buttonBackgroundColor;
					submitButton.Layer.BorderColor = UIColor.FromWhiteAlpha (0.0f, 0.1f).CGColor;
					submitButton.Layer.BorderWidth = 0.5f;


					this.VisualEffectView.ContentView.Add (submitButton);

				}
				break;
			case ButtonMode.Cancel:
				{
					UIButton cancelButton = new UIButton (new CGRect (0, this.ActualBox.Frame.Size.Height - buttonHeight, this.ActualBox.Frame.Size.Width, buttonHeight));
					cancelButton.SetTitle (!(String.IsNullOrWhiteSpace (this.CancelButtonText)) ? this.CancelButtonText : @"Cancel", UIControlState.Normal);
					cancelButton.TouchUpInside += (object sender, EventArgs e) => {
						CancelButtonTapped ();

					};


					cancelButton.TitleLabel.Font = UIFont.SystemFontOfSize (16.0f);
					cancelButton.SetTitleColor (ButtonsTextColor, UIControlState.Normal);
					cancelButton.SetTitleColor (UIColor.Gray, UIControlState.Highlighted);
					cancelButton.BackgroundColor = buttonBackgroundColor;

					cancelButton.Layer.BorderColor = UIColor.FromWhiteAlpha (0.0f, 0.1f).CGColor;
					cancelButton.Layer.BorderWidth = 0.5f;
					this.VisualEffectView.ContentView.Add (cancelButton);

				}
				break;

			}


			// 
			this.VisualEffectView.Frame = new CGRect (0, 0, this.ActualBox.Frame.Size.Width, this.ActualBox.Frame.Size.Height + 45);    
			this.ActualBox.Add (this.VisualEffectView);

			this.ActualBox.Center = this.Center;


			var window = UIApplication.SharedApplication.Windows[UIApplication.SharedApplication.Windows.Length - 1];


			mBackingView = new UIView (window.Bounds);

			if (!DisableBackgroundOverlay) 
			{
				mBackingView.BackgroundColor = BackgroundOverlayColor;

				this.InsertSubview (mBackingView, 0);
			}
		}

		/// <summary>
		/// Determines whether this instance cancel button tapped.
		/// </summary>
		/// <returns><c>true</c> if this instance cancel button tapped; otherwise, <c>false</c>.</returns>
		private void CancelButtonTapped ()
		{
			HandleCancel ();

			this.Hide ();

			OnCancel (this, null);
		}

		/// <summary>
		/// Submits the button tapped.
		/// </summary>
		private void SubmitButtonTapped ()
		{
			if (CanSubmit ()) 
			{
				HandleSubmit ();
				Hide ();
			}

		}
			
		/// <summary>
		/// Devices the orientation did change.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void DeviceOrientationDidChange (NSNotification notification)
		{
			ResetFrame (true);

		}

		/// <summary>
		/// Keyboards the did show.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyboardDidShow (NSNotification notification)
		{
			ResetFrame (true);

			UIView.Animate (0.2f, () => {
				CGRect frame = this.ActualBox.Frame;
				frame.Y -= YCorrection ();
				this.ActualBox.Frame = frame;

			});

		}

		/// <summary>
		/// Keyboards the did hide.
		/// </summary>
		/// <param name="notification">Notification.</param>
		private void KeyboardDidHide (NSNotification notification)
		{
			ResetFrame (true);

		}

		/// <summary>
		/// Ys the correction.
		/// </summary>
		/// <returns>The correction.</returns>
		private nfloat YCorrection ()
		{
			var yCorrection = 115.0f;


			if (UIKit.UIDeviceOrientationExtensions.IsLandscape (UIDevice.CurrentDevice.Orientation)) {
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
					yCorrection = 80.0f;
				} else if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
					yCorrection = 100.0f;
				}

				if (this.BoxType == XamDialogType.LoginAndPasswordInput) {
					yCorrection += 45.0f;
				}
			} else {
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad) {
					yCorrection = 0.0f;
				}
			}
			return yCorrection;

		}

		/// <summary>
		/// Resets the frame.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		private void ResetFrame (Boolean animated)
		{

			var window = UIApplication.SharedApplication.Windows[UIApplication.SharedApplication.Windows.Length - 1];

			this.Frame = window.Frame;

			// 
			if (animated) {
				UIView.Animate (0.3f, () => {
					this.Center = new CGPoint (window.Center.X, window.Center.Y);
					this.ActualBox.Center = this.Center;
					mBackingView.Frame = this.Bounds;
				});

			} else {
				this.Center = new CGPoint (window.Center.X, window.Center.Y);
				this.ActualBox.Center = this.Center;
				mBackingView.Frame = this.Bounds;
			}

		}

		/// <summary>
		/// Displaies the N decimal.
		/// </summary>
		/// <returns>The N decimal.</returns>
		/// <param name="dbValue">Db value.</param>
		/// <param name="nDecimal">N decimal.</param>
		private string DisplayNDecimal (double dbValue, int nDecimal)
		{
			string decimalPoints = "0";
			if (nDecimal > 0) {
				decimalPoints += ".";
				for (int i = 0; i < nDecimal; i++)
					decimalPoints += "0";
			}
			return dbValue.ToString (decimalPoints);
		}

		protected abstract bool CanSubmit ();

		protected abstract void HandleCancel ();

		protected abstract void HandleSubmit ();




	
		#endregion

		#region Static Methods

		/// <summary>
		/// Boxs the type of the of.
		/// </summary>
		/// <returns>The of type.</returns>
		/// <param name="boxType">Box type.</param>
		public static XamDialogView BoxOfType (XamDialogType boxType)
		{
			return null;
		}

		#endregion
	}
}

