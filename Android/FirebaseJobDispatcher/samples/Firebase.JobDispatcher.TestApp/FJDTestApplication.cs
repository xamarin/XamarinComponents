using System;
using Android.App;
using Android.Runtime;
using FJDTestApp.Model;

namespace FJDTestApp
{
	[Application]
	public class FJDTestApplication : Application
	{
		static volatile IJobHistoryStorage jobStorage;
		static readonly object lockObject = new Object();

		public FJDTestApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
		{

		}

		public override void OnCreate()
		{
			base.OnCreate();
		}

		public static IJobHistoryStorage JobHistoryStorage
		{
			get
			{
				if (jobStorage == null)
				{
					lock (lockObject)
					{
						if (jobStorage == null)
						{
							jobStorage = new MemoryBackedJobHistoryStorage();
						}
					}
				}
				return jobStorage;
			}
		}
	}
}
