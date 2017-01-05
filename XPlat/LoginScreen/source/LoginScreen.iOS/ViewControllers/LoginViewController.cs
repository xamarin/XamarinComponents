using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using LoginScreen.Views;

namespace LoginScreen.ViewControllers
{
	class LoginViewController : BaseViewController
	{
		readonly ICredentialsProvider credentialsProvider;
		readonly ILoginScreenMessages messages;
		readonly ResetPasswordViewController resetPasswordScreen;
		readonly RegistrationViewController registrationScreen;
		Action completionHandler;

		public LoginViewController (ICredentialsProvider credentialsProvider, ILoginScreenMessages messages)
			: base (messages.LoginFormTitle)
		{
			this.credentialsProvider = credentialsProvider;
			this.messages = messages;
			this.resetPasswordScreen = new ResetPasswordViewController (credentialsProvider, messages);
			this.registrationScreen = new RegistrationViewController (credentialsProvider, messages);
		}

		public void SetCompletionHandler (Action completionHandler)
		{
			this.completionHandler = completionHandler;
			registrationScreen.SetCompletionHandler (completionHandler);
		}

		protected override BaseFormView CreateFormView ()
		{
			var form = new LoginFormView (credentialsProvider.ShowPasswordResetLink, credentialsProvider.ShowRegistration)
			{
				Title = messages.LoginFormTitle,
				UserNameFieldPlaceHolder = messages.UserNameFieldPlaceHolder,
				PasswordFieldPlaceHolder = messages.PasswordFieldPlaceHolder,
				LogInButtonTitle = messages.LogInButtonTitle,
				ForgotPasswordButtonTitle = messages.ForgotPasswordButtonTitle,
				OrLabelText = messages.OrLabelText,
				RegisterSuggesionLabelText = messages.RegisterSuggesionLabelText,
				RegisterButtonTitle = messages.RegisterButtonTitle
			};
			
			form.LogInClick += LogInClick;
			form.ForgotPasswordClick += ForgotPasswordClick;
			form.RegisterClick += RegisterClick;

			return form;
		}

		void LogInClick (object sender, EventArgs e)
		{
			View.EndEditing (true);

			var form = sender as LoginFormView;
			if (form != null) {
				bool valid = true;

				if (String.IsNullOrEmpty (form.UserName)) {
					form.ShowBubbleForUserName (messages.ErrorMessageUserNameRequired);
					valid = false;
				}
				if (String.IsNullOrEmpty (form.Password)) {
					form.ShowBubbleForPassword (messages.ErrorMessagePasswordRequired);
					valid = false;
				}

				if (valid) {
					StartActivityAnimation (messages.LoginWaitingMessage);
					credentialsProvider.Login (form.UserName, form.Password, LoggedIn, x => Failed (form, x));
				}
			}
		}

		void LoggedIn ()
		{
			InvokeOnMainThread (() => {
				StopActivityAnimation ();
				completionHandler.Invoke ();
			});
		}

		void Failed (LoginFormView form, LoginScreenFaultDetails details)
		{
			InvokeOnMainThread (() => {
				StopActivityAnimation ();
				if (!String.IsNullOrEmpty (details.UserNameErrorMessage)) {
					form.ShowBubbleForUserName (details.UserNameErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.PasswordErrorMessage)) {
					form.ShowBubbleForPassword (details.PasswordErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.CommonErrorMessage)) {
					ShowAlert (messages.LoginCommonErrorTitle, details.CommonErrorMessage, messages.AlertCancelButtonTitle);
				}
			});
		}

		void ForgotPasswordClick (object sender, EventArgs e)
		{
			EnsureNavigationController ();
			NavigationController.PushViewController (resetPasswordScreen, true);
		}

		void RegisterClick (object sender, EventArgs e)
		{
			EnsureNavigationController ();
			NavigationController.PushViewController (registrationScreen, true);
		}

		void EnsureNavigationController ()
		{
			if (NavigationController == null) {
				throw new InvalidOperationException ("NavigationController is required.");
			}
		}
	}
}

