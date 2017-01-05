using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using LoginScreen.Views;

namespace LoginScreen.ViewControllers
{
	class RegistrationViewController : BaseViewController
	{
		readonly ICredentialsProvider credentialsProvider;
		readonly ILoginScreenMessages messages;
		Action completionHandler;

		public RegistrationViewController (ICredentialsProvider credentialsProvider, ILoginScreenMessages messages)
			: base (messages.RegistrationFormTitle)
		{
			this.credentialsProvider = credentialsProvider;
			this.messages = messages;
		}

		public void SetCompletionHandler (Action completionHandler)
		{
			this.completionHandler = completionHandler;
		}

		protected override BaseFormView CreateFormView ()
		{
			var form = new RegistrationFormView
			{
				Title = messages.RegistrationFormTitle,
				EmailFieldPlaceHolder = messages.EmailFieldPlaceHolder,
				UserNameFieldPlaceHolder = messages.UserNameFieldPlaceHolder,
				PasswordFieldPlaceHolder = messages.PasswordFieldPlaceHolder,
				PasswordConfirmationFieldPlaceHolder = messages.PasswordConfirmationFieldPlaceHolder,
				RegisterButtonTitle = messages.RegisterButtonTitle
			};

			form.RegisterClick += RegisterClick;

			return form;
		}

		void RegisterClick (object sender, EventArgs e)
		{
			View.EndEditing (true);

			var form = sender as RegistrationFormView;
			if (form != null) {
				bool valid = true;

				if (String.IsNullOrEmpty (form.Email)) {
					form.ShowBubbleForEmail (messages.ErrorMessageEmailRequired);
					valid = false;
				} else if (!IsEmailValid (form.Email)) {
					form.ShowBubbleForEmail (messages.ErrorMessageEmailInvalid);
					valid = false;
				}
				if (String.IsNullOrEmpty (form.UserName)) {
					form.ShowBubbleForUserName (messages.ErrorMessageUserNameRequired);
					valid = false;
				}
				if (String.IsNullOrEmpty (form.Password)) {
					form.ShowBubbleForPassword (messages.ErrorMessagePasswordRequired);
					valid = false;
				} else if (!form.Password.Equals (form.PasswordConfirmation, StringComparison.Ordinal)) {
					form.ShowBubbleForPasswordConfirmation (messages.ErrorMessagePasswordDoesNotMatch);
					valid = false;
				}

				if (valid) {
					StartActivityAnimation (messages.RegistrationWaitingMessage);
					credentialsProvider.Register (form.Email, form.UserName, form.Password, Registered, x => Failed (form, x));
				}
			}
		}

		void Registered ()
		{
			InvokeOnMainThread (() => {
				if (credentialsProvider.NeedLoginAfterRegistration) {
					StopActivityAnimation ();
					#if __UNIFIED__
					NavigationController.PopViewController (true);
					#else
					NavigationController.PopViewControllerAnimated (true);
					#endif
				} else {
					StopActivityAnimation ();
					#if __UNIFIED__
					NavigationController.PopViewController (false);
					#else
					NavigationController.PopViewControllerAnimated (false);
					#endif
					completionHandler.Invoke ();
				}
			});
		}

		void Failed (RegistrationFormView form, LoginScreenFaultDetails details)
		{
			InvokeOnMainThread (() => {
				StopActivityAnimation ();
				if (!String.IsNullOrEmpty (details.EmailErrorMessage)) {
					form.ShowBubbleForEmail (details.EmailErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.UserNameErrorMessage)) {
					form.ShowBubbleForUserName (details.UserNameErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.PasswordErrorMessage)) {
					form.ShowBubbleForPassword (details.PasswordErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.CommonErrorMessage)) {
					ShowAlert (messages.RegistrationCommonErrorTitle, details.CommonErrorMessage, messages.AlertCancelButtonTitle);
				}
			});
		}
	}
}

