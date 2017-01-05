using System;
using System.Drawing;

#if __UNIFIED__
using UIKit;
using Foundation;
using CoreGraphics;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using CGRect = global::System.Drawing.RectangleF;
#endif

namespace LoginScreen.Views
{
	class RegistrationFormView : BaseFormView
	{
		CustomTextField emailField;
		CustomTextField userNameField;
		CustomTextField passwordField;
		CustomTextField passwordConfirmationField;
		UIButton registerButton;

		public RegistrationFormView ()
		{
			emailField = CreateField ();
			emailField.KeyboardType = UIKeyboardType.EmailAddress;
			emailField.AutocapitalizationType = UITextAutocapitalizationType.None;

			userNameField = CreateField ();
			userNameField.AutocapitalizationType = UITextAutocapitalizationType.None;

			passwordField = CreateField ();
			passwordField.SecureTextEntry = true;

			passwordConfirmationField = CreateField ();
			passwordConfirmationField.SecureTextEntry = true;

			ApplyFieldsNavigation (() => OnRegisterClick (EventArgs.Empty), emailField, userNameField, passwordField, passwordConfirmationField);

			registerButton = CreateButton ();
			registerButton.SetBackgroundImage (UIImage.FromFile ("Images/register-button.png").StretchableImage (5, 0), UIControlState.Normal);
			registerButton.TouchUpInside += (sender, e) => OnRegisterClick (EventArgs.Empty);

			AddSubviews (emailField, userNameField, passwordField, passwordConfirmationField, registerButton);

			Bounds = new RectangleF (0, 0, 299, 281);
		}

		public event EventHandler RegisterClick;

		public string Email {
			get { return emailField.Text; }
		}

		public string UserName {
			get { return userNameField.Text; }
		}

		public string Password {
			get { return passwordField.Text; }
		}

		public string PasswordConfirmation {
			get { return passwordConfirmationField.Text; }
		}

		public string EmailFieldPlaceHolder {
			get { return emailField.Placeholder; }
			set { emailField.Placeholder = value; }
		}

		public string UserNameFieldPlaceHolder {
			get { return userNameField.Placeholder; }
			set { userNameField.Placeholder = value; }
		}

		public string PasswordFieldPlaceHolder {
			get { return passwordField.Placeholder; }
			set { passwordField.Placeholder = value; }
		}

		public string PasswordConfirmationFieldPlaceHolder {
			get { return passwordConfirmationField.Placeholder; }
			set { passwordConfirmationField.Placeholder = value; }
		}

		public string RegisterButtonTitle {
			get { return registerButton.Title (UIControlState.Normal); }
			set { registerButton.SetTitle (value, UIControlState.Normal); }
		}

		public void ShowBubbleForEmail (string text)
		{
			ShowBubbleFor (emailField, text);
		}

		public void ShowBubbleForUserName (string text)
		{
			ShowBubbleFor (userNameField, text);
		}

		public void ShowBubbleForPasswordConfirmation (string text)
		{
			ShowBubbleFor (passwordConfirmationField, text);
		}

		public void ShowBubbleForPassword (string text)
		{
			ShowBubbleFor (passwordField, text);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			emailField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 60, Bounds.Width - 30, 32);
			userNameField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 100, Bounds.Width - 30, 32);
			passwordField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 140, Bounds.Width - 30, 32);
			passwordConfirmationField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 180, Bounds.Width - 30, 32);
			registerButton.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 220, Bounds.Width - 30, 30);
		}

		protected virtual void OnRegisterClick (EventArgs e)
		{
			var handler = RegisterClick;
			if (handler != null)
				handler (this, e);
		}
	}
}

