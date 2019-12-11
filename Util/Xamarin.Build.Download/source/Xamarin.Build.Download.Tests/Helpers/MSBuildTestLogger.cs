// Copyright (c) 2015-2016 Xamarin Inc.

using System.Collections.Generic;
using System.Linq;
using Microsoft.Build.Framework;

namespace Xamarin.ContentPipeline.Tests
{
	public class MSBuildTestLogger : ILogger
	{
		IEventSource eventSource;

		public void Initialize (IEventSource eventSource)
		{
			this.eventSource = eventSource;
			Events = new List<BuildEventArgs> ();
			eventSource.AnyEventRaised += EventRaised;
			Verbosity = LoggerVerbosity.Normal;
		}

		public void Shutdown ()
		{
			eventSource.AnyEventRaised -= EventRaised;
		}

		void EventRaised (object sender, BuildEventArgs e)
		{
			Events.Add (e);
			System.Console.WriteLine (e.Message);
		}

		public List<BuildEventArgs> Events { get; private set; }

		public IEnumerable<BuildErrorEventArgs> Errors {
			get {
				return Events.OfType<BuildErrorEventArgs> ();
			}
		}

		public IEnumerable<BuildWarningEventArgs> Warnings {
			get {
				return Events.OfType<BuildWarningEventArgs> ();
			}
		}

		public LoggerVerbosity Verbosity { get; set; }
		public string Parameters { get; set; }
	}
}