using System;
using Foundation;

namespace YouTube.Player
{
	public partial class PlayerView
	{
		public float[] AvailablePlaybackRates
		{
			get
			{
				NSArray nsPlaybackRates = _AvailablePlaybackRates();

				if (nsPlaybackRates == null)
					return null;

				float[] playbackRates = new float[nsPlaybackRates.Count];

				for (nuint i = 0; i < nsPlaybackRates.Count; i++)
					playbackRates[i] = nsPlaybackRates.GetItem<NSNumber>(i).FloatValue;

				return playbackRates;
			}
		}

		public PlaybackQuality[] AvailableQualityLevels
		{
			get
			{
				NSArray nsQualityLevels = _AvailableQualityLevels();

				if (nsQualityLevels == null)
					return null;

				PlaybackQuality[] qualityLevels = new PlaybackQuality[nsQualityLevels.Count];

				for (nuint i = 0; i < nsQualityLevels.Count; i++)
				{
					if (IntPtr.Size == 8)
						qualityLevels[i] = (PlaybackQuality)nsQualityLevels.GetItem<NSNumber>(i).Int64Value;
					else
						qualityLevels[i] = (PlaybackQuality)nsQualityLevels.GetItem<NSNumber>(i).Int32Value;
				}

				return qualityLevels;
			}
		}
	}
}
