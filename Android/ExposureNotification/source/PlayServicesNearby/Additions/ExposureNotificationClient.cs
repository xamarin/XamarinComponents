using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidTask = Android.Gms.Tasks.Task;

namespace Android.Gms.Nearby.ExposureNotification
{
	public partial interface IExposureNotificationClient
	{
		public Task StartAsync(ExposureConfiguration config)
			=> NativeStart(config).CastTask();

		public Task StopAsync()
			=> NativeStop().CastTask();

		public async Task<IList<ExposureInformation>> GetExposureInformationAsync()
			=> await NativeExposureInformation().CastTask<JavaList<ExposureInformation>>();

		public Task<ExposureSummary> GetExposureSummaryAsync()
			=> NativeExposureSummary().CastTask<ExposureSummary>();

		public async Task<int> GetMaxDiagnosisKeyCountAsync()
			=> (int)await NativeGetMaxDiagnosisKeyCount().CastTask<Java.Lang.Integer>();

		public async Task<bool> IsEnabledAsync()
			=> (bool)await NativeIsEnabled().CastTask<Java.Lang.Boolean>();

		public Task ProvideDiagnosisKeysAsync(IList<TemporaryExposureKey> keys)
			=> NativeProvideDiagnosisKeys(keys).CastTask();

		public Task ResetAllDataAsync()
			=> NativeResetAllData().CastTask();

		public Task ResetTemporaryExposureKeyAsync()
			=> NativeResetTemporaryExposureKey().CastTask();

		public async Task<IList<TemporaryExposureKey>> GetTemporaryExposureKeyHistoryAsync()
			=> await NativeStop().CastTask<JavaList<TemporaryExposureKey>>();
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
