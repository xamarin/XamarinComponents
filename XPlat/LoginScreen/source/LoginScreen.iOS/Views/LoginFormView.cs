using System;
using System.Drawing;

#if __UNIFIED__
using CoreGraphics;
using Foundation;
using UIKit;
#else
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;
#endif

using LoginScreen.Utils;
using System.Collections.Generic;

namespace LoginScreen.Views
{
	class LoginFormView : BaseFormView
	{
		readonly CustomTextField userNameField;
		readonly CustomTextField passwordField;
		readonly UIImageView userNameIconView;
		readonly UIImageView passwordIconView;
		readonly UIButton loginButton;
		readonly UIButton forgotPasswordButton;
		readonly UIImageView arrowView;
		readonly UIImageView leftDividerView;
		readonly UIImageView rightDividerView;
		readonly UILabel orLabel;
		readonly UILabel registerSuggesionLabel;
		readonly UIButton registerButton;
		readonly bool showForgotPassword;
		readonly bool showRegister;

		public LoginFormView (bool showForgotPassword, bool showRegister)
		{
			this.showForgotPassword = showForgotPassword;
			this.showRegister = showRegister;

			userNameField = CreateField ();
			userNameField.AutocapitalizationType = UITextAutocapitalizationType.None;
			userNameField.TextInset = new UIEdgeInsets (6, 10, 6, 30);

			passwordField = CreateField ();
			passwordField.SecureTextEntry = true;
			passwordField.TextInset = new UIEdgeInsets (6, 10, 6, 30);

			ApplyFieldsNavigation (() => OnLogInClick (EventArgs.Empty), userNameField, passwordField);

			userNameIconView = new UIImageView (UIImage.FromFile ("Images/username-icon.png"));
			passwordIconView = new UIImageView (UIImage.FromFile ("Images/password-icon.png"));

			loginButton = CreateButton ();
			loginButton.SetBackgroundImage (UIImage.FromFile ("Images/login-button.png").StretchableImage (5, 0), UIControlState.Normal);
			loginButton.TouchUpInside += (sender, e) => OnLogInClick (EventArgs.Empty);

			forgotPasswordButton = new UIButton
			{
				ExclusiveTouch = true,
				Font = Fonts.HelveticaNeueMedium (13),
				TitleShadowOffset = new SizeF (0f, 1f)
			};
			forgotPasswordButton.SetTitleColor (UIColor.FromRGB (65, 100, 140), UIControlState.Normal);
			forgotPasswordButton.SetTitleShadowColor (UIColor.White.ColorWithAlpha (0.7f), UIControlState.Normal);
			forgotPasswordButton.TouchUpInside += (sender, e) => OnForgotPasswordClick (EventArgs.Empty);
			arrowView = new UIImageView (UIImage.FromFile ("Images/arrow-right.png"));

			orLabel = CreateLabel ();
			registerSuggesionLabel = CreateLabel ();

			var divider = UIImage.FromFile ("Images/divider-line.png");
			leftDividerView = new UIImageView (divider);
			rightDividerView = new UIImageView (divider);

			registerButton = CreateButton ();
			registerButton.SetBackgroundImage (UIImage.FromFile ("Images/register-button.png").StretchableImage (5, 0), UIControlState.Normal);
			registerButton.TouchUpInside += (sender, e) => OnRegisterClick (EventArgs.Empty);

			List <UIView> subViews = new List<UIView> (new UIView [] {userNameField, passwordField, userNameIconView, passwordIconView, loginButton});

			if (this.showForgotPassword)
				subViews.AddRange (new UIView [] {forgotPasswordButton, arrowView});

			if (this.showRegister)
				subViews.AddRange (new UIView [] {leftDividerView, rightDividerView, orLabel, registerSuggesionLabel, registerButton});

			AddSubviews (subViews.ToArray ());

			BackButtonHidden = true;

			int boundsHeight = 331;
			if (!this.showForgotPassword)
				boundsHeight -= 33;
			if (!this.showRegister)
				boundsHeight -= 101;

			Bounds = new CGRect (0, 0, 299, boundsHeight);
		}

		public event EventHandler LogInClick;
		public event EventHandler ForgotPasswordClick;
		public event EventHandler RegisterClick;

