using System;

namespace Xamarin.ExposureNotifications
{
	public class TemporaryExposureKey
	{
		// static readonly DateTimeOffset epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		public TemporaryExposureKey()
		{
		}

		public TemporaryExposureKey(byte[] keyData, DateTimeOffset rollingStart, TimeSpan rollingDuration, RiskLevel transmissionRisk)
		{
			Key = keyData;
			RollingStart = rollingStart;
			RollingDuration = rollingDuration;
			TransmissionRiskLevel = transmissionRisk;
		}

		internal TemporaryExposureKey(byte[] keyData, long rollingStart, TimeSpan rollingDuration, RiskLevel transmissionRisk)
		{
			Key = keyData;
			RollingStart = DateTimeOffset.FromUnixTimeSeconds(rollingStart * (60 * 10));
			RollingDuration = rollingDuration;
			TransmissionRiskLevel = transmissionRisk;
		}

		public byte[]? Key { get; set; }

		public DateTimeOffset RollingStart { get; set; }

		public TimeSpan RollingDuration { get; set; }

		public RiskLevel TransmissionRiskLevel { get; set; }

		// public string KeyBase64 =>
		//	Convert.ToBase64String(Key);
		// public long RollingStartTenMinuteIntervalsSinceEpoch =>
		//	(long)((RollingStart - epoch).TotalMinutes / 10);
		// public int RollingDurationTenMinuteIntervals =>
		//	(int)(RollingDuration.TotalMinutes / 10);
	}
}
