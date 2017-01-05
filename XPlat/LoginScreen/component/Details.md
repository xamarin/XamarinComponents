`LoginScreen` adds login, registration and reset password features to your iOS and Android applications.

To use `LoginScreen` you must implement `ICredentialsProvider` interface:

```csharp
using LoginScreen;

public class CredentialsProvider : ICredentialsProvider
{
	// Constructor without parameters is required

	public bool NeedLoginAfterRegistration {
		get {
			// If you want your user to login after he/she has been registered
			return true;

			// Otherwise you can:
			// return false;
		}
	}

	public void Login (string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
	{
		// Do some operations to login user

		// If login was successfully completed
		successCallback();

		// Otherwise
		// failCallback(new LoginScreenFaultDetails {
		// 	CommonErrorMessage = "Some error message relative to whole form",
		// 	UserNameErrorMessage = "Some error message relative to user name form field",
		// 	PasswordErrorMessage = "Some error message relative to password form field"
		// });
	}

	public void Register (string email, string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
	{
		// Do some operations to register user

		// If registration was successfully completed
		successCallback();

		// Otherwise
		// failCallback(new LoginScreenFaultDetails {
		// 	CommonErrorMessage = "Some error message relative to whole form",
		// 	EmailErrorMessage = "Some error message relative to e-mail form field",
		// 	UserNameErrorMessage = "Some error message relative to user name form field",
		// 	PasswordErrorMessage = "Some error message relative to password form field"
		// });
	}

	public void ResetPassword (string email, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
	{
		// Do some operations to reset user's password

		// If password was successfully reset
		successCallback();

		// Otherwise
		// failCallback(new LoginScreenFaultDetails {
		// 	CommonErrorMessage = "Some error message relative to whole form",
		// 	EmailErrorMessage = "Some error message relative to e-mail form field"
		// });
	}

	public bool ShowPasswordResetLink {
		get {
			// If you want your login screen to have a forgot password button
			return true;

			// Otherwise you can:
			// return false;
		}
	}
	
	public bool ShowRegistration {
		get {
			// If you want your login screen to have a register new user button
			return true;

			// Otherwise you can:
			// return false;
		}
	}
}
```
You must call one of callbacks; otherwise login screen will never hide.

###You can add `LoginScreen` to your app as follows:
- adding `LoginScreen` to your iOS app:

```csharp
LoginScreen.LoginScreenControl<CredentialsProvider>.Activate (viewController);
```

- adding `LoginScreen` to your Android app:

```csharp
LoginScreen.LoginScreenControl<CredentialsProvider>.Activate (context);
```

*Screenshots assembled with [PlaceIt](http://placeit.breezi.com).*
