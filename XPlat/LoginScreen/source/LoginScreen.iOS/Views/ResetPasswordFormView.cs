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
	class ResetPasswordFormView : BaseFormView
	{
		CustomTextField emailField;
		UIButton resetButton;

		public ResetPasswordFormView ()
		{
			emailField = CreateField ();
			emailField.KeyboardType = UIKeyboardType.EmailAddress;
			emailField.AutocapitalizationType = UITextAutocapitalizationType.None;

			ApplyFieldsNavigation (() => OnResetClick (EventArgs.Empty), emailField);

			resetButton = CreateButton ();
			resetButton.SetBackgroundImage (UIImage.FromFile ("Images/reset-button.png").StretchableImage (5, 0), UIControlState.Normal);
			resetButton.TouchUpInside += (sender, e) => OnResetClick (EventArgs.Empty);

			AddSubviews (emailField, resetButton);

			Bounds = new RectangleF (0, 0, 299, 162);
		}

		public event EventHandler ResetClick;

		public string Email {
			get { return emailField.Text; }
		}

		public string EmailFieldPlaceHolder {
			get { return emailField.Placeholder; }
			set { emailField.Placeholder = value; }
		}

		public string ResetButtonTitle {
			get { return resetButton.Title (UIControlState.Normal); }
			set { resetButton.SetTitle (value, UIControlState.Normal); }
		}

		public void ShowBubbleForEmail (string text)
		{
			ShowBubbleFor (emailField, text);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			emailField.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 60, Bounds.Width - 30, 32);
			resetButton.Frame = new CGRect (Bounds.X + 15, Bounds.Y + 100, Bounds.Width - 30, 30);
		}

		protected virtual void OnResetClick (EventArgs e)
		{
			var handler = ResetClick;
			if (handler != null)
				handler (this, e);
		}
	}
}

