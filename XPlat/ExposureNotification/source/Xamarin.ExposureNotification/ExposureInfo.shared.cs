using System;
using System.ComponentModel;

namespace Xamarin.ExposureNotifications
{
	public class ExposureInfo
	{
		public ExposureInfo()
		{
		}

		public ExposureInfo(DateTime timestamp, TimeSpan duration, int attenuationValue, int totalRiskScore, RiskLevel riskLevel)
		{
			Timestamp = timestamp;
			Duration = duration;
			AttenuationValue = attenuationValue;
			TotalRiskScore = totalRiskScore;
			TransmissionRiskLevel = riskLevel;
		}

		// When the contact occurred
		public DateTime Timestamp { get; }

		// How long the contact lasted in 5 min increments
		public TimeSpan Duration { get; }

		public int AttenuationValue { get; }

		public int TotalRiskScore { get; }

		public RiskLevel TransmissionRiskLevel { get; }
	}

	public class ExposureDetectionSummary
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ExposureDetectionSummary(int daysSinceLastExposure, ulong matchedKeyCount, byte maximumRiskScore)
			: this(daysSinceLastExposure, matchedKeyCount, (int)maximumRiskScore, null, 0)
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public ExposureDetectionSummary(int daysSinceLastExposure, ulong matchedKeyCount, byte maximumRiskScore, TimeSpan[]? attenuationDurations, int summationRiskScore)
			: this(daysSinceLastExposure, matchedKeyCount, (int)maximumRiskScore, attenuationDurations, summationRiskScore)
		{
		}

		public ExposureDetectionSummary(int daysSinceLastExposure, ulong matchedKeyCount, int highestRiskScore)
			: this(daysSinceLastExposure, matchedKeyCount, highestRiskScore, null, 0)
		{
		}

		public ExposureDetectionSummary(int daysSinceLastExposure, ulong matchedKeyCount, int highestRiskScore, TimeSpan[]? attenuationDurations, int summationRiskScore)
		{
			DaysSinceLastExposure = daysSinceLastExposure;
			MatchedKeyCount = matchedKeyCount;
			HighestRiskScore = highestRiskScore;
			AttenuationDurations = attenuationDurations;
			SummationRiskScore = summationRiskScore;
		}

		public int DaysSinceLastExposure { get; }

		public ulong MatchedKeyCount { get; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use HighestRiskScore instead.")]
		public byte MaximumRiskScore => (byte)HighestRiskScore;

		public int HighestRiskScore { get; }

		public TimeSpan[]? AttenuationDurations { get; }

		public int SummationRiskScore { get; }
	}

	public enum RiskLevel
	{
		Invalid = 0,
		Lowest = 1,
		Low = 2,
		MediumLow = 3,
		Medium = 4,
		MediumHigh = 5,
		High = 6,
		VeryHigh = 7,
		Highest = 8
	}
}
