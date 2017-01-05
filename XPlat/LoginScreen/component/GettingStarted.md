`LoginScreen` adds login, registration and reset password features to your iOS and Android applications.

### Examples
#### Implementing credential operation's logic:
To implement your credential operation's logic you need to implement interface `ICredentialsProvider`:

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

#### Adding `LoginScreen` to your app:
- iOS app

```csharp
using LoginScreen;

private bool needLogin = true;

public override void ViewDidAppear (bool animated)
{
	base.ViewDidAppear (animated);

	if (needLogin) {
		LoginScreenControl<CredentialsProvider>.Activate (this);
		needLogin = false;
	}
}
```

- Android app

```csharp
using LoginScreen;

protected override void OnCreate (Bundle bundle)
{
	base.OnCreate (bundle);

	// ...

	LoginScreenControl<CredentialsProvider>.Activate (this);
}
```

#### Localization

To localize `LoginScreen` or change strings from default you need to implement `ILoginScreenMessages` interface:

```csharp
using LoginScreen;

public class LoginScreenMessages : ILoginScreenMessages
{
	// Constructor without parameters is required
	
	// There must be placed properties that returns some sctrings to use it to display
}
```

And then, you neet to use `LoginScreenControl<CredentialsProvider, LoginScreenMessages>` instead of `LoginScreenControl<CredentialsProvider>`.

*Screenshots assembled with [PlaceIt](http://placeit.breezi.com).*
