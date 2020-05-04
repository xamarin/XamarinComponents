using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using AndroidTask = Android.Gms.Tasks.Task;

namespace Android.Gms.Nearby.ExposureNotification
{
	public partial interface IExposureNotificationClient
	{
		public Task Start(ExposureConfiguration config)
			=> NativeStart(config).CastTask();

		public Task Stop()
			=> NativeStop().CastTask();

		public Task<ExposureInformation> GetExposureInformation()
			=> NativeExposureInformation().CastTask<ExposureInformation>();

		public Task<ExposureSummary> GetExposureSummary()
			=> NativeExposureSummary().CastTask<ExposureSummary>();

		public Task<Java.Lang.Integer> GetMaxDiagnosisKeyCount()
			=> NativeGetMaxDiagnosisKeyCount().CastTask<Java.Lang.Integer>();

		public Task<Java.Lang.Boolean> IsEnabled()
			=> NativeIsEnabled().CastTask<Java.Lang.Boolean>();

		public Task ProvideDiagnosisKeys(List<TemporaryExposureKey> keys)
			=> NativeProvideDiagnosisKeys(keys).CastTask();

		public Task ResetAllData()
			=> NativeResetAllData().CastTask();

		public Task ResetTemporaryExposureKey()
			=> NativeResetTemporaryExposureKey().CastTask();

		public Task<JavaList<TemporaryExposureKey>> GetTemporaryExposureKeyHistory()
			=> NativeStop().CastTask<JavaList<TemporaryExposureKey>>();
	}

	internal static class GoogleTaskExtensions
	{
		public static Task CastTask(this AndroidTask androidTask)
		{
			var tcs = new TaskCompletionSource<bool>();

			androidTask.AddOnCompleteListener(new MyCompleteListener(
				t =>
				{
					if (t.Exception == null)
						tcs.TrySetResult(false);
					else
						tcs.TrySetException(t.Exception);
				}
			));

			return tcs.Task;
		}

		public static Task<TResult> CastTask<TResult>(this AndroidTask androidTask)
			where TResult : Java.Lang.Object
		{
			var tcs = new TaskCompletionSource<TResult>();

			androidTask.AddOnCompleteListener(new MyCompleteListener(
				t =>
				{
					if (t.Exception == null)
						tcs.TrySetResult(t.Result.JavaCast<TResult>());
					else
						tcs.TrySetException(t.Exception);
				}));

			return tcs.Task;
		}

		class MyCompleteListener : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
		{
			public MyCompleteListener(Action<AndroidTask> onComplete)
				=> OnCompleteHandler = onComplete;

			public Action<AndroidTask> OnCompleteHandler { get; }

			public void OnComplete(AndroidTask task)
				=> OnCompleteHandler?.Invoke(task);
		}
	}

}