using System;
using Android.Util;

namespace TimberSample
{
	// Not a real crash reporting library!
	public static class FakeCrashLibrary
	{
		public static void Log(LogPriority priority, string tag, string message)
		{
			// TODO add log entry to circular buffer.

			Console.WriteLine($"Logging message: '{message}', tagged: '{tag}', priority: '{priority}'.");
		}

		public static void LogWarning(Exception ex)
		{
			// TODO report non-fatal warning.

			Console.WriteLine($"Logging warning: '{ex.Message}'.");
		}

		public static void LogError(Exception ex)
		{
			// TODO report non-fatal error.

			Console.WriteLine($"Logging error: '{ex.Message}'.");
		}
	}
}
