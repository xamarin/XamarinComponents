using System;
namespace Firebase.JobDispatcher
{
	/// <summary>
	/// This is a C# mapping of com.firebase.jobdispatcher.Lifetime
	/// </summary>
	public static class JobLifetime
	{
		/// <summary>
		/// The Job should be preserved until the next boot. This is the default
		/// </summary>
		public const int UntilNextBoot = 1;

		/// <summary>
		/// The job should be preserved "forever".
		/// </summary>
		public const int Forever = 2;
	}
}
