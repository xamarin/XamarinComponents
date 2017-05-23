using Android.Util;
using System;

namespace Xamarin.InAppBilling.Utilities
{
	/// <summary>
	/// Helper class to support logging within the In-App Purchases routines
	/// </summary>
	internal class Logger
	{
		#region Constants
		const string Tag = "InApp-Billing";
		#endregion 

		#region Public methods
		/// <summary>
		/// Writes debug information to the log
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="args">Arguments.</param>
		public static void Debug (string format, params object[] args)
		{
			#if DEBUG
			Log.Debug (Tag, string.Format (format, args));
			//Console.WriteLine(format,args);
			#endif 
		}

		/// <summary>
		/// Writes general information to the log
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="args">Arguments.</param>
		public static void Info (string format, params object[] args)
		{
			Log.Info (Tag, string.Format (format, args));

//			#if DEBUG
//			Console.WriteLine(format,args);
//			#endif
		}

		/// <summary>
		/// Writes error information to the log
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="args">Arguments.</param>
		public static void Error (string format, params object[] args)
		{
			Log.Error (Tag, string.Format (format, args));

//			#if DEBUG
//			Console.WriteLine(format,args);
//			#endif
		}
		#endregion 
	}
}

