using Android.Content;
using Android.Util;

using LoginScreen.Activities;

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
		/// Starts an activity for login screen using the specified <paramref name="context"/>.
		/// </summary>
		/// <param name="context">Used to specify сontext for new activity and starts it.</param>
		public static void Activate (Context context)
		{
			Intent intent = new Intent (context, typeof(LoginActivity));
			intent.PutExtra (BaseActivity.CredentialsProviderKey, typeof(TCredentialsProvider).AssemblyQualifiedName);
			intent.PutExtra (BaseActivity.MessagesKey, typeof(TMessages).AssemblyQualifiedName);
			context.StartActivity (intent);
		}
	}
}