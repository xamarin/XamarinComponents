using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Android.Content;
using Android.Runtime;
using Fabric;
using Java.Lang;

namespace Crashlytics
{
	public partial class Crashlytics : global::Fabric.Kit, global::Fabric.IKitGroup
	{
		public override int CompareTo(Java.Lang.Object obj)
		{
			return -1;
		}

		static void AndroidEnvironmentOnUnhandledExceptionRaiser(RaiseThrowableEventArgs eventArgs, bool callJavaDefaultUncaughtExceptionHandler)
		{
			var exception = MonoExceptionHelper.Create(eventArgs.Exception);

			if (callJavaDefaultUncaughtExceptionHandler && Thread.DefaultUncaughtExceptionHandler != null)
				Thread.DefaultUncaughtExceptionHandler.UncaughtException(Thread.CurrentThread(), exception);
			else
				Crashlytics.LogException(exception);
		}

		public static void HandleManagedExceptions(bool callJavaDefaultUncaughtExceptionHandler = true)
		{
			AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
				AndroidEnvironmentOnUnhandledExceptionRaiser(args, callJavaDefaultUncaughtExceptionHandler);
		}
	}

	public class MonoException : Java.Lang.Exception
	{
		public MonoException(string message, StackTraceElement[] stack)
			: base(message)
		{
			SetStackTrace(stack);
		}

		public MonoException(string message, StackTraceElement[] stack, Throwable cause)
			: base(message, cause)
		{
			SetStackTrace(stack);
		}
	}

	public partial class MonoExceptionHelper : MonoException
	{
		public MonoExceptionHelper(string message, StackTraceElement[] stack) : base(message, stack)
		{
		}

		public MonoExceptionHelper(string message, StackTraceElement[] stack, Throwable cause) : base(message, stack, cause)
		{
		}

		/// <summary>
		///     Format for stack trace entries with line numbers:
		///     "at FullClassName. MethodName (MethodParams) in FileName :line LineNumber "
		/// </summary>
		private static readonly Regex ExpressionWithLineNumbers =
			new Regex(
				@"^\s*at (?<ClassName>\S*)\.(?<MethodName>\S*) (?<MethodArguments>\(.*\)) (?<Offset>.*) in (?<Filename>.*):(?<LineNumber>\d*)\s*$");

		/// <summary>
		///     /// Format for stack trace entries without line numbers:
		///     "at FullClassName. MethodName (MethodParams)"
		/// </summary>
		private static readonly Regex ExpressionWithoutLineNumbers =
			new Regex(@"^\s*at (?<ClassName>\S*)\.(?<MethodName>\S*) (?<MethodArguments>\S*) (?<Offset>.*)\s*$");

		public static MonoExceptionHelper Create(System.Exception e)
		{
			var message = string.Format("({0}) {1}", e.GetType().Name, e.Message);

			if (e is AggregateException aggregateException)
			{
				var flattened = aggregateException.Flatten();

				var stackTraceElements1 = new List<StackTraceElement>();
				stackTraceElements1.AddRange(ParseStack(aggregateException.StackTrace ?? string.Empty).ToArray());
				foreach (var innerException in flattened.InnerExceptions)
				{
					var innerExceptionTitle = string.Format("({0}) {1}", innerException.GetType().Name, innerException.Message);
					stackTraceElements1.Add(new StackTraceElement("NEXT-INNER-EXCEPTION", innerExceptionTitle, "unknown.cs", 1));
					stackTraceElements1.AddRange(ParseStack(innerException.StackTrace ?? string.Empty).ToArray());
				}
				return new MonoExceptionHelper(message, stackTraceElements1.ToArray());
			}

			if (e.InnerException != null)
			{
				var stackTraceElements2 = ParseStack(e.StackTrace ?? string.Empty).ToArray();
				return new MonoExceptionHelper(message, stackTraceElements2, Create(e.InnerException));
			}

			var stackTraceElements3 = ParseStack(e.StackTrace ?? string.Empty).ToArray();
			return new MonoExceptionHelper(message, stackTraceElements3);
		}

		private static IEnumerable<StackTraceElement> ParseStack(string stack)
		{
			if (stack != null)
			{
				var lines = stack.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

				foreach (var line in lines)
				{
					var match = ExpressionWithLineNumbers.Match(line);
					if (match.Success)
						yield return StackTraceElementWithLineNumbers(match);
					else
					{
						match = ExpressionWithoutLineNumbers.Match(line);
						if (match.Success)
							yield return StackTraceElementWithoutLineNumbers(match);
						else
							yield return StackTraceElement(line);
					}
				}
			}
		}

		private static StackTraceElement StackTraceElement(string line)
		{
			return new StackTraceElement(line, "", "unknown.cs", 1);
		}

		private static StackTraceElement StackTraceElementWithLineNumbers(Match match)
		{
			var lineNumber = int.Parse(match.Groups["LineNumber"].Value);

			if (lineNumber != 0)
			{
				var method = match.Groups["MethodName"].Value + match.Groups["MethodArguments"].Value +
										 match.Groups["Offset"].Value;
				return new StackTraceElement(match.Groups["ClassName"].Value, method,
					match.Groups["Filename"].Value, lineNumber);
			}
			return StackTraceElementWithoutLineNumbers(match);
		}

		private static StackTraceElement StackTraceElementWithoutLineNumbers(Match match)
		{
			var method = match.Groups["MethodName"].Value + match.Groups["MethodArguments"].Value +
								  match.Groups["Offset"].Value;

			var virtualFilename = match.Groups["ClassName"].Value + match.Groups["Offset"].Value;
			virtualFilename = virtualFilename.Replace('.', '/'); // Change namespace separators to path separators.
			virtualFilename = Regex.Replace(virtualFilename, @"[^\w\/]", ""); // Only alow alphanumeric and /
			virtualFilename += ".cs";
			return new StackTraceElement(match.Groups["ClassName"].Value, method, virtualFilename, 1);
		}
	}
}
