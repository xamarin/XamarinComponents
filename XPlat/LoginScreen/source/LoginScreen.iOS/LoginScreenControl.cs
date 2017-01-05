using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif

using LoginScreen.ViewControllers;

namespace LoginScreen
{
	/// <summary>
	/// Login screen control.
	/// <para>
	/// It represents API to show login screen using the specified <typeparamref name="TCredentialsProvider" /> and <typeparamref name="TMessages" />.
	/// </para>
	/// </summary>
	/// <typeparam name="TCredentialsProvider">Represents credential operation's provider that implements <see cref="LoginScreen.ICredentialsProvider" />.</typeparam>
	/// <typeparam name="TMessages">Represents a tuple of displayable strings, have to implement <see cref="LoginScreen.ILoginScreenMessages" />.</typeparam>
	public class LoginScreenControl<TCredentialsProvider, TMessages>
		where TCredentialsProvider : ICredentialsProvider, new()
		where TMessages : ILoginScreenMessages, new()
	{
		/// <summary>
		/// Presents login screen using the specified <paramref name="viewController"/>.
		/// </summary>
		/// <param name="viewController">Used to present login screen.</param>
		public static void Activate (UIViewController viewController)
		{
			var loginScreen = new LoginViewController (new TCredentialsProvider(), new TMessages());
			var navigationController = new CustomNavigationController (loginScreen);
			loginScreen.SetCompletionHandler (() => navigationController.DismissViewController (true, null));
			viewController.PresentViewController (navigationController, true, null);
		}
	}
}

