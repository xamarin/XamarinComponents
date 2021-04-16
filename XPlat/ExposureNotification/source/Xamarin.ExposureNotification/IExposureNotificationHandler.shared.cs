using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.ExposureNotifications
{
	public interface IExposureNotificationHandler
	{
		string UserExplanation { get; }

		Task<Configuration> GetConfigurationAsync();

		// Go fetch the keys from your server
		Task FetchExposureKeyBatchFilesFromServerAsync(Func<IEnumerable<string>, Task> submitBatches, CancellationToken cancellationToken);

		// Might be exposed, check and alert user if necessary
		Task ExposureDetectedAsync(ExposureDetectionSummary summary, Func<Task<IEnumerable<ExposureInfo>>> getExposureInfo);

		Task UploadSelfExposureKeysToServerAsync(IEnumerable<TemporaryExposureKey> temporaryExposureKeys);
	}

	public interface IExposureNotificationDailySummaryHandler : IExposureNotificationHandler
	{
		Task<DailySummaryConfiguration> GetDailySummaryConfigurationAsync();

		// Might be exposed, check and alert user if necessary
		Task ExposureStateUpdatedAsync(IEnumerable<ExposureWindow> windows, IEnumerable<DailySummary>? summaries);
	}

	public interface INativeImplementation
	{
		Task StartAsync();

		Task StopAsync();

		Task<bool> IsEnabledAsync();

		Task<(ExposureDetectionSummary summary, Func<Task<IEnumerable<ExposureInfo>>> getInfo)> DetectExposuresAsync(IEnumerable<string> files);

		Task<IEnumerable<TemporaryExposureKey>> GetSelfTemporaryExposureKeysAsync();

		Task<Status> GetStatusAsync();
	}

	public interface IDailySummaryNativeImplementation : INativeImplementation
	{
		Task<(IEnumerable<DailySummary> summaries, Func<Task<IEnumerable<ExposureWindow>>> getExposureWindows)> DetectExposureWindowsAsync(IEnumerable<string> files);
	}
}
