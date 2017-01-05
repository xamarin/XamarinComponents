using System;
using System.Timers;

using LoginScreen;

namespace LoginScreenSample
{
	public class TestCredentialsProvider : ICredentialsProvider
	{
		readonly Random rnd = new Random ();

		public bool NeedLoginAfterRegistration {
			get { return false; }
		}

		public bool ShowRegistration {
			get { return true; }
		}
		public bool ShowPasswordResetLink {
			get { return true; }
		}

		public void Login (string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			DelayInvoke (EquiprobableSelect (successCallback, () => failCallback (new LoginScreenFaultDetails { CommonErrorMessage = "Something wrong happened." })));
		}

		public void Register (string email, string userName, string password, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			DelayInvoke (() => {
				if (password.Length < 4) {
					failCallback (new LoginScreenFaultDetails { PasswordErrorMessage = "Password must be at least 4 chars." });
				} else {
					successCallback ();
				}
			});
		}

		public void ResetPassword (string email, Action successCallback, Action<LoginScreenFaultDetails> failCallback)
		{
			DelayInvoke (EquiprobableSelect (successCallback, () => failCallback (new LoginScreenFaultDetails { CommonErrorMessage = "Something wrong happened." })));
		}

		private void DelayInvoke (Action operation)
		{
			Timer timer = new Timer ();
			timer.AutoReset = false;
			timer.Interval = 3000;
			timer.Elapsed += (object sender, ElapsedEventArgs e) => operation.Invoke ();
			timer.Start ();
		}

		private T EquiprobableSelect<T> (T first, T second)
		{
			return rnd.Next (100) < 50 ? first : second;
		}
	}
}

