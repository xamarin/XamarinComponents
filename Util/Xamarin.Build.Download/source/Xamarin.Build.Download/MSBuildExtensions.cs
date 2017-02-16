using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Xamarin.Build.Download
{
	//from Xamarin.Android MSBuild targets
	static class MSBuildExtensions
	{
		public static void LogDebugMessage (this TaskLoggingHelper log, string message, params object [] messageArgs)
		{
			log.LogMessage (MessageImportance.Low, message, messageArgs);
		}

		public static void LogCodedError (this TaskLoggingHelper log, string code, string message, params object [] messageArgs)
		{
			log.LogError (string.Empty, code, string.Empty, string.Empty, 0, 0, 0, 0, message, messageArgs);
		}
	}

	public interface ILogger
	{
		void LogCodedError (string code, string message, params object [] messageArgs);
		void LogErrorFromException (System.Exception exception);
	}
}