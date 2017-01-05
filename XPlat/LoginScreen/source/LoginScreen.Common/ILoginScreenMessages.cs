namespace LoginScreen {
	/// <summary>
	/// Interface to provide the tuple of displayable strings to the <see cref="LoginScreen.LoginScreenControl"/>.
	/// </summary>
	public interface ILoginScreenMessages {
		/// <summary>
		/// Gets the login form title.
		/// </summary>
		/// <value>The login form title.</value>
		string LoginFormTitle { get; }

		/// <summary>
		/// Gets the user name field place holder.
		/// </summary>
		/// <value>The user name field place holder.</value>
		string UserNameFieldPlaceHolder { get; }

		/// <summary>
		/// Gets the password field place holder.
		/// </summary>
		/// <value>The password field place holder.</value>
		string PasswordFieldPlaceHolder { get; }

		/// <summary>
		/// Gets the log in button title.
		/// </summary>
		/// <value>The log in button title.</value>
		string LogInButtonTitle { get; }

		/// <summary>
		/// Gets the forgot password button title.
		/// </summary>
		/// <value>The forgot password button title.</value>
		string ForgotPasswordButtonTitle { get; }

		/// <summary>
		/// Gets the or label text.
		/// </summary>
		/// <value>The or label text.</value>
		string OrLabelText { get; }

		/// <summary>
		/// Gets the register suggesion label text.
		/// </summary>
		/// <value>The register suggesion label text.</value>
		string RegisterSuggesionLabelText { get; }

		/// <summary>
		/// Gets the register button title.
		/// </summary>
		/// <value>The register button title.</value>
		string RegisterButtonTitle { get; }

		/// <summary>
		/// Gets the reset password form title.
		/// </summary>
		/// <value>The reset password form title.</value>
		string ResetPasswordFormTitle { get; }

		/// <summary>
		/// Gets the email field place holder.
		/// </summary>
		/// <value>The email field place holder.</value>
		string EmailFieldPlaceHolder { get; }

		/// <summary>
		/// Gets the reset button title.
		/// </summary>
		/// <value>The reset button title.</value>
		string ResetButtonTitle { get;}

		/// <summary>
		/// Gets the registration form title.
		/// </summary>
		/// <value>The registration form title.</value>
		string RegistrationFormTitle { get; }

		/// <summary>
		/// Gets the password confirmation field place holder.
		/// </summary>
		/// <value>The password confirmation field place holder.</value>
		string PasswordConfirmationFieldPlaceHolder { get; }

		/// <summary>
		/// Gets the email required error message.
		/// </summary>
		/// <value>The email required error message.</value>
		string ErrorMessageEmailRequired { get; }

		/// <summary>
		/// Gets the email invalid error message.
		/// </summary>
		/// <value>The email invalid error message.</value>
		string ErrorMessageEmailInvalid { get; }

		/// <summary>
		/// Gets the user name required error message.
		/// </summary>
		/// <value>The user name required error message.</value>
		string ErrorMessageUserNameRequired { get; }

		/// <summary>
		/// Gets the password required error message.
		/// </summary>
		/// <value>The password required error message.</value>
		string ErrorMessagePasswordRequired { get; }

		/// <summary>
		/// Gets the password does not match error message.
		/// </summary>
		/// <value>The password does not match error message.</value>
		string ErrorMessagePasswordDoesNotMatch { get; }

		/// <summary>
		/// Gets the login waiting message.
		/// </summary>
		/// <value>The login waiting message.</value>
		string LoginWaitingMessage { get; }

		/// <summary>
		/// Gets the registration waiting message.
		/// </summary>
		/// <value>The registration waiting message.</value>
		string RegistrationWaitingMessage { get; }

		/// <summary>
		/// Gets the reseting password waiting message.
		/// </summary>
		/// <value>The reseting password waiting message.</value>
		string ResetingPasswordWaitingMessage { get; }

		/// <summary>
		/// Gets the login common error title.
		/// </summary>
		/// <value>The login common error title.</value>
		string LoginCommonErrorTitle { get; }

		/// <summary>
		/// Gets the registration common error title.
		/// </summary>
		/// <value>The registration common error title.</value>
		string RegistrationCommonErrorTitle { get; }

		/// <summary>
		/// Gets the reseting password common error title.
		/// </summary>
		/// <value>The reseting password common error title.</value>
		string ResetingPasswordCommonErrorTitle { get; }

		/// <summary>
		/// Gets the alert cancel button title.
		/// </summary>
		/// <value>The alert cancel button title.</value>
		string AlertCancelButtonTitle { get; }
	}
}

