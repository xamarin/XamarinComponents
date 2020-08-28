using System;
using System.Collections.Generic;

namespace Xamarin.ExposureNotifications
{
	public class DailySummaryConfiguration
	{
		public int[] AttenuationThresholds { get; set; } = new int[] { 50, 70, 90 };

		public Dictionary<DistanceEstimate, double> AttenuationWeights { get; set; } =
			new Dictionary<DistanceEstimate, double>
			{
				//[DistanceEstimate.Immediate] = 2.0,
				//[DistanceEstimate.Near] = 1.0,
				//[DistanceEstimate.Medium] = 0.5,
				//[DistanceEstimate.Other] = 0.0,
			};

		public Dictionary<Infectiousness, double> InfectiousnessWeights { get; set; } =
			new Dictionary<Infectiousness, double>
			{
				//[Infectiousness.None] = 0.0,
				//[Infectiousness.Standard] = 1.0,
				//[Infectiousness.High] = 2.0,
			};

		public Dictionary<ReportType, double> ReportTypeWeights { get; set; } =
			new Dictionary<ReportType, double>
			{
				//[ReportType.Unknown] = 0.0,
				//[ReportType.ConfirmedTest] = 0.0,
				//[ReportType.ConfirmedClinicalDiagnosis] = 0.0,
				//[ReportType.SelfReported] = 0.0,
				//[ReportType.Recursive] = 0.0,
				//[ReportType.Revoked] = 0.0,
			};

		public int DaysSinceLastExposureThreshold { get; set; } = 0;

		public Dictionary<int, Infectiousness> DaysSinceOnsetInfectiousness { get; set; } =
			new Dictionary<int, Infectiousness>()
			{
				//[-14] = Infectiousness.Standard,
				//[0] = Infectiousness.Standard,
				//[14] = Infectiousness.Standard,
			};

		public Infectiousness DefaultInfectiousness { get; set; } = Infectiousness.Standard;

		public ReportType DefaultReportType { get; set; } = ReportType.Unknown;
	}

	public class DailySummary
	{
		readonly Dictionary<ReportType, DailySummaryReport?>? reports;

		public DailySummary()
		{
		}

		public DailySummary(DateTime timestamp, DailySummaryReport summary, IDictionary<ReportType, DailySummaryReport?> reports)
		{
			Timestamp = timestamp;
			Summary = summary ?? throw new ArgumentNullException(nameof(summary));
			this.reports = new Dictionary<ReportType, DailySummaryReport?>(
				reports ?? throw new ArgumentNullException(nameof(reports)));
		}

		// The date that the exposure occurred
		public DateTime Timestamp { get; }

		// The summary of all exposures for the day
		public DailySummaryReport? Summary { get; }

		public DailySummaryReport? GetReport(ReportType reportType)
		{
			if (reports != null && reports.TryGetValue(reportType, out var report))
				return report;

			return null;
		}
	}

	public class DailySummaryReport
	{
		public DailySummaryReport()
		{
		}

		public DailySummaryReport(double maximumScore, double scoreSum, double weightedDurationSum)
		{
			MaximumScore = maximumScore;
			ScoreSum = scoreSum;
			WeightedDurationSum = weightedDurationSum;
		}

		// Highest score of all exposures for this item
		public double MaximumScore { get; }

		// Sum of scores for all exposure for this item
		public double ScoreSum { get; }

		// Sum of exposure durations weighted by their attenuation
		public double WeightedDurationSum { get; }
	}

	public class ExposureWindow
	{
		public ExposureWindow()
		{
		}

		public ExposureWindow(CalibrationConfidence calibrationConfidence, DateTime timestamp, Infectiousness infectiousness, ReportType reportType, IEnumerable<ScanInstance> scanInstances)
		{
			CalibrationConfidence = calibrationConfidence;
			Timestamp = timestamp;
			Infectiousness = infectiousness;
			ReportType = reportType;
			ScanInstances = new List<ScanInstance>(scanInstances);
		}

		public CalibrationConfidence CalibrationConfidence { get; } = CalibrationConfidence.Lowest;

		public DateTime Timestamp { get; }

		public Infectiousness Infectiousness { get; } = Infectiousness.Standard;

		public ReportType ReportType { get; } = ReportType.ConfirmedTest;

		public IReadOnlyList<ScanInstance> ScanInstances { get; } = new ScanInstance[0];
	}

	public class ScanInstance
	{
		public ScanInstance()
		{
		}

		public ScanInstance(int minimumAttenuation, int typicalAttenuation, TimeSpan sinceLastScan)
		{
			MinimumAttenuation = minimumAttenuation;
			TypicalAttenuation = typicalAttenuation;
			SinceLastScan = sinceLastScan;
		}

		public int MinimumAttenuation { get; }

		public int TypicalAttenuation { get; }

		public TimeSpan SinceLastScan { get; }
	}

	public enum CalibrationConfidence
	{
		Lowest,
		Low,
		Medium,
		High
	}

	public enum DistanceEstimate
	{
		Immediate,
		Near,
		Medium,
		Other,
	}

	public enum Infectiousness
	{
		None,
		Standard,
		High,
	}

	public enum ReportType
	{
		Unknown,

		ConfirmedTest,
		ConfirmedClinicalDiagnosis,
		SelfReported,
		Recursive,
		Revoked,
	}
}
