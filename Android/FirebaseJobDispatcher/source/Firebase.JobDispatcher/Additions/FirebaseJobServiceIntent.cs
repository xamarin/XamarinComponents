using System;
namespace Firebase.JobDispatcher
{
	/// <summary>
	/// Each JobService subclass must set this as the Action within an IntentFilter.
	/// </summary>
	public static class FirebaseJobServiceIntent
	{
		public const string Action = "com.firebase.jobdispatcher.ACTION_EXECUTE";
	}
	
}
