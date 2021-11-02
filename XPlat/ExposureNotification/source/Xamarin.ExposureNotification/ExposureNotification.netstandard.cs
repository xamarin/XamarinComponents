using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Xamarin.ExposureNotifications
{
	public static partial class ExposureNotification
	{
		static object Instance => null;

		static void PlatformInit()
			=> throw new PlatformNotSupportedException();

		static Task PlatformStart()
			=> throw new PlatformNotSupportedException();

		static Task PlatformStop()
			=> throw new PlatformNotSupportedException();

		static Task<bool> PlatformIsEnabled()
			=> throw new PlatformNotSupportedException();

		static Task PlatformScheduleFetch()
			=> throw new PlatformNotSupportedException();

		static Task<(ExposureDetectionSummary, IEnumerable<ExposureInfo>)> PlatformDetectExposuresAsync(IEnumerable<TemporaryExposureKey> diagnosisKeys, CancellationToken cancellationToken)
			=> throw new PlatformNotSupportedException();

		static Task<IEnumerable<TemporaryExposureKey>> PlatformGetTemporaryExposureKeys()
			=> throw new PlatformNotSupportedException();

		static Task<Status> PlatformGetStatusAsync()
			=> throw new PlatformNotSupportedException();
	}
}
