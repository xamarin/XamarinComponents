namespace LoginScreen
{
	/// <summary>
	/// Default implementation of the <see cref="LoginScreen.ILoginScreenMessages" />.
	/// </summary>
	public class DefaultLoginScreenMessages : ILoginScreenMessages
	{
		/// <summary>
		/// Gets the default login form title.
		/// </summary>
		/// <value>The login form title.</value>
		public string LoginFormTitle {
			get { return "Login"; }
		}

		/// <summary>
		/// Gets the default user name field place holder.
		/// </summary>
		/// <value>The user name field place holder.</value>
		public string UserNameFieldPlaceHolder {
			get { return "Username"; }
		}

		/// <summary>
		/// Gets the default password field place holder.
		/// </summary>
		/// <value>The password field place holder.</value>
		public string PasswordFieldPlaceHolder {
			get { return "Password"; }
		}

		/// <summary>
		/// Gets the default log in button title.
		/// </summary>
		/// <value>The log in button title.</value>
		public string LogInButtonTitle {
			get { return "Log In"; }
		}

		/// <summary>
		/// Gets the default forgot password button title.
		/// </summary>
		/// <value>The forgot password button title.</value>
		public string ForgotPasswordButtonTitle {
			get { return "Forgot your Password?"; }
		}

		/// <summary>
		/// Gets the default or label text.
		/// </summary>
		/// <value>The or label text.</value>
		public string OrLabelText {
			get { return "or"; }
		}

		/// <summary>
		/// Gets the default register suggesion label text.
		/// </summary>
		/// <value>The register suggesion label text.</value>
		public string RegisterSuggesionLabelText {
			get { return "Don't have an account yet?"; }
		}

		/// <summary>
		/// Gets the default register button title.
		/// </summary>
		/// <value>The register button title.</value>
		public string RegisterButtonTitle {
			get { return "Register"; }
		}

		/// <summary>
		/// Gets the default reset password form title.
		/// </summary>
		/// <value>The reset password form title.</value>
		public string ResetPasswordFormTitle {
			get { return "Reset Password"; }
		}

		/// <summary>
		/// Gets the default email field place holder.
		/// </summary>
		/// <value>The email field place holder.</value>
		public string EmailFieldPlaceHolder {
			get { return "Email"; }
		}

		/// <summary>
		/// Gets the default reset button title.
		/// </summary>
		/// <value>The reset button title.</value>
		public string ResetButtonTitle {
			get { return "Reset"; }
		}

		/// <summary>
		/// Gets the default registration form title.
		/// </summary>
		/// <value>The registration form title.</value>
		public string RegistrationFormTitle {
			get { return "Registration"; }
		}

		/// <summary>
		/// Gets the default password confirmation field place holder.
		/// </summary>
		/// <value>The password confirmation field place holder.</value>
		public string PasswordConfirmationFieldPlaceHolder {
			get { return "Confirm Password"; }
		}

		/// <summary>
		/// Gets the default email required error message.
		/// </summary>
		/// <value>The email required error message.</value>
		public string ErrorMessageEmailRequired {
			get { return "Email is required."; }
		}

		/// <summary>
		/// Gets the default email invalid error message.
		/// </summary>
		/// <value>The email invalid error message.</value>
		public string ErrorMessageEmailInvalid {
			get { return "Email you entered is incorrect."; }
		}

		/// <summary>
		/// Gets the default user name required error message.
		/// </summary>
		/// <value>The user name required error message.</value>
		public string ErrorMessageUserNameRequired {
			get { return "Username is required."; }
		}

		/// <summary>
		/// Gets the default password required error message.
		/// </summary>
		/// <value>The password required error message.</value>
		public string ErrorMessagePasswordRequired {
			get { return "Password is required."; }
		}

		/// <summary>
		/// Gets the default password does not match error message.
		/// </summary>
		/// <value>The password does not match error message.</value>
		public string ErrorMessagePasswordDoesNotMatch {
			get { return "Password does not match."; }
		}

		/// <summary>
		/// Gets the default login waiting message.
		/// </summary>
		/// <value>The login waiting message.</value>
		public string LoginWaitingMessage {
			get { return "Logging in…"; }
		}

		/// <summary>
		/// Gets the default registration waiting message.
		/// </summary>
		/// <value>The registration waiting message.</value>
		public string RegistrationWaitingMessage {
			get { return "Registering…"; }
		}

		/// <summary>
		/// Gets the default reseting password waiting message.
		/// </summary>
		/// <value>The reseting password waiting message.</value>
		public string ResetingPasswordWaitingMessage {
			get { return "Reseting password…"; }
		}

		/// <summary>
		/// Gets the default login common error title.
		/// </summary>
		/// <value>The login common error title.</value>
		public string LoginCommonErrorTitle {
			get { return "Authorization error"; }
		}

		/// <summary>
		/// Gets the default registration common error title.
		/// </summary>
		/// <value>The registration common error title.</value>
		public string RegistrationCommonErrorTitle {
			get { return "Registration error"; }
		}

		/// <summary>
		/// Gets the default reseting password common error title.
		/// </summary>
		/// <value>The reseting password common error title.</value>
		public string ResetingPasswordCommonErrorTitle {
			get { return "Reseting error"; }
		}

		/// <summary>
		/// Gets the default alert cancel button title.
		/// </summary>
		/// <value>The alert cancel button title.</value>
		public string AlertCancelButtonTitle {
			get { return "OK"; }
		}
	}
}

