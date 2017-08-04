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
	[Activity (Label = "LoginActivity")]			
	class LoginActivity : BaseActivity
	{
		private const int RegisterActivityRequestCode = 1;
		private EditText userNameEdit;
		private EditText passwordEdit;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.a_login);

			rootLayout = FindViewById<RelativeLayout> (Resource.Id.rootLayout);

			userNameEdit = FindViewById<EditText> (Resource.Id.loginEdit);
			userNameEdit.Hint = messages.UserNameFieldPlaceHolder;

			passwordEdit = FindViewById<EditText> (Resource.Id.passwordEdit);
			passwordEdit.Hint = messages.PasswordFieldPlaceHolder;

			FindViewById<TextView> (Resource.Id.loginText).Text = messages.LoginFormTitle;
			FindViewById<TextView> (Resource.Id.forgotText).Text = messages.ForgotPasswordButtonTitle;
			FindViewById<TextView> (Resource.Id.noAccountText).Text = messages.RegisterSuggesionLabelText;

			var loginButton = FindViewById<Button> (Resource.Id.loginButton);
			loginButton.Text = messages.LogInButtonTitle;
			loginButton.Click += LoginClicked;

			var registerButton = FindViewById<Button> (Resource.Id.registerButton);
			registerButton.Text = messages.RegisterButtonTitle;
			registerButton.Click += RegisterClicked;

			FindViewById<LinearLayout> (Resource.Id.forgotLayout).Click += ForgotPasswordClicked;

			if (!credentialsProvider.ShowRegistration)
				FindViewById <View> (Resource.Id.noAccountSection).Visibility = ViewStates.Gone;

			if (!credentialsProvider.ShowPasswordResetLink)
				FindViewById <View> (Resource.Id.forgotLayout).Visibility = ViewStates.Gone;
		}

		protected override void OnResume ()
		{
			base.OnResume ();
			userNameEdit.Text = string.Empty;
			passwordEdit.Text = string.Empty;
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == RegisterActivityRequestCode
			    && resultCode == Result.Ok
			    && !credentialsProvider.NeedLoginAfterRegistration) {
				Finish ();
			}
		}

		void ForgotPasswordClicked (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof(ResetActivity));
			intent.PutExtra (CredentialsProviderKey, Intent.GetStringExtra (CredentialsProviderKey));
			intent.PutExtra (MessagesKey, Intent.GetStringExtra (MessagesKey));
			StartActivity (intent);
		}

		void RegisterClicked (object sender, EventArgs e)
		{
			Intent intent = new Intent (this, typeof(RegisterActivity));
			intent.PutExtra (CredentialsProviderKey, Intent.GetStringExtra (CredentialsProviderKey));
			intent.PutExtra (MessagesKey, Intent.GetStringExtra (MessagesKey));
			StartActivityForResult (intent, RegisterActivityRequestCode);
		}

		void LoginClicked (object sender, EventArgs e)
		{
			rootLayout.RequestFocus ();

			bool validationPassed = true;

			if (string.IsNullOrEmpty (userNameEdit.Text)) {
				ShowAlert (userNameEdit, messages.ErrorMessageUserNameRequired);
				validationPassed = false;
			}

			if (string.IsNullOrEmpty (passwordEdit.Text)) {
				ShowAlert (passwordEdit, messages.ErrorMessagePasswordRequired);
				validationPassed = false;
			}

			if (validationPassed) {
				progressDialog = ProgressDialog.Show (this, messages.LoginWaitingMessage, String.Empty, true, false);

				credentialsProvider.Login (userNameEdit.Text, passwordEdit.Text, LoginSuccessed, LoginFailed);
			}
		}

		void LoginSuccessed ()
		{
			RunOnUiThread (() =>
			{
				progressDialog.Dismiss ();
				Finish ();
			});
		}

		void LoginFailed (LoginScreenFaultDetails details)
		{
			RunOnUiThread (() =>
			{
				progressDialog.Dismiss ();

				if (!string.IsNullOrEmpty (details.UserNameErrorMessage)) {
					ShowAlert (userNameEdit, details.UserNameErrorMessage);
				}

				if (!string.IsNullOrEmpty (details.PasswordErrorMessage)) {
					ShowAlert (passwordEdit, details.PasswordErrorMessage);
				}

				if (!string.IsNullOrEmpty (details.CommonErrorMessage)) {
					var builder = new AlertDialog.Builder (this);
				
					builder.SetCancelable (false);

					builder.SetTitle (messages.LoginCommonErrorTitle);
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

