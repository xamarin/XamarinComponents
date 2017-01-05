namespace LoginScreen
{
	/// <summary>
	/// Login screen control.
	/// <para>
	/// It represents API to show login screen using the <see cref="LoginScreen.DefaultLoginScreenMessages"/> and the specified <typeparamref name="TCredentialsProvider" />.
	/// </para>
	/// </summary>
	/// <typeparam name="TCredentialsProvider">Represents credential operation's provider that implements <see cref="LoginScreen.ICredentialProvider" />.</typeparam>
	public class LoginScreenControl<TCredentialsProvider> : LoginScreenControl<TCredentialsProvider, DefaultLoginScreenMessages>
		where TCredentialsProvider : ICredentialsProvider, new()
	{
	}
}