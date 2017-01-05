namespace LoginScreen
{
	/// <summary>
	/// Represents fail details for credential operation's provided by <see cref="LoginScreen.ICredentialsProvider"/> implementers.
	/// </summary>
	public class LoginScreenFaultDetails
	{
		/// <summary>
		/// Gets or sets the common error message for whole form.
		/// </summary>
		/// <value>The common error message for whole form or <c>null<c/> if no error.</value>
		public string CommonErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets error message for the user name form field.
		/// </summary>
		/// <value>The error message for the user name form field or <c>null<c/> if no error at this field.</value>
		public string UserNameErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets error message for the password form field.
		/// </summary>
		/// <value>The error message for the password form field or <c>null<c/> if no error at this field.</value>
		public string PasswordErrorMessage { get; set; }

		/// <summary>
		/// Gets or sets error message for the e-mail form field.
		/// </summary>
		/// <value>The error message for the e-mail form field or <c>null<c/> if no error at this field.</value>
		public string EmailErrorMessage { get; set; }
	}
}

