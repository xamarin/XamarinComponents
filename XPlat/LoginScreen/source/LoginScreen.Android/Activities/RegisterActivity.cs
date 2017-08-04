using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginScreen.Activities
{
	[Activity (Label = "RegisterActivity")]			
	class RegisterActivity : BaseActivity
	{
		private EditText emailEdit;
		private EditText userNameEdit;
		private EditText passwordEdit;
		private EditText confirmPasswordEdit;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.a_register);

			rootLayout = FindViewById<RelativeLayout>(Resource.Id.rootLayout);

			FindViewById<TextView>(Resource.Id.registerText).Text = messages.RegistrationFormTitle;

			var registerButton = FindViewById<Button>(Resource.Id.registerButton);
			registerButton.Text = messages.RegisterButtonTitle;
			registerButton.Click += RegisterClicked;

			emailEdit = FindViewById<EditText>(Resource.Id.emailEdit);
			emailEdit.Hint = messages.EmailFieldPlaceHolder;

			userNameEdit = FindViewById<EditText>(Resource.Id.loginEdit);
			userNameEdit.Hint = messages.UserNameFieldPlaceHolder;

			passwordEdit = FindViewById<EditText>(Resource.Id.passwordEdit);
			passwordEdit.Hint = messages.PasswordFieldPlaceHolder;

			confirmPasswordEdit = FindViewById<EditText>(Resource.Id.confirmPasswordEdit);
			confirmPasswordEdit.Hint = messages.PasswordConfirmationFieldPlaceHolder;
		}

		void RegisterClicked(object sender, EventArgs e)
		{
			rootLayout.RequestFocus();

			bool validationPassed = true;

			if (string.IsNullOrEmpty(emailEdit.Text))
			{
				ShowAlert(emailEdit, messages.ErrorMessageEmailRequired);
				validationPassed = false;
			}

			if (!IsValidEmail(emailEdit.Text))
			{
				ShowAlert(emailEdit, messages.ErrorMessageEmailInvalid);
				validationPassed = false;
			}
			
			if (string.IsNullOrEmpty(userNameEdit.Text))
			{
				ShowAlert(userNameEdit, messages.ErrorMessageUserNameRequired);
				validationPassed = false;
			}
			
			if (string.IsNullOrEmpty(passwordEdit.Text))
			{
				ShowAlert(passwordEdit, messages.ErrorMessagePasswordRequired);
				validationPassed = false;
			}

			if (passwordEdit.Text != confirmPasswordEdit.Text)
			{
				ShowAlert(confirmPasswordEdit, messages.ErrorMessagePasswordDoesNotMatch);
				validationPassed = false;
			}

			if (validationPassed)
			{
				progressDialog = ProgressDialog.Show(this, messages.RegistrationWaitingMessage, String.Empty, true, false);
				
				credentialsProvider.Register(emailEdit.Text, userNameEdit.Text, passwordEdit.Text, RegisterSuccessed, RegisterFailed);
			}
		}

		void RegisterSuccessed()
		{
			RunOnUiThread(()=>
			{
				progressDialog.Dismiss();
				Intent returnIntent = new Intent();
				SetResult(Result.Ok, returnIntent);
				Finish();
			});
		}
		
		void RegisterFailed(LoginScreenFaultDetails details)
		{
			RunOnUiThread(()=>
            {
				progressDialog.Dismiss();

				if (!string.IsNullOrEmpty (details.EmailErrorMessage)) {
					ShowAlert (emailEdit, details.EmailErrorMessage);
				}

				if (!string.IsNullOrEmpty (details.UserNameErrorMessage)) {
					ShowAlert (userNameEdit, details.UserNameErrorMessage);
				}
				
				if (!string.IsNullOrEmpty (details.PasswordErrorMessage)) {
					ShowAlert (passwordEdit, details.PasswordErrorMessage);
				}
				
				if (!string.IsNullOrEmpty (details.CommonErrorMessage)) {
					var builder = new AlertDialog.Builder (this);

					builder.SetCancelable (false);

					builder.SetTitle (messages.RegistrationCommonErrorTitle);
					builder.SetMessage (details.CommonErrorMessage);

					builder.SetPositiveButton (messages.AlertCancelButtonTitle, (sender, args) =>
					{
						// do nothing => dismiss
					});

					var dialog = builder.Create ();

					dialog.Show ();
				}
			});
		}
	}
}

