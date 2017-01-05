using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using LoginScreen.Views;

namespace LoginScreen.ViewControllers
{
	class ResetPasswordViewController : BaseViewController
	{
		readonly ICredentialsProvider credentialsProvider;
		readonly ILoginScreenMessages messages;

		public ResetPasswordViewController (ICredentialsProvider credentialsProvider, ILoginScreenMessages messages)
			: base (messages.ResetPasswordFormTitle)
		{
			this.credentialsProvider = credentialsProvider;
			this.messages = messages;
		}

		protected override BaseFormView CreateFormView ()
		{
			var form = new ResetPasswordFormView
			{
				Title = messages.ResetPasswordFormTitle,
				EmailFieldPlaceHolder = messages.EmailFieldPlaceHolder,
				ResetButtonTitle = messages.ResetButtonTitle
			};

			form.ResetClick += ResetClick;

			return form;
		}

		void ResetClick (object sender, EventArgs e)
		{
			View.EndEditing (true);

			var form = sender as ResetPasswordFormView;
			if (form != null) {
				bool valid = true;

				if (String.IsNullOrEmpty (form.Email)) {
					form.ShowBubbleForEmail (messages.ErrorMessageEmailRequired);
					valid = false;
				} else if (!IsEmailValid (form.Email)) {
					form.ShowBubbleForEmail (messages.ErrorMessageEmailInvalid);
					valid = false;
				}

				if (valid) {
					StartActivityAnimation (messages.ResetingPasswordWaitingMessage);
					credentialsProvider.ResetPassword (form.Email, PasswordReset, x => Failed (form, x));
				}
			}
		}

		void PasswordReset ()
		{
			InvokeOnMainThread (() => {
				StopActivityAnimation ();
				#if __UNIFIED__
				NavigationController.PopViewController (true);
				#else
				NavigationController.PopViewControllerAnimated (true);
				#endif
			});
		}

		void Failed (ResetPasswordFormView form, LoginScreenFaultDetails details)
		{
			InvokeOnMainThread (() => {
				StopActivityAnimation ();
				if (!String.IsNullOrEmpty (details.EmailErrorMessage)) {
					form.ShowBubbleForEmail (details.EmailErrorMessage);
				}
				if (!String.IsNullOrEmpty (details.CommonErrorMessage)) {
					ShowAlert (messages.ResetingPasswordCommonErrorTitle, details.CommonErrorMessage, messages.AlertCancelButtonTitle);
				}
			});
		}
	}
}

