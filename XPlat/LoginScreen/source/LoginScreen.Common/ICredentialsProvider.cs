using System;

namespace LoginScreen
{
	/// <summary>
	/// Interface to provide credential operations to the <see cref="LoginScreen.LoginScreenControl"/>.
	/// </summary>
	public interface ICredentialsProvider {
		/// <summary>
		/// Returns a value indicating whether the <see cref="LoginScreen.LoginScreenControl"/> needs to show login screen after success registration.
		/// </summary>
		/// <value>
		/// <c>true</c> if need to show login after success registration; otherwise, <c>false</c>.
		/// </value>
		bool NeedLoginAfterRegistration { get; }

		/// <summary>
		/// Login the user with the specified <paramref name="userName"/> and <paramref name="password"/>.
		/// Call <paramref name="successCallback"/> if login was completed successfully; otherwise, call <paramref name="failCallback"/>.
		/// </summary>
		/// <param name="userName">Represents User Name.</param>
		/// <param name="password">Represents Password.</param>
		/// <param name="successCallback">Represents callback for successfully login completion.</param>
		/// <param name="failCallback">Represents callback for unsuccessfully login completion.</param>
		void Login (string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback);

		/// <summary>
		/// Register the user with the specified <paramref name="email"/>, <paramref name="userName"/> and <paramref name="password"/>.
		/// Call <paramref name="successCallback"/> if registration was completed successfully; otherwise, call <paramref name="failCallback"/>.
		/// </summary>
		/// <param name="email">Represents E-mail address.</param>
		/// <param name="userName">Represents User Name.</param>
		/// <param name="password">Represents Password.</param>
		/// <param name="successCallback">Represents callback for successfully registration completion.</param>
		/// <param name="failCallback">Represents callback for unsuccessfully registration completion.</param>
		void Register (string email, string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback);

		/// <summary>
		/// Reset password for the user with the specified <paramref name="email"/>.
		/// Call <paramref name="successCallback"/> if resetting was completed successfully; otherwise, call <paramref name="failCallback"/>.
		/// </summary>
		/// <param name="email">Represents E-mail address.</param>
		/// <param name="successCallback">Represents callback for successfully resetting completion.</param>
		/// <param name="failCallback">Represents callback for unsuccessfully resetting completion.</param>
		void ResetPassword (string email, Action successCallback, Action<LoginScreenFaultDetails> failCallback);

		/// <summary>
		/// Returns a value indicating whether the <see cref="LoginScreen.LoginScreenControl"/> needs to show the registration section.
		/// </summary>
		/// <value>
		/// <c>true</c> if need to show the registration section; otherwise, <c>false</c>.
		/// </value>
		bool ShowRegistration { get; }

		/// <summary>
		/// Returns a value indicating whether the <see cref="LoginScreen.LoginScreenControl"/> needs to show the forgot password button.
		/// </summary>
		/// <value>
		/// <c>true</c> if need to show the forgot password button; otherwise, <c>false</c>.
		/// </value>
		bool ShowPasswordResetLink { get; }
	}
}


