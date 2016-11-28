using System;
using Android.App;
using Android.Runtime;
using Android.Util;

using TimberLog;

namespace TimberSample
{
	[Application]
	public class ExampleApp : Application
	{
		protected ExampleApp(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

#if DEBUG
			Timber.Plant(new Timber.DebugTree());
#else
			Timber.Plant(new CrashReportingTree());
#endif
		}

		// A tree which logs important information for crash reporting.
		private class CrashReportingTree : Timber.Tree
		{
			protected override void Log(LogPriority priority, string tag, string message, Java.Lang.Throwable t)
			{
				if (priority == LogPriority.Verbose || priority == LogPriority.Debug)
				{
					return;
				}

				FakeCrashLibrary.Log(priority, tag, message);

				if (t != null)
				{
					if (priority == LogPriority.Error)
					{
						FakeCrashLibrary.LogError(t);
					}
					else if (priority == LogPriority.Warn)
					{
						FakeCrashLibrary.LogWarning(t);
					}
				}
			}
		}
	}
}
