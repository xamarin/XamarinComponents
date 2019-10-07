// Copyright (c) 2015-2016 Xamarin Inc.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using Xunit;

namespace Xamarin.ContentPipeline.Tests
{
	public class TestsBase : IDisposable
	{
		string originalWorkingDir;
		string tempDir;

		public TestsBase ()
		{
			originalWorkingDir = Environment.CurrentDirectory;
			tempDir = Path.GetTempFileName ();
			File.Delete (tempDir);
			Directory.CreateDirectory (tempDir);
			Environment.CurrentDirectory = tempDir;
		}

        public bool IsWindows
            => System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

        public void Dispose ()
		{
			Environment.CurrentDirectory = originalWorkingDir;
			Directory.Delete (tempDir, true);

			//HACK: touching BuildManager causes Mono to initialize its DefaultBuildManagerand never shut it down
			StopMonoBuildManager (BuildManager.DefaultBuildManager);
		}

		public string TempDir { get { return tempDir; } }

		protected string GetTempPath (params string [] components)
		{
			return Path.Combine (tempDir, Path.Combine (components));
		}

		protected static void AssertNoMessagesOrWarnings (MSBuildTestLogger logger, params string[] ignorePatterns)
		{
            // Skip some msbuild test harness warnings that we truly don't care about
            var errors = logger.Errors.ToList();
            errors.RemoveAll(e => ignorePatterns.Any(p => e.Message.Contains(p)));
            var warnings = logger.Warnings.ToList();
            warnings.RemoveAll(w => ignorePatterns.Any(p => w.Message.Contains(p)));

            BuildEventArgs err = errors.FirstOrDefault ();

            if (err == null) {
				err = warnings.FirstOrDefault ();
				if (err == null) {
					return;
				}
			}
			throw new Exception (err.Message);
		}

		// work around Mono's ProjectInstance.Build using a separate BuildManager and not shutting it down
		// Mono's BuildManager has a non-background worker thread and doesn't shut it down automatically
		protected bool BuildProject (
			ProjectCollection projects, ProjectInstance project, string [] targets, IEnumerable<ILogger> loggers,
			IEnumerable<ForwardingLoggerRecord> remoteLoggers, out IDictionary<string, TargetResult> targetOutputs)
		{
			var parameters = new BuildParameters (projects) {
				Loggers = loggers,
				ForwardingLoggers = remoteLoggers,
				DefaultToolsVersion = projects.DefaultToolsVersion,
			};
			var data = new BuildRequestData (project, targets ?? project.DefaultTargets.ToArray ());
			var result = BuildManager.DefaultBuildManager.Build (parameters, data);
			targetOutputs = result.ResultsByTarget;
			return result.OverallResult == BuildResultCode.Success;
		}

		static void StopMonoBuildManager (BuildManager buildManager)
		{
			try {
				const BindingFlags instanceFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
				PropertyInfo nodeManagerProp = typeof (BuildManager).GetProperty ("BuildNodeManager", instanceFlags);
				var nodeManager = nodeManagerProp.GetValue (buildManager);
				var stopMeth = nodeManager.GetType ().GetMethod ("Stop", instanceFlags);
				stopMeth.Invoke (nodeManager, new object[0]);
			} catch {
			}
		}

		protected bool BuildProject (
			ProjectCollection projects, ProjectInstance project, string target, ILogger logger)
		{
			IDictionary<string, TargetResult> targetOutputs;
			return BuildProject (projects, project, new [] { target }, new [] { logger }, new ForwardingLoggerRecord[0], out targetOutputs);
		}
	}
}