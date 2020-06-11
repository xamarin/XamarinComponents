using System;

namespace ExposureNotifications {

	public static class ENAttenuationRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = byte.MaxValue;
	}

	public static class ENRiskLevelRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 7;
	}

	public static class ENRiskLevelValueRange {
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 8;
	}

	public static class ENRiskWeightRange {
		
		public static byte Default { get; } = 1;
		public static byte Min { get; } = byte.MinValue;
		public static byte Max { get; } = 100;
	}
}