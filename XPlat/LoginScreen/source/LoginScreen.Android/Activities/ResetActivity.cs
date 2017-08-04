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
	[Activity (Label = "ResetActivity")]			
	class ResetActivity : BaseActivity
	{
		private EditText emailEdit;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.a_reset);

			rootLayout = FindViewById<RelativeLayout>(Resource.Id.rootLayout);

			FindViewById<TextView>(Resource.Id.resetText).Text = messages.ResetPasswordFormTitle;

			emailEdit = FindViewById<EditText>(Resource.Id.emailEdit);
			emailEdit.Hint = messages.EmailFieldPlaceHolder;

			var resetButton = FindViewById<Button>(Resource.Id.resetButton);
			resetButton.Text = messages.ResetButtonTitle;
			resetButton.Click += ResetClicked;
		}

		void ResetClicked (object sender, EventArgs e)
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

			if (validationPassed)
			{
				progressDialog = ProgressDialog.Show(this, messages.ResetingPasswordWaitingMessage, String.Empty, true, false);
				
				credentialsProvider.ResetPassword(emailEdit.Text, ResetSuccessed, ResetFailed);
			}
		}

		void ResetSuccessed()
		{
			RunOnUiThread(()=>
			{
				progressDialog.Dismiss();
				Finish();
			});
		}
		
		void ResetFailed(LoginScreenFaultDetails details)
		{
			RunOnUiThread(()=>
			{
				progressDialog.Dismiss();
				
				if (!string.IsNullOrEmpty (details.EmailErrorMessage)) {
					ShowAlert (emailEdit, details.EmailErrorMessage);
				}

				if (!string.IsNullOrEmpty (details.CommonErrorMessage)) {
					var builder = new AlertDialog.Builder (this);

					builder.SetCancelable (false);

					builder.SetTitle (messages.ResetingPasswordCommonErrorTitle);
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