		public string UserName {
			get { return userNameField.Text; }
		}

		public string Password {
			get { return passwordField.Text; }
		}

		public string UserNameFieldPlaceHolder {
			get { return userNameField.Placeholder; }
			set { userNameField.Placeholder = value; }
		}

		public string PasswordFieldPlaceHolder {
			get { return passwordField.Placeholder; }
			set { passwordField.Placeholder = value; }
		}

		public string LogInButtonTitle {
			get { return loginButton.Title (UIControlState.Normal); }
			set { loginButton.SetTitle (value, UIControlState.Normal); }
		}

		public string ForgotPasswordButtonTitle {
			get { return forgotPasswordButton.Title (UIControlState.Normal); }
			set { forgotPasswordButton.SetTitle (value, UIControlState.Normal); }
		}

		public string OrLabelText {
			get { return orLabel.Text; }
			set { orLabel.Text = value; }
		}

		public string RegisterSuggesionLabelText {
			get { return registerSuggesionLabel.Text; }
			set { registerSuggesionLabel.Text = value; }
		}

		public string RegisterButtonTitle {
			get { return registerButton.Title (UIControlState.Normal); }
			set { registerButton.SetTitle (value, UIControlState.Normal); }
		}

		public void ShowBubbleForUserName (string text)
		{
			ShowBubbleFor (userNameField, text);
		}

		public void ShowBubbleForPassword (string text)
		{
			ShowBubbleFor (passwordField, text);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			userNameField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 60, Bounds.Width - 30, 32);
			userNameIconView.Frame = new CGRect (userNameField.Frame.Right - 28, userNameField.Frame.Y + 8, 16, 15);

			passwordField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 99, Bounds.Width - 30, 32);
			passwordIconView.Frame = new CGRect (passwordField.Frame.Right - 28, passwordField.Frame.Y + 8, 16, 15);

			loginButton.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 146, Bounds.Width - 30, 30);

			forgotPasswordButton.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 180, forgotPasswordButton.SizeThatFits (new CGSize (Bounds.Width - 30, 0)).Width, 33);
			arrowView.Frame = new CGRect (forgotPasswordButton.Frame.Right + 5, forgotPasswordButton.Frame.Top + 10, 8, 13);

			int orLabelYOffset = 230;
			if (!showForgotPassword)
				orLabelYOffset -= 33;

			orLabel.Bounds = new CGRect (0, 0, orLabel.SizeThatFits (new CGSize (Bounds.Width - 56, 0)).Width, 20);
			orLabel.Center = new CGPoint (Bounds.GetMidX (), Bounds.Y + orLabelYOffset);
			leftDividerView.Frame = CGRect.FromLTRB (Bounds.X + 15, orLabel.Center.Y + 1, orLabel.Frame.Left - 13, orLabel.Center.Y + 3);
			rightDividerView.Frame = CGRect.FromLTRB (orLabel.Frame.Right + 13, orLabel.Center.Y + 1, Bounds.Right - 15, orLabel.Center.Y + 3);

			registerSuggesionLabel.Frame = new CGRect (Bounds.X + 15, Bounds.Y + orLabelYOffset + 12, Bounds.Width - 30, 20);

			registerButton.Frame = new CGRect (Bounds.X + 15, Bounds.Y + orLabelYOffset + 42, Bounds.Width - 30, 30); //331
		}

		protected virtual void OnLogInClick (EventArgs e)
		{
			var handler = LogInClick;
			if (handler != null)
				handler (this, e);
		}

		protected virtual void OnForgotPasswordClick (EventArgs e)
		{
			var handler = ForgotPasswordClick;
			if (handler != null)
				handler (this, e);
		}

		protected virtual void OnRegisterClick (EventArgs e)
		{
			var handler = RegisterClick;
			if (handler != null)
				handler (this, e);
		}

		private UILabel CreateLabel ()
		{
			return new UILabel ()
			{
				BackgroundColor = UIColor.Clear,
				Font = Fonts.HelveticaNeueMedium (13),
				TextColor = UIColor.FromRGB (65, 100, 140),
				ShadowColor = UIColor.White.ColorWithAlpha (0.7f),
				ShadowOffset = new SizeF (0f, 1f)
			};
		}
	}
}

